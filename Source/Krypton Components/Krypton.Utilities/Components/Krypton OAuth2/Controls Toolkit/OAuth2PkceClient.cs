#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if NET8_0_OR_GREATER
using System.Text.Json;
#else
using System.Web.Script.Serialization;
#endif

namespace Krypton.Utilities;

/// <summary>
/// OAuth2 client using PKCE (Proof Key for Code Exchange) for the authorization code flow.
/// </summary>
/// <remarks>
/// <para>
/// Supports authorization code exchange and token refresh. Use <see cref="AuthorizeWithBrowserAsync"/>
/// for the full flow (opens a Krypton-styled sign-in window with WebView2), or call
/// <see cref="BuildAuthorizationUrl"/>, obtain the code via your own browser, then <see cref="ExchangeCodeAsync"/>.
/// </para>
/// <para>
/// Example (Azure AD / Microsoft Entra):
/// <code>
/// var options = new OAuth2PkceOptions
/// {
///     Authority = "https://login.microsoftonline.com/{tenant-id}/v2.0",
///     ClientId = "your-client-id",
///     RedirectUri = "myapp://callback",
///     Scopes = "openid profile User.Read"
/// };
/// var client = new OAuth2PkceClient(options);
/// var tokens = await client.AuthorizeWithBrowserAsync(this, "Sign in to My App");
/// // Use tokens.AccessToken for API calls; tokens.RefreshToken to refresh when expired.
/// </code>
/// </para>
/// </remarks>
public class OAuth2PkceClient
{
    private static readonly HttpClient SharedHttpClient = new();
    private readonly OAuth2PkceOptions _options;
    private readonly IOAuth2BrowserHost? _browserHost;

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2PkceClient"/> class.
    /// </summary>
    /// <param name="options">The OAuth2 PKCE options. Can be created via <see cref="OAuth2ProviderPresets"/> or <see cref="IOAuth2Provider"/>.</param>
    /// <param name="browserHost">Optional custom browser host. When null, uses the built-in KryptonWebView2 form when WebView2 is available.</param>
    public OAuth2PkceClient(OAuth2PkceOptions options, IOAuth2BrowserHost? browserHost = null)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _browserHost = browserHost;
    }

    /// <summary>
    /// Initializes a new instance using a custom <see cref="IOAuth2Provider"/>.
    /// </summary>
    /// <param name="provider">The provider (e.g., custom or built-in).</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <param name="scopes">Space-separated scopes. Default is "openid".</param>
    /// <param name="browserHost">Optional custom browser host.</param>
    public OAuth2PkceClient(IOAuth2Provider provider, string clientId, string redirectUri, string scopes = "openid", IOAuth2BrowserHost? browserHost = null)
        : this(OAuth2ProviderPresets.Custom(provider, clientId, redirectUri, scopes), browserHost)
    {
    }

    /// <summary>
    /// Builds the authorization URL with PKCE parameters.
    /// </summary>
    public string BuildAuthorizationUrl(string codeVerifier, string? state = null)
    {
        var codeChallenge = PkceHelper.ComputeCodeChallenge(codeVerifier);
        var authEndpoint = !string.IsNullOrEmpty(_options.AuthorizationEndpoint)
            ? _options.AuthorizationEndpoint
            : GetDefaultAuthorizationEndpoint();

        var responseMode = _options.ResponseMode ?? "query";
        var query = new Dictionary<string, string?>
        {
            ["client_id"] = _options.ClientId,
            ["response_type"] = "code",
            ["redirect_uri"] = _options.RedirectUri,
            ["response_mode"] = responseMode,
            ["scope"] = _options.Scopes,
            ["code_challenge"] = codeChallenge,
            ["code_challenge_method"] = "S256"
        };

        if (!string.IsNullOrEmpty(state))
        {
            query["state"] = state;
        }

        if (!string.IsNullOrEmpty(_options.Prompt))
        {
            query["prompt"] = _options.Prompt;
        }

        if (!string.IsNullOrEmpty(_options.LoginHint))
        {
            query["login_hint"] = _options.LoginHint;
        }

        if (_options.ExtraParameters != null)
        {
            foreach (var kv in _options.ExtraParameters)
                query[kv.Key] = kv.Value;
        }

        var queryString = string.Join("&", query.Select(kv => $"{Uri.EscapeDataString(kv.Key ?? "")}={Uri.EscapeDataString(kv.Value ?? "")}"));

        return $"{authEndpoint}?{queryString}";
    }

    /// <summary>
    /// Gets the token endpoint URL for the configured authority.
    /// </summary>
    public string? GetTokenEndpoint()
    {
        if (!string.IsNullOrEmpty(_options.TokenEndpoint))
        {
            return _options.TokenEndpoint;
        }

        return GetDefaultTokenEndpoint();
    }

    private string GetDefaultAuthorizationEndpoint()
    {
        var auth = _options.GetNormalizedAuthority();
        return auth.EndsWith("/oauth2/v2.0/authorize", StringComparison.OrdinalIgnoreCase)
            ? auth
            : $"{auth.TrimEnd('/')}/oauth2/v2.0/authorize";
    }

    private string GetDefaultTokenEndpoint()
    {
        var auth = _options.GetNormalizedAuthority();
        return auth.EndsWith("/oauth2/v2.0/token", StringComparison.OrdinalIgnoreCase)
            ? auth
            : $"{auth.TrimEnd('/')}/oauth2/v2.0/token";
    }

    /// <summary>
    /// Exchanges an authorization code for tokens.
    /// </summary>
    public async Task<OAuth2TokenResponse> ExchangeCodeAsync(string? authorizationCode, string codeVerifier, CancellationToken cancellationToken = default)
    {
        var tokenEndpoint = GetTokenEndpoint();
        var context = new OAuth2TokenRequestContext("authorization_code", tokenEndpoint);
        var content = new FormUrlEncodedContent(new Dictionary<string, string?>
        {
            ["client_id"] = _options.ClientId,
            ["grant_type"] = "authorization_code",
            ["code"] = authorizationCode,
            ["redirect_uri"] = _options.RedirectUri,
            ["code_verifier"] = codeVerifier
        });

        using var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint) { Content = content };
        RunHttpInterceptorsBefore(request, context);

        using var response = await SharedHttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        RunHttpInterceptorsAfter(request, response, json, context);

        if (!response.IsSuccessStatusCode)
        {
            throw CreateExceptionFromErrorResponse(json, (int)response.StatusCode);
        }

        var tokens = ParseTokenResponse(json);

        if (_options.FlowHooks != null)
        {
            await _options.FlowHooks.InvokeAfterTokensReceivedAsync(tokens, cancellationToken).ConfigureAwait(false);
        }

        return tokens;
    }

    /// <summary>
    /// Runs the full OAuth2 PKCE flow: opens a browser window for sign-in, intercepts the redirect,
    /// exchanges the authorization code for tokens, and returns the token response.
    /// </summary>
    public async Task<OAuth2TokenResponse> AuthorizeWithBrowserAsync(IWin32Window? owner = null, string title = "Sign in", CancellationToken cancellationToken = default)
    {
        var codeVerifier = PkceHelper.GenerateCodeVerifier();
        var stateBytes = new byte[16];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(stateBytes);
        }

        var state = Convert.ToBase64String(stateBytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');

        var authUrl = BuildAuthorizationUrl(codeVerifier, state);

        if (_options.FlowHooks != null)
        {
            await _options.FlowHooks.InvokeBeforeAuthorizeAsync(authUrl, state, cancellationToken).ConfigureAwait(false);
        }

        OAuth2AuthorizationResult result;
        if (_browserHost != null)
        {
            result = await _browserHost.LaunchAsync(authUrl, _options.RedirectUri, title, owner, cancellationToken).ConfigureAwait(true);
        }
        else
        {
#if WEBVIEW2_AVAILABLE
            var defaultHost = new OAuth2WebView2BrowserHost();
            result = await defaultHost.LaunchAsync(authUrl, _options.RedirectUri, title, owner, cancellationToken).ConfigureAwait(true);
#else
            throw new OAuth2Exception("OAuth2 authorization with embedded browser requires either a custom " + nameof(IOAuth2BrowserHost) + " or WebView2.");
#endif
        }

        if (!result.Success)
        {
            throw new OAuth2Exception(result.ErrorMessage ?? "Authorization was not completed.");
        }

        if (string.IsNullOrEmpty(result.Code))
        {
            throw new OAuth2Exception("No authorization code was received.");
        }

        if (_options.FlowHooks != null)
        {
            await _options.FlowHooks.InvokeAfterCodeReceivedAsync(result.Code, result.State, cancellationToken).ConfigureAwait(false);
        }

        return await ExchangeCodeAsync(result.Code, codeVerifier, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Refreshes the access token using a refresh token.
    /// </summary>
    public async Task<OAuth2TokenResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenEndpoint = GetTokenEndpoint();
        var context = new OAuth2TokenRequestContext("refresh_token", tokenEndpoint);
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["grant_type"] = "refresh_token",
            ["refresh_token"] = refreshToken
        });

        using var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint) { Content = content };
        RunHttpInterceptorsBefore(request, context);

        using var response = await SharedHttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        RunHttpInterceptorsAfter(request, response, json, context);

        if (!response.IsSuccessStatusCode)
        {
            throw CreateExceptionFromErrorResponse(json, (int)response.StatusCode);
        }

        var tokens = ParseTokenResponse(json);
        if (_options.FlowHooks != null)
        {
            await _options.FlowHooks.InvokeAfterTokensReceivedAsync(tokens, cancellationToken).ConfigureAwait(false);
            await _options.FlowHooks.InvokeAfterRefreshAsync(tokens, cancellationToken).ConfigureAwait(false);
        }
        return tokens;
    }

    private void RunHttpInterceptorsBefore(HttpRequestMessage request, OAuth2TokenRequestContext context)
    {
        var interceptors = _options.HttpInterceptors;
        if (interceptors == null)
        {
            return;
        }

        foreach (var interceptor in interceptors)
            interceptor.OnBeforeTokenRequest(request, context);
    }

    private void RunHttpInterceptorsAfter(HttpRequestMessage request, HttpResponseMessage response, string jsonBody, OAuth2TokenRequestContext context)
    {
        var interceptors = _options.HttpInterceptors;
        if (interceptors == null)
        {
            return;
        }

        foreach (var interceptor in interceptors)
            interceptor.OnAfterTokenResponse(response, jsonBody, context);
    }

    private static OAuth2Exception CreateExceptionFromErrorResponse(string json, int statusCode)
    {
        string? error = null, errorDescription = null;
        try
        {
#if NET8_0_OR_GREATER
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.TryGetProperty("error", out var errProp))
            {
                error = errProp.GetString();
            }

            if (root.TryGetProperty("error_description", out var descProp))
            {
                errorDescription = descProp.GetString();
            }
#else
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(json);
            if (dict?.TryGetValue("error", out var errObj) == true && errObj != null)
            {
                error = errObj.ToString();
            }

            if (dict?.TryGetValue("error_description", out var descObj) == true && descObj != null)
            {
                errorDescription = descObj.ToString();
            }
#endif
        }
        catch { }

        var msg = string.IsNullOrEmpty(errorDescription) ? (error != null ? $"OAuth2 error: {error}" : $"Token request failed with status {statusCode}.") : errorDescription;
        return new OAuth2Exception(msg) { Error = error, ErrorDescription = errorDescription };
    }

    private static OAuth2TokenResponse ParseTokenResponse(string json)
    {
        var resp = new OAuth2TokenResponse();
#if NET8_0_OR_GREATER
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        if (root.TryGetProperty("access_token", out var at))
        {
            resp.AccessToken = at.GetString();
        }

        if (root.TryGetProperty("refresh_token", out var rt))
        {
            resp.RefreshToken = rt.GetString();
        }

        if (root.TryGetProperty("token_type", out var tt))
        {
            resp.TokenType = tt.GetString();
        }

        if (root.TryGetProperty("expires_in", out var ei) && ei.TryGetInt32(out var val))
        {
            resp.ExpiresIn = val;
        }

        if (root.TryGetProperty("id_token", out var idt))
        {
            resp.IdToken = idt.GetString();
        }

        if (root.TryGetProperty("scope", out var sc))
        {
            resp.Scope = sc.GetString();
        }
#else
        var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(json);
        if (dict != null)
        {
            resp.AccessToken = GetString(dict, "access_token");
            resp.RefreshToken = GetString(dict, "refresh_token");
            resp.TokenType = GetString(dict, "token_type");
            resp.IdToken = GetString(dict, "id_token");
            resp.Scope = GetString(dict, "scope");
            resp.ExpiresIn = GetInt(dict, "expires_in");
        }
#endif
        return resp;
    }

#if !NET8_0_OR_GREATER
    private static string? GetString(Dictionary<string, object> dict, string key) =>
        dict.TryGetValue(key, out var obj) && obj != null ? obj.ToString() : null;
    private static int GetInt(Dictionary<string, object> dict, string key) =>
        dict.TryGetValue(key, out var obj) && obj != null && int.TryParse(obj.ToString(), out var r) ? r : 0;
#endif
}
