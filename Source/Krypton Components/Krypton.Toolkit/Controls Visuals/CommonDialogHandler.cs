#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2025. All rights reserved. 
 *  
 */
#endregion

#if NET8_0_OR_GREATER
using MethodInvoker = System.Windows.Forms.MethodInvoker;
#endif 

// ReSharper disable InconsistentNaming

namespace Krypton.Toolkit;

internal class CommonDialogHandler
{
    private readonly bool _embed;

    internal class Attributes
    {
        public IntPtr hWnd;
        public string Text;
        public PI.WINDOWINFO WinInfo;
        public string ClassName { get; set; }
        public Point ClientLocation { get; set; }
        public Size Size { get; set; }
        public int DlgCtrlId { get; set; }
        public VisualControlBase? Button { get; set; }
    }

    private readonly List<Attributes> _controls = [];
    internal readonly Color _backColour;
    private readonly Color _defaultFontColour;
    private readonly Color _inputFontColour;
    private readonly Font _labelFont;
    private bool _embeddingDone;
    internal KryptonForm? _wrapperForm;
    private bool _isResizable;
    private IntPtr _resizeHandle;
    private System.Timers.Timer _resizeTimer;

    public CommonDialogHandler(bool embed)
    {
        _embed = embed;
        // Gain access to the global palette
        var igp = KryptonManager.CurrentGlobalPalette;
        _backColour = igp.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
        _defaultFontColour = igp.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        _inputFontColour = igp.GetContentShortTextColor1(PaletteContentStyle.InputControlStandalone, PaletteState.Normal);
        _labelFont = igp.GetContentShortTextFont(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal)!;
    }

    internal string Title { get; set; }

    internal Icon Icon { get; set; }

    internal bool ShowIcon { get; set; }


