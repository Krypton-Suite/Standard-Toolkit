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
