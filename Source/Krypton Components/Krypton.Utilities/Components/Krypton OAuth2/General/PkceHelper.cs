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
/// Internal helper for PKCE (Proof Key for Code Exchange) as per RFC 7636.
/// </summary>
internal static class PkceHelper
{
    private const int _codeVerifierLength = 43;
    private const string _codeVerifierChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";

    /// <summary>
    /// Generates a cryptographically random code verifier.
    /// </summary>
    /// <returns>Base64url-encoded code verifier (43–128 chars).</returns>
    public static string GenerateCodeVerifier()
    {
        var bytes = new byte[_codeVerifierLength];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        var chars = new char[_codeVerifierLength];
        for (var i = 0; i < _codeVerifierLength; i++)
        {
            chars[i] = _codeVerifierChars[bytes[i] % _codeVerifierChars.Length];
        }

        return new string(chars);
    }

    /// <summary>
    /// Computes the S256 code challenge from a code verifier.
    /// </summary>
    /// <param name="codeVerifier">The code verifier.</param>
    /// <returns>Base64url-encoded SHA256 hash of the verifier.</returns>
    public static string ComputeCodeChallenge(string codeVerifier)
    {
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
        return Base64UrlEncode(hash);
    }

    /// <summary>
    /// Base64url encodes bytes (no padding, URL-safe characters).
    /// </summary>
    private static string Base64UrlEncode(byte[] input)
    {
        var base64 = Convert.ToBase64String(input);
        return base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
}
