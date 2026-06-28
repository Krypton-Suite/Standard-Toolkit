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
/// Event arguments raised while a dockspace separator is resized, allowing handlers to constrain the resize bounds.
/// </summary>
public class DockspaceSeparatorResizeEventArgs : DockspaceSeparatorEventArgs
{
    #region Identity
    /// <summary>
    /// Extends separator event arguments with initial resize bounds for the dockspace separator drag operation.
    /// </summary>
    /// <param name="separator">Separator control being dragged to resize the dockspace.</param>
    /// <param name="element">Dockspace docking element that owns the separator.</param>
    /// <param name="resizeRect">Initial bounds rectangle limiting dockspace resize during separator drag.</param>
    public DockspaceSeparatorResizeEventArgs(KryptonSeparator separator,
        KryptonDockingDockspace element,
        Rectangle resizeRect)
        : base(separator, element) =>
        ResizeRect = resizeRect;

    #endregion

    #region Public
    /// <summary>
    /// Bounds rectangle limiting dockspace resize during separator drag; handlers may adjust before resize applies.
    /// </summary>
    public Rectangle ResizeRect { get; set; }

    #endregion
}
