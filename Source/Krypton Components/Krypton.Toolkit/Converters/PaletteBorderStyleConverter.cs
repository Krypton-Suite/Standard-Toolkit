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
/// Custom type converter so that PaletteBorderStyle values appear as neat text at design time.
/// </summary>
internal class PaletteBorderStyleConverter : StringLookupConverter<PaletteBorderStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteBorderStyle, string> _pairs = new BiDictionary<PaletteBorderStyle, string>(
        new Dictionary<PaletteBorderStyle, string>
        {
            {PaletteBorderStyle.ButtonStandalone, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_STANDALONE},
            {PaletteBorderStyle.ButtonAlternate, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_ALTERNATE},
            {PaletteBorderStyle.ButtonLowProfile, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_LOW_PROFILE},
            {PaletteBorderStyle.ButtonButtonSpec, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_BUTTON_SPEC},
            {PaletteBorderStyle.ButtonBreadCrumb, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_BREAD_CRUMB},
            {PaletteBorderStyle.ButtonCalendarDay, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_CALENDAR_DAY},
            {PaletteBorderStyle.ButtonCluster, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_CLUSTER},
            {PaletteBorderStyle.ButtonGallery, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_GALLERY},
            {PaletteBorderStyle.ButtonNavigatorStack, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_NAVIGATOR_STACK},
            {PaletteBorderStyle.ButtonNavigatorOverflow, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_NAVIGATOR_OVERFLOW},
            {PaletteBorderStyle.ButtonNavigatorMini, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_NAVIGATOR_MINI},
            {PaletteBorderStyle.ButtonInputControl, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_INPUT_CONTROL},
            {PaletteBorderStyle.ButtonListItem, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_LIST_ITEM},
            {PaletteBorderStyle.ButtonForm, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_FORM},
            {PaletteBorderStyle.ButtonFormClose, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_FORM_CLOSE},
            {PaletteBorderStyle.ButtonCommand, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_COMMAND},
            {PaletteBorderStyle.ButtonCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_CUSTOM1},
            {PaletteBorderStyle.ButtonCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_CUSTOM2},
            {PaletteBorderStyle.ButtonCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_BUTTON_CUSTOM3},
            {PaletteBorderStyle.ControlClient, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_CLIENT},
            {PaletteBorderStyle.ControlAlternate, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_ALTERNATE},
            {PaletteBorderStyle.ControlGroupBox, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_GROUP_BOX},
            {PaletteBorderStyle.ControlToolTip, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_TOOL_TIP},
            {PaletteBorderStyle.ControlRibbon, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_RIBBON},
            {PaletteBorderStyle.ControlRibbonAppMenu, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_RIBBON_APP_MENU},
            {PaletteBorderStyle.ControlCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_CUSTOM1},
            {PaletteBorderStyle.ControlCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_CUSTOM2},
            {PaletteBorderStyle.ControlCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTROL_CUSTOM3},
            {PaletteBorderStyle.ContextMenuOuter, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_OUTER},
            {PaletteBorderStyle.ContextMenuInner, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_INNER},
            {PaletteBorderStyle.ContextMenuHeading, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_HEADING},
            {PaletteBorderStyle.ContextMenuSeparator, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_SEPARATOR},
            {PaletteBorderStyle.ContextMenuItemSplit, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_SPLIT},
            {PaletteBorderStyle.ContextMenuItemImage, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_IMAGE},
            {PaletteBorderStyle.ContextMenuItemImageColumn, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_IMAGE_COLUMN},
            {PaletteBorderStyle.ContextMenuItemHighlight, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_CONTEXT_MENU_ITEM_HIGHLIGHT},
            {PaletteBorderStyle.InputControlStandalone, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_INPUT_CONTROL_STANDALONE},
            {PaletteBorderStyle.InputControlRibbon, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_INPUT_CONTROL_RIBBON},
            {PaletteBorderStyle.InputControlCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_INPUT_CONTROL_CUSTOM1},
            {PaletteBorderStyle.InputControlCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_INPUT_CONTROL_CUSTOM2},
            {PaletteBorderStyle.InputControlCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_INPUT_CONTROL_CUSTOM3},
            {PaletteBorderStyle.FormMain, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_FORM_MAIN},
            {PaletteBorderStyle.FormCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_FORM_CUSTOM1},
            {PaletteBorderStyle.FormCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_FORM_CUSTOM2},
            {PaletteBorderStyle.FormCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_FORM_CUSTOM3},
            {PaletteBorderStyle.GridHeaderColumnList, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_LIST},
            {PaletteBorderStyle.GridHeaderRowList, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_LIST},
            {PaletteBorderStyle.GridDataCellList, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_LIST},
            {PaletteBorderStyle.GridHeaderColumnSheet, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_SHEET},
            {PaletteBorderStyle.GridHeaderRowSheet, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_SHEET},
            {PaletteBorderStyle.GridDataCellSheet, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_SHEET},
            {PaletteBorderStyle.GridHeaderColumnCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_CUSTOM1},
            {PaletteBorderStyle.GridHeaderColumnCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_CUSTOM2},
            {PaletteBorderStyle.GridHeaderColumnCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_COLUMN_CUSTOM3},
            {PaletteBorderStyle.GridHeaderRowCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_CUSTOM1},
            {PaletteBorderStyle.GridHeaderRowCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_CUSTOM2},
            {PaletteBorderStyle.GridHeaderRowCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_HEADER_ROW_CUSTOM3},
            {PaletteBorderStyle.GridDataCellCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_CUSTOM1},
            {PaletteBorderStyle.GridDataCellCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_CUSTOM2},
            {PaletteBorderStyle.GridDataCellCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_GRID_DATA_CELL_CUSTOM3},
            {PaletteBorderStyle.HeaderPrimary, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_PRIMARY},
            {PaletteBorderStyle.HeaderSecondary, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_SECONDARY},
            {PaletteBorderStyle.HeaderDockActive, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_DOCK_ACTIVE},
            {PaletteBorderStyle.HeaderDockInactive, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_DOCK_INACTIVE},
            {PaletteBorderStyle.HeaderForm, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_FORM},
            {PaletteBorderStyle.HeaderCalendar, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_CALENDAR},
            {PaletteBorderStyle.HeaderCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_CUSTOM1},
            {PaletteBorderStyle.HeaderCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_CUSTOM2},
            {PaletteBorderStyle.HeaderCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_HEADER_CUSTOM3},
            {PaletteBorderStyle.SeparatorLowProfile, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_SEPARATOR_LOW_PROFILE},
            {PaletteBorderStyle.SeparatorHighProfile, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_SEPARATOR_HIGH_PROFILE},
            {PaletteBorderStyle.SeparatorHighInternalProfile, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_SEPARATOR_HIGH_INTERNAL_PROFILE},
            {PaletteBorderStyle.TabHighProfile, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_HIGH_PROFILE},
            {PaletteBorderStyle.TabStandardProfile, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_STANDARD_PROFILE},
            {PaletteBorderStyle.TabLowProfile, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_LOW_PROFILE},
            {PaletteBorderStyle.TabOneNote, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_ONE_NOTE},
            {PaletteBorderStyle.TabDock, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_DOCK},
            {PaletteBorderStyle.TabDockAutoHidden, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_DOCK_AUTO_HIDDEN},
            {PaletteBorderStyle.TabCustom1, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_CUSTOM1},
            {PaletteBorderStyle.TabCustom2, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_CUSTOM2},
            {PaletteBorderStyle.TabCustom3, DesignTimeUtilities.DEFAULT_PALETTE_BORDER_TAB_CUSTOM3}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteBorderStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteBorderStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}