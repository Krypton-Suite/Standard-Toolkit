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

// ReSharper disable MemberCanBeInternal

// ReSharper disable RedundantNullableFlowAttribute
namespace Krypton.Docking;

/// <summary>
/// Docking element bound to a <see cref="KryptonDockableNavigator"/> that hosts pages as navigator tabs.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingNavigator : DockingElementClosedCollection
{
    #region Instance Fields
    private readonly string _storeName;

    #endregion

    #region Identity
    /// <summary>
    /// Delegates to the three-parameter constructor with store name <c>Workspace</c> and a new <see cref="KryptonDockableNavigator"/>.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    public KryptonDockingNavigator(string name)
        : this(name, @"Workspace", new KryptonDockableNavigator())
    {
    }

    /// <summary>
    /// Attaches to <paramref name="navigator"/> and subscribes to page insert, drag, and drop events.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="storeName">Name used when creating store-page placeholders.</param>
    /// <param name="navigator">Dockable navigator control to host pages.</param>
    /// <exception cref="ArgumentNullException"><paramref name="navigator"/> is <see langword="null"/>.</exception>
    public KryptonDockingNavigator(string name,
        string storeName,
        KryptonDockableNavigator navigator)
        : base(name)
    {
        _storeName = storeName;
        DockableNavigatorControl = navigator ?? throw new ArgumentNullException(nameof(navigator));

        DockableNavigatorControl.Disposed += OnDockableNavigatorDisposed;
        DockableNavigatorControl.CellPageInserting += OnDockableNavigatorPageInserting;
        DockableNavigatorControl.BeforePageDrag += OnDockableNavigatorBeforePageDrag;
        DockableNavigatorControl.PageDrop += OnDockableNavigatorPageDrop;
    }
    #endregion

    #region Public
    /// <summary>
    /// Dockable navigator control that hosts this element's pages.
    /// </summary>
    public KryptonDockableNavigator DockableNavigatorControl { get; }

    /// <summary>
    /// When assigned, raises <c>DockableNavigatorAdded</c> on the docking manager when one is reachable.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IDockingElement? Parent
    {
        set
        {
            // Let base class perform standard processing
            base.Parent = value;

            // Generate event so that any dockable navigator customization can be performed.
            KryptonDockingManager? dockingManager = DockingManager;
            if (dockingManager != null)
            {
                var args = new DockableNavigatorEventArgs(DockableNavigatorControl, this);
                dockingManager.RaiseDockableNavigatorAdded(args);
            }
        }
    }

    /// <summary>
    /// Adds a single page to the navigator after verifying it is not already hosted in the hierarchy.
    /// </summary>
    /// <param name="page">Page to append.</param>
    /// <exception cref="ArgumentOutOfRangeException">The page is already present in the docking hierarchy.</exception>
    /// <exception cref="ApplicationException">No docking manager is attached to this subtree.</exception>
    public void Append(KryptonPage page) =>
        // Use existing array adding method to prevent duplication of code
        Append(new[] { page });

    /// <summary>
    /// Adds pages to the navigator after verifying they are not already hosted in the hierarchy, then updates navigator minimum size from page and bar metrics.
    /// </summary>
    /// <param name="pages">Pages to append; may be <see langword="null"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">A page is already present in the docking hierarchy.</exception>
    /// <exception cref="ApplicationException">No docking manager is attached to this subtree.</exception>
    public void Append(KryptonPage[]? pages)
    {
        // Demand that pages are not already present
        DemandPagesNotBePresent(pages);

        if (pages != null)
        {
            DockableNavigatorControl.Pages.AddRange(pages);
        }

        var childMinSize = Size.Empty;
        foreach (var page in DockableNavigatorControl.Pages)
        {
            if (childMinSize.Height < page.MinimumSize.Height)
            {
                childMinSize.Height = page.MinimumSize.Height;
            }
            if (childMinSize.Width < page.MinimumSize.Width)
            {
                childMinSize.Width = page.MinimumSize.Width;
            }
        }
        switch (DockableNavigatorControl.Header.HeaderPositionBar)
        {
            case VisualOrientation.Top:
            case VisualOrientation.Bottom:
                childMinSize.Height += DockableNavigatorControl.Bar.BarMinimumHeight;
                break;
            case VisualOrientation.Left:
            case VisualOrientation.Right:
                childMinSize.Width += DockableNavigatorControl.Bar.BarMinimumHeight;
                break;
        }
        DockableNavigatorControl.Size = childMinSize;
        DockableNavigatorControl.MinimumSize = childMinSize;
    }

    /// <summary>
    /// Propagates a show request for the supplied page through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="page">Page whose display elements should become visible.</param>
    /// <exception cref="ArgumentNullException"><paramref name="page"/> is <see langword="null"/>.</exception>
    public void ShowPage(KryptonPage page)
    {
        // Cannot show a null reference
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        ShowPages(new[] { page.UniqueName });
    }

    /// <summary>
    /// Propagates a show request for the named page through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page whose display elements should become visible.</param>
    /// <exception cref="ArgumentNullException"><paramref name="uniqueName"/> is null or whitespace.</exception>
    public void ShowPage([DisallowNull] string uniqueName)
    {
        // Cannot show a null reference
        if (string.IsNullOrWhiteSpace(uniqueName))
        {
            throw new ArgumentNullException(nameof(uniqueName));
        }

        ShowPages(new[] { uniqueName });
    }

    /// <summary>
    /// Propagates a show request for the supplied pages through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="pages">Pages whose display elements should become visible.</param>
    /// <exception cref="ArgumentNullException"><paramref name="pages"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="pages"/> contains a null entry.</exception>
    public void ShowPages(KryptonPage[] pages)
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
    /// Propagates a show request for the named pages through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="uniqueNames">Unique names of pages whose display elements should become visible.</param>
    /// <exception cref="ArgumentNullException"><paramref name="uniqueNames"/> is <see langword="null"/> or contains a null entry.</exception>
    /// <exception cref="ArgumentException"><paramref name="uniqueNames"/> contains an empty string.</exception>
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
    /// Propagates a show-all request through the docking hierarchy inside a batched update.
    /// </summary>
    public void ShowAllPages()
    {
        using var update = new DockingMultiUpdate(this);
        base.PropogateAction(DockingPropogateAction.ShowAllPages, null as string[]);
    }

    /// <summary>
    /// Propagates a hide request for the supplied page through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="page">Page whose display elements should be hidden.</param>
    /// <exception cref="ArgumentNullException"><paramref name="page"/> is <see langword="null"/>.</exception>
    public void HidePage(KryptonPage page)
    {
        // Cannot hide a null reference
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        HidePages(new[] { page.UniqueName });
    }

    /// <summary>
    /// Propagates a hide request for the named page through the docking hierarchy inside a batched update when <paramref name="uniqueName"/> is non-empty.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page whose display elements should be hidden.</param>
    /// <exception cref="ArgumentNullException"><paramref name="uniqueName"/> is <see langword="null"/>.</exception>
    public void HidePage(string uniqueName)
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
    /// Propagates a hide request for the supplied pages through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="pages">Pages whose display elements should be hidden.</param>
    /// <exception cref="ArgumentNullException"><paramref name="pages"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="pages"/> contains a null entry.</exception>
    public void HidePages(KryptonPage[] pages)
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
    /// Propagates a hide request for the named pages through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="uniqueNames">Unique names of pages whose display elements should be hidden.</param>
    /// <exception cref="ArgumentNullException"><paramref name="uniqueNames"/> is <see langword="null"/> or contains a null entry.</exception>
    /// <exception cref="ArgumentException"><paramref name="uniqueNames"/> contains an empty string.</exception>
    public void HidePages(string[] uniqueNames)
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
    /// Propagates a hide-all request through the docking hierarchy inside a batched update.
    /// </summary>
    public void HideAllPages()
    {
        using var update = new DockingMultiUpdate(this);
        base.PropogateAction(DockingPropogateAction.HideAllPages, null as string[]);
    }

    /// <summary>
    /// Propagates a remove request for the named page through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to remove.</param>
    /// <param name="disposePage">Whether removed pages should also be disposed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="uniqueName"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="uniqueName"/> is empty.</exception>
    public void RemovePage(string uniqueName, bool disposePage)
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
    /// Propagates a remove request for the supplied pages through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="pages">Pages to remove.</param>
    /// <param name="disposePage">Whether removed pages should also be disposed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="pages"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="pages"/> contains a null entry.</exception>
    public void RemovePages(KryptonPage[] pages, bool disposePage)
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
    /// Propagates a remove request for the named pages through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="uniqueNames">Unique names of pages to remove.</param>
    /// <param name="disposePage">Whether removed pages should also be disposed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="uniqueNames"/> is <see langword="null"/> or contains a null entry.</exception>
    /// <exception cref="ArgumentException"><paramref name="uniqueNames"/> contains an empty string.</exception>
    public void RemovePages(string[] uniqueNames, bool disposePage)
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
    /// Propagates a remove-all request through the docking hierarchy inside a batched update.
    /// </summary>
    /// <param name="disposePage">Whether removed pages should also be disposed.</param>
    public void RemoveAllPages(bool disposePage)
    {
        // Remove all details about all pages from all parts of the hierarchy
        using var update = new DockingMultiUpdate(this);
        base.PropogateAction(disposePage ? DockingPropogateAction.RemoveAndDisposeAllPages : DockingPropogateAction.RemoveAllPages, null as string[]);
    }

    /// <summary>
    /// Applies navigator-specific handling for loading, store-page, and debug actions, ignores selected global actions, then delegates remaining actions to the base implementation.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="uniqueNames">Page unique names targeted by the action.</param>
    /// <exception cref="ArgumentNullException"><paramref name="uniqueNames"/> is <see langword="null"/> for actions that require page names.</exception>
    public override void PropogateAction(DockingPropogateAction action, string[]? uniqueNames)
    {
        KryptonPageCollection pageCollection = DockableNavigatorControl.Pages;
        switch (action)
        {
            case DockingPropogateAction.Loading:
                // Remove all pages including store pages
                pageCollection.Clear();
                return;
            case DockingPropogateAction.ShowAllPages:
            case DockingPropogateAction.HideAllPages:
            case DockingPropogateAction.RemoveAllPages:
            case DockingPropogateAction.RemoveAndDisposeAllPages:
                // Ignore some global actions
                return;
            case DockingPropogateAction.StorePages:
                if (uniqueNames == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames));
                }
                foreach (var uniqueName in uniqueNames)
                {
                    // Swap pages that are not placeholders to become placeholders
                    KryptonPage? page = pageCollection[uniqueName];
                    if ((page != null) && page is not KryptonStorePage)
                    {
                        // Replace the existing page with a placeholder that has the same unique name
                        var placeholder = new KryptonStorePage(uniqueName, _storeName);
                        pageCollection.Insert(pageCollection.IndexOf(page), placeholder);
                        pageCollection.Remove(page);
                    }
                }
                break;
            case DockingPropogateAction.StoreAllPages:
                // Process each page inside the cell
                for (var i = pageCollection.Count - 1; i >= 0; i--)
                {
                    // Swap pages that are not placeholders to become placeholders
                    KryptonPage? page = pageCollection[i];
                    if (page is not null and not KryptonStorePage)
                    {
                        // Replace the existing page with a placeholder that has the same unique name
                        var placeholder = new KryptonStorePage(page.UniqueName, _storeName);
                        pageCollection.Insert(pageCollection.IndexOf(page), placeholder);
                        pageCollection.Remove(page);
                    }
                }

                break;
            case DockingPropogateAction.ClearFillerStoredPages:
            case DockingPropogateAction.ClearStoredPages:
                if (uniqueNames == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames));
                }
                foreach (KryptonStorePage removePage in uniqueNames.Select(uniqueName => pageCollection[uniqueName]).OfType<KryptonStorePage>())
                {
                    pageCollection.Remove(removePage);
                }
                break;
            case DockingPropogateAction.ClearAllStoredPages:
            {
                // Process each page inside the cell
                for (var i = pageCollection.Count - 1; i >= 0; i--)
                {
                    // Remove all placeholders
                    KryptonPage page = pageCollection[i];
                    if (page is KryptonStorePage)
                    {
                        pageCollection.Remove(page);
                    }
                }
            }
                break;
            case DockingPropogateAction.DebugOutput:
                Console.WriteLine(GetType().ToString());
                DockableNavigatorControl.DebugOutput();
                break;
        }

        // Let base class perform standard processing
        base.PropogateAction(action, uniqueNames);
    }

    /// <summary>
    /// Answers <see cref="DockingPropogateBoolState.ContainsPage"/>, <see cref="DockingPropogateBoolState.ContainsStorePage"/>, and <see cref="DockingPropogateBoolState.IsPageShowing"/> for navigator-hosted pages before delegating to the base implementation.
    /// </summary>
    /// <param name="state">Boolean query to resolve.</param>
    /// <param name="uniqueName">Unique name of the page the query concerns.</param>
    /// <returns><see langword="true"/> or <see langword="false"/> when this navigator can answer; otherwise the base result.</returns>
    public override bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName)
    {
        switch (state)
        {
            case DockingPropogateBoolState.ContainsPage:
            {
                // Return the definitive answer 'true' if the control contains the named page
                KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
                if ((page != null) && page is not KryptonStorePage)
                {
                    return true;
                }
            }
                break;
            case DockingPropogateBoolState.ContainsStorePage:
            {
                // Return definitive answer 'true' if the group controls contains a store page for the unique name.
                KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
                if (page is KryptonStorePage)
                {
                    return true;
                }
            }
                break;
            case DockingPropogateBoolState.IsPageShowing:
            {
                // If we have the requested page then return the visible state of the page
                KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
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
    /// Returns a non-placeholder navigator page for <see cref="DockingPropogatePageState.PageForUniqueName"/> before delegating to the base implementation.
    /// </summary>
    /// <param name="state">Page query to resolve.</param>
    /// <param name="uniqueName">Unique name of the page the query concerns.</param>
    /// <returns>The matching page when hosted here and not a store page; otherwise the base result.</returns>
    public override KryptonPage? PropogatePageState(DockingPropogatePageState state, string uniqueName)
    {
        if (state == DockingPropogatePageState.PageForUniqueName)
        {
            // If we have the requested name page and it is not a placeholder then we have found it
            KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
            if ((page != null) && page is not KryptonStorePage)
            {
                return page;
            }
        }

        // Let base class perform standard processing
        return base.PropogatePageState(state, uniqueName);
    }

    /// <summary>
    /// Appends non-placeholder navigator pages to <paramref name="pages"/> for <see cref="DockingPropogatePageList.All"/> and <see cref="DockingPropogatePageList.Filler"/> requests before delegating to the base implementation.
    /// </summary>
    /// <param name="state">Page-list query to resolve.</param>
    /// <param name="pages">Collection to append matching pages into.</param>
    public override void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages)
    {
        switch (state)
        {
            case DockingPropogatePageList.All:
            case DockingPropogatePageList.Docked:
            case DockingPropogatePageList.Floating:
            case DockingPropogatePageList.Filler:
            {
                // If the request relevant to this space control?
                if (state is DockingPropogatePageList.All or DockingPropogatePageList.Filler)
                {
                    // Process each page inside the navigator
                    for (var i = DockableNavigatorControl.Pages.Count - 1; i >= 0; i--)
                    {
                        // Only add real pages and not placeholders
                        KryptonPage? page = DockableNavigatorControl.Pages[i];
                        if (page is not null and not KryptonStorePage)
                        {
                            pages.Add(page);
                        }
                    }
                }
            }
                break;
        }

        // Let base class perform standard processing
        base.PropogatePageList(state, pages);
    }

    /// <summary>
    /// Appends navigator drag targets for dragged pages that allow navigator docking.
    /// </summary>
    /// <param name="floatingWindow">Floating window under drag, if any.</param>
    /// <param name="dragData">Pages under drag.</param>
    /// <param name="targets">List to append candidate targets into.</param>
    public override void PropogateDragTargets(KryptonFloatingWindow? floatingWindow,
        PageDragEndData? dragData,
        DragTargetList targets)
    {
        // Create list of the pages that are allowed to be dropped into this navigator
        var pages = new KryptonPageCollection();
        if (dragData != null)
        {

            foreach (KryptonPage page in dragData.Pages.Where(static page =>
                         page.AreFlagsSet(KryptonPageFlags.DockingAllowNavigator)))
            {
                pages.Add(page);
            }
        }

        // Do we have any pages left for dragging?
        if (pages.Count > 0)
        {
            DragTargetList navigatorTargets = DockableNavigatorControl.GenerateDragTargets(new PageDragEndData(this, pages), KryptonPageFlags.DockingAllowNavigator);

            targets.AddRange(navigatorTargets.ToArray());
        }
    }

    /// <summary>
    /// Returns <see cref="DockingLocation.Navigator"/> when a non-placeholder page with <paramref name="uniqueName"/> is hosted in this navigator.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns><see cref="DockingLocation.Navigator"/> when the page is present; otherwise <see cref="DockingLocation.None"/>.</returns>
    public override DockingLocation FindPageLocation(string uniqueName)
    {
        KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
        return (page != null)
               && page is not KryptonStorePage
            ? DockingLocation.Navigator
            : DockingLocation.None;
    }

    /// <summary>
    /// Returns this element when a non-placeholder page with <paramref name="uniqueName"/> is hosted in this navigator.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns>This element when the page is present; otherwise <see langword="null"/>.</returns>
    public override IDockingElement? FindPageElement(string uniqueName)
    {
        KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
        return (page != null)
               && page is not KryptonStorePage
            ? this
            : null;
    }

    /// <summary>
    /// Returns this element when a store-page placeholder for <paramref name="uniqueName"/> exists at <see cref="DockingLocation.Navigator"/>.
    /// </summary>
    /// <param name="location">Docking location to search.</param>
    /// <param name="uniqueName">Unique name of the page whose placeholder is sought.</param>
    /// <returns>This element when a matching store page is present; otherwise <see langword="null"/>.</returns>
    public override IDockingElement? FindStorePageElement(DockingLocation location, string uniqueName)
    {
        if (location == DockingLocation.Navigator)
        {
            KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
            if (page is KryptonStorePage)
            {
                return this;
            }
        }

        return null;
    }

    /// <summary>
    /// Always returns this element; the <paramref name="uniqueName"/> argument is not consulted.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page being located.</param>
    /// <returns>This element.</returns>
    public override KryptonDockingNavigator FindDockingNavigator(string uniqueName) => this;

    /// <summary>
    /// Count of visible pages reported by the dockable navigator.
    /// </summary>
    public int VisiblePages => DockableNavigatorControl.Pages.VisibleCount;

    /// <summary>
    /// Sets the dockable navigator selected page when <paramref name="uniqueName"/> is present in its page collection.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to select.</param>
    public void SelectPage(string uniqueName)
    {
        // Check that the pages collection contains the named paged
        KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
        if (page != null)
        {
            DockableNavigatorControl.SelectedPage = page;
        }
    }

    /// <summary>
    /// Writes navigator page entries and custom page data for pages that allow configuration save.
    /// </summary>
    /// <param name="xmlWriter">Destination XML writer.</param>
    public override void SaveElementToXml(XmlWriter xmlWriter)
    {
        // Output navigator docking element
        xmlWriter.WriteStartElement(XmlElementName);
        xmlWriter.WriteAttributeString(@"N", Name);
        xmlWriter.WriteAttributeString(@"C", DockableNavigatorControl.Pages.Count(static page => page.AreFlagsSet(KryptonPageFlags.AllowConfigSave)).ToString());

        // Persist each child page in turn
        KryptonDockingManager? dockingManager = DockingManager;

        foreach (KryptonPage page in DockableNavigatorControl.Pages.Where(static page => page.AreFlagsSet(KryptonPageFlags.AllowConfigSave)))
        {
            xmlWriter.WriteStartElement("KP");
            XmlHelper.TextToXmlAttribute(xmlWriter, @"UN", page.UniqueName);
            XmlHelper.TextToXmlAttribute(xmlWriter, @"S", CommonHelper.BoolToString(page is KryptonStorePage));
            XmlHelper.TextToXmlAttribute(xmlWriter, @"V", CommonHelper.BoolToString(page.LastVisibleSet), @"True");
            xmlWriter.WriteStartElement(@"CPD");
            var args = new DockPageSavingEventArgs(dockingManager, xmlWriter, page);
            dockingManager?.RaisePageSaving(args);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
        }

        // Output an xml for the contained workspace

        // Terminate the workspace element
        xmlWriter.WriteFullEndElement();
    }

    /// <summary>
    /// Clears existing navigator pages and rebuilds layout from persisted page entries, raising page-loading events for custom data.
    /// </summary>
    /// <param name="xmlReader">Source XML reader positioned at this element.</param>
    /// <param name="pages">Pool of pages available to satisfy non-placeholder entries.</param>
    /// <exception cref="ArgumentException">The XML structure or attributes do not match this element.</exception>
    public override void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
    {
        // Is it the expected xml element name?
        if (xmlReader.Name != XmlElementName)
        {
            throw new ArgumentException($@"Element name '{XmlElementName}' was expected but found '{xmlReader.Name}' instead.", nameof(xmlReader));
        }

        // Grab the element attributes
        var elementName = xmlReader.GetAttribute(@"N");
        var elementCount = xmlReader.GetAttribute(@"C") ?? string.Empty;

        // Check the name matches up
        if (elementName != Name)
        {
            throw new ArgumentException($@"Attribute 'N' value '{Name}' was expected but found '{elementName}' instead.", nameof(xmlReader));
        }

        // Remove any existing pages in the navigator
        DockableNavigatorControl.Pages.Clear();

        // If there are children then load them
        var count = int.Parse(elementCount);
        if (count > 0)
        {
            KryptonDockingManager? manager = DockingManager;
            for (var i = 0; i < count; i++)
            {
                // Read past this element
                if (!xmlReader.Read())
                {
                    throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
                }

                // Is it the expected xml element name?
                if (xmlReader.Name != @"KP")
                {
                    throw new ArgumentException($@"Element name 'KP' was expected but found '{xmlReader.Name}' instead.", nameof(xmlReader));
                }

                // Get the unique name of the page
                var uniqueName = XmlHelper.XmlAttributeToText(xmlReader, @"UN");
                var boolStore = CommonHelper.StringToBool(XmlHelper.XmlAttributeToText(xmlReader, @"S"));
                var boolVisible = CommonHelper.StringToBool(XmlHelper.XmlAttributeToText(xmlReader, @"V", @"True"));

                // If the entry is for just a placeholder...
                KryptonPage? page;
                if (boolStore)
                {
                    // Recreate the requested store page and append
                    page = new KryptonStorePage(uniqueName, _storeName);
                    DockableNavigatorControl.Pages.Add(page);
                }
                else
                {
                    // Can we find a provided page to match the incoming layout?
                    page = pages[uniqueName];
                    if (page == null && manager != null)
                    {
                        // Generate event so developer can create and supply the page now
                        var args = new RecreateLoadingPageEventArgs(uniqueName);

                        manager.RaiseRecreateLoadingPage(args);

                        if (args is { Cancel: false, Page: not null })
                        {
                            page = args.Page;
                        }
                    }

                    if (page != null)
                    {
                        // Use the loaded visible state
                        page.Visible = boolVisible;

                        // Remove from provided collection as we can only add it once to the docking hierarchy
                        pages.Remove(page);

                        // Add into the navigator
                        DockableNavigatorControl.Pages.Add(page);
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
        }

        // Read past this element to the end element
        if (!xmlReader.Read())
        {
            throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DN";

    #endregion

    #region Implementation
    private void OnDockableNavigatorDisposed(object? sender, EventArgs e)
    {
        // Unhook from events to prevent memory leaking
        DockableNavigatorControl.Disposed -= OnDockableNavigatorDisposed;
        DockableNavigatorControl.CellPageInserting -= OnDockableNavigatorPageInserting;
        DockableNavigatorControl.BeforePageDrag -= OnDockableNavigatorBeforePageDrag;
        DockableNavigatorControl.PageDrop -= OnDockableNavigatorPageDrop;

        // Generate event so that any dockable navigator customization can be reversed.
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new DockableNavigatorEventArgs(DockableNavigatorControl, this);
            dockingManager.RaiseDockableNavigatorRemoved(args);
        }
    }

    private void OnDockableNavigatorPageInserting(object? sender, KryptonPageEventArgs e)
    {
        // Remove any store page for the unique name of this page being added. In either case of adding a store
        // page or a regular page we want to ensure there does not exist a store page for that same unique name.
        KryptonDockingManager? dockingManager = DockingManager;
        if (e.Item != null)
        {
            dockingManager?.PropogateAction(DockingPropogateAction.ClearFillerStoredPages,
                new[] { e.Item.UniqueName });
        }
    }

    private void OnDockableNavigatorBeforePageDrag(object? sender, PageDragCancelEventArgs e)
    {
        // Validate the list of names to those that are still present in the navigator
        var pages = e.Pages.Where(page => page is not KryptonStorePage && DockableNavigatorControl.Pages.Contains(page)).ToList();

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

    private void OnDockableNavigatorPageDrop(object? sender, PageDropEventArgs e)
    {
        // Use event to indicate the page is moving to a navigator and allow it to be cancelled
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null && e.Page != null)
        {
            var args = new CancelUniqueNameEventArgs(e.Page.UniqueName, false);
            dockingManager.RaisePageNavigatorRequest(args);

            // Pass back the result of the event
            e.Cancel = args.Cancel;
        }
    }
    #endregion
}