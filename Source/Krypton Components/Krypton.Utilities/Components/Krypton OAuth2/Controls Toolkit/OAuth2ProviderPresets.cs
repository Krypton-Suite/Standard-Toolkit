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
/// Factory methods for common OAuth2 provider configurations.
/// </summary>
public static class OAuth2ProviderPresets
{
    /// <summary>
    /// Creates options for Microsoft Azure AD / Entra ID.
    /// </summary>
    public static OAuth2PkceOptions AzureAd(string clientId, string redirectUri, string tenantId = "common", string scopes = "openid profile")
    {
        var options = new OAuth2PkceOptions
        {
            Authority = $"https://login.microsoftonline.com/{tenantId.TrimEnd('/')}/v2.0",
            ClientId = clientId,
            RedirectUri = redirectUri,
            Scopes = scopes
        };
        new AzureAdOAuth2Provider().ApplyTo(options);
        return options;
    }

    /// <summary>
    /// Creates options for Google OAuth2.
    /// </summary>
    public static OAuth2PkceOptions Google(string clientId, string redirectUri, string scopes = "openid email profile")
    {
        var options = new OAuth2PkceOptions { ClientId = clientId, RedirectUri = redirectUri, Scopes = scopes };
        new GoogleOAuth2Provider().ApplyTo(options);
        return options;
    }

    /// <summary>
    /// Creates options for GitHub OAuth2.
    /// </summary>
    public static OAuth2PkceOptions GitHub(string clientId, string redirectUri, string scopes = "read:user")
    {
        var options = new OAuth2PkceOptions { ClientId = clientId, RedirectUri = redirectUri, Scopes = scopes.Replace(",", " ") };
        new GitHubOAuth2Provider().ApplyTo(options);
        return options;
    }

    /// <summary>
    /// Creates options for a custom provider by applying the given <see cref="IOAuth2Provider"/>.
    /// </summary>
    public static OAuth2PkceOptions Custom(IOAuth2Provider provider, string clientId, string redirectUri, string scopes = "openid")
    {
        var options = new OAuth2PkceOptions { ClientId = clientId, RedirectUri = redirectUri, Scopes = scopes };
        provider.ApplyTo(options);
        return options;
    }
}
