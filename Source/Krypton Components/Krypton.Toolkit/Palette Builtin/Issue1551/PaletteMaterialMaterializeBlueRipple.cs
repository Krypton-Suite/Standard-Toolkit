#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Light Materialize Blue palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialMaterializeBlueRipple : PaletteMaterialMaterializeBlue
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialMaterializeBlueRipple"/> class.
    /// </summary>
    public PaletteMaterialMaterializeBlueRipple()
    {
        ThemeName = "Material - Materialize Blue (Ripple)";
        RippleEffect = true;
    }
}
