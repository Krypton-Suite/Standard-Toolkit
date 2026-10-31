#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Horizontal date chart of task bars and milestones, with a time scale, today line, and pan/zoom.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonDateTimeline), "ToolboxBitmaps.KryptonDateTimeline.bmp")]
[DefaultEvent(nameof(ItemClick))]
[DefaultProperty(nameof(Items))]
[Designer("Krypton.Charting.KryptonDateTimelineDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[DesignerCategory(@"code")]
[Description(@"Displays tasks and milestones on a horizontal time scale.")]
public class KryptonDateTimeline : VisualSimpleBase, ITimelineAccessible
{
    #region Instance Fields

    private readonly ViewDrawDateTimeline _draw;
    private readonly KryptonDateTimelineItemCollection _items;
    private bool _showToday;
    private bool _autoRange;
    private DateTimelineStyle _style;
    private bool _allowMove;
    private DateTime _visibleStart;
    private DateTime _visibleFinish;
    private int _selectedIndex;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when an item is clicked or chosen with the keyboard.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when an item is clicked or chosen with the keyboard.")]
    public event EventHandler<KryptonDateTimelineItemEventArgs>? ItemClick;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDateTimeline"/> class.
    /// </summary>
    public KryptonDateTimeline()
    {
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        TabStop = true;
        BackColor = Color.Transparent;
        _showToday = true;
        _autoRange = true;
        _allowMove = true;
        _selectedIndex = -1;
        _items = new KryptonDateTimelineItemCollection(this);

        StateCommon = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteTriple(StateNormal, NeedPaintDelegate);
        StatePressed = new PaletteTriple(StateTracking, NeedPaintDelegate);
        ItemStyles = new TimelineItemStyleValues(NeedPaintDelegate);

        _draw = new ViewDrawDateTimeline(this);
        ViewManager = new ViewManager(this, _draw);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets the tasks and milestones. A fresh control has no items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Tasks and milestones. A fresh control has no items.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonDateTimelineItemCollectionEditor), typeof(UITypeEditor))]
    public KryptonDateTimelineItemCollection Items => _items;

    private bool ShouldSerializeItems() => _items.Count > 0;

    private void ResetItems() => _items.Clear();

    /// <summary>
    /// Gets or sets whether items are row bars or stacked boxes above a bottom axis.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Chart draws row bars. Vis stacks boxes above a bottom axis, with a stem for a single date.")]
    [DefaultValue(DateTimelineStyle.Chart)]
    public DateTimelineStyle Style
    {
        get => _style;
        set
        {
            if (_style != value)
            {
                _style = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether dragging an item moves its dates. Chart style also changes the row.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, dragging an item moves its dates. Chart style also changes the row.")]
    [DefaultValue(true)]
    public bool AllowMove
    {
        get => _allowMove;
        set => _allowMove = value;
    }

    /// <summary>
    /// Gets or sets whether a marker is drawn at today's date when it is inside the visible range.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw a marker at today's date when it is inside the visible range.")]
    [DefaultValue(true)]
    public bool ShowTodayIndicator
    {
        get => _showToday;
        set
        {
            if (_showToday != value)
            {
                _showToday = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the visible range tracks the items. Zooming, panning, or assigning a visible date turns this off.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, the visible range tracks the items. Zoom, pan, or an assigned date turns this off.")]
    [DefaultValue(true)]
    public bool AutoRange
    {
        get => _autoRange;
        set
        {
            if (_autoRange == value)
            {
                return;
            }

            if (!value)
            {
                GetVisibleRange(out _visibleStart, out _visibleFinish);
            }

            _autoRange = value;
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Gets or sets the first visible date. Assigning a value turns <see cref="AutoRange"/> off.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"First visible date. Assigning a value turns AutoRange off.")]
    public DateTime VisibleStart
    {
        get
        {
            GetVisibleRange(out DateTime start, out DateTime _);
            return start;
        }
        set
        {
            GetVisibleRange(out DateTime _, out DateTime finish);
            SetVisibleRange(value, finish);
        }
    }

    private bool ShouldSerializeVisibleStart() => !_autoRange;

    private void ResetVisibleStart() => AutoRange = true;

    /// <summary>
    /// Gets or sets the last visible date. Assigning a value turns <see cref="AutoRange"/> off.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Last visible date. Assigning a value turns AutoRange off.")]
    public DateTime VisibleFinish
    {
        get
        {
            GetVisibleRange(out DateTime _, out DateTime finish);
            return finish;
        }
        set
        {
            GetVisibleRange(out DateTime start, out DateTime _);
            SetVisibleRange(start, value);
        }
    }

    private bool ShouldSerializeVisibleFinish() => !_autoRange;

    private void ResetVisibleFinish() => AutoRange = true;

    /// <summary>
    /// Gets or sets the selected item index, or -1 when nothing is selected.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Selected item index, or -1 when nothing is selected.")]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => _selectedIndex;
        set => SelectCore(value, false);
    }

    /// <summary>
    /// Gets or sets the selected item.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonDateTimelineItem? SelectedItem
    {
        get => _selectedIndex >= 0 && _selectedIndex < _items.Count ? _items[_selectedIndex] : null;
        set => SelectedIndex = value == null ? -1 : _items.IndexOf(value);
    }

    /// <summary>
    /// Gets the colours for each <see cref="TimelineItemStyle"/>.
    /// Expand this in the property grid to edit Hatched, Completed, Delayed, and Cancelled.
    /// An empty colour keeps that style's built-in colour.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Colours for Hatched, Completed, Delayed, and Cancelled. Empty keeps the built-in colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TimelineItemStyleValues ItemStyles { get; }

    private bool ShouldSerializeItemStyles() => !ItemStyles.IsDefault;

    private void ResetItemStyles() => ItemStyles.Reset();

    /// <summary>
    /// Gets access to the common timeline appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common timeline appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled timeline appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled timeline appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal timeline appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal timeline appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking timeline appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking timeline appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed and selected timeline appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed and selected timeline appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(560, 220);

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new TimelineAccessibleObject(this);

    /// <inheritdoc />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        if ((ModifierKeys & Keys.Control) == Keys.Control)
        {
            _draw.ZoomAt(e.Location, e.Delta);
            if (e is HandledMouseEventArgs handled)
            {
                handled.Handled = true;
            }

            return;
        }

        base.OnMouseWheel(e);
    }

    #endregion

    #region Internal

    internal bool FocusCues => ShowFocusCues;

    internal void OnItemsChanged()
    {
        if (_selectedIndex >= _items.Count)
        {
            _selectedIndex = _items.Count == 0 ? -1 : _items.Count - 1;
        }

        PerformNeedPaint(true);
    }

    internal void GetVisibleRange(out DateTime start, out DateTime finish)
    {
        if (!_autoRange)
        {
            start = _visibleStart;
            finish = _visibleFinish <= _visibleStart ? _visibleStart.AddDays(1) : _visibleFinish;
            return;
        }

        bool any = false;
        DateTime min = DateTime.MaxValue;
        DateTime max = DateTime.MinValue;
        foreach (KryptonDateTimelineItem item in _items)
        {
            if (!item.HasDate)
            {
                continue;
            }

            DateTime anchor = item.Anchor;
            DateTime end = item.BarFinish;
            if (anchor < min)
            {
                min = anchor;
            }

            if (end > max)
            {
                max = end;
            }

            any = true;
        }

        if (!any)
        {
            start = DateTime.Today.AddDays(-7);
            finish = DateTime.Today.AddDays(7);
            return;
        }

        if (max <= min)
        {
            max = min.AddDays(1);
        }

        TimeSpan span = max - min;
        long padTicks = Math.Max(span.Ticks / 20, TimeSpan.TicksPerHour);
        var pad = TimeSpan.FromTicks(padTicks);
        start = min - pad;
        finish = max + pad;
    }

    internal void SetVisibleRange(DateTime start, DateTime finish)
    {
        if (finish <= start)
        {
            finish = start.AddMinutes(1);
        }

        _autoRange = false;
        _visibleStart = start;
        _visibleFinish = finish;
        PerformNeedPaint(true);
    }

    internal void SelectCore(int index, bool raiseClick)
    {
        if (index < -1 || index >= _items.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        bool changed = _selectedIndex != index;
        if (changed)
        {
            _selectedIndex = index;
            PerformNeedPaint(false);
        }

        if (raiseClick && index >= 0)
        {
            ItemClick?.Invoke(this, new KryptonDateTimelineItemEventArgs(_items[index], index));
        }
    }

    internal bool ProcessKey(KeyEventArgs e)
    {
        if (_items.Count == 0)
        {
            return false;
        }

        int next;
        if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
        {
            next = _selectedIndex < 0 ? 0 : Math.Max(0, _selectedIndex - 1);
        }
        else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
        {
            next = _selectedIndex < 0 ? 0 : Math.Min(_items.Count - 1, _selectedIndex + 1);
        }
        else if (e.KeyCode == Keys.Home)
        {
            next = 0;
        }
        else if (e.KeyCode == Keys.End)
        {
            next = _items.Count - 1;
        }
        else
        {
            return false;
        }

        if (next != _selectedIndex)
        {
            SelectCore(next, true);
        }

        return true;
    }

    internal IPaletteTriple PaletteFor(PaletteState state)
    {
        if (state == PaletteState.Disabled)
        {
            return StateDisabled;
        }

        if (state == PaletteState.Pressed || state == PaletteState.CheckedPressed)
        {
            return StatePressed;
        }

        if (state == PaletteState.Tracking)
        {
            return StateTracking;
        }

        return StateNormal;
    }

    int ITimelineAccessible.ItemCount => _items.Count;

    int ITimelineAccessible.SelectedIndex => _selectedIndex;

    string ITimelineAccessible.GetAccessibleItemName(int index)
    {
        KryptonDateTimelineItem item = _items[index];
        string name = !string.IsNullOrEmpty(item.Caption) ? item.Caption : item.Anchor.ToString("d");
        return item.Style == TimelineItemStyle.Normal || string.IsNullOrEmpty(name) ? name : name + " " + item.Style;
    }

    #endregion
}
