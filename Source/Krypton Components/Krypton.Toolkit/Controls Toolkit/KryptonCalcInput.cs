#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a numeric input control with a calculator dropdown.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCalcInput), "ToolboxBitmaps.KryptonCalcInput.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Value))]
[DefaultBindingProperty(nameof(Value))]
[Designer(typeof(KryptonCalcInputDesigner))]
[DesignerCategory(@"code")]
[Description(@"Represents a numeric input control with integrated calculator dropdown.")]
public class KryptonCalcInput : VisualControlBase, IContainedInputControl
{
    #region Instance Fields
    private VisualPopupToolTip? _visualPopupToolTip;
    private readonly ButtonSpecManagerLayout? _buttonManager;
    private readonly ViewLayoutDocker _drawDockerInner;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly KryptonTextBox _textBox;
    private readonly ViewDrawDropDownButton _dropDownGlyph;
    private readonly ButtonController _buttonController;
    private InputControlStyle _inputControlStyle;
    private decimal _value;
    private bool? _fixedActive;
    private bool _alwaysActive;
    private bool _mouseOver;
    private bool _trackingMouseEnter;
    private bool _allowDecimals;
    private int _decimalPlaces;
    private bool _trailingZeroes;
    private bool _autoSize;
    private bool _forcedLayout;
    private bool _thousandsSeparator;
    private Padding _contentPadding;
    private int _dropDownWidth;
    private VisualOrientation _popupSide;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Value property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Value property changes.")]
    [Category(@"Action")]
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Occurs when the value of the TextChanged property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? TextChanged;

    /// <summary>
    /// Occurs when the mouse enters the control.
    /// </summary>
    [Description(@"Raises the TrackMouseEnter event in the wrapped control.")]
    [Category(@"Mouse")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? TrackMouseEnter;

    /// <summary>
    /// Occurs when the mouse leaves the control.
    /// </summary>
    [Description(@"Raises the TrackMouseLeave event in the wrapped control.")]
    [Category(@"Mouse")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? TrackMouseLeave;

    /// <summary>
    /// Occurs when the value of the BackColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackColorChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImage property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImageLayout property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the value of the ForeColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? ForeColorChanged;

    /// <summary>
    /// Occurs when the value of the PaddingChanged property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? PaddingChanged;

    /// <summary>
    /// Occurs when any ButtonSpec in <see cref="ButtonSpecs"/> is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Raised when any ButtonSpec is clicked.")]
    public event EventHandler<ButtonSpecEventArgs>? ButtonSpecClicked;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCalcInput class.
    /// </summary>
    public KryptonCalcInput()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // By default we are not multiline and so the height is fixed
        SetStyle(ControlStyles.FixedHeight, true);

        // Cannot select this control, only the child TextBox
        SetStyle(ControlStyles.Selectable, false);

        // Defaults
        _inputControlStyle = InputControlStyle.Standalone;
        _alwaysActive = true;
        _decimalPlaces = 0;
        _allowDecimals = false;
        _trailingZeroes = true;
        _autoSize = false;
        _contentPadding = Padding.Empty;
        _dropDownWidth = 0;
        _popupSide = VisualOrientation.Bottom;

        // Create storage properties
        ButtonSpecs = new CalcButtonSpecCollection(this);

        // Monitor ButtonSpec additions/removals so we can surface click events
        ButtonSpecs.Inserted += OnButtonSpecInserted;
        ButtonSpecs.Removed += OnButtonSpecRemoved;

        // Create the palette storage
        StateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateActive = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);

        // Create the internal textbox used for containing content
        _textBox = new KryptonTextBox();
        _textBox.StateCommon.Border.Draw = InheritBool.False;
        _textBox.StateCommon.Border.Width = 0;
        _textBox.TextChanged += OnTextBoxTextChanged;
        _textBox.GotFocus += OnTextBoxGotFocus;
        _textBox.LostFocus += OnTextBoxLostFocus;
        _textBox.KeyDown += OnTextBoxKeyDown;
        _textBox.KeyUp += OnTextBoxKeyUp;
        _textBox.KeyPress += OnTextBoxKeyPress;
        _textBox.PreviewKeyDown += OnTextBoxPreviewKeyDown;
        _textBox.Validating += OnTextBoxValidating;
        _textBox.Validated += OnTextBoxValidated;
        _textBox.MouseEnter += OnTextBoxMouseEnter;
        _textBox.MouseLeave += OnTextBoxMouseLeave;

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_textBox)
        {
            DisplayPadding = new Padding(1, 1, 1, 0)
        };

