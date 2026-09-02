#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Issue #2117: add and remove named themes in a <c>.ktheme</c> pack via
/// <see cref="KryptonPaletteCollectionEditor"/> in Krypton.Toolkit.Utilities.
/// </summary>
public partial class PaletteCollectionEditorDemo : KryptonForm
{
    private string? _sampleCollectionPath;

    public PaletteCollectionEditorDemo()
    {
        InitializeComponent();
        kbtnCreateSample.Click += (_, _) => CreateSampleCollection();
        kbtnEditCollection.Click += (_, _) => OpenEditor(_sampleCollectionPath);
        kbtnEditEmpty.Click += (_, _) => OpenEditor(null);
    }

    private void CreateSampleCollection()
    {
        var folder = Path.Combine(Path.GetTempPath(), @"KryptonPaletteCollectionEditorDemo");
        Directory.CreateDirectory(folder);
        _sampleCollectionPath = Path.Combine(folder, @"sample-collection.ktheme");

        var lime = CreateNamedMarker(@"Collection-Lime", Color.Lime);
        var orange = CreateNamedMarker(@"Collection-Orange", Color.Orange);
        var violetPath = Path.Combine(folder, @"Collection-Violet.kthemex");
        var violet = CreateNamedMarker(@"Collection-Violet", Color.BlueViolet);
        try
        {
            lime.Export(Path.Combine(folder, @"Collection-Lime.kthemex"), ignoreDefaults: true, silent: true);
            orange.Export(Path.Combine(folder, @"Collection-Orange.kthemex"), ignoreDefaults: true, silent: true);
            violet.Export(violetPath, ignoreDefaults: true, silent: true);
            KryptonPaletteFile.ExportCollection(_sampleCollectionPath, new[] { lime, orange }, ignoreDefaults: true, collectionName: @"2117-collection-editor");
        }
        finally
        {
            lime.Dispose();
            orange.Dispose();
            violet.Dispose();
        }

        kwlblStatus.Text =
            $@"Sample collection: {_sampleCollectionPath}. Extra .kthemex beside it: {violetPath}. Open the editor, then Add Collection-Violet.kthemex or Remove a theme.";
        kbtnEditCollection.Enabled = true;
    }

    private void OpenEditor(string? collectionPath)
    {
        KryptonPaletteCollectionEditor.Show(this, collectionPath);
        if (!string.IsNullOrWhiteSpace(collectionPath) && File.Exists(collectionPath))
        {
            var names = KryptonPaletteFile.GetThemeNames(collectionPath!);
            kwlblStatus.Text = $@"Collection now has {names.Length} theme(s): {string.Join(@", ", names)}.";
        }
    }

    private static KryptonCustomPaletteBase CreateNamedMarker(string name, Color marker)
    {
        var palette = new KryptonCustomPaletteBase();
        palette.SetPaletteName(name);
        palette.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = marker;
        return palette;
    }
}
