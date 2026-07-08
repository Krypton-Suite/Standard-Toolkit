#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. AvilÃ©s (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Displays a check box with word-wrapped main text and optional subtext, using Krypton styling.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCheckBox), @"ToolboxBitmaps.KryptonCheckBox.bmp")]
[DefaultEvent(nameof(CheckedChanged))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(CheckState))]
[Designer(typeof(KryptonCheckBoxExtendedDesigner))]
[DesignerCategory(@"code")]
[DisplayName(@"Krypton CheckBox Extended")]
[Description(@"Displays a check box with word-wrapped text and optional subtext.")]
public class KryptonCheckBoxExtended : VisualSimpleBase, IContentValues
{
    #region Instance Fields

    private LabelStyle _style;
    private VisualOrientation _orientation;
    private readonly CheckBoxController _controller;
    private readonly ViewLayoutDocker _layoutDocker;
    private readonly ViewLayoutDocker _layoutCheckBoxGlyph;
    private readonly ViewDrawCheckBox _drawCheckBox;
    private readonly ViewDrawCheckBoxExtendedContent _drawContent;
    private readonly PaletteContentInheritRedirect _paletteCommonRedirect;
    private readonly PaletteRedirectCheckBox? _paletteCheckBoxImages;
    private readonly PaletteContentInheritOverride _overrideNormal;
    private KryptonCommand? _command;
    private VisualOrientation _checkPosition;
    private CheckState _checkState;
    private CheckState _wasCheckState;
    private bool _wasEnabled;
    private bool _checked;
    private bool _threeState;
    private bool _useMnemonic;
    private readonly CheckBoxExtendedLayoutValues _layoutValues;
    private readonly CheckBoxExtendedSubtextLinkValues _subtextLinkValues;
    private readonly ViewLayoutSeparator _textGapSeparator;
    private readonly SubtextLinkPresenter _subtextLinkPresenter;
    private Cursor? _savedCursor;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the control is double clicked with the mouse.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? DoubleClick;

    /// <summary>
    /// Occurs when the control is mouse double clicked with the mouse.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? MouseDoubleClick;

    /// <summary>
    /// Occurs when the value of the ImeMode property is changed.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? ImeModeChanged;

    /// <summary>
    /// Occurs when the value of the Checked property has changed.
    /// </summary>
    [Category(@"Misc")]
    [Description(@"Occurs whenever the Checked property has changed.")]
    public event EventHandler? CheckedChanged;

    /// <summary>
    /// Occurs when the value of the CheckState property has changed.
    /// </summary>
    [Category(@"Misc")]
    [Description(@"Occurs whenever the CheckState property has changed.")]
    public event EventHandler? CheckStateChanged;

    /// <summary>
    /// Occurs when the value of the KryptonCommand property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the KryptonCommand property changes.")]
    public event EventHandler? KryptonCommandChanged;

    /// <summary>
    /// Occurs when the user clicks a link within the subtext.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user clicks a link within the subtext.")]
    public event LinkLabelLinkClickedEventHandler? SubtextLinkClicked;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCheckBoxExtended"/> class.
    /// </summary>
    public KryptonCheckBoxExtended()
    {
        SetStyle(ControlStyles.StandardClick |
                 ControlStyles.StandardDoubleClick, false);

        _style = LabelStyle.NormalPanel;
        _orientation = VisualOrientation.Top;
        _checkPosition = VisualOrientation.Left;
        _checked = false;
        _threeState = false;
        _checkState = CheckState.Unchecked;
        _useMnemonic = true;
        AutoCheck = true;

        Values = new CheckBoxExtendedTextValues(NeedPaintDelegate);
        Values.TextChanged += OnCheckBoxTextChanged;
        _layoutValues = new CheckBoxExtendedLayoutValues(NeedPaintDelegate);
        _layoutValues.SetOwner(this);
        _subtextLinkValues = new CheckBoxExtendedSubtextLinkValues(NeedPaintDelegate);
        _subtextLinkValues.SetOwner(this);
        Images = new CheckBoxImages(NeedPaintDelegate);

        _paletteCommonRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalPanel);
        _paletteCheckBoxImages = new PaletteRedirectCheckBox(Redirector, Images);

