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
            new Pair(PaletteBorderStyle.ButtonStandalone, KryptonLanguageManager.BorderStyleStrings.ButtonStandalone),
            new Pair(PaletteBorderStyle.ButtonAlternate, KryptonLanguageManager.BorderStyleStrings.ButtonAlternate),
            new Pair(PaletteBorderStyle.ButtonLowProfile, KryptonLanguageManager.BorderStyleStrings.ButtonLowProfile),
            new Pair(PaletteBorderStyle.ButtonButtonSpec, KryptonLanguageManager.BorderStyleStrings.ButtonButtonSpec),
            new Pair(PaletteBorderStyle.ButtonBreadCrumb, KryptonLanguageManager.BorderStyleStrings.ButtonBreadCrumb),
            new Pair(PaletteBorderStyle.ButtonCalendarDay, KryptonLanguageManager.BorderStyleStrings.ButtonCalendarDay),
            new Pair(PaletteBorderStyle.ButtonCluster, KryptonLanguageManager.BorderStyleStrings.ButtonCluster),
            new Pair(PaletteBorderStyle.ButtonGallery, KryptonLanguageManager.BorderStyleStrings.ButtonGallery),
            new Pair(PaletteBorderStyle.ButtonNavigatorStack,
                KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorStack),
            new Pair(PaletteBorderStyle.ButtonNavigatorOverflow,
                KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorOverflow),
            new Pair(PaletteBorderStyle.ButtonNavigatorMini,
                KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorMini),
            new Pair(PaletteBorderStyle.ButtonInputControl,
                KryptonLanguageManager.BorderStyleStrings.ButtonInputControl),
            new Pair(PaletteBorderStyle.ButtonListItem, KryptonLanguageManager.BorderStyleStrings.ButtonListItem),
            new Pair(PaletteBorderStyle.ButtonForm, KryptonLanguageManager.BorderStyleStrings.ButtonForm),
            new Pair(PaletteBorderStyle.ButtonFormClose, KryptonLanguageManager.BorderStyleStrings.ButtonFormClose),
            new Pair(PaletteBorderStyle.ButtonCommand, KryptonLanguageManager.BorderStyleStrings.ButtonCommand),
            new Pair(PaletteBorderStyle.ButtonCustom1, KryptonLanguageManager.BorderStyleStrings.ButtonCustom1),
            new Pair(PaletteBorderStyle.ButtonCustom2, KryptonLanguageManager.BorderStyleStrings.ButtonCustom2),
            new Pair(PaletteBorderStyle.ButtonCustom3, KryptonLanguageManager.BorderStyleStrings.ButtonCustom3),
            new Pair(PaletteBorderStyle.ControlClient, KryptonLanguageManager.BorderStyleStrings.ControlClient),
            new Pair(PaletteBorderStyle.ControlAlternate, KryptonLanguageManager.BorderStyleStrings.ControlAlternate),
            new Pair(PaletteBorderStyle.ControlGroupBox, KryptonLanguageManager.BorderStyleStrings.ControlGroupBox),
            new Pair(PaletteBorderStyle.ControlToolTip, KryptonLanguageManager.BorderStyleStrings.ControlToolTip),
            new Pair(PaletteBorderStyle.ControlRibbon, KryptonLanguageManager.BorderStyleStrings.ControlRibbon),
            new Pair(PaletteBorderStyle.ControlRibbonAppMenu,
                KryptonLanguageManager.BorderStyleStrings.ControlRibbonAppMenu),
            new Pair(PaletteBorderStyle.ControlCustom1, KryptonLanguageManager.BorderStyleStrings.ControlCustom1),
            new Pair(PaletteBorderStyle.ControlCustom2, KryptonLanguageManager.BorderStyleStrings.ControlCustom2),
            new Pair(PaletteBorderStyle.ControlCustom3, KryptonLanguageManager.BorderStyleStrings.ControlCustom3),
            new Pair(PaletteBorderStyle.ContextMenuOuter, KryptonLanguageManager.BorderStyleStrings.ContextMenuOuter),
            new Pair(PaletteBorderStyle.ContextMenuInner, KryptonLanguageManager.BorderStyleStrings.ContextMenuInner),
            new Pair(PaletteBorderStyle.ContextMenuHeading,
                KryptonLanguageManager.BorderStyleStrings.ContextMenuHeading),
            new Pair(PaletteBorderStyle.ContextMenuSeparator,
                KryptonLanguageManager.BorderStyleStrings.ContextMenuSeparator),
            new Pair(PaletteBorderStyle.ContextMenuItemSplit,
                KryptonLanguageManager.BorderStyleStrings.ContextMenuItemSplit),
            new Pair(PaletteBorderStyle.ContextMenuItemImage,
                KryptonLanguageManager.BorderStyleStrings.ContextMenuItemImage),
            new Pair(PaletteBorderStyle.ContextMenuItemImageColumn,
                KryptonLanguageManager.BorderStyleStrings.ContextMenuItemImageColumn),
            new Pair(PaletteBorderStyle.ContextMenuItemHighlight,
                KryptonLanguageManager.BorderStyleStrings.ContextMenuItemHighlight),
            new Pair(PaletteBorderStyle.InputControlStandalone,
                KryptonLanguageManager.BorderStyleStrings.InputControlStandalone),
            new Pair(PaletteBorderStyle.InputControlRibbon,
                KryptonLanguageManager.BorderStyleStrings.InputControlRibbon),
            new Pair(PaletteBorderStyle.InputControlCustom1,
                KryptonLanguageManager.BorderStyleStrings.InputControlCustom1),
            new Pair(PaletteBorderStyle.InputControlCustom2,
                KryptonLanguageManager.BorderStyleStrings.InputControlCustom2),
            new Pair(PaletteBorderStyle.InputControlCustom3,
                KryptonLanguageManager.BorderStyleStrings.InputControlCustom3),
            new Pair(PaletteBorderStyle.FormMain, KryptonLanguageManager.BorderStyleStrings.FormMain),
            new Pair(PaletteBorderStyle.FormCustom1, KryptonLanguageManager.BorderStyleStrings.FormCustom1),
            new Pair(PaletteBorderStyle.FormCustom2, KryptonLanguageManager.BorderStyleStrings.FormCustom2),
            new Pair(PaletteBorderStyle.FormCustom3, KryptonLanguageManager.BorderStyleStrings.FormCustom3),
            new Pair(PaletteBorderStyle.GridHeaderColumnList,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnList),
            new Pair(PaletteBorderStyle.GridHeaderRowList, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowList),
            new Pair(PaletteBorderStyle.GridDataCellList, KryptonLanguageManager.BorderStyleStrings.GridDataCellList),
            new Pair(PaletteBorderStyle.GridHeaderColumnSheet,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnSheet),
            new Pair(PaletteBorderStyle.GridHeaderRowSheet,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderRowSheet),
            new Pair(PaletteBorderStyle.GridDataCellSheet, KryptonLanguageManager.BorderStyleStrings.GridDataCellSheet),
            new Pair(PaletteBorderStyle.GridHeaderColumnCustom1,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom1),
            new Pair(PaletteBorderStyle.GridHeaderColumnCustom2,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom2),
            new Pair(PaletteBorderStyle.GridHeaderColumnCustom3,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom3),
            new Pair(PaletteBorderStyle.GridHeaderRowCustom1,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom1),
            new Pair(PaletteBorderStyle.GridHeaderRowCustom2,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom2),
            new Pair(PaletteBorderStyle.GridHeaderRowCustom3,
                KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom3),
            new Pair(PaletteBorderStyle.GridDataCellCustom1,
                KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom1),
            new Pair(PaletteBorderStyle.GridDataCellCustom2,
                KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom2),
            new Pair(PaletteBorderStyle.GridDataCellCustom3,
                KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom3),
            new Pair(PaletteBorderStyle.HeaderPrimary, KryptonLanguageManager.BorderStyleStrings.HeaderPrimary),
            new Pair(PaletteBorderStyle.HeaderSecondary, KryptonLanguageManager.BorderStyleStrings.HeaderSecondary),
            new Pair(PaletteBorderStyle.HeaderDockActive, KryptonLanguageManager.BorderStyleStrings.HeaderDockActive),
            new Pair(PaletteBorderStyle.HeaderDockInactive,
                KryptonLanguageManager.BorderStyleStrings.HeaderDockInactive),
            new Pair(PaletteBorderStyle.HeaderForm, KryptonLanguageManager.BorderStyleStrings.HeaderForm),
            new Pair(PaletteBorderStyle.HeaderCalendar, KryptonLanguageManager.BorderStyleStrings.HeaderCalendar),
            new Pair(PaletteBorderStyle.HeaderCustom1, KryptonLanguageManager.BorderStyleStrings.HeaderCustom1),
            new Pair(PaletteBorderStyle.HeaderCustom2, KryptonLanguageManager.BorderStyleStrings.HeaderCustom2),
            new Pair(PaletteBorderStyle.HeaderCustom3, KryptonLanguageManager.BorderStyleStrings.HeaderCustom3),
            new Pair(PaletteBorderStyle.SeparatorLowProfile,
                KryptonLanguageManager.BorderStyleStrings.SeparatorLowProfile),
            new Pair(PaletteBorderStyle.SeparatorHighProfile,
                KryptonLanguageManager.BorderStyleStrings.SeparatorHighProfile),
            new Pair(PaletteBorderStyle.SeparatorHighInternalProfile,
                KryptonLanguageManager.BorderStyleStrings.SeparatorHighInternalProfile),
            new Pair(PaletteBorderStyle.TabHighProfile, KryptonLanguageManager.BorderStyleStrings.TabHighProfile),
            new Pair(PaletteBorderStyle.TabStandardProfile,
                KryptonLanguageManager.BorderStyleStrings.TabStandardProfile),
            new Pair(PaletteBorderStyle.TabLowProfile, KryptonLanguageManager.BorderStyleStrings.TabLowProfile),
            new Pair(PaletteBorderStyle.TabOneNote, KryptonLanguageManager.BorderStyleStrings.TabOneNote),
            new Pair(PaletteBorderStyle.TabDock, KryptonLanguageManager.BorderStyleStrings.TabDock),
            new Pair(PaletteBorderStyle.TabDockAutoHidden, KryptonLanguageManager.BorderStyleStrings.TabDockAutoHidden),
            new Pair(PaletteBorderStyle.TabCustom1, KryptonLanguageManager.BorderStyleStrings.TabCustom1),
            new Pair(PaletteBorderStyle.TabCustom2, KryptonLanguageManager.BorderStyleStrings.TabCustom2),
            new Pair(PaletteBorderStyle.TabCustom3, KryptonLanguageManager.BorderStyleStrings.TabCustom3)
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
