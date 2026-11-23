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
/// Smart-tag action list for <see cref="KryptonVuMeter"/>.
/// </summary>
internal class KryptonVuMeterActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonVuMeter _control;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonVuMeterActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonVuMeterActionList(KryptonVuMeterDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonVuMeter)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

    /// <summary>Gets or sets the painted face.</summary>
    public VuMeterStyle Style
    {
        get => _control.Values.Style;
        set => SetValue(nameof(VuMeterValues.Style), value, () => _control.Values.Style = value);
    }

    /// <summary>Gets or sets whether the level is typed in or read from the recording device.</summary>
    public VuMeterInputMode InputMode
    {
        get => _control.Values.InputMode;
        set => SetValue(nameof(VuMeterValues.InputMode), value, () => _control.Values.InputMode = value);
    }

    /// <summary>Gets or sets the target level in VU.</summary>
    public double Value
    {
        get => _control.Values.Value;
        set => SetValue(nameof(VuMeterValues.Value), value, () => _control.Values.Value = value);
    }

    /// <summary>Gets or sets whether a peak indicator is drawn.</summary>
    public bool PeakHold
    {
        get => _control.Values.PeakHold;
        set => SetValue(nameof(VuMeterValues.PeakHold), value, () => _control.Values.PeakHold = value);
    }

    #endregion

    #region Public Override

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();
        actions.Add(new DesignerActionHeaderItem("Meter"));
        actions.Add(new DesignerActionPropertyItem(nameof(Style), "Style", "Meter",
            "Analog needle or segmented bar."));
        actions.Add(new DesignerActionPropertyItem(nameof(InputMode), "Input", "Meter",
            "Manual uses Value. Capture reads the default recording device."));
        actions.Add(new DesignerActionPropertyItem(nameof(Value), "Value", "Meter",
            "Target level in VU. The indicator eases toward this value."));
        actions.Add(new DesignerActionPropertyItem(nameof(PeakHold), "Peak hold", "Meter",
            "Draw a second indicator that holds the recent peak."));
        return actions;
    }

    #endregion

    #region Implementation

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

    #endregion
}
