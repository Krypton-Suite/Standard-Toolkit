#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Designer for <see cref="KryptonRadialMenu"/>.
/// </summary>
internal class KryptonRadialMenuDesigner : ComponentDesigner
{
    #region Instance Fields

    private KryptonRadialMenu? _menu;
    private IComponentChangeService? _changeService;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);
        _menu = component as KryptonRadialMenu;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (_changeService != null)
        {
            _changeService.ComponentRemoving += OnComponentRemoving;
        }
    }

    /// <inheritdoc />
    public override ICollection AssociatedComponents
    {
        get
        {
            var compound = new ArrayList(base.AssociatedComponents);
            if (_menu != null)
            {
                compound.AddRange(_menu.Items);
                foreach (var item in _menu.Items)
                {
                    if (item is KryptonRadialMenuItem commandItem)
                    {
                        compound.AddRange(commandItem.Items);
                    }
                }
            }

            return compound;
        }
    }

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            actionLists.AddRange(base.ActionLists);
            actionLists.Add(new KryptonRadialMenuActionList(this));
            return actionLists;
        }
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && _changeService != null)
        {
            _changeService.ComponentRemoving -= OnComponentRemoving;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Implementation

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_menu == null || !ReferenceEquals(e.Component, _menu))
        {
            return;
        }

        // Detach child items when the menu is removed.
        for (var i = _menu.Items.Count - 1; i >= 0; i--)
        {
            var item = _menu.Items[i];
            _menu.Items.Remove(item);
            _changeService?.OnComponentChanging(_menu, null);
            item.Dispose();
        }
    }

    #endregion
}
