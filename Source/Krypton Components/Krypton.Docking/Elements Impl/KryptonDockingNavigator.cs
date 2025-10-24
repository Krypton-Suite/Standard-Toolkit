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
/// Provides docking functionality by attaching to an existing KryptonDockableNavigator
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
    /// Initialize a new instance of the KryptonDockingNavigator class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    public KryptonDockingNavigator(string name)
        : this(name, @"Workspace", new KryptonDockableNavigator())
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonDockingNavigator class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="storeName">Name to use for storage pages.</param>
    /// <param name="navigator">Reference to navigator to manage.</param>
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
    /// Gets the control this element is managing.
    /// </summary>
    public KryptonDockableNavigator DockableNavigatorControl { get; }

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
    /// Add a KryptonPage to the navigator.
    /// </summary>
    /// <param name="page">KryptonPage to be added.</param>
    public void Append(KryptonPage page) =>
        // Use existing array adding method to prevent duplication of code
        Append(new[] { page });

    /// <summary>
    /// Add a KryptonPage array to the navigator.
    /// </summary>
    /// <param name="pages">Array of KryptonPage instances to be added.</param>
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
    /// Show all display elements of the provided page.
    /// </summary>
    /// <param name="page">Reference to page that should be shown.</param>
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
    /// Show all display elements of the provided page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page that should be shown.</param>
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
    /// Show all display elements of the provided pages.
    /// </summary>
    /// <param name="pages">Array of references to pages that should be shown.</param>
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
    /// Hide all display elements of the provided page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page that should be hidden.</param>
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
    /// Hide all display elements of the provided pages.
    /// </summary>
    /// <param name="pages">Array of references to pages that should be hidden.</param>
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
    /// Hide all display elements of the provided pages.
    /// </summary>
    /// <param name="uniqueNames">Array of unique names of the pages that should be hidden.</param>
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
    /// Remove the referenced pages.
    /// </summary>
    /// <param name="pages">Array of references to pages that should be removed.</param>
    /// <param name="disposePage">Should the page be disposed when removed.</param>
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
    /// Remove the named pages.
    /// </summary>
    /// <param name="uniqueNames">Array of unique names of the pages that should be removed.</param>
    /// <param name="disposePage">Should the page be disposed when removed.</param>
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
    /// Propagates a page request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="state">Request that should result in a page reference if found.</param>
    /// <param name="uniqueName">Unique name of the page the request relates to.</param>
    /// <returns>Reference to page that matches the request; otherwise null.</returns>
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
    /// Propagates a page list request down the hierarchy of docking elements.
    /// </summary>
    /// <param name="state">Request that should result in pages collection being modified.</param>
    /// <param name="pages">Pages collection for modification by the docking elements.</param>
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
    /// Propagates a request for drag targets down the hierarchy of docking elements.
    /// </summary>
    /// <param name="floatingWindow">Reference to window being dragged.</param>
    /// <param name="dragData">Set of pages being dragged.</param>
    /// <param name="targets">Collection of drag targets.</param>
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
    /// Find the docking location of the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page.</param>
    /// <returns>Enumeration value indicating docking location.</returns>
    public override DockingLocation FindPageLocation(string uniqueName)
    {
        KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
        return (page != null)
               && page is not KryptonStorePage
            ? DockingLocation.Navigator
            : DockingLocation.None;
    }

    /// <summary>
    /// Find the docking element that contains the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page.</param>
    /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
    public override IDockingElement? FindPageElement(string uniqueName)
    {
        KryptonPage? page = DockableNavigatorControl.Pages[uniqueName];
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
    /// Find a navigator element by searching the hierarchy.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable navigator element is required.</param>
    /// <returns>KryptonDockingNavigator reference if found; otherwise false.</returns>
    public override KryptonDockingNavigator FindDockingNavigator(string uniqueName) => this;

    /// <summary>
    /// Gets the number of visible pages.
    /// </summary>
    public int VisiblePages => DockableNavigatorControl.Pages.VisibleCount;

    /// <summary>
    /// Ensure the provided page is selected within the cell that contains it.
    /// </summary>
    /// <param name="uniqueName">Unique name to be selected.</param>
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
    /// Saves docking configuration information using a provider xml writer.
    /// </summary>
    /// <param name="xmlWriter">Xml writer object.</param>
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