        // Create inner view for placing inside the drawing docker
        _drawDockerInner = new ViewLayoutDocker
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

        // Add dropdown glyph holder (inserted after construction below)

        // Create view for the control border and background
        _drawDockerOuter = new ViewDrawDocker(StateNormal.Back, StateNormal.Border)
        {
            { _drawDockerInner, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDockerOuter);

        // Create button specification collection manager
        _buttonManager = new ButtonSpecManagerLayout(this, Redirector, ButtonSpecs, null,
            [_drawDockerInner],
            [StateCommon],
            [PaletteMetricInt.HeaderButtonEdgeInsetInputControl],
            [PaletteMetricPadding.HeaderButtonPaddingInputControl],
            CreateToolStripRenderer,
            NeedPaintDelegate);

        // Create the manager for handling tooltips
        ToolTipManager = new ToolTipManager(ToolTipValues);
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;
        _buttonManager.ToolTipManager = ToolTipManager;

        // Create the dropdown glyph view (renderer draws an arrow or custom glyph)
        _dropDownGlyph = new ViewDrawDropDownButton(StateCommon.Content)
        {
            Orientation = VisualOrientation.Top
        };

        // Set calculator icon for dropdown glyph
        try
        {
            _dropDownGlyph.CustomGlyph = ResourceFiles.Generic.GenericKryptonImageResources.KryptonCalcInput;
        }
        catch
        {
        }

        // Constrain glyph size using a fixed-size decorator and center it
        var glyphFixed = new ViewDecoratorFixedSize(_dropDownGlyph, new Size(20, 20));
        var glyphCenter = new ViewLayoutCenter(1);
        glyphCenter.Add(glyphFixed);

        // Add the glyph to the right side of the inner docker
        _drawDockerInner.Add(glyphCenter, ViewDockStyle.Right);

        // Create dropdown controller
        _buttonController = new ButtonController(glyphCenter, NeedPaintDelegate)
        {
            BecomesFixed = true,
            ClickOnDown = false
        };

        // Assign the controller to the view element
        glyphCenter.MouseController = _buttonController;
        glyphCenter.KeyController = _buttonController;
        glyphCenter.SourceController = _buttonController;

        // Handle dropdown button click
        _buttonController.Click += OnDropDownClick;

        // Add textbox to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_textBox);

        // Defaults for calculator value
        _value = 0m;
        UpdateTextBoxValue();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Remove any showing tooltip
            OnCancelToolTip(this, EventArgs.Empty);

            // Tell the buttons class to cleanup resources
            _buttonManager?.Destruct();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Type Definitions
    /// <summary>
    /// Collection for managing ButtonSpecAny instances.
    /// </summary>
    public class CalcButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the CalcButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public CalcButtonSpecCollection(KryptonCalcInput owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the automatic resize of the control to fit contents.
    /// </summary>
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Autosizes the control based on the maximum value possible.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool AutoSize
    {
        get => _autoSize;
        set
        {
            if (_autoSize != value)
            {
                _autoSize = value;
                UpdateAutoSizing();
            }
        }
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
    /// Gets or sets extra padding inside the text area. When empty, palette padding is used.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Extra padding inside the text area. When empty, palette padding is used.")]
    [DefaultValue(typeof(Padding), "0,0,0,0")]
    public Padding ContentPadding
    {
        get => _contentPadding;
        set
        {
            if (_contentPadding != value)
            {
                _contentPadding = value;
                if (_contentPadding != Padding.Empty)
                {
                    _layoutFill.DisplayPadding = _contentPadding;
                }
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(false)]
    [AllowNull]
    public override string Text
    {
        get => _textBox.Text;
        set => _textBox.Text = value;
    }

    /// <summary>
    /// Gets or sets horizontal alignment of the text in the edit area.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Horizontal alignment of the text in the edit area.")]
    [DefaultValue(HorizontalAlignment.Left)]
    public HorizontalAlignment TextAlign
    {
        get => _textBox.TextAlign;
        set
        {
            if (_textBox.TextAlign != value)
            {
                _textBox.TextAlign = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        get => base.ContextMenuStrip;
        set
        {
            base.ContextMenuStrip = value;
            _textBox.ContextMenuStrip = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of decimal places to display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the number of decimal places to display.")]
    [DefaultValue(0)]
    [Browsable(true)]
    public int DecimalPlaces
    {
        get => _decimalPlaces;
        set
        {
            if (_decimalPlaces != value)
            {
                _decimalPlaces = value;
                UpdateTextBoxValue();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the control accepts decimal values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control can accept decimal values, rather than integer values only.")]
    [DefaultValue(false)]
    public bool AllowDecimals
    {
        get => _allowDecimals;
        set
        {
            if (_allowDecimals != value)
            {
                _allowDecimals = value;
                UpdateTextBoxValue();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the control displays trailing zeroes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control will display trailing zeroes, when decimals are in play")]
    [DefaultValue(true)]
    public bool TrailingZeroes
    {
        get => _trailingZeroes;
        set
        {
            if (_trailingZeroes != value)
            {
                _trailingZeroes = value;
                UpdateTextBoxValue();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the thousands separator will be inserted between each three decimal digits.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates whether the thousands separator will be inserted between each three decimal digits.")]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool ThousandsSeparator
    {
        get => _thousandsSeparator;
        set
        {
            if (_thousandsSeparator != value)
            {
                _thousandsSeparator = value;
                UpdateTextBoxValue();
            }
        }
    }

    /// <summary>
    /// Gets or sets the default width of the calculator dropdown. At runtime a sensible minimum is enforced.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Default width of the calculator popup. A sensible minimum is enforced at runtime.")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int DropDownWidth
    {
        get => _dropDownWidth;
        set
        {
            if (_dropDownWidth != value)
            {
                _dropDownWidth = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the side of the control where the calculator popup appears.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Side of the control where the calculator popup appears (Top/Bottom/Left/Right).")]
    [DefaultValue(VisualOrientation.Bottom)]
    public VisualOrientation PopupSide
    {
        get => _popupSide;
        set
        {
            if (_popupSide != value)
            {
                _popupSide = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the current value of the numeric up-down control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The current value of the numeric up-down control.")]
    [DefaultValue(0.0d)]
    [Bindable(true)]
    public decimal Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                UpdateTextBoxValue();
                OnValueChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if the control is always active or only when the mouse is over the control or has focus.")]
    [DefaultValue(true)]
    public bool AlwaysActive
    {
        get => _alwaysActive;
        set
        {
            if (_alwaysActive != value)
            {
                _alwaysActive = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Input control style.")]
    public InputControlStyle InputControlStyle
    {
        get => _inputControlStyle;
        set
        {
            if (_inputControlStyle != value)
            {
                _inputControlStyle = value;
                StateCommon.SetStyles(value);
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetInputControlStyle() => InputControlStyle = InputControlStyle.Standalone;

    private bool ShouldSerializeInputControlStyle() => InputControlStyle != InputControlStyle.Standalone;

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips { get; set; }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority { get; set; }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CalcButtonSpecCollection ButtonSpecs { get; }

    /// <summary>
    /// Gets access to the common textbox appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common textbox appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled textbox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled textbox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal textbox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal textbox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active textbox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active textbox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateActive { get; }

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => _textBox;

    /// <summary>
    /// Gets a value indicating whether the control has input focus.
    /// </summary>
    [Browsable(false)]
    public override bool Focused => _textBox.Focused;

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => _textBox?.Select(start, length);

    /// <summary>
    /// Sets the fixed state of the control.
    /// </summary>
    /// <param name="active">Should the control be fixed as active.</param>
    public void SetFixedState(bool active) => _fixedActive = active;

    /// <summary>
    /// Gets access to the ToolTipManager used for displaying tool tips.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolTipManager ToolTipManager { get; }

    /// <summary>
    /// Gets a value indicating if the input control is active.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsActive =>
        _fixedActive ?? DesignMode || AlwaysActive ||
        ContainsFocus || _mouseOver;

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => _textBox != null && _textBox.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => _textBox?.Select();

    /// <summary>
    /// Get the preferred size of the control based on a proposed size.
    /// </summary>
    /// <param name="proposedSize">Starting size proposed by the caller.</param>
    /// <returns>Calculated preferred size.</returns>
    public override Size GetPreferredSize(Size proposedSize)
    {
        // Do we have a manager to ask for a preferred size?
        if (ViewManager != null)
        {
            // Ask the view to perform a layout
            Size retSize = ViewManager.GetPreferredSize(Renderer, proposedSize);

            // Apply the maximum sizing
            if (MaximumSize.Width > 0)
            {
                retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
            }

            if (MaximumSize.Height > 0)
            {
                retSize.Height = Math.Min(MaximumSize.Height, retSize.Height);
            }

            // Apply the minimum sizing
            if (MinimumSize.Width > 0)
            {
                retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
            }

            if (MinimumSize.Height > 0)
            {
                retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);
            }

            return retSize;
        }

        // Fall back on default control processing
        return base.GetPreferredSize(proposedSize);
    }

    /// <summary>
    /// Gets or sets a value indicating whether an ampersand is included in the text of the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true the first character after an ampersand will be used as a mnemonic.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _buttonManager!.UseMnemonic;

        set
        {
            if (_buttonManager!.UseMnemonic != value)
            {
                _buttonManager.UseMnemonic = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets if the control is in the ribbon design mode.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool InRibbonDesignMode { get; set; }
    #endregion

    #region Protected
    /// <summary>
    /// Force the layout logic to size and position the controls.
    /// </summary>
    protected void ForceControlLayout()
    {
        if (!IsHandleCreated)
        {
            _forcedLayout = true;
            OnLayout(new LayoutEventArgs(null, null));
            _forcedLayout = false;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a value has been entered by the user.
    /// </summary>
    protected bool UserEdit { get; set; }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the ValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTrackMouseEnter(EventArgs e) => TrackMouseEnter?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTrackMouseLeave(EventArgs e) => TrackMouseLeave?.Invoke(this, e);
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates a new instance of the control collection for the KryptonTextBox.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <summary>
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // Let base class do standard stuff
        base.OnHandleCreated(e);

        // Ensure the child textbox reflects any explicitly set Font immediately
        _textBox.Font = Font;
        PerformNeedPaint(false);

        // We need a layout to occur before any painting
        InvokeLayout();

        // We need to recalculate the correct height
        Height = PreferredHeight;
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Change in enabled state requires a layout and repaint
        UpdateStateAndPalettes();

        // Update view elements
        _drawDockerInner.Enabled = Enabled;
        _drawDockerOuter.Enabled = Enabled;

        // Update state to reflect change in enabled state
        _buttonManager?.RefreshButtons();

        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        InvalidateChildren();
        // If no explicit font set, update textbox font from palette
        var fontProp = System.ComponentModel.TypeDescriptor.GetProperties(this)[nameof(Font)];
        bool explicitFont = fontProp?.ShouldSerializeValue(this) == true;
        if (!explicitFont)
        {
            IPaletteTriple triple = GetTripleState();
            Font? font = triple.PaletteContent!.GetContentShortTextFont(_drawDockerOuter.State);
            if ((_textBox.Handle != IntPtr.Zero) && font != null && !_textBox.Font.Equals(font))
            {
                _textBox.Font = font;
            }
        }
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Processes a notification from palette of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnPaletteNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        InvalidateChildren();
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Raises the BackColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackColorChanged(EventArgs e) => BackColorChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageChanged(EventArgs e) => BackgroundImageChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageLayoutChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageLayoutChanged(EventArgs e) => BackgroundImageLayoutChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ForeColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnForeColorChanged(EventArgs e) => ForeColorChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the Resize event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnResize(EventArgs e)
    {
        // Let base class raise events
        base.OnResize(e);

        // We must have a layout calculation
        ForceControlLayout();
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        if (!IsDisposed && !Disposing)
        {
            // Update to match the new palette settings
            Height = PreferredHeight;

            // Apply current content padding visually by adjusting our fill padding
            if (_contentPadding != Padding.Empty)
            {
                _layoutFill.DisplayPadding = _contentPadding;
            }

            // Let base class calculate fill rectangle
            base.OnLayout(levent);

            // Only use layout logic if control is fully initialized or if being forced
            // to allow a relayout or if in design mode.
            if (IsHandleCreated || _forcedLayout || (DesignMode && (_textBox != null)))
            {
                Rectangle fillRect = _layoutFill.FillRect;
                _textBox?.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);

                // In the designer, ensure the control Width reflects the full visual width
                // (not just the inner textbox width), so the property grid reports the true size.
                if (DesignMode && ViewManager != null)
                {
                    Size pref = ViewManager.GetPreferredSize(Renderer, new Size(int.MaxValue, int.MaxValue));
                    if (Width < pref.Width)
                    {
                        Width = pref.Width;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Raises the MouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
        _mouseOver = true;
        PerformNeedPaint(true);
        InvalidateChildren();
        base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        _mouseOver = false;
        PerformNeedPaint(true);
        InvalidateChildren();
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        _textBox?.Focus();
    }

    /// <summary>
    /// Performs the work of setting the specified bounds of this control.
    /// </summary>
    /// <param name="x">The new Left property value of the control.</param>
    /// <param name="y">The new Top property value of the control.</param>
    /// <param name="width">The new Width property value of the control.</param>
    /// <param name="height">The new Height property value of the control.</param>
    /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
    protected override void SetBoundsCore(int x, int y,
        int width, int height,
        BoundsSpecified specified)
    {
        // Get the preferred size of the entire control
        Size preferredSize = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));

        // If setting the actual height
        if (specified.HasFlag(BoundsSpecified.Height))
        {
            // Override the actual height used
            height = preferredSize.Height;
        }

        base.SetBoundsCore(x, y, width, height, specified);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(120, PreferredHeight);

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated && !e.NeedLayout)
        {
            InvalidateChildren();
        }
        else
        {
            ForceControlLayout();
        }

        if (!IsDisposed && !Disposing)
        {
            // Update the back/fore/font from the palette settings
            UpdateStateAndPalettes();
            IPaletteTriple triple = GetTripleState();
            _textBox.BackColor = triple.PaletteBack.GetBackColor1(_drawDockerOuter.State);
            _textBox.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(_drawDockerOuter.State);

            // Apply font: honor an explicit Font override set on this control; otherwise use palette font
            var fontProp = System.ComponentModel.TypeDescriptor.GetProperties(this)[nameof(Font)];
            bool explicitFont = fontProp?.ShouldSerializeValue(this) == true;
            Font? desiredFont = explicitFont
                ? Font
                : triple.PaletteContent.GetContentShortTextFont(_drawDockerOuter.State);
            if ((_textBox.Handle != IntPtr.Zero) && desiredFont != null && !_textBox.Font.Equals(desiredFont))
            {
                _textBox.Font = desiredFont;
            }

            // Alignment mapping from palette
            var hAlign = triple.PaletteContent.GetContentShortTextH(_drawDockerOuter.State);
            _textBox.TextAlign = hAlign switch
            {
                PaletteRelativeAlign.Center => HorizontalAlignment.Center,
                PaletteRelativeAlign.Far => HorizontalAlignment.Right,
                _ => HorizontalAlignment.Left
            };
        }

        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the PaddingChanged event.
    /// </summary>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnPaddingChanged(EventArgs e) => PaddingChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        _textBox.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        _textBox.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.NCHITTEST:
                // Treat clicks as client area to avoid click-through on transparent pixels
                base.WndProc(ref m);
                m.Result = (IntPtr)PI.HT.CLIENT;
                return;
            default:
                base.WndProc(ref m);
                return;
        }
    }
    #endregion

    #region Internal
    internal bool InTransparentDesignMode => InRibbonDesignMode;

    internal bool IsFixedActive => _fixedActive != null;
    #endregion

    #region Implementation
    private void UpdateAutoSizing()
    {
        if (AutoSize)
        {
            var graphics = Graphics.FromHwnd(Handle);
            var newWidth = (int)Math.Ceiling(graphics.MeasureString(Value.ToString(), Font).Width);
            newWidth += 60; // Add space for dropdown button and padding
            newWidth = Math.Max(newWidth, MinimumSize.Width);
            newWidth = Math.Min(newWidth, MaximumSize.Width);

            if (newWidth > 0)
            {
                Width = newWidth;
                PerformNeedPaint(true);
            }
        }
    }

    private void InvalidateChildren()
    {
        if (_textBox != null)
        {
            _textBox.Invalidate();

            if (!IsDisposed && !Disposing)
            {
                PI.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, 0x85);
            }
        }
    }

    private void UpdateStateAndPalettes()
    {
        // Get the correct palette settings to use
        IPaletteTriple tripleState = GetTripleState();
        _drawDockerOuter.SetPalettes(tripleState.PaletteBack, tripleState.PaletteBorder!);

        // Update enabled state
        _drawDockerOuter.Enabled = Enabled;

        // Find the new state of the main view element
        PaletteState state = Enabled ? (IsActive ? PaletteState.Tracking : PaletteState.Normal) : PaletteState.Disabled;

        _drawDockerOuter.ElementState = state;
    }

    internal PaletteInputControlTripleStates GetTripleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

    private int PreferredHeight
    {
        get
        {
            // Get the preferred size of the entire control
            Size preferredSize = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));

            // We only need the height
            return preferredSize.Height;
        }
    }

    private void UpdateTextBoxValue()
    {
        if (_textBox != null)
        {
            var numberFormat = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.CurrentCulture.NumberFormat.Clone();

            if (ThousandsSeparator)
            {
                // Respect current culture separators and digits
                numberFormat.NumberDecimalDigits = _decimalPlaces;
            }

            string formatString = AllowDecimals
                ? $"F{_decimalPlaces.ToString(System.Globalization.CultureInfo.InvariantCulture)}"
                : "F0";

            if (TrailingZeroes || !AllowDecimals)
            {
                // Simple culture-aware format when zero trimming is not requested
                _textBox.Text = _value.ToString(formatString, System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                // Mirror KryptonNumericUpDown: compute a culture formatted string with requested places,
                // and an invariant trimmed string; remove the delta to keep grouping while trimming zeros.
                var culture = System.Globalization.CultureInfo.CurrentCulture;
                string textAsRequested = _value.ToString(formatString, culture);
                string textTrimmedInvariant = _value.ToString(@"0.#########################",
                    System.Globalization.CultureInfo.InvariantCulture);
                int lengthToRemove = textAsRequested.Length - textTrimmedInvariant.Length;
                if (lengthToRemove > 0 && lengthToRemove <= textAsRequested.Length)
                {
                    _textBox.Text = textAsRequested.Substring(0, textAsRequested.Length - lengthToRemove);
                }
                else
                {
                    _textBox.Text = textAsRequested;
                }
            }
        }
    }

    private void OnTextBoxTextChanged(object? sender, EventArgs e)
    {
        if (_textBox != null && decimal.TryParse(_textBox.Text, out decimal newValue))
        {
            if (_value != newValue)
            {
                _value = newValue;
                OnValueChanged(EventArgs.Empty);
            }
        }
        OnTextChanged(e);
    }

    private void OnTextBoxGotFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        PerformNeedPaint(true);
        InvalidateChildren();
        base.OnGotFocus(e);
    }

    private void OnTextBoxLostFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        PerformNeedPaint(true);
        InvalidateChildren();
        base.OnLostFocus(e);
    }

    private void OnTextBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnTextBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnTextBoxKeyPress(object? sender, KeyPressEventArgs e)
    {
        // Handle numeric input validation
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            if (AllowDecimals && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                // Allow decimal separator only if not already present
                if (_textBox.Text.Contains('.') || _textBox.Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == '-')
            {
                // Allow negative sign only at the beginning
                if (_textBox.SelectionStart != 0 || _textBox.Text.Contains('-'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        if (!e.Handled)
        {
            OnKeyPress(e);
        }
    }

    private void OnTextBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnTextBoxValidating(object? sender, CancelEventArgs e) => OnValidating(e);

    private void OnTextBoxValidated(object? sender, EventArgs e) => OnValidated(e);

    private void OnTextBoxMouseEnter(object? sender, EventArgs e)
    {
        if (!_trackingMouseEnter)
        {
            _trackingMouseEnter = true;
            InvalidateChildren();
            OnTrackMouseEnter(EventArgs.Empty);
            OnMouseEnter(e);
        }
    }

    private void OnTextBoxMouseLeave(object? sender, EventArgs e)
    {
        if (_trackingMouseEnter)
        {
            _trackingMouseEnter = false;
            InvalidateChildren();
            OnTrackMouseLeave(EventArgs.Empty);
            OnMouseLeave(e);
        }
    }

    private void OnDropDownClick(object? sender, EventArgs e)
    {
        ShowCalculator();
    }

    private void ShowCalculator()
    {
        // Ensure owner form is active and focus is correct
        var ownerForm = FindForm();
        ownerForm?.Activate();
        Focus();
        _textBox.Focus();

        // Create a new KryptonContextMenu each time
        Rectangle dropScreenRect = RectangleToScreen(ClientRectangle);
        var kcm = new CalcContextMenu(dropScreenRect);

        // Honor palette settings
        if (PaletteMode != PaletteMode.Custom)
        {
            kcm.PaletteMode = PaletteMode;
        }
        else
        {
            kcm.LocalCustomPalette = LocalCustomPalette;
        }

        // Preview row using a disabled menu item (ensures reliable repaint on item clicks)
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        string expr = Value.ToString(culture);
        bool selectAllPrimed = true;
        var displayItem = new KryptonContextMenuItem(expr)
        {
            AutoClose = false,
            Enabled = false
        };
        displayItem.StateDisabled.ItemTextStandard.ShortText.Font = Font;
        var palette = KryptonManager.CurrentGlobalPalette;
        if (palette != null)
        {
            // Use a palette-derived text color that contrasts with context menu surfaces
            var c = palette.GetContentShortTextColor1(PaletteContentStyle.ContextMenuItemTextStandard, PaletteState.Normal);
            if (c != GlobalStaticValues.EMPTY_COLOR && !c.IsEmpty)
            {
                displayItem.StateDisabled.ItemTextStandard.ShortText.Color1 = c;
                displayItem.StateDisabled.ItemTextStandard.ShortText.Color2 = c;
            }
        }
        kcm.Items.Add(displayItem);

        // Build a 5x4 grid as a single items block with 4 columns
        var items = new KryptonContextMenuItems
        {
            ImageColumn = false
        };

        // Column definitions (top to bottom)
        string[][] columns =
        [
            ["7","4","1","0","<-"] ,
            ["8","5","2",".","(" ] ,
            ["9","6","3","C",")" ] ,
            ["/","*","-","+","=" ]
        ];

        for (int c = 0; c < columns.Length; c++)
        {
            if (c > 0)
            {
                items.Items.Add(new KryptonContextMenuSeparator { Horizontal = false });
            }

            for (int r = 0; r < columns[c].Length; r++)
            {
                string token = columns[c][r];
                var mi = new KryptonContextMenuItem(token, (_, __) =>
                {
                    switch (token)
                    {
                        case "C":
                            expr = string.Empty;
                            selectAllPrimed = false;
                            displayItem.Text = expr;
                            break;
                        case "<-":
                            if (!string.IsNullOrEmpty(expr))
                            {
                                expr = expr.Substring(0, expr.Length - 1);
                            }
                            selectAllPrimed = false;
                            displayItem.Text = expr;
                            break;
                        case "=":
                        {
                            try
                            {
                                var dt = new System.Data.DataTable();
                                string normalized = NormalizeExpressionForCompute(expr, culture);
                                object? v = dt.Compute(normalized, null);
                                if (v != null)
                                {
                                    decimal dec = Convert.ToDecimal(v, System.Globalization.CultureInfo.InvariantCulture);
                                    Value = dec;
                                }
                            }
                            catch { }
                            break;
                        }
                        default:
                        {
                            if (selectAllPrimed)
                            {
                                if (IsDigitToken(token))
                                {
                                    expr = token;
                                }
                                else if (IsOperatorToken(token))
                                {
                                    expr += token;
                                }
                                else
                                {
                                    expr = token;
                                }
                                selectAllPrimed = false;
                            }
                            else
                            {
                                expr += token;
                            }
                            displayItem.Text = expr;
                            break;
                        }
                    }
                });
                // Keep the menu open for all inputs except '=' which should compute and close
                mi.AutoClose = token == "=";
                items.Items.Add(mi);
            }
        }

        kcm.Items.Add(items);

        // Show relative to control with side preference and 1px gap like DTP
        Rectangle screenRect = RectangleToScreen(ClientRectangle);
        var posH = KryptonContextMenuPositionH.Left;
        var posV = KryptonContextMenuPositionV.Below;
        switch (_popupSide)
        {
            case VisualOrientation.Top:
                posV = KryptonContextMenuPositionV.Above;
                break;
            case VisualOrientation.Left:
                posH = KryptonContextMenuPositionH.Before;
                break;
            case VisualOrientation.Right:
                posH = KryptonContextMenuPositionH.After;
                break;
        }

        if (posV == KryptonContextMenuPositionV.Above)
        {
            screenRect.Y -= 1;
        }
        else if (posV == KryptonContextMenuPositionV.Below)
        {
            screenRect.Height += 1;
        }

        if (posH == KryptonContextMenuPositionH.Before)
        {
            screenRect.X -= 1;
        }
        else if (posH == KryptonContextMenuPositionH.After)
        {
            screenRect.Width += 1;
        }

        // Respect DropDownWidth as a minimum width
        if (_dropDownWidth > 0)
        {
            // KryptonContextMenu measures itself; we nudge via an invisible padding item if needed
            // Add a spacer heading with non-breaking spaces to reach desired width
            int target = _dropDownWidth;
            using (var g = CreateGraphics())
            {
                string pad = "\u00A0\u00A0\u00A0\u00A0";
                while (TextRenderer.MeasureText(g, displayItem.Text + pad, Font).Width < target)
                {
                    pad += "\u00A0\u00A0";
                }
                // Temporarily extend heading to influence width
                displayItem.Text = expr + pad;
            }
        }

        kcm.Closed += OnKryptonCalcContextMenuClosed;
        kcm.Show(this, screenRect, posH, posV);
    }

    private void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!IsDisposed && !Disposing)
        {
            // Do not show tooltips when the form we are in does not have focus
            Form? topForm = FindForm();
            if (topForm is { ContainsFocus: false })
            {
                return;
            }

            // Never show tooltips at design time
            if (!DesignMode)
            {
                IContentValues? sourceContent = null;
                var toolTipStyle = LabelStyle.ToolTip;

                var shadow = true;

                // Find the button spec associated with the tooltip request
                ButtonSpec? buttonSpec = _buttonManager?.ButtonSpecFromView(e.Target);

                // If the tooltip is for a button spec
                if (buttonSpec != null)
                {
                    // Are we allowed to show page related tooltips
                    if (AllowButtonSpecToolTips)
                    {
                        // Create a helper object to provide tooltip values
                        var buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

                        // Is there actually anything to show for the tooltip
                        if (buttonSpecMapping.HasContent)
                        {
                            sourceContent = buttonSpecMapping;
                            toolTipStyle = buttonSpec.ToolTipStyle;
                            shadow = buttonSpec.ToolTipShadow;
                        }
                    }
                }

                if (sourceContent != null)
                {
                    // Remove any currently showing tooltip
                    _visualPopupToolTip?.Dispose();

                    // Create the actual tooltip popup object
                    _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                        sourceContent,
                        Renderer,
                        PaletteBackStyle.ControlToolTip,
                        PaletteBorderStyle.ControlToolTip,
                        CommonHelper.ContentStyleFromLabelStyle(toolTipStyle),
                        shadow);

                    _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;
                    _visualPopupToolTip.ShowRelativeTo(e.Target, e.ControlMousePosition);
                }
            }
        }
    }

    private void OnCancelToolTip(object? sender, EventArgs e) =>
        // Remove any currently showing tooltip
        _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page anymore
        _visualPopupToolTip = null;
    }

    private void OnKryptonCalcContextMenuClosed(object? sender, EventArgs e)
    {
        if (sender is KryptonContextMenu kcm)
        {
            kcm.Closed -= OnKryptonCalcContextMenuClosed;
            _buttonController.RemoveFixed();
            kcm.Dispose();
        }
    }

    private static string NormalizeExpressionForCompute(string expr, System.Globalization.CultureInfo culture)
    {
        var nf = culture.NumberFormat;
        string decimalSep = nf.NumberDecimalSeparator;
        string groupSep = nf.NumberGroupSeparator;

        string s = expr.Replace("\u00A0", string.Empty).Replace(" ", string.Empty);

        if (!string.IsNullOrEmpty(groupSep))
        {
            s = s.Replace(groupSep, string.Empty);
        }

        if (!string.IsNullOrEmpty(decimalSep) && decimalSep != ".")
        {
            s = s.Replace(decimalSep, ".");
        }

        return s;
    }

    private static bool IsDigitToken(string t) => t.Length == 1 && (char.IsDigit(t[0]) || t[0] == '.');

    private static bool IsOperatorToken(string t)
    {
        if (t.Length != 1)
        {
            return false;
        }
        char ch = t[0];
        return ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '(' || ch == ')';
    }
    #endregion

    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    private sealed class CalcContextMenu : KryptonContextMenu
    {
        private readonly Rectangle _dropScreenRect;

        public CalcContextMenu(Rectangle dropScreenRect) => _dropScreenRect = dropScreenRect;

        protected override VisualContextMenu CreateContextMenu(KryptonContextMenu kcm,
            PaletteBase? palette,
            PaletteMode paletteMode,
            PaletteRedirect redirector,
            PaletteRedirectContextMenu redirectorImages,
            KryptonContextMenuCollection items,
            bool enabled,
            bool keyboardActivated) =>
            new VisualContextMenuDTP(kcm, palette, paletteMode, redirector, redirectorImages, items, enabled, keyboardActivated, _dropScreenRect);
    }

    #region ButtonSpec wiring
    private void OnButtonSpecInserted(object? sender, ButtonSpecEventArgs e)
    {
        if (e.ButtonSpec != null)
        {
            e.ButtonSpec.Click += OnAnyButtonSpecClick;
        }
    }

    private void OnButtonSpecRemoved(object? sender, ButtonSpecEventArgs e)
    {
        if (e.ButtonSpec != null)
        {
            e.ButtonSpec.Click -= OnAnyButtonSpecClick;
        }
    }

    private void OnAnyButtonSpecClick(object? sender, EventArgs e)
    {
        if (sender is ButtonSpec spec)
        {
            var index = ButtonSpecs.IndexOf(spec);
            OnButtonSpecClicked(new ButtonSpecEventArgs(spec, index));
        }
    }

    /// <summary>
    /// Raises the ButtonSpecClicked event.
    /// </summary>
    /// <param name="e">Event args containing the clicked spec.</param>
    protected virtual void OnButtonSpecClicked(ButtonSpecEventArgs e) => ButtonSpecClicked?.Invoke(this, e);
    #endregion
}