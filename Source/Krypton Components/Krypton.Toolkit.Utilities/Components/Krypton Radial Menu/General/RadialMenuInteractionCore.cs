#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shared navigation, hit-testing, and activation logic for radial popup and hosted control hosts.
/// </summary>
internal sealed class RadialMenuInteractionCore
{
    #region Constants

    internal const int EditorPageSize = RadialMenuMetrics.EditorPageSize;

    #endregion

    #region Instance Fields

    private readonly IRadialMenuInteractionHost _host;
    private readonly Stack<KryptonRadialMenuItemCollection> _navigation = new Stack<KryptonRadialMenuItemCollection>();
    private KryptonRadialMenuItemCollection _currentItems;
    private KryptonRadialMenuItemBase? _activeEditor;
    private RadialSectorInfo[] _sectors = Array.Empty<RadialSectorInfo>();
    private List<KryptonRadialMenuItemBase> _visibleItems = [];
    private List<KryptonRadialMenuItemBase> _allVisibleItems = [];
    private int _trackingIndex = -1;
    private int _pressedIndex = -1;
    private bool _trackingOuterRing;
    private bool _pressedOuterRing;
    private int _trackingEditorIndex = -1;
    private int _pageOffset;
    private bool _draggingSlider;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="RadialMenuInteractionCore"/> class.
    /// </summary>
    /// <param name="host">Host surface.</param>
    public RadialMenuInteractionCore(IRadialMenuInteractionHost host)
    {
        _host = host ?? ThrowHelper.ThrowArgumentNullException(host);
        _currentItems = host.RootItems;
    }

    #endregion

    #region Public

    /// <summary>Gets the visible sector items.</summary>
    public IReadOnlyList<KryptonRadialMenuItemBase> VisibleItems => _visibleItems;

    /// <summary>Gets the built sector descriptors.</summary>
    public RadialSectorInfo[] Sectors => _sectors;

    /// <summary>Gets the tracking sector index.</summary>
    public int TrackingIndex => _trackingIndex;

    /// <summary>Gets whether tracking is on the outer ring.</summary>
    public bool TrackingOuterRing => _trackingOuterRing;

    /// <summary>Gets the pressed sector index.</summary>
    public int PressedIndex => _pressedIndex;

    /// <summary>Gets whether press is on the outer ring.</summary>
    public bool PressedOuterRing => _pressedOuterRing;

    /// <summary>Gets the tracking editor index.</summary>
    public int TrackingEditorIndex => _trackingEditorIndex;

    /// <summary>Gets the active editor item, if any.</summary>
    public KryptonRadialMenuItemBase? ActiveEditor => _activeEditor;

    /// <summary>Gets whether the navigation stack can go back or an editor is open.</summary>
    public bool CanGoBack => _navigation.Count > 0 || _activeEditor != null;

    /// <summary>Gets whether a slider drag is in progress.</summary>
    public bool DraggingSlider => _draggingSlider;

    /// <summary>Gets whether an editor ring is active.</summary>
    public bool EditorMode => _activeEditor != null;

    /// <summary>
    /// Resets navigation to the host root items.
    /// </summary>
    public void ResetToRoot()
    {
        _navigation.Clear();
        _currentItems = _host.RootItems;
        _activeEditor = null;
        _trackingIndex = -1;
        _pressedIndex = -1;
        _trackingOuterRing = false;
        _pressedOuterRing = false;
        _trackingEditorIndex = -1;
        _pageOffset = 0;
        _draggingSlider = false;
        RebuildLayout(playNavigateAnimation: false);
    }

    /// <summary>
    /// Rebuilds layout for the current level without resetting navigation.
    /// </summary>
    public void RefreshLayout() => RebuildLayout(playNavigateAnimation: false);

