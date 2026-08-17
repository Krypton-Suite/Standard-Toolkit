#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Dark Grey chrome with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialDarkGrayRipple : PaletteMaterialDarkGray
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialDarkGrayRipple"/> class.
    /// </summary>
    public PaletteMaterialDarkGrayRipple()
    {
        ThemeName = "Material - Dark Gray (Ripple)";
        RippleEffect = true;
    }
}
