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
/// Designer-sited dialog for adding <c>.kpalx</c> (and other palette) files to a <c>.kpal</c> pack
/// and removing named themes. Drop from the toolbox, set <see cref="PackPath"/>, then call
/// <see cref="ShowDialog()"/>.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonListBox), "ToolboxBitmaps.KryptonListBox.bmp")]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(PackPath))]
[Description(@"Add or remove named themes in a multi-theme .kpal pack.")]
public class KryptonPalettePackEditor : Component
{
    private string _packPath = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPalettePackEditor"/> class.
    /// </summary>
    public KryptonPalettePackEditor()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPalettePackEditor"/> class.
    /// </summary>
    /// <param name="container">The container that owns this component.</param>
    public KryptonPalettePackEditor(IContainer container)
        : this()
    {
        ThrowHelper.ThrowIfNull(container);
        container.Add(this);
    }

    /// <summary>
    /// Pack file shown when the dialog opens. May be empty so the user can browse or create one.
    /// </summary>
    [Category(@"Data")]
    [DefaultValue(@"")]
    [Description(@"Path of the .kpal pack to edit.")]
    public string PackPath
    {
        get => _packPath;
        set => _packPath = value ?? string.Empty;
    }

    /// <summary>
    /// Shows the pack editor without an owner window.
    /// </summary>
    /// <returns>The dialog result.</returns>
    public DialogResult ShowDialog() => ShowDialog(null);

    /// <summary>
    /// Shows the pack editor owned by <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <returns>The dialog result.</returns>
    public DialogResult ShowDialog(IWin32Window? owner)
    {
        using var form = new VisualKryptonPalettePackEditorForm(PackPath);
        var result = owner is null ? form.ShowDialog() : form.ShowDialog(owner);
        PackPath = form.PackPath;
        return result;
    }

    /// <summary>
    /// Shows the pack editor with default settings.
    /// </summary>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show() => Show(null, null);

    /// <summary>
    /// Shows the pack editor owned by <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show(IWin32Window? owner) => Show(owner, null);

    /// <summary>
    /// Shows the pack editor owned by <paramref name="owner"/>, opened at <paramref name="packPath"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <param name="packPath">Existing or new <c>.kpal</c> path. May be empty.</param>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show(IWin32Window? owner, string? packPath)
    {
        using var form = new VisualKryptonPalettePackEditorForm(packPath);
        return owner is null ? form.ShowDialog() : form.ShowDialog(owner);
    }
}
