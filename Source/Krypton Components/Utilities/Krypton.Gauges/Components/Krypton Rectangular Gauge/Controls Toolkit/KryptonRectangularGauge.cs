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
/// Displays a compact rectangular analogue indicator with optional segments and interactive input.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRectangularGauge), "ToolboxBitmaps.KryptonRectangularGauge.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Values))]
[DefaultBindingProperty(nameof(Value))]
[DesignerCategory(@"code")]
[Designer("Krypton.Gauges.KryptonRectangularGaugeDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[Description(@"Rectangular analogue indicator with solid or segmented fill and optional interactive input.")]
public partial class KryptonRectangularGauge : KryptonGaugeBase
{
    #region Instance Fields

    private readonly ViewDrawRectangularGauge _drawGauge;
    private readonly RectangularGaugeValues _values;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRectangularGauge"/> class.
    /// </summary>
    public KryptonRectangularGauge()
    {
        InitializeComponent();
        _values = new RectangularGaugeValues(this);
        InitializeDisplayed(_values.Value);
        _drawGauge = new ViewDrawRectangularGauge(this, StateCommon.Back)
        {
            Enabled = Enabled
        };
        ViewManager = new ViewManager(this, _drawGauge);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the rectangular face settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Value, style, ranges, animation, and input mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override GaugeValues Values => _values;

    /// <summary>
    /// Gets the rectangular-specific face settings.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RectangularGaugeValues RectangularValues => _values;

    private bool ShouldSerializeValues() => !_values.IsDefault;

    private void ResetValues() => _values.Reset();

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(160, 56);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawGauge.Enabled = Enabled;
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override bool TryMapPointToValue(Point point, out double value)
    {
        double normalized = GaugeGeometry.PointToLinearNormalized(point, _drawGauge.IndicatorBounds, _values.Orientation);
        value = _values.Minimum + ((_values.Maximum - _values.Minimum) * normalized);
        return _drawGauge.IndicatorBounds.Width > 0 && _drawGauge.IndicatorBounds.Height > 0;
    }

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new RectangularGaugeAccessibleObject(this);

    #endregion

    #region Accessibility

    private sealed class RectangularGaugeAccessibleObject : ControlAccessibleObject
    {
        private readonly KryptonRectangularGauge _owner;

        public RectangularGaugeAccessibleObject(KryptonRectangularGauge owner)
            : base(owner) =>
            _owner = owner;

        public override string? Name =>
            !string.IsNullOrEmpty(_owner.AccessibleName) ? _owner.AccessibleName : base.Name ?? @"Rectangular gauge";

        public override string? Value =>
            _owner.DisplayedValue.ToString("0.##", CultureInfo.CurrentCulture) +
            (string.IsNullOrEmpty(_owner.Values.Units) ? string.Empty : " " + _owner.Values.Units);

        public override AccessibleRole Role =>
            _owner.AccessibleRole != AccessibleRole.Default
                ? _owner.AccessibleRole
                : (_owner.Values.InputMode == GaugeInputMode.Interactive ? AccessibleRole.Slider : AccessibleRole.Graphic);
    }

    #endregion
}
