﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Data associated with a change in the active page.
    /// </summary>
    public class ActivePageChangedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ActivePageChangedEventArgs class.
        /// </summary>
        /// <param name="oldPage">Previous active page value.</param>
        /// <param name="newPage">New active page value.</param>
        public ActivePageChangedEventArgs(KryptonPage oldPage,
                                          KryptonPage newPage)
        {
            OldPage = oldPage;
            NewPage = newPage;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the old page reference.
        /// </summary>
        public KryptonPage OldPage { get; }

        /// <summary>
        /// Gets the new page reference.
        /// </summary>
        public KryptonPage NewPage { get; }

        #endregion
    }
}
