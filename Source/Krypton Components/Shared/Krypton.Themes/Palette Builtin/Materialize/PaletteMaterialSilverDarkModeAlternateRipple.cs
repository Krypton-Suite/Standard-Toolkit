#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Material Dark Silver (Dark Mode - Alternate) palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialSilverDarkModeAlternateRipple : PaletteMaterialSilverDarkModeAlternate
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialSilverDarkModeAlternateRipple"/> class.
    /// </summary>
    public PaletteMaterialSilverDarkModeAlternateRipple()
    {
        ThemeName = "Material - Silver (Dark Mode - Alternate) (Ripple)";
        RippleEffect = true;
    }
}
