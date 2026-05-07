#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a TextBox with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTextBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
[DefaultEvent(nameof(TextChanged))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[Designer(typeof(KryptonTextBoxDesigner))]
[DesignerCategory(@"code")]
[Description(@"Enables the user to enter text, and provides multiline editing and password character masking.")]
public class KryptonTextBox : VisualControlBase,
    IContainedInputControl
{
    #region Classes
    private class InternalTextBox : TextBox
    {
        #region Instance Fields
        private readonly KryptonTextBox _kryptonTextBox;
        private bool _mouseOver;

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the mouse enters the InternalTextBox.
        /// </summary>
        public event EventHandler? TrackMouseEnter;

        /// <summary>
        /// Occurs when the mouse leaves the InternalTextBox.
        /// </summary>
        public event EventHandler? TrackMouseLeave;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InternalTextBox class.
        /// </summary>
        /// <param name="kryptonTextBox">Reference to owning control.</param>
        public InternalTextBox(KryptonTextBox kryptonTextBox)
        {
            _kryptonTextBox = kryptonTextBox;

            // Remove from view until size for the first time by the Krypton control
            Size = Size.Empty;

            // We provide the border manually
            BorderStyle = BorderStyle.None;
        }
        #endregion

        #region MouseOver
        /// <summary>
        /// Gets and sets if the mouse is currently over the combo box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MouseOver
        {
            get => _mouseOver;

            set
            {
                // Only interested in changes
                if (_mouseOver != value)
                {
                    _mouseOver = value;

                    // Generate appropriate change event
                    if (_mouseOver)
                    {
                        OnTrackMouseEnter(EventArgs.Empty);
                    }
                    else
                    {
                        OnTrackMouseLeave(EventArgs.Empty);
                    }
                }
            }
        }
        #endregion

        #region Protected

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.NCHITTEST:
                    if (_kryptonTextBox.InTransparentDesignMode)
                    {
                        m.Result = (IntPtr)PI.HT.TRANSPARENT;
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }

                    break;
                case PI.WM_.MOUSELEAVE:
                    // Mouse is not over the control
                    MouseOver = false;
                    _kryptonTextBox.PerformNeedPaint(true);
                    Invalidate();
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSEMOVE:
                    // Mouse is over the control
                    if (!MouseOver)
                    {
                        MouseOver = true;
                        _kryptonTextBox.PerformNeedPaint(true);
                        Invalidate();
                    }
                    base.WndProc(ref m);
                    break;
                case PI.WM_.PRINTCLIENT:
                case PI.WM_.PAINT:
                {
                    var ps = new PI.PAINTSTRUCT();

                    // Do we need to BeginPaint or just take the given HDC?
                    var hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

                    // Paint the entire area in the background color
                    using Graphics g = Graphics.FromHdc(hdc);
                    // Grab the client area of the control
                    PI.GetClientRect(Handle, out PI.RECT rect);

                    var textRectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left,
                        rect.bottom - rect.top);

                    // Create rect for the text area
                    Size borderSize = SystemInformation.BorderSize;
                    rect.left -= borderSize.Width + 1;

                    if (!string.IsNullOrWhiteSpace(_kryptonTextBox.CueHint.CueHintText)
                        && string.IsNullOrEmpty(_kryptonTextBox.Text)
                       )
                    {
                        // Go perform the drawing of the CueHint
                        using var backBrush = new SolidBrush(BackColor);
                        _kryptonTextBox.CueHint.PerformPaint(_kryptonTextBox, g, textRectangle, backBrush);
                    }
                    else
                    {
                        using (var backBrush = new SolidBrush(BackColor))
                        {
                            // Draw entire client area in the background color
                            g.FillRectangle(backBrush,
                                textRectangle);
                        }

                        // If enabled then let the combo draw the text area
                        if (_kryptonTextBox.Enabled)
                        {
                            // Let base implementation draw the actual text area
                            if (m.WParam == IntPtr.Zero)
                            {
                                m.WParam = hdc;
                                DefWndProc(ref m);
                                m.WParam = IntPtr.Zero;
                            }
                            else
                            {
                                DefWndProc(ref m);
                            }
                        }
                        else
                        {
                            // Set the correct text rendering hint for the text drawing. We only draw if the edit text is disabled so we
                            // just always grab the disable state value. Without this line the wrong hint can occur because it inherits
                            // it from the device context. Resulting in blurred text.
                            g.TextRenderingHint =
                                CommonHelper.PaletteTextHintToRenderingHint(
                                    _kryptonTextBox.StateDisabled.PaletteContent.GetContentShortTextHint(PaletteState.Disabled));

                            // Define the string formatting requirements
                            var stringFormat = new StringFormat
                            {
                                Trimming = StringTrimming.None,
                                LineAlignment = StringAlignment.Near
                            };
                            if (!_kryptonTextBox.Multiline)
                            {
                                stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
                            }

                            stringFormat.Alignment = _kryptonTextBox.TextAlign switch
                            {
                                HorizontalAlignment.Left => RightToLeft == RightToLeft.Yes
                                    ? StringAlignment.Far
                                    : StringAlignment.Near,
                                HorizontalAlignment.Right => RightToLeft == RightToLeft.Yes
                                    ? StringAlignment.Near
                                    : StringAlignment.Far,
                                HorizontalAlignment.Center => StringAlignment.Center,
                                _ => stringFormat.Alignment
                            };

                            // Use the correct prefix setting
                            stringFormat.HotkeyPrefix = HotkeyPrefix.None;

                            // Decide on the text to draw disabled
                            var drawString = Text;
                            if (PasswordChar != '\0')
                            {
                                drawString = new string(PasswordChar, Text.Length);
                            }

                            // Define the font to use for disabled painting – always query the palette first.
                            // Avoids exception - magnitudes faster than another repaint AND try/catch.
                            var disabledFont = _kryptonTextBox
                                                   .GetTripleState()
                                                   .PaletteContent?
                                                   .GetContentShortTextFont(PaletteState.Disabled)
                                               ?? Font; // Fallback: current Font if palette returns null
                            using var foreBrush = new SolidBrush(ForeColor);
                            g.DrawString(drawString, disabledFont, foreBrush,
                                textRectangle,
                                stringFormat);
                        }

                        // Remove clipping settings
                        PI.SelectClipRgn(hdc, IntPtr.Zero);
                    }

                    // Do we need to match the original BeginPaint?
                    if (m.WParam == IntPtr.Zero)
                    {
                        PI.EndPaint(Handle, ref ps);
                    }
                }
                    break;
                case PI.WM_.CONTEXTMENU:
                    // Only interested in overriding the behavior when we have a krypton context menu...
                    if (_kryptonTextBox.KryptonContextMenu != null)
                    {
                        // Extract the screen mouse position (if might not actually be provided)
                        var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                        // If keyboard activated, the menu position is centered
                        if (((int)(long)m.LParam) == -1)
                        {
                            mousePt = PointToScreen(new Point(Width / 2, Height / 2));
                        }

                        // Show the context menu
                        _kryptonTextBox.KryptonContextMenu.Show(_kryptonTextBox, mousePt);

                        // We eat the message!
                        return;
                    }
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

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
    }

    #endregion

    #region Type Definitions
    /// <summary>
    /// Collection for managing ButtonSpecAny instances.
    /// </summary>
    public class TextBoxButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the TextBoxButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public TextBoxButtonSpecCollection(KryptonTextBox owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Instance Fields

    private VisualPopupToolTip? _visualPopupToolTip;
    private readonly ButtonSpecManagerLayout? _buttonManager;
    private readonly ViewLayoutDocker _drawDockerInner;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly InternalTextBox _textBox;
    private InputControlStyle _inputControlStyle;
    private bool? _fixedActive;
    private bool _forcedLayout;
    private bool _autoSize;
    private bool _mouseOver;
    private bool _alwaysActive;
    private bool _trackingMouseEnter;
    private int _cachedHeight;
    private bool _multilineStringEditor;
    private bool _showEllipsisButton;
    //private bool _isInAlphaNumericMode;
    private readonly ButtonSpecAny _editorButton;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the AcceptsTab property changes.
    /// </summary>
    [Description(@"Occurs when the value of the AcceptsTab property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? AcceptsTabChanged;

    /// <summary>
    /// Occurs when the value of the HideSelection property changes.
    /// </summary>
    [Description(@"Occurs when the value of the HideSelection property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? HideSelectionChanged;

    /// <summary>
    /// Occurs when the value of the TextAlign property changes.
    /// </summary>
    [Description(@"Occurs when the value of the TextAlign property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? TextAlignChanged;

    /// <summary>
    /// Occurs when the value of the Modified property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Modified property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? ModifiedChanged;

    /// <summary>
    /// Occurs when the value of the Multiline property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Multiline property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? MultilineChanged;

    /// <summary>
    /// Occurs when the value of the ReadOnly property changes.
    /// </summary>
    [Description(@"Occurs when the value of the ReadOnly property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? ReadOnlyChanged;

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
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTextBox class.
    /// </summary>
    public KryptonTextBox()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // By default, we are not multiline and so the height is fixed
        SetStyle(ControlStyles.FixedHeight, true);

        // Cannot select this control, only the child TextBox, and does not generate a click event
        SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

        // Defaults
        _inputControlStyle = InputControlStyle.Standalone;
        _autoSize = true;
        _cachedHeight = -1;
        _alwaysActive = true;
        AllowButtonSpecToolTips = false;
        AllowButtonSpecToolTipPriority = false;

        // Create storage properties
        ButtonSpecs = new TextBoxButtonSpecCollection(this);

        // Create the palette storage
        StateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateActive = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        CueHint = new PaletteCueHintText(Redirector, NeedPaintDelegate);

        // Create the internal text box used for containing content
        _textBox = new InternalTextBox(this);
        _textBox.DoubleClick += OnDoubleClick;
        _textBox.MouseDoubleClick += OnMouseDoubleClick;
        _textBox.TrackMouseEnter += OnTextBoxMouseChange;
        _textBox.TrackMouseLeave += OnTextBoxMouseChange;
        _textBox.AcceptsTabChanged += OnTextBoxAcceptsTabChanged;
        _textBox.TextAlignChanged += OnTextBoxTextAlignChanged;
        _textBox.TextChanged += OnTextBoxTextChanged;
        _textBox.HideSelectionChanged += OnTextBoxHideSelectionChanged;
        _textBox.ModifiedChanged += OnTextBoxModifiedChanged;
        _textBox.MultilineChanged += OnTextBoxMultilineChanged;
        _textBox.ReadOnlyChanged += OnTextBoxReadOnlyChanged;
        _textBox.GotFocus += OnTextBoxGotFocus;
        _textBox.LostFocus += OnTextBoxLostFocus;
        _textBox.KeyDown += OnTextBoxKeyDown;
        _textBox.KeyUp += OnTextBoxKeyUp;
        _textBox.KeyPress += OnTextBoxKeyPress;
        _textBox.PreviewKeyDown += OnTextBoxPreviewKeyDown;
        _textBox.Validating += OnTextBoxValidating;
        _textBox.Validated += OnTextBoxValidated;
        _textBox.Click += OnTextBoxClick;  // SKC: make sure that the default click is also routed.

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_textBox);

        // Create inner view for placing inside the drawing docker
        _drawDockerInner = new ViewLayoutDocker
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

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

        // Create the button spec for the multiline editor button.
        _editorButton = new ButtonSpecAny
        {
            Image = GenericImageResources.SelectParentControlFlipped,
            Style = PaletteButtonStyle.ButtonSpec,
            Type = PaletteButtonSpecStyle.Generic
        };
        _editorButton.Click += OnEditorButtonClicked;

        // Add text box to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_textBox);

        //_isInAlphaNumericMode = false;

        _showEllipsisButton = false;
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

            // Remember to pull down the manager instance
            _buttonManager?.Destruct();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public

    // TODO: Return to this...
    /*
    /// <summary>Gets or sets a value indicating whether this instance is in alpha numeric mode.</summary>
    /// <value><c>true</c> if this instance is in alpha numeric mode; otherwise, <c>false</c>.</value>
    [Category(@"Data"), DefaultValue(false), Description(@"Only allow numerical input.")]
    public bool IsInAlphaNumericMode { get => _isInAlphaNumericMode; set { _isInAlphaNumericMode = value; SetIsInAlphaNumericMode(this); } }
    */

    /// <summary>
    /// Gets access to the common textbox appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Set a watermark/prompt message for the user.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteCueHintText CueHint { get; }

    private bool ShouldSerializeCueHint() => !CueHint.IsDefault;


    /// <summary>
    /// Gets and sets if the control is in the tab chain.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool TabStop
    {
        get => _textBox.TabStop;
        set => _textBox.TabStop = value;
    }

    /// <summary>
    /// Gets and sets if the control is in the ribbon design mode.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool InRibbonDesignMode { get; set; }

    /// <summary>
    /// Gets and sets if the control uses the multiline string editor widget.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the control uses the multiline string editor widget.")]
    [DefaultValue(false)]
    public bool MultilineStringEditor
    {
        get => _multilineStringEditor;
        set
        {
            if (_multilineStringEditor != value)
            {
                SetMultilineStringEditor(value);
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [show ellipsis button].</summary>
    /// <value><c>true</c> if [show ellipsis button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals")]
    [Description(@"Shows a ellipsis (...) button in the textbox.")]
    [DefaultValue(false)]
    public bool ShowEllipsisButton { get => _showEllipsisButton; set { _showEllipsisButton = value; ToggleEllipsisButtonVisibility(value); } }

    /// <summary>
    /// Gets access to the contained TextBox instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public TextBox TextBox => _textBox;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => TextBox;

    /// <summary>
    /// Gets and sets a value indicating if the control is automatically sized.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(false)]
    public override bool AutoSize
    {
        get => _autoSize;

        set
        {
            if (_autoSize != value)
            {
                _autoSize = value;

                // Multiline allows a variable height, otherwise we are fixed in height
                SetStyle(ControlStyles.FixedHeight, !Multiline && _autoSize);

                // Add adjust actual height to match new setting
                AdjustHeight(false);
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the control has input focus.
    /// </summary>
    [Browsable(false)]
    public override bool Focused => TextBox.Focused;

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
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    /// Gets and sets the text associated with the control.
    /// </summary>
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get => _textBox.Text;
        set => _textBox.Text = value;
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
    /// Gets a value indicating whether the user can undo the previous operation in a rich text box control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanUndo => _textBox.CanUndo;

    /// <summary>
    /// Gets a value indicating whether the contents have changed since last last.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Modified => _textBox.Modified;

    /// <summary>
    /// Gets and sets the selected text within the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
        get => _textBox.SelectedText;
        set => _textBox.SelectedText = value;
    }

    /// <summary>
    /// Gets and sets the selection length for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => _textBox.SelectionLength;
        set => _textBox.SelectionLength = value;
    }

    /// <summary>
    /// Gets and sets the starting point of text selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => _textBox.SelectionStart;
        set => _textBox.SelectionStart = value;
    }

    /// <summary>
    /// Gets the length of text in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TextLength => _textBox.TextLength;

    /// <summary>
    /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Defines if mnemonic characters generate click events for button specs.")]
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
    /// Gets or sets the lines of text in a multiline edit, as an array of String values.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The lines of text in a multiline edit, as an array of String values.")]
    [Editor(@"System.Windows.Forms.Design.StringArrayEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [MergableProperty(false)]
    [Localizable(true)]
    public string[] Lines
    {
        get => _textBox.Lines;
        set => _textBox.Lines = value;
    }

    /// <summary>
    /// Gets or sets, for multiline edit controls, which scroll bars will be shown for this control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates, for multiline edit controls, which scroll bars will be shown for this control.")]
    [DefaultValue(ScrollBars.None)]
    [Localizable(true)]
    public ScrollBars ScrollBars
    {
        get => _textBox.ScrollBars;
        set => _textBox.ScrollBars = value;
    }

    /// <summary>
    /// Gets or sets how the text should be aligned for edit controls.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates how the text should be aligned for edit controls.")]
    [DefaultValue(HorizontalAlignment.Left)]
    [Localizable(true)]
    public HorizontalAlignment TextAlign
    {
        get => _textBox.TextAlign;
        set => _textBox.TextAlign = value;
    }

    /// <summary>
    /// Indicates if lines are automatically word-wrapped for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if lines are automatically word-wrapped for multiline edit controls.")]
    [DefaultValue(true)]
    [Localizable(true)]
    public bool WordWrap
    {
        get => _textBox.WordWrap;
        set => _textBox.WordWrap = value;
    }

    /// <summary>
    /// Gets and sets whether the text in the control can span more than one line.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Control whether the text in the control can span more than one line.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool Multiline
    {
        get => _textBox.Multiline;

        set
        {
            if (_textBox.Multiline != value)
            {
                _textBox.Multiline = value;

                // Multiline allows a variable height, otherwise we are fixed in height
                SetStyle(ControlStyles.FixedHeight, !value && _autoSize);

                // Add adjust actual height to match new setting
                AdjustHeight(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if return characters are accepted as input for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if return characters are accepted as input for multiline edit controls.")]
    [DefaultValue(false)]
    public bool AcceptsReturn
    {
        get => _textBox.AcceptsReturn;
        set => _textBox.AcceptsReturn = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if tab characters are accepted as input for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if tab characters are accepted as input for multiline edit controls.")]
    [DefaultValue(false)]
    public bool AcceptsTab
    {
        get => _textBox.AcceptsTab;
        set => _textBox.AcceptsTab = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if all the characters should be left alone or converted to uppercase or lowercase.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if all the characters should be left alone or converted to uppercase or lowercase.")]
    [DefaultValue(CharacterCasing.Normal)]
    public CharacterCasing CharacterCasing
    {
        get => _textBox.CharacterCasing;
        set => _textBox.CharacterCasing = value;
    }

    /// <summary>
    /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates that the selection should be hidden when the edit control loses focus.")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => _textBox.HideSelection;
        set => _textBox.HideSelection = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the edit control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies the maximum number of characters that can be entered into the edit control.")]
    [DefaultValue(32767)]
    [Localizable(true)]
    public int MaxLength
    {
        get => _textBox.MaxLength;
        set => _textBox.MaxLength = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text in the edit control can be changed or not.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Controls whether the text in the edit control can be changed or not.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get => _textBox.ReadOnly;
        set => _textBox.ReadOnly = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether shortcuts defined for the control are enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether shortcuts defined for the control are enabled.")]
    [DefaultValue(true)]
    public bool ShortcutsEnabled
    {
        get => _textBox.ShortcutsEnabled;
        set => _textBox.ShortcutsEnabled = value;
    }

    /// <summary>
    /// Gets or sets the character to display for password input for single-line edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates the character to display for password input for single-line edit controls.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue('\0')]
    [Localizable(true)]
    public char PasswordChar
    {
        get => _textBox.PasswordChar;
        set => _textBox.PasswordChar = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the text in the edit control should appear as the default password character.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the text in the edit control should appear as the default password character.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
        get => _textBox.UseSystemPasswordChar;
        set => _textBox.UseSystemPasswordChar = value;
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
    /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
    /// </summary>
    [Description(@"The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Localizable(true)]
    [Browsable(true)]
    public AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get => _textBox.AutoCompleteCustomSource;
        set => _textBox.AutoCompleteCustomSource = value;
    }

    /// <summary>
    /// Gets or sets the text completion behavior of the textbox.
    /// </summary>
    [Description(@"Indicates the text completion behavior of the textbox.")]
    [DefaultValue(AutoCompleteMode.None)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteMode AutoCompleteMode
    {
        get => _textBox.AutoCompleteMode;
        set => _textBox.AutoCompleteMode = value;
    }

    /// <summary>
    /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
    /// </summary>
    [Description(@"The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
    [DefaultValue(AutoCompleteSource.None)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteSource AutoCompleteSource
    {
        get => _textBox.AutoCompleteSource;
        set => _textBox.AutoCompleteSource = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
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
    public TextBoxButtonSpecCollection ButtonSpecs { get; }

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
    /// Appends text to the current text of a rich text box.
    /// </summary>
    /// <param name="text">The text to append to the current contents of the text box.</param>
    public void AppendText(string text) => _textBox.AppendText(text);

    /// <summary>
    /// Clears all text from the text box control.
    /// </summary>
    public void Clear() => _textBox.Clear();

    /// <summary>
    /// Clears information about the most recent operation from the undo buffer of the rich text box.
    /// </summary>
    public void ClearUndo() => _textBox.ClearUndo();

    /// <summary>
    /// Copies the current selection in the text box to the Clipboard.
    /// </summary>
    public void Copy() => _textBox.Copy();

    /// <summary>
    /// Moves the current selection in the text box to the Clipboard.
    /// </summary>
    public void Cut() => _textBox.Cut();

    /// <summary>
    /// Replaces the current selection in the text box with the contents of the Clipboard.
    /// </summary>
    public void Paste() => _textBox.Paste();

    /// <summary>
    /// Scrolls the contents of the control to the current caret position.
    /// </summary>
    public void ScrollToCaret() => _textBox.ScrollToCaret();

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => _textBox.Select(start, length);

    /// <summary>
    /// Selects all text in the control.
    /// </summary>
    public void SelectAll() => _textBox.SelectAll();

    /// <summary>
    /// Undoes the last edit operation in the text box.
    /// </summary>
    public void Undo() => _textBox.Undo();

    /// <summary>
    /// Specifies that the value of the SelectionLength property is zero so that no characters are selected in the control.
    /// </summary>
    public void DeselectAll() => _textBox.DeselectAll();

    /// <summary>
    /// Retrieves the character that is closest to the specified location within the control.
    /// </summary>
    /// <param name="pt">The location from which to seek the nearest character.</param>
    /// <returns>The character at the specified location.</returns>
    public int GetCharFromPosition(Point pt) => _textBox.GetCharFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the character nearest to the specified location.
    /// </summary>
    /// <param name="pt">The location to search.</param>
    /// <returns>The zero-based character index at the specified location.</returns>
    public int GetCharIndexFromPosition(Point pt) => _textBox.GetCharIndexFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the first character of a given line.
    /// </summary>
    /// <param name="lineNumber">The line for which to get the index of its first character.</param>
    /// <returns>The zero-based character index in the specified line.</returns>
    public int GetFirstCharIndexFromLine(int lineNumber) => _textBox.GetFirstCharIndexFromLine(lineNumber);

    /// <summary>
    /// Retrieves the index of the first character of the current line.
    /// </summary>
    /// <returns>The zero-based character index in the current line.</returns>
    public int GetFirstCharIndexOfCurrentLine() => _textBox.GetFirstCharIndexOfCurrentLine();

    /// <summary>
    /// Retrieves the line number from the specified character position within the text of the RichTextBox control.
    /// </summary>
    /// <param name="index">The character index position to search.</param>
    /// <returns>The zero-based line number in which the character index is located.</returns>
    public int GetLineFromCharIndex(int index) => _textBox.GetLineFromCharIndex(index);

    /// <summary>
    /// Retrieves the location within the control at the specified character index.
    /// </summary>
    /// <param name="index">The index of the character for which to retrieve the location.</param>
    /// <returns>The location of the specified character.</returns>
    public Point GetPositionFromCharIndex(int index) => _textBox.GetPositionFromCharIndex(index);

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
    public bool IsActive => (_fixedActive ?? DesignMode || AlwaysActive || ContainsFocus || _mouseOver || _textBox.MouseOver);

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => TextBox.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => TextBox.Select();

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
        else
        {
            // Fall back on default control processing
            return base.GetPreferredSize(proposedSize);
        }
    }

    /// <summary>
    /// Gets the rectangle that represents the display area of the control.
    /// </summary>
    public override Rectangle DisplayRectangle
    {
        get
        {
            // Ensure that the layout is calculated in order to know the remaining display space
            ForceViewLayout();

            // The inside text box is the client rectangle size
            return new Rectangle(_textBox.Location, _textBox.Size);
        }
    }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool DesignerGetHitTest(Point pt)
    {
        // Ignore call as view builder is already destructed
        if (IsDisposed)
        {
            return false;
        }

        // Check if any of the button specs want the point
        return (_buttonManager != null) && _buttonManager.DesignerGetHitTest(pt);
    }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public Component? DesignerComponentFromPoint(Point pt) =>
        // Ignore call as view builder is already destructed
        IsDisposed ? null : ViewManager?.ComponentFromPoint(pt);

    // Ask the current view for a decision
    /// <summary>
    /// Internal design time method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public void DesignerMouseLeave() =>
        // Simulate the mouse leaving the control so that the tracking
        // element that thinks it has the focus is informed it does not
        OnMouseLeave(EventArgs.Empty);

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
    /// Sets up the multiline string editor for the control.
    /// </summary>
    /// <param name="value">
    /// true to enable the multiline string editor; otherwise false.
    /// </param>
    protected void SetMultilineStringEditor(bool value)
    {
        _multilineStringEditor = value;
        // FIXME: This should probably rather be drawn as a glyph or something and not be
        // added to the ButtonSpecs that can be modified by the user, but I lack the
        // familiarity with the Krypton Framework and the time to figure out how to implement
        // this the proper way.
        if (value == false)
        {
            ButtonSpecs.Remove(_editorButton);
        }
        else
        {
            if (!ButtonSpecs.Contains(_editorButton))
            {
                ButtonSpecs.Add(_editorButton);
            }
        }
    }
    #endregion

    #region Protected Virtual
    // ReSharper disable VirtualMemberNeverOverridden.Global
    /// <summary>
    /// Raises the AcceptsTabChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnAcceptsTabChanged(EventArgs e) => AcceptsTabChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TextAlignChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTextAlignChanged(EventArgs e) => TextAlignChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the HideSelectionChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnHideSelectionChanged(EventArgs e) => HideSelectionChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ModifiedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnModifiedChanged(EventArgs e) => ModifiedChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the MultilineChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnMultilineChanged(EventArgs e) => MultilineChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ReadOnlyChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnReadOnlyChanged(EventArgs e) => ReadOnlyChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    [Description(@"Raises the TrackMouseEnter event.")]
    protected virtual void OnTrackMouseEnter(EventArgs e) => TrackMouseEnter?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    [Description(@"Raises the TrackMouseLeave event.")]
    protected virtual void OnTrackMouseLeave(EventArgs e) => TrackMouseLeave?.Invoke(this, e);
    // ReSharper restore VirtualMemberNeverOverridden.Global
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

        // Force the font to be set into the text box child control
        PerformNeedPaint(false);

        // We need a layout to occur before any painting
        InvokeLayout();

        // We need to recalculate the correct height
        AdjustHeight(true);
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
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        _textBox.Focus();
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
            // Update with latest content padding for placing around the contained text box control
            var paletteContent = GetTripleState().PaletteContent;
            if (paletteContent != null)
            {
                Padding contentPadding = paletteContent.GetBorderContentPadding(null, _drawDockerOuter.State);
                _layoutFill.DisplayPadding = contentPadding;
            }
        }

        // Ensure the height is correct
        AdjustHeight(false);

        // Let base class calculate fill rectangle
        base.OnLayout(levent);

        // Only use layout logic if control is fully initialized or if being forced
        // to allow a relayout or if in design mode.
        if (IsHandleCreated || _forcedLayout || (DesignMode && (_textBox != null)))
        {
            Rectangle fillRect = _layoutFill.FillRect;
            //  for centering the inner text field vertically
            var y = Height / 2 - _textBox.Height / 2;

            _textBox.SetBounds(fillRect.X, y, fillRect.Width, fillRect.Height);
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
        _textBox.Invalidate();
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
        _textBox.Invalidate();
        base.OnMouseLeave(e);
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
        // Do we need to prevent the height from being altered?
        if (_autoSize && !Multiline)
        {
            switch (Dock)
            {
                case DockStyle.Fill:
                case DockStyle.Left:
                case DockStyle.Right:
                    if ((specified & ~BoundsSpecified.Height) == specified)
                    {
                        _cachedHeight = height;
                    }

                    break;
            }

            // Override the actual height used to the fixed height for single line
            height = PreferredHeight;
        }
        else
        {
            _cachedHeight = height;
        }

        base.SetBoundsCore(x, y, width, height, specified);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(100, PreferredHeight);

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated && !e.NeedLayout)
        {
            _textBox.Invalidate();
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
            PaletteState state = _drawDockerOuter.State;
            _textBox.BackColor = triple.PaletteBack.GetBackColor1(state);
            _textBox.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(state);

            // Only set the font if the text box has been created
            Font? font = triple.PaletteContent.GetContentShortTextFont(state);
            if ((_textBox.Handle != IntPtr.Zero) && !_textBox.Font.Equals(font))
            {
                _textBox.Font = font!;
            }
        }

        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the PaddingChanged event.
    /// </summary>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnPaddingChanged(EventArgs e)
    {
        base.OnPaddingChanged(e);

        // Add adjust actual height to match new setting
        AdjustHeight(false);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs containing the event data.</param>
    protected override void OnPaint(PaintEventArgs? e) => base.OnPaint(e);

    /// <summary>
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        TextBox.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        TextBox.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.NCHITTEST:
                if (InTransparentDesignMode)
                {
                    m.Result = (IntPtr)PI.HT.TRANSPARENT;
                }
                else
                {
                    base.WndProc(ref m);
                }

                break;
            case PI.WM_.LBUTTONDOWN:
                base.WndProc(ref m);
                break;
            default:
                base.WndProc(ref m);
                break;
        }
    }

    #endregion

    #region Internal
    internal bool InTransparentDesignMode => InRibbonDesignMode;

    #endregion

    #region Implementation

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

    internal IPaletteTriple GetTripleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

    private int PreferredHeight
    {
        get
        {
            // Get the preferred size of the entire control
            Size preferredSize = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));

            // We only need to the height
            return preferredSize.Height;
        }
    }

    private void AdjustHeight(bool ignoreAnchored)
    {
        // If any of the vertical edges are anchored then we might need to ignore the call
        if (!ignoreAnchored || ((Anchor & (AnchorStyles.Bottom | AnchorStyles.Top)) != (AnchorStyles.Bottom | AnchorStyles.Top)))
        {
            // If auto sizing the control and not in multiline mode then override the height
            // #1842 when autosize == true and MultiLine == true and the _cachedHeight == -1 which is the initial value
            // the box collapses. Only when _cachedHeight > -1 it will be assigned. Otherwise Height is left alone.
            if (_autoSize && !Multiline)
            {
                Height = PreferredHeight;
            }
            else if (_cachedHeight > -1)
            {
                Height = _cachedHeight;
            }
        }
    }

    private void OnTextBoxAcceptsTabChanged(object? sender, EventArgs e) => OnAcceptsTabChanged(e);

    private void OnTextBoxTextChanged(object? sender, EventArgs e) => OnTextChanged(e);

    private void OnTextBoxTextAlignChanged(object? sender, EventArgs e) => OnTextAlignChanged(e);

    private void OnTextBoxHideSelectionChanged(object? sender, EventArgs e) => OnHideSelectionChanged(e);

    private void OnTextBoxModifiedChanged(object? sender, EventArgs e) => OnModifiedChanged(e);

    private void OnTextBoxMultilineChanged(object? sender, EventArgs e) => OnMultilineChanged(e);

    private void OnTextBoxReadOnlyChanged(object? sender, EventArgs e) => OnReadOnlyChanged(e);

    private void OnTextBoxGotFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        PerformNeedPaint(true);
        OnGotFocus(e);
    }

    private void OnTextBoxLostFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        PerformNeedPaint(true);
        OnLostFocus(e);
    }

    private void OnTextBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnTextBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnTextBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnTextBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnTextBoxValidated(object? sender, EventArgs e) => OnValidated(e);

    private void OnTextBoxValidating(object? sender, CancelEventArgs e) => OnValidating(e);

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
                        if (Redirector != null)
                        {
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
                }

                if (sourceContent != null)
                {
                    // Remove any currently showing tooltip
                    if (_visualPopupToolTip != null)
                    {
                        _visualPopupToolTip.Dispose();

                        if (AllowButtonSpecToolTipPriority)
                        {
                            visualBasePopupToolTip?.Dispose();
                        }
                    }

                    // Create the actual tooltip popup object
                    _visualPopupToolTip = new VisualPopupToolTip(Redirector!,
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

    // Remove any currently showing tooltip
    private void OnCancelToolTip(object? sender, EventArgs e) => _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page any more
        _visualPopupToolTip = null;
    }

    private void OnTextBoxMouseChange(object? sender, EventArgs e)
    {
        // Change in tracking state?
        if (_textBox.MouseOver != _trackingMouseEnter)
        {
            _trackingMouseEnter = _textBox.MouseOver;

            // Raise appropriate event
            if (_trackingMouseEnter)
            {
                OnTrackMouseEnter(EventArgs.Empty);
                OnMouseEnter(e);
            }
            else
            {
                OnTrackMouseLeave(EventArgs.Empty);
                OnMouseLeave(e);
            }
        }
    }

    private void OnEditorButtonClicked(object? sender, EventArgs e) => new MultilineStringEditor1(this).ShowEditor();

    private void OnMouseDoubleClick(object? sender, MouseEventArgs e) => base.OnMouseDoubleClick(e);

    private void OnDoubleClick(object? sender, EventArgs e) => base.OnDoubleClick(e);

    private void OnTextBoxClick(object? sender, EventArgs e) =>
        // ReSharper disable RedundantBaseQualifier
        base.OnClick(e);
    // ReSharper restore RedundantBaseQualifier

    //private void SetIsInAlphaNumericMode(KryptonTextBox owner)
    //{
    //    // TODO: Return to this...
    //}

    private void ToggleEllipsisButtonVisibility(bool visible)
    {
        // Setup button
        var bsaEllipsisButton = new ButtonSpecAny();

        bsaEllipsisButton.Text = @"&...";

        if (visible)
        {
            ButtonSpecs.Add(bsaEllipsisButton);

            bsaEllipsisButton.Visible = true;
        }
        else
        {
            bsaEllipsisButton.Visible = false;

            ButtonSpecs.Remove(bsaEllipsisButton);
        }
    }

    #endregion
}