#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  Version 6.0.0  
 *
 */
#endregion

using System;
using Krypton.Navigator;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need to provide a colletion of pages.
    /// </summary>
    public class PagesEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PagesEventArgs class.
        /// </summary>
        /// <param name="pages">Collection of pages.</param>
        public PagesEventArgs(KryptonPageCollection pages)
        {
            Pages = pages;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to a collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

        #endregion
    }
}
