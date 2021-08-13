#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
 */
#endregion

// ReSharper disable UnusedType.Global

namespace Krypton.Toolkit.Controls_Toolkit
{
    /// <summary>
    ///  Represents
    ///  a common dialog box that displays a list of fonts that are currently installed
    ///  on
    ///  the system.
    /// </summary>
    public class KryptonFontDialog : FontDialog
    {
        private readonly CommonDialogHandler _commonDialogHandler;

        /// <summary>
        ///  Represents
        ///  a common dialog box that displays a list of fonts that are currently installed
        ///  on
        ///  the system.
        /// </summary>
        public KryptonFontDialog()
        {
            _commonDialogHandler = new CommonDialogHandler();
        }

        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            (var handled, var retValue) = _commonDialogHandler.HookProc(hWnd, msg, wparam, lparam);
            return handled ? retValue :base.HookProc(hWnd, msg, wparam, lparam);
        }

        private bool EnumerateChildWindow(IntPtr hwnd, IntPtr lParam)
        {
            bool result = false;
            GCHandle gch = GCHandle.FromIntPtr(lParam);
            if (gch.Target is List<IntPtr> list)
            {
                list.Add(hwnd);
                result = true; // return true as long as children are found
            }
            return result;
        }
    }
}
