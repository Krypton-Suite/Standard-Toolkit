#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Toolkit-internal animated spoke spinner. When <see cref="Color"/> is <see cref="Color.Empty"/>,
/// spoke colours follow the active Krypton palette. Consumers should use
/// <c>Krypton.Toolkit.Utilities.KryptonLoadingCircle</c> instead.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[EditorBrowsable(EditorBrowsableState.Never)]
internal class InternalKryptonLoadingCircle : Control
{
    #region Constants

    private const double NumberOfDegreesInCircle = 360;
    private const double NumberOfDegreesInHalfCircle = NumberOfDegreesInCircle / 2;
    private const int DefaultInnerCircleRadius = 8;
    private const int DefaultOuterCircleRadius = 10;
    private const int DefaultNumberOfSpoke = 10;
    private const int DefaultSpokeThickness = 4;

    private const int MacOSXInnerCircleRadius = 5;
    private const int MacOSXOuterCircleRadius = 11;
    private const int MacOSXNumberOfSpoke = 12;
    private const int MacOSXSpokeThickness = 2;

    private const int FireFoxInnerCircleRadius = 6;
    private const int FireFoxOuterCircleRadius = 7;
    private const int FireFoxNumberOfSpoke = 9;
    private const int FireFoxSpokeThickness = 4;

    private const int IE7InnerCircleRadius = 8;
    private const int IE7OuterCircleRadius = 9;
    private const int IE7NumberOfSpoke = 24;
    private const int IE7SpokeThickness = 4;

    #endregion

    #region Instance Fields

    private readonly InternalLoadingCircleValues _values;
    private readonly Timer _timer;
    private PaletteBase? _palette;
    private int _progressValue;
    private PointF _centerPoint;
    private Color[] _colors = Array.Empty<Color>();
    private double[] _angles = Array.Empty<double>();

    #endregion

    #region Public

    /// <summary>
    /// Gets the spinner configuration values.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InternalLoadingCircleValues CircleValues => _values;

    /// <summary>
    /// Gets or sets the lightest spoke colour. <see cref="Color.Empty"/> uses the active palette.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color Color
    {
        get => _values.Color;
        set => _values.Color = value;
    }

    /// <summary>
    /// Gets or sets the outer circle radius.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int OuterCircleRadius
    {
        get => _values.OuterCircleRadius;
        set => _values.OuterCircleRadius = value;
    }

    /// <summary>
    /// Gets or sets the inner circle radius.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int InnerCircleRadius
    {
        get => _values.InnerCircleRadius;
        set => _values.InnerCircleRadius = value;
    }

    /// <summary>
    /// Gets or sets the number of spokes.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int NumberSpoke
    {
        get => _values.NumberSpoke;
        set => _values.NumberSpoke = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the spinner animation is active.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Active
    {
        get => _values.Active;
        set => _values.Active = value;
    }

    /// <summary>
    /// Gets or sets the spoke thickness.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SpokeThickness
    {
        get => _values.SpokeThickness;
        set => _values.SpokeThickness = value;
    }

    /// <summary>
    /// Gets or sets the rotation speed. Higher is slower.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int RotationSpeed
    {
        get => _values.RotationSpeed;
        set => _values.RotationSpeed = value;
    }

    /// <summary>
    /// Gets or sets a geometry style preset.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public InternalLoadingCircleStylePresets StylePreset
    {
        get => _values.StylePreset;
        set => _values.StylePreset = value;
    }

    /// <summary>
    /// Gets or sets the timer interval driving the rotation animation.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal int TimerInterval
    {
        get => _timer.Interval;
        set => _timer.Interval = value;
    }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalKryptonLoadingCircle"/> class.
    /// </summary>
    public InternalKryptonLoadingCircle()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

        _values = new InternalLoadingCircleValues(this);

        _palette = KryptonManager.CurrentGlobalPalette;
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        GenerateColoursPallet();
        GetSpokesAngles();
        GetControlCenterPoint();

        _timer = new Timer();
        _timer.Tick += OnTimerTick;
        ActiveTimer();

        Resize += OnLoadingCircleResize;
    }

    #endregion

