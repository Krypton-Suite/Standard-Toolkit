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
/// Contract for nodes in the docking hierarchy tree.
/// </summary>
public interface IDockingElement : IEnumerable<IDockingElement>
{
    /// <summary>
    /// Unique identifier for this node within its parent collection.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Comma-separated ancestry path from the root element to this node.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Walks the hierarchy to locate the element at the given comma-separated path.
    /// </summary>
    /// <param name="path">Comma-separated list of element names to resolve.</param>
    /// <returns>The matching element, or <see langword="null"/> when no node matches the path.</returns>
    IDockingElement? ResolvePath(string path);

    /// <summary>
    /// Parent element in the hierarchy; <see langword="null"/> for the root.
    /// </summary>
    IDockingElement? Parent { get; set; }

    /// <summary>
    /// Broadcasts a docking action to descendants for the named pages.
    /// </summary>
    /// <param name="action">Action requested from matching descendants.</param>
    /// <param name="uniqueNames">Unique names of the pages the action applies to, or <see langword="null"/> when not page-specific.</param>
    void PropogateAction(DockingPropogateAction action, string[]? uniqueNames);

    /// <summary>
    /// Broadcasts a docking action to descendants for the supplied page instances.
    /// </summary>
    /// <param name="action">Action requested from matching descendants.</param>
    /// <param name="pages">Page instances the action applies to.</param>
    void PropogateAction(DockingPropogateAction action, KryptonPage[] pages);

    /// <summary>
    /// Broadcasts a docking action to descendants with an associated integer value.
    /// </summary>
    /// <param name="action">Action requested from matching descendants.</param>
    /// <param name="value">Integer value passed with the action.</param>
    void PropogateAction(DockingPropogateAction action, int value);

    /// <summary>
    /// Queries descendants for a boolean state associated with a page.
    /// </summary>
    /// <param name="state">Boolean state to query.</param>
    /// <param name="uniqueName">Unique name of the page the query applies to.</param>
    /// <returns><see langword="true"/> or <see langword="false"/> when a descendant reports the state; otherwise <see langword="null"/> when no descendant can answer.</returns>
    bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName);

    /// <summary>
    /// Queries descendants to read or update an integer state value.
    /// </summary>
    /// <param name="state">Integer state to query or modify.</param>
    /// <param name="value">Value supplied to descendants and updated when a match is found.</param>
    void PropogateIntState(DockingPropogateIntState state, ref int value);

    /// <summary>
    /// Collects drag-drop targets from descendants for the pages being dragged.
    /// </summary>
    /// <param name="floatingWindow">Floating window being dragged, or <see langword="null"/> when the drag did not originate from a floating window.</param>
    /// <param name="dragData">Pages and context for the drag operation.</param>
    /// <param name="targets">Collection populated with targets discovered in descendants.</param>
    void PropogateDragTargets(KryptonFloatingWindow? floatingWindow,
        PageDragEndData? dragData,
        DragTargetList targets);

    /// <summary>
    /// Locates a page reference in descendants for the given state query.
    /// </summary>
    /// <param name="state">Page lookup requested from descendants.</param>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns>The matching page, or <see langword="null"/> when no descendant hosts that page.</returns>
    KryptonPage? PropogatePageState(DockingPropogatePageState state, string uniqueName);

    /// <summary>
    /// Adds matching pages from descendants into the supplied collection.
    /// </summary>
    /// <param name="state">Page-list query sent to descendants.</param>
    /// <param name="pages">Collection appended by matching descendants.</param>
    void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages);

    /// <summary>
    /// Adds matching workspace cells from descendants into the supplied collection.
    /// </summary>
    /// <param name="state">Cell-list query sent to descendants.</param>
    /// <param name="cells">Collection appended by matching descendants.</param>
    void PropogateCellList(DockingPropogateCellList state, KryptonWorkspaceCellList cells);

    /// <summary>
    /// Returns the docking location of the page with the given unique name.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns>The docking location reported by the hosting descendant.</returns>
    DockingLocation FindPageLocation(string uniqueName);
        
    /// <summary>
    /// Returns the hierarchy node that currently hosts the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to locate.</param>
    /// <returns>The hosting element, or <see langword="null"/> when the page is not in the hierarchy.</returns>
    IDockingElement? FindPageElement(string uniqueName);

    /// <summary>
    /// Returns the element holding the stored placeholder for the named page at the given location.
    /// </summary>
    /// <param name="location">Docking location whose store should be searched.</param>
    /// <param name="uniqueName">Unique name of the page whose store entry is required.</param>
    /// <returns>The element containing the store page, or <see langword="null"/> when no store entry exists.</returns>
    IDockingElement? FindStorePageElement(DockingLocation location, string uniqueName);

    /// <summary>
    /// Searches descendants for a floating element that can host the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to place in a floating window.</param>
    /// <returns>A suitable floating element, or <see langword="null"/> when none is found.</returns>
    KryptonDockingFloating? FindDockingFloating(string uniqueName);

    /// <summary>
    /// Searches descendants for an edge-docked element that can host the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to dock against a control edge.</param>
    /// <returns>A suitable edge-docked element, or <see langword="null"/> when none is found.</returns>
    KryptonDockingEdgeDocked? FindDockingEdgeDocked(string uniqueName);

    /// <summary>
    /// Searches descendants for an auto-hidden edge element that can host the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to auto-hide against a control edge.</param>
    /// <returns>A suitable auto-hidden edge element, or <see langword="null"/> when none is found.</returns>
    KryptonDockingEdgeAutoHidden? FindDockingEdgeAutoHidden(string uniqueName);

    /// <summary>
    /// Searches descendants for a workspace element that can host the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to place in a workspace.</param>
    /// <returns>A suitable workspace element, or <see langword="null"/> when none is found.</returns>
    KryptonDockingWorkspace? FindDockingWorkspace(string uniqueName);

    /// <summary>
    /// Searches descendants for a navigator element that can host the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to place in a navigator.</param>
    /// <returns>A suitable navigator element, or <see langword="null"/> when none is found.</returns>
    KryptonDockingNavigator? FindDockingNavigator(string uniqueName);
        
    /// <summary>
    /// Serializes this element and its descendants to XML.
    /// </summary>
    /// <param name="xmlWriter">Writer that receives the docking configuration.</param>
    void SaveElementToXml(XmlWriter xmlWriter);

    /// <summary>
    /// Restores this element and its descendants from XML.
    /// </summary>
    /// <param name="xmlReader">Reader positioned at the saved element markup.</param>
    /// <param name="pages">Available pages used to resolve saved references.</param>
    void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages);

    /// <summary>
    /// Number of direct child docking elements.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Direct child at the given zero-based index.
    /// </summary>
    /// <param name="index">Zero-based index of the child to return.</param>
    /// <returns>The child at <paramref name="index"/>, or <see langword="null"/> when the index is out of range.</returns>
    IDockingElement? this[int index] { get; }

    /// <summary>
    /// Direct child with the given name.
    /// </summary>
    /// <param name="name">Name of the child to return.</param>
    /// <returns>The named child, or <see langword="null"/> when no child matches <paramref name="name"/>.</returns>
    IDockingElement? this[string name] { get; }
}
#endregion

