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
/// Smart-tag action list for <see cref="KryptonAmpereMeterGauge"/>.
/// </summary>
internal class KryptonAmpereMeterGaugeActionList : DesignerActionList
{
    private readonly KryptonAmpereMeterGauge _control;
    private readonly IComponentChangeService? _service;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonAmpereMeterGaugeActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonAmpereMeterGaugeActionList(KryptonAmpereMeterGaugeDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonAmpereMeterGauge)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    /// <summary>Gets or sets the target value.</summary>
    public double Value
    {
        get => _control.Values.Value;
        set => SetValue(nameof(GaugeValues.Value), value, () => _control.Values.Value = value);
    }

    /// <summary>Gets or sets the scale minimum (negative values enable DC center-zero).</summary>
    public double Minimum
    {
        get => _control.Values.Minimum;
        set => SetValue(nameof(GaugeValues.Minimum), value, () => _control.Values.Minimum = value);
    }

    /// <summary>Gets or sets the scale maximum.</summary>
    public double Maximum
    {
        get => _control.Values.Maximum;
        set => SetValue(nameof(GaugeValues.Maximum), value, () => _control.Values.Maximum = value);
    }

    /// <summary>Gets or sets whether the user can drag the needle.</summary>
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
            "Target value in amperes."));
        actions.Add(new DesignerActionPropertyItem(nameof(Minimum), "Minimum", "Gauge",
            "Scale minimum. Use a negative value for a DC center-zero meter."));
        actions.Add(new DesignerActionPropertyItem(nameof(Maximum), "Maximum", "Gauge",
            "Scale maximum."));
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
}
