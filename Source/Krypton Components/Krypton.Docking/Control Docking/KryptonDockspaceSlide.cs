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

using System.ComponentModel;
using Krypton.Navigator;
using Krypton.Workspace;

namespace Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonWorkspace to work within the docking edge of a control.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockspaceSlide : KryptonDockspace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockspaceSlide class.
        /// </summary>
        public KryptonDockspaceSlide()
        {
            // Cannot drag pages inside the sliding dockspace
            AllowPageDrag = false;
        }
        #endregion

        #region Protectect
        /// <summary>
        /// Initialize a new cell.
        /// </summary>
        /// <param name="cell">Cell being added to the control.</param>
        protected override void NewCellInitialize(KryptonWorkspaceCell cell)
        {
            // Let base class perform event hooking and customizations
            base.NewCellInitialize(cell);

            // We only ever show a single page in the dockspace, so remove default 
            // tabbed appearance and instead use a header group mode instead
            cell.NavigatorMode = NavigatorMode.HeaderGroup;
        }
        #endregion
    }
}
