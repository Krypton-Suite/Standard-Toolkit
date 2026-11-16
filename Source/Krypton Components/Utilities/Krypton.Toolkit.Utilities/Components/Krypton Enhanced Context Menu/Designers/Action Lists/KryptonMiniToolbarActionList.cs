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
/// Designer action list for <see cref="KryptonMiniToolbar"/>.
/// </summary>
internal class KryptonMiniToolbarActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonMiniToolbar _toolbar;
    private readonly IComponentChangeService? _changeService;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarActionList"/> class.
    /// </summary>
    /// <param name="owner">Owning designer.</param>
    public KryptonMiniToolbarActionList(KryptonMiniToolbarDesigner owner)
        : base(owner.Component)
    {
        _toolbar = (KryptonMiniToolbar)owner.Component!;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets idle opacity of the selection Mini Toolbar.
    /// </summary>
    public byte IdleOpacity
    {
        get => _toolbar.IdleOpacity;
        set => SetProperty(nameof(KryptonMiniToolbar.IdleOpacity), value);
    }

    /// <summary>
    /// Gets or sets the mouse distance at which the selection Mini Toolbar becomes opaque.
    /// </summary>
    public int ApproachDistance
    {
        get => _toolbar.ApproachDistance;
        set => SetProperty(nameof(KryptonMiniToolbar.ApproachDistance), value);
    }

    /// <summary>
    /// Gets or sets whether the popup draws a shadow.
    /// </summary>
    public bool ShowShadow
    {
        get => _toolbar.ShowShadow;
        set => SetProperty(nameof(KryptonMiniToolbar.ShowShadow), value);
    }

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();
        if (_toolbar == null)
        {
            return items;
        }

        items.Add(new DesignerActionHeaderItem(@"Behavior"));
        items.Add(new DesignerActionPropertyItem(nameof(IdleOpacity), @"Idle Opacity", @"Behavior", @"Idle opacity of the selection Mini Toolbar (0-255)."));
        items.Add(new DesignerActionPropertyItem(nameof(ApproachDistance), @"Approach Distance", @"Behavior", @"Mouse distance in pixels at which the bar becomes fully opaque."));
        items.Add(new DesignerActionPropertyItem(nameof(ShowShadow), @"Show Shadow", @"Visuals", @"Draw a shadow under the Mini Toolbar popup."));
        items.Add(new DesignerActionHeaderItem(@"Data"));
        items.Add(new DesignerActionMethodItem(this, nameof(EditItems), @"Edit Items...", @"Data", @"Configure Mini Toolbar buttons, combos, and galleries.", true));
        return items;
    }

    /// <summary>
    /// Opens the items collection editor.
    /// </summary>
    public void EditItems()
    {
        var editor = TypeDescriptor.GetEditor(_toolbar.Items, typeof(UITypeEditor)) as UITypeEditor;
        if (editor == null)
        {
            return;
        }

        var context = new ItemsContext(_toolbar);
        editor.EditValue(context, context, _toolbar.Items);
        _changeService?.OnComponentChanged(_toolbar, context.PropertyDescriptor, null, _toolbar.Items);
    }

    #endregion

    #region Implementation

    private void SetProperty(string propertyName, object value)
    {
        PropertyDescriptor? descriptor = TypeDescriptor.GetProperties(_toolbar)[propertyName];
        if (descriptor == null)
        {
            return;
        }

        _changeService?.OnComponentChanging(_toolbar, descriptor);
        descriptor.SetValue(_toolbar, value);
        _changeService?.OnComponentChanged(_toolbar, descriptor, null, value);
    }

    #endregion

    #region Nested

    private sealed class ItemsContext : ITypeDescriptorContext
    {
        private readonly KryptonMiniToolbar _toolbar;

        public ItemsContext(KryptonMiniToolbar toolbar) => _toolbar = toolbar;

        public IContainer? Container => _toolbar.Container;

        public object Instance => _toolbar.Items;

        public PropertyDescriptor? PropertyDescriptor =>
            TypeDescriptor.GetProperties(_toolbar)[nameof(KryptonMiniToolbar.Items)];

        public object? GetService(Type serviceType) => _toolbar.Site?.GetService(serviceType);

        public void OnComponentChanged()
        {
        }

        public bool OnComponentChanging() => true;
    }

    #endregion
}
