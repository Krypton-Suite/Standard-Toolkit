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
/// Event arguments for a DockspaceAdding/DockspaceRemoved events.
/// </summary>
public class DockspaceEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockspaceEventArgs class.
    /// </summary>
    /// <param name="dockspace">Reference to new dockspace control instance.</param>
    /// <param name="element">Reference to docking dockspace element that is managing the dockspace control.</param>
    public DockspaceEventArgs(KryptonDockspace dockspace,
        KryptonDockingDockspace? element)
    {
        DockspaceControl = dockspace;
        DockspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the KryptonDockspace control.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace control.
    /// </summary>
    public KryptonDockingDockspace? DockspaceElement { get; }

    #endregion
}