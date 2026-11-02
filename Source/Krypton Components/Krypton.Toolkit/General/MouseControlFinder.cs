#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, lesandrog et al. 2025 - 2026. All rights reserved.
 *
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
    /// <returns>
    ///     The return value is a handle to the window that contains the point. If no window exists at the given point, the return value is null. 
    ///     If the point is over a static text control, the return value is a handle to the window under the static text control.
    /// </returns>
    public static IntPtr HwndUnderMouse(Point screenPoint)
    {
        return PI.WindowFromPoint(screenPoint);
    }

    /// <summary>
    /// Returns the WinForms Control under the mouse or null if none found.
    /// </summary>
    /// <param name="screenPoint">The screen coordinates</param>
    /// <returns>Returns the control under the screenPoint or if none was found, null.</returns>
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
