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
/// Applies light browser-style group accents to normal navigator tab palettes via page Tab colors.
/// </summary>
public static class NavigatorTabGroupBarAccent
{
    /// <summary>
    /// Tints the page tab border colors to match the group accent, or clears when ungrouped.
    /// </summary>
    public static void Apply(KryptonPage page, NavigatorTabGroup? group)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        if (group == null || group.Color.IsEmpty)
        {
            Clear(page);
            return;
        }

        Color accent = group.Color;
        SetBorder(page.StateNormal.Tab, accent);
        SetBorder(page.StateTracking.Tab, ControlPaint.Light(accent));
        SetBorder(page.StatePressed.Tab, ControlPaint.Dark(accent));
        SetBorder(page.StateSelected.Tab, accent);
        SetBorder(page.StateDisabled.Tab, ControlPaint.Light(accent, 0.5f));
    }

    /// <summary>
    /// Clears custom tab border color overrides for the page.
    /// </summary>
    public static void Clear(KryptonPage page)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        ResetBorder(page.StateNormal.Tab);
        ResetBorder(page.StateTracking.Tab);
        ResetBorder(page.StatePressed.Tab);
        ResetBorder(page.StateSelected.Tab);
        ResetBorder(page.StateDisabled.Tab);
    }

    /// <summary>
    /// Re-applies accents for every page in the navigator using the group catalog.
    /// </summary>
    public static void SyncNavigator(KryptonNavigator navigator, NavigatorTabGroupCollection groups)
    {
        if (navigator == null)
        {
            throw new ArgumentNullException(nameof(navigator));
        }

        if (groups == null)
        {
            throw new ArgumentNullException(nameof(groups));
        }

        foreach (KryptonPage page in navigator.Pages)
        {
            NavigatorTabGroup? group = string.IsNullOrEmpty(page.TabGroupId) ? null : groups[page.TabGroupId];
            Apply(page, group);
        }
    }

    /// <summary>
    /// Re-applies accents for every page in every workspace cell using the group catalog.
    /// </summary>
    public static void SyncWorkspace(Krypton.Workspace.KryptonWorkspace workspace, NavigatorTabGroupCollection groups)
    {
        if (workspace == null)
        {
            throw new ArgumentNullException(nameof(workspace));
        }

        if (groups == null)
        {
            throw new ArgumentNullException(nameof(groups));
        }

        for (Krypton.Workspace.KryptonWorkspaceCell? cell = workspace.FirstCell();
             cell != null;
             cell = workspace.NextCell(cell))
        {
            SyncNavigator(cell, groups);
        }
    }

    private static void SetBorder(PaletteTabTriple tab, Color color)
    {
        tab.Border.Color1 = color;
        tab.Border.Color2 = color;
        tab.Border.Width = 2;
    }

    private static void ResetBorder(PaletteTabTriple tab)
    {
        tab.Border.Color1 = GlobalStaticVariables.EMPTY_COLOR;
        tab.Border.Color2 = GlobalStaticVariables.EMPTY_COLOR;
        tab.Border.Width = -1;
    }
}