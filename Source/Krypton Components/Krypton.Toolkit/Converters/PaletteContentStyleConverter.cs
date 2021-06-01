﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteContentStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteContentStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContentStyleConverter clas.
        /// </summary>
        public PaletteContentStyleConverter()
            : base(typeof(PaletteContentStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(PaletteContentStyle.ButtonStandalone,             "Button - Standalone"),
            new Pair(PaletteContentStyle.ButtonLowProfile,             "Button - Low Profile"),
            new Pair(PaletteContentStyle.ButtonButtonSpec,             "Button - ButtonSpec"),
            new Pair(PaletteContentStyle.ButtonBreadCrumb,             "Button - BreadCrumb"),
            new Pair(PaletteContentStyle.ButtonCalendarDay,            "Button - Calendar Day"),
            new Pair(PaletteContentStyle.ButtonCluster,                "Button - Cluster"),
            new Pair(PaletteContentStyle.ButtonGallery,                "Button - Gallery"),
            new Pair(PaletteContentStyle.ButtonNavigatorStack,         "Button - Navigator Stack"),
            new Pair(PaletteContentStyle.ButtonNavigatorOverflow,      "Button - Navigator Overflow"),
            new Pair(PaletteContentStyle.ButtonNavigatorMini,          "Button - Navigator Mini"),
            new Pair(PaletteContentStyle.ButtonInputControl,           "Button - Input Control"),
            new Pair(PaletteContentStyle.ButtonListItem,               "Button - List Item"),
            new Pair(PaletteContentStyle.ButtonForm,                   "Button - Form"),
            new Pair(PaletteContentStyle.ButtonFormClose,              "Button - Form Close"),
            new Pair(PaletteContentStyle.ButtonCommand,                "Button - Command"),
            new Pair(PaletteContentStyle.ButtonCustom1,                "Button - Custom1"),
            new Pair(PaletteContentStyle.ButtonCustom2,                "Button - Custom2"),
            new Pair(PaletteContentStyle.ButtonCustom3,                "Button - Custom3"),
            new Pair(PaletteContentStyle.ContextMenuHeading,           "ContextMenu - Heading"),
            new Pair(PaletteContentStyle.ContextMenuItemImage,         "ContextMenu - Item Image"),
            new Pair(PaletteContentStyle.ContextMenuItemTextStandard,  "ContextMenu - Item Text Standard"),
            new Pair(PaletteContentStyle.ContextMenuItemTextAlternate, "ContextMenu - Item Text Alternate"),
            new Pair(PaletteContentStyle.ContextMenuItemShortcutText,  "ContextMenu - Item ShortcutText"),
            new Pair(PaletteContentStyle.GridHeaderColumnList,         "Grid - HeaderColumn - List"),
            new Pair(PaletteContentStyle.GridHeaderRowList,            "Grid - RowColumn - List"),
            new Pair(PaletteContentStyle.GridDataCellList,             "Grid - DataCell - List"),
            new Pair(PaletteContentStyle.GridHeaderColumnSheet,        "Grid - HeaderColumn - Sheet"),
            new Pair(PaletteContentStyle.GridHeaderRowSheet,           "Grid - RowColumn - Sheet"),
            new Pair(PaletteContentStyle.GridDataCellSheet,            "Grid - DataCell - Sheet"),
            new Pair(PaletteContentStyle.GridHeaderColumnCustom1,      "Grid - HeaderColumn - Custom1"),
            new Pair(PaletteContentStyle.GridHeaderColumnCustom2,      "Grid - HeaderColumn - Custom2"),
            new Pair(PaletteContentStyle.GridHeaderColumnCustom3,      "Grid - HeaderColumn - Custom3"),
            new Pair(PaletteContentStyle.GridHeaderRowCustom1,         "Grid - RowColumn - Custom1"),
            new Pair(PaletteContentStyle.GridHeaderRowCustom2,         "Grid - RowColumn - Custom2"),
            new Pair(PaletteContentStyle.GridHeaderRowCustom3,         "Grid - RowColumn - Custom3"),
            new Pair(PaletteContentStyle.GridDataCellCustom1,          "Grid - DataCell - Custom1"),
            new Pair(PaletteContentStyle.GridDataCellCustom2,          "Grid - DataCell - Custom2"),
            new Pair(PaletteContentStyle.GridDataCellCustom3,          "Grid - DataCell - Custom3"),
            new Pair(PaletteContentStyle.HeaderPrimary,                "Header - Primary"),
            new Pair(PaletteContentStyle.HeaderSecondary,              "Header - Secondary"),
            new Pair(PaletteContentStyle.HeaderDockActive,             "Header - Dock - Active"),
            new Pair(PaletteContentStyle.HeaderDockInactive,           "Header - Dock - Inactive"),
            new Pair(PaletteContentStyle.HeaderForm,                   "Header - Form"),
            new Pair(PaletteContentStyle.HeaderCalendar,               "Header - Calendar"),
            new Pair(PaletteContentStyle.HeaderCustom1,                "Header - Custom1"),
            new Pair(PaletteContentStyle.HeaderCustom2,                "Header - Custom2"),
            new Pair(PaletteContentStyle.HeaderCustom3,                "Header - Custom3"),
            new Pair(PaletteContentStyle.LabelNormalControl,           "Label - Normal (Control)"),
            new Pair(PaletteContentStyle.LabelBoldControl,             "Label - Bold (Control)"),
            new Pair(PaletteContentStyle.LabelTitleControl,            "Label - Italic (Control)"),
            new Pair(PaletteContentStyle.LabelTitleControl,            "Label - Title (Control)"),
            new Pair(PaletteContentStyle.LabelNormalPanel,             "Label - Normal (Panel)"),
            new Pair(PaletteContentStyle.LabelBoldPanel,               "Label - Bold (Panel)"),
            new Pair(PaletteContentStyle.LabelItalicPanel,             "Label - Italic (Panel)"),
            new Pair(PaletteContentStyle.LabelTitlePanel,              "Label - Title (Panel)"),
            new Pair(PaletteContentStyle.LabelGroupBoxCaption,         "Label - Caption (Panel)"),
            new Pair(PaletteContentStyle.LabelToolTip,                 "Label - ToolTip"),
            new Pair(PaletteContentStyle.LabelSuperTip,                "Label - SuperTip"),
            new Pair(PaletteContentStyle.LabelKeyTip,                  "Label - KeyTip"),
            new Pair(PaletteContentStyle.LabelCustom1,                 "Label - Custom1"),
            new Pair(PaletteContentStyle.LabelCustom2,                 "Label - Custom2"),
            new Pair(PaletteContentStyle.TabHighProfile,               "Tab - High Profile"),
            new Pair(PaletteContentStyle.TabStandardProfile,           "Tab - Standard Profile"),
            new Pair(PaletteContentStyle.TabLowProfile,                "Tab - Low Profile"),
            new Pair(PaletteContentStyle.TabOneNote,                   "Tab - OneNote"),
            new Pair(PaletteContentStyle.TabDock,                      "Tab - Dock"),
            new Pair(PaletteContentStyle.TabDockAutoHidden,            "Tab - Dock AutoHidden"),
            new Pair(PaletteContentStyle.TabCustom1,                   "Tab - Custom1"),
            new Pair(PaletteContentStyle.TabCustom2,                   "Tab - Custom2"),
            new Pair(PaletteContentStyle.TabCustom3,                   "Tab - Custom3"),
            new Pair(PaletteContentStyle.InputControlStandalone,       "InputControl - Standalone"),
            new Pair(PaletteContentStyle.InputControlRibbon,           "InputControl - Ribbon"),
            new Pair(PaletteContentStyle.InputControlCustom1,          "InputControl - Custom1"),
            new Pair(PaletteContentStyle.InputControlCustom2,          "InputControl - Custom2"),
            new Pair(PaletteContentStyle.InputControlCustom3,          "InputControl - Custom3")
        };

        #endregion
    }
}
