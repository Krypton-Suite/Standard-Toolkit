#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2025. All rights reserved. 
 *  
 */
#endregion

// Used only for this class
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Represents a common dialog box that displays colours
/// that are currently installed on the system.
/// </summary>
[ToolboxBitmap(typeof(ColorDialog), "ToolboxBitmaps.KryptonColorDialog.png"),
 Description("Displays a Kryptonised version of the standard Colour dialog, which displays colours that are currently installed on the system.")]
public class KryptonColorDialog : ColorDialog
{
    private readonly CommonDialogHandler _commonDialogHandler;

    #region Magic to get alpha working
    private readonly KryptonTrackBar _alphaSlider;
    private readonly KryptonLabel _alphaLabel;
    private readonly KryptonPanel _alphaPanel;
    private CommonDialogHandler.Attributes _redEdit;
    private CommonDialogHandler.Attributes _greenEdit;
    private CommonDialogHandler.Attributes _blueEdit;
    private readonly Timer _alphaUpdateTimer;
    #endregion

    /// <summary>
    /// Represents a common dialog box that displays colours
    /// that are currently installed on the system.
    /// </summary>
    public KryptonColorDialog()
    {
        _alphaSlider = new KryptonTrackBar
        {
            AutoSize = false,
            //this.kryptonTrackBar1.Location = new System.Drawing.Point(318, 69);
            Maximum = 255,
            Name = "kryptonTrackBar1",
            Orientation = Orientation.Vertical,
            Size = new Size(23, 57),
            TabIndex = 27,
            Text = @"Alpha",
            TickStyle = TickStyle.TopLeft,
            TrackBarSize = PaletteTrackBarSize.Large,
            VolumeControl = true,
            Value = 255
        };
        _alphaSlider.ToolTipValues.Description = @"0";
        _alphaSlider.ToolTipValues.Heading = @"Alpha";
        _alphaSlider.ToolTipValues.EnableToolTips = true;
        _alphaSlider.ValueChanged += AlphaSlider_ValueChanged;

        _alphaLabel = new KryptonLabel
        {
            LabelStyle = LabelStyle.NormalPanel,
            Location = new Point(288, 97),
            Name = "kryptonLabel1",
            Orientation = VisualOrientation.Left,
            Size = new Size(24, 36),
            TabIndex = 28,
            Text = "255"
        };
        _alphaPanel = new KryptonPanel
        {
            TabIndex = 29
        };
        _alphaUpdateTimer = new System.Windows.Forms.Timer();
        _alphaUpdateTimer.Enabled = false;
        _alphaUpdateTimer.Tick += Timer1OnTick;
        _commonDialogHandler = new CommonDialogHandler(true)
        {
            ClickCallback = ClickCallback,
            Icon = DialogImageResources.Colour_V10,
            ShowIcon = false
        };
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        _alphaUpdateTimer.Enabled = false;
        base.Dispose(disposing);
    }

