#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementForeColor
{
    /// <summary>
    /// Foreground color and overrides the theme based color.
    /// </summary>
    [DefaultValue(null)]
    [Description("Foreground color and overrides the theme based color.")]
    public Color ForeColor { get; set; }
}

