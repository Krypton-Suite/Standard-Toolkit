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

using System.ComponentModel;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event arguments for events that need to request a KryptonPage from a provided unique name.
    /// </summary>
    public class RecreateLoadingPageEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RecreateLoadingPageEventArgs class.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page that needs creating.</param>
        public RecreateLoadingPageEventArgs(string uniqueName)
            : base(false)
        {
            UniqueName = uniqueName;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the page to be used for the requested unique name.
        /// </summary>
        public KryptonPage Page { get; set; }

        /// <summary>
        /// Gets the unique name of the page requested to be recreated.
        /// </summary>
        public string UniqueName { get; }

        #endregion
    }
}
