#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

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
