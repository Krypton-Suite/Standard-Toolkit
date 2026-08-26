#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Designer for <see cref="KryptonMenuBar"/>.
/// </summary>
internal class KryptonMenuBarDesigner : ControlDesigner
{
    #region Instance Fields

    private KryptonMenuBar? _menuBar;
    private IDesignerHost? _designerHost;
    private IComponentChangeService? _changeService;
    private DesignerVerbCollection? _verbs;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);

        Debug.Assert(component != null);

        AutoResizeHandles = true;

        _menuBar = component as KryptonMenuBar;
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;

        if (_changeService != null)
        {
            _changeService.ComponentRemoving += OnComponentRemoving;
        }
    }

    /// <inheritdoc />
    public override DesignerVerbCollection Verbs
    {
        get
        {
            if (_verbs == null)
            {
                _verbs = new DesignerVerbCollection
                {
                    new DesignerVerb(@"Insert Standard Items", OnInsertStandardItems)
                };
            }

            return _verbs;
        }
    }

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            actionLists.AddRange(base.ActionLists);
            actionLists.Add(new KryptonMenuBarActionList(this));
            return actionLists;
        }
    }

    /// <inheritdoc />
    public override ICollection AssociatedComponents
    {
        get
        {
            var compound = new ArrayList(base.AssociatedComponents);
            if (_menuBar != null)
            {
                compound.AddRange(_menuBar.Items);
            }

            return compound;
        }
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && _changeService != null)
            {
                _changeService.ComponentRemoving -= OnComponentRemoving;
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }

    #endregion

    #region Internal

    /// <summary>
    /// Inserts standard File / Edit / Tools / Help items using the designer host when available.
    /// </summary>
    internal static void InsertStandardItems(
        KryptonMenuBar menuBar,
        IDesignerHost? host,
        IComponentChangeService? changeService)
    {
        DesignerTransaction? transaction = null;
        try
        {
            transaction = host?.CreateTransaction(@"Insert Standard Items");
            foreach (var item in KryptonStandardMenuFactory.CreateStandardMenuBarItems())
            {
                menuBar.Items.Add(item);
            }

            changeService?.OnComponentChanged(menuBar, null, null, null);
            transaction?.Commit();
            transaction = null;
        }
        finally
        {
            transaction?.Cancel();
        }
    }

    #endregion

    #region Implementation

    private void OnInsertStandardItems(object? sender, EventArgs e)
    {
        if (_menuBar == null)
        {
            return;
        }

        InsertStandardItems(_menuBar, _designerHost, _changeService);
    }

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_menuBar == null || !Equals(e.Component, _menuBar))
        {
            return;
        }

        for (var i = _menuBar.Items.Count - 1; i >= 0; i--)
        {
            var item = _menuBar.Items[i];
            _menuBar.Items.RemoveAt(i);
            if (item.Site != null)
            {
                _designerHost?.DestroyComponent(item);
            }
        }
    }

    #endregion
}
