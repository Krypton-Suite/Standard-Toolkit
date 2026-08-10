#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Light Lime Green palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialLimeGreenRipple : PaletteMaterialLimeGreen
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialLimeGreenRipple"/> class.
    /// </summary>
    public PaletteMaterialLimeGreenRipple()
    {
        ThemeName = "Material - Lime Green (Ripple)";
        RippleEffect = true;
    }
}
