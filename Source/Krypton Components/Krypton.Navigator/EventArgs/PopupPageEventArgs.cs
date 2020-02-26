// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System.Drawing;

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a popup page event.
    /// </summary>
    public class PopupPageEventArgs : KryptonPageCancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PopupPageEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        /// <param name="screenRect">Screen rectangle for showing the popup.</param>
        public PopupPageEventArgs(KryptonPage page, 
                                  int index, 
                                  Rectangle screenRect)
            : base(page, index)
        {
            ScreenRect = screenRect;
        }
        #endregion

        #region ScreenRect
        /// <summary>
        /// Gets and sets the screen rectangle for showing the popup page.
        /// </summary>
        public Rectangle ScreenRect { get; set; }

        #endregion
    }
}
