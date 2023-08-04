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
    internal class PaletteContentStyleConverter : StringLookupConverter<PaletteContentStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteContentStyle, string> _pairs = new Dictionary<PaletteContentStyle, string>
        {
            {PaletteContentStyle.ButtonStandalone, KryptonLanguageManager.ContentStyleStrings.ButtonStandalone},
            {PaletteContentStyle.ButtonLowProfile, KryptonLanguageManager.ContentStyleStrings.ButtonLowProfile},
            {PaletteContentStyle.ButtonButtonSpec, KryptonLanguageManager.ContentStyleStrings.ButtonButtonSpec},
            {PaletteContentStyle.ButtonBreadCrumb, KryptonLanguageManager.ContentStyleStrings.ButtonBreadCrumb},
            {PaletteContentStyle.ButtonCalendarDay, KryptonLanguageManager.ContentStyleStrings.ButtonCalendarDay},
            {PaletteContentStyle.ButtonCluster, KryptonLanguageManager.ContentStyleStrings.ButtonCluster},
            {PaletteContentStyle.ButtonGallery, KryptonLanguageManager.ContentStyleStrings.ButtonGallery},
            {PaletteContentStyle.ButtonNavigatorStack, KryptonLanguageManager.ContentStyleStrings.ButtonNavigatorStack},
            {PaletteContentStyle.ButtonNavigatorOverflow, KryptonLanguageManager.ContentStyleStrings.ButtonNavigatorOverflow},
            {PaletteContentStyle.ButtonNavigatorMini, KryptonLanguageManager.ContentStyleStrings.ButtonNavigatorMini},
            {PaletteContentStyle.ButtonInputControl, KryptonLanguageManager.ContentStyleStrings.ButtonInputControl},
            {PaletteContentStyle.ButtonListItem, KryptonLanguageManager.ContentStyleStrings.ButtonListItem},
            {PaletteContentStyle.ButtonForm, KryptonLanguageManager.ContentStyleStrings.ButtonForm},
            {PaletteContentStyle.ButtonFormClose, KryptonLanguageManager.ContentStyleStrings.ButtonFormClose},
            {PaletteContentStyle.ButtonCommand, KryptonLanguageManager.ContentStyleStrings.ButtonCommand},
            {PaletteContentStyle.ButtonCustom1, KryptonLanguageManager.ContentStyleStrings.ButtonCustom1},
            {PaletteContentStyle.ButtonCustom2, KryptonLanguageManager.ContentStyleStrings.ButtonCustom2},
            {PaletteContentStyle.ButtonCustom3, KryptonLanguageManager.ContentStyleStrings.ButtonCustom3},
            {PaletteContentStyle.ContextMenuHeading, KryptonLanguageManager.ContentStyleStrings.ContextMenuHeading},
            {PaletteContentStyle.ContextMenuItemImage, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemImage},
            {PaletteContentStyle.ContextMenuItemTextStandard, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemTextStandard},
            {PaletteContentStyle.ContextMenuItemTextAlternate, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemTextAlternate},
            {PaletteContentStyle.ContextMenuItemShortcutText, KryptonLanguageManager.ContentStyleStrings.ContextMenuItemShortcutText},
            {PaletteContentStyle.GridHeaderColumnList, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnList},
            {PaletteContentStyle.GridHeaderRowList, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowList},
            {PaletteContentStyle.GridDataCellList, KryptonLanguageManager.ContentStyleStrings.GridDataCellList},
            {PaletteContentStyle.GridHeaderColumnSheet, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnSheet},
            {PaletteContentStyle.GridHeaderRowSheet, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowSheet},
            {PaletteContentStyle.GridDataCellSheet, KryptonLanguageManager.ContentStyleStrings.GridDataCellSheet},
            {PaletteContentStyle.GridHeaderColumnCustom1, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnCustom1},
            {PaletteContentStyle.GridHeaderColumnCustom2, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnCustom2},
            {PaletteContentStyle.GridHeaderColumnCustom3, KryptonLanguageManager.ContentStyleStrings.GridHeaderColumnCustom3},
            {PaletteContentStyle.GridHeaderRowCustom1, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowCustom1},
            {PaletteContentStyle.GridHeaderRowCustom2, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowCustom2},
            {PaletteContentStyle.GridHeaderRowCustom3, KryptonLanguageManager.ContentStyleStrings.GridHeaderRowCustom3},
            {PaletteContentStyle.GridDataCellCustom1, KryptonLanguageManager.ContentStyleStrings.GridDataCellCustom1},
            {PaletteContentStyle.GridDataCellCustom2, KryptonLanguageManager.ContentStyleStrings.GridDataCellCustom2},
            {PaletteContentStyle.GridDataCellCustom3, KryptonLanguageManager.ContentStyleStrings.GridDataCellCustom3},
            {PaletteContentStyle.HeaderPrimary, KryptonLanguageManager.ContentStyleStrings.HeaderPrimary},
            {PaletteContentStyle.HeaderSecondary, KryptonLanguageManager.ContentStyleStrings.HeaderSecondary},
            {PaletteContentStyle.HeaderDockActive, KryptonLanguageManager.ContentStyleStrings.HeaderDockActive},
            {PaletteContentStyle.HeaderDockInactive, KryptonLanguageManager.ContentStyleStrings.HeaderDockInactive},
            {PaletteContentStyle.HeaderForm, KryptonLanguageManager.ContentStyleStrings.HeaderForm},
            {PaletteContentStyle.HeaderCalendar, KryptonLanguageManager.ContentStyleStrings.HeaderCalendar},
            {PaletteContentStyle.HeaderCustom1, KryptonLanguageManager.ContentStyleStrings.HeaderCustom1},
            {PaletteContentStyle.HeaderCustom2, KryptonLanguageManager.ContentStyleStrings.HeaderCustom2},
            {PaletteContentStyle.HeaderCustom3, KryptonLanguageManager.ContentStyleStrings.HeaderCustom3},
            {PaletteContentStyle.LabelNormalControl, KryptonLanguageManager.ContentStyleStrings.LabelNormalControl},
            {PaletteContentStyle.LabelBoldControl, KryptonLanguageManager.ContentStyleStrings.LabelBoldControl},
            {PaletteContentStyle.LabelItalicControl, KryptonLanguageManager.ContentStyleStrings.LabelItalicControl},
            {PaletteContentStyle.LabelTitleControl, KryptonLanguageManager.ContentStyleStrings.LabelTitleControl},
            {PaletteContentStyle.LabelNormalPanel, KryptonLanguageManager.ContentStyleStrings.LabelNormalPanel},
            {PaletteContentStyle.LabelBoldPanel, KryptonLanguageManager.ContentStyleStrings.LabelBoldPanel},
            {PaletteContentStyle.LabelItalicPanel, KryptonLanguageManager.ContentStyleStrings.LabelItalicPanel},
            {PaletteContentStyle.LabelTitlePanel, KryptonLanguageManager.ContentStyleStrings.LabelTitlePanel},
            {PaletteContentStyle.LabelGroupBoxCaption, KryptonLanguageManager.ContentStyleStrings.LabelGroupBoxCaption},
            {PaletteContentStyle.LabelToolTip, KryptonLanguageManager.ContentStyleStrings.LabelToolTip},
            {PaletteContentStyle.LabelSuperTip, KryptonLanguageManager.ContentStyleStrings.LabelSuperTip},
            {PaletteContentStyle.LabelKeyTip, KryptonLanguageManager.ContentStyleStrings.LabelKeyTip},
            {PaletteContentStyle.LabelCustom1, KryptonLanguageManager.ContentStyleStrings.LabelCustom1},
            {PaletteContentStyle.LabelCustom2, KryptonLanguageManager.ContentStyleStrings.LabelCustom2},
            {PaletteContentStyle.TabHighProfile, KryptonLanguageManager.ContentStyleStrings.TabHighProfile},
            {PaletteContentStyle.TabStandardProfile, KryptonLanguageManager.ContentStyleStrings.TabStandardProfile},
            {PaletteContentStyle.TabLowProfile, KryptonLanguageManager.ContentStyleStrings.TabLowProfile},
            {PaletteContentStyle.TabOneNote, KryptonLanguageManager.ContentStyleStrings.TabOneNote},
            {PaletteContentStyle.TabDock, KryptonLanguageManager.ContentStyleStrings.TabDock},
            {PaletteContentStyle.TabDockAutoHidden, KryptonLanguageManager.ContentStyleStrings.TabDockAutoHidden},
            {PaletteContentStyle.TabCustom1, KryptonLanguageManager.ContentStyleStrings.TabCustom1},
            {PaletteContentStyle.TabCustom2, KryptonLanguageManager.ContentStyleStrings.TabCustom2},
            {PaletteContentStyle.TabCustom3, KryptonLanguageManager.ContentStyleStrings.TabCustom3},
            {PaletteContentStyle.InputControlStandalone, KryptonLanguageManager.ContentStyleStrings.InputControlStandalone},
            {PaletteContentStyle.InputControlRibbon, KryptonLanguageManager.ContentStyleStrings.InputControlRibbon},
            {PaletteContentStyle.InputControlCustom1, KryptonLanguageManager.ContentStyleStrings.InputControlCustom1},
            {PaletteContentStyle.InputControlCustom2, KryptonLanguageManager.ContentStyleStrings.InputControlCustom2},
            {PaletteContentStyle.InputControlCustom3,KryptonLanguageManager.ContentStyleStrings.InputControlCustom3 }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteContentStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
