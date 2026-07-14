#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Draw and operate a rotary knob.
/// </summary>
public class ViewDrawKnob : ViewDrawPanel
{
    #region Instance Fields
    private readonly NeedPaintHandler? _needPaint;
    private int _minimum;
    private int _maximum;
    private int _largeChange;
    private int _smallChange;
    private int _sizeLargeScaleMarker;
    private int _sizeSmallScaleMarker;
    private bool _showSmallScale;
    private bool _showLargeScale;
    private int _value;
    private Rectangle _rectKnob;
    private Point _knobCenter;
    private KnobIndicatorShape _indicatorShape;
    private int _indicatorSize;
    private PointF[]? _indicatorCustomPoints;
    private GraphicsPath? _indicatorCustomPath;
    private KnobStyle _knobStyle;
    private KnobBackplateShape _backplateShape;
    private Color _backplateColor1;
    private Color _backplateColor2;
    private Color _backplateBorderColor;
    private bool _showBackplateInsetWell;
    private bool _showBackplateDropShadow;
    private Rectangle _rectBackplate;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Value property changes.
    /// </summary>
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Occurs when the value changes because of a user action.
    /// </summary>
    public event EventHandler? Scroll;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawKnob class.
    /// </summary>
    /// <param name="stateNormal">Reference to normal state values.</param>
    /// <param name="stateDisabled">Reference to disabled state values.</param>
    /// <param name="stateTracking">Reference to tracking state values.</param>
    /// <param name="statePressed">Reference to pressed state values.</param>
    /// <param name="needPaint">Delegate used to request repainting.</param>
    public ViewDrawKnob(PaletteKnobStatesOverride stateNormal,
        PaletteKnobStates stateDisabled,
        PaletteKnobFaceStatesOverride stateTracking,
        PaletteKnobFaceStatesOverride statePressed,
        NeedPaintHandler needPaint)
        : base(stateNormal.Back)
    {
        StateNormal = stateNormal;
        StateDisabled = stateDisabled;
        StateTracking = stateTracking;
        StatePressed = statePressed;
        _needPaint = needPaint;

        _minimum = 0;
        _maximum = 100;
        _largeChange = 20;
        _smallChange = 5;
        _sizeLargeScaleMarker = 6;
        _sizeSmallScaleMarker = 3;
        _showLargeScale = true;
        _value = 0;
        _indicatorShape = KnobIndicatorShape.Circle;
        _indicatorSize = 6;
        _indicatorCustomPath = null;
        _knobStyle = KnobStyle.Classic;
        _backplateShape = KnobBackplateShape.None;
        _backplateColor1 = GlobalStaticVariables.EMPTY_COLOR;
        _backplateColor2 = GlobalStaticVariables.EMPTY_COLOR;
        _backplateBorderColor = GlobalStaticVariables.EMPTY_COLOR;
        _showBackplateInsetWell = true;
        _showBackplateDropShadow = true;

        var controller = new KnobController(this);
        MouseController = controller;
        KeyController = controller;
        SourceController = controller;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"ViewDrawKnob:{Id}";

    #endregion

    #region Public
    /// <summary>
    /// Gets access to the normal state.
    /// </summary>
    public PaletteKnobStatesOverride StateNormal { get; }

    /// <summary>
    /// Gets access to the disabled state.
    /// </summary>
    public PaletteKnobStates StateDisabled { get; }

    /// <summary>
    /// Gets access to the tracking state.
    /// </summary>
    public PaletteKnobFaceStatesOverride StateTracking { get; }

    /// <summary>
    /// Gets access to the pressed state.
    /// </summary>
    public PaletteKnobFaceStatesOverride StatePressed { get; }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) => FixedState = state;

    /// <summary>
    /// Gets or sets the minimum value.
    /// </summary>
    public int Minimum
    {
        get => _minimum;
        set => SetRange(value, _maximum);
    }

