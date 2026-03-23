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
/// Exception thrown when an OAuth2 operation fails.
/// </summary>
public class OAuth2Exception : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2Exception"/> class.
    /// </summary>
    public OAuth2Exception()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2Exception"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public OAuth2Exception(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2Exception"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public OAuth2Exception(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Gets the optional OAuth2 error code (e.g., "invalid_grant", "access_denied").
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Gets the optional OAuth2 error description from the provider.
    /// </summary>
    public string? ErrorDescription { get; set; }
}
