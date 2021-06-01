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

namespace Krypton.Navigator
{
    /// <summary>
    /// Provide a KryptonPageFlags enumeration with event details.
    /// </summary>
    public class KryptonPageFlagsEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPageFlagsEventArgs class.
        /// </summary>
        /// <param name="flags">KryptonPageFlags enumeration.</param>
        public KryptonPageFlagsEventArgs(KryptonPageFlags flags)
        {
            // Remember parameter details
            Flags = flags;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the KryptonPageFlags enumeration value.
        /// </summary>
        public KryptonPageFlags Flags { get; }

        #endregion
    }
}
