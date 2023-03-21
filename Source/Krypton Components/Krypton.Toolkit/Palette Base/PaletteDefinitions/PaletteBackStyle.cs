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
    /// Specifies the style of background.
    /// </summary>
    [TypeConverter(typeof(PaletteBackStyleConverter))]
    public enum PaletteBackStyle
    {
        /// <summary>
        /// Specifies a background style appropriate for a standalone button style.
        /// </summary>
        ButtonStandalone,

        /// <summary>
        /// Specifies a background style appropriate for an alternate standalone button style.
        /// </summary>
        ButtonAlternate,

        /// <summary>
        /// Specifies a background style appropriate for a low profile button style.
        /// </summary>
        ButtonLowProfile,

        /// <summary>
        /// Specifies a background style appropriate for a button spec.
        /// </summary>
        ButtonButtonSpec,

        /// <summary>
        /// Specifies a background style appropriate for a bread crumb.
        /// </summary>
        ButtonBreadCrumb,

        /// <summary>
        /// Specifies a background style appropriate for a calendar day.
        /// </summary>
        ButtonCalendarDay,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon cluster button.
        /// </summary>
        ButtonCluster,

        /// <summary>
        /// Specifies a background style appropriate for a gallery button style.
        /// </summary>
        ButtonGallery,

        /// <summary>
        /// Specifies a background style appropriate for a navigator stack.
        /// </summary>
        ButtonNavigatorStack,

        /// <summary>
        /// Specifies a background style appropriate for a navigator overflow button.
        /// </summary>
        ButtonNavigatorOverflow,

        /// <summary>
        /// Specifies a background style appropriate for a navigator mini button.
        /// </summary>
        ButtonNavigatorMini,

        /// <summary>
        /// Specifies a background style appropriate for an input control button.
        /// </summary>
        ButtonInputControl,

        /// <summary>
        /// Specifies a background style appropriate for a list item button.
        /// </summary>
        ButtonListItem,

        /// <summary>
        /// Specifies a background style appropriate for a form level button.
        /// </summary>
        ButtonForm,

        /// <summary>
        /// Specifies a background style appropriate for a form level close button.
        /// </summary>
        ButtonFormClose,

        /// <summary>
        /// Specifies a background style appropriate for a command button.
        /// </summary>
        ButtonCommand,

        /// <summary>
        /// Specifies a background style appropriate for the first custom button style.
        /// </summary>
        ButtonCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom button style.
        /// </summary>
        ButtonCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom button style.
        /// </summary>
        ButtonCustom3,

        /// <summary>
        /// Specifies a background style appropriate for a client control style.
        /// </summary>
        ControlClient,

        /// <summary>
        /// Specifies a background style appropriate for an alternate control style.
        /// </summary>
        ControlAlternate,

        /// <summary>
        /// Specifies a background style appropriate for a group box control style.
        /// </summary>
        ControlGroupBox,

        /// <summary>
        /// Specifies a background style appropriate for a tool tip popup.
        /// </summary>
        ControlToolTip,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon style control.
        /// </summary>
        ControlRibbon,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon application button menu control.
        /// </summary>
        ControlRibbonAppMenu,

        /// <summary>
        /// Specifies a background style appropriate for the first custom control style.
        /// </summary>
        ControlCustom1,
        ControlCustom2,
        ControlCustom3,

        /// <summary>
        /// Specifies a background style appropriate for the outer part of a context menu control.
        /// </summary>
        ContextMenuOuter,

        /// <summary>
        /// Specifies a background style appropriate for the inner part of a context menu control.
        /// </summary>
        ContextMenuInner,

        /// <summary>
        /// Specifies a background style appropriate for a context menu heading.
        /// </summary>
        ContextMenuHeading,

        /// <summary>
        /// Specifies a background style appropriate for a context menu separator.
        /// </summary>
        ContextMenuSeparator,

        /// <summary>
        /// Specifies a background style appropriate for a context menu image.
        /// </summary>
        ContextMenuItemImage,

        /// <summary>
        /// Specifies a background style appropriate for the vertical split of a context menu item.
        /// </summary>
        ContextMenuItemSplit,

        /// <summary>
        /// Specifies a background style appropriate for a context menu image column.
        /// </summary>
        ContextMenuItemImageColumn,

        /// <summary>
        /// Specifies a background style appropriate for a context menu highlight column.
        /// </summary>
        ContextMenuItemHighlight,

        /// <summary>
        /// Specifies a background style appropriate for a standalone input control.
        /// </summary>
        InputControlStandalone,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon style input control.
        /// </summary>
        InputControlRibbon,

        /// <summary>
        /// Specifies a background style appropriate for the first custom input control style.
        /// </summary>
        InputControlCustom1,
        InputControlCustom2,
        InputControlCustom3,

        /// <summary>
        /// Specifies a background style appropriate for column headers in a list style grid.
        /// </summary>
        GridHeaderColumnList,

        /// <summary>
        /// Specifies a background style appropriate for row headers in a list style grid.
        /// </summary>
        GridHeaderRowList,

        /// <summary>
        /// Specifies a background style appropriate for data cells in a list style grid.
        /// </summary>
        GridDataCellList,

        /// <summary>
        /// Specifies a background style appropriate for blank areas in a list style grid.
        /// </summary>
        GridBackgroundList,

        /// <summary>
        /// Specifies a background style appropriate for column headers in a sheet style grid.
        /// </summary>
        GridHeaderColumnSheet,

        /// <summary>
        /// Specifies a background style appropriate for row headers in a sheet style grid.
        /// </summary>
        GridHeaderRowSheet,

        /// <summary>
        /// Specifies a background style appropriate for data cells in a sheet style grid.
        /// </summary>
        GridDataCellSheet,

        /// <summary>
        /// Specifies a background style appropriate for blank areas in a sheet style grid.
        /// </summary>
        GridBackgroundSheet,

        /// <summary>
        /// Specifies a background style appropriate for column headers in a custom grid style.
        /// </summary>
        GridHeaderColumnCustom1,
        GridHeaderColumnCustom2,
        GridHeaderColumnCustom3,

        /// <summary>
        /// Specifies a background style appropriate for row headers in a custom grid style.
        /// </summary>
        GridHeaderRowCustom1,
        GridHeaderRowCustom2,
        GridHeaderRowCustom3,

        /// <summary>
        /// Specifies a background style appropriate for data cells in a custom grid style.
        /// </summary>
        GridDataCellCustom1,
        GridDataCellCustom2,
        GridDataCellCustom3,

        /// <summary>
        /// Specifies a background style appropriate for blank areas in a custom grid style.
        /// </summary>
        GridBackgroundCustom1,
        GridBackgroundCustom2,
        GridBackgroundCustom3,

        /// <summary>
        /// Specifies a background style appropriate for a primary header style.
        /// </summary>
        HeaderPrimary,

        /// <summary>
        /// Specifies a background style appropriate for a secondary header style.
        /// </summary>
        HeaderSecondary,

        /// <summary>
        /// Specifies a background style appropriate for an inactive docking header.
        /// </summary>
        HeaderDockInactive,

        /// <summary>
        /// Specifies a background style appropriate for an active docking header.
        /// </summary>
        HeaderDockActive,

        /// <summary>
        /// Specifies a background style appropriate for a main form header style.
        /// </summary>
        HeaderForm,

        /// <summary>
        /// Specifies a background style appropriate for a calendar title area.
        /// </summary>
        HeaderCalendar,

        /// <summary>
        /// Specifies a background style appropriate for the first custom header style.
        /// </summary>
        HeaderCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom header style.
        /// </summary>
        HeaderCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom header style.
        /// </summary>
        HeaderCustom3,

        /// <summary>
        /// Specifies a background style appropriate for a client panel style.
        /// </summary>
        PanelClient,

        /// <summary>
        /// Specifies a background style appropriate for an alternate panel style.
        /// </summary>
        PanelAlternate,

        /// <summary>
        /// Specifies a background style appropriate for an inactive ribbon.
        /// </summary>
        PanelRibbonInactive,

        /// <summary>
        /// Specifies a background style appropriate for the first custom panel style.
        /// </summary>
        PanelCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second  custom panel style.
        /// </summary>
        PanelCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom panel style.
        /// </summary>
        PanelCustom3,

        /// <summary>
        /// Specifies a background style appropriate for a low profile separator style.
        /// </summary>
        SeparatorLowProfile,

        /// <summary>
        /// Specifies a background style appropriate for a high profile separator style.
        /// </summary>
        SeparatorHighProfile,

        /// <summary>
        /// Specifies a background style appropriate for a high profile for internal separator style.
        /// </summary>
        SeparatorHighInternalProfile,

        /// <summary>
        /// Specifies a background style appropriate for the first custom separator style.
        /// </summary>
        SeparatorCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom separator style.
        /// </summary>
        SeparatorCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom separator style.
        /// </summary>
        SeparatorCustom3,

        /// <summary>
        /// Specifies a background style appropriate for a high profile tab.
        /// </summary>
        TabHighProfile,

        /// <summary>
        /// Specifies a background style appropriate for a standard profile tab.
        /// </summary>
        TabStandardProfile,

        /// <summary>
        /// Specifies a background style appropriate for a low profile tab.
        /// </summary>
        TabLowProfile,

        /// <summary>
        /// Specifies a background style appropriate for a OneNote tab.
        /// </summary>
        TabOneNote,

        /// <summary>
        /// Specifies a background style appropriate for a docking tab.
        /// </summary>
        TabDock,

        /// <summary>
        /// Specifies a background style appropriate for a auto hidden docking tab.
        /// </summary>
        TabDockAutoHidden,

        /// <summary>
        /// Specifies a background style appropriate for the first custom tab style.
        /// </summary>
        TabCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom tab style.
        /// </summary>
        TabCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom tab style.
        /// </summary>
        TabCustom3,

        /// <summary>
        /// Specifies a background style appropriate for a main form.
        /// </summary>
        FormMain,

        /// <summary>
        /// Specifies a background style appropriate for the first custom form style.
        /// </summary>
        FormCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom form style.
        /// </summary>
        FormCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom form style.
        /// </summary>
        FormCustom3
    }
}