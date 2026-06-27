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
/// Event payload when a dockable workspace control is added to or removed from the docking tree.
/// </summary>
public class DockableWorkspaceEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the dockable workspace control and its owning docking element.
    /// </summary>
    /// <param name="workspace">Dockable workspace control that was added or removed; may be null.</param>
    /// <param name="element">Docking element that owns the dockable workspace control.</param>
    public DockableWorkspaceEventArgs(KryptonDockableWorkspace? workspace,
        KryptonDockingWorkspace element)
    {
        DockableWorkspaceControl = workspace;
        DockingWorkspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Dockable workspace control that was added or removed; may be null.
    /// </summary>
    public KryptonDockableWorkspace? DockableWorkspaceControl { get; }

    /// <summary>
    /// Docking element that owns the dockable workspace control; assigned at construction.
    /// </summary>
    public KryptonDockingWorkspace DockingWorkspaceElement { get; }

    #endregion
}
