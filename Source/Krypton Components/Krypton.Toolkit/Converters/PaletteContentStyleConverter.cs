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
/// Custom type converter so that PaletteContentStyle values appear as neat text at design time.
/// </summary>
internal class PaletteContentStyleConverter : StringLookupConverter<PaletteContentStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteContentStyle, string> _pairs = new BiDictionary<PaletteContentStyle, string>(
        new Dictionary<PaletteContentStyle, string>
        {
            {PaletteContentStyle.ButtonStandalone, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_STANDALONE},
            {PaletteContentStyle.ButtonLowProfile, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LOW_PROFILE},
            {PaletteContentStyle.ButtonButtonSpec, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BUTTON_SPEC},
            {PaletteContentStyle.ButtonBreadCrumb, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_BREAD_CRUMB},
            {PaletteContentStyle.ButtonCalendarDay, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CALENDAR_DAY},
            {PaletteContentStyle.ButtonCluster, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CLUSTER},
            {PaletteContentStyle.ButtonGallery, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_GALLERY},
            {PaletteContentStyle.ButtonNavigatorStack, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_STACK},
            {PaletteContentStyle.ButtonNavigatorOverflow, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_OVERFLOW},
            {PaletteContentStyle.ButtonNavigatorMini, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_NAVIGATOR_MINI},
            {PaletteContentStyle.ButtonInputControl, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_INPUT_CONTROL},
            {PaletteContentStyle.ButtonListItem, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_LIST_ITEM},
            {PaletteContentStyle.ButtonForm, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM},
            {PaletteContentStyle.ButtonFormClose, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_FORM_CLOSE},
            {PaletteContentStyle.ButtonCommand, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_COMMAND},
            {PaletteContentStyle.ButtonCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM1},
            {PaletteContentStyle.ButtonCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM2},
            {PaletteContentStyle.ButtonCustom3, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_BUTTON_CUSTOM3},
            {PaletteContentStyle.ContextMenuHeading, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_HEADING},
            {PaletteContentStyle.ContextMenuItemImage, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_IMAGE},
            {PaletteContentStyle.ContextMenuItemTextStandard, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_STANDARD},
            {PaletteContentStyle.ContextMenuItemTextAlternate, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_TEXT_ALTERNATE},
            {PaletteContentStyle.ContextMenuItemShortcutText, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_CONTEXT_MENU_ITEM_SHORTCUT_TEXT},
            {PaletteContentStyle.GridHeaderColumnList, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_LIST},
            {PaletteContentStyle.GridHeaderRowList, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_LIST},
            {PaletteContentStyle.GridDataCellList, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_LIST},
            {PaletteContentStyle.GridHeaderColumnSheet, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_SHEET},
            {PaletteContentStyle.GridHeaderRowSheet, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_SHEET},
            {PaletteContentStyle.GridDataCellSheet, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_SHEET},
            {PaletteContentStyle.GridHeaderColumnCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM1},
            {PaletteContentStyle.GridHeaderColumnCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM2},
            {PaletteContentStyle.GridHeaderColumnCustom3, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_COLUMN_CUSTOM3},
            {PaletteContentStyle.GridHeaderRowCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM1},
            {PaletteContentStyle.GridHeaderRowCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM2},
            {PaletteContentStyle.GridHeaderRowCustom3, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_HEADER_ROW_CUSTOM3},
            {PaletteContentStyle.GridDataCellCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM1},
            {PaletteContentStyle.GridDataCellCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM2},
            {PaletteContentStyle.GridDataCellCustom3, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_GRID_DATA_CELL_CUSTOM3},
            {PaletteContentStyle.HeaderPrimary, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_PRIMARY},
            {PaletteContentStyle.HeaderSecondary, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_SECONDARY},
            {PaletteContentStyle.HeaderDockActive, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_DOCK_ACTIVE},
            {PaletteContentStyle.HeaderDockInactive, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_DOCK_INACTIVE},
            {PaletteContentStyle.HeaderForm, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_FORM},
            {PaletteContentStyle.HeaderCalendar, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CALENDAR},
            {PaletteContentStyle.HeaderCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM1},
            {PaletteContentStyle.HeaderCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM2},
            {PaletteContentStyle.HeaderCustom3, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_HEADER_CUSTOM3},
            {PaletteContentStyle.LabelAlternateControl, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ALTERNATE_CONTROL},
            {PaletteContentStyle.LabelNormalControl, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_NORMAL_CONTROL},
            {PaletteContentStyle.LabelBoldControl, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_BOLD_CONTROL},
            {PaletteContentStyle.LabelItalicControl, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ITALIC_CONTROL},
            {PaletteContentStyle.LabelTitleControl, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TITLE_CONTROL},
            {PaletteContentStyle.LabelAlternatePanel, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ALTERNATE_PANEL},
            {PaletteContentStyle.LabelNormalPanel, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_NORMAL_PANEL},
            {PaletteContentStyle.LabelBoldPanel, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_BOLD_PANEL},
            {PaletteContentStyle.LabelItalicPanel, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_ITALIC_PANEL},
            {PaletteContentStyle.LabelTitlePanel, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TITLE_PANEL},
            {PaletteContentStyle.LabelGroupBoxCaption, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_GROUP_BOX_CAPTION},
            {PaletteContentStyle.LabelToolTip, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_TOOL_TIP},
            {PaletteContentStyle.LabelSuperTip, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_SUPER_TIP},
            {PaletteContentStyle.LabelKeyTip, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_KEY_TIP},
            {PaletteContentStyle.LabelCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_CUSTOM1},
            {PaletteContentStyle.LabelCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_LABEL_CUSTOM2},
            {PaletteContentStyle.TabHighProfile, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_HIGH_PROFILE},
            {PaletteContentStyle.TabStandardProfile, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_STANDARD_PROFILE},
            {PaletteContentStyle.TabLowProfile, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_LOW_PROFILE},
            {PaletteContentStyle.TabOneNote, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_ONE_NOTE},
            {PaletteContentStyle.TabDock, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_DOCK},
            {PaletteContentStyle.TabDockAutoHidden, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_DOCK_AUTO_HIDDEN},
            {PaletteContentStyle.TabCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM1},
            {PaletteContentStyle.TabCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM2},
            {PaletteContentStyle.TabCustom3, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_TAB_CUSTOM3},
            {PaletteContentStyle.InputControlStandalone, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_STANDALONE},
            {PaletteContentStyle.InputControlRibbon, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_RIBBON},
            {PaletteContentStyle.InputControlCustom1, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM1},
            {PaletteContentStyle.InputControlCustom2, DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM2},
            {PaletteContentStyle.InputControlCustom3,DesignTimeUtilities.DEFAULT_PALETTE_CONTENT_STYLE_INPUT_CONTROL_CUSTOM3}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteContentStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteContentStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}