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
/// Provide a RichTextBox with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRichTextBox), "ToolboxBitmaps.KryptonRichTextBox.bmp")]
[DefaultEvent(nameof(TextChanged))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[Designer(typeof(KryptonRichTextBoxDesigner))]
[DesignerCategory(@"code")]
[Description(@"Enables the user to enter text, and provides multi-line editing and password character masking.")]
public class KryptonRichTextBox : VisualControlBase,
    IContainedInputControl
{
    #region Classes
    private class InternalRichTextBox : RichTextBox
    {
        #region Static Fields

        private const double AN_INCH = 14.4;

        #endregion

        #region Instance Fields
        private readonly KryptonRichTextBox _kryptonRichTextBox;
        private bool _mouseOver;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the mouse enters the InternalComboBox.
        /// </summary>
        public event EventHandler? TrackMouseEnter;

        /// <summary>
        /// Occurs when the mouse leaves the InternalComboBox.
        /// </summary>
        public event EventHandler? TrackMouseLeave;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InternalTextBox class.
        /// </summary>
        /// <param name="kryptonRichTextBox">Reference to owning control.</param>
        public InternalRichTextBox(KryptonRichTextBox kryptonRichTextBox)
        {
            _kryptonRichTextBox = kryptonRichTextBox;

            // Remove from view until size for the first time by the Krypton control
            Size = Size.Empty;

            // We provide the border manually
            BorderStyle = BorderStyle.None;
        }
        #endregion

        #region Public
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

        /// <summary>
        /// Print the specified range of characters.
        /// </summary>
        /// <param name="charFrom">Start character.</param>
        /// <param name="charTo">End character.</param>
        /// <param name="gr">Graphics instance to use.</param>
        /// <param name="bounds">Drawing bounds.</param>
        /// <returns>Pointer to returned result.</returns>
        public int Print(int charFrom, int charTo, Graphics gr, Rectangle bounds)
        {
            //Calculate the area to render and print
            PI.RECT rectToPrint;
            rectToPrint.top = 0;
            rectToPrint.bottom = (int)(bounds.Height * AN_INCH);
            rectToPrint.left = 0;
            rectToPrint.right = (int)(bounds.Width * AN_INCH);

            //Calculate the size of the page
            PI.RECT rectPage;
            rectPage.top = 0;
            rectPage.bottom = (int)(gr.ClipBounds.Height * AN_INCH);
            rectPage.left = 0;
            rectPage.right = (int)(gr.ClipBounds.Right * AN_INCH);

            var hdc = gr.GetHdc();

            PI.FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = charTo;
            fmtRange.chrg.cpMin = charFrom;
            fmtRange.hdc = hdc;
            fmtRange.hdcTarget = hdc;
            fmtRange.rc = rectToPrint;
            fmtRange.rcPage = rectPage;

            var wParam = new IntPtr(1);

            //Get the pointer to the FORMATRANGE structure in memory
            var lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lParam, false);

            //Send the rendered data for printing 
            var res = (IntPtr)PI.SendMessage(Handle, PI.EM_FORMATRANGE, wParam, lParam);

            //Free the block of memory allocated
            Marshal.FreeCoTaskMem(lParam);

            //Release the device context handle obtained by a previous call
            gr.ReleaseHdc(hdc);

            //Return last + 1 character printer
            return (int)res.ToInt64();
        }
        #endregion

        #region Protected
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Do not forward, to allow the correct Background for disabled state
            // See https://github.com/Krypton-Suite/Standard-Toolkit/issues/722
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
                    if (_kryptonRichTextBox.InTransparentDesignMode)
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
                    _kryptonRichTextBox.PerformNeedPaint(true);
                    Invalidate();
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSEMOVE:
                    // Mouse is over the control
                    if (!MouseOver)
                    {
                        MouseOver = true;
                        _kryptonRichTextBox.PerformNeedPaint(true);
                        Invalidate();
                    }
                    base.WndProc(ref m);
                    break;
                case PI.WM_.CONTEXTMENU:
                    // Only interested in overriding the behavior when we have a krypton context menu...
                    if (_kryptonRichTextBox.KryptonContextMenu != null)
                    {
                        // Extract the screen mouse position (if might not actually be provided)
                        var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                        // If keyboard activated, the menu position is centered
                        if (((int)(long)m.LParam) == -1)
                        {
                            mousePt = PointToScreen(new Point(Width / 2, Height / 2));
                        }

                        // Show the context menu
                        _kryptonRichTextBox.KryptonContextMenu.Show(_kryptonRichTextBox, mousePt);

                        // We eat the message!
                        return;
                    }
                    base.WndProc(ref m);
                    break;
                case PI.WM_.PAINT:
                    if (!string.IsNullOrWhiteSpace(_kryptonRichTextBox.CueHint.CueHintText)
                        && (_kryptonRichTextBox.TextLength == 0)
                       )
                    {
                        var ps = new PI.PAINTSTRUCT();
                        // Do we need to BeginPaint or just take the given HDC?
                        IntPtr hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;
                        using (Graphics g = Graphics.FromHdc(hdc))
                        {
                            // Grab the client area of the control
                            PI.GetClientRect(Handle, out PI.RECT rect);

                            PaletteState state = _kryptonRichTextBox.Enabled
                                ? _kryptonRichTextBox.IsActive
                                    ? PaletteState.Tracking
                                    : PaletteState.Normal
                                : PaletteState.Disabled;
                            IPaletteTriple states = _kryptonRichTextBox.GetTripleState();

                            // Drawn entire client area in the background color
                            using var backBrush = new SolidBrush(states.PaletteBack.GetBackColor1(state));
                            // Go perform the drawing of the CueHint
                            _kryptonRichTextBox.CueHint.PerformPaint(_kryptonRichTextBox, g, rect, backBrush);
                        }

                        // Do we need to match the original BeginPaint?
                        if (m.WParam == IntPtr.Zero)
                        {
                            PI.EndPaint(Handle, ref ps);
                        }
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

        /// <summary>
        /// Raises the OnMouseMove event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _kryptonRichTextBox.OnMouseMove(e);
        }
        #endregion
    }
    #endregion

    #region Instance Fields

    private VisualPopupToolTip? _visualPopupToolTip;
    private readonly ViewLayoutDocker _drawDockerInner;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly InternalRichTextBox _richTextBox;
    private InputControlStyle _inputControlStyle;
    private bool? _fixedActive;
    private bool _forcedLayout;
    private bool _autoSize;
    private bool _mouseOver;
    private bool _alwaysActive;
    private bool _trackingMouseEnter;
    private bool _firstPaint;
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
    /// Occurs when the current selection has changed.
    /// </summary>
    [Description(@"Occurs when the current selection has changed.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectionChanged;

    /// <summary>
    /// Occurs when the user takes an action that would change a protected range of text.
    /// </summary>
    [Description(@"Occurs when the user takes an action that would change a protected range of text.")]
    [Category(@"Behavior")]
    public event EventHandler? Protected;

    /// <summary>
    /// Occurs when a hyperlink in the text is clicked.
    /// </summary>
    [Description(@"Occurs when a hyperlink in the text is clicked.")]
    [Category(@"Behavior")]
    public event LinkClickedEventHandler? LinkClicked;

    /// <summary>
    /// Occurs when the horizontal scroll bar is clicked.
    /// </summary>
    [Description(@"Occurs when the horizontal scroll bar is clicked.")]
    [Category(@"Behavior")]
    public event EventHandler? HScroll;

    /// <summary>
    /// Occurs when the vertical scroll bar is clicked.
    /// </summary>
    [Description(@"Occurs when the vertical scroll bar is clicked.")]
    [Category(@"Behavior")]
    public event EventHandler? VScroll;

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
    /// Initialize a new instance of the KryptonRichTextBox class.
    /// </summary>
    public KryptonRichTextBox()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // Cannot select this control, only the child TextBox
        SetStyle(ControlStyles.Selectable, false);

        // Defaults
        _autoSize = false;
        _alwaysActive = true;
        _firstPaint = true;
        _inputControlStyle = InputControlStyle.Standalone;

        // Create the palette storage
        StateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateActive = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        CueHint = new PaletteCueHintText(Redirector, NeedPaintDelegate);

        // Create the internal text box used for containing content
        _richTextBox = new InternalRichTextBox(this);
        _richTextBox.TrackMouseEnter += OnRichTextBoxMouseChange;
        _richTextBox.TrackMouseLeave += OnRichTextBoxMouseChange;
        _richTextBox.AcceptsTabChanged += OnRichTextBoxAcceptsTabChanged;
        _richTextBox.TextChanged += OnRichTextBoxTextChanged;
        _richTextBox.HideSelectionChanged += OnRichTextBoxHideSelectionChanged;
        _richTextBox.ModifiedChanged += OnRichTextBoxModifiedChanged;
        _richTextBox.MultilineChanged += OnRichTextBoxMultilineChanged;
        _richTextBox.ReadOnlyChanged += OnRichTextBoxReadOnlyChanged;
        _richTextBox.GotFocus += OnRichTextBoxGotFocus;
        _richTextBox.LostFocus += OnRichTextBoxLostFocus;
        _richTextBox.KeyDown += OnRichTextBoxKeyDown;
        _richTextBox.KeyUp += OnRichTextBoxKeyUp;
        _richTextBox.KeyPress += OnRichTextBoxKeyPress;
        _richTextBox.PreviewKeyDown += OnRichTextBoxPreviewKeyDown;
        _richTextBox.LinkClicked += OnRichTextBoxLinkClicked;
        _richTextBox.Protected += OnRichTextBoxProtected;
        _richTextBox.SelectionChanged += OnRichTextBoxSelectionChanged;
        _richTextBox.HScroll += OnRichTextBoxHScroll;
        _richTextBox.VScroll += OnRichTextBoxVScroll;
        _richTextBox.Validating += OnRichTextBoxValidating;
        _richTextBox.Validated += OnRichTextBoxValidated;

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_richTextBox);

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

        // Create the manager for handling tooltips
        ToolTipManager = new ToolTipManager(ToolTipValues);
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;

        // Add text box to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_richTextBox);

        // Update the back/fore/font from the palette settings
        UpdateStateAndPalettes();
        _richTextBox.BackColor = StateActive.PaletteBack.GetBackColor1(PaletteState.Tracking);
        _richTextBox.ForeColor = StateActive.PaletteContent.GetContentShortTextColor1(PaletteState.Tracking);

        // Only set the font if the rich text box has been created
        if (_richTextBox.Handle != IntPtr.Zero)
        {
            _richTextBox.Font = StateActive.PaletteContent.GetContentShortTextFont(PaletteState.Tracking)!;
        }
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
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// 
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Set a watermark/prompt message for the user.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteCueHintText CueHint { get; }

    private bool ShouldSerializeCueHint() => !CueHint.IsDefault;

    /// <summary>
    /// Gets and sets if the control is in the tab chain.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool TabStop
    {
        get => _richTextBox.TabStop;
        set => _richTextBox.TabStop = value;
    }

    /// <summary>
    /// Gets and sets if the control is in the ribbon design mode.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool InRibbonDesignMode { get; set; }

    /// <summary>
    /// Gets access to the contained RichTextBox instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public RichTextBox RichTextBox => _richTextBox;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => RichTextBox;

    /// <summary>
    /// Gets a value indicating whether the control has input focus.
    /// </summary>
    [Browsable(false)]
    public override bool Focused => RichTextBox.Focused;

    /// <summary>
    /// Gets or sets the ability to drag/drop onto the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool AllowDrop
    {
        get => base.AllowDrop;
        set => base.AllowDrop = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if the control is automatically sized.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public override bool AutoSize
    {
        get => _autoSize;
        set => _autoSize = value;
    }

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
    public override string Text
    {
        get => _richTextBox.Text;
        set => _richTextBox.Text = value;
    }

    private bool ShouldSerializeText() =>
        // Must always persist the text value if even when it is the default. Otherwise when
        // you first move over the control which has been given RTF it resets the fonts.
        true;

    /// <summary>
    /// Gets the length of text in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TextLength => _richTextBox.TextLength;

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [DefaultValue(null)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        get => base.ContextMenuStrip;

        set
        {
            base.ContextMenuStrip = value;
            _richTextBox.ContextMenuStrip = value;
        }
    }

    /// <summary>
    /// Gets and sets if the control can redo a previously undo operation.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanRedo => _richTextBox.CanRedo;

    /// <summary>
    /// Gets a value indicating whether the user can undo the previous operation in a rich text box control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanUndo => _richTextBox.CanUndo;

    /// <summary>
    /// Gets a value indicating whether the contents have changed since last.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Modified => _richTextBox.Modified;

    /// <summary>
    /// Gets and sets the language option.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RichTextBoxLanguageOptions LanguageOption
    {
        get => _richTextBox.LanguageOption;
        set => _richTextBox.LanguageOption = value;
    }

    /// <summary>
    /// Gets and sets the name of the action to be redone.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? RedoActionName => _richTextBox.RedoActionName;

    /// <summary>
    /// Gets and sets the name of the action to be undone.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? UndoActionName => _richTextBox.UndoActionName;

    /// <summary>
    /// Gets and sets if keyboard shortcuts are enabled.
    /// </summary>
    [Browsable(false)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool RichTextShortcutsEnabled
    {
        get => _richTextBox.RichTextShortcutsEnabled;
        set => _richTextBox.RichTextShortcutsEnabled = value;
    }

    /// <summary>
    /// Gets and sets the text in rich text format.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RefreshProperties(RefreshProperties.All)]
    [AllowNull]
    public string? Rtf
    {
        get => _richTextBox.Rtf ?? string.Empty;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.Rtf = value;
        }
    }

    /// <summary>
    /// Gets and sets the selection portion of the rich text format.
    /// </summary>
    [Browsable(false)]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedRtf
    {
        get => _richTextBox.SelectedRtf;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectedRtf = value;
        }
    }

    /// <summary>
    /// Gets and sets the selected text within the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
        get => _richTextBox.SelectedText;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectedText = value;
        }
    }

    /// <summary>
    /// Gets and sets the alignment of the selection.
    /// </summary>
    [Browsable(false)]
    [DefaultValue(HorizontalAlignment.Left)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public HorizontalAlignment SelectionAlignment
    {
        get => _richTextBox.SelectionAlignment;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionAlignment = value;
        }
    }

    /// <summary>
    /// Gets and sets the background color of the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionBackColor
    {
        get => _richTextBox.SelectionBackColor;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionBackColor = value;
        }
    }

    /// <summary>
    /// Gets and sets the bullet indentation of the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SelectionBullet
    {
        get => _richTextBox.SelectionBullet;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionBullet = value;
        }
    }

    /// <summary>
    /// Gets and sets the character offset of the selection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionCharOffset
    {
        get => _richTextBox.SelectionCharOffset;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionCharOffset = value;
        }
    }

    /// <summary>
    /// Gets and sets the text color of the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionColor
    {
        get => _richTextBox.SelectionColor;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionColor = value;
        }
    }

    /// <summary>
    /// Gets and sets the text font for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font SelectionFont
    {
        // Tested that: the System.WWindows.Forms.RichTextBox returns RichtTextBox.Font when no text has been selected.
        // Null forgiving operator removes the warning.
        get => _richTextBox.SelectionFont!;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionFont = value;
        }
    }

    /// <summary>
    /// Gets and sets the hanging indent for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionHangingIndent
    {
        get => _richTextBox.SelectionHangingIndent;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionHangingIndent = value;
        }
    }

    /// <summary>
    /// Gets and sets the indent for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionIndent
    {
        get => _richTextBox.SelectionIndent;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionIndent = value;
        }
    }

    /// <summary>
    /// Gets and sets the selection length for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => _richTextBox.SelectionLength;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionLength = value;
        }
    }

    /// <summary>
    /// Gets and sets the protected setting for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionProtected
    {
        get => _richTextBox.SelectionLength;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionLength = value;
        }
    }

    /// <summary>
    /// Gets and sets the right indent for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionRightIndent
    {
        get => _richTextBox.SelectionRightIndent;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionRightIndent = value;
        }
    }

    /// <summary>
    /// Gets and sets the starting point of text selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => _richTextBox.SelectionStart;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionStart = value;
        }
    }

    /// <summary>
    /// Gets and sets the tab settings for the selected area.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int[] SelectionTabs
    {
        get => _richTextBox.SelectionTabs;

        set
        {
            PerformNeedPaint(true);
            _richTextBox.SelectionTabs = value;
        }
    }

    /// <summary>
    /// Gets and sets the type of selection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RichTextBoxSelectionTypes SelectionType => _richTextBox.SelectionType;

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
        get => _richTextBox.Lines;
        set => _richTextBox.Lines = value;
    }

    /// <summary>
    /// Gets or sets, for multiline edit controls, which scroll bars will be shown for this control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates, for multiline edit controls, which scroll bars will be shown for this control.")]
    [DefaultValue(RichTextBoxScrollBars.Both)]
    [Localizable(true)]
    public RichTextBoxScrollBars ScrollBars
    {
        get => _richTextBox.ScrollBars;
        set => _richTextBox.ScrollBars = value;
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
        get => _richTextBox.WordWrap;
        set => _richTextBox.WordWrap = value;
    }

    /// <summary>
    /// Defines the right margin dimensions.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Defines the right margin dimensions.")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int RightMargin
    {
        get => _richTextBox.RightMargin;
        set => _richTextBox.RightMargin = value;
    }

    /// <summary>
    /// Turns on/off the selection margin.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Turns on/off the selection margin.")]
    [DefaultValue(false)]
    public bool ShowSelectionMargin
    {
        get => _richTextBox.ShowSelectionMargin;
        set => _richTextBox.ShowSelectionMargin = value;
    }

    /// <summary>
    /// Defines the current scaling factor of the KryptonRichTextBox display; 1.0 is normal viewing.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Defines the current scaling factor of the KryptonRichTextBox display; 1.0 is normal viewing.")]
    [DefaultValue(1.0f)]
    [Localizable(true)]
    public float ZoomFactor
    {
        get => _richTextBox.ZoomFactor;
        set => _richTextBox.ZoomFactor = value;
    }

    /// <summary>
    /// Gets and sets whether the text in the control can span more than one line.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Control whether the text in the control can span more than one line.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    [Localizable(true)]
    public bool Multiline
    {
        get => _richTextBox.Multiline;
        set => _richTextBox.Multiline = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if tab characters are accepted as input for multiline edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if tab characters are accepted as input for multiline edit controls.")]
    [DefaultValue(false)]
    public bool AcceptsTab
    {
        get => _richTextBox.AcceptsTab;
        set => _richTextBox.AcceptsTab = value;
    }

    /// <summary>
    /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates that the selection should be hidden when the edit control loses focus.")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => _richTextBox.HideSelection;
        set => _richTextBox.HideSelection = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the edit control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies the maximum number of characters that can be entered into the edit control.")]
    [DefaultValue(0x7fffffff)]
    [Localizable(true)]
    public int MaxLength
    {
        get => _richTextBox.MaxLength;
        set => _richTextBox.MaxLength = value;
    }

    /// <summary>
    /// Turns on/off automatic word selection.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Turns on/off automatic word selection.")]
    [DefaultValue(false)]
    public bool AutoWordSelection
    {
        get => _richTextBox.AutoWordSelection;
        set => _richTextBox.AutoWordSelection = value;
    }

    /// <summary>
    /// Defines the indent for bullets in the control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Defines the indent for bullets in the control.")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int BulletIndent
    {
        get => _richTextBox.BulletIndent;
        set => _richTextBox.BulletIndent = value;
    }

    /// <summary>
    /// Indicates whether URLs are automatically formatted as links.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether URLs are automatically formatted as links.")]
    [DefaultValue(true)]
    public bool DetectUrls
    {
        get => _richTextBox.DetectUrls;
        set => _richTextBox.DetectUrls = value;
    }

    /// <summary>
    /// Enable drag/drop of text, pictures and other data.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Enable drag/drop of text, pictures and other data.")]
    [DefaultValue(false)]
    public bool EnableAutoDragDrop
    {
        get => _richTextBox.EnableAutoDragDrop;
        set => _richTextBox.EnableAutoDragDrop = value;
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
        get => _richTextBox.ReadOnly;
        set => _richTextBox.ReadOnly = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether shortcuts defined for the control are enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether shortcuts defined for the control are enabled.")]
    [DefaultValue(true)]
    public bool ShortcutsEnabled
    {
        get => _richTextBox.ShortcutsEnabled;
        set => _richTextBox.ShortcutsEnabled = value;
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

    private bool ShouldSerializeInputControlStyle() => InputControlStyle != InputControlStyle.Standalone;
    private void ResetInputControlStyle() => InputControlStyle = InputControlStyle.Standalone;

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
    public void AppendText(string text) => _richTextBox.AppendText(text);

    /// <summary>
    /// Clears all text from the text box control.
    /// </summary>
    public void Clear() => _richTextBox.Clear();

    /// <summary>
    /// Clears information about the most recent operation from the undo buffer of the rich text box. 
    /// </summary>
    public void ClearUndo() => _richTextBox.ClearUndo();

    /// <summary>
    /// Copies the current selection in the text box to the Clipboard.
    /// </summary>
    public void Copy() => _richTextBox.Copy();

    /// <summary>
    /// Moves the current selection in the text box to the Clipboard.
    /// </summary>
    public void Cut() => _richTextBox.Cut();

    /// <summary>
    /// Specifies that the value of the SelectionLength property is zero so that no characters are selected in the control.
    /// </summary>
    public void DeselectAll() => _richTextBox.DeselectAll();

    /// <summary>
    /// Determines whether you can paste information from the Clipboard in the specified data format.
    /// </summary>
    /// <param name="clipFormat">One of the System.Windows.Forms.DataFormats.Format values.</param>
    /// <returns>true if you can paste data from the Clipboard in the specified data format; otherwise, false.</returns>
    public bool CanPaste(DataFormats.Format clipFormat) => _richTextBox.CanPaste(clipFormat);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <returns>The location within the control where the search text was found or -1 if the search string is not found or an empty search string is specified in the str parameter.</returns>
    public int Find(string str) => _richTextBox.Find(str);

    /// <summary>
    /// Searches the text of a RichTextBox control for the first instance of a character from a list of characters.
    /// </summary>
    /// <param name="characterSet">The array of characters to search for.</param>
    /// <returns>The location within the control where the search characters were found or -1 if the search characters are not found or an empty search character set is specified in the char parameter.</returns>
    public int Find(char[] characterSet) => _richTextBox.Find(characterSet);

    /// <summary>
    /// Searches the text of a RichTextBox control, at a specific starting point, for the first instance of a character from a list of characters.
    /// </summary>
    /// <param name="characterSet">The array of characters to search for.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <returns>The location within the control where the search characters are found.</returns>
    public int Find(char[] characterSet, int start) => _richTextBox.Find(characterSet, start);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string with specific options applied to the search.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <param name="options">A bitwise combination of the RichTextBoxFinds values.</param>
    /// <returns>The location within the control where the search text was found.</returns>
    public int Find(string str, RichTextBoxFinds options) => _richTextBox.Find(str, options);

    /// <summary>
    /// Searches a range of text in a RichTextBox control for the first instance of a character from a list of characters.
    /// </summary>
    /// <param name="characterSet">The array of characters to search for.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <param name="end">The location within the control's text at which to end searching.</param>
    /// <returns>The location within the control where the search characters are found.</returns>
    public int Find(char[] characterSet, int start, int end) => _richTextBox.Find(characterSet, start, end);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string at a specific location within the control and with specific options applied to the search.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <param name="options">A bitwise combination of the RichTextBoxFinds values.</param>
    /// <returns>The location within the control where the search text was found.</returns>
    public int Find(string str, int start, RichTextBoxFinds options) => _richTextBox.Find(str, start, options);

    /// <summary>
    /// Searches the text in a RichTextBox control for a string within a range of text within the control and with specific options applied to the search.
    /// </summary>
    /// <param name="str">The text to locate in the control.</param>
    /// <param name="start">The location within the control's text at which to begin searching.</param>
    /// <param name="end">The location within the control's text at which to end searching. This value must be equal to negative one (-1) or greater than or equal to the start parameter.</param>
    /// <param name="options">A bitwise combination of the RichTextBoxFinds values.</param>
    /// <returns></returns>
    public int Find(string str, int start, int end, RichTextBoxFinds options) => _richTextBox.Find(str, start, end, options);

    /// <summary>
    /// Retrieves the character that is closest to the specified location within the control.
    /// </summary>
    /// <param name="pt">The location from which to seek the nearest character.</param>
    /// <returns>The character at the specified location.</returns>
    public int GetCharFromPosition(Point pt) => _richTextBox.GetCharFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the character nearest to the specified location.
    /// </summary>
    /// <param name="pt">The location to search.</param>
    /// <returns>The zero-based character index at the specified location.</returns>
    public int GetCharIndexFromPosition(Point pt) => _richTextBox.GetCharIndexFromPosition(pt);

    /// <summary>
    /// Retrieves the index of the first character of a given line.
    /// </summary>
    /// <param name="lineNumber">The line for which to get the index of its first character.</param>
    /// <returns>The zero-based character index in the specified line.</returns>
    public int GetFirstCharIndexFromLine(int lineNumber) => _richTextBox.GetFirstCharIndexFromLine(lineNumber);

    /// <summary>
    /// Retrieves the index of the first character of the current line.
    /// </summary>
    /// <returns>The zero-based character index in the current line.</returns>
    public int GetFirstCharIndexOfCurrentLine() => _richTextBox.GetFirstCharIndexOfCurrentLine();

    /// <summary>
    /// Retrieves the line number from the specified character position within the text of the RichTextBox control.
    /// </summary>
    /// <param name="index">The character index position to search.</param>
    /// <returns>The zero-based line number in which the character index is located.</returns>
    public int GetLineFromCharIndex(int index) => _richTextBox.GetLineFromCharIndex(index);

    /// <summary>
    /// Retrieves the location within the control at the specified character index.
    /// </summary>
    /// <param name="index">The index of the character for which to retrieve the location.</param>
    /// <returns>The location of the specified character.</returns>
    public Point GetPositionFromCharIndex(int index) => _richTextBox.GetPositionFromCharIndex(index);

    /// <summary>
    /// Loads a rich text format (RTF) or standard ASCII text file into the RichTextBox control.
    /// </summary>
    /// <param name="path">The name and location of the file to load into the control.</param>
    public void LoadFile(string path) => _richTextBox.LoadFile(path);

    /// <summary>
    /// Loads the contents of an existing data stream into the RichTextBox control.
    /// </summary>
    /// <param name="data">A stream of data to load into the RichTextBox control.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void LoadFile(Stream data, RichTextBoxStreamType fileType) => _richTextBox.LoadFile(data, fileType);

    /// <summary>
    /// Loads a specific type of file into the RichTextBox control.
    /// </summary>
    /// <param name="path">The name and location of the file to load into the control.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void LoadFile(string path, RichTextBoxStreamType fileType) => _richTextBox.LoadFile(path, fileType);

    /// <summary>
    /// Replaces the current selection in the text box with the contents of the Clipboard.
    /// </summary>
    public void Paste() => _richTextBox.Paste();

    /// <summary>
    /// Undoes the last edit operation in the text box.
    /// </summary>
    public void Undo() => _richTextBox.Undo();

    /// <summary>
    /// Pastes the contents of the Clipboard in the specified Clipboard format.
    /// </summary>
    /// <param name="clipFormat">The Clipboard format in which the data should be obtained from the Clipboard.</param>
    public void Paste(DataFormats.Format clipFormat) => _richTextBox.Paste(clipFormat);

    /// <summary>
    /// Reapplies the last operation that was undone in the control.
    /// </summary>
    public void Redo() => _richTextBox.Redo();

    /// <summary>
    /// Saves the contents of the RichTextBox to a rich text format (RTF) file.
    /// </summary>
    /// <param name="path">The name and location of the file to save.</param>
    public void SaveFile(string path) => _richTextBox.SaveFile(path);

    /// <summary>
    /// Saves the contents of a RichTextBox control to an open data stream.
    /// </summary>
    /// <param name="data">The data stream that contains the file to save to.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void SaveFile(Stream data, RichTextBoxStreamType fileType) => _richTextBox.SaveFile(data, fileType);

    /// <summary>
    /// Saves the contents of the KryptonRichTextBox to a specific type of file.
    /// </summary>
    /// <param name="path">The name and location of the file to save.</param>
    /// <param name="fileType">One of the RichTextBoxStreamType values.</param>
    public void SaveFile(string path, RichTextBoxStreamType fileType) => _richTextBox.SaveFile(path, fileType);

    /// <summary>
    /// Scrolls the contents of the control to the current caret position.
    /// </summary>
    public void ScrollToCaret() => _richTextBox.ScrollToCaret();

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => _richTextBox.Select(start, length);

    /// <summary>
    /// Selects all text in the control.
    /// </summary>
    public void SelectAll() => _richTextBox.SelectAll();

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
        _fixedActive ?? DesignMode || AlwaysActive || ContainsFocus || _mouseOver || _richTextBox.MouseOver;

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => RichTextBox.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => RichTextBox.Select();

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
            // Ask the view to Perform a layout
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
            return new Rectangle(_richTextBox.Location, _richTextBox.Size);
        }
    }

    /// <summary>
    /// Print the specified range of characters.
    /// </summary>
    /// <param name="charFrom">Start character.</param>
    /// <param name="charTo">End character.</param>
    /// <param name="gr">Graphics instance to use.</param>
    /// <param name="bounds">Drawing bounds.</param>
    /// <returns>Pointer to returned result.</returns>
    public int Print(int charFrom, int charTo, Graphics gr, Rectangle bounds) => _richTextBox.Print(charFrom, charTo, gr, bounds);

    /// <summary>
    /// Override the display padding for the layout fill.
    /// </summary>
    /// <param name="padding">Display padding value.</param>
    public void SetLayoutDisplayPadding(Padding padding) => _layoutFill.DisplayPadding = padding;

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool DesignerGetHitTest(Point pt)
    {
        // Ignore call as view builder is already destructed
        return !IsDisposed;
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
        _forcedLayout = true;
        OnLayout(new LayoutEventArgs(null, null));
        _forcedLayout = false;
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the AcceptsTabChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnAcceptsTabChanged(EventArgs e) => AcceptsTabChanged?.Invoke(this, e);

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
    /// Raises the VScroll event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnVScroll(EventArgs e) => VScroll?.Invoke(this, e);

    /// <summary>
    /// Raises the HScroll event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnHScroll(EventArgs e) => HScroll?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectionChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnSelectionChanged(EventArgs e) => SelectionChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the Protected event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnProtected(EventArgs e) => Protected?.Invoke(this, e);

    /// <summary>
    /// Raises the LinkClicked event.
    /// </summary>
    /// <param name="e">A LinkClickedEventArgs that contains the event data.</param>
    protected virtual void OnLinkClicked(LinkClickedEventArgs e) => LinkClicked?.Invoke(this, e);

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

        // Force the font to be set into the text box child control
        PerformNeedPaint(false);

        // We need a layout to occur before any painting
        InvokeLayout();
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

        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
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
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        RichTextBox.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        RichTextBox.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
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
            Padding contentPadding = GetTripleState().PaletteContent!.GetBorderContentPadding(null, _drawDockerOuter.State);
            _layoutFill.DisplayPadding = contentPadding;
        }

        // Let base class calculate fill rectangle
        base.OnLayout(levent);

        if (!IsDisposed && !Disposing)
        {
            // Only use layout logic if control is fully initialized or if being forced
            // to allow a relayout or if in design mode.
            if (_forcedLayout || DesignMode)
            {
                Rectangle fillRect = _layoutFill.FillRect;
                _richTextBox.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
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
        _richTextBox.Invalidate();
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
        _richTextBox.Invalidate();
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        _richTextBox.Focus();
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(100, 96);

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (!e.NeedLayout)
        {
            _richTextBox.Invalidate();
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

            Color backColor = triple.PaletteBack.GetBackColor1(state);
            if (_richTextBox.BackColor != backColor)
            {
                _richTextBox.BackColor = backColor;
            }

            Color foreColor = triple.PaletteContent!.GetContentShortTextColor1(state);
            if (_richTextBox.ForeColor != foreColor)
            {
                _richTextBox.ForeColor = foreColor;
            }

            // Only set the font if the rich text box has been created
            Font? font = triple.PaletteContent.GetContentShortTextFont(state);
            if ((_richTextBox.Handle != IntPtr.Zero) && !_richTextBox.Font.Equals(font))
            {
                _richTextBox.Font = font!;
            }
        }

        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs containing the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        if (_firstPaint)
        {
            _firstPaint = false;
            ForceControlLayout();
        }

        base.OnPaint(e);
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
            default:
                base.WndProc(ref m);
                break;
        }
    }

    /// <summary>
    /// Fixes the cursor changing issue.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnCursorChanged(EventArgs e)
    {
        base.OnCursorChanged(e);

        _richTextBox.Cursor = Cursor;
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

    private IPaletteTriple GetTripleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

    private void OnRichTextBoxMouseChange(object? sender, EventArgs e)
    {
        // Change in tracking state?
        if (_richTextBox.MouseOver != _trackingMouseEnter)
        {
            _trackingMouseEnter = _richTextBox.MouseOver;

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

    private void OnRichTextBoxAcceptsTabChanged(object? sender, EventArgs e) => OnAcceptsTabChanged(e);

    private void OnRichTextBoxTextChanged(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(CueHint.CueHintText)
            && TextLength <= 1)
        {
            // Needed to prevent character turds being left behind
            // Oh, and to get rid of the initial cuetext drawing ;-)
            _richTextBox.Invalidate();
        }

        OnTextChanged(e);
    }

    private void OnRichTextBoxHideSelectionChanged(object? sender, EventArgs e) => OnHideSelectionChanged(e);

    private void OnRichTextBoxModifiedChanged(object? sender, EventArgs e) => OnModifiedChanged(e);

    private void OnRichTextBoxMultilineChanged(object? sender, EventArgs e) => OnMultilineChanged(e);

    private void OnRichTextBoxReadOnlyChanged(object? sender, EventArgs e) => OnReadOnlyChanged(e);

    private void OnRichTextBoxGotFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        PerformNeedPaint(true);
        OnGotFocus(e);
    }

    private void OnRichTextBoxLostFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        PerformNeedPaint(true);
        OnLostFocus(e);
    }

    private void OnRichTextBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnRichTextBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnRichTextBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnRichTextBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnRichTextBoxVScroll(object? sender, EventArgs e) => OnVScroll(e);

    private void OnRichTextBoxHScroll(object? sender, EventArgs e) => OnHScroll(e);

    private void OnRichTextBoxSelectionChanged(object? sender, EventArgs e) => OnSelectionChanged(e);

    private void OnRichTextBoxProtected(object? sender, EventArgs e) => OnProtected(e);

    private void OnRichTextBoxLinkClicked(object? sender, LinkClickedEventArgs e) => OnLinkClicked(e);

    private void OnRichTextBoxValidated(object? sender, EventArgs e) => OnValidated(e);

    private void OnRichTextBoxValidating(object? sender, CancelEventArgs e) => OnValidating(e);

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

            // Never show tooltips are design time
            if (!DesignMode)
            {
                IContentValues? sourceContent = null;
                var toolTipStyle = LabelStyle.ToolTip;

                var shadow = true;

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
        VisualPopupToolTip popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page anymore
        _visualPopupToolTip = null;
    }
    #endregion
}