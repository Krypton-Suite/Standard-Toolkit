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
/// Designer for <see cref="KryptonEnhancedContextMenu"/>.
/// </summary>
internal class KryptonEnhancedContextMenuDesigner : ComponentDesigner
{
    #region Instance Fields

    private KryptonEnhancedContextMenu? _menu;
    private IComponentChangeService? _changeService;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);
        _menu = component as KryptonEnhancedContextMenu;
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
                compound.Add(_menu.MiniToolbar);
                compound.AddRange(_menu.MiniToolbar.Items);
                compound.Add(_menu.Menu);
                compound.AddRange(_menu.Menu.Items);
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
            actionLists.Add(new KryptonEnhancedContextMenuActionList(this));
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

        _changeService?.OnComponentChanging(_menu, null);
    }

    #endregion
}
