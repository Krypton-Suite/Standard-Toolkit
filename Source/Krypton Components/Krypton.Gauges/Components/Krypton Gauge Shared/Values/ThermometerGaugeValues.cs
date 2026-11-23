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
/// Face settings for <see cref="KryptonThermometerGauge"/>.
/// </summary>
public class ThermometerGaugeValues : GaugeValues
{
    #region Instance Fields

    private const int DefaultMajorTickCount = 11;
    private const int DefaultMinorTickCount = 4;
    private const float DefaultBulbScale = 1f;

    private Orientation _orientation = Orientation.Vertical;
    private int _majorTickCount = DefaultMajorTickCount;
    private int _minorTickCount = DefaultMinorTickCount;
    private bool _showBulb = true;
    private float _bulbScale = DefaultBulbScale;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ThermometerGaugeValues"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    public ThermometerGaugeValues(KryptonGaugeBase owner)
        : base(owner)
    {
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    protected override bool IsSpecializedDefault =>
        _orientation == Orientation.Vertical &&
        _majorTickCount == DefaultMajorTickCount &&
        _minorTickCount == DefaultMinorTickCount &&
        _showBulb &&
        Math.Abs(_bulbScale - DefaultBulbScale) < 0.001f;

    /// <inheritdoc />
    protected override void ResetSpecialized()
    {
        Orientation = Orientation.Vertical;
        MajorTickCount = DefaultMajorTickCount;
        MinorTickCount = DefaultMinorTickCount;
        ShowBulb = true;
        BulbScale = DefaultBulbScale;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the tube runs vertically or horizontally.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Vertical (bulb at the cold end / bottom) or horizontal.")]
    [DefaultValue(Orientation.Vertical)]
    public Orientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation == value)
            {
                return;
            }

            _orientation = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeOrientation() => _orientation != Orientation.Vertical;

    private void ResetOrientation() => Orientation = Orientation.Vertical;

    /// <summary>
    /// Gets or sets how many major ticks (including ends) are drawn on the tube.
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
    /// Gets or sets whether the bulb is drawn at the cold end of the tube.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw the bulb at the cold end of the tube.")]
    [DefaultValue(true)]
    public bool ShowBulb
    {
        get => _showBulb;
        set
        {
            if (_showBulb == value)
            {
                return;
            }

            _showBulb = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeShowBulb() => !_showBulb;

    private void ResetShowBulb() => ShowBulb = true;

    /// <summary>
    /// Gets or sets the bulb diameter relative to the tube thickness. Values are clamped to 0.5..2.0.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Bulb size relative to the tube thickness.")]
    [DefaultValue(DefaultBulbScale)]
    public float BulbScale
    {
        get => _bulbScale;
        set
        {
            value = Math.Max(0.5f, Math.Min(2f, value));
            if (Math.Abs(_bulbScale - value) < 0.001f)
            {
                return;
            }

            _bulbScale = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeBulbScale() => Math.Abs(_bulbScale - DefaultBulbScale) >= 0.001f;

    private void ResetBulbScale() => BulbScale = DefaultBulbScale;

    #endregion
}
