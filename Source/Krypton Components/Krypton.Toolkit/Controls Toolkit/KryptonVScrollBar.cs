#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

// ReSharper disable CompareOfFloatsByEqualityOperator
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// A vertical scrollbar control with Krypton theming.
/// </summary>
[Designer(typeof(KryptonScrollBarDesigner))]
[DefaultEvent(nameof(Scroll))]
[DefaultProperty(nameof(Value))]
[ToolboxBitmap(typeof(VScrollBar))]
[DesignerCategory(@"code")]
[Description(@"A vertical scrollbar control with Krypton theming.")]
public class KryptonVScrollBar : Control
{
    #region Instance Fields

    /// <summary>
    /// Indicates many changes to the scrollbar are happening, so stop painting till finished.
    /// </summary>
    private bool _inUpdate;

    private Rectangle _clickedBarRectangle;
    private Rectangle _thumbRectangle;
    private Rectangle _topArrowRectangle;
    private Rectangle _bottomArrowRectangle;
    private Rectangle _channelRectangle;
    private bool _topArrowClicked;
    private bool _bottomArrowClicked;
    private bool _topBarClicked;
    private bool _bottomBarClicked;
    private bool _thumbClicked;
    private ScrollBarState _thumbState = ScrollBarState.Normal;
    private ScrollBarArrowButtonState _topButtonState = ScrollBarArrowButtonState.UpNormal;
    private ScrollBarArrowButtonState _bottomButtonState = ScrollBarArrowButtonState.DownNormal;
    private int _minimum;
    private int _maximum;
    private int _smallChange = 1;
    private int _largeChange = 10;
    private int _value;
    private int _thumbWidth = 15;
    private int _thumbHeight;
    private int _arrowWidth = 15;
    private int _arrowHeight = 17;
    private int _thumbBottomLimitBottom;
    private int _thumbBottomLimitTop;
    private int _thumbTopLimit;
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
    private KryptonContextMenuItem _kcmTop;
    private KryptonContextMenuItem _kcmBottom;
    private KryptonContextMenuSeparator _kcmSeparator2;
    private KryptonContextMenuItem _kcmLargeUp;
    private KryptonContextMenuItem _kcmLargeDown;
    private KryptonContextMenuSeparator _kcmSeparator3;
    private KryptonContextMenuItem _kcmSmallUp;
    private KryptonContextMenuItem _kcmSmallDown;

    #endregion

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonVScrollBar"/> class.
    /// </summary>
    public KryptonVScrollBar()
    {
        // sets the control styles of the control
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                                                | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint
                                                | ControlStyles.UserPaint, true);

