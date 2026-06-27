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
/// Root collection element that creates and tracks floating-window children for a single owner form.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingFloating : DockingElementClosedCollection
{
    #region Identity
    /// <summary>
    /// Records <paramref name="ownerForm"/> as the owner form for floating windows created under this element.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="ownerForm">Form assigned as owner for every floating window created under this element.</param>
    /// <exception cref="ArgumentNullException"><paramref name="ownerForm"/> is <see langword="null"/>.</exception>
    public KryptonDockingFloating(string name, Form ownerForm)
        : base(name) =>
        OwnerForm = ownerForm ?? throw new ArgumentNullException(nameof(ownerForm));

    #endregion

    #region Public
    /// <summary>
    /// Form assigned as owner for every floating window created under this element.
    /// </summary>
    public Form OwnerForm { get; }

    /// <summary>
    /// Creates a child floating-window element with an auto-generated name and raises manager customization events.
    /// </summary>
    /// <returns>The new floating-window element added to this collection.</returns>
    public KryptonDockingFloatingWindow AddFloatingWindow() =>
        // Generate a unique string by creating a GUID
        AddFloatingWindow(CommonHelper.UniqueString);

    /// <summary>
    /// Creates a child floating-window element with the supplied name and raises manager customization events.
    /// </summary>
    /// <param name="name">Initial name of the floating-window element; may be <see langword="null"/>.</param>
    /// <returns>The new floating-window element added to this collection.</returns>
    public KryptonDockingFloatingWindow AddFloatingWindow(string? name) => CreateFloatingWindow(name);

    /// <summary>
    /// Always returns this element; the <paramref name="uniqueName"/> argument is not consulted.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page being located.</param>
    /// <returns>This element.</returns>
    public override KryptonDockingFloating FindDockingFloating(string uniqueName) => this;

    /// <summary>
    /// Searches child floating-window elements for one that contains a store-page placeholder for <paramref name="uniqueName"/>.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page whose placeholder is sought.</param>
    /// <returns>The first matching child floating-window element, or <see langword="null"/> when none contain the placeholder.</returns>
    public KryptonDockingFloatingWindow? FloatingWindowForStorePage(string uniqueName)
    {
        // Search all the child docking elements
        foreach (IDockingElement child in this)
        {
            // Only interested in floating window elements
            if (child is KryptonDockingFloatingWindow floatingWindow)
            {
                var ret = floatingWindow.PropogateBoolState(DockingPropogateBoolState.ContainsStorePage, uniqueName);
                if (ret.HasValue && ret.Value)
                {
                    return floatingWindow;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Whether newly created floating windows expose a minimise box; not serialized by the designer.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public bool UseMinimiseBox { get; set; }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DF";

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
        if (child != null)
        {
            child.LoadElementFromXml(xmlReader, pages);
        }
        else
        {
            // Create a new floating window and then reload it
            KryptonDockingFloatingWindow floatingWindow = AddFloatingWindow(xmlReader.GetAttribute(@"N"));
            floatingWindow.LoadElementFromXml(xmlReader, pages);
        }
    }
    #endregion

    #region Implementation
    private KryptonDockingFloatingWindow CreateFloatingWindow(string? name)
    {
        // Create a floatspace and floating window for hosting the floatspace
        var floatSpaceElement = new KryptonDockingFloatspace(@"Floatspace");
        var floatingWindowElement = new KryptonDockingFloatingWindow(name, OwnerForm, floatSpaceElement, UseMinimiseBox);
        floatingWindowElement.Disposed += OnDockingFloatingWindowDisposed;
        InternalAdd(floatingWindowElement);

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Generate events so the floating window/dockspace appearance can be customized
            var floatingWindowArgs = new FloatingWindowEventArgs(floatingWindowElement.FloatingWindow, floatingWindowElement);
            var floatSpaceArgs = new FloatspaceEventArgs(floatSpaceElement.FloatspaceControl, floatSpaceElement);
            dockingManager.RaiseFloatingWindowAdding(floatingWindowArgs);
            dockingManager.RaiseFloatspaceAdding(floatSpaceArgs);
        }

        return floatingWindowElement;
    }

    private void OnDockingFloatingWindowDisposed(object? sender, EventArgs e)
    {
        // Cast to correct type and unhook event handlers so garbage collection can occur
        var floatingWindowElement = sender as KryptonDockingFloatingWindow ?? throw new ArgumentNullException(nameof(sender));
        floatingWindowElement.Disposed -= OnDockingFloatingWindowDisposed;

        // Remove the element from our child collection as it is no longer valid
        InternalRemove(floatingWindowElement);
    }
    #endregion
}