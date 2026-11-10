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
/// Smart-tag action list for <see cref="KryptonThermometerGauge"/>.
/// </summary>
internal class KryptonThermometerGaugeActionList : DesignerActionList
{
    private readonly KryptonThermometerGauge _control;
    private readonly IComponentChangeService? _service;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonThermometerGaugeActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonThermometerGaugeActionList(KryptonThermometerGaugeDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonThermometerGauge)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    /// <summary>Gets or sets the target value.</summary>
    public double Value
    {
        get => _control.Values.Value;
        set => SetValue(nameof(GaugeValues.Value), value, () => _control.Values.Value = value);
    }

    /// <summary>Gets or sets the tube orientation.</summary>
    public Orientation Orientation
    {
        get => _control.ThermometerValues.Orientation;
        set => SetThermo(nameof(ThermometerGaugeValues.Orientation), value, () => _control.ThermometerValues.Orientation = value);
    }

    /// <summary>Gets or sets whether the user can drag the liquid level.</summary>
    public GaugeInputMode InputMode
    {
        get => _control.Values.InputMode;
        set => SetValue(nameof(GaugeValues.InputMode), value, () => _control.Values.InputMode = value);
    }

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();
        actions.Add(new DesignerActionHeaderItem("Gauge"));
        actions.Add(new DesignerActionPropertyItem(nameof(Value), "Value", "Gauge",
            "Target value."));
        actions.Add(new DesignerActionPropertyItem(nameof(Orientation), "Orientation", "Gauge",
            "Vertical or horizontal tube."));
        actions.Add(new DesignerActionPropertyItem(nameof(InputMode), "Input", "Gauge",
            "Manual uses Value from code. Interactive allows pointer and keyboard input."));
        return actions;
    }

    private void SetValue(string propertyName, object? proposed, Action assign)
    {
        PropertyDescriptor? property = TypeDescriptor.GetProperties(_control.Values)[propertyName];
        object? previous = property?.GetValue(_control.Values);
        if (Equals(previous, proposed))
        {
            return;
        }

        _service?.OnComponentChanging(_control.Values, property);
        assign();
        _service?.OnComponentChanged(_control.Values, property, previous, proposed);
    }

    private void SetThermo(string propertyName, object? proposed, Action assign)
    {
        PropertyDescriptor? property = TypeDescriptor.GetProperties(_control.ThermometerValues)[propertyName];
        object? previous = property?.GetValue(_control.ThermometerValues);
        if (Equals(previous, proposed))
        {
            return;
        }

        _service?.OnComponentChanging(_control.ThermometerValues, property);
        assign();
        _service?.OnComponentChanged(_control.ThermometerValues, property, previous, proposed);
    }
}