    /// <summary>
    /// Paints the current radial surface.
    /// </summary>
    /// <param name="g">Graphics.</param>
    /// <param name="bounds">Client paint bounds.</param>
    public void Paint(Graphics g, Rectangle bounds)
    {
        var colors = RadialMenuColorSet.FromPalette(_host.ResolvePalette(), _host.Values);
        RadialMenuPainter.Paint(
            g,
            bounds,
            _host.Values,
            colors,
            _visibleItems,
            _sectors,
            _trackingIndex,
            _trackingOuterRing,
            _pressedIndex,
            _pressedOuterRing,
            CanGoBack,
            EditorMode,
            _activeEditor,
            _trackingEditorIndex,
            _host.Appearance,
            _host.Metrics);
    }

    /// <summary>
    /// Hit-tests a client point.
    /// </summary>
    /// <param name="clientPoint">Client point.</param>
    /// <returns>Hit result.</returns>
    public RadialHitResult HitTest(Point clientPoint)
    {
        var center = new PointF(_host.ClientSize.Width / 2f, _host.ClientSize.Height / 2f);
        var metrics = _host.Metrics;
        var startAngle = _host.Values.StartAngle;
        var hitPadding = metrics.HitPadding;
        var editorCount = GetEditorSectorCount(_activeEditor);
        if (_activeEditor is KryptonRadialMenuSliderItem)
        {
            var dx = clientPoint.X - center.X;
            var dy = clientPoint.Y - center.Y;
            var distance = Math.Sqrt((dx * dx) + (dy * dy));
            var inner = Math.Max(0f, metrics.InnerRadius - hitPadding);
            var outer = metrics.MenuRadius + hitPadding;
            if (distance <= inner)
            {
                return new RadialHitResult(RadialHitKind.Center, -1, -1);
            }

            if (distance <= outer)
            {
                return new RadialHitResult(RadialHitKind.Editor, -1, 0);
            }

            return RadialHitResult.None;
        }

        return RadialLayoutEngine.HitTest(
            clientPoint,
            center,
            metrics.MenuRadius,
            metrics.InnerRadius,
            _sectors,
            _activeEditor != null,
            editorCount,
            startAngle,
            hitPadding,
            metrics.OuterRingThickness);
    }

    /// <summary>
    /// Updates tracking from a mouse move.
    /// </summary>
    /// <param name="clientPoint">Client point.</param>
    /// <param name="suppressToolTips">When true, cancels tooltips.</param>
    /// <returns>The current hit result.</returns>
    public RadialHitResult UpdateTrackingFromMove(Point clientPoint, bool suppressToolTips)
    {
        if (_draggingSlider && _activeEditor is KryptonRadialMenuSliderItem slider)
        {
            var center = new PointF(_host.ClientSize.Width / 2f, _host.ClientSize.Height / 2f);
            slider.SetNormalizedValue(RadialLayoutEngine.AngleToNormalized(clientPoint, center, _host.Values.StartAngle));
            _host.InvalidateSurface();
            return new RadialHitResult(RadialHitKind.Editor, -1, 0);
        }

        var hit = HitTest(clientPoint);
        if (suppressToolTips)
        {
            _host.ToolTipHost?.Cancel();
        }
        else
        {
            UpdateToolTipHover(hit, moving: false);
        }

        var changed = false;
        if (_activeEditor != null)
        {
            if (_trackingEditorIndex != hit.EditorIndex)
            {
                _trackingEditorIndex = hit.Kind == RadialHitKind.Editor ? hit.EditorIndex : -1;
                changed = true;
            }
        }
        else
        {
            var nextIndex = hit.Kind is RadialHitKind.Sector or RadialHitKind.OuterRing ? hit.SectorIndex : -1;
            var nextOuter = hit.Kind == RadialHitKind.OuterRing;
            if (_trackingIndex != nextIndex || _trackingOuterRing != nextOuter)
            {
                _trackingIndex = nextIndex;
                _trackingOuterRing = nextOuter;
                changed = true;
                UpdateAccessibleName();
            }
        }

        if (changed)
        {
            _host.InvalidateSurface();
        }

        return hit;
    }

