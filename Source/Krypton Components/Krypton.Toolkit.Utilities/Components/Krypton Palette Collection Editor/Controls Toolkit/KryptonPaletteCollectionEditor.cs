#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Designer-sited dialog for adding <c>.kthemex</c> (and other palette) files to a <c>.ktheme</c> collection
/// and removing named themes. Drop from the toolbox, set <see cref="CollectionPath"/>, then call
/// <see cref="ShowDialog()"/>.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonListBox), "ToolboxBitmaps.KryptonListBox.bmp")]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(CollectionPath))]
[Description(@"Add or remove named themes in a multi-theme .ktheme collection.")]
public class KryptonPaletteCollectionEditor : Component
{
    private string _collectionPath = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPaletteCollectionEditor"/> class.
    /// </summary>
    public KryptonPaletteCollectionEditor()
    {
        Strings = new KryptonPaletteCollectionEditorStrings();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPaletteCollectionEditor"/> class.
    /// </summary>
    /// <param name="container">The container that owns this component.</param>
    public KryptonPaletteCollectionEditor(IContainer container)
        : this()
    {
        ThrowHelper.ThrowIfNull(container);
        container.Add(this);
    }

    /// <summary>
    /// Collection file shown when the dialog opens. May be empty so the user can browse or create one.
    /// </summary>
    [Category(@"Data")]
    [DefaultValue(@"")]
    [Localizable(true)]
    [Description(@"Path of the .ktheme collection to edit.")]
    public string CollectionPath
    {
        get => _collectionPath;
        set => _collectionPath = value ?? string.Empty;
    }

    /// <summary>
    /// Gets the localisable strings used by the collection editor dialog.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Localizable strings used by the collection editor dialog.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonPaletteCollectionEditorStrings Strings { get; }

    private bool ShouldSerializeStrings() => !Strings.IsDefault;

    private void ResetStrings() => Strings.Reset();

    /// <summary>
    /// Shows the collection editor without an owner window.
    /// </summary>
    /// <returns>The dialog result.</returns>
    public DialogResult ShowDialog() => ShowDialog(null);

    /// <summary>
    /// Shows the collection editor owned by <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <returns>The dialog result.</returns>
    public DialogResult ShowDialog(IWin32Window? owner)
    {
        using var form = new VisualKryptonPaletteCollectionEditorForm(CollectionPath, Strings);
        var result = owner is null ? form.ShowDialog() : form.ShowDialog(owner);
        CollectionPath = form.CollectionPath;
        return result;
    }

    /// <summary>
    /// Shows the collection editor with default settings.
    /// </summary>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show() => Show(null, null);

    /// <summary>
    /// Shows the collection editor owned by <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show(IWin32Window? owner) => Show(owner, null);

    /// <summary>
    /// Shows the collection editor owned by <paramref name="owner"/>, opened at <paramref name="collectionPath"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <param name="collectionPath">Existing or new <c>.ktheme</c> path. May be empty.</param>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show(IWin32Window? owner, string? collectionPath)
    {
        using var editor = new KryptonPaletteCollectionEditor();
        if (!string.IsNullOrWhiteSpace(collectionPath))
        {
            editor.CollectionPath = collectionPath!;
        }

        return editor.ShowDialog(owner);
    }
}
