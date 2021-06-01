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

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need to provide a unique name.
    /// </summary>
    public class UniqueNameEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the UniqueNameEventArgs class.
        /// </summary>
        /// <param name="uniqueName">Unique name of page.</param>
        public UniqueNameEventArgs(string uniqueName)
        {
            UniqueName = uniqueName;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the unique name of a page.
        /// </summary>
        public string UniqueName { get; }

        #endregion
    }
}
