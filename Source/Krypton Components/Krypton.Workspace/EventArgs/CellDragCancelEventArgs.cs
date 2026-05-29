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

namespace Krypton.Workspace;

/// <summary>
/// Details for an cancellable event that provides pages and cell associated with a page dragging event.
/// </summary>
public class CellDragCancelEventArgs : PageDragCancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the CellDragCancelEventArgs class.
    /// </summary>
    /// <param name="screenPoint">Screen point of the mouse.</param>
    /// <param name="screenOffset">Screen offset of the mouse to the source element.</param>
    /// <param name="c">Control that started the drag operation.</param>
    /// <param name="pages">Array of event associated pages.</param>
    /// <param name="cell">Workspace cell associated with pages.</param>
    public CellDragCancelEventArgs(Point screenPoint,
        Point screenOffset,
        Control c,
        KryptonPage[] pages,
        KryptonWorkspaceCell cell)
        : base(screenPoint, screenOffset, c, pages) =>
        Cell = cell;

    /// <summary>
    /// Initialize a new instance of the CellDragCancelEventArgs class.
    /// </summary>
    /// <param name="e">Event to upgrade to this event.</param>
    /// <param name="cell">Workspace cell associated with pages.</param>
    public CellDragCancelEventArgs(PageDragCancelEventArgs e,
        KryptonWorkspaceCell cell)
        : base(e.ScreenPoint, e.ElementOffset, e.Control, e.Pages) =>
        Cell = cell;

    #endregion

    #region Cell
    /// <summary>
    /// Gets access to associated workspace cell.
    /// </summary>
    public KryptonWorkspaceCell Cell { get; }

    #endregion
}