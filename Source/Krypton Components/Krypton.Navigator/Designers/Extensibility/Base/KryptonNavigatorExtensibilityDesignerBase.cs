#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Base class for Krypton.Navigator designers using the WinForms Designer Extensibility SDK.
/// Provides common functionality and patterns for all Krypton.Navigator control designers.
/// </summary>
public abstract class KryptonNavigatorExtensibilityDesignerBase : ComponentDesigner
{
    #region Instance Fields
    private IComponentChangeService? _changeService;
    private ISelectionService? _selectionService;
    private IDesignerHost? _designerHost;
    #endregion

    #region Protected
    /// <summary>
    /// Gets the component change service.
    /// </summary>
    protected IComponentChangeService? ChangeService => _changeService;

    /// <summary>
    /// Gets the selection service.
    /// </summary>
    protected ISelectionService? SelectionService => _selectionService;

    /// <summary>
    /// Gets the designer host.
    /// </summary>
    protected IDesignerHost? DesignerHost => _designerHost;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The component to initialize the designer with.</param>
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);

        // Get the design-time services
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
    }

    /// <summary>
    /// Gets the action lists for the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            AddActionLists(actionLists);
            return actionLists;
        }
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Adds action lists to the collection. Override to provide custom action lists.
    /// </summary>
    /// <param name="actionLists">The action list collection to add to.</param>
    protected virtual void AddActionLists(DesignerActionListCollection actionLists)
    {
        // Base implementation does nothing - override in derived classes
    }

    /// <summary>
    /// Notifies that a property has changed.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    protected void NotifyPropertyChanged(string propertyName)
    {
        _changeService?.OnComponentChanged(Component, null, null, null);
    }
    #endregion
}
