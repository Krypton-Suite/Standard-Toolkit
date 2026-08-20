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
/// Designer for <see cref="KryptonMiniToolbar"/>.
/// </summary>
internal class KryptonMiniToolbarDesigner : ComponentDesigner
{
    #region Instance Fields

    private KryptonMiniToolbar? _toolbar;
    private IComponentChangeService? _changeService;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);
        _toolbar = component as KryptonMiniToolbar;
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
            if (_toolbar != null)
            {
                compound.AddRange(_toolbar.Items);
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
            actionLists.Add(new KryptonMiniToolbarActionList(this));
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
        if (_toolbar == null || !ReferenceEquals(e.Component, _toolbar))
        {
            return;
        }

        for (var i = _toolbar.Items.Count - 1; i >= 0; i--)
        {
            var item = _toolbar.Items[i];
            _toolbar.Items.Remove(item);
            _changeService?.OnComponentChanging(_toolbar, null);
            item.Dispose();
        }
    }

    #endregion
}
