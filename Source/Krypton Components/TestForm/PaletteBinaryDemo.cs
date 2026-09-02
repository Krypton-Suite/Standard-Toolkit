#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;

using Krypton.Themes;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demo for issue #2117: save and load custom palettes as <c>.kpalx</c> XML
/// (same document as legacy <c>.xml</c>), plus an optional native <c>.kpal</c> persist stream.
/// </summary>
public sealed class PaletteBinaryDemo : KryptonForm
{
    private readonly KryptonManager _manager = new();
    private KryptonCustomPaletteBase _palette = new();
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonListBox _lstLog;
    private readonly PaletteMode _savedPaletteMode;
    private readonly KryptonCustomPaletteBase? _savedCustomPalette;
    private readonly KryptonComboBox _cboExtraTheme;
    private readonly KryptonComboBox _cboPackTheme;
    private readonly KryptonPaletteFileComboBox _fileCombo;
    private readonly KryptonPaletteFileTreeView _fileTree;
    private readonly KryptonPaletteFileListBox _fileList;
    private readonly PaletteMode[] _extraModes;
    private string? _lastDirectory;
    private string? _packPath;

    public PaletteBinaryDemo()
    {
        Text = @"Feature #2117 - Palette binary save/load";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(960, 620);
        MinimumSize = new Size(840, 520);

        _savedPaletteMode = KryptonManager.CurrentGlobalPaletteMode;
        _savedCustomPalette = _manager.GlobalCustomPalette;
        KryptonPaletteFile.EnsureShellAssociations();

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 128,
            Text =
                @"How to test issue #2117:" + Environment.NewLine +
                @"1) Pick an extra Krypton.Themes palette, populate from it, then Export .kpalx (XML). Optionally export native .kpal, compressed-XML .kpal, and legacy .xml." + Environment.NewLine +
                @"2) Open the .kpalx file in a text editor — it is the KryptonPalette XML document. Import each file and confirm the sample header follows the theme." + Environment.NewLine +
                @"3) Convert XML to .kpalx… rewrites a legacy .xml. Export .kpal pack stores several named themes. Pack folder to .kpal stores a directory tree (relative / paths). The combo, tree, and list on the left scan the last export folder including subfolders."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 36,
            Text = @"Status: pick an extra Krypton.Themes palette, populate, then export/import."
        };

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(8),
            WrapContents = true
        };

        _extraModes = LoadExtraModes();
        _cboExtraTheme = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 280
        };
        foreach (var mode in _extraModes)
        {
            _cboExtraTheme.Items.Add(KryptonThemeCatalog.GetDisplayName(mode));
        }

        if (_cboExtraTheme.Items.Count > 0)
        {
            var macOs = Array.IndexOf(_extraModes, PaletteMode.MacOSLight);
            _cboExtraTheme.SelectedIndex = macOs >= 0 ? macOs : 0;
        }

        _cboPackTheme = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 220
        };

        var btnPopulate = new KryptonButton { Text = @"Populate from extra theme", AutoSize = true };
        btnPopulate.Click += (_, _) => PopulateFromBase();

        var btnExportKpalx = new KryptonButton { Text = @"Export .kpalx (XML)", AutoSize = true };
        btnExportKpalx.Click += (_, _) => Export(KryptonPaletteFileFormat.Xml, @"sample.kpalx");

        var btnExportBinary = new KryptonButton { Text = @"Export native .kpal", AutoSize = true };
        btnExportBinary.Click += (_, _) => Export(KryptonPaletteFileFormat.PaletteBinary, @"sample.kpal");

        var btnExportCompressed = new KryptonButton { Text = @"Export compressed-XML .kpal", AutoSize = true };
        btnExportCompressed.Click += (_, _) => Export(KryptonPaletteFileFormat.PaletteCompressedXml, @"sample-xml.kpal");

        var btnExportXml = new KryptonButton { Text = @"Export XML", AutoSize = true };
        btnExportXml.Click += (_, _) => Export(KryptonPaletteFileFormat.Xml, @"sample.xml");

        var btnImport = new KryptonButton { Text = @"Import file...", AutoSize = true };
        btnImport.Click += (_, _) => ImportFile();

        var btnConvert = new KryptonButton { Text = @"Convert XML to .kpalx...", AutoSize = true };
        btnConvert.Click += (_, _) => ConvertFile();

        var btnExportPack = new KryptonButton { Text = @"Export .kpal pack", AutoSize = true };
        btnExportPack.Click += (_, _) => ExportPackFile();

        var btnPackFolder = new KryptonButton { Text = @"Pack folder to .kpal...", AutoSize = true };
        btnPackFolder.Click += (_, _) => ExportPackFromFolder();

        var btnLoadPackTheme = new KryptonButton { Text = @"Load pack theme", AutoSize = true };
        btnLoadPackTheme.Click += (_, _) => LoadPackTheme();

        var btnApply = new KryptonButton { Text = @"Apply as global custom", AutoSize = true };
        btnApply.Click += (_, _) => ApplyCustom();

        var btnReset = new KryptonButton { Text = @"Reset global theme", AutoSize = true };
        btnReset.Click += (_, _) => ResetGlobal();

        buttonPanel.Controls.Add(_cboExtraTheme);
        buttonPanel.Controls.Add(btnPopulate);
        buttonPanel.Controls.Add(btnExportKpalx);
        buttonPanel.Controls.Add(btnExportBinary);
        buttonPanel.Controls.Add(btnExportCompressed);
        buttonPanel.Controls.Add(btnExportXml);
        buttonPanel.Controls.Add(btnImport);
        buttonPanel.Controls.Add(btnConvert);
        buttonPanel.Controls.Add(btnExportPack);
        buttonPanel.Controls.Add(btnPackFolder);
        buttonPanel.Controls.Add(_cboPackTheme);
        buttonPanel.Controls.Add(btnLoadPackTheme);
        buttonPanel.Controls.Add(btnApply);
        buttonPanel.Controls.Add(btnReset);

        var sample = new KryptonHeaderGroup
        {
            Dock = DockStyle.Top,
            Height = 120
        };
        sample.ValuesPrimary.Heading = @"Sample header (follows the imported palette)";
        sample.ValuesPrimary.Description = @"Button, header, and panel chrome should match the loaded theme.";
        var sampleButton = new KryptonButton
        {
            Text = @"Sample button",
            Location = new Point(16, 16),
            AutoSize = true
        };
        sample.Panel.Controls.Add(sampleButton);

        _lstLog = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };

        _fileCombo = new KryptonPaletteFileComboBox
        {
            Dock = DockStyle.Top,
            AutoApply = true,
            SearchSubdirectories = true
        };
        _fileCombo.KryptonManager = _manager;

        _fileTree = new KryptonPaletteFileTreeView
        {
            Dock = DockStyle.Fill,
            AutoApply = true,
            SearchSubdirectories = true
        };
        _fileTree.KryptonManager = _manager;

        _fileList = new KryptonPaletteFileListBox
        {
            Dock = DockStyle.Bottom,
            Height = 120,
            AutoApply = true,
            SearchSubdirectories = true
        };
        _fileList.KryptonManager = _manager;

        var fileLabel = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 40,
            Text = @"Palette files (last export folder, including subfolders):"
        };

        var filePanel = new Panel
        {
            Dock = DockStyle.Left,
            Width = 300,
            Padding = new Padding(8, 0, 8, 8)
        };
        filePanel.Controls.Add(_fileTree);
        filePanel.Controls.Add(_fileList);
        filePanel.Controls.Add(_fileCombo);
        filePanel.Controls.Add(fileLabel);

        Controls.Add(_lstLog);
        Controls.Add(filePanel);
        Controls.Add(sample);
        Controls.Add(buttonPanel);
        Controls.Add(_lblStatus);
        Controls.Add(instructions);

        FormClosed += (_, _) => ResetGlobal();
    }

    private void PopulateFromBase()
    {
        if (_extraModes.Length == 0)
        {
            _palette.BasePaletteMode = PaletteMode.Office2010Silver;
            _palette.PopulateFromBase(silent: true);
            _palette.SetPaletteName(@"Office 2010 Silver (demo)");
            Log(@"Krypton.Themes extras were not catalogued; populated from core Office 2010 Silver.");
            _lblStatus.Text = @"Status: populated from core fallback. Export the three formats, then import each back.";
            return;
        }

        var mode = _extraModes[Math.Max(0, _cboExtraTheme.SelectedIndex)];
        var created = KryptonThemeCustomPaletteHelper.CreateCustomPalette(mode);
        var previous = _palette;
        _palette = created;
        previous.Dispose();
        Log($@"Populated from extra theme '{_palette.GetPaletteName()}' ({mode}) via Krypton.Themes.");
        _lblStatus.Text = @"Status: populated from Krypton.Themes. Export the three formats, then import each back.";
    }

    private static PaletteMode[] LoadExtraModes()
    {
        // Touch the Themes helper type so Krypton.Themes.dll is loaded before catalog discovery.
        _ = typeof(KryptonThemeCustomPaletteHelper);

        return KryptonThemeCatalog.GetDescriptors()
            .Where(descriptor => !descriptor.IsCore)
            .Select(descriptor => descriptor.Mode)
            .OrderBy(mode => KryptonThemeCatalog.GetDisplayName(mode), StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private void Export(KryptonPaletteFileFormat format, string suggestedName)
    {
        using var dialog = new SaveFileDialog
        {
            Title = @"Save palette",
            Filter = KryptonPaletteFile.DialogFilter,
            DefaultExt = format == KryptonPaletteFileFormat.Xml
                ? (string.Equals(Path.GetExtension(suggestedName), @"." + KryptonPaletteFile.XmlExtension, StringComparison.OrdinalIgnoreCase)
                    ? KryptonPaletteFile.XmlExtension
                    : KryptonPaletteFile.Extension)
                : KryptonPaletteFile.BinaryExtension,
            FileName = suggestedName,
            InitialDirectory = _lastDirectory ?? string.Empty,
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _lastDirectory = Path.GetDirectoryName(dialog.FileName) ?? _lastDirectory;
        _palette.Export(dialog.FileName, ignoreDefaults: true, silent: true, format);
        var info = new FileInfo(dialog.FileName);
        Log($@"Exported {format} → {dialog.FileName} ({info.Length:N0} bytes).");
        _lblStatus.Text = $@"Status: exported {format} ({info.Length:N0} bytes).";
        RefreshFileSelectors();
    }

    private void ImportFile()
    {
        using var dialog = new OpenFileDialog
        {
            Title = @"Load palette",
            Filter = KryptonPaletteFile.DialogFilter,
            DefaultExt = KryptonPaletteFile.Extension,
            InitialDirectory = _lastDirectory ?? string.Empty,
            CheckFileExists = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _lastDirectory = Path.GetDirectoryName(dialog.FileName) ?? _lastDirectory;
        if (KryptonPaletteFile.IsPack(dialog.FileName))
        {
            BindPackFile(dialog.FileName);
            LoadPackTheme();
            RefreshFileSelectors();
            return;
        }

        _palette.Import(dialog.FileName, silent: true);
        ApplyCustom();
        var info = new FileInfo(dialog.FileName);
        Log($@"Imported {dialog.FileName} ({info.Length:N0} bytes). Name='{_palette.GetPaletteName()}'.");
        _lblStatus.Text = $@"Status: imported '{_palette.GetPaletteName()}'.";
        RefreshFileSelectors();
    }

    private void ConvertFile()
    {
        using var open = new OpenFileDialog
        {
            Title = @"Convert palette from",
            Filter = KryptonPaletteFile.DialogFilter,
            DefaultExt = KryptonPaletteFile.XmlExtension,
            InitialDirectory = _lastDirectory ?? string.Empty,
            CheckFileExists = true
        };

        if (open.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _lastDirectory = Path.GetDirectoryName(open.FileName) ?? _lastDirectory;

        using var save = new SaveFileDialog
        {
            Title = @"Convert palette to",
            Filter = KryptonPaletteFile.DialogFilter,
            DefaultExt = KryptonPaletteFile.Extension,
            FileName = Path.GetFileNameWithoutExtension(open.FileName) + @"." + KryptonPaletteFile.Extension,
            InitialDirectory = _lastDirectory ?? string.Empty,
            OverwritePrompt = true
        };

        if (save.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _lastDirectory = Path.GetDirectoryName(save.FileName) ?? _lastDirectory;

        try
        {
            var destination = KryptonPaletteFile.Convert(open.FileName, save.FileName);
            var info = new FileInfo(destination);
            _palette.Import(destination, silent: true);
            ApplyCustom();
            Log($@"Converted {open.FileName} → {destination} ({info.Length:N0} bytes). Name='{_palette.GetPaletteName()}'.");
            _lblStatus.Text = $@"Status: converted to '{_palette.GetPaletteName()}' ({info.Length:N0} bytes).";
            RefreshFileSelectors();
        }
        catch (Exception ex)
        {
            Log($@"Convert failed: {ex.Message}");
            _lblStatus.Text = @"Status: convert failed.";
            KryptonMessageBox.Show(this, ex.Message, @"Palette Convert", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void ExportPackFile()
    {
        using var dialog = new SaveFileDialog
        {
            Title = @"Save palette pack",
            Filter = KryptonPaletteFile.DialogFilter,
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            FileName = @"themes.kpal",
            InitialDirectory = _lastDirectory ?? string.Empty,
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _lastDirectory = Path.GetDirectoryName(dialog.FileName) ?? _lastDirectory;
        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            if (_extraModes.Length >= 2)
            {
                palettes.Add(KryptonThemeCustomPaletteHelper.CreateCustomPalette(_extraModes[0]));
                palettes.Add(KryptonThemeCustomPaletteHelper.CreateCustomPalette(_extraModes[1]));
            }
            else
            {
                palettes.Add(CreateNamedMarker(@"Pack-Lime", Color.Lime));
                palettes.Add(CreateNamedMarker(@"Pack-Orange", Color.Orange));
            }

            var destination = KryptonPaletteFile.ExportPack(dialog.FileName, palettes, ignoreDefaults: true, packName: @"2117-pack");
            var info = new FileInfo(destination);
            BindPackFile(destination);
            Log($@"Exported pack ({palettes.Count} themes) → {destination} ({info.Length:N0} bytes).");
            _lblStatus.Text = $@"Status: exported pack ({palettes.Count} themes, {info.Length:N0} bytes).";
            RefreshFileSelectors();
        }
        catch (Exception ex)
        {
            Log($@"Pack export failed: {ex.Message}");
            KryptonMessageBox.Show(this, ex.Message, @"Palette Pack", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
        finally
        {
            for (var i = 0; i < palettes.Count; i++)
            {
                palettes[i].Dispose();
            }
        }
    }

    private void ExportPackFromFolder()
    {
        using var folder = new FolderBrowserDialog
        {
            Description = @"Select a folder of .kpalx / .kpal / .xml palettes (subfolders are included).",
            ShowNewFolderButton = false
        };
        if (!string.IsNullOrWhiteSpace(_lastDirectory))
        {
            folder.SelectedPath = _lastDirectory;
        }

        if (folder.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var sourceFolder = folder.SelectedPath ?? string.Empty;
        if (string.IsNullOrWhiteSpace(sourceFolder))
        {
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Title = @"Save folder pack",
            Filter = KryptonPaletteFile.DialogFilter,
            DefaultExt = KryptonPaletteFile.BinaryExtension,
            FileName = Path.GetFileName(sourceFolder.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)) + @".kpal",
            InitialDirectory = _lastDirectory ?? string.Empty,
            OverwritePrompt = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            var destination = KryptonPaletteFile.ExportPackFromDirectory(dialog.FileName, sourceFolder, searchSubdirectories: true, ignoreDefaults: true, packName: Path.GetFileName(sourceFolder));
            _lastDirectory = Path.GetDirectoryName(destination) ?? _lastDirectory;
            BindPackFile(destination);
            var names = KryptonPaletteFile.GetThemeNames(destination);
            var info = new FileInfo(destination);
            Log($@"Packed folder '{sourceFolder}' ({names.Length} themes) → {destination} ({info.Length:N0} bytes).");
            _lblStatus.Text = $@"Status: packed folder ({names.Length} themes, {info.Length:N0} bytes).";
            RefreshFileSelectors();
        }
        catch (Exception ex)
        {
            Log($@"Folder pack failed: {ex.Message}");
            KryptonMessageBox.Show(this, ex.Message, @"Palette Pack", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void BindPackFile(string path)
    {
        _packPath = path;
        _cboPackTheme.Items.Clear();
        var names = KryptonPaletteFile.GetThemeNames(path);
        foreach (var name in names)
        {
            _cboPackTheme.Items.Add(name);
        }

        if (_cboPackTheme.Items.Count > 0)
        {
            _cboPackTheme.SelectedIndex = 0;
        }
    }

    private void LoadPackTheme()
    {
        if (string.IsNullOrWhiteSpace(_packPath) || _cboPackTheme.SelectedIndex < 0)
        {
            Log(@"Export or import a .kpal pack first, then pick a theme name.");
            return;
        }

        var packPath = _packPath;
        var themeName = _cboPackTheme.GetItemText(_cboPackTheme.SelectedItem);
        if (string.IsNullOrWhiteSpace(packPath) || string.IsNullOrWhiteSpace(themeName))
        {
            Log(@"Export or import a .kpal pack first, then pick a theme name.");
            return;
        }

        _palette.Import(packPath!, themeName!, silent: true);
        ApplyCustom();
        Log($@"Imported pack theme '{themeName}' from {_packPath}.");
        _lblStatus.Text = $@"Status: imported pack theme '{themeName}'.";
    }

    private static KryptonCustomPaletteBase CreateNamedMarker(string name, Color marker)
    {
        var palette = new KryptonCustomPaletteBase();
        palette.SetPaletteName(name);
        palette.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = marker;
        return palette;
    }

    private void ApplyCustom()
    {
        _manager.GlobalCustomPalette = _palette;
        _manager.GlobalPaletteMode = PaletteMode.Custom;
        Log(@"Applied custom palette to KryptonManager.");
    }

    private void ResetGlobal()
    {
        _manager.GlobalCustomPalette = _savedCustomPalette;
        _manager.GlobalPaletteMode = _savedPaletteMode;
        _lblStatus.Text = @"Status: restored the previous global theme.";
        Log(@"Restored previous global theme.");
    }

    private void RefreshFileSelectors()
    {
        var directory = _lastDirectory ?? string.Empty;
        if (!string.Equals(_fileCombo.PaletteDirectory, directory, StringComparison.OrdinalIgnoreCase))
        {
            _fileCombo.PaletteDirectory = directory;
        }
        else
        {
            _fileCombo.Reload();
        }

        if (!string.Equals(_fileList.PaletteDirectory, directory, StringComparison.OrdinalIgnoreCase))
        {
            _fileList.PaletteDirectory = directory;
        }
        else
        {
            _fileList.Reload();
        }

        if (!string.Equals(_fileTree.PaletteDirectory, directory, StringComparison.OrdinalIgnoreCase))
        {
            _fileTree.PaletteDirectory = directory;
        }
        else
        {
            _fileTree.Reload();
        }
    }

    private void Log(string message) => _lstLog.Items.Insert(0, $@"{DateTime.Now:HH:mm:ss}  {message}");
}
