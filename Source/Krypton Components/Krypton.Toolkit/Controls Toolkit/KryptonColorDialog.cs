#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Represents a common dialog box that displays colours
    /// that are currently installed on the system.
    /// </summary>
    [ToolboxBitmap(typeof(ColorDialog)), Description("Displays a Kryptonised version of the standard Colour dialog, which displays colours that are currently installed on the system.")]
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
        /// Changes the default Icon to Developer set
        /// </summary>
        public Icon Icon
        {
            get => _commonDialogHandler.Icon;
            set => _commonDialogHandler.Icon = value;
        }

        /// <summary>
        /// Changes the default Icon to Developer set
        /// </summary>
        [DefaultValue(false)]
        public bool ShowIcon
        {
            get => _commonDialogHandler.ShowIcon;
            set => _commonDialogHandler.ShowIcon = value;
        }

        /// <summary>
        /// Represents a common dialog box that displays colours
        /// that are currently installed on the system.
        /// </summary>
        public KryptonColorDialog() =>
            _commonDialogHandler = new CommonDialogHandler(true)
            {
                ClickCallback = ClickCallback,
                Icon = CommonDialogIcons.Colour_V10,
                ShowIcon = false
            };

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
            if (msg == PI.WM_.INITDIALOG
                && !FullOpen
                )
            {
                // Find the Static colour set 000002D0
                var clrColourBox = _commonDialogHandler.Controls.FirstOrDefault(ctl => ctl.DlgCtrlId == 0x000002D0);
                var rcClient = clrColourBox.WinInfo.rcClient;
                var lpPoint = new PI.POINT(rcClient.left, rcClient.top);
                PI.ScreenToClient(hWnd, ref lpPoint);
                clrColourBox.ClientLocation = new Point(lpPoint.X, lpPoint.Y);
                clrColourBox.Size = new Size(rcClient.right - rcClient.left, rcClient.bottom - rcClient.top);

                // Find the bottom of the OK button (Might not have OK text !!) 00000001
                var btnOk = _commonDialogHandler.Controls.FirstOrDefault(ctl => ctl.DlgCtrlId == 0x00000001);

                // Now adjust the size so that it the correct display on "All" supported OS's
                // https://github.com/Krypton-Suite/Standard-Toolkit/issues/415
                Size toolBoxSize = _commonDialogHandler._toolBox.ClientSize;
                toolBoxSize.Width = clrColourBox.Size.Width + 2 * clrColourBox.ClientLocation.X;
                toolBoxSize.Height = btnOk.ClientLocation.Y + btnOk.Size.Height *3 /2;

                _commonDialogHandler._toolBox.ClientSize = toolBoxSize;
            }

            if (!handled
                && (msg == PI.WM_.WINDOWPOSCHANGING)
                && _commonDialogHandler.EmbeddingDone
            )
            {
                PI.WINDOWPOS pos = (PI.WINDOWPOS)PI.PtrToStructure(lparam, typeof(PI.WINDOWPOS));
                if (!pos.flags.HasFlag(PI.SWP_.NOSIZE)
                    && (pos.hwnd == hWnd)
                    )
                {
                    var newSize = new Size(pos.cx, pos.cy);
                    if (!FullOpen
                        && newSize.Width > _commonDialogHandler._toolBox.Size.Width
                    )
                    {
                        // Need to fiddle the width and height to workaround the Magic hidden "&d" button
                        // https://github.com/Krypton-Suite/Standard-Toolkit/issues/416
                        if (!ShowIcon)
                        {
                            newSize.Width -= 16;
                        }

                        newSize.Height -= 44;
                    }

                    handled = _commonDialogHandler.SetNewPosAndClientSize(new Point(pos.x, pos.y), newSize);
                    if (!handled)
                    {
                        pos.flags |= PI.SWP_.NOSIZE;
                        PI.StructureToPtr(pos, lparam);
                    }

                    retValue = IntPtr.Zero;
                }
            }
            return handled ? retValue : base.HookProc(hWnd, msg, wparam, lparam);
        }

    }
}
