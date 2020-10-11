// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System;

namespace Krypton.Workspace
{
    /// <summary>
    /// Data associated with a change in the active cell.
    /// </summary>
    public class ActiveCellChangedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ActiveCellChangedEventArgs class.
        /// </summary>
        /// <param name="oldCell">Previous active cell value.</param>
        /// <param name="newCell">New active cell value.</param>
        public ActiveCellChangedEventArgs(KryptonWorkspaceCell oldCell,
                                          KryptonWorkspaceCell newCell)
        {
            OldCell = oldCell;
            NewCell = newCell;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the old cell reference.
        /// </summary>
        public KryptonWorkspaceCell OldCell { get; }

        /// <summary>
        /// Gets the new cell reference.
        /// </summary>
        public KryptonWorkspaceCell NewCell { get; }

        #endregion
    }
}
