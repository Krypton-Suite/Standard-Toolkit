#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Base class for Krypton.Docking action lists using the WinForms Designer Extensibility SDK.
/// Provides common functionality and patterns for all Krypton.Docking control action lists.
/// </summary>
public abstract class KryptonDockingExtensibilityActionListBase : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonDockingExtensibilityDesignerBase _owner;
    private IComponentChangeService? _changeService;
    #endregion

    #region Protected
    /// <summary>
    /// Gets the component change service.
    /// </summary>
    protected IComponentChangeService? ChangeService => _changeService;

    /// <summary>
    /// Gets the owner designer.
    /// </summary>
    protected KryptonDockingExtensibilityDesignerBase Owner => _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingExtensibilityActionListBase class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    protected KryptonDockingExtensibilityActionListBase(KryptonDockingExtensibilityDesignerBase owner)
        : base(owner.Component)
    {
        _owner = owner;
                _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Sets a property value and notifies of the change.
    /// </summary>
    /// <param name="propertyName">Name of the property to set.</param>
    /// <param name="value">Value to set.</param>
    protected void SetPropertyValue(string propertyName, object value)
    {
        if (Component != null)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(Component)[propertyName];
            if (propertyDescriptor != null)
            {
                var oldValue = propertyDescriptor.GetValue(Component);
                if (!Equals(oldValue, value))
                {
                    propertyDescriptor.SetValue(Component, value);
                    _changeService?.OnComponentChanged(Component, propertyDescriptor, oldValue, value);
                }
            }
        }
    }

    /// <summary>
    /// Gets a property value.
    /// </summary>
    /// <param name="propertyName">Name of the property to get.</param>
    /// <returns>Property value.</returns>
    protected object? GetPropertyValue(string propertyName)
    {
        if (Component != null)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(Component)[propertyName];
            return propertyDescriptor?.GetValue(Component);
        }
        return null;
    }
    #endregion
}
