﻿#region BSD License
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
    /// Custom type converter so that PaletteBackStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteBackStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(PaletteBackStyle.ButtonStandalone, "Button - Standalone"),
        //    new(PaletteBackStyle.ButtonAlternate, "Button - Alternate"),
        //    new(PaletteBackStyle.ButtonLowProfile, "Button - Low Profile"),
        //    new(PaletteBackStyle.ButtonButtonSpec, "Button - ButtonSpec"),
        //    new(PaletteBackStyle.ButtonBreadCrumb, "Button - BreadCrumb"),
        //    new(PaletteBackStyle.ButtonCalendarDay, "Button - Calendar Day"),
        //    new(PaletteBackStyle.ButtonCluster, "Button - Cluster"),
        //    new(PaletteBackStyle.ButtonGallery, "Button - Gallery"),
        //    new(PaletteBackStyle.ButtonNavigatorStack, "Button - Navigator Stack"),
        //    new(PaletteBackStyle.ButtonNavigatorOverflow, "Button - Navigator Overflow"),
        //    new(PaletteBackStyle.ButtonNavigatorMini, "Button - Navigator Mini"),
        //    new(PaletteBackStyle.ButtonInputControl, "Button - Input Control"),
        //    new(PaletteBackStyle.ButtonListItem, "Button - List Item"),
        //    new(PaletteBackStyle.ButtonForm, "Button - Form"),
        //    new(PaletteBackStyle.ButtonFormClose, "Button - Form Close"),
        //    new(PaletteBackStyle.ButtonCommand, "Button - Command"),
        //    new(PaletteBackStyle.ButtonCustom1, "Button - Custom1"),
        //    new(PaletteBackStyle.ButtonCustom2, "Button - Custom2"),
        //    new(PaletteBackStyle.ButtonCustom3, "Button - Custom3"),
        //    new(PaletteBackStyle.ControlClient, "Control - Client"),
        //    new(PaletteBackStyle.ControlAlternate, "Control - Alternate"),
        //    new(PaletteBackStyle.ControlGroupBox, "Control - GroupBox"),
        //    new(PaletteBackStyle.ControlToolTip, "Control - ToolTip"),
        //    new(PaletteBackStyle.ControlRibbon, "Control - Ribbon"),
        //    new(PaletteBackStyle.ControlRibbonAppMenu, "Control - RibbonAppMenu"),
        //    new(PaletteBackStyle.ControlCustom1, "Control - Custom1"),
        //    new(PaletteBackStyle.ControlCustom2, "Control - Custom2"),
        //    new(PaletteBackStyle.ControlCustom3, "Control - Custom3"),
        //    new(PaletteBackStyle.ContextMenuOuter, "ContextMenu - Outer"),
        //    new(PaletteBackStyle.ContextMenuInner, "ContextMenu - Inner"),
        //    new(PaletteBackStyle.ContextMenuHeading, "ContextMenu - Heading"),
        //    new(PaletteBackStyle.ContextMenuSeparator, "ContextMenu - Separator"),
        //    new(PaletteBackStyle.ContextMenuItemSplit, "ContextMenu - Item Split"),
        //    new(PaletteBackStyle.ContextMenuItemImage, "ContextMenu - Item Image"),
        //    new(PaletteBackStyle.ContextMenuItemImageColumn, "ContextMenu - Item ImageColumn"),
        //    new(PaletteBackStyle.ContextMenuItemHighlight, "ContextMenu - Item Highlight"),
        //    new(PaletteBackStyle.InputControlStandalone, "InputControl - Standalone"),
        //    new(PaletteBackStyle.InputControlRibbon, "InputControl - Ribbon"),
        //    new(PaletteBackStyle.InputControlCustom1, "InputControl - Custom1"),
        //    new(PaletteBackStyle.InputControlCustom2, "InputControl - Custom2"),
        //    new(PaletteBackStyle.InputControlCustom3, "InputControl - Custom3"),
        //    new(PaletteBackStyle.FormMain, "Form - Main"),
        //    new(PaletteBackStyle.FormCustom1, "Form - Custom1"),
        //    new(PaletteBackStyle.FormCustom2, "Form - Custom2"),
        //    new(PaletteBackStyle.FormCustom3, "Form - Custom3"),
        //    new(PaletteBackStyle.GridHeaderColumnList, "Grid - HeaderColumn - List"),
        //    new(PaletteBackStyle.GridHeaderRowList, "Grid - HeaderRow - List"),
        //    new(PaletteBackStyle.GridDataCellList, "Grid - DataCell - List"),
        //    new(PaletteBackStyle.GridBackgroundList, "Grid - Background - List"),
        //    new(PaletteBackStyle.GridHeaderColumnSheet, "Grid - HeaderColumn - Sheet"),
        //    new(PaletteBackStyle.GridHeaderRowSheet, "Grid - HeaderRow - Sheet"),
        //    new(PaletteBackStyle.GridDataCellSheet, "Grid - DataCell - Sheet"),
        //    new(PaletteBackStyle.GridBackgroundSheet, "Grid - Background - Sheet"),
        //    new(PaletteBackStyle.GridHeaderColumnCustom1, "Grid - HeaderColumn - Custom1"),
        //    new(PaletteBackStyle.GridHeaderColumnCustom2, "Grid - HeaderColumn - Custom2"),
        //    new(PaletteBackStyle.GridHeaderColumnCustom3, "Grid - HeaderColumn - Custom3"),
        //    new(PaletteBackStyle.GridHeaderRowCustom1, "Grid - HeaderRow - Custom1"),
        //    new(PaletteBackStyle.GridHeaderRowCustom2, "Grid - HeaderRow - Custom2"),
        //    new(PaletteBackStyle.GridHeaderRowCustom3, "Grid - HeaderRow - Custom3"),
        //    new(PaletteBackStyle.GridDataCellCustom1, "Grid - DataCell - Custom1"),
        //    new(PaletteBackStyle.GridDataCellCustom2, "Grid - DataCell - Custom2"),
        //    new(PaletteBackStyle.GridDataCellCustom3, "Grid - DataCell - Custom3"),
        //    new(PaletteBackStyle.GridBackgroundCustom1, "Grid - Background - Custom1"),
        //    new(PaletteBackStyle.GridBackgroundCustom2, "Grid - Background - Custom2"),
        //    new(PaletteBackStyle.GridBackgroundCustom3, "Grid - Background - Custom3"),
        //    new(PaletteBackStyle.HeaderPrimary, "Header - Primary"),
        //    new(PaletteBackStyle.HeaderSecondary, "Header - Secondary"),
        //    new(PaletteBackStyle.HeaderDockActive, "Header - Dock - Active"),
        //    new(PaletteBackStyle.HeaderDockInactive, "Header - Dock - Inactive"),
        //    new(PaletteBackStyle.HeaderForm, "Header - Form"),
        //    new(PaletteBackStyle.HeaderCalendar, "Header - Calendar"),
        //    new(PaletteBackStyle.HeaderCustom1, "Header - Custom1"),
        //    new(PaletteBackStyle.HeaderCustom2, "Header - Custom2"),
        //    new(PaletteBackStyle.HeaderCustom3, "Header - Custom3"),
        //    new(PaletteBackStyle.PanelClient, "Panel - Client"),
        //    new(PaletteBackStyle.PanelAlternate, "Panel - Alternate"),
        //    new(PaletteBackStyle.PanelRibbonInactive, "Panel - Ribbon Inactive"),
        //    new(PaletteBackStyle.PanelCustom1, "Panel - Custom1"),
        //    new(PaletteBackStyle.PanelCustom2, "Panel - Custom2"),
        //    new(PaletteBackStyle.PanelCustom3, "Panel - Custom3"),
        //    new(PaletteBackStyle.SeparatorLowProfile, "Separator - Low Profile"),
        //    new(PaletteBackStyle.SeparatorHighProfile, "Separator - High Profile"),
        //    new(PaletteBackStyle.SeparatorHighInternalProfile, "Separator - High Internal Profile"),
        //    new(PaletteBackStyle.TabHighProfile, "Tab - High Profile"),
        //    new(PaletteBackStyle.TabStandardProfile, "Tab - Standard Profile"),
        //    new(PaletteBackStyle.TabLowProfile, "Tab - Low Profile"),
        //    new(PaletteBackStyle.TabOneNote, "Tab - OneNote"),
        //    new(PaletteBackStyle.TabDock, "Tab - Dock"),
        //    new(PaletteBackStyle.TabDockAutoHidden, "Tab - Dock AutoHidden"),
        //    new(PaletteBackStyle.TabCustom1, "Tab - Custom1"),
        //    new(PaletteBackStyle.TabCustom2, "Tab - Custom2"),
        //    new(PaletteBackStyle.TabCustom3, "Tab - Custom3")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new(PaletteBackStyle.ButtonStandalone, KryptonLanguageManager.BackStyleStrings.ButtonStandalone),
            new(PaletteBackStyle.ButtonAlternate, KryptonLanguageManager.BackStyleStrings.ButtonAlternate),
            new(PaletteBackStyle.ButtonLowProfile, KryptonLanguageManager.BackStyleStrings.ButtonLowProfile),
            new(PaletteBackStyle.ButtonButtonSpec, KryptonLanguageManager.BackStyleStrings.ButtonButtonSpec),
            new(PaletteBackStyle.ButtonBreadCrumb, KryptonLanguageManager.BackStyleStrings.ButtonBreadCrumb),
            new(PaletteBackStyle.ButtonCalendarDay, KryptonLanguageManager.BackStyleStrings.ButtonCalendarDay),
            new(PaletteBackStyle.ButtonCluster, KryptonLanguageManager.BackStyleStrings.ButtonCluster),
            new(PaletteBackStyle.ButtonGallery, KryptonLanguageManager.BackStyleStrings.ButtonGallery),
            new(PaletteBackStyle.ButtonNavigatorStack, KryptonLanguageManager.BackStyleStrings.ButtonNavigatorStack),
            new(PaletteBackStyle.ButtonNavigatorOverflow, KryptonLanguageManager.BackStyleStrings.ButtonNavigatorOverflow),
            new(PaletteBackStyle.ButtonNavigatorMini, KryptonLanguageManager.BackStyleStrings.ButtonNavigatorMini),
            new(PaletteBackStyle.ButtonInputControl, KryptonLanguageManager.BackStyleStrings.ButtonInputControl),
            new(PaletteBackStyle.ButtonListItem, KryptonLanguageManager.BackStyleStrings.ButtonListItem),
            new(PaletteBackStyle.ButtonForm, KryptonLanguageManager.BackStyleStrings.ButtonForm),
            new(PaletteBackStyle.ButtonFormClose, KryptonLanguageManager.BackStyleStrings.ButtonFormClose),
            new(PaletteBackStyle.ButtonCommand, KryptonLanguageManager.BackStyleStrings.ButtonCommand),
            new(PaletteBackStyle.ButtonCustom1, KryptonLanguageManager.BackStyleStrings.ButtonCustom1),
            new(PaletteBackStyle.ButtonCustom2, KryptonLanguageManager.BackStyleStrings.ButtonCustom2),
            new(PaletteBackStyle.ButtonCustom3, KryptonLanguageManager.BackStyleStrings.ButtonCustom3),
            new(PaletteBackStyle.ControlClient, KryptonLanguageManager.BackStyleStrings.ControlClient),
            new(PaletteBackStyle.ControlAlternate, KryptonLanguageManager.BackStyleStrings.ControlAlternate),
            new(PaletteBackStyle.ControlGroupBox, KryptonLanguageManager.BackStyleStrings.ControlGroupBox),
            new(PaletteBackStyle.ControlToolTip, KryptonLanguageManager.BackStyleStrings.ControlToolTip),
            new(PaletteBackStyle.ControlRibbon, KryptonLanguageManager.BackStyleStrings.ControlRibbon),
            new(PaletteBackStyle.ControlRibbonAppMenu, KryptonLanguageManager.BackStyleStrings.ControlRibbonAppMenu),
            new(PaletteBackStyle.ControlCustom1, KryptonLanguageManager.BackStyleStrings.ControlCustom1),
            new(PaletteBackStyle.ControlCustom2, KryptonLanguageManager.BackStyleStrings.ControlCustom2),
            new(PaletteBackStyle.ControlCustom3, KryptonLanguageManager.BackStyleStrings.ControlCustom3),
            new(PaletteBackStyle.ContextMenuOuter, KryptonLanguageManager.BackStyleStrings.ContextMenuOuter),
            new(PaletteBackStyle.ContextMenuInner, KryptonLanguageManager.BackStyleStrings.ContextMenuInner),
            new(PaletteBackStyle.ContextMenuHeading, KryptonLanguageManager.BackStyleStrings.ContextMenuHeading),
            new(PaletteBackStyle.ContextMenuSeparator, KryptonLanguageManager.BackStyleStrings.ContextMenuSeparator),
            new(PaletteBackStyle.ContextMenuItemSplit, KryptonLanguageManager.BackStyleStrings.ContextMenuItemSplit),
            new(PaletteBackStyle.ContextMenuItemImage, KryptonLanguageManager.BackStyleStrings.ContextMenuItemImage),
            new(PaletteBackStyle.ContextMenuItemImageColumn, KryptonLanguageManager.BackStyleStrings.ContextMenuItemImageColumn),
            new(PaletteBackStyle.ContextMenuItemHighlight, KryptonLanguageManager.BackStyleStrings.ContextMenuItemHighlight),
            new(PaletteBackStyle.InputControlStandalone, KryptonLanguageManager.BackStyleStrings.InputControlStandalone),
            new(PaletteBackStyle.InputControlRibbon, KryptonLanguageManager.BackStyleStrings.InputControlRibbon),
            new(PaletteBackStyle.InputControlCustom1, KryptonLanguageManager.BackStyleStrings.InputControlCustom1),
            new(PaletteBackStyle.InputControlCustom2, KryptonLanguageManager.BackStyleStrings.InputControlCustom2),
            new(PaletteBackStyle.InputControlCustom3, KryptonLanguageManager.BackStyleStrings.InputControlCustom3),
            new(PaletteBackStyle.FormMain, KryptonLanguageManager.BackStyleStrings.FormMain),
            new(PaletteBackStyle.FormCustom1, KryptonLanguageManager.BackStyleStrings.FormCustom1),
            new(PaletteBackStyle.FormCustom2, KryptonLanguageManager.BackStyleStrings.FormCustom2),
            new(PaletteBackStyle.FormCustom3, KryptonLanguageManager.BackStyleStrings.FormCustom3),
            new(PaletteBackStyle.GridHeaderColumnList, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnList),
            new(PaletteBackStyle.GridHeaderRowList, KryptonLanguageManager.BackStyleStrings.GridHeaderRowList),
            new(PaletteBackStyle.GridDataCellList, KryptonLanguageManager.BackStyleStrings.GridDataCellList),
            new(PaletteBackStyle.GridBackgroundList, KryptonLanguageManager.BackStyleStrings.GridBackgroundList),
            new(PaletteBackStyle.GridHeaderColumnSheet, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnSheet),
            new(PaletteBackStyle.GridHeaderRowSheet, KryptonLanguageManager.BackStyleStrings.GridHeaderRowSheet),
            new(PaletteBackStyle.GridDataCellSheet, KryptonLanguageManager.BackStyleStrings.GridDataCellSheet),
            new(PaletteBackStyle.GridBackgroundSheet, KryptonLanguageManager.BackStyleStrings.GridBackgroundSheet),
            new(PaletteBackStyle.GridHeaderColumnCustom1, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnCustom1),
            new(PaletteBackStyle.GridHeaderColumnCustom2, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnCustom2),
            new(PaletteBackStyle.GridHeaderColumnCustom3, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnCustom3),
            new(PaletteBackStyle.GridHeaderRowCustom1, KryptonLanguageManager.BackStyleStrings.GridHeaderRowCustom1),
            new(PaletteBackStyle.GridHeaderRowCustom2, KryptonLanguageManager.BackStyleStrings.GridHeaderRowCustom2),
            new(PaletteBackStyle.GridHeaderRowCustom3, KryptonLanguageManager.BackStyleStrings.GridHeaderRowCustom3),
            new(PaletteBackStyle.GridDataCellCustom1, KryptonLanguageManager.BackStyleStrings.GridDataCellCustom1),
            new(PaletteBackStyle.GridDataCellCustom2, KryptonLanguageManager.BackStyleStrings.GridDataCellCustom2),
            new(PaletteBackStyle.GridDataCellCustom3, KryptonLanguageManager.BackStyleStrings.GridDataCellCustom3),
            new(PaletteBackStyle.GridBackgroundCustom1, KryptonLanguageManager.BackStyleStrings.GridBackgroundCustom1),
            new(PaletteBackStyle.GridBackgroundCustom2, KryptonLanguageManager.BackStyleStrings.GridBackgroundCustom2),
            new(PaletteBackStyle.GridBackgroundCustom3, KryptonLanguageManager.BackStyleStrings.GridBackgroundCustom3),
            new(PaletteBackStyle.HeaderPrimary, KryptonLanguageManager.BackStyleStrings.HeaderPrimary),
            new(PaletteBackStyle.HeaderSecondary, KryptonLanguageManager.BackStyleStrings.HeaderSecondary),
            new(PaletteBackStyle.HeaderDockActive, KryptonLanguageManager.BackStyleStrings.HeaderDockActive),
            new(PaletteBackStyle.HeaderDockInactive, KryptonLanguageManager.BackStyleStrings.HeaderDockInactive),
            new(PaletteBackStyle.HeaderForm, KryptonLanguageManager.BackStyleStrings.HeaderForm),
            new(PaletteBackStyle.HeaderCalendar, KryptonLanguageManager.BackStyleStrings.HeaderCalendar),
            new(PaletteBackStyle.HeaderCustom1, KryptonLanguageManager.BackStyleStrings.HeaderCustom1),
            new(PaletteBackStyle.HeaderCustom2, KryptonLanguageManager.BackStyleStrings.HeaderCustom2),
            new(PaletteBackStyle.HeaderCustom3, KryptonLanguageManager.BackStyleStrings.HeaderCustom3),
            new(PaletteBackStyle.PanelClient, KryptonLanguageManager.BackStyleStrings.PanelClient),
            new(PaletteBackStyle.PanelAlternate, KryptonLanguageManager.BackStyleStrings.PanelAlternate),
            new(PaletteBackStyle.PanelRibbonInactive, KryptonLanguageManager.BackStyleStrings.PanelRibbonInactive),
            new(PaletteBackStyle.PanelCustom1, KryptonLanguageManager.BackStyleStrings.PanelCustom1),
            new(PaletteBackStyle.PanelCustom2, KryptonLanguageManager.BackStyleStrings.PanelCustom2),
            new(PaletteBackStyle.PanelCustom3, KryptonLanguageManager.BackStyleStrings.PanelCustom3),
            new(PaletteBackStyle.SeparatorLowProfile, KryptonLanguageManager.BackStyleStrings.SeparatorLowProfile),
            new(PaletteBackStyle.SeparatorHighProfile, KryptonLanguageManager.BackStyleStrings.SeparatorHighProfile),
            new(PaletteBackStyle.SeparatorHighInternalProfile, KryptonLanguageManager.BackStyleStrings.SeparatorHighInternalProfile),
            new(PaletteBackStyle.TabHighProfile, KryptonLanguageManager.BackStyleStrings.TabHighProfile),
            new(PaletteBackStyle.TabStandardProfile, KryptonLanguageManager.BackStyleStrings.TabStandardProfile),
            new(PaletteBackStyle.TabLowProfile, KryptonLanguageManager.BackStyleStrings.TabLowProfile),
            new(PaletteBackStyle.TabOneNote, KryptonLanguageManager.BackStyleStrings.TabOneNote),
            new(PaletteBackStyle.TabDock, KryptonLanguageManager.BackStyleStrings.TabDock),
            new(PaletteBackStyle.TabDockAutoHidden, KryptonLanguageManager.BackStyleStrings.TabDockAutoHidden),
            new(PaletteBackStyle.TabCustom1, KryptonLanguageManager.BackStyleStrings.TabCustom1),
            new(PaletteBackStyle.TabCustom2, KryptonLanguageManager.BackStyleStrings.TabCustom2),
            new(PaletteBackStyle.TabCustom3, KryptonLanguageManager.BackStyleStrings.TabCustom3)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBackStyleConverter class.
        /// </summary>
        public PaletteBackStyleConverter()
            : base(typeof(PaletteBackStyle))
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
