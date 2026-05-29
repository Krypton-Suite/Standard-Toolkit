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
/// Event arguments for a AutoHiddenSeparatorResize event.
/// </summary>
public class AutoHiddenSeparatorResizeEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the AutoHiddenSeparatorResizeEventArgs class.
    /// </summary>
    /// <param name="separator">Reference to separator control instance.</param>
    /// <param name="dockspace">Reference to dockspace control instance.</param>
    /// <param name="page">Reference to page contained in the dockspace.</param>
    /// <param name="resizeRect">Initial resizing rectangle.</param>
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
    /// Gets a reference to the KryptonSeparator control.
    /// </summary>
    public KryptonSeparator SeparatorControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockspace control.
    /// </summary>
    public KryptonDockspace DockspaceControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonPage instance.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Gets and sets the rectangle that limits resizing of the dockspace using the separator.
    /// </summary>
    public Rectangle ResizeRect { get; set; }

    #endregion
}