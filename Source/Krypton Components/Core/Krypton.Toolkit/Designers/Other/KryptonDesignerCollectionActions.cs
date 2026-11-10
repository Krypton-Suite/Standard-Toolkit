#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Opens a property's collection editor from a designer verb or smart tag.
/// </summary>
internal static class KryptonDesignerCollectionActions
{
    /// <summary>
    /// Invokes the <see cref="UITypeEditor"/> for <paramref name="propertyName"/> on
    /// <paramref name="component"/>, using the component site for designer services.
    /// </summary>
    /// <param name="component">Component that owns the collection property.</param>
    /// <param name="propertyName">Name of the collection property to edit.</param>
    public static void EditProperty(IComponent component, string propertyName)
    {
        PropertyDescriptor? descriptor = TypeDescriptor.GetProperties(component)[propertyName];
        if (descriptor == null)
        {
            return;
        }

        var collection = descriptor.GetValue(component);
        if (collection == null)
        {
            return;
        }

        var editor = descriptor.GetEditor(typeof(UITypeEditor)) as UITypeEditor
                     ?? TypeDescriptor.GetEditor(collection, typeof(UITypeEditor)) as UITypeEditor;
        if (editor == null)
        {
            return;
        }

        var context = new CollectionEditorContext(component, descriptor, collection);
        editor.EditValue(context, context, collection);
    }

    private sealed class CollectionEditorContext : ITypeDescriptorContext
    {
        private readonly IComponent _owner;
        private readonly object _collection;

        public CollectionEditorContext(IComponent owner, PropertyDescriptor? descriptor, object collection)
        {
            _owner = owner;
            _collection = collection;
            PropertyDescriptor = descriptor;
        }

        public IContainer? Container => _owner.Site?.Container;

        public object Instance => _collection;

        public PropertyDescriptor? PropertyDescriptor { get; }

        public object? GetService(Type serviceType) => _owner.Site?.GetService(serviceType);

        public void OnComponentChanged()
        {
        }

        public bool OnComponentChanging() => true;
    }
}
