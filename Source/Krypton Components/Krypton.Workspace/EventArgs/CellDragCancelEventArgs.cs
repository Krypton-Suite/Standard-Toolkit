// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System.Windows.Forms;
using System.Drawing;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Details for an cancellable event that provides pages and cell associated with a page dragging event.
    /// </summary>
    public class CellDragCancelEventArgs : PageDragCancelEventArgs
    {
        #region Instance Fields

        #endregion

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
            : base(screenPoint, screenOffset, c, pages)
        {
            Cell = cell;
        }

        /// <summary>
        /// Initialize a new instance of the CellDragCancelEventArgs class.
        /// </summary>
        /// <param name="e">Event to upgrade to this event.</param>
        /// <param name="cell">Workspace cell associated with pages.</param>
        public CellDragCancelEventArgs(PageDragCancelEventArgs e,
                                       KryptonWorkspaceCell cell)
            : base(e.ScreenPoint, e.ElementOffset, e.Control, e.Pages)
        {
            Cell = cell;
        }
        #endregion

        #region Cell
        /// <summary>
        /// Gets access to associated workspace cell.
        /// </summary>
        public KryptonWorkspaceCell Cell { get; }

        #endregion
    }
}
