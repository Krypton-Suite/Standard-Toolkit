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

[ToolboxBitmap(typeof(BackgroundWorker)), ToolboxItem(false)]
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
    private int _mProgressValue;
    private PointF _mCenterPoint;
    private Color[] _mColors;
    private double[] _mAngles;

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
    /// Gets or sets the lightest color of the circle.
    /// </summary>
    /// <value>The lightest color of the circle.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color Color { get => _values.Color; set => _values.Color = value; }

    /// <summary>
    /// Gets or sets the outer circle radius.
    /// </summary>
    /// <value>The outer circle radius.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int OuterCircleRadius { get => _values.OuterCircleRadius; set => _values.OuterCircleRadius = value; }

    /// <summary>
    /// Gets or sets the inner circle radius.
    /// </summary>
    /// <value>The inner circle radius.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int InnerCircleRadius { get => _values.InnerCircleRadius; set => _values.InnerCircleRadius = value; }

    /// <summary>
    /// Gets or sets the number of spoke.
    /// </summary>
    /// <value>The number of spoke.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int NumberSpoke { get => _values.NumberSpoke; set => _values.NumberSpoke = value; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:LoadingCircle"/> is active.
    /// </summary>
    /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Active { get => _values.Active; set => _values.Active = value; }

    /// <summary>
    /// Gets or sets the spoke thickness.
    /// </summary>
    /// <value>The spoke thickness.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SpokeThickness { get => _values.SpokeThickness; set => _values.SpokeThickness = value; }

    /// <summary>
    /// Gets or sets the rotation speed.
    /// </summary>
    /// <value>The rotation speed.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int RotationSpeed { get => _values.RotationSpeed; set => _values.RotationSpeed = value; }

    /// <summary>
    /// Quickly sets the style to one of these presets, or a custom style if desired
    /// </summary>
    /// <value>The style preset.</value>
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
    /// Initializes a new instance of the <see cref="T:LoadingCircle"/> class.
    /// </summary>
    public KryptonLoadingCircle()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

        _values = new LoadingCircleValues(this);

        GenerateColoursPallet();
        GetSpokesAngles();
        GetControlCenterPoint();

        _timer = new Timer();
        _timer.Tick += aTimer_Tick;
        ActiveTimer();

        Resize += LoadingCircle_Resize;
    }

    #endregion

    // Events ============================================================
    /// <summary>
    /// Handles the Resize event of the LoadingCircle control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    private void LoadingCircle_Resize(object? sender, EventArgs e)
    {
        GetControlCenterPoint();
    }

    /// <summary>
    /// Handles the Tick event of the aTimer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    private void aTimer_Tick(object? sender, EventArgs e)
    {
        _mProgressValue = ++_mProgressValue % _values.NumberSpoke;
        Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        if (_values.NumberSpoke > 0)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int intPosition = _mProgressValue;
            for (int intCounter = 0; intCounter < _values.NumberSpoke; intCounter++)
            {
                intPosition = intPosition % _values.NumberSpoke;
                DrawLine(e.Graphics,
                    GetCoordinate(_mCenterPoint, _values.InnerCircleRadius, _mAngles[intPosition]),
                    GetCoordinate(_mCenterPoint, _values.OuterCircleRadius, _mAngles[intPosition]),
                    _mColors[intCounter], _values.SpokeThickness);
                intPosition++;
            }
        }

        base.OnPaint(e);
    }

    // Overridden Methods ================================================
    /// <summary>
    /// Retrieves the size of a rectangular area into which a control can be fitted.
    /// </summary>
    /// <param name="proposedSize">The custom-sized area for a control.</param>
    /// <returns>
    /// An ordered pair of type <see cref="T:System.Drawing.Size"></see> representing the width and height of a rectangle.
    /// </returns>
    public override Size GetPreferredSize(Size proposedSize)
    {
        proposedSize.Width =
            (_values.OuterCircleRadius + _values.SpokeThickness) * 2;

        return proposedSize;
    }

    // Methods ===========================================================
    /// <summary>
    /// Darkens a specified color.
    /// </summary>
    /// <param name="objColor">Color to darken.</param>
    /// <param name="intPercent">The percent of darken.</param>
    /// <returns>The new color generated.</returns>
    private Color Darken(Color objColor, int intPercent)
    {
        int intRed = objColor.R;
        int intGreen = objColor.G;
        int intBlue = objColor.B;
        return Color.FromArgb(intPercent, Math.Min(intRed, byte.MaxValue), Math.Min(intGreen, byte.MaxValue), Math.Min(intBlue, byte.MaxValue));
    }

    /// <summary>
    /// Generates the colors pallet.
    /// </summary>
    internal void GenerateColoursPallet()
    {
        _mColors = GenerateColoursPallet(_values.Color, _values.Active, _values.NumberSpoke);
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
    /// <returns>PointF object</returns>
    private PointF GetControlCenterPoint(Control objControl) => new(objControl.Width / 2, objControl.Height / 2 - 1);

    /// <summary>
    /// Draws the line with GDI+.
    /// </summary>
    /// <param name="objGraphics">The Graphics object.</param>
    /// <param name="objPointOne">The point one.</param>
    /// <param name="objPointTwo">The point two.</param>
    /// <param name="objColor">Color of the spoke.</param>
    /// <param name="intLineThickness">The thickness of spoke.</param>
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
    /// <param name="objCircleCenter">The Circle center.</param>
    /// <param name="intRadius">The radius.</param>
    /// <param name="dblAngle">The angle.</param>
    /// <returns></returns>
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
    /// <param name="intNumberSpoke">The number spoke.</param>
    /// <returns>An array of angle.</returns>
    private double[] GetSpokesAngles(int intNumberSpoke)
    {
        double[] angles = new double[intNumberSpoke];
        double dblAngle = NumberOfDegreesInCircle / intNumberSpoke;

        for (int shtCounter = 0; shtCounter < intNumberSpoke; shtCounter++)
            angles[shtCounter] = shtCounter == 0 ? dblAngle : angles[shtCounter - 1] + dblAngle;

        return angles;
    }

    /// <summary>
    /// Actives the timer.
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
    /// <param name="numberSpoke">The number spoke.</param>
    /// <param name="spokeThickness">The spoke thickness.</param>
    /// <param name="innerCircleRadius">The inner circle radius.</param>
    /// <param name="outerCircleRadius">The outer circle radius.</param>
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
    /// <param name="preset">The preset to apply.</param>
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