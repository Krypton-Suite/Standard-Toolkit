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
/// Provides the previous and new target values for <see cref="KryptonGaugeBase.ValueChanged"/>.
/// </summary>
public sealed class GaugeValueChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="GaugeValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="oldValue">Previous target value.</param>
    /// <param name="newValue">New target value. The painted indicator eases toward this value when animation is enabled.</param>
    public GaugeValueChangedEventArgs(double oldValue, double newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// Gets the previous target value.
    /// </summary>
    public double OldValue { get; }

    /// <summary>
    /// Gets the new target value.
    /// </summary>
    public double NewValue { get; }
}
