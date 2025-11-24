#region BSD License
/*
 *
 *    BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

internal class DesignTimeUtilities
{
    #region Design Time Constants

    #region ButtonStyles

    internal const string DEFAULT_BUTTON_SPEC_STYLE_ALTERNATE = @"Alternate";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_STANDALONE = @"Standalone";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_LOW_PROFILE = @"Low Profile";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_BUTTON_SPEC = @"ButtonSpec";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_BREAD_CRUMB = @"Bread Crumb";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_CALENDAR_DAY = @"Calendar Day";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_CLUSTER = @"Cluster";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_GALLERY = @"Gallery";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_STACK = @"Navigator Stack";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_OVERFLOW = @"Navigator Overflow";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_MINI = @"Navigator Mini";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_INPUT_CONTROL = @"Input Control";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_LIST_ITEM = @"List Item";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_FORM = @"Form";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_FORM_CLOSE = @"Form Close";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_COMMAND = @"Command";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_ONE = @"Custom 1";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_TWO = @"Custom 2";
    internal const string DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_THREE = @"Custom 3";

    #endregion

    #region DataGridViewStyles

    internal const string DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_ONE = @"Custom 1";
    internal const string DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_TWO = @"Custom 2";
    internal const string DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_THREE = @"Custom 3";
    internal const string DEFAULT_DATA_GRID_VIEW_STYLE_MIXED = @"Mixed";
    internal const string DEFAULT_DATA_GRID_VIEW_STYLE_LIST = @"List";
    internal const string DEFAULT_DATA_GRID_VIEW_STYLE_SHEET = @"Sheet";

    #endregion

    #region GridStyles

    internal const string DEFAULT_GRID_STYLE_CUSTOM_ONE = @"Custom 1";
    internal const string DEFAULT_GRID_STYLE_CUSTOM_TWO = @"Custom 2";
    internal const string DEFAULT_GRID_STYLE_CUSTOM_THREE = @"Custom 3";
    internal const string DEFAULT_GRID_STYLE_LIST = @"List";
    internal const string DEFAULT_GRID_STYLE_SHEET = @"Sheet";

    #endregion

    #region HeaderGroupCollapsedTarget

    internal const string DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_BOTH = @"Collapse to Both Headers";
    internal const string DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_PRIMARY = @"Collapse to Primary Header";
    internal const string DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_SECONDARY = @"Collapse to Secondary Header";

    #endregion

    #region HeaderStyles

    internal const string DEFAULT_HEADER_STYLE_CALENDAR = @"Calendar";
    internal const string DEFAULT_HEADER_STYLE_CUSTOM_ONE = @"Custom 1";
    internal const string DEFAULT_HEADER_STYLE_CUSTOM_TWO = @"Custom 2";
    internal const string DEFAULT_HEADER_STYLE_CUSTOM_THREE = @"Custom 3";
    internal const string DEFAULT_HEADER_STYLE_DOCK_ACTIVE = @"Dock - Active";
    internal const string DEFAULT_HEADER_STYLE_DOCK_INACTIVE = @"Dock - Inactive";
    internal const string DEFAULT_HEADER_STYLE_FORM = @"Form";
    internal const string DEFAULT_HEADER_STYLE_PRIMARY = @"Primary";
    internal const string DEFAULT_HEADER_STYLE_SECONDARY = @"Secondary";

    #endregion

    #region InputControlStyles

    internal const string DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_ONE = @"Custom 1";
    internal const string DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_TWO = @"Custom 2";
    internal const string DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_THREE = @"Custom 3";
    internal const string DEFAULT_INPUT_CONTROL_STYLE_RIBBON = @"Ribbon";
    internal const string DEFAULT_INPUT_CONTROL_STYLE_PANEL_ALTERNATE = @"Panel Alternate";
    internal const string DEFAULT_INPUT_CONTROL_STYLE_PANEL_CLIENT = @"Panel Client";
    internal const string DEFAULT_INPUT_CONTROL_STYLE_STANDALONE = @"Standalone";

    #endregion

    #region IntegratedToolBar

    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEW = @"New";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_OPEN = @"Open";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE = @"Save";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_AS = @"Save As";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_ALL = @"Save All";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CUT = @"Cut";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_COPY = @"Copy";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PASTE = @"Paste";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_UNDO = @"Undo";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_REDO = @"Redo";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PAGE_SETUP = @"Page Setup";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT_PREVIEW = @"Print Preview";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT = @"Print";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_QUICK_PRINT = @"Quick Print";

    #endregion

    #region KryptonLinkBehavior

    internal const string DEFAULT_LINK_BEHAVIOR_ALWAYS_UNDERLINE = @"Always Underline";
    internal const string DEFAULT_LINK_BEHAVIOR_HOVER_UNDERLINE = @"Hover Underline";
    internal const string DEFAULT_LINK_BEHAVIOR_NEVER_UNDERLINE = @"Never Underline";

    #endregion

    #region LabelStyles

    internal const string DEFAULT_LABEL_STYLE_BOLD_CONTROL = @"Bold (Control)";
    internal const string DEFAULT_LABEL_STYLE_BOLD_PANEL = @"Bold (Panel)";
    internal const string DEFAULT_LABEL_STYLE_CUSTOM_ONE = @"Custom 1";
    internal const string DEFAULT_LABEL_STYLE_CUSTOM_TWO = @"Custom 2";
    internal const string DEFAULT_LABEL_STYLE_CUSTOM_THREE = @"Custom 3";
    internal const string DEFAULT_LABEL_STYLE_GROUP_BOX_CAPTION = @"Caption (Panel)";
    internal const string DEFAULT_LABEL_STYLE_ALTERNATE_CONTROL = @"Alternate (Control)";
    internal const string DEFAULT_LABEL_STYLE_NORMAL_CONTROL = @"Normal (Control)";
    internal const string DEFAULT_LABEL_STYLE_ALTERNATE_PANEL = @"Alternate (Panel)";
    internal const string DEFAULT_LABEL_STYLE_NORMAL_PANEL = @"Normal (Panel)";
    internal const string DEFAULT_LABEL_STYLE_TITLE_CONTROL = @"Title (Control)";
    internal const string DEFAULT_LABEL_STYLE_TITLE_PANEL = @"Title (Panel)";
    internal const string DEFAULT_LABEL_STYLE_ITALIC_CONTROL = @"Italic (Control)";
    internal const string DEFAULT_LABEL_STYLE_ITALIC_PANEL = @"Italic (Panel)";
    internal const string DEFAULT_LABEL_STYLE_TOOL_TIP = @"ToolTip";
    internal const string DEFAULT_LABEL_STYLE_SUPER_TIP = @"SuperTip";
    internal const string DEFAULT_LABEL_STYLE_KEY_TIP = @"KeyTip";

    #endregion

    #region PaletteBackStyles

    internal const string DEFAULT_BUTTON_STANDALONE = @"Button - Standalone";
    internal const string DEFAULT_BUTTON_ALTERNATE = @"Button - Alternate";
    internal const string DEFAULT_BUTTON_LOW_PROFILE = @"Button - Low Profile";
    internal const string DEFAULT_BUTTON_BUTTON_SPEC = @"Button - ButtonSpec";
    internal const string DEFAULT_BUTTON_BREAD_CRUMB = @"Button - BreadCrumb";
    internal const string DEFAULT_BUTTON_CALENDAR_DAY = @"Button - Calendar Day";
    internal const string DEFAULT_BUTTON_CLUSTER = @"Button - Cluster";
    internal const string DEFAULT_BUTTON_GALLERY = @"Button - Gallery";
    internal const string DEFAULT_BUTTON_NAVIGATOR_STACK = @"Button - Navigator Stack";
    internal const string DEFAULT_BUTTON_NAVIGATOR_OVERFLOW = @"Button - Navigator Overflow";
    internal const string DEFAULT_BUTTON_NAVIGATOR_MINI = @"Button - Navigator Mini";
    internal const string DEFAULT_BUTTON_INPUT_CONTROL = @"Button - Input Control";
    internal const string DEFAULT_BUTTON_LIST_ITEM = @"Button - List Item";
    internal const string DEFAULT_BUTTON_FORM = @"Button - Form";
    internal const string DEFAULT_BUTTON_FORM_CLOSE = @"Button - Form Close";
    internal const string DEFAULT_BUTTON_COMMAND = @"Button - Command";
    internal const string DEFAULT_BUTTON_CUSTOM1 = @"Button - Custom1";
    internal const string DEFAULT_BUTTON_CUSTOM2 = @"Button - Custom2";
    internal const string DEFAULT_BUTTON_CUSTOM3 = @"Button - Custom3";
    internal const string DEFAULT_CONTROL_CLIENT = @"Control - Client";
    internal const string DEFAULT_CONTROL_ALTERNATE = @"Control - Alternate";
    internal const string DEFAULT_CONTROL_GROUP_BOX = @"Control - GroupBox";
    internal const string DEFAULT_CONTROL_TOOL_TIP = @"Control - ToolTip";
    internal const string DEFAULT_CONTROL_RIBBON = @"Control - Ribbon";
    internal const string DEFAULT_CONTROL_RIBBON_APP_MENU = @"Control - RibbonAppMenu";
    internal const string DEFAULT_CONTROL_CUSTOM1 = @"Control - Custom1";
    internal const string DEFAULT_CONTROL_CUSTOM2 = @"Control - Custom2";
    internal const string DEFAULT_CONTROL_CUSTOM3 = @"Control - Custom3";
    internal const string DEFAULT_CONTEXT_MENU_OUTER = @"ContextMenu - Outer";
    internal const string DEFAULT_CONTEXT_MENU_INNER = @"ContextMenu - Inner";
    internal const string DEFAULT_CONTEXT_MENU_HEADING = @"ContextMenu - Heading";
    internal const string DEFAULT_CONTEXT_MENU_SEPARATOR = @"ContextMenu - Separator";
    internal const string DEFAULT_CONTEXT_MENU_ITEM_SPLIT = @"ContextMenu - Item Split";
    internal const string DEFAULT_CONTEXT_MENU_ITEM_IMAGE = @"ContextMenu - Item Image";
    internal const string DEFAULT_CONTEXT_MENU_ITEM_IMAGE_COLUMN = @"ContextMenu - Item ImageColumn";
    internal const string DEFAULT_CONTEXT_MENU_ITEM_HIGHLIGHT = @"ContextMenu - Item Highlight";
    internal const string DEFAULT_INPUT_CONTROL_STANDALONE = @"InputControl - Standalone";
    internal const string DEFAULT_INPUT_CONTROL_RIBBON = @"InputControl - Ribbon";
    internal const string DEFAULT_INPUT_CONTROL_CUSTOM1 = @"InputControl - Custom1";
    internal const string DEFAULT_INPUT_CONTROL_CUSTOM2 = @"InputControl - Custom2";
    internal const string DEFAULT_INPUT_CONTROL_CUSTOM3 = @"InputControl - Custom3";
    internal const string DEFAULT_FORM_MAIN = @"Form - Main";
    internal const string DEFAULT_FORM_CUSTOM1 = @"Form - Custom1";
    internal const string DEFAULT_FORM_CUSTOM2 = @"Form - Custom2";
    internal const string DEFAULT_FORM_CUSTOM3 = @"Form - Custom3";
    internal const string DEFAULT_GRID_HEADER_COLUMN_LIST = @"Grid - HeaderColumn - List";
    internal const string DEFAULT_GRID_HEADER_ROW_LIST = @"Grid - HeaderRow - List";
    internal const string DEFAULT_GRID_DATA_CELL_LIST = @"Grid - DataCell - List";
    internal const string DEFAULT_GRID_BACKGROUND_LIST = @"Grid - Background - List";
    internal const string DEFAULT_GRID_HEADER_COLUMN_SHEET = @"Grid - HeaderColumn - Sheet";
    internal const string DEFAULT_GRID_HEADER_ROW_SHEET = @"Grid - HeaderRow - Sheet";
    internal const string DEFAULT_GRID_DATA_CELL_SHEET = @"Grid - DataCell - Sheet";
    internal const string DEFAULT_GRID_BACKGROUND_SHEET = @"Grid - Background - Sheet";
    internal const string DEFAULT_GRID_HEADER_COLUMN_CUSTOM1 = @"Grid - HeaderColumn - Custom1";
    internal const string DEFAULT_GRID_HEADER_COLUMN_CUSTOM2 = @"Grid - HeaderColumn - Custom2";
    internal const string DEFAULT_GRID_HEADER_COLUMN_CUSTOM3 = @"Grid - HeaderColumn - Custom3";
    internal const string DEFAULT_GRID_HEADER_ROW_CUSTOM1 = @"Grid - HeaderRow - Custom1";
    internal const string DEFAULT_GRID_HEADER_ROW_CUSTOM2 = @"Grid - HeaderRow - Custom2";
    internal const string DEFAULT_GRID_HEADER_ROW_CUSTOM3 = @"Grid - HeaderRow - Custom3";
    internal const string DEFAULT_GRID_DATA_CELL_CUSTOM1 = @"Grid - DataCell - Custom1";
    internal const string DEFAULT_GRID_DATA_CELL_CUSTOM2 = @"Grid - DataCell - Custom2";
    internal const string DEFAULT_GRID_DATA_CELL_CUSTOM3 = @"Grid - DataCell - Custom3";
    internal const string DEFAULT_GRID_BACKGROUND_CUSTOM1 = @"Grid - Background - Custom1";
    internal const string DEFAULT_GRID_BACKGROUND_CUSTOM2 = @"Grid - Background - Custom2";
    internal const string DEFAULT_GRID_BACKGROUND_CUSTOM3 = @"Grid - Background - Custom3";
    internal const string DEFAULT_HEADER_PRIMARY = @"Header - Primary";
    internal const string DEFAULT_HEADER_SECONDARY = @"Header - Secondary";
    internal const string DEFAULT_HEADER_DOCK_ACTIVE = @"Header - Dock - Active";
    internal const string DEFAULT_HEADER_DOCK_INACTIVE = @"Header - Dock - Inactive";
    internal const string DEFAULT_HEADER_FORM = @"Header - Form";
    internal const string DEFAULT_HEADER_CALENDAR = @"Header - Calendar";
    internal const string DEFAULT_HEADER_CUSTOM1 = @"Header - Custom1";
    internal const string DEFAULT_HEADER_CUSTOM2 = @"Header - Custom2";
    internal const string DEFAULT_HEADER_CUSTOM3 = @"Header - Custom3";
    internal const string DEFAULT_PANEL_CLIENT = @"Panel - Client";
    internal const string DEFAULT_PANEL_ALTERNATE = @"Panel - Alternate";
    internal const string DEFAULT_PANEL_RIBBON_INACTIVE = @"Panel - Ribbon Inactive";
    internal const string DEFAULT_PANEL_CUSTOM1 = @"Panel - Custom1";
    internal const string DEFAULT_PANEL_CUSTOM2 = @"Panel - Custom2";
    internal const string DEFAULT_PANEL_CUSTOM3 = @"Panel - Custom3";
    internal const string DEFAULT_SEPARATOR_LOW_PROFILE = @"Separator - Low Profile";
    internal const string DEFAULT_SEPARATOR_HIGH_PROFILE = @"Separator - High Profile";
    internal const string DEFAULT_SEPARATOR_HIGH_INTERNAL_PROFILE = @"Separator - High Internal Profile";
    internal const string DEFAULT_TAB_HIGH_PROFILE = @"Tab - High Profile";
    internal const string DEFAULT_TAB_STANDARD_PROFILE = @"Tab - Standard Profile";
    internal const string DEFAULT_TAB_LOW_PROFILE = @"Tab - Low Profile";
    internal const string DEFAULT_TAB_ONE_NOTE = @"Tab - OneNote";
    internal const string DEFAULT_TAB_DOCK = @"Tab - Dock";
    internal const string DEFAULT_TAB_DOCK_AUTO_HIDDEN = @"Tab - Dock AutoHidden";
    internal const string DEFAULT_TAB_CUSTOM1 = @"Tab - Custom1";
    internal const string DEFAULT_TAB_CUSTOM2 = @"Tab - Custom2";
    internal const string DEFAULT_TAB_CUSTOM3 = @"Tab - Custom3";
    internal const string DEFAULT_CONTROL = @"System - Control";

    #endregion

    #region PaletteBorderStyles

    internal const string DEFAULT_PALETTE_BORDER_BUTTON_STANDALONE = @"Button - Standalone";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_ALTERNATE = @"Button - Alternate";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_LOW_PROFILE = @"Button - Low Profile";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_BUTTON_SPEC = @"Button - ButtonSpec";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_BREAD_CRUMB = @"Button - BreadCrumb";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_CALENDAR_DAY = @"Button - Calendar Day";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_CLUSTER = @"Button - Cluster";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_GALLERY = @"Button - Gallery";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_NAVIGATOR_STACK = @"Button - Navigator Stack";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_NAVIGATOR_OVERFLOW = @"Button - Navigator Overflow";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_NAVIGATOR_MINI = @"Button - Navigator Mini";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_INPUT_CONTROL = @"Button - Input Control";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_LIST_ITEM = @"Button - List Item";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_FORM = @"Button - Form";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_FORM_CLOSE = @"Button - Form Close";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_COMMAND = @"Button - Command";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_CUSTOM1 = @"Button - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_CUSTOM2 = @"Button - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_BUTTON_CUSTOM3 = @"Button - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_CLIENT = @"Control - Client";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_ALTERNATE = @"Control - Alternate";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_GROUP_BOX = @"Control - GroupBox";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_TOOL_TIP = @"Control - ToolTip";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_RIBBON = @"Control - Ribbon";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_RIBBON_APP_MENU = @"Control - RibbonAppMenu";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_CUSTOM1 = @"Control - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_CUSTOM2 = @"Control - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_CONTROL_CUSTOM3 = @"Control - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_OUTER = @"ContextMenu - Outer";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_INNER = @"ContextMenu - Inner";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_HEADING = @"ContextMenu - Heading";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_SEPARATOR = @"ContextMenu - Separator";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_SPLIT = @"ContextMenu - Item Split";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_IMAGE = @"ContextMenu - Item Image";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_IMAGE_COLUMN = @"ContextMenu - Item ImageColumn";
    internal const string DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_HIGHLIGHT = @"ContextMenu - Item Highlight";
    internal const string DEFAULT_PALETTE_BORDER_INPUT_CONTROL_STANDALONE = @"InputControl - Standalone";
    internal const string DEFAULT_PALETTE_BORDER_INPUT_CONTROL_RIBBON = @"InputControl - Ribbon";
    internal const string DEFAULT_PALETTE_BORDER_INPUT_CONTROL_CUSTOM1 = @"InputControl - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_INPUT_CONTROL_CUSTOM2 = @"InputControl - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_INPUT_CONTROL_CUSTOM3 = @"InputControl - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_FORM_MAIN = @"Form - Main";
    internal const string DEFAULT_PALETTE_BORDER_FORM_CUSTOM1 = @"Form - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_FORM_CUSTOM2 = @"Form - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_FORM_CUSTOM3 = @"Form - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_LIST = @"Grid - HeaderColumn - List";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_LIST = @"Grid - HeaderRow - List";
    internal const string DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_LIST = @"Grid - DataCell - List";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_SHEET = @"Grid - HeaderColumn - Sheet";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_SHEET = @"Grid - HeaderRow - Sheet";
    internal const string DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_SHEET = @"Grid - DataCell - Sheet";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_CUSTOM1 = @"Grid - HeaderColumn - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_CUSTOM2 = @"Grid - HeaderColumn - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_CUSTOM3 = @"Grid - HeaderColumn - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_CUSTOM1 = @"Grid - HeaderRow - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_CUSTOM2 = @"Grid - HeaderRow - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_CUSTOM3 = @"Grid - HeaderRow - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_CUSTOM1 = @"Grid - DataCell - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_CUSTOM2 = @"Grid - DataCell - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_CUSTOM3 = @"Grid - DataCell - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_PRIMARY = @"Header - Primary";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_SECONDARY = @"Header - Secondary";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_DOCK_ACTIVE = @"Header - Dock - Active";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_DOCK_INACTIVE = @"Header - Dock - Inactive";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_FORM = @"Header - Form";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_CALENDAR = @"Header - Calendar";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_CUSTOM1 = @"Header - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_CUSTOM2 = @"Header - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_HEADER_CUSTOM3 = @"Header - Custom3";
    internal const string DEFAULT_PALETTE_BORDER_SEPARATOR_LOW_PROFILE = @"Separator - Low Profile";
    internal const string DEFAULT_PALETTE_BORDER_SEPARATOR_HIGH_PROFILE = @"Separator - High Profile";
    internal const string DEFAULT_PALETTE_BORDER_SEPARATOR_HIGH_INTERNAL_PROFILE = @"Separator - High Internal Profile";
    internal const string DEFAULT_PALETTE_BORDER_TAB_HIGH_PROFILE = @"Tab - High Profile";
    internal const string DEFAULT_PALETTE_BORDER_TAB_STANDARD_PROFILE = @"Tab - Standard Profile";
    internal const string DEFAULT_PALETTE_BORDER_TAB_LOW_PROFILE = @"Tab - Low Profile";
    internal const string DEFAULT_PALETTE_BORDER_TAB_ONE_NOTE = @"Tab - OneNote";
    internal const string DEFAULT_PALETTE_BORDER_TAB_DOCK = @"Tab - Dock";
    internal const string DEFAULT_PALETTE_BORDER_TAB_DOCK_AUTO_HIDDEN = @"Tab - Dock AutoHidden";
    internal const string DEFAULT_PALETTE_BORDER_TAB_CUSTOM1 = @"Tab - Custom1";
    internal const string DEFAULT_PALETTE_BORDER_TAB_CUSTOM2 = @"Tab - Custom2";
    internal const string DEFAULT_PALETTE_BORDER_TAB_CUSTOM3 = @"Tab - Custom3";

    #endregion

    #region PaletteButtonOrientation

    internal const string DEFAULT_PALETTE_BUTTON_ORIENTATION_AUTO = @"Auto";
    internal const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_BOTTOM = @"Fixed Bottom";
    internal const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_TOP = @"Fixed Top";
    internal const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_LEFT = @"Fixed Left";
    internal const string DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_RIGHT = @"Fixed Right";
    internal const string DEFAULT_PALETTE_BUTTON_ORIENTATION_INHERIT = @"Inherit";

    #endregion

    #region PaletteButtonSpecStyles

    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CLOSE = @"Close";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CONTEXT = @"Context";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEXT = @"Next";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PREVIOUS = @"Previous";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_GENERIC = @"Generic";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_LEFT = @"Arrow Left";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_RIGHT = @"Arrow Right";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_UP = @"Arrow Up";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_DOWN = @"Arrow Down";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_DROP_DOWN = @"drop-down";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_VERTICAL = @"Pin Vertical";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_HORIZONTAL = @"Pin Horizontal";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_CLOSE = @"Form Close";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MAX = @"Form Max";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MIN = @"Form Min";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_RESTORE = @"Form Restore";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_HELP = @"Form Help";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_CLOSE = @"Pendant Close";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_MIN = @"Pendant Min";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_RESTORE = @"Pendant Restore";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_MAXIMIZE = @"Workspace Maximize";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_RESTORE = @"Workspace Restore";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_MINIMIZE = @"Ribbon Minimize";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_EXPAND = @"Ribbon Expand";

    #endregion

    #region PaletteButtonStyles

    internal const string DEFAULT_PALETTE_BUTTON_STYLE_INHERIT = @"Inherit";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_STANDALONE = @"Standalone";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_ALTERNATE = @"Alternate";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_LOW_PROFILE = @"Low Profile";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_BREAD_CRUMB = @"BreadCrumb";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_CLUSTER = @"Cluster";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_STACK = @"Navigator Stack";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_OVERFLOW = @"Navigator Overflow";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_MINI = @"Navigator Mini";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_INPUT_CONTROL = @"Input Control";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_LIST_ITEM = @"List Item";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_FORM = @"Form";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_FORM_CLOSE = @"Form Close";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_BUTTON_SPEC = @"ButtonSpec";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_COMMAND = @"Command";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM1 = @"Custom 1";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM2 = @"Custom 2";
    internal const string DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM3 = @"Custom 3";

    #endregion

    #region PaletteContentStyles

    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_STANDALONE = @"Button - Standalone";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LOW_PROFILE = @"Button - Low Profile";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BUTTON_SPEC = @"Button - ButtonSpec";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BREAD_CRUMB = @"Button - BreadCrumb";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CALENDAR_DAY = @"Button - Calendar Day";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CLUSTER = @"Button - Cluster";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_GALLERY = @"Button - Gallery";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_STACK = @"Button - Navigator Stack";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_OVERFLOW = @"Button - Navigator Overflow";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_MINI = @"Button - Navigator Mini";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_INPUT_CONTROL = @"Button - Input Control";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LIST_ITEM = @"Button - List Item";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM = @"Button - Form";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM_CLOSE = @"Button - Form Close";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_COMMAND = @"Button - Command";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM1 = @"Button - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM2 = @"Button - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM3 = @"Button - Custom3";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_HEADING = @"ContextMenu - Heading";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_IMAGE = @"ContextMenu - Item Image";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_STANDARD = @"ContextMenu - Item Text Standard";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_ALTERNATE = @"ContextMenu - Item Text Alternate";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_SHORTCUT_TEXT = @"ContextMenu - Item ShortcutText";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_LIST = @"Grid - HeaderColumn - List";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_LIST = @"Grid - RowColumn - List";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_LIST = @"Grid - DataCell - List";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_SHEET = @"Grid - HeaderColumn - Sheet";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_SHEET = @"Grid - RowColumn - Sheet";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_SHEET = @"Grid - DataCell - Sheet";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM1 = @"Grid - HeaderColumn - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM2 = @"Grid - HeaderColumn - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM3 = @"Grid - HeaderColumn - Custom3";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM1 = @"Grid - RowColumn - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM2 = @"Grid - RowColumn - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM3 = @"Grid - RowColumn - Custom3";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM1 = @"Grid - DataCell - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM2 = @"Grid - DataCell - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM3 = @"Grid - DataCell - Custom3";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_PRIMARY = @"Header - Primary";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_SECONDARY = @"Header - Secondary";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_DOCK_ACTIVE = @"Header - Dock - Active";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_DOCK_INACTIVE = @"Header - Dock - Inactive";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_FORM = @"Header - Form";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CALENDAR = @"Header - Calendar";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM1 = @"Header - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM2 = @"Header - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM3 = @"Header - Custom3";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ALTERNATE_CONTROL = @"Label - Alternate (Control)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_NORMAL_CONTROL = @"Label - Normal (Control)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_BOLD_CONTROL = @"Label - Bold (Control)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ITALIC_CONTROL = @"Label - Italic (Control)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TITLE_CONTROL = @"Label - Title (Control)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ALTERNATE_PANEL = @"Label - Alternate (Panel)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_NORMAL_PANEL = @"Label - Normal (Panel)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_BOLD_PANEL = @"Label - Bold (Panel)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ITALIC_PANEL = @"Label - Italic (Panel)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TITLE_PANEL = @"Label - Title (Panel)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_GROUP_BOX_CAPTION = @"Label - Caption (Panel)";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TOOL_TIP = @"Label - ToolTip";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_SUPER_TIP = @"Label - SuperTip";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_KEY_TIP = @"Label - KeyTip";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_CUSTOM1 = @"Label - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_CUSTOM2 = @"Label - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_HIGH_PROFILE = @"Tab - High Profile";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_STANDARD_PROFILE = @"Tab - Standard Profile";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_LOW_PROFILE = @"Tab - Low Profile";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_ONE_NOTE = @"Tab - OneNote";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_DOCK = @"Tab - Dock";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_DOCK_AUTO_HIDDEN = @"Tab - Dock AutoHidden";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM1 = @"Tab - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM2 = @"Tab - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM3 = @"Tab - Custom3";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_STANDALONE = @"InputControl - Standalone";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_RIBBON = @"InputControl - Ribbon";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM1 = @"InputControl - Custom1";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM2 = @"InputControl - Custom2";
    internal const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM3 = @"InputControl - Custom3";

    #endregion

    #region PaletteImageEffect

    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_INHERIT = @"Inherit";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT = @"Light";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT_LIGHT = @"LightLight";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_NORMAL = @"Normal";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_DISABLED = @"Disabled";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_DARK = @"Dark";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_DARK_DARK = @"DarkDark";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE = @"GrayScale";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_RED = @"GrayScale - Red";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_GREEN = @"GrayScale - Green";
    internal const string DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_BLUE = @"GrayScale - Blue";

    #endregion

    #region PaletteImageStyles

    internal const string DEFAULT_PALETTE_IMAGE_STYLE_INHERIT = @"Inherit";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_STRETCH = @"Stretch";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_TILE = @"Tile";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X = @"TileFlip - X";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_Y = @"TileFlip - Y";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X_Y = @"TileFlip - XY";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_TOP_LEFT = @"Top - Left";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_TOP_MIDDLE = @"Top - Middle";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_TOP_RIGHT = @"Top - Right";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_CENTER_LEFT = @"Center - Left";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_CENTER_MIDDLE = @"Center - Middle";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_CENTER_RIGHT = @"Center - Right";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_LEFT = @"Bottom - Left";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_MIDDLE = @"Bottom - Middle";
    internal const string DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_RIGHT = @"Bottom - Right";

    #endregion

    #region PaletteMode

    internal const string DEFAULT_PALETTE_SYSTEM = @"Professional - System";
    internal const string DEFAULT_PALETTE_OFFICE_2003 = @"Professional - Office 2003";
    internal const string DEFAULT_PALETTE_OFFICE_2007_BLACK = @"Office 2007 - Black";
    internal const string DEFAULT_PALETTE_OFFICE_2007_BLUE = @"Office 2007 - Blue";
    internal const string DEFAULT_PALETTE_OFFICE_2007_SILVER = @"Office 2007 - Silver";
    internal const string DEFAULT_PALETTE_OFFICE_2007_WHITE = @"Office 2007 - White";
    internal const string DEFAULT_PALETTE_OFFICE_2007_BLACK_DARK_MODE = @"Office 2007 - Black (Dark Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2007_BLUE_DARK_MODE = @"Office 2007 - Blue (Dark Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2007_SILVER_DARK_MODE = @"Office 2007 - Silver (Dark Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2007_DARK_GRAY = @"Office 2007 - Dark Gray";
    internal const string DEFAULT_PALETTE_OFFICE_2007_BLUE_LIGHT_MODE = @"Office 2007 - Blue (Light Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2007_SILVER_LIGHT_MODE = @"Office 2007 - Silver (Light Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2007_LIGHT_GRAY = @"Office 2007 - Light Gray";
    internal const string DEFAULT_PALETTE_OFFICE_2010_BLACK = @"Office 2010 - Black";
    internal const string DEFAULT_PALETTE_OFFICE_2010_BLUE = @"Office 2010 - Blue";
    internal const string DEFAULT_PALETTE_OFFICE_2010_SILVER = @"Office 2010 - Silver";
    internal const string DEFAULT_PALETTE_OFFICE_2010_WHITE = @"Office 2010 - White";
    internal const string DEFAULT_PALETTE_OFFICE_2010_BLACK_DARK_MODE = @"Office 2010 - Black (Dark Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2010_BLUE_DARK_MODE = @"Office 2010 - Blue (Dark Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2010_SILVER_DARK_MODE = @"Office 2010 - Silver (Dark Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2010_DARK_GRAY = @"Office 2010 - Dark Gray";
    internal const string DEFAULT_PALETTE_OFFICE_2010_BLUE_LIGHT_MODE = @"Office 2010 - Blue (Light Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2010_SILVER_LIGHT_MODE = @"Office 2010 - Silver (Light Mode)";
    internal const string DEFAULT_PALETTE_OFFICE_2010_LIGHT_GRAY = @"Office 2010 - Light Gray";
    internal const string DEFAULT_PALETTE_OFFICE_2013_WHITE = @"Office 2013 - White";
    internal const string DEFAULT_PALETTE_OFFICE_2013_DARK_GRAY = @"Office 2013 - Dark Gray";
    internal const string DEFAULT_PALETTE_OFFICE_2013_LIGHT_GRAY = @"Office 2013 - Light Gray";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_BLACK = @"Microsoft 365 - Black";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_BLUE = @"Microsoft 365 - Blue";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_SILVER = @"Microsoft 365 - Silver";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_WHITE = @"Microsoft 365 - White";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_BLACK_DARK_MODE = @"Microsoft 365 - Black (Dark Mode)";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_BLUE_DARK_MODE = @"Microsoft 365 - Blue (Dark Mode)";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_SILVER_DARK_MODE = @"Microsoft 365 - Silver (Dark Mode)";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_DARK_GRAY = @"Microsoft 365 - Dark Gray";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_BLUE_LIGHT_MODE = @"Microsoft 365 - Blue (Light Mode)";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_SILVER_LIGHT_MODE = @"Microsoft 365 - Silver (Light Mode)";
    internal const string DEFAULT_PALETTE_MICROSOFT_365_LIGHT_GRAY = @"Microsoft 365 - Light Gray";
    internal const string DEFAULT_PALETTE_SPARKLE_BLUE = @"Sparkle - Blue";
    internal const string DEFAULT_PALETTE_SPARKLE_ORANGE = @"Sparkle - Orange";
    internal const string DEFAULT_PALETTE_SPARKLE_PURPLE = @"Sparkle - Purple";
    internal const string DEFAULT_PALETTE_SPARKLE_BLUE_DARK_MODE = @"Sparkle - Blue (Dark Mode)";
    internal const string DEFAULT_PALETTE_SPARKLE_ORANGE_DARK_MODE = @"Sparkle - Orange (Dark Mode)";
    internal const string DEFAULT_PALETTE_SPARKLE_PURPLE_DARK_MODE = @"Sparkle - Purple (Dark Mode)";
    internal const string DEFAULT_PALETTE_SPARKLE_BLUE_LIGHT_MODE = @"Sparkle - Blue (Light Mode)";
    internal const string DEFAULT_PALETTE_SPARKLE_ORANGE_LIGHT_MODE = @"Sparkle - Orange (Light Mode)";
    internal const string DEFAULT_PALETTE_SPARKLE_PURPLE_LIGHT_MODE = @"Sparkle - Purple (Light Mode)";
    internal const string DEFAULT_PALETTE_CUSTOM = @"Custom";

    #endregion

    #region PaletteTextTrim

    internal const string DEFAULT_PALETTE_TEXT_TRIM_INHERIT = @"Inherit";
    internal const string DEFAULT_PALETTE_TEXT_TRIM_HIDE = @"Hide";
    internal const string DEFAULT_PALETTE_TEXT_TRIM_CHARACTER = @"Character";
    internal const string DEFAULT_PALETTE_TEXT_TRIM_WORD = @"Word";
    internal const string DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_CHARACTER = @"Ellipsis Character";
    internal const string DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_WORD = @"Ellipsis Word";
    internal const string DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_PATH = @"Ellipsis Path";

    #endregion

    #region PlacementMode

    internal const string DEFAULT_PLACEMENT_MODE_ABSOLUTE = @"Placement Mode - Absolute";
    internal const string DEFAULT_PLACEMENT_MODE_ABSOLUTE_POINT = @"Placement Mode - Absolute Point";
    internal const string DEFAULT_PLACEMENT_MODE_BOTTOM = @"Placement Mode - Bottom";
    internal const string DEFAULT_PLACEMENT_MODE_CENTER = @"Placement Mode - Center";
    internal const string DEFAULT_PLACEMENT_MODE_LEFT = @"Placement Mode - Left";
    internal const string DEFAULT_PLACEMENT_MODE_MOUSE = @"Placement Mode - Mouse";
    internal const string DEFAULT_PLACEMENT_MODE_MOUSE_POINT = @"Placement Mode - Mouse Point";
    internal const string DEFAULT_PLACEMENT_MODE_RELATIVE = @"Placement Mode - Relative";
    internal const string DEFAULT_PLACEMENT_MODE_RELATIVE_POINT = @"Placement Mode - Relative Point";
    internal const string DEFAULT_PLACEMENT_MODE_RIGHT = @"Placement Mode - Right";
    internal const string DEFAULT_PLACEMENT_MODE_TOP = @"Placement Mode - Top";

    #endregion

    #region SeperatorStyles

    internal const string DEFAULT_SEPARATOR_STYLE_LOW_PROFILE = @"Low Profile";
    internal const string DEFAULT_SEPARATOR_STYLE_HIGH_PROFILE = @"High Profile";
    internal const string DEFAULT_SEPARATOR_STYLE_HIGH_INTERNAL_PROFILE = @"High Internal Profile";
    internal const string DEFAULT_SEPARATOR_STYLE_CUSTOM1 = @"Custom 1";
    internal const string DEFAULT_SEPARATOR_STYLE_CUSTOM2 = @"Custom 2";
    internal const string DEFAULT_SEPARATOR_STYLE_CUSTOM3 = @"Custom 3";

    #endregion

    #region TabBorderStyles

    internal const string DEFAULT_TAB_BORDER_STYLE_ONE_NOTE = @"OneNote";
    internal const string DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_SMALL = @"Square Equal Small";
    internal const string DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_MEDIUM = @"Square Equal Medium";
    internal const string DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_LARGE = @"Square Equal Large";
    internal const string DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_SMALL = @"Square Outsize Small";
    internal const string DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_MEDIUM = @"Square Outsize Medium";
    internal const string DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_LARGE = @"Square Outsize Large";
    internal const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_SMALL = @"Rounded Equal Small";
    internal const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_MEDIUM = @"Rounded Equal Medium";
    internal const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_LARGE = @"Rounded Equal Large";
    internal const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_SMALL = @"Rounded Outsize Small";
    internal const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_MEDIUM = @"Rounded Outsize Medium";
    internal const string DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_LARGE = @"Rounded Outsize Large";
    internal const string DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_NEAR = @"Slant Equal Near";
    internal const string DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_FAR = @"Slant Equal Far";
    internal const string DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_BOTH = @"Slant Equal Both";
    internal const string DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_NEAR = @"Slant Outsize Near";
    internal const string DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_FAR = @"Slant Outsize Far";
    internal const string DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_BOTH = @"Slant Outsize Both";
    internal const string DEFAULT_TAB_BORDER_STYLE_SMOOTH_EQUAL = @"Smooth Equal";
    internal const string DEFAULT_TAB_BORDER_STYLE_SMOOTH_OUTSIZE = @"Smooth Outsize";
    internal const string DEFAULT_TAB_BORDER_STYLE_DOCK_EQUAL = @"Dock Equal";
    internal const string DEFAULT_TAB_BORDER_STYLE_DOCK_OUTSIZE = @"Dock Outsize";

    #endregion

    #region TabStyles

    internal const string DEFAULT_TAB_STYLE_HIGH_PROFILE = @"High Profile";
    internal const string DEFAULT_TAB_STYLE_STANDARD_PROFILE = @"Standard Profile";
    internal const string DEFAULT_TAB_STYLE_LOW_PROFILE = @"Low Profile";
    internal const string DEFAULT_TAB_STYLE_ONE_NOTE = @"OneNote";
    internal const string DEFAULT_TAB_STYLE_DOCK = @"Dock";
    internal const string DEFAULT_TAB_STYLE_DOCK_AUTO_HIDDEN = @"Dock AutoHidden";
    internal const string DEFAULT_TAB_STYLE_CUSTOM1 = @"Custom 1";
    internal const string DEFAULT_TAB_STYLE_CUSTOM2 = @"Custom 2";
    internal const string DEFAULT_TAB_STYLE_CUSTOM3 = @"Custom 3";

    #endregion

    #region ToastNotificationIcon

    internal static string DEFAULT_ICON_NONE = @"None";
    internal static string DEFAULT_ICON_HAND = @"Hand";
    internal static string DEFAULT_ICON_SYSTEM_HAND = @"Hand (System)";
    internal static string DEFAULT_ICON_QUESTION = @"Question";
    internal static string DEFAULT_ICON_SYSTEM_QUESTION = @"Question (System)";
    internal static string DEFAULT_ICON_EXCLAMATION = @"Exclamation";
    internal static string DEFAULT_ICON_SYSTEM_EXCLAMATION = @"Exclamation (System)";
    internal static string DEFAULT_ICON_ASTERISK = @"Asterisk";
    internal static string DEFAULT_ICON_SYSTEM_ASTERISK = @"Asterisk (System)";
    internal static string DEFAULT_ICON_STOP = @"Stop";
    internal static string DEFAULT_ICON_SYSTEM_STOP = @"Stop (System)";
    internal static string DEFAULT_ICON_ERROR = @"Error";
    internal static string DEFAULT_ICON_SYSTEM_ERROR = @"Error (System)";
    internal static string DEFAULT_ICON_WARNING = @"Warning";
    internal static string DEFAULT_ICON_SYSTEM_WARNING = "Warning (System)";
    internal static string DEFAULT_ICON_INFORMATION = @"Information";
    internal static string DEFAULT_ICON_SYSTEM_INFORMATION = @"Information (System)";
    internal static string DEFAULT_ICON_SHIELD = @"User Account Control Shield";
    internal static string DEFAULT_ICON_WINDOWS_LOGO = @"Windows Logo";
    internal static string DEFAULT_ICON_APPLICATION = @"Application";
    internal static string DEFAULT_ICON_SYSTEM_APPLICATION = @"Application (System)";
    internal static string DEFAULT_ICON_OK = @"OK";
    internal static string DEFAULT_ICON_CUSTOM = @"Custom";

    #endregion

    #endregion
}