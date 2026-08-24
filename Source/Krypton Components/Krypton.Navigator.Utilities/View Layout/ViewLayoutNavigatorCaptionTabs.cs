#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Caption-hosted tab strip that mirrors a <see cref="KryptonNavigator"/> page collection
/// for injection into <see cref="KryptonForm"/> via <see cref="KryptonForm.InjectViewElement"/>.
/// </summary>
internal sealed class ViewLayoutNavigatorCaptionTabs : ViewLayoutDocker
{
    #region Instance Fields

    private readonly KryptonNavigator _navigator;
    private readonly NeedPaintHandler _needPaint;
    private readonly Action<KryptonPage, Point>? _showContextMenu;
    private readonly Action? _newTabClick;
    private readonly Dictionary<KryptonPage, ViewDrawButton> _pageToButton = new();
    private readonly Dictionary<ViewDrawButton, KryptonPage> _buttonToPage = new();
    private readonly Dictionary<ViewBase, NavigatorTabGroup> _headerToGroup = new();
    private ViewDrawButton? _newTabButton;
    private bool _showNewTabButton;
    private bool _allowTabGroups = true;
    private NavigatorTabGroupCollection? _tabGroups;
    private NavigatorTabGroupAppearance _tabGroupAppearance = new();
    private KryptonPage? _draggingPage;
    private KryptonPageCollection? _draggingPages;
    private bool _externalDragging;
    private bool _eventsHooked;
    private ToolTipManager? _newTabToolTipManager;
    private VisualPopupToolTip? _newTabToolTipPopup;
    private Rectangle _spareCaptionRect;
    private readonly List<Rectangle> _spareCaptionRects = new();
    private Action<IReadOnlyList<Rectangle>>? _spareCaptionAreasChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewLayoutNavigatorCaptionTabs"/> class.
    /// </summary>
    /// <param name="navigator">Navigator whose pages are mirrored.</param>
    /// <param name="needPaint">Paint/layout request for the hosting form chrome.</param>
    /// <param name="showContextMenu">Optional callback used to show a context menu for a caption tab.</param>
    /// <param name="newTabClick">Optional callback invoked when the caption new-tab button is clicked.</param>
    public ViewLayoutNavigatorCaptionTabs(KryptonNavigator navigator,
        NeedPaintHandler needPaint,
        Action<KryptonPage, Point>? showContextMenu = null,
        Action? newTabClick = null)
    {
        _navigator = navigator ?? ThrowHelper.ThrowArgumentNullException(navigator);
        _needPaint = needPaint ?? ThrowHelper.ThrowArgumentNullException(needPaint);
        _showContextMenu = showContextMenu;
        _newTabClick = newTabClick;

        Orientation = VisualOrientation.Top;
        PreferredSizeAll = true;

        HookEvents();
        RebuildTabs();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnhookEvents();
            ClearTabs();
            DisposeNewTabToolTips();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether a '+' button is shown to the right of the last caption tab.
    /// </summary>
    public bool ShowNewTabButton
    {
        get => _showNewTabButton;
        set
        {
            if (_showNewTabButton == value)
            {
                return;
            }

            _showNewTabButton = value;
            RebuildTabs();
        }
    }

    /// <summary>
    /// Gets or sets whether browser-style tab groups are rendered in the caption strip.
    /// </summary>
    public bool AllowTabGroups
    {
        get => _allowTabGroups;
        set
        {
            if (_allowTabGroups == value)
            {
                return;
            }

            _allowTabGroups = value;
            RebuildTabs();
        }
    }

    /// <summary>
    /// Gets or sets the group catalog used for headers, accents, and collapse.
    /// </summary>
    public NavigatorTabGroupCollection? TabGroups
    {
        get => _tabGroups;
        set
        {
            if (ReferenceEquals(_tabGroups, value))
            {
                return;
            }

            UnhookTabGroups();
            _tabGroups = value;
            HookTabGroups();
            RebuildTabs();
        }
    }

    /// <summary>
    /// Gets or sets wash / underline / border options for caption tab groups.
    /// </summary>
    public NavigatorTabGroupAppearance TabGroupAppearance
    {
        get => _tabGroupAppearance;
        set
        {
            value ??= new NavigatorTabGroupAppearance();
            if (ReferenceEquals(_tabGroupAppearance, value))
            {
                return;
            }

            _tabGroupAppearance = value;
            RebuildTabs();
        }
    }

    /// <summary>
    /// Optional callback when spare caption drag regions are recalculated (multi-strip support).
    /// </summary>
    public Action<IReadOnlyList<Rectangle>>? SpareCaptionAreasChanged
    {
        get => _spareCaptionAreasChanged;
        set => _spareCaptionAreasChanged = value;
    }

    /// <summary>
    /// Gets the navigator mirrored by this strip.
    /// </summary>
    public KryptonNavigator Navigator => _navigator;

    /// <summary>
    /// Rebuilds tab buttons from the navigator pages collection.
    /// </summary>
    public void RebuildTabs()
    {
        ClearTabs();

        // ViewLayoutDocker lays out Left-docked children in reverse collection order
        // (last child becomes leftmost). Build left-to-right visual items, then add reverse.
        var visualItems = new List<CaptionVisualItem>();

        string? currentGroupId = null;
        for (var i = 0; i < _navigator.Pages.Count; i++)
        {
            KryptonPage page = _navigator.Pages[i];
            if (!page.LastVisibleSet)
            {
                continue;
            }

            string groupId = page.TabGroupId ?? string.Empty;
            NavigatorTabGroup? group = null;
            if (_allowTabGroups && !string.IsNullOrEmpty(groupId) && _tabGroups != null)
            {
                group = _tabGroups[groupId];
            }

            if (group != null)
            {
                if (!string.Equals(currentGroupId, groupId, StringComparison.Ordinal))
                {
                    visualItems.Add(CaptionVisualItem.ForHeader(group));
                    currentGroupId = groupId;
                }

                if (!group.Collapsed)
                {
                    visualItems.Add(CaptionVisualItem.ForTab(page, group));
                }
            }
            else
            {
                currentGroupId = null;
                visualItems.Add(CaptionVisualItem.ForTab(page, null));
            }
        }

        if (_showNewTabButton)
        {
            visualItems.Add(CaptionVisualItem.ForNewTab());
        }

        for (var i = visualItems.Count - 1; i >= 0; i--)
        {
            CaptionVisualItem item = visualItems[i];
            switch (item.Kind)
            {
                case CaptionVisualKind.NewTab:
                    AddNewTabButton();
                    Add(new ViewLayoutSeparator(4), ViewDockStyle.Left);
                    break;
                case CaptionVisualKind.Header:
                    if (item.Group != null)
                    {
                        AddGroupHeader(item.Group);
                        Add(new ViewLayoutSeparator(2), ViewDockStyle.Left);
                    }
                    break;
                case CaptionVisualKind.Tab:
                    if (item.Page != null)
                    {
                        AddTab(item.Page, item.Group?.Color);
                    }
                    break;
            }
        }

        SyncCheckedState();
        _needPaint(this, new NeedLayoutEventArgs(true));
    }

    /// <summary>
    /// Updates the checked state to match <see cref="KryptonNavigator.SelectedPage"/>.
    /// </summary>
    public void SyncCheckedState()
    {
        var selected = _navigator.SelectedPage;
        foreach (var pair in _pageToButton)
        {
            pair.Value.Checked = ReferenceEquals(pair.Key, selected);
        }
    }

    #endregion

    #region Layout

    /// <inheritdoc />
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        SyncChromePadding(context);
        Size preferred = base.GetPreferredSize(context);
        // Prefer a compact chrome height; do not force the caption taller than the form heading.
        var minHeight = Math.Max(22, Math.Min(28, _navigator.Bar.BarMinimumHeight));
        preferred.Height = Math.Max(preferred.Height, minHeight);
        return preferred;
    }

