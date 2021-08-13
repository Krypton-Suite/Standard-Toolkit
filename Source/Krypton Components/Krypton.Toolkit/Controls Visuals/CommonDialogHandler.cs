#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
 */
#endregion


namespace Krypton.Toolkit
{
    internal class CommonDialogHandler
    {
        private readonly KryptonManager _kryptonManager;

        private class Attributes
        {
            public IntPtr hWnd;
            public string Text;
            public /*PI.WS_*/ long ControlType;
            public PI.WINDOWINFO WinInfo;
            public string ClassName { get; set; }
            public Point ClientLocation { get; set; }
            public Size Size { get; set; }
        }

        private readonly List<Attributes> _controls = new();
        private readonly Color _backColour;
        private readonly Color _defaultFontColour;
        private readonly Color _inputFontColour;
        private IntPtr _backBrush = IntPtr.Zero;
        private readonly Font _labelFont;

        public CommonDialogHandler()
        {
            // Gain access to the global palette
            _kryptonManager = new KryptonManager();
            _backColour = _kryptonManager.GlobalPalette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
            _defaultFontColour = _kryptonManager.GlobalPalette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
            _inputFontColour = _kryptonManager.GlobalPalette.GetContentShortTextColor1(PaletteContentStyle.InputControlStandalone, PaletteState.Normal);
            _labelFont = _kryptonManager.GlobalPalette.GetContentShortTextFont(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        }

        internal (bool handled, IntPtr retValue) HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            switch (msg)
            {
                case PI.WM_.SHOWWINDOW:
                    {
                        var childHandles = new List<IntPtr>();
                        GCHandle gch = GCHandle.Alloc(childHandles);
                        try
                        {
                            var childProc = new PI.WindowEnumProc(EnumerateChildWindow);
                            PI.EnumChildWindows(hWnd, childProc, GCHandle.ToIntPtr(gch));
                            // Pre-allocate 256 characters, since this is the maximum class name length.
                            var name = new StringBuilder(256);
                            if (gch.Target is List<IntPtr> list)
                            {
                                foreach (var child in list)
                                {
                                    var attributes = new Attributes
                                    {
                                        hWnd = child,
                                        ControlType = PI.GetWindowLong(child, PI.GWL_.STYLE)
                                    };
                                    PI.GetWindowInfo(child, out attributes.WinInfo);
                                    var nRet = PI.GetClassName(child, name, name.Capacity);
                                    if (nRet != 0)
                                        attributes.ClassName = name.ToString().ToLowerInvariant();
                                    _controls.Add(attributes);
                                }
                            }
                        }
                        finally
                        {
                            if (gch.IsAllocated)
                            {
                                gch.Free();
                            }
                        }

                        var labelLogFont = _labelFont.ToHfont();
                        var buttonFont = _kryptonManager.GlobalPalette.GetContentShortTextFont(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
                        var buttonLogFont = buttonFont.ToHfont();
                        var editFont = _kryptonManager.GlobalPalette.GetContentShortTextFont(PaletteContentStyle.InputControlStandalone, PaletteState.Normal);
                        var editLogFont = editFont.ToHfont();
                        foreach (Attributes control in _controls)
                        {
                            switch (control.ClassName)
                            {
                                case @"static":
                                case @"combobox":
                                case @"combolbox":
                                    PI.SendMessage(control.hWnd, PI.WM_.SETFONT, labelLogFont, new IntPtr(1));
                                    break;
                                case @"edit":
                                    PI.SendMessage(control.hWnd, PI.WM_.SETFONT, editLogFont, new IntPtr(1));
                                    break;
                                case @"button":
                                    {

                                        if ((control.WinInfo.dwStyle & PI.WS_.VISIBLE) != PI.WS_.VISIBLE)
                                            break;
                                        var text = new StringBuilder(64);
                                        PI.GetWindowText(control.hWnd, text, 64);
                                        control.Text = text.ToString();
                                        var rcClient = control.WinInfo.rcClient;
                                        var lpPoint = new PI.POINT(rcClient.left, rcClient.top);
                                        PI.ScreenToClient(hWnd, ref lpPoint);
                                        control.ClientLocation = new Point(lpPoint.X, lpPoint.Y);
                                        control.Size = new Size(rcClient.right - rcClient.left, rcClient.bottom - rcClient.top);

                                        if ((control.ControlType & PI.BS_.GROUPBOX) == PI.BS_.GROUPBOX)
                                        {
                                            PI.SendMessage(control.hWnd, PI.WM_.SETFONT, labelLogFont, new IntPtr(1));
                                        }
                                        else if ((control.ControlType & PI.BS_.AUTORADIOBUTTON) == PI.BS_.AUTORADIOBUTTON)
                                        {
                                            PI.SendMessage(control.hWnd, PI.WM_.SETFONT, buttonLogFont, new IntPtr(1));
                                        }
                                        else if (((control.ControlType & PI.BS_.AUTO3STATE) == PI.BS_.AUTO3STATE)
                                                 || ((control.ControlType & PI.BS_.AUTOCHECKBOX) == PI.BS_.AUTOCHECKBOX))
                                        {
                                            var panel = new KryptonPanel
                                            {
                                                Size = control.Size
                                            };
                                            panel.Location = control.ClientLocation;
                                            PI.SetParent(panel.Handle, hWnd);

                                            var button = new KryptonCheckBox
                                            {
                                                AutoSize = false,
                                                Text = control.Text,
                                                Dock = DockStyle.Fill,
                                                LabelStyle = LabelStyle.NormalPanel
                                            };
                                            panel.Controls.Add(button);
                                            button.Click += delegate (object sender, EventArgs args)
                                            {
                                                PI.SendMessage(control.hWnd, PI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                                            };
                                            PI.ShowWindow(control.hWnd, PI.ShowWindowCommands.SW_HIDE);
                                        }
                                        else // Normal Button
                                        {
                                            var panel = new KryptonPanel
                                            {
                                                Size = control.Size
                                            };
                                            panel.Location = control.ClientLocation;
                                            PI.SetParent(panel.Handle, hWnd);

                                            var button = new KryptonButton
                                            {
                                                AutoSize = false,
                                                Text = control.Text,
                                                Dock = DockStyle.Fill
                                            };
                                            panel.Controls.Add(button);
                                            button.Click += delegate
                                            {
                                                PI.SendMessage(control.hWnd, PI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                                            };
                                            PI.ShowWindow(control.hWnd, PI.ShowWindowCommands.SW_HIDE);
                                        }
                                    }
                                    break;
                                default:
                                    PI.SendMessage(control.hWnd, PI.WM_.SETFONT, labelLogFont, new IntPtr(1));
                                    break;
                            }

                        }
                        break;
                    }
                case PI.WM_.PAINT:
                    {
                        foreach (Attributes control in _controls)
                        {
                            if ((control.WinInfo.dwStyle & PI.WS_.VISIBLE) != PI.WS_.VISIBLE)
                                continue;
                            if (control.ClassName != @"button")
                                continue;
                            if ((control.ControlType & PI.BS_.GROUPBOX) == PI.BS_.GROUPBOX)
                            {
                                PI.PAINTSTRUCT ps = new();

                                // Do we need to BeginPaint or just take the given HDC?
                                var hdc = PI.BeginPaint(control.hWnd, ref ps);
                                using (Graphics g = Graphics.FromHdc(hdc))
                                {
                                    g.SmoothingMode = SmoothingMode.AntiAlias;
                                    var lineColor = _kryptonManager.GlobalPalette.GetBorderColor1(PaletteBorderStyle.ControlGroupBox, PaletteState.Normal);
                                    DrawRoundedRectangle(g, new Pen(lineColor), new Point(0, 10), control.Size - new Size(1, 11), 5);
                                    var font = _kryptonManager.GlobalPalette.GetContentShortTextFont(
                                        PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
                                    TextRenderer.DrawText(g, control.Text, font, new Point(4, 0), _defaultFontColour, _backColour,
                                        TextFormatFlags.HidePrefix | TextFormatFlags.NoClipping);
                                }

                                PI.EndPaint(control.hWnd, ref ps);
                            }
                        }

                        break;
                    }
                case PI.WM_.CTLCOLORDLG:
                    {
                        if (_backBrush == IntPtr.Zero)
                        {
                            _backBrush = PI.CreateSolidBrush(ColorTranslator.ToWin32(_backColour));
                        }
                        return (true, _backBrush);
                    }
                case PI.WM_.CTLCOLORSTATIC:
                    // WM_CTLCOLORSTATIC was the correct way to control the color of the group box title.
                    // However, it no longer works: If your application uses a manifest to include the version 6 comctl library,
                    // the Groupbox control no longer sends the WM_CTLCOLORSTATIC to its parent to get a brush

                    PI.SetTextColor(wparam, ColorTranslator.ToWin32(_defaultFontColour));
                    PI.SetBkColor(wparam, ColorTranslator.ToWin32(_backColour));
                    //PI.SetBkMode(wparam, ColorTranslator.ToWin32(Color.Transparent));
                    return (true, _backBrush);
                //else if (msg == PI.WM_.CTLCOLORBTN)
                //{
                //    // By default, the DefWindowProc function selects the default system colors for the button.
                //    // Buttons with the BS_PUSHBUTTON, BS_DEFPUSHBUTTON, or BS_PUSHLIKE styles do not use the returned brush.
                //    // Buttons with these styles are always drawn with the default system colors.
                //    // Drawing push buttons requires several different brushes-face, highlight, and shadow
                //    // but the WM_CTLCOLORBTN message allows only one brush to be returned.
                //    var fontColour = _kryptonManager.GlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
                //    var backColour = _kryptonManager.GlobalPalette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal);
                //    PI.SetTextColor(wparam, ColorTranslator.ToWin32(fontColour));
                //    PI.SetBkColor(wparam, ColorTranslator.ToWin32(backColour));
                //    //PI.SetBkMode(wparam, ColorTranslator.ToWin32(Color.Transparent));
                //    return _backBrush;
                //}
                //else if (msg == PI.WM_.CTLCOLORLISTBOX)
                //{
                //    var fontColour = _kryptonManager.GlobalPalette.GetContentShortTextColor1(PaletteContentStyle.InputControlStandalone, PaletteState.Normal);
                //    var backColour = _kryptonManager.GlobalPalette.GetBackColor1(PaletteBackStyle.InputControlStandalone, PaletteState.Normal);
                //    PI.SetTextColor(wparam, ColorTranslator.ToWin32(fontColour));
                //    PI.SetBkColor(wparam, ColorTranslator.ToWin32(backColour));
                //    PI.SetBkMode(wparam, ColorTranslator.ToWin32(Color.Black));
                //    return PI.CreateSolidBrush(ColorTranslator.ToWin32(backColour));
                //    ////return _backBrush;
                //}
                case PI.WM_.CTLCOLOREDIT:
                    {
                        var backColour = _kryptonManager.GlobalPalette.GetBackColor1(PaletteBackStyle.InputControlStandalone, PaletteState.Normal);
                        PI.SetTextColor(wparam, ColorTranslator.ToWin32(_inputFontColour));
                        PI.SetBkColor(wparam, ColorTranslator.ToWin32(backColour));
                        PI.SetBkMode(wparam, ColorTranslator.ToWin32(Color.Black));
                        return (true, PI.CreateSolidBrush(ColorTranslator.ToWin32(backColour)));
                    }
            }

            return (false, IntPtr.Zero);
        }

        private static bool EnumerateChildWindow(IntPtr hWnd, IntPtr lParam)
        {
            var result = false;
            GCHandle gch = GCHandle.FromIntPtr(lParam);
            if (gch.Target is List<IntPtr> list)
            {
                list.Add(hWnd);
                result = true; // return true as long as children are found
            }
            return result;
        }

        public static void DrawRoundedRectangle(Graphics g, Pen pen, Point location, Size size, int radius)
        {
            var roundRect = new RoundedRectangleF(size.Width, size.Height, radius, location.X, location.Y);
            g.DrawPath(pen, roundRect.Path);
        }
    }
}
