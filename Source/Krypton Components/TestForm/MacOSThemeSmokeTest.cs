#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class MacOSThemeSmokeTest : KryptonForm
{
    public MacOSThemeSmokeTest()
    {
        InitializeComponent();
    }

    private void MacOSThemeSmokeTest_Load(object sender, EventArgs e)
    {
        kryptonManager1.GlobalPaletteMode = PaletteMode.MacOSLight;
        kryptonThemeComboBox1.Text = @"macOS - Light";
        kryptonButton2.ButtonStyle = ButtonStyle.Command;

        SetupToolbarRow();
        SetupNavigatorPages();
    }

    private void SetupToolbarRow()
    {
        kryptonPanel2.Height = 76;
        kryptonPanel2.Width = Math.Max(kryptonPanel2.Width, 450);

        var btnExport = new KryptonButton
        {
            Location = new Point(12, 42),
            Size = new Size(140, 25),
            TabIndex = 5
        };
        btnExport.Values.Text = "Export palette XML…";
        btnExport.Click += OnExportPaletteClick;
        kryptonPanel2.Controls.Add(btnExport);

        var btnContext = new KryptonButton
        {
            Location = new Point(158, 42),
            Size = new Size(120, 25),
            TabIndex = 6
        };
        btnContext.Values.Text = "Context menu";
        btnContext.Click += OnContextMenuClick;
        kryptonPanel2.Controls.Add(btnContext);

        var btnImport = new KryptonButton
        {
            Location = new Point(284, 42),
            Size = new Size(150, 25),
            TabIndex = 7
        };
        btnImport.Values.Text = "Import sample XML…";
        btnImport.Click += OnImportSamplePaletteClick;
        kryptonPanel2.Controls.Add(btnImport);
    }

    private void SetupNavigatorPages()
    {
        kryptonPageGeneral.Controls.Add(CreateSamplePanel(@"Buttons, checkbox, and list on a client panel."));
        kryptonPageSettings.Controls.Add(CreateSamplePanel(@"Switch theme above; accent button uses Command style."));
        kryptonPageAbout.Controls.Add(CreateSamplePanel(
            @"Mac themes: PaletteRibbonShape.Mac hides QAT, File tab, and group titles; forms get DWM blur + shadow and ~248-alpha title bars. Sample XML: Documents/Palettes/macOS-Light.xml."));
    }

    private static KryptonPanel CreateSamplePanel(string description)
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };

        var label = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Text = description,
            AutoSize = false,
            Height = 40
        };

        var check = new KryptonCheckBox
        {
            Text = @"Enable feature",
            Dock = DockStyle.Top
        };

        var button = new KryptonButton
        {
            Dock = DockStyle.Top,
            Height = 32,
            Margin = new Padding(0, 8, 0, 0)
        };
        button.Values.Text = @"Sample action";

        panel.Controls.Add(button);
        panel.Controls.Add(check);
        panel.Controls.Add(label);
        return panel;
    }

    private void OnExportPaletteClick(object? sender, EventArgs e)
    {
        var mode = KryptonManager.CurrentGlobalPaletteMode;
        if (mode != PaletteMode.MacOSLight && mode != PaletteMode.MacOSDark)
        {
            mode = PaletteMode.MacOSLight;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = @"Palette files (*.xml)|*.xml",
            FileName = mode == PaletteMode.MacOSDark ? @"macOS-Dark.xml" : @"macOS-Light.xml",
            Title = @"Export macOS palette"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        MacOSCustomPaletteHelper.ExportToFile(mode, dialog.FileName);
        KryptonMessageBox.Show(this, $@"Exported to {dialog.FileName}", @"Export complete");
    }

    private void OnImportSamplePaletteClick(object? sender, EventArgs e)
    {
        string? repoRoot = FindRepositoryRoot();
        if (repoRoot == null)
        {
            KryptonMessageBox.Show(this, @"Could not locate Documents/Palettes under the repository root.", @"Import");
            return;
        }

        string palettesDir = System.IO.Path.Combine(repoRoot, "Documents", "Palettes");
        string lightPath = System.IO.Path.Combine(palettesDir, "macOS-Light.xml");
        string darkPath = System.IO.Path.Combine(palettesDir, "macOS-Dark.xml");

        if (!System.IO.File.Exists(lightPath) && !System.IO.File.Exists(darkPath))
        {
            KryptonMessageBox.Show(this,
                $@"No sample palettes found in {palettesDir}. Run Source\TestHarnesses\MacPaletteExport to generate them.",
                @"Import");
            return;
        }

        using var dialog = new OpenFileDialog
        {
            Filter = @"Palette files (*.xml)|*.xml",
            InitialDirectory = palettesDir,
            Title = @"Import macOS sample palette"
        };

        if (System.IO.File.Exists(lightPath))
        {
            dialog.FileName = System.IO.Path.GetFileName(lightPath);
        }

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var custom = new KryptonCustomPaletteBase();
        custom.Import(dialog.FileName, silent: true);
        kryptonManager1.GlobalCustomPalette = custom;
        kryptonManager1.GlobalPaletteMode = PaletteMode.Custom;
        kryptonThemeComboBox1.Text = custom.GetPaletteName() ?? @"Custom";
        KryptonMessageBox.Show(this, $@"Imported {dialog.FileName}", @"Import complete");
    }

    private static string? FindRepositoryRoot()
    {
        var dir = new System.IO.DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (System.IO.Directory.Exists(System.IO.Path.Combine(dir.FullName, "Documents", "Palettes")))
            {
                return dir.FullName;
            }

            dir = dir.Parent;
        }

        return null;
    }

    private void OnContextMenuClick(object? sender, EventArgs e)
    {
        using var menu = new KryptonContextMenu();
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem(@"Cut"));
        items.Items.Add(new KryptonContextMenuItem(@"Copy"));
        items.Items.Add(new KryptonContextMenuItem(@"Paste"));
        menu.Items.Add(items);
        menu.Show(this, PointToScreen(new Point(12, 42)));
    }
}