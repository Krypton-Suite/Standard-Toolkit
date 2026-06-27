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
/// Event arguments raised when a workspace cell is added to or removed from a dockable workspace.
/// </summary>
public class DockableWorkspaceCellEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the workspace control, docking element, and cell involved in the add or remove operation.
    /// </summary>
    /// <param name="workspace">Dockable workspace hosting the cell.</param>
    /// <param name="element">Docking element coordinating the dockable workspace in the layout tree.</param>
    /// <param name="cell">Workspace cell being added to or removed from the dockable workspace.</param>
    public DockableWorkspaceCellEventArgs(KryptonDockableWorkspace workspace,
        KryptonDockingWorkspace element,
        KryptonWorkspaceCell cell)
    {
        DockableWorkspaceControl = workspace;
        WorkspaceElement = element;
        CellControl = cell;
    }
    #endregion

    #region Public
    /// <summary>
    /// Dockable workspace UI control hosting the cell.
    /// </summary>
    public KryptonDockableWorkspace DockableWorkspaceControl { get; }

    /// <summary>
    /// Docking element coordinating the dockable workspace in the layout tree.
    /// </summary>
    public KryptonDockingWorkspace WorkspaceElement { get; }

    /// <summary>
    /// Workspace cell added to or removed from the dockable workspace.
    /// </summary>
    public KryptonWorkspaceCell CellControl { get; }

    #endregion
}
