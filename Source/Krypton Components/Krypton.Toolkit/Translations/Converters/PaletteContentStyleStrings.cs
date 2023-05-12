﻿#region BSD License
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
        public bool IsDefault => ButtonStandalone.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_STANDALONE) &&
                                 ButtonLowProfile.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LOW_PROFILE) &&
                                 ButtonButtonSpec.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BUTTON_SPEC) &&
                                 ButtonBreadCrumb.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BREAD_CRUMB) &&
                                 ButtonCalendarDay.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CALENDAR_DAY) &&
                                 ButtonCluster.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CLUSTER) &&
                                 ButtonGallery.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_GALLERY) &&
                                 ButtonNavigatorStack.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_STACK) &&
                                 ButtonNavigatorOverflow.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_OVERFLOW) &&
                                 ButtonNavigatorMini.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_MINI) &&
                                 ButtonInputControl.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_INPUT_CONTROL) &&
                                 ButtonListItem.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LIST_ITEM) &&
                                 ButtonForm.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM) &&
                                 ButtonFormClose.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM_CLOSE) &&
                                 ButtonCustom1.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM1) &&
                                 ButtonCustom2.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM2) &&
                                 ButtonCustom3.Equals(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM3) &&
                                 ContextMenuHeading.Equals(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_HEADING) &&
                                 ContextMenuItemImage.Equals(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_IMAGE) &&
                                 ContextMenuItemTextStandard.Equals(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_STANDARD) &&
                                 ContextMenuItemTextAlternate.Equals(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_ALTERNATE) &&
                                 ContextMenuItemShortcutText.Equals(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_SHORTCUT_TEXT) &&
                                 GridHeaderColumnList.Equals(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_LIST) &&
                                 GridHeaderRowList.Equals(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_LIST) &&
                                 GridDataCellList.Equals(DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_LIST) &&
                                 GridHeaderColumnSheet.Equals(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_SHEET) &&
                                 GridHeaderRowList.Equals(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_LIST) &&
                                 GridDataCellSheet.Equals(DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_SHEET);

        public void Reset()
        {
            ButtonStandalone = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_STANDALONE;

            ButtonLowProfile = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LOW_PROFILE;

            ButtonButtonSpec = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BUTTON_SPEC;

            ButtonBreadCrumb = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BREAD_CRUMB;

            ButtonCalendarDay = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CALENDAR_DAY;

            ButtonCluster = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CLUSTER;

            ButtonNavigatorMini = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_MINI;

            ButtonNavigatorOverflow = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_OVERFLOW;

            ButtonNavigatorStack = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_STACK;

            ButtonInputControl = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_INPUT_CONTROL;

            ButtonListItem = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LIST_ITEM;

            ButtonForm = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM;

            ButtonFormClose = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM_CLOSE;

            ButtonCommand = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_COMMAND;

            ButtonCustom1 = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM1;

            ButtonCustom2 = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM2;

            ButtonCustom3 = DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM3;

            ContextMenuHeading = DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_HEADING;

            ContextMenuItemImage = DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_IMAGE;
        }

        /// <summary>Gets or sets the standalone palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The standalone palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_STANDALONE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonStandalone { get; set; }

        /// <summary>Gets or sets the low profile palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The low profile palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LOW_PROFILE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonLowProfile { get; set; }

        /// <summary>Gets or sets the button spec palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button spec palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BUTTON_SPEC)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonButtonSpec { get; set; }

        /// <summary>Gets or sets the breadcrumb palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The breadcrumb palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BREAD_CRUMB)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonBreadCrumb { get; set; }

        /// <summary>Gets or sets the calendar day palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The calendar day palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CALENDAR_DAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCalendarDay { get; set; }

        /// <summary>Gets or sets the cluster palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The cluster palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CLUSTER)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCluster { get; set; }

        /// <summary>Gets or sets the gallery palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The gallery palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_GALLERY)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonGallery { get; set; }

        /// <summary>Gets or sets the navigator stack palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The navigator stack palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_STACK)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonNavigatorStack { get; set; }

        /// <summary>Gets or sets the navigator overflow palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The navigator overflow palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_OVERFLOW)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonNavigatorOverflow { get; set; }

        /// <summary>Gets or sets the navigator mini palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The navigator mini palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_MINI)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonNavigatorMini { get; set; }

        /// <summary>Gets or sets the input control palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The input control palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_INPUT_CONTROL)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonInputControl { get; set; }

        /// <summary>Gets or sets the list item palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The list item palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LIST_ITEM)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonListItem { get; set; }

        /// <summary>Gets or sets the form palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The form palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonForm { get; set; }

        /// <summary>Gets or sets the form close palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The form close palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM_CLOSE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonFormClose { get; set; }

        /// <summary>Gets or sets the command palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The commannd palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_COMMAND)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCommand { get; set; }

        /// <summary>Gets or sets the command 1 palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The command 1 palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM1)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCustom1 { get; set; }

        /// <summary>Gets or sets the command 2 palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The command 2 palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM2)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCustom2 { get; set; }

        /// <summary>Gets or sets the command 3 palette button content style.</summary>
        [Category(@"Visuals")]
        [Description(@"The command 3 palette button content style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM3)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCustom3 { get; set; }

        /// <summary>Gets or sets the heading palette context menu style.</summary>
        [Category(@"Visuals")]
        [Description(@"The heading palette context menu style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_HEADING)]
        [RefreshProperties(RefreshProperties.All)]
        public string ContextMenuHeading { get; set; }

        /// <summary>Gets or sets the item image palette context menu style.</summary>
        [Category(@"Visuals")]
        [Description(@"The item image palette context menu style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_IMAGE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ContextMenuItemImage { get; set; }

        /// <summary>Gets or sets the item standard text palette context menu style.</summary>
        [Category(@"Visuals")]
        [Description(@"The item standard text palette context menu style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_STANDARD)]
        [RefreshProperties(RefreshProperties.All)]
        public string ContextMenuItemTextStandard { get; set; }

        /// <summary>Gets or sets the item alternate text palette context menu style.</summary>
        [Category(@"Visuals")]
        [Description(@"The item alternate text palette context menu style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_ALTERNATE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ContextMenuItemTextAlternate { get; set; }

        /// <summary>Gets or sets the item shortcut text palette context menu style.</summary>
        [Category(@"Visuals")]
        [Description(@"The item shortcut text palette context menu style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_SHORTCUT_TEXT)]
        [RefreshProperties(RefreshProperties.All)]
        public string ContextMenuItemShortcutText { get; set; }

        /// <summary>Gets or sets the header column list grid style.</summary>
        [Category(@"Visuals")]
        [Description(@"The header column list grid style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_LIST)]
        [RefreshProperties(RefreshProperties.All)]
        public string GridHeaderColumnList { get; set; }

        /// <summary>Gets or sets the header row list grid style.</summary>
        [Category(@"Visuals")]
        [Description(@"The header row list grid style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM1)]
        [RefreshProperties(RefreshProperties.All)]
        public string GridHeaderRowList { get; set; }

        /// <summary>Gets or sets the data cell list grid style.</summary>
        [Category(@"Visuals")]
        [Description(@"The data cell list grid style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_LIST)]
        [RefreshProperties(RefreshProperties.All)]
        public string GridDataCellList { get; set; }

        /// <summary>Gets or sets the header column sheet grid style.</summary>
        [Category(@"Visuals")]
        [Description(@"The header column sheet grid style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_SHEET)]
        [RefreshProperties(RefreshProperties.All)]
        public string GridHeaderColumnSheet { get; set; }

        /// <summary>Gets or sets the header row sheet grid style.</summary>
        [Category(@"Visuals")]
        [Description(@"The header row sheet grid style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_SHEET)]
        [RefreshProperties(RefreshProperties.All)]
        public string GridHeaderRowSheet { get; set; }

        /// <summary>Gets or sets the data cell sheet grid style.</summary>
        [Category(@"Visuals")]
        [Description(@"The data cell sheet grid style.")]
        [DefaultValue(DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_SHEET)]
        [RefreshProperties(RefreshProperties.All)]
        public string GridDataCellSheet { get; set; }

        #endregion
    }
}