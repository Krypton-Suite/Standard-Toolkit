#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonSearchBoxStrings : GlobalId
    {
        #region Static Strings

        private const string DEFAULT_SEARCH_BOX_CUE_TEXT = @"Enter a search term...";
        private const string DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP = @"Clear Search Box";
        private const string DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP_DESCRIPTION = @"Click to clear the search box.";

        #endregion

        #region Identity

        public KryptonSearchBoxStrings()
        {
            Reset();
        }

        #endregion
        #region Properties

        [Category("Visuals")]
        [Description("The cue text displayed in the search box when it is empty.")]
        [DefaultValue(DEFAULT_SEARCH_BOX_CUE_TEXT)]
        [Localizable(true)]
        public string SearchBoxCueText { get; set; }

        [Category("Visuals")]
        [Description("The tooltip text displayed when hovering over the clear search box button.")]
        [DefaultValue(DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP)]
        [Localizable(true)]
        public string ClearSearchBoxToolTip { get; set; }

        [Category("Visuals")]
        [Description("The description text displayed in the tooltip when hovering over the clear search box button.")]
        [DefaultValue(DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP_DESCRIPTION)]
        [Localizable(true)]
        public string ClearSearchBoxToolTipDescription { get; set; }

        #endregion

        #region IsDefault

        [Browsable(false)]
        public bool IsDefault =>
            SearchBoxCueText == DEFAULT_SEARCH_BOX_CUE_TEXT &&
            ClearSearchBoxToolTip == DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP &&
            ClearSearchBoxToolTipDescription == DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP_DESCRIPTION;

        #endregion

        #region Public

        public void Reset()
        {
            SearchBoxCueText = DEFAULT_SEARCH_BOX_CUE_TEXT;
            ClearSearchBoxToolTip = DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP;
            ClearSearchBoxToolTipDescription = DEFAULT_CLEAR_SEARCH_BOX_TOOLTIP_DESCRIPTION;
        }

        #endregion

        #region Overrides

        public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

        #endregion
    }
}
