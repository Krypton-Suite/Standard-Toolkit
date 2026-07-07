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
/// Krypton-themed replacement for the designer <c>CollectionForm</c> type used by collection editors.
/// </summary>
public abstract class KryptonDesignerCollectionForm : KryptonForm
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerCollectionForm"/> class.
    /// </summary>
    /// <param name="editor">Reference to owning collection editor.</param>
    protected KryptonDesignerCollectionForm(KryptonDesignerCollectionEditor editor)
    {
        Editor = editor;
        ControlBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the owning collection editor.
    /// </summary>
    protected KryptonDesignerCollectionEditor Editor { get; }

    /// <summary>
    /// Gets the designer context from the owning editor.
    /// </summary>
    public ITypeDescriptorContext? Context => Editor.DesignerContext;

    /// <summary>
    /// Gets or sets the collection instance being edited.
    /// </summary>
    public object? EditValue { get; private set; }

    /// <summary>
    /// Gets or sets the current root items shown in the editor.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object[]? Items { get; protected set; }

    /// <summary>
    /// Initializes the form from the collection instance being edited.
    /// </summary>
    /// <param name="value">Collection instance to edit.</param>
    public void InitializeEditValue(object? value)
    {
        EditValue = value;
        Items = Editor.ExtractItems(value);

        OnEditValueChanged();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Creates a new item for the collection.
    /// </summary>
    /// <param name="itemType">Type of the item to create.</param>
    /// <returns>New item instance.</returns>
    protected object CreateInstance(Type itemType) => Editor.CreateDesignerInstance(itemType);

    /// <summary>
    /// Destroys a collection item instance.
    /// </summary>
    /// <param name="instance">Instance to destroy.</param>
    protected void DestroyInstance(object instance) => Editor.DestroyDesignerInstance(instance);

    /// <summary>
    /// Gets the requested designer service.
    /// </summary>
    /// <param name="serviceType">Type of service to retrieve.</param>
    /// <returns>Service instance if available.</returns>
    protected new object? GetService(Type serviceType) => Editor.GetDesignerService(serviceType);

    /// <summary>
    /// Provides an opportunity to perform processing when a collection value has changed.
    /// </summary>
    protected virtual void OnEditValueChanged()
    {
    }

    /// <summary>
    /// Applies the owning component palette to the dialog and its child controls.
    /// </summary>
    /// <param name="paletteMode">Palette mode to apply.</param>
    /// <param name="customPalette">Optional custom palette.</param>
    protected void ApplyOwnerPalette(PaletteMode paletteMode, KryptonCustomPaletteBase? customPalette)
    {
        PaletteMode = paletteMode;
        if (paletteMode == PaletteMode.Custom)
        {
            LocalCustomPalette = customPalette;
        }

        ApplyPalette(Controls, paletteMode, customPalette);
    }

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        RefreshPropertyGrids(Controls);
    }

    /// <summary>
    /// Applies palette settings to all <see cref="VisualControlBase"/> descendants.
    /// </summary>
    /// <param name="controls">Root control collection.</param>
    /// <param name="paletteMode">Palette mode to apply.</param>
    /// <param name="customPalette">Optional custom palette.</param>
    public static void ApplyPalette(Control.ControlCollection controls, PaletteMode paletteMode,
        KryptonCustomPaletteBase? customPalette)
    {
        foreach (Control control in controls)
        {
            // Let the property grid inherit palette from the form to avoid recreating
            // the inner WinForms grid before layout has positioned it.
            if (control is KryptonPropertyGrid)
            {
                continue;
            }

            if (control is VisualControlBase visualControl)
            {
                visualControl.PaletteMode = paletteMode;
                visualControl.LocalCustomPalette = customPalette;
            }

            if (control.HasChildren)
            {
                ApplyPalette(control.Controls, paletteMode, customPalette);
            }
        }
    }

    private static void RefreshPropertyGrids(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is KryptonPropertyGrid propertyGrid)
            {
                propertyGrid.PerformLayout();
                propertyGrid.Invalidate(true);
            }

            if (control.HasChildren)
            {
                RefreshPropertyGrids(control.Controls);
            }
        }
    }
    #endregion
}

/// <summary>
/// Collection editor that hosts a <see cref="KryptonDesignerCollectionForm"/> with Krypton dialog chrome.
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
    #endregion

    #region Protected
    /// <summary>
    /// Creates the Krypton-themed collection editor form.
    /// </summary>
    /// <returns>Editor form instance.</returns>
    protected abstract KryptonDesignerCollectionForm CreateKryptonDesignerCollectionForm();

    /// <inheritdoc />
    protected override CollectionForm CreateCollectionForm() =>
        throw new NotSupportedException($"{GetType().Name} uses {nameof(KryptonDesignerCollectionForm)}.");

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
