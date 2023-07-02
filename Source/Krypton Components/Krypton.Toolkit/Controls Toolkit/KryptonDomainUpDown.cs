﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide a DomainUpDown with Krypton styling applied.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonDomainUpDown), "ToolboxBitmaps.KryptonDomainUpDown.bmp")]
    [DefaultEvent(nameof(SelectedItemChanged))]
    [DefaultProperty(nameof(Items))]
    [DefaultBindingProperty(nameof(SelectedItem))]
    [Designer(typeof(KryptonDomainUpDownDesigner))]
    [DesignerCategory(@"code")]
    [Description(@"Represents a Windows spin box (also known as an up-down control) that displays string values.")]
    public class KryptonDomainUpDown : VisualControlBase,
                                       IContainedInputControl
    {
        #region Classes
        private class InternalDomainUpDown : DomainUpDown
        {
            #region Instance Fields
            private readonly KryptonDomainUpDown _kryptonDomainUpDown;
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
            /// Initialize a new instance of the InternalDomainUpDown class.
            /// </summary>
            /// <param name="kryptonDomainUpDown">Reference to owning control.</param>
            public InternalDomainUpDown(KryptonDomainUpDown kryptonDomainUpDown)
            {
                _kryptonDomainUpDown = kryptonDomainUpDown;

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
                        if (_kryptonDomainUpDown.InTransparentDesignMode)
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
                        _kryptonDomainUpDown.PerformNeedPaint(true);
                        Invalidate();
                        base.WndProc(ref m);
                        break;
                    case PI.WM_.MOUSEMOVE:
                        // Mouse is over the control
                        if (!MouseOver)
                        {
                            MouseOver = true;
                            _kryptonDomainUpDown.PerformNeedPaint(true);
                            Invalidate();
                        }
                        base.WndProc(ref m);
                        break;
                    case PI.WM_.CONTEXTMENU:
                        // Only interested in overriding the behavior when we have a krypton context menu...
                        if (_kryptonDomainUpDown.KryptonContextMenu != null)
                        {
                            // Extract the screen mouse position (if might not actually be provided)
                            Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                            // If keyboard activated, the menu position is centered
                            if (((int)(long)m.LParam) == -1)
                            {
                                mousePt = PointToScreen(new Point(Width / 2, Height / 2));
                            }

                            // Show the context menu
                            _kryptonDomainUpDown.KryptonContextMenu.Show(_kryptonDomainUpDown, mousePt);

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

            #region Internal
            /// <summary>
            /// Gets or sets a value indicating whether a value has been entered by the user.
            /// </summary>
            protected internal bool InternalUserEdit
            {
                get => UserEdit;
                set => UserEdit = value;
            }
            #endregion
        }

        private class SubclassEdit : NativeWindow
        {
            #region Instance Fields

            private readonly InternalDomainUpDown _internalDomainUpDown;
            private bool _mouseOver;

            #endregion

            #region Events
            /// <summary>
            /// Occurs when the mouse enters the InternalDomainUpDown.
            /// </summary>
            public event EventHandler? TrackMouseEnter;

            /// <summary>
            /// Occurs when the mouse leaves the InternalDomainUpDown.
            /// </summary>
            public event EventHandler? TrackMouseLeave;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the SubclassEdit class.
            /// </summary>
            /// <param name="editControl">Handle of the Edit control to subclass.</param>
            /// <param name="kryptonDomainUpDown">Reference to top level control.</param>
            /// <param name="internalDomainUpDown">Reference to internal domain control.</param>
            public SubclassEdit(IntPtr editControl,
                                KryptonDomainUpDown kryptonDomainUpDown,
                                InternalDomainUpDown internalDomainUpDown)
            {
                DomainUpDown = kryptonDomainUpDown;
                _internalDomainUpDown = internalDomainUpDown;

                // Attach ourself to the provided control, subclassing it
                AssignHandle(editControl);

                // By default, not over a valid part of the client
                MousePoint = new Point(-int.MaxValue, -int.MaxValue);
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
            /// Gets the last mouse point if the mouse is over the control.
            /// </summary>
            public Point MousePoint { get; private set; }

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
            /// Gets access to the owning domain up down control.
            /// </summary>
            protected KryptonDomainUpDown DomainUpDown { get; }

            /// <summary>
            /// Process Windows-based messages.
            /// </summary>
            /// <param name="m">A Windows-based message.</param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case PI.WM_.NCHITTEST:
                        if (DomainUpDown.InTransparentDesignMode)
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
                        MousePoint = new Point(-int.MaxValue, -int.MaxValue);
                        DomainUpDown.PerformNeedPaint(true);
                        base.WndProc(ref m);
                        break;
                    case PI.WM_.MOUSEMOVE:
                        // Extra mouse position
                        MousePoint = new Point((int)m.LParam.ToInt64());

                        // Mouse is over the control
                        if (!MouseOver)
                        {
                            PI.TRACKMOUSEEVENTS tme = new PI.TRACKMOUSEEVENTS
                            {

                                // This structure needs to know its own size in bytes
                                cbSize = (uint)Marshal.SizeOf(typeof(PI.TRACKMOUSEEVENTS)),
                                dwHoverTime = 100,

                                // We need to know then the mouse leaves the client window area
                                dwFlags = PI.TME_LEAVE,

                                // We want to track our own window
                                hWnd = Handle
                            };

                            // Call Win32 API to start tracking
                            PI.TrackMouseEvent(ref tme);

                            MouseOver = true;
                            DomainUpDown.PerformNeedPaint(true);
                        }
                        base.WndProc(ref m);
                        break;
                    case PI.WM_.PRINTCLIENT:
                    case PI.WM_.PAINT:
                        {
                            PI.PAINTSTRUCT ps = new PI.PAINTSTRUCT();

                            // Do we need to BeginPaint or just take the given HDC?
                            IntPtr hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

                            // Paint the entire area in the background color
                            using (Graphics g = Graphics.FromHdc(hdc))
                            {
                                // Grab the client area of the control
                                PI.GetClientRect(Handle, out PI.RECT rect);

                                PaletteState state = DomainUpDown.Enabled
                                        ? (DomainUpDown.IsActive ? PaletteState.Tracking : PaletteState.Normal)
                                        : PaletteState.Disabled
                                    ;
                                PaletteInputControlTripleStates states = DomainUpDown.GetTripleState();

                                // Drawn entire client area in the background color
                                using (SolidBrush backBrush = new SolidBrush(states.PaletteBack.GetBackColor1(state)))
                                {
                                    g.FillRectangle(backBrush, new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top));
                                }

                                // Create rect for the text area
                                Size borderSize = SystemInformation.BorderSize;
                                rect.left -= borderSize.Width + 1;

                                //////////////////////////////////////////////////////
                                // Following to allow the Draw to always happen, to allow centering etc  
                                _internalDomainUpDown.TextAlign =
                                    states.Content.GetContentShortTextH(state) switch
                                    {
                                        PaletteRelativeAlign.Center => HorizontalAlignment.Center,
                                        PaletteRelativeAlign.Far => HorizontalAlignment.Right,
                                        _ => HorizontalAlignment.Left
                                    };

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

                            // Do we need to match the original BeginPaint?
                            if (m.WParam == IntPtr.Zero)
                            {
                                PI.EndPaint(Handle, ref ps);
                            }
                        }
                        break;
                    case PI.WM_.CONTEXTMENU:
                        // Only interested in overriding the behavior when we have a krypton context menu...
                        if (DomainUpDown.KryptonContextMenu != null)
                        {
                            // Extract the screen mouse position (if might not actually be provided)
                            Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                            // If keyboard activated, the menu position is centered
                            if (((int)(long)m.LParam) == -1)
                            {
                                PI.GetClientRect(Handle, out PI.RECT clientRect);
                                mousePt = DomainUpDown.PointToScreen(new Point((clientRect.right - clientRect.left) / 2,
                                                                               (clientRect.bottom - clientRect.top) / 2));
                            }

                            // Show the context menu
                            DomainUpDown.KryptonContextMenu.Show(DomainUpDown, mousePt);

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

        private class SubclassButtons : SubclassEdit, IContentValues, IDisposable
        {
            #region Instance Fields
            private PaletteTripleToPalette _palette;
            private ViewDrawButton _viewButton;
            private IntPtr _screenDC;
            private Point _mousePressed;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the SubclassButtons class.
            /// </summary>
            /// <param name="buttonsPtr">Handle of the Buttons control to subclass.</param>
            /// <param name="kryptonDomainUpDown">Reference to top level control.</param>
            /// <param name="internalDomainUpDown">Reference to internal domain control.</param>
            public SubclassButtons(IntPtr buttonsPtr,
                                   KryptonDomainUpDown kryptonDomainUpDown,
                                   InternalDomainUpDown internalDomainUpDown)
                : base(buttonsPtr, kryptonDomainUpDown, internalDomainUpDown)
            {
                _mousePressed = new Point(-int.MaxValue, -int.MaxValue);

                // We need to create and cache a device context compatible with the display
                _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

            }
            #endregion  

            #region Public
            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            public void Dispose()
            {
                if (_screenDC != IntPtr.Zero)
                {
                    PI.DeleteDC(_screenDC);
                    _screenDC = IntPtr.Zero;
                }
            }

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
            public virtual Color GetImageTransparentColor(PaletteState state) => Color.Empty;

            /// <summary>
            /// Gets the content long text.
            /// </summary>
            /// <returns>String value.</returns>
            public virtual string GetLongText() => string.Empty;

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
                    case PI.WM_.LBUTTONDBLCLK:
                    case PI.WM_.LBUTTONDOWN:
                        _mousePressed = new Point((int)m.LParam.ToInt64());
                        base.WndProc(ref m);
                        PI.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, 0x300);
                        break;
                    case PI.WM_.LBUTTONUP:
                    case PI.WM_.MBUTTONUP:
                    case PI.WM_.MBUTTONDOWN:
                    case PI.WM_.RBUTTONUP:
                    case PI.WM_.RBUTTONDOWN:
                        _mousePressed = new Point(-int.MaxValue, -int.MaxValue);
                        base.WndProc(ref m);
                        PI.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, 0x300);
                        break;
                    case PI.WM_.PRINTCLIENT:
                    case PI.WM_.PAINT:
                        {
                            PI.PAINTSTRUCT ps = new PI.PAINTSTRUCT();

                            // Do we need to BeginPaint or just take the given HDC?
                            IntPtr hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

                            // Grab the client area of the control
                            PI.GetClientRect(Handle, out PI.RECT rect);
                            Rectangle clientRect = new Rectangle(rect.left, rect.top, rect.right - rect.left,
                                rect.bottom - rect.top);

                            try
                            {
                                // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
                                IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, clientRect.Right, clientRect.Bottom);

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
                                        // Drawn entire client area in the background color
                                        using (SolidBrush backBrush =
                                               new SolidBrush(DomainUpDown.DomainUpDown.BackColor))
                                        {
                                            g.FillRectangle(backBrush, clientRect);
                                        }

                                        // Draw the actual up and down buttons split inside the client rectangle
                                        DrawUpDownButtons(g,
                                            new Rectangle(clientRect.X, clientRect.Y, clientRect.Width,
                                                clientRect.Height - 1));

                                        // Now blit from the bitmap from the screen to the real dc
                                        PI.BitBlt(hdc, clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height,
                                            _screenDC, clientRect.X, clientRect.Y, PI.SRCCOPY);
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
                                // Do we need to match the original BeginPaint?
                                if (m.WParam == IntPtr.Zero)
                                {
                                    PI.EndPaint(Handle, ref ps);
                                }
                            }
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            #endregion

            #region Private
            private void DrawUpDownButtons(Graphics g, Rectangle clientRect)
            {
                // Create the views and palette entries first time around
                if (_viewButton == null)
                {
                    // Create helper object to get all values from the KryptonDomainUpDown redirector
                    _palette = new PaletteTripleToPalette(DomainUpDown.Redirector,
                                                          PaletteBackStyle.ButtonStandalone,
                                                          PaletteBorderStyle.ButtonStandalone,
                                                          PaletteContentStyle.ButtonStandalone);

                    // Create view element for drawing the actual buttons
                    _viewButton = new ViewDrawButton(_palette, _palette, _palette,
                                                     _palette, _palette, _palette, _palette,
                                                     new PaletteMetricRedirect(DomainUpDown.Redirector),
                                                     this, VisualOrientation.Top, false);
                }

                // Update with the latset button style for the up/down buttons
                _palette.SetStyles(DomainUpDown.UpDownButtonStyle);

                // Find button rectangles
                Rectangle upRect = new Rectangle(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height / 2);
                Rectangle downRect = new Rectangle(clientRect.X, upRect.Bottom, clientRect.Width,
                    clientRect.Bottom - upRect.Bottom);

                // Position and draw the up/down buttons
                using ViewLayoutContext layoutContext = new ViewLayoutContext(DomainUpDown, DomainUpDown.Renderer);
                using RenderContext renderContext =
                    new RenderContext(DomainUpDown, g, clientRect, DomainUpDown.Renderer);
                // Up button
                layoutContext.DisplayRectangle = upRect;
                _viewButton.ElementState = ButtonElementState(upRect);
                _viewButton.Layout(layoutContext);
                _viewButton.Render(renderContext);
                renderContext.Renderer.RenderGlyph.DrawInputControlNumericUpGlyph(renderContext, _viewButton.ClientRectangle, _palette.PaletteContent, _viewButton.ElementState);

                // Down button
                layoutContext.DisplayRectangle = downRect;
                _viewButton.ElementState = ButtonElementState(downRect);
                _viewButton.Layout(layoutContext);
                _viewButton.Render(renderContext);
                renderContext.Renderer.RenderGlyph.DrawInputControlNumericDownGlyph(renderContext, _viewButton.ClientRectangle, _palette.PaletteContent, _viewButton.ElementState);
            }

            private PaletteState ButtonElementState(Rectangle buttonRect)
            {
                if (DomainUpDown.Enabled)
                {
                    if (MouseOver && buttonRect.Contains(MousePoint))
                    {
                        if (buttonRect.Contains(_mousePressed))
                        {
                            return PaletteState.Pressed;
                        }
                        else if (_mousePressed.X == -int.MaxValue)
                        {
                            return PaletteState.Tracking;
                        }
                    }

                    return DomainUpDown.IsActive || DomainUpDown is { IsFixedActive: true, InputControlStyle: InputControlStyle.Standalone }
                        ? DomainUpDown.InputControlStyle == InputControlStyle.Standalone ? PaletteState.CheckedNormal : PaletteState.CheckedTracking
                        : PaletteState.Normal;
                }
                else
                {
                    return PaletteState.Disabled;
                }
            }
            #endregion
        }
        #endregion

        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class DomainUpDownButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
        {
            #region Identity
            /// <summary>
            /// Initialize a new instance of the DomainUpDownButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public DomainUpDownButtonSpecCollection(KryptonDomainUpDown owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Instance Fields

        private VisualPopupToolTip _visualPopupToolTip;
        private readonly ButtonSpecManagerLayout _buttonManager;
        private readonly ViewLayoutDocker _drawDockerInner;
        private readonly ViewDrawDocker _drawDockerOuter;
        private readonly ViewLayoutFill _layoutFill;
        private readonly InternalDomainUpDown _domainUpDown;
        private InputControlStyle _inputControlStyle;
        private ButtonStyle _upDownButtonStyle;
        private SubclassEdit _subclassEdit;
        private SubclassButtons _subclassButtons;
        private bool? _fixedActive;
        private bool _forcedLayout;
        private bool _mouseOver;
        private bool _alwaysActive;
        private bool _trackingMouseEnter;
        private float _cornerRoundingRadius;
        private int _cachedHeight;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the SelectedItem property changes.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Occurs when the value of the SelectedItem property changes.")]
        public event EventHandler? SelectedItemChanged;

        /// <summary>
        /// Occurs when the user scrolls the scroll box.
        /// </summary>
        [Category(@"Action")]
        [Description(@"Occurs when the user scrolls the scroll box.")]
        public event ScrollEventHandler? Scroll;

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
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDomainUpDown class.
        /// </summary>
        public KryptonDomainUpDown()
        {
            // Contains another control and needs marking as such for validation to work
            SetStyle(ControlStyles.ContainerControl, true);

            // By default we are not multiline and so the height is fixed
            SetStyle(ControlStyles.FixedHeight, true);

            // Cannot select this control, only the child TextBox
            SetStyle(ControlStyles.Selectable, false);

            // Defaults
            _inputControlStyle = InputControlStyle.Standalone;
            _upDownButtonStyle = ButtonStyle.InputControl;
            _cachedHeight = -1;
            _alwaysActive = true;
            AllowButtonSpecToolTips = false;
            AllowButtonSpecToolTipPriority = false;

            // Create storage properties
            ButtonSpecs = new DomainUpDownButtonSpecCollection(this);

            // Create the palette storage
            StateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, NeedPaintDelegate);
            StateDisabled = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
            StateNormal = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
            StateActive = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);

            // Create the internal domain updown used for containing content
            _domainUpDown = new InternalDomainUpDown(this);
            _domainUpDown.Scroll += OnDomainUpDownScroll;
            _domainUpDown.SelectedItemChanged += OnDomainUpDownSelectedItemChanged;
            _domainUpDown.TrackMouseEnter += OnDomainUpDownMouseChange;
            _domainUpDown.TrackMouseLeave += OnDomainUpDownMouseChange;
            _domainUpDown.GotFocus += OnDomainUpDownGotFocus;
            _domainUpDown.LostFocus += OnDomainUpDownLostFocus;
            _domainUpDown.KeyDown += OnDomainUpDownKeyDown;
            _domainUpDown.KeyUp += OnDomainUpDownKeyUp;
            _domainUpDown.KeyPress += OnDomainUpDownKeyPress;
            _domainUpDown.PreviewKeyDown += OnDomainUpDownPreviewKeyDown;
            _domainUpDown.TextChanged += OnDomainUpDownTextChanged;
            _domainUpDown.Validating += OnDomainUpDownValidating;
            _domainUpDown.Validated += OnDomainUpDownValidated;

            // Create the element that fills the remainder space and remembers fill rectangle
            _layoutFill = new ViewLayoutFill(_domainUpDown)
            {
                DisplayPadding = new Padding(1, 1, 1, 0)
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

            // Create button specification collection manager
            _buttonManager = new ButtonSpecManagerLayout(this, Redirector, ButtonSpecs, null,
                                                         new[] { _drawDockerInner },
                                                         new IPaletteMetric[] { StateCommon },
                                                         new[] { PaletteMetricInt.HeaderButtonEdgeInsetInputControl },
                                                         new[] { PaletteMetricPadding.HeaderButtonPaddingInputControl },
                                                         CreateToolStripRenderer,
                                                         NeedPaintDelegate);

            // Create the manager for handling tooltips
            ToolTipManager = new ToolTipManager(ToolTipValues);
            ToolTipManager.ShowToolTip += OnShowToolTip;
            ToolTipManager.CancelToolTip += OnCancelToolTip;
            _buttonManager.ToolTipManager = ToolTipManager;

            // Add text box to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(_domainUpDown);

            // Set `CornerRoundingRadius' to 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE' (-1)
            _cornerRoundingRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;
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
                _buttonManager.Destruct();

                // Tell the buttons class to cleanup resources
                _subclassButtons?.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the corner rounding radius.</summary>
        /// <value>The corner rounding radius.</value>
        [Category(@"Visuals")]
        [Description(@"Gets or sets the corner rounding radius.")]
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float CornerRoundingRadius
        {
            get => _cornerRoundingRadius;

            set => SetCornerRoundingRadius(value);
        }

        /// <summary>
        /// Gets and sets if the control is in the tab chain.
        /// </summary>
        public new bool TabStop
        {
            get => DomainUpDown.TabStop;
            set => DomainUpDown.TabStop = value;
        }

        /// <summary>
        /// Gets and sets if the control is in the ribbon design mode.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool InRibbonDesignMode { get; set; }

        /// <summary>
        /// Gets access to the contained DomainUpDown instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public DomainUpDown DomainUpDown => _domainUpDown;

        /// <summary>
        /// Gets access to the contained input control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public Control ContainedControl => DomainUpDown;

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
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
        /// Gets a value indicating whether the control has input focus.
        /// </summary>
        [Browsable(false)]
        public override bool Focused => DomainUpDown.Focused;

        /// <summary>
        /// Gets or sets the text for the control.
        /// </summary>
        [AllowNull]
        public override string Text
        {
            get => DomainUpDown.Text;
            set => DomainUpDown.Text = value;
        }

        /// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        public override ContextMenuStrip? ContextMenuStrip
        {
            get => base.ContextMenuStrip;

            set
            {
                base.ContextMenuStrip = value;
                DomainUpDown.ContextMenuStrip = value;
            }
        }

        /// <summary>
        /// Gets the collection of allowable items of the domain up down.
        /// </summary>
        [Category(@"Data")]
        [Description(@"The allowable items of the domain up down.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor", typeof(UITypeEditor))]
        [Localizable(true)]
        public DomainUpDown.DomainUpDownItemCollection Items => DomainUpDown.Items;

        /// <summary>
        /// Gets or sets the index value of the selected item. 
        /// </summary>
        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get => DomainUpDown.SelectedIndex;
            set => DomainUpDown.SelectedIndex = value;
        }

        /// <summary>
        /// Gets or sets the selected item based on the index value of the selected item in the collection.  
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? SelectedItem
        {
            get => DomainUpDown.SelectedItem;
            set => DomainUpDown.SelectedItem = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item collection is sorted.   
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Controls whether items in the domain list are sorted.")]
        [DefaultValue(false)]
        public bool Sorted
        {
            get => DomainUpDown.Sorted;
            set => DomainUpDown.Sorted = value;
        }

        /// <summary>
        /// Gets or sets how the text should be aligned for edit controls.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Indicates how the text should be aligned for edit controls.\rDo not use this property, it is provided for backwards compatability only.")]
        [DefaultValue(typeof(HorizontalAlignment), nameof(Left))]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public HorizontalAlignment TextAlign
        {
            get
            {
                return StateCommon.Content.GetContentShortTextH(PaletteState.Normal) switch
                {
                    PaletteRelativeAlign.Center => HorizontalAlignment.Center,
                    PaletteRelativeAlign.Far => HorizontalAlignment.Right,
                    _ => HorizontalAlignment.Left
                };
                //return DomainUpDown.TextAlign;
            }
            set
            {
                StateCommon.Content.TextH = value switch
                {
                    HorizontalAlignment.Right => PaletteRelativeAlign.Far,
                    HorizontalAlignment.Center => PaletteRelativeAlign.Center,
                    _ => PaletteRelativeAlign.Near
                };
                DomainUpDown.TextAlign = value;
            }
        }

        /// <summary>
        /// Gets or sets how the up-down control will position the up down buttons relative to its text box.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Indicates how the up-down control will position the up down buttons relative to its text box.")]
        [DefaultValue(typeof(LeftRightAlignment), nameof(Right))]
        [Localizable(true)]
        public LeftRightAlignment UpDownAlign
        {
            get => DomainUpDown.UpDownAlign;
            set => DomainUpDown.UpDownAlign = value;
        }

        /// <summary>
        /// Gets or sets whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Indicates whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.")]
        [DefaultValue(true)]
        public bool InterceptArrowKeys
        {
            get => DomainUpDown.InterceptArrowKeys;
            set => DomainUpDown.InterceptArrowKeys = value;
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
            get => DomainUpDown.ReadOnly;
            set => DomainUpDown.ReadOnly = value;
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
        /// Gets and sets the up and down buttons style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Up and down buttons style.")]
        public ButtonStyle UpDownButtonStyle
        {
            get => _upDownButtonStyle;

            set
            {
                if (_upDownButtonStyle != value)
                {
                    _upDownButtonStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetUpDownButtonStyle() => UpDownButtonStyle = ButtonStyle.InputControl;

        private bool ShouldSerializeUpDownButtonStyle() => UpDownButtonStyle != ButtonStyle.InputControl;

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
        public DomainUpDownButtonSpecCollection ButtonSpecs { get; }

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
        /// Displays the previous item in the collection.
        /// </summary>
        public void UpButton() => DomainUpDown.UpButton();

        /// <summary>
        /// Displays the next item in the collection.
        /// </summary>
        public void DownButton() => DomainUpDown.DownButton();

        /// <summary>
        /// Selects a range of text in the control.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length) => DomainUpDown.Select(start, length);

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
            ContainsFocus || _mouseOver || _domainUpDown.MouseOver ||
            _subclassEdit is { MouseOver: true } ||
            _subclassButtons is { MouseOver: true };

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        /// <returns>true if the input focus request was successful; otherwise, false.</returns>
        public new bool Focus() => DomainUpDown != null && DomainUpDown.Focus();

        /// <summary>
        /// Activates the control.
        /// </summary>
        public new void Select() => DomainUpDown?.Select();

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

                // The inside text box is the client rectangle size
                return new Rectangle(DomainUpDown.Location, DomainUpDown.Size);
            }
        }

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
        public Component DesignerComponentFromPoint(Point pt) =>
            // Ignore call as view builder is already destructed
            IsDisposed ? null : ViewManager.ComponentFromPoint(pt);

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
        /// Gets or sets a value indicating whether a value has been entered by the user.
        /// </summary>
        protected bool UserEdit
        {
            get => _domainUpDown.InternalUserEdit;
            set => _domainUpDown.InternalUserEdit = value;
        }
        #endregion

        #region Protected Virtual
        // ReSharper disable VirtualMemberNeverOverridden.Global
        /// <summary>
        /// Raises the SelectedItemChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnSelectedItemChanged(EventArgs e) => SelectedItemChanged?.Invoke(this, e);

        /// <summary>
        /// Raises the SelectedItemChanged event.
        /// </summary>
        /// <param name="e">A ScrollEventArgs that contains the event data.</param>
        protected virtual void OnScroll(ScrollEventArgs e) => Scroll?.Invoke(this, e);

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

            // Subclass the various child controls of the domain up down control
            UpdateChildEditControl();
            SubclassButtonsControl();

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
            // Change in enabled state requires a layout and repaint
            UpdateStateAndPalettes();

            // Ensure we have subclassed the contained edit control
            UpdateChildEditControl();

            // Update view elements
            _drawDockerInner.Enabled = Enabled;
            _drawDockerOuter.Enabled = Enabled;

            // Update state to reflect change in enabled state
            _buttonManager.RefreshButtons();

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
            base.OnPaletteChanged(e);
        }

        /// <summary>
        /// Processes a notification from palette of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
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
                // Subclass the various child controls of the domain up down control
                SubclassEditControl();
                SubclassButtonsControl();

                // Update to match the new palette settings
                Height = PreferredHeight;

                // Let base class calulcate fill rectangle
                base.OnLayout(levent);

                // Only use layout logic if control is fully initialized or if being forced
                // to allow a relayout or if in design mode.
                if (IsHandleCreated || _forcedLayout || (DesignMode && (_domainUpDown != null)))
                {
                    Rectangle fillRect = _layoutFill.FillRect;
                    _domainUpDown.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
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
            _domainUpDown.Focus();
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
                PaletteState state = _drawDockerOuter.State;
                _domainUpDown.BackColor = triple.PaletteBack.GetBackColor1(state);
                _domainUpDown.ForeColor = triple.PaletteContent.GetContentShortTextColor1(state);

                // Only set the font if the domain up down has been created
                Font font = triple.PaletteContent.GetContentShortTextFont(state);
                if ((_domainUpDown.Handle != IntPtr.Zero) && !_domainUpDown.Font.Equals(font))
                {
                    _domainUpDown.Font = font;
                }
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
            DomainUpDown.TabStop = TabStop;
            base.OnTabStopChanged(e);
        }

        /// <summary>
        /// Raises the CausesValidationChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnCausesValidationChanged(EventArgs e)
        {
            DomainUpDown.CausesValidation = CausesValidation;
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
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>Raises the Paint event.</summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        #endregion

        #region Internal
        internal bool InTransparentDesignMode => InRibbonDesignMode;

        internal bool IsFixedActive => _fixedActive != null;

        #endregion

        #region Implementation
        private void InvalidateChildren()
        {
            if (DomainUpDown != null)
            {
                DomainUpDown.Invalidate();
                PI.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, 0x85);
            }
        }

        private void SubclassEditControl()
        {
            if (_subclassEdit != null)
            {
                if (_domainUpDown.Controls.Count >= 2)
                {
                    if (_subclassEdit.Handle != _domainUpDown.Controls[1].Handle)
                    {
                        _subclassEdit.TrackMouseEnter -= OnDomainUpDownMouseChange;
                        _subclassEdit.TrackMouseLeave -= OnDomainUpDownMouseChange;
                        _subclassEdit.ReleaseHandle();
                        _subclassEdit = null;
                    }
                }
            }

            // Do we need to subclass the edit control
            if (_subclassEdit == null)
            {
                if (_domainUpDown.Controls.Count >= 2)
                {
                    _subclassEdit = new SubclassEdit(_domainUpDown.Controls[1].Handle, this, _domainUpDown);
                    _subclassEdit.TrackMouseEnter += OnDomainUpDownMouseChange;
                    _subclassEdit.TrackMouseLeave += OnDomainUpDownMouseChange;
                }
            }
        }

        private void SubclassButtonsControl()
        {
            if (_subclassButtons != null)
            {
                if (_domainUpDown.Controls.Count >= 1)
                {
                    if (_subclassButtons.Handle != _domainUpDown.Controls[0].Handle)
                    {
                        _subclassButtons.TrackMouseEnter -= OnDomainUpDownMouseChange;
                        _subclassButtons.TrackMouseLeave -= OnDomainUpDownMouseChange;
                        _subclassButtons.ReleaseHandle();
                        _subclassButtons = null;
                    }
                }
            }

            if (_subclassButtons == null)
            {
                if (_domainUpDown.Controls.Count >= 1)
                {
                    _subclassButtons = new SubclassButtons(_domainUpDown.Controls[0].Handle, this, _domainUpDown);
                    _subclassButtons.TrackMouseEnter += OnDomainUpDownMouseChange;
                    _subclassButtons.TrackMouseLeave += OnDomainUpDownMouseChange;
                }
            }
        }

        private void UpdateChildEditControl() => SubclassEditControl();

        private void UpdateStateAndPalettes()
        {
            // Get the correct palette settings to use
            IPaletteTriple tripleState = GetTripleState();
            _drawDockerOuter.SetPalettes(tripleState.PaletteBack, tripleState.PaletteBorder);

            // Update enabled state
            _drawDockerOuter.Enabled = Enabled;

            // Find the new state of the main view element
            PaletteState state = IsActive ? PaletteState.Tracking : PaletteState.Normal;

            _drawDockerOuter.ElementState = state;
        }

        internal PaletteInputControlTripleStates GetTripleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

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

        private void OnDomainUpDownTextChanged(object sender, EventArgs e) => OnTextChanged(e);

        private void OnDomainUpDownScroll(object sender, ScrollEventArgs e) => OnScroll(e);

        private void OnDomainUpDownSelectedItemChanged(object sender, EventArgs e) => OnSelectedItemChanged(e);

        private void OnDomainUpDownGotFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            InvalidateChildren();
            base.OnGotFocus(e);
        }

        private void OnDomainUpDownLostFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            InvalidateChildren();
            // ReSharper disable RedundantBaseQualifier
            base.OnLostFocus(e);
            // ReSharper restore RedundantBaseQualifier
        }

        private void OnDomainUpDownKeyPress(object sender, KeyPressEventArgs e) => OnKeyPress(e);

        private void OnDomainUpDownKeyUp(object sender, KeyEventArgs e) => OnKeyUp(e);

        private void OnDomainUpDownKeyDown(object sender, KeyEventArgs e) => OnKeyDown(e);

        private void OnDomainUpDownPreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

        private void OnDomainUpDownValidated(object sender, EventArgs e) => OnValidated(e);

        private void OnDomainUpDownValidating(object sender, CancelEventArgs e) => OnValidating(e);

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
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
                    LabelStyle toolTipStyle = LabelStyle.ToolTip;

                    bool shadow = true;

                    // Find the button spec associated with the tooltip request
                    ButtonSpec buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

                    // If the tooltip is for a button spec
                    if (buttonSpec != null)
                    {
                        // Are we allowed to show page related tooltips
                        if (AllowButtonSpecToolTips)
                        {
                            // Create a helper object to provide tooltip values
                            ButtonSpecToContent buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

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

        private void OnCancelToolTip(object sender, EventArgs e) =>
            // Remove any currently showing tooltip
            _visualPopupToolTip?.Dispose();

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }

        private void OnDomainUpDownMouseChange(object sender, EventArgs e)
        {
            // Find new tracking mouse change state
            var tracking = _domainUpDown.MouseOver ||
                           _subclassEdit is { MouseOver: true } ||
                           _subclassButtons is { MouseOver: true };

            // Change in tracking state?
            if (tracking != _trackingMouseEnter)
            {
                _trackingMouseEnter = tracking;
                InvalidateChildren();

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

        private void SetCornerRoundingRadius(float? radius)
        {
            _cornerRoundingRadius = radius ?? GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;

            StateCommon.Border.Rounding = _cornerRoundingRadius;
        }

        #endregion
    }
}
