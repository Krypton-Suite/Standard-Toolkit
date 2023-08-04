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
    internal class PaletteBorderStyleConverter : StringLookupConverter<PaletteBorderStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteBorderStyle, string> _pairs = new Dictionary<PaletteBorderStyle, string>
        {
            {PaletteBorderStyle.ButtonStandalone, KryptonLanguageManager.BorderStyleStrings.ButtonStandalone},
            {PaletteBorderStyle.ButtonAlternate, KryptonLanguageManager.BorderStyleStrings.ButtonAlternate},
            {PaletteBorderStyle.ButtonLowProfile, KryptonLanguageManager.BorderStyleStrings.ButtonLowProfile},
            {PaletteBorderStyle.ButtonButtonSpec, KryptonLanguageManager.BorderStyleStrings.ButtonButtonSpec},
            {PaletteBorderStyle.ButtonBreadCrumb, KryptonLanguageManager.BorderStyleStrings.ButtonBreadCrumb},
            {PaletteBorderStyle.ButtonCalendarDay, KryptonLanguageManager.BorderStyleStrings.ButtonCalendarDay},
            {PaletteBorderStyle.ButtonCluster, KryptonLanguageManager.BorderStyleStrings.ButtonCluster},
            {PaletteBorderStyle.ButtonGallery, KryptonLanguageManager.BorderStyleStrings.ButtonGallery},
            {PaletteBorderStyle.ButtonNavigatorStack, KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorStack},
            {PaletteBorderStyle.ButtonNavigatorOverflow, KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorOverflow},
            {PaletteBorderStyle.ButtonNavigatorMini, KryptonLanguageManager.BorderStyleStrings.ButtonNavigatorMini},
            {PaletteBorderStyle.ButtonInputControl, KryptonLanguageManager.BorderStyleStrings.ButtonInputControl},
            {PaletteBorderStyle.ButtonListItem, KryptonLanguageManager.BorderStyleStrings.ButtonListItem},
            {PaletteBorderStyle.ButtonForm, KryptonLanguageManager.BorderStyleStrings.ButtonForm},
            {PaletteBorderStyle.ButtonFormClose, KryptonLanguageManager.BorderStyleStrings.ButtonFormClose},
            {PaletteBorderStyle.ButtonCommand, KryptonLanguageManager.BorderStyleStrings.ButtonCommand},
            {PaletteBorderStyle.ButtonCustom1, KryptonLanguageManager.BorderStyleStrings.ButtonCustom1},
            {PaletteBorderStyle.ButtonCustom2, KryptonLanguageManager.BorderStyleStrings.ButtonCustom2},
            {PaletteBorderStyle.ButtonCustom3, KryptonLanguageManager.BorderStyleStrings.ButtonCustom3},
            {PaletteBorderStyle.ControlClient, KryptonLanguageManager.BorderStyleStrings.ControlClient},
            {PaletteBorderStyle.ControlAlternate, KryptonLanguageManager.BorderStyleStrings.ControlAlternate},
            {PaletteBorderStyle.ControlGroupBox, KryptonLanguageManager.BorderStyleStrings.ControlGroupBox},
            {PaletteBorderStyle.ControlToolTip, KryptonLanguageManager.BorderStyleStrings.ControlToolTip},
            {PaletteBorderStyle.ControlRibbon, KryptonLanguageManager.BorderStyleStrings.ControlRibbon},
            {PaletteBorderStyle.ControlRibbonAppMenu, KryptonLanguageManager.BorderStyleStrings.ControlRibbonAppMenu},
            {PaletteBorderStyle.ControlCustom1, KryptonLanguageManager.BorderStyleStrings.ControlCustom1},
            {PaletteBorderStyle.ControlCustom2, KryptonLanguageManager.BorderStyleStrings.ControlCustom2},
            {PaletteBorderStyle.ControlCustom3, KryptonLanguageManager.BorderStyleStrings.ControlCustom3},
            {PaletteBorderStyle.ContextMenuOuter, KryptonLanguageManager.BorderStyleStrings.ContextMenuOuter},
            {PaletteBorderStyle.ContextMenuInner, KryptonLanguageManager.BorderStyleStrings.ContextMenuInner},
            {PaletteBorderStyle.ContextMenuHeading, KryptonLanguageManager.BorderStyleStrings.ContextMenuHeading},
            {PaletteBorderStyle.ContextMenuSeparator, KryptonLanguageManager.BorderStyleStrings.ContextMenuSeparator},
            {PaletteBorderStyle.ContextMenuItemSplit, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemSplit},
            {PaletteBorderStyle.ContextMenuItemImage, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemImage},
            {PaletteBorderStyle.ContextMenuItemImageColumn, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemImageColumn},
            {PaletteBorderStyle.ContextMenuItemHighlight, KryptonLanguageManager.BorderStyleStrings.ContextMenuItemHighlight},
            {PaletteBorderStyle.InputControlStandalone, KryptonLanguageManager.BorderStyleStrings.InputControlStandalone},
            {PaletteBorderStyle.InputControlRibbon, KryptonLanguageManager.BorderStyleStrings.InputControlRibbon},
            {PaletteBorderStyle.InputControlCustom1, KryptonLanguageManager.BorderStyleStrings.InputControlCustom1},
            {PaletteBorderStyle.InputControlCustom2, KryptonLanguageManager.BorderStyleStrings.InputControlCustom2},
            {PaletteBorderStyle.InputControlCustom3, KryptonLanguageManager.BorderStyleStrings.InputControlCustom3},
            {PaletteBorderStyle.FormMain, KryptonLanguageManager.BorderStyleStrings.FormMain},
            {PaletteBorderStyle.FormCustom1, KryptonLanguageManager.BorderStyleStrings.FormCustom1},
            {PaletteBorderStyle.FormCustom2, KryptonLanguageManager.BorderStyleStrings.FormCustom2},
            {PaletteBorderStyle.FormCustom3, KryptonLanguageManager.BorderStyleStrings.FormCustom3},
            {PaletteBorderStyle.GridHeaderColumnList, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnList},
            {PaletteBorderStyle.GridHeaderRowList, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowList},
            {PaletteBorderStyle.GridDataCellList, KryptonLanguageManager.BorderStyleStrings.GridDataCellList},
            {PaletteBorderStyle.GridHeaderColumnSheet, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnSheet},
            {PaletteBorderStyle.GridHeaderRowSheet, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowSheet},
            {PaletteBorderStyle.GridDataCellSheet, KryptonLanguageManager.BorderStyleStrings.GridDataCellSheet},
            {PaletteBorderStyle.GridHeaderColumnCustom1, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom1},
            {PaletteBorderStyle.GridHeaderColumnCustom2, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom2},
            {PaletteBorderStyle.GridHeaderColumnCustom3, KryptonLanguageManager.BorderStyleStrings.GridHeaderColumnCustom3},
            {PaletteBorderStyle.GridHeaderRowCustom1, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom1},
            {PaletteBorderStyle.GridHeaderRowCustom2, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom2},
            {PaletteBorderStyle.GridHeaderRowCustom3, KryptonLanguageManager.BorderStyleStrings.GridHeaderRowCustom3},
            {PaletteBorderStyle.GridDataCellCustom1, KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom1},
            {PaletteBorderStyle.GridDataCellCustom2, KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom2},
            {PaletteBorderStyle.GridDataCellCustom3, KryptonLanguageManager.BorderStyleStrings.GridDataCellCustom3},
            {PaletteBorderStyle.HeaderPrimary, KryptonLanguageManager.BorderStyleStrings.HeaderPrimary},
            {PaletteBorderStyle.HeaderSecondary, KryptonLanguageManager.BorderStyleStrings.HeaderSecondary},
            {PaletteBorderStyle.HeaderDockActive, KryptonLanguageManager.BorderStyleStrings.HeaderDockActive},
            {PaletteBorderStyle.HeaderDockInactive, KryptonLanguageManager.BorderStyleStrings.HeaderDockInactive},
            {PaletteBorderStyle.HeaderForm, KryptonLanguageManager.BorderStyleStrings.HeaderForm},
            {PaletteBorderStyle.HeaderCalendar, KryptonLanguageManager.BorderStyleStrings.HeaderCalendar},
            {PaletteBorderStyle.HeaderCustom1, KryptonLanguageManager.BorderStyleStrings.HeaderCustom1},
            {PaletteBorderStyle.HeaderCustom2, KryptonLanguageManager.BorderStyleStrings.HeaderCustom2},
            {PaletteBorderStyle.HeaderCustom3, KryptonLanguageManager.BorderStyleStrings.HeaderCustom3},
            {PaletteBorderStyle.SeparatorLowProfile, KryptonLanguageManager.BorderStyleStrings.SeparatorLowProfile},
            {PaletteBorderStyle.SeparatorHighProfile, KryptonLanguageManager.BorderStyleStrings.SeparatorHighProfile},
            {PaletteBorderStyle.SeparatorHighInternalProfile, KryptonLanguageManager.BorderStyleStrings.SeparatorHighInternalProfile},
            {PaletteBorderStyle.TabHighProfile, KryptonLanguageManager.BorderStyleStrings.TabHighProfile},
            {PaletteBorderStyle.TabStandardProfile, KryptonLanguageManager.BorderStyleStrings.TabStandardProfile},
            {PaletteBorderStyle.TabLowProfile, KryptonLanguageManager.BorderStyleStrings.TabLowProfile},
            {PaletteBorderStyle.TabOneNote, KryptonLanguageManager.BorderStyleStrings.TabOneNote},
            {PaletteBorderStyle.TabDock, KryptonLanguageManager.BorderStyleStrings.TabDock},
            {PaletteBorderStyle.TabDockAutoHidden, KryptonLanguageManager.BorderStyleStrings.TabDockAutoHidden},
            {PaletteBorderStyle.TabCustom1, KryptonLanguageManager.BorderStyleStrings.TabCustom1},
            {PaletteBorderStyle.TabCustom2, KryptonLanguageManager.BorderStyleStrings.TabCustom2},
            {PaletteBorderStyle.TabCustom3, KryptonLanguageManager.BorderStyleStrings.TabCustom3 }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteBorderStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
