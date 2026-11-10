#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Designer for <see cref="KryptonTimeline"/>.
/// </summary>
internal class KryptonTimelineDesigner : ControlDesigner
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
                    new KryptonTimelineActionList(this)
                };
            }

            return _actionLists;
        }
    }
}

/// <summary>
/// Smart-tag action list for <see cref="KryptonTimeline"/>.
/// </summary>
internal class KryptonTimelineActionList : DesignerActionList
{
    private readonly KryptonTimeline _timeline;
    private readonly IComponentChangeService? _service;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimelineActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonTimelineActionList(KryptonTimelineDesigner owner)
        : base(owner.Component)
    {
        _timeline = (KryptonTimeline)owner.Component;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    /// <summary>
    /// Gets or sets the timeline orientation.
    /// </summary>
    public Orientation Orientation
    {
        get => _timeline.Orientation;
        set => Change(nameof(KryptonTimeline.Orientation), value, () => _timeline.Orientation = value);
    }

    /// <summary>
    /// Gets or sets how text and icons sit around the connector.
    /// </summary>
    public TimelineLayout Layout
    {
        get => _timeline.Layout;
        set => Change(nameof(KryptonTimeline.Layout), value, () => _timeline.Layout = value);
    }

    /// <summary>
    /// Gets or sets whether dragging an item reorders the steps.
    /// </summary>
    public bool AllowMove
    {
        get => _timeline.AllowMove;
        set => Change(nameof(KryptonTimeline.AllowMove), value, () => _timeline.AllowMove = value);
    }

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _timeline.PaletteMode;
        set => Change(nameof(KryptonTimeline.PaletteMode), value, () => _timeline.PaletteMode = value);
    }

    /// <summary>
    /// Opens the item collection editor.
    /// </summary>
    public void EditItems() => KryptonDesignerCollectionActions.EditProperty(_timeline, nameof(KryptonTimeline.Items));

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection
        {
            new DesignerActionHeaderItem(@"Appearance"),
            new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), @"Appearance", @"Horizontal or vertical layout"),
            new DesignerActionPropertyItem(nameof(Layout), nameof(Layout), @"Appearance", @"Text and icon placement"),
            new DesignerActionPropertyItem(nameof(AllowMove), @"Allow Move", @"Behavior", @"Drag an item to reorder the steps"),
            new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Appearance", @"Palette applied to drawing"),
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
