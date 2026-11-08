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
/// Smart-tag action list for <see cref="KryptonEqualizer"/>.
/// </summary>
internal class KryptonEqualizerActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonEqualizer _control;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEqualizerActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonEqualizerActionList(KryptonEqualizerDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonEqualizer)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

    /// <summary>Gets or sets the painted face.</summary>
    public EqualizerStyle Style
    {
        get => _control.Values.Style;
        set => SetValue(nameof(EqualizerValues.Style), value, () => _control.Values.Style = value);
    }

    /// <summary>Gets or sets whether levels are typed in or read from the recording device.</summary>
    public EqualizerInputMode InputMode
    {
        get => _control.Values.InputMode;
        set => SetValue(nameof(EqualizerValues.InputMode), value, () => _control.Values.InputMode = value);
    }

    /// <summary>Gets or sets the number of bands.</summary>
    public int BandCount
    {
        get => _control.Values.BandCount;
        set => SetValue(nameof(EqualizerValues.BandCount), value, () => _control.Values.BandCount = value);
    }

    /// <summary>Gets or sets whether gain thumbs can be dragged.</summary>
    public bool Interactive
    {
        get => _control.Values.Interactive;
        set => SetValue(nameof(EqualizerValues.Interactive), value, () => _control.Values.Interactive = value);
    }

    /// <summary>Gets or sets whether a peak indicator is drawn.</summary>
    public bool PeakHold
    {
        get => _control.Values.PeakHold;
        set => SetValue(nameof(EqualizerValues.PeakHold), value, () => _control.Values.PeakHold = value);
    }

    #endregion

    #region Public Override

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();
        actions.Add(new DesignerActionHeaderItem("Equalizer"));
        actions.Add(new DesignerActionPropertyItem(nameof(Style), "Style", "Equalizer",
            "Solid bars or segmented LED-style bars."));
        actions.Add(new DesignerActionPropertyItem(nameof(InputMode), "Input", "Equalizer",
            "Manual uses SetLevels. Capture reads the default recording device."));
        actions.Add(new DesignerActionPropertyItem(nameof(BandCount), "Bands", "Equalizer",
            "Number of frequency bands."));
        actions.Add(new DesignerActionPropertyItem(nameof(Interactive), "Interactive", "Equalizer",
            "Allow dragging gain thumbs."));
        actions.Add(new DesignerActionPropertyItem(nameof(PeakHold), "Peak hold", "Equalizer",
            "Draw peak marks that hold, then fall."));
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
