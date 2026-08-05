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
/// Caption composite that hosts one <see cref="ViewLayoutNavigatorCaptionTabs"/> strip per
/// <see cref="Krypton.Workspace.KryptonWorkspaceCell"/> (IDE-style multi-strip caption document groups).
/// </summary>
internal sealed class ViewLayoutCaptionDocumentGroups : ViewLayoutDocker
{
    private readonly Krypton.Workspace.KryptonWorkspace _workspace;
    private readonly NeedPaintHandler _needPaint;
    private readonly Action<KryptonPage, Point>? _showContextMenu;
    private readonly NavigatorTabGroupCollection _tabGroups;
    private readonly NavigatorTabGroupAppearance _tabGroupAppearance;
    private readonly bool _allowTabGroups;
    private readonly bool _showNewTabButton;
    private readonly Action? _newTabClick;
    private readonly Dictionary<Krypton.Workspace.KryptonWorkspaceCell, ViewLayoutNavigatorCaptionTabs> _cellStrips = new();
    private readonly List<Rectangle> _lastSpareAreas = new();
    private bool _eventsHooked;

    public ViewLayoutCaptionDocumentGroups(
        Krypton.Workspace.KryptonWorkspace workspace,
        NeedPaintHandler needPaint,
        Action<KryptonPage, Point>? showContextMenu,
        NavigatorTabGroupCollection tabGroups,
        NavigatorTabGroupAppearance tabGroupAppearance,
        bool allowTabGroups,
        bool showNewTabButton,
        Action? newTabClick)
    {
        _workspace = workspace ?? ThrowHelper.ThrowArgumentNullException<Krypton.Workspace.KryptonWorkspace>(nameof(workspace));
        _needPaint = needPaint ?? ThrowHelper.ThrowArgumentNullException<NeedPaintHandler>(nameof(needPaint));
        _showContextMenu = showContextMenu;
        _tabGroups = tabGroups ?? ThrowHelper.ThrowArgumentNullException<NavigatorTabGroupCollection>(nameof(tabGroups));
        _tabGroupAppearance = tabGroupAppearance ?? ThrowHelper.ThrowArgumentNullException<NavigatorTabGroupAppearance>(nameof(tabGroupAppearance));
        _allowTabGroups = allowTabGroups;
        _showNewTabButton = showNewTabButton;
        _newTabClick = newTabClick;

        Orientation = VisualOrientation.Top;
        PreferredSizeAll = true;

        HookEvents();
        RebuildStrips();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnhookEvents();
            ClearStrips();
        }

