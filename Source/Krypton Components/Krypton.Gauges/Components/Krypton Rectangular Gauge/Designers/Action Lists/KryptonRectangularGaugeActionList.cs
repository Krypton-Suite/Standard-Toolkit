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
/// Smart-tag action list for <see cref="KryptonRectangularGauge"/>.
/// </summary>
internal class KryptonRectangularGaugeActionList : DesignerActionList
{
    private readonly KryptonRectangularGauge _control;
    private readonly IComponentChangeService? _service;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRectangularGaugeActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonRectangularGaugeActionList(KryptonRectangularGaugeDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonRectangularGauge)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    /// <summary>Gets or sets the target value.</summary>
    public double Value
    {
        get => _control.Values.Value;
        set => SetValue(nameof(GaugeValues.Value), value, () => _control.Values.Value = value);
    }

    /// <summary>Gets or sets the painted face.</summary>
    public RectangularGaugeStyle Style
    {
        get => _control.RectangularValues.Style;
        set => SetRect(nameof(RectangularGaugeValues.Style), value, () => _control.RectangularValues.Style = value);
    }

    /// <summary>Gets or sets whether the user can drag the fill.</summary>
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
        actions.Add(new DesignerActionPropertyItem(nameof(Style), "Style", "Gauge",
            "Solid fill or segmented blocks."));
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

    private void SetRect(string propertyName, object? proposed, Action assign)
    {
        PropertyDescriptor? property = TypeDescriptor.GetProperties(_control.RectangularValues)[propertyName];
        object? previous = property?.GetValue(_control.RectangularValues);
        if (Equals(previous, proposed))
        {
            return;
        }

        _service?.OnComponentChanging(_control.RectangularValues, property);
        assign();
        _service?.OnComponentChanged(_control.RectangularValues, property, previous, proposed);
    }
}
