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
/// Provides docking functionality for floating windows.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingFloating : DockingElementClosedCollection
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingFloating class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="ownerForm">Reference to form that will own all the floating windows.</param>
    public KryptonDockingFloating(string name, Form ownerForm)
        : base(name) =>
        OwnerForm = ownerForm ?? throw new ArgumentNullException(nameof(ownerForm));

    #endregion

    #region Public
    /// <summary>
    /// Gets the form the floating windows have as the owner.
    /// </summary>
    public Form OwnerForm { get; }

    /// <summary>
    /// Create and add a new floating window.
    /// </summary>
    /// <returns>Reference to docking element that handles the new workspace.</returns>
    public KryptonDockingFloatingWindow AddFloatingWindow() =>
        // Generate a unique string by creating a GUID
        AddFloatingWindow(CommonHelper.UniqueString);

    /// <summary>
    /// Create and add a new floating window.
    /// </summary>
    /// <param name="name">Initial name of the dockspace element.</param>
    /// <returns>Reference to docking element that handles the new workspace.</returns>
    public KryptonDockingFloatingWindow AddFloatingWindow(string? name) => CreateFloatingWindow(name);

    /// <summary>
    /// Find a floating docking element by searching the hierarchy.
    /// </summary>
    /// <param name="uniqueName">Named page for which a suitable floating element is required.</param>
    /// <returns>KryptonDockingFloating reference if found; otherwise false.</returns>
    public override KryptonDockingFloating FindDockingFloating(string uniqueName) => this;

    /// <summary>
    /// Return the floating window element that contains a placeholder for the named page.
    /// </summary>
    /// <param name="uniqueName">Unique name for search.</param>
    /// <returns>Reference to KryptonDockingFloatingWindow if placeholder found; otherwise null.</returns>
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
    /// 
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