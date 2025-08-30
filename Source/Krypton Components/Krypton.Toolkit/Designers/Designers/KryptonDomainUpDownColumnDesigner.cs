#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonDomainUpDownColumnDesigner : ComponentDesigner
{
    #region Instance Fields
    private KryptonDataGridViewDomainUpDownColumn? _domainUpDown;
    private IComponentChangeService? _changeService;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // Cast to correct type
        _domainUpDown = component as KryptonDataGridViewDomainUpDownColumn;

        // Get access to the design services
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (_changeService != null)
        {
            _changeService.ComponentRemoving += OnComponentRemoving;
        }
    }
    #endregion

    #region Implementation
    public override ICollection AssociatedComponents => base.AssociatedComponents;

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if ((_domainUpDown != null) && Equals(e.Component, _domainUpDown))
        {
            _changeService?.OnComponentChanging(_domainUpDown, null);
            _domainUpDown.ButtonSpecs.Clear();
            _changeService?.OnComponentChanged(_domainUpDown, null, null, null);
        }
    }
    #endregion
}