#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Step timeline of text, icons, and a connector, themed by the palette.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTimeline), "ToolboxBitmaps.KryptonTimeline.bmp")]
[DefaultEvent(nameof(ItemClick))]
[DefaultProperty(nameof(Items))]
[Designer("Krypton.Charting.KryptonTimelineDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[DesignerCategory(@"code")]
[Description(@"Displays an ordered timeline of text, icons, and a connector.")]
public class KryptonTimeline : VisualSimpleBase, ITimelineAccessible
{
    #region Instance Fields

    private readonly ViewDrawTimeline _draw;
    private readonly KryptonTimelineItemCollection _items;
    private Orientation _orientation;
    private TimelineLayout _layout;
    private bool _allowMove;
    private int _selectedIndex;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when an item is clicked or chosen with the keyboard.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when an item is clicked or chosen with the keyboard.")]
    public event EventHandler<KryptonTimelineItemEventArgs>? ItemClick;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTimeline"/> class.
    /// </summary>
    public KryptonTimeline()
    {
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        TabStop = true;
        BackColor = Color.Transparent;
        _orientation = Orientation.Horizontal;
        _layout = TimelineLayout.Alternate;
        _allowMove = true;
        _selectedIndex = -1;
        _items = new KryptonTimelineItemCollection(this);

        StateCommon = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteTriple(StateNormal, NeedPaintDelegate);
        StatePressed = new PaletteTriple(StateTracking, NeedPaintDelegate);
        ItemStyles = new TimelineItemStyleValues(NeedPaintDelegate);

        _draw = new ViewDrawTimeline(this);
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
    /// Gets or sets whether items run horizontally or vertically.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Whether items run horizontally or vertically.")]
    [DefaultValue(typeof(Orientation), "Horizontal")]
    public Orientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets how text and icons sit around the connector.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"How text and icons sit around the connector.")]
    [DefaultValue(TimelineLayout.Alternate)]
    public new TimelineLayout Layout
    {
        get => _layout;
        set
        {
            if (_layout != value)
            {
                _layout = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether dragging an item reorders the steps.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, dragging an item reorders the steps.")]
    [DefaultValue(true)]
    public bool AllowMove
    {
        get => _allowMove;
        set => _allowMove = value;
    }

    /// <summary>
    /// Gets the ordered steps. A fresh control has no items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Ordered steps. A fresh control has no items.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonTimelineItemCollectionEditor), typeof(UITypeEditor))]
    public KryptonTimelineItemCollection Items => _items;

    private bool ShouldSerializeItems() => _items.Count > 0;

    private void ResetItems() => _items.Clear();

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
    public KryptonTimelineItem? SelectedItem
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
    protected override Size DefaultSize => new Size(480, 160);

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new TimelineAccessibleObject(this);

    #endregion

    #region Internal

    internal void OnItemsChanged()
    {
        if (_selectedIndex >= _items.Count)
        {
            _selectedIndex = _items.Count == 0 ? -1 : _items.Count - 1;
        }

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

        if (raiseClick && index >= 0 && (changed || raiseClick))
        {
            ItemClick?.Invoke(this, new KryptonTimelineItemEventArgs(_items[index], index));
        }
    }

    internal void Reorder(int from, int to)
    {
        if (from == to || from < 0 || to < 0 || from >= _items.Count || to >= _items.Count)
        {
            return;
        }

        KryptonTimelineItem item = _items[from];
        _items.RemoveAt(from);
        _items.Insert(to, item);
        _selectedIndex = to;
        PerformNeedPaint(false);
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

    internal bool FocusCues => ShowFocusCues;

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
        KryptonTimelineItem item = _items[index];
        string name;
        if (!string.IsNullOrEmpty(item.StartText) && !string.IsNullOrEmpty(item.EndText))
        {
            name = item.StartText + " " + item.EndText;
        }
        else
        {
            name = string.IsNullOrEmpty(item.StartText) ? item.EndText : item.StartText;
        }

        return item.Style == TimelineItemStyle.Normal || string.IsNullOrEmpty(name) ? name : name + " " + item.Style;
    }

    #endregion
}
