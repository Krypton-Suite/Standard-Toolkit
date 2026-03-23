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
/// Intercepts token HTTP requests and responses. Implement to add custom headers, log traffic,
/// or transform requests/responses.
/// </summary>
public interface IOAuth2HttpInterceptor
{
    /// <summary>
    /// Called before sending the token request. Use to add headers or modify the request.
    /// </summary>
    /// <param name="request">The outgoing HTTP request. Can be modified.</param>
    /// <param name="context">Context describing the token request (grant type, etc.).</param>
    void OnBeforeTokenRequest(HttpRequestMessage request, OAuth2TokenRequestContext context);

    /// <summary>
    /// Called after receiving the token response. Use to log, validate, or transform the response.
    /// </summary>
    /// <param name="response">The HTTP response.</param>
    /// <param name="jsonBody">The raw response body.</param>
    /// <param name="context">Context describing the token request.</param>
    void OnAfterTokenResponse(HttpResponseMessage response, string jsonBody, OAuth2TokenRequestContext context);
}

/// <summary>
/// Context for a token endpoint request (authorization code exchange or refresh).
/// </summary>
public sealed class OAuth2TokenRequestContext
{
    /// <summary>
    /// Gets the grant type: "authorization_code" or "refresh_token".
    /// </summary>
    public string GrantType { get; }

    /// <summary>
    /// Gets the token endpoint URL.
    /// </summary>
    public string? TokenEndpoint { get; }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public OAuth2TokenRequestContext(string grantType, string? tokenEndpoint)
    {
        GrantType = grantType;
        TokenEndpoint = tokenEndpoint;
    }
}
