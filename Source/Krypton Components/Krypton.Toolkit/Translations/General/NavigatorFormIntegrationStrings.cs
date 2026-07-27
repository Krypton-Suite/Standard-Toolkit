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
        NewTab.Equals(DEFAULT_NEW_TAB);

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

    #endregion
}
