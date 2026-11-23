#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Face settings for <see cref="KryptonAmpereMeterGauge"/>.
/// Default scale is AC (<c>0…10</c> A). Set <see cref="GaugeValues.Minimum"/> negative for a DC center-zero scale.
/// </summary>
public class AmpereMeterGaugeValues : GaugeValues
{
    #region Instance Fields

    private const float DefaultStartAngle = 200f;
    private const float DefaultSweepAngle = 140f;
    private const int DefaultMajorTickCount = 11;
    private const int DefaultMinorTickCount = 4;
    private const double FactoryMaximum = 10d;
    private const string FactoryUnits = "A";

    private float _startAngle = DefaultStartAngle;
    private float _sweepAngle = DefaultSweepAngle;
    private int _majorTickCount = DefaultMajorTickCount;
    private int _minorTickCount = DefaultMinorTickCount;
    private bool _showCenterZeroMark = true;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="AmpereMeterGaugeValues"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    public AmpereMeterGaugeValues(KryptonGaugeBase owner)
        : base(owner)
    {
        Maximum = FactoryMaximum;
        Units = FactoryUnits;
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault =>
        InputMode == GaugeInputMode.Manual &&
        Minimum == 0d &&
        Maximum == FactoryMaximum &&
        Value == 0d &&
        Animate &&
        AnimationMilliseconds == 200 &&
        ShowScaleLabels &&
        string.IsNullOrEmpty(Caption) &&
        Units == FactoryUnits &&
        SmallChange == 1d &&
        LargeChange == 10d &&
        Ranges.IsDefault &&
        IsSpecializedDefault;

    /// <inheritdoc />
    protected override bool IsSpecializedDefault =>
        Math.Abs(_startAngle - DefaultStartAngle) < 0.001f &&
        Math.Abs(_sweepAngle - DefaultSweepAngle) < 0.001f &&
        _majorTickCount == DefaultMajorTickCount &&
        _minorTickCount == DefaultMinorTickCount &&
        _showCenterZeroMark;

    /// <inheritdoc />
    public override void Reset()
    {
        base.Reset();
        Maximum = FactoryMaximum;
        Units = FactoryUnits;
    }

    /// <inheritdoc />
    protected override void ResetSpecialized()
    {
        StartAngle = DefaultStartAngle;
        SweepAngle = DefaultSweepAngle;
        MajorTickCount = DefaultMajorTickCount;
        MinorTickCount = DefaultMinorTickCount;
        ShowCenterZeroMark = true;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the start angle of the scale arc in degrees. Zero is to the right; angles increase clockwise.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Start angle of the scale arc in degrees. Zero is right; increases clockwise.")]
    [DefaultValue(DefaultStartAngle)]
    public float StartAngle
    {
        get => _startAngle;
        set
        {
            if (Math.Abs(_startAngle - value) < 0.001f)
            {
                return;
            }

            _startAngle = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeStartAngle() => Math.Abs(_startAngle - DefaultStartAngle) >= 0.001f;

    private void ResetStartAngle() => StartAngle = DefaultStartAngle;

    /// <summary>
    /// Gets or sets the sweep of the scale arc in degrees. Positive values sweep clockwise.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Sweep of the scale arc in degrees. Positive values sweep clockwise.")]
    [DefaultValue(DefaultSweepAngle)]
    public float SweepAngle
    {
        get => _sweepAngle;
        set
        {
            if (Math.Abs(value) < 1f)
            {
                value = value < 0f ? -1f : 1f;
            }

            if (Math.Abs(_sweepAngle - value) < 0.001f)
            {
                return;
            }

            _sweepAngle = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeSweepAngle() => Math.Abs(_sweepAngle - DefaultSweepAngle) >= 0.001f;

    private void ResetSweepAngle() => SweepAngle = DefaultSweepAngle;

    /// <summary>
    /// Gets or sets how many major ticks (including ends) are drawn on the arc.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Number of major ticks including the ends.")]
    [DefaultValue(DefaultMajorTickCount)]
    public int MajorTickCount
    {
        get => _majorTickCount;
        set
        {
            value = Math.Max(2, value);
            if (_majorTickCount == value)
            {
                return;
            }

            _majorTickCount = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeMajorTickCount() => _majorTickCount != DefaultMajorTickCount;

    private void ResetMajorTickCount() => MajorTickCount = DefaultMajorTickCount;

    /// <summary>
    /// Gets or sets how many minor ticks are drawn between each pair of major ticks.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Minor ticks between each pair of major ticks.")]
    [DefaultValue(DefaultMinorTickCount)]
    public int MinorTickCount
    {
        get => _minorTickCount;
        set
        {
            value = Math.Max(0, value);
            if (_minorTickCount == value)
            {
                return;
            }

            _minorTickCount = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeMinorTickCount() => _minorTickCount != DefaultMinorTickCount;

    private void ResetMinorTickCount() => MinorTickCount = DefaultMinorTickCount;

    /// <summary>
    /// Gets or sets whether the zero tick is emphasised when the scale is bipolar (<see cref="GaugeValues.Minimum"/> is negative).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Emphasise the center-zero tick when Minimum is negative.")]
    [DefaultValue(true)]
    public bool ShowCenterZeroMark
    {
        get => _showCenterZeroMark;
        set
        {
            if (_showCenterZeroMark == value)
            {
                return;
            }

            _showCenterZeroMark = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeShowCenterZeroMark() => !_showCenterZeroMark;

    private void ResetShowCenterZeroMark() => ShowCenterZeroMark = true;

    #endregion
}
