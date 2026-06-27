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
/// Event payload for dockspace separator resize operations where handlers may adjust movement limits.
/// </summary>
public class DockspaceSeparatorResizeEventArgs : DockspaceSeparatorEventArgs
{
    #region Identity
    /// <summary>
    /// Captures the separator, owning dockspace element, and initial resize bounds.
    /// </summary>
    /// <param name="separator">Separator control being dragged.</param>
    /// <param name="element">Docking element that owns the separator.</param>
    /// <param name="resizeRect">Initial movement rectangle before handler adjustment.</param>
    public DockspaceSeparatorResizeEventArgs(KryptonSeparator separator,
        KryptonDockingDockspace element,
        Rectangle resizeRect)
        : base(separator, element) =>
        ResizeRect = resizeRect;

    #endregion

    #region Public
    /// <summary>
    /// Movement rectangle applied during separator dragging; handlers may change this value before it is applied.
    /// </summary>
    public Rectangle ResizeRect { get; set; }

    #endregion
}
