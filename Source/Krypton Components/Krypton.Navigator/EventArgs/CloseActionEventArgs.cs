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

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a close button action event.
    /// </summary>
    public class CloseActionEventArgs : KryptonPageEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseActionEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        /// <param name="action">Close action to take.</param>
        public CloseActionEventArgs(KryptonPage page, 
                                    int index, 
                                    CloseButtonAction action)
            : base(page, index)
        {
            Action = action;
        }
        #endregion

        #region Action
        /// <summary>
        /// Gets and sets the close action to take.
        /// </summary>
        public CloseButtonAction Action { get; set; }

        #endregion
    }
}
