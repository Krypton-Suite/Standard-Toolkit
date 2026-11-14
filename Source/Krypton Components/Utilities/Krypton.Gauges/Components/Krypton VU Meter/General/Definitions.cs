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
/// Face drawn by <see cref="KryptonVuMeter"/>.
/// </summary>
public enum VuMeterStyle
{
    /// <summary>
    /// Classic needle meter with a non-linear -20 to +3 scale.
    /// </summary>
    Analog = 0,

    /// <summary>
    /// Segmented bar. Below 0 VU is the low colour; 0 and above is the high colour.
    /// </summary>
    Segments = 1
}

/// <summary>
/// How <see cref="KryptonVuMeter"/> obtains its target level.
/// </summary>
public enum VuMeterInputMode
{
    /// <summary>
    /// <see cref="VuMeterValues.Value"/> is set by the application.
    /// </summary>
    Manual = 0,

    /// <summary>
    /// The control reads the default recording device and updates <see cref="VuMeterValues.Value"/>.
    /// Assignments are ignored until the mode returns to <see cref="Manual"/>.
    /// </summary>
    Capture = 1
}

/// <summary>
/// Recording channel used while <see cref="VuMeterInputMode.Capture"/> is active.
/// </summary>
public enum VuMeterChannel
{
    /// <summary>
    /// First channel of the mix format.
    /// </summary>
    Left = 0,

    /// <summary>
    /// Second channel, or the first channel when the device is mono.
    /// </summary>
    Right = 1,

    /// <summary>
    /// Average of every channel in the mix format.
    /// </summary>
    Mix = 2
}
