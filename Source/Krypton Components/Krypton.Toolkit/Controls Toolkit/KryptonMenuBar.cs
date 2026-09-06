#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Native horizontal form menu bar that is not a <see cref="ToolStrip"/> / <see cref="MenuStrip"/> subclass.
/// Top-level items are <see cref="KryptonContextMenuItem"/> instances (plus separators); click, Alt, and
/// mnemonic open the item's nested <see cref="KryptonContextMenuItem.Items"/> as a <see cref="KryptonContextMenu"/> popup.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonMenuBar), "ToolboxBitmaps.KryptonMenuBar.bmp")]
[DefaultProperty(nameof(Items))]
[Designer(typeof(KryptonMenuBarDesigner))]
[DesignerCategory(@"code")]
[Description(@"Native Krypton menu bar using context-menu items.")]
[Docking(DockingBehavior.Ask)]
public class KryptonMenuBar : VisualSimpleBase
{
    #region Instance Fields

    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewLayoutStack _itemStack;
    private readonly PaletteBackInheritMenuStrip _barBackInherit;
    private readonly PaletteBack _barBack;
    private readonly PaletteBorderInheritRedirect _barBorderInherit;
    private readonly PaletteBorder _barBorder;
    private readonly PaletteMetricRedirect _itemMetric;
    private readonly Dictionary<KryptonContextMenuItem, ViewDrawMenuBarItem> _itemViews;
    private readonly KryptonContextMenu _dropDown;
    private readonly DropDownKeyFilter _keyFilter;
    private bool _useMnemonic;
    private bool _menuMode;
    private bool _filterActive;
    private bool _rebuildSuspended;
    private KryptonContextMenuItem? _openItem;
    private KryptonContextMenuItem? _highlightedItem;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMenuBar"/> class.
    /// </summary>
    public KryptonMenuBar()
    {
        SetStyle(ControlStyles.Selectable, false);
        AccessibleRole = AccessibleRole.MenuBar;

        _useMnemonic = true;
        _itemViews = [];
        _keyFilter = new DropDownKeyFilter(this);

        Items = [];
        Items.Inserted += OnItemsInserted;
        Items.Removing += OnItemsRemoving;
        Items.Removed += OnItemsRemoved;
        Items.Cleared += OnItemsCleared;
        Items.Reordered += OnItemsReordered;

        StateCommon = new PaletteTripleRedirect(Redirector,
            PaletteBackStyle.ButtonLowProfile,
            PaletteBorderStyle.ButtonLowProfile,
            PaletteContentStyle.ButtonLowProfile,
            NeedPaintDelegate);
        StateDisabled = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StatePressed = new PaletteTriple(StateCommon, NeedPaintDelegate);

        _barBackInherit = new PaletteBackInheritMenuStrip(Redirector.Target);
        _barBack = new PaletteBack(_barBackInherit, NeedPaintDelegate)
        {
            ColorStyle = PaletteColorStyle.Linear,
            ColorAngle = 90f,
            ColorAlign = PaletteRectangleAlign.Local,
            ImageAlign = PaletteRectangleAlign.Local
        };
        _barBorderInherit = new PaletteBorderInheritRedirect(Redirector, PaletteBorderStyle.ControlClient)
        {
            OverrideBorderToFalse = true
        };
        _barBorder = new PaletteBorder(_barBorderInherit, NeedPaintDelegate);
        _itemMetric = new PaletteMetricRedirect(Redirector);

        _itemStack = new ViewLayoutStack(true)
        {
            FillLastChild = false
        };
        _drawDocker = new ViewDrawDocker(_barBack, _barBorder, null);
        _drawDocker.Add(_itemStack, ViewDockStyle.Fill);
        ViewManager = new ViewManager(this, _drawDocker);

        _dropDown = new KryptonContextMenu();

        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Dock = DockStyle.Top;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            CloseDropDown();
            SetDropDownFilter(false);
            UnhookAllItems();
            Items.Inserted -= OnItemsInserted;
            Items.Removing -= OnItemsRemoving;
            Items.Removed -= OnItemsRemoved;
            Items.Cleared -= OnItemsCleared;
            Items.Reordered -= OnItemsReordered;
            _dropDown.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the automatic resize of the control to fit contents.
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    [DefaultValue(AutoSizeMode.GrowAndShrink)]
    public override AutoSizeMode AutoSizeMode
    {
        get => base.AutoSizeMode;
        set => base.AutoSizeMode = value;
    }

    /// <summary>
    /// Gets or sets which control borders are docked to its parent and determines how a control is resized with its parent.
    /// </summary>
    [DefaultValue(DockStyle.Top)]
    public override DockStyle Dock
    {
        get => base.Dock;
        set => base.Dock = value;
    }

    /// <summary>
    /// Gets the collection of top-level menu items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Top-level menu items displayed on the bar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    public KryptonMenuBarItemCollection Items { get; }

    /// <summary>
    /// Gets and sets a value indicating if mnemonics are processed for top-level items.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Process mnemonic characters in top-level item text.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _useMnemonic;

        set
        {
            if (_useMnemonic != value)
            {
                _useMnemonic = value;
                RebuildItemViews();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to the common item appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled item appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal item appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the tracking item appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed item appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the menu bar background appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the menu bar background.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateBarBackground => _barBack;

    private bool ShouldSerializeStateBarBackground() => !_barBack.IsDefault;

    /// <summary>
    /// Inserts the standard File, Edit, Tools, and Help top-level items.
    /// </summary>
    public void InsertStandardItems()
    {
        foreach (KryptonContextMenuItem item in KryptonStandardMenuFactory.CreateStandardMenuBarItems())
        {
            Items.Add(item);
        }
    }

    #endregion

    #region Internal

    /// <summary>
    /// Called by <see cref="MenuBarItemController"/> when the mouse enters a top-level item.
    /// Switches the open drop-down while a menu is showing.
    /// </summary>
    /// <param name="controller">Controller for the item under the mouse.</param>
    internal void OnItemMouseEnter(MenuBarItemController controller)
    {
        if (!controller.Item.Enabled || !controller.Item.Visible)
        {
            return;
        }

        if (_openItem != null && !ReferenceEquals(_openItem, controller.Item))
        {
            OpenItem(controller.Item, false);
        }
        else if (_menuMode)
        {
            HighlightItem(controller.Item);
        }
    }

    /// <summary>
    /// Processes form-level command keys for shortcuts, Alt activation, and menu-mode navigation.
    /// </summary>
    /// <param name="msg">Windows message.</param>
    /// <param name="keyData">Key data.</param>
    /// <returns><c>true</c> if the key was handled; otherwise <c>false</c>.</returns>
    internal bool ProcessBarCmdKey(ref Message msg, Keys keyData)
    {
        if (!Enabled || !Visible)
        {
            return false;
        }

        if (Items.ProcessShortcut(keyData))
        {
            return true;
        }

        var keyCode = keyData & Keys.KeyCode;
        var mods = keyData & Keys.Modifiers;

        if ((keyCode == Keys.Menu && (mods == Keys.None || mods == Keys.Alt))
            || (keyCode == Keys.F10 && mods == Keys.None))
        {
            if (_menuMode || _openItem != null)
            {
                CloseDropDown();
                ExitMenuMode();
            }
            else
            {
                EnterMenuMode();
            }

            return true;
        }

        if (_openItem != null)
        {
            return false;
        }

        if (!_menuMode)
        {
            return false;
        }

        if (mods != Keys.None)
        {
            return false;
        }

        switch (keyCode)
        {
            case Keys.Left:
                MoveHighlight(-1);
                return true;
            case Keys.Right:
                MoveHighlight(1);
                return true;
            case Keys.Down:
            case Keys.Enter:
            case Keys.Space:
                if (_highlightedItem != null)
                {
                    OpenItem(_highlightedItem, true);
                    return true;
                }

                return false;
            case Keys.Escape:
                ExitMenuMode();
                return true;
            default:
                return false;
        }
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(150, 24);

    /// <inheritdoc />
    protected override void OnPaletteChanged(EventArgs e)
    {
        base.OnPaletteChanged(e);
        _barBackInherit.SetPalette(Redirector.Target);
    }

    /// <inheritdoc />
    protected override bool ProcessMnemonic(char charCode)
    {
        if (UseMnemonic && CanProcessMnemonic())
        {
            foreach (KryptonContextMenuItemBase item in Items)
            {
                if (item is KryptonContextMenuItem menuItem
                    && menuItem.Visible
                    && menuItem.Enabled
                    && IsMnemonic(charCode, menuItem.Text))
                {
                    OpenItem(menuItem, true);
                    return true;
                }
            }
        }

        return base.ProcessMnemonic(charCode);
    }

    #endregion

    #region Implementation

    private void OnItemClick(object? sender, MouseEventArgs e)
    {
        if (sender is MenuBarItemController controller)
        {
            OpenItem(controller.Item, false);
        }
    }

    private void OpenItem(KryptonContextMenuItem item, bool keyboardActivated)
    {
        if (!item.Enabled || !item.Visible)
        {
            return;
        }

        HighlightItem(item);
        _menuMode = true;

        if (item.Items.Count == 0)
        {
            item.PerformClick();
            ExitMenuMode();
            return;
        }

        if (ReferenceEquals(_openItem, item) && _dropDown.VisualContextMenu != null)
        {
            return;
        }

        CloseDropDown();
        SyncDropDownPalette();

        if (!_itemViews.TryGetValue(item, out var view))
        {
            return;
        }

        var screenRect = RectangleToScreen(view.ClientRectangle);
        screenRect.Height += 1;

        _dropDown.Closed += OnDropDownClosed;
        var shown = _dropDown.ShowCollection(this, screenRect,
            KryptonContextMenuPositionH.Left,
            KryptonContextMenuPositionV.Below,
            keyboardActivated, true, item.Items);

        if (shown)
        {
            _openItem = item;
            SetDropDownFilter(true);
        }
        else
        {
            _dropDown.Closed -= OnDropDownClosed;
        }

        PerformNeedPaint(false);
    }

    private void CloseDropDown()
    {
        if (_dropDown.VisualContextMenu != null)
        {
            _dropDown.Closed -= OnDropDownClosed;
            _dropDown.Close();
            ReleaseOpenItem();
        }

        SetDropDownFilter(false);
    }

    private void OnDropDownClosed(object? sender, ToolStripDropDownClosedEventArgs e)
    {
        _dropDown.Closed -= OnDropDownClosed;
        ReleaseOpenItem();
        SetDropDownFilter(false);

        if (!_menuMode)
        {
            _highlightedItem = null;
        }

        ApplyHighlightState();
        PerformNeedPaint(false);
    }

    private void ReleaseOpenItem()
    {
        if (_openItem != null && _itemViews.TryGetValue(_openItem, out var view))
        {
            view.MenuBarController?.RemoveFixed();
        }

        _openItem = null;
    }

    private void SyncDropDownPalette()
    {
        _dropDown.Enabled = Enabled;
        if (PaletteMode != PaletteMode.Custom)
        {
            _dropDown.PaletteMode = PaletteMode;
        }
        else
        {
            _dropDown.LocalCustomPalette = LocalCustomPalette;
        }
    }

    private void EnterMenuMode()
    {
        var first = GetFirstVisibleItem();
        if (first == null)
        {
            return;
        }

        _menuMode = true;
        HighlightItem(first);
    }

    private void ExitMenuMode()
    {
        _menuMode = false;
        _highlightedItem = null;
        ApplyHighlightState();
        PerformNeedPaint(false);
    }

    private void HighlightItem(KryptonContextMenuItem item)
    {
        _highlightedItem = item;
        ApplyHighlightState();
        PerformNeedPaint(false);
    }

    private void MoveHighlight(int delta)
    {
        var items = GetVisibleItems();
        if (items.Count == 0)
        {
            return;
        }

        var index = _highlightedItem == null ? 0 : items.IndexOf(_highlightedItem);
        if (index < 0)
        {
            index = 0;
        }

        index = (index + delta + items.Count) % items.Count;
        HighlightItem(items[index]);
    }

    private void OpenAdjacent(int delta)
    {
        var items = GetVisibleItems();
        if (items.Count == 0)
        {
            return;
        }

        var current = _openItem ?? _highlightedItem;
        var index = current == null ? 0 : items.IndexOf(current);
        if (index < 0)
        {
            index = 0;
        }

        index = (index + delta + items.Count) % items.Count;
        OpenItem(items[index], true);
    }

    private void ApplyHighlightState()
    {
        foreach (var pair in _itemViews)
        {
            if (ReferenceEquals(pair.Key, _openItem))
            {
                continue;
            }

            pair.Value.ElementState = ReferenceEquals(pair.Key, _highlightedItem)
                ? PaletteState.Tracking
                : PaletteState.Normal;
        }
    }

    private List<KryptonContextMenuItem> GetVisibleItems()
    {
        var list = new List<KryptonContextMenuItem>();
        foreach (KryptonContextMenuItemBase item in Items)
        {
            if (item is KryptonContextMenuItem menuItem && menuItem.Visible && menuItem.Enabled)
            {
                list.Add(menuItem);
            }
        }

        return list;
    }

    private KryptonContextMenuItem? GetFirstVisibleItem()
    {
        var items = GetVisibleItems();
        return items.Count > 0 ? items[0] : null;
    }

    private static int GetPopupDepth()
    {
        var manager = VisualPopupManager.Singleton;
        if (manager.CurrentPopup == null)
        {
            return 0;
        }

        return 1 + manager.StackedPopups.Length;
    }

    private bool ProcessDropDownKey(Keys key)
    {
        var depth = GetPopupDepth();
        if (depth <= 0)
        {
            return false;
        }

        switch (key)
        {
            case Keys.Left:
                if (depth == 1)
                {
                    OpenAdjacent(-1);
                    return true;
                }

                return false;
            case Keys.Right:
                if (depth == 1)
                {
                    OpenAdjacent(1);
                    return true;
                }

                return false;
            case Keys.Escape:
                if (depth == 1)
                {
                    var keep = _openItem;
                    CloseDropDown();
                    _menuMode = true;
                    if (keep != null)
                    {
                        HighlightItem(keep);
                    }

                    return true;
                }

                return false;
            case Keys.Menu:
                CloseDropDown();
                ExitMenuMode();
                return true;
            default:
                return false;
        }
    }

    private void SetDropDownFilter(bool enabled)
    {
        if (enabled == _filterActive)
        {
            return;
        }

        if (enabled)
        {
            Application.AddMessageFilter(_keyFilter);
        }
        else
        {
            Application.RemoveMessageFilter(_keyFilter);
        }

        _filterActive = enabled;
    }

    private void RebuildItemViews()
    {
        if (_rebuildSuspended)
        {
            return;
        }

        _itemStack.Clear();
        _itemViews.Clear();

        foreach (KryptonContextMenuItemBase item in Items)
        {
            switch (item)
            {
                case KryptonContextMenuItem menuItem:
                {
                    var view = new ViewDrawMenuBarItem(menuItem, StateDisabled, StateNormal,
                        StateTracking, StatePressed, _itemMetric, UseMnemonic);
                    var controller = new MenuBarItemController(this, menuItem, view, NeedPaintDelegate);
                    controller.Click += OnItemClick;
                    view.MouseController = controller;
                    view.KeyController = controller;
                    view.SourceController = controller;
                    _itemStack.Add(view);
                    _itemViews[menuItem] = view;
                    break;
                }
                case KryptonContextMenuSeparator separator:
                    _itemStack.Add(new ViewDrawMenuBarSeparator(separator, Redirector.Target));
                    break;
            }
        }

        ApplyHighlightState();
    }

    private void OnItemsInserted(object sender, TypedCollectionEventArgs<KryptonContextMenuItemBase> e)
    {
        if (e.Item != null)
        {
            e.Item.PropertyChanged += OnItemPropertyChanged;
        }

        RebuildItemViews();
        PerformNeedPaint(true);
    }

    private void OnItemsRemoving(object sender, TypedCollectionEventArgs<KryptonContextMenuItemBase> e)
    {
        if (e.Item != null)
        {
            e.Item.PropertyChanged -= OnItemPropertyChanged;
        }
    }

    private void OnItemsRemoved(object sender, TypedCollectionEventArgs<KryptonContextMenuItemBase> e)
    {
        if (ReferenceEquals(_openItem, e.Item))
        {
            CloseDropDown();
        }

        if (ReferenceEquals(_highlightedItem, e.Item))
        {
            _highlightedItem = null;
        }

        RebuildItemViews();
        PerformNeedPaint(true);
    }

    private void OnItemsCleared(object? sender, EventArgs e)
    {
        CloseDropDown();
        ExitMenuMode();
        RebuildItemViews();
        PerformNeedPaint(true);
    }

    private void OnItemsReordered(object? sender, EventArgs e)
    {
        RebuildItemViews();
        PerformNeedPaint(true);
    }

    private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        RebuildItemViews();
        PerformNeedPaint(true);
    }

    private void UnhookAllItems()
    {
        _rebuildSuspended = true;
        foreach (KryptonContextMenuItemBase item in Items)
        {
            item.PropertyChanged -= OnItemPropertyChanged;
        }

        _rebuildSuspended = false;
        _itemStack.Clear();
        _itemViews.Clear();
    }

    #endregion

    #region Nested Types

    private sealed class DropDownKeyFilter : IMessageFilter
    {
        private readonly KryptonMenuBar _owner;

        public DropDownKeyFilter(KryptonMenuBar owner) => _owner = owner;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg is not (PI.WM_.KEYDOWN or PI.WM_.SYSKEYDOWN))
            {
                return false;
            }

            var key = (Keys)(int)m.WParam.ToInt64();
            return _owner.ProcessDropDownKey(key);
        }
    }

    #endregion
}
