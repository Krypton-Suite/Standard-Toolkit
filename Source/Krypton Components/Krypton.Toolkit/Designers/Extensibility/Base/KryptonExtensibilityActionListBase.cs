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
/// Base class for Krypton action lists using the WinForms Designer Extensibility SDK.
/// Provides common functionality and patterns for all Krypton control action lists.
/// </summary>
public abstract class KryptonExtensibilityActionListBase : DesignerActionList
{
    #region Instance Fields
    private readonly IComponentChangeService? _changeService;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonExtensibilityActionListBase class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    protected KryptonExtensibilityActionListBase(ComponentDesigner owner)
        : base(owner.Component)
    {
        // Cache service used to notify when a property has changed
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the component change service.
    /// </summary>
    protected IComponentChangeService? ChangeService => _changeService;

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

    /// <summary>
    /// Sets a property value with change notification.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="component">The component.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="currentValue">The current value.</param>
    /// <param name="newValue">The new value.</param>
    /// <param name="setter">The setter action.</param>
    protected void SetPropertyValue<T>(IComponent component, string propertyName, T currentValue, T newValue, Action<T> setter)
    {
        if (component != null && !EqualityComparer<T>.Default.Equals(currentValue, newValue))
        {
            NotifyPropertyChanging(component, TypeDescriptor.GetProperties(component)[propertyName]);
            setter(newValue);
            NotifyPropertyChanged(component, TypeDescriptor.GetProperties(component)[propertyName], currentValue, newValue);
        }
    }

    /// <summary>
    /// Sets a property value with change notification using a custom equality comparer.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="component">The component.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="currentValue">The current value.</param>
    /// <param name="newValue">The new value.</param>
    /// <param name="setter">The setter action.</param>
    /// <param name="comparer">The equality comparer.</param>
    protected void SetPropertyValue<T>(IComponent component, string propertyName, T currentValue, T newValue, Action<T> setter, IEqualityComparer<T> comparer)
    {
        if (component != null && !comparer.Equals(currentValue, newValue))
        {
            NotifyPropertyChanging(component, TypeDescriptor.GetProperties(component)[propertyName]);
            setter(newValue);
            NotifyPropertyChanged(component, TypeDescriptor.GetProperties(component)[propertyName], currentValue, newValue);
        }
    }
    #endregion
}
