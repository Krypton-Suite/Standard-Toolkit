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
/// Configuration options for OAuth2 with PKCE (Proof Key for Code Exchange) authentication.
/// </summary>
/// <remarks>
/// <para>
/// Use this class to configure the OAuth2 authorization code flow with PKCE.
/// PKCE is recommended for public clients (e.g., desktop/mobile apps) that cannot securely store a client secret.
/// </para>
/// <para>
/// Required properties: <see cref="Authority"/>, <see cref="ClientId"/>, and <see cref="RedirectUri"/>.
/// The authority is typically the base URL of your OAuth2/OpenID Connect provider (e.g., "https://login.microsoftonline.com/tenant-id/v2.0").
/// </para>
/// </remarks>
public class OAuth2PkceOptions
{
    /// <summary>
    /// Gets or sets the OAuth2 authority (identity provider) base URL.
    /// </summary>
    /// <value>
    /// The authority URL, e.g. "https://login.microsoftonline.com/{tenant}/v2.0" or "https://accounts.google.com".
    /// Trailing slashes are normalized.
    /// </value>
    public string Authority { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the client identifier (application ID) registered with the identity provider.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the redirect URI. Must match the value registered with the identity provider.
    /// </summary>
    /// <value>
    /// For desktop apps, a custom URI scheme is commonly used, e.g. "myapp://callback" or "http://localhost:port/callback".
    /// </value>
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the scopes requested during authorization.
    /// </summary>
    /// <value>
    /// Space-separated scopes, e.g. "openid profile email" or "https://graph.microsoft.com/User.Read".
    /// </value>
    public string Scopes { get; set; } = "openid";

    /// <summary>
    /// Gets or sets optional extra query parameters to append to the authorization URL.
    /// </summary>
    public IDictionary<string, string>? ExtraParameters { get; set; }

    /// <summary>
    /// Gets or sets optional prompt parameter (e.g., "login", "consent", "select_account").
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// Gets or sets optional login hint to pre-fill the username.
    /// </summary>
    public string? LoginHint { get; set; }

    /// <summary>
    /// Gets or sets the authorization endpoint URL. If null, derived from <see cref="Authority"/> using Azure AD v2 convention.
    /// </summary>
    public string? AuthorizationEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the token endpoint URL. If null, derived from <see cref="Authority"/> using Azure AD v2 convention.
    /// </summary>
    public string? TokenEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the response_mode for the authorization request (e.g., "query" or "form_post").
    /// Azure AD uses "query" by default.
    /// </summary>
    internal string? ResponseMode { get; set; }

    /// <summary>
    /// Gets or sets HTTP interceptors for token requests. Called before sending and after receiving token endpoint responses.
    /// </summary>
    public IList<IOAuth2HttpInterceptor>? HttpInterceptors { get; set; }

    /// <summary>
    /// Gets or sets flow hooks invoked at key points (before authorize, after code received, after tokens received, after refresh).
    /// </summary>
    public OAuth2FlowHooks? FlowHooks { get; set; }

    /// <summary>
    /// Normalizes the authority URL by removing trailing slashes.
    /// </summary>
    internal string GetNormalizedAuthority()
    {
        var auth = (Authority ?? string.Empty).Trim();
        return auth.TrimEnd('/');
    }
}
