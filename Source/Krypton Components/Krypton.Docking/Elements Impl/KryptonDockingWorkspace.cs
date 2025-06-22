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

// ReSharper disable MemberCanBeInternal

// ReSharper disable RedundantNullableFlowAttribute
namespace Krypton.Docking;

/// <summary>
/// Provides docking functionality by attaching to an existing KryptonDockableWorkspace
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingWorkspace : KryptonDockingSpace
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingWorkspace class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    public KryptonDockingWorkspace(string name)
        : this(name, @"Workspace", new KryptonDockableWorkspace())
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonDockingWorkspace class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="storeName">Name to use for storage pages.</param>
    /// <param name="workspace">Reference to workspace to manage.</param>
    public KryptonDockingWorkspace(string name,
        string storeName,
        [DisallowNull] KryptonDockableWorkspace workspace)
        : base(name, storeName)
    {
        SpaceControl = workspace ?? throw new ArgumentNullException(nameof(workspace));

        if (DockableWorkspaceControl != null)
        {
            DockableWorkspaceControl.CellPageInserting += OnSpaceCellPageInserting;
            DockableWorkspaceControl.BeforePageDrag += OnDockableWorkspaceBeforePageDrag;
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the control this element is managing.
    /// </summary>
    public KryptonDockableWorkspace? DockableWorkspaceControl => SpaceControl as KryptonDockableWorkspace;

    /// <summary>
    /// Gets and sets access to the parent docking element.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IDockingElement? Parent
    {
        set
        {
            // Let base class perform standard processing
            base.Parent = value;

            // Generate event so that any dockable workspace customization can be performed.
            KryptonDockingManager? dockingManager = DockingManager;
            if (dockingManager != null)
            {
                var args = new DockableWorkspaceEventArgs(DockableWorkspaceControl, this);
                dockingManager.RaiseDockableWorkspaceAdded(args);
            }
        }
    }

    /// <summary>
    /// Show all display elements of the provided page.
    /// </summary>
    /// <param name="page">Reference to page that should be shown.</param>
    public void ShowPage([DisallowNull] KryptonPage page)
    {
        // Cannot show a null reference
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        ShowPages(new[] { page.UniqueName });
    }

    /// <summary>
    /// Show all display elements of the provided page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page that should be shown.</param>
    public void ShowPage([DisallowNull] string uniqueName)
    {
        // Cannot show a null reference
        if (uniqueName == null)
        {
            throw new ArgumentNullException(nameof(uniqueName));
        }

        ShowPages(new[] { uniqueName });
    }

    /// <summary>
    /// Show all display elements of the provided pages.
    /// </summary>
    /// <param name="pages">Array of references to pages that should be shown.</param>
    public void ShowPages([DisallowNull] KryptonPage[] pages)
    {
        // Cannot show a null reference
        if (pages == null)
        {
            throw new ArgumentNullException(nameof(pages));
        }

        if (pages.Length > 0)
        {
            var uniqueNames = new string[pages.Length];
            for (var i = 0; i < uniqueNames.Length; i++)
            {
                // Cannot show a null page reference
                if (pages[i] == null)
                {
                    throw new ArgumentException(@"pages array contains a null page reference", nameof(pages));
                }

                uniqueNames[i] = pages[i].UniqueName;
            }

            ShowPages(uniqueNames);
        }
    }

    /// <summary>
    /// Show all display elements of the provided pages.
    /// </summary>
    /// <param name="uniqueNames">Array of unique names of the pages that should be shown.</param>
    public void ShowPages([DisallowNull] string[] uniqueNames)
    {
        // Cannot show a null reference
        if (uniqueNames == null)
        {
            throw new ArgumentNullException(nameof(uniqueNames));
        }

        if (uniqueNames.Length > 0)
        {
            // Cannot show a null or zero length unique name
            foreach (var uniqueName in uniqueNames)
            {
                if (uniqueName == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames), @"uniqueNames array contains a null string reference");
                }

                if (uniqueName.Length == 0)
                {
                    throw new ArgumentException(@"uniqueNames array contains a zero length string", nameof(uniqueNames));
                }
            }

            using var update = new DockingMultiUpdate(this);
            base.PropogateAction(DockingPropogateAction.ShowPages, uniqueNames);
        }
    }

    /// <summary>
    /// Show all display elements of all pages.
    /// </summary>
    public void ShowAllPages()
    {
        using var update = new DockingMultiUpdate(this);
        base.PropogateAction(DockingPropogateAction.ShowAllPages, null as string[]);
    }

    /// <summary>
    /// Hide all display elements of the provided page.
    /// </summary>
    /// <param name="page">Reference to page that should be hidden.</param>
    public void HidePage([DisallowNull] KryptonPage page)
    {
        // Cannot hide a null reference
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        HidePages(new[] { page.UniqueName });
    }

    /// <summary>
    /// Hide all display elements of the provided page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page that should be hidden.</param>
    public void HidePage([DisallowNull] string uniqueName)
    {
        // Cannot hide a null reference
        if (uniqueName == null)
        {
            throw new ArgumentNullException(nameof(uniqueName));
        }

        if (uniqueName.Length > 0)
        {
            HidePages(new[] { uniqueName });
        }
    }

    /// <summary>
    /// Hide all display elements of the provided pages.
    /// </summary>
    /// <param name="pages">Array of references to pages that should be hidden.</param>
    public void HidePages([DisallowNull] KryptonPage[] pages)
    {
        // Cannot hide a null reference
        if (pages == null)
        {
            throw new ArgumentNullException(nameof(pages));
        }

        if (pages.Length > 0)
        {
            // Cannot hide a null page reference
            var uniqueNames = new string[pages.Length];
            for (var i = 0; i < uniqueNames.Length; i++)
            {
                // Cannot show a null page reference
                if (pages[i] == null)
                {
                    throw new ArgumentException(@"pages array contains a null page reference", nameof(pages));
                }

                uniqueNames[i] = pages[i].UniqueName;
            }

            HidePages(uniqueNames);
        }
    }

    /// <summary>
    /// Hide all display elements of the provided pages.
    /// </summary>
    /// <param name="uniqueNames">Array of unique names of the pages that should be hidden.</param>
    public void HidePages([DisallowNull] string[] uniqueNames)
    {
        // Cannot hide a null reference
        if (uniqueNames == null)
        {
            throw new ArgumentNullException(nameof(uniqueNames));
        }

        if (uniqueNames.Length > 0)
        {
            // Cannot hide a null or zero length unique name
            foreach (var uniqueName in uniqueNames)
            {
                if (uniqueName == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames), @"uniqueNames array contains a null string reference");
                }

                if (uniqueName.Length == 0)
                {
                    throw new ArgumentException(@"uniqueNames array contains a zero length string", nameof(uniqueNames));
                }
            }

            using var update = new DockingMultiUpdate(this);
            base.PropogateAction(DockingPropogateAction.HidePages, uniqueNames);
        }
    }

    /// <summary>
    /// Hide all display elements of all pages.
    /// </summary>
    public void HideAllPages()
    {
        using var update = new DockingMultiUpdate(this);
        base.PropogateAction(DockingPropogateAction.HideAllPages, null as string[]);
    }

    /// <summary>
    /// Remove the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page that should be removed.</param>
    /// <param name="disposePage">Should the page be disposed when removed.</param>
    public void RemovePage([DisallowNull] string uniqueName, bool disposePage)
    {
        // Cannot remove a null reference
        if (uniqueName == null)
        {
            throw new ArgumentNullException(nameof(uniqueName));
        }

        // Unique names cannot be zero length
        if (uniqueName.Length == 0)
        {
            throw new ArgumentException(@"uniqueName cannot be zero length", nameof(uniqueName));
        }

        RemovePages(new[] { uniqueName }, disposePage);
    }

    /// <summary>
    /// Remove the referenced pages.
    /// </summary>
    /// <param name="pages">Array of references to pages that should be removed.</param>
    /// <param name="disposePage">Should the page be disposed when removed.</param>
    public void RemovePages([DisallowNull] KryptonPage[] pages, bool disposePage)
    {
        // Cannot remove a null reference
        if (pages == null)
        {
            throw new ArgumentNullException(nameof(pages));
        }

        if (pages.Length > 0)
        {
            // Cannot remove a null page reference
            var uniqueNames = new string[pages.Length];
            for (var i = 0; i < uniqueNames.Length; i++)
            {
                // Cannot show a null page reference
                if (pages[i] == null)
                {
                    throw new ArgumentException(@"pages array contains a null page reference", nameof(pages));
                }

                uniqueNames[i] = pages[i].UniqueName;
            }

            RemovePages(uniqueNames, disposePage);
        }
    }

    /// <summary>
    /// Remove the named pages.
    /// </summary>
    /// <param name="uniqueNames">Array of unique names of the pages that should be removed.</param>
    /// <param name="disposePage">Should the page be disposed when removed.</param>
    public void RemovePages([DisallowNull] string[] uniqueNames, bool disposePage)
    {
        // Cannot remove a null reference
        if (uniqueNames == null)
        {
            throw new ArgumentNullException(nameof(uniqueNames));
        }

        if (uniqueNames.Length > 0)
        {
            // Cannot remove a null or zero length unique name
            foreach (var uniqueName in uniqueNames)
            {
                if (uniqueName == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames), @"uniqueNames array contains a null string reference");
                }

                if (uniqueName.Length == 0)
                {
                    throw new ArgumentException(@"uniqueNames array contains a zero length string", nameof(uniqueNames));
                }
            }

            // Remove page details from all parts of the hierarchy
            using var update = new DockingMultiUpdate(this);
            base.PropogateAction(disposePage ? DockingPropogateAction.RemoveAndDisposePages : DockingPropogateAction.RemovePages, uniqueNames);
        }
    }

    /// <summary>
    /// Remove all pages.
    /// </summary>
    /// <param name="disposePage">Should the page be disposed when removed.</param>
    public void RemoveAllPages(bool disposePage)
    {
        // Remove all details about all pages from all parts of the hierarchy
        using var update = new DockingMultiUpdate(this);
        base.PropogateAction(disposePage ? DockingPropogateAction.RemoveAndDisposeAllPages : DockingPropogateAction.RemoveAllPages, null as string[]);
    }

    /// <summary>
    /// Propagates an action request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
    public override void PropogateAction(DockingPropogateAction action, string[]? uniqueNames)
    {
        switch (action)
        {
            case DockingPropogateAction.ShowAllPages:
            case DockingPropogateAction.HideAllPages:
            case DockingPropogateAction.RemoveAllPages:
            case DockingPropogateAction.RemoveAndDisposeAllPages:
                // Ignore some global actions
                break;
            default:
                base.PropogateAction(action, uniqueNames);
                break;
        }
    }

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
        // Create list of the pages that are allowed to be dropped into this workspace
        var pages = new KryptonPageCollection();
        if (dragData != null)
        {
            foreach (KryptonPage page in dragData.Pages.Where(static page =>
                         page.AreFlagsSet(KryptonPageFlags.DockingAllowWorkspace)))
            {
                pages.Add(page);
            }
        }

        // Do we have any pages left for dragging?
        if (pages.Count > 0)
        {
            if (DockableWorkspaceControl != null)
            {
                DragTargetList workspaceTargets = DockableWorkspaceControl.GenerateDragTargets(new PageDragEndData(this, pages), KryptonPageFlags.DockingAllowWorkspace);
                targets.AddRange(workspaceTargets.ToArray());
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
        KryptonPage? page = DockableWorkspaceControl?.PageForUniqueName(uniqueName);
        return (page != null)
               && page is not KryptonStorePage
            ? DockingLocation.Workspace
            : DockingLocation.None;
    }

    /// <summary>
    /// Find the docking element that contains the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page.</param>
    /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
    public override IDockingElement? FindPageElement(string uniqueName)
    {
        KryptonPage? page = DockableWorkspaceControl?.PageForUniqueName(uniqueName);
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
        if (location == DockingLocation.Workspace)
        {
            KryptonPage? page = DockableWorkspaceControl?.PageForUniqueName(uniqueName);
            if (page is KryptonStorePage)
            {
                return this;
            }
        }

        return null;
    }

    /// <summary>
    /// Find a workspace element by searching the hierarchy.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable workspace element is required.</param>
    /// <returns>KryptonDockingWorkspace reference if found; otherwise false.</returns>
    public override KryptonDockingWorkspace FindDockingWorkspace(string uniqueName) => this;

    #endregion

    #region Protected
    /// <summary>
    /// Gets the propagate action used to clear a store page for this implementation.
    /// </summary>
    protected override DockingPropogateAction ClearStoreAction => DockingPropogateAction.ClearFillerStoredPages;

    /// <summary>
    /// Raises the type specific space control removed event determined by the derived class.
    /// </summary>
    protected override void RaiseRemoved()
    {
        // Generate event so that any dockable workspace customization can be reversed.
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new DockableWorkspaceEventArgs(DockableWorkspaceControl, this);
            dockingManager.RaiseDockableWorkspaceRemoved(args);
        }
    }

    /// <summary>
    /// Raises the type specific cell adding event determined by the derived class.
    /// </summary>
    /// <param name="cell">Reference to new cell being added.</param>
    protected override void RaiseCellAdding(KryptonWorkspaceCell cell)
    {
        // Generate event so the dockable workspace cell customization can be performed.
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            if (DockableWorkspaceControl != null)
            {
                var args = new DockableWorkspaceCellEventArgs(DockableWorkspaceControl, this, cell);
                dockingManager.RaiseDockableWorkspaceCellAdding(args);
            }
        }
    }

    /// <summary>
    /// Raises the type specific cell removed event determined by the derived class.
    /// </summary>
    /// <param name="cell">Reference to an existing cell being removed.</param>
    protected override void RaiseCellRemoved(KryptonWorkspaceCell cell)
    {
        // Generate event so the dockable workspace cell customization can be reversed.
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            if (DockableWorkspaceControl != null)
            {
                var args = new DockableWorkspaceCellEventArgs(DockableWorkspaceControl, this, cell);
                dockingManager.RaiseDockableWorkspaceCellRemoved(args);
            }
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
            if (e.Page != null)
            {
                var args = new CancelUniqueNameEventArgs(e.Page.UniqueName, false);
                dockingManager.RaisePageWorkspaceRequest(args);

                // Pass back the result of the event
                e.Cancel = args.Cancel;
            }
        }
    }

    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DW";

    #endregion

    #region Implementation
    private void OnDockableWorkspaceBeforePageDrag(object? sender, PageDragCancelEventArgs e)
    {
        // Validate the list of names to those that are still present in the dockspace
        var pages = new List<KryptonPage>();
        foreach (KryptonPage page in e.Pages)
        {
            if (page is not KryptonStorePage
                && (DockableWorkspaceControl?.CellForPage(page) != null))
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