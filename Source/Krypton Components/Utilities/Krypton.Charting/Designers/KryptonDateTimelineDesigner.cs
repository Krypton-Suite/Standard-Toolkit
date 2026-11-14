#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Designer for <see cref="KryptonDateTimeline"/>.
/// </summary>
internal class KryptonDateTimelineDesigner : ControlDesigner
{
    private DesignerActionListCollection? _actionLists;

    /// <inheritdoc />
    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);
        AutoResizeHandles = true;
    }

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            if (_actionLists == null)
            {
                _actionLists = new DesignerActionListCollection
                {
                    new KryptonDateTimelineActionList(this)
                };
            }

            return _actionLists;
        }
    }
}

/// <summary>
/// Smart-tag action list for <see cref="KryptonDateTimeline"/>.
/// </summary>
internal class KryptonDateTimelineActionList : DesignerActionList
{
    private readonly KryptonDateTimeline _timeline;
    private readonly IComponentChangeService? _service;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDateTimelineActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDateTimelineActionList(KryptonDateTimelineDesigner owner)
        : base(owner.Component)
    {
        _timeline = (KryptonDateTimeline)owner.Component;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    /// <summary>
    /// Gets or sets the date-timeline style.
    /// </summary>
    public DateTimelineStyle Style
    {
        get => _timeline.Style;
        set => Change(nameof(KryptonDateTimeline.Style), value, () => _timeline.Style = value);
    }

    /// <summary>
    /// Gets or sets whether today's date is marked.
    /// </summary>
    public bool ShowTodayIndicator
    {
        get => _timeline.ShowTodayIndicator;
        set => Change(nameof(KryptonDateTimeline.ShowTodayIndicator), value, () => _timeline.ShowTodayIndicator = value);
    }

    /// <summary>
    /// Gets or sets whether the visible range tracks the items.
    /// </summary>
    public bool AutoRange
    {
        get => _timeline.AutoRange;
        set => Change(nameof(KryptonDateTimeline.AutoRange), value, () => _timeline.AutoRange = value);
    }

    /// <summary>
    /// Gets or sets whether dragging an item moves it.
    /// </summary>
    public bool AllowMove
    {
        get => _timeline.AllowMove;
        set => Change(nameof(KryptonDateTimeline.AllowMove), value, () => _timeline.AllowMove = value);
    }

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _timeline.PaletteMode;
        set => Change(nameof(KryptonDateTimeline.PaletteMode), value, () => _timeline.PaletteMode = value);
    }

    /// <summary>
    /// Opens the item collection editor.
    /// </summary>
    public void EditItems() => KryptonDesignerCollectionActions.EditProperty(_timeline, nameof(KryptonDateTimeline.Items));

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection
        {
            new DesignerActionHeaderItem(@"Appearance"),
            new DesignerActionPropertyItem(nameof(Style), @"Style", @"Appearance", @"Chart rows or vis.js stacked boxes"),
            new DesignerActionHeaderItem(@"Behavior"),
            new DesignerActionPropertyItem(nameof(ShowTodayIndicator), @"Show Today", @"Behavior", @"Draw a marker at today's date"),
            new DesignerActionPropertyItem(nameof(AutoRange), @"Auto Range", @"Behavior", @"Fit the scale to the items"),
            new DesignerActionPropertyItem(nameof(AllowMove), @"Allow Move", @"Behavior", @"Drag an item to change its dates"),
            new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Behavior", @"Palette applied to drawing"),
            new DesignerActionHeaderItem(@"Data"),
            new DesignerActionMethodItem(this, nameof(EditItems), @"Edit Items", @"Data", @"Edit the timeline items", true)
        };
        return actions;
    }

    private void Change<T>(string propertyName, T value, Action assign)
    {
        PropertyDescriptor? property = TypeDescriptor.GetProperties(_timeline)[propertyName];
        object? previous = property?.GetValue(_timeline);
        _service?.OnComponentChanging(_timeline, property);
        assign();
        _service?.OnComponentChanged(_timeline, property, previous, value);
    }
}
