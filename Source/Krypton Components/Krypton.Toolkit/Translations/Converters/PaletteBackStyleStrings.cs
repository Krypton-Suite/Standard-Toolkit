#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

#pragma warning disable VSSpell001
namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="PaletteBackStyleConverter"/> strings used within Krypton and that are localizable.</summary>
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
    private const string DEFAULT_CONTROL = @"System - Control";

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
                             InputControlCustom2.Equals(DEFAULT_INPUT_CONTROL_CUSTOM2) &&
                             InputControlCustom3.Equals(DEFAULT_INPUT_CONTROL_CUSTOM3) &&
                             FormMain.Equals(DEFAULT_FORM_MAIN) &&
                             FormCustom1.Equals(DEFAULT_FORM_CUSTOM1) &&
                             FormCustom2.Equals(DEFAULT_FORM_CUSTOM2) &&
                             FormCustom3.Equals(DEFAULT_FORM_CUSTOM3) &&
                             GridHeaderColumnList.Equals(DEFAULT_GRID_HEADER_COLUMN_LIST) &&
                             GridHeaderRowList.Equals(DEFAULT_GRID_HEADER_ROW_LIST) &&
                             GridDataCellList.Equals(DEFAULT_GRID_DATA_CELL_LIST) &&
                             GridBackgroundList.Equals(DEFAULT_GRID_BACKGROUND_LIST) &&
                             GridHeaderColumnList.Equals(DEFAULT_GRID_HEADER_COLUMN_LIST) &&
                             GridHeaderColumnSheet.Equals(DEFAULT_GRID_HEADER_COLUMN_SHEET) &&
                             GridHeaderRowSheet.Equals(DEFAULT_GRID_HEADER_ROW_SHEET) &&
                             GridDataCellSheet.Equals(DEFAULT_GRID_DATA_CELL_SHEET) &&
                             GridBackgroundSheet.Equals(DEFAULT_GRID_BACKGROUND_SHEET) &&
                             GridHeaderColumnCustom1.Equals(DEFAULT_GRID_HEADER_COLUMN_CUSTOM1) &&
                             GridHeaderColumnCustom2.Equals(DEFAULT_GRID_HEADER_COLUMN_CUSTOM2) &&
                             GridHeaderColumnCustom3.Equals(DEFAULT_GRID_HEADER_COLUMN_CUSTOM3) &&
                             GridHeaderRowCustom1.Equals(DEFAULT_GRID_HEADER_ROW_CUSTOM1) &&
                             GridHeaderRowCustom2.Equals(DEFAULT_GRID_HEADER_ROW_CUSTOM2) &&
                             GridHeaderRowCustom3.Equals(DEFAULT_GRID_HEADER_ROW_CUSTOM3) &&
                             GridDataCellCustom1.Equals(DEFAULT_GRID_DATA_CELL_CUSTOM1) &&
                             GridDataCellCustom2.Equals(DEFAULT_GRID_DATA_CELL_CUSTOM2) &&
                             GridDataCellCustom3.Equals(DEFAULT_GRID_DATA_CELL_CUSTOM3) &&
                             GridBackgroundCustom1.Equals(DEFAULT_GRID_BACKGROUND_CUSTOM1) &&
                             GridBackgroundCustom2.Equals(DEFAULT_GRID_BACKGROUND_CUSTOM2) &&
                             GridBackgroundCustom3.Equals(DEFAULT_GRID_BACKGROUND_CUSTOM3) &&
                             HeaderPrimary.Equals(DEFAULT_HEADER_PRIMARY) &&
                             HeaderSecondary.Equals(DEFAULT_HEADER_SECONDARY) &&
                             HeaderDockActive.Equals(DEFAULT_HEADER_DOCK_ACTIVE) &&
                             HeaderDockInactive.Equals(DEFAULT_HEADER_DOCK_INACTIVE) &&
                             HeaderForm.Equals(DEFAULT_HEADER_FORM) &&
                             HeaderCalendar.Equals(DEFAULT_HEADER_CALENDAR) &&
                             HeaderCustom1.Equals(DEFAULT_HEADER_CUSTOM1) &&
                             HeaderCustom2.Equals(DEFAULT_HEADER_CUSTOM2) &&
                             HeaderCustom3.Equals(DEFAULT_HEADER_CUSTOM3) &&
                             PanelClient.Equals(DEFAULT_PANEL_CLIENT) &&
                             PanelAlternate.Equals(DEFAULT_PANEL_ALTERNATE) &&
                             PanelCustom1.Equals(DEFAULT_PANEL_CUSTOM1) &&
                             PanelCustom2.Equals(DEFAULT_PANEL_CUSTOM2) &&
                             PanelCustom3.Equals(DEFAULT_PANEL_CUSTOM3) &&
                             SeparatorLowProfile.Equals(DEFAULT_SEPARATOR_LOW_PROFILE) &&
                             SeparatorHighProfile.Equals(DEFAULT_SEPARATOR_HIGH_PROFILE) &&
                             SeparatorHighInternalProfile.Equals(DEFAULT_SEPARATOR_HIGH_INTERNAL_PROFILE) &&
                             TabHighProfile.Equals(DEFAULT_TAB_HIGH_PROFILE) &&
                             TabStandardProfile.Equals(DEFAULT_TAB_STANDARD_PROFILE) &&
                             TabLowProfile.Equals(DEFAULT_TAB_LOW_PROFILE) &&
                             TabOneNote.Equals(DEFAULT_TAB_ONE_NOTE) &&
                             TabDock.Equals(DEFAULT_TAB_DOCK) &&
                             TabDockAutoHidden.Equals(DEFAULT_TAB_DOCK_AUTO_HIDDEN) &&
                             TabCustom1.Equals(DEFAULT_TAB_CUSTOM1) &&
                             TabCustom2.Equals(DEFAULT_TAB_CUSTOM2) &&
                             TabCustom3.Equals(DEFAULT_TAB_CUSTOM3) &&
                             Control.Equals(DEFAULT_CONTROL);

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

        InputControlCustom3 = DEFAULT_INPUT_CONTROL_CUSTOM3;

        FormMain = DEFAULT_FORM_MAIN;

        FormCustom1 = DEFAULT_FORM_CUSTOM1;

        FormCustom2 = DEFAULT_FORM_CUSTOM2;

        FormCustom3 = DEFAULT_FORM_CUSTOM3;

        GridHeaderColumnList = DEFAULT_GRID_HEADER_COLUMN_LIST;

        GridHeaderRowList = DEFAULT_GRID_HEADER_ROW_LIST;

        GridDataCellList = DEFAULT_GRID_DATA_CELL_LIST;

        GridBackgroundList = DEFAULT_GRID_BACKGROUND_LIST;

        GridHeaderColumnSheet = DEFAULT_GRID_HEADER_COLUMN_SHEET;

        GridHeaderRowSheet = DEFAULT_GRID_HEADER_ROW_SHEET;

        GridDataCellSheet = DEFAULT_GRID_DATA_CELL_SHEET;

        GridBackgroundSheet = DEFAULT_GRID_BACKGROUND_SHEET;

        GridHeaderColumnCustom1 = DEFAULT_GRID_HEADER_COLUMN_CUSTOM1;

        GridHeaderColumnCustom2 = DEFAULT_GRID_HEADER_COLUMN_CUSTOM2;

        GridHeaderColumnCustom3 = DEFAULT_GRID_HEADER_COLUMN_CUSTOM3;

        GridHeaderRowCustom1 = DEFAULT_GRID_HEADER_ROW_CUSTOM1;

        GridHeaderRowCustom2 = DEFAULT_GRID_HEADER_ROW_CUSTOM2;

        GridHeaderRowCustom3 = DEFAULT_GRID_HEADER_ROW_CUSTOM3;

        GridDataCellCustom1 = DEFAULT_GRID_DATA_CELL_CUSTOM1;

        GridDataCellCustom2 = DEFAULT_GRID_DATA_CELL_CUSTOM2;

        GridDataCellCustom3 = DEFAULT_GRID_DATA_CELL_CUSTOM3;

        GridBackgroundCustom1 = DEFAULT_GRID_BACKGROUND_CUSTOM1;

        GridBackgroundCustom2 = DEFAULT_GRID_BACKGROUND_CUSTOM2;

        GridBackgroundCustom3 = DEFAULT_GRID_BACKGROUND_CUSTOM3;

        HeaderPrimary = DEFAULT_HEADER_PRIMARY;

        HeaderSecondary = DEFAULT_HEADER_SECONDARY;

        HeaderDockActive = DEFAULT_HEADER_DOCK_ACTIVE;

        HeaderDockInactive = DEFAULT_HEADER_DOCK_INACTIVE;

        HeaderForm = DEFAULT_HEADER_FORM;

        HeaderCalendar = DEFAULT_HEADER_CALENDAR;

        HeaderCustom1 = DEFAULT_HEADER_CUSTOM1;

        HeaderCustom2 = DEFAULT_HEADER_CUSTOM2;

        HeaderCustom3 = DEFAULT_HEADER_CUSTOM3;

        PanelClient = DEFAULT_PANEL_CLIENT;

        PanelAlternate = DEFAULT_PANEL_ALTERNATE;

        PanelRibbonInactive = DEFAULT_PANEL_RIBBON_INACTIVE;

        PanelCustom1 = DEFAULT_PANEL_CUSTOM1;

        PanelCustom2 = DEFAULT_PANEL_CUSTOM2;

        PanelCustom3 = DEFAULT_PANEL_CUSTOM3;

        SeparatorLowProfile = DEFAULT_SEPARATOR_LOW_PROFILE;

        SeparatorHighProfile = DEFAULT_SEPARATOR_HIGH_PROFILE;

        SeparatorHighInternalProfile = DEFAULT_SEPARATOR_HIGH_INTERNAL_PROFILE;

        TabHighProfile = DEFAULT_TAB_HIGH_PROFILE;

        TabStandardProfile = DEFAULT_TAB_STANDARD_PROFILE;

        TabLowProfile = DEFAULT_TAB_LOW_PROFILE;

        TabOneNote = DEFAULT_TAB_ONE_NOTE;

        TabDock = DEFAULT_TAB_DOCK;

        TabDockAutoHidden = DEFAULT_TAB_DOCK_AUTO_HIDDEN;

        TabCustom1 = DEFAULT_TAB_CUSTOM1;

        TabCustom2 = DEFAULT_TAB_CUSTOM2;

        TabCustom3 = DEFAULT_TAB_CUSTOM3;

        Control = DEFAULT_CONTROL;
    }

    /// <summary>Gets or sets the button standalone palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button standalone palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_STANDALONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonStandalone { get; set; }

    /// <summary>Gets or sets the button alternate palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button alternate palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_ALTERNATE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonAlternate { get; set; }

    /// <summary>Gets or sets the button low profile palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button low profile palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_LOW_PROFILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonLowProfile { get; set; }

    /// <summary>Gets or sets the button spec palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button spec palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_BUTTON_SPEC)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonButtonSpec { get; set; }

    /// <summary>Gets or sets the button breadcrumb palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button breadcrumb palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_BREAD_CRUMB)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonBreadCrumb { get; set; }

    /// <summary>Gets or sets the button calendar day palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button calendar day palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_CALENDAR_DAY)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonCalendarDay { get; set; }

    /// <summary>Gets or sets the button cluster palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button cluster palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_CLUSTER)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonCluster { get; set; }

    /// <summary>Gets or sets the button gallery palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button gallery palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_GALLERY)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonGallery { get; set; }

    /// <summary>Gets or sets the button navigator stack palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button navigator stack palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_NAVIGATOR_STACK)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonNavigatorStack { get; set; }

    /// <summary>Gets or sets the button navigator overflow palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button navigator overflow palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_NAVIGATOR_OVERFLOW)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonNavigatorOverflow { get; set; }

    /// <summary>Gets or sets the button navigator mini palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button navigator mini palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_NAVIGATOR_MINI)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonNavigatorMini { get; set; }

    /// <summary>Gets or sets the button input control palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button input control palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_INPUT_CONTROL)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonInputControl { get; set; }

    /// <summary>Gets or sets the button list item palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button list item palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_LIST_ITEM)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonListItem { get; set; }

    /// <summary>Gets or sets the button form palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button form palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_FORM)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonForm { get; set; }

    /// <summary>Gets or sets the button form close palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button form close palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_FORM_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonFormClose { get; set; }

    /// <summary>Gets or sets the button command palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button command palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_COMMAND)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonCommand { get; set; }

    /// <summary>Gets or sets the button custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button custom 1 palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_CUSTOM1)]
    [RefreshProperties(RefreshProperties.All)]
    public string ButtonCustom1 { get; set; }

    /// <summary>Gets or sets the button custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button custom 2 palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_CUSTOM2)]
    public string ButtonCustom2 { get; set; }

    /// <summary>Gets or sets the button custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The button custom 3 palette back style.")]
    [DefaultValue(DEFAULT_BUTTON_CUSTOM3)]
    public string ButtonCustom3 { get; set; }

    /// <summary>Gets or sets the control client palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control client palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_CLIENT)]
    public string ControlClient { get; set; }

    /// <summary>Gets or sets the control alternate palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control alternate palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_ALTERNATE)]
    public string ControlAlternate { get; set; }

    /// <summary>Gets or sets the control group box palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control group box palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_GROUP_BOX)]
    public string ControlGroupBox { get; set; }

    /// <summary>Gets or sets the control tool tip palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control tool tip palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_TOOL_TIP)]
    public string ControlToolTip { get; set; }

    /// <summary>Gets or sets the control ribbon palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control ribbon palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_RIBBON)]
    public string ControlRibbon { get; set; }

    /// <summary>Gets or sets the control ribbon app menu palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control ribbon app menu palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_RIBBON_APP_MENU)]
    public string ControlRibbonAppMenu { get; set; }

    /// <summary>Gets or sets the control custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control custom 1 palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_CUSTOM1)]
    public string ControlCustom1 { get; set; }

    /// <summary>Gets or sets the control custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control custom 2 palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_CUSTOM2)]
    public string ControlCustom2 { get; set; }

    /// <summary>Gets or sets the control custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The control cusom 3 palette back style.")]
    [DefaultValue(DEFAULT_CONTROL_CUSTOM3)]
    public string ControlCustom3 { get; set; }

    /// <summary>Gets or sets the context menu outer palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu outer palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_OUTER)]
    public string ContextMenuOuter { get; set; }

    /// <summary>Gets or sets the context menu inner palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu inner palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_INNER)]
    public string ContextMenuInner { get; set; }

    /// <summary>Gets or sets the context menu heading palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu heading palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_HEADING)]
    public string ContextMenuHeading { get; set; }

    /// <summary>Gets or sets the context menu separator palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu separator palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_SEPARATOR)]
    public string ContextMenuSeparator { get; set; }

    /// <summary>Gets or sets the context menu item split palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu item split palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_SPLIT)]
    public string ContextMenuItemSplit { get; set; }

    /// <summary>Gets or sets the context menu item image palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu item image palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_IMAGE)]
    public string ContextMenuItemImage { get; set; }

    /// <summary>Gets or sets the context menu item image column palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu item image column palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_IMAGE_COLUMN)]
    public string ContextMenuItemImageColumn { get; set; }

    /// <summary>Gets or sets the context menu item highlight palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context menu item highlight palette back style.")]
    [DefaultValue(DEFAULT_CONTEXT_MENU_ITEM_HIGHLIGHT)]
    public string ContextMenuItemHighlight { get; set; }

    /// <summary>Gets or sets the input control standalone palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control standalone palette back style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_STANDALONE)]
    public string InputControlStandalone { get; set; }

    /// <summary>Gets or sets the input control ribbon palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control ribbon palette back style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_RIBBON)]
    public string InputControlRibbon { get; set; }

    /// <summary>Gets or sets the input control custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control custom 1 palette back style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_CUSTOM1)]
    public string InputControlCustom1 { get; set; }

    /// <summary>Gets or sets the input control custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control custom 2 palette back style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_CUSTOM2)]
    public string InputControlCustom2 { get; set; }

    /// <summary>Gets or sets the input control custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control custom 3 palette back style.")]
    [DefaultValue(DEFAULT_INPUT_CONTROL_CUSTOM3)]
    public string InputControlCustom3 { get; set; }

    /// <summary>Gets or sets the input control form main palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The input control form main palette back style.")]
    [DefaultValue(DEFAULT_FORM_MAIN)]
    public string FormMain { get; set; }

    /// <summary>Gets or sets the form custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form custom 1 palette back style.")]
    [DefaultValue(DEFAULT_FORM_CUSTOM1)]
    public string FormCustom1 { get; set; }

    /// <summary>Gets or sets the form custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form custom 2 palette back style.")]
    [DefaultValue(DEFAULT_FORM_CUSTOM2)]
    public string FormCustom2 { get; set; }

    /// <summary>Gets or sets the form custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form custom 3 palette back style.")]
    [DefaultValue(DEFAULT_FORM_CUSTOM3)]
    public string FormCustom3 { get; set; }

    /// <summary>Gets or sets the grid header column list palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header column list palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_COLUMN_LIST)]
    public string GridHeaderColumnList { get; set; }

    /// <summary>Gets or sets the grid header row list palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header row list palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_ROW_LIST)]
    public string GridHeaderRowList { get; set; }

    /// <summary>Gets or sets the grid data cell list palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid data cell list palette back style.")]
    [DefaultValue(DEFAULT_GRID_DATA_CELL_LIST)]
    public string GridDataCellList { get; set; }

    /// <summary>Gets or sets the grid background list palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid background list palette back style.")]
    [DefaultValue(DEFAULT_GRID_BACKGROUND_LIST)]
    public string GridBackgroundList { get; set; }

    /// <summary>Gets or sets the grid header column sheet palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header column sheet palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_COLUMN_SHEET)]
    public string GridHeaderColumnSheet { get; set; }

    /// <summary>Gets or sets the grid header row sheet palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header row sheet palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_ROW_SHEET)]
    public string GridHeaderRowSheet { get; set; }

    /// <summary>Gets or sets the grid data cell sheet palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid data cell sheet palette back style.")]
    [DefaultValue(DEFAULT_GRID_DATA_CELL_SHEET)]
    public string GridDataCellSheet { get; set; }

    /// <summary>Gets or sets the grid background sheet palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid background sheet palette back style.")]
    [DefaultValue(DEFAULT_GRID_BACKGROUND_SHEET)]
    public string GridBackgroundSheet { get; set; }

    /// <summary>Gets or sets the grid header column custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header column custom 1 palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_COLUMN_CUSTOM1)]
    public string GridHeaderColumnCustom1 { get; set; }

    /// <summary>Gets or sets the grid header column custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header column custom 2 palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_COLUMN_CUSTOM2)]
    public string GridHeaderColumnCustom2 { get; set; }

    /// <summary>Gets or sets the grid header column custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header column custom 3 palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_COLUMN_CUSTOM3)]
    public string GridHeaderColumnCustom3 { get; set; }

    /// <summary>Gets or sets the grid header row custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header row custom 1 palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_ROW_CUSTOM1)]
    public string GridHeaderRowCustom1 { get; set; }

    /// <summary>Gets or sets the grid header row custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header row custom 2 palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_ROW_CUSTOM2)]
    public string GridHeaderRowCustom2 { get; set; }

    /// <summary>Gets or sets the grid header row custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid header row custom 3 palette back style.")]
    [DefaultValue(DEFAULT_GRID_HEADER_ROW_CUSTOM3)]
    public string GridHeaderRowCustom3 { get; set; }

    /// <summary>Gets or sets the grid data cell custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid data cell custom 1 palette back style.")]
    [DefaultValue(DEFAULT_GRID_DATA_CELL_CUSTOM1)]
    public string GridDataCellCustom1 { get; set; }

    /// <summary>Gets or sets the grid data cell custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid data cell custom 2 palette back style.")]
    [DefaultValue(DEFAULT_GRID_DATA_CELL_CUSTOM2)]
    public string GridDataCellCustom2 { get; set; }

    /// <summary>Gets or sets the grid data cell custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid data cell custom 3 palette back style.")]
    [DefaultValue(DEFAULT_GRID_DATA_CELL_CUSTOM3)]
    public string GridDataCellCustom3 { get; set; }

    /// <summary>Gets or sets the grid background custom 1 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid background custom 1 palette back style.")]
    [DefaultValue(DEFAULT_GRID_BACKGROUND_CUSTOM1)]
    public string GridBackgroundCustom1 { get; set; }

    /// <summary>Gets or sets the grid background custom 2 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid background custom 2 palette back style.")]
    [DefaultValue(DEFAULT_GRID_BACKGROUND_CUSTOM2)]
    public string GridBackgroundCustom2 { get; set; }

    /// <summary>Gets or sets the grid background custom 3 palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The grid background custom 3 palette back style.")]
    [DefaultValue(DEFAULT_GRID_BACKGROUND_CUSTOM3)]
    public string GridBackgroundCustom3 { get; set; }

    /// <summary>Gets or sets the header primary palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header primary palette back style.")]
    [DefaultValue(DEFAULT_HEADER_PRIMARY)]
    public string HeaderPrimary { get; set; }

    /// <summary>Gets or sets the header secondary palette back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header secondary palette back style.")]
    [DefaultValue(DEFAULT_HEADER_SECONDARY)]
    public string HeaderSecondary { get; set; }

    /// <summary>Gets or sets the header dock active back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header dock active palette back style.")]
    [DefaultValue(DEFAULT_HEADER_DOCK_ACTIVE)]
    public string HeaderDockActive { get; set; }

    /// <summary>Gets or sets the header dock inactive back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header dock inactive palette back style.")]
    [DefaultValue(DEFAULT_HEADER_DOCK_INACTIVE)]
    public string HeaderDockInactive { get; set; }

    /// <summary>Gets or sets the header form back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header form palette back style.")]
    [DefaultValue(DEFAULT_HEADER_FORM)]
    public string HeaderForm { get; set; }

    /// <summary>Gets or sets the header calendar back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header calendar back style.")]
    [DefaultValue(DEFAULT_HEADER_CALENDAR)]
    public string HeaderCalendar { get; set; }

    /// <summary>Gets or sets the header custom 1 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header custom 1 back style.")]
    [DefaultValue(DEFAULT_HEADER_CUSTOM1)]
    public string HeaderCustom1 { get; set; }

    /// <summary>Gets or sets the header custom 2 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header custom 2 back style.")]
    [DefaultValue(DEFAULT_HEADER_CUSTOM2)]
    public string HeaderCustom2 { get; set; }

    /// <summary>Gets or sets the header custom 3 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The header custom 3 back style.")]
    [DefaultValue(DEFAULT_HEADER_CUSTOM3)]
    public string HeaderCustom3 { get; set; }

    /// <summary>Gets or sets the panel client back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel client back style.")]
    [DefaultValue(DEFAULT_PANEL_CLIENT)]
    public string PanelClient { get; set; }

    /// <summary>Gets or sets the panel alternate back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel alternate back style.")]
    [DefaultValue(DEFAULT_PANEL_ALTERNATE)]
    public string PanelAlternate { get; set; }

    /// <summary>Gets or sets the panel ribbon inactive back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel ribbon inactive back style.")]
    [DefaultValue(DEFAULT_PANEL_RIBBON_INACTIVE)]
    public string PanelRibbonInactive { get; set; }

    /// <summary>Gets or sets the panel custom 1 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel custom 1 back style.")]
    [DefaultValue(DEFAULT_PANEL_CUSTOM1)]
    public string PanelCustom1 { get; set; }

    /// <summary>Gets or sets the panel custom 2 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel custom 2 back style.")]
    [DefaultValue(DEFAULT_PANEL_CUSTOM2)]
    public string PanelCustom2 { get; set; }

    /// <summary>Gets or sets the panel custom 3 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The panel custom 3 back style.")]
    [DefaultValue(DEFAULT_PANEL_CUSTOM3)]
    public string PanelCustom3 { get; set; }

    /// <summary>Gets or sets the separator low profile back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The separator low profile back style.")]
    [DefaultValue(DEFAULT_SEPARATOR_LOW_PROFILE)]
    public string SeparatorLowProfile { get; set; }

    /// <summary>Gets or sets the separator high profile back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The separator high profile back style.")]
    [DefaultValue(DEFAULT_SEPARATOR_HIGH_PROFILE)]
    public string SeparatorHighProfile { get; set; }

    /// <summary>Gets or sets the separator high internal profile back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The separator high internal profile back style.")]
    [DefaultValue(DEFAULT_SEPARATOR_HIGH_INTERNAL_PROFILE)]
    public string SeparatorHighInternalProfile { get; set; }

    /// <summary>Gets or sets the tab high profile back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab high profile back style.")]
    [DefaultValue(DEFAULT_TAB_HIGH_PROFILE)]
    public string TabHighProfile { get; set; }

    /// <summary>Gets or sets the tab standard profile back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab standard profile back style.")]
    [DefaultValue(DEFAULT_TAB_STANDARD_PROFILE)]
    public string TabStandardProfile { get; set; }

    /// <summary>Gets or sets the tab low profile back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab low profile back style.")]
    [DefaultValue(DEFAULT_TAB_LOW_PROFILE)]
    public string TabLowProfile { get; set; }

    /// <summary>Gets or sets the tab OneNote back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab OneNote back style.")]
    [DefaultValue(DEFAULT_TAB_ONE_NOTE)]
    public string TabOneNote { get; set; }

    /// <summary>Gets or sets the tab dock back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab dock back style.")]
    [DefaultValue(DEFAULT_TAB_DOCK)]
    public string TabDock { get; set; }

    /// <summary>Gets or sets the tab dock auto hidden back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab dock auto hidden back style.")]
    [DefaultValue(DEFAULT_TAB_DOCK_AUTO_HIDDEN)]
    public string TabDockAutoHidden { get; set; }

    /// <summary>Gets or sets the tab custom 1 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab custom 1 back style.")]
    [DefaultValue(DEFAULT_TAB_CUSTOM1)]
    public string TabCustom1 { get; set; }

    /// <summary>Gets or sets the tab custom 2 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab custom 2 back style.")]
    [DefaultValue(DEFAULT_TAB_CUSTOM2)]
    public string TabCustom2 { get; set; }

    /// <summary>Gets or sets the tab custom 3 back style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The tab custom 3 back style.")]
    [DefaultValue(DEFAULT_TAB_CUSTOM3)]
    public string TabCustom3 { get; set; }

    /// <summary>Gets or sets the control.</summary>
    [Category(@"Visuals")]
    [Description(@"The control back style string.")]
    [DefaultValue(DEFAULT_CONTROL)]
    public string Control { get; set; }

    #endregion
}