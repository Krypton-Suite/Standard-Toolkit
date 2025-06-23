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
/// Event arguments for a DockspaceCellAdding/DockspaceCellRemoving events.
/// </summary>
public class DockspaceCellEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockspaceCellEventArgs class.
    /// </summary>
    /// <param name="dockspace">Reference to existing dockspace control instance.</param>
    /// <param name="element">Reference to docking dockspace element that is managing the dockspace control.</param>
    /// <param name="cell">Reference to dockspace control cell instance.</param>
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
    /// Gets a reference to the KryptonDockspace that contains the cell.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace.
    /// </summary>
    public KryptonDockingDockspace DockspaceElement { get; }

    /// <summary>
    /// Gets a reference to the KryptonWorkspaceCell control.
    /// </summary>
    public KryptonWorkspaceCell CellControl { get; }

    #endregion
}