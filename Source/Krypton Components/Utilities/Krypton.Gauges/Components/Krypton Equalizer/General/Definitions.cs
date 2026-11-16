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
/// Face drawn by <see cref="KryptonEqualizer"/>.
/// </summary>
public enum EqualizerStyle
{
    /// <summary>
    /// Solid spectrum bars with an optional gain thumb.
    /// </summary>
    Bars = 0,

    /// <summary>
    /// Segmented LED-style bars with an optional gain thumb.
    /// </summary>
    Segments = 1
}

/// <summary>
/// How <see cref="KryptonEqualizer"/> obtains spectrum band levels.
/// </summary>
public enum EqualizerInputMode
{
    /// <summary>
    /// Band levels are set by the application through <see cref="KryptonEqualizer.SetLevels(double[])"/> or each <see cref="EqualizerBand.Level"/>.
    /// </summary>
    Manual = 0,

    /// <summary>
    /// The control reads the default recording device, runs an FFT, and updates band levels.
    /// Level assignments are ignored until the mode returns to <see cref="Manual"/>.
    /// </summary>
    Capture = 1
}
