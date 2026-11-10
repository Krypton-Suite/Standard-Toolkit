#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shared helpers for multi-select tree view operations.
/// </summary>
internal static class TreeViewMultiSelectHelper
{
    /// <summary>
    /// Returns every node in the same flat display order as <see cref="TreeNode.NextVisibleNode"/>.
    /// </summary>
    internal static List<TreeNode> GetFlatVisibleNodes(TreeView treeView)
    {
        var result = new List<TreeNode>();
        if (treeView.Nodes.Count == 0)
        {
            return result;
        }

        for (TreeNode? current = treeView.Nodes[0]; current is not null; current = current.NextVisibleNode)
        {
            result.Add(current);
        }

        return result;
    }

    /// <summary>
    /// Returns the inclusive visible range between two nodes in flat display order.
    /// </summary>
    internal static List<TreeNode> GetInclusiveVisibleRange(TreeNode anchor, TreeNode end)
    {
        TreeNode first = GetEarlierVisibleNode(anchor, end);
        TreeNode last = ReferenceEquals(first, anchor) ? end : anchor;

        var range = new List<TreeNode>();
        for (TreeNode? current = first; current is not null; current = current.NextVisibleNode)
        {
            range.Add(current);
            if (ReferenceEquals(current, last))
            {
                break;
            }
        }

        if (range.Count == 0 || !ReferenceEquals(range[range.Count - 1], last))
        {
            return new List<TreeNode> { end };
        }

        return range;
    }

    /// <summary>
    /// Enumerates nodes in the order they are displayed when the tree is expanded.
    /// </summary>
    internal static IEnumerable<TreeNode> EnumerateVisibleNodes(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            yield return node;

            if (node.IsExpanded)
            {
                foreach (TreeNode child in EnumerateVisibleNodes(node.Nodes))
                {
                    yield return child;
                }
            }
        }
    }

    /// <summary>
    /// Enumerates every node in the tree, regardless of expansion state.
    /// </summary>
    internal static IEnumerable<TreeNode> EnumerateAllNodes(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            yield return node;

            foreach (TreeNode child in EnumerateAllNodes(node.Nodes))
            {
                yield return child;
            }
        }
    }

    /// <summary>
    /// Returns the bounding rectangle of a tree node in tree-view client coordinates.
    /// </summary>
    internal static Rectangle GetNodeBounds(TreeView treeView, TreeNode node, bool textOnly = false)
    {
        if (!treeView.IsHandleCreated || node.TreeView != treeView || node.Handle == IntPtr.Zero)
        {
            return Rectangle.Empty;
        }

        Rectangle bounds = GetNodeBoundsFromMessage(treeView, node, textOnly);
        if (!bounds.IsEmpty)
        {
            return bounds;
        }

        return GetNodeBoundsFromVisibleOrder(treeView, node);
    }

    /// <summary>
    /// Normalizes a rubber-band rectangle from two client points.
    /// </summary>
    internal static Rectangle NormalizeRectangle(Point start, Point end)
    {
        var x = Math.Min(start.X, end.X);
        var y = Math.Min(start.Y, end.Y);
        var width = Math.Abs(start.X - end.X);
        var height = Math.Abs(start.Y - end.Y);
        return new Rectangle(x, y, width, height);
    }

    private static Rectangle GetNodeBoundsFromMessage(TreeView treeView, TreeNode node, bool textOnly)
    {
        IntPtr rectPtr = Marshal.AllocHGlobal(Marshal.SizeOf<PI.RECT>());
        try
        {
            // WinForms stores HTREEITEM in the first pointer-sized field of RECT (see TreeNode.Bounds).
            Marshal.WriteIntPtr(rectPtr, node.Handle);

            uint result = PI.SendMessage(
                treeView.Handle,
                PI.TVM_.TVM_GETITEMRECT,
                textOnly ? (IntPtr)1 : IntPtr.Zero,
                rectPtr);

            if (result == 0)
            {
                return Rectangle.Empty;
            }

            var rect = Marshal.PtrToStructure<PI.RECT>(rectPtr);
            return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
        }
        finally
        {
            Marshal.FreeHGlobal(rectPtr);
        }
    }

    private static Rectangle GetNodeBoundsFromVisibleOrder(TreeView treeView, TreeNode node)
    {
        if (treeView.Nodes.Count == 0)
        {
            return Rectangle.Empty;
        }

        var y = 0;
        var itemHeight = Math.Max(1, treeView.ItemHeight);

        for (TreeNode? current = treeView.Nodes[0]; current is not null; current = current.NextVisibleNode)
        {
            if (ReferenceEquals(current, node))
            {
                return new Rectangle(0, y, treeView.ClientSize.Width, itemHeight);
            }

            y += itemHeight;
        }

        return Rectangle.Empty;
    }

    private static TreeNode GetEarlierVisibleNode(TreeNode first, TreeNode second)
    {
        TreeView? treeView = first.TreeView ?? second.TreeView;
        if (treeView is null || treeView.Nodes.Count == 0)
        {
            return first;
        }

        for (TreeNode? current = treeView.Nodes[0]; current is not null; current = current.NextVisibleNode)
        {
            if (ReferenceEquals(current, first))
            {
                return first;
            }

            if (ReferenceEquals(current, second))
            {
                return second;
            }
        }

        return first;
    }
}