    internal (bool handled, IntPtr retValue) HookProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            case PI.WM_.INITDIALOG:
            {
                if (!string.IsNullOrWhiteSpace(Title))
                {
                    PI.SetWindowText(hWnd, Title);
                }

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
                                DlgCtrlId = PI.GetDlgCtrlID(child)
                            };
                            PI.GetWindowInfo(child, out attributes.WinInfo);
                            var nRet = PI.GetClassName(child, name, name.Capacity);
                            if (nRet != 0)
                            {
                                attributes.ClassName = name.ToString().ToLowerInvariant();
                            }

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
                //var buttonFont = _kryptonManager.GlobalPalette.GetContentShortTextFont(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
                //var buttonLogFont = buttonFont.ToHfont();
                var editFont = KryptonManager.CurrentGlobalPalette.GetContentShortTextFont(PaletteContentStyle.InputControlStandalone, PaletteState.Normal);
                var editLogFont = editFont!.ToHfont();
                foreach (Attributes control in _controls)
                {
                    switch (control.ClassName)
                    {
                        case @"edit":
                            // Following is the magic required to handle the DlgEx
                            //if ((control.WinInfo.dwStyle & 0x2000) == 0x2000)
                        {
                            //    //PI.ES_.NUMBER == 0x2000
                            //    var text = new StringBuilder(64);
                            //    PI.GetWindowText(control.hWnd, text, 64);
                            //    control.Text = text.ToString();
                            var rcClient = control.WinInfo.rcClient;
                            var lpPoint = new PI.POINT(rcClient.left, rcClient.top);
                            PI.ScreenToClient(hWnd, ref lpPoint);
                            control.ClientLocation = new Point(lpPoint.X, lpPoint.Y);
                            control.Size = new Size(rcClient.right - rcClient.left, rcClient.bottom - rcClient.top + 4);
                            //    var panel = new KryptonPanel
                            //    {
                            //        Size = control.Size
                            //    };
                            //    panel.Location = control.ClientLocation;
                            //    PI.SetParent(panel.Handle, hWnd);

                            //    var button = new KryptonNumericUpDown
                            //    {
                            //        AutoSize = false,
                            //        Text = control.Text,
                            //        Dock = DockStyle.Fill,
                            //        InputControlStyle = InputControlStyle.Standalone,
                            //        Enabled = (control.WinInfo.dwStyle & PI.WS_.DISABLED) == 0
                            //    };
                            //    panel.Controls.Add(button);
                            //    control.Button = button;
                            //    button.NumericUpDown.ValueChanged += delegate(object sender, EventArgs args)
                            //    {
                            //        PI.SendMessage(control.hWnd, PI.WM_.SETTEXT, IntPtr.Zero,
                            //            button.NumericUpDown.Text);
                            //    };
                            //    button.Click += delegate (object sender, EventArgs args)
                            //    {
                            //        PI.SendMessage(control.hWnd, PI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                            //        ClickCallback?.Invoke(control);
                            //    };
                            //    PI.ShowWindow(control.hWnd, PI.ShowWindowCommands.SW_HIDE);
                        }
                            PI.SendMessage(control.hWnd, PI.WM_.SETFONT, editLogFont, new IntPtr(1));
                            break;

                        case @"button":
                        {
                            if ((control.WinInfo.dwStyle & PI.WS_.VISIBLE) != PI.WS_.VISIBLE)
                            {
                                break;
                            }

                            var text = new StringBuilder(64);
                            PI.GetWindowText(control.hWnd, text, 64);
                            control.Text = text.ToString();
                            var rcClient = control.WinInfo.rcClient;
                            var lpPoint = new PI.POINT(rcClient.left, rcClient.top);
                            PI.ScreenToClient(hWnd, ref lpPoint);
                            control.ClientLocation = new Point(lpPoint.X, lpPoint.Y);
                            control.Size = new Size(rcClient.right - rcClient.left, rcClient.bottom - rcClient.top);

                            if ((control.WinInfo.dwStyle & PI.BS_.GROUPBOX) == PI.BS_.GROUPBOX)
                            {
                                PI.SendMessage(control.hWnd, PI.WM_.SETFONT, labelLogFont, new IntPtr(1));
                            }
                            else if (((control.WinInfo.dwStyle & PI.BS_.AUTORADIOBUTTON) == PI.BS_.AUTORADIOBUTTON)
                                     || ((control.WinInfo.dwStyle & PI.BS_.RADIOBUTTON) == PI.BS_.RADIOBUTTON))
                            {
                                //PI.SendMessage(control.hWnd, PI.WM_.SETFONT, buttonLogFont, new IntPtr(1));
                                var panel = new KryptonPanel
                                {
                                    Size = control.Size
                                };
                                panel.Location = control.ClientLocation;
                                PI.SetParent(panel.Handle, hWnd);

                                var button = new KryptonRadioButton
                                {
                                    AutoCheck = (control.WinInfo.dwStyle & PI.BS_.AUTORADIOBUTTON) == PI.BS_.AUTORADIOBUTTON,
                                    AutoSize = false,
                                    Text = control.Text,
                                    Dock = DockStyle.Fill,
                                    LabelStyle = LabelStyle.NormalPanel,
                                    Enabled = (control.WinInfo.dwStyle & PI.WS_.DISABLED) == 0
                                };
                                panel.Controls.Add(button);
                                control.Button = button;
                                button.Click += delegate
                                {
                                    PI.SendMessage(control.hWnd, PI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                                    ClickCallback?.Invoke(control);
                                };
                                PI.ShowWindow(control.hWnd, PI.ShowWindowCommands.SW_HIDE);
                            }
                            else if (((control.WinInfo.dwStyle & PI.BS_.AUTO3STATE) == PI.BS_.AUTO3STATE)
                                     || ((control.WinInfo.dwStyle & PI.BS_._3STATE) == PI.BS_._3STATE)
                                     || ((control.WinInfo.dwStyle & PI.BS_.AUTOCHECKBOX) == PI.BS_.AUTOCHECKBOX)
                                     || ((control.WinInfo.dwStyle & PI.BS_.CHECKBOX) == PI.BS_.CHECKBOX))
                            {
                                var panel = new KryptonPanel
                                {
                                    Size = control.Size
                                };
                                panel.Location = control.ClientLocation;
                                PI.SetParent(panel.Handle, hWnd);

                                var button = new KryptonCheckBox
                                {
                                    AutoCheck = ((control.WinInfo.dwStyle & PI.BS_.AUTO3STATE) == PI.BS_.AUTO3STATE)
                                                || ((control.WinInfo.dwStyle & PI.BS_.AUTOCHECKBOX) == PI.BS_.AUTOCHECKBOX),
                                    ThreeState = ((control.WinInfo.dwStyle & PI.BS_.AUTO3STATE) == PI.BS_.AUTO3STATE)
                                                 || ((control.WinInfo.dwStyle & PI.BS_._3STATE) == PI.BS_._3STATE),
                                    AutoSize = false,
                                    Text = control.Text,
                                    Dock = DockStyle.Fill,
                                    LabelStyle = LabelStyle.NormalPanel,
                                    Enabled = (control.WinInfo.dwStyle & PI.WS_.DISABLED) == 0
                                };
                                panel.Controls.Add(button);
                                control.Button = button;
                                button.Click += delegate
                                {
                                    PI.SendMessage(control.hWnd, PI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                                    ClickCallback?.Invoke(control);
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
                                    Dock = DockStyle.Fill,
                                    Enabled = (control.WinInfo.dwStyle & PI.WS_.DISABLED) == 0
                                };
                                panel.Controls.Add(button);
                                control.Button = button;
                                button.Click += delegate
                                {
                                    PI.SendMessage(control.hWnd, PI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                                    ClickCallback?.Invoke(control);
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
                if (_embed && !_embeddingDone)
                {
                    PerformEmbedding(hWnd);
                    return (true, new IntPtr(1));
                }
            }
                break;

            case PI.WM_.DESTROY:
                if (_embeddingDone)
                {
                    _wrapperForm?.Close();
                }
                break;

            case PI.WM_.PAINT:
                foreach (Attributes control in _controls)
                {
                    if ((control.WinInfo.dwStyle & PI.WS_.VISIBLE) != PI.WS_.VISIBLE)
                    {
                        continue;
                    }

                    if (control.ClassName != @"button")
                    {
                        continue;
                    }

                    if ((control.WinInfo.dwStyle & PI.BS_.GROUPBOX) == PI.BS_.GROUPBOX)
                    {
                        var ps = new PI.PAINTSTRUCT();

                        // Do we need to BeginPaint or just take the given HDC?
                        var hdc = PI.BeginPaint(control.hWnd, ref ps);
                        if (hdc == IntPtr.Zero)
                        {
                            break;
                        }

                        using (Graphics g = Graphics.FromHdc(hdc))
                        {
                            using var gh = new GraphicsHint(g, PaletteGraphicsHint.AntiAlias);
                            var lineColor = KryptonManager.CurrentGlobalPalette.GetBorderColor1(PaletteBorderStyle.ControlGroupBox, PaletteState.Normal);
                            DrawRoundedRectangle(g, new Pen(lineColor), new Point(0, 10),
                                control.Size - new Size(1, 11), 5);
                            var font = KryptonManager.CurrentGlobalPalette.GetContentShortTextFont(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
                            TextRenderer.DrawText(g, control.Text, font, new Point(4, 0), _defaultFontColour,
                                _backColour,
                                TextFormatFlags.HidePrefix | TextFormatFlags.NoClipping);
                        }

                        PI.EndPaint(control.hWnd, ref ps);
                        PI.ReleaseDC(control.hWnd, hdc);
                    }
                }
                break;

            //case PI.WM_.ERASEBKGND:
            //    //PI.RECT rectClient;
            //    //PI.GetClientRect(hWnd, out rectClient);
            //    //PI.FillRect(wParam, ref rectClient, new IntPtr(ColorTranslator.ToWin32(_backColour)));
            //    return (true, new IntPtr(1));

            case PI.WM_.CTLCOLORDLG:
                PI.SetDCBrushColor(wParam, ColorTranslator.ToWin32(_backColour));
                PI.SetBkMode(wParam, ColorTranslator.ToWin32(Color.Transparent));
                PI.SetBkColor(wParam, ColorTranslator.ToWin32(_backColour));
                return (true, PI.GetStockObject(PI.StockObjects.DC_BRUSH));

            case PI.WM_.CTLCOLORSTATIC:
                // WM_CTLCOLORSTATIC was the correct way to control the color of the group box title.
                // However, it no longer works: If your application uses a manifest to include the version 6 comctl library,
                // the Groupbox control no longer sends the WM_CTLCOLORSTATIC to its parent to get a brush

                PI.SetTextColor(wParam, ColorTranslator.ToWin32(_defaultFontColour));
                PI.SetDCBrushColor(wParam, ColorTranslator.ToWin32(_backColour));
                PI.SetBkColor(wParam, ColorTranslator.ToWin32(_backColour));
                //PI.SetBkMode(wParam, ColorTranslator.ToWin32(Color.Transparent));
                return (true, PI.GetStockObject(PI.StockObjects.DC_BRUSH));

            case PI.WM_.CTLCOLORBTN:
            {
                // By default, the DefWindowProc function selects the default system colors for the button.
                // Buttons with the BS_PUSHBUTTON, BS_DEFPUSHBUTTON, or BS_PUSHLIKE styles do not use the returned brush.
                // Buttons with these styles are always drawn with the default system colors.
                // Drawing push buttons requires several different brushes-face, highlight, and shadow
                // but the WM_CTLCOLORBTN message allows only one brush to be returned.
                var fontColour = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
                var backColour = KryptonManager.CurrentGlobalPalette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal);
                PI.SetTextColor(wParam, ColorTranslator.ToWin32(fontColour));
                PI.SetDCBrushColor(wParam, ColorTranslator.ToWin32(backColour));
                PI.SetBkMode(wParam, ColorTranslator.ToWin32(Color.Transparent));
                return (true, PI.GetStockObject(PI.StockObjects.DC_BRUSH));
            }

            case PI.WM_.CTLCOLORLISTBOX:
                PI.SetTextColor(wParam, ColorTranslator.ToWin32(_defaultFontColour));
                PI.SetDCBrushColor(wParam, ColorTranslator.ToWin32(_backColour));
                PI.SetBkColor(wParam, ColorTranslator.ToWin32(_backColour));
                //PI.SetBkMode(wParam, ColorTranslator.ToWin32(Color.Transparent));
                return (true, PI.GetStockObject(PI.StockObjects.DC_BRUSH));

            case PI.WM_.CTLCOLOREDIT:
                PI.SetTextColor(wParam, ColorTranslator.ToWin32(_inputFontColour));
                PI.SetDCBrushColor(wParam, ColorTranslator.ToWin32(_backColour));
                PI.SetBkMode(wParam, ColorTranslator.ToWin32(Color.Transparent));
                return (true, PI.GetStockObject(PI.StockObjects.DC_BRUSH));

            case PI.WM_.CTLCOLORSCROLLBAR:
                PI.SetTextColor(wParam, ColorTranslator.ToWin32(_inputFontColour));
                PI.SetDCBrushColor(wParam, ColorTranslator.ToWin32(_inputFontColour));
                PI.SetBkColor(wParam, ColorTranslator.ToWin32(_inputFontColour));
                PI.SetBkMode(wParam, ColorTranslator.ToWin32(Color.Transparent));
                return (true, PI.GetStockObject(PI.StockObjects.DC_BRUSH));
        }

        return (false, IntPtr.Zero);
    }

    internal Action<Attributes /*control*/>? ClickCallback { get; set; }

    internal IReadOnlyList<Attributes> Controls => _controls.AsReadOnly();

    private void PerformEmbedding(IntPtr hWnd)
    {
        var controlType = PI.GetWindowLong(hWnd, PI.GWL_.STYLE);
        controlType &= ~(PI.WS_.POPUPWINDOW | PI.WS_.CAPTION | PI.WS_.DLGFRAME | PI.WS_.OVERLAPPEDWINDOW);
        controlType |= PI.WS_.CHILD | PI.WS_.VISIBLE | PI.WS_.GROUP;
        PI.SetWindowLong(hWnd, PI.GWL_.STYLE, controlType);

        var lExStyle = PI.GetWindowLong(hWnd, PI.GWL_.EXSTYLE);
        lExStyle &= ~(PI.WS_EX_.DLGMODALFRAME | PI.WS_EX_.CLIENTEDGE | PI.WS_EX_.STATICEDGE);
        PI.SetWindowLong(hWnd, PI.GWL_.EXSTYLE, lExStyle);
        PI.GetWindowInfo(hWnd, out var winInfo);
        var text = new StringBuilder(256);
        PI.GetWindowText(hWnd, text, 256);
        _wrapperForm = new KryptonForm
        {
            AutoScaleMode = AutoScaleMode.None,
            ClientSize = new Size(winInfo.rcClient.right - winInfo.rcClient.left, winInfo.rcClient.bottom - winInfo.rcClient.top),
            FormBorderStyle = _isResizable ? FormBorderStyle.SizableToolWindow : FormBorderStyle.FixedToolWindow,
            StartPosition = FormStartPosition.Manual,
            Name = text.ToString(),
            Text = text.ToString(),
            Location = new Point(winInfo.rcWindow.left, winInfo.rcWindow.top),
            Padding = new Padding(0),
            TopMost = true
        };

        if (ShowIcon)
        {
            _wrapperForm.FormBorderStyle = _isResizable ? FormBorderStyle.Sizable : FormBorderStyle.Fixed3D;
            _wrapperForm.MaximizeBox = false;
            _wrapperForm.MinimizeBox = false;
            _wrapperForm.Icon = Icon;
        }

        Size toolBoxClientSize = _wrapperForm.ClientSize;
        var kryptonPanel1 = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Location = new Point(0, 0),
            Name = "kryptonPanel1",
            ClientSize = toolBoxClientSize,
            TabIndex = 0,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };
        _wrapperForm.Controls.Add(kryptonPanel1);

        if (!_isResizable)
        {
            PI.MoveWindow(hWnd, 0, 0, toolBoxClientSize.Width, toolBoxClientSize.Height, false);
        }
        else
        {
            _resizeHandle = hWnd;
            _wrapperForm.Resize += FormResize;
        }

        var wrapperParent = PI.GetParent(hWnd);
        PI.SetParent(hWnd, kryptonPanel1.Handle);
        var nativeWindow = new NativeWindow();
        nativeWindow.AssignHandle(wrapperParent);
        _wrapperForm.Show(nativeWindow);
        _embeddingDone = true;
        if (_isResizable)
        {
            // Delay Send a WM_SIZE; Maybe due to the caching in the "Desktop Window Manager"
            _resizeTimer = new System.Timers.Timer(200)
            {
                AutoReset = false,
                Enabled = true
            };
            // Hook up the Elapsed event for the timer. 
            _resizeTimer.Elapsed += OnResizeTimedEvent;
        }
    }

    private void OnResizeTimedEvent(object? sender, ElapsedEventArgs e)
    {
        _resizeTimer.Dispose();
        if (_wrapperForm != null)
        {
            var clientSize = _wrapperForm.Size;
            // Delay Send a WM_SIZE; Maybe due to the caching in the "Desktop Window Manager"
            _wrapperForm.BeginInvoke((MethodInvoker)(() =>
                    PI.SetWindowPos(_wrapperForm.Handle, IntPtr.Zero, 0, 0, clientSize.Width + 1, clientSize.Height,
                        PI.SWP_.NOACTIVATE | PI.SWP_.NOMOVE |
                        PI.SWP_.NOZORDER | PI.SWP_.NOCOPYBITS |
                        PI.SWP_.NOOWNERZORDER | PI.SWP_.ASYNCWINDOWPOS)
                ));
        }
    }

    private void FormResize(object? sender, EventArgs e)
    {
        if (_resizeHandle != IntPtr.Zero)
        {
            if (_wrapperForm != null)
            {
                var clientSize = _wrapperForm.ClientSize;
                _wrapperForm.SuspendLayout();
                PI.MoveWindow(_resizeHandle, 0, 0, clientSize.Width, clientSize.Height, true);
                _wrapperForm.ResumeLayout(false);
            }
        }
    }

    internal bool EmbeddingDone => _embeddingDone;

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

    private static void DrawRoundedRectangle(Graphics g, Pen pen, Point location, Size size, int radius)
    {
        using var gh = new GraphicsHint(g, PaletteGraphicsHint.AntiAlias);
        var roundRect = new RoundedRectangleF(size.Width, size.Height, radius, location.X, location.Y);
        g.DrawPath(pen, roundRect.Path);
    }

    internal bool SetNewPosAndClientSize(Point loc, Size size)
    {
        if (size == Size.Empty)
        {
            return false; // Probably already been triggered !
        }

        if (_wrapperForm != null)
        {
            _wrapperForm.Location = loc;
            _wrapperForm.ClientSize = size;
        }

        return true;
    }

    public void SetResizable(bool isResizable) => _isResizable = isResizable;
}