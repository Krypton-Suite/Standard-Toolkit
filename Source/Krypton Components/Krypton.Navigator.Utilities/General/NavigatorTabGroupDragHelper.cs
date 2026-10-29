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
/// Shared helpers for collecting group member pages and applying join-on-drop membership.
/// </summary>
internal static class NavigatorTabGroupDragHelper
{
    public static void CollectDragPages(KryptonNavigator navigator, KryptonPage seed, KryptonPageCollection pages, bool dragWholeGroup)
    {
        pages.Clear();
        if (!seed.AreFlagsSet(KryptonPageFlags.AllowPageDrag))
        {
            return;
        }

        if (!dragWholeGroup || string.IsNullOrEmpty(seed.TabGroupId))
        {
            pages.Add(seed);
            return;
        }

        string groupId = seed.TabGroupId;
        foreach (KryptonPage page in navigator.Pages)
        {
            if (page.LastVisibleSet
                && page.AreFlagsSet(KryptonPageFlags.AllowPageDrag)
                && string.Equals(page.TabGroupId, groupId, StringComparison.Ordinal))
            {
                pages.Add(page);
            }
        }

        if (pages.Count == 0)
        {
            pages.Add(seed);
        }
    }

    public static int CountGroupMembers(KryptonNavigator navigator, string groupId)
    {
        var count = 0;
        foreach (KryptonPage page in navigator.Pages)
        {
            if (page.LastVisibleSet && string.Equals(page.TabGroupId, groupId, StringComparison.Ordinal))
            {
                count++;
            }
        }

        return count;
    }

    public static void JoinPageToTargetGroup(KryptonPage dragged, KryptonPage target, NavigatorTabGroupCollection? groups)
    {
        if (string.IsNullOrEmpty(target.TabGroupId))
        {
            return;
        }

        NavigatorTabGroup? group = groups?[target.TabGroupId];
        if (groups != null && group == null)
        {
            return;
        }

        dragged.TabGroupId = target.TabGroupId;
        NavigatorTabGroupBarAccent.Apply(dragged, group);
    }
}