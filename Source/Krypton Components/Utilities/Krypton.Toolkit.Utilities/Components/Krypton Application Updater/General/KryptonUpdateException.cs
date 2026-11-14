#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>A feed, download, verification, or apply failure reported by <see cref="KryptonApplicationUpdater"/>.</summary>
public sealed class KryptonUpdateException : Exception
{
    /// <summary>Initializes the exception.</summary>
    /// <param name="message">Text safe to show in the UI.</param>
    public KryptonUpdateException(string message)
        : base(message)
    {
    }

    /// <summary>Initializes the exception with an inner cause.</summary>
    /// <param name="message">Text safe to show in the UI.</param>
    /// <param name="innerException">The underlying failure.</param>
    public KryptonUpdateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>Initializes an HTTP failure. <paramref name="httpStatus"/> lets a range download tell 416 from other errors.</summary>
    /// <param name="message">Text safe to show in the UI.</param>
    /// <param name="httpStatus">Status code returned by the server.</param>
    internal KryptonUpdateException(string message, int httpStatus)
        : base(message)
    {
        HttpStatus = httpStatus;
    }

    /// <summary>Gets the HTTP status when this failure came from a response, or 0.</summary>
    internal int HttpStatus { get; }
}
