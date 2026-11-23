#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Handles the value property for the <see cref="KryptonKnob"/>.
/// </summary>
public class KnobValueChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KnobValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public KnobValueChangedEventArgs(int value) => Value = value;

    /// <summary>
    /// Gets or sets the value for the <see cref="KryptonKnob"/>.
    /// </summary>
    public int Value { get; set; }
}
