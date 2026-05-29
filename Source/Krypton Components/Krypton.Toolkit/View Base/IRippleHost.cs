#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Defines a small contract for views that can host touch/click ripple effects.
/// </summary>
public interface IRippleHost
{
    /// <summary>
    /// Starts a ripple animation originating at the specified control-relative point.
    /// </summary>
    /// <param name="origin">Point in control coordinates where the ripple should originate.</param>
    void StartRipple(Point origin);

    /// <summary>
    /// Cancels all in-flight ripples and stops the animation timer if running.
    /// </summary>
    void CancelRipples();
}
