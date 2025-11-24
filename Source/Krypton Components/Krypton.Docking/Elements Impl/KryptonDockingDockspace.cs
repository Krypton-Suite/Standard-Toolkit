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
/// Provides docking functionality within a control edge using a KryptonDockspace.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingDockspace : KryptonDockingSpace
{
    #region Instance Fields
    private int _cacheCellVisibleCount;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the dockspace changes to have one or more visible cells.
    /// </summary>
    public event EventHandler? HasVisibleCells;

    /// <summary>
    /// Occurs when the dockspace changes to no longer have any visible cells.
    /// </summary>
    public event EventHandler? HasNoVisibleCells;
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonDockingDockspace class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="edge">Docking edge this dockspace is against.</param>
    /// <param name="size">Initial size of the dockspace.</param>
    public KryptonDockingDockspace(string name, DockingEdge edge, Size size)
        : base(name, @"Docked")
    {
        // Create a new dockspace that will be a host for docking pages
        var space = new KryptonDockspace
        {
            Size = size,
            Dock = DockingHelper.DockStyleFromDockEdge(edge, false)
        };
        space.CellCountChanged += OnDockspaceCellCountChanged;
        space.CellVisibleCountChanged += OnDockspaceCellVisibleCountChanged;
        space.CellPageInserting += OnSpaceCellPageInserting;
        space.PageCloseClicked += OnDockspacePageCloseClicked;
        space.PageAutoHiddenClicked += OnDockspacePageAutoHiddenClicked;
        space.PagesDoubleClicked += OnDockspacePagesDoubleClicked;
        space.PageDropDownClicked += OnDockspaceDropDownClicked;
        space.BeforePageDrag += OnDockspaceBeforePageDrag;
        SpaceControl = space;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the control this element is managing.
    /// </summary>
    public KryptonDockspace DockspaceControl => (SpaceControl as KryptonDockspace)!;

    /// <summary>
    /// Gets the sibling auto hidden edge.
    /// </summary>
    public KryptonDockingEdgeAutoHidden? EdgeAutoHiddenElement
    {
        get
        {
            // Scan up the parent chain to get the edge we are expected to be inside
            if (GetParentType(typeof(KryptonDockingEdge)) is KryptonDockingEdge dockingEdge)
            {
                // Extract the expected fixed name of the auto hidden edge element
                return dockingEdge[@"AutoHidden"] as KryptonDockingEdgeAutoHidden;
            }

            return null;
        }
    }

    /// <summary>
    /// Propagates an action request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="value">Integer value associated with the request.</param>
    public override void PropogateAction(DockingPropogateAction action, int value)
    {
        switch (action)
        {
            case DockingPropogateAction.RepositionDockspace:
                // Only processes if it applies to us
                if (value == Order)
                {
                    Control? parent = DockspaceControl.Parent;
                    if (parent != null)
                    {
                        // Process all sibling controls starting from end to front of collection
                        var indexInsert = -1;
                        for (var i = parent.Controls.Count - 1; i >= 0; i--)
                        {
                            Control c = parent.Controls[i];

                            // Insert before the last auto hidden panel/slidepanel (this handles the Order=0 case)
                            if ((c is KryptonAutoHiddenPanel or KryptonAutoHiddenSlidePanel))
                            {
                                indexInsert = i;
                            }

                            // Insert before the 'order' found dockspace separator (this handles the Order>0 cases)
                            if (c is KryptonDockspaceSeparator)
                            {
                                if (value == 1)
                                {
                                    indexInsert = i - 1;
                                    break;
                                }

                                value--;
                            }
                        }

                        // Did we manage to find an insertion point
                        if (indexInsert >= 0)
                        {
                            // Our separator should be one before is in the controls collection
                            var ourIndex = parent.Controls.IndexOf(DockspaceControl);
                            if (ourIndex > 0)
                            {
                                Control separator = parent.Controls[ourIndex - 1];
                                parent.Controls.SetChildIndex(separator, indexInsert);
                            }

                            parent.Controls.SetChildIndex(DockspaceControl, indexInsert);
                        }
                    }
                }
                break;
            default:
                base.PropogateAction(action, value);
                break;
        }
    }

    /// <summary>
    /// Propagates an integer state request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="state">Integer state that is requested to be recovered.</param>
    /// <param name="value">Value discovered from matching </param>
    public override void PropogateIntState(DockingPropogateIntState state, ref int value) =>
        // User our value if it is the largest encountered so far
        value = Math.Max(value, Order);

    /// <summary>
    /// Propagates a request for drag targets down the hierarchy of docking elements.
    /// </summary>
    /// <param name="floatingWindow">Reference to window being dragged.</param>
    /// <param name="dragData">Set of pages being dragged.</param>
    /// <param name="targets">Collection of drag targets.</param>
    public override void PropogateDragTargets(KryptonFloatingWindow? floatingWindow,
        PageDragEndData? dragData,
        DragTargetList targets)
    {
        if (DockspaceControl.CellVisibleCount > 0)
        {
            // Create list of the pages that are allowed to be dropped into this dockspace
            var pages = new KryptonPageCollection();
            if (dragData != null)
            {
                foreach (KryptonPage page in dragData.Pages)
                {
                    if (page.AreFlagsSet(KryptonPageFlags.DockingAllowDocked))
                    {
                        pages.Add(page);
                    }
                }
            }

            // Do we have any pages left for dragging?
            if (pages.Count > 0)
            {
                DragTargetList dockspaceTargets = DockspaceControl.GenerateDragTargets(new PageDragEndData(this, pages), KryptonPageFlags.DockingAllowDocked);
                targets.AddRange(dockspaceTargets.ToArray());
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
        KryptonPage? page = DockspaceControl.PageForUniqueName(uniqueName);
        return (page != null)
               && page is not KryptonStorePage
            ? DockingLocation.Docked
            : DockingLocation.None;
    }

    /// <summary>
    /// Find the docking element that contains the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page.</param>
    /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
    public override IDockingElement? FindPageElement(string uniqueName)
    {
        KryptonPage? page = DockspaceControl.PageForUniqueName(uniqueName);
        return (page != null)
               && page is not KryptonStorePage
            ? this
            : null;
    }

    /// <summary>
    /// Find the docking element that contains the location specific store page for the named page.
    /// </summary>
    /// <param name="location">Location to be searched.</param>
    /// <param name="uniqueName">Unique name of the page to be found.</param>
    /// <returns>IDockingElement reference if store page is found; otherwise null.</returns>
    public override IDockingElement? FindStorePageElement(DockingLocation location, string uniqueName)
    {
        if (location == DockingLocation.Docked)
        {
            KryptonPage? page = DockspaceControl.PageForUniqueName(uniqueName);
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
    /// Gets the propagate action used to clear a store page for this implementation.
    /// </summary>
    protected override DockingPropogateAction ClearStoreAction => DockingPropogateAction.ClearDockedStoredPages;

    /// <summary>
    /// Raises the type specific space control removed event determined by the derived class.
    /// </summary>
    protected override void RaiseRemoved()
    {
        // Generate event so that any dockspace customization can be reversed.
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new DockspaceEventArgs(DockspaceControl, this);
            dockingManager.RaiseDockspaceRemoved(args);
        }

        // Generate event so interested parties know this element and associated control have been removed
        Dispose();
    }

    /// <summary>
    /// Raises the type specific cell adding event determined by the derived class.
    /// </summary>
    /// <param name="cell">Reference to new cell being added.</param>
    protected override void RaiseCellAdding(KryptonWorkspaceCell cell)
    {
        // Generate event so the dockspace cell customization can be performed.
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new DockspaceCellEventArgs(DockspaceControl, this, cell);
            dockingManager.RaiseDockspaceCellAdding(args);
        }
    }

    /// <summary>
    /// Raises the type specific cell removed event determined by the derived class.
    /// </summary>
    /// <param name="cell">Reference to an existing cell being removed.</param>
    protected override void RaiseCellRemoved(KryptonWorkspaceCell cell)
    {
        // Generate event so the dockspace cell customization can be reversed.
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new DockspaceCellEventArgs(DockspaceControl, this, cell);
            dockingManager.RaiseDockspaceCellRemoved(args);
        }
    }

    /// <summary>
    /// Occurs when a page is dropped on the control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PageDropEventArgs containing the event data.</param>
    protected override void RaiseSpacePageDrop(object? sender, PageDropEventArgs e)
    {
        // Use event to indicate the page is moving to a workspace and allow it to be cancelled
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new CancelUniqueNameEventArgs(e.Page?.UniqueName!, false);
            dockingManager.RaisePageDockedRequest(args);

            // Pass back the result of the event
            e.Cancel = args.Cancel;
        }
    }

    /// <summary>
    /// Raises the HasVisibleCells event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnHasVisibleCells(EventArgs e) => HasVisibleCells?.Invoke(this, e);

    /// <summary>
    /// Raises the HasNoVisibleCells event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnHasNoVisibleCells(EventArgs e) => HasNoVisibleCells?.Invoke(this, e);

    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DD";

    /// <summary>
    /// Saves docking configuration information using a provider xml writer.
    /// </summary>
    /// <param name="xmlWriter">Xml writer object.</param>
    public override void SaveElementToXml(XmlWriter xmlWriter)
    {
        // Find the ordered position of this dockspace inside the parent control
        Control? parent = DockspaceControl.Parent;
        if (parent != null)
        {
            // Count the number of KryptonDockspace that occur after ourself in the collection by scanning
            // backwards from end of collection to the front.
            var numDockspace = 0;
            for (var i = parent.Controls.Count - 1; i >= 0; i--)
            {
                Control child = parent.Controls[i];
                if (child == DockspaceControl)
                {
                    break;
                }
                else if (child is KryptonDockspace)
                {
                    numDockspace++;
                }
            }

            Order = numDockspace;
        }

        // Let base class save the pages into the dockspace
        base.SaveElementToXml(xmlWriter);
    }

    /// <summary>
    /// Loads docking configuration information using a provider xml reader.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages for adding.</param>
    public override void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
    {
        // Let base class load the pages into the dockspace
        base.LoadElementFromXml(xmlReader, pages);

        // If a size was found during loading then apply it now
        if (!LoadSize.IsEmpty)
        {
            switch (DockspaceControl.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    DockspaceControl.Width = LoadSize.Width;
                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    DockspaceControl.Height = LoadSize.Height;
                    break;
            }
        }

        // Determine the correct visible state of the control
        if (DockspaceControl.CellVisibleCount == 0)
        {
            _cacheCellVisibleCount = 0;
            OnHasNoVisibleCells(EventArgs.Empty);
        }
        else
        {
            _cacheCellVisibleCount = 1;
            OnHasVisibleCells(EventArgs.Empty);
        }

        // If loading did not create any pages then kill ourself as not needed
        if (DockspaceControl.PageCount == 0)
        {
            DockspaceControl.Dispose();
            // TODO: Is this safe ?. It means that whatever uses this afterwards could be accessing null things !
            SpaceControl = null!;
        }
    }
    #endregion

    #region Implementation
    private void OnDockspaceCellVisibleCountChanged(object? sender, EventArgs e)
    {
        if (DockspaceControl.CellVisibleCount == 0)
        {
            if (_cacheCellVisibleCount > 0)
            {
                _cacheCellVisibleCount = 0;
                OnHasNoVisibleCells(EventArgs.Empty);
            }
        }
        else
        {
            if (_cacheCellVisibleCount == 0)
            {
                _cacheCellVisibleCount = 1;
                OnHasVisibleCells(EventArgs.Empty);
            }
        }
    }

    private void OnDockspaceCellCountChanged(object? sender, EventArgs e)
    {
        // When all the cells (and so pages) have been removed we kill ourself
        if (DockspaceControl.CellCount == 0)
        {
            DockspaceControl.Dispose();
        }
    }

    private void OnDockspacePageCloseClicked(object? sender, UniqueNameEventArgs e)
    {
        // Generate event so that the close action is handled for the named page
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.CloseRequest(new[] { e.UniqueName });
    }

    private void OnDockspacePageAutoHiddenClicked(object? sender, UniqueNameEventArgs e)
    {
        // Generate event so that the switch from docked to auto hidden is handled for cell that contains the named page
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.SwitchDockedCellToAutoHiddenGroupRequest(e.UniqueName);
    }

    private void OnDockspacePagesDoubleClicked(object? sender, UniqueNamesEventArgs e)
    {
        // Generate event so that the switch from docked to floating is handled for the provided list of named pages
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.SwitchDockedToFloatingWindowRequest(e.UniqueNames);
    }

    private void OnDockspaceDropDownClicked(object? sender, CancelDropDownEventArgs e)
    {
        // Generate event so that the appropriate context menu options are presented and actioned
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            e.Cancel = !dockingManager.ShowPageContextMenuRequest(e.Page!, e.KryptonContextMenu!);
        }
    }

    private void OnDockspaceBeforePageDrag(object? sender, PageDragCancelEventArgs e)
    {
        // Validate the list of names to those that are still present in the dockspace
        var pages = new List<KryptonPage>();
        foreach (KryptonPage page in e.Pages)
        {
            if (page is not KryptonStorePage && (DockspaceControl.CellForPage(page) != null))
            {
                pages.Add(page);
            }
        }

        // Only need to start docking dragging if we have some valid pages
        if (pages.Count != 0)
        {
            // Ask the docking manager for a IDragPageNotify implementation to handle the dragging operation
            KryptonDockingManager? dockingManager = DockingManager;
            dockingManager?.DoDragDrop(e.ScreenPoint, e.ElementOffset, e.Control, e.Pages);
        }

        // Always take over docking
        e.Cancel = true;
    }
    #endregion
}