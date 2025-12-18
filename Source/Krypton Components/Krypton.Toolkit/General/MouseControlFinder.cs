#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Aids in finding controls under the given coordinates.
/// </summary>
public static class MouseControlFinder
{
    /// <summary>
    /// Returns the HWND under the current mouse cursor (screen coordinates).
    /// </summary>
    /// <param name="screenPoint">The screen coordinates</param>
    /// <returns></returns>
    public static IntPtr HwndUnderMouse(Point screenPoint)
    {
        return PI.WindowFromPoint(screenPoint);
    }

    /// <summary>
    /// Returns the WinForms Control under the mouse or null if none found.
    /// </summary>
    /// <param name="screenPoint">The screen coordinates</param>
    /// <returns></returns>
    public static Control? ControlUnderMouse(Point screenPoint)
    {
        IntPtr hwnd = HwndUnderMouse(screenPoint);
        while (hwnd != IntPtr.Zero)
        {
            Control? control = Control.FromHandle(hwnd);
            if (control != null)
            {
                return control;
            }
            hwnd = PI.GetParent(hwnd);
        }
        return null;
    }
}
