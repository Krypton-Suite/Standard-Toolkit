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
/// OAuth2 provider for Microsoft Azure AD / Entra ID.
/// </summary>
public sealed class AzureAdOAuth2Provider : IOAuth2Provider
{
    #region Internal Fields

    private readonly string _tenantId;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance for the specified tenant.
    /// </summary>
    /// <param name="tenantId">Tenant ID, "common" for multi-tenant, "consumers" for personal Microsoft accounts. Default is "common".</param>
    public AzureAdOAuth2Provider(string tenantId = "common")
    {
        _tenantId = tenantId ?? "common";
    }

    #endregion

    #region IOAuth2Provider Implementation

    /// <inheritdoc />
    public string Name => "Azure AD";

    /// <inheritdoc />
    public void ApplyTo(OAuth2PkceOptions options)
    {
        var authority = (options.Authority ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(authority))
        {
            authority = $"https://login.microsoftonline.com/{_tenantId.TrimEnd('/')}/v2.0";
        }

        var baseUrl = authority.TrimEnd('/');
        options.Authority = baseUrl;
        options.AuthorizationEndpoint ??= $"{baseUrl}/oauth2/v2.0/authorize";
        options.TokenEndpoint ??= $"{baseUrl}/oauth2/v2.0/token";
        options.ResponseMode ??= "query";
    }

    #endregion
}