#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Material Dark Materialize Light Blue - Dark Mode palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialMaterializeLightBlueDarkRipple : PaletteMaterialMaterializeLightBlueDark
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialMaterializeLightBlueDarkRipple"/> class.
    /// </summary>
    public PaletteMaterialMaterializeLightBlueDarkRipple()
    {
        ThemeName = "Material - Materialize Light Blue - Dark Mode (Ripple)";
        RippleEffect = true;
    }
}
