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
    /// Custom type converter so that PaletteBorderStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteBorderStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(PaletteBorderStyle.ButtonStandalone, "Button - Standalone"),
        //    new(PaletteBorderStyle.ButtonAlternate, "Button - Alternate"),
        //    new(PaletteBorderStyle.ButtonLowProfile, "Button - Low Profile"),
        //    new(PaletteBorderStyle.ButtonButtonSpec, "Button - ButtonSpec"),
        //    new(PaletteBorderStyle.ButtonBreadCrumb, "Button - BreadCrumb"),
        //    new(PaletteBorderStyle.ButtonCalendarDay, "Button - Calendar Day"),
        //    new(PaletteBorderStyle.ButtonCluster, "Button - Cluster"),
        //    new(PaletteBorderStyle.ButtonGallery, "Button - Gallery"),
        //    new(PaletteBorderStyle.ButtonNavigatorStack, "Button - Navigator Stack"),
        //    new(PaletteBorderStyle.ButtonNavigatorOverflow, "Button - Navigator Overflow"),
        //    new(PaletteBorderStyle.ButtonNavigatorMini, "Button - Navigator Mini"),
        //    new(PaletteBorderStyle.ButtonInputControl, "Button - Input Control"),
        //    new(PaletteBorderStyle.ButtonListItem, "Button - List Item"),
        //    new(PaletteBorderStyle.ButtonForm, "Button - Form"),
        //    new(PaletteBorderStyle.ButtonFormClose, "Button - Form Close"),
        //    new(PaletteBorderStyle.ButtonCommand, "Button - Command"),
        //    new(PaletteBorderStyle.ButtonCustom1, "Button - Custom1"),
        //    new(PaletteBorderStyle.ButtonCustom2, "Button - Custom2"),
        //    new(PaletteBorderStyle.ButtonCustom3, "Button - Custom3"),
        //    new(PaletteBorderStyle.ControlClient, "Control - Client"),
        //    new(PaletteBorderStyle.ControlAlternate, "Control - Alternate"),
        //    new(PaletteBorderStyle.ControlGroupBox, "Control - GroupBox"),
        //    new(PaletteBorderStyle.ControlToolTip, "Control - ToolTip"),
        //    new(PaletteBorderStyle.ControlRibbon, "Control - Ribbon"),
        //    new(PaletteBorderStyle.ControlRibbonAppMenu, "Control - RibbonAppMenu"),
        //    new(PaletteBorderStyle.ControlCustom1, "Control - Custom1"),
        //    new(PaletteBorderStyle.ControlCustom2, "Control - Custom2"),
        //    new(PaletteBorderStyle.ControlCustom3, "Control - Custom3"),
        //    new(PaletteBorderStyle.ContextMenuOuter, "ContextMenu - Outer"),
        //    new(PaletteBorderStyle.ContextMenuInner, "ContextMenu - Inner"),
        //    new(PaletteBorderStyle.ContextMenuHeading, "ContextMenu - Heading"),
        //    new(PaletteBorderStyle.ContextMenuSeparator, "ContextMenu - Separator"),
        //    new(PaletteBorderStyle.ContextMenuItemSplit, "ContextMenu - Item Split"),
        //    new(PaletteBorderStyle.ContextMenuItemImage, "ContextMenu - Item Image"),
        //    new(PaletteBorderStyle.ContextMenuItemImageColumn, "ContextMenu - Item ImageColumn"),
        //    new(PaletteBorderStyle.ContextMenuItemHighlight, "ContextMenu - Item Highlight"),
        //    new(PaletteBorderStyle.InputControlStandalone, "InputControl - Standalone"),
        //    new(PaletteBorderStyle.InputControlRibbon, "InputControl - Ribbon"),
        //    new(PaletteBorderStyle.InputControlCustom1, "InputControl - Custom1"),
        //    new(PaletteBorderStyle.InputControlCustom2, "InputControl - Custom2"),
        //    new(PaletteBorderStyle.InputControlCustom3, "InputControl - Custom3"),
        //    new(PaletteBorderStyle.FormMain, "Form - Main"),
        //    new(PaletteBorderStyle.FormCustom1, "Form - Custom1"),
        //    new(PaletteBorderStyle.FormCustom2, "Form - Custom2"),
        //    new(PaletteBorderStyle.FormCustom3, "Form - Custom3"),
        //    new(PaletteBorderStyle.GridHeaderColumnList, "Grid - HeaderColumn - List"),
        //    new(PaletteBorderStyle.GridHeaderRowList, "Grid - HeaderRow - List"),
        //    new(PaletteBorderStyle.GridDataCellList, "Grid - DataCell - List"),
        //    new(PaletteBorderStyle.GridHeaderColumnSheet, "Grid - HeaderColumn - Sheet"),
        //    new(PaletteBorderStyle.GridHeaderRowSheet, "Grid - HeaderRow - Sheet"),
        //    new(PaletteBorderStyle.GridDataCellSheet, "Grid - DataCell - Sheet"),
        //    new(PaletteBorderStyle.GridHeaderColumnCustom1, "Grid - HeaderColumn - Custom1"),
        //    new(PaletteBorderStyle.GridHeaderColumnCustom2, "Grid - HeaderColumn - Custom2"),
        //    new(PaletteBorderStyle.GridHeaderColumnCustom3, "Grid - HeaderColumn - Custom3"),
        //    new(PaletteBorderStyle.GridHeaderRowCustom1, "Grid - HeaderRow - Custom1"),
        //    new(PaletteBorderStyle.GridHeaderRowCustom2, "Grid - HeaderRow - Custom2"),
        //    new(PaletteBorderStyle.GridHeaderRowCustom3, "Grid - HeaderRow - Custom3"),
        //    new(PaletteBorderStyle.GridDataCellCustom1, "Grid - DataCell - Custom1"),
        //    new(PaletteBorderStyle.GridDataCellCustom2, "Grid - DataCell - Custom2"),
        //    new(PaletteBorderStyle.GridDataCellCustom3, "Grid - DataCell - Custom3"),
        //    new(PaletteBorderStyle.HeaderPrimary, "Header - Primary"),
        //    new(PaletteBorderStyle.HeaderSecondary, "Header - Secondary"),
        //    new(PaletteBorderStyle.HeaderDockActive, "Header - Dock - Active"),
        //    new(PaletteBorderStyle.HeaderDockInactive, "Header - Dock - Inactive"),
        //    new(PaletteBorderStyle.HeaderForm, "Header - Form"),
        //    new(PaletteBorderStyle.HeaderCalendar, "Header - Calendar"),
        //    new(PaletteBorderStyle.HeaderCustom1, "Header - Custom1"),
        //    new(PaletteBorderStyle.HeaderCustom2, "Header - Custom2"),
        //    new(PaletteBorderStyle.HeaderCustom2, "Header - Custom3"),
        //    new(PaletteBorderStyle.SeparatorLowProfile, "Separator - Low Profile"),
        //    new(PaletteBorderStyle.SeparatorHighProfile, "Separator - High Profile"),
        //    new(PaletteBorderStyle.SeparatorHighInternalProfile, "Separator - High Internal Profile"),
        //    new(PaletteBorderStyle.TabHighProfile, "Tab - High Profile"),
        //    new(PaletteBorderStyle.TabStandardProfile, "Tab - Standard Profile"),
        //    new(PaletteBorderStyle.TabLowProfile, "Tab - Low Profile"),
        //    new(PaletteBorderStyle.TabOneNote, "Tab - OneNote"),
        //    new(PaletteBorderStyle.TabDock, "Tab - Dock"),
        //    new(PaletteBorderStyle.TabDockAutoHidden, "Tab - Dock AutoHidden"),
        //    new(PaletteBorderStyle.TabCustom1, "Tab - Custom1"),
        //    new(PaletteBorderStyle.TabCustom2, "Tab - Custom2"),
        //    new(PaletteBorderStyle.TabCustom3, "Tab - Custom3")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new(PaletteBorderStyle.ButtonStandalone, KryptonLanguageManager.BorderStyleStrings.ButtonStandalone),
            new(PaletteBorderStyle.ButtonAlternate, KryptonLanguageManager.BorderStyleStrings.ButtonAlternate),
            new(PaletteBorderStyle.ButtonLowProfile, KryptonLanguageManager.BorderStyleStrings.ButtonLowProfile),
            new(PaletteBorderStyle.ButtonButtonSpec, KryptonLanguageManager.BorderStyleStrings.ButtonButtonSpec),
            new(PaletteBorderStyle.ButtonBreadCrumb, KryptonLanguageManager.BorderStyleStrings.ButtonBreadCrumb),
            new(PaletteBorderStyle.ButtonCalendarDay, KryptonLanguageManager.BorderStyleStrings.ButtonCalendarDay),
            new(PaletteBorderStyle.ButtonCluster, KryptonLanguageManager.BorderStyleStrings.ButtonCluster),
            new(PaletteBorderStyle.ButtonGallery, KryptonLanguageManager.BorderStyleStrings.ButtonGallery),
            new(PaletteBorderStyle.ButtonNavigatorStack, KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorStack),
            new(PaletteBorderStyle.ButtonNavigatorOverflow, KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorOverflow),
            new(PaletteBorderStyle.ButtonNavigatorMini, KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorMini),
            new(PaletteBorderStyle.ButtonInputControl, KryptonLanguageManager.BorderStyleStrings.ButtonInputControl),
            new(PaletteBorderStyle.ButtonListItem, KryptonLanguageManager.BorderStyleStrings.ButtonListItem),
            new(PaletteBorderStyle.ButtonForm, KryptonLanguageManager.BorderStyleStrings.ButtonForm),
            new(PaletteBorderStyle.ButtonFormClose, KryptonLanguageManager.BorderStyleStrings.ButtonFormClose),
            new(PaletteBorderStyle.ButtonCommand, KryptonLanguageManager.BorderStyleStrings.ButtonCommand),
            new(PaletteBorderStyle.ButtonCustom1, KryptonLanguageManager.BorderStyleStrings.ButtonCustom1),
            new(PaletteBorderStyle.ButtonCustom2, KryptonLanguageManager.BorderStyleStrings.ButtonCustom2),
            new(PaletteBorderStyle.ButtonCustom3, KryptonLanguageManager.BorderStyleStrings.ButtonCustom3),
            new(PaletteBorderStyle.ControlClient, KryptonLanguageManager.BorderStyleStrings.ControlClient),
            new(PaletteBorderStyle.ControlAlternate, KryptonLanguageManager.BorderStyleStrings.ControlAlternate),
            new(PaletteBorderStyle.ControlGroupBox, KryptonLanguageManager.BorderStyleStrings.ControlGroupBox),
            new(PaletteBorderStyle.ControlToolTip, KryptonLanguageManager.BorderStyleStrings.ControlToolTip),
            new(PaletteBorderStyle.ControlRibbon, KryptonLanguageManager.BorderStyleStrings.ControlRibbon),
            new(PaletteBorderStyle.ControlRibbonAppMenu, KryptonLanguageManager.BorderStyleStrings.ControlRibbonAppMenu),
            new(PaletteBorderStyle.ControlCustom1, KryptonLanguageManager.BorderStyleStrings.ControlCustom1),
            new(PaletteBorderStyle.ControlCustom2, KryptonLanguageManager.BorderStyleStrings.ControlCustom2),
            new(PaletteBorderStyle.ControlCustom3, KryptonLanguageManager.BorderStyleStrings.ControlCustom3),
            new(PaletteBorderStyle.ContextMenuOuter, KryptonLanguageManager.BorderStyleStrings.ContextMenuOuter),
            new(PaletteBorderStyle.ContextMenuInner, KryptonLanguageManager.BorderStyleStrings.ContextMenuInner),
            new(PaletteBorderStyle.ContextMenuHeading, KryptonLanguageManager.BorderStyleStrings.ContextMenuHeading),
            new(PaletteBorderStyle.ContextMenuSeparator, KryptonLanguageManager.BorderStyleStrings.ContextMenuSeparator),
            new(PaletteBorderStyle.ContextMenuItemSplit, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemSplit),
            new(PaletteBorderStyle.ContextMenuItemImage, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemImage),
            new(PaletteBorderStyle.ContextMenuItemImageColumn, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemImageColumn),
            new(PaletteBorderStyle.ContextMenuItemHighlight, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemHighlight),
            new(PaletteBorderStyle.InputControlStandalone, KryptonLanguageManager.BorderStyleStrings.InputControlStandalone),
            new(PaletteBorderStyle.InputControlRibbon, KryptonLanguageManager.BorderStyleStrings.InputControlRibbon),
            new(PaletteBorderStyle.InputControlCustom1, KryptonLanguageManager.BorderStyleStrings.InputControlCustom1),
            new(PaletteBorderStyle.InputControlCustom2, KryptonLanguageManager.BorderStyleStrings.InputControlCustom2),
            new(PaletteBorderStyle.InputControlCustom3, KryptonLanguageManager.BorderStyleStrings.InputControlCustom3),
            new(PaletteBorderStyle.FormMain, KryptonLanguageManager.BorderStyleStrings.FormMain),
            new(PaletteBorderStyle.FormCustom1, KryptonLanguageManager.BorderStyleStrings.FormCustom1),
            new(PaletteBorderStyle.FormCustom2, KryptonLanguageManager.BorderStyleStrings.FormCustom2),
            new(PaletteBorderStyle.FormCustom3, KryptonLanguageManager.BorderStyleStrings.FormCustom3),
            new(PaletteBorderStyle.GridHeaderColumnList, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnList),
            new(PaletteBorderStyle.GridHeaderRowList, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowList),
            new(PaletteBorderStyle.GridDataCellList, KryptonLanguageManager.BorderStyleStrings.GridDataCellList),
            new(PaletteBorderStyle.GridHeaderColumnSheet, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnSheet),
            new(PaletteBorderStyle.GridHeaderRowSheet, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowSheet),
            new(PaletteBorderStyle.GridDataCellSheet, KryptonLanguageManager.BorderStyleStrings.GridDataCellSheet),
            new(PaletteBorderStyle.GridHeaderColumnCustom1, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom1),
            new(PaletteBorderStyle.GridHeaderColumnCustom2, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom2),
            new(PaletteBorderStyle.GridHeaderColumnCustom3, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom3),
            new(PaletteBorderStyle.GridHeaderRowCustom1, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom1),
            new(PaletteBorderStyle.GridHeaderRowCustom2, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom2),
            new(PaletteBorderStyle.GridHeaderRowCustom3, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom3),
            new(PaletteBorderStyle.GridDataCellCustom1, KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom1),
            new(PaletteBorderStyle.GridDataCellCustom2, KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom2),
            new(PaletteBorderStyle.GridDataCellCustom3, KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom3),
            new(PaletteBorderStyle.HeaderPrimary, KryptonLanguageManager.BorderStyleStrings.HeaderPrimary),
            new(PaletteBorderStyle.HeaderSecondary, KryptonLanguageManager.BorderStyleStrings.HeaderSecondary),
            new(PaletteBorderStyle.HeaderDockActive, KryptonLanguageManager.BorderStyleStrings.HeaderDockActive),
            new(PaletteBorderStyle.HeaderDockInactive, KryptonLanguageManager.BorderStyleStrings.HeaderDockInactive),
            new(PaletteBorderStyle.HeaderForm, KryptonLanguageManager.BorderStyleStrings.HeaderForm),
            new(PaletteBorderStyle.HeaderCalendar, KryptonLanguageManager.BorderStyleStrings.HeaderCalendar),
            new(PaletteBorderStyle.HeaderCustom1, KryptonLanguageManager.BorderStyleStrings.HeaderCustom1),
            new(PaletteBorderStyle.HeaderCustom2, KryptonLanguageManager.BorderStyleStrings.HeaderCustom2),
            new(PaletteBorderStyle.HeaderCustom3, KryptonLanguageManager.BorderStyleStrings.HeaderCustom3),
            new(PaletteBorderStyle.SeparatorLowProfile, KryptonLanguageManager.BorderStyleStrings.SeparatorLowProfile),
            new(PaletteBorderStyle.SeparatorHighProfile, KryptonLanguageManager.BorderStyleStrings.SeparatorHighProfile),
            new(PaletteBorderStyle.SeparatorHighInternalProfile, KryptonLanguageManager.BorderStyleStrings.SeparatorHighInternalProfile),
            new(PaletteBorderStyle.TabHighProfile, KryptonLanguageManager.BorderStyleStrings.TabHighProfile),
            new(PaletteBorderStyle.TabStandardProfile, KryptonLanguageManager.BorderStyleStrings.TabStandardProfile),
            new(PaletteBorderStyle.TabLowProfile, KryptonLanguageManager.BorderStyleStrings.TabLowProfile),
            new(PaletteBorderStyle.TabOneNote, KryptonLanguageManager.BorderStyleStrings.TabOneNote),
            new(PaletteBorderStyle.TabDock, KryptonLanguageManager.BorderStyleStrings.TabDock),
            new(PaletteBorderStyle.TabDockAutoHidden, KryptonLanguageManager.BorderStyleStrings.TabDockAutoHidden),
            new(PaletteBorderStyle.TabCustom1, KryptonLanguageManager.BorderStyleStrings.TabCustom1),
            new(PaletteBorderStyle.TabCustom2, KryptonLanguageManager.BorderStyleStrings.TabCustom2),
            new(PaletteBorderStyle.TabCustom3, KryptonLanguageManager.BorderStyleStrings.TabCustom3)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBorderStyleConverter class.
        /// </summary>
        public PaletteBorderStyleConverter()
            : base(typeof(PaletteBorderStyle))
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
