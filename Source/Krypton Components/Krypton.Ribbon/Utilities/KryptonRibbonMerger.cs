#region Original MIT License

/*
 * Copyright(C) 2018 Michael Winsor
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 * Created: September 28, 2018 9:03:32 PM
 */

#endregion

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>Merges a krypton ribbon with another ribbon.</summary>
/// <remarks>
/// <para>To affect the order of the merged groups and tabs, set the Tag property to a value from 0 to n, where n is the count of the target group minus 1.</para>
/// </remarks>
public class KryptonRibbonMerger
{
    #region Instance Fields

    private readonly HashSet<Component> _mergedItems = new HashSet<Component>();

    #endregion

    #region Public

    /// <summary>
    /// Property to return the target ribbon that will receive the merged items.
    /// </summary>
    public KryptonRibbon TargetRibbon
    {
        get;
    }

    #endregion

    #region Identity

    /// <summary>
    /// Merges a krypton ribbon with another ribbon.
    /// </summary>
    /// <param name="targetRibbon">The ribbon to merge with.</param>
    /// <exception cref="ArgumentNullException">Will be thrown if the parameter targetRibbon is null.</exception>
    public KryptonRibbonMerger([DisallowNull ]KryptonRibbon targetRibbon)
    {
        TargetRibbon = targetRibbon ?? throw new ArgumentNullException(nameof(targetRibbon));
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Function to retrieve the sorting index from the items tag.
    /// </summary>
    /// <param name="tagValue">The value for the tag.</param>
    /// <param name="maxValue">The maximum value for the items.</param>
    /// <returns>The sorting index.</returns>
    private int GetSortIndexFromTag(object tagValue, int maxValue)
    {
        if (maxValue < 0)
        {
            return 0;
        }

        return tagValue switch
        {
            null => maxValue,
            string when int.TryParse(tagValue.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture,
                out int parsedValue) => parsedValue.Max(0).Min(maxValue),
            _ => (int)Convert.ChangeType(tagValue, typeof(int))
        };
    }

    /// <summary>
    /// Function to unmerge items for a group from an existing items list in the target ribbon group.
    /// </summary>
    /// <param name="sourceItems">The source items to unmerge.</param>
    /// <param name="targetItems">The destination items to unmerge.</param>
    private void UnmergeGroupItems(KryptonRibbonGroupContainerCollection sourceItems, KryptonRibbonGroupContainerCollection targetItems)
    {
        IEnumerable<KryptonRibbonGroupContainer> items = targetItems.ToArray();

        foreach (KryptonRibbonGroupContainer item in items)
        {
            if (!_mergedItems.Contains(item))
            {
                continue;
            }

            _mergedItems.Remove(item);
            targetItems.Remove(item);
            sourceItems.Add(item);
        }
    }

    /// <summary>
    /// Function to merge items from a group into an existing items list in the target ribbon group.
    /// </summary>
    /// <param name="sourceItems">The source items to merge.</param>
    /// <param name="targetItems">The destination items to merge.</param>
    private void MergeGroupItems(KryptonRibbonGroupContainerCollection sourceItems, KryptonRibbonGroupContainerCollection targetItems)
    {
        IEnumerable<KryptonRibbonGroupContainer> items = sourceItems.ToArray();

        foreach (KryptonRibbonGroupContainer sourceItem in items)
        {
            if (targetItems.Contains(sourceItem))
            {
                continue;
            }

            sourceItems.Remove(sourceItem);
            int index = GetSortIndexFromTag(sourceItem.Tag!, targetItems.Count);
            targetItems.Insert(index, sourceItem);

            if (!_mergedItems.Contains(sourceItem))
            {
                _mergedItems.Add(sourceItem);
            }
        }
    }

    /// <summary>
    /// Function to unmerge the groups for a tab from an existing tab in the target ribbon tab.
    /// </summary>
    /// <param name="sourceGroups">The source groups to unmerge.</param>
    /// <param name="targetGroups">The destination groups to unmerge.</param>
    private void UnmergeGroups(KryptonRibbonGroupCollection sourceGroups, KryptonRibbonGroupCollection targetGroups)
    {
        IEnumerable<KryptonRibbonGroup> groups = targetGroups.ToArray();

        foreach (KryptonRibbonGroup grp in groups)
        {
            if (!_mergedItems.Contains(grp))
            {
                KryptonRibbonGroup? existingGroup = sourceGroups.FirstOrDefault(item => (string.Equals(item.TextLine1, grp.TextLine1, StringComparison.CurrentCulture))
                    && (string.Equals(item.TextLine2, grp.TextLine2, StringComparison.CurrentCulture)));

                if (existingGroup is not null)
                {
                    UnmergeGroupItems(existingGroup.Items, grp.Items);
                }

                continue;
            }

            _mergedItems.Remove(grp);
            targetGroups.Remove(grp);
            sourceGroups.Add(grp);
        }
    }

    /// <summary>
    /// Function to merge the groups from a tab into an existing tab in the target ribbon tab.
    /// </summary>
    /// <param name="sourceGroups">The source groups to merge.</param>
    /// <param name="targetGroups">The destination groups to merge.</param>
    private void MergeGroups(KryptonRibbonGroupCollection sourceGroups, KryptonRibbonGroupCollection targetGroups)
    {
        IEnumerable<KryptonRibbonGroup> groups = sourceGroups.ToArray();

        foreach (KryptonRibbonGroup sourceGroup in groups)
        {
            KryptonRibbonGroup? existingGroup = targetGroups.FirstOrDefault(item => (string.Equals(item.TextLine1, sourceGroup.TextLine1, StringComparison.CurrentCulture))
                && (string.Equals(item.TextLine2, sourceGroup.TextLine2, StringComparison.CurrentCulture)));

            if ((existingGroup is null) && (!targetGroups.Contains(sourceGroup)))
            {
                sourceGroups.Remove(sourceGroup);

                int index = GetSortIndexFromTag(sourceGroup.Tag!, targetGroups.Count);
                targetGroups.Insert(index, sourceGroup);
                if (!_mergedItems.Contains(sourceGroup))
                {
                    _mergedItems.Add(sourceGroup);
                }
                continue;
            }

            // We'll need to merge the group items.
            MergeGroupItems(sourceGroup.Items, existingGroup!.Items);
        }
    }

    /// <summary>
    /// Function to merge the tabs for a source ribbon into a target ribbon.
    /// </summary>
    /// <param name="sourceRibbon">The ribbon to merge.</param>
    /// <param name="targetRibbon">The ribbon to be merged into.</param>
    private void MergeTabs(KryptonRibbon? sourceRibbon, KryptonRibbon targetRibbon)
    {
        IEnumerable<KryptonRibbonTab> sourceTabs = sourceRibbon!.RibbonTabs.ToArray();

        foreach (KryptonRibbonTab tab in sourceTabs)
        {
            KryptonRibbonTab? existingTab = targetRibbon.RibbonTabs.FirstOrDefault(item => string.Equals(item.Text, tab.Text, StringComparison.CurrentCulture));

            // The tab doesn't exist, so just add it
            if ((existingTab is null) && (!targetRibbon.RibbonTabs.Contains(tab)))
            {
                sourceRibbon.RibbonTabs.Remove(tab);

                int index = GetSortIndexFromTag(tab.Tag!, targetRibbon.RibbonTabs.Count);

                targetRibbon.RibbonTabs.Insert(index, tab);
                if (!_mergedItems.Contains(tab))
                {
                    _mergedItems.Add(tab);
                }
                continue;
            }

            // We'll need to merge the groups.
            MergeGroups(tab.Groups, existingTab!.Groups);
        }
    }

    /// <summary>
    /// Function to unmerge the contexts for a source ribbon from a target ribbon.
    /// </summary>
    /// <param name="sourceRibbon">The ribbon to unmerge.</param>
    /// <param name="targetRibbon">The ribbon to be unmerged from.</param>
    private void UnmergeContexts(KryptonRibbon sourceRibbon, KryptonRibbon targetRibbon)
    {
        IEnumerable<KryptonRibbonContext> contexts = targetRibbon.RibbonContexts.ToArray();

        foreach (KryptonRibbonContext context in contexts)
        {
            if (!_mergedItems.Contains(context))
            {
                continue;
            }

            KryptonRibbonContext? existing = sourceRibbon.RibbonContexts.FirstOrDefault(item => string.Equals(item.ContextTitle, context.ContextTitle, StringComparison.CurrentCulture));

            // The tab doesn't exist, so just add it
            if ((existing is not null) || (sourceRibbon.RibbonContexts.Contains(context)))
            {
                continue;
            }

            _mergedItems.Remove(context);
            targetRibbon.RibbonContexts.Remove(context);
            sourceRibbon.RibbonContexts.Add(context);
        }
    }

    /// <summary>
    /// Function to merge the contexts for a source ribbon into a target ribbon.
    /// </summary>
    /// <param name="sourceRibbon">The ribbon to merge.</param>
    /// <param name="targetRibbon">The ribbon to be merged into.</param>
    private void MergeContexts(KryptonRibbon? sourceRibbon, KryptonRibbon targetRibbon)
    {
        IEnumerable<KryptonRibbonContext> contexts = sourceRibbon!.RibbonContexts.ToArray();

        foreach (KryptonRibbonContext context in contexts)
        {
            KryptonRibbonContext? existing = targetRibbon.RibbonContexts.FirstOrDefault(item => string.Equals(item.ContextTitle, context.ContextTitle, StringComparison.CurrentCulture));

            // The tab doesn't exist, so just add it
            if ((existing is not null) || (targetRibbon.RibbonContexts.Contains(context)))
            {
                continue;
            }

            sourceRibbon.RibbonContexts.Remove(context);

            int index = GetSortIndexFromTag(context.Tag!, targetRibbon.RibbonContexts.Count);

            targetRibbon.RibbonContexts.Insert(index, context);
            if (!_mergedItems.Contains(context))
            {
                _mergedItems.Add(context);
            }
        }
    }

    /// <summary>
    /// Function to unmerge the tabs for a source ribbon from a target ribbon.
    /// </summary>
    /// <param name="sourceRibbon">The ribbon to merge into.</param>
    /// <param name="targetRibbon">The ribbon to be unmerged from.</param>
    private void UnmergeTabs(KryptonRibbon sourceRibbon, KryptonRibbon targetRibbon)
    {
        IEnumerable<KryptonRibbonTab> tabs = targetRibbon.RibbonTabs.ToArray();

        foreach (KryptonRibbonTab tab in tabs)
        {
            if (!_mergedItems.Contains(tab))
            {
                KryptonRibbonTab? existingTab = sourceRibbon.RibbonTabs.FirstOrDefault(item => string.Equals(item.Text, tab.Text, StringComparison.CurrentCulture));

                if (existingTab is not null)
                {
                    UnmergeGroups(existingTab.Groups, tab.Groups);
                }
                continue;
            }

            _mergedItems.Remove(tab);
            targetRibbon.RibbonTabs.Remove(tab);
            sourceRibbon.RibbonTabs.Add(tab);
        }
    }

    /// <summary>
    /// Function to merge a ribbon with the target ribbon
    /// </summary>
    /// <param name="ribbon">The ribbon to merge.</param>
    public void Merge(KryptonRibbon? ribbon)
    {
        if (ribbon is null)
        {
            return;
        }

        string selectedContext = TargetRibbon.SelectedContext;
        KryptonRibbonTab? selectedTab = TargetRibbon.SelectedTab;

        MergeTabs(ribbon, TargetRibbon);
        MergeContexts(ribbon, TargetRibbon);

        // Ensure that the layout is refreshed.
        TargetRibbon.CheckPerformLayout();
        ribbon.CheckPerformLayout();

        // Restore the selected tab.
        TargetRibbon.SelectedContext = selectedContext;
        TargetRibbon.SelectedTab = selectedTab;

        FixGroupWidths();
    }

    /// <summary>
    /// Function to unmerge the specified ribbon from the target ribbon.
    /// </summary>
    /// <param name="ribbon">The ribbon to unmerge.</param>
    public void UnMerge(KryptonRibbon? ribbon)
    {
        if (ribbon is null)
        {
            return;
        }

        KryptonRibbonTab? selected = TargetRibbon.SelectedTab;

        UnmergeContexts(ribbon, TargetRibbon);
        UnmergeTabs(ribbon, TargetRibbon);

        // Ensure that the layout is refreshed.
        TargetRibbon.CheckPerformLayout();
        ribbon.CheckPerformLayout();

        // Restore the selected tab.
        if (TargetRibbon.RibbonTabs.Contains(selected))
        {
            TargetRibbon.SelectedTab = selected;
        }
        else
        {
            TargetRibbon.ResetSelectedTab();
        }
    }

    /// <summary>
    /// Function to correct the clipping for groups that have long names, but little content.
    /// </summary>
    public void FixGroupWidths()
    {
        using var g = Graphics.FromHwnd(TargetRibbon.Parent!.Handle);
        double dpi = g.DpiY / 96.0;

        foreach (KryptonRibbonTab tab in TargetRibbon.RibbonTabs)
        {
            foreach (KryptonRibbonGroup grp in tab.Groups)
            {
                Size size = TextRenderer.MeasureText(g, grp.TextLine1 + (string.IsNullOrWhiteSpace(grp.TextLine2) 
                        ? $" {grp.TextLine2}"
                        : string.Empty), 
                    TargetRibbon.Parent.Font);
                grp.MinimumWidth = size.Width + (int)(8 * dpi);
            }
        }
    }

    #endregion
}