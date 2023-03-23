#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Specifies the style of content.
    /// </summary>
    [TypeConverter(typeof(PaletteContentStyleConverter))]
    public enum PaletteContentStyle
    {
        /// <summary>
        /// Specifies a content style appropriate for a standalone button style.
        /// </summary>
        ButtonStandalone,

        /// <summary>
        /// Specifies a content style appropriate for an alternate standalone button style.
        /// </summary>
        ButtonAlternate,

        /// <summary>
        /// Specifies a content style appropriate for a low profile button style.
        /// </summary>
        ButtonLowProfile,

        /// <summary>
        /// Specifies a content style appropriate for a button spec.
        /// </summary>
        ButtonButtonSpec,

        /// <summary>
        /// Specifies a content style appropriate for a bread crumb.
        /// </summary>
        ButtonBreadCrumb,

        /// <summary>
        /// Specifies a content style appropriate for a calendar day.
        /// </summary>
        ButtonCalendarDay,

        /// <summary>
        /// Specifies a content style appropriate for a ribbon cluster button.
        /// </summary>
        ButtonCluster,

        /// <summary>
        /// Specifies a content style appropriate for a ribbon gallery button.
        /// </summary>
        ButtonGallery,

        /// <summary>
        /// Specifies a content style appropriate for a navigator stack.
        /// </summary>
        ButtonNavigatorStack,

        /// <summary>
        /// Specifies a content style appropriate for a navigator overflow button.
        /// </summary>
        ButtonNavigatorOverflow,

        /// <summary>
        /// Specifies a content style appropriate for a navigator mini button.
        /// </summary>
        ButtonNavigatorMini,

        /// <summary>
        /// Specifies a content style appropriate for an input control button.
        /// </summary>
        ButtonInputControl,

        /// <summary>
        /// Specifies a content style appropriate for a list item button.
        /// </summary>
        ButtonListItem,

        /// <summary>
        /// Specifies a content style appropriate for a form level button.
        /// </summary>
        ButtonForm,

        /// <summary>
        /// Specifies a content style appropriate for a form level close button.
        /// </summary>
        ButtonFormClose,

        /// <summary>
        /// Specifies a content style appropriate for a command button.
        /// </summary>
        ButtonCommand,

        /// <summary>
        /// Specifies a content style appropriate for the first custom button style.
        /// </summary>
        ButtonCustom1,

        /// <summary>
        /// Specifies a content style appropriate for the second custom button style.
        /// </summary>
        ButtonCustom2,

        /// <summary>
        /// Specifies a content style appropriate for the third custom button style.
        /// </summary>
        ButtonCustom3,

        /// <summary>
        /// Specifies a content style appropriate for a context menu heading.
        /// </summary>
        ContextMenuHeading,

        /// <summary>
        /// Specifies a content style appropriate for the image of a context menu item.
        /// </summary>
        ContextMenuItemImage,

        /// <summary>
        /// Specifies a content style appropriate for the text/extra text of a standard context menu item.
        /// </summary>
        ContextMenuItemTextStandard,

        /// <summary>
        /// Specifies a content style appropriate for the text/extra text of a alternate context menu item.
        /// </summary>
        ContextMenuItemTextAlternate,

        /// <summary>
        /// Specifies a content style appropriate for the shortcut text of a context menu item.
        /// </summary>
        ContextMenuItemShortcutText,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a list style grid.
        /// </summary>
        GridHeaderColumnList,

        /// <summary>
        /// Specifies a border style appropriate for column rows in a list style grid.
        /// </summary>
        GridHeaderRowList,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a list style grid.
        /// </summary>
        GridDataCellList,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a sheet style grid.
        /// </summary>
        GridHeaderColumnSheet,

        /// <summary>
        /// Specifies a border style appropriate for column rows in a sheet style grid.
        /// </summary>
        GridHeaderRowSheet,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a sheet style grid.
        /// </summary>
        GridDataCellSheet,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a custom grid style.
        /// </summary>
        GridHeaderColumnCustom1,
        GridHeaderColumnCustom2,
        GridHeaderColumnCustom3,

        /// <summary>
        /// Specifies a border style appropriate for column rows in a custom grid style.
        /// </summary>
        GridHeaderRowCustom1,
        GridHeaderRowCustom2,
        GridHeaderRowCustom3,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a custom grid style.
        /// </summary>
        GridDataCellCustom1,
        GridDataCellCustom2,
        GridDataCellCustom3,

        /// <summary>
        /// Specifies a content style appropriate for a primary Header.
        /// </summary>
        HeaderPrimary,

        /// <summary>
        /// Specifies a content style appropriate for a secondary Header.
        /// </summary>
        HeaderSecondary,

        /// <summary>
        /// Specifies a content style appropriate for an inactive docking header.
        /// </summary>
        HeaderDockInactive,

        /// <summary>
        /// Specifies a content style appropriate for an active docking header.
        /// </summary>
        HeaderDockActive,

        /// <summary>
        /// Specifies a content style appropriate for a main form header style.
        /// </summary>
        HeaderForm,

        /// <summary>
        /// Specifies a content style appropriate for a calendar title area.
        /// </summary>
        HeaderCalendar,

        /// <summary>
        /// Specifies a content style appropriate for the first custom header style.
        /// </summary>
        HeaderCustom1,

        /// <summary>
        /// Specifies a content style appropriate for the second custom header style.
        /// </summary>
        HeaderCustom2,
        HeaderCustom3,

        /// <summary>
        /// Specifies a normal label for use on a control style background.
        /// </summary>
        LabelNormalControl,

        /// <summary>
        /// Specifies a bold label for use on a control style background.
        /// </summary>
        LabelBoldControl,

        /// <summary>
        /// Specifies an italic label for use on a control style background.
        /// </summary>
        LabelItalicControl,

        /// <summary>
        /// Specifies a label appropriate for titles for use on a control style background.
        /// </summary>
        LabelTitleControl,

        /// <summary>
        /// Specifies a normal label for use on a panel style background.
        /// </summary>
        LabelNormalPanel,

        /// <summary>
        /// Specifies a bold label for use on a panel style background.
        /// </summary>
        LabelBoldPanel,

        /// <summary>
        /// Specifies a italic label for use on a panel style background.
        /// </summary>
        LabelItalicPanel,

        /// <summary>
        /// Specifies a label appropriate for titles for use on a panel style background.
        /// </summary>
        LabelTitlePanel,

        /// <summary>
        /// Specifies a normal label for use on a group box panel style background.
        /// </summary>
        LabelGroupBoxCaption,

        /// <summary>
        /// Specifies a label style appropriate for a tooltip popup.
        /// </summary>
        LabelToolTip,

        /// <summary>
        /// Specifies a label style appropriate for a super tooltip popup.
        /// </summary>
        LabelSuperTip,

        /// <summary>
        /// Specifies a label style appropriate for a key tooltip popup.
        /// </summary>
        LabelKeyTip,

        /// <summary>
        /// Specifies the first custom label style.
        /// </summary>
        LabelCustom1,

        /// <summary>
        /// Specifies the second custom label style.
        /// </summary>
        LabelCustom2,

        /// <summary>
        /// Specifies the third custom label style.
        /// </summary>
        LabelCustom3,

        /// <summary>
        /// Specifies a content style appropriate for a high profile tab.
        /// </summary>
        TabHighProfile,

        /// <summary>
        /// Specifies a content style appropriate for a standard profile tab.
        /// </summary>
        TabStandardProfile,

        /// <summary>
        /// Specifies a content style appropriate for a low profile tab.
        /// </summary>
        TabLowProfile,

        /// <summary>
        /// Specifies a content style appropriate for a OneNote tab.
        /// </summary>
        TabOneNote,

        /// <summary>
        /// Specifies a content style appropriate for a docking tab.
        /// </summary>
        TabDock,

        /// <summary>
        /// Specifies a content style appropriate for a auto hidden docking tab.
        /// </summary>
        TabDockAutoHidden,

        /// <summary>
        /// Specifies a content style appropriate for the first custom tab style.
        /// </summary>
        TabCustom1,

        /// <summary>
        /// Specifies a content style appropriate for the second custom tab style.
        /// </summary>
        TabCustom2,

        /// <summary>
        /// Specifies a content style appropriate for the third custom tab style.
        /// </summary>
        TabCustom3,

        /// <summary>
        /// Specifies a content style appropriate for a standalone input control.
        /// </summary>
        InputControlStandalone,

        /// <summary>
        /// Specifies a content style appropriate for a ribbon style input control.
        /// </summary>
        InputControlRibbon,

        /// <summary>
        /// Specifies a content style appropriate for the first custom input control style.
        /// </summary>
        InputControlCustom1,
        InputControlCustom2,
        InputControlCustom3
    }
}