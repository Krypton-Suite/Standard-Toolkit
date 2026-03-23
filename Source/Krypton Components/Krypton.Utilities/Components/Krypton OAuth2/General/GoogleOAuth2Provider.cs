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
/// OAuth2 provider for Google.
/// </summary>
public sealed class GoogleOAuth2Provider : IOAuth2Provider
{
    /// <inheritdoc />
    public string Name => "Google";

    /// <inheritdoc />
    public void ApplyTo(OAuth2PkceOptions options)
    {
        options.Authority = "https://accounts.google.com";
        options.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        options.TokenEndpoint = "https://oauth2.googleapis.com/token";
    }
}
