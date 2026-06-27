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
/// Event payload when a dockspace workspace cell is added to or removed from a dockspace.
/// </summary>
public class DockspaceCellEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the parent dockspace, its docking element, and the affected workspace cell.
    /// </summary>
    /// <param name="dockspace">Dockspace control that contains the cell.</param>
    /// <param name="element">Docking element that owns the dockspace control.</param>
    /// <param name="cell">Workspace cell that was added or removed.</param>
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
    /// Dockspace control that contains the cell; assigned at construction.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Docking element that owns the dockspace control; assigned at construction.
    /// </summary>
    public KryptonDockingDockspace DockspaceElement { get; }

    /// <summary>
    /// Workspace cell that was added or removed; assigned at construction.
    /// </summary>
    public KryptonWorkspaceCell CellControl { get; }

    #endregion
}
