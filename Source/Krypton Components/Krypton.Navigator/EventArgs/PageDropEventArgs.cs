// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System.ComponentModel;

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for an event that indicates a page is being dropped.
    /// </summary>
    public class PageDropEventArgs : CancelEventArgs
    {
        #region Instance Fields
        private KryptonPage _page;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageDropEventArgs class.
        /// </summary>
        /// <param name="page">Page that is being dropped.</param>
        public PageDropEventArgs(KryptonPage page)
        {
            _page = page;
        }
        #endregion

        #region Page
        /// <summary>
        /// Gets and sets the page to be dropped.
        /// </summary>
        public KryptonPage Page
        {
            get => _page;
            set => _page = Page;
        }
        #endregion
    }
}
