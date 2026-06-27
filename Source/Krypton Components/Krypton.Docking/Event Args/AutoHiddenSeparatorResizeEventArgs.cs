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
/// Event arguments raised while an auto-hidden separator is resized, allowing handlers to constrain the resize bounds.
/// </summary>
public class AutoHiddenSeparatorResizeEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the separator, dockspace, page, and initial resize bounds for the auto-hidden separator drag operation.
    /// </summary>
    /// <param name="separator">Separator control being dragged to resize the dockspace.</param>
    /// <param name="dockspace">Dockspace control whose size is being adjusted.</param>
    /// <param name="page">Page contained in the dockspace being resized; may be null.</param>
    /// <param name="resizeRect">Initial bounds rectangle limiting dockspace resize during separator drag.</param>
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
    /// Separator control being dragged to resize the dockspace.
    /// </summary>
    public KryptonSeparator SeparatorControl { get; }

    /// <summary>
    /// Dockspace control whose size is being adjusted by the separator drag.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Page contained in the dockspace being resized; may be null.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Bounds rectangle limiting dockspace resize during separator drag; handlers may adjust before resize applies.
    /// </summary>
    public Rectangle ResizeRect { get; set; }

    #endregion
}
