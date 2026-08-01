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
public abstract class VisualDesignerCollectionForm : KryptonForm
{
    #region Instance Fields
    private bool _designerDpiConfigured;
    private readonly KryptonDesignerCollectionEditor? _editor;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="VisualDesignerCollectionForm"/> class for the WinForms designer.
    /// </summary>
    protected VisualDesignerCollectionForm()
    {
        // Editor dialogs host content directly on the form surface, not the default internal panel.
        SetInheritedControlOverride();
        ControlBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualDesignerCollectionForm"/> class.
    /// </summary>
    /// <param name="editor">Reference to owning collection editor.</param>
    protected VisualDesignerCollectionForm(KryptonDesignerCollectionEditor editor)
        : this()
    {
        _editor = editor;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the owning collection editor.
    /// </summary>
    protected KryptonDesignerCollectionEditor Editor =>
        _editor ?? throw new InvalidOperationException(@"Collection editor is not available outside a design-time session.");

    /// <summary>
    /// Gets the designer context from the owning editor.
    /// </summary>
    public ITypeDescriptorContext? Context => _editor?.DesignerContext;

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
        if (_editor is null)
        {
            return;
        }

        EditValue = value;
        Items = _editor.ExtractItems(value);

        OnEditValueChanged();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Creates a new item for the collection.
    /// </summary>
    /// <param name="itemType">Type of the item to create.</param>
    /// <returns>New item instance.</returns>
    protected object CreateInstance(Type itemType) =>
        _editor?.CreateDesignerInstance(itemType)
        ?? throw new InvalidOperationException(@"Collection editor is not available outside a design-time session.");

    /// <summary>
    /// Destroys a collection item instance.
    /// </summary>
    /// <param name="instance">Instance to destroy.</param>
    protected void DestroyInstance(object instance) => _editor?.DestroyDesignerInstance(instance);

    /// <summary>
    /// Gets the requested designer service.
    /// </summary>
    /// <param name="serviceType">Type of service to retrieve.</param>
    /// <returns>Service instance if available.</returns>
    protected new object? GetService(Type serviceType) => _editor?.GetDesignerService(serviceType);

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

    /// <summary>
    /// Configures DPI scaling for the designer editor dialog.
    /// </summary>
    protected void ConfigureDesignerDpi()
    {
        if (_designerDpiConfigured)
        {
            return;
        }

        KryptonDesignerEditorDpi.Configure(this);
        _designerDpiConfigured = true;
    }

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        ConfigureDesignerDpi();
        base.OnLoad(e);
    }

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        KryptonDesignerEditorDpi.ApplyOnShown(this);
    }

    /// <summary>
    /// Applies the owning component palette when set; otherwise the manager global palette.
    /// </summary>
    protected void ApplyOwnerPaletteFromContext() =>
        KryptonDesignerEditorTheme.ApplyFromContext(this, Context);

    /// <summary>
    /// Commits the edited items back to the collection instance.
    /// </summary>
    protected void CommitDesignerItems()
    {
        if (Items is not null && _editor is not null)
        {
            EditValue = _editor.ApplyDesignerItems(EditValue, Items);
        }
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
    #endregion
}