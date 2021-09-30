﻿#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Represents a common dialog box that displays a list of fonts
    /// that are currently installed on the system.
    /// </summary>
    public class KryptonFontDialog : FontDialog
    {
        private readonly CommonDialogHandler _commonDialogHandler;

        /// <summary>
        /// Changes the title of the common Print Dialog
        /// </summary>
        public string Title
        {
            get => _commonDialogHandler.Title;
            set => _commonDialogHandler.Title = value;
        }

        /// <summary>
        /// Represents a common dialog box that displays a list of fonts
        /// that are currently installed on the system.
        /// </summary>
        public KryptonFontDialog() => _commonDialogHandler = new CommonDialogHandler(true);

        //protected override bool RunDialog(IntPtr hWndOwner)
        //{
        //    var ret = base.RunDialog(hWndOwner);
        //    //return ret;// || _commonDialogHandler._T;
        //}

        /// <inheritdoc />
        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            var (handled, retValue) = _commonDialogHandler.HookProc(hWnd, msg, wparam, lparam);
            if (!handled)
            {
                if (msg == PI.WM_.PRINTCLIENT )
                {
                    // Supposedly finished init, so go finalise the checkboxes
                    foreach (var control in _commonDialogHandler.Controls)
                    {
                        if (control.Button is KryptonCheckBox checkBox)
                        {
                            var state = PI.IsDlgButtonChecked(hWnd, control.DlgCtrlId);
                            checkBox.Checked = (state != PI.BST_.UNCHECKED);
                        }
                    }
                }

                Debug.WriteLine(@"0x{0:X} : {1}", msg, hWnd);
            }

            return handled ? retValue :base.HookProc(hWnd, msg, wparam, lparam);
        }

    }
}
