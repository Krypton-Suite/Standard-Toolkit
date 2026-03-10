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
/// Browser host that opens the authorization URL in the system default browser and uses
/// an <see cref="IOAuth2RedirectHandler"/> to receive the callback.
/// </summary>
/// <remarks>
/// <para>
/// Use with <see cref="OAuth2HttpListenerRedirectHandler"/> when redirect_uri is http://localhost:port/callback,
/// or implement <see cref="IOAuth2RedirectHandler"/> for custom protocol handlers.
/// </para>
/// </remarks>
public sealed class OAuth2SystemBrowserHost : IOAuth2BrowserHost
{
    private readonly IOAuth2RedirectHandler _redirectHandler;

    /// <summary>
    /// Initializes a new instance with the specified redirect handler.
    /// </summary>
    /// <param name="redirectHandler">Handler that waits for and parses the OAuth2 redirect.</param>
    public OAuth2SystemBrowserHost(IOAuth2RedirectHandler redirectHandler)
    {
        _redirectHandler = redirectHandler ?? throw new ArgumentNullException(nameof(redirectHandler));
    }

    /// <inheritdoc />
    public async Task<OAuth2AuthorizationResult> LaunchAsync(
        string authorizationUrl,
        string redirectUri,
        string title,
        IWin32Window? owner,
        CancellationToken cancellationToken)
    {
        var redirectTask = _redirectHandler.WaitForRedirectAsync(redirectUri, cancellationToken);

        Process.Start(new ProcessStartInfo(authorizationUrl)
        {
            UseShellExecute = true
        });

        return await redirectTask.ConfigureAwait(true);
    }
}
