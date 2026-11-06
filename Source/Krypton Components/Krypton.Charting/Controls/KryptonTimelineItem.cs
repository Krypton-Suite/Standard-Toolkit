#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// One step on a <see cref="KryptonTimeline"/>: start text, icon, and end text joined by the connector.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonTimelineItem
{
    #region Instance Fields

    private string _startText;
    private string _endText;
    private Image? _image;
    private bool _startUseBox;
    private bool _endUseBox;
    private TimelineItemStyle _style;
    private object? _tag;
    private Action? _changed;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimelineItem"/> class.
    /// </summary>
    public KryptonTimelineItem()
    {
        _startText = string.Empty;
        _endText = string.Empty;
    }

    /// <inheritdoc />
    public override string ToString() =>
        string.IsNullOrEmpty(_startText)
            ? (string.IsNullOrEmpty(_endText) ? nameof(KryptonTimelineItem) : _endText)
            : _startText;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the text drawn on the start side of the connector.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text drawn on the start side of the connector.")]
    [DefaultValue("")]
    public string StartText
    {
        get => _startText;
        set
        {
            value ??= string.Empty;
            if (_startText != value)
            {
                _startText = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets the text drawn on the end side of the connector.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text drawn on the end side of the connector.")]
    [DefaultValue("")]
    public string EndText
    {
        get => _endText;
        set
        {
            value ??= string.Empty;
            if (_endText != value)
            {
                _endText = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets the image drawn on the connector. A circle is drawn when this is null.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Image drawn on the connector. A circle is drawn when empty.")]
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
    /// Gets or sets whether <see cref="StartText"/> is drawn inside a palette box.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw the start text inside a palette box.")]
    [DefaultValue(false)]
    public bool StartUseBox
    {
        get => _startUseBox;
        set
        {
            if (_startUseBox != value)
            {
                _startUseBox = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether <see cref="EndText"/> is drawn inside a palette box.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw the end text inside a palette box.")]
    [DefaultValue(false)]
    public bool EndUseBox
    {
        get => _endUseBox;
        set
        {
            if (_endUseBox != value)
            {
                _endUseBox = value;
                Notify();
            }
        }
    }

    /// <summary>
    /// Gets or sets the status drawn on this step.
    /// </summary>
    /// <remarks>
    /// <see cref="TimelineItemStyle.Normal"/> uses the palette only.
    /// The other values add a hatch, a status stripe, a check, or a strike on the icon and any text box.
    /// </remarks>
    [Category(@"Appearance")]
    [Description(@"Status drawn on this step: normal, hatched, completed, delayed, or cancelled.")]
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

    internal void Attach(Action changed) => _changed = changed;

    internal void Detach() => _changed = null;

    private void Notify() => _changed?.Invoke();

    #endregion
}

/// <summary>
/// Event data for a <see cref="KryptonTimeline"/> item click or keyboard selection.
/// </summary>
public class KryptonTimelineItemEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimelineItemEventArgs"/> class.
    /// </summary>
    /// <param name="item">Item that was activated.</param>
    /// <param name="index">Index of <paramref name="item"/> in <see cref="KryptonTimeline.Items"/>.</param>
    public KryptonTimelineItemEventArgs(KryptonTimelineItem item, int index)
    {
        Item = item ?? throw new ArgumentNullException(nameof(item));
        Index = index;
    }

    /// <summary>
    /// Gets the item that was activated.
    /// </summary>
    public KryptonTimelineItem Item { get; }

    /// <summary>
    /// Gets the index of <see cref="Item"/>.
    /// </summary>
    public int Index { get; }
}

/// <summary>
/// Ordered steps owned by a <see cref="KryptonTimeline"/>.
/// </summary>
public class KryptonTimelineItemCollection : Collection<KryptonTimelineItem>
{
    #region Instance Fields

    private readonly KryptonTimeline _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimelineItemCollection"/> class.
    /// </summary>
    /// <param name="owner">Timeline that owns the items.</param>
    public KryptonTimelineItemCollection(KryptonTimeline owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void InsertItem(int index, KryptonTimelineItem item)
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
    protected override void SetItem(int index, KryptonTimelineItem item)
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
        foreach (KryptonTimelineItem item in this)
        {
            item.Detach();
        }

        base.ClearItems();
        _owner.OnItemsChanged();
    }

    #endregion
}

/// <summary>
/// Collection editor for <see cref="KryptonTimelineItem"/> instances.
/// </summary>
public class KryptonTimelineItemCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimelineItemCollectionEditor"/> class.
    /// </summary>
    /// <param name="type">Type of the collection being edited.</param>
    public KryptonTimelineItemCollectionEditor(Type type)
        : base(type)
    {
    }

    /// <inheritdoc />
    protected override Type CreateCollectionItemType() => typeof(KryptonTimelineItem);

    /// <inheritdoc />
    protected override string GetDisplayText(object? value)
    {
        if (value is KryptonTimelineItem item)
        {
            return item.ToString() ?? base.GetDisplayText(value);
        }

        return base.GetDisplayText(value);
    }
}
