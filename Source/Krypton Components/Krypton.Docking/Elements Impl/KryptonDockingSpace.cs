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
/// Base class for docking elements that manage a KryptonSpace derived class.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public abstract class KryptonDockingSpace : DockingElementClosedCollection
{
    #region Instance Fields

    private KryptonSpace? _space;
    private readonly string _storeName;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingSpace class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="storeName">Name to use for storage pages.</param>
    protected KryptonDockingSpace(string name, string storeName)
        : base(name) =>
        _storeName = storeName;

    #endregion

    #region Public
    /// <summary>
    /// Add a KryptonPage to the currently active cell or create a new cell is no cell is currently active.
    /// </summary>
    /// <param name="page">KryptonPage to be added.</param>
    public void Append(KryptonPage page) =>
        // Use existing array adding method to prevent duplication of code
        Append(new[] { page });

    /// <summary>
    /// Add a KryptonPage array to the currently active cell or create a new cell is no cell is currently active.
    /// </summary>
    /// <param name="pages">Array of KryptonPage instances to be added.</param>
    public void Append(KryptonPage[]? pages)
    {
        // Demand that pages are not already present
        DemandPagesNotBePresent(pages);

        if (pages != null)
        {
            ObserveAutoHiddenSlideSize(pages);
            // If there is no active cell, or cell is page-less...
            KryptonWorkspaceCell? cell = SpaceControl?.ActiveCell;
            if (cell == null || cell.Pages.Count == 0)
            {
                // Remove the page-less cell if exists...
                if (cell?.Pages.Count == 0)
                {
                    SpaceControl!.Root.Children!.Remove(cell);
                }

                // ...create a new cell and place at the end of the root collection
                cell = new KryptonWorkspaceCell();
                SpaceControl!.Root.Children!.Add(cell);
            }

            // Add all provided pages into the cell
            cell.Pages.AddRange(pages);
            var childMinSize = cell.GetMinSize();
            if (SpaceControl != null)
            {
                SpaceControl.Size = childMinSize;
                SpaceControl.MinimumSize = childMinSize;
            }
        }
    }

    private void ObserveAutoHiddenSlideSize(KryptonPage[] pages)
    {
        if (SpaceControl != null)
        {
            Size currentHintSize = SpaceControl.Size;
            foreach (Size newPageSize in pages.Select(static page => page.AutoHiddenSlideSize))
            {
                if (currentHintSize.Width < newPageSize.Width)
                {
                    currentHintSize.Width = newPageSize.Width;
                }

                if (currentHintSize.Height < newPageSize.Height)
                {
                    currentHintSize.Height = newPageSize.Height;
                }
            }

            SpaceControl.Size = currentHintSize;
        }
    }

    /// <summary>
    /// Add a KryptonPage into an existing cell.
    /// </summary>
    /// <param name="cell">Reference to existing workspace cell.</param>
    /// <param name="page">KryptonPage instance to be added.</param>
    public void CellAppend(KryptonWorkspaceCell cell, KryptonPage page) =>
        // Use existing array adding method to prevent duplication of code
        CellAppend(cell, new[] { page });

    /// <summary>
    /// Add a KryptonPage array into an existing cell.
    /// </summary>
    /// <param name="cell">Reference to existing workspace cell.</param>
    /// <param name="pages">Array of KryptonPage instances to be added.</param>
    public void CellAppend(KryptonWorkspaceCell cell, KryptonPage[]? pages)
    {
        // Demand that pages are not already present
        DemandPagesNotBePresent(pages);

        // Cannot insert to a null cell
        if (cell == null)
        {
            throw new ArgumentNullException(nameof(cell));
        }

        // Check that we actually contain the provided workspace cell
        KryptonWorkspaceCell? checkCell = SpaceControl?.FirstCell();
        while (checkCell != null)
        {
            if (checkCell == cell)
            {
                break;
            }

            checkCell = SpaceControl!.NextCell(checkCell);
        }

        if (cell != checkCell)
        {
            throw new ArgumentException(@"KryptonWorkspaceCell reference is not inside this workspace", nameof(cell));
        }

        // Append all the pages to end of the cell pages collection
        if (pages != null)
        {
            ObserveAutoHiddenSlideSize(pages);
            cell.Pages.AddRange(pages);
        }
    }

    /// <summary>
    /// Add a KryptonPage array into an existing cell starting at the provided index.
    /// </summary>
    /// <param name="cell">Reference to existing workspace cell.</param>
    /// <param name="index">Index for inserting new pages.</param>
    /// <param name="page">KryptonPage instance to be added.</param>
    public void CellInsert(KryptonWorkspaceCell cell, int index, KryptonPage page) =>
        // Use existing array adding method to prevent duplication of code
        CellInsert(cell, index, new[] { page });

    /// <summary>
    /// Add a KryptonPage array into an existing cell starting at the provided index.
    /// </summary>
    /// <param name="cell">Reference to existing workspace cell.</param>
    /// <param name="index">Index for inserting new pages.</param>
    /// <param name="pages">Array of KryptonPage instances to be added.</param>
    public void CellInsert([DisallowNull] KryptonWorkspaceCell cell, int index, KryptonPage[]? pages)
    {
        // Demand that pages are not already present
        DemandPagesNotBePresent(pages);

        // Cannot insert to a null cell
        if (cell == null)
        {
            throw new ArgumentNullException(nameof(cell));
        }

        // Check that we actually contain the provided workspace cell
        KryptonWorkspaceCell? checkCell = SpaceControl?.FirstCell();
        while (checkCell != null)
        {
            if (checkCell == cell)
            {
                break;
            }

            checkCell = SpaceControl!.NextCell(checkCell);
        }

        if (cell != checkCell)
        {
            throw new ArgumentException(@"KryptonWorkspaceCell reference is not inside this workspace", nameof(cell));
        }

        if (pages != null)
        {
            ObserveAutoHiddenSlideSize(pages);
            // Insert all the pages in sequence starting at the provided index
            foreach (KryptonPage page in pages)
            {
                cell.Pages.Insert(index++, page);
            }
        }
    }

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

            // Grab the strings from the docking manager
            UpdateStrings();

            // Generate adding event for each cell that already exists
            KryptonWorkspaceCell? cell = SpaceControl?.FirstCell();
            while (cell != null)
            {
                OnSpaceCellAdding(SpaceControl!, new WorkspaceCellEventArgs(cell));
                cell = SpaceControl!.NextCell(cell);
            }
        }
    }

    /// <summary>
    /// Propagates an action request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
    public override void PropogateAction(DockingPropogateAction action, string[]? uniqueNames)
    {
        if (SpaceControl == null)
        {
            // Let base class perform standard processing
            base.PropogateAction(action, uniqueNames);
            return;
        }

        switch (action)
        {
            case DockingPropogateAction.Loading:
                // Force layout so that the correct number of pages is recognized
                SpaceControl.PerformLayout();

                // Remove all the pages including store pages
                SpaceControl.ClearAllPages();

                // Force layout so that the control will kill itself
                SpaceControl.PerformLayout();

                break;
            case DockingPropogateAction.ShowPages:
            case DockingPropogateAction.HidePages:
            {
                var newVisible = (action == DockingPropogateAction.ShowPages);
                // Update visible state of pages that are not placeholders
                if (uniqueNames == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames));
                }
                foreach (KryptonPage? page in uniqueNames
                             .Select(uniqueName => SpaceControl.PageForUniqueName(uniqueName))
                             .Where(static page => page is not null and not KryptonStorePage))
                {
                    page!.Visible = newVisible;
                }
            }
                break;
            case DockingPropogateAction.ShowAllPages:
                SpaceControl.ShowAllPages(typeof(KryptonStorePage));
                break;
            case DockingPropogateAction.HideAllPages:
                SpaceControl.HideAllPages(typeof(KryptonStorePage));
                break;
            case DockingPropogateAction.RemovePages:
            case DockingPropogateAction.RemoveAndDisposePages:
                if (uniqueNames == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames));
                }
                foreach (var uniqueName in uniqueNames)
                {
                    // If the named page exists and is not placeholder then remove it
                    KryptonPage? removePage = SpaceControl.PageForUniqueName(uniqueName);
                    if (removePage is not null and not KryptonStorePage)
                    {
                        // Find the cell that contains the target so we can remove the page
                        KryptonWorkspaceCell? cell = SpaceControl.CellForPage(removePage);
                        if (cell != null)
                        {
                            cell.Pages.Remove(removePage);

                            if (action == DockingPropogateAction.RemoveAndDisposePages)
                            {
                                removePage.Dispose();
                            }
                        }
                    }
                }
                SpaceControl.PerformLayout();
                break;
            case DockingPropogateAction.RemoveAllPages:
            case DockingPropogateAction.RemoveAndDisposeAllPages:
            {
                // Process each cell in turn
                KryptonWorkspaceCell? cell = SpaceControl.FirstCell();
                while (cell != null)
                {
                    // Process each page inside the cell
                    for (var i = cell.Pages.Count - 1; i >= 0; i--)
                    {
                        // Only remove the actual page and not placeholders
                        KryptonPage? page = cell.Pages[i];
                        if (page is not null and not KryptonStorePage)
                        {
                            cell.Pages.RemoveAt(i);

                            if (action == DockingPropogateAction.RemoveAndDisposeAllPages)
                            {
                                page.Dispose();
                            }
                        }
                    }

                    cell = SpaceControl!.NextCell(cell);
                }

                // Force layout so that the control will kill itself
                SpaceControl?.PerformLayout();
            }
                break;
            case DockingPropogateAction.StorePages:
                if (uniqueNames == null)
                {
                    throw new ArgumentNullException(nameof(uniqueNames));
                }
                foreach (var uniqueName in uniqueNames)
                {
                    // Swap pages that are not placeholders to become placeholders
                    KryptonPage? page = SpaceControl.PageForUniqueName(uniqueName);
                    if (page is not null and not KryptonStorePage)
                    {
                        // Replace the existing page with a placeholder that has the same unique name
                        KryptonWorkspaceCell? cell = SpaceControl.CellForPage(page);
                        var placeholder = new KryptonStorePage(uniqueName, _storeName);
                        if (cell != null)
                        {
                            cell.Pages.Insert(cell.Pages.IndexOf(page), placeholder);
                            cell.Pages.Remove(page);
                        }
                    }
                }
                break;
            case DockingPropogateAction.StoreAllPages:
            {
                // Process each cell in turn
                KryptonWorkspaceCell? cell = SpaceControl.FirstCell();
                while (cell != null)
                {
                    // Process each page inside the cell
                    for (var i = cell.Pages.Count - 1; i >= 0; i--)
                    {
                        // Swap pages that are not placeholders to become placeholders
                        KryptonPage? page = cell.Pages[i];
                        if (page is not null and not KryptonStorePage)
                        {
                            // Replace the existing page with a placeholder that has the same unique name
                            var placeholder = new KryptonStorePage(page.UniqueName, _storeName);
                            cell.Pages.Insert(cell.Pages.IndexOf(page), placeholder);
                            cell.Pages.Remove(page);
                        }
                    }

                    cell = SpaceControl!.NextCell(cell);
                }
            }
                break;
            case DockingPropogateAction.ClearFillerStoredPages:
            case DockingPropogateAction.ClearFloatingStoredPages:
            case DockingPropogateAction.ClearDockedStoredPages:
            case DockingPropogateAction.ClearStoredPages:
                // Only process an attempt to clear all pages or those related to this docking location
                if ((action == DockingPropogateAction.ClearStoredPages) || (action == ClearStoreAction))
                {
                    if (uniqueNames == null)
                    {
                        throw new ArgumentNullException(nameof(uniqueNames));
                    }
                    foreach (var uniqueName in uniqueNames)
                    {
                        // Only remove a matching unique name if it is a placeholder page
                        KryptonPage? removePage = SpaceControl.PageForUniqueName(uniqueName);
                        if (removePage is KryptonStorePage)
                        {
                            // Check if the page is one marked to be ignored in this operation
                            if (removePage != IgnoreStorePage)
                            {
                                // Find the cell that contains the target so we can remove the page
                                KryptonWorkspaceCell? cell = SpaceControl.CellForPage(removePage);
                                cell?.Pages.Remove(removePage);
                            }
                        }
                    }
                }
                break;
            case DockingPropogateAction.ClearAllStoredPages:
            {
                // Process each cell in turn
                KryptonWorkspaceCell? cell = SpaceControl.FirstCell();
                while (cell != null)
                {
                    // Process each page inside the cell
                    for (var i = cell.Pages.Count - 1; i >= 0; i--)
                    {
                        // Remove all placeholders
                        KryptonPage page = cell.Pages[i];
                        if (page is KryptonStorePage)
                        {
                            cell.Pages.Remove(page);
                        }
                    }

                    cell = SpaceControl.NextCell(cell);
                }
            }
                break;
            case DockingPropogateAction.StringChanged:
                UpdateStrings();
                break;
            case DockingPropogateAction.DebugOutput:
                Console.WriteLine(SpaceControl.ToString());
                SpaceControl.DebugOutput();
                break;
        }

        // Let base class perform standard processing
        base.PropogateAction(action, uniqueNames);
    }

    /// <summary>
    /// Propagates an action request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="pages">Array of pages the action relates to.</param>
    public override void PropogateAction(DockingPropogateAction action, KryptonPage[] pages)
    {
        if (action == DockingPropogateAction.RestorePages)
        {
            foreach (KryptonPage page in pages)
            {
                // Swap pages that are placeholders for the actual pages
                KryptonPage? storePage = SpaceControl?.PageForUniqueName(page.UniqueName);
                if (storePage is KryptonStorePage)
                {
                    KryptonWorkspaceCell? cell = SpaceControl!.CellForPage(storePage);
                    cell?.Pages.Insert(cell.Pages.IndexOf(storePage), page);
                }
            }
        }

        // Let base class perform standard processing
        base.PropogateAction(action, pages);
    }

    /// <summary>
    /// Propagates a boolean state request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="state">Boolean state that is requested to be recovered.</param>
    /// <param name="uniqueName">Unique name of the page the request relates to.</param>
    /// <returns>True/False if state is known; otherwise null indicating no information available.</returns>
    public override bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName)
    {
        switch (state)
        {
            case DockingPropogateBoolState.ContainsPage:
            {
                // Return the definitive answer 'true' if the control contains the named page
                KryptonPage? page = SpaceControl?.PageForUniqueName(uniqueName);
                if (page is not null and not KryptonStorePage)
                {
                    return true;
                }
            }
                break;
            case DockingPropogateBoolState.ContainsStorePage:
            {
                // Return definitive answer 'true' if the group controls contains a store page for the unique name.
                KryptonPage? page = SpaceControl?.PageForUniqueName(uniqueName);
                if (page is KryptonStorePage)
                {
                    return true;
                }
            }
                break;
            case DockingPropogateBoolState.IsPageShowing:
            {
                // If we have the requested page then return the visible state of the page
                KryptonPage? page = SpaceControl?.PageForUniqueName(uniqueName);
                if (page is not null and not KryptonStorePage)
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
    /// Propagates a page request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="state">Request that should result in a page reference if found.</param>
    /// <param name="uniqueName">Unique name of the page the request relates to.</param>
    /// <returns>Reference to page that matches the request; otherwise null.</returns>
    public override KryptonPage? PropogatePageState(DockingPropogatePageState state, string uniqueName)
    {
        switch (state)
        {
            case DockingPropogatePageState.PageForUniqueName:
            {
                // If we have the requested name page and it is not a placeholder then we have found it
                KryptonPage? page = SpaceControl?.PageForUniqueName(uniqueName);
                if (page is not null and not KryptonStorePage)
                {
                    return page;
                }
            }
                break;
        }

        // Let base class perform standard processing
        return base.PropogatePageState(state, uniqueName);
    }

    /// <summary>
    /// Propagates a page list request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="state">Request that should result in pages collection being modified.</param>
    /// <param name="pages">Pages collection for modification by the docking elements.</param>
    public override void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages)
    {
        // If the request relevant to this space control?
        var processCells = state switch
        {
            DockingPropogatePageList.All => true,
            DockingPropogatePageList.Docked => (ClearStoreAction == DockingPropogateAction.ClearDockedStoredPages),
            DockingPropogatePageList.Floating => (ClearStoreAction == DockingPropogateAction.ClearFloatingStoredPages),
            DockingPropogatePageList.Filler => (ClearStoreAction == DockingPropogateAction.ClearFillerStoredPages),
            _ => false
        };

        if (processCells)
        {
            // Process each cell in turn
            KryptonWorkspaceCell? cell = SpaceControl?.FirstCell();
            while (cell != null)
            {
                // Process each page inside the cell
                for (var i = cell.Pages.Count - 1; i >= 0; i--)
                {
                    // Only add real pages and not placeholders
                    KryptonPage? page = cell.Pages[i];
                    if (page is not null and not KryptonStorePage)
                    {
                        pages.Add(page);
                    }
                }

                cell = SpaceControl!.NextCell(cell);
            }
        }

        // Let base class perform standard processing
        base.PropogatePageList(state, pages);
    }

    /// <summary>
    /// Propagates a workspace cell list request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="state">Request that should result in the cells collection being modified.</param>
    /// <param name="cells">Cells collection for modification by the docking elements.</param>
    public override void PropogateCellList(DockingPropogateCellList state, KryptonWorkspaceCellList cells)
    {
        var processCells = state switch
        {
            DockingPropogateCellList.All => true,
            DockingPropogateCellList.Docked => (ClearStoreAction == DockingPropogateAction.ClearDockedStoredPages),
            DockingPropogateCellList.Floating => (ClearStoreAction == DockingPropogateAction.ClearFloatingStoredPages),
            DockingPropogateCellList.Workspace => (ClearStoreAction == DockingPropogateAction.ClearFillerStoredPages),
            _ => false
        };
        if (processCells)
        {
            // Find each cell in turn
            KryptonWorkspaceCell? cell = SpaceControl?.FirstCell();
            while (cell != null)
            {
                cells.Add(cell);
                cell = SpaceControl!.NextCell(cell);
            }
        }

        // Let base class perform standard processing
        base.PropogateCellList(state, cells);
    }

    /// <summary>
    /// Gets the number of visible pages.
    /// </summary>
    public int VisiblePages => SpaceControl?.PageVisibleCount ?? 0;

    /// <summary>
    /// Return an array of the visible pages that are inside the cell that contains the provided unique name.
    /// </summary>
    /// <param name="uniqueName">Unique name of page that is inside the target cell.</param>
    /// <returns>Array of page references.</returns>
    public KryptonPage[] CellVisiblePages(string uniqueName)
    {
        var pages = new List<KryptonPage>();

        // Grab the cell that contains the provided unique name
        KryptonWorkspaceCell? cell = SpaceControl?.CellForUniqueName(uniqueName);
        if (cell != null)
        {
            // Only interested in visible pages that are not placeholders
            pages.AddRange(cell.Pages.Where(static page => page is not KryptonStorePage && page.LastVisibleSet));
        }

        return pages.ToArray();
    }

    /// <summary>
    /// Return the workspace cell that contains the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name for search.</param>
    /// <returns>Reference to KryptonWorkspaceCell if match found; otherwise null.</returns>
    public KryptonWorkspaceCell? CellForPage(string uniqueName) => SpaceControl?.CellForUniqueName(uniqueName);

    /// <summary>
    /// Ensure the provided page is selected within the cell that contains it.
    /// </summary>
    /// <param name="uniqueName">Unique name to be selected.</param>
    public void SelectPage(string uniqueName)
    {
        // Find the cell that contains the target named paged
        KryptonWorkspaceCell? cell = CellForPage(uniqueName);
        // Check that the pages collection contains the named paged
        KryptonPage? page = cell?.Pages[uniqueName];
        if (page != null)
        {
            cell!.SelectedPage = page;
        }
    }

    /// <summary>
    /// Update the strings from the docking manager.
    /// </summary>
    public void UpdateStrings()
    {
        if (SpaceControl != null)
        {
            KryptonDockingManager? dockingManager = DockingManager;
            if (dockingManager?.Strings != null)
            {
                SpaceControl.CloseTooltip = dockingManager.Strings.TextClose;
                SpaceControl.PinTooltip = dockingManager.Strings.TextAutoHide;
                SpaceControl.DropDownTooltip = dockingManager.Strings.TextWindowLocation;
            }
        }
    }

    /// <summary>
    /// Saves docking configuration information using a provider xml writer.
    /// </summary>
    /// <param name="xmlWriter">Xml writer object.</param>
    public override void SaveElementToXml(XmlWriter xmlWriter)
    {
        // Output workspace based docking element
        xmlWriter.WriteStartElement(XmlElementName);
        xmlWriter.WriteAttributeString(@"N", Name);
        xmlWriter.WriteAttributeString(@"O", Order.ToString());
        if (SpaceControl != null)
        {
            xmlWriter.WriteAttributeString(@"S", CommonHelper.SizeToString(SpaceControl.Size));

            // Output an xml for the contained workspace
            SpaceControl.PageSaving += OnSpaceControlPageSaving;
            SpaceControl.SaveLayoutToXml(xmlWriter);
            SpaceControl.PageSaving -= OnSpaceControlPageSaving;
        }

        // Terminate the workspace element
        xmlWriter.WriteFullEndElement();
    }

    /// <summary>
    /// Loads docking configuration information using a provider xml reader.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages for adding.</param>
    public override void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
    {
        // Is it the expected xml element name?
        if (xmlReader.Name != XmlElementName)
        {
            throw new ArgumentException($@"Element name '{XmlElementName}' was expected but found '{xmlReader.Name}' instead.", nameof(xmlReader));
        }

        // Grab the element attributes
        var elementName = xmlReader.GetAttribute(@"N");
        var elementOrder = xmlReader.GetAttribute(@"O");
        var elementSize = xmlReader.GetAttribute(@"S");

        // Check the name matches up
        if (elementName != Name)
        {
            throw new ArgumentException($@"Attribute 'N' value '{Name}' was expected but found '{elementName}' instead.", nameof(xmlReader));
        }

        // Check for the optional element order value
        if (!string.IsNullOrEmpty(elementOrder))
        {
            Order = int.Parse(elementOrder);
        }
        else
        {
            Order = -1;
        }

        // Check for the optional element size value
        LoadSize = !string.IsNullOrEmpty(elementSize) ? CommonHelper.StringToSize(elementSize) : Size.Empty;

        // Read to the expect child element
        if (!xmlReader.Read())
        {
            throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
        }

        // This should always be a workspace definition
        if (xmlReader.Name != @"KW")
        {
            throw new ArgumentException($@"Element name 'KW' was expected but found '{xmlReader.Name}' instead.", nameof(xmlReader));
        }

        // Let derived class perform element specific persistence
        LoadDockingElement(xmlReader, pages);

        // Read past this element to the end element
        if (!xmlReader.Read())
        {
            throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets and sets (just once) the KryptonSpace derived class being managed.
    /// </summary>
    [DisallowNull]
    protected KryptonSpace? SpaceControl
    {
        get => _space;

        set
        {
            // Should only ever set the value once
            if (_space != null)
            {
                throw new ArgumentException(@"Cannot set the 'Space' property more than once.", nameof(SpaceControl));
            }

            // Cache for future use
            _space = value ?? throw new ArgumentNullException(nameof(value));

            // Hook into space events we need to monitor
            if (SpaceControl != null)
            {
                SpaceControl.Disposed += OnSpaceDisposed;
                SpaceControl.WorkspaceCellAdding += OnSpaceCellAdding;
                SpaceControl.PageDrop += RaiseSpacePageDrop;
            }
        }
    }

    /// <summary>
    /// Gets and sets the ordering of the associated control used during loading.
    /// </summary>
    protected int Order { get; set; }

    /// <summary>
    /// Gets and sets the size of the control found during loading.
    /// </summary>
    protected Size LoadSize { get; set; }

    /// <summary>
    /// Occurs when a page is added to a cell in the workspace.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A KryptonPageEventArgs containing the event data.</param>
    protected virtual void OnSpaceCellPageInserting(object? sender, KryptonPageEventArgs e)
    {
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            if (e.Item is KryptonStorePage page)
            {
                switch (sender)
                {
                    case KryptonDockspace dockspace
                        when (dockspace.CellForPage(e.Item) != null):
                    // Prevent this existing store page from being removed due to the Propagate action below. This can
                    // occur because a cell with pages is added in one go and so insert events are generated for the
                    // existing pages inside the cell to ensure that the event is always fired consistently.
                    case KryptonDockableWorkspace workspace
                        when (workspace.CellForPage(e.Item) != null):
                        // Prevent this existing store page from being removed due to the Propagate action below. This can
                        // occur because a cell with pages is added in one go and so insert events are generated for the
                        // existing pages inside the cell to ensure that the event is always fired consistently.
                        IgnoreStorePage = page;
                        break;
                }
            }

            // Remove any store page for the unique name of this page being added.
            if (e.Item != null)
            {
                dockingManager.PropogateAction(ClearStoreAction, new[] { e.Item.UniqueName });
            }
            IgnoreStorePage = null;
        }
    }

    /// <summary>
    /// Gets the propagate action used to clear a store page for this implementation.
    /// </summary>
    protected abstract DockingPropogateAction ClearStoreAction { get; }

    /// <summary>
    /// Raises the type specific space control removed event determined by the derived class.
    /// </summary>
    protected abstract void RaiseRemoved();

    /// <summary>
    /// Raises the type specific cell adding event determined by the derived class.
    /// </summary>
    /// <param name="cell">Reference to new cell being added.</param>
    protected abstract void RaiseCellAdding(KryptonWorkspaceCell cell);

    /// <summary>
    /// Raises the type specific cell removed event determined by the derived class.
    /// </summary>
    /// <param name="cell">Reference to an existing cell being removed.</param>
    protected abstract void RaiseCellRemoved(KryptonWorkspaceCell cell);

    /// <summary>
    /// Occurs when a page is dropped on the control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PageDropEventArgs containing the event data.</param>
    protected abstract void RaiseSpacePageDrop(object? sender, PageDropEventArgs e);

    /// <summary>
    /// Perform docking element specific actions based on the loading xml.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    protected override void LoadDockingElement(XmlReader xmlReader, KryptonPageCollection pages)
    {
        // Load layout information and use any matching pages in the provided collection
        if (SpaceControl != null)
        {
            SpaceControl.PageLoading += OnSpaceControlPageLoading;
            SpaceControl.RecreateLoadingPage += OnSpaceControlRecreateLoadingPage;
            SpaceControl.LoadLayoutFromXml(xmlReader, pages);
            SpaceControl.PageLoading -= OnSpaceControlPageLoading;
            SpaceControl.RecreateLoadingPage -= OnSpaceControlRecreateLoadingPage;
        }
    }

    /// <summary>
    /// Gets and sets reference to store page to be ignored during action.
    /// </summary>
    protected KryptonStorePage? IgnoreStorePage { get; set; }

    #endregion

    #region Implementation
    private void OnSpaceDisposed(object? sender, EventArgs e)
    {
        // Unhook from events to prevent memory leaking
        if (SpaceControl != null)
        {
            SpaceControl.Disposed -= OnSpaceDisposed;
            SpaceControl.WorkspaceCellAdding -= OnSpaceCellAdding;
            SpaceControl.PageDrop -= RaiseSpacePageDrop;
        }

        // Raise event to indicate the space control has been removed
        RaiseRemoved();
    }

    private void OnSpaceCellAdding(object? sender, WorkspaceCellEventArgs e)
    {
        var childMinSize = e.Cell.GetMinSize();
        if (SpaceControl != null)
        {
            SpaceControl.MinimumSize = new Size(Math.Max(SpaceControl.MinimumSize.Width, childMinSize.Width),
                Math.Max(SpaceControl.MinimumSize.Height, childMinSize.Height));
        }

        RaiseCellAdding(e.Cell);

        // Need to generate the removed event to match this adding event
        e.Cell.Disposed += OnSpaceCellRemoved;
    }

    private void OnSpaceCellRemoved(object? sender, EventArgs e)
    {
        // Remove event hooks so cell can be garbage collected
        if (sender is KryptonWorkspaceCell cell)
        {
            cell.Disposed -= OnSpaceCellRemoved;
            RaiseCellRemoved(cell);
        }
    }

    private void OnSpaceControlPageLoading(object? sender, PageLoadingEventArgs e)
    {
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new DockPageLoadingEventArgs(dockingManager, e.XmlReader, e.Page);
            dockingManager.RaisePageLoading(args);
        }
    }

    private void OnSpaceControlPageSaving(object? sender, PageSavingEventArgs e)
    {
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            var args = new DockPageSavingEventArgs(dockingManager, e.XmlWriter, e.Page);
            dockingManager.RaisePageSaving(args);
        }
    }

    private void OnSpaceControlRecreateLoadingPage(object? sender, RecreateLoadingPageEventArgs e)
    {
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.RaiseRecreateLoadingPage(e);
    }
    #endregion
}