    /// <summary>
    /// Handles mouse-down press state / slider drag start.
    /// </summary>
    /// <param name="clientPoint">Client point.</param>
    /// <returns>Hit result.</returns>
    public RadialHitResult BeginPress(Point clientPoint)
    {
        var hit = HitTest(clientPoint);
        if (_activeEditor is KryptonRadialMenuSliderItem item && hit.Kind != RadialHitKind.Center)
        {
            _draggingSlider = true;
            var center = new PointF(_host.ClientSize.Width / 2f, _host.ClientSize.Height / 2f);
            item.SetNormalizedValue(
                RadialLayoutEngine.AngleToNormalized(clientPoint, center, _host.Values.StartAngle));
            _host.InvalidateSurface();
            return hit;
        }

        if (hit.Kind == RadialHitKind.Sector || hit.Kind == RadialHitKind.OuterRing)
        {
            _pressedIndex = hit.SectorIndex;
            _pressedOuterRing = hit.Kind == RadialHitKind.OuterRing;
            _host.InvalidateSurface();
        }

        return hit;
    }

    /// <summary>
    /// Ends a slider drag without activating.
    /// </summary>
    public void EndSliderDrag()
    {
        if (!_draggingSlider)
        {
            return;
        }

        _draggingSlider = false;
        _host.InvalidateSurface();
    }

    /// <summary>
    /// Clears pressed state and activates the hit under the pointer.
    /// </summary>
    /// <param name="clientPoint">Client point.</param>
    public void CompleteClick(Point clientPoint)
    {
        if (_draggingSlider)
        {
            EndSliderDrag();
            return;
        }

        var hit = HitTest(clientPoint);
        _pressedIndex = -1;
        _pressedOuterRing = false;

        switch (hit.Kind)
        {
            case RadialHitKind.Center:
                HandleCenterClick();
                break;
            case RadialHitKind.OuterRing:
                OpenChildOrEditor(hit.SectorIndex);
                break;
            case RadialHitKind.Sector:
                HandleSectorBodyClick(hit.SectorIndex);
                break;
            case RadialHitKind.Editor:
                HandleEditorClick(hit.EditorIndex);
                break;
        }

        _host.InvalidateSurface();
    }

    /// <summary>
    /// Clears tracking when the pointer leaves the surface.
    /// </summary>
    public void ClearTracking()
    {
        if (_trackingIndex != -1 || _trackingOuterRing || _trackingEditorIndex != -1)
        {
            _trackingIndex = -1;
            _trackingOuterRing = false;
            _trackingEditorIndex = -1;
            _host.InvalidateSurface();
        }

        _host.ToolTipHost?.Cancel();
    }

    /// <summary>
    /// Handles mouse-wheel scrolling for editors or paging.
    /// </summary>
    /// <param name="delta">Wheel delta.</param>
    public void HandleMouseWheel(int delta)
    {
        if (_activeEditor is KryptonRadialMenuFontListItem fonts)
        {
            fonts.ScrollOffset += delta > 0 ? -1 : 1;
            _host.InvalidateSurface();
            return;
        }

        if (_activeEditor is KryptonRadialMenuSliderItem slider)
        {
            slider.Value += delta > 0 ? slider.SmallChange : -slider.SmallChange;
            _host.InvalidateSurface();
            return;
        }

        if (_activeEditor is KryptonRadialMenuCalendarItem calendar)
        {
            calendar.ShiftMonth(delta > 0 ? -1 : 1);
            _host.InvalidateSurface();
            return;
        }

        if (_activeEditor != null)
        {
            return;
        }

        var maxVisible = _host.Values.MaxVisibleItems;
        if (maxVisible <= 0 || _allVisibleItems.Count <= maxVisible)
        {
            return;
        }

        var maxOffset = _allVisibleItems.Count - maxVisible;
        var next = _pageOffset + (delta > 0 ? -1 : 1);
        next = Math.Max(0, Math.Min(maxOffset, next));
        if (next == _pageOffset)
        {
            return;
        }

        _pageOffset = next;
        RebuildLayout(playNavigateAnimation: false);
        _host.InvalidateSurface();
    }

