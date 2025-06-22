#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <inheritdoc />
[ToolboxBitmap(typeof(TableLayoutPanel)), Description("A Kryptonised version of the TableLayoutPanel. Handles the layout of its components, and arranges them in the format of a table automatically.")]
public class KryptonTableLayoutPanel : TableLayoutPanel
{
    #region Instance Fields
    private readonly KryptonPanel _backGroundPanel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTableLayoutPanel class.
    /// </summary>
    public KryptonTableLayoutPanel()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        // If you want a transparent background then use the normal TableLayoutPanel
        SetStyle(ControlStyles.SupportsTransparentBackColor, false);    

        // Yes, we want to be drawn double buffered by default
        base.DoubleBuffered = true;

        _backGroundPanel = new KryptonPanel()
        {
            TabStop = false,
            Visible = false // Has to be hidden in case there is a scroll being used
        };

        _backGroundPanel.StateCommon.PropertyChanged += State_PropertyChanged;
        _backGroundPanel.StateDisabled.PropertyChanged += State_PropertyChanged;
        _backGroundPanel.StateNormal.PropertyChanged += State_PropertyChanged;

        base.BackgroundImageLayout = ImageLayout.None;
    }

    private void State_PropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        // Handle explicit settings to the controls
        BackGroundPanel_Refreshed();

    #endregion

    #region Public Override designers
    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => _backGroundPanel.BackColor;
        set
        {
            if (value == Color.Transparent)
            {
                throw new NotSupportedException(
                    @"If you want a transparent background then use the normal TableLayoutPanel");
            }

            _backGroundPanel.BackColor = value;
            BackGroundPanel_Refreshed();
        }
    }

    /// <summary>
    /// Gets or sets the font of the text Displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [AmbientValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value!;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets the background image Displayed in the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Image? BackgroundImage
    {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
    }

    /// <summary>
    /// Gets or sets the background image layout.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageLayout BackgroundImageLayout
    {
        get => base.BackgroundImageLayout;
        set => base.BackgroundImageLayout = value;
    }

    #endregion

    #region Public Panel Exposure

    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut menu to show when the user right-clicks the page.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _backGroundPanel.KryptonContextMenu;
        set => _backGroundPanel.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Palette applied to drawing.")]
    public PaletteMode PaletteMode
    {
        [DebuggerStepThrough]
        get => _backGroundPanel.PaletteMode;

        set
        {
            _backGroundPanel.PaletteMode = value;
            BackGroundPanel_Refreshed();
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    /// <summary>
    /// Resets the PaletteMode property to its default value.
    /// </summary>
    public void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Custom palette applied to drawing.")]
    [DefaultValue(null)]
    public PaletteBase Palette
    {
        [DebuggerStepThrough]
        get => _backGroundPanel.Palette!;
        set
        {
            _backGroundPanel.Palette = value;
            BackGroundPanel_Refreshed();
        }
    }

    /// <summary>
    /// Gets and sets the panel style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Panel style.")]
    public PaletteBackStyle PanelBackStyle
    {
        get => _backGroundPanel.PanelBackStyle;
        set
        {
            _backGroundPanel.PanelBackStyle = value;
            BackGroundPanel_Refreshed();
        }
    }

    private bool ShouldSerializePanelBackStyle() => PanelBackStyle != PaletteBackStyle.PanelClient;

    private void ResetPanelBackStyle() => PanelBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets access to the common panel appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common panel appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _backGroundPanel.StateCommon;

    private bool ShouldSerializeStateCommon() => !_backGroundPanel.StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _backGroundPanel.StateDisabled;

    private bool ShouldSerializeStateDisabled() => !_backGroundPanel.StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _backGroundPanel.StateNormal;

    private bool ShouldSerializeStateNormal() => !_backGroundPanel.StateNormal.IsDefault;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) => _backGroundPanel.SetFixedState(state);
    #endregion

    #region Pass Messages to Panel
    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        _backGroundPanel.Enabled = Enabled;
        BackGroundPanel_Refreshed();
    }

    /// <inheritdoc />
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        _backGroundPanel.Size = Size;
        SetToBehindTable();
    }

    private Bitmap _bm;
    private void BackGroundPanel_Refreshed()
    {
        // Fix for #774: https://github.com/Krypton-Suite/Standard-Toolkit/issues/774
        if (_backGroundPanel.Height == 0 || _backGroundPanel.Width == 0)
        {
            return;
        }

        _bm = new Bitmap(_backGroundPanel.Width, _backGroundPanel.Height, PixelFormat.Format32bppRgb);
        _backGroundPanel.DrawToBitmap(_bm,
            new Rectangle(0, 0, _backGroundPanel.Width, _backGroundPanel.Height));
        BackgroundImage = _bm;
    }

    private void SetToBehindTable()
    {
        if (Parent == null)
        {
            return;
        }

        if (_backGroundPanel.Parent == null)
        {
            Parent.Controls.Add(_backGroundPanel);
            //_backGroundPanel.Parent = Parent;
            var index = Parent.Controls.GetChildIndex(this);
            Parent.Controls.SetChildIndex(_backGroundPanel, index + 1);
        }
        BackGroundPanel_Refreshed();
    }

    /// <inheritdoc />
    protected override void OnLocationChanged(EventArgs e)
    {
        base.OnLocationChanged(e);
        var location = Location;
        location.Offset(10, 10);
        _backGroundPanel.Location = location;
        SetToBehindTable();
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            // We need to snoop the need to show a context menu
            case PI.WM_.CONTEXTMENU:
            {
                // Only interested in overriding the behavior when we have a krypton context menu...
                if (KryptonContextMenu != null)
                {
                    // Extract the screen mouse position (if might not actually be provided)
                    var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                    // If keyboard activated, the menu position is centered
                    if (((int)(long)m.LParam) == -1)
                    {
                        mousePt = new Point(Width / 2, Height / 2);
                    }
                    else
                    {
                        mousePt = PointToClient(mousePt);

                        // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                        // of the showing context menu just like it happens for a ContextMenuStrip.
                        mousePt.X -= 1;
                        mousePt.Y -= 1;
                    }

                    // If the mouse position is within our client area
                    if (ClientRectangle.Contains(mousePt))
                    {
                        // Show the context menu
                        KryptonContextMenu.Show(this, PointToScreen(mousePt));

                        // We eat the message!
                        return;
                    }
                }

                break;
            }
            case PI.WM_.WINDOWPOSCHANGED:
            {
                // Do the move thing first
                base.WndProc(ref m);
                PI.WINDOWPOS structure = (PI.WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(PI.WINDOWPOS))!;
                if (!structure.flags.HasFlag(PI.SWP_.NOZORDER))
                {
                    if (_backGroundPanel.Parent != null
                        && Parent != null)
                    {
                        var index = Parent.Controls.GetChildIndex(this);
                        Parent.Controls.SetChildIndex(_backGroundPanel, index + 1);
                        BackGroundPanel_Refreshed();
                    }
                }

                // We ate the message!
                return;
            }
        }


        base.WndProc(ref m);
    }
    #endregion
}