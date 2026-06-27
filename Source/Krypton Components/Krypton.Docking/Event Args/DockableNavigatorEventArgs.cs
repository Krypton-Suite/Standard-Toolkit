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
/// Event arguments raised when a dockable navigator is added or removed from the docking layout.
/// </summary>
public class DockableNavigatorEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the navigator control and docking navigator element for the add or remove operation.
    /// </summary>
    /// <param name="navigator">Dockable navigator control being added or removed.</param>
    /// <param name="element">Docking element coordinating the dockable navigator in the layout tree.</param>
    public DockableNavigatorEventArgs(KryptonDockableNavigator navigator,
        KryptonDockingNavigator element)
    {
        DockableNavigatorControl = navigator;
        DockingNavigatorElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Dockable navigator UI control being added or removed.
    /// </summary>
    public KryptonDockableNavigator DockableNavigatorControl { get; }

    /// <summary>
    /// Docking element coordinating the dockable navigator in the layout tree.
    /// </summary>
    public KryptonDockingNavigator DockingNavigatorElement { get; }

    #endregion
}
