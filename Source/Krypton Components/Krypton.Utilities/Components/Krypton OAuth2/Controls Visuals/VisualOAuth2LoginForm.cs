#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#region Using Directives

#if WEBVIEW2_AVAILABLE
using Microsoft.Web.WebView2.Core;
#endif

#endregion

namespace Krypton.Utilities;

/// <summary>
/// A Krypton-styled form that hosts a WebView2 control for OAuth2 authorization.
/// Intercepts the redirect to the configured redirect URI and extracts the authorization code.
/// </summary>
internal sealed partial class VisualOAuth2LoginForm : KryptonForm
{
#if WEBVIEW2_AVAILABLE
    private readonly string _authorizationUrl;
    private readonly string _redirectUri;
#endif
    private readonly TaskCompletionSource<OAuth2AuthorizationResult> _tcs = new();

    public VisualOAuth2LoginForm(string authorizationUrl, string redirectUri, string title = "Sign in")
    {
        InitializeComponent();
#if WEBVIEW2_AVAILABLE
        _authorizationUrl = authorizationUrl;
        _redirectUri = redirectUri.TrimEnd('/');
#endif
        Text = title;
    }

    /// <summary>
    /// Starts the authorization flow and returns when the user completes or cancels.
    /// </summary>
    public Task<OAuth2AuthorizationResult> RunAsync(IWin32Window? owner)
    {
#if WEBVIEW2_AVAILABLE
        _ = InitializeAndNavigateAsync();
#else
        _tcs.TrySetResult(new OAuth2AuthorizationResult(false, null, null, "WebView2 is required for OAuth2 sign-in."));
#endif
        return _tcs.Task;
    }

#if WEBVIEW2_AVAILABLE
    private async Task InitializeAndNavigateAsync()
    {
        try
        {
            await kryptonWebView21!.EnsureCoreWebView2Async().ConfigureAwait(true);

            if (kryptonWebView21.CoreWebView2 != null)
            {
                kryptonWebView21.CoreWebView2.NavigationCompleted += OnNavigationCompleted;
                var handler = new OAuth2WebView2RedirectHandler(kryptonWebView21.CoreWebView2);
                kryptonWebView21.CoreWebView2.Navigate(_authorizationUrl);
                var result = await handler.WaitForRedirectAsync(_redirectUri, CancellationToken.None).ConfigureAwait(true);
                SetResult(result);
                Close();
            }
            else
            {
                SetResult(new OAuth2AuthorizationResult(false, null, null, "WebView2 core is not available."));
            }
        }
        catch (Exception ex)
        {
            SetResult(new OAuth2AuthorizationResult(false, null, null, ex.Message));
        }
    }

    private void OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        if (kryptonStatusLabel != null)
        {
            kryptonStatusLabel.Values.Text = e.IsSuccess ? "Sign in with your account" : $"Navigation failed: {e.WebErrorStatus}";
        }
    }

    private void SetResult(OAuth2AuthorizationResult result)
    {
        _tcs.TrySetResult(result);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!_tcs.Task.IsCompleted)
        {
            SetResult(new OAuth2AuthorizationResult(false, null, null, "Sign-in was cancelled."));
        }

        if (kryptonWebView21?.CoreWebView2 != null)
        {
            kryptonWebView21.CoreWebView2.NavigationCompleted -= OnNavigationCompleted;
        }

        base.OnFormClosing(e);
    }
#endif
}
