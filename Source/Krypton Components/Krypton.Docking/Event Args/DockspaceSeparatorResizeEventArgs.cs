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
/// Event arguments for a DockspaceSeparatorResize event.
/// </summary>
public class DockspaceSeparatorResizeEventArgs : DockspaceSeparatorEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockspaceSeparatorResizeEventArgs class.
    /// </summary>
    /// <param name="separator">Reference to separator control instance.</param>
    /// <param name="element">Reference to dockspace docking element that is managing the separator.</param>
    /// <param name="resizeRect">Initial resizing rectangle.</param>
    public DockspaceSeparatorResizeEventArgs(KryptonSeparator separator,
        KryptonDockingDockspace element,
        Rectangle resizeRect)
        : base(separator, element) =>
        ResizeRect = resizeRect;

    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the rectangle that limits resizing of the dockspace using the separator.
    /// </summary>
    public Rectangle ResizeRect { get; set; }

    #endregion
}