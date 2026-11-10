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
/// Face settings for <see cref="KryptonLinearGauge"/>.
/// </summary>
public class LinearGaugeValues : GaugeValues
{
    #region Instance Fields

    private const int DefaultMajorTickCount = 11;
    private const int DefaultMinorTickCount = 4;

    private Orientation _orientation = Orientation.Horizontal;
    private int _majorTickCount = DefaultMajorTickCount;
    private int _minorTickCount = DefaultMinorTickCount;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="LinearGaugeValues"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    public LinearGaugeValues(KryptonGaugeBase owner)
        : base(owner)
    {
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    protected override bool IsSpecializedDefault =>
        _orientation == Orientation.Horizontal &&
        _majorTickCount == DefaultMajorTickCount &&
        _minorTickCount == DefaultMinorTickCount;

    /// <inheritdoc />
    protected override void ResetSpecialized()
    {
        Orientation = Orientation.Horizontal;
        MajorTickCount = DefaultMajorTickCount;
        MinorTickCount = DefaultMinorTickCount;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the track runs horizontally or vertically.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Horizontal or vertical track.")]
    [DefaultValue(Orientation.Horizontal)]
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

    private bool ShouldSerializeOrientation() => _orientation != Orientation.Horizontal;

    private void ResetOrientation() => Orientation = Orientation.Horizontal;

    /// <summary>
    /// Gets or sets how many major ticks (including ends) are drawn on the track.
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
