// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System.Drawing;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Target within the workspace.
    /// </summary>
    public abstract class DragTargetWorkspace : DragTarget
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DragTargetWorkspace class.
        /// </summary>
        /// <param name="screenRect">Rectangle for screen area.</param>
        /// <param name="hotRect">Rectangle for hot area.</param>
        /// <param name="drawRect">Rectangle for draw area.</param>
        /// <param name="hint">Target hint which should be one of the edges.</param>
        /// <param name="workspace">Control instance for drop.</param>
        /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
        public DragTargetWorkspace(Rectangle screenRect,
                                   Rectangle hotRect,
                                   Rectangle drawRect,
                                   DragTargetHint hint,
                                   KryptonWorkspace workspace,
                                   KryptonPageFlags allowFlags)
            : base(screenRect, hotRect, drawRect, hint, allowFlags)
        {
            Workspace = workspace;
        }

        /// <summary>
        /// Release unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">Called from Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Workspace = null;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the target workspace control.
        /// </summary>
        public KryptonWorkspace Workspace { get; private set; }

        #endregion

        #region Protected
        /// <summary>
        /// Process the drag pages in the context of a target navigator.
        /// </summary>
        /// <param name="workspace">Target workspace instance.</param>
        /// <param name="target">Target workspace cell instance.</param>
        /// <param name="data">Dragged page data.</param>
        /// <returns>Last page to be transferred.</returns>
        protected KryptonPage ProcessDragEndData(KryptonWorkspace workspace,
                                                 KryptonWorkspaceCell target,
                                                 PageDragEndData data)
        {
            KryptonPage ret = null;

            // Add each source page to the target
            foreach (KryptonPage page in data.Pages)
            {
                // Only add the page if one of the allow flags is set
                if ((page.Flags & (int)AllowFlags) != 0)
                {
                    // Use event to allow decision on if the page should be dropped
                    // (or even swap the page for a different page to be dropped)
                    PageDropEventArgs e = new PageDropEventArgs(page);
                    workspace.OnPageDrop(e);

                    if (!e.Cancel && (e.Page != null))
                    {
                        target.Pages.Add(e.Page);
                        ret = e.Page;
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
