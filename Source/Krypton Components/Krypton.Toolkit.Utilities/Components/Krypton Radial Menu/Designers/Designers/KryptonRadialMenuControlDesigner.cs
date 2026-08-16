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
/// Designer for <see cref="KryptonRadialMenuControl"/>.
/// </summary>
internal class KryptonRadialMenuControlDesigner : ControlDesigner
{
    #region Instance Fields

    private KryptonRadialMenuControl? _control;
    private IComponentChangeService? _changeService;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);
        _control = component as KryptonRadialMenuControl;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (_changeService != null)
        {
            _changeService.ComponentRemoving += OnComponentRemoving;
        }
    }

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists =>
        new DesignerActionListCollection
        {
            new KryptonRadialMenuControlActionList(this)
        };

    /// <inheritdoc />
    public override ICollection AssociatedComponents
    {
        get
        {
            if (_control == null)
            {
                return base.AssociatedComponents;
            }

            var list = new ArrayList();
            foreach (var item in _control.Items)
            {
                list.Add(item);
                if (item is KryptonRadialMenuItem command)
                {
                    foreach (var child in command.Items)
                    {
                        list.Add(child);
                    }
                }
            }

            return list;
        }
    }

    #endregion

    #region Implementation

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_control == null || !ReferenceEquals(e.Component, _control))
        {
            return;
        }

        for (var i = _control.Items.Count - 1; i >= 0; i--)
        {
            var item = _control.Items[i];
            _control.Items.RemoveAt(i);
            item.Dispose();
        }
    }

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
}
