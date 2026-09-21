#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Optional animated border drawn around a <see cref="KryptonSplashScreenManager"/> splash window.
/// Default is <see cref="None"/> so existing splash appearances are unchanged.
/// </summary>
public enum KryptonSplashBorderAnimation
{
    /// <summary>No extra border.</summary>
    None = 0,

    /// <summary>Full-rectangle border whose opacity pulses.</summary>
    Pulse = 1,

    /// <summary>Themed edge with a highlight that travels around the perimeter.</summary>
    Sweep = 2
}
