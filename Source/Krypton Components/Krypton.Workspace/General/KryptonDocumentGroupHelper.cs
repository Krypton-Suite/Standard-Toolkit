#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Workspace;

/// <summary>
/// Thin helpers for IDE-style document groups over <see cref="KryptonWorkspace"/> cells.
/// </summary>
/// <remarks>
/// Each <see cref="KryptonWorkspaceCell"/> is an independent tab group. Prefer these helpers
/// for programmatic split/move; drag-to-edge already creates cells interactively.
/// </remarks>
public static class KryptonDocumentGroupHelper
{
    /// <summary>
    /// Splits the active cell by creating an empty sibling cell beside it.
    /// </summary>
    /// <param name="workspace">Target workspace.</param>
    /// <param name="orientation">Horizontal creates side-by-side cells; Vertical stacks them.</param>
    /// <returns>The newly created empty cell, or null if no active cell exists.</returns>
    public static KryptonWorkspaceCell? SplitActiveCell(KryptonWorkspace workspace, Orientation orientation)
    {
        if (workspace == null)
        {
            throw new ArgumentNullException(nameof(workspace));
        }

        KryptonWorkspaceCell? active = workspace.ActiveCell;
        if (active == null)
        {
            active = workspace.FirstCell();
        }

        if (active == null)
        {
            var cell = new KryptonWorkspaceCell();
            workspace.Root.Children!.Add(cell);
            workspace.ActiveCell = cell;
            return cell;
        }

        return InsertSiblingCell(workspace, active, orientation, after: true, movePage: null);
    }

    /// <summary>
    /// Moves a page into a new sibling cell beside its current cell.
    /// </summary>
    public static KryptonWorkspaceCell? MovePageToNewCell(KryptonWorkspace workspace, KryptonPage page, Orientation orientation)
    {
        if (workspace == null)
        {
            throw new ArgumentNullException(nameof(workspace));
        }

        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        KryptonWorkspaceCell? source = FindCellContainingPage(workspace, page);
        if (source == null)
        {
            return null;
        }

        return InsertSiblingCell(workspace, source, orientation, after: true, movePage: page);
    }

    /// <summary>
    /// Removes empty cells that still leave at least one cell in the workspace.
    /// </summary>
    public static int CloseEmptyCells(KryptonWorkspace workspace)
    {
        if (workspace == null)
        {
            throw new ArgumentNullException(nameof(workspace));
        }

        var cells = new List<KryptonWorkspaceCell>();
        CollectCells(workspace.Root, cells);
        var removed = 0;

        foreach (KryptonWorkspaceCell cell in cells)
        {
            if (cell.Pages.Count > 0)
            {
                continue;
            }

            if (cells.Count - removed <= 1)
            {
                break;
            }

            if (cell.WorkspaceParent is KryptonWorkspaceSequence sequence)
            {
                sequence.Children!.Remove(cell);
                cell.Dispose();
                removed++;
            }
        }

        if (removed > 0)
        {
            workspace.PerformLayout();
        }

        return removed;
    }

    private static KryptonWorkspaceCell InsertSiblingCell(
        KryptonWorkspace workspace,
        KryptonWorkspaceCell target,
        Orientation orientation,
        bool after,
        KryptonPage? movePage)
    {
        if (target.WorkspaceParent is not KryptonWorkspaceSequence parent)
        {
            parent = workspace.Root;
            if (!parent.Children!.Contains(target))
            {
                parent.Children.Add(target);
            }
        }

        var newCell = new KryptonWorkspaceCell();
        if (movePage != null)
        {
            target.Pages.Remove(movePage);
            newCell.Pages.Add(movePage);
            if (newCell.AllowTabSelect)
            {
                newCell.SelectedPage = movePage;
            }
        }

        bool needsNestedSequence =
            (orientation == Orientation.Horizontal && parent.Orientation == Orientation.Vertical) ||
            (orientation == Orientation.Vertical && parent.Orientation == Orientation.Horizontal);

        if (needsNestedSequence)
        {
            var sequence = new KryptonWorkspaceSequence(orientation);
            var index = parent.Children!.IndexOf(target);
            parent.Children.RemoveAt(index);
            sequence.Children!.Add(target);
            if (after)
            {
                sequence.Children.Add(newCell);
            }
            else
            {
                sequence.Children.Insert(0, newCell);
            }

            parent.Children.Insert(index, sequence);
        }
        else
        {
            // Align parent orientation with requested split when it only has the target.
            if (parent.Children!.Count == 1)
            {
                parent.Orientation = orientation;
            }

            var index = parent.Children.IndexOf(target);
            if (after)
            {
                parent.Children.Insert(index + 1, newCell);
            }
            else
            {
                parent.Children.Insert(index, newCell);
            }
        }

        workspace.PerformLayout();
        workspace.ActiveCell = newCell;
        newCell.Select();
        return newCell;
    }

    private static KryptonWorkspaceCell? FindCellContainingPage(KryptonWorkspace workspace, KryptonPage page)
    {
        var cells = new List<KryptonWorkspaceCell>();
        CollectCells(workspace.Root, cells);
        foreach (KryptonWorkspaceCell cell in cells)
        {
            if (cell.Pages.Contains(page))
            {
                return cell;
            }
        }

        return null;
    }

    private static void CollectCells(KryptonWorkspaceSequence sequence, List<KryptonWorkspaceCell> cells)
    {
        if (sequence.Children == null)
        {
            return;
        }

        foreach (Component child in sequence.Children)
        {
            switch (child)
            {
                case KryptonWorkspaceCell cell:
                    cells.Add(cell);
                    break;
                case KryptonWorkspaceSequence nested:
                    CollectCells(nested, cells);
                    break;
            }
        }
    }
}
