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
/// Event payload when a dockspace separator is added to or removed from the docking tree.
/// </summary>
public class DockspaceSeparatorEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the separator control and its owning dockspace docking element.
    /// </summary>
    /// <param name="separator">Separator control that was added or removed.</param>
    /// <param name="element">Docking element that owns the separator; may be null.</param>
    public DockspaceSeparatorEventArgs(KryptonSeparator separator,
        KryptonDockingDockspace? element)
    {
        SeparatorControl = separator;
        DockspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Separator control that was added or removed; assigned at construction.
    /// </summary>
    public KryptonSeparator SeparatorControl { get; }

    /// <summary>
    /// Docking element that owns the separator; may be null.
    /// </summary>
    public KryptonDockingDockspace? DockspaceElement { get; }

    #endregion
}
