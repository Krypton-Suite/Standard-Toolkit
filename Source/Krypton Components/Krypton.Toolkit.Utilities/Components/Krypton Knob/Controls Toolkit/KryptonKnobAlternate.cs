#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(KnobValues))]
[ToolboxBitmap(typeof(KryptonKnob))]
[Description(@"Allow the user to select a value by rotating an enhanced knob with scale graduations.")]
public partial class KryptonKnobAlternate : UserControl
{
    #region Instance Fields

    private readonly KryptonKnobAlternateValues _knobValues;
    private readonly PaletteKnobStatesOverride _overrideNormal;
    private readonly PaletteKnobFaceStatesOverride _overrideTracking;
    private readonly PaletteKnobFaceStatesOverride _overridePressed;
    private readonly PaletteRedirect _paletteRedirect;

    private bool _scaleTypefaceAutoSize = true;
    private bool _drawDivInside;
    private bool _showSmallScale = false;
    private bool _showLargeScale = true;
    private bool _isFocused = false;
    private bool _isKnobRotating = false;
    private bool _mouseOver;
    private bool _captured;
    private bool _isFixed;
    private PaletteState _elementState = PaletteState.Normal;

    private Brush? _brushKnob;
    private Brush? _brushKnobPointer;

    private int _minimum = 0;
    private int _maximum = 25;
    private int _largeChange = 5;
    private int _smallChange = 1;
    private int _scaleDivisions;
    private int _scaleSubDivisions;
    private int _mouseWheelBarPartitions = 10;
    private int _value = 0;

    private Font _scaleTypeface;
    private Font _knobTypeface;

    private float _startAngle = 135;
    private float _endAngle = 405;
    private float _deltaAngle;
    private float _drawRatio = 1;
    private float _gradLength = 4;

    private Rectangle _rKnob;

    private Point _pKnob;

    private Pen _dottedPen;

    private Image _offScreenImage;

    private Graphics _gOffScreen;

    private KnobIndicatorShape _indicatorShape = KnobIndicatorShape.Circle;
    private KnobStyle _knobStyle = KnobStyle.Classic;

    private Rectangle _rBackplate;

    #endregion

    #region Events
    //-------------------------------------------------------
    // An event that clients can use to be notified whenever 
    // the Value is Changed.                                 
    //-------------------------------------------------------
    public event KnobValueChangedEventHandler ValueChanged;

    //-------------------------------------------------------
    // Invoke the ValueChanged event; called  when value     
    // is changed                                            
    //-------------------------------------------------------
    protected virtual void OnValueChanged(object? sender, KnobValueChangedEventArgs e) => ValueChanged?.Invoke(sender, e);
    #endregion

    #region Properties
    /// <summary>
    /// Gets access to enhanced knob value properties.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Groups enhanced knob graduation, appearance, and value properties.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonKnobAlternateValues KnobValues => _knobValues;

    private bool ShouldSerializeKnobValues() => !_knobValues.IsDefault;

    private void ResetKnobValues() => _knobValues.Reset();

    /// <summary>
    /// Gets industrial backplate settings drawn behind the knob.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Industrial mounting backplate shape, colours, and depth effects.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobAlternateBackplateValues Backplate => KnobValues.Backplate;

    /// <summary>
    /// Gets plate label settings drawn on the industrial backplate.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text labels drawn on the industrial backplate.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobPlateLabelsValues PlateLabels => KnobValues.PlateLabels;

