// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using Krypton.Navigator;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for the change in auto hidden page showing state.
    /// </summary>
    public class AutoHiddenShowingStateEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AutoHiddenShowingStateEventArgs class.
        /// </summary>
        /// <param name="page">Page for which state has changed.</param>
        /// <param name="state">New state of the auto hidden page.</param>
        public AutoHiddenShowingStateEventArgs(KryptonPage page, DockingAutoHiddenShowState state)
        {
            Page = page;
            NewState = state;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the page that has had the state change.
        /// </summary>
        public KryptonPage Page { get; }

        /// <summary>
        /// Gets the new state of the auto hidden page.
        /// </summary>
        public DockingAutoHiddenShowState NewState { get; }

        #endregion
    }
}
