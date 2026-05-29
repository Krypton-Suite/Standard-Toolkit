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

namespace Krypton.Toolkit;

internal class KryptonContextMenuItemsDesigner : ComponentDesigner
{
    #region Instance Fields
    private KryptonContextMenuItems? _contextMenuItems;
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
        _contextMenuItems = component as KryptonContextMenuItems;

        // Get access to the services
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;

        // We need to know when we are being removed
        _changeService!.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            var compound = new ArrayList(base.AssociatedComponents);

            if (_contextMenuItems != null)
            {
                compound.AddRange(_contextMenuItems.Items);
            }

            return compound;
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Releases all resources used by the component. 
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
                // Unhook from events
                _changeService!.ComponentRemoving -= OnComponentRemoving;
            }
        }
        finally
        {
            // Must let base class do standard stuff
            base.Dispose(disposing);
        }
    }
    #endregion

    #region Implementation
    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our item collection is being removed
        if ((_contextMenuItems != null) && (Equals(e.Component, _contextMenuItems)))
        {
            // Need access to host in order to delete a component
            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            // We need to remove all items from the item collection
            for (var j = _contextMenuItems.Items.Count - 1; j >= 0; j--)
            {
                var item = _contextMenuItems.Items[j] as Component;
                _contextMenuItems.Items.Remove(item);
                host?.DestroyComponent(item);
            }
        }
    }
    #endregion
}