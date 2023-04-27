using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PaletteBackStyleStrings : GlobalId
    {
        #region Static Fields

        private const string DEFAULT_BUTTON_STANDALONE = @"Button - Standalone";
        private const string DEFAULT_BUTTON_ALTERNATE = @"Button - Alternate";
        private const string DEFAULT_BUTTON_LOW_PROFILE = @"Button - Low Profile";
        private const string DEFAULT_BUTTON_BUTTON_SPEC = @"Button - ButtonSpec";
        private const string DEFAULT_BUTTON_BREAD_CRUMB = @"Button - BreadCrumb";
        private const string DEFAULT_BUTTON_CALENDAR_DAY = @"Button - Calendar Day";
        private const string DEFAULT_BUTTON_CLUSTER = @"Button - Cluster";
        private const string DEFAULT_BUTTON_GALLERY = @"Button - Gallery";
        private const string DEFAULT_BUTTON_NAVIGATOR_STACK = @"Button - Navigator Stack";
        private const string DEFAULT_BUTTON_NAVIGATOR_OVERFLOW = @"Button - Navigator Overflow";
        private const string DEFAULT_BUTTON_NAVIGATOR_MINI = @"Button - Navigator Mini";
        private const string DEFAULT_BUTTON_INPUT_CONTROL = @"Button - Input Control";
        private const string DEFAULT_BUTTON_LIST_ITEM = @"Button - List Item";
        private const string DEFAULT_BUTTON_FORM = @"Button - Form";
        private const string DEFAULT_BUTTON_FORM_CLOSE = @"Button - Form Close";
        private const string DEFAULT_BUTTON_COMMAND = @"Button - Command";
        private const string DEFAULT_BUTTON_CUSTOM1 = @"Button - Custom1";
        private const string DEFAULT_BUTTON_CUSTOM2 = @"Button - Custom2";
        private const string DEFAULT_BUTTON_CUSTOM3 = @"Button - Custom3";
        private const string DEFAULT_CONTROL_CLIENT = @"Control - Client";
        private const string DEFAULT_CONTROL_ALTERNATE = @"Control - Alternate";
        private const string DEFAULT_CONTROL_GROUP_BOX = @"Control - GroupBox";
        private const string DEFAULT_CONTROL_TOOL_TIP = @"Control - ToolTip";
        private const string DEFAULT_CONTROL_RIBBON = @"Control - Ribbon";
        private const string DEFAULT_CONTROL_RIBBON_APP_MENU = @"Control - RibbonAppMenu";
        private const string DEFAULT_CONTROL_CUSTOM1 = @"Control - Custom1";
        private const string DEFAULT_CONTROL_CUSTOM2 = @"Control - Custom2";
        private const string DEFAULT_CONTROL_CUSTOM3 = @"Control - Custom3";
        private const string DEFAULT_CONTEXT_MENU_OUTER = @"ContextMenu - Outer";
        private const string DEFAULT_CONTEXT_MENU_INNER = @"ContextMenu - Inner";
        private const string DEFAULT_CONTEXT_MENU_HEADING = @"ContextMenu - Heading";
        private const string DEFAULT_CONTEXT_MENU_SEPARATOR = @"ContextMenu - Separator";
        private const string DEFAULT_CONTEXT_MENU_ITEM_SPLIT = @"ContextMenu - Item Split";
        private const string DEFAULT_CONTEXT_MENU_ITEM_IMAGE = @"ContextMenu - Item Image";
        private const string DEFAULT_CONTEXT_MENU_ITEM_IMAGE_COLUMN = @"ContextMenu - Item ImageColumn";
        private const string DEFAULT_CONTEXT_MENU_ITEM_HIGHLIGHT = @"ContextMenu - Item Highlight";
        private const string DEFAULT_INPUT_CONTROL_STANDALONE = @"InputControl - Standalone";
        private const string DEFAULT_INPUT_CONTROL_RIBBON = @"InputControl - Ribbon";
        private const string DEFAULT_INPUT_CONTROL_CUSTOM1 = @"InputControl - Custom1";
        private const string DEFAULT_INPUT_CONTROL_CUSTOM2 = @"InputControl - Custom2";
        private const string DEFAULT_INPUT_CONTROL_CUSTOM3 = @"InputControl - Custom3";
        private const string DEFAULT_FORM_MAIN = @"Form - Main";
        private const string DEFAULT_FORM_CUSTOM1 = @"Form - Custom1";
        private const string DEFAULT_FORM_CUSTOM2 = @"Form - Custom2";
        private const string DEFAULT_FORM_CUSTOM3 = @"Form - Custom3";
        private const string DEFAULT_GRID_HEADER_COLUMN_LIST = @"Grid - HeaderColumn - List";
        private const string DEFAULT_GRID_HEADER_ROW_LIST = @"Grid - HeaderRow - List";
        private const string DEFAULT_GRID_DATA_CELL_LIST = @"Grid - DataCell - List";
        private const string DEFAULT_GRID_BACKGROUND_LIST = @"Grid - Background - List";
        private const string DEFAULT_GRID_HEADER_COLUMN_SHEET = @"Grid - HeaderColumn - Sheet";
        private const string DEFAULT_GRID_HEADER_ROW_SHEET = @"Grid - HeaderRow - Sheet";
        private const string DEFAULT_GRID_DATA_CELL_SHEET = @"Grid - DataCell - Sheet";
        private const string DEFAULT_GRID_BACKGROUND_SHEET = @"Grid - Background - Sheet";
        private const string DEFAULT_GRID_HEADER_COLUMN_CUSTOM1 = @"Grid - HeaderColumn - Custom1";
        private const string DEFAULT_GRID_HEADER_COLUMN_CUSTOM2 = @"Grid - HeaderColumn - Custom2";
        private const string DEFAULT_GRID_HEADER_COLUMN_CUSTOM3 = @"Grid - HeaderColumn - Custom3";
        private const string DEFAULT_GRID_HEADER_ROW_CUSTOM1 = @"Grid - HeaderRow - Custom1";
        private const string DEFAULT_GRID_HEADER_ROW_CUSTOM2 = @"Grid - HeaderRow - Custom2";
        private const string DEFAULT_GRID_HEADER_ROW_CUSTOM3 = @"Grid - HeaderRow - Custom3";
        private const string DEFAULT_GRID_DATA_CELL_CUSTOM1 = @"Grid - DataCell - Custom1";
        private const string DEFAULT_GRID_DATA_CELL_CUSTOM2 = @"Grid - DataCell - Custom2";
        private const string DEFAULT_GRID_DATA_CELL_CUSTOM3 = @"Grid - DataCell - Custom3";
        private const string DEFAULT_GRID_BACKGROUND_CUSTOM1 = @"Grid - Background - Custom1";
        private const string DEFAULT_GRID_BACKGROUND_CUSTOM2 = @"Grid - Background - Custom2";
        private const string DEFAULT_GRID_BACKGROUND_CUSTOM3 = @"Grid - Background - Custom3";
        private const string DEFAULT_HEADER_PRIMARY = @"Header - Primary";
        private const string DEFAULT_HEADER_SECONDARY = @"Header - Secondary";
        private const string DEFAULT_HEADER_DOCK_ACTIVE = @"Header - Dock - Active";
        private const string DEFAULT_HEADER_DOCK_INACTIVE = @"Header - Dock - Inactive";
        private const string DEFAULT_HEADER_FORM = @"Header - Form";
        private const string DEFAULT_HEADER_CALENDAR = @"Header - Calendar";
        private const string DEFAULT_HEADER_CUSTOM1 = @"Header - Custom1";
        private const string DEFAULT_HEADER_CUSTOM2 = @"Header - Custom2";
        private const string DEFAULT_HEADER_CUSTOM3 = @"Header - Custom3";
        private const string DEFAULT_PANEL_CLIENT = @"Panel - Client";
        private const string DEFAULT_PANEL_ALTERNATE = @"Panel - Alternate";
        private const string DEFAULT_PANEL_RIBBON_INACTIVE = @"Panel - Ribbon Inactive";
        private const string DEFAULT_PANEL_CUSTOM1 = @"Panel - Custom1";
        private const string DEFAULT_PANEL_CUSTOM2 = @"Panel - Custom2";
        private const string DEFAULT_PANEL_CUSTOM3 = @"Panel - Custom3";
        private const string DEFAULT_SEPARATOR_LOW_PROFILE = @"Separator - Low Profile";
        private const string DEFAULT_SEPARATOR_HIGH_PROFILE = @"Separator - High Profile";
        private const string DEFAULT_SEPARATOR_HIGH_INTERNAL_PROFILE = @"Separator - High Internal Profile";
        private const string DEFAULT_TAB_HIGH_PROFILE = @"Tab - High Profile";
        private const string DEFAULT_TAB_STANDARD_PROFILE = @"Tab - Standard Profile";
        private const string DEFAULT_TAB_LOW_PROFILE = @"Tab - Low Profile";
        private const string DEFAULT_TAB_ONE_NOTE = @"Tab - OneNote";
        private const string DEFAULT_TAB_DOCK = @"Tab - Dock";
        private const string DEFAULT_TAB_DOCK_AUTO_HIDDEN = @"Tab - Dock AutoHidden";
        private const string DEFAULT_TAB_CUSTOM1 = @"Tab - Custom1";
        private const string DEFAULT_TAB_CUSTOM2 = @"Tab - Custom2";
        private const string DEFAULT_TAB_CUSTOM3 = @"Tab - Custom3";

        #endregion

        #region Identity

        public PaletteBackStyleStrings()
        {
            Reset();
        }

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        [Browsable(false)]
        public bool IsDefault => ButtonAlternate.Equals(DEFAULT_BUTTON_ALTERNATE) &&
                                 ButtonBreadCrumb.Equals(DEFAULT_BUTTON_BREAD_CRUMB) &&
                                 ButtonButtonSpec.Equals(DEFAULT_BUTTON_BUTTON_SPEC) &&
                                 ButtonCalendarDay.Equals(DEFAULT_BUTTON_CALENDAR_DAY) &&
                                 ButtonCluster.Equals(DEFAULT_BUTTON_CLUSTER) &&
                                 ButtonGallery.Equals(DEFAULT_BUTTON_GALLERY) &&
                                 ButtonLowProfile.Equals(DEFAULT_BUTTON_LOW_PROFILE) &&
                                 ButtonNavigatorOverflow.Equals(DEFAULT_BUTTON_NAVIGATOR_OVERFLOW) &&
                                 ButtonNavigatorStack.Equals(DEFAULT_BUTTON_NAVIGATOR_STACK) &&
                                 ButtonStandalone.Equals(DEFAULT_BUTTON_STANDALONE) &&
                                 ButtonNavigatorMini.Equals(DEFAULT_BUTTON_NAVIGATOR_MINI) &&
                                 ButtonInputControl.Equals(DEFAULT_BUTTON_INPUT_CONTROL) &&
                                 ButtonListItem.Equals(DEFAULT_BUTTON_LIST_ITEM) &&
                                 ButtonForm.Equals(DEFAULT_BUTTON_FORM) &&
                                 ButtonFormClose.Equals(DEFAULT_BUTTON_FORM_CLOSE) &&
                                 ButtonCommand.Equals(DEFAULT_BUTTON_COMMAND) &&
                                 ButtonCustom1.Equals(DEFAULT_BUTTON_CUSTOM1) &&
                                 ButtonCustom2.Equals(DEFAULT_BUTTON_CUSTOM2) &&
                                 ButtonCustom3.Equals(DEFAULT_BUTTON_CUSTOM3) &&
                                 ControlClient.Equals(DEFAULT_CONTROL_CLIENT) &&
                                 ControlAlternate.Equals(DEFAULT_CONTROL_ALTERNATE) &&
                                 ControlGroupBox.Equals(DEFAULT_CONTROL_GROUP_BOX) &&
                                 ControlToolTip.Equals(DEFAULT_CONTROL_TOOL_TIP) &&
                                 ControlRibbon.Equals(DEFAULT_CONTROL_RIBBON) &&
                                 ControlRibbonAppMenu.Equals(DEFAULT_CONTROL_RIBBON_APP_MENU) &&
                                 ControlCustom1.Equals(DEFAULT_CONTROL_CUSTOM1) &&
                                 ControlCustom2.Equals(DEFAULT_CONTROL_CUSTOM2) &&
                                 ControlCustom3.Equals(DEFAULT_CONTROL_CUSTOM3) &&
                                 ContextMenuOuter.Equals(DEFAULT_CONTEXT_MENU_OUTER) &&
                                 ContextMenuInner.Equals(DEFAULT_CONTEXT_MENU_INNER) &&
                                 ContextMenuHeading.Equals(DEFAULT_CONTEXT_MENU_HEADING) &&
                                 ContextMenuSeparator.Equals(DEFAULT_CONTEXT_MENU_SEPARATOR) &&
                                 ContextMenuItemSplit.Equals(DEFAULT_CONTEXT_MENU_ITEM_SPLIT) &&
                                 ContextMenuItemImage.Equals(DEFAULT_CONTEXT_MENU_ITEM_IMAGE) &&
                                 ContextMenuItemImageColumn.Equals(DEFAULT_CONTEXT_MENU_ITEM_IMAGE_COLUMN) &&
                                 ContextMenuItemHighlight.Equals(DEFAULT_CONTEXT_MENU_ITEM_HIGHLIGHT) &&
                                 InputControlStandalone.Equals(DEFAULT_INPUT_CONTROL_STANDALONE) &&
                                 InputControlRibbon.Equals(DEFAULT_INPUT_CONTROL_RIBBON) &&
                                 InputControlCustom1.Equals(DEFAULT_INPUT_CONTROL_CUSTOM1) &&
                                 InputControlCustom2.Equals(DEFAULT_INPUT_CONTROL_CUSTOM2);

        public void Reset()
        {
            ButtonAlternate = DEFAULT_BUTTON_ALTERNATE;

            ButtonBreadCrumb = DEFAULT_BUTTON_BREAD_CRUMB;

            ButtonButtonSpec = DEFAULT_BUTTON_BUTTON_SPEC;

            ButtonCalendarDay = DEFAULT_BUTTON_CALENDAR_DAY;

            ButtonCluster = DEFAULT_BUTTON_CLUSTER;

            ButtonGallery = DEFAULT_BUTTON_GALLERY;

            ButtonLowProfile = DEFAULT_BUTTON_LOW_PROFILE;

            ButtonNavigatorOverflow = DEFAULT_BUTTON_NAVIGATOR_OVERFLOW;

            ButtonNavigatorStack = DEFAULT_BUTTON_NAVIGATOR_STACK;

            ButtonStandalone = DEFAULT_BUTTON_STANDALONE;

            ButtonCustom1 = DEFAULT_BUTTON_CUSTOM1;

            ButtonCustom2 = DEFAULT_BUTTON_CUSTOM2;

            ButtonCustom3 = DEFAULT_BUTTON_CUSTOM3;

            ButtonNavigatorMini = DEFAULT_BUTTON_NAVIGATOR_MINI;

            ButtonInputControl = DEFAULT_BUTTON_INPUT_CONTROL;

            ButtonListItem = DEFAULT_BUTTON_LIST_ITEM;

            ButtonForm = DEFAULT_BUTTON_FORM;

            ButtonFormClose = DEFAULT_BUTTON_FORM_CLOSE;

            ButtonCommand = DEFAULT_BUTTON_COMMAND;

            ControlClient = DEFAULT_CONTROL_CLIENT;

            ControlAlternate = DEFAULT_CONTROL_ALTERNATE;

            ControlGroupBox = DEFAULT_CONTROL_GROUP_BOX;

            ControlToolTip = DEFAULT_CONTROL_TOOL_TIP;

            ControlRibbon = DEFAULT_CONTROL_RIBBON;

            ControlRibbonAppMenu = DEFAULT_CONTROL_RIBBON_APP_MENU;

            ControlCustom1 = DEFAULT_CONTROL_CUSTOM1;

            ControlCustom2 = DEFAULT_CONTROL_CUSTOM2;

            ControlCustom3 = DEFAULT_CONTROL_CUSTOM3;

            ContextMenuOuter = DEFAULT_CONTEXT_MENU_OUTER;

            ContextMenuInner = DEFAULT_CONTEXT_MENU_INNER;

            ContextMenuHeading = DEFAULT_CONTEXT_MENU_HEADING;

            ContextMenuSeparator = DEFAULT_CONTEXT_MENU_SEPARATOR;

            ContextMenuItemSplit = DEFAULT_CONTEXT_MENU_ITEM_SPLIT;

            ContextMenuItemImage = DEFAULT_CONTEXT_MENU_ITEM_IMAGE;

            ContextMenuItemImageColumn = DEFAULT_CONTEXT_MENU_ITEM_IMAGE_COLUMN;

            ContextMenuItemHighlight = DEFAULT_CONTEXT_MENU_ITEM_HIGHLIGHT;

            InputControlStandalone = DEFAULT_INPUT_CONTROL_STANDALONE;

            InputControlRibbon = DEFAULT_INPUT_CONTROL_RIBBON;

            InputControlCustom1 = DEFAULT_INPUT_CONTROL_CUSTOM1;

            InputControlCustom2 = DEFAULT_INPUT_CONTROL_CUSTOM2;
        }

        /// <summary>Gets or sets the button standalone palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button standalone palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_STANDALONE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonStandalone { get; set; }

        /// <summary>Gets or sets the button alternate palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button alternate palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_ALTERNATE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonAlternate { get; set; }

        /// <summary>Gets or sets the button low profile palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button low profile palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_LOW_PROFILE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonLowProfile { get; set; }

        /// <summary>Gets or sets the button spec palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button spec palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_BUTTON_SPEC)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonButtonSpec { get; set; }

        /// <summary>Gets or sets the button breadcrumb palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button breadcrumb palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_BREAD_CRUMB)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonBreadCrumb { get; set; }

        /// <summary>Gets or sets the button calendar day palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button calendar day palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_CALENDAR_DAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCalendarDay { get; set; }

        /// <summary>Gets or sets the button cluster palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button cluster palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_CLUSTER)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCluster { get; set; }

        /// <summary>Gets or sets the button gallery palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button gallery palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_GALLERY)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonGallery { get; set; }

        /// <summary>Gets or sets the button navigator stack palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button navigator stack palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_NAVIGATOR_STACK)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonNavigatorStack { get; set; }

        /// <summary>Gets or sets the button navigator overflow palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button navigator overflow palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_NAVIGATOR_OVERFLOW)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonNavigatorOverflow { get; set; }

        /// <summary>Gets or sets the button navigator mini palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button navigator mini palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_NAVIGATOR_MINI)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonNavigatorMini { get; set; }

        /// <summary>Gets or sets the button input control palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button input control palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_INPUT_CONTROL)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonInputControl { get; set; }

        /// <summary>Gets or sets the button list item palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button list item palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_LIST_ITEM)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonListItem { get; set; }

        /// <summary>Gets or sets the button form palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button form palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_FORM)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonForm { get; set; }

        /// <summary>Gets or sets the button form close palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button form close palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_FORM_CLOSE)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonFormClose { get; set; }

        /// <summary>Gets or sets the button command palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button command palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_COMMAND)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCommand { get; set; }

        /// <summary>Gets or sets the button custom 1 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button custom 1 palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_CUSTOM1)]
        [RefreshProperties(RefreshProperties.All)]
        public string ButtonCustom1 { get; set; }

        /// <summary>Gets or sets the button custom 2 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button custom 2 palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_CUSTOM2)]
        public string ButtonCustom2 { get; set; }

        /// <summary>Gets or sets the button custom 3 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The button custom 3 palette back style.")]
        [DefaultValue(DEFAULT_BUTTON_CUSTOM3)]
        public string ButtonCustom3 { get; set; }

        /// <summary>Gets or sets the control client palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control client palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_CLIENT)]
        public string ControlClient { get; set; }

        /// <summary>Gets or sets the control alternate palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control alternate palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_ALTERNATE)]
        public string ControlAlternate { get; set; }

        /// <summary>Gets or sets the control group box palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control group box palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_GROUP_BOX)]
        public string ControlGroupBox { get; set; }

        /// <summary>Gets or sets the control tool tip palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control tool tip palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_TOOL_TIP)]
        public string ControlToolTip { get; set; }

        /// <summary>Gets or sets the control ribbon palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control ribbon palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_RIBBON)]
        public string ControlRibbon { get; set; }

        /// <summary>Gets or sets the control ribbon app menu palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control ribbon app menu palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_RIBBON_APP_MENU)]
        public string ControlRibbonAppMenu { get; set; }

        /// <summary>Gets or sets the control custom 1 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control custom 1 palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_CUSTOM1)]
        public string ControlCustom1 { get; set; }

        /// <summary>Gets or sets the control custom 2 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control custom 2 palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_CUSTOM2)]
        public string ControlCustom2 { get; set; }

        /// <summary>Gets or sets the control custom 3 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The control cusom 3 palette back style.")]
        [DefaultValue(DEFAULT_CONTROL_CUSTOM3)]
        public string ControlCustom3 { get; set; }

        /// <summary>Gets or sets the context menu outer palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu outer palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_OUTER)]
        public string ContextMenuOuter { get; set; }

        /// <summary>Gets or sets the context menu inner palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu inner palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_INNER)]
        public string ContextMenuInner { get; set; }

        /// <summary>Gets or sets the context menu heading palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu heading palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_HEADING)]
        public string ContextMenuHeading { get; set; }

        /// <summary>Gets or sets the context menu separator palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu separator palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_SEPARATOR)]
        public string ContextMenuSeparator { get; set; }

        /// <summary>Gets or sets the context menu item split palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu item split palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_SPLIT)]
        public string ContextMenuItemSplit { get; set; }

        /// <summary>Gets or sets the context menu item image palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu item image palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_IMAGE)]
        public string ContextMenuItemImage { get; set; }

        /// <summary>Gets or sets the context menu item image column palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu item image column palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_IMAGE_COLUMN)]
        public string ContextMenuItemImageColumn { get; set; }

        /// <summary>Gets or sets the context menu item highlight palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The context menu item highlight palette back style.")]
        [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_HIGHLIGHT)]
        public string ContextMenuItemHighlight { get; set; }

        /// <summary>Gets or sets the input control standalone palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The input control standalone palette back style.")]
        [DefaultValue(DEFAULT_INPUT_CONTROL_STANDALONE)]
        public string InputControlStandalone { get; set; }

        /// <summary>Gets or sets the input control ribbon palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The input control ribbon palette back style.")]
        [DefaultValue(DEFAULT_INPUT_CONTROL_RIBBON)]
        public string InputControlRibbon { get; set; }

        /// <summary>Gets or sets the input control custom 1 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The input control custom 1 palette back style.")]
        [DefaultValue(DEFAULT_INPUT_CONTROL_CUSTOM1)]
        public string InputControlCustom1 { get; set; }

        /// <summary>Gets or sets the input control custom 2 palette back style.</summary>
        [Category(@"Visuals")]
        [Description(@"The input control custom 2 palette back style.")]
        [DefaultValue(DEFAULT_INPUT_CONTROL_CUSTOM2)]
        public string InputControlCustom2 { get; set; }

        #endregion
    }
}