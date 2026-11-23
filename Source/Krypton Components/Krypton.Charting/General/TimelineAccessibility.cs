#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Item names and selection exposed to the timeline accessible object.
/// </summary>
internal interface ITimelineAccessible
{
    /// <summary>Gets the number of items.</summary>
    int ItemCount { get; }

    /// <summary>Gets the selected item index, or -1.</summary>
    int SelectedIndex { get; }

    /// <summary>Gets the accessible name of the item at <paramref name="index"/>.</summary>
    string GetAccessibleItemName(int index);
}

/// <summary>
/// Exposes a timeline as a list of item names.
/// </summary>
internal sealed class TimelineAccessibleObject : Control.ControlAccessibleObject
{
    private readonly Control _owner;
    private readonly ITimelineAccessible _source;

    /// <summary>
    /// Initialize a new instance of the <see cref="TimelineAccessibleObject"/> class.
    /// </summary>
    /// <param name="owner">Owning timeline control.</param>
    public TimelineAccessibleObject(Control owner)
        : base(owner)
    {
        _owner = owner;
        _source = (ITimelineAccessible)owner;
    }

    /// <inheritdoc />
    public override AccessibleRole Role => AccessibleRole.List;

    /// <inheritdoc />
    public override string? Name => string.IsNullOrEmpty(_owner.AccessibleName) ? _owner.Name : _owner.AccessibleName;

    /// <inheritdoc />
    public override int GetChildCount() => _source.ItemCount;

    /// <inheritdoc />
    public override AccessibleObject? GetChild(int index)
    {
        if (index < 0 || index >= _source.ItemCount)
        {
            return null;
        }

        return new TimelineItemAccessibleObject(this, _source, index);
    }
}

/// <summary>
/// One timeline item in the accessibility tree.
/// </summary>
internal sealed class TimelineItemAccessibleObject : AccessibleObject
{
    private readonly AccessibleObject _parent;
    private readonly ITimelineAccessible _source;
    private readonly int _index;

    /// <summary>
    /// Initialize a new instance of the <see cref="TimelineItemAccessibleObject"/> class.
    /// </summary>
    public TimelineItemAccessibleObject(AccessibleObject parent, ITimelineAccessible source, int index)
    {
        _parent = parent;
        _source = source;
        _index = index;
    }

    /// <inheritdoc />
    public override AccessibleRole Role => AccessibleRole.ListItem;

    /// <inheritdoc />
    public override string Name => _source.GetAccessibleItemName(_index);

    /// <inheritdoc />
    public override AccessibleObject Parent => _parent;

    /// <inheritdoc />
    public override AccessibleStates State
    {
        get
        {
            AccessibleStates state = AccessibleStates.Selectable | AccessibleStates.Focusable;
            if (_index == _source.SelectedIndex)
            {
                state |= AccessibleStates.Selected;
            }

            return state;
        }
    }
}