#region Interface IFloatingMessages
/// <summary>
/// Callback surface for keyboard and mouse messages raised by a floating window during docking drag operations.
/// </summary>
public interface IFloatingMessages
{
    /// <summary>
    /// Notifies that <c>WM_KEYDOWN</c> was received from the floating window.
    /// </summary>
    /// <param name="m">Windows message structure for the key-down event.</param>
    /// <returns><see langword="true"/> to stop further dispatch of the message; otherwise <see langword="false"/>.</returns>
    bool OnKEYDOWN(ref Message m);

    /// <summary>
    /// Notifies that <c>WM_MOUSEMOVE</c> was received from the floating window.
    /// </summary>
    void OnMOUSEMOVE();

    /// <summary>
    /// Notifies that <c>WM_LBUTTONUP</c> was received from the floating window.
    /// </summary>
    void OnLBUTTONUP();
}
#endregion

#region Enum DockingEdge
/// <summary>
/// Edge of a host control used for docking layout.
/// </summary>
public enum DockingEdge
{
    /// <summary>Left side of the host control.</summary>
    Left,

    /// <summary>Right side of the host control.</summary>
    Right,

    /// <summary>Top side of the host control.</summary>
    Top,

    /// <summary>Bottom side of the host control.</summary>
    Bottom
}
#endregion

#region Enum DockingCloseAction
/// <summary>
/// Action taken when a docking page close is requested.
/// </summary>
public enum DockingCloseRequest
{
    /// <summary>No close action is performed.</summary>
    None,

    /// <summary>The page is removed from the docking hierarchy.</summary>
    RemovePage,

    /// <summary>The page is removed from the docking hierarchy and then disposed.</summary>
    RemovePageAndDispose,

    /// <summary>The page is hidden but remains in the hierarchy.</summary>
    HidePage
}
#endregion

#region Enum DockingLocation
/// <summary>
/// Current placement of a page within the docking system.
/// </summary>
public enum DockingLocation
{
    /// <summary>Page is auto-hidden along a control edge.</summary>
    AutoHidden,

    /// <summary>Page is docked along a control edge.</summary>
    Docked,

    /// <summary>Page is hosted inside a floating window.</summary>
    Floating,

    /// <summary>Page is hosted inside a standalone workspace.</summary>
    Workspace,

    /// <summary>Page is hosted inside a standalone navigator.</summary>
    Navigator,

    /// <summary>Page is hosted by a custom docking extension.</summary>
    Custom,

    /// <summary>Page is not present in the docking hierarchy.</summary>
    None
}
#endregion

