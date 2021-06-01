﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Toolkit;

namespace Krypton.Workspace
{
    /// <summary>
    /// Proxy class for receiving page notifications.
    /// </summary>
    public class CellPageNotify : IDragPageNotify
    {
        #region Instance Fields
        private readonly KryptonWorkspace _workspace;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CellPageNotify class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace.</param>
        public CellPageNotify(KryptonWorkspace workspace)
        {
            _workspace = workspace;
        }
        #endregion

        #region Public
        /// <summary>
        /// Occurs when a page drag is about to begin and allows it to be cancelled.
        /// </summary>
        /// <param name="sender">Source of the page drag; should never be null.</param>
        /// <param name="navigator">Navigator instance associated with source; can be null.</param>
        /// <param name="e">Event arguments indicating list of pages being dragged.</param>
        public void PageDragStart(object sender, KryptonNavigator navigator, PageDragCancelEventArgs e)
        {
            _workspace.InternalPageDragStart(sender, navigator, e);
        }

        /// <summary>
        /// Occurs when the mouse moves during the drag operation.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
        public void PageDragMove(object sender, PointEventArgs e)
        {
            _workspace.InternalPageDragMove(sender as KryptonNavigator, e);
        }

        /// <summary>
        /// Occurs when drag operation completes with pages being dropped.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        public bool PageDragEnd(object sender, PointEventArgs e)
        {
            return _workspace.InternalPageDragEnd(sender as KryptonNavigator, e);
        }

        /// <summary>
        /// Occurs when dragging pages has been cancelled.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        public void PageDragQuit(object sender)
        {
            _workspace.InternalPageDragQuit(sender as KryptonNavigator);
        }
        #endregion
    }
}
