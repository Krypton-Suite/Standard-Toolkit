#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

// ReSharper disable CompareOfFloatsByEqualityOperator
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// A horizontal scrollbar control with Krypton theming.
/// </summary>
[Designer(typeof(KryptonHScrollBarDesigner))]
[DefaultEvent(nameof(Scroll))]
[DefaultProperty(nameof(Value))]
[ToolboxBitmap(typeof(HScrollBar), "ToolboxBitmaps.KryptonHorizontalScrollBar.bmp")]
[DesignerCategory(@"code")]
[Description(@"A horizontal scrollbar control with Krypton theming.")]
public class KryptonHScrollBar : Control
{
    #region Instance Fields

    /// <summary>
    /// Indicates many changes to the scrollbar are happening, so stop painting till finished.
    /// </summary>
    private bool _inUpdate;

    private Rectangle _clickedBarRectangle;
    private Rectangle _thumbRectangle;
    private Rectangle _leftArrowRectangle;
    private Rectangle _rightArrowRectangle;
    private Rectangle _channelRectangle;
    private bool _leftArrowClicked;
    private bool _rightArrowClicked;
    private bool _leftBarClicked;
    private bool _rightBarClicked;
    private bool _thumbClicked;
    private ScrollBarState _thumbState = ScrollBarState.Normal;
    private ScrollBarArrowButtonState _leftButtonState = ScrollBarArrowButtonState.UpNormal;
    private ScrollBarArrowButtonState _rightButtonState = ScrollBarArrowButtonState.DownNormal;
    private int _minimum;
    private int _maximum;
    private int _smallChange = 1;
    private int _largeChange = 10;
    private int _value;
    private int _thumbWidth;
    private int _thumbHeight = 15;
    private int _arrowWidth = 17;
    private int _arrowHeight = 15;
    private int _thumbRightLimitRight;
    private int _thumbRightLimitLeft;
    private int _thumbLeftLimit;
    private int _thumbPosition;
    private int _trackPosition;

    /// <summary>
    /// The progress timer for moving the thumb.
    /// </summary>
    private readonly Timer _progressTimer = new Timer();

    private Color _borderColor = Color.FromArgb(93, 140, 201);
    private Color _disabledBorderColor = Color.Gray;

    private PaletteBase? _palette;

    private readonly PaletteRedirect? _paletteRedirect;
    private readonly PaletteInputControlTripleRedirect _stateCommon;
    private readonly PaletteInputControlTripleStates _stateNormal;
    private readonly PaletteInputControlTripleStates _stateDisabled;
    private readonly PaletteInputControlTripleStates _stateActive;

    #region Context Menu Items

    private KryptonContextMenu? _kryptonContextMenu;
    private KryptonContextMenuItem _kcmScrollHere;
    private KryptonContextMenuSeparator _kcmSeparator1;
    private KryptonContextMenuItem _kcmLeft;
    private KryptonContextMenuItem _kcmRight;
    private KryptonContextMenuSeparator _kcmSeparator2;
    private KryptonContextMenuItem _kcmLargeLeft;
    private KryptonContextMenuItem _kcmLargeRight;
    private KryptonContextMenuSeparator _kcmSeparator3;
    private KryptonContextMenuItem _kcmSmallLeft;
    private KryptonContextMenuItem _kcmSmallRight;

    #endregion

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonHScrollBar"/> class.
    /// </summary>
    public KryptonHScrollBar()
    {
        // sets the control styles of the control
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                                                | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint
                                                | ControlStyles.UserPaint, true);

        Width = 200;
        Height = 19;

        // sets the scrollbar up
        SetUpScrollBar();

