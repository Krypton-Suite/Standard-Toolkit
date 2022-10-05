namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that NavigatorMode values appear as neat text at design time.
    /// </summary>
    public class NavigatorModeConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorMode class.
        /// </summary>
        public NavigatorModeConverter()
            : base(typeof(NavigatorMode))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(NavigatorMode.BarTabGroup,                    "Bar - Tab - Group"),
            new(NavigatorMode.BarTabOnly,                     "Bar - Tab - Only"),
            new(NavigatorMode.BarRibbonTabGroup,              "Bar - RibbonTab - Group"),
            new(NavigatorMode.BarRibbonTabOnly,               "Bar - RibbonTab - Only"),
            new(NavigatorMode.BarCheckButtonGroupOutside,     "Bar - CheckButton - Group - Outside"),
            new(NavigatorMode.BarCheckButtonGroupInside,      "Bar - CheckButton - Group - Inside"),
            new(NavigatorMode.BarCheckButtonGroupOnly,        "Bar - CheckButton - Group - Only"),
            new(NavigatorMode.BarCheckButtonOnly,             "Bar - CheckButton - Only"),
            new(NavigatorMode.HeaderBarCheckButtonGroup,      "HeaderBar - CheckButton - Group"),
            new(NavigatorMode.HeaderBarCheckButtonHeaderGroup,"HeaderBar - CheckButton - HeaderGroup"),
            new(NavigatorMode.HeaderBarCheckButtonOnly,       "HeaderBar - CheckButton - Only"),
            new(NavigatorMode.StackCheckButtonGroup,          "Stack - CheckButton - Group"),
            new(NavigatorMode.StackCheckButtonHeaderGroup,    "Stack - CheckButton - HeaderGroup"),
            new(NavigatorMode.OutlookFull,                    "Outlook - Full"),
            new(NavigatorMode.OutlookMini,                    "Outlook - Mini"),
            new(NavigatorMode.Panel,                          "Panel"),
            new(NavigatorMode.Group,                          "Group"),
            new(NavigatorMode.HeaderGroup,                    "HeaderGroup"),
            new(NavigatorMode.HeaderGroupTab,                 "HeaderGroup - Tab") };

        #endregion
    }
}
