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
    /// Custom type converter so that PaletteBackStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteBackStyleConverter : StringLookupConverter<PaletteBackStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteBackStyle, string> _pairs = new Dictionary<PaletteBackStyle, string>
        {
            {PaletteBackStyle.ButtonStandalone, KryptonLanguageManager.BackStyleStrings.ButtonStandalone},
            {PaletteBackStyle.ButtonAlternate, KryptonLanguageManager.BackStyleStrings.ButtonAlternate},
            {PaletteBackStyle.ButtonLowProfile, KryptonLanguageManager.BackStyleStrings.ButtonLowProfile},
            {PaletteBackStyle.ButtonButtonSpec, KryptonLanguageManager.BackStyleStrings.ButtonButtonSpec},
            {PaletteBackStyle.ButtonBreadCrumb, KryptonLanguageManager.BackStyleStrings.ButtonBreadCrumb},
            {PaletteBackStyle.ButtonCalendarDay, KryptonLanguageManager.BackStyleStrings.ButtonCalendarDay},
            {PaletteBackStyle.ButtonCluster, KryptonLanguageManager.BackStyleStrings.ButtonCluster},
            {PaletteBackStyle.ButtonGallery, KryptonLanguageManager.BackStyleStrings.ButtonGallery},
            {PaletteBackStyle.ButtonNavigatorStack, KryptonLanguageManager.BackStyleStrings.ButtonNavigatorStack},
            {PaletteBackStyle.ButtonNavigatorOverflow, KryptonLanguageManager.BackStyleStrings.ButtonNavigatorOverflow},
            {PaletteBackStyle.ButtonNavigatorMini, KryptonLanguageManager.BackStyleStrings.ButtonNavigatorMini},
            {PaletteBackStyle.ButtonInputControl, KryptonLanguageManager.BackStyleStrings.ButtonInputControl},
            {PaletteBackStyle.ButtonListItem, KryptonLanguageManager.BackStyleStrings.ButtonListItem},
            {PaletteBackStyle.ButtonForm, KryptonLanguageManager.BackStyleStrings.ButtonForm},
            {PaletteBackStyle.ButtonFormClose, KryptonLanguageManager.BackStyleStrings.ButtonFormClose},
            {PaletteBackStyle.ButtonCommand, KryptonLanguageManager.BackStyleStrings.ButtonCommand},
            {PaletteBackStyle.ButtonCustom1, KryptonLanguageManager.BackStyleStrings.ButtonCustom1},
            {PaletteBackStyle.ButtonCustom2, KryptonLanguageManager.BackStyleStrings.ButtonCustom2},
            {PaletteBackStyle.ButtonCustom3, KryptonLanguageManager.BackStyleStrings.ButtonCustom3},
            {PaletteBackStyle.ControlClient, KryptonLanguageManager.BackStyleStrings.ControlClient},
            {PaletteBackStyle.ControlAlternate, KryptonLanguageManager.BackStyleStrings.ControlAlternate},
            {PaletteBackStyle.ControlGroupBox, KryptonLanguageManager.BackStyleStrings.ControlGroupBox},
            {PaletteBackStyle.ControlToolTip, KryptonLanguageManager.BackStyleStrings.ControlToolTip},
            {PaletteBackStyle.ControlRibbon, KryptonLanguageManager.BackStyleStrings.ControlRibbon},
            {PaletteBackStyle.ControlRibbonAppMenu, KryptonLanguageManager.BackStyleStrings.ControlRibbonAppMenu},
            {PaletteBackStyle.ControlCustom1, KryptonLanguageManager.BackStyleStrings.ControlCustom1},
            {PaletteBackStyle.ControlCustom2, KryptonLanguageManager.BackStyleStrings.ControlCustom2},
            {PaletteBackStyle.ControlCustom3, KryptonLanguageManager.BackStyleStrings.ControlCustom3},
            {PaletteBackStyle.ContextMenuOuter, KryptonLanguageManager.BackStyleStrings.ContextMenuOuter},
            {PaletteBackStyle.ContextMenuInner, KryptonLanguageManager.BackStyleStrings.ContextMenuInner},
            {PaletteBackStyle.ContextMenuHeading, KryptonLanguageManager.BackStyleStrings.ContextMenuHeading},
            {PaletteBackStyle.ContextMenuSeparator, KryptonLanguageManager.BackStyleStrings.ContextMenuSeparator},
            {PaletteBackStyle.ContextMenuItemSplit, KryptonLanguageManager.BackStyleStrings.ContextMenuItemSplit},
            {PaletteBackStyle.ContextMenuItemImage, KryptonLanguageManager.BackStyleStrings.ContextMenuItemImage},
            {PaletteBackStyle.ContextMenuItemImageColumn, KryptonLanguageManager.BackStyleStrings.ContextMenuItemImageColumn},
            {PaletteBackStyle.ContextMenuItemHighlight, KryptonLanguageManager.BackStyleStrings.ContextMenuItemHighlight},
            {PaletteBackStyle.InputControlStandalone, KryptonLanguageManager.BackStyleStrings.InputControlStandalone},
            {PaletteBackStyle.InputControlRibbon, KryptonLanguageManager.BackStyleStrings.InputControlRibbon},
            {PaletteBackStyle.InputControlCustom1, KryptonLanguageManager.BackStyleStrings.InputControlCustom1},
            {PaletteBackStyle.InputControlCustom2, KryptonLanguageManager.BackStyleStrings.InputControlCustom2},
            {PaletteBackStyle.InputControlCustom3, KryptonLanguageManager.BackStyleStrings.InputControlCustom3},
            {PaletteBackStyle.FormMain, KryptonLanguageManager.BackStyleStrings.FormMain},
            {PaletteBackStyle.FormCustom1, KryptonLanguageManager.BackStyleStrings.FormCustom1},
            {PaletteBackStyle.FormCustom2, KryptonLanguageManager.BackStyleStrings.FormCustom2},
            {PaletteBackStyle.FormCustom3, KryptonLanguageManager.BackStyleStrings.FormCustom3},
            {PaletteBackStyle.GridHeaderColumnList, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnList},
            {PaletteBackStyle.GridHeaderRowList, KryptonLanguageManager.BackStyleStrings.GridHeaderRowList},
            {PaletteBackStyle.GridDataCellList, KryptonLanguageManager.BackStyleStrings.GridDataCellList},
            {PaletteBackStyle.GridBackgroundList, KryptonLanguageManager.BackStyleStrings.GridBackgroundList},
            {PaletteBackStyle.GridHeaderColumnSheet, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnSheet},
            {PaletteBackStyle.GridHeaderRowSheet, KryptonLanguageManager.BackStyleStrings.GridHeaderRowSheet},
            {PaletteBackStyle.GridDataCellSheet, KryptonLanguageManager.BackStyleStrings.GridDataCellSheet},
            {PaletteBackStyle.GridBackgroundSheet, KryptonLanguageManager.BackStyleStrings.GridBackgroundSheet},
            {PaletteBackStyle.GridHeaderColumnCustom1, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnCustom1},
            {PaletteBackStyle.GridHeaderColumnCustom2, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnCustom2},
            {PaletteBackStyle.GridHeaderColumnCustom3, KryptonLanguageManager.BackStyleStrings.GridHeaderColumnCustom3},
            {PaletteBackStyle.GridHeaderRowCustom1, KryptonLanguageManager.BackStyleStrings.GridHeaderRowCustom1},
            {PaletteBackStyle.GridHeaderRowCustom2, KryptonLanguageManager.BackStyleStrings.GridHeaderRowCustom2},
            {PaletteBackStyle.GridHeaderRowCustom3, KryptonLanguageManager.BackStyleStrings.GridHeaderRowCustom3},
            {PaletteBackStyle.GridDataCellCustom1, KryptonLanguageManager.BackStyleStrings.GridDataCellCustom1},
            {PaletteBackStyle.GridDataCellCustom2, KryptonLanguageManager.BackStyleStrings.GridDataCellCustom2},
            {PaletteBackStyle.GridDataCellCustom3, KryptonLanguageManager.BackStyleStrings.GridDataCellCustom3},
            {PaletteBackStyle.GridBackgroundCustom1, KryptonLanguageManager.BackStyleStrings.GridBackgroundCustom1},
            {PaletteBackStyle.GridBackgroundCustom2, KryptonLanguageManager.BackStyleStrings.GridBackgroundCustom2},
            {PaletteBackStyle.GridBackgroundCustom3, KryptonLanguageManager.BackStyleStrings.GridBackgroundCustom3},
            {PaletteBackStyle.HeaderPrimary, KryptonLanguageManager.BackStyleStrings.HeaderPrimary},
            {PaletteBackStyle.HeaderSecondary, KryptonLanguageManager.BackStyleStrings.HeaderSecondary},
            {PaletteBackStyle.HeaderDockActive, KryptonLanguageManager.BackStyleStrings.HeaderDockActive},
            {PaletteBackStyle.HeaderDockInactive, KryptonLanguageManager.BackStyleStrings.HeaderDockInactive},
            {PaletteBackStyle.HeaderForm, KryptonLanguageManager.BackStyleStrings.HeaderForm},
            {PaletteBackStyle.HeaderCalendar, KryptonLanguageManager.BackStyleStrings.HeaderCalendar},
            {PaletteBackStyle.HeaderCustom1, KryptonLanguageManager.BackStyleStrings.HeaderCustom1},
            {PaletteBackStyle.HeaderCustom2, KryptonLanguageManager.BackStyleStrings.HeaderCustom2},
            {PaletteBackStyle.HeaderCustom3, KryptonLanguageManager.BackStyleStrings.HeaderCustom3},
            {PaletteBackStyle.PanelClient, KryptonLanguageManager.BackStyleStrings.PanelClient},
            {PaletteBackStyle.PanelAlternate, KryptonLanguageManager.BackStyleStrings.PanelAlternate},
            {PaletteBackStyle.PanelRibbonInactive, KryptonLanguageManager.BackStyleStrings.PanelRibbonInactive},
            {PaletteBackStyle.PanelCustom1, KryptonLanguageManager.BackStyleStrings.PanelCustom1},
            {PaletteBackStyle.PanelCustom2, KryptonLanguageManager.BackStyleStrings.PanelCustom2},
            {PaletteBackStyle.PanelCustom3, KryptonLanguageManager.BackStyleStrings.PanelCustom3},
            {PaletteBackStyle.SeparatorLowProfile, KryptonLanguageManager.BackStyleStrings.SeparatorLowProfile},
            {PaletteBackStyle.SeparatorHighProfile, KryptonLanguageManager.BackStyleStrings.SeparatorHighProfile},
            {PaletteBackStyle.SeparatorHighInternalProfile, KryptonLanguageManager.BackStyleStrings.SeparatorHighInternalProfile},
            {PaletteBackStyle.TabHighProfile, KryptonLanguageManager.BackStyleStrings.TabHighProfile},
            {PaletteBackStyle.TabStandardProfile, KryptonLanguageManager.BackStyleStrings.TabStandardProfile},
            {PaletteBackStyle.TabLowProfile, KryptonLanguageManager.BackStyleStrings.TabLowProfile},
            {PaletteBackStyle.TabOneNote, KryptonLanguageManager.BackStyleStrings.TabOneNote},
            {PaletteBackStyle.TabDock, KryptonLanguageManager.BackStyleStrings.TabDock},
            {PaletteBackStyle.TabDockAutoHidden, KryptonLanguageManager.BackStyleStrings.TabDockAutoHidden},
            {PaletteBackStyle.TabCustom1, KryptonLanguageManager.BackStyleStrings.TabCustom1},
            {PaletteBackStyle.TabCustom2, KryptonLanguageManager.BackStyleStrings.TabCustom2},
            {PaletteBackStyle.TabCustom3, KryptonLanguageManager.BackStyleStrings.TabCustom3},
            {PaletteBackStyle.Control, KryptonLanguageManager.BackStyleStrings.Control }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteBackStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
