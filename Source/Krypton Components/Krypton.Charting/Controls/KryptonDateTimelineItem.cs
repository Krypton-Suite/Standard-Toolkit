#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// One task or milestone on a <see cref="KryptonDateTimeline"/>.
/// A positive span (<see cref="Finish"/> later than <see cref="Start"/>) is a bar.
/// An equal or earlier finish, or a single assigned date, is a milestone.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonDateTimelineItem
{
    #region Instance Fields

    private DateTime _start;
    private DateTime _finish;
    private string _caption;
    private string _details;
    private Image? _image;
    private int _row;
    private TimelineItemStyle _style;
    private object? _tag;
    private Action? _changed;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDateTimelineItem"/> class.
    /// </summary>
    public KryptonDateTimelineItem()
    {
        _caption = string.Empty;
        _details = string.Empty;
    }

    /// <inheritdoc />
    public override string ToString() =>
        string.IsNullOrEmpty(_caption) ? nameof(KryptonDateTimelineItem) : _caption;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the start date. <see cref="DateTime.MinValue"/> means the date is not set.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Start date. DateTime.MinValue means the date is not set.")]
    public DateTime Start
    {
        get => _start;
        set
        {
            if (_start != value)
            {
                _start = value;
                Notify();
            }
        }
    }

    private bool ShouldSerializeStart() => _start != DateTime.MinValue;

    private void ResetStart() => Start = DateTime.MinValue;

    /// <summary>
    /// Gets or sets the finish date. A finish later than <see cref="Start"/> draws a bar; otherwise the item is a milestone.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Finish date. Later than Start draws a bar; otherwise the item is a milestone.")]
    public DateTime Finish
    {
        get => _finish;
        set
        {
            if (_finish != value)
            {
                _finish = value;
                Notify();
            }
        }
    }

    private bool ShouldSerializeFinish() => _finish != DateTime.MinValue;

    private void ResetFinish() => Finish = DateTime.MinValue;

    /// <summary>
    /// Gets or sets the caption drawn on the bar or beside the milestone.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Caption drawn on the bar or beside the milestone.")]
    [DefaultValue("")]
    public string Caption
    {
        get => _caption;
        set
        {
            value ??= string.Empty;
            if (_caption != value)
            {
                _caption = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets the secondary text drawn under the caption when the bar is tall enough.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Secondary text drawn under the caption when the bar is tall enough.")]
    [DefaultValue("")]
    public string Details
    {
        get => _details;
        set
        {
            value ??= string.Empty;
            if (_details != value)
            {
                _details = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets an optional image drawn inside a task bar or in place of the milestone diamond.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional image drawn inside a task bar or in place of the milestone diamond.")]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;
        set
        {
            if (!ReferenceEquals(_image, value))
            {
                _image = value;
                Notify();
            }
        }
    }

    private bool ShouldSerializeImage() => _image != null;

    private void ResetImage() => Image = null;

    /// <summary>
    /// Gets or sets the zero-based row that stacks this item under the time scale.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Zero-based row that stacks this item under the time scale.")]
    [DefaultValue(0)]
    public int Row
    {
        get => _row;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (_row != value)
            {
                _row = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets the status drawn on this task or milestone.
    /// </summary>
    /// <remarks>
    /// <see cref="TimelineItemStyle.Normal"/> uses the palette only.
    /// The other values add a hatch, a status stripe, a check, or a strike. Chart and Vis both draw it.
    /// </remarks>
    [Category(@"Appearance")]
    [Description(@"Status drawn on this task or milestone: normal, hatched, completed, delayed, or cancelled.")]
    [DefaultValue(TimelineItemStyle.Normal)]
    public TimelineItemStyle Style
    {
        get => _style;
        set
        {
            if (_style != value)
            {
                _style = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets user-defined data associated with the item.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the item.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(StringConverter))]
    public object? Tag
    {
        get => _tag;
        set
        {
            if (!Equals(_tag, value))
            {
                _tag = value;
                Notify();
            }
        }
    }

    private bool ShouldSerializeTag() => _tag != null;

    private void ResetTag() => Tag = null;

    #endregion

    #region Internal

    /// <summary>
    /// Gets a value indicating whether the item has at least one assigned date.
    /// </summary>
    internal bool HasDate => _start != DateTime.MinValue || _finish != DateTime.MinValue;

    /// <summary>
    /// Gets a value indicating whether the item draws as a milestone diamond.
    /// A single assigned date, or a finish that is not later than the start, is a milestone.
    /// </summary>
    [Browsable(false)]
    public bool IsMilestone => !HasDate || _finish <= _start || _start == DateTime.MinValue || _finish == DateTime.MinValue;

    /// <summary>
    /// Gets the date used to place a milestone, or the bar start.
    /// </summary>
    internal DateTime Anchor => _start != DateTime.MinValue ? _start : _finish;

    /// <summary>
    /// Gets the exclusive end of a task bar. Milestones return <see cref="Anchor"/>.
    /// </summary>
    internal DateTime BarFinish => IsMilestone ? Anchor : _finish;

    internal void Place(DateTime start, DateTime finish, int row)
    {
        if (row < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(row));
        }

        if (_start == start && _finish == finish && _row == row)
        {
            return;
        }

        _start = start;
        _finish = finish;
        _row = row;
        Notify();
    }

    internal void Attach(Action changed) => _changed = changed;

    internal void Detach() => _changed = null;

    private void Notify() => _changed?.Invoke();

    #endregion
}

/// <summary>
/// Event data for a <see cref="KryptonDateTimeline"/> item click or keyboard selection.
/// </summary>
public class KryptonDateTimelineItemEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDateTimelineItemEventArgs"/> class.
    /// </summary>
    /// <param name="item">Item that was activated.</param>
    /// <param name="index">Index of <paramref name="item"/> in <see cref="KryptonDateTimeline.Items"/>.</param>
    public KryptonDateTimelineItemEventArgs(KryptonDateTimelineItem item, int index)
    {
        Item = item ?? throw new ArgumentNullException(nameof(item));
        Index = index;
    }

    /// <summary>
    /// Gets the item that was activated.
    /// </summary>
    public KryptonDateTimelineItem Item { get; }

    /// <summary>
    /// Gets the index of <see cref="Item"/>.
    /// </summary>
    public int Index { get; }
}

/// <summary>
/// Tasks and milestones owned by a <see cref="KryptonDateTimeline"/>.
/// </summary>
public class KryptonDateTimelineItemCollection : Collection<KryptonDateTimelineItem>
{
    #region Instance Fields

    private readonly KryptonDateTimeline _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDateTimelineItemCollection"/> class.
    /// </summary>
    /// <param name="owner">Date timeline that owns the items.</param>
    public KryptonDateTimelineItemCollection(KryptonDateTimeline owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void InsertItem(int index, KryptonDateTimelineItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        item.Attach(_owner.OnItemsChanged);
        base.InsertItem(index, item);
        _owner.OnItemsChanged();
    }

    /// <inheritdoc />
    protected override void SetItem(int index, KryptonDateTimelineItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        this[index].Detach();
        item.Attach(_owner.OnItemsChanged);
        base.SetItem(index, item);
        _owner.OnItemsChanged();
    }

    /// <inheritdoc />
    protected override void RemoveItem(int index)
    {
        this[index].Detach();
        base.RemoveItem(index);
        _owner.OnItemsChanged();
    }

    /// <inheritdoc />
    protected override void ClearItems()
    {
        foreach (KryptonDateTimelineItem item in this)
        {
            item.Detach();
        }

        base.ClearItems();
        _owner.OnItemsChanged();
    }

    #endregion
}

/// <summary>
/// Collection editor for <see cref="KryptonDateTimelineItem"/> instances.
/// </summary>
public class KryptonDateTimelineItemCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDateTimelineItemCollectionEditor"/> class.
    /// </summary>
    /// <param name="type">Type of the collection being edited.</param>
    public KryptonDateTimelineItemCollectionEditor(Type type)
        : base(type)
    {
    }

    /// <inheritdoc />
    protected override Type CreateCollectionItemType() => typeof(KryptonDateTimelineItem);

    /// <inheritdoc />
    protected override string GetDisplayText(object? value)
    {
        if (value is KryptonDateTimelineItem item)
        {
            return item.ToString() ?? base.GetDisplayText(value);
        }

        return base.GetDisplayText(value);
    }
}
