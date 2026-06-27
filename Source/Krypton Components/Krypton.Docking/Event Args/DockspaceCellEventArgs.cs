#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Event arguments raised when a workspace cell is added to or removed from a dockspace.
/// </summary>
public class DockspaceCellEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the dockspace control, docking element, and cell involved in the add or remove operation.
    /// </summary>
    /// <param name="dockspace">Dockspace control hosting the cell.</param>
    /// <param name="element">Docking element coordinating the dockspace in the layout tree.</param>
    /// <param name="cell">Workspace cell being added to or removed from the dockspace.</param>
    public DockspaceCellEventArgs(KryptonDockspace dockspace,
        KryptonDockingDockspace element,
        KryptonWorkspaceCell cell)
    {
        DockspaceControl = dockspace;
        DockspaceElement = element;
        CellControl = cell;
    }
    #endregion

    #region Public
    /// <summary>
    /// Dockspace UI control hosting the cell.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Docking element coordinating the dockspace in the layout tree.
    /// </summary>
    public KryptonDockingDockspace DockspaceElement { get; }

    /// <summary>
    /// Workspace cell added to or removed from the dockspace.
    /// </summary>
    public KryptonWorkspaceCell CellControl { get; }

    #endregion
}
