#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection editor that hosts a <see cref="VisualDesignerCollectionForm"/> with Krypton dialog chrome.
/// </summary>
public abstract class KryptonDesignerCollectionEditor : CollectionEditor
{
    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerCollectionEditor"/> class.
    /// </summary>
    /// <param name="type">Type of collection to edit.</param>
    protected KryptonDesignerCollectionEditor(Type type)
        : base(type)
    {
    }
    
    #endregion

    #region Internal
        /// <summary>
        /// Gets the designer context for the current edit session.
        /// </summary>
        internal ITypeDescriptorContext? DesignerContext => Context;

        /// <summary>
        /// Extracts the items from the collection instance being edited.
        /// </summary>
        /// <param name="editValue">Collection instance.</param>
        /// <returns>Items in the collection.</returns>
        internal object[]? ExtractItems(object? editValue) => editValue is null ? null : GetItems(editValue);

        /// <summary>
        /// Creates a new collection item instance.
        /// </summary>
        /// <param name="itemType">Type of item to create.</param>
        /// <returns>New item instance.</returns>
        internal object CreateDesignerInstance(Type itemType) => CreateInstance(itemType);

        /// <summary>
        /// Destroys a collection item instance.
        /// </summary>
        /// <param name="instance">Instance to destroy.</param>
        internal void DestroyDesignerInstance(object instance) => DestroyInstance(instance);

        /// <summary>
        /// Gets the requested designer service.
        /// </summary>
        /// <param name="serviceType">Type of service to retrieve.</param>
        /// <returns>Service instance if available.</returns>
        internal object? GetDesignerService(Type serviceType) => Context?.GetService(serviceType);

        /// <summary>
        /// Writes the edited items into the collection instance.
        /// </summary>
        /// <param name="editValue">Collection instance.</param>
        /// <param name="items">Updated items.</param>
        /// <returns>Updated collection instance.</returns>
        internal object? ApplyDesignerItems(object? editValue, object[] items) => SetItems(editValue, items);

        /// <summary>
        /// Gets the display text for a collection item.
        /// </summary>
        /// <param name="value">Collection item.</param>
        /// <returns>Display text.</returns>
        internal string GetDesignerDisplayText(object? value) => GetDisplayText(value!);

        /// <summary>
        /// Gets the item types that can be created in the collection editor.
        /// </summary>
        /// <returns>Creatable item types.</returns>
        internal Type[] GetDesignerNewItemTypes() => CreateNewItemTypes();

        /// <summary>
        /// Gets the collection item type edited by this editor.
        /// </summary>
        internal Type DesignerCollectionItemType => CollectionType;
    
        #endregion

    #region Protected
        /// <summary>
        /// Creates the Krypton-themed collection editor form.
        /// </summary>
        /// <returns>Editor form instance.</returns>
        protected abstract VisualDesignerCollectionForm CreateKryptonDesignerCollectionForm();

        /// <inheritdoc />
        protected override CollectionForm CreateCollectionForm() =>
            throw new NotSupportedException($"{GetType().Name} uses {nameof(VisualDesignerCollectionForm)}.");

        /// <inheritdoc />
        public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
        {
            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService editorService)
            {
                using var form = CreateKryptonDesignerCollectionForm();
                form.InitializeEditValue(value);
                if (editorService.ShowDialog(form) == DialogResult.OK)
                {
                    value = form.EditValue;
                }
            }

            return value;
        }
        #endregion
}
