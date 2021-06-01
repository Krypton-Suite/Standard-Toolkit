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

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for the PageCloseRequest event.
    /// </summary>
    public class CloseRequestEventArgs : UniqueNameEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseRequestEventArgs class.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page associated with the event.</param>
        /// <param name="closeRequest">Initial close action to use.</param>
        public CloseRequestEventArgs(string uniqueName, DockingCloseRequest closeRequest)
            : base(uniqueName)
        {
            CloseRequest = closeRequest;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the close action to be performed.
        /// </summary>
        public DockingCloseRequest CloseRequest { get; set; }

        #endregion
    }
}
