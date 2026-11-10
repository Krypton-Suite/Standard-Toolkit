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
/// Displays a classic bulb-and-tube thermometer with coloured ranges and optional interactive input.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonThermometerGauge), "ToolboxBitmaps.KryptonThermometerGauge.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Values))]
[DefaultBindingProperty(nameof(Value))]
[DesignerCategory(@"code")]
[Designer("Krypton.Gauges.KryptonThermometerGaugeDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[Description(@"Thermometer gauge with bulb, tube, ranges, and optional interactive input.")]
public partial class KryptonThermometerGauge : KryptonGaugeBase
{
    #region Instance Fields

    private readonly ViewDrawThermometerGauge _drawGauge;
    private readonly ThermometerGaugeValues _values;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonThermometerGauge"/> class.
    /// </summary>
    public KryptonThermometerGauge()
    {
        InitializeComponent();
        _values = new ThermometerGaugeValues(this);
        InitializeDisplayed(_values.Value);
        _drawGauge = new ViewDrawThermometerGauge(this, StateCommon.Back)
        {
            Enabled = Enabled
        };
        ViewManager = new ViewManager(this, _drawGauge);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the thermometer face settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Value, orientation, ranges, animation, and input mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override GaugeValues Values => _values;

    /// <summary>
    /// Gets the thermometer-specific face settings.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ThermometerGaugeValues ThermometerValues => _values;

    private bool ShouldSerializeValues() => !_values.IsDefault;

    private void ResetValues() => _values.Reset();

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(64, 220);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawGauge.Enabled = Enabled;
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override bool TryMapPointToValue(Point point, out double value)
    {
        double normalized = GaugeGeometry.PointToLinearNormalized(point, _drawGauge.TubeBounds, _values.Orientation);
        value = _values.Minimum + ((_values.Maximum - _values.Minimum) * normalized);
        return _drawGauge.TubeBounds.Width > 0 && _drawGauge.TubeBounds.Height > 0;
    }

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new ThermometerGaugeAccessibleObject(this);

    #endregion

    #region Accessibility

    private sealed class ThermometerGaugeAccessibleObject : ControlAccessibleObject
    {
        private readonly KryptonThermometerGauge _owner;

        public ThermometerGaugeAccessibleObject(KryptonThermometerGauge owner)
            : base(owner) =>
            _owner = owner;

        public override string? Name =>
            !string.IsNullOrEmpty(_owner.AccessibleName) ? _owner.AccessibleName : base.Name ?? @"Thermometer gauge";

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