        if (_palette != null)
        {
            _palette.PalettePaintInternal += OnPalettePaint;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        _palette = KryptonManager.CurrentGlobalPalette;

        _paletteRedirect = new PaletteRedirect(_palette);

        // Create the palette provider
        _stateCommon = new PaletteInputControlTripleRedirect(_paletteRedirect, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.LabelNormalPanel, OnNeedPaint);
        _stateDisabled = new PaletteInputControlTripleStates(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteInputControlTripleStates(_stateCommon, OnNeedPaint);
        _stateActive = new PaletteInputControlTripleStates(_stateCommon, OnNeedPaint);

        // Initialize palette border colors with default values
        _stateNormal.Border.Color1 = _borderColor;
        _stateActive.Border.Color1 = _borderColor;
        _stateDisabled.Border.Color1 = _disabledBorderColor;

        // Ensure border is drawn by default (all palette border properties are accessible through StateCommon/StateNormal/StateDisabled/StateActive.Border)
        // Available properties: Draw, DrawBorders, Width, Color1, Color2, ColorStyle, ColorAlign, ColorAngle, Rounding, Image, ImageStyle, ImageAlign, GraphicsHint

        // timer for clicking and holding the mouse button
        // over/below the thumb and on the arrow buttons
        _progressTimer.Interval = 20;
        _progressTimer.Tick += ProgressTimerTick;

        // initializes the context menu
        InitializeComponent();

        _maximum = 100;
        _minimum = 0;
        _value = 0;
    }

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the scrollbar scrolled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Is raised, when the scrollbar was scrolled.")]
    public event ScrollEventHandler? Scroll;
    #endregion

    #region Public

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text { get; set; }

    /// <summary>
    /// Gets or sets the minimum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the minimum value.")]
    [DefaultValue(0)]
    public int Minimum
    {
        get => _minimum;

        set
        {
            // no change or value invalid - return
            if (_minimum == value || value < 0 || value >= _maximum)
            {
                return;
            }

            _minimum = value;

            // is current large change value invalid - adjust
            if (_largeChange > _maximum - _minimum)
            {
                _largeChange = _maximum - _minimum;
            }

            SetUpScrollBar();

            // current value less than new minimum value - adjust
            if (_value < value)
            {
                _value = value;
                ChangeThumbPosition(GetThumbPosition());
                Refresh();
            }
            else
            {
                // current value is valid - adjust thumb position
                ChangeThumbPosition(GetThumbPosition());

                Refresh();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the maximum value.")]
    [DefaultValue(100)]
    public int Maximum
    {
        get => _maximum;

        set
        {
            // no change or new max. value invalid - return
            if (value == _maximum || value < 1 || value <= _minimum)
            {
                return;
            }

            _maximum = value;

            // is large change value invalid - adjust
            if (_largeChange > _maximum - _minimum)
            {
                _largeChange = _maximum - _minimum;
            }

            SetUpScrollBar();

            // is current value greater than new maximum value - adjust
            int scrollableMaximum = GetScrollableMaximum();
            if (_value > scrollableMaximum)
            {
                _value = scrollableMaximum;
                ChangeThumbPosition(GetThumbPosition());
                Refresh();
            }
            else
            {
                // current value is valid - adjust thumb position
                ChangeThumbPosition(GetThumbPosition());

                Refresh();
            }
        }
    }

    /// <summary>
    /// Gets or sets the small change amount.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the small change value.")]
    [DefaultValue(1)]
    public int SmallChange
    {
        get => _smallChange;

        set
        {
            // no change or new small change value invalid - return
            if (value == _smallChange || value < 1 || value >= _largeChange)
            {
                return;
            }

            _smallChange = value;

            SetUpScrollBar();
        }
    }

    /// <summary>
    /// Gets or sets the large change amount.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the large change value.")]
    [DefaultValue(10)]
    public int LargeChange
    {
        get => _largeChange;

        set
        {
            // no change or new large change value is invalid - return
            if (value == _largeChange || value < _smallChange || value < 2)
            {
                return;
            }

            // if value is greater than scroll area - adjust
            if (value > _maximum - _minimum)
            {
                _largeChange = _maximum - _minimum;
            }
            else
            {
                // set new value
                _largeChange = value;
            }

            _value = CoerceValue(_value);
            SetUpScrollBar();
        }
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the current value.")]
    [DefaultValue(0)]
    public int Value
    {
        get => _value;

        set
        {
            // no change or invalid value - return
            if (value < _minimum || value > _maximum)
            {
                return;
            }

            int newValue = CoerceValue(value);
            if (_value == newValue)
            {
                return;
            }

            _value = newValue;

            // adjust thumb position
            ChangeThumbPosition(GetThumbPosition());

            // raise scroll event
            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, -1, newValue, ScrollOrientation.HorizontalScroll));

            Refresh();
        }
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
    /// Gets or sets the border color.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category(@"Appearance")]
    [Description(@"Gets or sets the border color.")]
    [DefaultValue(typeof(Color), "Color.FromARGB(93, 140, 201)")]
    public Color BorderColor
    {
        get => _borderColor;

        set
        {
            if (_borderColor != value)
            {
                _borderColor = value;

                // Sync with palette system
                _stateNormal.Border.Color1 = value;
                _stateActive.Border.Color1 = value;

                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the border color in disabled state.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category(@"Appearance")]
    [Description(@"Gets or sets the border color in disabled state.")]
    [DefaultValue(typeof(Color), "Color.Gray")]
    public Color DisabledBorderColor
    {
        get => _disabledBorderColor;

        set
        {
            if (_disabledBorderColor != value)
            {
                _disabledBorderColor = value;

                // Sync with palette system
                _stateDisabled.Border.Color1 = value;

                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The KryptonContextMenu to show when the user right-clicks the control.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _kryptonContextMenu;

        set
        {
            if (_kryptonContextMenu != value)
            {
                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Disposed -= OnKryptonContextMenuDisposed;
                }

                _kryptonContextMenu = value;

                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Disposed += OnKryptonContextMenuDisposed;
                }
            }
        }
    }

    private void OnKryptonContextMenuDisposed(object? sender, EventArgs e) => _kryptonContextMenu = null;

    /// <summary>
    /// Gets access to the common scrollbar appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common scrollbar appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleRedirect StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled scrollbar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled scrollbar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal scrollbar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal scrollbar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active scrollbar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active scrollbar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateActive => _stateActive;

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    #endregion

    #region Implementation

    #region Public Methods

    /// <summary>
    /// Prevents the drawing of the control until <see cref="EndUpdate"/> is called.
    /// </summary>
    public void BeginUpdate()
    {
        PI.SendMessage(Handle, PI.SETREDRAW, (IntPtr)0/*false*/, IntPtr.Zero);
        _inUpdate = true;
    }

    /// <summary>
    /// Ends the updating process and the control can draw itself again.
    /// </summary>
    public void EndUpdate()
    {
        PI.SendMessage(Handle, PI.SETREDRAW, (IntPtr)1/*true*/, IntPtr.Zero);
        _inUpdate = false;
        SetUpScrollBar();
        Refresh();
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Raises the <see cref="Scroll"/> event.
    /// </summary>
    /// <param name="e">The <see cref="ScrollEventArgs"/> that contains the event data.</param>
    protected virtual void OnScroll(ScrollEventArgs e) =>
        // if event handler is attached - raise scroll event
        Scroll?.Invoke(this, e);

    /// <summary>
    /// Paints the background of the control.
    /// </summary>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains information about the control to paint.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // no painting here
    }

    /// <summary>
    /// Paints the control.
    /// </summary>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains information about the control to paint.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        // Get border rounding for clipping the control shape
        float borderRounding = 0;
        if (_palette != null)
        {
            IRenderer renderer = _palette.GetRenderer();
            PaletteState borderState = Enabled ? PaletteState.Normal : PaletteState.Disabled;
            IPaletteBorder paletteBorder = borderState == PaletteState.Disabled
                ? _stateDisabled.PaletteBorder
                : _stateNormal.PaletteBorder;
            borderRounding = paletteBorder.GetBorderRounding(borderState);

            // Ensure rounding doesn't exceed what can fit in the control
            if (borderRounding > 0)
            {
                // Clamp rounding to fit within the control bounds
                borderRounding = Math.Min(borderRounding, Math.Min(ClientRectangle.Width / 2f, ClientRectangle.Height / 2f));
            }
        }

        // Apply clipping to round the control corners if rounding is specified
        Region? originalClip = null;
        if (borderRounding > 0 && ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
        {
            originalClip = e.Graphics.Clip?.Clone();
            using GraphicsPath roundedPath = CommonHelper.RoundedRectanglePath(ClientRectangle, (int)Math.Round(borderRounding));
            e.Graphics.SetClip(roundedPath, CombineMode.Replace);
        }

        // sets the smoothing mode to none
        using var gh = new GraphicsHint(e.Graphics, PaletteGraphicsHint.None);

        // save client rectangle
        Rectangle rect = ClientRectangle;

        // Get DPI scaling factor
        float dpiFactor = GetDpiFactor();
        int borderOffset = (int)Math.Round(1 * dpiFactor);

        // adjust the rectangle for horizontal scrollbar
        rect.X += _arrowWidth + borderOffset;
        rect.Y += borderOffset;
        rect.Width -= (_arrowWidth * 2) + (borderOffset * 2);
        rect.Height -= borderOffset * 2;

        KryptonScrollBarRenderer.InitColors();

        // draws the background
        KryptonScrollBarRenderer.DrawBackground(
            e.Graphics,
            ClientRectangle,
            ScrollBarOrientation.Horizontal);

        // draws the track
        KryptonScrollBarRenderer.DrawTrack(
            e.Graphics,
            rect,
            ScrollBarState.Normal,
            ScrollBarOrientation.Horizontal);

        // draw thumb and grip
        KryptonScrollBarRenderer.DrawThumb(
            e.Graphics,
            _thumbRectangle,
            _thumbState,
            ScrollBarOrientation.Horizontal);

        if (Enabled)
        {
            KryptonScrollBarRenderer.DrawThumbGrip(
                e.Graphics,
                _thumbRectangle,
                ScrollBarOrientation.Horizontal);
        }

        // draw arrows
        KryptonScrollBarRenderer.DrawArrowButton(
            e.Graphics,
            _leftArrowRectangle,
            _leftButtonState,
            true,
            ScrollBarOrientation.Horizontal);

        KryptonScrollBarRenderer.DrawArrowButton(
            e.Graphics,
            _rightArrowRectangle,
            _rightButtonState,
            false,
            ScrollBarOrientation.Horizontal);

        // check if left or right bar was clicked
        if (_leftBarClicked)
        {
            _clickedBarRectangle.X = _thumbLeftLimit;
            _clickedBarRectangle.Width =
                _thumbRectangle.X - _thumbLeftLimit;

            KryptonScrollBarRenderer.DrawTrack(
                e.Graphics,
                _clickedBarRectangle,
                ScrollBarState.Pressed,
                ScrollBarOrientation.Horizontal);
        }
        else if (_rightBarClicked)
        {
            int borderOffsetS = (int)Math.Round(1 * GetDpiFactor());
            _clickedBarRectangle.X = _thumbRectangle.Right + borderOffsetS;
            _clickedBarRectangle.Width =
                _thumbRightLimitRight - _clickedBarRectangle.X + borderOffsetS;

            KryptonScrollBarRenderer.DrawTrack(
                e.Graphics,
                _clickedBarRectangle,
                ScrollBarState.Pressed,
                ScrollBarOrientation.Horizontal);
        }

        // Restore original clip region before drawing border (border renderer handles its own clipping)
        if (originalClip != null)
        {
            Region? oldClip = e.Graphics.Clip;
            e.Graphics.Clip = originalClip;
            oldClip?.Dispose();
            originalClip = null;
        }

        // draw border with rounding support
        if (_palette != null)
        {
            IRenderer renderer = _palette.GetRenderer();
            using var renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, renderer);

            // Determine the appropriate palette state
            PaletteState borderState = Enabled ? PaletteState.Normal : PaletteState.Disabled;

            // Get the appropriate border palette
            IPaletteBorder paletteBorder = borderState == PaletteState.Disabled
                ? _stateDisabled.PaletteBorder
                : _stateNormal.PaletteBorder;

            // Render the border with rounding support
            renderer.RenderStandardBorder.DrawBorder(
                renderContext,
                ClientRectangle,
                paletteBorder,
                VisualOrientation.Top,
                borderState);
        }
        else
        {
            // Fallback to simple border if no palette available
            using var pen = new Pen(Enabled ? KryptonScrollBarRenderer.BorderColors[0] : _disabledBorderColor);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }
    }

    /// <summary>
    /// Raises the MouseDown event.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        Focus();

        switch (e.Button)
        {
            case MouseButtons.Left:
                {

                    Point mouseLocation = e.Location;

                    if (_thumbRectangle.Contains(mouseLocation))
                    {
                        _thumbClicked = true;
                        _thumbPosition = mouseLocation.X - _thumbRectangle.X;
                        _thumbState = ScrollBarState.Pressed;

                        Invalidate(_thumbRectangle);
                    }
                    else if (_leftArrowRectangle.Contains(mouseLocation))
                    {
                        _leftArrowClicked = true;
                        _leftButtonState = ScrollBarArrowButtonState.UpPressed;

                        Invalidate(_leftArrowRectangle);

                        ProgressThumb(true);
                    }
                    else if (_rightArrowRectangle.Contains(mouseLocation))
                    {
                        _rightArrowClicked = true;
                        _rightButtonState = ScrollBarArrowButtonState.DownPressed;

                        Invalidate(_rightArrowRectangle);

                        ProgressThumb(true);
                    }
                    else
                    {
                        _trackPosition = mouseLocation.X;

                        if (_trackPosition < _thumbRectangle.X)
                        {
                            _leftBarClicked = true;
                        }
                        else
                        {
                            _rightBarClicked = true;
                        }

                        ProgressThumb(true);
                    }

                    break;
                }
            case MouseButtons.Right:
                _trackPosition = e.X;
                // Show KryptonContextMenu if available
                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Show(this, PointToScreen(e.Location));
                }
                break;
        }
    }

    /// <summary>
    /// Raises the MouseUp event.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (e.Button == MouseButtons.Left)
        {
            if (_thumbClicked)
            {
                _thumbClicked = false;
                _thumbState = ScrollBarState.Normal;

                OnScroll(new ScrollEventArgs(
                    ScrollEventType.EndScroll,
                    -1,
                    _value,
                    ScrollOrientation.HorizontalScroll)
                );
            }
            else if (_leftArrowClicked)
            {
                _leftArrowClicked = false;
                _leftButtonState = ScrollBarArrowButtonState.UpNormal;
                StopTimer();
            }
            else if (_rightArrowClicked)
            {
                _rightArrowClicked = false;
                _rightButtonState = ScrollBarArrowButtonState.DownNormal;
                StopTimer();
            }
            else if (_leftBarClicked)
            {
                _leftBarClicked = false;
                StopTimer();
            }
            else if (_rightBarClicked)
            {
                _rightBarClicked = false;
                StopTimer();
            }

            Invalidate();
        }
    }

    /// <summary>
    /// Raises the MouseEnter event.
    /// </summary>
    /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);

        _rightButtonState = ScrollBarArrowButtonState.DownActive;
        _leftButtonState = ScrollBarArrowButtonState.UpActive;
        _thumbState = ScrollBarState.Active;

        Invalidate();
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        ResetScrollStatus();
    }

