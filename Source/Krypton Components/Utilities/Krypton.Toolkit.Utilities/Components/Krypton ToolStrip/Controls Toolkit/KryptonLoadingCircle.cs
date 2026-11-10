#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Animated spoke spinner for forms and ToolStrip hosts. When <see cref="Color"/> is
/// <see cref="Color.Empty"/>, spoke colours follow the active Krypton palette.
/// </summary>
[ToolboxBitmap(typeof(BackgroundWorker)), ToolboxItem(true)]
[Description("Animated loading spinner that follows Krypton palette colours when Color is Empty.")]
public partial class KryptonLoadingCircle : Control
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

    private readonly LoadingCircleValues _values;
    private readonly Timer _timer;
    private PaletteBase? _palette;
    private int _mProgressValue;
    private PointF _mCenterPoint;
    private Color[] _mColors = Array.Empty<Color>();
    private double[] _mAngles = Array.Empty<double>();

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the expandable spinner configuration values for designer and runtime use.
    /// </summary>
    [Category("LoadingCircle")]
    [Description("Spoke colour, geometry, and rotation settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public LoadingCircleValues CircleValues => _values;

    private bool ShouldSerializeCircleValues() => !_values.IsDefault;

    private void ResetCircleValues() => _values.Reset();

    /// <summary>
    /// Gets or sets the lightest spoke colour. <see cref="Color.Empty"/> uses the active palette.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color Color { get => _values.Color; set => _values.Color = value; }

    /// <summary>
    /// Gets or sets the outer circle radius.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int OuterCircleRadius { get => _values.OuterCircleRadius; set => _values.OuterCircleRadius = value; }

    /// <summary>
    /// Gets or sets the inner circle radius.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int InnerCircleRadius { get => _values.InnerCircleRadius; set => _values.InnerCircleRadius = value; }

    /// <summary>
    /// Gets or sets the number of spokes.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int NumberSpoke { get => _values.NumberSpoke; set => _values.NumberSpoke = value; }

    /// <summary>
    /// Gets or sets a value indicating whether the spinner animation is active.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Active { get => _values.Active; set => _values.Active = value; }

    /// <summary>
    /// Gets or sets the spoke thickness.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SpokeThickness { get => _values.SpokeThickness; set => _values.SpokeThickness = value; }

    /// <summary>
    /// Gets or sets the rotation speed. Higher is slower.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int RotationSpeed { get => _values.RotationSpeed; set => _values.RotationSpeed = value; }

    /// <summary>
    /// Quickly sets the style to one of the presets, or a custom style if desired.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public StylePresets StylePreset { get => _values.StylePreset; set => _values.StylePreset = value; }

    /// <summary>
    /// Gets or sets the timer interval driving the rotation animation.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal int TimerInterval { get => _timer.Interval; set => _timer.Interval = value; }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonLoadingCircle"/> class.
    /// </summary>
    public KryptonLoadingCircle()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

        _values = new LoadingCircleValues(this);

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
        _timer.Tick += aTimer_Tick;
        ActiveTimer();

        Resize += LoadingCircle_Resize;
    }

    #endregion

    #region Dispose

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Resize -= LoadingCircle_Resize;
            _timer.Tick -= aTimer_Tick;
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

    // Events ============================================================
    /// <summary>
    /// Handles the Resize event of the LoadingCircle control.
    /// </summary>
    private void LoadingCircle_Resize(object? sender, EventArgs e) => GetControlCenterPoint();

    /// <summary>
    /// Handles the Tick event of the animation timer.
    /// </summary>
    private void aTimer_Tick(object? sender, EventArgs e)
    {
        _mProgressValue = ++_mProgressValue % _values.NumberSpoke;
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

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        GenerateColoursPallet();
        Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="Control.Paint"/> event.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        if (_values.NumberSpoke > 0 && _mColors.Length > 0 && _mAngles.Length > 0)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Stored radii/thickness are logical (96 DPI); scale for the current monitor.
            int innerRadius = LogicalToDeviceUnits(_values.InnerCircleRadius);
            int outerRadius = LogicalToDeviceUnits(_values.OuterCircleRadius);
            int spokeThickness = LogicalToDeviceUnits(_values.SpokeThickness);

            int intPosition = _mProgressValue;
            for (int intCounter = 0; intCounter < _values.NumberSpoke; intCounter++)
            {
                intPosition = intPosition % _values.NumberSpoke;
                DrawLine(e.Graphics,
                    GetCoordinate(_mCenterPoint, innerRadius, _mAngles[intPosition]),
                    GetCoordinate(_mCenterPoint, outerRadius, _mAngles[intPosition]),
                    _mColors[intCounter], spokeThickness);
                intPosition++;
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

    // Overridden Methods ================================================
    /// <inheritdoc />
    public override Size GetPreferredSize(Size proposedSize)
    {
        proposedSize.Width =
            (LogicalToDeviceUnits(_values.OuterCircleRadius) + LogicalToDeviceUnits(_values.SpokeThickness)) * 2;

        return proposedSize;
    }

    // Methods ===========================================================
    /// <summary>
    /// Darkens a specified color by adjusting the alpha channel.
    /// </summary>
    private Color Darken(Color objColor, int intPercent)
    {
        int intRed = objColor.R;
        int intGreen = objColor.G;
        int intBlue = objColor.B;
        return Color.FromArgb(intPercent, Math.Min(intRed, byte.MaxValue), Math.Min(intGreen, byte.MaxValue), Math.Min(intBlue, byte.MaxValue));
    }

    /// <summary>
    /// Resolves the lightest spoke colour: explicit <see cref="Color"/>, or palette content colour when Empty.
    /// </summary>
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
    /// Generates the spoke colour array from the resolved base colour.
    /// </summary>
    internal void GenerateColoursPallet()
    {
        _mColors = GenerateColoursPallet(ResolveSpokeColor(), _values.Active, _values.NumberSpoke);
    }

    /// <summary>
    /// Generates the colors pallet.
    /// </summary>
    /// <param name="objColor">Color of the lightest spoke.</param>
    /// <param name="blnShadeColor">if set to <c>true</c> the color will be shaded on X spoke.</param>
    /// <param name="intNbSpoke">The number of spokes.</param>
    /// <returns>An array of color used to draw the circle.</returns>
    private Color[] GenerateColoursPallet(Color objColor, bool blnShadeColor, int intNbSpoke)
    {
        Color[] objColors = new Color[NumberSpoke];

        // Value is used to simulate a gradient feel... For each spoke, the
        // color will be darken by value in intIncrement.
        byte bytIncrement = (byte)(byte.MaxValue / NumberSpoke);

        //Reset variable in case of multiple passes
        byte PERCENTAGE_OF_DARKEN = 0;

        for (int intCursor = 0; intCursor < NumberSpoke; intCursor++)
        {
            if (blnShadeColor)
            {
                if (intCursor == 0 || intCursor < NumberSpoke - intNbSpoke)
                {
                    objColors[intCursor] = objColor;
                }
                else
                {
                    // Increment alpha channel color
                    PERCENTAGE_OF_DARKEN += bytIncrement;

                    // Ensure that we don't exceed the maximum alpha
                    // channel value (255)
                    if (PERCENTAGE_OF_DARKEN > byte.MaxValue)
                    {
                        PERCENTAGE_OF_DARKEN = byte.MaxValue;
                    }

                    // Determine the spoke forecolor
                    objColors[intCursor] = Darken(objColor, PERCENTAGE_OF_DARKEN);
                }
            }
            else
            {
                objColors[intCursor] = objColor;
            }
        }

        return objColors;
    }

    /// <summary>
    /// Gets the control center point.
    /// </summary>
    private void GetControlCenterPoint()
    {
        _mCenterPoint = GetControlCenterPoint(this);
    }

    /// <summary>
    /// Gets the control center point.
    /// </summary>
    private PointF GetControlCenterPoint(Control objControl) => new(objControl.Width / 2, objControl.Height / 2 - 1);

    /// <summary>
    /// Draws the line with GDI+.
    /// </summary>
    private void DrawLine(Graphics objGraphics, PointF objPointOne, PointF objPointTwo,
        Color objColor, int intLineThickness)
    {
        using (Pen objPen = new Pen(new SolidBrush(objColor), intLineThickness))
        {
            objPen.StartCap = LineCap.Round;
            objPen.EndCap = LineCap.Round;
            objGraphics.DrawLine(objPen, objPointOne, objPointTwo);
        }
    }

    /// <summary>
    /// Gets the coordinate.
    /// </summary>
    private PointF GetCoordinate(PointF objCircleCenter, int intRadius, double dblAngle)
    {
        double angle = Math.PI * dblAngle / NumberOfDegreesInHalfCircle;

        return new PointF(objCircleCenter.X + intRadius * (float)Math.Cos(angle),
            objCircleCenter.Y + intRadius * (float)Math.Sin(angle));
    }

    /// <summary>
    /// Gets the spokes angles.
    /// </summary>
    internal void GetSpokesAngles()
    {
        _mAngles = GetSpokesAngles(_values.NumberSpoke);
    }

    /// <summary>
    /// Gets the spoke angles.
    /// </summary>
    private double[] GetSpokesAngles(int intNumberSpoke)
    {
        double[] angles = new double[intNumberSpoke];
        double dblAngle = NumberOfDegreesInCircle / intNumberSpoke;

        for (int shtCounter = 0; shtCounter < intNumberSpoke; shtCounter++)
            angles[shtCounter] = shtCounter == 0 ? dblAngle : angles[shtCounter - 1] + dblAngle;

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
            _mProgressValue = 0;
        }

        GenerateColoursPallet();
        Invalidate();
    }

    /// <summary>
    /// Sets the circle appearance.
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
    /// Applies a named geometry preset by calling <see cref="SetCircleAppearance"/> with its fixed values.
    /// </summary>
    internal void ApplyStylePreset(StylePresets preset)
    {
        switch (preset)
        {
            case StylePresets.MacOSX:
                SetCircleAppearance(MacOSXNumberOfSpoke,
                    MacOSXSpokeThickness, MacOSXInnerCircleRadius,
                    MacOSXOuterCircleRadius);
                break;
            case StylePresets.Firefox:
                SetCircleAppearance(FireFoxNumberOfSpoke,
                    FireFoxSpokeThickness, FireFoxInnerCircleRadius,
                    FireFoxOuterCircleRadius);
                break;
            case StylePresets.IE7:
                SetCircleAppearance(IE7NumberOfSpoke,
                    IE7SpokeThickness, IE7InnerCircleRadius,
                    IE7OuterCircleRadius);
                break;
            case StylePresets.Custom:
                SetCircleAppearance(DefaultNumberOfSpoke,
                    DefaultSpokeThickness,
                    DefaultInnerCircleRadius,
                    DefaultOuterCircleRadius);
                break;
        }
    }
}
