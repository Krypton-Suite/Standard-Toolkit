#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if WEBVIEW2_AVAILABLE

namespace Krypton.Utilities;

/// <summary>
/// Default browser host that uses KryptonWebView2 in a Krypton-styled form.
/// </summary>
public sealed class OAuth2WebView2BrowserHost : IOAuth2BrowserHost
{
    /// <inheritdoc />
    public Task<OAuth2AuthorizationResult> LaunchAsync(
        string authorizationUrl,
        string redirectUri,
        string title,
        IWin32Window? owner,
        CancellationToken cancellationToken) =>
        KryptonOAuth2Login.ShowAsync(owner, authorizationUrl, redirectUri, title);
}

#endif
