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
    /// Custom type converter so that PaletteContentStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteContentStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(PaletteContentStyle.ButtonStandalone, "Button - Standalone"),
        //    new(PaletteContentStyle.ButtonLowProfile, "Button - Low Profile"),
        //    new(PaletteContentStyle.ButtonButtonSpec, "Button - ButtonSpec"),
        //    new(PaletteContentStyle.ButtonBreadCrumb, "Button - BreadCrumb"),
        //    new(PaletteContentStyle.ButtonCalendarDay, "Button - Calendar Day"),
        //    new(PaletteContentStyle.ButtonCluster, "Button - Cluster"),
        //    new(PaletteContentStyle.ButtonGallery, "Button - Gallery"),
        //    new(PaletteContentStyle.ButtonNavigatorStack, "Button - Navigator Stack"),
        //    new(PaletteContentStyle.ButtonNavigatorOverflow, "Button - Navigator Overflow"),
        //    new(PaletteContentStyle.ButtonNavigatorMini, "Button - Navigator Mini"),
        //    new(PaletteContentStyle.ButtonInputControl, "Button - Input Control"),
        //    new(PaletteContentStyle.ButtonListItem, "Button - List Item"),
        //    new(PaletteContentStyle.ButtonForm, "Button - Form"),
        //    new(PaletteContentStyle.ButtonFormClose, "Button - Form Close"),
        //    new(PaletteContentStyle.ButtonCommand, "Button - Command"),
        //    new(PaletteContentStyle.ButtonCustom1, "Button - Custom1"),
        //    new(PaletteContentStyle.ButtonCustom2, "Button - Custom2"),
        //    new(PaletteContentStyle.ButtonCustom3, "Button - Custom3"),
        //    new(PaletteContentStyle.ContextMenuHeading, "ContextMenu - Heading"),
        //    new(PaletteContentStyle.ContextMenuItemImage, "ContextMenu - Item Image"),
        //    new(PaletteContentStyle.ContextMenuItemTextStandard, "ContextMenu - Item Text Standard"),
        //    new(PaletteContentStyle.ContextMenuItemTextAlternate, "ContextMenu - Item Text Alternate"),
        //    new(PaletteContentStyle.ContextMenuItemShortcutText, "ContextMenu - Item ShortcutText"),
        //    new(PaletteContentStyle.GridHeaderColumnList, "Grid - HeaderColumn - List"),
        //    new(PaletteContentStyle.GridHeaderRowList, "Grid - RowColumn - List"),
        //    new(PaletteContentStyle.GridDataCellList, "Grid - DataCell - List"),
        //    new(PaletteContentStyle.GridHeaderColumnSheet, "Grid - HeaderColumn - Sheet"),
        //    new(PaletteContentStyle.GridHeaderRowSheet, "Grid - RowColumn - Sheet"),
        //    new(PaletteContentStyle.GridDataCellSheet, "Grid - DataCell - Sheet"),
        //    new(PaletteContentStyle.GridHeaderColumnCustom1, "Grid - HeaderColumn - Custom1"),
        //    new(PaletteContentStyle.GridHeaderColumnCustom2, "Grid - HeaderColumn - Custom2"),
        //    new(PaletteContentStyle.GridHeaderColumnCustom3, "Grid - HeaderColumn - Custom3"),
        //    new(PaletteContentStyle.GridHeaderRowCustom1, "Grid - RowColumn - Custom1"),
        //    new(PaletteContentStyle.GridHeaderRowCustom2, "Grid - RowColumn - Custom2"),
        //    new(PaletteContentStyle.GridHeaderRowCustom3, "Grid - RowColumn - Custom3"),
        //    new(PaletteContentStyle.GridDataCellCustom1, "Grid - DataCell - Custom1"),
        //    new(PaletteContentStyle.GridDataCellCustom2, "Grid - DataCell - Custom2"),
        //    new(PaletteContentStyle.GridDataCellCustom3, "Grid - DataCell - Custom3"),
        //    new(PaletteContentStyle.HeaderPrimary, "Header - Primary"),
        //    new(PaletteContentStyle.HeaderSecondary, "Header - Secondary"),
        //    new(PaletteContentStyle.HeaderDockActive, "Header - Dock - Active"),
        //    new(PaletteContentStyle.HeaderDockInactive, "Header - Dock - Inactive"),
        //    new(PaletteContentStyle.HeaderForm, "Header - Form"),
        //    new(PaletteContentStyle.HeaderCalendar, "Header - Calendar"),
        //    new(PaletteContentStyle.HeaderCustom1, "Header - Custom1"),
        //    new(PaletteContentStyle.HeaderCustom2, "Header - Custom2"),
        //    new(PaletteContentStyle.HeaderCustom3, "Header - Custom3"),
        //    new(PaletteContentStyle.LabelNormalControl, "Label - Normal (Control)"),
        //    new(PaletteContentStyle.LabelBoldControl, "Label - Bold (Control)"),
        //    new(PaletteContentStyle.LabelItalicControl, "Label - Italic (Control)"),
        //    new(PaletteContentStyle.LabelTitleControl, "Label - Title (Control)"),
        //    new(PaletteContentStyle.LabelNormalPanel, "Label - Normal (Panel)"),
        //    new(PaletteContentStyle.LabelBoldPanel, "Label - Bold (Panel)"),
        //    new(PaletteContentStyle.LabelItalicPanel, "Label - Italic (Panel)"),
        //    new(PaletteContentStyle.LabelTitlePanel, "Label - Title (Panel)"),
        //    new(PaletteContentStyle.LabelGroupBoxCaption, "Label - Caption (Panel)"),
        //    new(PaletteContentStyle.LabelToolTip, "Label - ToolTip"),
        //    new(PaletteContentStyle.LabelSuperTip, "Label - SuperTip"),
        //    new(PaletteContentStyle.LabelKeyTip, "Label - KeyTip"),
        //    new(PaletteContentStyle.LabelCustom1, "Label - Custom1"),
        //    new(PaletteContentStyle.LabelCustom2, "Label - Custom2"),
        //    new(PaletteContentStyle.TabHighProfile, "Tab - High Profile"),
        //    new(PaletteContentStyle.TabStandardProfile, "Tab - Standard Profile"),
        //    new(PaletteContentStyle.TabLowProfile, "Tab - Low Profile"),
        //    new(PaletteContentStyle.TabOneNote, "Tab - OneNote"),
        //    new(PaletteContentStyle.TabDock, "Tab - Dock"),
        //    new(PaletteContentStyle.TabDockAutoHidden, "Tab - Dock AutoHidden"),
        //    new(PaletteContentStyle.TabCustom1, "Tab - Custom1"),
        //    new(PaletteContentStyle.TabCustom2, "Tab - Custom2"),
        //    new(PaletteContentStyle.TabCustom3, "Tab - Custom3"),
        //    new(PaletteContentStyle.InputControlStandalone, "InputControl - Standalone"),
        //    new(PaletteContentStyle.InputControlRibbon, "InputControl - Ribbon"),
        //    new(PaletteContentStyle.InputControlCustom1, "InputControl - Custom1"),
        //    new(PaletteContentStyle.InputControlCustom2, "InputControl - Custom2"),
        //    new(PaletteContentStyle.InputControlCustom3, "InputControl - Custom3")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new(PaletteContentStyle.ButtonStandalone, KryptonLanguageManager.ContentStyleStrings.ButtonStandalone),
            new(PaletteContentStyle.ButtonLowProfile, KryptonLanguageManager.ContentStyleStrings.ButtonLowProfile),
            new(PaletteContentStyle.ButtonButtonSpec, KryptonLanguageManager.ContentStyleStrings.ButtonButtonSpec),
            new(PaletteContentStyle.ButtonBreadCrumb, KryptonLanguageManager.ContentStyleStrings.ButtonBreadCrumb),
            new(PaletteContentStyle.ButtonCalendarDay, KryptonLanguageManager.ContentStyleStrings.ButtonCalendarDay),
            new(PaletteContentStyle.ButtonCluster, KryptonLanguageManager.ContentStyleStrings.ButtonCluster),
            new(PaletteContentStyle.ButtonGallery, KryptonLanguageManager.ContentStyleStrings.ButtonGallery),
            new(PaletteContentStyle.ButtonNavigatorStack, KryptonLanguageManager.ContentStyleStrings.ButtonNavigatorStack),
            new(PaletteContentStyle.ButtonNavigatorOverflow, KryptonLanguageManager.ContentStyleStrings.ButtonNavigatorOverflow),
            new(PaletteContentStyle.ButtonNavigatorMini, KryptonLanguageManager.ContentStyleStrings.ButtonNavigatorMini),
            new(PaletteContentStyle.ButtonInputControl, KryptonLanguageManager.ContentStyleStrings.ButtonInputControl),
            new(PaletteContentStyle.ButtonListItem, KryptonLanguageManager.ContentStyleStrings.ButtonListItem),
            new(PaletteContentStyle.ButtonForm, KryptonLanguageManager.ContentStyleStrings.ButtonForm),
            new(PaletteContentStyle.ButtonFormClose, KryptonLanguageManager.ContentStyleStrings.ButtonFormClose),
            new(PaletteContentStyle.ButtonCommand, KryptonLanguageManager.ContentStyleStrings.ButtonCommand),
            new(PaletteContentStyle.ButtonCustom1, KryptonLanguageManager.ContentStyleStrings.ButtonCustom1),
            new(PaletteContentStyle.ButtonCustom2, KryptonLanguageManager.ContentStyleStrings.ButtonCustom2),
            new(PaletteContentStyle.ButtonCustom3, KryptonLanguageManager.ContentStyleStrings.ButtonCustom3),
            new(PaletteContentStyle.ContextMenuHeading, KryptonLanguageManager.ContentStyleStrings.ContextMenuHeading),
            new(PaletteContentStyle.ContextMenuItemImage, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemImage),
            new(PaletteContentStyle.ContextMenuItemTextStandard, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemTextStandard),
            new(PaletteContentStyle.ContextMenuItemTextAlternate, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemTextAlternate),
            new(PaletteContentStyle.ContextMenuItemShortcutText, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemShortcutText),
            new(PaletteContentStyle.GridHeaderColumnList, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnList),
            new(PaletteContentStyle.GridHeaderRowList, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowList),
            new(PaletteContentStyle.GridDataCellList, KryptonLanguageManager.ContentStyleStrings.GridDataCellList),
            new(PaletteContentStyle.GridHeaderColumnSheet, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnSheet),
            new(PaletteContentStyle.GridHeaderRowSheet, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowSheet),
            new(PaletteContentStyle.GridDataCellSheet, KryptonLanguageManager.ContentStyleStrings.GridDataCellSheet),
            new(PaletteContentStyle.GridHeaderColumnCustom1, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnCustom1),
            new(PaletteContentStyle.GridHeaderColumnCustom2, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnCustom2),
            new(PaletteContentStyle.GridHeaderColumnCustom3, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnCustom3),
            new(PaletteContentStyle.GridHeaderRowCustom1, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowCustom1),
            new(PaletteContentStyle.GridHeaderRowCustom2, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowCustom2),
            new(PaletteContentStyle.GridHeaderRowCustom3, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowCustom3),
            new(PaletteContentStyle.GridDataCellCustom1, KryptonLanguageManager.ContentStyleStrings.GridDataCellCustom1),
            new(PaletteContentStyle.GridDataCellCustom2, KryptonLanguageManager.ContentStyleStrings.GridDataCellCustom2),
            new(PaletteContentStyle.GridDataCellCustom3, KryptonLanguageManager.ContentStyleStrings.GridDataCellCustom3),
            new(PaletteContentStyle.HeaderPrimary, KryptonLanguageManager.ContentStyleStrings.HeaderPrimary),
            new(PaletteContentStyle.HeaderSecondary, KryptonLanguageManager.ContentStyleStrings.HeaderSecondary),
            new(PaletteContentStyle.HeaderDockActive, KryptonLanguageManager.ContentStyleStrings.HeaderDockActive),
            new(PaletteContentStyle.HeaderDockInactive, KryptonLanguageManager.ContentStyleStrings.HeaderDockInactive),
            new(PaletteContentStyle.HeaderForm, KryptonLanguageManager.ContentStyleStrings.HeaderForm),
            new(PaletteContentStyle.HeaderCalendar, KryptonLanguageManager.ContentStyleStrings.HeaderCalendar),
            new(PaletteContentStyle.HeaderCustom1, KryptonLanguageManager.ContentStyleStrings.HeaderCustom1),
            new(PaletteContentStyle.HeaderCustom2, KryptonLanguageManager.ContentStyleStrings.HeaderCustom2),
            new(PaletteContentStyle.HeaderCustom3, KryptonLanguageManager.ContentStyleStrings.HeaderCustom3),
            new(PaletteContentStyle.LabelNormalControl, KryptonLanguageManager.ContentStyleStrings.LabelNormalControl),
            new(PaletteContentStyle.LabelBoldControl, KryptonLanguageManager.ContentStyleStrings.LabelBoldControl),
            new(PaletteContentStyle.LabelItalicControl, KryptonLanguageManager.ContentStyleStrings.LabelItalicControl),
            new(PaletteContentStyle.LabelTitleControl, KryptonLanguageManager.ContentStyleStrings.LabelTitleControl),
            new(PaletteContentStyle.LabelNormalPanel, KryptonLanguageManager.ContentStyleStrings.LabelNormalPanel),
            new(PaletteContentStyle.LabelBoldPanel, KryptonLanguageManager.ContentStyleStrings.LabelBoldPanel),
            new(PaletteContentStyle.LabelItalicPanel, KryptonLanguageManager.ContentStyleStrings.LabelItalicPanel),
            new(PaletteContentStyle.LabelTitlePanel, KryptonLanguageManager.ContentStyleStrings.LabelTitlePanel),
            new(PaletteContentStyle.LabelGroupBoxCaption, KryptonLanguageManager.ContentStyleStrings.LabelGroupBoxCaption),
            new(PaletteContentStyle.LabelToolTip, KryptonLanguageManager.ContentStyleStrings.LabelToolTip),
            new(PaletteContentStyle.LabelSuperTip, KryptonLanguageManager.ContentStyleStrings.LabelSuperTip),
            new(PaletteContentStyle.LabelKeyTip, KryptonLanguageManager.ContentStyleStrings.LabelKeyTip),
            new(PaletteContentStyle.LabelCustom1, KryptonLanguageManager.ContentStyleStrings.LabelCustom1),
            new(PaletteContentStyle.LabelCustom2, KryptonLanguageManager.ContentStyleStrings.LabelCustom2),
            new(PaletteContentStyle.TabHighProfile, KryptonLanguageManager.ContentStyleStrings.TabHighProfile),
            new(PaletteContentStyle.TabStandardProfile, KryptonLanguageManager.ContentStyleStrings.TabStandardProfile),
            new(PaletteContentStyle.TabLowProfile, KryptonLanguageManager.ContentStyleStrings.TabLowProfile),
            new(PaletteContentStyle.TabOneNote, KryptonLanguageManager.ContentStyleStrings.TabOneNote),
            new(PaletteContentStyle.TabDock, KryptonLanguageManager.ContentStyleStrings.TabDock),
            new(PaletteContentStyle.TabDockAutoHidden, KryptonLanguageManager.ContentStyleStrings.TabDockAutoHidden),
            new(PaletteContentStyle.TabCustom1, KryptonLanguageManager.ContentStyleStrings.TabCustom1),
            new(PaletteContentStyle.TabCustom2, KryptonLanguageManager.ContentStyleStrings.TabCustom2),
            new(PaletteContentStyle.TabCustom3, KryptonLanguageManager.ContentStyleStrings.TabCustom3),
            new(PaletteContentStyle.InputControlStandalone, KryptonLanguageManager.ContentStyleStrings.InputControlStandalone),
            new(PaletteContentStyle.InputControlRibbon, KryptonLanguageManager.ContentStyleStrings.InputControlRibbon),
            new(PaletteContentStyle.InputControlCustom1, KryptonLanguageManager.ContentStyleStrings.InputControlCustom1),
            new(PaletteContentStyle.InputControlCustom2, KryptonLanguageManager.ContentStyleStrings.InputControlCustom2),
            new(PaletteContentStyle.InputControlCustom3, KryptonLanguageManager.ContentStyleStrings.InputControlCustom3)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContentStyleConverter class.
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
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
