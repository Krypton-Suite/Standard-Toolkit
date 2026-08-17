#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Light Materialize Light Blue palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialMaterializeLightBlueRipple : PaletteMaterialMaterializeLightBlue
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialMaterializeLightBlueRipple"/> class.
    /// </summary>
    public PaletteMaterialMaterializeLightBlueRipple()
    {
        ThemeName = "Material - Materialize Light Blue (Ripple)";
        RippleEffect = true;
    }
}
