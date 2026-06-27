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
/// Event arguments raised when a dockspace separator is added or removed.
/// </summary>
public class DockspaceSeparatorEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Stores the separator control and owning dockspace docking element for the event.
    /// </summary>
    /// <param name="separator">Separator control instance being added or removed.</param>
    /// <param name="element">Dockspace docking element that owns the separator; may be null.</param>
    public DockspaceSeparatorEventArgs(KryptonSeparator separator,
        KryptonDockingDockspace? element)
    {
        SeparatorControl = separator;
        DockspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Separator control involved in the add or remove operation.
    /// </summary>
    public KryptonSeparator SeparatorControl { get; }

    /// <summary>
    /// Dockspace docking element that owns the separator; may be null when the element is unavailable.
    /// </summary>
    public KryptonDockingDockspace? DockspaceElement { get; }

    #endregion
}
