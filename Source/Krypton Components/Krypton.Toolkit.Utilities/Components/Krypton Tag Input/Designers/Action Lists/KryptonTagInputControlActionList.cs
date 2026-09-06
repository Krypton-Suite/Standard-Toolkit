#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Smart-tag action list for <see cref="KryptonTagInputControl"/>.
/// </summary>
internal class KryptonTagInputControlActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonTagInputControl _control;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagInputControlActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonTagInputControlActionList(KryptonTagInputControlDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonTagInputControl)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

    /// <summary>Gets or sets cue hint text shown in the empty input.</summary>
    public string CueHintText
    {
        get => _control.Values.CueHintText;
        set => SetValuesProperty(nameof(KryptonTagInputValues.CueHintText), _control.Values.CueHintText, value,
            () => _control.Values.CueHintText = value);
    }

    /// <summary>Gets or sets whether duplicate tag text is allowed.</summary>
    public bool AllowDuplicates
    {
        get => _control.Values.AllowDuplicates;
        set => SetValuesProperty(nameof(KryptonTagInputValues.AllowDuplicates), _control.Values.AllowDuplicates, value,
            () => _control.Values.AllowDuplicates = value);
    }

    /// <summary>Gets or sets the maximum number of tags, or 0 for no limit.</summary>
    public int MaxTags
    {
        get => _control.Values.MaxTags;
        set => SetValuesProperty(nameof(KryptonTagInputValues.MaxTags), _control.Values.MaxTags, value,
            () => _control.Values.MaxTags = value);
    }

    /// <summary>Gets or sets whether the input is read-only.</summary>
    public bool ReadOnly
    {
        get => _control.ReadOnly;
        set
        {
            if (_control.ReadOnly != value)
            {
                _service?.OnComponentChanged(_control, null, _control.ReadOnly, value);
                _control.ReadOnly = value;
            }
        }
    }

    /// <summary>Gets or sets whether suggestion auto-complete is enabled.</summary>
    public bool EnableSuggestions
    {
        get => _control.Values.EnableSuggestions;
        set => SetValuesProperty(nameof(KryptonTagInputValues.EnableSuggestions), _control.Values.EnableSuggestions, value,
            () => _control.Values.EnableSuggestions = value);
    }

    /// <summary>Gets or sets whether each chip shows a close button.</summary>
    public bool ShowRemoveButton
    {
        get => _control.Values.ShowRemoveButton;
        set => SetValuesProperty(nameof(KryptonTagInputValues.ShowRemoveButton), _control.Values.ShowRemoveButton, value,
            () => _control.Values.ShowRemoveButton = value);
    }

    #endregion

    #region Public Override

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();
        if (_control == null)
        {
            return actions;
        }

        actions.Add(new DesignerActionHeaderItem("Behavior"));
        actions.Add(new DesignerActionPropertyItem(nameof(ReadOnly), "Read only", "Behavior",
            "When true, tags cannot be added or removed from the UI."));
        actions.Add(new DesignerActionPropertyItem(nameof(AllowDuplicates), "Allow duplicates", "Behavior",
            "Whether the same tag text may be added more than once."));
        actions.Add(new DesignerActionPropertyItem(nameof(MaxTags), "Max tags", "Behavior",
            "Maximum number of tags. Use 0 for no limit."));
        actions.Add(new DesignerActionPropertyItem(nameof(EnableSuggestions), "Suggestions", "Behavior",
            "Whether the input uses the suggestion list for auto-complete."));

        actions.Add(new DesignerActionHeaderItem("Appearance"));
        actions.Add(new DesignerActionPropertyItem(nameof(CueHintText), "Cue hint", "Appearance",
            "Cue hint text shown in the input when it is empty."));
        actions.Add(new DesignerActionPropertyItem(nameof(ShowRemoveButton), "Show remove", "Appearance",
            "Whether each chip shows a themed close button."));

        return actions;
    }

    #endregion

    #region Implementation

    private void SetValuesProperty<T>(string propertyName, T current, T value, Action apply)
    {
        if (Equals(current, value))
        {
            return;
        }

        var descriptor = TypeDescriptor.GetProperties(_control.Values)[propertyName];
        _service?.OnComponentChanged(_control.Values, descriptor, current, value);
        apply();
    }

    #endregion
}
