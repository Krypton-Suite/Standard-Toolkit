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
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for close reason event handlers.
    /// </summary>
    public class CloseReasonEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseReasonEventArgs class.
        /// </summary>
        /// <param name="closeReason">Reason for the close action occuring.</param>
        public CloseReasonEventArgs(ToolStripDropDownCloseReason closeReason)
        {
            CloseReason = closeReason;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the reason for the context menu closing.
        /// </summary>
        public ToolStripDropDownCloseReason CloseReason { get; }

        #endregion
    }
}
