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
/// Handles receiving the OAuth2 redirect (callback). Implement to support custom redirect schemes
/// such as system browser with HTTP listener, custom URI protocol handlers, or in-process WebView interception.
/// </summary>
/// <remarks>
/// <para>
/// When using the system browser, the flow is: open the authorization URL, then call
/// <see cref="WaitForRedirectAsync"/> to listen for the callback. The implementation might start
/// an HTTP listener on localhost or register a custom protocol handler.
/// </para>
/// <para>
/// <c>OAuth2WebView2RedirectHandler</c> uses WebView2's NavigationStarting to intercept
/// redirects within an embedded browser.
/// </para>
/// </remarks>
public interface IOAuth2RedirectHandler
{
    /// <summary>
    /// Waits for the OAuth2 provider to redirect to <paramref name="redirectUri"/> and returns
    /// the parsed authorization result (code, state, or error).
    /// </summary>
    /// <param name="redirectUri">The redirect URI. The implementation must detect when a request/navigation targets this URI.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authorization result with code or error.</returns>
    Task<OAuth2AuthorizationResult> WaitForRedirectAsync(string redirectUri, CancellationToken cancellationToken);
}
