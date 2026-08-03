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
    private const int DefaultBorderWidth = 2;

    /// <summary>
    /// Tints the page tab border colors to match the group accent, or clears when ungrouped / disabled.
    /// </summary>
    public static void Apply(KryptonPage page, NavigatorTabGroup? group, NavigatorTabGroupAppearance? appearance = null)
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

        if (appearance != null && (!appearance.ShowMemberBorder || appearance.MemberBorderWidth <= 0))
        {
            Clear(page);
            return;
        }

        Color accent = group.Color;
        int width = appearance?.MemberBorderWidth ?? DefaultBorderWidth;
        SetBorder(page.StateNormal.Tab, accent, width);
        SetBorder(page.StateTracking.Tab, ControlPaint.Light(accent), width);
        SetBorder(page.StatePressed.Tab, ControlPaint.Dark(accent), width);
        SetBorder(page.StateSelected.Tab, accent, width);
        SetBorder(page.StateDisabled.Tab, ControlPaint.Light(accent, 0.5f), width);
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
    public static void SyncNavigator(KryptonNavigator navigator, NavigatorTabGroupCollection groups,
        NavigatorTabGroupAppearance? appearance = null)
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
            Apply(page, group, appearance);
        }
    }

    /// <summary>
    /// Re-applies accents for every page in every workspace cell using the group catalog.
    /// </summary>
    public static void SyncWorkspace(Krypton.Workspace.KryptonWorkspace workspace, NavigatorTabGroupCollection groups,
        NavigatorTabGroupAppearance? appearance = null)
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
            SyncNavigator(cell, groups, appearance);
        }
    }

    private static void SetBorder(PaletteTabTriple tab, Color color, int width)
    {
        tab.Border.Color1 = color;
        tab.Border.Color2 = color;
        tab.Border.Width = width;
    }

    private static void ResetBorder(PaletteTabTriple tab)
    {
        tab.Border.Color1 = SharedStaticVariables.EMPTY_COLOR;
        tab.Border.Color2 = SharedStaticVariables.EMPTY_COLOR;
        tab.Border.Width = -1;
    }
}
