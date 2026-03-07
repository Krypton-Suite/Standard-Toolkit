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
/// OAuth2 provider for GitHub.
/// </summary>
public sealed class GitHubOAuth2Provider : IOAuth2Provider
{
    /// <inheritdoc />
    public string Name => "GitHub";

    /// <inheritdoc />
    public void ApplyTo(OAuth2PkceOptions options)
    {
        options.Authority = "https://github.com";
        options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        options.TokenEndpoint = "https://github.com/login/oauth/access_token";
    }
}
