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

namespace Krypton.Toolkit;

/// <summary>
/// Provide a ComboBox with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[LookupBindingProperties(nameof(DataSource), nameof(DisplayMember), nameof(ValueMember), nameof(SelectedValue))]
[Designer(typeof(KryptonComboBoxDesigner))]
//[Designer(@"Krypton.Toolkit.KryptonContextMenuDesigner, Krypton.Toolkit")]
[DesignerCategory(@"code")]
[Description(@"Displays an editable textbox with a drop-down list of permitted values.")]
public class KryptonComboBox : VisualControlBase,
    IContainedInputControl,
    ISupportInitializeNotification
{
    #region Classes
    private class InternalPanel : Panel
    {
        #region Instance Fields
        private readonly KryptonComboBox _kryptonComboBox;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the InternalPanel class.
        /// </summary>
        /// <param name="kryptonComboBox">Reference to owning control.</param>
        public InternalPanel(KryptonComboBox kryptonComboBox) => _kryptonComboBox = kryptonComboBox;

        #endregion

        #region Public
        /// <summary>
        /// Retrieves the size of a rectangular area into which a control can be fitted.
        /// </summary>
        public override Size GetPreferredSize(Size proposedSize)
        {
            var maxSize = Size.Empty;

            // Find the largest size of any child control
            foreach (Control c in Controls)
            {
                try
                {
                    Size cSize = c.GetPreferredSize(proposedSize);
                    maxSize.Width = Math.Max(maxSize.Width, cSize.Width);
                    maxSize.Height = Math.Max(maxSize.Height, cSize.Height);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    // For some reason when the font `simSun` is used the control.GetPreferredSize throws it's toys out of the pram
                    maxSize = c.Size;
                }
            }

            // The panel needs to be 2 above and 2 below bigger than the height of an item
            return new Size(maxSize.Width - 3, _kryptonComboBox._comboBox.ItemHeight + 4);
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
                    if (_kryptonComboBox.InTransparentDesignMode)
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
        #endregion
    }

    private class InternalComboBox : ComboBox, IContentValues
    {
        #region Instance Fields
        private readonly KryptonComboBox _kryptonComboBox;
        private PaletteTripleToPalette _palette;
        private ViewDrawButton? _viewButton;
        private bool? _appThemed;
        private bool _mouseTracking;
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
        /// Initialize a new instance of the InternalComboBox class.
        /// </summary>
        /// <param name="kryptonComboBox">Reference to owning control.</param>
        public InternalComboBox(KryptonComboBox kryptonComboBox)
        {
            // Remember incoming reference
            _kryptonComboBox = kryptonComboBox;

            // Remove from view until size for the first time by the Krypton control
            UpdateItemHeight();  // Ensure ItemHeight is set properly; see #1677
            DropDownHeight = 200;
            //DrawMode = DrawMode.OwnerDrawFixed; // #20 fix, but this causes other problems; see #578
            DrawMode = DrawMode.OwnerDrawVariable;
            SetStyle(/*ControlStyles.UserPaint | */ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets if the combo box is currently dropped.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Dropped { get; set; }

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
        /// Reset the app themed setting so it is retested when next required.
        /// </summary>
        public void ClearAppThemed() => _appThemed = null;

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public virtual string GetShortText() => string.Empty;

        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public virtual Image? GetImage(PaletteState state) => null;

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public virtual string GetLongText() => string.Empty;

        #endregion

        #region Protected
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Do not forward, to allow the correct Background for disabled state
            // See https://github.com/Krypton-Suite/Standard-Toolkit/issues/662
        }

        /// <summary>
        /// Raises the FontChanged event.
        /// </summary>
        /// <param name="e">Contains the event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            UpdateItemHeight();
            base.OnFontChanged(e);
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
                    if (_kryptonComboBox.InTransparentDesignMode)
                    {
                        m.Result = (IntPtr)PI.HT.TRANSPARENT;
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                    break;
                case PI.WM_.MOUSELEAVE:
                {
                    // Mouse is not over the control
                    MouseOver = false;
                    _mouseTracking = false;
                    _kryptonComboBox.PerformNeedPaint(false);
                    Invalidate();
                }
                    break;
                case PI.WM_.MOUSEMOVE:
                {
                    // Mouse is over the control
                    if (!MouseOver)
                    {
                        MouseOver = true;
                        _kryptonComboBox.PerformNeedPaint(false);
                        Invalidate();
                    }

                    // Grab the client area of the control
                    PI.GetClientRect(Handle, out PI.RECT rect);

                    // Get the constant used to crack open the display
                    var dropDownWidth = SystemInformation.VerticalScrollBarWidth;
                    Size borderSize = SystemInformation.BorderSize;

                    // Create rect for the text area
                    rect.left += borderSize.Width;
                    rect.right -= borderSize.Width + dropDownWidth;
                    rect.top += borderSize.Height;
                    rect.bottom -= borderSize.Height;

                    // Create rectangle that represents the drop-down button
                    var dropRect = new Rectangle(rect.right + 2, rect.top, dropDownWidth - 2,
                        rect.bottom - rect.top);

                    // Extract the point in client coordinates
                    var clientPoint = new Point((int)m.LParam);
                    var mouseTracking = dropRect.Contains(clientPoint);
                    if (mouseTracking != _mouseTracking)
                    {
                        _mouseTracking = mouseTracking;
                        _kryptonComboBox.PerformNeedPaint(false);
                        Invalidate();
                    }
                }
                    break;
                case PI.WM_.PRINTCLIENT:
                case PI.WM_.PAINT:
                {
                    if (!string.IsNullOrWhiteSpace(_kryptonComboBox.CueHint.CueHintText))
                    {
                        PI.SendMessage(Handle, PI.CB_SETCUEBANNER, IntPtr.Zero, _kryptonComboBox.CueHint.CueHintText);
                    }
                    var ps = new PI.PAINTSTRUCT();

                    // Do we need to BeginPaint or just take the given HDC?
                    var hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

                    //////////////////////////////////////////////////////
                    // Following removed to allow the Draw to always happen, to allow centering etc
                    //if (_kryptonComboBox.Enabled && _kryptonComboBox.DropDownStyle == ComboBoxStyle.DropDown)
                    //{
                    // Let base implementation draw the actual text area
                    //if (m.WParam == IntPtr.Zero)
                    //{
                    //    m.WParam = hdc;
                    //    DefWndProc(ref m);
                    //    m.WParam = IntPtr.Zero;
                    //}
                    //else
                    //{
                    //    DefWndProc(ref m);
                    //}
                    //}

                    // Paint the entire area in the background color
                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        // Grab the client area of the control
                        PI.GetClientRect(Handle, out PI.RECT rect);

                        PaletteState state = _kryptonComboBox.Enabled
                            ? _kryptonComboBox.IsActive
                                ? PaletteState.Tracking
                                : PaletteState.Normal
                            : PaletteState.Disabled;
                        PaletteInputControlTripleStates states = _kryptonComboBox.GetComboBoxTripleState();

                        // Drawn entire client area in the background color
                        using var backBrush = new SolidBrush(states.PaletteBack.GetBackColor1(state));
                        g.FillRectangle(backBrush, new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top));

                        // Get the constant used to crack open the display
                        var dropDownWidth = SystemInformation.VerticalScrollBarWidth;
                        Size borderSize = SystemInformation.BorderSize;

                        // Create rect for the text area
                        rect.top += borderSize.Height;
                        rect.bottom -= borderSize.Height;

                        // Create rectangle that represents the drop-down button
                        Rectangle dropRect;

                        // Update text and drop-down rects dependent on the right to left setting
                        if (_kryptonComboBox.RightToLeft == RightToLeft.Yes)
                        {
                            dropRect = new Rectangle(rect.left + borderSize.Width + 1, rect.top + 1, dropDownWidth - 2, rect.bottom - rect.top - 2);
                            rect.left += borderSize.Width + dropDownWidth;
                            rect.right -= borderSize.Width;
                        }
                        else
                        {
                            rect.left += borderSize.Width;
                            rect.right -= borderSize.Width + dropDownWidth;
                            dropRect = new Rectangle(rect.right + 1, rect.top + 1, dropDownWidth - 2, rect.bottom - rect.top - 2);
                        }

                        // Exclude border from being drawn, we need to take off another 2 pixels from all edges
                        PI.IntersectClipRect(hdc, rect.left + 2, rect.top, rect.right - 2, rect.bottom);
                        var displayText = _kryptonComboBox.Text;
                        if (!string.IsNullOrWhiteSpace(_kryptonComboBox.CueHint.CueHintText)
                            && string.IsNullOrEmpty(displayText)
                           )
                        {
                            // Go perform the drawing of the CueHint
                            _kryptonComboBox.CueHint.PerformPaint(_kryptonComboBox, g, rect, backBrush);
                        }
                        else
                            ////////////////////////////////////////////////////////
                            //// Following commented out, to allow the Draw to always happen even tho the edit box will draw over afterwards
                            //// Draw Over is tracked here
                            ////  https://github.com/Krypton-Suite/Standard-Toolkit/issues/179
                            //// If not enabled or not the dropDown Style then we can draw over the text area
                            ////if (!_kryptonComboBox.Enabled || _kryptonComboBox.DropDownStyle != ComboBoxStyle.DropDown)
                        {
                            using var graphicsHint = new GraphicsTextHint(g, CommonHelper.PaletteTextHintToRenderingHint(states.Content.GetContentShortTextHint(state)));

                            TextFormatFlags flags = TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;

                            // Use the correct prefix setting
                            flags |= TextFormatFlags.NoPrefix;

                            // Do we need to switch drawing direction?
                            if (RightToLeft == RightToLeft.Yes)
                            {
                                flags |= TextFormatFlags.RightToLeft;
                            }

                            switch (states.Content.GetContentShortTextH(state))
                            {
                                case PaletteRelativeAlign.Near:
                                    flags |= TextFormatFlags.Left;
                                    break;
                                case PaletteRelativeAlign.Center:
                                    flags |= TextFormatFlags.HorizontalCenter;
                                    break;
                                case PaletteRelativeAlign.Far:
                                    flags |= TextFormatFlags.Right;
                                    break;
                            }

                            // Draw text using font defined by the control
                            var rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left,
                                rect.bottom - rect.top);
                            rectangle = CommonHelper.ApplyPadding(VisualOrientation.Top, rectangle, states.Content.GetBorderContentPadding(null, state));
                            // Find correct text color
                            Color textColor = states.Content.GetContentShortTextColor1(state);
                            Font? contentShortTextFont = states.Content.GetContentShortTextFont(state);
                            // Find correct background color
                            Color backColor = states.PaletteBack.GetBackColor1(state);

                            // TODO: Replace this with the graphic DrawString to get around some drawing looking Very Poor
                            TextRenderer.DrawText(g,
                                Text, contentShortTextFont,
                                rectangle,
                                textColor, backColor,
                                flags);
                        }

                        // Remove clipping settings
                        PI.SelectClipRgn(hdc, IntPtr.Zero);

                        // Draw the drop-down button
                        DrawDropButton(g, dropRect);
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
                    if (_kryptonComboBox.KryptonContextMenu != null)
                    {
                        // Extract the screen mouse position (if might not actually be provided)
                        var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                        // If keyboard activated, the menu position is centered
                        if (((int)(long)m.LParam) == -1)
                        {
                            mousePt = PointToScreen(new Point(Width / 2, Height / 2));
                        }

                        // Show the context menu
                        _kryptonComboBox.KryptonContextMenu.Show(_kryptonComboBox, mousePt);

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
        [Description(@"Raises the TrackMouseEnter event in the wrapped control.")]
        [Category(@"Mouse")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnTrackMouseEnter(EventArgs e) => TrackMouseEnter?.Invoke(this, e);

        /// <summary>
        /// Raises the TrackMouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTrackMouseLeave(EventArgs e) => TrackMouseLeave?.Invoke(this, e);

        #endregion

        #region Implementation
        private void DrawDropButton(Graphics? g, Rectangle drawRect)
        {
            // Create the view and palette entries first time around
            if (_viewButton == null)
            {
                // Create helper object to get all values from the KryptonComboBox redirector
                _palette = new PaletteTripleToPalette(_kryptonComboBox.Redirector,
                    PaletteBackStyle.ButtonStandalone,
                    PaletteBorderStyle.ButtonStandalone,
                    PaletteContentStyle.ButtonStandalone);

                // Create view element for drawing the actual button
                _viewButton = new ViewDrawButton(_palette, _palette, _palette,
                    _palette, _palette, _palette, _palette,
                    new PaletteMetricRedirect(_kryptonComboBox.Redirector),
                    this, VisualOrientation.Top, false);
            }

            // Update with the latest button style for the drop-down
            _palette.SetStyles(_kryptonComboBox.DropButtonStyle);

            // Find the new state for the button
            PaletteState state;
            if (_kryptonComboBox.Enabled)
            {
                if (Dropped)
                {
                    state = PaletteState.Pressed;
                }
                else if (_mouseTracking)
                {
                    state = PaletteState.Tracking;
                }
                else if (_kryptonComboBox.IsActive || _kryptonComboBox is { IsFixedActive: true, InputControlStyle: InputControlStyle.Standalone })
                {
                    state = _kryptonComboBox.InputControlStyle == InputControlStyle.Standalone ? PaletteState.CheckedNormal : PaletteState.CheckedTracking;
                }
                else
                {
                    state = PaletteState.Normal;
                }
            }
            else
            {
                state = PaletteState.Disabled;
            }

            _viewButton.ElementState = state;

            // Position the button element inside the available drop-down button area
            using (var layoutContext = new ViewLayoutContext(_kryptonComboBox, _kryptonComboBox.Renderer))
            {
                // Define the available area for layout
                layoutContext.DisplayRectangle = drawRect;

                // Perform actual layout inside that area
                _viewButton.Layout(layoutContext);
            }

            // Fill background with the solid background color
            using (var backBrush = new SolidBrush(BackColor))
            {
                g?.FillRectangle(backBrush, drawRect);
            }

            // Ask the element to draw now
            using (var renderContext = new RenderContext(_kryptonComboBox, g, drawRect, _kryptonComboBox.Renderer))
            {
                // Ask the button element to draw itself
                _viewButton.Render(renderContext);

                // Call the renderer directly to draw the drop-down glyph
                renderContext.Renderer!.RenderGlyph.DrawInputControlDropDownGlyph(renderContext,
                    _viewButton.ClientRectangle,
                    _palette.PaletteContent!,
                    state);
            }
        }

        private void UpdateItemHeight()
        {
            // Working on Windows XP or earlier systems?
            //if (_osMajorVersion < 6)
            //{
            //    // Fudge by adding one to the font height, this gives the actual space used by the
            //    // combo box control to draw an individual item in the main part of the control
            //    ItemHeight = Font.Height + 1;
            //}
            //else
            //{
            //    // Vista performs differently depending of the use of themes...
            //    if (IsAppThemed)
            //    {
            //        // Fudge by subtracting 1, which ensure correct sizing of combo box main area
            //        //ItemHeight = Font.Height - 1;

            //        // #1455 - The lower part of the text can become clipped with chars like g, y, p, etc.
            //        // when subtracting one from the font height.
            //        ItemHeight = Font.Height;
            //    }
            //    else
            //    {
            //        // On under Vista without themes is the font height the actual height used
            //        // by the combo box for the space required for drawing the actual item
            //        ItemHeight = Font.Height;
            //    }
            //}

            // #1455 - The lower part of the text can become clipped with chars like g, y, p, etc.
            // when subtracting one from the font height.
            ItemHeight = _osMajorVersion < 6
                ? Font.Height + 1
                : Font.Height;
        }

        private bool IsAppThemed
        {
            get
            {
                try
                {
                    _appThemed ??= PI.IsThemeActive() && PI.IsAppThemed();

                    return _appThemed.Value;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion
    }

    private class SubclassEdit : NativeWindow
    {
        #region Instance Fields
        private readonly KryptonComboBox _kryptonComboBox;
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
        /// Initialize a new instance of the SubclassEdit class.
        /// </summary>
        /// <param name="editControl">Handle of the Edit control to subclass.</param>
        /// <param name="kryptonComboBox">Reference to top level control.</param>
        public SubclassEdit(IntPtr editControl,
            KryptonComboBox kryptonComboBox)
        {
            _kryptonComboBox = kryptonComboBox;

            // Attach ourself to the provided control, subclassing it
            AssignHandle(editControl);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets if the mouse is currently over the combo box.
        /// </summary>
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
        /// Sets the visible state of the control.
        /// </summary>
        public bool Visible
        {
            set => PI.SetWindowPos(Handle,
                IntPtr.Zero,
                0, 0, 0, 0,
                PI.SWP_.NOMOVE | PI.SWP_.NOSIZE |
                (value ? PI.SWP_.SHOWWINDOW : PI.SWP_.HIDEWINDOW)
            );
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
                    if (_kryptonComboBox.InTransparentDesignMode)
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
                    _kryptonComboBox.PerformNeedPaint(false);
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSEMOVE:
                    // Mouse is over the control
                    if (!MouseOver)
                    {
                        var tme = new PI.TRACKMOUSEEVENTS
                        {
                            // This structure needs to know its own size in bytes
                            cbSize = (uint)Marshal.SizeOf<PI.TRACKMOUSEEVENTS>(),
                            dwHoverTime = 100,

                            // We need to know then the mouse leaves the client window area
                            dwFlags = PI.TME_LEAVE,

                            // We want to track our own window
                            hWnd = Handle
                        };

                        // Call Win32 API to start tracking
                        PI.TrackMouseEvent(ref tme);

                        MouseOver = true;
                        _kryptonComboBox.PerformNeedPaint(false);
                    }
                    base.WndProc(ref m);
                    break;
                case PI.WM_.CONTEXTMENU:
                    // Only interested in overriding the behavior when we have a krypton context menu...
                    if (_kryptonComboBox.KryptonContextMenu != null)
                    {
                        // Extract the screen mouse position (if might not actually be provided)
                        var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                        // If keyboard activated, the menu position is centered
                        if (((int)(long)m.LParam) == -1)
                        {
                            PI.GetClientRect(Handle, out PI.RECT clientRect);
                            mousePt = new Point((clientRect.right - clientRect.left) / 2,
                                (clientRect.bottom - clientRect.top) / 2);
                        }

                        // Show the context menu
                        _kryptonComboBox.KryptonContextMenu.Show(_kryptonComboBox, mousePt);

                        // We eat the message!
                        return;
                    }
                    base.WndProc(ref m);
                    break;
                case PI.WM_.DESTROY:
                    // Remove this code as it prevents the auto suggest features from working
                    // _kryptonComboBox.DetachEditControl();
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
    public class ComboBoxButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ComboBoxButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public ComboBoxButtonSpecCollection(KryptonComboBox owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Static Fields
    private static readonly int _osMajorVersion;
    #endregion

    #region Instance Fields

    private VisualPopupToolTip? _visualPopupToolTip;
    private readonly ButtonSpecManagerLayout? _buttonManager;
    private readonly ViewLayoutDocker _drawDockerInner;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly InternalComboBox _comboBox;
    private readonly InternalPanel _comboHolder;
    private SubclassEdit? _subclassEdit;
    private ButtonStyle _dropButtonStyle;
    private PaletteBackStyle _dropBackStyle;
    private InputControlStyle _inputControlStyle;
    private bool? _fixedActive;
    private readonly FixedContentValue? _contentValues;
    private ButtonStyle _style;
    private readonly ViewDrawButton _drawButton;
    private readonly ViewDrawPanel _drawPanel;
    private Padding _layoutPadding;
    private IntPtr _screenDC;
    private readonly ButtonSpecAny _toolTipSpec;
    private VisualPopupToolTip _toolTip;
    private bool _firstTimePaint;
    private bool _trackingMouseEnter;
    private bool _forcedLayout;
    private bool _mouseOver;
    private bool _alwaysActive;
    private int _cachedHeight;
    private int _hoverIndex;

    // #1697 Work-around
    // When changing DropDownStyle while the control is disabled the newly selected style was not applied.
    // _deferredComboBoxStyle caches the selected change which is applied when the control is enabled again.
    private ComboBoxStyle? _deferredComboBoxStyle;
    #endregion

    #region Events
    /// <summary>This event is not relevant for this class.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler DoubleClick
    {
        add => base.DoubleClick += value;
        remove => base.DoubleClick -= value;
    }

    /// <summary>This event is not relevant for this class.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event MouseEventHandler MouseDoubleClick
    {
        add => base.MouseDoubleClick += value;
        remove => base.MouseDoubleClick -= value;
    }

    /// <summary>
    /// Occurs when [draw item].
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when an item needs to be Drawn.")]
    public event DrawItemEventHandler? DrawItem;

    /// <summary>
    /// Occurs when the control is initialized.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the control has been fully initialized.")]
    public event EventHandler? Initialized;

    /// <summary>
    /// Occurs when the drop-down portion of the KryptonComboBox is shown.
    /// </summary>
    [Description(@"Occurs when the drop-down portion of the KryptonComboBox is shown.")]
    [Category(@"Behavior")]
    public event EventHandler? DropDown;

    /// <summary>
    /// Indicates that the drop-down portion of the KryptonComboBox has closed.
    /// </summary>
    [Description(@"Indicates that the drop-down portion of the KryptonComboBox has closed.")]
    [Category(@"Behavior")]
    public event EventHandler? DropDownClosed;

    /// <summary>
    /// Occurs when the value of the DropDownStyle property changed.
    /// </summary>
    [Description(@"Occurs when the value of the DropDownStyle property changed.")]
    [Category(@"Behavior")]
    public event EventHandler? DropDownStyleChanged;

    /// <summary>
    /// Occurs when the value of the SelectedIndex property changes.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedIndex property changes.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when an item is chosen from the drop-down list and the drop-down list is closed.
    /// </summary>
    [Description(@"Occurs when an item is chosen from the drop-down list and the drop-down list is closed.")]
    [Category(@"Behavior")]
    public event EventHandler? SelectionChangeCommitted;

    /// <summary>
    /// Occurs when the value of the DataSource property changed.
    /// </summary>
    [Description(@"Occurs when the value of the DataSource property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? DataSourceChanged;

    /// <summary>
    /// Occurs when the value of the DisplayMember property changed.
    /// </summary>
    [Description(@"Occurs when the value of the DisplayMember property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? DisplayMemberChanged;

    /// <summary>
    /// Occurs when the list format has changed.
    /// </summary>
    [Description(@"Occurs when the list format has changed.")]
    [Category(@"PropertyChanged")]
    public event ListControlConvertEventHandler? Format;

    /// <summary>
    /// Occurs when the value of the FormatInfo property changed.
    /// </summary>
    [Description(@"Occurs when the value of the FormatInfo property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? FormatInfoChanged;

    /// <summary>
    /// Occurs when the value of the FormatString property changed.
    /// </summary>
    [Description(@"Occurs when the value of the FormatString property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? FormatStringChanged;

    /// <summary>
    /// Occurs when the value of the FormattingEnabled property changed.
    /// </summary>
    [Description(@"Occurs when the value of the FormattingEnabled property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? FormattingEnabledChanged;

    /// <summary>
    /// Occurs when the value of the SelectedValue property changed.
    /// </summary>
    [Description(@"Occurs when the value of the SelectedValue property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? SelectedValueChanged;

    /// <summary>
    /// Occurs when the value of the ValueMember property changed.
    /// </summary>
    [Description(@"Occurs when the value of the ValueMember property changed.")]
    [Category(@"PropertyChanged")]
    public event EventHandler? ValueMemberChanged;

    /// <summary>
    /// Occurs when the KryptonComboBox text has changed.
    /// </summary>
    [Description(@"Occurs when the KryptonComboBox text has changed.")]
    [Category(@"Behavior")]
    public event EventHandler? TextUpdate;

    /// <summary>
    /// Occurs when the hovered selection changed.
    /// </summary>
    [Description(@"Occurs when the hovered selection changed.")]
    [Category(@"Behavior")]
    public event EventHandler<HoveredSelectionChangedEventArgs>? HoveredSelectionChanged;

    /// <summary>
    /// Occurs when the <see cref="KryptonComboBox"/> wants to display a tooltip.
    /// </summary>
    [Description(@"Occurs when the KryptonComboBox wants to display a tooltip.")]
    [Category(@"Behavior")]
    public event EventHandler<ToolTipNeededEventArgs>? ToolTipNeeded;

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
    /// Occurs when the value of the Paint property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? Paint;

    /// <summary>
    /// Occurs when the value of the PaddingChanged property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? PaddingChanged;
    #endregion

    #region Identity
    static KryptonComboBox() =>
        // Cache the major os version number
        _osMajorVersion = Environment.OSVersion.Version.Major;

    /// <summary>
    /// Initialize a new instance of the KryptonComboBox class.
    /// </summary>
    public KryptonComboBox()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // The height is always fixed
        SetStyle(ControlStyles.FixedHeight, true);

        // Cannot select this control, only the child TextBox
        SetStyle(ControlStyles.Selectable, false);
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

        // Default values
        _alwaysActive = true;
        AllowButtonSpecToolTips = false;
        AllowButtonSpecToolTipPriority = false;
        _cachedHeight = -1;
        _inputControlStyle = InputControlStyle.Standalone;
        _dropButtonStyle = ButtonStyle.InputControl;
        _dropBackStyle = PaletteBackStyle.ControlClient;
        _style = ButtonStyle.ListItem;
        _firstTimePaint = true;
        _hoverIndex = -1;
        _toolTipSpec = new ButtonSpecAny
        {
            ToolTipStyle = LabelStyle.SuperTip
        };

        // Create storage properties
        ButtonSpecs = new ComboBoxButtonSpecCollection(this);

        // Create the palette storage
        StateCommon = new PaletteComboBoxRedirect(Redirector, NeedPaintDelegate);
        StateDisabled = new PaletteComboBoxStates(StateCommon.ComboBox, StateCommon.Item, NeedPaintDelegate);
        StateNormal = new PaletteComboBoxStates(StateCommon.ComboBox, StateCommon.Item, NeedPaintDelegate);
        StateActive = new PaletteComboBoxJustComboStates(StateCommon.ComboBox, NeedPaintDelegate);
        StateTracking = new PaletteComboBoxJustItemStates(StateCommon.Item, NeedPaintDelegate);
        CueHint = new PaletteCueHintText(Redirector, NeedPaintDelegate);

        // Create the draw element for owner drawing individual items
        _contentValues = new FixedContentValue();
        _drawPanel = new ViewDrawPanel(StateCommon.DropBack);
        _drawButton = new ViewDrawButton(StateDisabled.Item, StateNormal.Item,
            StateTracking.Item, StateTracking.Item,
            new PaletteMetricRedirect(Redirector),
            _contentValues, VisualOrientation.Top, false);

        // Create the internal combo box used for containing content
        _comboBox = new InternalComboBox(this);
        _comboBox.IntegralHeight = true;
        _comboBox.DrawItem += OnComboBoxDrawItem;
        _comboBox.MeasureItem += OnComboBoxMeasureItem;
        _comboBox.TrackMouseEnter += OnComboBoxMouseChange;
        _comboBox.TrackMouseLeave += OnComboBoxMouseChange;
        //AddTooltipControlsTo(_comboBox);
        _comboBox.DropDown += OnComboBoxDropDown;
        _comboBox.DropDownClosed += OnComboBoxDropDownClosed;
        _comboBox.DropDownStyleChanged += OnComboBoxDropDownStyleChanged;
        _comboBox.DoubleClick += OnDoubleClick;
        _comboBox.SelectedIndexChanged += OnComboBoxSelectedIndexChanged;
        _comboBox.SelectionChangeCommitted += OnComboBoxSelectionChangeCommitted;
        _comboBox.TextUpdate += OnComboBoxTextUpdate;
        _comboBox.TextChanged += OnComboBoxTextChanged;
        _comboBox.GotFocus += OnComboBoxGotFocus;
        _comboBox.LostFocus += OnComboBoxLostFocus;
        _comboBox.MouseDoubleClick += OnMouseDoubleClick;
        _comboBox.KeyDown += OnComboBoxKeyDown;
        _comboBox.KeyUp += OnComboBoxKeyUp;
        _comboBox.KeyPress += OnComboBoxKeyPress;
        _comboBox.PreviewKeyDown += OnComboBoxPreviewKeyDown;
        _comboBox.DataSourceChanged += OnComboBoxDataSourceChanged;
        _comboBox.DisplayMemberChanged += OnComboBoxDisplayMemberChanged;
        _comboBox.Format += OnComboBoxFormat;
        _comboBox.FormatInfoChanged += OnComboBoxFormatInfoChanged;
        _comboBox.FormatStringChanged += OnComboBoxFormatStringChanged;
        _comboBox.FormattingEnabledChanged += OnComboBoxFormattingEnabledChanged;
        _comboBox.SelectedValueChanged += OnComboBoxSelectedValueChanged;
        _comboBox.ValueMemberChanged += OnComboBoxValueMemberChanged;
        _comboBox.Validating += OnComboBoxValidating;
        _comboBox.Validated += OnComboBoxValidated;
        _comboHolder = new InternalPanel(this);
        _comboHolder.Controls.Add(_comboBox);

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_comboHolder);

        // Create inner view for placing inside the drawing docker
        _drawDockerInner = new ViewLayoutDocker
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

        // Create view for the control border and background
        _drawDockerOuter = new ViewDrawDocker(StateNormal.ComboBox.Back, StateNormal.ComboBox.Border)
        {
            { _drawDockerInner, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDockerOuter);

        // Create button specification collection manager
        _buttonManager = new ButtonSpecManagerLayout(this, Redirector, ButtonSpecs, null,
            [_drawDockerInner],
            [StateCommon.ComboBox],
            [PaletteMetricInt.HeaderButtonEdgeInsetInputControl],
            [PaletteMetricPadding.HeaderButtonPaddingInputControl],
            CreateToolStripRenderer,
            NeedPaintDelegate);

        // Create the manager for handling tooltips
        ToolTipManager = new ToolTipManager(ToolTipValues);
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;
        _buttonManager.ToolTipManager = ToolTipManager;

        // We need to create and cache a device context compatible with the display
        _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

        // Add combo box holder to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_comboHolder);

        // Must set the initial font otherwise the Form level font setting will cause the control
        // to not work correctly. Happens on Vista when the Form has non-default Font setting.
        var triple = StateActive.ComboBox;
        _comboBox.BackColor = triple.PaletteBack.GetBackColor1(PaletteState.Tracking);
        _comboBox.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(PaletteState.Tracking);
        _comboBox.Font = triple.PaletteContent.GetContentShortTextFont(PaletteState.Tracking)!;
        AutoCompleteMode = AutoCompleteMode.None;
        AutoCompleteSource = AutoCompleteSource.None;

        // #1697 Work-around
        // When changing DropDownStyle while the control is disabled the newly selected style was not applied.
        // _deferredComboBoxStyle caches the selected change which is applied when the control is enabled again.
        _deferredComboBoxStyle = null;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // If we are tracking an edit control
            DetachEditControl();

            // Remove any showing tooltip
            OnCancelToolTip(this, EventArgs.Empty);

            // Remember to pull down the manager instance
            _buttonManager?.Destruct();
        }

        base.Dispose(disposing);

        if (_screenDC != IntPtr.Zero)
        {
            PI.DeleteDC(_screenDC);
            _screenDC = IntPtr.Zero;
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the common textbox appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Set a watermark/prompt message for the user.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteCueHintText CueHint { get; }

    private bool ShouldSerializeCueHint() => !CueHint.IsDefault;

    /// <summary>
    /// Signals the object that initialization is starting.
    /// </summary>
    public virtual void BeginInit() =>
        // Remember that fact we are inside a BeginInit/EndInit pair
        IsInitializing = true;

    /// <summary>
    /// Signals the object that initialization is complete.
    /// </summary>
    public virtual void EndInit()
    {
        // We are now initialized
        IsInitialized = true;

        // We are no longer initializing
        IsInitializing = false;

        // Force calculation of the drop-down items again so they are sized correctly
        _comboBox.DrawMode = DrawMode.OwnerDrawVariable;

        // Raise event to show control is now initialized
        OnInitialized(EventArgs.Empty);
    }

    /// <summary>
    /// Gets a value indicating if the control is initialized.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInitialized
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>
    /// Gets a value indicating if the control is initialized.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInitializing
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>
    /// Gets and sets if the control is in the tab chain.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool TabStop
    {
        get => _comboBox.TabStop;
        set => _comboBox.TabStop = value;
    }

    /// <summary>Gets or sets the draw mode of the combobox.</summary>
    /// <value>The draw mode of the combobox.</value>
    [Description(@"Gets or sets the draw mode of the combobox.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public DrawMode DrawMode
    {
        get => _comboBox.DrawMode;
        set
        {
            _comboBox.DrawMode = value;
            Invalidate();
        }
    }

    /// <summary>
    /// Gets and sets if the control is in the ribbon design mode.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool InRibbonDesignMode { get; set; }

    /// <summary>
    /// Gets access to the contained ComboBox instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public ComboBox ComboBox => _comboBox;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => ComboBox;

    /// <summary>
    /// Gets a value indicating whether the control has input focus.
    /// </summary>
    [Browsable(false)]
    public override bool Focused => ComboBox.Focused;

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
    [AllowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public override string Text
    {
        get => _comboBox.Text;
        set => _comboBox.Text = value;
    }

    /// <summary>
    /// Gets and sets the selected item.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem
    {
        get => _comboBox.SelectedItem;
        set => _comboBox.SelectedItem = value;
    }

    /// <summary>
    /// Gets and sets the text that is selected in the editable portion of the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedText
    {
        get => _comboBox.SelectedText;
        set => _comboBox.SelectedText = value;
    }

    /// <summary>
    /// Gets and sets the selected index.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => _comboBox.SelectedIndex;
        set => _comboBox.SelectedIndex = value;
    }

    /// <summary>
    /// Gets and sets the selected value.
    /// </summary>
    [Bindable(true)]
    [Browsable(false)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedValue
    {
        get => _comboBox.SelectedValue;
        //null forgiving operator used, to remove the warning
        set => _comboBox.SelectedValue = value!;
    }

    /// <summary>
    /// Gets and sets a value indicating whether the control is displaying its drop-down portion.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DroppedDown
    {
        get => _comboBox.DroppedDown;
        set => _comboBox.DroppedDown = value;
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public override ContextMenuStrip? ContextMenuStrip
    {
        get => base.ContextMenuStrip;

        set
        {
            base.ContextMenuStrip = value;
            _comboBox.ContextMenuStrip = value;
        }
    }

    /// <summary>
    /// Gets and sets the value member.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to use as the actual value of the items in the control.")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string ValueMember
    {
        get => _comboBox.ValueMember;
        set => _comboBox.ValueMember = value;
    }

    /// <summary>
    /// Gets and sets the list that this control will use to gets its items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the list that this control will use to gets its items.")]
    [AttributeProvider(typeof(IListSource))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    [AllowNull]
    public object? DataSource
    {
        get => _comboBox.DataSource;
        set => _comboBox.DataSource = value;
    }

    /// <summary>
    /// Gets and sets the property to display for the items in this control.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to display for the items in this control.")]
    [TypeConverter(@"System.Windows.Forms.Design.DataMemberFieldConverter")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string DisplayMember
    {
        get => _comboBox.DisplayMember;
        set => _comboBox.DisplayMember = value;
    }

    /// <summary>
    /// Gets and sets the formatting provider.
    /// </summary>
    [DefaultValue(null)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IFormatProvider? FormatInfo
    {
        get => _comboBox.FormatInfo;
        set => _comboBox.FormatInfo = value;
    }

    /// <summary>
    /// Gets and sets the number of characters selected in the editable portion of the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => _comboBox.SelectionLength;
        set => _comboBox.SelectionLength = value;
    }

    /// <summary>
    /// Gets and sets the starting index of selected text in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => _comboBox.SelectionStart;
        set => _comboBox.SelectionStart = value;
    }

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
    /// Gets and sets the appearance and functionality of the KryptonComboBox.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Controls the appearance and functionality of the KryptonComboBox.")]
    [Editor(typeof(OverrideComboBoxStyleDropDownStyle), typeof(UITypeEditor))]
    [DefaultValue(ComboBoxStyle.DropDown)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public ComboBoxStyle DropDownStyle
    {
        // #1697 Work-around
        // When _deferredComboBoxStyle has been set this value takes precedence over _comboBox.DropDownStyle
        get => _deferredComboBoxStyle.HasValue
            ? _deferredComboBoxStyle.Value
            : _comboBox.DropDownStyle;

        set
        {
            // #1697 Work-around
            // If the _deferredComboBoxStyle has been set and DropDownStyle is changed again while the control is disabled this change has to be recorded.
            if (_comboBox.DropDownStyle != value || (_deferredComboBoxStyle.HasValue && _deferredComboBoxStyle.Value != value))
            {
                if (value == ComboBoxStyle.Simple)
                {
                    throw new ArgumentOutOfRangeException(nameof(_comboBox.DropDownStyle), @"KryptonComboBox does not support the DropDownStyle.Simple style.");
                }

                // #1697 Work-around
                // When changing DropDownStyle while the control is disabled the newly selected style was not applied.
                // _deferredComboBoxStyle caches the selected change which is applied when the control is enabled again.
                if (Enabled)
                {
                    _comboBox.DropDownStyle = value;
                    UpdateEditControl();
                }
                else
                {
                    // #1697 Work-around
                    // If the controls is disabled, record the change in DropDownStyle
                    _deferredComboBoxStyle = value;
                }
            }
        }
    }

    /// <summary />
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Determines if the ComboBox Items are shown in full")]
    public bool IntegralHeight
    {
        get => _comboBox.IntegralHeight;
        set => _comboBox.IntegralHeight = value;
    }

    /// <summary>
    /// Gets and sets the height, in pixels, of the drop-down box in a KryptonComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The height, in pixels, of the drop-down box in a KryptonComboBox.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(200)]
    [Browsable(true)]
    public int DropDownHeight
    {
        get => _comboBox.DropDownHeight;
        set => _comboBox.DropDownHeight = value;
    }

    /// <summary>
    /// Gets and sets the width, in pixels, of the drop-down box in a KryptonComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The width, in pixels, of the drop-down box in a KryptonComboBox.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [DefaultValue(200)]
    public int DropDownWidth
    {
        get => _comboBox.DropDownWidth;
        set => _comboBox.DropDownWidth = value;
    }

    /// <summary>
    /// Gets and sets the height, in pixels, of items in an owner-draw KryptonComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Do not use this property, it is provided for backwards compatability only.")]
    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public int ItemHeight
    {
        get => _comboBox.ItemHeight;

        set
        {
            // Do nothing, we set the ItemHeight internally to match the font
        }
    }

    /// <summary>
    /// Gets and sets the maximum number of entries to display in the drop-down list.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The maximum number of entries to display in the drop-down list.")]
    [Localizable(true)]
    [DefaultValue(8)]
    public int MaxDropDownItems
    {
        get => _comboBox.MaxDropDownItems;
        set => _comboBox.MaxDropDownItems = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the edit control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies the maximum number of characters that can be entered into the edit control.")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int MaxLength
    {
        get => _comboBox.MaxLength;
        set => _comboBox.MaxLength = value;
    }

    /// <summary>
    /// Gets or sets whether the items in the list portion of the KryptonComboBox are sorted.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether the items in the list portion of the KryptonComboBox are sorted.")]
    [DefaultValue(false)]
    public bool Sorted
    {
        get => _comboBox.Sorted;
        set => _comboBox.Sorted = value;
    }

    /// <summary>
    /// Gets or sets the items in the KryptonComboBox.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The items in the KryptonComboBox.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [Localizable(true)]
    public ComboBox.ObjectCollection Items => _comboBox.Items;

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
                StateCommon.SetStyles(value);
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetItemStyle() => ItemStyle = ButtonStyle.ListItem;

    private bool ShouldSerializeItemStyle() => ItemStyle != ButtonStyle.ListItem;

    /// <summary>
    /// Gets and sets the drop button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"DropButton style.")]
    public ButtonStyle DropButtonStyle
    {
        get => _dropButtonStyle;

        set
        {
            if (_dropButtonStyle != value)
            {
                _dropButtonStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetDropButtonStyle() => DropButtonStyle = ButtonStyle.InputControl;

    private bool ShouldSerializeDropButtonStyle() => DropButtonStyle != ButtonStyle.InputControl;

    /// <summary>
    /// Gets and sets the drop button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"DropButton style.")]
    public PaletteBackStyle DropBackStyle
    {
        get => _dropBackStyle;

        set
        {
            if (_dropBackStyle != value)
            {
                _dropBackStyle = value;
                StateCommon.SetStyles(value);
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetDropBackStyle() => DropBackStyle = PaletteBackStyle.ControlClient;

    private bool ShouldSerializeDropBackStyle() => DropBackStyle != PaletteBackStyle.ControlClient;

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
    public ComboBoxButtonSpecCollection ButtonSpecs { get; }

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
        get => _comboBox.AutoCompleteCustomSource;
        set => _comboBox.AutoCompleteCustomSource = value;
    }

    /// <summary>
    /// Gets or sets the text completion behavior of the combobox.
    /// </summary>
    [Description(@"Indicates the text completion behavior of the combobox.")]
    [DefaultValue(AutoCompleteMode.None)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteMode AutoCompleteMode
    {
        get => _comboBox.AutoCompleteMode;
        set => _comboBox.AutoCompleteMode = value;
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
        get => _comboBox.AutoCompleteSource;
        set => _comboBox.AutoCompleteSource = value;
    }

    /// <summary>
    /// Gets or sets the format specifier characters that indicate how a value is to be Displayed.
    /// </summary>
    [Description(@"The format specifier characters that indicate how a value is to be Displayed.")]
    [Editor(@"System.Windows.Forms.Design.FormatStringEditor", typeof(UITypeEditor))]
    [MergableProperty(false)]
    [DefaultValue(@"")]
    public string FormatString
    {
        get => _comboBox.FormatString;
        set => _comboBox.FormatString = value;
    }

    /// <summary>
    /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.
    /// </summary>
    [Description(@"If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.")]
    [DefaultValue(false)]
    public bool FormattingEnabled
    {
        get => _comboBox.FormattingEnabled;
        set => _comboBox.FormattingEnabled = value;
    }

    /// <summary>
    /// Gets access to the common combobox appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common combobox appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteComboBoxRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled combobox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled combobox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteComboBoxStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal combobox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal combobox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteComboBoxStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active combobox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active combobox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteComboBoxJustComboStates StateActive { get; }

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    /// <summary>
    /// Gets access to the tracking combobox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking combobox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteComboBoxJustItemStates StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Finds the first item in the combo box that starts with the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindString(string str) => _comboBox.FindString(str);

    /// <summary>
    /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindString(string str, int startIndex) => _comboBox.FindString(str, startIndex);

    /// <summary>
    /// Finds the first item in the combo box that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
    public int FindStringExact(string str) => _comboBox.FindStringExact(str);

    /// <summary>
    /// Finds the first item after the specified index that matches the specified string.
    /// </summary>
    /// <param name="str">The String to search for.</param>
    /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
    /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
    public int FindStringExact(string str, int startIndex) => _comboBox.FindStringExact(str, startIndex);

    /// <summary>
    /// Returns the height of an item in the ComboBox.
    /// </summary>
    /// <param name="index">The index of the item to return the height of.</param>
    /// <returns>The height, in pixels, of the item at the specified index.</returns>
    public int GetItemHeight(int index) => _comboBox.GetItemHeight(index);

    /// <summary>
    /// Returns the text representation of the specified item.
    /// </summary>
    /// <param name="item">The object from which to get the contents to display.</param>
    /// <returns>If the DisplayMember property is not specified, the value returned by GetItemText is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
    public string? GetItemText(object? item) => _comboBox.GetItemText(item);

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => _comboBox.Select(start, length);

    /// <summary>
    /// Selects all text in the control.
    /// </summary>
    public void SelectAll() => _comboBox.SelectAll();

    /// <summary>
    /// Maintains performance when items are added to the ComboBox one at a time.
    /// </summary>
    public void BeginUpdate() => _comboBox.BeginUpdate();

    /// <summary>
    /// Resumes painting the ComboBox control after painting is suspended by the BeginUpdate method.
    /// </summary>
    public void EndUpdate() => _comboBox.EndUpdate();

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
    public bool IsActive =>
        _fixedActive ?? DesignMode
        || AlwaysActive
        || ContainsFocus
        || _mouseOver
        || _comboBox.MouseOver
        || _subclassEdit is { MouseOver: true };

    /// <summary>
    /// Gets access to the ToolTipManager used for displaying tool tips.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolTipManager ToolTipManager { get; }

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => ComboBox.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => ComboBox.Select();

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

    /// <summary>https://github.com/Krypton-Suite/Standard-Toolkit/issues/688</summary>
    /// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values. The default is <see langword="Top" /> and <see langword="Left" />.</returns>
    [Category(@"CatLayout")]
    [DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
    [Description(@"Defines the edges of the container to which a certain control is bound. When a control is anchored to an edge, the distance between the control's closest edge and the specified edge will remain constant")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public override AnchorStyles Anchor
    {
        get => base.Anchor;
        set => base.Anchor = value.HasFlag(AnchorStyles.Bottom | AnchorStyles.Top)
            ? value ^ AnchorStyles.Bottom
            : value;
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

            // The inside combo box is the client rectangle size
            return new Rectangle(_comboHolder.Location, _comboHolder.Size);
        }
    }

    /// <summary>
    /// Override the display padding for the layout fill.
    /// </summary>
    /// <param name="padding">Display padding value.</param>
    public void SetLayoutDisplayPadding(Padding padding) => _layoutPadding = padding;

    /// <summary>
    /// Internal designing mode method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
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
    /// Internal designing mode method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    public Component? DesignerComponentFromPoint(Point pt) =>
        // Ignore call as view builder is already destructed
        IsDisposed ? null : ViewManager?.ComponentFromPoint(pt);

    // Ask the current view for a decision
    /// <summary>
    /// Internal designing mode method.
    /// </summary>
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
    // ReSharper disable VirtualMemberNeverOverridden.Global
    /// <summary>
    /// Raises the Initialized event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnInitialized(EventArgs e) => Initialized?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Raises the TextUpdate event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTextUpdate(EventArgs e) => TextUpdate?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectionChangeCommitted event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectionChangeCommitted(EventArgs e) => SelectionChangeCommitted?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDownStyleChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDropDownStyleChanged(EventArgs e) => DropDownStyleChanged?.Invoke(this, e);

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
    /// Raises the Format event.
    /// </summary>
    /// <param name="e">An ListControlConvertEventArgs containing the event data.</param>
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

    /// <summary>
    /// Raises the SelectedValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedValueChanged(EventArgs e) => SelectedValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ValueMemberChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueMemberChanged(EventArgs e) => ValueMemberChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDownClosed event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDropDownClosed(EventArgs e) => DropDownClosed?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDown event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDropDown(EventArgs e) => DropDown?.Invoke(this, e);

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
    /// Raises the HoveredSelectionChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnHoverSelectionChanged(HoveredSelectionChangedEventArgs e)
    {
        HoveredSelectionChanged?.Invoke(this, e);
        // See if there is a tooltip to display for the new selection.
        var args = new ToolTipNeededEventArgs(e.Index, e.Item);
        OnToolTipNeeded(args);
        if (!args.IsEmpty)
        {
            ShowToolTip(args, e.Bounds.Location);
        }
    }

    /// <summary>
    /// Raises the <see cref="E:DrawItem" /> event.
    /// </summary>
    /// <param name="e">The <see cref="DrawItemEventArgs"/> instance containing the event data.</param>
    protected virtual void OnDrawItem(DrawItemEventArgs e) => DrawItem?.Invoke(this, e);

    /// <summary>
    /// Raises the ToolTipNeeded event.
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnToolTipNeeded(ToolTipNeededEventArgs e) => ToolTipNeeded?.Invoke(this, e);
    // ReSharper restore VirtualMemberNeverOverridden.Global
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates a new instance of the control collection for the KryptonComboBox.
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

        // Subclass the child edit control
        UpdateEditControl();

        // Force the font to be set into the text box child control
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
        // Ensure we have subclassed the contained edit control
        UpdateEditControl();

        // Update view elements
        _drawDockerInner.Enabled = Enabled;
        _drawDockerOuter.Enabled = Enabled;

        // Update state to reflect change in enabled state
        _buttonManager?.RefreshButtons();

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);

        // #1697 Work-around
        // When changing DropDownStyle while the control is disabled the newly selected style was not applied.
        if (Enabled && _deferredComboBoxStyle.HasValue)
        {
            DropDownStyle = _deferredComboBoxStyle.Value;
            _deferredComboBoxStyle = null;
        }
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
        ComboBox.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        ComboBox.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">An PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        // First time we paint we perform a layout to ensure drawing works correctly
        if (_firstTimePaint)
        {
            _firstTimePaint = false;
            ForceControlLayout();
        }

        // ToDo: Create a new API for this in a later version
        //if (StateCommon.ComboBox.Content.SynchronizeDropDownWidth)
        //{
        //    DropDownWidth = Size.Width;
        //}

        base.OnPaint(e);
        Paint?.Invoke(this, e!);
    }

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
    /// Raises the MouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
        _mouseOver = true;
        PerformNeedPaint(false);
        _comboBox.Invalidate();
        base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        _mouseOver = false;
        PerformNeedPaint(false);
        _comboBox.Invalidate();
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        _comboBox.Focus();
    }

    /// <summary>
    /// Occurs when a user preference has changed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event details.</param>
    protected override void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        UpdateStateAndPalettes();
        var triple = GetComboBoxTripleState();
        PaletteState state = _drawDockerOuter.State;
        _comboBox.BackColor = triple.PaletteBack.GetBackColor1(state);
        _comboBox.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(state);
        _comboBox.Font = triple.PaletteContent.GetContentShortTextFont(state)!;
        _comboBox.ClearAppThemed();
        _comboHolder.BackColor = _comboBox.BackColor;

        base.OnUserPreferenceChanged(sender, e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        if (!IsDisposed && !Disposing && !DroppedDown)
        {
            AttachEditControl();

            // Let base class calculate fill rectangle
            base.OnLayout(levent);

            try
            {
                // Only use layout logic if control is fully initialized or if being forced
                // to allow a relayout or if in design mode.
                if ((_forcedLayout || (DesignMode && (_comboHolder != null)))
                    && _layoutFill.FillRect is { Height: > 0, Width: > 0 } fillRect
                    && fillRect != _comboHolder.Bounds)
                {
                    _comboHolder.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
                    _comboBox.SetBounds(-(1 + _layoutPadding.Left), -1, fillRect.Width + 2 + _layoutPadding.Right, fillRect.Height);

                    // Always center the combo vertically
                    _comboBox.Top = fillRect.Height / 2 - _comboBox.Height / 2;

                    // IntegralHeight does not always work as it should when set to true (possibly in this case).
                    // Toggling it corrects the chopped off text and shows the item in full
                    IntegralHeight = !IntegralHeight;
                    IntegralHeight = !IntegralHeight;
                }
            }
            catch
            {
                // Probably creation order in the designer is a bit wonky...
                // Ignore for now
            }
        }
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
        // If setting the actual height
        if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
        {
            // First time the height is set, remember it
            if (_cachedHeight == -1)
            {
                _cachedHeight = height;
            }

            // Override the actual height used
            height = PreferredHeight;
        }

        // If setting the actual height then cache it for later
        if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
        {
            _cachedHeight = height;
        }

        base.SetBoundsCore(x, y, width, height, specified);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(121, PreferredHeight);

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (!e.NeedLayout)
        {
            _comboBox.Invalidate();
        }
        else if (!DroppedDown)
        {
            ForceControlLayout();
        }

        if (!IsDisposed && !Disposing)
        {
            UpdateStateAndPalettes();
            var triple = GetComboBoxTripleState();
            PaletteState state = _drawDockerOuter.State;
            _comboBox.BackColor = triple.PaletteBack.GetBackColor1(state);
            _comboBox.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(state);
            _comboBox.Font = triple.PaletteContent.GetContentShortTextFont(state)!;
            _comboHolder.BackColor = _comboBox.BackColor;
        }

        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        base.OnPaletteChanged(e);
        _comboBox.Invalidate();
    }

    /// <summary>
    /// Processes a notification from palette of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnPaletteNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        base.OnPaletteChanged(e);
        _comboBox.Invalidate();
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
    #endregion

    #region Internal
    internal bool InTransparentDesignMode => InRibbonDesignMode;

    internal bool IsFixedActive => _fixedActive != null;

    internal void DetachEditControl()
    {
        if (_subclassEdit != null)
        {
            // Stop subclassing
            //_subclassEdit.ReleaseHandle();
            _subclassEdit = null;
        }
    }
    #endregion

    #region Implementation
    private void AttachEditControl()
    {
        if (!IsDisposed && !Disposing)
        {
            // Only need to subclass once
            if (_subclassEdit == null)
            {
                // Find the first child
                var childPtr = PI.GetWindow(_comboBox.Handle, PI.GetWindowType.GW_CHILD);

                // If we found a child then it is the edit class
                if (childPtr != IntPtr.Zero)
                {
                    if (DropDownStyle == ComboBoxStyle.Simple)
                    {
                        //this.childListBox = new ComboBox.ComboBoxChildNativeWindow(this, ComboBox.ChildWindowType.ListBox);
                        //this.childListBox.AssignHandle(window);
                        childPtr = PI.GetWindow(childPtr, PI.GetWindowType.GW_HWNDNEXT);
                    }
                    _subclassEdit = new SubclassEdit(childPtr, this);
                    // Following will have been done by Framework
                    //PI.SendMessage(childPtr, PI.WM_.EM_SETMARGINS, new IntPtr(3), IntPtr.Zero);
                    _subclassEdit.TrackMouseEnter += OnComboBoxMouseChange;
                    _subclassEdit.TrackMouseLeave += OnComboBoxMouseChange;
                }
            }
        }
    }

    internal void UpdateEditControl()
    {
        AttachEditControl();

        // Only show the child edit control when we are enabled
        if (_subclassEdit != null)
        {
            //_subclassEdit.Visible = Enabled; got to allow the formatting to be seen when not editing
            // This does not work see https://github.com/Krypton-Suite/Standard-Toolkit/issues/179
            _subclassEdit.Visible = false;  // On Focus is supposed to enable this
        }
    }

    private void UpdateStateAndPalettes()
    {
        // Get the correct palette settings to use
        var tripleState = GetComboBoxTripleState();
        _drawDockerOuter.SetPalettes(tripleState.PaletteBack, tripleState.PaletteBorder!);

        // Update enabled state
        _drawDockerOuter.Enabled = Enabled;

        // Find the new state of the main view element
        PaletteState state = Enabled ? (IsActive ? PaletteState.Tracking : PaletteState.Normal) : PaletteState.Disabled;

        _drawDockerOuter.ElementState = state;
    }

    internal PaletteInputControlTripleStates GetComboBoxTripleState() => Enabled ? IsActive ? StateActive.ComboBox : StateNormal.ComboBox : StateDisabled.ComboBox;

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

    private void OnComboBoxDrawItem(object? sender, DrawItemEventArgs e)
    {
        Rectangle drawBounds = e.Bounds;

        // Do we need to draw the edit area
        if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
        {
            // TODO: Check if this is covered by the WM_PAINT in the internal Combo
            // Always get base implementation to draw the background
            e.DrawBackground();

            // Find correct text color
            Color textColor = _comboBox.ForeColor;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                textColor = SystemColors.HighlightText;
            }

            // Find correct background color
            Color backColor = _comboBox.BackColor;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                backColor = SystemColors.Highlight;
            }

            // Is there an item to draw
            if (e.Index >= 0)
            {
                // Set the correct text rendering hint for the text drawing. We only draw if the edit text is enabled so we
                // just always grab the normal state value. Without this line the wrong hint can occur because it inherits
                // it from the device context. Resulting in blurred text.
                e.Graphics.TextRenderingHint = CommonHelper.PaletteTextHintToRenderingHint(StateNormal.Item.PaletteContent!.GetContentShortTextHint(PaletteState.Normal));

                TextFormatFlags flags = TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding;

                // Use the correct prefix setting
                flags |= TextFormatFlags.NoPrefix;

                // Do we need to switch drawing direction?
                if (RightToLeft == RightToLeft.Yes)
                {
                    flags |= TextFormatFlags.Right;
                }

                // Draw text using font defined by the control
                TextRenderer.DrawText(e.Graphics,
                    _comboBox.Text, _comboBox.Font,
                    drawBounds,
                    textColor, backColor,
                    flags);
            }
        }
        else
        {
            // Is there an item to draw
            if (e.Index >= 0)
            {
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
                    // If selected then show as a checked item
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        buttonState = PaletteState.Tracking;
                        if (_hoverIndex != e.Index)
                        {
                            _hoverIndex = e.Index;
                            // Raise the Hover event
                            var ev = new HoveredSelectionChangedEventArgs(e.Bounds, e.Index, Items[e.Index]!);
                            OnHoverSelectionChanged(ev);
                        }
                    }
                }

                // Update the view with the calculated state
                _drawButton.ElementState = buttonState;

                // Grab the raw device context for the graphics instance
                var hdc = e.Graphics.GetHdc();

                try
                {
                    // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
                    var hBitmap = PI.CreateCompatibleBitmap(hdc, drawBounds.Right, drawBounds.Bottom);

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
                            using Graphics g = Graphics.FromHdc(_screenDC);
                            // Ask the view element to layout in given space, needs this before a render call
                            using (var context = new ViewLayoutContext(this, Renderer))
                            {
                                context.DisplayRectangle = drawBounds;
                                _drawPanel.Layout(context);
                                _drawButton.Layout(context);
                            }

                            // Ask the view element to actually draw
                            using (var context = new RenderContext(this, g, drawBounds, Renderer))
                            {
                                _drawPanel.Render(context);
                                _drawButton.Render(context);
                            }

                            // Now blit from the bitmap from the screen to the real dc
                            PI.BitBlt(hdc, drawBounds.X, drawBounds.Y, drawBounds.Width, drawBounds.Height, _screenDC, drawBounds.X, drawBounds.Y, PI.SRCCOPY);
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
                finally
                {
                    // Must reserve the GetHdc() call before
                    e.Graphics.ReleaseHdc();
                }
            }
        }
    }

    private void OnComboBoxMeasureItem(object? sender, MeasureItemEventArgs e)
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
            _contentValues!.ShortText = _comboBox.GetItemText(Items[index]);
            _contentValues.LongText = null;
            _contentValues.Image = null;
            _contentValues.ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        }

        // Always ensure there is some text that can be measured, if only a single space. The height of
        // the first item is used to calculate the total height of the drop-down. So if the first time
        // had null then the height would be very small for the item and also the drop-down.
        if (string.IsNullOrEmpty(_contentValues.ShortText))
        {
            _contentValues.ShortText = @" ";
        }
    }

    private void OnComboBoxMouseChange(object? sender, EventArgs e)
    {
        // Find new tracking mouse change state
        var tracking = _comboBox.MouseOver || _subclassEdit is { MouseOver: true };

        // Change in tracking state?
        if (tracking != _trackingMouseEnter)
        {
            _trackingMouseEnter = tracking;

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

        PerformNeedPaint(false);
        _comboBox.Invalidate();
    }

    private void OnComboBoxGotFocus(object? sender, EventArgs e)
    {
        if (DropDownStyle == ComboBoxStyle.DropDown)
        {
            _subclassEdit!.Visible = true;
            PaletteState state = Enabled
                ? IsActive
                    ? PaletteState.Tracking
                    : PaletteState.Normal
                : PaletteState.Disabled;
            _comboBox.Font = GetComboBoxTripleState().Content.GetContentShortTextFont(state)!;
        }

        base.OnGotFocus(e);
        PerformNeedPaint(false);
        _comboBox.Invalidate();
    }

    private void OnComboBoxLostFocus(object? sender, EventArgs e)
    {
        if (DropDownStyle == ComboBoxStyle.DropDown)
        {
            _subclassEdit!.Visible = false;
            _comboBox.Font = GetComboBoxTripleState().Content.GetContentShortTextFont(PaletteState.Normal)!;
        }

        // ReSharper disable RedundantBaseQualifier
        base.OnLostFocus(e);
        // ReSharper restore RedundantBaseQualifier
        PerformNeedPaint(false);
        _comboBox.Invalidate();
    }

    private void OnComboBoxTextChanged(object? sender, EventArgs e) => OnTextChanged(e);

    private void OnComboBoxTextUpdate(object? sender, EventArgs e) => OnTextUpdate(e);

    private void OnComboBoxSelectionChangeCommitted(object? sender, EventArgs e) => OnSelectionChangeCommitted(e);

    private void OnComboBoxSelectedIndexChanged(object? sender, EventArgs e) => OnSelectedIndexChanged(e);

    private void OnComboBoxDropDownStyleChanged(object? sender, EventArgs e) => OnDropDownStyleChanged(e);

    private void OnComboBoxDataSourceChanged(object? sender, EventArgs e) => OnDataSourceChanged(e);

    private void OnComboBoxDisplayMemberChanged(object? sender, EventArgs e) => OnDisplayMemberChanged(e);

    private void OnComboBoxDropDownClosed(object? sender, EventArgs e)
    {
        _comboBox.Dropped = false;
        Refresh();
        OnDropDownClosed(e);
    }

    private void OnComboBoxDropDown(object? sender, EventArgs e)
    {
        _comboBox.Dropped = true;
        _hoverIndex = -1;
        Refresh();
        OnDropDown(e);
    }

    private void OnComboBoxKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnComboBoxKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnComboBoxKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnComboBoxPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnComboBoxValidated(object? sender, EventArgs e) => OnValidated(e);

    private void OnComboBoxValidating(object? sender, CancelEventArgs e) => OnValidating(e);

    private void OnComboBoxFormat(object? sender, ListControlConvertEventArgs e) => OnFormat(e);

    private void OnComboBoxFormatInfoChanged(object? sender, EventArgs e) => OnFormatInfoChanged(e);

    private void OnComboBoxFormatStringChanged(object? sender, EventArgs e) => OnFormatStringChanged(e);

    private void OnComboBoxFormattingEnabledChanged(object? sender, EventArgs e) => OnFormattingEnabledChanged(e);

    private void OnComboBoxSelectedValueChanged(object? sender, EventArgs e)
    {
        UpdateEditControl();
        PerformNeedPaint(false);
        _comboBox.Invalidate();
        OnSelectedValueChanged(e);
    }

    private void OnComboBoxValueMemberChanged(object? sender, EventArgs e) => OnValueMemberChanged(e);

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

                // Find the button spec associated with the tooltip request
                ButtonSpec? buttonSpec = _buttonManager!.ButtonSpecFromView(e.Target);

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

                    if (AllowButtonSpecToolTipPriority)
                    {
                        visualBasePopupToolTip?.Dispose();
                    }

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

    // Remove any currently showing tooltip
    private void OnCancelToolTip(object? sender, EventArgs e) => _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page anymore
        _visualPopupToolTip = null;
    }

    private VisualPopupToolTip GetToolTip()
    {
        if (_toolTip is { IsDisposed: false }
           )
        {
            return _toolTip;
        }

        var redirector = new PaletteRedirect(KryptonManager.CurrentGlobalPalette);
        _toolTip = new VisualPopupToolTip(redirector,
            new ButtonSpecToContent(redirector, _toolTipSpec), KryptonManager
                .CurrentGlobalPalette.GetRenderer(),
            ToolTipValues.ToolTipShadow);
        return _toolTip;
    }

    private void ShowToolTip(ToolTipNeededEventArgs e, Point location)
    {
        _toolTipSpec.ToolTipTitle = e.Heading;
        _toolTipSpec.ToolTipBody = e.Description;
        _toolTipSpec.ToolTipImage = e.Icon;
        VisualPopupToolTip tip = GetToolTip();
        // Needed to make Krypton update the tooltip data with the data of the spec.
        tip.PerformNeedPaint(true);
        var point = location with { X = location.X + DropDownWidth };
        tip.ShowCalculatingSize(PointToScreen(point));
    }

    private void OnDoubleClick(object? sender, EventArgs e) => base.OnDoubleClick(e);

    private void OnMouseDoubleClick(object? sender, MouseEventArgs e) => base.OnMouseDoubleClick(e);

    #endregion
}