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
/// Displays a circular analogue gauge with an optional needle, coloured ranges, and interactive input.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCircularGauge), "ToolboxBitmaps.KryptonCircularGauge.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Values))]
[DefaultBindingProperty(nameof(Value))]
[DesignerCategory(@"code")]
[Designer("Krypton.Gauges.KryptonCircularGaugeDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[Description(@"Circular analogue gauge with needle, ranges, and optional interactive input.")]
public partial class KryptonCircularGauge : KryptonGaugeBase
{
    #region Instance Fields

    private readonly ViewDrawCircularGauge _drawGauge;
    private readonly CircularGaugeValues _values;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCircularGauge"/> class.
    /// </summary>
    public KryptonCircularGauge()
    {
        InitializeComponent();
        _values = new CircularGaugeValues(this);
        InitializeDisplayed(_values.Value);
        _drawGauge = new ViewDrawCircularGauge(this, StateCommon.Back)
        {
            Enabled = Enabled
        };
        ViewManager = new ViewManager(this, _drawGauge);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the circular face settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Value, arc, ranges, animation, and input mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override GaugeValues Values => _values;

    /// <summary>
    /// Gets the circular-specific face settings.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CircularGaugeValues CircularValues => _values;

    private bool ShouldSerializeValues() => !_values.IsDefault;

    private void ResetValues() => _values.Reset();

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(180, 180);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawGauge.Enabled = Enabled;
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override bool TryMapPointToValue(Point point, out double value)
    {
        if (!GaugeGeometry.TryPointToNormalized(point, _drawGauge.DialCenter, _values.StartAngle, _values.SweepAngle, out double normalized))
        {
            value = _values.Value;
            return false;
        }

        value = _values.Minimum + ((_values.Maximum - _values.Minimum) * normalized);
        return true;
    }

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new CircularGaugeAccessibleObject(this);

    #endregion

    #region Accessibility

    private sealed class CircularGaugeAccessibleObject : ControlAccessibleObject
    {
        private readonly KryptonCircularGauge _owner;

        public CircularGaugeAccessibleObject(KryptonCircularGauge owner)
            : base(owner) =>
            _owner = owner;

        public override string? Name =>
            !string.IsNullOrEmpty(_owner.AccessibleName) ? _owner.AccessibleName : base.Name ?? @"Circular gauge";

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
