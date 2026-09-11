#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Workspace;

/// <summary>
/// Shared helpers for logical RTL packing of horizontal workspace sequences.
/// </summary>
/// <remarks>
/// Uses the same two-flag contract as <see cref="KryptonForm"/>: both
/// <see cref="Control.RightToLeft"/> and a bool layout flag must be on.
/// Does not enable <c>WS_EX_LAYOUTRTL</c>; cell contents are not GDI-mirrored.
/// Collection order and XML persistence stay left-to-right logical.
/// </remarks>
internal static class WorkspaceRtlLayout
{
    /// <summary>
    /// Gets whether the workspace should pack horizontal sequences from the right.
    /// </summary>
    /// <param name="workspace">Owning workspace.</param>
    /// <returns>True when both RTL flags are set.</returns>
    public static bool IsRtl(KryptonWorkspace? workspace) =>
        workspace != null && CommonHelper.IsRightToLeftLayout(workspace);

    /// <summary>
    /// Gets whether a sequence of the given orientation should pack from the right.
    /// </summary>
    /// <param name="workspace">Owning workspace.</param>
    /// <param name="orientation">Sequence orientation.</param>
    /// <returns>True for a horizontal sequence when both RTL flags are set.</returns>
    public static bool IsHorizontalRtl(KryptonWorkspace? workspace, Orientation orientation) =>
        orientation == Orientation.Horizontal && IsRtl(workspace);

    /// <summary>
    /// Gets the running X for the first packed item.
    /// </summary>
    /// <param name="client">Container rectangle.</param>
    /// <param name="isRtl">True to pack from the right edge.</param>
    /// <returns>Left edge for LTR; right edge for RTL.</returns>
    public static int StartX(Rectangle client, bool isRtl) => isRtl ? client.Right : client.X;

    /// <summary>
    /// Places the next packed item and advances the running X.
    /// </summary>
    /// <param name="x">Running X; updated to the next slot.</param>
    /// <param name="y">Top of the item.</param>
    /// <param name="width">Item width.</param>
    /// <param name="height">Item height.</param>
    /// <param name="isRtl">True to pack toward the left.</param>
    /// <param name="gap">Extra gap after the item along the packing direction.</param>
    /// <returns>Rectangle for the item.</returns>
    public static Rectangle NextItem(ref int x, int y, int width, int height, bool isRtl, int gap = 0)
    {
        Rectangle rect;
        if (isRtl)
        {
            x -= width;
            rect = new Rectangle(x, y, width, height);
            x -= gap;
        }
        else
        {
            rect = new Rectangle(x, y, width, height);
            x += width + gap;
        }

        return rect;
    }

    /// <summary>
    /// Maps a geometric drop edge to collection insert-at-start versus insert-at-end.
    /// </summary>
    /// <param name="edge">Physical drop edge.</param>
    /// <param name="isRtl">True when RTL layout is active.</param>
    /// <returns>
    /// True to insert at the start of <c>Children</c> (or before the target cell).
    /// Left/Right are swapped when <paramref name="isRtl"/> is true so a drop on the
    /// physical left still appears on the visual left.
    /// </returns>
    public static bool InsertAtCollectionStart(VisualOrientation edge, bool isRtl)
    {
        var atStart = edge is VisualOrientation.Left or VisualOrientation.Top;
        if (isRtl && (edge is VisualOrientation.Left or VisualOrientation.Right))
        {
            atStart = !atStart;
        }

        return atStart;
    }
}
