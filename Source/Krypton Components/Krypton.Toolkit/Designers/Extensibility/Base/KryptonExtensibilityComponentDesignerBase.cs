#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base class for Krypton component designers using the WinForms Designer Extensibility SDK.
/// Provides common functionality for non-visual components.
/// </summary>
public abstract class KryptonExtensibilityComponentDesignerBase : ComponentDesigner
{
    #region Instance Fields
    private IComponentChangeService? _changeService;
    private ISelectionService? _selectionService;
    private IDesignerHost? _designerHost;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate with the designer.</param>
    public override void Initialize(IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // Get access to the design services
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;

        // Hook into component change events
        if (_changeService != null)
        {
            _changeService.ComponentRemoving += OnComponentRemoving;
        }

        // Initialize the specific designer
        if (component != null)
        {
            InitializeDesigner(component);
        }
    }

    /// <summary>
    /// Releases the resources used by the designer.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from component change events
            if (_changeService != null)
            {
                _changeService.ComponentRemoving -= OnComponentRemoving;
            }
        }

        base.Dispose(disposing);
    }
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

    /// <summary>
    /// Initializes the specific designer implementation.
    /// Override this method to provide control-specific initialization.
    /// </summary>
    /// <param name="component">The component being designed.</param>
    protected abstract void InitializeDesigner(IComponent component);

    /// <summary>
    /// Handles component removal events.
    /// Override this method to provide control-specific cleanup.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A ComponentEventArgs that contains the event data.</param>
    protected virtual void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // Default implementation does nothing
        // Override in derived classes for specific cleanup
    }

    /// <summary>
    /// Notifies that a property has changed.
    /// </summary>
    /// <param name="component">The component that changed.</param>
    /// <param name="member">The member that changed.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected void NotifyPropertyChanged(IComponent component, MemberDescriptor? member, object? oldValue, object? newValue)
    {
        _changeService?.OnComponentChanged(component, member, oldValue, newValue);
    }

    /// <summary>
    /// Notifies that a property is changing.
    /// </summary>
    /// <param name="component">The component that is changing.</param>
    /// <param name="member">The member that is changing.</param>
    protected void NotifyPropertyChanging(IComponent component, MemberDescriptor? member)
    {
        _changeService?.OnComponentChanging(component, member);
    }
    #endregion
}
