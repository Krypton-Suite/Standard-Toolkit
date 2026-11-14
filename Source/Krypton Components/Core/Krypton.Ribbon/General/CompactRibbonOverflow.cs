#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Walks compact ribbon groups to decide which commands stay on the row.
/// </summary>
internal static class CompactRibbonOverflow
{
    /// <summary>
    /// Collects visible commands in reading order. Separators are not commands.
    /// </summary>
    /// <param name="tab">Tab whose groups are measured.</param>
    /// <returns>Leaf commands from left to right.</returns>
    public static List<IRibbonGroupItem> CollectLeaves(KryptonRibbonTab tab)
    {
        var leaves = new List<IRibbonGroupItem>();
        foreach (KryptonRibbonGroup group in tab.Groups)
        {
            if (!group.Visible)
            {
                continue;
            }

            foreach (KryptonRibbonGroupContainer container in group.Items)
            {
                AppendLeaves(container, leaves);
            }
        }

        return leaves;
    }

    /// <summary>
    /// Groups that still have a command in the overflow set, in tab order.
    /// </summary>
    /// <param name="tab">Tab that owns the groups.</param>
    /// <param name="ribbon">Ribbon holding the overflow set.</param>
    /// <returns>Groups to show in the overflow popup.</returns>
    public static List<KryptonRibbonGroup> GroupsWithOverflow(KryptonRibbonTab tab, KryptonRibbon ribbon)
    {
        var groups = new List<KryptonRibbonGroup>();
        foreach (KryptonRibbonGroup group in tab.Groups)
        {
            if (!group.Visible)
            {
                continue;
            }

            var leaves = new List<IRibbonGroupItem>();
            foreach (KryptonRibbonGroupContainer container in group.Items)
            {
                AppendLeaves(container, leaves);
            }

            foreach (IRibbonGroupItem leaf in leaves)
            {
                if (ribbon.IsCompactOverflow(leaf))
                {
                    groups.Add(group);
                    break;
                }
            }
        }

        return groups;
    }

    /// <summary>
    /// True when the group still has a command that is not in the overflow set.
    /// </summary>
    /// <param name="group">Group to test.</param>
    /// <param name="ribbon">Ribbon holding the overflow set.</param>
    /// <returns>True when at least one command stays on the row.</returns>
    public static bool HasRemaining(KryptonRibbonGroup group, KryptonRibbon ribbon)
    {
        var leaves = new List<IRibbonGroupItem>();
        foreach (KryptonRibbonGroupContainer container in group.Items)
        {
            AppendLeaves(container, leaves);
        }

        foreach (IRibbonGroupItem leaf in leaves)
        {
            if (!ribbon.IsCompactOverflow(leaf))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Hides a group separator once every command after it has overflowed.
    /// </summary>
    /// <param name="tab">Tab whose groups are measured.</param>
    /// <param name="ribbon">Ribbon whose overflow set receives the separators.</param>
    public static void AddOrphanSeparators(KryptonRibbonTab tab, KryptonRibbon ribbon)
    {
        foreach (KryptonRibbonGroup group in tab.Groups)
        {
            if (!group.Visible)
            {
                continue;
            }

            MarkSequence(group.Items, ribbon);
        }
    }

    private static void AppendLeaves(KryptonRibbonGroupItem item, List<IRibbonGroupItem> leaves)
    {
        switch (item)
        {
            case KryptonRibbonGroupLines lines when lines.Items != null:
                foreach (KryptonRibbonGroupItem child in lines.Items)
                {
                    AppendLeaves(child, leaves);
                }

                return;
            case KryptonRibbonGroupTriple triple when triple.Items != null:
                foreach (KryptonRibbonGroupItem child in triple.Items)
                {
                    AppendLeaves(child, leaves);
                }

                return;
            case KryptonRibbonGroupCluster cluster:
                foreach (KryptonRibbonGroupItem child in cluster.Items)
                {
                    AppendLeaves(child, leaves);
                }

                return;
            case KryptonRibbonGroupSeparator:
                return;
            default:
                if (item.Visible)
                {
                    leaves.Add(item);
                }

                return;
        }
    }

    private static bool MarkSequence(IEnumerable items, KryptonRibbon ribbon)
    {
        var list = new List<KryptonRibbonGroupItem>();
        foreach (KryptonRibbonGroupItem item in items)
        {
            list.Add(item);
        }

        var keepAfter = false;
        for (var i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] is KryptonRibbonGroupSeparator separator)
            {
                if (!keepAfter)
                {
                    ribbon.AddCompactOverflow(separator);
                }

                continue;
            }

            if (Mark(list[i], ribbon))
            {
                keepAfter = true;
            }
        }

        return keepAfter;
    }

    private static bool Mark(KryptonRibbonGroupItem item, KryptonRibbon ribbon)
    {
        switch (item)
        {
            case KryptonRibbonGroupLines lines when lines.Items != null:
                return MarkSequence(lines.Items, ribbon);
            case KryptonRibbonGroupTriple triple when triple.Items != null:
                return MarkSequence(triple.Items, ribbon);
            case KryptonRibbonGroupCluster cluster:
                return MarkSequence(cluster.Items, ribbon);
            default:
                return item.Visible && !ribbon.IsCompactOverflow(item);
        }
    }
}
