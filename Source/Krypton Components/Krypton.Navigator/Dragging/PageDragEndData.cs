// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for an event that provides pages and cell associated with a page dragging event.
    /// </summary>
    public class PageDragEndData
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageDragEndData class.
        /// </summary>
        /// <param name="source">Source object for the drag data..</param>
        /// <param name="pages">Collection of pages.</param>
        public PageDragEndData(object source,
                               KryptonPageCollection pages)
            : this(source, null, pages)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PageDragEndData class.
        /// </summary>
        /// <param name="source">Source object for the drag data..</param>
        /// <param name="navigator">Navigator associated with pages.</param>
        /// <param name="pages">Collection of pages.</param>
        public PageDragEndData(object source,
                               KryptonNavigator navigator,
                               KryptonPageCollection pages)
        {
            Source = source;
            Navigator = navigator;
            Pages = pages;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the source of the drag data.
        /// </summary>
        public object Source { get; }

        /// <summary>
        /// Gets access to any associated KryptonNavigator instance.
        /// </summary>
        public KryptonNavigator Navigator { get; }

        /// <summary>
        /// Gets access to the collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

        #endregion
    }
}
