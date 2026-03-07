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
/// Represents the response from an OAuth2 token endpoint (authorization code exchange or token refresh).
/// </summary>
public class OAuth2TokenResponse
{
    /// <summary>
    /// Gets or sets the access token for API authorization.
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the refresh token, if the provider issued one.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the token type, typically "Bearer".
    /// </summary>
    public string? TokenType { get; set; }

    /// <summary>
    /// Gets or sets the number of seconds until the access token expires.
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the optional ID token (JWT) from OpenID Connect.
    /// </summary>
    public string? IdToken { get; set; }

    /// <summary>
    /// Gets or sets the scopes that were granted.
    /// </summary>
    public string? Scope { get; set; }

    /// <summary>
    /// Gets whether a refresh token is available for obtaining new access tokens.
    /// </summary>
    public bool HasRefreshToken => !string.IsNullOrEmpty(RefreshToken);
}
