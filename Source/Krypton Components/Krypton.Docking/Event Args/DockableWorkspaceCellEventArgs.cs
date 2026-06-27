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
/// Event payload when a dockable workspace cell is added to or removed from a dockable workspace.
/// </summary>
public class DockableWorkspaceCellEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the parent dockable workspace, its docking element, and the affected workspace cell.
    /// </summary>
    /// <param name="workspace">Dockable workspace control that contains the cell.</param>
    /// <param name="element">Docking element that owns the dockable workspace control.</param>
    /// <param name="cell">Workspace cell that was added or removed.</param>
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
    /// Dockable workspace control that contains the cell; assigned at construction.
    /// </summary>
    public KryptonDockableWorkspace DockableWorkspaceControl { get; }

    /// <summary>
    /// Docking element that owns the dockable workspace control; assigned at construction.
    /// </summary>
    public KryptonDockingWorkspace WorkspaceElement { get; }

    /// <summary>
    /// Workspace cell that was added or removed; assigned at construction.
    /// </summary>
    public KryptonWorkspaceCell CellControl { get; }

    #endregion
}
