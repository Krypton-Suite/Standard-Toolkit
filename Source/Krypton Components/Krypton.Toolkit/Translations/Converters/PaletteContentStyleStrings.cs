#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PaletteContentStyleStrings : GlobalId
    {
        #region Static Fields

        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_STANDALONE = @"Button - Standalone";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LOW_PROFILE = @"Button - Low Profile";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BUTTON_SPEC = @"Button - ButtonSpec";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BREAD_CRUMB = @"Button - BreadCrumb";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CALENDAR_DAY = @"Button - Calendar Day";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CLUSTER = @"Button - Cluster";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_GALLERY = @"Button - Gallery";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_STACK = @"Button - Navigator Stack";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_OVERFLOW = @"Button - Navigator Overflow";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_MINI = @"Button - Navigator Mini";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_INPUT_CONTROL = @"Button - Input Control";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LIST_ITEM = @"Button - List Item";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM = @"Button - Form";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM_CLOSE = @"Button - Form Close";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_COMMAND = @"Button - Command";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM1 = @"Button - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM2 = @"Button - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM3 = @"Button - Custom3";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_HEADING = @"ContextMenu - Heading";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_IMAGE = @"ContextMenu - Item Image";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_STANDARD = @"ContextMenu - Item Text Standard";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_ALTERNATE = @"ContextMenu - Item Text Alternate";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_SHORTCUT_TEXT = @"ContextMenu - Item ShortcutText";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_LIST = @"Grid - HeaderColumn - List";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_LIST = @"Grid - RowColumn - List";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_LIST = @"Grid - DataCell - List";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_SHEET = @"Grid - HeaderColumn - Sheet";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_SHEET = @"Grid - RowColumn - Sheet";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_SHEET = @"Grid - DataCell - Sheet";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM1 = @"Grid - HeaderColumn - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM2 = @"Grid - HeaderColumn - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM3 = @"Grid - HeaderColumn - Custom3";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM1 = @"Grid - RowColumn - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM2 = @"Grid - RowColumn - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM3 = @"Grid - RowColumn - Custom3";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM1 = @"Grid - DataCell - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM2 = @"Grid - DataCell - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM3 = @"Grid - DataCell - Custom3";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_PRIMARY = @"Header - Primary";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_SECONDARY = @"Header - Secondary";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_DOCK_ACTIVE = @"Header - Dock - Active";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_DOCK_INACTIVE = @"Header - Dock - Inactive";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_FORM = @"Header - Form";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CALENDAR = @"Header - Calendar";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM1 = @"Header - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM2 = @"Header - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM3 = @"Header - Custom3";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_NORMAL_CONTROL = @"Label - Normal (Control)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_BOLD_CONTROL = @"Label - Bold (Control)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ITALIC_CONTROL = @"Label - Italic (Control)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TITLE_CONTROL = @"Label - Title (Control)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_NORMAL_PANEL = @"Label - Normal (Panel)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_BOLD_PANEL = @"Label - Bold (Panel)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ITALIC_PANEL = @"Label - Italic (Panel)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TITLE_PANEL = @"Label - Title (Panel)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_GROUP_BOX_CAPTION = @"Label - Caption (Panel)";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TOOL_TIP = @"Label - ToolTip";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_SUPER_TIP = @"Label - SuperTip";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_KEY_TIP = @"Label - KeyTip";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_CUSTOM1 = @"Label - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_LABEL_CUSTOM2 = @"Label - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_HIGH_PROFILE = @"Tab - High Profile";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_STANDARD_PROFILE = @"Tab - Standard Profile";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_LOW_PROFILE = @"Tab - Low Profile";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_ONE_NOTE = @"Tab - OneNote";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_DOCK = @"Tab - Dock";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_DOCK_AUTO_HIDDEN = @"Tab - Dock AutoHidden";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM1 = @"Tab - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM2 = @"Tab - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM3 = @"Tab - Custom3";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_STANDALONE = @"InputControl - Standalone";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_RIBBON = @"InputControl - Ribbon";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM1 = @"InputControl - Custom1";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM2 = @"InputControl - Custom2";
        private const string DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM3 = @"InputControl - Custom3";

        #endregion

        #region Identity

        public PaletteContentStyleStrings()
        {
            Reset();
        }

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        [Browsable(false)]
        public bool IsDefault => false;

        public void Reset()
        {

        }

        #endregion
    }
}