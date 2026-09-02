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
/// Issue #2117: add and remove named themes in a <c>.kpal</c> pack via
/// <see cref="KryptonPalettePackEditor"/> in Krypton.Toolkit.Utilities.
/// </summary>
public partial class PalettePackEditorDemo : KryptonForm
{
    private string? _samplePackPath;

    public PalettePackEditorDemo()
    {
        InitializeComponent();
        kbtnCreateSample.Click += (_, _) => CreateSamplePack();
        kbtnEditPack.Click += (_, _) => OpenEditor(_samplePackPath);
        kbtnEditEmpty.Click += (_, _) => OpenEditor(null);
    }

    private void CreateSamplePack()
    {
        var folder = Path.Combine(Path.GetTempPath(), @"KryptonPalettePackEditorDemo");
        Directory.CreateDirectory(folder);
        _samplePackPath = Path.Combine(folder, @"sample-pack.kpal");

        var lime = CreateNamedMarker(@"Pack-Lime", Color.Lime);
        var orange = CreateNamedMarker(@"Pack-Orange", Color.Orange);
        var violetPath = Path.Combine(folder, @"Pack-Violet.kpalx");
        var violet = CreateNamedMarker(@"Pack-Violet", Color.BlueViolet);
        try
        {
            lime.Export(Path.Combine(folder, @"Pack-Lime.kpalx"), ignoreDefaults: true, silent: true);
            orange.Export(Path.Combine(folder, @"Pack-Orange.kpalx"), ignoreDefaults: true, silent: true);
            violet.Export(violetPath, ignoreDefaults: true, silent: true);
            KryptonPaletteFile.ExportPack(_samplePackPath, new[] { lime, orange }, ignoreDefaults: true, packName: @"2117-pack-editor");
        }
        finally
        {
            lime.Dispose();
            orange.Dispose();
            violet.Dispose();
        }

        kwlblStatus.Text =
            $@"Sample pack: {_samplePackPath}. Extra .kpalx beside it: {violetPath}. Open the editor, then Add Pack-Violet.kpalx or Remove a theme.";
        kbtnEditPack.Enabled = true;
    }

    private void OpenEditor(string? packPath)
    {
        KryptonPalettePackEditor.Show(this, packPath);
        if (!string.IsNullOrWhiteSpace(packPath) && File.Exists(packPath))
        {
            var names = KryptonPaletteFile.GetThemeNames(packPath!);
            kwlblStatus.Text = $@"Pack now has {names.Length} theme(s): {string.Join(@", ", names)}.";
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
