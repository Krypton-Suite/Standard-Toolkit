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
/// Event arguments for a DockableWorkspaceRemoved event.
/// </summary>
public class DockableWorkspaceEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockableWorkspaceEventArgs class.
    /// </summary>
    /// <param name="workspace">Reference to dockable workspace control instance.</param>
    /// <param name="element">Reference to docking workspace element that is managing the dockable workspace control.</param>
    public DockableWorkspaceEventArgs(KryptonDockableWorkspace? workspace,
        KryptonDockingWorkspace element)
    {
        DockableWorkspaceControl = workspace;
        DockingWorkspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the KryptonDockableWorkspace control.
    /// </summary>
    public KryptonDockableWorkspace? DockableWorkspaceControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingWorkspace that is managing the dockable workspace control.
    /// </summary>
    public KryptonDockingWorkspace DockingWorkspaceElement { get; }

    #endregion
}