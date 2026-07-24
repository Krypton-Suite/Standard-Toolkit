#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(TrackBar)), ToolboxItem(false)]
public partial class KryptonSliderButton : UserControl, IContentValues
{
    #region Constants
    private const int LogicalButtonSize = 16;
    private const float LogicalCircleDiameter = 15.1f;
    #endregion

    #region ...   Enums    ...
    public enum ButtonStyles
    {
        PlusButton = 0,
        MinusButton = 1
    }
    #endregion

    public KryptonSliderButton()
    {
        _values = new SliderButtonValues(this);

        //Initialize Component
        InitializeComponent();

        //(1) To remove flicker we use double buffering for drawing
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);

        //(5) Create redirection object to the base palette
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect = new PaletteRedirect(_palette);

        //(6) Create accessor objects for the back, border and content
        _mPaletteBack = new PaletteBackInheritRedirect(_paletteRedirect);
        _mPaletteBorder = new PaletteBorderInheritRedirect(_paletteRedirect);
        _mPaletteContent = new PaletteContentInheritRedirect(_paletteRedirect);

        //Set Properties
        BackColor = Color.Transparent;
        ApplyDpiScaledSize();

        InitColors();

    }

    //Palette State
    private KryptonManager _kManager = new KryptonManager();
    private PaletteBackInheritRedirect _mPaletteBack;
    private PaletteBorderInheritRedirect _mPaletteBorder;
    private PaletteContentInheritRedirect _mPaletteContent;
    private IDisposable? _mMementoContent;
    private IDisposable? _mMementoBack1;
    private IDisposable? _mMementoBack2;

    private PaletteBase? _palette;
    private PaletteRedirect _paletteRedirect;

    //Colors
    private Color _mInnerColor = Color.FromArgb(99, 106, 116);
    private Color _mOuterColor = Color.FromArgb(236, 236, 236);

    //Declares
    private readonly SliderButtonValues _values;
    private bool _mHighlight;
    private bool _mDown;

    //Events
    public event SliderButtonFireEventHandler SliderButtonFire;
    public delegate void SliderButtonFireEventHandler(KryptonSliderButton sender, EventArgs e);

    //Properties

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Click behavior, orientation, style, and fire rate settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SliderButtonValues ButtonValues => _values;

    private bool ShouldSerializeButtonValues() => !_values.IsDefault;

    private void ResetButtonValues() => _values.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SingleClick
    {
        get => _values.SingleClick;
        set => _values.SingleClick = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VisualOrientation Orientation
    {
        get => _values.Orientation;
        set => _values.Orientation = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonStyles ButtonStyle
    {
        get => _values.ButtonStyle;
        set => _values.ButtonStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBackStyle VisualLook
    {
        get => _values.VisualLook;
        set => _values.VisualLook = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int EventFireRate
    {
        get => _values.EventFireRate;
        set => _values.EventFireRate = value;
    }

    /// <summary>
    /// Applies a new fire-timer interval, honouring the same "not while pressed" guard as the original property setter.
    /// </summary>
    internal void SetEventFireRateCore(int value)
    {
        if (FireTimer.Interval != value & !_mDown)
        {
            FireTimer.Interval = value;
        }
    }

    //ComponentFactory Palette Painting
    protected override void OnPaint(PaintEventArgs e)
    {
        // Bounds are logical 16×16 at 96 DPI; Size is already DPI-scaled.
        Rectangle buttonBounds = ClientRectangle;
        float circleDiameter = Width * (LogicalCircleDiameter / LogicalButtonSize);
        RectangleF buttonCircleBounds = new RectangleF(0, 0, circleDiameter, circleDiameter);

        //Smoothing Mode
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        //Paint Base
        base.OnPaint(e);

        if (_palette != null)
        {

            //Get the renderer associated with this palette
            IRenderer renderer = _palette.GetRenderer();

            //Create the rendering context that is passed into all renderer calls
            using (RenderContext renderContext = new RenderContext(this, e.Graphics, buttonBounds, renderer))
            {

                // Set the style we want picked up from the base palette
                _mPaletteBack.Style = PaletteBackStyle.HeaderPrimary;


                //Fill The Space
                using (GraphicsPath path = GetButtonPath(buttonBounds))
                {
                    // Ask renderer to draw the background
                    _mMementoBack1 = renderer.RenderStandardBack.DrawBack(renderContext, buttonBounds, path, _mPaletteBack, _values.Orientation, Enabled ? PaletteState.Normal : PaletteState.Disabled, _mMementoBack1);
                }

                // We want the inner part of the control to act like a button, so 
                // we need to find the correct palette state based on if the mouse 
                // is over the control if the mouse button is pressed down or not.
                PaletteState buttonState = GetButtonState();

                // Set the style of button we want to draw
                _mPaletteBack.Style = _values.VisualLook;
                _mPaletteBorder.Style = (PaletteBorderStyle)_values.VisualLook;
                _mPaletteContent.Style = (PaletteContentStyle)_values.VisualLook;

                // Do we need to draw the background?
                if (_mPaletteBack.GetBackDraw(buttonState) == InheritBool.True)
                {
                    using (GraphicsPath path = GetRoundedSquarePath(buttonCircleBounds))
                    {
                        // Ask renderer to draw the background
                        _mMementoBack2 = renderer.RenderStandardBack.DrawBack(renderContext, buttonBounds, path, _mPaletteBack, _values.Orientation, buttonState, _mMementoBack2);
                    }
                }

                // Do we need to draw the border?
                if (_mPaletteBorder.GetBorderDraw(buttonState) == InheritBool.True)
                {
                    // Now we draw the border of the inner area, also in ButtonStandalone style
                    e.Graphics.DrawEllipse(new Pen(_mPaletteBorder.GetBorderColor2(buttonState)), buttonCircleBounds);
                }

                e.Graphics.SmoothingMode = SmoothingMode.None;

                //Draw Magnifying Sign
                switch (_values.ButtonStyle)
                {
                    case ButtonStyles.MinusButton:
                        Rectangle minusOuterBounds = new Rectangle(
                            LogicalToDeviceUnits(3), Height / 2 - LogicalToDeviceUnits(2),
                            LogicalToDeviceUnits(10), LogicalToDeviceUnits(4));
                        Rectangle minusInnerBounds = new Rectangle(
                            LogicalToDeviceUnits(4), Height / 2 - LogicalToDeviceUnits(1),
                            LogicalToDeviceUnits(8), LogicalToDeviceUnits(2));

                        e.Graphics.FillRectangle(new SolidBrush(_mOuterColor), minusOuterBounds);
                        e.Graphics.FillRectangle(new SolidBrush(_mInnerColor), minusInnerBounds);

                        break;
                    case ButtonStyles.PlusButton:
                        DrawPlusOuter(e.Graphics, _mOuterColor);
                        DrawPlusInner(e.Graphics, _mInnerColor);
                        break;
                }

            }
        }

    }

    /// <inheritdoc />
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        ApplyDpiScaledSize();
        Invalidate();
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
        //Active Palette
        //If m_palette IsNot Nothing Then
        if (_palette != null)
        {

            // We want the inner part of the control to act like a button, so 
            // we need to find the correct palette state based on if the mouse 
            // is over the control and currently being pressed down or not.
            PaletteState buttonState = GetButtonState();

            // Create a rectangle inset, this is where we will draw a button
            Rectangle buttonBounds = ClientRectangle;

            // Get the renderer associated with this palette
            IRenderer renderer = _palette.GetRenderer();

            // Create a layout context used to allow the renderer to layout the content
            using (ViewLayoutContext viewContext = new ViewLayoutContext(this, renderer))
            {

                // Setup the appropriate style for the content
                _mPaletteContent.Style = (PaletteContentStyle)_values.VisualLook;

                // Cleaup resources by disposing of old memento instance
                _mMementoContent?.Dispose();

                // Ask the renderer to work out how the Content values will be layed out and
                // return a memento object that we cache for use when actually performing painting
                _mMementoContent = renderer.RenderStandardContent.LayoutContent(viewContext, buttonBounds, _mPaletteContent, this, _values.Orientation, buttonState);
                //m_mementoContent = Renderer.RenderStandardContent.LayoutContent(ViewContext, ButtonBounds, m_paletteContent, this, m_orientation, false, buttonState, false);

            }
        }

        //Base Layout Call
        base.OnLayout(e);

    }

    //Disposal
    protected override void Dispose(bool disposing)
    {

        if (disposing)
        {

            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
                _palette = null;
            }


            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            //Dispose Memento Content
            if (_mMementoContent != null)
            {
                _mMementoContent.Dispose();
                _mMementoContent = null;
            }

            //Dispose Memento BackGround One
            if (_mMementoBack1 != null)
            {
                _mMementoBack1.Dispose();
                _mMementoBack1 = null;
            }

            //Dispose Memento BackGround Two
            if (_mMementoBack2 != null)
            {
                _mMementoBack2.Dispose();
                _mMementoBack2 = null;
            }

            components?.Dispose();

        }

        base.Dispose(disposing);
    }

    //Button Helpers
    private PaletteState GetButtonState()
    {
        if (!Enabled)
        {
            return PaletteState.Disabled;
        }
        else
        {
            if (_mDown)
            {
                if (_mHighlight)
                {
                    return PaletteState.CheckedPressed;
                }
                else
                {
                    return PaletteState.Pressed;
                }
            }
            else
            {
                if (_mHighlight)
                {
                    return PaletteState.Tracking;
                }
                else
                {
                    return PaletteState.Normal;
                }
            }
        }
    }
    public GraphicsPath GetRoundedSquarePath(RectangleF bounds)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(bounds);
        path.CloseFigure();
        return path;
    }
    private GraphicsPath GetButtonPath(RectangleF bounds)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(bounds);
        return path;
    }
    private void DrawPlusOuter(Graphics gfx, Color fill)
    {
        gfx.FillRectangle(new SolidBrush(fill), new Rectangle(
            LogicalToDeviceUnits(3), Height / 2 - LogicalToDeviceUnits(2),
            LogicalToDeviceUnits(10), LogicalToDeviceUnits(4)));
        gfx.FillRectangle(new SolidBrush(fill), new Rectangle(
            Width / 2 - LogicalToDeviceUnits(2), LogicalToDeviceUnits(3),
            LogicalToDeviceUnits(4), LogicalToDeviceUnits(10)));
    }
    private void DrawPlusInner(Graphics gfx, Color fill)
    {
        gfx.FillRectangle(new SolidBrush(fill), new Rectangle(
            LogicalToDeviceUnits(4), Height / 2 - LogicalToDeviceUnits(1),
            LogicalToDeviceUnits(8), LogicalToDeviceUnits(2)));
        gfx.FillRectangle(new SolidBrush(fill), new Rectangle(
            Width / 2 - LogicalToDeviceUnits(1), LogicalToDeviceUnits(4),
            LogicalToDeviceUnits(2), LogicalToDeviceUnits(8)));
    }

    //Key Mouse Events
    private void KryptonSliderButton_MouseDown(object? sender, MouseEventArgs e)
    {
        _mDown = true;
        //Single click?
        if (!_values.SingleClick)
        { FireTimer.Start(); }

        Invalidate();
    }
    private void KryptonSliderButton_MouseEnter(object? sender, EventArgs e)
    {
        _mHighlight = true;
        Invalidate();
    }
    private void KryptonSliderButton_MouseLeave(object? sender, EventArgs e)
    {
        _mHighlight = false;
        Invalidate();
    }
    private void KryptonSliderButton_MouseUp(object? sender, MouseEventArgs e)
    {
        _mDown = false;

        //Single click?
        if (!_values.SingleClick)
        { FireTimer.Stop(); }
        else
        { SliderButtonFire(this, new EventArgs()); }

        Invalidate();
    }

    //Implements IContentValues
    public Image? GetImage(PaletteState state) => null;

    public Color GetImageTransparentColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    public string GetLongText() => string.Empty;

    public string GetShortText() => string.Empty;

    public Image? GetOverlayImage(PaletteState state) => null;

    public Color GetOverlayImageTransparentColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

    public Size GetOverlayImageFixedSize(PaletteState state) => GetDpiScaledButtonSize();

    //Krypton Palette
    private void k_palette_BasePaletteChanged(object? sender, EventArgs e)
    {
        Invalidate();
    }
    private void k_palette_BaseRendererChanged(object? sender, EventArgs e)
    {
        Invalidate();
    }
    private void k_palette_PalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        Invalidate();
    }

    //Fire Machine Gun
    private void FireTimer_Tick(object? sender, EventArgs e)
    {
        SliderButtonFire?.Invoke(this, new EventArgs());
    }


    //Krypton Events
    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.PalettePaint -= OnPalettePaint;
        }
        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect.Target = _palette;
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
            InitColors();
        }
        Invalidate();

    }

    private void InitColors()
    {
        //Colors
        if (_palette != null)
        {
            _mInnerColor = _palette.ColorTable.GripDark;

            // Ignore this color if the palette uses an Office2010-Renderer
            _mOuterColor = _palette.GetRenderer() is RenderOffice2010 or RenderOffice2013 or RenderMicrosoft365 ? Color.Transparent : _palette.ColorTable.GripLight;
        }
    }

    // net472 only has LogicalToDeviceUnits(int); scale each axis separately.
    private Size GetDpiScaledButtonSize() =>
        new Size(LogicalToDeviceUnits(LogicalButtonSize), LogicalToDeviceUnits(LogicalButtonSize));

    private void ApplyDpiScaledSize() => Size = GetDpiScaledButtonSize();

}