        StateCommon = new PaletteContent(_paletteCommonRedirect, NeedPaintDelegate);
        StateDisabled = new PaletteContent(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteContent(StateCommon, NeedPaintDelegate);
        OverrideFocus = new PaletteContent(_paletteCommonRedirect, NeedPaintDelegate);

        ConfigureWrapTextDefaults();

        _overrideNormal = new PaletteContentInheritOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride, false);

        _drawContent = new ViewDrawCheckBoxExtendedContent(_overrideNormal, this, VisualOrientation.Top)
        {
            UseMnemonic = _useMnemonic,
            TestForFocusCues = true,
            SubtextSeparatorHeight = _layoutValues.SubtextSeparatorHeight
        };

        _drawCheckBox = new ViewDrawCheckBox(_paletteCheckBoxImages)
        {
            CheckState = _checkState
        };

        _layoutCheckBoxGlyph = new ViewLayoutDocker
        {
            { _drawCheckBox, ViewDockStyle.Top },
            { new ViewLayoutFill(), ViewDockStyle.Fill }
        };

        _textGapSeparator = new ViewLayoutSeparator(0, 0);

        _layoutDocker = new ViewLayoutDocker
        {
            { _layoutCheckBoxGlyph, ViewDockStyle.Left },
            { _textGapSeparator, ViewDockStyle.Left },
            { _drawContent, ViewDockStyle.Fill }
        };

        _subtextLinkPresenter = new SubtextLinkPresenter();
        _subtextLinkPresenter.LinkClicked += OnSubtextLinkPresenterLinkClicked;
        _subtextLinkPresenter.NonLinkClick += OnSubtextLinkPresenterNonLinkClick;
        Controls.Add(_subtextLinkPresenter);

        _controller = new CheckBoxController(_drawCheckBox, _layoutDocker, NeedPaintDelegate);
        _controller.Click += OnControllerClick;
        _controller.Enabled = true;
        _layoutDocker.MouseController = _controller;
        _layoutDocker.KeyController = _controller;

        UpdateForOrientation();
        ApplyTextGap();