    #region Dispose

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Resize -= OnLoadingCircleResize;
            _timer.Tick -= OnTimerTick;
            _timer.Stop();
            _timer.Dispose();

            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
                _palette = null;
            }
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        GenerateColoursPallet();
        Invalidate();
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        if (_values.NumberSpoke > 0 && _colors.Length > 0 && _angles.Length > 0)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int innerRadius = LogicalToDeviceUnits(_values.InnerCircleRadius);
            int outerRadius = LogicalToDeviceUnits(_values.OuterCircleRadius);
            int spokeThickness = LogicalToDeviceUnits(_values.SpokeThickness);

            int position = _progressValue;
            for (int counter = 0; counter < _values.NumberSpoke; counter++)
            {
                position %= _values.NumberSpoke;
                DrawLine(e.Graphics,
                    GetCoordinate(_centerPoint, innerRadius, _angles[position]),
                    GetCoordinate(_centerPoint, outerRadius, _angles[position]),
                    _colors[counter], spokeThickness);
                position++;
            }
        }

        base.OnPaint(e);
    }

    /// <inheritdoc />
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        GetControlCenterPoint();
        Invalidate();
    }

    /// <inheritdoc />
    public override Size GetPreferredSize(Size proposedSize)
    {
        proposedSize.Width =
            (LogicalToDeviceUnits(_values.OuterCircleRadius) + LogicalToDeviceUnits(_values.SpokeThickness)) * 2;

        return proposedSize;
    }

    #endregion

    #region Implementation

    private void OnLoadingCircleResize(object? sender, EventArgs e) => GetControlCenterPoint();

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _progressValue = ++_progressValue % _values.NumberSpoke;
        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.PalettePaint -= OnPalettePaint;
        }

        _palette = KryptonManager.CurrentGlobalPalette;
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        GenerateColoursPallet();
        Invalidate();
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        GenerateColoursPallet();
        Invalidate();
    }

    private Color Darken(Color color, int percent) =>
        Color.FromArgb(percent, Math.Min(color.R, byte.MaxValue), Math.Min(color.G, byte.MaxValue), Math.Min(color.B, byte.MaxValue));

    private Color ResolveSpokeColor()
    {
        if (!_values.Color.IsEmpty)
        {
            return _values.Color;
        }

        PaletteBase? palette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (palette == null)
        {
            return SystemColors.ControlText;
        }

        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        return palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
    }

    /// <summary>
    /// Regenerates the spoke colour array from the resolved base colour.
    /// </summary>
    internal void GenerateColoursPallet() =>
        _colors = GenerateColoursPallet(ResolveSpokeColor(), _values.Active, _values.NumberSpoke);

    private Color[] GenerateColoursPallet(Color color, bool shadeColor, int spokeCount)
    {
        Color[] colors = new Color[NumberSpoke];
        byte increment = (byte)(byte.MaxValue / NumberSpoke);
        byte percentageOfDarken = 0;

        for (int cursor = 0; cursor < NumberSpoke; cursor++)
        {
            if (shadeColor)
            {
                if (cursor == 0 || cursor < NumberSpoke - spokeCount)
                {
                    colors[cursor] = color;
                }
                else
                {
                    percentageOfDarken += increment;
                    if (percentageOfDarken > byte.MaxValue)
                    {
                        percentageOfDarken = byte.MaxValue;
                    }

                    colors[cursor] = Darken(color, percentageOfDarken);
                }
            }
            else
            {
                colors[cursor] = color;
            }
        }

        return colors;
    }

    private void GetControlCenterPoint() => _centerPoint = new PointF(Width / 2f, Height / 2f - 1f);

    private void DrawLine(Graphics graphics, PointF pointOne, PointF pointTwo, Color color, int lineThickness)
    {
        using (var pen = new Pen(new SolidBrush(color), lineThickness))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            graphics.DrawLine(pen, pointOne, pointTwo);
        }
    }

    private PointF GetCoordinate(PointF circleCenter, int radius, double angleDegrees)
    {
        double angle = Math.PI * angleDegrees / NumberOfDegreesInHalfCircle;
        return new PointF(circleCenter.X + radius * (float)Math.Cos(angle),
            circleCenter.Y + radius * (float)Math.Sin(angle));
    }

    /// <summary>
    /// Recalculates spoke angles for the current spoke count.
    /// </summary>
    internal void GetSpokesAngles() => _angles = GetSpokesAngles(_values.NumberSpoke);

    private double[] GetSpokesAngles(int numberSpoke)
    {
        double[] angles = new double[numberSpoke];
        double angleStep = NumberOfDegreesInCircle / numberSpoke;

        for (int counter = 0; counter < numberSpoke; counter++)
        {
            angles[counter] = counter == 0 ? angleStep : angles[counter - 1] + angleStep;
        }

        return angles;
    }

    /// <summary>
    /// Starts or stops the animation timer based on <see cref="Active"/>.
    /// </summary>
    internal void ActiveTimer()
    {
        if (_values.Active)
        {
            _timer.Start();
        }
        else
        {
            _timer.Stop();
            _progressValue = 0;
        }

        GenerateColoursPallet();
        Invalidate();
    }

    /// <summary>
    /// Sets the circle appearance geometry.
    /// </summary>
    public void SetCircleAppearance(int numberSpoke, int spokeThickness, int innerCircleRadius, int outerCircleRadius)
    {
        NumberSpoke = numberSpoke;
        SpokeThickness = spokeThickness;
        InnerCircleRadius = innerCircleRadius;
        OuterCircleRadius = outerCircleRadius;
        Invalidate();
    }

    /// <summary>
    /// Applies a named geometry preset.
    /// </summary>
    internal void ApplyStylePreset(InternalLoadingCircleStylePresets preset)
    {
        switch (preset)
        {
            case InternalLoadingCircleStylePresets.MacOSX:
                SetCircleAppearance(MacOSXNumberOfSpoke, MacOSXSpokeThickness, MacOSXInnerCircleRadius, MacOSXOuterCircleRadius);
                break;
            case InternalLoadingCircleStylePresets.Firefox:
                SetCircleAppearance(FireFoxNumberOfSpoke, FireFoxSpokeThickness, FireFoxInnerCircleRadius, FireFoxOuterCircleRadius);
                break;
            case InternalLoadingCircleStylePresets.IE7:
                SetCircleAppearance(IE7NumberOfSpoke, IE7SpokeThickness, IE7InnerCircleRadius, IE7OuterCircleRadius);
                break;
            case InternalLoadingCircleStylePresets.Custom:
                SetCircleAppearance(DefaultNumberOfSpoke, DefaultSpokeThickness, DefaultInnerCircleRadius, DefaultOuterCircleRadius);
                break;
        }
    }

    #endregion
}
