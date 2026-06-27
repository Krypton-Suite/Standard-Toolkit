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
/// Event arguments raised when a dockable workspace is added to or removed from the docking layout.
/// </summary>
public class DockableWorkspaceEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the dockable workspace control and docking element for the add or remove operation.
    /// </summary>
    /// <param name="workspace">Dockable workspace control being added or removed; may be null.</param>
    /// <param name="element">Docking element coordinating the dockable workspace in the layout tree.</param>
    public DockableWorkspaceEventArgs(KryptonDockableWorkspace? workspace,
        KryptonDockingWorkspace element)
    {
        DockableWorkspaceControl = workspace;
        DockingWorkspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Dockable workspace UI control being added or removed; may be null.
    /// </summary>
    public KryptonDockableWorkspace? DockableWorkspaceControl { get; }

    /// <summary>
    /// Docking element coordinating the dockable workspace in the layout tree.
    /// </summary>
    public KryptonDockingWorkspace DockingWorkspaceElement { get; }

    #endregion
}