    /// <inheritdoc />
    public override void Layout(ViewLayoutContext context)
    {
        SyncChromePadding(context);

        // Re-implement Left docking so tab/chrome buttons are vertically centered in the
        // caption (form button specs use ViewLayoutCenter). Stretching full height puts
        // tab top accents flush with the monitor edge when the form is maximized.
        Debug.Assert(context != null);
        ClientRectangle = context!.DisplayRectangle;
        Rectangle fillerRect = ClientRectangle;

        switch (Orientation)
        {
            case VisualOrientation.Top:
                fillerRect.X += Padding.Left;
                fillerRect.Y += Padding.Top;
                fillerRect.Width -= Padding.Horizontal;
                fillerRect.Height -= Padding.Vertical;
                break;
            case VisualOrientation.Bottom:
                fillerRect.X += Padding.Right;
                fillerRect.Y += Padding.Bottom;
                fillerRect.Width -= Padding.Horizontal;
                fillerRect.Height -= Padding.Vertical;
                break;
            case VisualOrientation.Left:
                fillerRect.X += Padding.Top;
                fillerRect.Y += Padding.Right;
                fillerRect.Width -= Padding.Vertical;
                fillerRect.Height -= Padding.Horizontal;
                break;
            case VisualOrientation.Right:
                fillerRect.X += Padding.Bottom;
                fillerRect.Y += Padding.Left;
                fillerRect.Width -= Padding.Vertical;
                fillerRect.Height -= Padding.Horizontal;
                break;
        }

        foreach (var child in Reverse().Where(child => child.Visible && GetDock(child) != ViewDockStyle.Fill))
        {
            context.DisplayRectangle = fillerRect;
            Size childSize = child.GetPreferredSize(context);

            switch (CalculateDock(OrientateDock(GetDock(child)), context.Control!))
            {
                case ViewDockStyle.Left:
                {
                    var height = Math.Min(childSize.Height, Math.Max(0, fillerRect.Height));
                    var y = fillerRect.Y + Math.Max(0, (fillerRect.Height - height) / 2);
                    context.DisplayRectangle = new Rectangle(fillerRect.X, y, childSize.Width, height);
                    fillerRect.Width -= childSize.Width;
                    fillerRect.X += childSize.Width;
                    break;
                }
                case ViewDockStyle.Right:
                {
                    var height = Math.Min(childSize.Height, Math.Max(0, fillerRect.Height));
                    var y = fillerRect.Y + Math.Max(0, (fillerRect.Height - height) / 2);
                    context.DisplayRectangle = new Rectangle(fillerRect.Right - childSize.Width, y, childSize.Width, height);
                    fillerRect.Width -= childSize.Width;
                    break;
                }
                case ViewDockStyle.Top:
                    context.DisplayRectangle = fillerRect with { Height = childSize.Height };
                    fillerRect.Height -= childSize.Height;
                    fillerRect.Y += childSize.Height;
                    break;
                case ViewDockStyle.Bottom:
                    context.DisplayRectangle = fillerRect with { Y = fillerRect.Bottom - childSize.Height, Height = childSize.Height };
                    fillerRect.Height -= childSize.Height;
                    break;
            }

            child.Layout(context);
        }

        foreach (ViewBase child in Reverse().Where(child => child.Visible && GetDock(child) == ViewDockStyle.Fill))
        {
            context.DisplayRectangle = fillerRect;
            child.Layout(context);
        }

        context.DisplayRectangle = ClientRectangle;
        _spareCaptionRect = fillerRect;
        _spareCaptionRects.Clear();
        if (fillerRect.Width > 8 && fillerRect.Height > 0)
        {
            _spareCaptionRects.Add(fillerRect);
        }

        UpdateCustomCaptionArea(context);
        _spareCaptionAreasChanged?.Invoke(_spareCaptionRects);
    }

