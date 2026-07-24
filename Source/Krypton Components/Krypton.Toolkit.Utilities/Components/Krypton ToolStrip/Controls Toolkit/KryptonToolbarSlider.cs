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
public partial class KryptonToolbarSlider : UserControl, IContentValues
{
    #region Instance Fields

    //Colors
    private Color _sliderLineTop = Color.FromArgb(116, 150, 194);
    private Color _sliderLineBottom = Color.FromArgb(222, 226, 236);

    //Krypton
    private PaletteBase? _palette;
    private PaletteRedirect _paletteRedirect;

    private PaletteBackInheritRedirect _mPaletteBack;
    private PaletteBorderInheritRedirect _mPaletteBorder;
    private PaletteContentInheritRedirect _mPaletteContent;
    private IDisposable? _mMementoContent;
    private IDisposable? _mMementoBack1;
    private IDisposable? _mMementoBack2;

    //Declares
    private readonly ToolbarSliderValues _values;
    private int _halfRange;
    private bool _highlight;
    private bool _sliderHighlight;
    private bool _down;

    private int _maximumValue;
    private int _minimumValue;

    #endregion

    #region Identity

    public KryptonToolbarSlider()
    {
        _values = new ToolbarSliderValues(this);

        //(1) To remove flicker we use double buffering for drawing
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        
        //Initialize Component
        InitializeComponent();

        _maximumValue = _values.Range / 2;
        _minimumValue = -1 * _values.Range / 2;

        //(5) Create redirection object to the base palette
        // add Palette Handler
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

        //Set Back Color
        BackColor = Color.Transparent;

        // Keep host height in sync when child buttons re-scale for DPI.
        kplus!.SizeChanged += (_, _) => Height = kplus.Height;
        kminus!.SizeChanged += (_, _) => Height = Math.Max(Height, kminus.Height);
    }

