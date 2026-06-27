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

#region Interface IDockingElement
/// <summary>
/// Contract for nodes in the docking element tree used to locate pages, propagate layout actions, and persist layout.
/// </summary>
public interface IDockingElement : IEnumerable<IDockingElement>
{
    /// <summary>
    /// Unique name that identifies this element within its parent's collection.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Comma-separated chain of element names from the root to this node.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Walks the hierarchy using a comma-separated name path and returns the matching element.
    /// </summary>
    /// <param name="path">Comma-separated list of names to resolve.</param>
    /// <returns>The matching element, or null when no node on the path exists.</returns>
    IDockingElement? ResolvePath(string path);

    /// <summary>
    /// Parent element in the hierarchy, or null when this node is the root.
    /// </summary>
    IDockingElement? Parent { get; set; }

    /// <summary>
    /// Dispatches a layout or lifecycle action for the named pages to descendants.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
    void PropogateAction(DockingPropogateAction action, string[]? uniqueNames);

    /// <summary>
    /// Dispatches a layout or lifecycle action for the supplied pages to descendants.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="pages">Array of pages the action relates to.</param>
    void PropogateAction(DockingPropogateAction action, KryptonPage[] pages);

    /// <summary>
    /// Dispatches a layout action that carries an integer parameter to descendants.
    /// </summary>
    /// <param name="action">Action that is requested to be performed.</param>
    /// <param name="value">Integer value associated with the request.</param>
    void PropogateAction(DockingPropogateAction action, int value);

    /// <summary>
    /// Queries descendants for a boolean flag about a page.
    /// </summary>
    /// <param name="state">Boolean state that is requested to be queried.</param>
    /// <param name="uniqueName">Unique name of the page the request relates to.</param>
    /// <returns>True or false when a descendant answers; otherwise null when no element can answer.</returns>
    bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName);

    /// <summary>
    /// Queries descendants for an integer value and updates the reference when a match is found.
    /// </summary>
    /// <param name="state">Integer state that is requested to be queried.</param>
    /// <param name="value">Seed value on input; updated when a descendant supplies a value.</param>
    void PropogateIntState(DockingPropogateIntState state, ref int value);

    /// <summary>
    /// Collects drop targets offered by descendants for a drag operation.
    /// </summary>
    /// <param name="floatingWindow">Reference to window being dragged.</param>
    /// <param name="dragData">Set of pages being dragged.</param>
    /// <param name="targets">Collection of drag targets.</param>
    void PropogateDragTargets(KryptonFloatingWindow? floatingWindow,
        PageDragEndData? dragData,
        DragTargetList targets);

    /// <summary>
    /// Locates a page reference matching the query for the named page.
    /// </summary>
    /// <param name="state">Request that should result in a page reference if found.</param>
    /// <param name="uniqueName">Unique name of the page the request relates to.</param>
    /// <returns>Reference to page that matches the request; otherwise null.</returns>
    KryptonPage? PropogatePageState(DockingPropogatePageState state, string uniqueName);

    /// <summary>
    /// Appends matching pages from descendants into the supplied collection.
    /// </summary>
    /// <param name="state">Request that should result in pages collection being modified.</param>
    /// <param name="pages">Pages collection for modification by the docking elements.</param>
    void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages);

    /// <summary>
    /// Appends matching workspace cells from descendants into the supplied collection.
    /// </summary>
    /// <param name="state">Request that should result in the cells collection being modified.</param>
    /// <param name="cells">Cells collection for modification by the docking elements.</param>
    void PropogateCellList(DockingPropogateCellList state, KryptonWorkspaceCellList cells);

    /// <summary>
    /// Determines where the named page currently resides in the docking layout.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page.</param>
    /// <returns>Enumeration value indicating docking location.</returns>
    DockingLocation FindPageLocation(string uniqueName);
        
    /// <summary>
    /// Returns the element that currently hosts the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page.</param>
    /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
    IDockingElement? FindPageElement(string uniqueName);

    /// <summary>
    /// Returns the element holding the placeholder for the named page at the given location.
    /// </summary>
    /// <param name="location">Location to be searched.</param>
    /// <param name="uniqueName">Unique name of the page to be found.</param>
    /// <returns>IDockingElement reference if store page is found; otherwise null.</returns>
    IDockingElement? FindStorePageElement(DockingLocation location, string uniqueName);

    /// <summary>
    /// Returns the floating host for the named page, or null if none exists.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable floating element is required.</param>
    /// <returns>KryptonDockingFloating reference if found; otherwise null.</returns>
    KryptonDockingFloating? FindDockingFloating(string uniqueName);

    /// <summary>
    /// Returns the edge-docked host for the named page, or null if none exists.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable docking edge element is required.</param>
    /// <returns>KryptonDockingEdgeDocked reference if found; otherwise null.</returns>
    KryptonDockingEdgeDocked? FindDockingEdgeDocked(string uniqueName);

    /// <summary>
    /// Returns the auto-hidden edge host for the named page, or null if none exists.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable auto hidden edge element is required.</param>
    /// <returns>KryptonDockingEdgeAutoHidden reference if found; otherwise null.</returns>
    KryptonDockingEdgeAutoHidden? FindDockingEdgeAutoHidden(string uniqueName);

    /// <summary>
    /// Returns the workspace host for the named page, or null if none exists.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable workspace element is required.</param>
    /// <returns>KryptonDockingWorkspace reference if found; otherwise null.</returns>
    KryptonDockingWorkspace? FindDockingWorkspace(string uniqueName);

    /// <summary>
    /// Returns the navigator host for the named page, or null if none exists.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable navigator element is required.</param>
    /// <returns>KryptonDockingNavigator reference if found; otherwise null.</returns>
    KryptonDockingNavigator? FindDockingNavigator(string uniqueName);
        
    /// <summary>
    /// Serializes this element and its children to XML.
    /// </summary>
    /// <param name="xmlWriter">Xml writer object.</param>
    void SaveElementToXml(XmlWriter xmlWriter);

    /// <summary>
    /// Restores this element and its children from XML using the supplied page collection.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages);

    /// <summary>
    /// Number of immediate child elements.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Child element at the zero-based index.
    /// </summary>
    /// <param name="index">Index.</param>
    /// <returns>Docking element at specified index.</returns>
    IDockingElement? this[int index] { get; }

    /// <summary>
    /// Child element with the specified name.
    /// </summary>
    /// <param name="name">Name of element.</param>
    /// <returns>Docking element with specified name.</returns>
    IDockingElement? this[string name] { get; }
}
#endregion

