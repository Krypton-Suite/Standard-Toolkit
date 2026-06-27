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

namespace Krypton.Docking;

/// <summary>
/// Abstract node in the docking hierarchy that forwards path resolution, page queries, and layout actions to child elements.
/// </summary>
public abstract class DockingElement : Component,
    IDockingElement
{
    #region Instance Fields

    private IDockingElement? _parent;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockingElement class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    protected DockingElement(string? name) =>
        // Do not allow null, use empty string instead
        Name = name ?? string.Empty;

    #endregion

    #region Public
    /// <summary>
    /// Identifier used when building comma-separated paths and resolving child elements by name.
    /// </summary>
    [Browsable(false)]
    [DisallowNull]
    public string Name { get; }

    /// <summary>
    /// Gets a comma separated list of names leading to this element.
    /// </summary>
    [Browsable(false)]
    public string Path
    {
        get
        {
            var path = new StringBuilder();

            IDockingElement? element = this;
            while (element != null)
            {
                // Need to comma separate element names
                if (path.Length > 0)
                {
                    path.Insert(0, ',');
                }

                // Prepend the elements name
                path.Insert(0, element.Name);

                // Walk up the chain of elements
                element = element.Parent;
            }

            return path.ToString();
        }
    }

    /// <summary>
    /// Walks the hierarchy by matching the first comma-separated segment to this element, then delegates the remainder to children.
    /// </summary>
    /// <param name="path">Comma-separated list of element names, starting with this element.</param>
    /// <returns>The element at the end of the path, or <see langword="null"/> when no segment matches.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="path"/> is empty.</exception>
    public virtual IDockingElement? ResolvePath(string path)
    {
        // Cannot resolve a null reference
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        // Path names cannot be zero length
        if (path.Length == 0)
        {
            throw new ArgumentException(@"Needs Comma separated list of names to resolve.", nameof(path));
        }

        // Extract the first name in the path
        var comma = path.IndexOf(',');
        var firstName = (comma == -1 ? path : path.Substring(0, comma));

        // If the first name matches ourself...
        if (firstName == Name)
        {
            // If there are no other names then we are the target
            if (firstName.Length == path.Length)
            {
                return this;
            }
            else
            {
                // Extract the remainder of the path
                var remainder = path.Substring(comma, path.Length - comma);

                // Give each child a chance to resolve the remainder of the path
                return this.Select(child => child.ResolvePath(remainder)).FirstOrDefault(static ret => ret != null);
            }
        }

        return null;
    }

    /// <summary>
    /// Parent element in the docking tree; assigning a parent that already contains a child with the same <see cref="Name"/> throws.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IDockingElement? Parent
    {
        get => _parent;

        set
        {
            // We do not allow the same name to occur twice in a collection (so check new parent collection)
            if (value?[Name] != null)
            {
                throw new ArgumentNullException(nameof(Parent), @"Parent provided already has our Name in its collection.");
            }

            _parent = value;
        }
    }

    /// <summary>
    /// Invokes <paramref name="action"/> on each direct child in reverse index order so self-removing children do not disturb the loop.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="uniqueNames">Page unique names targeted by the action; <see langword="null"/> for <c>StartUpdate</c> and <c>EndUpdate</c>.</param>
    public virtual void PropogateAction(DockingPropogateAction action, string[]? uniqueNames)
    {
        // Propagate the action request to all the child elements (Even the null ones !!)
        // (use reverse order so if element removes itself we still have a valid loop)
        for (var i = Count - 1; i >= 0; i--)
        {
            {
                this[i]!.PropogateAction(action, uniqueNames);
            }
        }
    }

    /// <summary>
    /// Invokes <paramref name="action"/> on each direct child in reverse index order, passing the supplied pages.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="pages">Pages targeted by the action.</param>
    public virtual void PropogateAction(DockingPropogateAction action, KryptonPage[] pages)
    {
        // Propagate the action request to all the child elements
        // (use reverse order so if element removes itself we still have a valid loop)
        for (var i = Count - 1; i >= 0; i--)
        {
            this[i]?.PropogateAction(action, pages);
        }
    }

    /// <summary>
    /// Invokes <paramref name="action"/> on each direct child in reverse index order, passing the integer argument.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="value">Integer argument for the action.</param>
    public virtual void PropogateAction(DockingPropogateAction action, int value)
    {
        // Propagate the action request to all the child elements
        // (use reverse order so if element removes itself we still have a valid loop)
        for (var i = Count - 1; i >= 0; i--)
        {
            this[i]?.PropogateAction(action, value);
        }
    }

    /// <summary>
    /// Asks each child in order for <paramref name="state"/>; returns the first definite answer.
    /// </summary>
    /// <param name="state">Boolean query to resolve.</param>
    /// <param name="uniqueName">Unique name of the page the query concerns.</param>
    /// <returns><see langword="true"/> or <see langword="false"/> when a child answers; otherwise <see langword="null"/>.</returns>
    public virtual bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName)
    {
        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            // If the child knows the exact answer then return it now
            var ret = this[i]?.PropogateBoolState(state, uniqueName);
            if (ret.HasValue)
            {
                return ret;
            }
        }

        return null;
    }

    /// <summary>
    /// Forwards <paramref name="state"/> to each child in reverse index order so they can update <paramref name="value"/>.
    /// </summary>
    /// <param name="state">Integer query to resolve.</param>
    /// <param name="value">Value aggregated by participating children.</param>
    public virtual void PropogateIntState(DockingPropogateIntState state, ref int value)
    {
        // Propagate the request to all the child elements
        for (var i = Count - 1; i >= 0; i--)
        {
            this[i]?.PropogateIntState(state, ref value);
        }
    }

    /// <summary>
    /// Asks each child in order for <paramref name="state"/>; returns the first matching page.
    /// </summary>
    /// <param name="state">Page query to resolve.</param>
    /// <param name="uniqueName">Unique name of the page the query concerns.</param>
    /// <returns>The first page returned by a child, or <see langword="null"/> when none respond.</returns>
    public virtual KryptonPage? PropogatePageState(DockingPropogatePageState state, string uniqueName)
    {
        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            // If the child knows the answer then return it now
            KryptonPage? page = this[i]?.PropogatePageState(state, uniqueName);
            if (page != null)
            {
                return page;
            }
        }

        return null;
    }

    /// <summary>
    /// Invokes <paramref name="state"/> on each child in reverse index order so they can add pages to <paramref name="pages"/>.
    /// </summary>
    /// <param name="state">Page-list query to resolve.</param>
    /// <param name="pages">Collection children append matching pages into.</param>
    public virtual void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages)
    {
        // Propagate the action request to all the child elements
        // (use reverse order so if element removes itself we still have a valid loop)
        for (var i = Count - 1; i >= 0; i--)
        {
            this[i]?.PropogatePageList(state, pages);
        }
    }

    /// <summary>
    /// Invokes <paramref name="state"/> on each child in reverse index order so they can add cells to <paramref name="cells"/>.
    /// </summary>
    /// <param name="state">Cell-list query to resolve.</param>
    /// <param name="cells">Collection children append matching cells into.</param>
    public virtual void PropogateCellList(DockingPropogateCellList state, KryptonWorkspaceCellList cells)
    {
        // Propagate the action request to all the child elements
        // (use reverse order so if element removes itself we still have a valid loop)
        for (var i = Count - 1; i >= 0; i--)
        {
            this[i]?.PropogateCellList(state, cells);
        }
    }

    /// <summary>
    /// Asks each child to contribute drag targets for the current drag operation.
    /// </summary>
    /// <param name="floatingWindow">Floating window being dragged, if any.</param>
    /// <param name="dragData">Pages under drag.</param>
    /// <param name="targets">List children append candidate targets into.</param>
    public virtual void PropogateDragTargets(KryptonFloatingWindow? floatingWindow,
        PageDragEndData? dragData,
        DragTargetList targets)
    {
        // Propagate the request to all child elements
        foreach (IDockingElement child in this)
        {
            child.PropogateDragTargets(floatingWindow, dragData, targets);
        }
    }

    /// <summary>
    /// Returns the first non-<see cref="DockingLocation.None"/> location reported by a child for <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns>The docking location reported by a child, or <see cref="DockingLocation.None"/> when none contain the page.</returns>
    public virtual DockingLocation FindPageLocation(string uniqueName)
    {
        // Default to not finding the page
        var location = DockingLocation.None;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            location = this[i]?.FindPageLocation(uniqueName) ?? DockingLocation.None;
            if (location != DockingLocation.None)
            {
                break;
            }
        }

        return location;
    }

    /// <summary>
    /// Returns the first child that contains a non-placeholder page with <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns>The owning child element, or <see langword="null"/> when no child contains the page.</returns>
    public virtual IDockingElement? FindPageElement(string uniqueName)
    {
        // Default to not finding the element
        IDockingElement? dockingElement = null;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            dockingElement = this[i]?.FindPageElement(uniqueName);
            if (dockingElement != null)
            {
                break;
            }
        }

        return dockingElement;
    }

    /// <summary>
    /// Returns the first child that holds a store placeholder for <paramref name="uniqueName"/> at <paramref name="location"/>.
    /// </summary>
    /// <param name="location">Docking location to search.</param>
    /// <param name="uniqueName">Unique name of the stored page.</param>
    /// <returns>The owning child element, or <see langword="null"/> when no matching store page exists.</returns>
    public virtual IDockingElement? FindStorePageElement(DockingLocation location, string uniqueName)
    {
        // Default to not finding the element
        IDockingElement? dockingElement = null;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            dockingElement = this[i]?.FindStorePageElement(location, uniqueName);
            if (dockingElement != null)
            {
                break;
            }
        }

        return dockingElement;
    }

    /// <summary>
    /// Returns the first <see cref="KryptonDockingFloating"/> child that answers for <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name used to locate a floating host.</param>
    /// <returns>A floating element from the subtree, or <see langword="null"/> when none match.</returns>
    public virtual KryptonDockingFloating? FindDockingFloating(string uniqueName)
    {
        // Default to not finding the element
        KryptonDockingFloating? floatingElement = null;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            floatingElement = this[i]?.FindDockingFloating(uniqueName);
            if (floatingElement != null)
            {
                break;
            }
        }

        return floatingElement;
    }

    /// <summary>
    /// Returns the first <see cref="KryptonDockingEdgeDocked"/> child that answers for <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name used to locate a docked edge host.</param>
    /// <returns>A docked-edge element from the subtree, or <see langword="null"/> when none match.</returns>
    public virtual KryptonDockingEdgeDocked? FindDockingEdgeDocked(string uniqueName)
    {
        // Default to not finding the element
        KryptonDockingEdgeDocked? edgeDockedElement = null;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            edgeDockedElement = this[i]?.FindDockingEdgeDocked(uniqueName);
            if (edgeDockedElement != null)
            {
                break;
            }
        }

        return edgeDockedElement;
    }

    /// <summary>
    /// Returns the first <see cref="KryptonDockingEdgeAutoHidden"/> child that answers for <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name used to locate an auto-hidden edge host.</param>
    /// <returns>An auto-hidden-edge element from the subtree, or <see langword="null"/> when none match.</returns>
    public virtual KryptonDockingEdgeAutoHidden? FindDockingEdgeAutoHidden(string uniqueName)
    {
        // Default to not finding the element
        KryptonDockingEdgeAutoHidden? edgeAutoHiddenElement = null;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            edgeAutoHiddenElement = this[i]?.FindDockingEdgeAutoHidden(uniqueName);
            if (edgeAutoHiddenElement != null)
            {
                break;
            }
        }

        return edgeAutoHiddenElement;
    }

    /// <summary>
    /// Returns the first <see cref="KryptonDockingWorkspace"/> child that answers for <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name used to locate a workspace host.</param>
    /// <returns>A workspace element from the subtree, or <see langword="null"/> when none match.</returns>
    public virtual KryptonDockingWorkspace? FindDockingWorkspace(string uniqueName)
    {
        // Default to not finding the element
        KryptonDockingWorkspace? workspaceElement = null;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            workspaceElement = this[i]?.FindDockingWorkspace(uniqueName);
            if (workspaceElement != null)
            {
                break;
            }
        }

        return workspaceElement;
    }

    /// <summary>
    /// Returns the first <see cref="KryptonDockingNavigator"/> child that answers for <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name used to locate a navigator host.</param>
    /// <returns>A navigator element from the subtree, or <see langword="null"/> when none match.</returns>
    public virtual KryptonDockingNavigator? FindDockingNavigator(string uniqueName)
    {
        // Default to not finding the element
        KryptonDockingNavigator? navigatorElement = null;

        // Search all child docking elements
        for (var i = 0; i < Count; i++)
        {
            navigatorElement = this[i]?.FindDockingNavigator(uniqueName);
            if (navigatorElement != null)
            {
                break;
            }
        }

        return navigatorElement;
    }

    /// <summary>
    /// Writes this element and each child as nested XML using <see cref="XmlElementName"/>.
    /// </summary>
    /// <param name="xmlWriter">Destination writer.</param>
    public virtual void SaveElementToXml(XmlWriter xmlWriter)
    {
        // Output docking element
        xmlWriter.WriteStartElement(XmlElementName);
        xmlWriter.WriteAttributeString(@"N", Name);
        xmlWriter.WriteAttributeString(@"C", Count.ToString());

        // Output an element per child
        foreach (IDockingElement child in this)
        {
            child.SaveElementToXml(xmlWriter);
        }

        // Terminate the workspace element
        xmlWriter.WriteFullEndElement();
    }

    /// <summary>
    /// Validates the current XML element, reloads element-specific state, then loads each child in sequence.
    /// </summary>
    /// <param name="xmlReader">Reader positioned on this element.</param>
    /// <param name="pages">Pages available to satisfy saved layout references.</param>
    /// <exception cref="ArgumentException">The XML structure or attributes do not match this element.</exception>
    public virtual void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
    {
        // Is it the expected xml element name?
        if (xmlReader.Name != XmlElementName)
        {
            throw new ArgumentException($@"Element name '{XmlElementName}' was expected but found '{xmlReader.Name}' instead.", nameof(xmlReader));
        }

        // Grab the element attributes
        var elementName = xmlReader.GetAttribute(@"N") ?? string.Empty;
        var elementCount = xmlReader.GetAttribute(@"C") ?? string.Empty;

        // Check the name matches up
        if (elementName != Name)
        {
            throw new ArgumentException($@"Attribute 'N' value '{Name}' was expected but found '{elementName}' instead.", nameof(xmlReader));
        }

        // Let derived class perform element specific persistence
        LoadDockingElement(xmlReader, pages);

        // If there are children then move over them
        var count = int.Parse(elementCount);
        if (count > 0)
        {
            for (var i = 0; i < count; i++)
            {
                // Read to the next element
                if (!xmlReader.Read())
                {
                    throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
                }

                // Find a child docking element with the matching name
                IDockingElement? child = this[xmlReader.GetAttribute(@"N")!];

                // Let derived class perform child element specific processing
                LoadChildDockingElement(xmlReader, pages, child);
            }
        }

        // Read past this element to the end element
        if (!xmlReader.Read())
        {
            throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
        }
    }

    /// <summary>
    /// Throws when any non-<see cref="KryptonStorePage"/> in <paramref name="pages"/> is already hosted in this hierarchy.
    /// </summary>
    /// <param name="pages">Candidate pages for an add or move operation.</param>
    /// <exception cref="ArgumentOutOfRangeException">A page is already present in the docking hierarchy.</exception>
    public void DemandPagesNotBePresent(KryptonPage[]? pages)
    {
        // We need a docking manager in order to perform testing
        DemandDockingManager();

        // We always allow store pages but check that others are not already present in the docking hierarchy
        if (pages != null
            && DockingManager != null
            && pages.Any(page => page is not KryptonStorePage && DockingManager.ContainsPage(page)))
        {
            throw new ArgumentOutOfRangeException(nameof(pages), @"Cannot perform operation with a page that is already present inside docking hierarchy");
        }
    }

    /// <summary>
    /// Throws when no ancestor <see cref="KryptonDockingManager"/> can be reached through <see cref="Parent"/>.
    /// </summary>
    /// <exception cref="ApplicationException">No docking manager is attached to this subtree.</exception>
    public void DemandDockingManager()
    {
        if (!HasDockManager)
        {
            throw new ApplicationException(@"Cannot perform this operation when there is no access to a KryptonDockingManager.");
        }
    }

    /// <summary>
    /// <see langword="true"/> when walking <see cref="Parent"/> reaches a <see cref="KryptonDockingManager"/>.
    /// </summary>
    [Browsable(false)]
    public bool HasDockManager => (DockingManager != null);

    /// <summary>
    /// The nearest <see cref="KryptonDockingManager"/> ancestor, discovered by walking <see cref="Parent"/>.
    /// </summary>
    [Browsable(false)]
    public KryptonDockingManager? DockingManager
    {
        get
        {
            // Searching from this element upwards
            IDockingElement? parent = this;
            while (parent != null)
            {
                // If we find a match then we are done
                if (parent is KryptonDockingManager manager)
                {
                    return manager;
                }

                // Keep going up the parent chain
                parent = parent.Parent;
            }

            // No match found
            return null;
        }
    }

    /// <summary>
    /// Walks <see cref="Parent"/> until an element whose runtime type equals <paramref name="findType"/> is found.
    /// </summary>
    /// <param name="findType">Exact type to match against each ancestor.</param>
    /// <returns>The matching ancestor, or <see langword="null"/> when the chain ends without a match.</returns>
    public IDockingElement? GetParentType(Type findType)
    {
        // Searching from this element upwards
        IDockingElement? parent = this;
        while (parent != null)
        {
            // If we find a match then we are done
            if (parent.GetType() == findType)
            {
                return parent;
            }

            // Keep going up the parent chain
            parent = parent.Parent;
        }

        // No match found
        return null;
    }

    /// <summary>
    /// Number of direct child elements; the default implementation returns zero.
    /// </summary>
    [Browsable(false)]
    public virtual int Count => 0;

    /// <summary>
    /// Direct child at <paramref name="index"/>; the default implementation returns <see langword="null"/>.
    /// </summary>
    /// <param name="index">Zero-based child index.</param>
    /// <returns>The child at <paramref name="index"/>, or <see langword="null"/> when this node has no children.</returns>
    public virtual IDockingElement? this[int index] => null;

    /// <summary>
    /// Direct child whose <see cref="Name"/> equals <paramref name="name"/>; the default implementation returns <see langword="null"/>.
    /// </summary>
    /// <param name="name">Child name to match.</param>
    /// <returns>The named child, or <see langword="null"/> when no child matches.</returns>
    public virtual IDockingElement? this[string name] => null;

    /// <summary>
    /// Yields direct children only; the default implementation yields nothing.
    /// </summary>
    /// <returns>An enumerator over immediate child elements.</returns>
    public virtual IEnumerator<IDockingElement> GetEnumerator()
    {
        yield break;
    }

    /// <summary>
    /// Non-generic enumeration wrapper around <see cref="GetEnumerator"/>.
    /// </summary>
    /// <returns>An enumerator over immediate child elements.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    #region Protected
    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected abstract string XmlElementName { get; }

    /// <summary>
    /// Perform docking element specific actions based on the loading xml.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    protected virtual void LoadDockingElement(XmlReader xmlReader, KryptonPageCollection pages)
    {
    }

    /// <summary>
    /// Perform docking element specific actions for loading a child xml.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    /// <param name="child">Optional reference to existing child docking element.</param>
    protected virtual void LoadChildDockingElement(XmlReader xmlReader,
        KryptonPageCollection pages,
        IDockingElement? child)
    {
        if (child != null)
        {
            child.LoadElementFromXml(xmlReader, pages);
        }
        else
        {
            var nodeName = xmlReader.Name;

            do
            {
                // Read past this element
                if (!xmlReader.Read())
                {
                    throw new ArgumentException(@"An element was expected but could not be read in.", nameof(xmlReader));
                }

                // Finished when we hit the end element matching the incoming one
                if ((xmlReader.NodeType == XmlNodeType.EndElement) && (xmlReader.Name == nodeName))
                {
                    break;
                }
            } while (true);
        }
    }
    #endregion
}