#region Enum DockingAutoHiddenShowState
/// <summary>
/// Slide animation state of an auto-hidden page panel.
/// </summary>
public enum DockingAutoHiddenShowState
{
    /// <summary>Panel is fully hidden against the control edge.</summary>
    Hidden,

    /// <summary>Panel is animating out from the edge into view.</summary>
    SlidingOut,

    /// <summary>Panel is animating back toward the hidden position.</summary>
    SlidingIn,

    /// <summary>Panel is fully visible along the control edge.</summary>
    Showing
}
#endregion

#region Enum DockingPropogateAction
/// <summary>
/// Action broadcast to descendants during hierarchy updates.
/// </summary>
public enum DockingPropogateAction
{
    /// <summary>No operation.</summary>
    Null,

    /// <summary>Signals the start of a batched hierarchy update.</summary>
    StartUpdate,

    /// <summary>Signals the end of a batched hierarchy update.</summary>
    EndUpdate,

    /// <summary>Shows display elements for the named pages.</summary>
    ShowPages,

    /// <summary>Shows display elements for every page in the hierarchy.</summary>
    ShowAllPages,

    /// <summary>Hides display elements for the named pages.</summary>
    HidePages,

    /// <summary>Hides display elements for every page in the hierarchy.</summary>
    HideAllPages,

    /// <summary>Replaces the named pages with position placeholders.</summary>
    StorePages,

    /// <summary>Replaces every page with position placeholders.</summary>
    StoreAllPages,

    /// <summary>Restores actual pages from stored placeholders.</summary>
    RestorePages,

    /// <summary>Removes auto-hidden store placeholders for the named pages.</summary>
    ClearAutoHiddenStoredPages,

    /// <summary>Removes docked store placeholders for the named pages.</summary>
    ClearDockedStoredPages,

    /// <summary>Removes floating store placeholders for the named pages.</summary>
    ClearFloatingStoredPages,

    /// <summary>Removes filler store placeholders for the named pages.</summary>
    ClearFillerStoredPages,

    /// <summary>Removes all store placeholders for the named pages.</summary>
    ClearStoredPages,

    /// <summary>Removes every store placeholder in the hierarchy.</summary>
    ClearAllStoredPages,

    /// <summary>Removes the named pages from the hierarchy.</summary>
    RemovePages,

    /// <summary>Removes the named pages from the hierarchy and disposes them.</summary>
    RemoveAndDisposePages,

    /// <summary>Removes every page from the hierarchy.</summary>
    RemoveAllPages,

    /// <summary>Removes every page from the hierarchy and disposes them.</summary>
    RemoveAndDisposeAllPages,

    /// <summary>Signals that a configuration load is about to begin.</summary>
    Loading,

    /// <summary>Requests dockspace controls with a matching order value to reposition themselves.</summary>
    RepositionDockspace,

    /// <summary>Notifies descendants that a localized string property changed.</summary>
    StringChanged,

    /// <summary>Requests diagnostic output of current docking contents.</summary>
    DebugOutput
}
#endregion

#region Enum DockingPropogateBoolState
/// <summary>
/// Boolean state queried from descendants.
/// </summary>
public enum DockingPropogateBoolState
{
    /// <summary>Whether a descendant hosts the named page.</summary>
    ContainsPage,

    /// <summary>Whether a descendant holds a stored placeholder for the named page.</summary>
    ContainsStorePage,

    /// <summary>Whether the named page is currently visible.</summary>
    IsPageShowing
}
#endregion

#region Enum DockingPropogateIntState
/// <summary>
/// Integer state queried from descendants.
/// </summary>
public enum DockingPropogateIntState
{
    /// <summary>Ordering index assigned to dockspace controls.</summary>
    DockspaceOrder
}
#endregion

#region Enum DockingPropogatePageState
/// <summary>
/// Page lookup requested from descendants.
/// </summary>
public enum DockingPropogatePageState
{
    /// <summary>Locate the live page instance for the given unique name.</summary>
    PageForUniqueName
}
#endregion

#region Enum DockingPropogatePageList
/// <summary>
/// Page-list query sent to descendants.
/// </summary>
public enum DockingPropogatePageList
{
    /// <summary>Collect every page in the hierarchy.</summary>
    All,

    /// <summary>Collect pages that are docked along control edges.</summary>
    Docked,

    /// <summary>Collect pages that are auto-hidden along control edges.</summary>
    AutoHidden,

    /// <summary>Collect pages hosted in floating windows.</summary>
    Floating,

    /// <summary>Collect filler placeholder pages.</summary>
    Filler
}
#endregion

#region Enum DockingPropogateCellList
/// <summary>
/// Workspace cell-list query sent to descendants.
/// </summary>
public enum DockingPropogateCellList
{
    /// <summary>Collect every workspace cell in the hierarchy.</summary>
    All,

    /// <summary>Collect workspace cells that host docked pages.</summary>
    Docked,

    /// <summary>Collect workspace cells that host floating pages.</summary>
    Floating,

    /// <summary>Collect workspace cells in standalone workspaces.</summary>
    Workspace
}
#endregion
