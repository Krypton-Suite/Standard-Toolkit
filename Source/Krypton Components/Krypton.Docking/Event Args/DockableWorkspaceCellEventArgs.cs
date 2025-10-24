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

namespace Krypton.Docking;

/// <summary>
/// Event arguments for a DockableWorkspaceCellAdding/DockableWorkspaceCellRemoving events.
/// </summary>
public class DockableWorkspaceCellEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockableWorkspaceCellEventArgs class.
    /// </summary>
    /// <param name="workspace">Reference to existing dockable workspace control instance.</param>
    /// <param name="element">Reference to docking workspace element that is managing the dockable workspace control.</param>
    /// <param name="cell">Reference to workspace control cell instance.</param>
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
    /// Gets a reference to the KryptonDockableWorkspace that contains the cell.
    /// </summary>
    public KryptonDockableWorkspace DockableWorkspaceControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingWorkspace that is managing the dockable workspace.
    /// </summary>
    public KryptonDockingWorkspace WorkspaceElement { get; }

    /// <summary>
    /// Gets a reference to the KryptonWorkspaceCell control.
    /// </summary>
    public KryptonWorkspaceCell CellControl { get; }

    #endregion
}