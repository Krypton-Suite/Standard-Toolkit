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
/// Designer action list for <see cref="KryptonEnhancedContextMenu"/>.
/// </summary>
internal class KryptonEnhancedContextMenuActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonEnhancedContextMenu _menu;
    private readonly IComponentChangeService? _changeService;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEnhancedContextMenuActionList"/> class.
    /// </summary>
    /// <param name="owner">Owning designer.</param>
    public KryptonEnhancedContextMenuActionList(KryptonEnhancedContextMenuDesigner owner)
        : base(owner.Component)
    {
        _menu = (KryptonEnhancedContextMenu)owner.Component!;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the Mini Toolbar is shown with the menu.
    /// </summary>
    public bool ShowMiniToolbar
    {
        get => _menu.ShowMiniToolbar;
        set => SetProperty(nameof(KryptonEnhancedContextMenu.ShowMiniToolbar), value);
    }

    /// <summary>
    /// Gets or sets whether a Mini Toolbar command keeps the strip after dismissing the menu list.
    /// </summary>
    public bool KeepMiniToolbarAfterCommand
    {
        get => _menu.KeepMiniToolbarAfterCommand;
        set => SetProperty(nameof(KryptonEnhancedContextMenu.KeepMiniToolbarAfterCommand), value);
    }

    /// <summary>
    /// Gets or sets the Mini Toolbar position.
    /// </summary>
    public KryptonMiniToolbarPosition MiniToolbarPosition
    {
        get => _menu.MiniToolbarPosition;
        set => SetProperty(nameof(KryptonEnhancedContextMenu.MiniToolbarPosition), value);
    }

    /// <summary>
    /// Gets or sets the pixel gap between the Mini Toolbar and the paired context menu.
    /// </summary>
    public int MiniToolbarGap
    {
        get => _menu.MiniToolbarGap;
        set => SetProperty(nameof(KryptonEnhancedContextMenu.MiniToolbarGap), value);
    }

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();
        if (_menu == null)
        {
            return items;
        }

        items.Add(new DesignerActionHeaderItem(@"Behavior"));
        items.Add(new DesignerActionPropertyItem(nameof(ShowMiniToolbar), @"Show Mini Toolbar", @"Behavior", @"Show the Mini Toolbar with the context menu."));
        items.Add(new DesignerActionPropertyItem(nameof(KeepMiniToolbarAfterCommand), @"Keep Mini Toolbar After Command", @"Behavior", @"Dismiss the menu list but keep the Mini Toolbar."));
        items.Add(new DesignerActionPropertyItem(nameof(MiniToolbarPosition), @"Mini Toolbar Position", @"Appearance", @"Position of the Mini Toolbar relative to the menu."));
        items.Add(new DesignerActionPropertyItem(nameof(MiniToolbarGap), @"Mini Toolbar Gap", @"Appearance", @"Pixel gap between the Mini Toolbar and the paired context menu."));
        items.Add(new DesignerActionHeaderItem(@"Data"));
        items.Add(new DesignerActionMethodItem(this, nameof(EditMiniToolbarItems), @"Edit Mini Toolbar Items...", @"Data", @"Configure the Mini Toolbar buttons, combos, and galleries.", true));
        items.Add(new DesignerActionMethodItem(this, nameof(EditMenuItems), @"Edit Menu Items...", @"Data", @"Configure the paired KryptonContextMenu items.", true));
        return items;
    }

    /// <summary>
    /// Opens the Mini Toolbar items collection editor.
    /// </summary>
    public void EditMiniToolbarItems() =>
        EditCollection(_menu.MiniToolbar, nameof(KryptonMiniToolbar.Items), _menu.MiniToolbar.Items);

    /// <summary>
    /// Opens the context menu items collection editor.
    /// </summary>
    public void EditMenuItems() =>
        EditCollection(_menu.Menu, nameof(KryptonContextMenu.Items), _menu.Menu.Items);

    #endregion

    #region Implementation

    private void SetProperty(string propertyName, object value)
    {
        PropertyDescriptor? descriptor = TypeDescriptor.GetProperties(_menu)[propertyName];
        if (descriptor == null)
        {
            return;
        }

        _changeService?.OnComponentChanging(_menu, descriptor);
        descriptor.SetValue(_menu, value);
        _changeService?.OnComponentChanged(_menu, descriptor, null, value);
    }

    private void EditCollection(object owner, string propertyName, object collection)
    {
        var editor = TypeDescriptor.GetEditor(collection, typeof(UITypeEditor)) as UITypeEditor;
        if (editor == null)
        {
            return;
        }

        var context = new CollectionEditorContext(owner, propertyName, collection);
        editor.EditValue(context, context, collection);
        _changeService?.OnComponentChanged(owner, context.PropertyDescriptor, null, collection);
    }

    #endregion

    #region Nested

    private sealed class CollectionEditorContext : ITypeDescriptorContext
    {
        private readonly object _owner;
        private readonly object _collection;

        public CollectionEditorContext(object owner, string propertyName, object collection)
        {
            _owner = owner;
            _collection = collection;
            PropertyDescriptor = TypeDescriptor.GetProperties(owner)[propertyName];
        }

        public IContainer? Container => (_owner as Component)?.Container;

        public object Instance => _collection;

        public PropertyDescriptor? PropertyDescriptor { get; }

        public object? GetService(Type serviceType) => (_owner as IComponent)?.Site?.GetService(serviceType);

        public void OnComponentChanged()
        {
        }

        public bool OnComponentChanging() => true;
    }

    #endregion
}
