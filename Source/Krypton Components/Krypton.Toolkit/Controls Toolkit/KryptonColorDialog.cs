#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Represents a common dialog box that displays colours
    /// that are currently installed on the system.
    /// </summary>
    public class KryptonColorDialog : ColorDialog
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
        /// Represents a common dialog box that displays colours
        /// that are currently installed on the system.
        /// </summary>
        public KryptonColorDialog() =>
            _commonDialogHandler = new CommonDialogHandler(true)
            {
                ClickCallback = ClickCallback
            };

        /// <summary>
        /// Changes the title of the common Print Dialog
        /// </summary>
        public string Title 
        { 
            get => _commonDialogHandler.Title; 
            set => _commonDialogHandler.Title = value;
        }


        private void ClickCallback(CommonDialogHandler.Attributes originalControl)
        {
            // When the expand is clicked
            // - Disable it
            // - Enable Add custom colour
            if (originalControl.DlgCtrlId == 0x000002CF)
            {
                originalControl.Button.Enabled = false;
                foreach (var control in _commonDialogHandler.Controls)
                {
                    if (control.DlgCtrlId == 0x000002C8)
                    {
                        control.Button.Enabled = true;
                        break;
                    }
                }
            }
        }

        //protected override bool RunDialog(IntPtr hWndOwner)
        //{
        //    var ret = base.RunDialog(hWndOwner);
        //}

        /// <inheritdoc />
        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            var (handled, retValue) = _commonDialogHandler.HookProc(hWnd, msg, wparam, lparam);
            if (!handled
                && (msg == PI.WM_.WINDOWPOSCHANGING)
                && _commonDialogHandler.EmbeddingDone
            )
            {
                PI.WINDOWPOS pos = (PI.WINDOWPOS)PI.PtrToStructure(lparam, typeof(PI.WINDOWPOS));
                if ( !pos.flags.HasFlag(PI.SWP_.NOSIZE)
                    && (pos.hwnd == hWnd)
                    )
                {
                    handled = _commonDialogHandler.SetNewPosAndClientSize(new Point(pos.x, pos.y), new Size(pos.cx, pos.cy));
                    if (!handled)
                    {
                        pos.flags |= PI.SWP_.NOSIZE;
                        PI.StructureToPtr(pos, lparam);
                    }

                    retValue = IntPtr.Zero;
                }
            }
            return handled ? retValue :base.HookProc(hWnd, msg, wparam, lparam);
        }

    }
}
