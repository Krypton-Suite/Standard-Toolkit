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
/// Face settings for <see cref="KryptonCircularGauge"/>.
/// </summary>
public class CircularGaugeValues : GaugeValues
{
    #region Instance Fields

    private const float DefaultStartAngle = 135f;
    private const float DefaultSweepAngle = 270f;
    private const int DefaultMajorTickCount = 11;
    private const int DefaultMinorTickCount = 4;

    private float _startAngle = DefaultStartAngle;
    private float _sweepAngle = DefaultSweepAngle;
    private bool _showNeedle = true;
    private bool _showPeak;
    private int _majorTickCount = DefaultMajorTickCount;
    private int _minorTickCount = DefaultMinorTickCount;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="CircularGaugeValues"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    public CircularGaugeValues(KryptonGaugeBase owner)
        : base(owner)
    {
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    protected override bool IsSpecializedDefault =>
        _startAngle == DefaultStartAngle &&
        _sweepAngle == DefaultSweepAngle &&
        _showNeedle &&
        !_showPeak &&
        _majorTickCount == DefaultMajorTickCount &&
        _minorTickCount == DefaultMinorTickCount;

    /// <inheritdoc />
    protected override void ResetSpecialized()
    {
        StartAngle = DefaultStartAngle;
        SweepAngle = DefaultSweepAngle;
        ShowNeedle = true;
        ShowPeak = false;
        MajorTickCount = DefaultMajorTickCount;
        MinorTickCount = DefaultMinorTickCount;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the start angle of the arc in degrees. Zero is to the right; angles increase clockwise.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Start angle of the arc in degrees. Zero is right; increases clockwise.")]
    [DefaultValue(DefaultStartAngle)]
    public float StartAngle
    {
        get => _startAngle;
        set
        {
            if (_startAngle == value)
            {
                return;
            }

            _startAngle = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeStartAngle() => _startAngle != DefaultStartAngle;

    private void ResetStartAngle() => StartAngle = DefaultStartAngle;

    /// <summary>
    /// Gets or sets the sweep of the arc in degrees. Positive values sweep clockwise.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Sweep of the arc in degrees. Positive values sweep clockwise.")]
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

            if (_sweepAngle == value)
            {
                return;
            }

            _sweepAngle = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeSweepAngle() => _sweepAngle != DefaultSweepAngle;

    private void ResetSweepAngle() => SweepAngle = DefaultSweepAngle;

    /// <summary>
    /// Gets or sets whether the needle is drawn.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw the needle.")]
    [DefaultValue(true)]
    public bool ShowNeedle
    {
        get => _showNeedle;
        set
        {
            if (_showNeedle == value)
            {
                return;
            }

            _showNeedle = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeShowNeedle() => !_showNeedle;

    private void ResetShowNeedle() => ShowNeedle = true;

    /// <summary>
    /// Gets or sets whether a peak marker is drawn at the recent high water mark.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw a peak marker at the recent high water mark.")]
    [DefaultValue(false)]
    public bool ShowPeak
    {
        get => _showPeak;
        set
        {
            if (_showPeak == value)
            {
                return;
            }

            _showPeak = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeShowPeak() => _showPeak;

    private void ResetShowPeak() => ShowPeak = false;

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

    #endregion
}
