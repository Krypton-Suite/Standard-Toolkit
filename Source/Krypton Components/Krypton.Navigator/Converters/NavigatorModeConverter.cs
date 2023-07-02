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

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that NavigatorMode values appear as neat text at design time.
    /// </summary>
    public class NavigatorModeConverter : StringLookupConverter
    {
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
        {
            new Pair(NavigatorMode.BarTabGroup, "Bar - Tab - Group"),
            new Pair(NavigatorMode.BarTabOnly, "Bar - Tab - Only"),
            new Pair(NavigatorMode.BarRibbonTabGroup, "Bar - RibbonTab - Group"),
            new Pair(NavigatorMode.BarRibbonTabOnly, "Bar - RibbonTab - Only"),
            new Pair(NavigatorMode.BarCheckButtonGroupOutside, "Bar - CheckButton - Group - Outside"),
            new Pair(NavigatorMode.BarCheckButtonGroupInside, "Bar - CheckButton - Group - Inside"),
            new Pair(NavigatorMode.BarCheckButtonGroupOnly, "Bar - CheckButton - Group - Only"),
            new Pair(NavigatorMode.BarCheckButtonOnly, "Bar - CheckButton - Only"),
            new Pair(NavigatorMode.HeaderBarCheckButtonGroup, "HeaderBar - CheckButton - Group"),
            new Pair(NavigatorMode.HeaderBarCheckButtonHeaderGroup, "HeaderBar - CheckButton - HeaderGroup"),
            new Pair(NavigatorMode.HeaderBarCheckButtonOnly, "HeaderBar - CheckButton - Only"),
            new Pair(NavigatorMode.StackCheckButtonGroup, "Stack - CheckButton - Group"),
            new Pair(NavigatorMode.StackCheckButtonHeaderGroup, "Stack - CheckButton - HeaderGroup"),
            new Pair(NavigatorMode.OutlookFull, "Outlook - Full"), 
            new Pair(NavigatorMode.OutlookMini, "Outlook - Mini"),
            new Pair(NavigatorMode.Panel, nameof(Panel)), 
            new Pair(NavigatorMode.Group, "Group"),
            new Pair(NavigatorMode.HeaderGroup, "HeaderGroup"),
            new Pair(NavigatorMode.HeaderGroupTab, "HeaderGroup - Tab")
        };

        #endregion
    }
}