        Width = 19;
        Height = 200;

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
            if (_value > value)
            {
                _value = _maximum;
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
            if (_value == value || value < _minimum || value > _maximum)
            {
                return;
            }

            _value = value;

            // adjust thumb position
            ChangeThumbPosition(GetThumbPosition());

            // raise scroll event
            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, -1, value, ScrollOrientation.VerticalScroll));

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

        // adjust the rectangle for vertical scrollbar
        rect.X++;
        rect.Y += _arrowHeight + 1;
        rect.Width -= 2;
        rect.Height -= (_arrowHeight * 2) + 2;

        KryptonScrollBarRenderer.InitColors();

        // draws the background
        KryptonScrollBarRenderer.DrawBackground(
            e.Graphics,
            ClientRectangle,
            ScrollBarOrientation.Vertical);

        // draws the track
        KryptonScrollBarRenderer.DrawTrack(
            e.Graphics,
            rect,
            ScrollBarState.Normal,
            ScrollBarOrientation.Vertical);

        // draw thumb and grip
        KryptonScrollBarRenderer.DrawThumb(
            e.Graphics,
            _thumbRectangle,
            _thumbState,
            ScrollBarOrientation.Vertical);

        if (Enabled)
        {
            KryptonScrollBarRenderer.DrawThumbGrip(
                e.Graphics,
                _thumbRectangle,
                ScrollBarOrientation.Vertical);
        }

        // draw arrows
        KryptonScrollBarRenderer.DrawArrowButton(
            e.Graphics,
            _topArrowRectangle,
            _topButtonState,
            true,
            ScrollBarOrientation.Vertical);

        KryptonScrollBarRenderer.DrawArrowButton(
            e.Graphics,
            _bottomArrowRectangle,
            _bottomButtonState,
            false,
            ScrollBarOrientation.Vertical);

        // check if top or bottom bar was clicked
        if (_topBarClicked)
        {
            _clickedBarRectangle.Y = _thumbTopLimit;
            _clickedBarRectangle.Height =
                _thumbRectangle.Y - _thumbTopLimit;

            KryptonScrollBarRenderer.DrawTrack(
                e.Graphics,
                _clickedBarRectangle,
                ScrollBarState.Pressed,
                ScrollBarOrientation.Vertical);
        }
        else if (_bottomBarClicked)
        {
            _clickedBarRectangle.Y = _thumbRectangle.Bottom + 1;
            _clickedBarRectangle.Height =
                _thumbBottomLimitBottom - _clickedBarRectangle.Y + 1;

            KryptonScrollBarRenderer.DrawTrack(
                e.Graphics,
                _clickedBarRectangle,
                ScrollBarState.Pressed,
                ScrollBarOrientation.Vertical);
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

        // Restore original clip region if we modified it
        if (originalClip != null)
        {
            e.Graphics.Clip = originalClip;
            originalClip.Dispose();
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
                        _thumbPosition = mouseLocation.Y - _thumbRectangle.Y;
                        _thumbState = ScrollBarState.Pressed;

                        Invalidate(_thumbRectangle);
                    }
                    else if (_topArrowRectangle.Contains(mouseLocation))
                    {
                        _topArrowClicked = true;
                        _topButtonState = ScrollBarArrowButtonState.UpPressed;

                        Invalidate(_topArrowRectangle);

                        ProgressThumb(true);
                    }
                    else if (_bottomArrowRectangle.Contains(mouseLocation))
                    {
                        _bottomArrowClicked = true;
                        _bottomButtonState = ScrollBarArrowButtonState.DownPressed;

                        Invalidate(_bottomArrowRectangle);

                        ProgressThumb(true);
                    }
                    else
                    {
                        _trackPosition = mouseLocation.Y;

                        if (_trackPosition < _thumbRectangle.Y)
                        {
                            _topBarClicked = true;
                        }
                        else
                        {
                            _bottomBarClicked = true;
                        }

                        ProgressThumb(true);
                    }

                    break;
                }
            case MouseButtons.Right:
                _trackPosition = e.Y;
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
                    ScrollOrientation.VerticalScroll)
                );
            }
            else if (_topArrowClicked)
            {
                _topArrowClicked = false;
                _topButtonState = ScrollBarArrowButtonState.UpNormal;
                StopTimer();
            }
            else if (_bottomArrowClicked)
            {
                _bottomArrowClicked = false;
                _bottomButtonState = ScrollBarArrowButtonState.DownNormal;
                StopTimer();
            }
            else if (_topBarClicked)
            {
                _topBarClicked = false;
                StopTimer();
            }
            else if (_bottomBarClicked)
            {
                _bottomBarClicked = false;
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

        _bottomButtonState = ScrollBarArrowButtonState.DownActive;
        _topButtonState = ScrollBarArrowButtonState.UpActive;
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

                _topButtonState = ScrollBarArrowButtonState.UpActive;
                _bottomButtonState = ScrollBarArrowButtonState.DownActive;
                var pos = e.Location.Y;

                // The thumb is all the way to the top
                if (pos <= (_thumbTopLimit + _thumbPosition))
                {
                    ChangeThumbPosition(_thumbTopLimit);

                    _value = _minimum;
                }
                else if (pos >= (_thumbBottomLimitTop + _thumbPosition))
                {
                    // The thumb is all the way to the bottom
                    ChangeThumbPosition(_thumbBottomLimitTop);

                    _value = _maximum;
                }
                else
                {
                    // The thumb is between the ends of the track.
                    ChangeThumbPosition(pos - _thumbPosition);

                    var pixelRange = Height - (2 * _arrowHeight) - _thumbHeight;
                    var thumbPos = _thumbRectangle.Y;
                    var arrowSize = _arrowHeight;

                    var percent = 0f;

                    if (pixelRange != 0)
                    {
                        // percent of the new position
                        percent = (thumbPos - arrowSize) / (float)pixelRange;
                    }

                    // the new value is somewhere between max and min, starting
                    // at min position
                    _value = Convert.ToInt32((percent * (_maximum - _minimum)) + _minimum);
                }

                // raise scroll event if new value different
                if (oldScrollValue != _value)
                {
                    OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, oldScrollValue, _value, ScrollOrientation.VerticalScroll));

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
            if (_topArrowRectangle.Contains(e.Location))
            {
                _topButtonState = ScrollBarArrowButtonState.UpHot;

                Invalidate(_topArrowRectangle);
            }
            else if (_bottomArrowRectangle.Contains(e.Location))
            {
                _bottomButtonState = ScrollBarArrowButtonState.DownHot;

                Invalidate(_bottomArrowRectangle);
            }
            else if (_thumbRectangle.Contains(e.Location))
            {
                _thumbState = ScrollBarState.Hot;

                Invalidate(_thumbRectangle);
            }
            else if (ClientRectangle.Contains(e.Location))
            {
                _topButtonState = ScrollBarArrowButtonState.UpActive;
                _bottomButtonState = ScrollBarArrowButtonState.DownActive;
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
        // Up&Down, PageUp, PageDown, Home, End
        if (keyData == Keys.Up)
        {
            Value -= _smallChange;

            return true;
        }

        if (keyData == Keys.Down)
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
                    if (_value + _largeChange > _maximum)
                    {
                        Value = _maximum;
                    }
                    else
                    {
                        Value += _largeChange;
                    }

                    return true;
                }
            case Keys.Home:
                Value = _minimum;

                return true;
            case Keys.End:
                Value = _maximum;

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
            _topButtonState = ScrollBarArrowButtonState.UpNormal;
            _bottomButtonState = ScrollBarArrowButtonState.DownNormal;
        }
        else
        {
            _thumbState = ScrollBarState.Disabled;
            _topButtonState = ScrollBarArrowButtonState.UpDisabled;
            _bottomButtonState = ScrollBarArrowButtonState.DownDisabled;
        }

        Refresh();
    }

    #endregion

    #region Misc Methods

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

        // set up the width's, height's and rectangles for vertical scrollbar
        _arrowHeight = 17;
        _arrowWidth = 15;
        _thumbWidth = 15;
        _thumbHeight = GetThumbSize();

        _clickedBarRectangle = ClientRectangle;
        _clickedBarRectangle.Inflate(-1, -1);
        _clickedBarRectangle.Y += _arrowHeight;
        _clickedBarRectangle.Height -= _arrowHeight * 2;

        _channelRectangle = _clickedBarRectangle;

        _thumbRectangle = new Rectangle(
            ClientRectangle.X + 2,
            ClientRectangle.Y + _arrowHeight + 1,
            _thumbWidth - 1,
            _thumbHeight
        );

        _topArrowRectangle = new Rectangle(
            ClientRectangle.X + 2,
            ClientRectangle.Y + 1,
            _arrowWidth,
            _arrowHeight
        );

        _bottomArrowRectangle = new Rectangle(
            ClientRectangle.X + 2,
            ClientRectangle.Bottom - _arrowHeight - 1,
            _arrowWidth,
            _arrowHeight
        );

        // Set the default starting thumb position.
        _thumbPosition = _thumbRectangle.Height / 2;

        // Set the bottom limit of the thumb's bottom border.
        _thumbBottomLimitBottom =
            ClientRectangle.Bottom - _arrowHeight - 2;

        // Set the bottom limit of the thumb's top border.
        _thumbBottomLimitTop =
            _thumbBottomLimitBottom - _thumbRectangle.Height;

        // Set the top limit of the thumb's top border.
        _thumbTopLimit = ClientRectangle.Y + _arrowHeight + 1;

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
            _bottomButtonState = ScrollBarArrowButtonState.DownActive;
            _topButtonState = ScrollBarArrowButtonState.UpActive;
        }
        else
        {
            _bottomButtonState = ScrollBarArrowButtonState.DownNormal;
            _topButtonState = ScrollBarArrowButtonState.UpNormal;
        }

        // set appearance of thumb
        _thumbState = _thumbRectangle.Contains(pos) ?
            ScrollBarState.Hot : ScrollBarState.Normal;

        _bottomArrowClicked = _bottomBarClicked =
            _topArrowClicked = _topBarClicked = false;

        StopTimer();

        Refresh();
    }

    /// <summary>
    /// Calculates the new value of the scrollbar.
    /// </summary>
    /// <param name="smallIncrement">true for a small change, false otherwise.</param>
    /// <param name="up">true for up movement, false otherwise.</param>
    /// <returns>The new scrollbar value.</returns>
    private int GetValue(bool smallIncrement, bool up)
    {
        int newValue;

        // calculate the new value of the scrollbar
        // with checking if new value is in bounds (min/max)
        if (up)
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

            if (newValue > _maximum)
            {
                newValue = _maximum;
            }
        }

        return newValue;
    }

    /// <summary>
    /// Calculates the new thumb position.
    /// </summary>
    /// <returns>The new thumb position.</returns>
    private int GetThumbPosition()
    {
        var pixelRange = Height - (2 * _arrowHeight) - _thumbHeight;
        var arrowSize = _arrowHeight;

        var realRange = _maximum - _minimum;
        var perc = 0f;

        if (realRange != 0)
        {
            perc = (_value - _minimum) / (float)realRange;
        }

        return Math.Max(_thumbTopLimit, Math.Min(
            _thumbBottomLimitTop,
            Convert.ToInt32((perc * pixelRange) + arrowSize)));
    }

    /// <summary>
    /// Calculates the height of the thumb.
    /// </summary>
    /// <returns>The height of the thumb.</returns>
    private int GetThumbSize()
    {
        var trackSize = Height - (2 * _arrowHeight);

        if (_maximum == 0 || _largeChange == 0)
        {
            return trackSize;
        }

        var newThumbSize = _largeChange * trackSize / (float)_maximum;

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
        _thumbRectangle.Y = position;
    }

    /// <summary>
    /// Controls the movement of the thumb.
    /// </summary>
    /// <param name="enableTimer">true for enabling the timer, false otherwise.</param>
    private void ProgressThumb(bool enableTimer)
    {
        var scrollOldValue = _value;
        var type = ScrollEventType.First;
        var thumbPos = _thumbRectangle.Y;
        var thumbSize = _thumbRectangle.Height;

        // arrow down or shaft down clicked
        if (_bottomArrowClicked || (_bottomBarClicked && (thumbPos + thumbSize) < _trackPosition))
        {
            type = _bottomArrowClicked ? ScrollEventType.SmallIncrement : ScrollEventType.LargeIncrement;

            _value = GetValue(_bottomArrowClicked, false);

            if (_value == _maximum)
            {
                ChangeThumbPosition(_thumbBottomLimitTop);

                type = ScrollEventType.Last;
            }
            else
            {
                ChangeThumbPosition(Math.Min(_thumbBottomLimitTop, GetThumbPosition()));
            }
        }
        else if (_topArrowClicked || (_topBarClicked && thumbPos > _trackPosition))
        {
            type = _topArrowClicked ? ScrollEventType.SmallDecrement : ScrollEventType.LargeDecrement;

            // arrow up or shaft up clicked
            _value = GetValue(_topArrowClicked, true);

            if (_value == _minimum)
            {
                ChangeThumbPosition(_thumbTopLimit);

                type = ScrollEventType.First;
            }
            else
            {
                ChangeThumbPosition(Math.Max(_thumbTopLimit, GetThumbPosition()));
            }
        }
        else if (!((_topArrowClicked && thumbPos == _thumbTopLimit) || (_bottomArrowClicked && thumbPos == _thumbBottomLimitTop)))
        {
            ResetScrollStatus();

            return;
        }

        if (scrollOldValue != _value)
        {
            OnScroll(new ScrollEventArgs(type, scrollOldValue, _value, ScrollOrientation.VerticalScroll));

            Invalidate(_channelRectangle);

            if (enableTimer)
            {
                EnableTimer();
            }
        }
        else
        {
            if (_topArrowClicked)
            {
                type = ScrollEventType.SmallDecrement;
            }
            else if (_bottomArrowClicked)
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
        _kcmTop = new KryptonContextMenuItem(nameof(Top), TopClick);
        _kcmBottom = new KryptonContextMenuItem(nameof(Bottom), BottomClick);
        _kcmSeparator2 = new KryptonContextMenuSeparator();
        _kcmLargeUp = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.PageUp, LargeUpClick);
        _kcmLargeDown = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.PageDown, LargeDownClick);
        _kcmSeparator3 = new KryptonContextMenuSeparator();
        _kcmSmallUp = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.ScrollUp, SmallUpClick);
        _kcmSmallDown = new KryptonContextMenuItem(KryptonManager.Strings.ScrollBarStrings.ScrollDown, SmallDownClick);

        // Create the KryptonContextMenu
        _kryptonContextMenu = new KryptonContextMenu
        {
            Items =
            {
                _kcmScrollHere,
                _kcmSeparator1,
                _kcmTop,
                _kcmBottom,
                _kcmSeparator2,
                _kcmLargeUp,
                _kcmLargeDown,
                _kcmSeparator3,
                _kcmSmallUp,
                _kcmSmallDown
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
        var thumbSize = _thumbHeight;
        var arrowSize = _arrowHeight;
        var size = Height;

        ChangeThumbPosition(Math.Max(_thumbTopLimit, Math.Min(_thumbBottomLimitTop, _trackPosition - (_thumbRectangle.Height / 2))));

        var thumbPos = _thumbRectangle.Y;

        var pixelRange = size - (2 * arrowSize) - thumbSize;
        var perc = 0f;

        if (pixelRange != 0)
        {
            perc = (thumbPos - arrowSize) / (float)pixelRange;
        }

        var oldValue = _value;

        _value = Convert.ToInt32((perc * (_maximum - _minimum)) + _minimum);

        OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, oldValue, _value, ScrollOrientation.VerticalScroll));

        Refresh();
    }

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void TopClick(object? sender, EventArgs e) => Value = _minimum;

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void BottomClick(object? sender, EventArgs e) => Value = _maximum;

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void LargeUpClick(object? sender, EventArgs e) => Value = GetValue(false, true);

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void LargeDownClick(object? sender, EventArgs e) => Value = GetValue(false, false);

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void SmallUpClick(object? sender, EventArgs e) => Value = GetValue(true, true);

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void SmallDownClick(object? sender, EventArgs e) => Value = GetValue(true, false);

    #endregion

    #endregion
}