    #endregion

    #region Implementation

    private void SyncChromePadding(ViewLayoutContext context)
    {
        Padding padding = Padding.Empty;

        if (context.Control is KryptonForm form)
        {
            // Match form control-box button metrics when the palette provides them.
            if (form.StateCommon?.Header != null)
            {
                padding = form.StateCommon.Header.GetMetricPadding(
                    form, PaletteState.Normal, PaletteMetricPadding.HeaderButtonPaddingForm);
            }

            // Maximized custom chrome clips the outer border via Region; without an inset the
            // tab strip sits flush against the monitor edges. Keep content inside the visible band.
            if (CommonHelper.IsFormMaximized(form) && form.MdiParent == null)
            {
                const int maximizedEdge = 8;
                padding = new Padding(
                    Math.Max(padding.Left, maximizedEdge),
                    Math.Max(padding.Top, maximizedEdge),
                    padding.Right,
                    Math.Max(padding.Bottom, 2));
            }
        }

        if (Padding != padding)
        {
            Padding = padding;
        }
    }

    private void UpdateCustomCaptionArea(ViewLayoutContext context)
    {
        if (context.Control is not KryptonForm form || form.IsDisposed)
        {
            return;
        }

        // Spare caption space to the right of the tab strip remains draggable (ribbon pattern).
        Rectangle spare = _spareCaptionRect;
        if (spare.Width <= 8 || spare.Height <= 0)
        {
            form.CustomCaptionArea = Rectangle.Empty;
            form.CustomCaptionAreas = Array.Empty<Rectangle>();
            return;
        }

        // FillRectangle is in window/view coordinates; CustomCaptionArea is client coordinates.
        Padding borders = form.RealWindowBorders;
        var clientSpare = new Rectangle(
            spare.X - borders.Left,
            spare.Y - borders.Top,
            spare.Width,
            spare.Height);
        form.CustomCaptionArea = clientSpare;
        form.CustomCaptionAreas = new[] { clientSpare };
    }

    private void HookEvents()
    {
        if (_eventsHooked)
        {
            return;
        }

        _navigator.Pages.Inserted += OnPagesChanged;
        _navigator.Pages.Removed += OnPagesChanged;
        _navigator.Pages.Cleared += OnPagesCleared;
        _navigator.Pages.Reordered += OnPagesReordered;
        _navigator.SelectedPageChanged += OnSelectedPageChanged;
        foreach (KryptonPage page in _navigator.Pages)
        {
            page.AppearancePropertyChanged += OnPageAppearanceChanged;
        }

        _eventsHooked = true;
    }

    private void UnhookEvents()
    {
        if (!_eventsHooked)
        {
            return;
        }

        _navigator.Pages.Inserted -= OnPagesChanged;
        _navigator.Pages.Removed -= OnPagesChanged;
        _navigator.Pages.Cleared -= OnPagesCleared;
        _navigator.Pages.Reordered -= OnPagesReordered;
        _navigator.SelectedPageChanged -= OnSelectedPageChanged;
        foreach (KryptonPage page in _navigator.Pages)
        {
            page.AppearancePropertyChanged -= OnPageAppearanceChanged;
        }

        UnhookTabGroups();
        _eventsHooked = false;
    }

    private void HookTabGroups()
    {
        if (_tabGroups == null)
        {
            return;
        }

        _tabGroups.Inserted += OnTabGroupsChanged;
        _tabGroups.Removed += OnTabGroupsChanged;
        _tabGroups.Cleared += OnTabGroupsCleared;
        foreach (NavigatorTabGroup group in _tabGroups)
        {
            group.PropertyChanged += OnTabGroupPropertyChanged;
        }
    }