        ViewManager = new ViewManager(this, _layoutDocker);

        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        ApplySubtextAppearance();
    }

    #endregion

    #region Public

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    /// <inheritdoc />
    // ToDo V120 LTS: Migrate designer editor to KryptonDesignerMultilineStringEditor (replaces System.ComponentModel.Design.MultilineStringEditor).
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public override string Text
    {
        get => Values.Text;
        set => Values.Text = value;
    }

    private bool ShouldSerializeText() => false;

    /// <inheritdoc />
    public override void ResetText() => Values.ResetText();
    /// <summary>
    /// Gets access to layout spacing values.
    /// </summary>
    [Category(@"CheckBox Extended")]
    [Description(@"Groups layout spacing properties for the check box extended control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxExtendedLayoutValues LayoutValues => _layoutValues;

    private bool ShouldSerializeLayoutValues() => !_layoutValues.IsDefault;

    /// <summary>
    /// Gets access to subtext link values.
    /// </summary>
    [Category(@"CheckBox Extended")]
    [Description(@"Groups subtext link properties for the check box extended control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxExtendedSubtextLinkValues SubtextLinkValues => _subtextLinkValues;

    private bool ShouldSerializeSubtextLinkValues() => !_subtextLinkValues.IsDefault;

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
                _drawContent.Orientation = value;
                UpdateForOrientation();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the position of the check box.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Visual position of the check box.")]
    [DefaultValue(VisualOrientation.Left)]
    [Localizable(true)]
    public virtual VisualOrientation CheckPosition
    {
        get => _checkPosition;

        set
        {
            if (_checkPosition != value)
            {
                _checkPosition = value;
                UpdateForOrientation();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Label style.")]
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

    private void ResetLabelStyle() => LabelStyle = LabelStyle.NormalPanel;

    private bool ShouldSerializeLabelStyle() => LabelStyle != LabelStyle.NormalPanel;

    /// <summary>
    /// Gets access to the label content.
    /// </summary>
    [Category(@"CheckBox Extended")]
    [Description(@"Groups main and subtext content properties.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxExtendedTextValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    /// <summary>
    /// Gets access to the image value overrides.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image value overrides.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxImages Images { get; }

    private bool ShouldSerializeImages() => !Images.IsDefault;

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
    /// Gets access to the label appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining label appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

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
    /// Gets or sets a value indicating if the component is in the checked state.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates if the component is in the checked state.")]
    [DefaultValue(false)]
    [Bindable(true)]
    public bool Checked
    {
        get => _checked;

        set
        {
            if (_checked != value)
            {
                _checked = value;
                _checkState = _checked ? CheckState.Checked : CheckState.Unchecked;
                OnCheckedChanged(EventArgs.Empty);
                OnCheckStateChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the check box is automatically changed state when clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Causes the check box to automatically change state when clicked.")]
    [DefaultValue(true)]
    public bool AutoCheck { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the component allows three states instead of two.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the component allows three states instead of two.")]
    [DefaultValue(false)]
    public bool ThreeState
    {
        get => _threeState;

        set
        {
            if (_threeState != value)
            {
                _threeState = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating the checked state of the component.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates the checked state of the component.")]
    [DefaultValue(CheckState.Unchecked)]
    [Bindable(true)]
    public CheckState CheckState
    {
        get => _checkState;

        set
        {
            if (_checkState != value)
            {
                _checkState = value;
                var newChecked = _checkState != CheckState.Unchecked;
                var checkedChanged = _checked != newChecked;
                _checked = newChecked;

                if (checkedChanged)
                {
                    OnCheckedChanged(EventArgs.Empty);
                }

                OnCheckStateChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the check button.")]
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
                    _wasCheckState = CheckState;
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
                    CheckState = _wasCheckState;
                }
            }
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="focus">Focus state for display.</param>
    /// <param name="enabled">Enabled state for display.</param>
    /// <param name="tracking">Tracking state for display.</param>
    /// <param name="pressed">Pressed state for display.</param>
    public virtual void SetFixedState(bool focus,
        bool enabled,
        bool tracking,
        bool pressed)
    {
        _controller.Enabled = false;
        _overrideNormal.Apply = focus;
        _drawContent.FixedState = enabled ? PaletteState.Normal : PaletteState.Disabled;
        _drawCheckBox.Enabled = enabled;
        _drawCheckBox.Tracking = tracking;
        _drawCheckBox.Pressed = pressed;
    }

    #endregion

    #region IContentValues

    /// <inheritdoc />
    public string GetShortText() => KryptonCommand?.Text ?? Values.GetShortText();

    /// <inheritdoc />
    public string GetLongText() => KryptonCommand?.ExtraText ?? Values.GetLongText();

    /// <inheritdoc />
    public Image? GetImage(PaletteState state) => KryptonCommand?.ImageSmall ?? Values.GetImage(state);

    /// <inheritdoc />
    public Color GetImageTransparentColor(PaletteState state) =>
        KryptonCommand?.ImageTransparentColor ?? Values.GetImageTransparentColor(state);

    /// <inheritdoc />
    public Image? GetOverlayImage(PaletteState state) => Values.GetOverlayImage(state);

    /// <inheritdoc />
    public Color GetOverlayImageTransparentColor(PaletteState state) => Values.GetOverlayImageTransparentColor(state);

    /// <inheritdoc />
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => Values.GetOverlayImagePosition(state);

    /// <inheritdoc />
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => Values.GetOverlayImageScaleMode(state);

    /// <inheritdoc />
    public float GetOverlayImageScaleFactor(PaletteState state) => Values.GetOverlayImageScaleFactor(state);

    /// <inheritdoc />
    public Size GetOverlayImageFixedSize(PaletteState state) => Values.GetOverlayImageFixedSize(state);

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnDoubleClick(EventArgs e) => DoubleClick?.Invoke(this, e);

    /// <inheritdoc />
    protected virtual void OnMouseDoubleClick(EventArgs e) => MouseDoubleClick?.Invoke(this, e);

    /// <inheritdoc />
    protected virtual void OnMouseImeModeChanged(EventArgs e) => ImeModeChanged?.Invoke(this, e);

    /// <inheritdoc />
    protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);

    /// <inheritdoc />
    protected virtual void OnCheckStateChanged(EventArgs e)
    {
        _drawCheckBox.CheckState = _checkState;
        CheckStateChanged?.Invoke(this, e);

        if (KryptonCommand != null)
        {
            KryptonCommand.CheckState = CheckState;
        }
    }

    /// <inheritdoc />
    protected override void OnGotFocus(EventArgs e)
    {
        if (!_drawContent.IsFixed)
        {
            _overrideNormal.Apply = true;
            PerformNeedPaint(false);
        }

        base.OnGotFocus(e);
    }

    /// <inheritdoc />
    protected virtual void OnKryptonCommandChanged(EventArgs e)
    {
        KryptonCommandChanged?.Invoke(this, e);

        if (KryptonCommand != null)
        {
            Enabled = KryptonCommand.Enabled;
            CheckState = KryptonCommand.CheckState;
        }

        PerformNeedPaint(true);
    }

    /// <inheritdoc />
    protected virtual void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Enabled):
                Enabled = KryptonCommand!.Enabled;
                break;
            case nameof(CheckState):
                CheckState = KryptonCommand!.CheckState;
                break;
            case nameof(Text):
            case @"ExtraText":
            case @"ImageSmall":
            case @"ImageTransparentColor":
                PerformNeedPaint(true);
                break;
        }
    }

    /// <inheritdoc />
    protected override void OnLostFocus(EventArgs e)
    {
        if (!_drawContent.IsFixed)
        {
            _overrideNormal.Apply = false;
            PerformNeedPaint(false);
        }

        base.OnLostFocus(e);
    }

    /// <inheritdoc />
    protected override void OnClick(EventArgs e)
    {
        if (AutoCheck)
        {
            CheckState = CheckState switch
            {
                CheckState.Unchecked => CheckState.Checked,
                CheckState.Checked => ThreeState ? CheckState.Indeterminate : CheckState.Unchecked,
                CheckState.Indeterminate => CheckState.Unchecked,
                _ => CheckState
            };
        }

        base.OnClick(e);
        KryptonCommand?.PerformExecute();
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        UpdateSubtextLinkCursor(e.Location);
        base.OnMouseMove(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        RestoreSubtextLinkCursor();
        base.OnMouseLeave(e);
    }

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new KryptonCheckBoxExtendedAccessibleObject(this);

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs? e)
    {
        ApplySubtextAppearance();
        UpdateSubtextLinkPresenter();
        base.OnPaint(e);
    }

    /// <inheritdoc />
    protected virtual void SetLabelStyle(LabelStyle style) =>
        _paletteCommonRedirect.Style = CommonHelper.ContentStyleFromLabelStyle(style);

    /// <inheritdoc />
    protected override bool ProcessMnemonic(char charCode)
    {
        if (UseMnemonic && AutoCheck && CanProcessMnemonic())
        {
            if (IsMnemonic(charCode, Values.Text))
            {
                if (!ContainsFocus)
                {
                    Focus();
                }

                OnClick(EventArgs.Empty);
                return true;
            }
        }

        return base.ProcessMnemonic(charCode);
    }

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        if (Enabled)
        {
            _drawContent.SetPalette(_overrideNormal);
        }
        else
        {
            _drawContent.SetPalette(StateDisabled);
        }

        _drawContent.Enabled = Enabled;
        _drawCheckBox.Enabled = Enabled;
        MarkLayoutDirty();
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override void OnRightToLeftChanged(EventArgs e)
    {
        UpdateForOrientation();
        base.OnRightToLeftChanged(e);
    }

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(300, 60);

    /// <inheritdoc />
    protected override bool EvalTransparentPaint() => true;

    /// <summary>
    /// Performs the default accessibility action for the control.
    /// </summary>
    internal void PerformAccessibilityClick()
    {
        if (!CanSelect)
        {
            return;
        }

        if (!ContainsFocus)
        {
            Focus();
        }

        OnClick(EventArgs.Empty);
    }

    #endregion

    #region Implementation

    private void ConfigureWrapTextDefaults()
    {
        PaletteContentText shortText = StateCommon.ShortText;
        shortText.MultiLine = InheritBool.True;
        shortText.MultiLineH = PaletteRelativeAlign.Near;
        shortText.TextH = PaletteRelativeAlign.Near;
        shortText.TextV = PaletteRelativeAlign.Near;
        shortText.Trim = PaletteTextTrim.Word;

        PaletteContentText longText = StateCommon.LongText;
        longText.MultiLine = InheritBool.True;
        longText.MultiLineH = PaletteRelativeAlign.Near;
        longText.TextH = PaletteRelativeAlign.Near;
        longText.TextV = PaletteRelativeAlign.Near;
        longText.Trim = PaletteTextTrim.Word;
    }

    private void ApplySubtextAppearance()
    {
        StateCommon.AdjacentGap = _layoutValues.SubtextSeparatorHeight;
        _drawContent.SubtextSeparatorHeight = _layoutValues.SubtextSeparatorHeight;
        _drawContent.SubtextFont = Values.SubtextFont;
        _drawContent.SubtextForeColor = Values.SubtextForeColor;

        if (Values.SubtextFont != null)
        {
            StateCommon.LongText.Font = Values.SubtextFont;
        }

        if (!Values.SubtextForeColor.IsEmpty)
        {
            StateCommon.LongText.Color1 = Values.SubtextForeColor;
            StateCommon.LongText.Color2 = Values.SubtextForeColor;
        }

        UpdateSubtextLinkPresenter();
    }

    internal void OnLayoutValuesChanged()
    {
        _drawContent.SubtextSeparatorHeight = _layoutValues.SubtextSeparatorHeight;
        StateCommon.AdjacentGap = _layoutValues.SubtextSeparatorHeight;
        ApplyTextGap();
        MarkLayoutDirty();
    }

    internal void OnSubtextLinkValuesChanged() => UpdateSubtextLinkPresenter();

    private void ApplyTextGap()
    {
        bool horizontalGap = _checkPosition is VisualOrientation.Left or VisualOrientation.Right;
        _textGapSeparator.SeparatorSize = horizontalGap
            ? new Size(_layoutValues.TextGap, 0)
            : new Size(0, _layoutValues.TextGap);
    }

    private void UpdateSubtextLinkPresenter()
    {
        bool useLinkPresenter = _subtextLinkValues.LinkArea.Length > 0 && !string.IsNullOrEmpty(Values.Subtext);
        _drawContent.SkipSubtextDrawing = useLinkPresenter;

        if (!useLinkPresenter)
        {
            _subtextLinkPresenter.Visible = false;
            return;
        }

        Rectangle subtextRect = _drawContent.SubtextLayoutRect;
        if (subtextRect.Width <= 0 || subtextRect.Height <= 0)
        {
            _subtextLinkPresenter.Visible = false;
            return;
        }

        Font subtextFont = Values.SubtextFont ?? Font;
        Color subtextColor = Values.SubtextForeColor.IsEmpty ? ForeColor : Values.SubtextForeColor;
        Color linkColor = _subtextLinkValues.LinkColor.IsEmpty ? SystemColors.HotTrack : _subtextLinkValues.LinkColor;

        _subtextLinkPresenter.SetBounds(subtextRect.X, subtextRect.Y, subtextRect.Width, subtextRect.Height, BoundsSpecified.All);
        _subtextLinkPresenter.Text = Values.Subtext;
        _subtextLinkPresenter.LinkArea = _subtextLinkValues.LinkArea;
        _subtextLinkPresenter.Font = subtextFont;
        _subtextLinkPresenter.ForeColor = subtextColor;
        _subtextLinkPresenter.LinkColor = linkColor;
        _subtextLinkPresenter.VisitedLinkColor = linkColor;
        _subtextLinkPresenter.Enabled = Enabled;
        _subtextLinkPresenter.RightToLeft = RightToLeft;
        _subtextLinkPresenter.Visible = true;
        _subtextLinkPresenter.BringToFront();
    }

    private void UpdateSubtextLinkCursor(Point clientLocation)
    {
        if (!Enabled
            || !_subtextLinkPresenter.Visible
            || !_subtextLinkPresenter.ContainsLinkPoint(clientLocation))
        {
            RestoreSubtextLinkCursor();
            return;
        }

        if (_savedCursor == null)
        {
            _savedCursor = Cursor;
            Cursor = Cursors.Hand;
        }
    }

    private void RestoreSubtextLinkCursor()
    {
        if (_savedCursor != null)
        {
            Cursor = _savedCursor;
            _savedCursor = null;
        }
    }

    private void OnSubtextLinkPresenterLinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) => OnSubtextLinkClicked(e);

    private void OnSubtextLinkPresenterNonLinkClick(object? sender, EventArgs e) => OnClick(EventArgs.Empty);

    private void OnSubtextLinkClicked(LinkLabelLinkClickedEventArgs e) => SubtextLinkClicked?.Invoke(this, e);

    private void OnCheckBoxTextChanged(object? sender, EventArgs e)
    {
        MarkLayoutDirty();
        OnTextChanged(EventArgs.Empty);
    }

    private void OnControllerClick(object? sender, EventArgs e) => OnClick(e);

    private void UpdateForOrientation()
    {
        ViewDockStyle dockStyle = _checkPosition switch
        {
            VisualOrientation.Right => _orientation switch
            {
                VisualOrientation.Bottom => RightToLeft == RightToLeft.Yes ? ViewDockStyle.Right : ViewDockStyle.Left,
                VisualOrientation.Left => ViewDockStyle.Top,
                VisualOrientation.Right => ViewDockStyle.Bottom,
                _ => RightToLeft == RightToLeft.Yes ? ViewDockStyle.Left : ViewDockStyle.Right
            },
            VisualOrientation.Top => _orientation switch
            {
                VisualOrientation.Bottom => ViewDockStyle.Bottom,
                VisualOrientation.Left => ViewDockStyle.Left,
                VisualOrientation.Right => ViewDockStyle.Right,
                _ => ViewDockStyle.Top
            },
            VisualOrientation.Bottom => _orientation switch
            {
                VisualOrientation.Bottom => ViewDockStyle.Top,
                VisualOrientation.Left => ViewDockStyle.Right,
                VisualOrientation.Right => ViewDockStyle.Left,
                _ => ViewDockStyle.Bottom
            },
            _ => _orientation switch
            {
                VisualOrientation.Bottom => RightToLeft == RightToLeft.Yes ? ViewDockStyle.Left : ViewDockStyle.Right,
                VisualOrientation.Left => ViewDockStyle.Bottom,
                VisualOrientation.Right => ViewDockStyle.Top,
                _ => RightToLeft == RightToLeft.Yes ? ViewDockStyle.Right : ViewDockStyle.Left
            }
        };

        ViewDockStyle separatorDock = dockStyle;
        _layoutDocker.SetDock(_layoutCheckBoxGlyph, dockStyle);
        _layoutDocker.SetDock(_textGapSeparator, separatorDock);
        ApplyTextGap();
    }

    #endregion
}
