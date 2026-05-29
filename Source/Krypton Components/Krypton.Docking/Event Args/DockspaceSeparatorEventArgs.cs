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
/// Event arguments for a DockspaceSeparatorAdding/DockspaceSeparatorRemoved event.
/// </summary>
public class DockspaceSeparatorEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockspaceSeparatorEventArgs class.
    /// </summary>
    /// <param name="separator">Reference to separator control instance.</param>
    /// <param name="element">Reference to dockspace docking element that is managing the separator.</param>
    public DockspaceSeparatorEventArgs(KryptonSeparator separator,
        KryptonDockingDockspace? element)
    {
        SeparatorControl = separator;
        DockspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the KryptonSeparator control..
    /// </summary>
    public KryptonSeparator SeparatorControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace.
    /// </summary>
    public KryptonDockingDockspace? DockspaceElement { get; }

    #endregion
}