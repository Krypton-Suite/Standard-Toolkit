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
/// Event payload for auto-hidden dockspace separator resize operations where handlers may adjust movement limits.
/// </summary>
public class AutoHiddenSeparatorResizeEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the separator, dockspace, page, and initial resize bounds for the auto-hidden resize.
    /// </summary>
    /// <param name="separator">Separator control being dragged.</param>
    /// <param name="dockspace">Dockspace control adjacent to the separator.</param>
    /// <param name="page">Page contained in the dockspace; may be null.</param>
    /// <param name="resizeRect">Initial movement rectangle before handler adjustment.</param>
    public AutoHiddenSeparatorResizeEventArgs(KryptonSeparator separator,
        KryptonDockspace dockspace,
        KryptonPage? page,
        Rectangle resizeRect)
    {
        SeparatorControl = separator;
        DockspaceControl = dockspace;
        Page = page;
        ResizeRect = resizeRect;
    }
    #endregion

    #region Public
    /// <summary>
    /// Separator control being dragged; assigned at construction.
    /// </summary>
    public KryptonSeparator SeparatorControl { get; }

    /// <summary>
    /// Dockspace control adjacent to the separator; assigned at construction.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Page contained in the dockspace; may be null.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Movement rectangle applied during separator dragging; handlers may change this value before it is applied.
    /// </summary>
    public Rectangle ResizeRect { get; set; }

    #endregion
}
