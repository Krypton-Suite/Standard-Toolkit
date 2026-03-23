#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Abstraction for the sign-in browser or UI. Implement this interface to use a custom
/// sign-in flow (e.g., system browser, embedded WebView, or custom form).
/// </summary>
/// <remarks>
/// <para>
/// When provided to <see cref="OAuth2PkceClient"/>, <see cref="LaunchAsync"/> is called during
/// <see cref="OAuth2PkceClient.AuthorizeWithBrowserAsync"/> instead of the default KryptonWebView2-based form.
/// Use this to open the system browser, a custom dialog, or integrate with platform-specific auth.
/// </para>
/// <para>
/// The default implementation (when no host is set) uses <see cref="VisualOAuth2LoginForm"/> when WebView2 is available.
/// </para>
/// </remarks>
public interface IOAuth2BrowserHost
{
    /// <summary>
    /// Launches the sign-in UI, navigates to the authorization URL, and returns when the user
    /// completes or cancels. The implementation must intercept the redirect to <paramref name="redirectUri"/>
    /// and extract the authorization code (or error) from the query string.
    /// </summary>
    /// <param name="authorizationUrl">The full authorization URL to open.</param>
    /// <param name="redirectUri">The redirect URI. When the provider redirects here, extract the code from the query.</param>
    /// <param name="title">Optional title for the sign-in window.</param>
    /// <param name="owner">Optional parent window.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A result with <see cref="OAuth2AuthorizationResult.Success"/> true and <see cref="OAuth2AuthorizationResult.Code"/> set on success.</returns>
    Task<OAuth2AuthorizationResult> LaunchAsync(
        string authorizationUrl,
        string redirectUri,
        string title,
        IWin32Window? owner,
        CancellationToken cancellationToken);
}
