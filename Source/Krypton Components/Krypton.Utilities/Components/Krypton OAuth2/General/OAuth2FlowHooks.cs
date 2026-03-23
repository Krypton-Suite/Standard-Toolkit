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
/// Optional hooks invoked at key points in the OAuth2 flow. Use for logging, metrics, or custom logic.
/// </summary>
public class OAuth2FlowHooks
{
    /// <summary>
    /// Called before opening the sign-in browser. Receives the authorization URL and state.
    /// </summary>
    public Func<string, string?, CancellationToken, Task>? BeforeAuthorize { get; set; }

    /// <summary>
    /// Called after the user completes sign-in and the authorization code is received, before token exchange.
    /// </summary>
    public Func<string, string?, CancellationToken, Task>? AfterAuthorizationCodeReceived { get; set; }

    /// <summary>
    /// Called after successful token exchange (authorization code or refresh).
    /// </summary>
    public Func<OAuth2TokenResponse, CancellationToken, Task>? AfterTokensReceived { get; set; }

    /// <summary>
    /// Called after a successful token refresh.
    /// </summary>
    public Func<OAuth2TokenResponse, CancellationToken, Task>? AfterRefresh { get; set; }

    /// <summary>
    /// Invokes <see cref="BeforeAuthorize"/> if set.
    /// </summary>
    internal async Task InvokeBeforeAuthorizeAsync(string authorizationUrl, string? state, CancellationToken ct)
    {
        if (BeforeAuthorize != null)
        {
            await BeforeAuthorize(authorizationUrl, state, ct).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Invokes <see cref="AfterAuthorizationCodeReceived"/> if set.
    /// </summary>
    internal async Task InvokeAfterCodeReceivedAsync(string? code, string? state, CancellationToken ct)
    {
        if (AfterAuthorizationCodeReceived != null)
        {
            if (code != null)
            {
                await AfterAuthorizationCodeReceived(code, state, ct).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Invokes <see cref="AfterTokensReceived"/> if set.
    /// </summary>
    internal async Task InvokeAfterTokensReceivedAsync(OAuth2TokenResponse tokens, CancellationToken ct)
    {
        if (AfterTokensReceived != null)
        {
            await AfterTokensReceived(tokens, ct).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Invokes <see cref="AfterRefresh"/> if set.
    /// </summary>
    internal async Task InvokeAfterRefreshAsync(OAuth2TokenResponse tokens, CancellationToken ct)
    {
        if (AfterRefresh != null)
        {
            await AfterRefresh(tokens, ct).ConfigureAwait(false);
        }
    }
}
