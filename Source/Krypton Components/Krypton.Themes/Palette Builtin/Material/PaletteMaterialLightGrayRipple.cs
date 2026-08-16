#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Light Grey chrome with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialLightGrayRipple : PaletteMaterialLightGray
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialLightGrayRipple"/> class.
    /// </summary>
    public PaletteMaterialLightGrayRipple()
    {
        ThemeName = "Material - Light Gray (Ripple)";
        RippleEffect = true;
    }
}
