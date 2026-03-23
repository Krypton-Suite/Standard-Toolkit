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
/// Redirect handler that starts an <see cref="HttpListener"/> on localhost to receive the OAuth2 callback.
/// Use when redirect_uri is http://localhost:port/callback (or similar).
/// </summary>
/// <remarks>
/// <para>
/// The redirect_uri must be registered with the OAuth2 provider and use the loopback address
/// (e.g. http://localhost:8400/callback or http://127.0.0.1:8400/). The listener starts on the
/// host/port from the redirect URI and waits for the provider's redirect.
/// </para>
/// </remarks>
public sealed class OAuth2HttpListenerRedirectHandler : IOAuth2RedirectHandler
{
    private const string _successHtml = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><title>Sign-in complete</title></head><body><h1>Sign-in complete</h1><p>You can close this window and return to the application.</p></body></html>";
    private const string _errorHtml = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><title>Sign-in failed</title></head><body><h1>Sign-in failed</h1><p>{0}</p><p>You can close this window.</p></body></html>";

    /// <inheritdoc />
    public async Task<OAuth2AuthorizationResult> WaitForRedirectAsync(string redirectUri, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(redirectUri))
        {
            return new OAuth2AuthorizationResult(false, null, null, "Redirect URI is empty.");
        }

        var prefix = GetListenerPrefix(redirectUri);
        if (string.IsNullOrEmpty(prefix))
        {
            return new OAuth2AuthorizationResult(false, null, null, "Redirect URI must be http://localhost or http://127.0.0.1 for HttpListener.");
        }

        using var listener = new HttpListener();
        listener.Prefixes.Add(prefix);
        listener.Start();

        try
        {
            using var reg = cancellationToken.Register(listener.Stop);

#if NET6_0_OR_GREATER
            var context = await listener.GetContextAsync().WaitAsync(cancellationToken).ConfigureAwait(false);
#else
            var context = await listener.GetContextAsync().ConfigureAwait(false);
#endif
            var request = context.Request;
            var response = context.Response;

            string? code = null;
            string? state = null;
            string? error = null;
            string? errorDescription = null;

            var query = request.Url?.Query;
            if (!string.IsNullOrEmpty(query) && query != null && query.StartsWith("?", StringComparison.Ordinal))
            {
                query = query.Substring(1);
                foreach (var part in query.Split('&'))
                {
                    var eq = part.IndexOf('=');
                    if (eq < 0)
                    {
                        continue;
                    }

                    var key = Uri.UnescapeDataString(part.Substring(0, eq).Replace('+', ' '));
                    var value = Uri.UnescapeDataString(part.Substring(eq + 1).Replace('+', ' '));

                    switch (key.ToUpperInvariant())
                    {
                        case "CODE":
                            code = value;
                            break;
                        case "STATE":
                            state = value;
                            break;
                        case "ERROR":
                            error = value;
                            break;
                        case "ERROR_DESCRIPTION":
                            errorDescription = value;
                            break;
                    }
                }
            }

            var body = _successHtml;
            OAuth2AuthorizationResult result;

            if (!string.IsNullOrEmpty(error))
            {
                result = new OAuth2AuthorizationResult(false, null, null, errorDescription ?? error);
                response.StatusCode = 400;
                body = string.Format(_errorHtml, WebUtility.HtmlEncode(errorDescription ?? error));
            }
            else if (!string.IsNullOrEmpty(code))
            {
                result = new OAuth2AuthorizationResult(true, code, state, null);
            }
            else
            {
                result = new OAuth2AuthorizationResult(false, null, null, "No authorization code in redirect.");
                response.StatusCode = 400;
                body = string.Format(_errorHtml, "No authorization code was received.");
            }

            var buffer = Encoding.UTF8.GetBytes(body);
            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/html; charset=utf-8";
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
            response.OutputStream.Close();

            return result;
        }
        catch (OperationCanceledException)
        {
            return new OAuth2AuthorizationResult(false, null, null, "Sign-in was cancelled.");
        }
        catch (HttpListenerException) when (cancellationToken.IsCancellationRequested)
        {
            return new OAuth2AuthorizationResult(false, null, null, "Sign-in was cancelled.");
        }
        catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
        {
            return new OAuth2AuthorizationResult(false, null, null, "Sign-in was cancelled.");
        }
    }

    private static string? GetListenerPrefix(string redirectUri)
    {
        try
        {
            var uri = new Uri(redirectUri);
            if (uri.Scheme != "http")
            {
                return null;
            }

            if (!string.Equals(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase) &&
                uri.Host != "127.0.0.1")
            {
                return null;
            }

            var port = uri.Port > 0 ? uri.Port : 80;
            var path = uri.AbsolutePath;
            if (string.IsNullOrEmpty(path) || path == "/")
            {
                path = "/";
            }
            else if (!path.EndsWith("/", StringComparison.Ordinal))
            {
                path += "/";
            }

            return $"{uri.Scheme}://{uri.Host}:{port}{path}";
        }
        catch
        {
            return null;
        }
    }
}
