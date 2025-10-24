#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Workspace;

/// <summary>
/// Data associated with a change in the active cell.
/// </summary>
public class ActiveCellChangedEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ActiveCellChangedEventArgs class.
    /// </summary>
    /// <param name="oldCell">Previous active cell value.</param>
    /// <param name="newCell">New active cell value.</param>
    public ActiveCellChangedEventArgs(KryptonWorkspaceCell? oldCell,
        KryptonWorkspaceCell? newCell)
    {
        OldCell = oldCell;
        NewCell = newCell;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the old cell reference.
    /// </summary>
    public KryptonWorkspaceCell? OldCell { get; }

    /// <summary>
    /// Gets the new cell reference.
    /// </summary>
    public KryptonWorkspaceCell? NewCell { get; }

    #endregion
}