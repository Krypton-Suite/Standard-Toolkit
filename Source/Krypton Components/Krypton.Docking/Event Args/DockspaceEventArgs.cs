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
/// Event payload when a dockspace control is added to or removed from the docking tree.
/// </summary>
public class DockspaceEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the dockspace control and its owning docking element.
    /// </summary>
    /// <param name="dockspace">Dockspace control that was added or removed.</param>
    /// <param name="element">Docking element that owns the dockspace control; may be null.</param>
    public DockspaceEventArgs(KryptonDockspace dockspace,
        KryptonDockingDockspace? element)
    {
        DockspaceControl = dockspace;
        DockspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Dockspace control that was added or removed; assigned at construction.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Docking element that owns the dockspace control; may be null.
    /// </summary>
    public KryptonDockingDockspace? DockspaceElement { get; }

    #endregion
}
