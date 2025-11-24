#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Dark palette variant with Ripple effect enabled.
/// </summary>
public sealed class PaletteMaterialDarkRipple : PaletteMaterialDark
{
    public PaletteMaterialDarkRipple()
    {
        RippleEffect = true;
    }
}
