#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if WEBVIEW2_AVAILABLE

using Microsoft.Web.WebView2.Core;

namespace Krypton.Utilities;

/// <summary>
/// Redirect handler that intercepts navigations in a WebView2 control and extracts
/// the OAuth2 callback parameters.
/// </summary>
public sealed class OAuth2WebView2RedirectHandler : IOAuth2RedirectHandler
{
    private readonly CoreWebView2 _coreWebView;
    private readonly TaskCompletionSource<OAuth2AuthorizationResult> _tcs = new();

    /// <summary>
    /// Initializes a new instance that observes navigations in the given WebView2 core.
    /// </summary>
    /// <param name="coreWebView">The WebView2 core to observe. Must not be null.</param>
    public OAuth2WebView2RedirectHandler(CoreWebView2 coreWebView)
    {
        _coreWebView = coreWebView ?? throw new ArgumentNullException(nameof(coreWebView));
    }

    /// <inheritdoc />
    public Task<OAuth2AuthorizationResult> WaitForRedirectAsync(string redirectUri, CancellationToken cancellationToken)
    {
        var redirect = (redirectUri ?? string.Empty).TrimEnd('/');
        if (string.IsNullOrEmpty(redirect))
        {
            _tcs.TrySetResult(new OAuth2AuthorizationResult(false, null, null, "Redirect URI is empty."));
            return _tcs.Task;
        }

        void OnNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            var uri = e.Uri ?? string.Empty;
            if (!IsRedirectMatch(uri, redirect))
            {
                return;
            }

            e.Cancel = true;
            _coreWebView.NavigationStarting -= OnNavigationStarting;

            string? code = null;
            string? state = null;
            string? error = null;
            string? errorDescription = null;

            try
            {
                var queryStart = uri.IndexOf('?');
                if (queryStart >= 0 && queryStart < uri.Length - 1)
                {
                    var query = uri.Substring(queryStart + 1);
                    foreach (var part in query.Split('&'))
                    {
                        var eq = part.IndexOf('=');
                        if (eq < 0)
                        {
                            continue;
                        }

                        var key = Uri.UnescapeDataString(part.Substring(0, eq).Replace('+', ' '));
                        var value = Uri.UnescapeDataString(part.Substring(eq + 1).Replace('+', ' '));

                        switch (key.ToUpperInvariant())
                        {
                            case "CODE":
                                code = value;
                                break;
                            case "STATE":
                                state = value;
                                break;
                            case "ERROR":
                                error = value;
                                break;
                            case "ERROR_DESCRIPTION":
                                errorDescription = value;
                                break;
                        }
                    }
                }
            }
            catch
            {
                _tcs.TrySetResult(new OAuth2AuthorizationResult(false, null, null, "Failed to parse redirect URL."));
                return;
            }

            if (!string.IsNullOrEmpty(error))
            {
                _tcs.TrySetResult(new OAuth2AuthorizationResult(false, null, null, errorDescription ?? error));
            }
            else if (!string.IsNullOrEmpty(code))
            {
                _tcs.TrySetResult(new OAuth2AuthorizationResult(true, code, state, null));
            }
            else
            {
                _tcs.TrySetResult(new OAuth2AuthorizationResult(false, null, null, "No authorization code in redirect."));
            }
        }

        cancellationToken.Register(() => _tcs.TrySetCanceled());
        _coreWebView.NavigationStarting += OnNavigationStarting;

        return _tcs.Task;
    }

    private static bool IsRedirectMatch(string url, string redirect)
    {
        if (string.IsNullOrEmpty(url))
        {
            return false;
        }

        var uri = url.Trim();
        if (!uri.StartsWith(redirect, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (uri.Length == redirect.Length)
        {
            return true;
        }

        var next = uri[redirect.Length];
        return next is '?' or '#' or '/';
    }
}

#endif
