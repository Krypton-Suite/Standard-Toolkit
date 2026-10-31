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
/// Provides the new target level for <see cref="KryptonVuMeter.ValueChanged"/>.
/// </summary>
public sealed class VuMeterValueChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="VuMeterValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="value">Target level in VU.</param>
    public VuMeterValueChangedEventArgs(double value) => Value = value;

    /// <summary>
    /// Gets the target level in VU. The painted needle or bar eases toward this value.
    /// </summary>
    public double Value { get; }
}
