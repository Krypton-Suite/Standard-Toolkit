#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Docking element that represents a tab group of auto-hidden pages on one edge, wrapping pages in proxy instances for display.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingAutoHiddenGroup : DockingElementClosedCollection
{
    #region Instance Fields

    private int _cacheCellVisibleCount;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the user clicks a page tab and requests that page be shown in the slide-out panel.
    /// </summary>
    public event EventHandler<KryptonPageEventArgs>? PageClicked;

    /// <summary>
    /// Occurs when the user moves the mouse over a page tab in the group.
    /// </summary>
    public event EventHandler<KryptonPageEventArgs>? PageHoverStart;

    /// <summary>
    /// Occurs when the mouse leaves a page tab after a hover.
    /// </summary>
    public event EventHandler<EventArgs>? PageHoverEnd;
    #endregion

    #region Identity
    /// <summary>
    /// Creates a <see cref="KryptonAutoHiddenGroup"/> tab control for <paramref name="edge"/> and wires page interaction events.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="edge">Edge where the auto-hidden group tabs are displayed.</param>
    public KryptonDockingAutoHiddenGroup(string name, DockingEdge edge)
        : base(name)
    {
        Edge = edge;

        // Create a control that will draw tabs for auto hidden pages
        AutoHiddenGroupControl = new KryptonAutoHiddenGroup(edge);
        AutoHiddenGroupControl.StoringPage += OnAutoHiddenGroupStoringPage;
        AutoHiddenGroupControl.TabClicked += OnAutoHiddenGroupTabClicked;
        AutoHiddenGroupControl.TabMouseHoverStart += OnAutoHiddenGroupHoverStart;
        AutoHiddenGroupControl.TabMouseHoverEnd += OnAutoHiddenGroupHoverEnd;
        AutoHiddenGroupControl.TabVisibleCountChanged += OnAutoHiddenGroupTabVisibleCountChanged;
        AutoHiddenGroupControl.Disposed += OnAutoHiddenGroupDisposed;
    }
    #endregion

    #region Public
    /// <summary>
    /// Edge where this group's tabs are displayed along the parent control.
    /// </summary>
    public DockingEdge Edge { get; }

    /// <summary>
    /// Tab control that displays auto-hidden page tabs for this group.
    /// </summary>
    public KryptonAutoHiddenGroup AutoHiddenGroupControl { get; }

    /// <summary>
    /// Sibling docked-edge element under the same parent <see cref="KryptonDockingEdge"/>, located by the fixed name <c>Docked</c>.
    /// </summary>
    /// <returns>The docked-edge element when the parent chain includes a <see cref="KryptonDockingEdge"/>; otherwise <see langword="null"/>.</returns>
    public KryptonDockingEdgeDocked? EdgeDockedElement
    {
        get
        {
            // Scan up the parent chain to get the edge we are expected to be inside
            if (GetParentType(typeof(KryptonDockingEdge)) is KryptonDockingEdge dockingEdge)
            {
                // Extract the expected fixed name of the docked edge element
                return dockingEdge["Docked"] as KryptonDockingEdgeDocked;
            }

            return null;
        }
    }

    /// <summary>
    /// Adds a page to the end of this auto-hidden group, wrapping non-store pages in a proxy.
    /// </summary>
    /// <param name="page">Page to add.</param>
    /// <exception cref="ArgumentOutOfRangeException">The page is already present elsewhere in the docking hierarchy.</exception>
    /// <exception cref="ApplicationException">No docking manager is attached to this subtree.</exception>
    public void Append(KryptonPage page) =>
        // Use existing array adding method to prevent duplication of code
        Append(new[] { page });

    /// <summary>
    /// Adds pages to the end of this auto-hidden group, wrapping non-store pages in proxies and clearing conflicting store placeholders first.
    /// </summary>
    /// <param name="pages">Pages to add; <see langword="null"/> is ignored.</param>
    /// <exception cref="ArgumentOutOfRangeException">A page is already present elsewhere in the docking hierarchy.</exception>
    /// <exception cref="ApplicationException">No docking manager is attached to this subtree.</exception>
    public void Append(KryptonPage[]? pages)
    {
        // Demand that pages are not already present
        DemandPagesNotBePresent(pages);

        if (pages != null)
        {
            AppendPagesToControl(pages);
        }
    }

    /// <summary>
    /// Applies page visibility, removal, store, and loading actions to this group's tab control before forwarding to child elements.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="uniqueNames">Page unique names targeted by the action; <see langword="null"/> for actions that apply to all pages.</param>
    public override void PropogateAction(DockingPropogateAction action, string[]? uniqueNames)
    {
        switch (action)
        {
            case DockingPropogateAction.ShowPages:
            case DockingPropogateAction.HidePages:
                if (uniqueNames != null)
                {
                    var newVisible = (action == DockingPropogateAction.ShowPages);
                    // Update visible state of pages that are not placeholders
                    foreach (KryptonPage? page in uniqueNames
                                 .Select(uniqueName => AutoHiddenGroupControl.Pages[uniqueName])
                                 .Where(static page => (page is not null and not KryptonStorePage))
                            )
                    {
                        page!.Visible = newVisible;
                    }
                }
                break;
            case DockingPropogateAction.ShowAllPages:
                AutoHiddenGroupControl.ShowAllPages(typeof(KryptonStorePage));
                break;
            case DockingPropogateAction.HideAllPages:
                AutoHiddenGroupControl.HideAllPages(typeof(KryptonStorePage));
                break;
            case DockingPropogateAction.RemovePages:
            case DockingPropogateAction.RemoveAndDisposePages:
                // Only remove the actual page and not placeholders
                if (uniqueNames != null)
                {
                    foreach (KryptonPage? page in uniqueNames
                                 .Select(uniqueName => AutoHiddenGroupControl.Pages[uniqueName])
                                 .Where(static page => (page is not null and not KryptonStorePage))
                            )
                    {
                        AutoHiddenGroupControl.Pages.Remove(page!);

                        if (action == DockingPropogateAction.RemoveAndDisposePages)
                        {
                            page!.Dispose();
                        }
                    }
                }

                break;
            case DockingPropogateAction.Loading:
                // Remove all pages including store pages
                AutoHiddenGroupControl.Pages.Clear();
                break;
            case DockingPropogateAction.RemoveAllPages:
            case DockingPropogateAction.RemoveAndDisposeAllPages:
                for (var i = AutoHiddenGroupControl.Pages.Count - 1; i >= 0; i--)
                {
                    // Only remove the actual page and not placeholders
                    KryptonPage? page = AutoHiddenGroupControl.Pages[i];
                    if ((page is not null and not KryptonStorePage))
                    {
                        AutoHiddenGroupControl.Pages.RemoveAt(i);

                        if (action == DockingPropogateAction.RemoveAndDisposeAllPages)
                        {
                            page.Dispose();
                        }
                    }
                }
                break;
            case DockingPropogateAction.StorePages:
                AutoHiddenGroupControl.StorePages(uniqueNames);
                break;
            case DockingPropogateAction.StoreAllPages:
                AutoHiddenGroupControl.StoreAllPages();
                break;
            case DockingPropogateAction.ClearAutoHiddenStoredPages:
            case DockingPropogateAction.ClearStoredPages:
                if (uniqueNames != null)
                {
                    foreach (var uniqueName in uniqueNames)
                    {
                        // Only remove a matching placeholder page
                        KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
                        if (page is KryptonStorePage)
                        {
                            AutoHiddenGroupControl.Pages.Remove(page);
                        }
                    }
                }

                break;
            case DockingPropogateAction.ClearAllStoredPages:
                for (var i = AutoHiddenGroupControl.Pages.Count - 1; i >= 0; i--)
                {
                    // Only remove a placeholder paged
                    KryptonPage page = AutoHiddenGroupControl.Pages[i];
                    if (page is KryptonStorePage)
                    {
                        AutoHiddenGroupControl.Pages.RemoveAt(i);
                    }
                }
                break;
        }

        // Let base class perform standard processing
        base.PropogateAction(action, uniqueNames);
    }

    /// <summary>
    /// Restores pages from store placeholders in this group's tab control before forwarding to child elements.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="pages">Pages targeted by the action.</param>
    public override void PropogateAction(DockingPropogateAction action, KryptonPage[] pages)
    {
        switch (action)
        {
            case DockingPropogateAction.RestorePages:
                AutoHiddenGroupControl.RestorePages(pages);
                break;
        }

        // Let base class perform standard processing
        base.PropogateAction(action, pages);
    }

    /// <summary>
    /// Answers page-presence, store-placeholder, and visibility queries for pages held by this group's tab control.
    /// </summary>
    /// <param name="state">Boolean query to resolve.</param>
    /// <param name="uniqueName">Unique name of the page the query concerns.</param>
    /// <returns><see langword="true"/> or <see langword="false"/> when this group can answer; otherwise delegates to the base implementation.</returns>
    public override bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName)
    {
        switch (state)
        {
            case DockingPropogateBoolState.ContainsPage:
            {
                // Return definitive answer 'true' if the group controls contains the named page (but not for a placeholder)
                KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
                if ((page != null) && page is not KryptonStorePage)
                {
                    return true;
                }
            }
                break;
            case DockingPropogateBoolState.ContainsStorePage:
            {
                // Return definitive answer 'true' if the group controls contains a store page for the unique name.
                KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
                if (page is KryptonStorePage)
                {
                    return true;
                }
            }
                break;
            case DockingPropogateBoolState.IsPageShowing:
            {
                // If requested page exists then return the visible state of the page (but not for a placeholder)
                KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
                if ((page != null) && page is not KryptonStorePage)
                {
                    return page.LastVisibleSet;
                }
            }
                break;
        }

        // Let base class perform standard processing
        return base.PropogateBoolState(state, uniqueName);
    }

    /// <summary>
    /// Returns the underlying page for a proxy entry in this group's tab control when asked for a page by unique name.
    /// </summary>
    /// <param name="state">Page query to resolve.</param>
    /// <param name="uniqueName">Unique name of the page the query concerns.</param>
    /// <returns>The unwrapped page when a matching proxy exists; otherwise delegates to the base implementation.</returns>
    public override KryptonPage? PropogatePageState(DockingPropogatePageState state, string uniqueName)
    {
        if (state == DockingPropogatePageState.PageForUniqueName)
        {
            // If we have the page (stored via a proxy) then return the actual page reference (but not for a placeholder)
            KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
            if (page is KryptonAutoHiddenProxyPage proxyPage)
            {
                return proxyPage.Page;
            }
        }

        // Let base class perform standard processing
        return base.PropogatePageState(state, uniqueName);
    }

    /// <summary>
    /// Adds real (non-placeholder) auto-hidden pages from this group into <paramref name="pages"/> when the query requests all or auto-hidden pages.
    /// </summary>
    /// <param name="state">Page-list query to resolve.</param>
    /// <param name="pages">Collection to receive matching pages.</param>
    public override void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages)
    {
        switch (state)
        {
            case DockingPropogatePageList.All:
            case DockingPropogatePageList.AutoHidden:
                for (var i = AutoHiddenGroupControl.Pages.Count - 1; i >= 0; i--)
                {
                    // Only add real pages and not just placeholders
                    KryptonPage? page = AutoHiddenGroupControl.Pages[i];
                    if (page is not null and not KryptonStorePage)
                    {
                        // Remember the real page is inside a proxy!
                        var proxyPage = page as KryptonAutoHiddenProxyPage;
                        if (proxyPage?.Page != null)
                        {
                            pages.Add(proxyPage.Page);
                        }
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Returns <see cref="DockingLocation.AutoHidden"/> when this group contains a non-placeholder page with <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns><see cref="DockingLocation.AutoHidden"/> when the page is present; otherwise <see cref="DockingLocation.None"/>.</returns>
    public override DockingLocation FindPageLocation(string uniqueName)
    {
        KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
        return (page != null) && page is not KryptonStorePage 
            ? DockingLocation.AutoHidden 
            : DockingLocation.None;
    }

    /// <summary>
    /// Returns this element when it contains a non-placeholder page with <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns>This instance when the page is present; otherwise <see langword="null"/>.</returns>
    public override IDockingElement? FindPageElement(string uniqueName)
    {
        KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
        return (page != null) && page is not KryptonStorePage 
            ? this 
            : null;
    }

    /// <summary>
    /// Returns this element when it holds a store placeholder for <paramref name="uniqueName"/> at <see cref="DockingLocation.AutoHidden"/>.
    /// </summary>
    /// <param name="location">Docking location to search.</param>
    /// <param name="uniqueName">Unique name of the stored page.</param>
    /// <returns>This instance when a matching store page exists; otherwise <see langword="null"/>.</returns>
    public override IDockingElement? FindStorePageElement(DockingLocation location, string uniqueName)
    {
        if (location == DockingLocation.AutoHidden)
        {
            KryptonPage? page = AutoHiddenGroupControl.Pages[uniqueName];
            if (page is KryptonStorePage)
            {
                return this;
            }
        }

        return null;
    }

    /// <summary>
    /// Collects visible, non-placeholder pages from this group, returning the underlying pages unwrapped from their proxies.
    /// </summary>
    /// <returns>Array of visible page references; empty when no visible pages exist.</returns>
    public KryptonPage[] VisiblePages()
    {
        var pages = new List<KryptonPage>();

        // Only interested in visible pages that are not placeholders
        foreach (KryptonPage page in AutoHiddenGroupControl.Pages)
        {
            if ((page is KryptonAutoHiddenProxyPage proxyPage) && page.LastVisibleSet)
            {
                // Add the actual page this proxy wraps
                if (proxyPage.Page != null)
                {
                    pages.Add(proxyPage.Page);
                }
            }
        }

        return pages.ToArray();
    }

    /// <summary>
    /// Writes this group's pages and saveable attributes to <paramref name="xmlWriter"/>, raising page-saving events for custom data.
    /// </summary>
    /// <param name="xmlWriter">Xml writer receiving the serialized layout.</param>
    public override void SaveElementToXml(XmlWriter xmlWriter)
    {
        KryptonDockingManager? manager = DockingManager;

        // Output docking manager element
        xmlWriter.WriteStartElement(XmlElementName);
        xmlWriter.WriteAttributeString(@"N", Name);
        xmlWriter.WriteAttributeString(@"C", AutoHiddenGroupControl.Pages.Count(static page => page.AreFlagsSet(KryptonPageFlags.AllowConfigSave)).ToString());

        // Output an element per page                 // Are we allowed to save the page?
        foreach (KryptonPage page in AutoHiddenGroupControl.Pages.Where(static page => page.AreFlagsSet(KryptonPageFlags.AllowConfigSave)))
        {
            xmlWriter.WriteStartElement("KP");
            xmlWriter.WriteAttributeString(@"UN", page.UniqueName);
            xmlWriter.WriteAttributeString(@"V", CommonHelper.BoolToString(page.LastVisibleSet)!);
            xmlWriter.WriteAttributeString(@"S", CommonHelper.BoolToString(page is KryptonStorePage)!);

            // Give event handlers a chance to save custom data with the page
            xmlWriter.WriteStartElement("CPD");
            var args = new DockPageSavingEventArgs(manager, xmlWriter, page);
            manager?.RaisePageSaving(args);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteFullEndElement();
        }

        // Terminate the workspace element
        xmlWriter.WriteFullEndElement();
    }

    /// <summary>
    /// Loads pages from XML into this group, updates tab visibility, and disposes the group when loading produces no pages.
    /// </summary>
    /// <param name="xmlReader">Xml reader positioned at this element.</param>
    /// <param name="pages">Collection of available pages for resolving unique names.</param>
    public override void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
    {
        // Let base class load the pages into the group
        base.LoadElementFromXml(xmlReader, pages);

        // Determine the correct visible state of the control
        if (AutoHiddenGroupControl.Pages.VisibleCount == 0)
        {
            _cacheCellVisibleCount = 0;
            AutoHiddenGroupControl.Visible = false;
        }
        else
        {
            _cacheCellVisibleCount = 1;
            AutoHiddenGroupControl.Visible = true;
        }

        // If loading did not create any pages then kill ourself as not needed
        if (AutoHiddenGroupControl.Pages.Count == 0)
        {
            AutoHiddenGroupControl.Dispose();
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PageClicked event.
    /// </summary>
    /// <param name="e">An KryptonPageEventArgs containing the event data.</param>
    protected virtual void OnPageClicked(KryptonPageEventArgs e) => PageClicked?.Invoke(this, e);

    /// <summary>
    /// Raises the PageHoverStart event.
    /// </summary>
    /// <param name="e">An KryptonPageEventArgs containing the event data.</param>
    protected virtual void OnPageHoverStart(KryptonPageEventArgs e) => PageHoverStart?.Invoke(this, e);

    /// <summary>
    /// Raises the PageHoverEnd event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnPageHoverEnd(EventArgs e) => PageHoverEnd?.Invoke(this, e);

    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DAHG";

    /// <summary>
    /// Perform docking element specific actions for loading a child xml.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    /// <param name="child">Optional reference to existing child docking element.</param>
    protected override void LoadChildDockingElement(XmlReader xmlReader,
        KryptonPageCollection pages,
        IDockingElement? child)
    {
        KryptonDockingManager? manager = DockingManager;

        // Is it the expected xml element name?
        if (xmlReader.Name != @"KP")
        {
            throw new ArgumentException($@"Element name 'KP' was expected but found '{xmlReader.Name}' instead.", nameof(xmlReader));
        }

        // Get the unique name of the page
        var uniqueName = xmlReader.GetAttribute(@"UN") ?? string.Empty;
        var boolStore = xmlReader.GetAttribute(@"S") ?? string.Empty;
        var boolVisible = xmlReader.GetAttribute(@"V") ?? string.Empty;

        KryptonPage? page;

        // If the entry is for just a placeholder...
        if (CommonHelper.StringToBool(boolStore))
        {
            // Recreate the requested store page and append
            page = new KryptonStorePage(uniqueName, @"AutoHiddenGroup");
            AutoHiddenGroupControl.Pages.Add(page);
        }
        else
        {
            // Can we find a provided page to match the incoming layout?
            page = pages[uniqueName];
            if (page == null)
            {
                // Generate event so developer can create and supply the page now
                var args = new RecreateLoadingPageEventArgs(uniqueName);
                manager?.RaiseRecreateLoadingPage(args);
                if (!args.Cancel)
                {
                    page = args.Page;

                    // Add recreated page to the looking dictionary
                    if ((page != null) && (pages[page.UniqueName] == null))
                    {
                        pages.Add(page);
                    }
                }
            }

            if (page != null)
            {
                // Use the loaded visible state
                page.Visible = CommonHelper.StringToBool(boolVisible);

                // Create a proxy around the page and append it
                var proxyPage = new KryptonAutoHiddenProxyPage(page);
                AutoHiddenGroupControl.Pages.Add(proxyPage);
            }
        }

        if (!xmlReader.Read())
        {
            throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
        }

        if (xmlReader.Name != @"CPD")
        {
            throw new ArgumentException(@"Expected 'CPD' element was not found", nameof(xmlReader));
        }

        var finished = xmlReader.IsEmptyElement;

        // Generate event so custom data can be loaded and/or the page to be added can be modified
        var pageLoading = new DockPageLoadingEventArgs(manager, xmlReader, page);
        manager?.RaisePageLoading(pageLoading);

        // Read everything until we get the end of custom data marker
        while (!finished)
        {
            // Check it has the expected name
            if (xmlReader.NodeType == XmlNodeType.EndElement)
            {
                finished = (xmlReader.Name == @"CPD");
            }

            if (!finished)
            {
                if (!xmlReader.Read())
                {
                    throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
                }
            }
        }

        if (!xmlReader.Read())
        {
            throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
        }
    }
    #endregion

    #region Implementation
    private void AppendPagesToControl(KryptonPage[] pages)
    {
        // Make a list of all the 'store' pages being added
        var uniqueNames = pages.OfType<KryptonStorePage>().Select(static page => page.UniqueName).ToList();

        // We only allow a single 'store' page in this docking location at a time
        if (uniqueNames.Count > 0)
        {
            DockingManager?.PropogateAction(DockingPropogateAction.ClearAutoHiddenStoredPages, uniqueNames.ToArray());
        }

        // Non-store pages need to be wrapped in a proxy appropriate for the auto hidden control
        for (var i = 0; i < pages.Length; i++)
        {
            if (pages[i] is not KryptonStorePage)
            {
                pages[i] = new KryptonAutoHiddenProxyPage(pages[i]);
            }
        }

        // Add the proxy pages so that we can still use the actual pages instances elsewhere
        AutoHiddenGroupControl.Pages.AddRange(pages);
    }

    private void OnAutoHiddenGroupStoringPage(object? sender, UniqueNameEventArgs e) =>
        // We only allow a single 'store' page in this docking location at a time
        DockingManager?.PropogateAction(DockingPropogateAction.ClearAutoHiddenStoredPages, new[] { e.UniqueName });

    private void OnAutoHiddenGroupTabClicked(object? sender, KryptonPageEventArgs e)
    {
        // The auto hidden group contains proxy pages and not the real pages
        if (e.Item is KryptonAutoHiddenProxyPage proxyPage)
        {
            OnPageClicked(new KryptonPageEventArgs(proxyPage.Page, e.Index));
        }
    }

    private void OnAutoHiddenGroupHoverStart(object? sender, KryptonPageEventArgs e)
    {
        // The auto hidden group contains proxy pages and not the real pages
        if (e.Item is KryptonAutoHiddenProxyPage proxyPage)
        {
            OnPageHoverStart(new KryptonPageEventArgs(proxyPage.Page, e.Index));
        }
    }

    private void OnAutoHiddenGroupHoverEnd(object? sender, EventArgs e) => OnPageHoverEnd(e);

    private void OnAutoHiddenGroupTabVisibleCountChanged(object? sender, EventArgs e)
    {
        if (AutoHiddenGroupControl.Pages.VisibleCount == 0)
        {
            if (_cacheCellVisibleCount > 0)
            {
                _cacheCellVisibleCount = 0;
                AutoHiddenGroupControl.Visible = false;
            }
        }
        else
        {
            if (_cacheCellVisibleCount == 0)
            {
                _cacheCellVisibleCount = 1;
                AutoHiddenGroupControl.Visible = true;
            }
        }
    }
    private void OnAutoHiddenGroupDisposed(object? sender, EventArgs e)
    {
        // Unhook from events so the control can be garbage collected
        AutoHiddenGroupControl.StoringPage -= OnAutoHiddenGroupStoringPage;
        AutoHiddenGroupControl.TabClicked -= OnAutoHiddenGroupTabClicked;
        AutoHiddenGroupControl.TabMouseHoverStart -= OnAutoHiddenGroupHoverStart;
        AutoHiddenGroupControl.TabMouseHoverEnd -= OnAutoHiddenGroupHoverEnd;
        AutoHiddenGroupControl.TabVisibleCountChanged -= OnAutoHiddenGroupTabVisibleCountChanged;
        AutoHiddenGroupControl.Disposed -= OnAutoHiddenGroupDisposed;

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Allow the auto hidden group to be customized by event handlers
            var groupArgs = new AutoHiddenGroupEventArgs(AutoHiddenGroupControl, this);
            dockingManager.RaiseAutoHiddenGroupRemoved(groupArgs);
        }

        // Generate event so interested parties know this element and associated window have been disposed
        Dispose();
    }
    #endregion
}