    /// <summary>
    /// Appends a character to an active text editor.
    /// </summary>
    /// <param name="keyChar">Character.</param>
    /// <returns>True when handled.</returns>
    public bool HandleKeyPress(char keyChar)
    {
        if (_activeEditor is not KryptonRadialMenuTextItem textItem || char.IsControl(keyChar))
        {
            return false;
        }

        textItem.DraftText += keyChar;
        _host.InvalidateSurface();
        return true;
    }

    /// <summary>
    /// Processes dialog / navigation keys.
    /// </summary>
    /// <param name="keyData">Key data.</param>
    /// <param name="allowRootEscapeDismiss">
    /// When true, Escape at root returns false so the host can dismiss (popup).
    /// When false, Escape at root is swallowed (hosted control stays visible).
    /// </param>
    /// <returns>True when handled.</returns>
    public bool TryProcessKeyboard(Keys keyData, bool allowRootEscapeDismiss)
    {
        switch (keyData)
        {
            case Keys.Escape:
                if (_activeEditor is KryptonRadialMenuTextItem textEsc)
                {
                    textEsc.CancelEdit();
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                    _host.InvalidateSurface();
                    return true;
                }

                if (_activeEditor != null || _navigation.Count > 0)
                {
                    HandleCenterClick();
                    return true;
                }

                return !allowRootEscapeDismiss;

            case Keys.Back:
                if (_activeEditor is KryptonRadialMenuTextItem textBack)
                {
                    if (textBack.DraftText.Length > 0)
                    {
                        textBack.DraftText = textBack.DraftText.Substring(0, textBack.DraftText.Length - 1);
                        _host.InvalidateSurface();
                    }

                    return true;
                }

                HandleCenterClick();
                return true;

            case Keys.Enter:
            case Keys.Space:
                ActivateKeyboardSelection();
                return true;

            case Keys.Left:
            case Keys.Up:
                MoveTracking(-1);
                return true;

            case Keys.Right:
            case Keys.Down:
                MoveTracking(1);
                return true;

            case Keys.Home:
                if (_activeEditor == null && _visibleItems.Count > 0)
                {
                    _trackingIndex = 0;
                    UpdateAccessibleName();
                    _host.InvalidateSurface();
                }

                return true;

            case Keys.End:
                if (_activeEditor == null && _visibleItems.Count > 0)
                {
                    _trackingIndex = _visibleItems.Count - 1;
                    UpdateAccessibleName();
                    _host.InvalidateSurface();
                }

                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// Performs the centre-button action (editor cancel, navigate back, or root raise).
    /// </summary>
    public void HandleCenterClick()
    {
        _host.ToolTipHost?.Cancel();
        if (_activeEditor != null)
        {
            if (_activeEditor is KryptonRadialMenuTextItem textItem)
            {
                textItem.CancelEdit();
            }

            _activeEditor = null;
            _draggingSlider = false;
            _trackingEditorIndex = -1;
            _host.InvalidateSurface();
            return;
        }

        if (_navigation.Count > 0)
        {
            _currentItems = _navigation.Pop();
            _pageOffset = 0;
            RebuildLayout(playNavigateAnimation: true);
            _host.InvalidateSurface();
            return;
        }

        _host.RaiseCenterButtonClick();
        if (_host.SupportsAutoClose)
        {
            _host.RequestClose(ToolStripDropDownCloseReason.CloseCalled);
        }
    }

    /// <summary>
    /// Builds a display name for accessibility.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <returns>Accessible name.</returns>
    public static string GetItemAccessibleName(KryptonRadialMenuItemBase item)
    {
        var name = item switch
        {
            KryptonRadialMenuItem command => string.IsNullOrEmpty(command.ResolveText) ? command.ToString() : command.ResolveText,
            KryptonRadialMenuSliderItem slider => string.IsNullOrEmpty(slider.Text) ? slider.ToString() : slider.Text,
            KryptonRadialMenuColorPaletteItem colors => string.IsNullOrEmpty(colors.Text) ? colors.ToString() : colors.Text,
            KryptonRadialMenuFontListItem fonts => string.IsNullOrEmpty(fonts.Text) ? fonts.ToString() : fonts.Text,
            KryptonRadialMenuTextItem textItem => string.IsNullOrEmpty(textItem.Label) ? textItem.ToString() : textItem.Label,
            KryptonRadialMenuCalendarItem calendar => string.IsNullOrEmpty(calendar.Text) ? calendar.ToString() : calendar.Text,
            _ => item.ToString()
        };

        return string.IsNullOrEmpty(name) ? @"Radial menu item" : name!;
    }

    #endregion

    #region Implementation

    private void RebuildLayout(bool playNavigateAnimation)
    {
        List<KryptonRadialMenuItemBase> list = [];
        foreach (var item in _currentItems.GetVisibleItems())
        {
            list.Add(item);
        }

        _allVisibleItems = list;
        if (_host.IsRightToLeft)
        {
            _allVisibleItems.Reverse();
        }

        var maxVisible = _host.Values.MaxVisibleItems;
        if (maxVisible > 0 && _allVisibleItems.Count > maxVisible)
        {
            var maxOffset = Math.Max(0, _allVisibleItems.Count - maxVisible);
            if (_pageOffset > maxOffset)
            {
                _pageOffset = maxOffset;
            }

            if (_pageOffset < 0)
            {
                _pageOffset = 0;
            }

            List<KryptonRadialMenuItemBase> page = [];
            foreach (var @base in _allVisibleItems.Skip(_pageOffset).Take(maxVisible))
            {
                page.Add(@base);
            }

            _visibleItems = page;
        }
        else
        {
            _pageOffset = 0;
            _visibleItems = _allVisibleItems;
        }

        _sectors = RadialLayoutEngine.BuildSectors(
            _visibleItems.Count,
            _host.EffectiveMenuRadius,
            _host.EffectiveInnerRadius,
            _host.Values.StartAngle);
        _trackingIndex = -1;
        _pressedIndex = -1;
        _trackingOuterRing = false;
        _pressedOuterRing = false;
        _trackingEditorIndex = -1;
        _host.ToolTipHost?.Cancel();
        UpdateAccessibleName();

        if (playNavigateAnimation)
        {
            _host.OnNavigated();
        }
    }

    private void UpdateAccessibleName()
    {
        if (_trackingIndex >= 0 && _trackingIndex < _visibleItems.Count)
        {
            _host.SetAccessibleName(GetItemAccessibleName(_visibleItems[_trackingIndex]));
            return;
        }

        _host.SetAccessibleName(@"Radial menu");
    }

    private void UpdateToolTipHover(RadialHitResult hit, bool moving)
    {
        var toolTips = _host.ToolTipHost;
        if (toolTips == null || moving || _draggingSlider || _activeEditor != null)
        {
            toolTips?.Cancel();
            return;
        }

        if ((hit.Kind is RadialHitKind.Sector or RadialHitKind.OuterRing)
            && hit.SectorIndex >= 0
            && hit.SectorIndex < _visibleItems.Count)
        {
            toolTips.UpdateHover(_visibleItems[hit.SectorIndex]);
            return;
        }

        toolTips.Cancel();
    }

    private void MoveTracking(int delta)
    {
        if (_activeEditor != null)
        {
            MoveEditorTracking(delta);
            return;
        }

        if (_visibleItems.Count == 0)
        {
            return;
        }

        if (_trackingIndex < 0)
        {
            _trackingIndex = delta > 0 ? 0 : _visibleItems.Count - 1;
        }
        else
        {
            _trackingIndex = (_trackingIndex + delta + _visibleItems.Count) % _visibleItems.Count;
        }

        UpdateToolTipHover(new RadialHitResult(RadialHitKind.Sector, _trackingIndex, -1), moving: false);
        UpdateAccessibleName();
        _host.InvalidateSurface();
    }

    private void MoveEditorTracking(int delta)
    {
        if (_activeEditor is KryptonRadialMenuCalendarItem calendar)
        {
            MoveCalendarTracking(calendar, delta);
            return;
        }

        if (_activeEditor is KryptonRadialMenuSliderItem slider)
        {
            slider.Value += delta > 0 ? slider.SmallChange : -slider.SmallChange;
            _host.InvalidateSurface();
            return;
        }

        var count = GetEditorSectorCount(_activeEditor);
        if (count <= 0)
        {
            return;
        }

        if (_trackingEditorIndex < 0)
        {
            _trackingEditorIndex = delta > 0 ? 0 : count - 1;
        }
        else
        {
            _trackingEditorIndex = (_trackingEditorIndex + delta + count) % count;
        }

        _host.InvalidateSurface();
    }

    private void MoveCalendarTracking(KryptonRadialMenuCalendarItem calendar, int delta)
    {
        var days = calendar.GetMonthDays();
        if (days.Length == 0)
        {
            return;
        }

        var offset = Math.Min(calendar.ScrollOffset, Math.Max(0, days.Length - 1));
        var count = Math.Min(EditorPageSize, days.Length - offset);
        if (count <= 0)
        {
            return;
        }

        if (_trackingEditorIndex < 0)
        {
            _trackingEditorIndex = delta > 0 ? 0 : count - 1;
            _host.InvalidateSurface();
            return;
        }

        var next = _trackingEditorIndex + delta;
        if (next >= count)
        {
            if (offset + count < days.Length)
            {
                calendar.ScrollOffset = offset + 1;
                _trackingEditorIndex = count - 1;
            }
            else
            {
                _trackingEditorIndex = 0;
            }
        }
        else if (next < 0)
        {
            if (offset > 0)
            {
                calendar.ScrollOffset = offset - 1;
                _trackingEditorIndex = 0;
            }
            else
            {
                _trackingEditorIndex = count - 1;
            }
        }
        else
        {
            _trackingEditorIndex = next;
        }

        _host.InvalidateSurface();
    }

    private void ActivateKeyboardSelection()
    {
        if (_activeEditor != null)
        {
            if (_trackingEditorIndex >= 0)
            {
                HandleEditorClick(_trackingEditorIndex);
            }
            else if (_activeEditor is KryptonRadialMenuSliderItem)
            {
                // Keep slider open until Esc / centre.
            }
            else if (_activeEditor is KryptonRadialMenuTextItem textItem)
            {
                textItem.CommitEdit();
                _activeEditor = null;
                _trackingEditorIndex = -1;
                _host.InvalidateSurface();
            }
            else
            {
                HandleCenterClick();
            }

            return;
        }

        if (_trackingIndex >= 0)
        {
            OpenChildOrEditor(_trackingIndex);
            _host.InvalidateSurface();
            return;
        }

        HandleCenterClick();
    }

    private void HandleSectorBodyClick(int sectorIndex)
    {
        _host.ToolTipHost?.Cancel();
        if (sectorIndex < 0 || sectorIndex >= _visibleItems.Count)
        {
            return;
        }

        var item = _visibleItems[sectorIndex];
        if (!item.Enabled)
        {
            return;
        }

        if (item.HasChildren)
        {
            _host.RaiseItemClick(item);
            return;
        }

        _host.RaiseItemClick(item);
        if (item is KryptonRadialMenuItem commandItem)
        {
            commandItem.PerformClick();
            if (commandItem.AutoClose && _host.SupportsAutoClose)
            {
                _host.RequestClose(ToolStripDropDownCloseReason.ItemClicked);
            }
        }
    }

    private void OpenChildOrEditor(int sectorIndex)
    {
        _host.ToolTipHost?.Cancel();
        if (sectorIndex < 0 || sectorIndex >= _visibleItems.Count)
        {
            return;
        }

        var item = _visibleItems[sectorIndex];
        if (!item.Enabled)
        {
            return;
        }

        _host.RaiseItemClick(item);

        switch (item)
        {
            case KryptonRadialMenuItem { HasChildren: true } commandItem:
                _navigation.Push(_currentItems);
                _currentItems = commandItem.Items;
                _pageOffset = 0;
                RebuildLayout(playNavigateAnimation: true);
                break;
            case KryptonRadialMenuItem commandItem:
                commandItem.PerformClick();
                if (commandItem.AutoClose && _host.SupportsAutoClose)
                {
                    _host.RequestClose(ToolStripDropDownCloseReason.ItemClicked);
                }
                break;
            case KryptonRadialMenuSliderItem:
            case KryptonRadialMenuColorPaletteItem:
            case KryptonRadialMenuFontListItem:
                _activeEditor = item;
                _trackingEditorIndex = -1;
                break;
            case KryptonRadialMenuTextItem textItem:
                textItem.BeginEdit();
                _activeEditor = textItem;
                _trackingEditorIndex = -1;
                break;
            case KryptonRadialMenuCalendarItem calendarItem:
                _activeEditor = calendarItem;
                _trackingEditorIndex = -1;
                break;
        }
    }

    private void HandleEditorClick(int editorIndex)
    {
        switch (_activeEditor)
        {
            case KryptonRadialMenuColorPaletteItem colors:
                if (editorIndex >= 0 && editorIndex < colors.Colors.Length)
                {
                    colors.SelectedColor = colors.Colors[editorIndex];
                    _activeEditor = null;
                    if (_host.SupportsAutoClose)
                    {
                        _host.RequestClose(ToolStripDropDownCloseReason.ItemClicked);
                    }
                }
                break;
            case KryptonRadialMenuFontListItem fonts:
                var families = fonts.FontFamilies;
                if (families.Length == 0)
                {
                    break;
                }

                var visible = Math.Min(EditorPageSize, families.Length);
                if (editorIndex >= 0 && editorIndex < visible)
                {
                    var familyIndex = (fonts.ScrollOffset + editorIndex) % families.Length;
                    fonts.SelectFamily(families[familyIndex]);
                    _activeEditor = null;
                    if (_host.SupportsAutoClose)
                    {
                        _host.RequestClose(ToolStripDropDownCloseReason.ItemClicked);
                    }
                }
                break;
            case KryptonRadialMenuTextItem textItem:
                if (editorIndex == 0)
                {
                    textItem.CancelEdit();
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                }
                else if (editorIndex == 1)
                {
                    textItem.CommitEdit();
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                }
                break;
            case KryptonRadialMenuCalendarItem calendarItem:
            {
                var days = calendarItem.GetMonthDays();
                var offset = Math.Min(calendarItem.ScrollOffset, Math.Max(0, days.Length - 1));
                var count = Math.Min(EditorPageSize, days.Length - offset);
                if (editorIndex >= 0 && editorIndex < count)
                {
                    calendarItem.SelectedDate = days[offset + editorIndex];
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                    if (_host.SupportsAutoClose)
                    {
                        _host.RequestClose(ToolStripDropDownCloseReason.ItemClicked);
                    }
                }
                break;
            }
        }
    }

    private static int GetEditorSectorCount(KryptonRadialMenuItemBase? editor) =>
        editor switch
        {
            KryptonRadialMenuColorPaletteItem colors => colors.Colors.Length,
            KryptonRadialMenuFontListItem fonts => Math.Min(EditorPageSize, fonts.FontFamilies.Length),
            KryptonRadialMenuTextItem => 2,
            KryptonRadialMenuCalendarItem calendar => GetCalendarVisibleDayCount(calendar),
            KryptonRadialMenuSliderItem => 1,
            _ => 0
        };

    private static int GetCalendarVisibleDayCount(KryptonRadialMenuCalendarItem calendar)
    {
        var days = calendar.GetMonthDays();
        var offset = Math.Min(calendar.ScrollOffset, Math.Max(0, days.Length - 1));
        return Math.Min(EditorPageSize, Math.Max(0, days.Length - offset));
    }

    #endregion
}
