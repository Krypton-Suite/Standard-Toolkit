#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Material Dark Lime Green palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialLimeGreenDarkRipple : PaletteMaterialLimeGreenDark
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialLimeGreenDarkRipple"/> class.
    /// </summary>
    public PaletteMaterialLimeGreenDarkRipple()
    {
        ThemeName = "Material - Lime Green - Dark Mode (Ripple)";
        RippleEffect = true;
    }
}
