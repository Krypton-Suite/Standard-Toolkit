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
/// Event arguments for a DockableNavigatorEventArgs event.
/// </summary>
public class DockableNavigatorEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockableNavigatorEventArgs class.
    /// </summary>
    /// <param name="navigator">Reference to dockable navigator control instance.</param>
    /// <param name="element">Reference to docking navigator element that is managing the dockable workspace control.</param>
    public DockableNavigatorEventArgs(KryptonDockableNavigator navigator,
        KryptonDockingNavigator element)
    {
        DockableNavigatorControl = navigator;
        DockingNavigatorElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the KryptonDockableNavigator control.
    /// </summary>
    public KryptonDockableNavigator DockableNavigatorControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingNavigator that is managing the dockable workspace control.
    /// </summary>
    public KryptonDockingNavigator DockingNavigatorElement { get; }

    #endregion
}