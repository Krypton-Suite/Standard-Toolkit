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
/// Represents the method that will handle the <see cref="KryptonKnob.KnobValueChanged"/> event.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="KnobValueChangedEventArgs"/> that contains the event data.</param>
public delegate void KnobValueChangedEventHandler(object? sender, KnobValueChangedEventArgs e);