        base.Dispose(disposing);
    }

    public Rectangle ClientCompositeRectangle => ClientRectangle;

    public void RebuildStrips()
    {
        ClearStrips();

        var cells = new List<Krypton.Workspace.KryptonWorkspaceCell>();
        CollectCells(_workspace.Root, cells);

        // Left-dock reverse order: add last cell first so visual order is first→last.
        for (var i = cells.Count - 1; i >= 0; i--)
        {
            Krypton.Workspace.KryptonWorkspaceCell cell = cells[i];
            if (cell.NavigatorMode != NavigatorMode.Panel)
            {
                cell.NavigatorMode = NavigatorMode.Panel;
            }

            var strip = new ViewLayoutNavigatorCaptionTabs(cell, _needPaint, _showContextMenu, _newTabClick)
            {
                ShowNewTabButton = _showNewTabButton && ReferenceEquals(cell, _workspace.ActiveCell),
                AllowTabGroups = _allowTabGroups,
                TabGroups = _tabGroups,
                TabGroupAppearance = _tabGroupAppearance,
                SpareCaptionAreasChanged = OnChildSpareAreasChanged
            };
            _cellStrips[cell] = strip;
            Add(strip, ViewDockStyle.Left);
            if (i > 0)
            {
                Add(new ViewLayoutSeparator(6), ViewDockStyle.Left);
            }
        }

        _needPaint(this, new NeedLayoutEventArgs(true));
    }

    public override void Layout(ViewLayoutContext context)
    {
        base.Layout(context);
        PublishSpareAreas(context);
    }

    private void PublishSpareAreas(ViewLayoutContext context)
    {
        if (context.Control is not KryptonForm form || form.IsDisposed)
        {
            return;
        }

        Padding borders = form.RealWindowBorders;
        var areas = new List<Rectangle>(_lastSpareAreas.Count + 1);
        foreach (Rectangle windowSpare in _lastSpareAreas)
        {
            if (windowSpare.Width > 8 && windowSpare.Height > 0)
            {
                areas.Add(new Rectangle(
                    windowSpare.X - borders.Left,
                    windowSpare.Y - borders.Top,
                    windowSpare.Width,
                    windowSpare.Height));
            }
        }

        // Residual caption to the right of the entire multi-strip composite.
        Rectangle residual = context.DisplayRectangle;
        if (residual.Width > ClientRectangle.Width)
        {
            var spare = new Rectangle(
                ClientRectangle.Right - borders.Left,
                ClientRectangle.Y - borders.Top,
                residual.Right - ClientRectangle.Right,
                ClientRectangle.Height);
            if (spare.Width > 8 && spare.Height > 0)
            {
                areas.Add(spare);
            }
        }

        form.CustomCaptionAreas = areas.Count == 0 ? Array.Empty<Rectangle>() : areas.ToArray();
        form.CustomCaptionArea = areas.Count > 0 ? areas[areas.Count - 1] : Rectangle.Empty;
    }

    private void OnChildSpareAreasChanged(IReadOnlyList<Rectangle> areas)
    {
        _lastSpareAreas.Clear();
        if (areas != null)
        {
            _lastSpareAreas.AddRange(areas);
        }
    }

    private void HookEvents()
    {
        if (_eventsHooked)
        {
            return;
        }

        _workspace.WorkspaceCellAdding += OnWorkspaceCellChanged;
        _workspace.WorkspaceCellRemoved += OnWorkspaceCellChanged;
        _workspace.ActiveCellChanged += OnActiveCellChanged;
        _eventsHooked = true;
    }

    private void UnhookEvents()
    {
        if (!_eventsHooked)
        {
            return;
        }

        _workspace.WorkspaceCellAdding -= OnWorkspaceCellChanged;
        _workspace.WorkspaceCellRemoved -= OnWorkspaceCellChanged;
        _workspace.ActiveCellChanged -= OnActiveCellChanged;
        _eventsHooked = false;
    }

    private void OnWorkspaceCellChanged(object? sender, Krypton.Workspace.WorkspaceCellEventArgs e) => RebuildStrips();

    private void OnActiveCellChanged(object? sender, Krypton.Workspace.ActiveCellChangedEventArgs e)
    {
        foreach (KeyValuePair<Krypton.Workspace.KryptonWorkspaceCell, ViewLayoutNavigatorCaptionTabs> pair in _cellStrips)
        {
            pair.Value.ShowNewTabButton = _showNewTabButton && ReferenceEquals(pair.Key, _workspace.ActiveCell);
        }

        _needPaint(this, new NeedLayoutEventArgs(true));
    }

    private void ClearStrips()
    {
        foreach (ViewLayoutNavigatorCaptionTabs strip in _cellStrips.Values)
        {
            strip.SpareCaptionAreasChanged = null;
            strip.Dispose();
        }

        _cellStrips.Clear();
        Clear();
    }

    private static void CollectCells(Krypton.Workspace.KryptonWorkspaceSequence sequence, List<Krypton.Workspace.KryptonWorkspaceCell> cells)
    {
        if (sequence.Children == null)
        {
            return;
        }

        foreach (Component child in sequence.Children)
        {
            switch (child)
            {
                case Krypton.Workspace.KryptonWorkspaceCell cell:
                    cells.Add(cell);
                    break;
                case Krypton.Workspace.KryptonWorkspaceSequence nested:
                    CollectCells(nested, cells);
                    break;
            }
        }
    }
}