    /// <summary>
    /// Changes the title of the common Colour Dialog
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible)]
    public string Title
    {
        get => _commonDialogHandler.Title;
        set => _commonDialogHandler.Title = value;
    }

    /// <summary>
    /// Changes the default Icon to Developer set
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public Icon Icon
    {
        get => _commonDialogHandler.Icon;
        set => _commonDialogHandler.Icon = value;
    }

    /// <summary>
    /// Changes the default Icon to Developer set
    /// </summary>
    [DefaultValue(false)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public bool ShowIcon
    {
        get => _commonDialogHandler.ShowIcon;
        set => _commonDialogHandler.ShowIcon = value;
    }

    private bool _showAlphaSlider;

    /// <summary>
    /// Allows an alpha slider to be displayed 
    /// </summary>
    /// <remarks>
    /// Will force FullOpen to be true if set.
    /// </remarks>
    [DefaultValue(false)]
    public bool ShowAlphaSlider
    {
        get => _showAlphaSlider;
        set
        {
            _showAlphaSlider = value;
            if (value)
            {
                AllowFullOpen = true;
                FullOpen = true;
            }
            else
            {
                _alphaUpdateTimer.Enabled = false;
            }
        }
    }

    /// <summary>Gets or sets the color selected by the user.</summary>
    /// <returns>The color selected by the user. If a color is not selected, the default value is black.</returns>
    [Category("Data")]
    [Description("The color selected in the dialog box")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new Color Color
    {
        get => !_showAlphaSlider ? base.Color : Color.FromArgb(_alphaSlider.Value, base.Color);

        set
        {
            base.Color = value;
            if (_showAlphaSlider)
            {
                _alphaSlider.Value = value.A;
                _alphaSlider.ToolTipValues.Description = _alphaSlider.Value.ToString(CultureInfo.InvariantCulture);
                _alphaLabel.Text = _alphaSlider.ToolTipValues.Description;
            }
        }
    }

    private void Timer1OnTick(object? sender, EventArgs e)
    {
        var text = new StringBuilder(6);
        if (PI.GetWindowText(_redEdit.hWnd, text, 4) <= 0)
        {
            // Probably Closing, or in transition
            return;
        }

        var red = byte.Parse(text.ToString());
        PI.GetWindowText(_greenEdit.hWnd, text, 4);
        var green = byte.Parse(text.ToString());
        PI.GetWindowText(_blueEdit.hWnd, text, 4);
        var blue = byte.Parse(text.ToString());

        _alphaPanel.StateCommon.Color1 = Color.FromArgb(_alphaSlider.Value, red, green, blue);
    }

    private void AlphaSlider_ValueChanged(object? sender, EventArgs e)
    {
        _alphaSlider.ToolTipValues.Description = _alphaSlider.Value.ToString(CultureInfo.InvariantCulture);
        _alphaLabel.Text = _alphaSlider.ToolTipValues.Description;
    }

    private void ClickCallback(CommonDialogHandler.Attributes originalControl)
    {
        // When the expand is clicked
        // - Disable it
        // - Enable Add custom colour
        if (originalControl.DlgCtrlId == 0x000002CF)
        {
            originalControl.Button!.Enabled = false;
            foreach (CommonDialogHandler.Attributes control in _commonDialogHandler.Controls.Where(static control => control.DlgCtrlId == 0x000002C8))
            {
                control.Button!.Enabled = true;
                break;
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
        if (msg == PI.WM_.INITDIALOG)
        {
            if (!FullOpen)
            {
                // Find the Static colour set 000002D0
                var clrColourBox = _commonDialogHandler.Controls.FirstOrDefault(ctl => ctl.DlgCtrlId == 0x000002D0);
                if (clrColourBox != null)
                {
                    var rcClient = clrColourBox.WinInfo.rcClient;
                    var lpPoint = new PI.POINT(rcClient.left, rcClient.top);
                    PI.ScreenToClient(hWnd, ref lpPoint);
                    clrColourBox.ClientLocation = new Point(lpPoint.X, lpPoint.Y);
                    clrColourBox.Size = new Size(rcClient.right - rcClient.left, rcClient.bottom - rcClient.top);
                }
                // Find the bottom of the OK button (Might not have OK text !!) 00000001
                var btnOk = _commonDialogHandler.Controls.First(static ctl => ctl.DlgCtrlId == 0x00000001);

                // Now adjust the size so that it the correct display on "All" supported OS's
                // https://github.com/Krypton-Suite/Standard-Toolkit/issues/415
                Size toolBoxSize = _commonDialogHandler._wrapperForm!.ClientSize;
                if (clrColourBox != null)
                {
                    toolBoxSize.Width = clrColourBox.Size.Width + 2 * clrColourBox.ClientLocation.X;
                }

                if (btnOk != null)
                {
                    toolBoxSize.Height = btnOk.ClientLocation.Y + btnOk.Size.Height * 3 / 2;
                }

                _commonDialogHandler._wrapperForm.ClientSize = toolBoxSize;
            }
            else
            {
                if (_showAlphaSlider)
                {
                    // Find the Static colour set 000002C5
                    var clrSolidColourBox = _commonDialogHandler.Controls.FirstOrDefault(ctl => ctl.DlgCtrlId == 0x000002C5);
                    if (clrSolidColourBox != null)
                    {
                        var rcClient = clrSolidColourBox.WinInfo.rcClient;
                        var lpPoint = new PI.POINT(rcClient.left, rcClient.top);
                        PI.ScreenToClient(hWnd, ref lpPoint);
                        _alphaPanel.Location = new Point(lpPoint.X, lpPoint.Y);
                        _alphaPanel.Size = new Size((rcClient.right - rcClient.left) / 2, rcClient.bottom - rcClient.top);
                    }

                    _commonDialogHandler._wrapperForm!.Controls[0].Controls.Add(_alphaPanel);
                    // find the colour edit box's
                    _redEdit = _commonDialogHandler.Controls.First(ctl => ctl.DlgCtrlId == 0x000002C2);
                    _greenEdit = _commonDialogHandler.Controls.First(ctl => ctl.DlgCtrlId == 0x000002C3);
                    _blueEdit = _commonDialogHandler.Controls.First(ctl => ctl.DlgCtrlId == 0x000002C4);
                    // Add a slider
                    _alphaSlider.Location = _redEdit.ClientLocation with { X = _redEdit.ClientLocation.X + _redEdit.Size.Width + 4 };
                    //_commonDialogHandler._wrapperForm.ClientSize
                    _alphaSlider.Size = new Size(_commonDialogHandler._wrapperForm.ClientSize.Width - _alphaSlider.Location.X + 4, _blueEdit.ClientLocation.Y - _redEdit.ClientLocation.Y + _blueEdit.Size.Height);
                    _commonDialogHandler._wrapperForm.Controls[0].Controls.Add(_alphaSlider);
                    // Find the Add button 
                    var btnAdd = _commonDialogHandler.Controls.First(static ctl => ctl.DlgCtrlId == 0x00002C8);
                    btnAdd.Button!.Parent!.Width -= 16;

                    _alphaLabel.Location = Point.Add(_blueEdit.ClientLocation, new Size(_blueEdit.Size.Width + 2, _blueEdit.Size.Height));
                    _commonDialogHandler._wrapperForm.Controls[0].Controls.Add(_alphaLabel);
                    _alphaUpdateTimer.Enabled = true;
                }
            }
        }

        if (!handled
            && (msg == PI.WM_.WINDOWPOSCHANGING)
            && _commonDialogHandler.EmbeddingDone
           )
        {
            var pos = (PI.WINDOWPOS)PI.PtrToStructure(lparam, typeof(PI.WINDOWPOS))!;
            if (!pos.flags.HasFlag(PI.SWP_.NOSIZE)
                && (pos.hwnd == hWnd)
               )
            {
                var newSize = new Size(pos.cx, pos.cy);
                if (!FullOpen
                    && newSize.Width > _commonDialogHandler._wrapperForm!.Size.Width
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