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

// ReSharper disable CompareOfFloatsByEqualityOperator
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// A custom scrollbar control.
/// </summary>
[Designer(typeof(KryptonScrollBarDesigner))]
[DefaultEvent(nameof(Scroll))]
[DefaultProperty(nameof(Value))]
[ToolboxBitmap(typeof(VScrollBar), "ToolboxBitmaps.KryptonScrollBar.bmp")]
[DesignerCategory(@"code")]
public class KryptonScrollBar : Control
{
    #region Instance Fields

    /// <summary>
    /// Indicates many changes to the scrollbar are happening, so stop painting till finished.
    /// </summary>
    private bool _inUpdate;

    /// <summary>
    /// The scrollbar orientation - horizontal / VERTICAL.
    /// </summary>
    private ScrollBarOrientation _orientation = ScrollBarOrientation.Vertical;

    /// <summary>
    /// The scroll orientation in scroll events.
    /// </summary>
    private ScrollOrientation _scrollOrientation = ScrollOrientation.VerticalScroll;

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

    private ContextMenuStrip _contextMenu;
    private IContainer components;
    private ToolStripMenuItem _tsmiScrollHere;
    private ToolStripSeparator _toolStripSeparator1;
    private ToolStripMenuItem _tsmiTop;
    private ToolStripMenuItem _tsmiBottom;
    private ToolStripSeparator _toolStripSeparator2;
    private ToolStripMenuItem _tsmiLargeUp;
    private ToolStripMenuItem _tsmiLargeDown;
    private ToolStripSeparator _toolStripSeparator3;
    private ToolStripMenuItem _tsmiSmallUp;
    private ToolStripMenuItem _tsmiSmallDown;

    #endregion

    #endregion

    #region Public