    private void UnhookTabGroups()
    {
        if (_tabGroups == null)
        {
            return;
        }

        _tabGroups.Inserted -= OnTabGroupsChanged;
        _tabGroups.Removed -= OnTabGroupsChanged;
        _tabGroups.Cleared -= OnTabGroupsCleared;
        foreach (NavigatorTabGroup group in _tabGroups)
        {
            group.PropertyChanged -= OnTabGroupPropertyChanged;
        }
    }

    private void OnPagesChanged(object sender, TypedCollectionEventArgs<KryptonPage> e)
    {
        if (e.Item != null)
        {
            e.Item.AppearancePropertyChanged -= OnPageAppearanceChanged;
            if (_navigator.Pages.Contains(e.Item))
            {
                e.Item.AppearancePropertyChanged += OnPageAppearanceChanged;
            }
        }

        RebuildTabs();
    }

    private void OnPagesCleared(object? sender, EventArgs e) => RebuildTabs();

    private void OnPagesReordered(object? sender, EventArgs e) => RebuildTabs();

    private void OnPageAppearanceChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(KryptonPage.TabGroupId)
            or nameof(KryptonPage.Text)
            or nameof(KryptonPage.TextTitle)
            or nameof(KryptonPage.ImageSmall))
        {
            RebuildTabs();
        }
    }

    private void OnTabGroupsChanged(object sender, TypedCollectionEventArgs<NavigatorTabGroup> e)
    {
        if (e.Item != null)
        {
            e.Item.PropertyChanged -= OnTabGroupPropertyChanged;
            if (_tabGroups != null && _tabGroups.Contains(e.Item))
            {
                e.Item.PropertyChanged += OnTabGroupPropertyChanged;
            }
        }

        RebuildTabs();
    }

    private void OnTabGroupsCleared(object? sender, EventArgs e) => RebuildTabs();

    private void OnTabGroupPropertyChanged(object? sender, PropertyChangedEventArgs e) => RebuildTabs();

    private void OnSelectedPageChanged(object? sender, EventArgs e)
    {
        SyncCheckedState();
        _needPaint(this, new NeedLayoutEventArgs(false));
    }

    private void ClearTabs()
    {
        foreach (var button in _pageToButton.Values)
        {
            button.MouseController = null;
            button.KeyController = null;
            button.SourceController = null;
            button.Dispose();
        }

        _pageToButton.Clear();
        _buttonToPage.Clear();
        _headerToGroup.Clear();

        if (_newTabButton != null)
        {
            _newTabButton.MouseController = null;
            _newTabButton.KeyController = null;
            _newTabButton.SourceController = null;
            _newTabButton.Dispose();
            _newTabButton = null;
        }

        Clear();
    }

    private void AddGroupHeader(NavigatorTabGroup group)
    {
        var header = new ViewDrawTabGroupHeader(
            _navigator,
            group,
            _tabGroupAppearance,
            _needPaint,
            ToggleGroupCollapsed,
            ActivateGroup,
            g => NavigatorTabGroupDragHelper.CountGroupMembers(_navigator, g.Id),
            OnGroupDragStart,
            OnTabDragMove,
            OnGroupDragEnd,
            OnTabDragQuit);
        _headerToGroup[header] = group;
        Add(header, ViewDockStyle.Left);
    }

    private void ToggleGroupCollapsed(NavigatorTabGroup group) =>
        group.Collapsed = !group.Collapsed;

    private void ActivateGroup(NavigatorTabGroup group)
    {
        // Prefer the currently selected page when it belongs to the group; otherwise first member.
        KryptonPage? selected = _navigator.SelectedPage;
        if (selected != null &&
            string.Equals(selected.TabGroupId, group.Id, StringComparison.Ordinal) &&
            _navigator.AllowTabSelect)
        {
            return;
        }

        foreach (KryptonPage page in _navigator.Pages)
        {
            if (page.LastVisibleSet &&
                string.Equals(page.TabGroupId, group.Id, StringComparison.Ordinal))
            {
                if (_navigator.AllowTabSelect)
                {
                    _navigator.SelectedPage = page;
                }

                break;
            }
        }
    }

    private void AddNewTabButton()
    {
        // Short glyph only — long text would draw beside '+' and look like another wide tab.
        // Hover tooltip uses NavigatorFormIntegrationStrings.NewTab ("New tab").
        NavigatorFormIntegrationStrings strings = KryptonManager.Strings.NavigatorIntegrationStrings;
        var content = new FixedContentValue(strings.NewTabButton, string.Empty, null, Color.Empty);

        EnsureNewTabToolTips();

        // MiniButton palette reads as a compact chrome control, not another document tab.
        var button = new ViewDrawCaptionNewTabButton(
            _navigator.StateDisabled.MiniButton,
            _navigator.StateNormal.MiniButton,
            _navigator.StateTracking.MiniButton,
            _navigator.StatePressed.MiniButton,
            content,
            _needPaint,
            _newTabClick,
            _newTabToolTipManager);

        _newTabButton = button;
        Add(button, ViewDockStyle.Left);
    }

    private void EnsureNewTabToolTips()
    {
        if (_newTabToolTipManager != null)
        {
            return;
        }

        _newTabToolTipManager = new ToolTipManager(new ToolTipValues(null, () =>
            _navigator.IsHandleCreated ? _navigator.DeviceDpi / 96f : 1f));
        _newTabToolTipManager.ShowToolTip += OnShowNewTabToolTip;
        _newTabToolTipManager.CancelToolTip += OnCancelNewTabToolTip;
    }

    private void DisposeNewTabToolTips()
    {
        OnCancelNewTabToolTip(this, EventArgs.Empty);

        if (_newTabToolTipManager == null)
        {
            return;
        }

        _newTabToolTipManager.ShowToolTip -= OnShowNewTabToolTip;
        _newTabToolTipManager.CancelToolTip -= OnCancelNewTabToolTip;
        _newTabToolTipManager = null;
    }

    private void OnShowNewTabToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!ReferenceEquals(e.Target, _newTabButton) || _navigator.IsDisposed)
        {
            return;
        }

        Form? topForm = _navigator.FindForm();
        if (topForm is { ContainsFocus: false })
        {
            return;
        }

        if (_navigator.Site?.DesignMode == true)
        {
            return;
        }

        string tipText = KryptonManager.Strings.NavigatorIntegrationStrings.NewTab;
        if (string.IsNullOrWhiteSpace(tipText))
        {
            return;
        }

        OnCancelNewTabToolTip(this, EventArgs.Empty);

        var content = new FixedContentValue(tipText, string.Empty, null, Color.Empty);
        _newTabToolTipPopup = new VisualPopupToolTip(
            _navigator.Redirector,
            content,
            _navigator.Renderer,
            PaletteBackStyle.ControlToolTip,
            PaletteBorderStyle.ControlToolTip,
            CommonHelper.ContentStyleFromLabelStyle(LabelStyle.ToolTip),
            true);
        _newTabToolTipPopup.Disposed += OnNewTabToolTipPopupDisposed;
        _newTabToolTipPopup.ShowCalculatingSize(e.ControlMousePosition);
    }

    private void OnCancelNewTabToolTip(object? sender, EventArgs e) =>
        _newTabToolTipPopup?.Dispose();

    private void OnNewTabToolTipPopupDisposed(object? sender, EventArgs e)
    {
        if (sender is VisualPopupToolTip popup)
        {
            popup.Disposed -= OnNewTabToolTipPopupDisposed;
        }

        _newTabToolTipPopup = null;
    }

    private void AddTab(KryptonPage page, Color? groupColor = null)
    {
        var content = new FixedContentValue(page.Text, string.Empty, page.ImageSmall, Color.Empty);
        var button = groupColor is { IsEmpty: false } accent
            ? new ViewDrawCaptionGroupTab(
                page.StateDisabled.Tab,
                page.StateNormal.Tab,
                page.StateTracking.Tab,
                page.StatePressed.Tab,
                page.StateSelected.Tab,
                page.StateSelected.Tab,
                page.StateSelected.Tab,
                content,
                accent,
                _tabGroupAppearance)
            {
                Checked = ReferenceEquals(page, _navigator.SelectedPage)
            }
            : new ViewDrawButton(
                page.StateDisabled.Tab,
                page.StateNormal.Tab,
                page.StateTracking.Tab,
                page.StatePressed.Tab,
                page.StateSelected.Tab,
                page.StateSelected.Tab,
                page.StateSelected.Tab,
                null,
                content,
                VisualOrientation.Top,
                true)
            {
                Checked = ReferenceEquals(page, _navigator.SelectedPage)
            };

        var controller = new ButtonController(button, _needPaint)
        {
            AllowDragging = true
        };
        controller.Click += (_, _) =>
        {
            if (!ReferenceEquals(_navigator.SelectedPage, page))
            {
                _navigator.SelectedPage = page;
            }
        };
        controller.RightClick += (_, e) => OnRightClick(page, e);
        controller.DragStart += (_, e) => OnTabDragStart(page, e);
        controller.DragMove += (_, e) => OnTabDragMove(e);
        controller.DragEnd += (_, e) => OnTabDragEnd(e);
        controller.DragQuit += (_, _) => OnTabDragQuit();

        button.MouseController = controller;
        button.KeyController = controller;
        button.SourceController = controller;

        _pageToButton[page] = button;
        _buttonToPage[button] = page;
        Add(button, ViewDockStyle.Left);
    }

    private void OnRightClick(KryptonPage page, MouseEventArgs e)
    {
        if (!ReferenceEquals(_navigator.SelectedPage, page) && _navigator.AllowTabSelect)
        {
            _navigator.SelectedPage = page;
        }

        // Defer until after WM_NCRBUTTON* finishes; showing a menu during NC mouse
        // handling can still allow DefWndProc to open the system menu.
        Point screenPoint = Control.MousePosition;
        Control? owner = _pageToButton.TryGetValue(page, out ViewDrawButton? button)
            ? button.OwningControl
            : _navigator;

        if (owner is { IsHandleCreated: true })
        {
            owner.BeginInvoke((Action)(() => ShowPageContextMenu(page, screenPoint)));
        }
        else
        {
            ShowPageContextMenu(page, screenPoint);
        }
    }

    private void ShowPageContextMenu(KryptonPage page, Point screenPoint)
    {
        if (_showContextMenu != null)
        {
            _showContextMenu(page, screenPoint);
            return;
        }

        if (CommonHelper.ValidKryptonContextMenu(page.KryptonContextMenu))
        {
            page.KryptonContextMenu!.Show(_navigator, screenPoint);
            return;
        }

        if (CommonHelper.ValidContextMenuStrip(page.ContextMenuStrip))
        {
            page.ContextMenuStrip!.Show(screenPoint);
        }
    }

    private void OnTabDragStart(KryptonPage page, DragStartEventCancelArgs e)
    {
        _draggingPage = page;
        _draggingPages = new KryptonPageCollection();
        _externalDragging = false;

        if (!_navigator.AllowPageDrag || _navigator.DragPageNotify == null || !page.AreFlagsSet(KryptonPageFlags.AllowPageDrag))
        {
            return;
        }

        NavigatorTabGroupDragHelper.CollectDragPages(_navigator, page, _draggingPages, dragWholeGroup: true);

        var dragArgs = new PageDragCancelEventArgs(ControllerPointToScreen(e.Point), e.Offset, e.Control, _draggingPages);
        _navigator.DragPageNotify.PageDragStart(this, _navigator, dragArgs);
        _externalDragging = !dragArgs.Cancel;
    }

    private void OnGroupDragStart(NavigatorTabGroup group, DragStartEventCancelArgs e)
    {
        KryptonPage? seed = null;
        foreach (KryptonPage page in _navigator.Pages)
        {
            if (page.LastVisibleSet && string.Equals(page.TabGroupId, group.Id, StringComparison.Ordinal))
            {
                seed = page;
                break;
            }
        }

        if (seed == null)
        {
            e.Cancel = true;
            return;
        }

        OnTabDragStart(seed, e);
    }

    private void OnTabDragMove(PointEventArgs e)
    {
        if (_externalDragging)
        {
            _navigator.DragPageNotify?.PageDragMove(this, new PointEventArgs(ControllerPointToScreen(e.Point)));
        }
    }

    private void OnTabDragEnd(PointEventArgs e) => OnGroupDragEnd(e);

    private void OnGroupDragEnd(PointEventArgs e)
    {
        if (_draggingPage == null)
        {
            return;
        }

        var dropped = false;
        Point screenPoint = ControllerPointToScreen(e.Point);

        // Dropping back onto our own caption strip is a reorder / group join, so the tear-out
        // machinery must be cancelled instead of being allowed to spawn a new window.
        if (_externalDragging && IsOverCaptionStrip(screenPoint))
        {
            _navigator.DragPageNotify?.PageDragQuit(this);
            _externalDragging = false;
        }

        if (_externalDragging)
        {
            dropped = _navigator.DragPageNotify?.PageDragEnd(this, new PointEventArgs(screenPoint)) ?? false;
            if (dropped && _draggingPages != null)
            {
                for (var i = _draggingPages.Count - 1; i >= 0; i--)
                {
                    KryptonPage page = _draggingPages[i];
                    if (_navigator.Pages.Contains(page))
                    {
                        _navigator.Pages.Remove(page);
                    }
                }
            }
        }

        if (!dropped)
        {
            ReorderWithinCaption(screenPoint);
        }

        _draggingPage = null;
        _draggingPages = null;
        _externalDragging = false;
    }

    private void OnTabDragQuit()
    {
        if (_externalDragging)
        {
            _navigator.DragPageNotify?.PageDragQuit(this);
        }

        _draggingPage = null;
        _draggingPages = null;
        _externalDragging = false;
    }

    private void ReorderWithinCaption(Point screenPoint)
    {
        if (_draggingPage == null || !_navigator.AllowPageReorder || !_draggingPage.AreFlagsSet(KryptonPageFlags.AllowPageReorder))
        {
            return;
        }

        if (!_pageToButton.TryGetValue(_draggingPage, out ViewDrawButton? dragButton) || dragButton == null)
        {
            return;
        }

        Control? ownerControl = dragButton.OwningControl;
        if (ownerControl == null)
        {
            return;
        }

        Point captionPoint = ScreenToCaptionPoint(ownerControl, screenPoint);

        // Dropping on a group header joins that group; it is the only available gesture when the
        // group is collapsed and therefore shows no member tabs to drop onto.
        if (_draggingPages is not { Count: > 1 })
        {
            NavigatorTabGroup? headerGroup = FindTargetGroupHeader(captionPoint);
            if (headerGroup != null)
            {
                MovePageIntoGroup(_draggingPage, headerGroup);
                SelectDraggedPage();
                return;
            }
        }

        ViewDrawButton? targetButton = FindTargetButton(captionPoint, dragButton);
        if (targetButton == null || !_buttonToPage.TryGetValue(targetButton, out KryptonPage? targetPage))
        {
            return;
        }

        var targetMid = targetButton.ClientRectangle.Left + (targetButton.ClientRectangle.Width / 2);
        var movingBefore = captionPoint.X < targetMid;

        var sourceIndex = _navigator.Pages.IndexOf(_draggingPage);
        var targetIndex = _navigator.Pages.IndexOf(targetPage);
        if (sourceIndex < 0 || targetIndex < 0)
        {
            return;
        }

        var insertIndex = movingBefore ? targetIndex : targetIndex + 1;
        if (sourceIndex < insertIndex)
        {
            insertIndex--;
        }

        // Whole-group caption drag moves contiguous members as a block when possible.
        var moving = new List<KryptonPage>();
        if (_draggingPages != null && _draggingPages.Count > 1)
        {
            foreach (KryptonPage page in _draggingPages)
            {
                if (_navigator.Pages.Contains(page))
                {
                    moving.Add(page);
                }
            }
        }

        if (moving.Count <= 1)
        {
            if (insertIndex != sourceIndex)
            {
                _navigator.Pages.Remove(_draggingPage);
                insertIndex = Math.Max(0, Math.Min(insertIndex, _navigator.Pages.Count));
                _navigator.Pages.Insert(insertIndex, _draggingPage);
            }

            NavigatorTabGroupDragHelper.JoinPageToTargetGroup(_draggingPage, targetPage, _tabGroups);
        }
        else
        {
            // Remove high-to-low so earlier indices stay valid, then insert as a block.
            for (var i = moving.Count - 1; i >= 0; i--)
            {
                int removeIndex = _navigator.Pages.IndexOf(moving[i]);
                if (removeIndex < 0)
                {
                    continue;
                }

                if (removeIndex < insertIndex)
                {
                    insertIndex--;
                }

                _navigator.Pages.RemoveAt(removeIndex);
            }

            insertIndex = Math.Max(0, Math.Min(insertIndex, _navigator.Pages.Count));
            for (var i = 0; i < moving.Count; i++)
            {
                _navigator.Pages.Insert(insertIndex + i, moving[i]);
                NavigatorTabGroupDragHelper.JoinPageToTargetGroup(moving[i], targetPage, _tabGroups);
            }
        }

        SelectDraggedPage();
    }

    private void SelectDraggedPage()
    {
        if (_draggingPage != null && _navigator.AllowTabSelect)
        {
            _navigator.SelectedPage = _draggingPage;
        }
    }

    /// <summary>
    /// Moves a page next to the existing members of a group and applies the group membership.
    /// </summary>
    private void MovePageIntoGroup(KryptonPage page, NavigatorTabGroup group)
    {
        var lastMember = -1;
        for (var i = 0; i < _navigator.Pages.Count; i++)
        {
            if (!ReferenceEquals(_navigator.Pages[i], page)
                && string.Equals(_navigator.Pages[i].TabGroupId, group.Id, StringComparison.Ordinal))
            {
                lastMember = i;
            }
        }

        var sourceIndex = _navigator.Pages.IndexOf(page);
        if (lastMember >= 0 && sourceIndex >= 0)
        {
            var insertIndex = lastMember + 1;
            if (sourceIndex < insertIndex)
            {
                insertIndex--;
            }

            if (insertIndex != sourceIndex)
            {
                _navigator.Pages.Remove(page);
                _navigator.Pages.Insert(Math.Max(0, Math.Min(insertIndex, _navigator.Pages.Count)), page);
            }
        }

        page.TabGroupId = group.Id;
        NavigatorTabGroupBarAccent.Apply(page, group, _tabGroupAppearance);
    }

    /// <summary>
    /// Converts a drag point reported by a view controller into a true screen point.
    /// </summary>
    private Point ControllerPointToScreen(Point controllerPoint)
    {
        // Caption views live in the non-client area, so the controller converted window
        // coordinates as if they were client coordinates; undo the resulting border offset.
        if (OwningControl is KryptonForm form)
        {
            Padding borders = form.RealWindowBorders;
            controllerPoint.Offset(-borders.Left, -borders.Top);
        }

        return controllerPoint;
    }

    /// <summary>
    /// Converts a screen point into the window coordinates used by the caption view rectangles.
    /// </summary>
    private Point ScreenToCaptionPoint(Control owner, Point screenPoint)
    {
        Point point = owner.PointToClient(screenPoint);

        // Injected caption views live in the non-client area, so their rectangles are relative to
        // the window rather than the client area.
        if (owner is KryptonForm form)
        {
            Padding borders = form.RealWindowBorders;
            point.Offset(borders.Left, borders.Top);
        }

        return point;
    }

    private bool IsOverCaptionStrip(Point screenPoint)
    {
        Control? owner = OwningControl;

        return owner != null && ClientRectangle.Contains(ScreenToCaptionPoint(owner, screenPoint));
    }

    private NavigatorTabGroup? FindTargetGroupHeader(Point captionPoint)
    {
        foreach (KeyValuePair<ViewBase, NavigatorTabGroup> pair in _headerToGroup)
        {
            if (pair.Key.ClientRectangle.Contains(captionPoint))
            {
                return pair.Value;
            }
        }

        return null;
    }

    private ViewDrawButton? FindTargetButton(Point captionPoint, ViewDrawButton draggingButton)
    {
        foreach (KeyValuePair<KryptonPage, ViewDrawButton> pair in _pageToButton)
        {
            ViewDrawButton button = pair.Value;
            if (ReferenceEquals(button, draggingButton))
            {
                continue;
            }

            if (button.ClientRectangle.Contains(captionPoint))
            {
                return button;
            }
        }

        return null;
    }

    private enum CaptionVisualKind
    {
        Tab,
        Header,
        NewTab
    }

    private readonly struct CaptionVisualItem
    {
        public CaptionVisualKind Kind { get; }
        public KryptonPage? Page { get; }
        public NavigatorTabGroup? Group { get; }

        private CaptionVisualItem(CaptionVisualKind kind, KryptonPage? page, NavigatorTabGroup? group)
        {
            Kind = kind;
            Page = page;
            Group = group;
        }

        public static CaptionVisualItem ForTab(KryptonPage page, NavigatorTabGroup? group) =>
            new CaptionVisualItem(CaptionVisualKind.Tab, page, group);

        public static CaptionVisualItem ForHeader(NavigatorTabGroup group) =>
            new CaptionVisualItem(CaptionVisualKind.Header, null, group);

        public static CaptionVisualItem ForNewTab() =>
            new CaptionVisualItem(CaptionVisualKind.NewTab, null, null);
    }

    #endregion

    #region Nested Types

    /// <summary>
    /// Compact caption '+' control sized like a chrome button rather than a document tab.
    /// </summary>
    private sealed class ViewDrawCaptionNewTabButton : ViewDrawButton
    {
        public ViewDrawCaptionNewTabButton(IPaletteTriple paletteDisabled,
            IPaletteTriple paletteNormal,
            IPaletteTriple paletteTracking,
            IPaletteTriple palettePressed,
            IContentValues content,
            NeedPaintHandler needPaint,
            Action? newTabClick,
            ToolTipManager? toolTipManager)
            : base(paletteDisabled, paletteNormal, paletteTracking, palettePressed,
                null, content, VisualOrientation.Top, false)
        {
            Checked = false;

            var controller = new ButtonController(this, needPaint)
            {
                AllowDragging = false
            };
            controller.Click += (_, _) => newTabClick?.Invoke();

            IMouseController mouseController = controller;
            if (toolTipManager != null)
            {
                mouseController = new ToolTipController(toolTipManager, this, controller);
            }

            MouseController = mouseController;
            KeyController = controller;
            SourceController = controller;
        }

        /// <inheritdoc />
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size preferred = base.GetPreferredSize(context);
            // Keep caption height; clamp width to a near-square chrome control.
            var side = Math.Max(22, preferred.Height);
            preferred.Width = Math.Max(side, Math.Min(preferred.Width, side + 6));
            preferred.Height = side;
            return preferred;
        }
    }

    /// <summary>
    /// Caption tab that belongs to a group. Draws a solid group-color underline that stays
    /// visible in every state (including selected), so grouped members read as one colored
    /// cluster joined to the washed group header.
    /// </summary>
    private sealed class ViewDrawCaptionGroupTab : ViewDrawButton
    {
        private readonly Color _accentColor;
        private readonly NavigatorTabGroupAppearance _appearance;

        public ViewDrawCaptionGroupTab(
            IPaletteTriple paletteDisabled,
            IPaletteTriple paletteNormal,
            IPaletteTriple paletteTracking,
            IPaletteTriple palettePressed,
            IPaletteTriple paletteCheckedNormal,
            IPaletteTriple paletteCheckedTracking,
            IPaletteTriple paletteCheckedPressed,
            IContentValues content,
            Color accentColor,
            NavigatorTabGroupAppearance appearance)
            : base(paletteDisabled, paletteNormal, paletteTracking, palettePressed,
                paletteCheckedNormal, paletteCheckedTracking, paletteCheckedPressed,
                null, content, VisualOrientation.Top, true)
        {
            _accentColor = accentColor;
            _appearance = appearance ?? ThrowHelper.ThrowArgumentNullException(appearance);
        }

        public override void Render(RenderContext context)
        {
            base.Render(context);

            if (_accentColor.IsEmpty ||
                !_appearance.ShowMemberUnderline ||
                _appearance.MemberUnderlineHeight <= 0 ||
                ClientRectangle.Width <= 0 ||
                ClientRectangle.Height <= 0)
            {
                return;
            }

            int height = Math.Min(_appearance.MemberUnderlineHeight, ClientRectangle.Height);
            var accent = new Rectangle(ClientRectangle.X, ClientRectangle.Bottom - height,
                ClientRectangle.Width, height);
            using var brush = new SolidBrush(_accentColor);
            context.Graphics.FillRectangle(brush, accent);
        }
    }

    #endregion
}
