#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementTextAlignmentHorizontal
{
    /// <summary>
    /// Text element horizontal text alignment.
    /// </summary>
    public PaletteRelativeAlign TextAlignmentHorizontal { get; set; }
}

public interface IKryptonTaskDialogElementTextAlignmentVertical
{
    /// <summary>
    /// Text element vertical text alignment.
    /// </summary>
    public PaletteRelativeAlign TextAlignmentVertical { get; set; }
}
