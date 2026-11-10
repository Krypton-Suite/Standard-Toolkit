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
/// Displays a square panel-style ampere meter with a short upper scale arc, bottom pivot needle,
/// coloured ranges, and optional interactive input. Defaults to AC (<c>0…10</c> A); set
/// <see cref="GaugeValues.Minimum"/> negative for a DC center-zero scale.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonAmpereMeterGauge), "ToolboxBitmaps.KryptonAmpereMeterGauge.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Values))]
[DefaultBindingProperty(nameof(Value))]
[DesignerCategory(@"code")]
[Designer("Krypton.Gauges.KryptonAmpereMeterGaugeDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[Description(@"Square panel ampere meter with short arc, ranges, and optional interactive input.")]
public partial class KryptonAmpereMeterGauge : KryptonGaugeBase
{
    #region Instance Fields

    private readonly ViewDrawAmpereMeterGauge _drawGauge;
    private readonly AmpereMeterGaugeValues _values;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonAmpereMeterGauge"/> class.
    /// </summary>
    public KryptonAmpereMeterGauge()
    {
        InitializeComponent();
        _values = new AmpereMeterGaugeValues(this);
        InitializeDisplayed(_values.Value);
        _drawGauge = new ViewDrawAmpereMeterGauge(this, StateCommon.Back)
        {
            Enabled = Enabled
        };
        ViewManager = new ViewManager(this, _drawGauge);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the ampere-meter face settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Value, arc, ranges, animation, and input mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override GaugeValues Values => _values;

    /// <summary>
    /// Gets the ampere-meter-specific face settings.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AmpereMeterGaugeValues AmpereMeterValues => _values;

    private bool ShouldSerializeValues() => !_values.IsDefault;

    private void ResetValues() => _values.Reset();

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(160, 160);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawGauge.Enabled = Enabled;
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override bool TryMapPointToValue(Point point, out double value)
    {
        if (!GaugeGeometry.TryPointToNormalized(point, _drawGauge.Pivot, _values.StartAngle, _values.SweepAngle, out double normalized))
        {
            value = _values.Value;
            return false;
        }

        value = _values.Minimum + ((_values.Maximum - _values.Minimum) * normalized);
        return true;
    }

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new AmpereMeterGaugeAccessibleObject(this);

    #endregion

    #region Accessibility

    private sealed class AmpereMeterGaugeAccessibleObject : ControlAccessibleObject
    {
        private readonly KryptonAmpereMeterGauge _owner;

        public AmpereMeterGaugeAccessibleObject(KryptonAmpereMeterGauge owner)
            : base(owner) =>
            _owner = owner;

        public override string? Name =>
            !string.IsNullOrEmpty(_owner.AccessibleName) ? _owner.AccessibleName : base.Name ?? @"Ampere meter";

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
