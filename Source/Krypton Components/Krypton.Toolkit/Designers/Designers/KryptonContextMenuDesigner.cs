#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonContextMenuDesigner : ComponentDesigner
{
    #region Instance Fields
    private KryptonContextMenu? _contextMenu;
    private IDesignerHost? _designerHost;
    private IComponentChangeService? _changeService;
    private DesignerVerbCollection? _verbs;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);

        Debug.Assert(component != null);

        _contextMenu = component as KryptonContextMenu;
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        _changeService!.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
#if KRYPTON_WINFORMS_DESIGNER_SDK
    public override IReadOnlyCollection<IComponent> AssociatedComponents
#else
    public override ICollection AssociatedComponents
#endif
    {
        get
        {
            var compound = KryptonDesignerSdkCompat.ToArrayList(base.AssociatedComponents);

            if (_contextMenu != null)
            {
                compound.AddRange(_contextMenu.Items);
            }

            return KryptonDesignerSdkCompat.Associated(compound);
        }
    }

    /// <summary>
    /// Gets the design-time verbs shown on the component context menu.
    /// </summary>
    public override DesignerVerbCollection Verbs
    {
        get
        {
            if (_verbs == null)
            {
                _verbs = new DesignerVerbCollection
                {
                    new DesignerVerb(@"Insert Standard Items", OnInsertStandardItems),
                    new DesignerVerb(@"Edit Items...", OnEditItems)
                };
            }

            return _verbs;
        }
    }

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            actionLists.AddRange(base.ActionLists);
            actionLists.Add(new KryptonContextMenuActionList(this));

            return actionLists;
        }
    }
    #endregion

    #region Internal

    /// <summary>
    /// Inserts the standard Edit shortcut-menu items using the designer host when available.
    /// </summary>
    internal static void InsertStandardItems(
        KryptonContextMenu contextMenu,
        IDesignerHost? host,
        IComponentChangeService? changeService)
    {
        DesignerTransaction? transaction = null;
        try
        {
            transaction = host?.CreateTransaction(@"Insert Standard Items");
            contextMenu.Items.Add(KryptonStandardMenuFactory.CreateStandardContextMenuItems());
            changeService?.OnComponentChanged(contextMenu, null, null, null);
            transaction?.Commit();
            transaction = null;
        }
        finally
        {
            transaction?.Cancel();
        }
    }

    /// <summary>
    /// Opens the collection editor for <see cref="KryptonContextMenu.Items"/>.
    /// </summary>
    internal static void EditItems(KryptonContextMenu contextMenu) =>
        KryptonDesignerCollectionActions.EditProperty(contextMenu, nameof(KryptonContextMenu.Items));

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
                _changeService!.ComponentRemoving -= OnComponentRemoving;
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }
    #endregion

    #region Implementation
    private void OnInsertStandardItems(object? sender, EventArgs e)
    {
        if (_contextMenu == null)
        {
            return;
        }

        InsertStandardItems(_contextMenu, _designerHost, _changeService);
    }

    private void OnEditItems(object? sender, EventArgs e)
    {
        if (_contextMenu == null)
        {
            return;
        }

        EditItems(_contextMenu);
    }

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_contextMenu == null || !Equals(e.Component, _contextMenu))
        {
            return;
        }

        var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

        for (var j = _contextMenu.Items.Count - 1; j >= 0; j--)
        {
            var item = _contextMenu.Items[j] as Component;
            _contextMenu.Items.Remove(item);
            host?.DestroyComponent(item);
        }
    }
    #endregion
}
