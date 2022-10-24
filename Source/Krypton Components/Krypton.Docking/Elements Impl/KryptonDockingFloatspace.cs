﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Docking
{
    /// <summary>
    /// Provides docking functionality within a floating window using a KryptonFloatspace.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockingFloatspace : KryptonDockingSpace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockingFloatspace class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        public KryptonDockingFloatspace(string name)
            : base(name, "Floating")
        {
            // Create a new floatspace that will be a host for docking pages
            SpaceControl = new KryptonFloatspace();
            FloatspaceControl.Dock = DockStyle.Fill;
            FloatspaceControl.CellPageInserting += OnSpaceCellPageInserting;
            FloatspaceControl.PageCloseClicked += OnFloatspacePageCloseClicked;
            FloatspaceControl.PagesDoubleClicked += OnFloatspacePagesDoubleClicked;
            FloatspaceControl.PageDropDownClicked += OnFloatspaceDropDownClicked;
            FloatspaceControl.BeforePageDrag += OnFloatspaceBeforePageDrag;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the control this element is managing.
        /// </summary>
        public KryptonFloatspace FloatspaceControl => (KryptonFloatspace)SpaceControl;

        /// <summary>
        /// Propagates a request for drag targets down the hierarchy of docking elements.
        /// </summary>
        /// <param name="floatingWindow">Reference to window being dragged.</param>
        /// <param name="dragData">Set of pages being dragged.</param>
        /// <param name="targets">Collection of drag targets.</param>
        public override void PropogateDragTargets(KryptonFloatingWindow floatingWindow,
                                                  PageDragEndData dragData, 
                                                  DragTargetList targets)
        {
            if (FloatspaceControl.CellVisibleCount > 0)
            {
                // Create list of the pages that are allowed to be dropped into this floatspace
                KryptonPageCollection pages = new();
                foreach (KryptonPage page in dragData.Pages)
                {
                    if (page.AreFlagsSet(KryptonPageFlags.DockingAllowFloating))
                    {
                        pages.Add(page);
                    }
                }

                // Do we have any pages left for dragging?
                if (pages.Count > 0)
                {
                    DragTargetList floatspaceTargets = FloatspaceControl.GenerateDragTargets(new PageDragEndData(this, pages), KryptonPageFlags.DockingAllowFloating);
                    targets.AddRange(floatspaceTargets.ToArray());
                }
            }
        }

        /// <summary>
        /// Find the docking location of the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>Enumeration value indicating docking location.</returns>
        public override DockingLocation FindPageLocation(string uniqueName)
        {
            KryptonPage page = FloatspaceControl.PageForUniqueName(uniqueName);
            if ((page != null) && page is not KryptonStorePage)
            {
                return DockingLocation.Floating;
            }
            else
            {
                return DockingLocation.None;
            }
        }

        /// <summary>
        /// Find the docking element that contains the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
        public override IDockingElement FindPageElement(string uniqueName)
        {
            KryptonPage page = FloatspaceControl.PageForUniqueName(uniqueName);
            if ((page != null) && page is not KryptonStorePage)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Find the docking element that contains the location specific store page for the named page.
        /// </summary>
        /// <param name="location">Location to be searched.</param>
        /// <param name="uniqueName">Unique name of the page to be found.</param>
        /// <returns>IDockingElement reference if store page is found; otherwise null.</returns>
        public override IDockingElement FindStorePageElement(DockingLocation location, string uniqueName)
        {
            if (location == DockingLocation.Floating)
            {
                KryptonPage page = FloatspaceControl.PageForUniqueName(uniqueName);
                if (page is KryptonStorePage)
                {
                    return this;
                }
            }

            return null;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the proprogate action used to clear a store page for this implementation.
        /// </summary>
        protected override DockingPropogateAction ClearStoreAction => DockingPropogateAction.ClearFloatingStoredPages;

        /// <summary>
        /// Occurs when a page is added to a cell in the workspace.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A KryptonPageEventArgs containing the event data.</param>
        protected override void OnSpaceCellPageInserting(object sender, KryptonPageEventArgs e)
        {
            // Remove any store page for the unique name of this page being added. In either case of adding a store
            // page or a regular page we want to ensure there does not exist a store page for that same unique name.
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                if (e.Item is KryptonStorePage page)
                {
                    if ((sender is KryptonFloatspace floatspace) && (floatspace.CellForPage(e.Item) != null))
                    {
                        // Prevent this existing store page from being removed due to the Propagate action below. This can
                        // occur because a cell with pages is added in one go and so insert events are generated for the
                        // existing pages inside the cell to ensure that the event is always fired consistently.
                        IgnoreStorePage = page;
                    }
                }

                dockingManager.PropogateAction(ClearStoreAction, new[] { e.Item.UniqueName });
                IgnoreStorePage = null;
            }
        }

        /// <summary>
        /// Raises the type specific space control removed event determinated by the derived class.
        /// </summary>
        protected override void RaiseRemoved()
        {
            // Generate event so the any floatspace customization can be reversed.
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                FloatspaceEventArgs args = new(FloatspaceControl, this);
                dockingManager.RaiseFloatspaceRemoved(args);
            }

            // Generate event so interested parties know this element and associated control have been removed
            Dispose();
        }

        /// <summary>
        /// Raises the type specific cell adding event determinated by the derived class.
        /// </summary>
        /// <param name="cell">Referecence to new cell being added.</param>
        protected override void RaiseCellAdding(KryptonWorkspaceCell cell)
        {
            // Generate event so the floatspace cell customization can be performed.
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                FloatspaceCellEventArgs args = new(FloatspaceControl, this, cell);
                dockingManager.RaiseFloatspaceCellAdding(args);
            }
        }

        /// <summary>
        /// Raises the type specific cell removed event determinated by the derived class.
        /// </summary>
        /// <param name="cell">Referecence to an existing cell being removed.</param>
        protected override void RaiseCellRemoved(KryptonWorkspaceCell cell)
        {
            // Generate event so the floatspace cell customization can be reversed.
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                FloatspaceCellEventArgs args = new(FloatspaceControl, this, cell);
                dockingManager.RaiseFloatspaceCellRemoved(args);
            }
        }

        /// <summary>
        /// Occurs when a page is dropped on the control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PageDropEventArgs containing the event data.</param>
        protected override void RaiseSpacePageDrop(object sender, PageDropEventArgs e)
        {
            // Use event to indicate the page is moving to a workspace and allow it to be cancelled
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                CancelUniqueNameEventArgs args = new(e.Page.UniqueName, false);
                dockingManager.RaisePageFloatingRequest(args);

                // Pass back the result of the event
                e.Cancel = args.Cancel;
            }
        }

        /// <summary>
        /// Gets the xml element name to use when saving.
        /// </summary>
        protected override string XmlElementName => @"DF";

        /// <summary>
        /// Loads docking configuration information using a provider xml reader.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages for adding.</param>
        public override void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
        {
            // Let base class load the pages into the floatspace
            base.LoadElementFromXml(xmlReader, pages);

            // If loading did not create any pages then kill ourself as not needed
            if (FloatspaceControl.PageCount == 0)
            {
                FloatspaceControl.Dispose();
            }
        }
        #endregion

        #region Implementation
        private void OnFloatspacePageCloseClicked(object sender, UniqueNameEventArgs e)
        {
            // Generate event so that the close action is handled for the named page
            KryptonDockingManager dockingManager = DockingManager;
            dockingManager?.CloseRequest(new[]{ e.UniqueName });
        }

        private void OnFloatspacePagesDoubleClicked(object sender, UniqueNamesEventArgs e)
        {
            // If the number of pages to be converted into a separate floating window is less than the
            // total number of visible pages then we allow the change to occur. Otherwise it would cause
            // all pages to be removed into another window which would be pointless.
            if (e.UniqueNames.Count < FloatspaceControl.PageVisibleCount)
            {
                KryptonDockingManager dockingManager = DockingManager;
                dockingManager?.SwitchFloatingToFloatingWindowRequest(e.UniqueNames);
            }
        }

        private void OnFloatspaceDropDownClicked(object sender, CancelDropDownEventArgs e)
        {
            // Generate event so that the appropriate context menu options are preseted and actioned
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                e.Cancel = !dockingManager.ShowPageContextMenuRequest(e.Page, e.KryptonContextMenu);
            }
        }

        private void OnFloatspaceBeforePageDrag(object sender, PageDragCancelEventArgs e)
        {
            // Validate the list of names to those that are still present in the floatspace
            var pages = e.Pages.Where(page => page is not KryptonStorePage && (FloatspaceControl.CellForPage(page) != null)).ToList();

            // Only need to start docking dragging if we have some valid pages
            if (pages.Count != 0)
            {
                KryptonDockingManager dockingManager = DockingManager;
                if (dockingManager != null)
                {
                    // If there is just a single visible cell
                    if (FloatspaceControl.CellVisibleCount == 1)
                    {
                        // And that visible cell has all the pages being dragged
                        KryptonWorkspaceCell cell = FloatspaceControl.FirstVisibleCell();
                        if (cell.Pages.VisibleCount == pages.Count)
                        {
                            // Get the owning floating window element
                            if (GetParentType(typeof(KryptonDockingFloatingWindow)) is KryptonDockingFloatingWindow window)
                            {
                                // Drag the entire floating window
                                dockingManager.DoDragDrop(e.ScreenPoint, e.ElementOffset, e.Control, window);

                                // Always take over docking
                                e.Cancel = true;
                                return;
                            }
                        }
                    }

                    // Add a placeholder for the cell that contains the dragged page, so the cell is not removed during dragging
                    KryptonWorkspaceCell firstCell = FloatspaceControl.CellForPage(e.Pages[0]);
                    firstCell?.Pages.Add(new KryptonStorePage("TemporaryPage", "Floating"));

                    // Ask the docking manager for a IDragPageNotify implementation to handle the dragging operation
                    dockingManager.DoDragDrop(e.ScreenPoint, e.ElementOffset, e.Control, e.Pages);
                }
            }

            // Always take over docking
            e.Cancel = true;
        }
        #endregion
    }
}
