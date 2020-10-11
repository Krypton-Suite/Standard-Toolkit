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
using System.Collections.Generic;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for listing pages unmatched during the load process.
    /// </summary>
    public class PagesUnmatchedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PagesUnmatchedEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="unmatched">List of pages unmatched during the load process.</param>
        public PagesUnmatchedEventArgs(KryptonWorkspace workspace,
                                       List<KryptonPage> unmatched)
        {
            Workspace = workspace;
            Unmatched = unmatched;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace reference.
        /// </summary>
        public KryptonWorkspace Workspace { get; }

        /// <summary>
        /// Gets the xml reader.
        /// </summary>
        public List<KryptonPage> Unmatched { get; }

        #endregion
    }
}
