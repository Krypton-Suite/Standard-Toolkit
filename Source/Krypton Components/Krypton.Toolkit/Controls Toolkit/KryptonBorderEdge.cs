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

/// <summary>
/// Display text and images with the styling features of the Krypton Toolkit
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonBorderEdge), "ToolboxBitmaps.KryptonBorderEdge.bmp")]
[DefaultEvent(nameof(Paint))]
[DefaultProperty(nameof(Orientation))]
[Designer(typeof(KryptonBorderEdgeDesigner))]
[DesignerCategory(@"code")]
[Description(@"Displays a vertical or horizontal border edge.")]
public class KryptonBorderEdge : VisualControlBase
{
    #region Instance Fields
    private Orientation _orientation;
    private readonly PaletteBorderInheritRedirect _borderRedirect;
    private PaletteBorderEdge _stateCurrent;
    private PaletteState _state;
    private readonly ViewDrawPanel _drawPanel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonBorderEdge class.
    /// </summary>
    public KryptonBorderEdge()
    {
        // The label cannot take the focus
        SetStyle(ControlStyles.Selectable, false);

        // Set default label style
        _orientation = Orientation.Horizontal;

        // Create the palette storage
        _borderRedirect = new PaletteBorderInheritRedirect(Redirector, PaletteBorderStyle.ControlClient);
        StateCommon = new PaletteBorderEdgeRedirect(_borderRedirect, NeedPaintDelegate);
        StateDisabled = new PaletteBorderEdge(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteBorderEdge(StateCommon, NeedPaintDelegate);
        _stateCurrent = StateNormal;
        _state = PaletteState.Normal;

        // Our view contains just a simple canvas that covers entire client area
        _drawPanel = new ViewDrawPanel(StateNormal);

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawPanel);

        // We want to be auto sized by default, but not the property default!
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
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
        set => base.Font = value;
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
    /// Gets or sets the tab order of the KryptonSplitterPanel within its KryptonSplitContainer.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int TabIndex
    {
        get => base.TabIndex;
        set => base.TabIndex = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can give the focus to this KryptonSplitterPanel using the TAB key.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool TabStop
    {
        get => base.TabStop;
        set => base.TabStop = value;
    }

    /// <summary>
    /// Gets and sets the internal padding space.
    /// </summary>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    /// <summary>
    /// Gets and sets the automatic resize of the control to fit contents.
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies if the control grows and shrinks to fit the contents exactly.")]
    [DefaultValue(AutoSizeMode.GrowAndShrink)]
    public AutoSizeMode AutoSizeMode
    {
        // ReSharper disable RedundantBaseQualifier
        get => base.GetAutoSizeMode();
        // ReSharper restore RedundantBaseQualifier

        set
        {
            // ReSharper disable RedundantBaseQualifier
            if (value != base.GetAutoSizeMode())
            {
                base.SetAutoSizeMode(value);
                // ReSharper restore RedundantBaseQualifier

                // Only perform an immediate layout if
                // currently performing auto size operations
                if (AutoSize)
                {
                    PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Border style.")]
    public PaletteBorderStyle BorderStyle
    {
        get => _borderRedirect.Style;

        set
        {
            if (_borderRedirect.Style != value)
            {
                _borderRedirect.Style = value;
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetBorderStyle() => BorderStyle = PaletteBorderStyle.ControlClient;

    private bool ShouldSerializeBorderStyle() => BorderStyle != PaletteBorderStyle.ControlClient;

    /// <summary>
    /// Gets and sets the orientation of the border edge used to determine sizing.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Orientation of border edge used to determine sizing.")]
    [DefaultValue(Orientation.Horizontal)]
    public virtual Orientation Orientation
    {
        get => _orientation;

        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to the common border edge appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common border edge appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorderEdgeRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled border edge appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled border edge appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorderEdge StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal border edge appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal border edge appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorderEdge StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Get the preferred size of the control based on a proposed size.
    /// </summary>
    /// <param name="proposedSize">Starting size proposed by the caller.</param>
    /// <returns>Calculated preferred size.</returns>
    public override Size GetPreferredSize(Size proposedSize)
    {
        // Let base class continue with standard calculations
        proposedSize = base.GetPreferredSize(proposedSize);

        // Do we need to apply the border width?
        if (AutoSize)
        {
            if (Orientation == Orientation.Horizontal)
            {
                proposedSize.Height = _stateCurrent.GetBorderWidth(_state);
            }
            else
            {
                proposedSize.Width = _stateCurrent.GetBorderWidth(_state);
            }
        }

        return proposedSize;
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) =>
        // Request fixed state from the view
        _drawPanel.FixedState = state;
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(50, 50);

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Cache the new state
        if (Enabled)
        {
            _stateCurrent = StateNormal;
            _state = PaletteState.Normal;
        }
        else
        {
            _stateCurrent = StateDisabled;
            _state = PaletteState.Disabled;
        }

        // Push correct palettes into the view
        _drawPanel.SetPalettes(_stateCurrent);

        // Update with latest enabled state
        _drawPanel.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }
    #endregion
}