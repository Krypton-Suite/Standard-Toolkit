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
                ParseAuthorizationQuery(uri, ref code, ref state, ref error, ref errorDescription);
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

    private static void ParseAuthorizationQuery(string uri, ref string? code, ref string? state, ref string? error, ref string? errorDescription)
    {
        if (string.IsNullOrEmpty(uri))
        {
            return;
        }

#if NET9_0_OR_GREATER
        ReadOnlySpan<char> uriSpan = uri.AsSpan();
        int queryStart = uriSpan.IndexOf('?');
        if (queryStart < 0 || queryStart >= uriSpan.Length - 1)
        {
            return;
        }

        ReadOnlySpan<char> querySpan = uriSpan.Slice(queryStart + 1);
        while (!querySpan.IsEmpty)
        {
            int ampIndex = querySpan.IndexOf('&');
            ReadOnlySpan<char> partSpan;

            if (ampIndex >= 0)
            {
                partSpan = querySpan.Slice(0, ampIndex);
                querySpan = querySpan.Slice(ampIndex + 1);
            }
            else
            {
                partSpan = querySpan;
                querySpan = ReadOnlySpan<char>.Empty;
            }

            int equalsIndex = partSpan.IndexOf('=');
            if (equalsIndex < 0)
            {
                continue;
            }

            string key = DecodeQueryPart(partSpan.Slice(0, equalsIndex));
            string value = DecodeQueryPart(partSpan.Slice(equalsIndex + 1));
            AssignAuthorizationField(key, value, ref code, ref state, ref error, ref errorDescription);
        }
#else
        var queryStart = uri.IndexOf('?');
        if (queryStart < 0 || queryStart >= uri.Length - 1)
        {
            return;
        }

        var query = uri.Substring(queryStart + 1);
        foreach (var part in query.Split('&'))
        {
            var equalsIndex = part.IndexOf('=');
            if (equalsIndex < 0)
            {
                continue;
            }

            string key = Uri.UnescapeDataString(part.Substring(0, equalsIndex).Replace('+', ' '));
            string value = Uri.UnescapeDataString(part.Substring(equalsIndex + 1).Replace('+', ' '));
            AssignAuthorizationField(key, value, ref code, ref state, ref error, ref errorDescription);
        }
#endif
    }

    private static void AssignAuthorizationField(string key, string value, ref string? code, ref string? state, ref string? error, ref string? errorDescription)
    {
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

#if NET9_0_OR_GREATER
    private static string DecodeQueryPart(ReadOnlySpan<char> valueSpan)
    {
        return Uri.UnescapeDataString(valueSpan.ToString().Replace('+', ' '));
    }
#endif
}

#endif