    /// <inheritdoc />
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        Height = Math.Max(kplus!.Height, kminus!.Height);
        kplus.Location = new Point(Width - kplus.Width, 0);
        Invalidate();
    }

    #endregion

    #region "Declares"

    

    //Events
    public event ValueChangedEventHandler ValueChanged;
    public delegate void ValueChangedEventHandler(KryptonToolbarSlider sender, SliderEventArgs e);

    //Enumerations
    public enum RangeTests
    {
        MinusDomain = 0,
        LeftDomain = 1,
        MiddleDomain = 2,
        RightDomain = 3,
        PlusDomain = 4,
        ElseDomain = 5
    }

    #endregion

    #region "Painting"

    //Overrides
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawLines(e.Graphics);
        DrawSlider(e.Graphics);
    }
    protected override void Dispose(bool disposing)
    {

        if (disposing)
        {

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


            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
                _palette = null;
            }


            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

        }

        base.Dispose(disposing);
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
            PaletteState buttonState = GetSliderState();

            // Create a rectangle inset, this is where we will draw a button
            Rectangle buttonBounds = GetSliderBounds(GetSliderPosition());

            // Get the renderer associated with this palette
            //Dim Renderer As IRenderer =  m_palette.GetRenderer()
            IRenderer renderer = _palette.GetRenderer();

            // Create a layout context used to allow the renderer to layout the content
            using (ViewLayoutContext viewContext = new ViewLayoutContext(this, renderer))
            {

                // Setup the appropriate style for the content
                _mPaletteContent.Style = PaletteContentStyle.ButtonStandalone;

                // Cleaup resources by disposing of old memento instance
                _mMementoContent?.Dispose();

                // Ask the renderer to work out how the Content values will be layed out and
                // return a memento object that we cache for use when actually performing painting
                _mMementoContent = renderer.RenderStandardContent.LayoutContent(viewContext, buttonBounds, _mPaletteContent, this, VisualOrientation.Top, buttonState);

            }
        }

        //Base Layout Call
        base.OnLayout(e);

    }

    //Renderers
    private void DrawLines(Graphics gfx)
    {

        //Smoothing Mode
        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        //Define Pen Colors
        Pen topPen = new Pen(_sliderLineTop, 1);
        Pen bottomPen = new Pen(_sliderLineBottom, 1);

        //Draw Lines
        gfx.DrawLine(topPen, 17, Height / 2 - 1, Width - 18, Height / 2 - 1);
        gfx.DrawLine(bottomPen, 17, Height / 2, Width - 18, Height / 2);
        gfx.DrawLine(topPen, Width / 2, Height / 2 - 3, Width / 2, Height / 2 + 3);
        gfx.DrawLine(bottomPen, Width / 2 + 1, Height / 2 - 3, Width / 2 + 1, Height / 2 + 3);

    }
    private void DrawSlider(Graphics gfx)
    {

        //Smoothing Mode
        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        //Slider Bounds
        Rectangle sliderBounds = GetSliderBounds(GetSliderPosition());

        //Check Palette
        if (_palette != null)
        {
            //Get the renderer associated with this palette
            IRenderer renderer = _palette.GetRenderer();

            //Create the rendering context that is passed into all renderer calls
            using (RenderContext renderContext = new RenderContext(this, gfx, sliderBounds, renderer))
            {
                // We want to draw the background of the entire control over the entire client area.
                using (GraphicsPath path = GetSliderPath(GetSliderPosition()))
                {

                    // Set the style we want picked up from the base palette
                    _mPaletteBack.Style = PaletteBackStyle.HeaderPrimary;

                    // Ask renderer to draw the background
                    _mMementoBack1 = renderer.RenderStandardBack.DrawBack(renderContext, sliderBounds, path, _mPaletteBack, VisualOrientation.Top, Enabled ? PaletteState.Normal : PaletteState.Disabled, _mMementoBack1);
                }

                //We want the inner part of the control to act like a button, so 
                //we need to find the correct palette state based on if the mouse 
                //is over the control if the mouse button is pressed down or not.
                PaletteState buttonState = GetSliderState();

                //Set the style of button we want to draw
                _mPaletteBack.Style = PaletteBackStyle.ButtonStandalone;
                _mPaletteBorder.Style = PaletteBorderStyle.ButtonStandalone;
                _mPaletteContent.Style = PaletteContentStyle.ButtonStandalone;

                //Do we need to draw the background?
                if (_mPaletteBack.GetBackDraw(buttonState) == InheritBool.True)
                {
                    //BackGround Path
                    using (GraphicsPath path = GetSliderPath(GetSliderPosition()))
                    {

                        // Ask renderer to draw the background
                        _mMementoBack2 = renderer.RenderStandardBack.DrawBack(renderContext, sliderBounds, path, _mPaletteBack, VisualOrientation.Top, buttonState, _mMementoBack2);
                        gfx.DrawPath(new Pen(_mPaletteBorder.GetBorderColor2(buttonState)), path);
                        gfx.DrawLine(new Pen(_mPaletteBorder.GetBorderColor2(buttonState)), sliderBounds.X + sliderBounds.Width / 2 + 1, sliderBounds.Y + 4, sliderBounds.X + sliderBounds.Width / 2 + 1, sliderBounds.Y + 8);

                    }
                }
            }
        }
    }

    #endregion

    #region "Helpers"

    //Slider Helpers
    private Point GetPointFromValue(int value)
    {
        if (value == 0)
        {
            return new Point((int)Math.Round(Width / 2.0), (int)Math.Round(Height / 2.0 - 6.5));
        }
        Point result = new Point((int)Math.Round(Width / 2.0 + (Width / 2.0 - 20.0) * (value / (double)_halfRange)), (int)Math.Round(Height / 2.0 - 6.5));
        if (result.X > Width - 0x16)
        {
            result.X = Width - 0x16;
        }
        if (result.X < 20)
        {
            result.X = 20;
        }
        return result;

    }
    private int GetValueFromPoint(Point value)
    {
        int result = (int)Math.Round((value.X - Width / 2.0) / (Width / 2.0 - 20.0) * _halfRange);
        if (result > _halfRange)
        {
            result = _halfRange;
        }
        if (result < -_halfRange)
        {
            result = -_halfRange;
        }
        return result;

    }
    private GraphicsPath GetSliderPath(Point location)
    {
        Point topLeft = new Point(location.X + 1, location.Y);
        Point midLeft = new Point(location.X + 1, location.Y + 9);
        Point tip = new Point(location.X + 5, location.Y + 13);
        Point midRight = new Point(location.X + 9, location.Y + 9);
        Point topRight = new Point(location.X + 9, location.Y);

        GraphicsPath path = new GraphicsPath();
        path.AddLine(topLeft, midLeft);
        path.AddLine(midLeft, tip);
        path.AddLine(tip, midRight);
        path.AddLine(midRight, topRight);
        path.AddLine(topLeft, topRight);
        return path;
    }
    private Rectangle GetSliderBounds(Point location)
    {
        return new Rectangle(location.X, location.Y, 9, 13);
    }
    private Point GetSliderPosition()
    {
        return new Point((int)Math.Round(GetPointFromValue(_values.Value).X - 4.5), (int)Math.Round(Height / 2.0 - 6.5));
    }

    private PaletteState GetSliderState()
    {
        if (!Enabled)
        {
            return PaletteState.Disabled;
        }
        else
        {
            if (_down)
            {
                return PaletteState.Pressed;
            }
            else
            {
                if (_highlight)
                {
                    if (_sliderHighlight)
                    {
                        return PaletteState.CheckedTracking;
                    }
                    else
                    {
                        return PaletteState.Tracking;
                    }
                }
                else
                {
                    return PaletteState.Normal;
                }
            }
        }
    }
    private RangeTests GetSliderRangeTest(Point location)
    {
        if ((location.X > 20) & (location.X < Width - 20))
        {
            if ((location.X >= Width / 2.0 - 5.0) & (location.X <= Width / 2.0 + 5.0))
            {
                return RangeTests.MiddleDomain;
            }
            if (location.X < Width / 2.0)
            {
                return RangeTests.LeftDomain;
            }
            return RangeTests.RightDomain;
        }
        if (location.X <= 20)
        {
            return RangeTests.MinusDomain;
        }
        if (location.X >= Width - 20)
        {
            return RangeTests.PlusDomain;
        }
        return RangeTests.ElseDomain;

    }

    //Slider Values
    private void ChangeValue(int newValue)
    {
        if (Enabled)
        {
            ValueChanged?.Invoke(this, new SliderEventArgs(newValue, _values.Value, _values.Range, _values.Steps));
            _values.Value = newValue;
        }
    }
    private void SliderIncrement()
    {
        if (_values.Value + _values.Steps <= _halfRange)
        {
            ChangeValue(_values.Value + _values.Steps);
        }
    }
    private void SliderDecrement()
    {
        if (_values.Value - _values.Steps >= -_halfRange)
        {
            ChangeValue(_values.Value - _values.Steps);
        }
    }

    #endregion

    #region "Events"

    //Key Mouse Events
    private void KryptonSliderButton_MouseDown(object? sender, MouseEventArgs e)
    {
        if (GetSliderBounds(GetSliderPosition()).Contains(e.Location))
        {
            _down = true;
            Invalidate();
        }
    }
    private void KryptonSliderButton_MouseEnter(object? sender, EventArgs e)
    {
        _highlight = true;
        Invalidate();
    }
    private void KryptonSliderButton_MouseLeave(object? sender, EventArgs e)
    {
        _highlight = false;
        Invalidate();
    }
    private void KryptonSlider_MouseMove(object? sender, MouseEventArgs e)
    {

        //Repaint Flag
        bool doInvalidate = false;

        //Check Mouse Location
        if (GetSliderBounds(GetSliderPosition()).Contains(e.Location))
        {
            if (!_sliderHighlight)
            {
                _sliderHighlight = true;
                doInvalidate = true;
            }
        }
        else
        {
            if (_sliderHighlight)
            {
                _sliderHighlight = false;
                doInvalidate = true;
            }
        }

        //Check Moving Slider
        if (_down)
        {
            /*
            switch (GetSliderRangeTest(e.Location))
            {
                case RangeTests.MinusDomain:
                    ChangeValue((m_range / 2) * -1);
                    break;
                case RangeTests.MiddleDomain:
                    ChangeValue(0);
                    break;
                case RangeTests.PlusDomain:
                    ChangeValue((int)m_range / 2);
                    break;
                default:
                    ChangeValue(GetValueFromPoint(e.Location));
                    break;
            }
            */
            ChangeValue(GetValueFromPoint(e.Location));
            doInvalidate = true;
        }

        //Check Invalidation
        if (doInvalidate)
        {
            Invalidate();
        }
    }
    private void KryptonSliderButton_MouseUp(object? sender, MouseEventArgs e)
    {
        _down = false;
        if (!GetSliderBounds(GetSliderPosition()).Contains(e.Location))
        {
            switch (GetSliderRangeTest(e.Location))
            {
                case RangeTests.LeftDomain:
                case RangeTests.RightDomain:
                    ChangeValue(GetValueFromPoint(e.Location));
                    break;
                case RangeTests.MiddleDomain:
                    ChangeValue(0);
                    break;
            }
        }
        Invalidate();
    }
    
    //Slider Buttons
    private void kminus_SliderButtonFire(KryptonSliderButton sender, EventArgs e)
    {
        SliderDecrement();
        Invalidate();
    }
    private void kplus_SliderButtonFire(KryptonSliderButton sender, EventArgs e)
    {
        SliderIncrement();
        Invalidate();
    }

    #endregion

    #region "Properties"

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Value, click behavior, fire interval, range, and step settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolbarSliderValues SliderValues => _values;

    private bool ShouldSerializeSliderValues() => !_values.IsDefault;

    private void ResetSliderValues() => _values.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Value
    {
        get => _values.Value;
        set => _values.Value = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SingleClick
    {
        get => _values.SingleClick;
        set => _values.SingleClick = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int FireInterval
    {
        get => _values.FireInterval;
        set => _values.FireInterval = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Range
    {
        get => _values.Range;
        set => _values.Range = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Steps
    {
        get => _values.Steps;
        set => _values.Steps = value;
    }

    #endregion

    #region "IContentValues"

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

    public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);

    #endregion

    #region "SliderEventArgs"

    public class SliderEventArgs : EventArgs
    {

        //Starter
        public SliderEventArgs(int newValue, int oldValue, int range, int steps)
            : base()
        {
            _mNewvalue = newValue;
            _mOldvalue = oldValue;
            _mRange = range;
            _mSteps = steps;
        }

        //Properties
        private int _mNewvalue;
        public int NewValue => _mNewvalue;
        private int _mOldvalue;
        public int OldValue => _mOldvalue;
        private int _mRange;
        public int Range => _mRange;
        private int _mSteps;
        public int Steps => _mSteps;

        //ToString
        public override string ToString()
        {
            string retStr = "";
            retStr += $"Values: {DateTime.Now.ToLongTimeString()}{Environment.NewLine}";
            retStr += $"NewValue: {_mNewvalue}{Environment.NewLine}";
            retStr += $"OldValue: {_mOldvalue}{Environment.NewLine}";
            retStr += $"Range: {_mRange}{Environment.NewLine}";
            retStr += $"Steps: {_mSteps}{Environment.NewLine}";
            return retStr;
        }

    }

    #endregion

    #region "   Krypton Events   "

    //Krypton Palette Events
    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        Invalidate();
    }

    //Krypton Palette Events
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
            //repaint with new values

            //set colors
            InitColours();


        }

        Invalidate();
    }

    private void InitColours()
    {
        if (_palette is not null)
        {
            //Colors
            _sliderLineTop = _palette.ColorTable.GripDark;
            _sliderLineBottom = _palette.ColorTable.GripLight;
        }
    }
    #endregion

}