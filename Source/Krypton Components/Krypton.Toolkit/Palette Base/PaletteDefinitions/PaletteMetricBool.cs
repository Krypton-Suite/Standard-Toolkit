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
    /// Specifies a bool type metric.
    /// </summary>
    public enum PaletteMetricBool
    {
        /// <summary>
        /// Specifies that no bool metric is required.
        /// </summary>
        None,

        /// <summary>
        /// Specifies when the border is drawn for the header group control.
        /// </summary>
        HeaderGroupOverlay,

        /// <summary>
        /// Specifies that split area controls use faded appearance for non-active area.
        /// </summary>
        SplitWithFading,

        /// <summary>
        /// Specifies that the spare tabs area be treated as the application caption area.
        /// </summary>
        RibbonTabsSpareCaption,

        /// <summary>
        /// Specifies if lines are drawn between nodes in the KryptonTreeView.
        /// </summary>
        TreeViewLines
    }
}