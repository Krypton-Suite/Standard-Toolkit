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
/// How an analogue gauge obtains and accepts its target value.
/// </summary>
public enum GaugeInputMode
{
    /// <summary>
    /// <see cref="GaugeValues.Value"/> is set by the application. Pointer and keyboard input are ignored.
    /// </summary>
    Manual = 0,

    /// <summary>
    /// The user can drag or click the face (and use the keyboard when focused) to change <see cref="GaugeValues.Value"/>.
    /// </summary>
    Interactive = 1
}

/// <summary>
/// Severity used when a <see cref="GaugeRange"/> does not supply an explicit colour.
/// </summary>
public enum GaugeRangeSeverity
{
    /// <summary>
    /// Draw with the normal fill / indicator colour.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Draw with the warning palette colour.
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Draw with the error palette colour.
    /// </summary>
    Error = 2
}

/// <summary>
/// Face drawn by <see cref="KryptonRectangularGauge"/>.
/// </summary>
public enum RectangularGaugeStyle
{
    /// <summary>
    /// Continuous fill proportional to the value.
    /// </summary>
    Solid = 0,

    /// <summary>
    /// Discrete lit segments proportional to the value.
    /// </summary>
    Segmented = 1
}
