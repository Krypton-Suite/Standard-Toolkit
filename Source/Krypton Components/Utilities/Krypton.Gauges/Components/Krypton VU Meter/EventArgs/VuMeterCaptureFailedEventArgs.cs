#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Provides the reason <see cref="KryptonVuMeter"/> could not read the default recording device.
/// </summary>
public sealed class VuMeterCaptureFailedEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="VuMeterCaptureFailedEventArgs"/> class.
    /// </summary>
    /// <param name="message">Description suitable for display.</param>
    /// <param name="error">Underlying failure, when one was thrown.</param>
    public VuMeterCaptureFailedEventArgs(string message, Exception? error)
    {
        Message = message ?? string.Empty;
        Error = error;
    }

    /// <summary>
    /// Gets a short description of the failure. The meter keeps showing its last level.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the exception that caused the failure, or <see langword="null"/> when only an error code was returned.
    /// </summary>
    public Exception? Error { get; }
}