#region Interface IFloatingMessages
/// <summary>
/// Callback surface for keyboard and mouse input routed from a floating drag window.
/// </summary>
public interface IFloatingMessages
{
    /// <summary>
    /// Called when WM_KEYDOWN is received; return true to suppress further handling.
    /// </summary>
    /// <returns>True to eat message; otherwise false.</returns>
    bool OnKEYDOWN(ref Message m);

    /// <summary>
    /// Called when WM_MOUSEMOVE is received during a floating drag.
    /// </summary>
    void OnMOUSEMOVE();

    /// <summary>
    /// Called when WM_LBUTTONUP is received to complete a floating drag.
    /// </summary>
    void OnLBUTTONUP();
}
#endregion

#region Enum DockingEdge
/// <summary>
/// Edge of a host control where docking layout is applied.
/// </summary>
public enum DockingEdge
{
    /// <summary>Dock pages along the left side of the host control.</summary>
    Left,

    /// <summary>Dock pages along the right side of the host control.</summary>
    Right,

    /// <summary>Dock pages along the top side of the host control.</summary>
    Top,

    /// <summary>Dock pages along the bottom side of the host control.</summary>
    Bottom
}
#endregion

#region Enum DockingCloseAction
/// <summary>
/// Outcome applied when a docking close is requested for a page.
/// </summary>
public enum DockingCloseRequest
{
    /// <summary>Leave the page in place when a close is requested.</summary>
    None,

    /// <summary>Remove the page from the docking hierarchy without disposing it.</summary>
    RemovePage,

    /// <summary>Remove the page from the hierarchy and dispose it afterward.</summary>
    RemovePageAndDispose,

    /// <summary>Hide the page while keeping it in the hierarchy.</summary>
    HidePage
}
#endregion

#region Enum DockingLocation
/// <summary>
/// Where a page currently resides within the docking layout.
/// </summary>
public enum DockingLocation
{
    /// <summary>Page is auto hidden against a control edge.</summary>
    AutoHidden,

    /// <summary>Page is docked against a control edge.</summary>
    Docked,

    /// <summary>Page is inside a floating window.</summary>
    Floating,

    /// <summary>Page is inside a standalone workspace.</summary>
    Workspace,

    /// <summary>Page is inside a standalone navigator.</summary>
    Navigator,

    /// <summary>Page is hosted by a custom extension element.</summary>
    Custom,

    /// <summary>Page is not present in the docking hierarchy.</summary>
    None
}
#endregion

#region Enum DockingAutoHiddenShowState
/// <summary>
/// Sliding visibility state of an auto-hidden page.
/// </summary>
public enum DockingAutoHiddenShowState
{
    /// <summary>
    /// Auto-hidden page is fully hidden.
    /// </summary>
    Hidden,

