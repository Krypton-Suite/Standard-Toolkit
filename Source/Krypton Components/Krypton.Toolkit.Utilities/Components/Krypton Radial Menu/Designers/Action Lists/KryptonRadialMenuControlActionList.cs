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
/// Designer action list for <see cref="KryptonRadialMenuControl"/>.
/// </summary>
internal class KryptonRadialMenuControlActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonRadialMenuControl _control;
    private readonly IComponentChangeService? _changeService;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuControlActionList"/> class.
    /// </summary>
    /// <param name="owner">Owning designer.</param>
    public KryptonRadialMenuControlActionList(KryptonRadialMenuControlDesigner owner)
        : base(owner.Component)
    {
        _control = (KryptonRadialMenuControl)owner.Component!;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the outer menu radius.
    /// </summary>
    public int MenuRadius
    {
        get => _control.MenuRadius;
        set
        {
            _changeService?.OnComponentChanging(_control, null);
            _control.MenuRadius = value;
            _changeService?.OnComponentChanged(_control, null, null, null);
        }
    }

    /// <summary>
    /// Gets or sets the inner radius.
    /// </summary>
    public int InnerRadius
    {
        get => _control.InnerRadius;
        set
        {
            _changeService?.OnComponentChanging(_control, null);
            _control.InnerRadius = value;
            _changeService?.OnComponentChanged(_control, null, null, null);
        }
    }

    /// <summary>
    /// Gets or sets the uniform scale factor.
    /// </summary>
    public float Scale
    {
        get => _control.Scale;
        set
        {
            _changeService?.OnComponentChanging(_control, null);
            _control.Scale = value;
            _changeService?.OnComponentChanged(_control, null, null, null);
        }
    }

    /// <summary>
    /// Gets or sets the display style.
    /// </summary>
    public KryptonRadialMenuDisplayStyle DisplayStyle
    {
        get => _control.DisplayStyle;
        set
        {
            _changeService?.OnComponentChanging(_control, null);
            _control.DisplayStyle = value;
            _changeService?.OnComponentChanged(_control, null, null, null);
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
            new DesignerActionPropertyItem(nameof(Scale), @"Scale", @"Appearance", @"Uniform scale factor (0.5–3)."),
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
        var editor = TypeDescriptor.GetEditor(_control.Values.Items, typeof(UITypeEditor)) as UITypeEditor;
        if (editor == null)
        {
            return;
        }

        var context = new RadialMenuControlItemsContext(_control);
        editor.EditValue(context, context, _control.Values.Items);
    }

    #endregion

    #region Nested

    private sealed class RadialMenuControlItemsContext(KryptonRadialMenuControl control) : ITypeDescriptorContext
    {
        public IContainer? Container => control.Container;
        public object Instance => control.Values.Items;
        public PropertyDescriptor? PropertyDescriptor => TypeDescriptor.GetProperties(control)[nameof(KryptonRadialMenuControl.Values.Items)];
        public object? GetService(Type serviceType) => control.Site?.GetService(serviceType);
        public void OnComponentChanged() { }
        public bool OnComponentChanging() => true;
    }

    #endregion
}
