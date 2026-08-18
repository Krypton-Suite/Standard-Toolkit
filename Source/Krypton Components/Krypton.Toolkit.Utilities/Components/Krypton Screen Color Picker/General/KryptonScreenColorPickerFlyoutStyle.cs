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
/// Chrome used for the magnifier flyout that follows the cursor while picking.
/// </summary>
public enum KryptonScreenColorPickerFlyoutStyle
{
    /// <summary>
    /// Painted PowerToys-style dark flyout (independent of the current Krypton palette).
    /// </summary>
    Classic = 0,

    /// <summary>
    /// Themed <see cref="KryptonHeaderGroup"/> flyout that follows the current (or local custom) palette.
    /// </summary>
    Krypton = 1
}
