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
/// Abstraction for an OAuth2/OIDC identity provider. Implement this interface to integrate
/// custom providers or to apply provider-specific endpoint and parameter configuration.
/// </summary>
/// <remarks>
/// <para>
/// Use <see cref="ApplyTo"/> to configure <see cref="OAuth2PkceOptions"/> with the provider's
/// authorization and token endpoints, default scopes, and any provider-specific parameters.
/// Built-in implementations include <see cref="AzureAdOAuth2Provider"/>, <see cref="GoogleOAuth2Provider"/>,
/// and <see cref="GitHubOAuth2Provider"/>.
/// </para>
/// </remarks>
public interface IOAuth2Provider
{
    /// <summary>
    /// Gets a display name for the provider (e.g., "Azure AD", "Google").
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Applies provider-specific configuration to the options, including endpoints and defaults.
    /// </summary>
    /// <param name="options">The options to configure. Must have <see cref="OAuth2PkceOptions.ClientId"/> and <see cref="OAuth2PkceOptions.RedirectUri"/> set before calling.</param>
    void ApplyTo(OAuth2PkceOptions options);
}
