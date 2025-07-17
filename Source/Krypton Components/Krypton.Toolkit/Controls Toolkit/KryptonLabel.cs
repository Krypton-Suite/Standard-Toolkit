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
[ToolboxBitmap(typeof(KryptonLabel), "ToolboxBitmaps.KryptonLabel.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[Designer(typeof(KryptonLabelDesigner))]
[DesignerCategory(@"code")]
[Description(@"Displays descriptive information.")]
public class KryptonLabel : VisualSimpleBase, IContentValues
{
    #region Instance Fields
    private LabelStyle _style;
    private VisualOrientation _orientation;
    private readonly ViewDrawContent _drawContent;
    private readonly PaletteContentInheritRedirect _paletteCommonRedirect;
    private KryptonCommand? _command;
    private bool _useMnemonic;
    private bool _wasEnabled;
    private Control? _target;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the KryptonCommand property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the KryptonCommand property changes.")]
    public event EventHandler? KryptonCommandChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLabel class.
    /// </summary>
    public KryptonLabel()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.UseTextForAccessibility, true);
        // The label cannot take the focus
        SetStyle(ControlStyles.Selectable, false);

        // Set default properties
        _style = LabelStyle.NormalPanel;
        _useMnemonic = true;
        _orientation = VisualOrientation.Top;
        _target = null;
        EnabledTarget = true;

        // Create content storage
        Values = new LabelValues(NeedPaintDelegate);
        Values.TextChanged += OnLabelTextChanged;

        // Create palette redirector
        _paletteCommonRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalPanel);

        // Create the palette provider
        StateCommon = new PaletteContent(_paletteCommonRedirect, NeedPaintDelegate);
        StateDisabled = new PaletteContent(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteContent(StateCommon, NeedPaintDelegate);

        // Our view contains background and border with content inside
        _drawContent = new ViewDrawContent(StateNormal, this, VisualOrientation.Top)
        {
            UseMnemonic = _useMnemonic
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawContent);

        // We want to be auto sized by default, but not the property default!
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }
    #endregion

    #region Public
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
    /// Gets and sets the mode for when auto sizing.
    /// </summary>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(AutoSizeMode.GrowAndShrink)]
    public new AutoSizeMode AutoSizeMode
    {
        get => base.AutoSizeMode;
        set => base.AutoSizeMode = value;
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
    /// Gets or sets the text associated with this control. 
    /// </summary>
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [Localizable(false)]
    [AllowNull]
    public override string Text
    {
        get => Values.Text;

        set => Values.Text = value;
    }

    private bool ShouldSerializeText()
        => false;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public override void ResetText() =>
        // Map onto the text property from the label values
        Values.ResetText();

    /// <summary>
    /// Gets and sets the visual orientation of the control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Visual orientation of the control.")]
    [DefaultValue(VisualOrientation.Top)]
    public virtual VisualOrientation Orientation
    {
        get => _orientation;

        set
        {
            if (_orientation != value)
            {
                _orientation = value;

                // Update the associated visual element that is effected
                _drawContent.Orientation = value;

                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeOrientation()
        => _orientation != VisualOrientation.Top;

    private void ResetOrientation() => _orientation = VisualOrientation.Top;

    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Label style.")]
    [DefaultValue(LabelStyle.NormalPanel)]
    public LabelStyle LabelStyle
    {
        get => _style;
        set
        {
            if (_style != value)
            {
                _style = value;
                SetLabelStyle(_style);
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLabelStyle() => LabelStyle != LabelStyle.NormalPanel;

    private void ResetLabelStyle() => LabelStyle = LabelStyle.NormalPanel;

    /// <summary>
    /// Gets access to the label content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Label values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public LabelValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    /// <summary>
    /// Gets access to the common label appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common label appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled label appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal label appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets or sets a value indicating whether an ampersand is included in the text of the control. 
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true the first character after an ampersand will be used as a mnemonic.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _useMnemonic;

        set
        {
            if (_useMnemonic != value)
            {
                _useMnemonic = value;
                _drawContent.UseMnemonic = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to the target for mnemonic and click actions.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Target control for mnemonic and click actions.")]
    [DefaultValue(null)]
    public virtual Control? Target
    {
        get => _target;
        set => _target = value;
    }

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the label.")]
    [DefaultValue(null)]
    public virtual KryptonCommand? KryptonCommand
    {
        get => _command;

        set
        {
            if (_command != value)
            {
                if (_command != null)
                {
                    _command.PropertyChanged -= OnCommandPropertyChanged;
                }
                else
                {
                    _wasEnabled = Enabled;
                }

                _command = value;
                OnKryptonCommandChanged(EventArgs.Empty);

                if (_command != null)
                {
                    _command.PropertyChanged += OnCommandPropertyChanged;
                }
                else
                {
                    Enabled = _wasEnabled;
                }
            }
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) =>
        // Request fixed state from the view
        _drawContent.FixedState = state;
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => KryptonCommand?.Text ?? Values.GetShortText();

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => KryptonCommand?.ExtraText ?? Values.GetLongText();

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => KryptonCommand?.ImageSmall ?? Values.GetImage(state);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => KryptonCommand?.ImageTransparentColor ?? Values.GetImageTransparentColor(state);
    #endregion

    #region Protected
    /// <summary>
    /// Gets access to the view element for the label.
    /// </summary>
    protected virtual ViewDrawContent ViewDrawContent => _drawContent;

    /// <summary>
    /// Gets and sets the enabled state of the target functionality.
    /// </summary>
    protected bool EnabledTarget { get; set; }

    /// <summary>
    /// Update the view elements based on the requested label style.
    /// </summary>
    /// <param name="style">New label style.</param>
    protected virtual void SetLabelStyle(LabelStyle style) => _paletteCommonRedirect.Style = CommonHelper.ContentStyleFromLabelStyle(style);

    /// <summary>
    /// Processes a mnemonic character.
    /// </summary>
    /// <param name="charCode">The mnemonic character entered.</param>
    /// <returns>true if the mnemonic was processed; otherwise, false.</returns>
    protected override bool ProcessMnemonic(char charCode)
    {
        // Are we allowed to process mnemonic?
        if (UseMnemonic && CanProcessMnemonic())
        {
            // Does the button primary text contain the mnemonic?
            if (IsMnemonic(charCode, Values.Text))
            {
                // Is target functionality enabled?
                if (EnabledTarget)
                {
                    // Do we have a target that can take the focus
                    if (Target is { CanFocus: true })
                    {
                        Target.Focus();
                        return true;
                    }
                }
            }
        }

        // No match found, let base class do standard processing
        return base.ProcessMnemonic(charCode);
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        // Is target functionality enabled?
        if (EnabledTarget)
        {
            // Do we have a target that can take the focus
            if (Target is { CanFocus: true })
            {
                Target.Focus();
            }
        }

        base.OnClick(e);
    }

    /// <summary>
    /// Raises the KryptonCommandChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnKryptonCommandChanged(EventArgs e)
    {
        KryptonCommandChanged?.Invoke(this, e);

        // Use the values from the new command
        if (KryptonCommand != null)
        {
            Enabled = KryptonCommand.Enabled;
        }

        // Redraw to update the text/extratext/image properties
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected virtual void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Enabled):
                Enabled = KryptonCommand?.Enabled ?? false;
                break;
            case nameof(Text):
            case @"ExtraText":
            case @"ImageSmall":
            case @"ImageTransparentColor":
                PerformNeedPaint(true);
                break;
        }
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        _drawContent.SetPalette(Enabled ? StateNormal : StateDisabled);

        _drawContent.Enabled = Enabled;

        // Need to relayout to reflect the change in state
        MarkLayoutDirty();

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(90, 25);

    /// <summary>
    /// Work out if this control needs to paint transparent areas.
    /// </summary>
    /// <returns>True if paint required; otherwise false.</returns>
    protected override bool EvalTransparentPaint() =>
        // Always need to draw the background because always transparent
        true;

    #endregion

    #region Implementation
    private void OnLabelTextChanged(object? sender, EventArgs e) => OnTextChanged(EventArgs.Empty);
    #endregion
}