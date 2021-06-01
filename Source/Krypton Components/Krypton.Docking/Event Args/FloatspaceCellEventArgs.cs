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
using Krypton.Workspace;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a FloatspaceCellAdding/FloatingCellRemoving events.
    /// </summary>
    public class FloatspaceCellEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the FloatspaceCellEventArgs class.
        /// </summary>
        /// <param name="floatspace">Reference to existing floatspace control instance.</param>
        /// <param name="element">Reference to docking floatspace element that is managing the floatspace control.</param>
        /// <param name="cell">Reference tofloatspace control cell instance.</param>
        public FloatspaceCellEventArgs(KryptonFloatspace floatspace,
                                       KryptonDockingFloatspace element,
                                       KryptonWorkspaceCell cell)
        {
            FloatspaceControl = floatspace;
            FloatspaceElement = element;
            CellControl = cell;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonFloatspace control.
        /// </summary>
        public KryptonFloatspace FloatspaceControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingFloatspace that is managing the floatspace.
        /// </summary>
        public KryptonDockingFloatspace FloatspaceElement { get; }

        /// <summary>
        /// Gets a reference to the KryptonWorkspaceCell control.
        /// </summary>
        public KryptonWorkspaceCell CellControl { get; }

        #endregion
    }
}