    /// <summary>
    /// Gets or sets how the knob face is rendered.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Visual style of the knob face.")]
    [DefaultValue(KnobStyle.Classic)]
    public KnobStyle KnobStyle
    {
        get => KnobValues.KnobStyle;
        set => KnobValues.KnobStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font ScaleTypeface
    {
        get => KnobValues.ScaleTypeface;
        set => KnobValues.ScaleTypeface = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ScaleTypefaceAutoSize
    {
        get => KnobValues.ScaleTypefaceAutoSize;
        set => KnobValues.ScaleTypefaceAutoSize = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float StartAngle
    {
        get => KnobValues.StartAngle;
        set => KnobValues.StartAngle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float EndAngle
    {
        get => KnobValues.EndAngle;
        set => KnobValues.EndAngle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KnobIndicatorShape IndicatorShape
    {
        get => KnobValues.IndicatorShape;
        set => KnobValues.IndicatorShape = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MouseWheelBarPartitions
    {
        get => KnobValues.MouseWheelBarPartitions;
        set => KnobValues.MouseWheelBarPartitions = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DrawDivInside
    {
        get => KnobValues.DrawDivInside;
        set => KnobValues.DrawDivInside = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color ScaleColour
    {
        get => KnobValues.ScaleColour;
        set => KnobValues.ScaleColour = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color KnobBackColour
    {
        get => KnobValues.KnobBackColour;
        set => KnobValues.KnobBackColour = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ScaleDivisions
    {
        get => KnobValues.ScaleDivisions;
        set => KnobValues.ScaleDivisions = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ScaleSubDivisions
    {
        get => KnobValues.ScaleSubDivisions;
        set => KnobValues.ScaleSubDivisions = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowSmallScale
    {
        get => KnobValues.ShowSmallScale;
        set => KnobValues.ShowSmallScale = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowLargeScale
    {
        get => KnobValues.ShowLargeScale;
        set => KnobValues.ShowLargeScale = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Minimum
    {
        get => KnobValues.Minimum;
        set => KnobValues.Minimum = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Maximum
    {
        get => KnobValues.Maximum;
        set => KnobValues.Maximum = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int LargeChange
    {
        get => KnobValues.LargeChange;
        set => KnobValues.LargeChange = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SmallChange
    {
        get => KnobValues.SmallChange;
        set => KnobValues.SmallChange = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Value
    {
        get => KnobValues.Value;
        set => KnobValues.Value = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color PointerColour
    {
        get => KnobValues.PointerColour;
        set => KnobValues.PointerColour = value;
    }

    /// <summary>
    /// Gets access to the common knob appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common knob appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the knob appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining knob appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets access to the disabled knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled knob appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal knob appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the tracking knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking knob appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobFaceStates StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed knob appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobFaceStates StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBackStyle BackStyle
    {
        get => KnobValues.BackStyle;
        set => KnobValues.BackStyle = value;
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public void SetFixedState(PaletteState state)
    {
        _isFixed = true;
        _elementState = state;
        UpdateBrushes();
        Invalidate();
    }
    #endregion


    #region Identity

    public KryptonKnobAlternate()
    {
        InitializeComponent();

        _knobValues = new KryptonKnobAlternateValues(this);

        _paletteRedirect = new PaletteRedirect(KryptonManager.CurrentGlobalPalette);

        StateCommon = new PaletteKnobRedirect(_paletteRedirect, OnNeedPaint);
        OverrideFocus = new PaletteKnobRedirect(_paletteRedirect, OnNeedPaint);
        StateDisabled = new PaletteKnobStates(StateCommon, OnNeedPaint);
        StateNormal = new PaletteKnobStates(StateCommon, OnNeedPaint);
        StateTracking = new PaletteKnobFaceStates(StateCommon, OnNeedPaint);
        StatePressed = new PaletteKnobFaceStates(StateCommon, OnNeedPaint);

        _overrideNormal = new PaletteKnobStatesOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride);
        _overrideTracking = new PaletteKnobFaceStatesOverride(OverrideFocus, StateTracking, PaletteState.FocusOverride);
        _overridePressed = new PaletteKnobFaceStatesOverride(OverrideFocus, StatePressed, PaletteState.FocusOverride);

        _dottedPen = new Pen(GetDarkColour(BackColor, 40)) { DashStyle = DashStyle.Dash, DashCap = DashCap.Flat };

        _knobTypeface = new Font(Font.FontFamily, Font.Size);

        _scaleTypeface = new Font(Font.FontFamily, Font.Size);

        // Properties initialisation

        // "start angle" and "end angle" possible values:

        // 90 = bottom (minimum value for "start angle")
        // 180 = left
        // 270 = top
        // 360 = right
        // 450 = bottom again (maximum value for "end angle")

        // So the couple (90, 450) will give an entire circle and the couple (180, 360) will give half a circle.

        _startAngle = 135;
        _endAngle = 405;
        _deltaAngle = _endAngle - _startAngle;

        _minimum = 0;
        _maximum = 100;
        _scaleDivisions = 11;
        _scaleSubDivisions = 4;
        _mouseWheelBarPartitions = 10;

        InitializePaletteFromBase();

        SetDimensions();

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    #endregion

    #region Implementation

    protected override void OnPaint(PaintEventArgs e)
    {
        if (_gOffScreen == null)
        {
            return;
        }

        Graphics g = e.Graphics;
        var backplateSettings = Backplate.GetSettings();

        _gOffScreen.Clear(BackColor);
        _gOffScreen.SmoothingMode = SmoothingMode.AntiAlias;

        if (backplateSettings.Shape != KnobBackplateShape.None)
        {
            KnobIndustrialDrawing.DrawBackplate(_gOffScreen, backplateSettings, _rBackplate, _rKnob);

            if (backplateSettings.ShowDropShadow)
            {
                KnobIndustrialDrawing.DrawKnobDropShadow(_gOffScreen, _rKnob);
            }
        }

        var facePalette = GetFacePalette();
        var paletteState = GetPaletteState();
        var faceColor1 = facePalette.GetElementColor1(paletteState);
        var faceColor2 = facePalette.GetElementColor2(paletteState);
        var borderColor = facePalette.GetElementColor3(paletteState);
        if (borderColor == GlobalStaticVariables.EMPTY_COLOR)
        {
            borderColor = facePalette.GetElementColor1(paletteState);
        }

        if (_knobStyle == KnobStyle.Classic && backplateSettings.Shape == KnobBackplateShape.None && _brushKnob != null)
        {
            _gOffScreen.FillEllipse(_brushKnob, _rKnob);
            _gOffScreen.DrawEllipse(new Pen(borderColor), _rKnob);
        }
        else
        {
            var drawColor1 = _knobStyle == KnobStyle.Classic ? GetLightColour(faceColor1, 55) : faceColor1;
            var drawColor2 = faceColor2 == GlobalStaticVariables.EMPTY_COLOR ? faceColor1 : faceColor2;
            if (_knobStyle == KnobStyle.Classic)
            {
                drawColor2 = GetDarkColour(drawColor2, 55);
            }

            KnobIndustrialDrawing.DrawKnobFace(
                _gOffScreen,
                _knobStyle,
                _rKnob,
                _pKnob,
                drawColor1,
                drawColor2,
                borderColor,
                BackColor);
        }

        if (_isFocused)
        {
            _dottedPen.Color = borderColor;
            _gOffScreen.DrawEllipse(_dottedPen, _rKnob);
        }

        DrawPointer(_gOffScreen);
        DrawDivisions(_gOffScreen, _rKnob);

        if (backplateSettings.Shape != KnobBackplateShape.None)
        {
            var plateCenter = new Point(
                _rBackplate.X + _rBackplate.Width / 2,
                _rBackplate.Y + _rBackplate.Height / 2);

            KnobIndustrialDrawing.DrawPlateLabels(
                _gOffScreen,
                PlateLabels.GetVisibleLabels(),
                plateCenter,
                _rBackplate.Width / 2f,
                _scaleTypeface);
        }

        g.DrawImage(_offScreenImage, 0, 0);
    }

    protected override void OnGotFocus(EventArgs e)
    {
        if (!_isFixed)
        {
            _overrideNormal.Apply = true;
            _overrideTracking.Apply = true;
            _overridePressed.Apply = true;
            UpdateBrushes();
            Invalidate();
        }

        base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
        if (!_isFixed)
        {
            _overrideNormal.Apply = false;
            _overrideTracking.Apply = false;
            _overridePressed.Apply = false;
            UpdateBrushes();
            Invalidate();
        }

        base.OnLostFocus(e);
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        UpdateBrushes();
        Invalidate();
        base.OnEnabledChanged(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        _mouseOver = IsPointInRectangle(PointToClient(MousePosition), _rKnob);
        UpdateTargetState();
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _mouseOver = false;
        _captured = false;
        UpdateTargetState();
        base.OnMouseLeave(e);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {

    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        _mouseOver = IsPointInRectangle(new Point(e.X, e.Y), _rKnob);

        if (IsPointInRectangle(new Point(e.X, e.Y), _rKnob))
        {
            _captured = e.Button == MouseButtons.Left;

            if (_isFocused)
            {
                // was already selected
                // Start Rotation of knob only if it was selected before        
                _isKnobRotating = true;
            }
            else
            {
                // Was not selected before => select it
                Focus();
                _isFocused = true;
                _isKnobRotating = false; // disallow rotation, must click again
                // draw dotted border to show that it is selected
                Invalidate();
            }
        }

        UpdateTargetState();
    }

    protected override bool IsInputKey(Keys keyData) =>
        keyData switch
        {
            Keys.Up or Keys.Down or Keys.Right or Keys.Left => true,
            _ => base.IsInputKey(keyData)
        };

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (IsPointInRectangle(new Point(e.X, e.Y), _rKnob))
        {
            if (_isFocused == true && _isKnobRotating == true)
            {
                // Change value is allowed only only after 2nd click                   
                Value = GetValueFromPosition(new Point(e.X, e.Y));
            }
            else
            {
                // 1st click = only focus
                _isFocused = true;
                _isKnobRotating = true;
            }

        }

        _captured = false;
        _mouseOver = IsPointInRectangle(new Point(e.X, e.Y), _rKnob);
        Cursor = Cursors.Default;
        UpdateTargetState();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        //--------------------------------------
        //  Following Handles Knob Rotating     
        //--------------------------------------
        var wasMouseOver = _mouseOver;
        _mouseOver = IsPointInRectangle(new Point(e.X, e.Y), _rKnob);
        if (wasMouseOver != _mouseOver)
        {
            UpdateTargetState();
        }

        if (e.Button == MouseButtons.Left && _isKnobRotating)
        {
            Cursor = Cursors.Hand;
            Point p = new Point(e.X, e.Y);
            int posVal = GetValueFromPosition(p);
            Value = posVal;
        }
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);

        if (_isFocused && _isKnobRotating && IsPointInRectangle(new Point(e.X, e.Y), _rKnob))
        {
            // the Delta value is always 120, as explained in MSDN
            int v = e.Delta / 120 * (_maximum - _minimum) / _mouseWheelBarPartitions;
            SetProperValue(Value + v);

            // Avoid to send MouseWheel event to the parent container
            ((HandledMouseEventArgs)e).Handled = true;
        }
    }

    protected override void OnLeave(EventArgs e)
    {
        // unselect the control (remove dotted border)
        _isFocused = false;
        _isKnobRotating = false;
        _mouseOver = false;
        _captured = false;
        UpdateTargetState();
        Invalidate();

        base.OnLeave(EventArgs.Empty);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (_isFocused)
        {
            //--------------------------------------------------------
            // Handles knob rotation with up,down,left and right keys 
            //--------------------------------------------------------
            if (e.KeyCode is Keys.Up or Keys.Right)
            {
                if (_value < _maximum)
                {
                    Value = _value + 1;
                }

                Refresh();
            }
            else if (e.KeyCode is Keys.Down or Keys.Left)
            {
                if (_value > _minimum)
                {
                    Value = _value - 1;
                }

                Refresh();
            }
        }
    }

    /// <summary>
    /// Draw the pointer of the knob (a small button inside the main button)
    /// </summary>
    /// <param name="g"></param>
    private void DrawPointer(Graphics g)
    {
        var indicatorPalette = GetIndicatorPalette();
        var paletteState = GetPaletteState();
        var indicatorBegin = indicatorPalette.GetElementColor1(paletteState);
        var indicatorEnd = indicatorPalette.GetElementColor2(paletteState);
        if (indicatorEnd == GlobalStaticVariables.EMPTY_COLOR)
        {
            indicatorEnd = indicatorBegin;
        }

        var indicatorBorder = indicatorPalette.GetElementColor3(paletteState);
        if (indicatorBorder == GlobalStaticVariables.EMPTY_COLOR)
        {
            indicatorBorder = indicatorBegin;
        }

        float radius = _rKnob.Width / 2;

        // Draw a line
        if (_indicatorShape == KnobIndicatorShape.Line)
        {
            int l = (int)radius / 2;
            int w = l / 4;
            Point[] pt = GetKnobLine(g, l);

            g.DrawLine(new Pen(indicatorBegin, w), pt[0], pt[1]);
        }
        else if (_indicatorShape == KnobIndicatorShape.Bar)
        {
            var angle = _deltaAngle * (Value - _minimum) / (float)(_maximum - _minimum) + _startAngle;
            KnobIndustrialDrawing.DrawIndustrialBar(g, _pKnob, _rKnob, angle, indicatorBegin, indicatorBorder);
        }
        else if (_indicatorShape == KnobIndicatorShape.GlowDot)
        {
            var dotSize = Math.Max(6, (int)Math.Round(radius * 0.14));
            var arrow = GetKnobPosition((int)radius - dotSize / 2 - 2);
            var rect = new RectangleF(arrow.X - dotSize / 2f, arrow.Y - dotSize / 2f, dotSize, dotSize);
            KnobIndustrialDrawing.DrawGlowDot(g, rect, indicatorBegin);
        }
        else
        {
            // Draw a circle
            int w = 0;
            int h = 0;
            int l = 0;

            string strvalmax = _maximum.ToString();
            string strvalmin = _minimum.ToString();
            string strval = strvalmax.Length > strvalmin.Length ? strvalmax : strvalmin;
            double val = Convert.ToDouble(strval);
            String str = $"{(int)val,0:D}";

            float fSize;
            SizeF strsize;
            if (_scaleTypefaceAutoSize)
            {
                // Use font family = _scaleTypeface, but size = automatic
                fSize = 6F * _drawRatio;
                if (fSize < 6)
                {
                    fSize = 6;
                }

                strsize = g.MeasureString(str, new Font(_scaleTypeface.FontFamily, fSize));
            }
            else
            {
                // Use font family = _scaleTypeface, but size = fixed
                fSize = _scaleTypeface.Size;
                strsize = g.MeasureString(str, _scaleTypeface);
            }

            int strw = (int)strsize.Width;
            int strh = (int)strsize.Height;
            w = Math.Max(strw, strh);
            // radius of small circle
            l = (int)radius - w / 2;

            h = w;

            Point Arrow = GetKnobPosition(l - 2); // Remove 2 pixels to offset the small circle inside the knob

            // Draw pointer arrow that shows knob position             
            Rectangle rPointer = new Rectangle(Arrow.X - w / 2, Arrow.Y - w / 2, w, h);


            DrawInsetCircle(ref g, rPointer, new Pen(GetLightColour(indicatorBorder, 55)));

            if (_brushKnobPointer != null)
            {
                g.FillEllipse(_brushKnobPointer, rPointer);
            }

        }
    }

    /// <summary>
    /// Draw graduations
    /// </summary>
    /// <param name="g"></param>
    /// <param name="rc">Knob rectangle</param>
    /// <returns></returns>
    private bool DrawDivisions(Graphics g, RectangleF rc)
    {
        if (this == null)
        {
            return false;
        }

        float cx = _pKnob.X;
        float cy = _pKnob.Y;

        float w = rc.Width;
        float h = rc.Height;

        float tx;
        float ty;

        float incr = GetRadian((_endAngle - _startAngle) / ((_scaleDivisions - 1) * (_scaleSubDivisions + 1)));
        float currentAngle = GetRadian(_startAngle);

        float radius = rc.Width / 2;
        float rulerValue = _minimum;

        Font font;

        var tickPalette = GetTickPalette();
        var tickColor = tickPalette.GetElementColor1(GetTickPaletteState());

        Pen penL = new Pen(tickColor, 2 * _drawRatio);
        Pen penS = new Pen(tickColor, 1 * _drawRatio);

        SolidBrush br = new SolidBrush(tickColor);

        PointF ptStart = new PointF(0, 0);
        PointF ptEnd = new PointF(0, 0);
        int n = 0;

        if (_showLargeScale)
        {
            // Size of maxi string
            string strvalmax = _maximum.ToString();
            string strvalmin = _minimum.ToString();
            string strval = strvalmax.Length > strvalmin.Length ? strvalmax : strvalmin;
            double val = Convert.ToDouble(strval);
            //double val = _maximum;
            string str = $"{(int)val,0:D}";
            float fSize;
            SizeF strsize;

            if (_scaleTypefaceAutoSize)
            {
                fSize = 6F * _drawRatio;
                if (fSize < 6)
                {
                    fSize = 6;
                }
            }
            else
            {
                fSize = _scaleTypeface.Size;
            }

            font = new Font(_scaleTypeface.FontFamily, fSize);
            strsize = g.MeasureString(str, font);

            int strw = (int)strsize.Width;
            int strh = (int)strsize.Height;
            int wmax = Math.Max(strw, strh);

            float l = 0;
            _gradLength = 2 * _drawRatio;

            for (; n < _scaleDivisions; n++)
            {
                // draw divisions
                ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
                ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));

                ptEnd.X = (float)(cx + (radius + _gradLength) * Math.Cos(currentAngle));
                ptEnd.Y = (float)(cy + (radius + _gradLength) * Math.Sin(currentAngle));

                g.DrawLine(penL, ptStart, ptEnd);


                //Draw graduation values                                                                                
                val = Math.Round(rulerValue);
                str = $"{(int)val,0:D}";

                // If autosize
                if (_scaleTypefaceAutoSize)
                {
                    strsize = g.MeasureString(str, new Font(_scaleTypeface.FontFamily, fSize));
                }
                else
                {
                    strsize = g.MeasureString(str, new Font(_scaleTypeface.FontFamily, _scaleTypeface.Size));
                }


                if (_drawDivInside)
                {
                    // graduations values inside the knob                        
                    l = (int)radius - wmax / 2 - 2;

                    tx = (float)(cx + l * Math.Cos(currentAngle));
                    ty = (float)(cy + l * Math.Sin(currentAngle));

                }
                else
                {
                    // graduation values outside the knob 
                    //l = (Width / 2) - (wmax / 2) ;
                    l = radius + _gradLength + wmax / 2;

                    tx = (float)(cx + l * Math.Cos(currentAngle));
                    ty = (float)(cy + l * Math.Sin(currentAngle));
                }

                g.DrawString(str,
                    font,
                    br,
                    tx - (float)(strsize.Width * 0.5),
                    ty - (float)(strsize.Height * 0.5));



                rulerValue += (_maximum - _minimum) / (_scaleDivisions - 1);

                if (n == _scaleDivisions - 1)
                {
                    break;
                }


                // Subdivisions
                #region SubDivisions

                if (_scaleDivisions <= 0)
                {
                    currentAngle += incr;
                }
                else
                {

                    for (int j = 0; j <= _scaleSubDivisions; j++)
                    {
                        currentAngle += incr;

                        // if user want to display small graduations
                        if (_showSmallScale)
                        {
                            ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
                            ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));
                            ptEnd.X = (float)(cx + (radius + _gradLength / 2) * Math.Cos(currentAngle));
                            ptEnd.Y = (float)(cy + (radius + _gradLength / 2) * Math.Sin(currentAngle));

                            g.DrawLine(penS, ptStart, ptEnd);
                        }
                    }
                }
                #endregion                    
            }
            font.Dispose();
        }

        return true;
    }

    /// <summary>
    /// Set position of button inside its rectangle to insure that divisions will fit.
    /// </summary>
    private void SetDimensions()
    {
        var backplateShape = Backplate.Shape;

        if (backplateShape != KnobBackplateShape.None)
        {
            var clientRect = new Rectangle(0, 0, Width, Height);
            KnobIndustrialDrawing.ComputeGeometry(clientRect, backplateShape, out _rBackplate, out _rKnob, out _pKnob);

            _drawRatio = _rKnob.Width / 150f;
            if (_drawRatio == 0.0)
            {
                _drawRatio = 1;
            }

            _gOffScreen?.Dispose();
            _offScreenImage?.Dispose();
            _offScreenImage = new Bitmap(Width, Height);
            _gOffScreen = Graphics.FromImage(_offScreenImage);
            UpdateBrushes();
            return;
        }

        _rBackplate = Rectangle.Empty;
        Font font;

        // Rectangle
        float x, y, w, h;
        x = 0;
        y = 0;
        w = h = Width;

        // Calculate ratio
        _drawRatio = w / 150;
        if (_drawRatio == 0.0)
        {
            _drawRatio = 1;
        }


        if (_showLargeScale)
        {
            Graphics Gr = CreateGraphics();
            string strvalmax = _maximum.ToString();
            string strvalmin = _minimum.ToString();
            string strval = strvalmax.Length > strvalmin.Length ? strvalmax : strvalmin;
            double val = Convert.ToDouble(strval);

            //double val = _maximum;
            String str = $"{(int)val,0:D}";

            float fSize = _scaleTypeface.Size;

            if (_scaleTypefaceAutoSize)
            {
                fSize = 6F * _drawRatio;
                if (fSize < 6)
                {
                    fSize = 6;
                }

                font = new Font(_scaleTypeface.FontFamily, fSize);
            }
            else
            {
                fSize = _scaleTypeface.Size;
                font = new Font(_scaleTypeface.FontFamily, _scaleTypeface.Size);
            }

            SizeF strsize = Gr.MeasureString(str, font);

            // Graduations outside
            _gradLength = 4 * _drawRatio;

            if (_drawDivInside)
            {
                // Graduations inside : remove only 2*8 pixels
                //x = y = 8;
                x = y = _gradLength;
                w = Width - 2 * x;
            }
            else
            {
                // remove 2 * size of text and length of graduation
                //_gradLength = 4 * _drawRatio;
                int strw = (int)strsize.Width;
                int strh = (int)strsize.Height;

                int max = Math.Max(strw, strh);
                x = max;
                y = max;
                w = (int)(Width - 2 * max - _gradLength);
            }

            if (w <= 0)
            {
                w = 1;
            }

            h = w;

            // Rectangle of the rounded knob
            _rKnob = new Rectangle((int)x, (int)y, (int)w, (int)h);

            Gr.Dispose();
        }
        else
        {
            _rKnob = new Rectangle(0, 0, Width, Height);
        }


        // Center of knob
        _pKnob = new Point(_rKnob.X + _rKnob.Width / 2, _rKnob.Y + _rKnob.Height / 2);

        // create offscreen image
        _gOffScreen?.Dispose();
        _offScreenImage?.Dispose();
        _offScreenImage = new Bitmap(Width, Height);
        // create offscreen graphics
        _gOffScreen = Graphics.FromImage(_offScreenImage);


        // Depends on retangle dimensions
        UpdateBrushes();
    }

    private void UpdateBrushes()
    {
        if (_rKnob.Width <= 0 || _rKnob.Height <= 0)
        {
            return;
        }

        var facePalette = GetFacePalette();
        var paletteState = GetPaletteState();
        var faceColor1 = facePalette.GetElementColor1(paletteState);
        var faceColor2 = facePalette.GetElementColor2(paletteState);
        if (faceColor2 == GlobalStaticVariables.EMPTY_COLOR)
        {
            faceColor2 = faceColor1;
        }

        _brushKnob?.Dispose();
        _brushKnob = new LinearGradientBrush(
            _rKnob, GetLightColour(faceColor1, 55), GetDarkColour(faceColor2, 55), LinearGradientMode.ForwardDiagonal);

        var indicatorPalette = GetIndicatorPalette();
        var indicatorBegin = indicatorPalette.GetElementColor1(paletteState);
        var indicatorEnd = indicatorPalette.GetElementColor2(paletteState);
        if (indicatorEnd == GlobalStaticVariables.EMPTY_COLOR)
        {
            indicatorEnd = indicatorBegin;
        }

        _brushKnobPointer?.Dispose();
        _brushKnobPointer = new LinearGradientBrush(
            _rKnob, GetLightColour(indicatorBegin, 55), GetDarkColour(indicatorEnd, 55), LinearGradientMode.ForwardDiagonal);
    }

    /// <summary>
    /// Sets the trackbar value so that it wont exceed allowed range.
    /// </summary>
    /// <param name="val">The value.</param>
    private void SetProperValue(int val)
    {
        if (val < _minimum)
        {
            Value = _minimum;
        }
        else if (val > _maximum)
        {
            Value = _maximum;
        }
        else
        {
            Value = val;
        }
    }

    /// <summary>
    /// gets knob position that is to be drawn on control minus a small amount in order that the knob position stay inside the circle.
    /// </summary>
    /// <returns>Point that describes current knob position</returns>
    private Point GetKnobPosition(int l)
    {
        float cx = _pKnob.X;
        float cy = _pKnob.Y;

        // FAB: 21/08/18            
        float degree = _deltaAngle * (Value - _minimum) / (_maximum - _minimum);

        degree = GetRadian(degree + _startAngle);

        Point Pos = new Point(0, 0)
        {
            X = (int)(cx + l * Math.Cos(degree)),
            Y = (int)(cy + l * Math.Sin(degree))
        };

        return Pos;
    }

    /// <summary>
    /// return 2 points of a line starting from the center of the knob to the periphery
    /// </summary>
    /// <param name="g"></param>
    /// <param name="l"></param>
    /// <returns></returns>
    private Point[] GetKnobLine(Graphics g, int l)
    {
        Point[] pret = new Point[2];

        float cx = _pKnob.X;
        float cy = _pKnob.Y;


        float radius = _rKnob.Width / 2;

        // FAB: 21/08/18            
        float degree = _deltaAngle * (Value - _minimum) / (_maximum - _minimum);

        degree = GetRadian(degree + _startAngle);


        double val = _maximum;
        string str = $"{(int)val,0:D}";
        float fSize;
        SizeF strsize;

        if (!_scaleTypefaceAutoSize)
        {
            fSize = _scaleTypeface.Size;
            strsize = g.MeasureString(str, _scaleTypeface);
        }
        else
        {
            fSize = 6F * _drawRatio;
            if (fSize < 6)
            {
                fSize = 6;
            }

            _knobTypeface = new Font(_scaleTypeface.FontFamily, fSize);
            strsize = g.MeasureString(str, _knobTypeface);
        }

        int strw = (int)strsize.Width;
        int strh = (int)strsize.Height;
        int w = Math.Max(strw, strh);


        Point Pos = new Point(0, 0);

        if (_drawDivInside)
        {
            // Center (from)
            Pos.X = (int)(cx + radius / 10 * Math.Cos(degree));
            Pos.Y = (int)(cy + radius / 10 * Math.Sin(degree));
            pret[0] = new Point(Pos.X, Pos.Y);

            // External (to)
            Pos.X = (int)(cx + (radius - w) * Math.Cos(degree));
            Pos.Y = (int)(cy + (radius - w) * Math.Sin(degree));
            pret[1] = new Point(Pos.X, Pos.Y);
        }
        else
        {
            // Internal (from)
            Pos.X = (int)(cx + (radius - _drawRatio * 10 - l) * Math.Cos(degree));
            Pos.Y = (int)(cy + (radius - _drawRatio * 10 - l) * Math.Sin(degree));


            pret[0] = new Point(Pos.X, Pos.Y);

            // External (to)
            Pos.X = (int)(cx + (radius - 4) * Math.Cos(degree));
            Pos.Y = (int)(cy + (radius - 4) * Math.Sin(degree));

            pret[1] = new Point(Pos.X, Pos.Y);
        }
        return pret;
    }

    /// <summary>
    /// converts geometrical position into value..
    /// </summary>
    /// <param name="p">Point that is to be converted</param>
    /// <returns>Value derived from position</returns>
    private int GetValueFromPosition(Point p)
    {
        float degree = 0;
        int v = 0;

        if (p.X <= _pKnob.X)
        {
            degree = (_pKnob.Y - p.Y) / (float)(_pKnob.X - p.X);
            degree = (float)Math.Atan(degree);

            degree = degree * (float)(180 / Math.PI) + (180 - _startAngle);

        }
        else if (p.X > _pKnob.X)
        {
            degree = (p.Y - _pKnob.Y) / (float)(p.X - _pKnob.X);
            degree = (float)Math.Atan(degree);

            degree = degree * (float)(180 / Math.PI) + 360 - _startAngle;
        }

        // round to the nearest value (when you click just before or after a graduation!)
        // FAB: 25/08/18            
        v = _minimum + (int)Math.Round(degree * (_maximum - _minimum) / _deltaAngle);

        if (v > _maximum)
        {
            v = _maximum;
        }

        if (v < _minimum)
        {
            v = _minimum;
        }

        return v;
    }

    private void InitializePaletteFromBase()
    {
        StateCommon.PopulateFromBase(PaletteState.Normal);
        StateNormal.PopulateFromBase(PaletteState.Normal);
        StateDisabled.PopulateFromBase(PaletteState.Disabled);
        StateTracking.PopulateFromBase(PaletteState.Tracking);
        StatePressed.PopulateFromBase(PaletteState.Pressed);

        BackColor = KryptonManager.CurrentGlobalPalette.ColorTable.MenuStripGradientBegin;
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (e.NeedLayout)
        {
            SetDimensions();
        }

        UpdateBrushes();
        Invalidate();
    }

    private void UpdateTargetState()
    {
        if (_isFixed)
        {
            return;
        }

        var newState = PaletteState.Normal;

        if (_mouseOver)
        {
            newState = _captured ? PaletteState.Pressed : PaletteState.Tracking;
        }

        if (_elementState != newState)
        {
            _elementState = newState;
            UpdateBrushes();
            Invalidate();
        }
    }

    private PaletteState GetPaletteState() => !Enabled ? PaletteState.Disabled : _elementState;

    private IPaletteElementColor GetFacePalette()
    {
        if (!Enabled)
        {
            return StateDisabled.Face;
        }

        return _elementState switch
        {
            PaletteState.Tracking => _overrideTracking.Face,
            PaletteState.Pressed => _overridePressed.Face,
            _ => _overrideNormal.Face
        };
    }

    private IPaletteElementColor GetTickPalette() => Enabled ? _overrideNormal.Tick : StateDisabled.Tick;

    private PaletteState GetTickPaletteState() => Enabled ? PaletteState.Normal : PaletteState.Disabled;

    private IPaletteElementColor GetIndicatorPalette() => Enabled ? _overrideNormal.Indicator : StateDisabled.Indicator;

    private void KryptonKnobControlEnhanced_Resize(object? sender, EventArgs e)
    {
        // Control remains square
        Height = Width;

        SetDimensions();

        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        _paletteRedirect.Target = KryptonManager.CurrentGlobalPalette;
        InitializePaletteFromBase();
        UpdateBrushes();
        Invalidate();
    }

    #region Knob Values Accessors
    internal bool ScaleTypefaceMatchesControlFont() =>
        _scaleTypeface.FontFamily.Equals(Font.FontFamily) &&
        _scaleTypeface.Size.Equals(Font.Size);

    internal Font GetScaleTypeface() => _scaleTypeface;

    internal void SetScaleTypeface(Font value)
    {
        _scaleTypeface = value;
        SetDimensions();
        Invalidate();
    }

    internal bool GetScaleTypefaceAutoSize() => _scaleTypefaceAutoSize;

    internal void SetScaleTypefaceAutoSize(bool value)
    {
        _scaleTypefaceAutoSize = value;
        SetDimensions();
        Invalidate();
    }

    internal float GetStartAngle() => _startAngle;

    internal void SetStartAngle(float value)
    {
        if (value >= 90 && value < _endAngle)
        {
            _startAngle = value;
            _deltaAngle = _endAngle - _startAngle;
            Invalidate();
        }
    }

    internal float GetEndAngle() => _endAngle;

    internal void SetEndAngle(float value)
    {
        if (value <= 450 && value > _startAngle)
        {
            _endAngle = value;
            _deltaAngle = _endAngle - _startAngle;
            Invalidate();
        }
    }

    internal KnobIndicatorShape GetIndicatorShape() => _indicatorShape;

    internal void SetIndicatorShape(KnobIndicatorShape value)
    {
        _indicatorShape = value;
        Invalidate();
    }

    internal int GetMouseWheelBarPartitions() => _mouseWheelBarPartitions;

    internal void SetMouseWheelBarPartitions(int value)
    {
        if (value > 0)
        {
            _mouseWheelBarPartitions = value;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(value), @"MouseWheelBarPartitions has to be greater than zero");
        }
    }

    internal bool GetDrawDivInside() => _drawDivInside;

    internal void SetDrawDivInside(bool value)
    {
        _drawDivInside = value;
        SetDimensions();
        Invalidate();
    }

    internal Color GetScaleColour() => StateCommon.Tick.Color1;

    internal void SetScaleColour(Color value)
    {
        StateCommon.Tick.Color1 = value;
        Invalidate();
    }

    internal void OnBackplateChanged()
    {
        SetDimensions();
        Invalidate();
    }

    internal void OnPlateLabelsChanged() => Invalidate();

    internal KnobStyle GetKnobStyle() => _knobStyle;

    internal void SetKnobStyle(KnobStyle value)
    {
        if (_knobStyle != value)
        {
            _knobStyle = value;
            Invalidate();
        }
    }

    internal Color GetKnobBackColour() => StateCommon.Face.Color1;

    internal void SetKnobBackColour(Color value)
    {
        StateCommon.Face.Color1 = value;
        SetDimensions();
        Invalidate();
    }

    internal int GetScaleDivisions() => _scaleDivisions;

    internal void SetScaleDivisions(int value)
    {
        if (value > 1)
        {
            _scaleDivisions = value;
            Invalidate();
        }
    }

    internal int GetScaleSubDivisions() => _scaleSubDivisions;

    internal void SetScaleSubDivisions(int value)
    {
        if (value > 0 && _scaleDivisions > 0 && (_maximum - _minimum) / (value * _scaleDivisions) > 0)
        {
            _scaleSubDivisions = value;
            Invalidate();
        }
    }

    internal bool GetShowSmallScale() => _showSmallScale;

    internal void SetShowSmallScale(bool value)
    {
        if (value)
        {
            if (_scaleDivisions > 0 && _scaleSubDivisions > 0 && (_maximum - _minimum) / (_scaleSubDivisions * _scaleDivisions) > 0)
            {
                _showSmallScale = value;
                Invalidate();
            }
        }
        else
        {
            _showSmallScale = value;
            Invalidate();
        }
    }

    internal bool GetShowLargeScale() => _showLargeScale;

    internal void SetShowLargeScale(bool value)
    {
        _showLargeScale = value;
        SetDimensions();
        Invalidate();
    }

    internal int GetMinimum() => _minimum;

    internal void SetMinimum(int value)
    {
        _minimum = value;
        Invalidate();
    }

    internal int GetMaximum() => _maximum;

    internal void SetMaximum(int value)
    {
        if (value > _minimum)
        {
            _maximum = value;

            if (_scaleSubDivisions > 0 && _scaleDivisions > 0 && (_maximum - _minimum) / (_scaleSubDivisions * _scaleDivisions) <= 0)
            {
                _showSmallScale = false;
            }

            SetDimensions();
            Invalidate();
        }
    }

    internal int GetLargeChange() => _largeChange;

    internal void SetLargeChange(int value)
    {
        _largeChange = value;
        Invalidate();
    }

    internal int GetSmallChange() => _smallChange;

    internal void SetSmallChange(int value)
    {
        _smallChange = value;
        Invalidate();
    }

    internal int GetValue() => _value;

    internal void SetValue(int value)
    {
        if (value >= _minimum && value <= _maximum)
        {
            _value = value;

            KnobValueChangedEventArgs e = new KnobValueChangedEventArgs(value);

            Invalidate();
            OnValueChanged(null, e);
        }
    }

    internal Color GetPointerColour() => StateCommon.Indicator.Color1;

    internal void SetPointerColour(Color value)
    {
        StateCommon.Indicator.Color1 = value;
        SetDimensions();
        Invalidate();
    }

    internal PaletteBackStyle GetBackStyle() => OverrideFocus.BackStyle;

    internal void SetBackStyle(PaletteBackStyle value)
    {
        if (OverrideFocus.BackStyle != value)
        {
            OverrideFocus.BackStyle = value;
            OnNeedPaint(this, new NeedLayoutEventArgs(true));
        }
    }
    #endregion

    public static float GetRadian(float val)
    {
        return (float)(val * Math.PI / 180);
    }


    public static Color GetDarkColour(Color c, byte d)
    {
        byte r = 0;
        byte g = 0;
        byte b = 0;

        if (c.R > d)
        {
            r = (byte)(c.R - d);
        }

        if (c.G > d)
        {
            g = (byte)(c.G - d);
        }

        if (c.B > d)
        {
            b = (byte)(c.B - d);
        }

        Color c1 = Color.FromArgb(r, g, b);
        return c1;
    }
    public static Color GetLightColour(Color c, byte d)
    {
        byte r = 255;
        byte g = 255;
        byte b = 255;

        if (c.R + d < 255)
        {
            r = (byte)(c.R + d);
        }

        if (c.G + d < 255)
        {
            g = (byte)(c.G + d);
        }

        if (c.B + d < 255)
        {
            b = (byte)(c.B + d);
        }

        Color c2 = Color.FromArgb(r, g, b);
        return c2;
    }

    /// <summary>
    /// Method which checks is particular point is in rectangle
    /// </summary>
    /// <param name="p">Point to be Chaecked</param>
    /// <param name="r">Rectangle</param>
    /// <returns>true is Point is in rectangle, else false</returns>
    public static bool IsPointInRectangle(Point p, Rectangle r)
    {
        bool flag = false || p.X > r.X && p.X < r.X + r.Width && p.Y > r.Y && p.Y < r.Y + r.Height;
        return flag;

    }

    public static void DrawInsetCircle(ref Graphics g, Rectangle r, Pen p)
    {

        Pen p1 = new Pen(GetDarkColour(p.Color, 50));
        Pen p2 = new Pen(GetLightColour(p.Color, 50));
        for (int i = 0; i < p.Width; i++)
        {
            Rectangle r1 = new Rectangle(r.X + i, r.Y + i, r.Width - i * 2, r.Height - i * 2);
            g.DrawArc(p2, r1, -45, 180);
            g.DrawArc(p1, r1, 135, 180);
        }
    }

    #endregion

}