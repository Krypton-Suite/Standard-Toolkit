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
    private ViewDrawButton? _newTabButton;
    private bool _showNewTabButton;
    private KryptonPage? _draggingPage;
    private bool _externalDragging;
    private bool _eventsHooked;
    private ToolTipManager? _newTabToolTipManager;
    private VisualPopupToolTip? _newTabToolTipPopup;
    private Rectangle _spareCaptionRect;

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
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));
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
    /// Rebuilds tab buttons from the navigator pages collection.
    /// </summary>
    public void RebuildTabs()
    {
        ClearTabs();

        // ViewLayoutDocker lays out Left-docked children in reverse collection order
        // (last child becomes leftmost). Add the compact '+' first (rightmost), a small
        // gap, then pages from last to first so visual order is Pages[0]…Pages[n], gap, +.
        if (_showNewTabButton)
        {
            AddNewTabButton();
            Add(new ViewLayoutSeparator(4), ViewDockStyle.Left);
        }

        for (var i = _navigator.Pages.Count - 1; i >= 0; i--)
        {
            KryptonPage page = _navigator.Pages[i];
            if (!page.LastVisibleSet)
            {
                continue;
            }

            AddTab(page);
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
        UpdateCustomCaptionArea(context);
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
            return;
        }

        // FillRectangle is in window/view coordinates; CustomCaptionArea is client coordinates.
        Padding borders = form.RealWindowBorders;
        form.CustomCaptionArea = new Rectangle(
            spare.X - borders.Left,
            spare.Y - borders.Top,
            spare.Width,
            spare.Height);
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
        _navigator.SelectedPageChanged += OnSelectedPageChanged;
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
        _navigator.SelectedPageChanged -= OnSelectedPageChanged;
        _eventsHooked = false;
    }

    private void OnPagesChanged(object? sender, TypedCollectionEventArgs<KryptonPage> e) => RebuildTabs();

    private void OnPagesCleared(object? sender, EventArgs e) => RebuildTabs();

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

    private void AddTab(KryptonPage page)
    {
        var content = new FixedContentValue(page.Text, string.Empty, page.ImageSmall, Color.Empty);
        var button = new ViewDrawButton(
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
        _externalDragging = false;

        if (!_navigator.AllowPageDrag || _navigator.DragPageNotify == null || !page.AreFlagsSet(KryptonPageFlags.AllowPageDrag))
        {
            return;
        }

        var pages = new KryptonPageCollection
        {
            page
        };

        var dragArgs = new PageDragCancelEventArgs(e.Point, e.Offset, e.Control, pages);
        _navigator.DragPageNotify.PageDragStart(this, _navigator, dragArgs);
        _externalDragging = !dragArgs.Cancel;
    }

    private void OnTabDragMove(PointEventArgs e)
    {
        if (_externalDragging)
        {
            _navigator.DragPageNotify?.PageDragMove(this, e);
        }
    }

    private void OnTabDragEnd(PointEventArgs e)
    {
        if (_draggingPage == null)
        {
            return;
        }

        var dropped = false;
        if (_externalDragging)
        {
            dropped = _navigator.DragPageNotify?.PageDragEnd(this, e) ?? false;
            if (dropped && _navigator.Pages.Contains(_draggingPage))
            {
                _navigator.Pages.Remove(_draggingPage);
            }
        }

        if (!dropped)
        {
            ReorderWithinCaption(e.Point);
        }

        _draggingPage = null;
        _externalDragging = false;
    }

    private void OnTabDragQuit()
    {
        if (_externalDragging)
        {
            _navigator.DragPageNotify?.PageDragQuit(this);
        }

        _draggingPage = null;
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

        var targetButton = FindTargetButton(screenPoint, dragButton);
        if (targetButton == null || !_buttonToPage.TryGetValue(targetButton, out KryptonPage? targetPage))
        {
            return;
        }

        var ownerControl = dragButton.OwningControl;
        if (ownerControl == null)
        {
            return;
        }

        var clientPoint = ownerControl.PointToClient(screenPoint);
        var targetMid = targetButton.ClientRectangle.Left + (targetButton.ClientRectangle.Width / 2);
        var movingBefore = clientPoint.X < targetMid;

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

        if (insertIndex == sourceIndex)
        {
            return;
        }

        _navigator.Pages.Remove(_draggingPage);
        _navigator.Pages.Insert(insertIndex, _draggingPage);
        _navigator.SelectedPage = _draggingPage;
    }

    private ViewDrawButton? FindTargetButton(Point screenPoint, ViewDrawButton draggingButton)
    {
        foreach (KeyValuePair<KryptonPage, ViewDrawButton> pair in _pageToButton)
        {
            ViewDrawButton button = pair.Value;
            if (ReferenceEquals(button, draggingButton))
            {
                continue;
            }

            if (button.OwningControl == null)
            {
                continue;
            }

            Rectangle screenRect = button.OwningControl.RectangleToScreen(button.ClientRectangle);
            if (screenRect.Contains(screenPoint))
            {
                return button;
            }
        }

        return null;
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

    #endregion
}