    /// <summary>
    /// Raises the MouseMove event.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        // moving and holding the left mouse button
        if (e.Button == MouseButtons.Left)
        {
            // Update the thumb position, if the new location is within the bounds.
            if (_thumbClicked)
            {
                var oldScrollValue = _value;

                _leftButtonState = ScrollBarArrowButtonState.UpActive;
                _rightButtonState = ScrollBarArrowButtonState.DownActive;
                var pos = e.Location.X;

                // The thumb is all the way to the left
                if (pos <= (_thumbLeftLimit + _thumbPosition))
                {
                    ChangeThumbPosition(_thumbLeftLimit);

                    _value = _minimum;
                }
                else if (pos >= (_thumbRightLimitLeft + _thumbPosition))
                {
                    // The thumb is all the way to the right
                    _value = GetScrollableMaximum();
                }
                else
                {
                    _value = GetValueFromThumbLeft(pos - _thumbPosition);
                }

                // raise scroll event if new value different
                if (oldScrollValue != _value)
                {
                    ChangeThumbPosition(GetThumbPosition());
                    OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, oldScrollValue, _value, ScrollOrientation.HorizontalScroll));

                    Refresh();
                }
            }
        }
        else if (!ClientRectangle.Contains(e.Location))
        {
            ResetScrollStatus();
        }
        else if (e.Button == MouseButtons.None) // only moving the mouse
        {
            if (_leftArrowRectangle.Contains(e.Location))
            {
                _leftButtonState = ScrollBarArrowButtonState.UpHot;

                Invalidate(_leftArrowRectangle);
            }
            else if (_rightArrowRectangle.Contains(e.Location))
            {
                _rightButtonState = ScrollBarArrowButtonState.DownHot;

                Invalidate(_rightArrowRectangle);
            }
            else if (_thumbRectangle.Contains(e.Location))
            {
                _thumbState = ScrollBarState.Hot;

                Invalidate(_thumbRectangle);
            }
            else if (ClientRectangle.Contains(e.Location))
            {
                _leftButtonState = ScrollBarArrowButtonState.UpActive;
                _rightButtonState = ScrollBarArrowButtonState.DownActive;
                _thumbState = ScrollBarState.Active;

                Invalidate();
            }
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.SizeChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        SetUpScrollBar();
    }

    /// <summary>
    /// Processes a dialog key.
    /// </summary>
    /// <param name="keyData">One of the <see cref="Keys"/> values that represents the key to process.</param>
    /// <returns>true, if the key was processed by the control, false otherwise.</returns>
    protected override bool ProcessDialogKey(Keys keyData)
    {
        // key handling is here - keys recognized by the control
        // Left&Right, PageUp, PageDown, Home, End, Ctrl+Home, Ctrl+End
        if (keyData == Keys.Left)
        {
            Value -= _smallChange;

            return true;
        }

        if (keyData == Keys.Right)
        {
            Value += _smallChange;

            return true;
        }

        switch (keyData)
        {
            case Keys.PageUp:
                Value = GetValue(false, true);

                return true;
            case Keys.PageDown:
            {
                int scrollableMaximum = GetScrollableMaximum();
                if (_value + _largeChange > scrollableMaximum)
                {
                    Value = scrollableMaximum;
                }
                else
                {
                    Value += _largeChange;
                }

                return true;
            }
            case Keys.Home:
            case Keys.Control | Keys.Home:
                Value = _minimum;

                return true;
            case Keys.End:
            case Keys.Control | Keys.End:
                Value = GetScrollableMaximum();

                return true;
            default:
                return base.ProcessDialogKey(keyData);
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.EnabledChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);

        if (Enabled)
        {
            _thumbState = ScrollBarState.Normal;
            _leftButtonState = ScrollBarArrowButtonState.UpNormal;
            _rightButtonState = ScrollBarArrowButtonState.DownNormal;
        }
        else
        {
            _thumbState = ScrollBarState.Disabled;
            _leftButtonState = ScrollBarArrowButtonState.UpDisabled;
            _rightButtonState = ScrollBarArrowButtonState.DownDisabled;
        }

        Refresh();
    }

    #endregion

    #region Misc Methods

    /// <summary>
    /// Gets the DPI scaling factor for the control.
    /// </summary>
    /// <returns>The DPI scaling factor (DeviceDpi / 96). Returns 1.0 if DPI cannot be determined.</returns>
    private float GetDpiFactor()
    {
        if (IsHandleCreated && DeviceDpi > 0)
        {
            return DeviceDpi / 96F;
        }

        // Fallback: use Graphics DPI if handle not created yet
        try
        {
            using Graphics g = CreateGraphics();
            return g.DpiX / 96F;
        }
        catch
        {
            return 1.0F;
        }
    }

    /// <summary>
    /// Sets up the scrollbar.
    /// </summary>
    private void SetUpScrollBar()
    {
        // if no drawing - return
        if (_inUpdate)
        {
            return;
        }

        // Get DPI scaling factor
        float dpiFactor = GetDpiFactor();

        // set up the width's, height's and rectangles for horizontal scrollbar
        // Scale dimensions based on DPI
        _arrowHeight = (int)Math.Round(15 * dpiFactor);
        _arrowWidth = (int)Math.Round(17 * dpiFactor);
        _thumbHeight = (int)Math.Round(15 * dpiFactor);
        _thumbWidth = GetThumbSize();

        int borderOffset = (int)Math.Round(1 * dpiFactor);
        int verticalOffset = (int)Math.Round(2 * dpiFactor);

        _clickedBarRectangle = ClientRectangle;
        _clickedBarRectangle.Inflate(-borderOffset, -borderOffset);
        _clickedBarRectangle.X += _arrowWidth;
        _clickedBarRectangle.Width -= _arrowWidth * 2;

        _channelRectangle = _clickedBarRectangle;

        _thumbRectangle = new Rectangle(
            ClientRectangle.X + _arrowWidth + borderOffset,
            ClientRectangle.Y + verticalOffset,
            _thumbWidth,
            _thumbHeight - borderOffset
        );

        _leftArrowRectangle = new Rectangle(
            ClientRectangle.X + borderOffset,
            ClientRectangle.Y + verticalOffset,
            _arrowWidth,
            _arrowHeight
        );

        _rightArrowRectangle = new Rectangle(
            ClientRectangle.Right - _arrowWidth - borderOffset,
            ClientRectangle.Y + verticalOffset,
            _arrowWidth,
            _arrowHeight
        );

        // Set the default starting thumb position.
        _thumbPosition = _thumbRectangle.Width / 2;

        // Set the right limit of the thumb's right border.
        _thumbRightLimitRight =
            ClientRectangle.Right - _arrowWidth - borderOffset;

        // Set the right limit of the thumb's left border.
        _thumbRightLimitLeft =
            _thumbRightLimitRight - _thumbRectangle.Width;

        // Set the left limit of the thumb's left border.
        _thumbLeftLimit = ClientRectangle.X + _arrowWidth + borderOffset;

        ChangeThumbPosition(GetThumbPosition());

        Refresh();
    }

    /// <summary>
    /// Handles the updating of the thumb.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">An object that contains the event data.</param>
    private void ProgressTimerTick(object? sender, EventArgs e) => ProgressThumb(true);

    /// <summary>
    /// Resets the scroll status of the scrollbar.
    /// </summary>
    private void ResetScrollStatus()
    {
        // get current mouse position
        Point pos = PointToClient(Cursor.Position);

        // set appearance of buttons in relation to where the mouse is -
        // outside or inside the control
        if (ClientRectangle.Contains(pos))
        {
            _rightButtonState = ScrollBarArrowButtonState.DownActive;
            _leftButtonState = ScrollBarArrowButtonState.UpActive;
        }
        else
        {
            _rightButtonState = ScrollBarArrowButtonState.DownNormal;
            _leftButtonState = ScrollBarArrowButtonState.UpNormal;
        }

        // set appearance of thumb
        _thumbState = _thumbRectangle.Contains(pos) ?
            ScrollBarState.Hot : ScrollBarState.Normal;

        _rightArrowClicked = _rightBarClicked =
            _leftArrowClicked = _leftBarClicked = false;

        StopTimer();

        Refresh();
    }

    /// <summary>
    /// Calculates the new value of the scrollbar.
    /// </summary>
    /// <param name="smallIncrement">true for a small change, false otherwise.</param>
    /// <param name="left">true for left movement, false otherwise.</param>
    /// <returns>The new scrollbar value.</returns>
    private int GetValue(bool smallIncrement, bool left)
    {
        int newValue;
        int scrollableMaximum = GetScrollableMaximum();

        // calculate the new value of the scrollbar
        // with checking if new value is in bounds (min/max)
        if (left)
        {
            newValue = _value - (smallIncrement ? _smallChange : _largeChange);

            if (newValue < _minimum)
            {
                newValue = _minimum;
            }
        }
        else
        {
            newValue = _value + (smallIncrement ? _smallChange : _largeChange);

            if (newValue > scrollableMaximum)
            {
                newValue = scrollableMaximum;
            }
        }

        return newValue;
    }

    private int GetScrollableMaximum()
    {
        int page = Math.Max(1, _largeChange);
        long scrollableMaximum = (long)_maximum - page + 1;

        if (scrollableMaximum < _minimum)
        {
            return _minimum;
        }

        return scrollableMaximum > int.MaxValue ? int.MaxValue : (int)scrollableMaximum;
    }

    private int CoerceValue(int value) => Math.Max(_minimum, Math.Min(value, GetScrollableMaximum()));

    private int GetThumbTravelRange() => Math.Max(0, _thumbRightLimitLeft - _thumbLeftLimit);

    private int GetValueFromThumbLeft(int thumbLeft)
    {
        int pixelRange = GetThumbTravelRange();
        int scrollableMaximum = GetScrollableMaximum();
        int realRange = scrollableMaximum - _minimum;

        if (pixelRange == 0 || realRange == 0)
        {
            return _minimum;
        }

        int clampedThumbLeft = Math.Max(_thumbLeftLimit, Math.Min(thumbLeft, _thumbRightLimitLeft));
        float percent = (clampedThumbLeft - _thumbLeftLimit) / (float)pixelRange;

        return CoerceValue(Convert.ToInt32((percent * realRange) + _minimum));
    }

    /// <summary>
    /// Calculates the new thumb position.
    /// </summary>
    /// <returns>The new thumb position.</returns>
    private int GetThumbPosition()
    {
        var pixelRange = GetThumbTravelRange();
        var realRange = GetScrollableMaximum() - _minimum;
        var perc = 0f;

        if (realRange != 0)
        {
            perc = (_value - _minimum) / (float)realRange;
        }

        return Math.Max(_thumbLeftLimit, Math.Min(
            _thumbRightLimitLeft,
            Convert.ToInt32((perc * pixelRange) + _thumbLeftLimit)));
    }

    /// <summary>
    /// Calculates the width of the thumb.
    /// </summary>
    /// <returns>The width of the thumb.</returns>
    private int GetThumbSize()
    {
        int borderOffset = (int)Math.Round(1 * GetDpiFactor());
        var trackSize = Math.Max(0, Width - (2 * _arrowWidth) - (borderOffset * 2));

        if (_maximum == 0 || _largeChange == 0)
        {
            return trackSize;
        }

        int contentRange = Math.Max(1, _maximum - _minimum + 1);
        var newThumbSize = _largeChange * trackSize / (float)contentRange;

        return Convert.ToInt32(Math.Min(trackSize, Math.Max(newThumbSize, 10f)));
    }

    /// <summary>
    /// Enables the timer.
    /// </summary>
    private void EnableTimer()
    {
        // if timer is not already enabled - enable it
        if (!_progressTimer.Enabled)
        {
            _progressTimer.Interval = 600;
            _progressTimer.Start();
        }
        else
        {
            // if already enabled, change tick time
            _progressTimer.Interval = 10;
        }
    }

    /// <summary>
    /// Stops the progress timer.
    /// </summary>
    private void StopTimer() => _progressTimer.Stop();

    /// <summary>
    /// Changes the position of the thumb.
    /// </summary>
    /// <param name="position">The new position.</param>
    private void ChangeThumbPosition(int position)
    {
        _thumbRectangle.X = Math.Max(_thumbLeftLimit, Math.Min(position, _thumbRightLimitLeft));
    }

    /// <summary>
    /// Controls the movement of the thumb.
    /// </summary>
    /// <param name="enableTimer">true for enabling the timer, false otherwise.</param>
    private void ProgressThumb(bool enableTimer)
    {
        var scrollOldValue = _value;
        var type = ScrollEventType.First;
        var thumbPos = _thumbRectangle.X;
        var thumbSize = _thumbRectangle.Width;

        // arrow right or shaft right clicked
        if (_rightArrowClicked || (_rightBarClicked && (thumbPos + thumbSize) < _trackPosition))
        {
            type = _rightArrowClicked ? ScrollEventType.SmallIncrement : ScrollEventType.LargeIncrement;

            _value = GetValue(_rightArrowClicked, false);

            if (_value == GetScrollableMaximum())
            {
                ChangeThumbPosition(_thumbRightLimitLeft);

                type = ScrollEventType.Last;
            }
            else
            {
                ChangeThumbPosition(Math.Min(_thumbRightLimitLeft, GetThumbPosition()));
            }
        }
        else if (_leftArrowClicked || (_leftBarClicked && thumbPos > _trackPosition))
        {
            type = _leftArrowClicked ? ScrollEventType.SmallDecrement : ScrollEventType.LargeDecrement;

            // arrow left or shaft left clicked
            _value = GetValue(_leftArrowClicked, true);

            if (_value == _minimum)
            {
                ChangeThumbPosition(_thumbLeftLimit);

                type = ScrollEventType.First;
            }
            else
            {
                ChangeThumbPosition(Math.Max(_thumbLeftLimit, GetThumbPosition()));
            }
        }
        else if (!((_leftArrowClicked && thumbPos == _thumbLeftLimit) || (_rightArrowClicked && thumbPos == _thumbRightLimitLeft)))
        {
            ResetScrollStatus();

            return;
        }

        if (scrollOldValue != _value)
        {
            OnScroll(new ScrollEventArgs(type, scrollOldValue, _value, ScrollOrientation.HorizontalScroll));

            Invalidate(_channelRectangle);

            if (enableTimer)
            {
                EnableTimer();
            }
        }
        else
        {
            if (_leftArrowClicked)
            {
                type = ScrollEventType.SmallDecrement;
            }
            else if (_rightArrowClicked)
            {
                type = ScrollEventType.SmallIncrement;
            }

            OnScroll(new ScrollEventArgs(type, _value));
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.PalettePaintInternal -= OnPalettePaint;
        }

        _palette = KryptonManager.CurrentGlobalPalette;

        _paletteRedirect!.Target = _palette;

        if (_palette != null)
        {
            _palette.PalettePaintInternal += OnPalettePaint;

            // Repaint
            KryptonScrollBarRenderer.InitColors();
        }

        Invalidate();
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

    /// <summary>
    /// Handler for palette paint requests.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e) => Invalidate();

    #endregion

    #region Context Menu Methods

    /// <summary>
    /// Initializes the context menu.
    /// </summary>
    private void InitializeComponent()
    {
        // Create KryptonContextMenu items
        _kcmScrollHere = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.ScrollHere, ScrollHereClick);
        _kcmSeparator1 = new KryptonContextMenuSeparator();
        _kcmLeft = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.Left, LeftClick);
        _kcmRight = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.Right, RightClick);
        _kcmSeparator2 = new KryptonContextMenuSeparator();
        _kcmLargeLeft = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.PageLeft, LargeLeftClick);
        _kcmLargeRight = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.PageRight, LargeRightClick);
        _kcmSeparator3 = new KryptonContextMenuSeparator();
        _kcmSmallLeft = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.ScrollLeft, SmallLeftClick);
        _kcmSmallRight = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.ScrollRight, SmallRightClick);

        // Create the KryptonContextMenu
        _kryptonContextMenu = new KryptonContextMenu
        {
            Items =
            {
                _kcmScrollHere,
                _kcmSeparator1,
                _kcmLeft,
                _kcmRight,
                _kcmSeparator2,
                _kcmLargeLeft,
                _kcmLargeRight,
                _kcmSeparator3,
                _kcmSmallLeft,
                _kcmSmallRight
            }
        };
    }

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void ScrollHereClick(object? sender, EventArgs e)
    {
        var oldValue = _value;

        _value = GetValueFromThumbLeft(_trackPosition - (_thumbRectangle.Width / 2));
        ChangeThumbPosition(GetThumbPosition());

        OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, oldValue, _value, ScrollOrientation.HorizontalScroll));

        Refresh();
    }

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void LeftClick(object? sender, EventArgs e) => Value = _minimum;

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void RightClick(object? sender, EventArgs e) => Value = GetScrollableMaximum();

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void LargeLeftClick(object? sender, EventArgs e) => Value = GetValue(false, true);

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void LargeRightClick(object? sender, EventArgs e) => Value = GetValue(false, false);

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void SmallLeftClick(object? sender, EventArgs e) => Value = GetValue(true, true);

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void SmallRightClick(object? sender, EventArgs e) => Value = GetValue(true, false);

    #endregion

    #endregion
}