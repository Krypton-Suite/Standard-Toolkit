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

using System;

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for an event that provides a new index position for a specified page.
    /// </summary>
    public class TabMovedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabMovedEventArgs class.
        /// </summary>
        /// <param name="page">Reference to page that has been moved.</param>
        /// <param name="index">New index of the page within the page collection.</param>
        public TabMovedEventArgs(KryptonPage page, int index)
        {
            Page = page;
            Index = index;
        }
        #endregion

        #region Dropped
        /// <summary>
        /// Gets a reference to the page that has been moved.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion

        #region Pages
        /// <summary>
        /// Gets the new index of the page within the page collection.
        /// </summary>
        public int Index { get; }

        #endregion
    }
}
