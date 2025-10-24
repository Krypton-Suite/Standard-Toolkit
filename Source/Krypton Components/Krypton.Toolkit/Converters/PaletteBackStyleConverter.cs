#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Custom type converter so that PaletteBackStyle values appear as neat text at design time.
/// </summary>
internal class PaletteBackStyleConverter : StringLookupConverter<PaletteBackStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteBackStyle, string> _pairs = new BiDictionary<PaletteBackStyle, string>(
        new Dictionary<PaletteBackStyle, string>
        {
            {PaletteBackStyle.ButtonStandalone, DesignTimeUtilities.DEFAULT_BUTTON_STANDALONE},
            {PaletteBackStyle.ButtonAlternate, DesignTimeUtilities.DEFAULT_BUTTON_ALTERNATE},
            {PaletteBackStyle.ButtonLowProfile, DesignTimeUtilities.DEFAULT_BUTTON_LOW_PROFILE},
            {PaletteBackStyle.ButtonButtonSpec, DesignTimeUtilities.DEFAULT_BUTTON_BUTTON_SPEC},
            {PaletteBackStyle.ButtonBreadCrumb, DesignTimeUtilities.DEFAULT_BUTTON_BREAD_CRUMB},
            {PaletteBackStyle.ButtonCalendarDay, DesignTimeUtilities.DEFAULT_BUTTON_CALENDAR_DAY},
            {PaletteBackStyle.ButtonCluster, DesignTimeUtilities.DEFAULT_BUTTON_CLUSTER},
            {PaletteBackStyle.ButtonGallery, DesignTimeUtilities.DEFAULT_BUTTON_GALLERY},
            {PaletteBackStyle.ButtonNavigatorStack, DesignTimeUtilities.DEFAULT_BUTTON_NAVIGATOR_STACK},
            {PaletteBackStyle.ButtonNavigatorOverflow, DesignTimeUtilities.DEFAULT_BUTTON_NAVIGATOR_OVERFLOW},
            {PaletteBackStyle.ButtonNavigatorMini, DesignTimeUtilities.DEFAULT_BUTTON_NAVIGATOR_MINI},
            {PaletteBackStyle.ButtonInputControl, DesignTimeUtilities.DEFAULT_BUTTON_INPUT_CONTROL},
            {PaletteBackStyle.ButtonListItem, DesignTimeUtilities.DEFAULT_BUTTON_LIST_ITEM},
            {PaletteBackStyle.ButtonForm, DesignTimeUtilities.DEFAULT_BUTTON_FORM},
            {PaletteBackStyle.ButtonFormClose, DesignTimeUtilities.DEFAULT_BUTTON_FORM_CLOSE},
            {PaletteBackStyle.ButtonCommand, DesignTimeUtilities.DEFAULT_BUTTON_COMMAND},
            {PaletteBackStyle.ButtonCustom1, DesignTimeUtilities.DEFAULT_BUTTON_CUSTOM1},
            {PaletteBackStyle.ButtonCustom2, DesignTimeUtilities.DEFAULT_BUTTON_CUSTOM2},
            {PaletteBackStyle.ButtonCustom3, DesignTimeUtilities.DEFAULT_BUTTON_CUSTOM3},
            {PaletteBackStyle.ControlClient, DesignTimeUtilities.DEFAULT_CONTROL_CLIENT},
            {PaletteBackStyle.ControlAlternate, DesignTimeUtilities.DEFAULT_CONTROL_ALTERNATE},
            {PaletteBackStyle.ControlGroupBox, DesignTimeUtilities.DEFAULT_CONTROL_GROUP_BOX},
            {PaletteBackStyle.ControlToolTip, DesignTimeUtilities.DEFAULT_CONTROL_TOOL_TIP},
            {PaletteBackStyle.ControlRibbon, DesignTimeUtilities.DEFAULT_CONTROL_RIBBON},
            {PaletteBackStyle.ControlRibbonAppMenu, DesignTimeUtilities.DEFAULT_CONTROL_RIBBON_APP_MENU},
            {PaletteBackStyle.ControlCustom1, DesignTimeUtilities.DEFAULT_CONTROL_CUSTOM1},
            {PaletteBackStyle.ControlCustom2, DesignTimeUtilities.DEFAULT_CONTROL_CUSTOM2},
            {PaletteBackStyle.ControlCustom3, DesignTimeUtilities.DEFAULT_CONTROL_CUSTOM3},
            {PaletteBackStyle.ContextMenuOuter, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_OUTER},
            {PaletteBackStyle.ContextMenuInner, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_INNER},
            {PaletteBackStyle.ContextMenuHeading, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_HEADING},
            {PaletteBackStyle.ContextMenuSeparator, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_SEPARATOR},
            {PaletteBackStyle.ContextMenuItemSplit, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_ITEM_SPLIT},
            {PaletteBackStyle.ContextMenuItemImage, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_ITEM_IMAGE},
            {PaletteBackStyle.ContextMenuItemImageColumn, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_ITEM_IMAGE_COLUMN},
            {PaletteBackStyle.ContextMenuItemHighlight, DesignTimeUtilities.DEFAULT_CONTEXT_MENU_ITEM_HIGHLIGHT},
            {PaletteBackStyle.InputControlStandalone, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STANDALONE},
            {PaletteBackStyle.InputControlRibbon, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_RIBBON},
            {PaletteBackStyle.InputControlCustom1, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_CUSTOM1},
            {PaletteBackStyle.InputControlCustom2, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_CUSTOM2},
            {PaletteBackStyle.InputControlCustom3, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_CUSTOM3},
            {PaletteBackStyle.FormMain, DesignTimeUtilities.DEFAULT_FORM_MAIN},
            {PaletteBackStyle.FormCustom1, DesignTimeUtilities.DEFAULT_FORM_CUSTOM1},
            {PaletteBackStyle.FormCustom2, DesignTimeUtilities.DEFAULT_FORM_CUSTOM2},
            {PaletteBackStyle.FormCustom3, DesignTimeUtilities.DEFAULT_FORM_CUSTOM3},
            {PaletteBackStyle.GridHeaderColumnList, DesignTimeUtilities.DEFAULT_GRID_HEADER_COLUMN_LIST},
            {PaletteBackStyle.GridHeaderRowList, DesignTimeUtilities.DEFAULT_GRID_HEADER_ROW_LIST},
            {PaletteBackStyle.GridDataCellList, DesignTimeUtilities.DEFAULT_GRID_DATA_CELL_LIST},
            {PaletteBackStyle.GridBackgroundList, DesignTimeUtilities.DEFAULT_GRID_BACKGROUND_LIST},
            {PaletteBackStyle.GridHeaderColumnSheet, DesignTimeUtilities.DEFAULT_GRID_HEADER_COLUMN_SHEET},
            {PaletteBackStyle.GridHeaderRowSheet, DesignTimeUtilities.DEFAULT_GRID_HEADER_ROW_SHEET},
            {PaletteBackStyle.GridDataCellSheet, DesignTimeUtilities.DEFAULT_GRID_DATA_CELL_SHEET},
            {PaletteBackStyle.GridBackgroundSheet, DesignTimeUtilities.DEFAULT_GRID_BACKGROUND_SHEET},
            {PaletteBackStyle.GridHeaderColumnCustom1, DesignTimeUtilities.DEFAULT_GRID_HEADER_COLUMN_CUSTOM1},
            {PaletteBackStyle.GridHeaderColumnCustom2, DesignTimeUtilities.DEFAULT_GRID_HEADER_COLUMN_CUSTOM2},
            {PaletteBackStyle.GridHeaderColumnCustom3, DesignTimeUtilities.DEFAULT_GRID_HEADER_COLUMN_CUSTOM3},
            {PaletteBackStyle.GridHeaderRowCustom1, DesignTimeUtilities.DEFAULT_GRID_HEADER_ROW_CUSTOM1},
            {PaletteBackStyle.GridHeaderRowCustom2, DesignTimeUtilities.DEFAULT_GRID_HEADER_ROW_CUSTOM2},
            {PaletteBackStyle.GridHeaderRowCustom3, DesignTimeUtilities.DEFAULT_GRID_HEADER_ROW_CUSTOM3},
            {PaletteBackStyle.GridDataCellCustom1, DesignTimeUtilities.DEFAULT_GRID_DATA_CELL_CUSTOM1},
            {PaletteBackStyle.GridDataCellCustom2, DesignTimeUtilities.DEFAULT_GRID_DATA_CELL_CUSTOM2},
            {PaletteBackStyle.GridDataCellCustom3, DesignTimeUtilities.DEFAULT_GRID_DATA_CELL_CUSTOM3},
            {PaletteBackStyle.GridBackgroundCustom1, DesignTimeUtilities.DEFAULT_GRID_BACKGROUND_CUSTOM1},
            {PaletteBackStyle.GridBackgroundCustom2, DesignTimeUtilities.DEFAULT_GRID_BACKGROUND_CUSTOM2},
            {PaletteBackStyle.GridBackgroundCustom3, DesignTimeUtilities.DEFAULT_GRID_BACKGROUND_CUSTOM3},
            {PaletteBackStyle.HeaderPrimary, DesignTimeUtilities.DEFAULT_HEADER_PRIMARY},
            {PaletteBackStyle.HeaderSecondary, DesignTimeUtilities.DEFAULT_HEADER_SECONDARY},
            {PaletteBackStyle.HeaderDockActive, DesignTimeUtilities.DEFAULT_HEADER_DOCK_ACTIVE},
            {PaletteBackStyle.HeaderDockInactive, DesignTimeUtilities.DEFAULT_HEADER_DOCK_INACTIVE},
            {PaletteBackStyle.HeaderForm, DesignTimeUtilities.DEFAULT_HEADER_FORM},
            {PaletteBackStyle.HeaderCalendar, DesignTimeUtilities.DEFAULT_HEADER_CALENDAR},
            {PaletteBackStyle.HeaderCustom1, DesignTimeUtilities.DEFAULT_HEADER_CUSTOM1},
            {PaletteBackStyle.HeaderCustom2, DesignTimeUtilities.DEFAULT_HEADER_CUSTOM2},
            {PaletteBackStyle.HeaderCustom3, DesignTimeUtilities.DEFAULT_HEADER_CUSTOM3},
            {PaletteBackStyle.PanelClient, DesignTimeUtilities.DEFAULT_PANEL_CLIENT},
            {PaletteBackStyle.PanelAlternate, DesignTimeUtilities.DEFAULT_PANEL_ALTERNATE},
            {PaletteBackStyle.PanelRibbonInactive, DesignTimeUtilities.DEFAULT_PANEL_RIBBON_INACTIVE},
            {PaletteBackStyle.PanelCustom1, DesignTimeUtilities.DEFAULT_PANEL_CUSTOM1},
            {PaletteBackStyle.PanelCustom2, DesignTimeUtilities.DEFAULT_PANEL_CUSTOM2},
            {PaletteBackStyle.PanelCustom3, DesignTimeUtilities.DEFAULT_PANEL_CUSTOM3},
            {PaletteBackStyle.SeparatorLowProfile, DesignTimeUtilities.DEFAULT_SEPARATOR_LOW_PROFILE},
            {PaletteBackStyle.SeparatorHighProfile, DesignTimeUtilities.DEFAULT_SEPARATOR_HIGH_PROFILE},
            {PaletteBackStyle.SeparatorHighInternalProfile, DesignTimeUtilities.DEFAULT_SEPARATOR_HIGH_INTERNAL_PROFILE},
            {PaletteBackStyle.TabHighProfile, DesignTimeUtilities.DEFAULT_TAB_HIGH_PROFILE},
            {PaletteBackStyle.TabStandardProfile, DesignTimeUtilities.DEFAULT_TAB_STANDARD_PROFILE},
            {PaletteBackStyle.TabLowProfile, DesignTimeUtilities.DEFAULT_TAB_LOW_PROFILE},
            {PaletteBackStyle.TabOneNote, DesignTimeUtilities.DEFAULT_TAB_ONE_NOTE},
            {PaletteBackStyle.TabDock, DesignTimeUtilities.DEFAULT_TAB_DOCK},
            {PaletteBackStyle.TabDockAutoHidden, DesignTimeUtilities.DEFAULT_TAB_DOCK_AUTO_HIDDEN},
            {PaletteBackStyle.TabCustom1, DesignTimeUtilities.DEFAULT_TAB_CUSTOM1},
            {PaletteBackStyle.TabCustom2, DesignTimeUtilities.DEFAULT_TAB_CUSTOM2},
            {PaletteBackStyle.TabCustom3, DesignTimeUtilities.DEFAULT_TAB_CUSTOM3},
            {PaletteBackStyle.Control, DesignTimeUtilities.DEFAULT_CONTROL}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteBackStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteBackStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}