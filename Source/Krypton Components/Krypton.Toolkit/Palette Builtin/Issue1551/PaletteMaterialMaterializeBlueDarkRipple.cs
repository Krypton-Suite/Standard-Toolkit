#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Dark Materialize Blue - Dark Mode palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialMaterializeBlueDarkRipple : PaletteMaterialMaterializeBlueDark
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialMaterializeBlueDarkRipple"/> class.
    /// </summary>
    public PaletteMaterialMaterializeBlueDarkRipple()
    {
        ThemeName = "Material - Materialize Blue - Dark Mode (Ripple)";
        RippleEffect = true;
    }
}
