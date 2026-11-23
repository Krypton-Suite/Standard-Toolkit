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
/// Face settings for <see cref="KryptonRectangularGauge"/>.
/// </summary>
public class RectangularGaugeValues : GaugeValues
{
    #region Instance Fields

    private const int DefaultSegmentCount = 10;

    private RectangularGaugeStyle _style = RectangularGaugeStyle.Solid;
    private Orientation _orientation = Orientation.Horizontal;
    private int _segmentCount = DefaultSegmentCount;
    private bool _showValueText = true;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="RectangularGaugeValues"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    public RectangularGaugeValues(KryptonGaugeBase owner)
        : base(owner)
    {
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    protected override bool IsSpecializedDefault =>
        _style == RectangularGaugeStyle.Solid &&
        _orientation == Orientation.Horizontal &&
        _segmentCount == DefaultSegmentCount &&
        _showValueText;

    /// <inheritdoc />
    protected override void ResetSpecialized()
    {
        Style = RectangularGaugeStyle.Solid;
        Orientation = Orientation.Horizontal;
        SegmentCount = DefaultSegmentCount;
        ShowValueText = true;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the painted face.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Solid fill or segmented blocks.")]
    [DefaultValue(RectangularGaugeStyle.Solid)]
    public RectangularGaugeStyle Style
    {
        get => _style;
        set
        {
            if (_style == value)
            {
                return;
            }

            _style = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeStyle() => _style != RectangularGaugeStyle.Solid;

    private void ResetStyle() => Style = RectangularGaugeStyle.Solid;

    /// <summary>
    /// Gets or sets whether the fill grows horizontally or vertically.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Horizontal or vertical fill direction.")]
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
    /// Gets or sets how many segments are drawn when <see cref="Style"/> is <see cref="RectangularGaugeStyle.Segmented"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Segment count when Style is Segmented.")]
    [DefaultValue(DefaultSegmentCount)]
    public int SegmentCount
    {
        get => _segmentCount;
        set
        {
            value = Math.Max(1, value);
            if (_segmentCount == value)
            {
                return;
            }

            _segmentCount = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeSegmentCount() => _segmentCount != DefaultSegmentCount;

    private void ResetSegmentCount() => SegmentCount = DefaultSegmentCount;

    /// <summary>
    /// Gets or sets whether the numeric value is drawn inside the indicator.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw the numeric value inside the indicator.")]
    [DefaultValue(true)]
    public bool ShowValueText
    {
        get => _showValueText;
        set
        {
            if (_showValueText == value)
            {
                return;
            }

            _showValueText = value;
            Owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeShowValueText() => !_showValueText;

    private void ResetShowValueText() => ShowValueText = true;

    #endregion
}
