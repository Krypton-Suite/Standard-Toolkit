#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

#pragma warning disable 67
namespace Krypton.Toolkit;

/// <summary>
/// Provide a ListBox with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonListBox), "ToolboxBitmaps.KryptonListBox.bmp")]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(Items))]
[DefaultBindingProperty(nameof(SelectedValue))]
[Designer(typeof(KryptonListBoxDesigner))]
[DesignerCategory(@"code")]
[Description(@"Represents a list box control that allows single or multiple item selection.")]
public class KryptonListBox : VisualControlBase,
    IContainedInputControl
{
    #region Classes
    private class InternalListBox : ListBox
    {
        #region Instance Fields
        private readonly ViewManager? _viewManager;
        private readonly KryptonListBox _kryptonListBox;
        private readonly IntPtr _screenDC;
        private bool _mouseOver;
        // Capture scroll position before user click
        private int _preClickTopIndex;

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the mouse enters the InternalListBox.
        /// </summary>
        public event EventHandler? TrackMouseEnter;

        /// <summary>
        /// Occurs when the mouse leaves the InternalListBox.
        /// </summary>
        public event EventHandler? TrackMouseLeave;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InternalListBox class.
        /// </summary>
        /// <param name="kryptonListBox">Reference to owning control.</param>
        public InternalListBox(KryptonListBox kryptonListBox)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);

            _kryptonListBox = kryptonListBox;
            MouseIndex = -1;

            // Create manager and view for drawing the background
            ViewDrawPanel = new ViewDrawPanel();
            _viewManager = new ViewManager(this, ViewDrawPanel);

            // Set required properties to act as an owner draw list box
            // ReSharper disable RedundantBaseQualifier
            base.Size = Size.Empty;
            base.BorderStyle = BorderStyle.None;
            base.IntegralHeight = false;
            base.MultiColumn = false;
            base.DrawMode = DrawMode.OwnerDrawVariable;
            // ReSharper restore RedundantBaseQualifier

            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);
            // Track pre-click scroll
            MouseDown += OnInternalListBoxMouseDown;
        }

        /// <summary>
        /// Releases all resources used by the Control.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_screenDC != IntPtr.Zero)
            {
                PI.DeleteDC(_screenDC);
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Recreate the window handle.
        /// </summary>
        public void Recreate() => RecreateHandle();

        /// <summary>
        /// Gets access to the contained view draw panel instance.
        /// </summary>
        public ViewDrawPanel ViewDrawPanel { get; }

        /// <summary>
        /// Gets the item index the mouse is over.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MouseIndex { get; private set; }

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
                        MouseIndex = -1;
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the drawing mode of the checked list box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override DrawMode DrawMode
        {
            get => DrawMode.OwnerDrawVariable;
            set { }
        }

        /// <summary>
        /// Force the remeasure of items, so they are sized correctly.
        /// </summary>
        public void RefreshItemSizes()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DrawMode = DrawMode.OwnerDrawVariable;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Ask the panel to layout given our available size
            using var context = new ViewLayoutContext(_viewManager, this, _kryptonListBox, _kryptonListBox.Renderer);
            ViewDrawPanel.Layout(context);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.ERASEBKGND:
                    // Do not draw the background here, always do it in the paint
                    // instead to prevent flicker because of a two stage drawing process
                    break;
                case PI.WM_.PRINTCLIENT:
                case PI.WM_.PAINT:
                    WmPaint(ref m);
                    break;
                case PI.WM_.VSCROLL:
                case PI.WM_.HSCROLL:
                case PI.WM_.MOUSEWHEEL:
                    Invalidate();
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSELEAVE:
                    // Mouse is not over the control
                    MouseOver = false;
                    _kryptonListBox.PerformNeedPaint(true);
                    Invalidate();
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSEMOVE:
                    // Mouse is over the control
                    if (!MouseOver)
                    {
                        MouseOver = true;
                        _kryptonListBox.PerformNeedPaint(true);
                        Invalidate();
                    }
                    else
                    {
                        // Find the item under the mouse
                        var mousePoint = new Point((int)m.LParam.ToInt64());
                        var mouseIndex = IndexFromPoint(mousePoint);

                        // If we have an actual item from the point
                        if ((mouseIndex >= 0) && (mouseIndex < Items.Count))
                        {
                            // Check that the mouse really is in the item rectangle
                            Rectangle indexRect = GetItemRectangle(mouseIndex);
                            if (!indexRect.Contains(mousePoint))
                            {
                                mouseIndex = -1;
                            }
                        }

                        // If item under mouse has changed, then need to reflect for tracking
                        if (MouseIndex != mouseIndex)
                        {
                            Invalidate();
                            MouseIndex = mouseIndex;
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
        #endregion

        #region Private
        private void WmPaint(ref Message m)
        {
            var ps = new PI.PAINTSTRUCT();

            // Do we need to BeginPaint or just take the given HDC?
            var hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
            Rectangle realRect = CommonHelper.RealClientRectangle(Handle);

            // No point drawing when one of the dimensions is zero
            if (realRect is { Width: > 0, Height: > 0 })
            {
                var hBitmap = PI.CreateCompatibleBitmap(hdc, realRect.Width, realRect.Height);

                // If we managed to get a compatible bitmap
                if (hBitmap != IntPtr.Zero)
                {
                    // Must use the screen device context for the bitmap when drawing into the
                    // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                    // Select the new bitmap into the screen DC
                    var oldBitmap = PI.SelectObject(_screenDC, hBitmap);

                    try
                    {

                        // Easier to draw using a graphics instance than a DC!
                        using (Graphics g = Graphics.FromHdc(_screenDC))
                        {
                            // Ask the view element to layout in given space, needs this before a render call
                            using (var context = new ViewLayoutContext(this, _kryptonListBox.Renderer))
                            {
                                context.DisplayRectangle = realRect;
                                ViewDrawPanel.Layout(context);
                            }

                            using (var context = new RenderContext(this, _kryptonListBox, g, realRect,
                                       _kryptonListBox.Renderer))
                            {
                                ViewDrawPanel.Render(context);
                            }

                            // Replace given DC with the screen DC for base window proc drawing
                            var beforeDC = m.WParam;
                            m.WParam = _screenDC;
                            DefWndProc(ref m);
                            m.WParam = beforeDC;

                            if (Items.Count == 0)
                            {
                                using var context = new RenderContext(this, _kryptonListBox, g, realRect,
                                    _kryptonListBox.Renderer);
                                ViewDrawPanel.Render(context);
                            }
                        }

                        // Now blit from the bitmap from the screen to the real dc
                        PI.BitBlt(hdc, 0, 0, realRect.Width, realRect.Height, _screenDC, 0, 0, PI.SRCCOPY);

                        // When disabled with no items the above code does not draw the background!
                        if (Items.Count == 0)
                        {
                            using Graphics g = Graphics.FromHdc(hdc);
                            using var context = new RenderContext(this, _kryptonListBox, g, realRect,
                                _kryptonListBox.Renderer);
                            ViewDrawPanel.Render(context);
                        }
                    }
                    finally
                    {
                        // Restore the original bitmap
                        PI.SelectObject(_screenDC, oldBitmap);

                        // Delete the temporary bitmap
                        PI.DeleteObject(hBitmap);
                    }
                }
            }

            // Complete BeginPaint if we started one
            if (m.WParam == IntPtr.Zero)
            {
                PI.EndPaint(Handle, ref ps);
            }
        }
        #endregion

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            // Prevent ObjectDisposedException if handle is invalid
            if (!IsHandleCreated || IsDisposed)
            {
                base.OnSelectedIndexChanged(e);
                return;
            }

            // Prevent scrollbar flicker by disabling redraw
            PI.SendMessage(Handle, PI.SETREDRAW, (IntPtr)0, IntPtr.Zero);
            BeginUpdate();
            try
            {
                // Let base update selection and possibly auto-scroll
                base.OnSelectedIndexChanged(e);
                // Only restore scroll if we clicked on a visible item
                if (_preClickTopIndex >= 0)
                {
                    TopIndex = Math.Min(_preClickTopIndex, Items.Count - 1);
                }
            }
            finally
            {
                EndUpdate();
                // Re-enable redraw and repaint
                PI.SendMessage(Handle, PI.SETREDRAW, (IntPtr)1, IntPtr.Zero);
                Invalidate();
            }
        }

        private void OnInternalListBoxMouseDown(object? sender, MouseEventArgs e)
        {
            // Only capture scroll position if the clicked item is already visible
            int index = IndexFromPoint(e.Location);
            if (index >= 0 && index < Items.Count)
            {
                // Check if the item is already visible
                int visibleItems = ClientSize.Height / ItemHeight;
                int bottomVisibleIndex = TopIndex + visibleItems - 1;

                if (index >= TopIndex && index <= bottomVisibleIndex)
                {
                    _preClickTopIndex = TopIndex;
                }
                else
                {
                    // For non-visible items, don't capture - let normal scrolling happen
                    _preClickTopIndex = -1;
                }
            }
        }
    }
    #endregion

    #region Instance Fields

    private readonly PaletteTripleOverride _overrideNormal;
    private readonly PaletteTripleOverride _overrideTracking;
    private readonly PaletteTripleOverride _overridePressed;
    private readonly PaletteTripleOverride _overrideCheckedNormal;
    private readonly PaletteTripleOverride _overrideCheckedTracking;
    private readonly PaletteTripleOverride _overrideCheckedPressed;
    private readonly ViewLayoutDocker _drawDockerInner;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly ViewDrawButton _drawButton;
    private readonly InternalListBox _listBox;
    private readonly FixedContentValue? _contentValues;
    private bool? _fixedActive;
    private ButtonStyle _style;
    private readonly IntPtr _screenDC;
    private int[] _lastSelectedColl;
    private int _lastSelectedIndex;
    private bool _mouseOver;
    private bool _alwaysActive;
    private bool _forcedLayout;
    private bool _trackingMouseEnter;
    // Captures the scroll position before a click/selection change
    private int _preClickTopIndex;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the DataSource property changes.
    /// </summary>
    [Description(@"Occurs when the value of the DataSource property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? DataSourceChanged;

    /// <summary>
    /// Occurs when the value of the DisplayMember property changes.
    /// </summary>
    [Description(@"Occurs when the value of the DisplayMember property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? DisplayMemberChanged;

    /// <summary>
    /// Occurs when the property of a control is bound to a data value.
    /// </summary>
    [Description(@"Occurs when the property of a control is bound to a data value.")]
    [Category(@"Property Changed")]
    public event EventHandler? Format;

    /// <summary>
    /// Occurs when the value of the FormatInfo property changes.
    /// </summary>
    [Description(@"Occurs when the value of the FormatInfo property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? FormatInfoChanged;

    /// <summary>
    /// Occurs when the value of the FormatString property changes.
    /// </summary>
    [Description(@"Occurs when the value of the FormatString property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? FormatStringChanged;

    /// <summary>
    /// Occurs when the value of the FormattingEnabled property changes.
    /// </summary>
    [Description(@"Occurs when the value of the FormattingEnabled property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? FormattingEnabledChanged;

    /// <summary>
    /// Occurs when the value of the SelectedValue property changes.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedValue property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? SelectedValueChanged;

    /// <summary>
    /// Occurs when the value of the SelectedIndex property changes.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedIndex property changes.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when the value of the ValueMember property changes.
    /// </summary>
    [Description(@"Occurs when the value of the ValueMember property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? ValueMemberChanged;

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
    /// Occurs when the value of the MouseClick property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? PaddingChanged;

    /// <summary>
    /// Occurs when the value of the MouseClick property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event PaintEventHandler? Paint;

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
    /// Occurs when [draw item].
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when an item needs to be Drawn.")]
    public event DrawItemEventHandler? DrawItem;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonListBox class.
    /// </summary>
    public KryptonListBox()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // Cannot select this control, only the child ListBox and does not generate a click event
        SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

        // Default fields
        _alwaysActive = true;
        _lastSelectedIndex = -1;
        _style = ButtonStyle.ListItem;
        base.Padding = new Padding(1);

        // Create the palette storage
        StateCommon = new PaletteListStateRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, NeedPaintDelegate);
        OverrideFocus = new PaletteListItemTripleRedirect(Redirector, PaletteBackStyle.ButtonListItem, PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, NeedPaintDelegate);
        StateDisabled = new PaletteListState(StateCommon, NeedPaintDelegate);
        StateActive = new PaletteDouble(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteListState(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StatePressed = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StateCheckedNormal = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StateCheckedTracking = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
        StateCheckedPressed = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);

        // Create the override handling classes
        _overrideNormal = new PaletteTripleOverride(OverrideFocus.Item, StateNormal.Item, PaletteState.FocusOverride);
        _overrideTracking = new PaletteTripleOverride(OverrideFocus.Item, StateTracking.Item, PaletteState.FocusOverride);
        _overridePressed = new PaletteTripleOverride(OverrideFocus.Item, StatePressed.Item, PaletteState.FocusOverride);
        _overrideCheckedNormal = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedNormal.Item, PaletteState.FocusOverride);
        _overrideCheckedTracking = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedTracking.Item, PaletteState.FocusOverride);
        _overrideCheckedPressed = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedPressed.Item, PaletteState.FocusOverride);

        // Create the draw element for owner drawing individual items
        _contentValues = new FixedContentValue();
        _drawButton = new ViewDrawButton(StateDisabled.Item, _overrideNormal,
            _overrideTracking, _overridePressed,
            _overrideCheckedNormal, _overrideCheckedTracking,
            _overrideCheckedPressed,
            new PaletteMetricRedirect(Redirector),
            _contentValues, VisualOrientation.Top, false);

        // Create the internal list box used for containing content
        _listBox = new InternalListBox(this);
        _listBox.DrawItem += OnListBoxDrawItem;
        _listBox.DoubleClick += OnDoubleClick;
        _listBox.MeasureItem += OnListBoxMeasureItem;
        _listBox.TrackMouseEnter += OnListBoxMouseChange;
        _listBox.TrackMouseLeave += OnListBoxMouseChange;
        _listBox.DataSourceChanged += OnListBoxDataSourceChanged;
        _listBox.DisplayMemberChanged += OnListBoxDisplayMemberChanged;
        _listBox.ValueMemberChanged += OnListBoxValueMemberChanged;
        _listBox.SelectedIndexChanged += OnListBoxSelectedIndexChanged;
        _listBox.SelectedValueChanged += OnListBoxSelectedValueChanged;
        _listBox.Format += OnListBoxFormat;
        _listBox.FormatInfoChanged += OnListBoxFormatInfoChanged;
        _listBox.FormatStringChanged += OnListBoxFormatStringChanged;
        _listBox.FormattingEnabledChanged += OnListBoxFormattingEnabledChanged;
        _listBox.GotFocus += OnListBoxGotFocus;
        _listBox.LostFocus += OnListBoxLostFocus;
        _listBox.MouseDoubleClick += OnMouseDoubleClick;
        _listBox.KeyDown += OnListBoxKeyDown;
        _listBox.KeyUp += OnListBoxKeyUp;
        _listBox.KeyPress += OnListBoxKeyPress;
        _listBox.PreviewKeyDown += OnListBoxPreviewKeyDown;
        _listBox.Validating += OnListBoxValidating;
        _listBox.Validated += OnListBoxValidated;
        _listBox.Click += OnListBoxClick;  // SKC: make sure that the default click is also routed.

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_listBox)
        {
            DisplayPadding = new Padding(1)
        };

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

        // We need to create and cache a device context compatible with the display
        _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

        // Add list box to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_listBox);
    }

    private void OnListBoxClick(object? sender, EventArgs e) =>
        // ReSharper disable RedundantBaseQualifier
        base.OnClick(e);
    // ReSharper restore RedundantBaseQualifier

    /// <summary>
    /// Releases all resources used by the Control.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (_screenDC != IntPtr.Zero)
        {
            PI.DeleteDC(_screenDC);
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the contained ListBox instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public ListBox ListBox => _listBox;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => ListBox;

    /// <summary>
    /// Gets or sets the text for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text
    {
        // Control.Text can take null but will always return an empty string when the input was null
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font
    {
        // base.Font will always return a Font
        // base can take null as a value

        get => base.Font;
        set => base.Font = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets and sets the internal padding space.
    /// </summary>
    [DefaultValue(typeof(Padding), "1,1,1,1")]
    public new Padding Padding
    {
        get => base.Padding;

        set
        {
            base.Padding = value;
            _layoutFill.DisplayPadding = value;
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Gets or sets the zero-based index of the currently selected item in a KryptonListBox.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => _listBox.SelectedIndex;
        set => _listBox.SelectedIndex = value;
    }

    /// <summary>
    /// Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.
    /// </summary>
    [Category(@"Data")]
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    public object? SelectedValue
    {
        get => _listBox.SelectedValue;
        set => _listBox.SelectedValue = value!;
    }

    /// <summary>
    /// Gets a collection that contains the zero-based indexes of all currently selected items in the KryptonListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListBox.SelectedIndexCollection SelectedIndices => _listBox.SelectedIndices;

    /// <summary>
    /// Gets or sets the currently selected item in the KryptonListBox.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem
    {
        get => _listBox.SelectedItem;
        set => _listBox.SelectedItem = value;
    }

    /// <summary>
    /// Gets a collection containing the currently selected items in the KryptonListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListBox.SelectedObjectCollection SelectedItems => _listBox.SelectedItems;

    /// <summary>
    /// Gets or sets the index of the first visible item in the KryptonListBox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TopIndex
    {
        get => _listBox.TopIndex;
        set => _listBox.TopIndex = value;
    }

    /// <summary>
    /// Gets and sets the item style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Item style.")]
    public ButtonStyle ItemStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                StateCommon.Item.SetStyles(_style);
                OverrideFocus.Item.SetStyles(_style);
                _listBox.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeItemStyle() => ItemStyle != ButtonStyle.ListItem;

    private void ResetItemStyle() => ItemStyle = ButtonStyle.ListItem;

    /// <summary>
    /// Gets or sets the width by which the horizontal scroll bar of a KryptonListBox can scroll.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The width, in pixels, by which a list box can be scrolled horizontally. Only valid HorizontalScrollbar is true.")]
    [Localizable(true)]
    [DefaultValue(0)]
    public virtual int HorizontalExtent
    {
        get => _listBox.HorizontalExtent;
        set => _listBox.HorizontalExtent = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a horizontal scroll bar is Displayed in the control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the KryptonListBox will display a horizontal scrollbar for items beyond the right edge of the KryptonListBox.")]
    [Localizable(true)]
    [DefaultValue(false)]
    public virtual bool HorizontalScrollbar
    {
        get => _listBox.HorizontalScrollbar;
        set => _listBox.HorizontalScrollbar = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the vertical scroll bar is shown at all times.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the list box should always have a scroll bar present, regardless of how many items are present.")]
    [Localizable(true)]
    [DefaultValue(false)]
    public virtual bool ScrollAlwaysVisible
    {
        get => _listBox.ScrollAlwaysVisible;
        set => _listBox.ScrollAlwaysVisible = value;
    }

    /// <summary>
    /// Gets or sets the selection mode of the KryptonListBox control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the list box is to be single-select, multi-select or not selectable.")]
    [DefaultValue(SelectionMode.One)]
    public virtual SelectionMode SelectionMode
    {
        get => _listBox.SelectionMode;
        set => _listBox.SelectionMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the items in the KryptonListBox are sorted alphabetically.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Controls whether the list is sorted.")]
    [DefaultValue(false)]
    public virtual bool Sorted
    {
        get => _listBox.Sorted;
        set => _listBox.Sorted = value;
    }

    /// <summary>
    /// Gets and sets the value member.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to use as the actual value of the items in the control.")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue("")]
    public virtual string ValueMember
    {
        get => _listBox.ValueMember;
        set => _listBox.ValueMember = value;
    }

    /// <summary>
    /// Gets and sets the list that this control will use to gets its items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the list that this control will use to gets its items.")]
    [AttributeProvider(typeof(IListSource))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    public virtual object? DataSource
    {
        get => _listBox.DataSource;
        set => _listBox.DataSource = value;
    }

    /// <summary>
    /// Gets and sets the property to display for the items in this control.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to display for the items in this control.")]
    [TypeConverter(@"System.Windows.Forms.Design.DataMemberFieldConverter")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue("")]
    public virtual string DisplayMember
    {
        get => _listBox.DisplayMember;
        set => _listBox.DisplayMember = value;
    }

    /// <summary>
    /// Gets the items of the KryptonListBox.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The items in the KryptonListBox.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [Localizable(true)]
    public virtual ListBox.ObjectCollection Items => _listBox.Items;

    /// <summary>
    /// Gets or sets the format specifier characters that indicate how a value is to be Displayed.
    /// </summary>
    [Description(@"The format specifier characters that indicate how a value is to be Displayed.")]
    [Editor(@"System.Windows.Forms.Design.FormatStringEditor", typeof(UITypeEditor))]
    [MergableProperty(false)]
    [DefaultValue("")]
    public string FormatString
    {
        get => _listBox.FormatString;
        set => _listBox.FormatString = value;
    }

    /// <summary>
    /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.
    /// </summary>
    [Description(@"If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.")]
    [DefaultValue(false)]
    public bool FormattingEnabled
    {
        get => _listBox.FormattingEnabled;
        set => _listBox.FormattingEnabled = value;
    }

    /// <summary>
    /// Gets and sets the background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Style used to draw the background.")]
    public PaletteBackStyle BackStyle
    {
        get => StateCommon.BackStyle;

        set
        {
            if (StateCommon.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                _listBox.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackStyle() => BackStyle != PaletteBackStyle.InputControlStandalone;

    private void ResetBackStyle() => BackStyle = PaletteBackStyle.InputControlStandalone;

    /// <summary>
    /// Gets and sets the border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Style used to draw the border.")]
    public PaletteBorderStyle BorderStyle
    {
        get => StateCommon.BorderStyle;

        set
        {
            if (StateCommon.BorderStyle != value)
            {
                StateCommon.BorderStyle = value;
                _listBox.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBorderStyle() => BorderStyle != PaletteBorderStyle.InputControlStandalone;

    private void ResetBorderStyle() => BorderStyle = PaletteBorderStyle.InputControlStandalone;

    /// <summary>
    /// Gets access to the item appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets access to the common appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListStateRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListState StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListState StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble StateActive { get; }

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the normal checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteListItemTriple StateCheckedPressed { get; }

    private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

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
    /// Unselects all items in the KryptonListBox.
    /// </summary>
    public void ClearSelected() => _listBox.ClearSelected();

    /// <summary>
    /// Finds the first item in the list box that starts with the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindString(string str) => _listBox.FindString(str);

    /// <summary>
    /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindString(string str, int startIndex) => _listBox.FindString(str, startIndex);

    /// <summary>
    /// Finds the first item in the list box that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindStringExact(string str) => _listBox.FindStringExact(str);

    /// <summary>
    /// Finds the first item after the specified index that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindStringExact(string str, int startIndex) => _listBox.FindStringExact(str, startIndex);

    /// <summary>
    /// Returns the height of an item in the KryptonListBox.
    /// </summary>
    /// <param name="index">The index of the item to return the height of.</param>
    /// <returns>The height, in pixels, of the item at the specified index.</returns>
    public int GetItemHeight(int index) => _listBox.GetItemHeight(index);

    /// <summary>
    /// Returns the bounding rectangle for an item in the KryptonListBox.
    /// </summary>
    /// <param name="index">The zero-based index of item whose bounding rectangle you want to return.</param>
    /// <returns>A Rectangle that represents the bounding rectangle for the specified item.</returns>
    public Rectangle GetItemRectangle(int index) => _listBox.GetItemRectangle(index);

    /// <summary>
    /// Returns a value indicating whether the specified item is selected.
    /// </summary>
    /// <param name="index">The zero-based index of the item that determines whether it is selected.</param>
    /// <returns>true if the specified item is currently selected in the KryptonListBox; otherwise, false.</returns>
    public bool GetSelected(int index) => _listBox.GetSelected(index);

    /// <summary>
    /// Returns the zero-based index of the item at the specified coordinates.
    /// </summary>
    /// <param name="p">A Point object containing the coordinates used to obtain the item index.</param>
    /// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
    public int IndexFromPoint(Point p) => _listBox.IndexFromPoint(p);

    /// <summary>
    /// Returns the zero-based index of the item at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the location to search.</param>
    /// <param name="y">The y-coordinate of the location to search.</param>
    /// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
    public int IndexFromPoint(int x, int y) => _listBox.IndexFromPoint(x, y);

    /// <summary>
    /// Selects or clears the selection for the specified item in a KryptonListBox.
    /// </summary>
    /// <param name="index">The zero-based index of the item in a KryptonListBox to select or clear the selection for.</param>
    /// <param name="value">true to select the specified item; otherwise, false.</param>
    public void SetSelected(int index, bool value) => _listBox.SetSelected(index, value);

    /// <summary>
    /// Returns the text representation of the specified item.
    /// </summary>
    /// <param name="item">The object from which to get the contents to display.</param>
    /// <returns>If the DisplayMember property is not specified, the value returned by GetItemText is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
    public string? GetItemText(object? item) => _listBox.GetItemText(item);

    /// <summary>
    /// Maintains performance while items are added to the ListBox one at a time by preventing the control from drawing until the EndUpdate method is called.
    /// </summary>
    public void BeginUpdate() => _listBox.BeginUpdate();

    /// <summary>
    /// Resumes painting the ListBox control after painting is suspended by the BeginUpdate method.
    /// </summary>
    public void EndUpdate() => _listBox.EndUpdate();

    /// <summary>
    /// Sets the fixed state of the control.
    /// </summary>
    /// <param name="active">Should the control be fixed as active.</param>
    public void SetFixedState(bool active) => _fixedActive = active;

    /// <summary>
    /// Gets a value indicating if the input control is active.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsActive => _fixedActive ?? DesignMode || AlwaysActive || ContainsFocus || _mouseOver || _listBox.MouseOver;

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => ListBox.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => ListBox.Select();
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
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the DataSourceChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDataSourceChanged(EventArgs e) => DataSourceChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the DisplayMemberChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDisplayMemberChanged(EventArgs e) => DisplayMemberChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ValueMemberChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueMemberChanged(EventArgs e) => ValueMemberChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedValueChanged(EventArgs e) => SelectedValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the Format event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormat(ListControlConvertEventArgs e) => Format?.Invoke(this, e);

    /// <summary>
    /// Raises the FormatInfoChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormatInfoChanged(EventArgs e) => FormatInfoChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the FormatStringChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormatStringChanged(EventArgs e) => FormatStringChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the FormattingEnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormattingEnabledChanged(EventArgs e) => FormattingEnabledChanged?.Invoke(this, e);
    #endregion

    #region Protected Override
    /// <summary>
    /// Creates a new instance of the control collection for the KryptonListBox.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        _listBox.BeginUpdate();
        try
        {
            // Preserve scroll and selected index to avoid shifting when theme changes
            int oldTopIndex = _listBox.TopIndex;
            int oldSelectedIndex = _listBox.SelectedIndex;
            _listBox.Recreate();
            // Restore scroll position and selection
            _listBox.TopIndex = Math.Min(oldTopIndex, _listBox.Items.Count - 1);
            if ((oldSelectedIndex >= 0) &&
                (oldSelectedIndex < _listBox.Items.Count))
            {
                _listBox.SelectedIndex = oldSelectedIndex;
            }
            _listBox.RefreshItemSizes();
            _listBox.Invalidate();
        }
        finally
        {
            _listBox.EndUpdate();
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
        _listBox.RefreshItemSizes();
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Change in enabled state requires a layout and repaint
        UpdateStateAndPalettes();
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
    /// Raises the PaddingChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaddingChanged(EventArgs e) => PaddingChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        ListBox.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        ListBox.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">An PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        Paint?.Invoke(this, e!);

        base.OnPaint(e);
    }

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

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
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated && !e.NeedLayout)
        {
            _listBox.Invalidate();
        }
        else
        {
            ForceControlLayout();
        }

        // Update palette to reflect latest state
        UpdateStateAndPalettes();
        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);

        // Only use layout logic if control is fully initialized or if being forced
        // to allow a relayout or if in design mode.
        if (IsHandleCreated || _forcedLayout || (DesignMode))
        {
            Rectangle fillRect = _layoutFill.FillRect;
            _listBox.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
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
        _listBox.Invalidate();
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
        _listBox.Invalidate();
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(120, 96);

    #endregion

    #region Implementation
    private void UpdateStateAndPalettes()
    {
        if (!IsDisposed)
        {
            // Get the correct palette settings to use
            IPaletteDouble doubleState = GetDoubleState();
            _listBox.ViewDrawPanel.SetPalettes(doubleState.PaletteBack);
            _drawDockerOuter.SetPalettes(doubleState.PaletteBack, doubleState.PaletteBorder!);
            _drawDockerOuter.Enabled = Enabled;

            // Find the new state of the main view element
            PaletteState state = Enabled ? (IsActive ? PaletteState.Tracking : PaletteState.Normal) : PaletteState.Disabled;

            _listBox.ViewDrawPanel.ElementState = state;
            _drawDockerOuter.ElementState = state;
        }
    }

    private IPaletteDouble GetDoubleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

    private void OnListBoxDrawItem(object? sender, DrawItemEventArgs e)
    {
        // We cannot do anything with an invalid index
        if (e.Index < 0)
        {
            return;
        }

        // Update our content object with values from the list item
        UpdateContentFromItemIndex(e.Index);

        // By default, the button is in the normal state
        var buttonState = PaletteState.Normal;

        // Is this item disabled
        if ((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)
        {
            buttonState = PaletteState.Disabled;
        }
        else
        {
            // Is the mouse over the item about to be drawn
            var mouseOver = (e.Index >= 0) && (e.Index == _listBox.MouseIndex);

            // If selected then show as a checked item
            if (((e.State & DrawItemState.Selected) == DrawItemState.Selected) &&
                (SelectionMode != SelectionMode.None))
            {
                _drawButton.Checked = true;
                buttonState = mouseOver ? PaletteState.CheckedTracking : PaletteState.CheckedNormal;
            }
            else
            {
                _drawButton.Checked = false;
                if (mouseOver)
                {
                    buttonState = PaletteState.Tracking;
                }
            }

            // Do we need to show item as having the focus
            var hasFocus = ((e.State & DrawItemState.Focus) == DrawItemState.Focus) &&
                           ((e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect);

            _overrideNormal.Apply = hasFocus;
            _overrideTracking.Apply = hasFocus;
            _overridePressed.Apply = hasFocus;
            _overrideCheckedTracking.Apply = hasFocus;
            _overrideCheckedNormal.Apply = hasFocus;
            _overrideCheckedPressed.Apply = hasFocus;
        }

        // Update view element state
        _drawButton.ElementState = buttonState;

        // Grab the raw device context for the graphics instance
        var hdc = e.Graphics.GetHdc();

        try
        {
            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
            var hBitmap = PI.CreateCompatibleBitmap(hdc, e.Bounds.Right, e.Bounds.Bottom);

            // If we managed to get a compatible bitmap
            if (hBitmap != IntPtr.Zero)
            {
                try
                {
                    // Must use the screen device context for the bitmap when drawing into the
                    // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                    PI.SelectObject(_screenDC, hBitmap);

                    // Easier to draw using a graphics instance than a DC!
                    using Graphics g = Graphics.FromHdc(_screenDC);
                    // Ask the view element to layout in given space, needs this before a render call
                    using (var context = new ViewLayoutContext(this, Renderer))
                    {
                        context.DisplayRectangle = e.Bounds;
                        _listBox.ViewDrawPanel.Layout(context);
                        _drawButton.Layout(context);
                    }

                    // Ask the view element to actually draw
                    using (var context = new RenderContext(this, g, e.Bounds, Renderer))
                    {
                        _listBox.ViewDrawPanel.Render(context);
                        _drawButton.Render(context);
                    }

                    // Now blit from the bitmap from the screen to the real dc
                    PI.BitBlt(hdc, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, _screenDC, e.Bounds.X, e.Bounds.Y, PI.SRCCOPY);
                }
                finally
                {
                    // Delete the temporary bitmap
                    PI.DeleteObject(hBitmap);
                }
            }
        }
        finally
        {
            // Must reserve the GetHdc() call before
            e.Graphics.ReleaseHdc();
        }
    }

    private void OnListBoxMeasureItem(object? sender, MeasureItemEventArgs e)
    {
        UpdateContentFromItemIndex(e.Index);

        // Ask the view element to layout in given space, needs this before a render call
        using var context = new ViewLayoutContext(this, Renderer);
        Size size = _drawButton.GetPreferredSize(context);
        e.ItemWidth = size.Width;
        e.ItemHeight = size.Height;
    }

    private void UpdateContentFromItemIndex(int index)
    {

        // If the object exposes the rich interface then use is...
        if (Items[index] is IContentValues itemValues)
        {
            _contentValues!.ShortText = itemValues.GetShortText();
            _contentValues.LongText = itemValues.GetLongText();
            _contentValues.Image = itemValues.GetImage(PaletteState.Normal);
            _contentValues.ImageTransparentColor = itemValues.GetImageTransparentColor(PaletteState.Normal);
        }
        else
        {
            // Get the text string for the item
            _contentValues!.ShortText = _listBox.GetItemText(Items[index]);
            _contentValues.LongText = null;
            _contentValues.Image = null;
            _contentValues.ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        }
    }

    private void OnListBoxDataSourceChanged(object? sender, EventArgs e) => OnDataSourceChanged(e);

    private void OnListBoxDisplayMemberChanged(object? sender, EventArgs e) => OnDisplayMemberChanged(e);

    private void OnListBoxValueMemberChanged(object? sender, EventArgs e) => OnValueMemberChanged(e);

    private void OnListBoxSelectedIndexChanged(object? sender, EventArgs e)
    {
        switch (_listBox.SelectionMode)
        {
            case SelectionMode.One:
                // Restore scroll to pre-click position
                int oldTopIndex = _preClickTopIndex;
                if (_lastSelectedIndex != _listBox.SelectedIndex)
                {
                    _lastSelectedIndex = _listBox.SelectedIndex;
                    UpdateStateAndPalettes();
                    _listBox.Invalidate();
                    // Only restore scroll if we clicked on a visible item
                    if (oldTopIndex >= 0)
                    {
                        _listBox.TopIndex = Math.Min(oldTopIndex, _listBox.Items.Count - 1);
                    }
                    OnSelectedIndexChanged(e);
                }
                break;
            case SelectionMode.MultiSimple:
            case SelectionMode.MultiExtended:
                if (SelectedIndicesChanged(_lastSelectedColl, _listBox.SelectedIndices))
                {
                    _lastSelectedColl = new int[_listBox.SelectedIndices.Count];
                    _listBox.SelectedIndices.CopyTo(_lastSelectedColl, 0);

                    UpdateStateAndPalettes();
                    _listBox.Invalidate();
                    OnSelectedIndexChanged(e);
                }
                break;
        }
    }

    private bool SelectedIndicesChanged(int[]? left,
        ListBox.SelectedIndexCollection? right)
    {
        // First time around the left can be null
        if ((left == null) && (right != null))
        {
            return true;
        }

        // Quickest check is to see if they have different number of entries
        if (left!.Length != right!.Count)
        {
            return true;
        }

        // Do it the slow way, check each entry and assume they are in numerical order
        return left.Where((t, i) => t != right[i]).Any();
    }

    private void OnListBoxSelectedValueChanged(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listBox.Invalidate();
        OnSelectedValueChanged(e);
    }

    private void OnListBoxFormat(object? sender, ListControlConvertEventArgs e) => OnFormat(e);

    private void OnListBoxFormatInfoChanged(object? sender, EventArgs e) => OnFormatInfoChanged(e);

    private void OnListBoxFormatStringChanged(object? sender, EventArgs e) => OnFormatStringChanged(e);

    private void OnListBoxFormattingEnabledChanged(object? sender, EventArgs e) => OnFormattingEnabledChanged(e);

    private void OnListBoxGotFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listBox.Invalidate();
        PerformNeedPaint(true);
        OnGotFocus(e);
    }

    private void OnListBoxLostFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listBox.Invalidate();
        PerformNeedPaint(true);
        OnLostFocus(e);
    }

    private void OnListBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnListBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnListBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnListBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnListBoxValidated(object? sender, EventArgs e) => OnValidated(e);

    private void OnListBoxValidating(object? sender, CancelEventArgs e) => OnValidating(e);

    private void OnListBoxMouseChange(object? sender, EventArgs e)
    {
        // Change in tracking state?
        if (_listBox.MouseOver != _trackingMouseEnter)
        {
            _trackingMouseEnter = _listBox.MouseOver;

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

    private void OnDoubleClick(object? sender, EventArgs e) => base.OnDoubleClick(e);

    private void OnMouseDoubleClick(object? sender, MouseEventArgs e) => base.OnMouseDoubleClick(e);

    #endregion
}