    /// <summary>
    /// Gets or sets the maximum value.
    /// </summary>
    public int Maximum
    {
        get => _maximum;
        set => SetRange(_minimum, value);
    }

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    public int Value
    {
        get => _value;
        set
        {
            if (value != _value)
            {
                if (value < _minimum || value > _maximum)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        @"Provided value is out of the Minimum to Maximum range of values.");
                }

                _value = value;
                OnValueChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Sets the value because of a user change.
    /// </summary>
    public int ScrollValue
    {
        set
        {
            if (value != _value)
            {
                if (value < _minimum || value > _maximum)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        @"Provided value is out of the Minimum to Maximum range of values.");
                }

                _value = value;
                OnScroll(EventArgs.Empty);
                OnValueChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the large change value.
    /// </summary>
    public int LargeChange
    {
        get => _largeChange;
        set => _largeChange = value;
    }

    /// <summary>
    /// Gets or sets the small change value.
    /// </summary>
    public int SmallChange
    {
        get => _smallChange;
        set => _smallChange = value;
    }

    /// <summary>
    /// Gets or sets the large scale marker length.
    /// </summary>
    public int SizeLargeScaleMarker
    {
        get => _sizeLargeScaleMarker;
        set => _sizeLargeScaleMarker = value;
    }

    /// <summary>
    /// Gets or sets the small scale marker length.
    /// </summary>
    public int SizeSmallScaleMarker
    {
        get => _sizeSmallScaleMarker;
        set => _sizeSmallScaleMarker = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether small scale marks are shown.
    /// </summary>
    public bool ShowSmallScale
    {
        get => _showSmallScale;
        set => _showSmallScale = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether large scale marks are shown.
    /// </summary>
    public bool ShowLargeScale
    {
        get => _showLargeScale;
        set => _showLargeScale = value;
    }

    /// <summary>
    /// Gets or sets the shape of the value indicator.
    /// </summary>
    public KnobIndicatorShape IndicatorShape
    {
        get => _indicatorShape;
        set => _indicatorShape = value;
    }

    /// <summary>
    /// Gets or sets the size of the value indicator in pixels.
    /// </summary>
    public int IndicatorSize
    {
        get => _indicatorSize;
        set => _indicatorSize = Math.Max(2, value);
    }

    /// <summary>
    /// Gets or sets normalized custom indicator points in local space (+X is outward; multiplied by half of <see cref="IndicatorSize"/>).
    /// </summary>
    public PointF[]? IndicatorCustomPoints
    {
        get => _indicatorCustomPoints;
        set => _indicatorCustomPoints = value;
    }

    /// <summary>
    /// Gets or sets a custom indicator path in local space (+X is outward; scaled by half of <see cref="IndicatorSize"/>).
    /// </summary>
    public GraphicsPath? IndicatorCustomPath
    {
        get => _indicatorCustomPath;
        set => _indicatorCustomPath = value;
    }

    /// <summary>
    /// Gets or sets how the knob face is rendered.
    /// </summary>
    public KnobStyle KnobStyle
    {
        get => _knobStyle;
        set => _knobStyle = value;
    }

    /// <summary>
    /// Gets or sets the industrial backplate shape.
    /// </summary>
    public KnobBackplateShape BackplateShape
    {
        get => _backplateShape;
        set => _backplateShape = value;
    }

    /// <summary>
    /// Gets or sets the primary backplate colour.
    /// </summary>
    public Color BackplateColor1
    {
        get => _backplateColor1;
        set => _backplateColor1 = value;
    }

    /// <summary>
    /// Gets or sets the secondary backplate colour.
    /// </summary>
    public Color BackplateColor2
    {
        get => _backplateColor2;
        set => _backplateColor2 = value;
    }

    /// <summary>
    /// Gets or sets the backplate border colour.
    /// </summary>
    public Color BackplateBorderColor
    {
        get => _backplateBorderColor;
        set => _backplateBorderColor = value;
    }

    /// <summary>
    /// Gets or sets whether an inset well is drawn on the backplate.
    /// </summary>
    public bool ShowBackplateInsetWell
    {
        get => _showBackplateInsetWell;
        set => _showBackplateInsetWell = value;
    }

    /// <summary>
    /// Gets or sets whether a drop shadow is drawn under the knob.
    /// </summary>
    public bool ShowBackplateDropShadow
    {
        get => _showBackplateDropShadow;
        set => _showBackplateDropShadow = value;
    }

    /// <summary>
    /// Sets the minimum and maximum values.
    /// </summary>
    /// <param name="minValue">The lower limit.</param>
    /// <param name="maxValue">The upper limit.</param>
    public void SetRange(int minValue, int maxValue)
    {
        if (_minimum == minValue && _maximum == maxValue)
        {
            return;
        }

        if (minValue > maxValue)
        {
            minValue = maxValue;
        }

        _minimum = minValue;
        _maximum = maxValue;

        var beforeValue = _value;
        if (_value < _minimum)
        {
            _value = _minimum;
        }

        if (_value > _maximum)
        {
            _value = _maximum;
        }

        if (beforeValue != _value)
        {
            OnValueChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Raises a need paint event.
    /// </summary>
    /// <param name="needLayout">Does the layout need recalculating.</param>
    public void PerformNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout));

    /// <summary>
    /// Determines if a point is inside the knob ellipse.
    /// </summary>
    /// <param name="pt">Point to test.</param>
    /// <returns>True if inside the knob area.</returns>
    public bool ContainsKnobPoint(Point pt) => _rectKnob.Contains(pt);

    /// <summary>
    /// Converts a point to the nearest knob value.
    /// </summary>
    /// <param name="p">Point to convert.</param>
    /// <returns>Value derived from position.</returns>
    public int GetValueFromPosition(Point p)
    {
        if (_maximum == _minimum)
        {
            return _minimum;
        }

        double degree;
        int v;

        if (p.X <= _knobCenter.X)
        {
            degree = (_knobCenter.Y - p.Y) / (double)(_knobCenter.X - p.X);
            degree = Math.Atan(degree);
            degree = degree * (180 / Math.PI) + 45;
            v = (int)Math.Round(degree * (_maximum - _minimum) / 270.0);
        }
        else
        {
            degree = (p.Y - _knobCenter.Y) / (double)(p.X - _knobCenter.X);
            degree = Math.Atan(degree);
            degree = 225 + degree * (180 / Math.PI);
            v = (int)Math.Round(degree * (_maximum - _minimum) / 270.0);
        }

        return Math.Max(_minimum, Math.Min(v, _maximum));
    }

    #endregion

    #region Layout
    /// <inheritdoc />
    public override void Layout(ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        ClientRectangle = context.DisplayRectangle;
        UpdateKnobGeometry();
        base.Layout(context);
    }
    #endregion

    #region Paint
    /// <inheritdoc />
    public override void RenderAfter(RenderContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Graphics is null)
        {
            throw new ArgumentNullException(nameof(context.Graphics));
        }

        var g = context.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Resolve the enabled state from the owning control so the disabled palette is used
        // even if the view element enabled flag has not yet been synchronised.
        var enabled = context.Control?.Enabled ?? Enabled;
        var paletteState = enabled ? State : PaletteState.Disabled;

        var facePalette = GetFacePalette(enabled);
        IPaletteElementColor tickPalette = GetTickPalette(enabled);
        IPaletteElementColor indicatorPalette = GetIndicatorPalette(enabled);

        var faceColor1 = facePalette.GetElementColor1(paletteState);
        var faceColor2 = facePalette.GetElementColor2(paletteState);
        var borderColor = facePalette.GetElementColor3(paletteState);
        if (borderColor == GlobalStaticVariables.EMPTY_COLOR)
        {
            borderColor = faceColor1;
        }

        // Many palettes return the same colour for the underlying track element regardless of
        // state, so mute the resolved colours to guarantee a visibly disabled appearance.
        if (!enabled)
        {
            faceColor1 = KnobColorUtility.GetDisabledColor(faceColor1);
            faceColor2 = KnobColorUtility.GetDisabledColor(faceColor2);
            borderColor = KnobColorUtility.GetDisabledColor(borderColor);
        }

        DrawBackplate(g, enabled);

        if (_showBackplateDropShadow && _backplateShape != KnobBackplateShape.None)
        {
            DrawKnobDropShadow(g);
        }

        DrawKnobFace(g, context, faceColor1, faceColor2, borderColor);

        if (context.Control is { Focused: true })
        {
            DrawFocusRing(g, borderColor);
        }

        var indicatorBegin = indicatorPalette.GetElementColor1(paletteState);
        var indicatorEnd = indicatorPalette.GetElementColor2(paletteState);
        if (indicatorEnd == GlobalStaticVariables.EMPTY_COLOR)
        {
            indicatorEnd = indicatorBegin;
        }

        var indicatorBorder = indicatorPalette.GetElementColor3(paletteState);
        if (indicatorBorder == GlobalStaticVariables.EMPTY_COLOR)
        {
            indicatorBorder = borderColor;
        }

        if (!enabled)
        {
            indicatorBegin = KnobColorUtility.GetDisabledColor(indicatorBegin);
            indicatorEnd = KnobColorUtility.GetDisabledColor(indicatorEnd);
            indicatorBorder = KnobColorUtility.GetDisabledColor(indicatorBorder);
        }

        var arrow = GetKnobPosition();
        DrawIndicator(g, arrow, indicatorBorder, indicatorBegin, indicatorEnd);

        var tickColor = tickPalette.GetElementColor1(GetTickPaletteState(enabled));
        if (!enabled)
        {
            tickColor = KnobColorUtility.GetDisabledColor(tickColor);
        }

        using var tickPen = new Pen(tickColor);

        if (_showSmallScale && _smallChange > 0)
        {
            for (var i = _minimum; i <= _maximum; i += _smallChange)
            {
                g.DrawLine(tickPen, GetMarkerPoint(0, i), GetMarkerPoint(_sizeSmallScaleMarker, i));
            }
        }

        if (_showLargeScale && _largeChange > 0)
        {
            for (var i = _minimum; i <= _maximum; i += _largeChange)
            {
                g.DrawLine(tickPen, GetMarkerPoint(0, i), GetMarkerPoint(_sizeLargeScaleMarker, i));
            }
        }

        if (!enabled)
        {
            KnobColorUtility.DrawDisabledWash(g, _rectKnob);
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the ValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the Scroll event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnScroll(EventArgs e) => Scroll?.Invoke(this, e);

    #endregion

    #region Implementation
    private IPaletteElementColor GetFacePalette(bool enabled)
    {
        if (!enabled)
        {
            return StateDisabled.Face;
        }

        return State switch
        {
            PaletteState.Tracking => StateTracking.Face,
            PaletteState.Pressed => StatePressed.Face,
            _ => StateNormal.Face
        };
    }

    private IPaletteElementColor GetIndicatorPalette(bool enabled)
    {
        if (!enabled)
        {
            return StateDisabled.Indicator;
        }

        return State switch
        {
            PaletteState.Tracking => StateTracking.Indicator,
            PaletteState.Pressed => StatePressed.Indicator,
            _ => StateNormal.Indicator
        };
    }

    private IPaletteElementColor GetTickPalette(bool enabled) => enabled ? StateNormal.Tick : StateDisabled.Tick;

    private static PaletteState GetTickPaletteState(bool enabled) => enabled ? PaletteState.Normal : PaletteState.Disabled;

    private void UpdateKnobGeometry()
    {
        var size = Math.Min(ClientRectangle.Width, ClientRectangle.Height);
        if (_backplateShape == KnobBackplateShape.None)
        {
            var offset = (int)Math.Round(size * 0.1);
            var knobSize = (int)Math.Round(size * 0.8);
            _rectBackplate = Rectangle.Empty;
            _rectKnob = new Rectangle(
                ClientRectangle.X + offset,
                ClientRectangle.Y + offset,
                knobSize,
                knobSize);
        }
        else
        {
            var margin = Math.Max(2, (int)Math.Round(size * 0.04));
            _rectBackplate = new Rectangle(
                ClientRectangle.X + margin,
                ClientRectangle.Y + margin,
                size - margin * 2,
                size - margin * 2);

            var knobSize = (int)Math.Round(_rectBackplate.Width * 0.68);
            var knobOffset = (_rectBackplate.Width - knobSize) / 2;
            _rectKnob = new Rectangle(
                _rectBackplate.X + knobOffset,
                _rectBackplate.Y + knobOffset,
                knobSize,
                knobSize);
        }

        _knobCenter = new Point(
            _rectKnob.X + _rectKnob.Width / 2,
            _rectKnob.Y + _rectKnob.Height / 2);
    }

    private Point GetKnobPosition()
    {
        if (_maximum == _minimum)
        {
            return _knobCenter;
        }

        var degree = 270.0 * _value / (_maximum - _minimum);
        degree = (degree + 135) * Math.PI / 180;

        var radius = _rectKnob.Width / 2.0 - 10.0;
        return new Point(
            (int)Math.Round(Math.Cos(degree) * radius + _knobCenter.X),
            (int)Math.Round(Math.Sin(degree) * radius + _knobCenter.Y));
    }

    private Point GetMarkerPoint(int length, int markerValue)
    {
        if (_maximum == _minimum)
        {
            return _knobCenter;
        }

        var degree = 270.0 * markerValue / (_maximum - _minimum);
        degree = (degree + 135) * Math.PI / 180;

        var radius = _rectKnob.Width / 2.0 - length + 7.0;
        return new Point(
            (int)Math.Round(Math.Cos(degree) * radius + _knobCenter.X),
            (int)Math.Round(Math.Sin(degree) * radius + _knobCenter.Y));
    }

    private static Color GetDarkColor(Color c, byte d)
    {
        var r = c.R > d ? (byte)(c.R - d) : (byte)0;
        var g = c.G > d ? (byte)(c.G - d) : (byte)0;
        var b = c.B > d ? (byte)(c.B - d) : (byte)0;
        return Color.FromArgb(r, g, b);
    }

    private static Color GetLightColor(Color c, byte d)
    {
        var r = c.R + d <= 255 ? (byte)(c.R + d) : (byte)255;
        var g = c.G + d <= 255 ? (byte)(c.G + d) : (byte)255;
        var b = c.B + d <= 255 ? (byte)(c.B + d) : (byte)255;
        return Color.FromArgb(r, g, b);
    }

    private float GetIndicatorAngleDegrees(Point arrow) =>
        (float)(Math.Atan2(arrow.Y - _knobCenter.Y, arrow.X - _knobCenter.X) * 180.0 / Math.PI);

    private int GetRoundedCornerRadius() =>
        Math.Max(4, (int)Math.Round(Math.Min(_rectKnob.Width, _rectKnob.Height) * 0.15));

    private Rectangle GetKnobRingInnerRect()
    {
        var innerSize = (int)Math.Round(_rectKnob.Width * 0.5);
        var offset = (_rectKnob.Width - innerSize) / 2;
        return new Rectangle(_rectKnob.X + offset, _rectKnob.Y + offset, innerSize, innerSize);
    }

    private void DrawKnobFace(Graphics g, RenderContext context, Color faceColor1, Color faceColor2, Color borderColor)
    {
        switch (_knobStyle)
        {
            case KnobStyle.Flat:
                DrawFlatFace(g, faceColor1, borderColor);
                break;
            case KnobStyle.Radial:
                DrawRadialFace(g, faceColor1, faceColor2, borderColor);
                break;
            case KnobStyle.Ring:
                DrawRingFace(g, context, faceColor1, faceColor2, borderColor);
                break;
            case KnobStyle.Bevel:
                DrawBevelFace(g, faceColor1, faceColor2, borderColor);
                break;
            case KnobStyle.RoundedSquare:
                DrawRoundedSquareFace(g, faceColor1, faceColor2, borderColor);
                break;
            case KnobStyle.Industrial:
                DrawIndustrialFace(g, faceColor1, faceColor2, borderColor);
                break;
            default:
                DrawClassicFace(g, faceColor1, faceColor2, borderColor);
                break;
        }
    }

    private void DrawClassicFace(Graphics g, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using var faceBrush = new LinearGradientBrush(_rectKnob, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal);
        g.FillEllipse(faceBrush, _rectKnob);
        using var borderPen = new Pen(borderColor);
        g.DrawEllipse(borderPen, _rectKnob);
    }

    private void DrawFlatFace(Graphics g, Color faceColor, Color borderColor)
    {
        using var faceBrush = new SolidBrush(faceColor);
        g.FillEllipse(faceBrush, _rectKnob);
        using var borderPen = new Pen(borderColor);
        g.DrawEllipse(borderPen, _rectKnob);
    }

    private void DrawRadialFace(Graphics g, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using var path = new GraphicsPath();
        path.AddEllipse(_rectKnob);
        using var brush = new PathGradientBrush(path)
        {
            CenterColor = GetLightColor(faceColor1, 30),
            SurroundColors = new[] { faceColor2 },
            CenterPoint = new PointF(_knobCenter.X, _knobCenter.Y)
        };
        g.FillPath(brush, path);
        using var borderPen = new Pen(borderColor);
        g.DrawEllipse(borderPen, _rectKnob);
    }

    private void DrawRingFace(Graphics g, RenderContext context, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using var faceBrush = new LinearGradientBrush(_rectKnob, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal);
        g.FillEllipse(faceBrush, _rectKnob);
        using var borderPen = new Pen(borderColor);
        g.DrawEllipse(borderPen, _rectKnob);

        var innerRect = GetKnobRingInnerRect();
        var innerColor = GetPalette().GetBackColor1(State);
        using var innerBrush = new SolidBrush(innerColor);
        g.FillEllipse(innerBrush, innerRect);
        using var innerBorderPen = new Pen(GetDarkColor(borderColor, 30));
        g.DrawEllipse(innerBorderPen, innerRect);
    }

    private void DrawBevelFace(Graphics g, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using var faceBrush = new LinearGradientBrush(_rectKnob, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal);
        g.FillEllipse(faceBrush, _rectKnob);

        using var lightPen = new Pen(GetLightColor(borderColor, 55));
        using var darkPen = new Pen(GetDarkColor(borderColor, 55));
        for (var i = 0; i <= 2; i++)
        {
            var arcRect = new Rectangle(_rectKnob.X + i, _rectKnob.Y + i, _rectKnob.Width - i * 2, _rectKnob.Height - i * 2);
            g.DrawArc(lightPen, arcRect, -45, 180);
            g.DrawArc(darkPen, arcRect, 135, 180);
        }

        using var borderPen = new Pen(borderColor);
        g.DrawEllipse(borderPen, _rectKnob);
    }

    private void DrawRoundedSquareFace(Graphics g, Color faceColor1, Color faceColor2, Color borderColor)
    {
        var radius = GetRoundedCornerRadius();
        using var path = CreateRoundedRectPath(_rectKnob, radius);
        var bounds = Rectangle.Round(path.GetBounds());
        if (bounds.Width < 1)
        {
            bounds.Width = 1;
        }

        if (bounds.Height < 1)
        {
            bounds.Height = 1;
        }

        using var faceBrush = new LinearGradientBrush(bounds, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal);
        g.FillPath(faceBrush, path);
        using var borderPen = new Pen(borderColor);
        g.DrawPath(borderPen, path);
    }

    private void DrawIndustrialFace(Graphics g, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using var path = new GraphicsPath();
        path.AddEllipse(_rectKnob);
        using var brush = new PathGradientBrush(path)
        {
            CenterColor = GetLightColor(faceColor1, 45),
            SurroundColors = new[] { faceColor2 == GlobalStaticVariables.EMPTY_COLOR ? GetDarkColor(faceColor1, 55) : faceColor2 },
            CenterPoint = new PointF(_knobCenter.X, _knobCenter.Y)
        };
        g.FillPath(brush, path);

        using var lightPen = new Pen(GetLightColor(borderColor, 40));
        using var darkPen = new Pen(GetDarkColor(borderColor, 50));
        for (var i = 0; i <= 2; i++)
        {
            var arcRect = new Rectangle(_rectKnob.X + i, _rectKnob.Y + i, _rectKnob.Width - i * 2, _rectKnob.Height - i * 2);
            g.DrawArc(lightPen, arcRect, -50, 160);
            g.DrawArc(darkPen, arcRect, 130, 160);
        }

        using var borderPen = new Pen(borderColor);
        g.DrawEllipse(borderPen, _rectKnob);
    }

    private void DrawBackplate(Graphics g, bool enabled)
    {
        if (_backplateShape == KnobBackplateShape.None)
        {
            return;
        }

        var color1 = _backplateColor1 == GlobalStaticVariables.EMPTY_COLOR ? Color.FromArgb(210, 210, 215) : _backplateColor1;
        var color2 = _backplateColor2 == GlobalStaticVariables.EMPTY_COLOR ? Color.FromArgb(150, 150, 158) : _backplateColor2;
        var border = _backplateBorderColor == GlobalStaticVariables.EMPTY_COLOR ? GetDarkColor(color1, 70) : _backplateBorderColor;

        // The backplate colours are custom (not palette driven), so mute them toward grey
        // to give a disabled appearance that matches the disabled palette knob face.
        if (!enabled)
        {
            color1 = KnobColorUtility.GetDisabledColor(color1);
            color2 = KnobColorUtility.GetDisabledColor(color2);
            border = KnobColorUtility.GetDisabledColor(border);
        }

        using var plateBrush = new LinearGradientBrush(_rectBackplate, color1, color2, LinearGradientMode.Vertical);

        switch (_backplateShape)
        {
            case KnobBackplateShape.Square:
                g.FillRectangle(plateBrush, _rectBackplate);
                using (var borderPen = new Pen(border))
                {
                    g.DrawRectangle(borderPen, _rectBackplate);
                }
                break;

            case KnobBackplateShape.RoundedSquare:
                var radius = Math.Max(6, (int)Math.Round(_rectBackplate.Width * 0.12));
                using (var path = CreateRoundedRectPath(_rectBackplate, radius))
                {
                    g.FillPath(plateBrush, path);
                    using var borderPen = new Pen(border);
                    g.DrawPath(borderPen, path);
                }
                break;

            case KnobBackplateShape.Circle:
                g.FillEllipse(plateBrush, _rectBackplate);
                using (var borderPen = new Pen(border))
                {
                    g.DrawEllipse(borderPen, _rectBackplate);
                }
                break;
        }

        if (_showBackplateInsetWell)
        {
            var wellInset = Math.Max(3, (int)Math.Round(_rectKnob.Width * 0.08));
            var wellRect = new Rectangle(
                _rectKnob.X - wellInset,
                _rectKnob.Y - wellInset,
                _rectKnob.Width + wellInset * 2,
                _rectKnob.Height + wellInset * 2);

            using var wellPen = new Pen(GetDarkColor(color2, 60), Math.Max(1f, wellInset * 0.45f));
            if (_backplateShape == KnobBackplateShape.Circle)
            {
                g.DrawEllipse(wellPen, wellRect);
            }
            else if (_backplateShape == KnobBackplateShape.RoundedSquare)
            {
                var wellRadius = Math.Max(8, (int)Math.Round(wellRect.Width * 0.12));
                using var wellPath = CreateRoundedRectPath(wellRect, wellRadius);
                g.DrawPath(wellPen, wellPath);
            }
            else
            {
                g.DrawRectangle(wellPen, wellRect);
            }
        }
    }

    private void DrawKnobDropShadow(Graphics g)
    {
        var shadowOffset = Math.Max(2, (int)Math.Round(_rectKnob.Width * 0.04));
        var shadowRect = new Rectangle(
            _rectKnob.X + shadowOffset,
            _rectKnob.Y + shadowOffset,
            _rectKnob.Width,
            _rectKnob.Height);

        using var shadowBrush = new SolidBrush(Color.FromArgb(70, 0, 0, 0));
        g.FillEllipse(shadowBrush, shadowRect);
    }

    private void DrawFocusRing(Graphics g, Color borderColor)
    {
        using var focusPen = new Pen(GetDarkColor(borderColor, 40))
        {
            DashStyle = DashStyle.Dot,
            DashCap = DashCap.Round
        };

        if (_knobStyle == KnobStyle.RoundedSquare)
        {
            using var path = CreateRoundedRectPath(_rectKnob, GetRoundedCornerRadius());
            g.DrawPath(focusPen, path);
        }
        else
        {
            g.DrawEllipse(focusPen, _rectKnob);
        }
    }

    private static GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
    {
        var path = new GraphicsPath();
        var diameter = radius * 2;
        if (diameter > rect.Width)
        {
            diameter = rect.Width;
        }

        if (diameter > rect.Height)
        {
            diameter = rect.Height;
        }

        var arc = new Rectangle(rect.Location, new Size(diameter, diameter));
        path.AddArc(arc, 180, 90);
        arc.X = rect.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = rect.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = rect.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }

    private void DrawIndicator(Graphics g, Point arrow, Color borderColor, Color beginColor, Color endColor)
    {
        var half = Math.Max(1, _indicatorSize / 2);

        if (_indicatorShape == KnobIndicatorShape.Circle)
        {
            var rect = new Rectangle(arrow.X - half, arrow.Y - half, _indicatorSize, _indicatorSize);
            DrawInsetCircle(g, rect, borderColor, beginColor, endColor);
            return;
        }

        if (_indicatorShape == KnobIndicatorShape.GlowDot)
        {
            var rect = new RectangleF(arrow.X - half, arrow.Y - half, _indicatorSize, _indicatorSize);
            DrawGlowDot(g, rect, beginColor);
            return;
        }

        if (_indicatorShape == KnobIndicatorShape.Bar)
        {
            DrawIndustrialBar(g, arrow, beginColor, borderColor);
            return;
        }

        if (_indicatorShape == KnobIndicatorShape.Dot)
        {
            var rect = new RectangleF(arrow.X - half, arrow.Y - half, _indicatorSize, _indicatorSize);
            DrawFlatDot(g, rect, beginColor, borderColor);
            return;
        }

        var state = g.Save();
        try
        {
            g.TranslateTransform(arrow.X, arrow.Y);
            g.RotateTransform(GetIndicatorAngleDegrees(arrow));

            switch (_indicatorShape)
            {
                case KnobIndicatorShape.Square:
                    DrawInsetPolygon(g, GetSquarePoints(half), borderColor, beginColor, endColor);
                    break;
                case KnobIndicatorShape.Diamond:
                    DrawInsetPolygon(g, GetDiamondPoints(half), borderColor, beginColor, endColor);
                    break;
                case KnobIndicatorShape.Triangle:
                    DrawInsetPolygon(g, GetTrianglePoints(half), borderColor, beginColor, endColor);
                    break;
                case KnobIndicatorShape.Line:
                    DrawInsetLine(g, half, borderColor, beginColor);
                    break;
                case KnobIndicatorShape.Needle:
                    DrawInsetPolygon(g, GetNeedlePoints(half), borderColor, beginColor, endColor);
                    break;
                case KnobIndicatorShape.Chevron:
                    DrawInsetPolygon(g, GetChevronPoints(half), borderColor, beginColor, endColor);
                    break;
                case KnobIndicatorShape.Custom:
                    DrawCustomIndicator(g, half, borderColor, beginColor, endColor);
                    break;
            }
        }
        finally
        {
            g.Restore(state);
        }
    }

    private static PointF[] GetSquarePoints(int half) => new PointF[]
    {
        new PointF(-half, -half),
        new PointF(half, -half),
        new PointF(half, half),
        new PointF(-half, half)
    };

    private static PointF[] GetDiamondPoints(int half) => new PointF[]
    {
        new PointF(0, -half),
        new PointF(half, 0),
        new PointF(0, half),
        new PointF(-half, 0)
    };

    private static PointF[] GetTrianglePoints(int half) => new PointF[]
    {
        new PointF(half, 0),
        new PointF(-half, -half),
        new PointF(-half, half)
    };

    private static PointF[] GetNeedlePoints(int half)
    {
        var width = Math.Max(2, half / 2);
        return new PointF[]
        {
            new PointF(half, 0),
            new PointF(-half, -width),
            new PointF(-half, width)
        };
    }

    private static PointF[] GetChevronPoints(int half) => new PointF[]
    {
        new PointF(half, 0),
        new PointF(-half * 0.35f, -half * 0.65f),
        new PointF(-half * 0.1f, 0),
        new PointF(-half * 0.35f, half * 0.65f)
    };

    private void DrawCustomIndicator(Graphics g, int half, Color borderColor, Color beginColor, Color endColor)
    {
        if (_indicatorCustomPath != null)
        {
            using var path = (GraphicsPath)_indicatorCustomPath.Clone();
            using var matrix = new Matrix();
            matrix.Scale(half, half);
            path.Transform(matrix);
            DrawInsetPath(g, path, borderColor, beginColor, endColor);
            return;
        }

        if (_indicatorCustomPoints != null && _indicatorCustomPoints.Length >= 3)
        {
            DrawInsetPolygon(g, ScaleCustomPoints(_indicatorCustomPoints, half), borderColor, beginColor, endColor);
            return;
        }

        DrawInsetPolygon(g, GetTrianglePoints(half), borderColor, beginColor, endColor);
    }

    private static PointF[] ScaleCustomPoints(PointF[] source, int half)
    {
        var scaled = new PointF[source.Length];
        for (var i = 0; i < source.Length; i++)
        {
            scaled[i] = new PointF(source[i].X * half, source[i].Y * half);
        }

        return scaled;
    }

    private static void DrawFlatDot(Graphics g, RectangleF rect, Color fillColor, Color borderColor)
    {
        using var fillBrush = new SolidBrush(fillColor);
        g.FillEllipse(fillBrush, rect);

        if (borderColor != GlobalStaticVariables.EMPTY_COLOR)
        {
            using var borderPen = new Pen(borderColor);
            g.DrawEllipse(borderPen, rect);
        }
    }

    private static void DrawGlowDot(Graphics g, RectangleF rect, Color fillColor)
    {
        var glowColor = Color.FromArgb(90, fillColor);
        var outer = new RectangleF(rect.X - rect.Width * 0.35f, rect.Y - rect.Height * 0.35f, rect.Width * 1.7f, rect.Height * 1.7f);
        using (var glowBrush = new SolidBrush(glowColor))
        {
            g.FillEllipse(glowBrush, outer);
        }

        using var fillBrush = new SolidBrush(fillColor);
        g.FillEllipse(fillBrush, rect);
        using var highlightBrush = new SolidBrush(Color.FromArgb(160, Color.White));
        var highlight = new RectangleF(rect.X + rect.Width * 0.22f, rect.Y + rect.Height * 0.18f, rect.Width * 0.35f, rect.Height * 0.35f);
        g.FillEllipse(highlightBrush, highlight);
    }

    private void DrawIndustrialBar(Graphics g, Point arrow, Color stripeColor, Color barColor)
    {
        var state = g.Save();
        try
        {
            g.TranslateTransform(_knobCenter.X, _knobCenter.Y);
            g.RotateTransform(GetIndicatorAngleDegrees(arrow));

            var barLength = _rectKnob.Width * 0.42f;
            var barThickness = Math.Max(4f, _rectKnob.Width * 0.1f);
            var barRect = new RectangleF(-barLength, -barThickness / 2f, barLength * 2f, barThickness);

            var baseColor = barColor == GlobalStaticVariables.EMPTY_COLOR ? Color.FromArgb(40, 40, 40) : GetDarkColor(barColor, 30);
            using (var baseBrush = new LinearGradientBrush(barRect, GetLightColor(baseColor, 25), GetDarkColor(baseColor, 25), LinearGradientMode.Vertical))
            {
                g.FillRectangle(baseBrush, barRect);
            }

            var stripeHeight = Math.Max(2f, barThickness * 0.28f);
            var stripeRect = new RectangleF(-barLength * 0.75f, -stripeHeight / 2f, barLength * 1.5f, stripeHeight);
            using (var stripeBrush = new SolidBrush(stripeColor))
            {
                g.FillRectangle(stripeBrush, stripeRect);
            }

            using var borderPen = new Pen(GetDarkColor(baseColor, 40));
            g.DrawRectangle(borderPen, barRect.X, barRect.Y, barRect.Width, barRect.Height);
        }
        finally
        {
            g.Restore(state);
        }
    }

    private static void DrawInsetPath(Graphics g, GraphicsPath path, Color borderColor, Color beginColor, Color endColor)
    {
        var bounds = Rectangle.Round(path.GetBounds());
        if (bounds.Width < 1)
        {
            bounds.Width = 1;
        }

        if (bounds.Height < 1)
        {
            bounds.Height = 1;
        }

        using var fillBrush = new LinearGradientBrush(bounds, beginColor, endColor, LinearGradientMode.ForwardDiagonal);
        g.FillPath(fillBrush, path);

        using var borderPen = new Pen(borderColor);
        g.DrawPath(borderPen, path);
    }

    private static void DrawInsetPolygon(Graphics g, PointF[] points, Color borderColor, Color beginColor, Color endColor)
    {
        using var path = new GraphicsPath();
        path.AddPolygon(points);

        var bounds = Rectangle.Round(path.GetBounds());
        if (bounds.Width < 1)
        {
            bounds.Width = 1;
        }

        if (bounds.Height < 1)
        {
            bounds.Height = 1;
        }

        using var fillBrush = new LinearGradientBrush(bounds, beginColor, endColor, LinearGradientMode.ForwardDiagonal);
        g.FillPath(fillBrush, path);

        using var lightPen = new Pen(GetLightColor(borderColor, 50));
        using var darkPen = new Pen(GetDarkColor(borderColor, 50));
        g.DrawLine(lightPen, points[0], points[1]);
        if (points.Length > 2)
        {
            var darkIndex = points.Length == 3 ? 2 : 3;
            g.DrawLine(darkPen, points[darkIndex - 1], points[darkIndex % points.Length]);
        }

        using var borderPen = new Pen(borderColor);
        g.DrawPath(borderPen, path);
    }

    private static void DrawInsetLine(Graphics g, int half, Color borderColor, Color beginColor)
    {
        var inner = -half * 2;
        using var pen = new Pen(beginColor, Math.Max(1f, half / 2f))
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };
        g.DrawLine(pen, inner, 0, half, 0);
        using var highlightPen = new Pen(GetLightColor(borderColor, 40), 1f);
        g.DrawLine(highlightPen, inner, -1, half, -1);
    }

    private static void DrawInsetCircle(Graphics g, Rectangle r, Color borderColor, Color beginColor, Color endColor)
    {
        using var fillBrush = new LinearGradientBrush(r, beginColor, endColor, LinearGradientMode.ForwardDiagonal);
        g.FillEllipse(fillBrush, r);

        using var lightPen = new Pen(GetLightColor(borderColor, 50));
        using var darkPen = new Pen(GetDarkColor(borderColor, 50));
        for (var i = 0; i <= 1; i++)
        {
            var arcRect = new Rectangle(r.X + i, r.Y + i, r.Width - i * 2, r.Height - i * 2);
            g.DrawArc(lightPen, arcRect, -45, 180);
            g.DrawArc(darkPen, arcRect, 135, 180);
        }
    }
    #endregion
}
