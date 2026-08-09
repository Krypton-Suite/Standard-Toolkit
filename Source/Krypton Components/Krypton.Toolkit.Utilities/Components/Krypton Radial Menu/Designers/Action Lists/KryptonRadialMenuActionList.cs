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
/// Designer action list for <see cref="KryptonRadialMenu"/>.
/// </summary>
internal class KryptonRadialMenuActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonRadialMenu _menu;
    private readonly IComponentChangeService? _changeService;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuActionList"/> class.
    /// </summary>
    /// <param name="owner">Owning designer.</param>
    public KryptonRadialMenuActionList(KryptonRadialMenuDesigner owner)
        : base(owner.Component)
    {
        _menu = (KryptonRadialMenu)owner.Component!;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the outer menu radius.
    /// </summary>
    public int MenuRadius
    {
        get => _menu.MenuRadius;
        set
        {
            _changeService?.OnComponentChanging(_menu, null);
            _menu.MenuRadius = value;
            _changeService?.OnComponentChanged(_menu, null, null, null);
        }
    }

    /// <summary>
    /// Gets or sets the inner radius.
    /// </summary>
    public int InnerRadius
    {
        get => _menu.InnerRadius;
        set
        {
            _changeService?.OnComponentChanging(_menu, null);
            _menu.InnerRadius = value;
            _changeService?.OnComponentChanged(_menu, null, null, null);
        }
    }

    /// <summary>
    /// Gets or sets the display style.
    /// </summary>
    public KryptonRadialMenuDisplayStyle DisplayStyle
    {
        get => _menu.DisplayStyle;
        set
        {
            _changeService?.OnComponentChanging(_menu, null);
            _menu.DisplayStyle = value;
            _changeService?.OnComponentChanged(_menu, null, null, null);
        }
    }

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection
        {
            new DesignerActionHeaderItem(@"Appearance"),
            new DesignerActionPropertyItem(nameof(MenuRadius), @"Menu Radius", @"Appearance", @"Outer radius of the radial menu."),
            new DesignerActionPropertyItem(nameof(InnerRadius), @"Inner Radius", @"Appearance", @"Centre button radius."),
            new DesignerActionPropertyItem(nameof(DisplayStyle), @"Display Style", @"Appearance", @"How text and images are arranged."),
            new DesignerActionHeaderItem(@"Data"),
            new DesignerActionMethodItem(this, nameof(EditItems), @"Edit Items...", @"Data", @"Edit the radial menu items.", true)
        };
        return items;
    }

    /// <summary>
    /// Opens the items collection editor.
    /// </summary>
    public void EditItems()
    {
        var editor = TypeDescriptor.GetEditor(_menu.Items, typeof(UITypeEditor)) as UITypeEditor;
        if (editor == null)
        {
            return;
        }

        var context = new RadialMenuItemsContext(_menu);
        editor.EditValue(context, context, _menu.Items);
    }

    #endregion

    #region Nested

    private sealed class RadialMenuItemsContext(KryptonRadialMenu menu) : ITypeDescriptorContext
    {
        public IContainer? Container => menu.Container;
        public object Instance => menu.Items;
        public PropertyDescriptor? PropertyDescriptor => TypeDescriptor.GetProperties(menu)[nameof(KryptonRadialMenu.Items)];
        public object? GetService(Type serviceType) => menu.Site?.GetService(serviceType);
        public void OnComponentChanged() { }
        public bool OnComponentChanging() => true;
    }

    #endregion
}
