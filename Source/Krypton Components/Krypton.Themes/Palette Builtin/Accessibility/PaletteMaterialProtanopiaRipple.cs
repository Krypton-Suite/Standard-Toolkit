#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Protanopia accessibility palette with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialProtanopiaRipple : PaletteMaterialProtanopia
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialProtanopiaRipple"/> class.
    /// </summary>
    public PaletteMaterialProtanopiaRipple()
    {
        ThemeName = "Material - Protanopia (Ripple)";
        RippleEffect = true;
    }
}