    /// <summary>Gets or sets the width of the scroll bar.</summary>
    /// <value>The width of the scroll bar.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ScrollBarWidth
    {
        get => Width; 
        set => Width = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text { get; set; }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonScrollBar"/> class.
    /// </summary>
    public KryptonScrollBar()
    {
        // sets the control styles of the control
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                                                | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint
                                                | ControlStyles.UserPaint, true);

        // initializes the context menu
        InitializeComponent();

        Width = 19;
        Height = 200;

        // sets the scrollbar up
        SetUpScrollBar();

        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        _palette = KryptonManager.CurrentGlobalPalette;

        _paletteRedirect = new PaletteRedirect(_palette);

        // Create the palette provider
        _stateCommon = new PaletteInputControlTripleRedirect(_paletteRedirect, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.LabelNormalPanel, null);
        _stateDisabled = new PaletteInputControlTripleStates(_stateCommon, null);
        _stateNormal = new PaletteInputControlTripleStates(_stateCommon, null);
        _stateActive = new PaletteInputControlTripleStates(_stateCommon, null);

        // timer for clicking and holding the mouse button
        // over/below the thumb and on the arrow buttons
        _progressTimer.Interval = 20;
        _progressTimer.Tick += ProgressTimerTick;

        // no image margin in context menu
        _contextMenu!.ShowImageMargin = false;
        ContextMenuStrip = _contextMenu;

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

    /// <summary>
    /// Gets or sets the orientation.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Gets or sets the orientation.")]
    [DefaultValue(ScrollBarOrientation.Vertical)]
    public ScrollBarOrientation Orientation
    {
        get => _orientation;

        set
        {
            // no change - return
            if (value == _orientation)
            {
                return;
            }

            _orientation = value;

            // change text of context menu entries
            ChangeContextMenuItems();

            // save scroll orientation for scroll event
            _scrollOrientation = value == ScrollBarOrientation.Vertical ?
                ScrollOrientation.VerticalScroll : ScrollOrientation.HorizontalScroll;

            // only in DesignMode switch width and height
            if (DesignMode)
            {
                Size = new Size(Height, Width);
            }

            // sets the scrollbar up
            SetUpScrollBar();
        }
    }

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

            // current value less than new minimum value - adjust
            if (_minimum < value)
            {
                _minimum = value;
            }

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
            if (_value == value || _value < _minimum || _value > _maximum)
            {
                return;
            }

            _value = value;

            // adjust thumb position
            ChangeThumbPosition(GetThumbPosition());

            // raise scroll event
            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, -1, value, _scrollOrientation));

            Refresh();
        }
    }

    /// <summary>
    /// Gets or sets the border color.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Gets or sets the border color.")]
    [DefaultValue(typeof(Color), "Color.FromARGB(93, 140, 201)")]
    public Color BorderColor
    {
        get => _borderColor;

        set
        {
            _borderColor = value;

            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the border color in disabled state.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Gets or sets the border color in disabled state.")]
    [DefaultValue(typeof(Color), "Color.Gray")]
    public Color DisabledBorderColor
    {
        get => _disabledBorderColor;

        set
        {
            _disabledBorderColor = value;

            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the opacity of the context menu (from 0 - 1).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Gets or sets the opacity of the context menu (from 0 - 1).")]
    [DefaultValue(1)]
    public double Opacity
    {
        get => _contextMenu.Opacity;

        set
        {
            // no change - return
            if (value == _contextMenu.Opacity)
            {
                return;
            }

            _contextMenu.AllowTransparency = value != 1;

            _contextMenu.Opacity = value;
        }
    }

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
        // sets the smoothing mode to none
        using var gh = new GraphicsHint(e.Graphics, PaletteGraphicsHint.None);

        // save client rectangle
        Rectangle rect = ClientRectangle;

        // adjust the rectangle
        if (_orientation == ScrollBarOrientation.Vertical)
        {
            rect.X++;
            rect.Y += _arrowHeight + 1;
            rect.Width -= 2;
            rect.Height -= (_arrowHeight * 2) + 2;
        }
        else
        {
            rect.X += _arrowWidth + 1;
            rect.Y++;
            rect.Width -= (_arrowWidth * 2) + 2;
            rect.Height -= 2;
        }

        KryptonScrollBarRenderer.InitColors();

        // draws the background
        KryptonScrollBarRenderer.DrawBackground(
            e.Graphics,
            ClientRectangle,
            _orientation);

        // draws the track
        KryptonScrollBarRenderer.DrawTrack(
            e.Graphics,
            rect,
            ScrollBarState.Normal,
            _orientation);

        // draw thumb and grip
        KryptonScrollBarRenderer.DrawThumb(
            e.Graphics,
            _thumbRectangle,
            _thumbState,
            _orientation);

        if (Enabled)
        {
            KryptonScrollBarRenderer.DrawThumbGrip(
                e.Graphics,
                _thumbRectangle,
                _orientation);
        }

        // draw arrows
        KryptonScrollBarRenderer.DrawArrowButton(
            e.Graphics,
            _topArrowRectangle,
            _topButtonState,
            true,
            _orientation);

        KryptonScrollBarRenderer.DrawArrowButton(
            e.Graphics,
            _bottomArrowRectangle,
            _bottomButtonState,
            false,
            _orientation);

        // check if top or bottom bar was clicked
        if (_topBarClicked)
        {
            if (_orientation == ScrollBarOrientation.Vertical)
            {
                _clickedBarRectangle.Y = _thumbTopLimit;
                _clickedBarRectangle.Height =
                    _thumbRectangle.Y - _thumbTopLimit;
            }
            else
            {
                _clickedBarRectangle.X = _thumbTopLimit;
                _clickedBarRectangle.Width =
                    _thumbRectangle.X - _thumbTopLimit;
            }

            KryptonScrollBarRenderer.DrawTrack(
                e.Graphics,
                _clickedBarRectangle,
                ScrollBarState.Pressed,
                _orientation);
        }
        else if (_bottomBarClicked)
        {
            if (_orientation == ScrollBarOrientation.Vertical)
            {
                _clickedBarRectangle.Y = _thumbRectangle.Bottom + 1;
                _clickedBarRectangle.Height =
                    _thumbBottomLimitBottom - _clickedBarRectangle.Y + 1;
            }
            else
            {
                _clickedBarRectangle.X = _thumbRectangle.Right + 1;
                _clickedBarRectangle.Width =
                    _thumbBottomLimitBottom - _clickedBarRectangle.X + 1;
            }

            KryptonScrollBarRenderer.DrawTrack(
                e.Graphics,
                _clickedBarRectangle,
                ScrollBarState.Pressed,
                _orientation);
        }

        // draw border
        using var pen = new Pen(Enabled ? KryptonScrollBarRenderer.BorderColors[0] : _disabledBorderColor);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
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
                // prevents showing the context menu if pressing the right mouse
                // button while holding the left
                ContextMenuStrip = null;

                Point mouseLocation = e.Location;

                if (_thumbRectangle.Contains(mouseLocation))
                {
                    _thumbClicked = true;
                    _thumbPosition = _orientation == ScrollBarOrientation.Vertical ? mouseLocation.Y - _thumbRectangle.Y : mouseLocation.X - _thumbRectangle.X;
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
                    _trackPosition =
                        _orientation == ScrollBarOrientation.Vertical ?
                            mouseLocation.Y : mouseLocation.X;

                    if (_trackPosition <
                        (_orientation == ScrollBarOrientation.Vertical ?
                            _thumbRectangle.Y : _thumbRectangle.X))
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
                _trackPosition =
                    _orientation == ScrollBarOrientation.Vertical ? e.Y : e.X;
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
            ContextMenuStrip = _contextMenu;

            if (_thumbClicked)
            {
                _thumbClicked = false;
                _thumbState = ScrollBarState.Normal;

                OnScroll(new ScrollEventArgs(
                    ScrollEventType.EndScroll,
                    -1,
                    _value,
                    _scrollOrientation)
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
                var pos = _orientation == ScrollBarOrientation.Vertical ?
                    e.Location.Y : e.Location.X;

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

                    int pixelRange, thumbPos, arrowSize;

                    // calculate the value - first some helper variables
                    // dependent on the current orientation
                    if (_orientation == ScrollBarOrientation.Vertical)
                    {
                        pixelRange = Height - (2 * _arrowHeight) - _thumbHeight;
                        thumbPos = _thumbRectangle.Y;
                        arrowSize = _arrowHeight;
                    }
                    else
                    {
                        pixelRange = Width - (2 * _arrowWidth) - _thumbWidth;
                        thumbPos = _thumbRectangle.X;
                        arrowSize = _arrowWidth;
                    }

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
                    OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, oldScrollValue, _value, _scrollOrientation));

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

    /*/// <summary>
    /// Performs the work of setting the specified bounds of this control.
    /// </summary>
    /// <param name="x">The new x value of the control.</param>
    /// <param name="y">The new y value of the control.</param>
    /// <param name="width">The new width value of the control.</param>
    /// <param name="height">The new height value of the control.</param>
    /// <param name="specified">A bitwise combination of the <see cref="BoundsSpecified"/> values.</param>
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        // only in design mode - constrain size
        if (DesignMode)
        {
            if (_orientation == ScrollBarOrientation.Vertical)
            {
                if (height < (2 * _arrowHeight) + 10)
                {
                    height = (2 * _arrowHeight) + 10;
                }

                width = 19;
            }
            else
            {
                if (width < (2 * _arrowWidth) + 10)
                {
                    width = (2 * _arrowWidth) + 10;
                }

                height = 19;
            }
        }

        base.SetBoundsCore(x, y, width, height, specified);

        if (DesignMode)
        {
            SetUpScrollBar();
        }
    }*/

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
        // Up&Down or Left&Right, PageUp, PageDown, Home, End
        var keyUp = Keys.Up;
        var keyDown = Keys.Down;

        if (_orientation == ScrollBarOrientation.Horizontal)
        {
            keyUp = Keys.Left;
            keyDown = Keys.Right;
        }

        if (keyData == keyUp)
        {
            Value -= _smallChange;

            return true;
        }

        if (keyData == keyDown)
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

        // set up the width's, height's and rectangles for the different
        // elements
        if (_orientation == ScrollBarOrientation.Vertical)
        {
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
        }
        else
        {
            _arrowHeight = 15;
            _arrowWidth = 17;
            _thumbHeight = 15;
            _thumbWidth = GetThumbSize();

            _clickedBarRectangle = ClientRectangle;
            _clickedBarRectangle.Inflate(-1, -1);
            _clickedBarRectangle.X += _arrowWidth;
            _clickedBarRectangle.Width -= _arrowWidth * 2;

            _channelRectangle = _clickedBarRectangle;

            _thumbRectangle = new Rectangle(
                ClientRectangle.X + _arrowWidth + 1,
                ClientRectangle.Y + 2,
                _thumbWidth,
                _thumbHeight - 1
            );

            _topArrowRectangle = new Rectangle(
                ClientRectangle.X + 1,
                ClientRectangle.Y + 2,
                _arrowWidth,
                _arrowHeight
            );

            _bottomArrowRectangle = new Rectangle(
                ClientRectangle.Right - _arrowWidth - 1,
                ClientRectangle.Y + 2,
                _arrowWidth,
                _arrowHeight
            );

            // Set the default starting thumb position.
            _thumbPosition = _thumbRectangle.Width / 2;

            // Set the bottom limit of the thumb's bottom border.
            _thumbBottomLimitBottom =
                ClientRectangle.Right - _arrowWidth - 2;

            // Set the bottom limit of the thumb's top border.
            _thumbBottomLimitTop =
                _thumbBottomLimitBottom - _thumbRectangle.Width;

            // Set the top limit of the thumb's top border.
            _thumbTopLimit = ClientRectangle.X + _arrowWidth + 1;
        }

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
        int pixelRange, arrowSize;

        if (_orientation == ScrollBarOrientation.Vertical)
        {
            pixelRange = Height - (2 * _arrowHeight) - _thumbHeight;
            arrowSize = _arrowHeight;
        }
        else
        {
            pixelRange = Width - (2 * _arrowWidth) - _thumbWidth;
            arrowSize = _arrowWidth;
        }

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
        var trackSize =
            _orientation == ScrollBarOrientation.Vertical ?
                Height - (2 * _arrowHeight) : Width - (2 * _arrowWidth);

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
        if (_orientation == ScrollBarOrientation.Vertical)
        {
            _thumbRectangle.Y = position;
        }
        else
        {
            _thumbRectangle.X = position;
        }
    }

    /// <summary>
    /// Controls the movement of the thumb.
    /// </summary>
    /// <param name="enableTimer">true for enabling the timer, false otherwise.</param>
    private void ProgressThumb(bool enableTimer)
    {
        var scrollOldValue = _value;
        var type = ScrollEventType.First;
        int thumbSize, thumbPos;

        if (_orientation == ScrollBarOrientation.Vertical)
        {
            thumbPos = _thumbRectangle.Y;
            thumbSize = _thumbRectangle.Height;
        }
        else
        {
            thumbPos = _thumbRectangle.X;
            thumbSize = _thumbRectangle.Width;
        }

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
            OnScroll(new ScrollEventArgs(type, scrollOldValue, _value, _scrollOrientation));

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

    /// <summary>
    /// Changes the Displayed text of the context menu items dependent of the current <see cref="ScrollBarOrientation"/>.
    /// </summary>
    private void ChangeContextMenuItems()
    {
        if (_orientation == ScrollBarOrientation.Vertical)
        {
            _tsmiTop.Text = nameof(Top);
            _tsmiBottom.Text = nameof(Bottom);
            _tsmiLargeDown.Text = KryptonManager.Strings.ScrollBarStrings.PageDown;
            _tsmiLargeUp.Text = KryptonManager.Strings.ScrollBarStrings.PageUp;
            _tsmiSmallDown.Text = KryptonManager.Strings.ScrollBarStrings.ScrollDown;
            _tsmiSmallUp.Text = KryptonManager.Strings.ScrollBarStrings.ScrollUp;
            _tsmiScrollHere.Text = KryptonManager.Strings.ScrollBarStrings.ScrollHere;
        }
        else
        {
            _tsmiTop.Text = nameof(Left);
            _tsmiBottom.Text = nameof(Right);
            _tsmiLargeDown.Text = KryptonManager.Strings.ScrollBarStrings.PageLeft;
            _tsmiLargeUp.Text = KryptonManager.Strings.ScrollBarStrings.PageRight;
            _tsmiSmallDown.Text = KryptonManager.Strings.ScrollBarStrings.ScrollRight;
            _tsmiSmallUp.Text = KryptonManager.Strings.ScrollBarStrings.ScrollLeft;
            _tsmiScrollHere.Text = KryptonManager.Strings.ScrollBarStrings.ScrollHere;
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.PalettePaint -= OnPalettePaint;
        }

        _palette = KryptonManager.CurrentGlobalPalette;

        _paletteRedirect!.Target = _palette;

        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;

            // Repaint
            KryptonScrollBarRenderer.InitColors();
        }

        Invalidate();
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

    #endregion

    #region Context Menu Methods

    /// <summary>
    /// Initializes the context menu.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();
        _contextMenu = new ContextMenuStrip(components);
        _tsmiScrollHere = new ToolStripMenuItem();
        _toolStripSeparator1 = new ToolStripSeparator();
        _tsmiTop = new ToolStripMenuItem();
        _tsmiBottom = new ToolStripMenuItem();
        _toolStripSeparator2 = new ToolStripSeparator();
        _tsmiLargeUp = new ToolStripMenuItem();
        _tsmiLargeDown = new ToolStripMenuItem();
        _toolStripSeparator3 = new ToolStripSeparator();
        _tsmiSmallUp = new ToolStripMenuItem();
        _tsmiSmallDown = new ToolStripMenuItem();
        _contextMenu.SuspendLayout();
        SuspendLayout();
        // 
        // contextMenu
        // 
        _contextMenu.Items.AddRange(new ToolStripItem[] {
            _tsmiScrollHere,
            _toolStripSeparator1,
            _tsmiTop,
            _tsmiBottom,
            _toolStripSeparator2,
            _tsmiLargeUp,
            _tsmiLargeDown,
            _toolStripSeparator3,
            _tsmiSmallUp,
            _tsmiSmallDown});
        _contextMenu.Name = nameof(_contextMenu);
        _contextMenu.Size = new Size(151, 176);
        // 
        // tsmiScrollHere
        // 
        _tsmiScrollHere.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsmiScrollHere.Name = nameof(_tsmiScrollHere);
        _tsmiScrollHere.Size = new Size(150, 22);
        _tsmiScrollHere.Text = KryptonManager.Strings.ScrollBarStrings.ScrollHere;
        _tsmiScrollHere.Click += ScrollHereClick;
        // 
        // toolStripSeparator1
        // 
        _toolStripSeparator1.Name = nameof(_toolStripSeparator1);
        _toolStripSeparator1.Size = new Size(147, 6);
        // 
        // tsmiTop
        // 
        _tsmiTop.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsmiTop.Name = nameof(_tsmiTop);
        _tsmiTop.Size = new Size(150, 22);
        _tsmiTop.Text = nameof(Top);
        _tsmiTop.Click += TopClick;
        // 
        // tsmiBottom
        // 
        _tsmiBottom.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsmiBottom.Name = nameof(_tsmiBottom);
        _tsmiBottom.Size = new Size(150, 22);
        _tsmiBottom.Text = nameof(Bottom);
        _tsmiBottom.Click += BottomClick;
        // 
        // toolStripSeparator2
        // 
        _toolStripSeparator2.Name = nameof(_toolStripSeparator2);
        _toolStripSeparator2.Size = new Size(147, 6);
        // 
        // tsmiLargeUp
        // 
        _tsmiLargeUp.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsmiLargeUp.Name = nameof(_tsmiLargeUp);
        _tsmiLargeUp.Size = new Size(150, 22);
        _tsmiLargeUp.Text = KryptonManager.Strings.ScrollBarStrings.PageUp;
        _tsmiLargeUp.Click += LargeUpClick;
        // 
        // tsmiLargeDown
        // 
        _tsmiLargeDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsmiLargeDown.Name = nameof(_tsmiLargeDown);
        _tsmiLargeDown.Size = new Size(150, 22);
        _tsmiLargeDown.Text = KryptonManager.Strings.ScrollBarStrings.PageDown;
        _tsmiLargeDown.Click += LargeDownClick;
        // 
        // toolStripSeparator3
        // 
        _toolStripSeparator3.Name = nameof(_toolStripSeparator3);
        _toolStripSeparator3.Size = new Size(147, 6);
        // 
        // tsmiSmallUp
        // 
        _tsmiSmallUp.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsmiSmallUp.Name = nameof(_tsmiSmallUp);
        _tsmiSmallUp.Size = new Size(150, 22);
        _tsmiSmallUp.Text = KryptonManager.Strings.ScrollBarStrings.ScrollUp;
        _tsmiSmallUp.Click += SmallUpClick;
        // 
        // tsmiSmallDown
        // 
        _tsmiSmallDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsmiSmallDown.Name = nameof(_tsmiSmallDown);
        _tsmiSmallDown.Size = new Size(150, 22);
        _tsmiSmallDown.Text = KryptonManager.Strings.ScrollBarStrings.ScrollDown;
        _tsmiSmallDown.Click += SmallDownClick;
        _contextMenu.ResumeLayout(false);
        ResumeLayout(false);
    }

    /// <summary>
    /// Context menu handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    private void ScrollHereClick(object? sender, EventArgs e)
    {
        int thumbSize, thumbPos, arrowSize, size;

        if (_orientation == ScrollBarOrientation.Vertical)
        {
            thumbSize = _thumbHeight;
            arrowSize = _arrowHeight;
            size = Height;

            ChangeThumbPosition(Math.Max(_thumbTopLimit, Math.Min(_thumbBottomLimitTop, _trackPosition - (_thumbRectangle.Height / 2))));

            thumbPos = _thumbRectangle.Y;
        }
        else
        {
            thumbSize = _thumbWidth;
            arrowSize = _arrowWidth;
            size = Width;

            ChangeThumbPosition(Math.Max(_thumbTopLimit, Math.Min(_thumbBottomLimitTop, _trackPosition - (_thumbRectangle.Width / 2))));

            thumbPos = _thumbRectangle.X;
        }

        var pixelRange = size - (2 * arrowSize) - thumbSize;
        var perc = 0f;

        if (pixelRange != 0)
        {
            perc = (thumbPos - arrowSize) / (float)pixelRange;
        }

        var oldValue = _value;

        _value = Convert.ToInt32((perc * (_maximum - _minimum)) + _minimum);

        OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, oldValue, _value, _scrollOrientation));

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