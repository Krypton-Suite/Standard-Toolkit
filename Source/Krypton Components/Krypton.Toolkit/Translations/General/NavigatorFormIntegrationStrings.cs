#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Localizable strings used by `KryptonNavigatorFormIntegrator` caption tabs.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class NavigatorFormIntegrationStrings : GlobalId
{
    #region Static Values

    private const string DEFAULT_NEW_WINDOW = @"New window";
    private const string DEFAULT_MOVE_TO_NEW_WINDOW = @"Move to new window";
    private const string DEFAULT_CLOSE_TAB = @"Close tab";
    private const string DEFAULT_CLOSE_OTHER_TABS = @"Close other tabs";
    private const string DEFAULT_CLOSE_TABS_TO_THE_RIGHT = @"Close tabs to the right";
    private const string DEFAULT_NEW_TAB_BUTTON = @"+";
    private const string DEFAULT_NEW_TAB = @"New tab";
    private const string DEFAULT_ADD_TO_GROUP = @"Add to group";
    private const string DEFAULT_NEW_GROUP = @"New group";
    private const string DEFAULT_UNGROUP = @"Remove from group";
    private const string DEFAULT_RENAME_GROUP = @"Rename group";
    private const string DEFAULT_RECOLOR_GROUP = @"Change group color";
    private const string DEFAULT_COLLAPSE_GROUP = @"Collapse group";
    private const string DEFAULT_EXPAND_GROUP = @"Expand group";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="NavigatorFormIntegrationStrings" /> class.</summary>
    public NavigatorFormIntegrationStrings()
    {
        Reset();
    }

    #endregion

    #region Overrides

    /// <summary>Returns a string that represents the current object.</summary>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether the strings are the default values.</summary>

    [Browsable(false)]
    public bool IsDefault =>
        NewWindow.Equals(DEFAULT_NEW_WINDOW) &&
        MoveToNewWindow.Equals(DEFAULT_MOVE_TO_NEW_WINDOW) &&
        CloseTab.Equals(DEFAULT_CLOSE_TAB) &&
        CloseOtherTabs.Equals(DEFAULT_CLOSE_OTHER_TABS) &&
        CloseTabsToTheRight.Equals(DEFAULT_CLOSE_TABS_TO_THE_RIGHT) &&
        NewTabButton.Equals(DEFAULT_NEW_TAB_BUTTON) &&
        NewTab.Equals(DEFAULT_NEW_TAB) &&
        AddToGroup.Equals(DEFAULT_ADD_TO_GROUP) &&
        NewGroup.Equals(DEFAULT_NEW_GROUP) &&
        Ungroup.Equals(DEFAULT_UNGROUP) &&
        RenameGroup.Equals(DEFAULT_RENAME_GROUP) &&
        RecolorGroup.Equals(DEFAULT_RECOLOR_GROUP) &&
        CollapseGroup.Equals(DEFAULT_COLLAPSE_GROUP) &&
        ExpandGroup.Equals(DEFAULT_EXPAND_GROUP);

    #endregion

    #region Implementation

    /// <summary>Resets the strings to their default values.</summary>
    public void Reset()
    {
        NewWindow = DEFAULT_NEW_WINDOW;
        MoveToNewWindow = DEFAULT_MOVE_TO_NEW_WINDOW;
        CloseTab = DEFAULT_CLOSE_TAB;
        CloseOtherTabs = DEFAULT_CLOSE_OTHER_TABS;
        CloseTabsToTheRight = DEFAULT_CLOSE_TABS_TO_THE_RIGHT;
        NewTabButton = DEFAULT_NEW_TAB_BUTTON;
        NewTab = DEFAULT_NEW_TAB;
        AddToGroup = DEFAULT_ADD_TO_GROUP;
        NewGroup = DEFAULT_NEW_GROUP;
        Ungroup = DEFAULT_UNGROUP;
        RenameGroup = DEFAULT_RENAME_GROUP;
        RecolorGroup = DEFAULT_RECOLOR_GROUP;
        CollapseGroup = DEFAULT_COLLAPSE_GROUP;
        ExpandGroup = DEFAULT_EXPAND_GROUP;
    }

    #endregion

    #region Properties

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Text shown by tear-out drag feedback.")]
    [DefaultValue(DEFAULT_NEW_WINDOW)]
    public string NewWindow { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for moving a tab into a new window.")]
    [DefaultValue(DEFAULT_MOVE_TO_NEW_WINDOW)]
    public string MoveToNewWindow { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for closing a tab.")]
    [DefaultValue(DEFAULT_CLOSE_TAB)]
    public string CloseTab { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for closing all tabs except the selected tab.")]
    [DefaultValue(DEFAULT_CLOSE_OTHER_TABS)]
    public string CloseOtherTabs { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for closing tabs to the right of the selected tab.")]
    [DefaultValue(DEFAULT_CLOSE_TABS_TO_THE_RIGHT)]
    public string CloseTabsToTheRight { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Compact glyph shown on the optional caption new-tab button (typically '+').")]
    [DefaultValue(DEFAULT_NEW_TAB_BUTTON)]
    public string NewTabButton { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tooltip and menu label for creating a new tab (caption '+' shows NewTabButton only).")]
    [DefaultValue(DEFAULT_NEW_TAB)]
    public string NewTab { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu submenu for assigning a tab to a group.")]
    [DefaultValue(DEFAULT_ADD_TO_GROUP)]
    public string AddToGroup { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for creating a new tab group.")]
    [DefaultValue(DEFAULT_NEW_GROUP)]
    public string NewGroup { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for removing a tab from its group.")]
    [DefaultValue(DEFAULT_UNGROUP)]
    public string Ungroup { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for renaming a tab group.")]
    [DefaultValue(DEFAULT_RENAME_GROUP)]
    public string RenameGroup { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for changing a tab group color.")]
    [DefaultValue(DEFAULT_RECOLOR_GROUP)]
    public string RecolorGroup { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for collapsing a tab group.")]
    [DefaultValue(DEFAULT_COLLAPSE_GROUP)]
    public string CollapseGroup { get; set; }

    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Caption-tab context menu command for expanding a tab group.")]
    [DefaultValue(DEFAULT_EXPAND_GROUP)]
    public string ExpandGroup { get; set; }

    #endregion
}