    /// <summary>
    /// Auto-hidden page is sliding out into view.
    /// </summary>
    SlidingOut,

    /// <summary>
    /// Auto-hidden page is sliding back to be hidden.
    /// </summary>
    SlidingIn,

    /// <summary>
    /// Auto-hidden page is fully visible.
    /// </summary>
    Showing
}
#endregion

#region Enum DockingPropogateAction
/// <summary>
/// Action broadcast through the docking element hierarchy.
/// </summary>
public enum DockingPropogateAction
{
    /// <summary>No operation.</summary>
    Null,

    /// <summary>Batch update is starting; defer layout until EndUpdate.</summary>
    StartUpdate,

    /// <summary>Batch update has ended; resume layout.</summary>
    EndUpdate,

    /// <summary>Show display elements for the named pages.</summary>
    ShowPages,

    /// <summary>Show display elements for every page.</summary>
    ShowAllPages,

    /// <summary>Hide display elements for the named pages.</summary>
    HidePages,

    /// <summary>Hide display elements for every page.</summary>
    HideAllPages,

    /// <summary>Replace the named pages with position placeholders.</summary>
    StorePages,

    /// <summary>Replace every page with position placeholders.</summary>
    StoreAllPages,

    /// <summary>Restore actual pages from position placeholders.</summary>
    RestorePages,

    /// <summary>Remove auto-hidden store pages for the named pages.</summary>
    ClearAutoHiddenStoredPages,

    /// <summary>Remove docked store pages for the named pages.</summary>
    ClearDockedStoredPages,

    /// <summary>Remove floating store pages for the named pages.</summary>
    ClearFloatingStoredPages,

    /// <summary>Remove filler store pages for the named pages.</summary>
    ClearFillerStoredPages,

    /// <summary>Remove all stored pages for the named pages.</summary>
    ClearStoredPages,

    /// <summary>Remove every stored page.</summary>
    ClearAllStoredPages,

    /// <summary>Remove all details of the named pages.</summary>
    RemovePages,

    /// <summary>Remove the named pages and dispose them.</summary>
    RemoveAndDisposePages,

    /// <summary>Remove all details of every page.</summary>
    RemoveAllPages,

    /// <summary>Remove every page and dispose them.</summary>
    RemoveAndDisposeAllPages,

    /// <summary>Layout load is about to begin.</summary>
    Loading,

    /// <summary>Reposition dockspace controls that share the ordering value.</summary>
    RepositionDockspace,

    /// <summary>A named string property changed and should be refreshed.</summary>
    StringChanged,

    /// <summary>Emit debug output describing docking contents.</summary>
    DebugOutput
}
#endregion

#region Enum DockingPropogateBoolState
/// <summary>
/// Boolean query broadcast through the docking element hierarchy.
/// </summary>
public enum DockingPropogateBoolState
{
    /// <summary>Whether a descendant contains the named page.</summary>
    ContainsPage,

    /// <summary>Whether a descendant contains a store page for the named page.</summary>
    ContainsStorePage,

    /// <summary>Whether the named page is currently showing.</summary>
    IsPageShowing
}
#endregion

#region Enum DockingPropogateIntState
/// <summary>
/// Integer query broadcast through the docking element hierarchy.
/// </summary>
public enum DockingPropogateIntState
{
    /// <summary>Control ordering value used by dockspace controls.</summary>
    DockspaceOrder
}
#endregion

#region Enum DockingPropogatePageState
/// <summary>
/// Page lookup request broadcast through the docking element hierarchy.
/// </summary>
public enum DockingPropogatePageState
{
    /// <summary>Locate the page instance for the given unique name.</summary>
    PageForUniqueName
}
#endregion

#region Enum DockingPropogatePageList
/// <summary>
/// Page list filter applied when collecting pages from the hierarchy.
/// </summary>
public enum DockingPropogatePageList
{
    /// <summary>Collect every page.</summary>
    All,

    /// <summary>Collect docked pages only.</summary>
    Docked,

    /// <summary>Collect auto-hidden pages only.</summary>
    AutoHidden,

    /// <summary>Collect floating pages only.</summary>
    Floating,

    /// <summary>Collect filler pages only.</summary>
    Filler
}
#endregion

#region Enum DockingPropogateCellList
/// <summary>
/// Workspace cell filter applied when collecting cells from the hierarchy.
/// </summary>
public enum DockingPropogateCellList
{
    /// <summary>Collect every workspace cell.</summary>
    All,

    /// <summary>Collect docked workspace cells only.</summary>
    Docked,

    /// <summary>Collect floating workspace cells only.</summary>
    Floating,

    /// <summary>Collect standalone workspace cells only.</summary>
    Workspace
}
#endregion
