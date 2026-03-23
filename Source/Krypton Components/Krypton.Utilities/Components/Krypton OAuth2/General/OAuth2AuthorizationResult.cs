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
/// Result of the OAuth2 authorization step (before token exchange).
/// </summary>
public sealed class OAuth2AuthorizationResult
{
    /// <summary>
    /// Gets whether the user completed sign-in successfully.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Gets the authorization code when <see cref="Success"/> is true; otherwise null.
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// Gets the state parameter returned by the provider, if any.
    /// </summary>
    public string? State { get; }

    /// <summary>
    /// Gets the error message when <see cref="Success"/> is false.
    /// </summary>
    public string? ErrorMessage { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2AuthorizationResult"/> class.
    /// </summary>
    public OAuth2AuthorizationResult(bool success, string? code, string? state, string? errorMessage)
    {
        Success = success;
        Code = code;
        State = state;
        ErrorMessage = errorMessage;
    }
}
