#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class WindowsXPLunaThemeSmokeTest : KryptonForm
{
    public WindowsXPLunaThemeSmokeTest()
    {
        InitializeComponent();
    }

    private void WindowsXPLunaThemeSmokeTest_Load(object sender, EventArgs e)
    {
        kryptonManager1.GlobalPaletteMode = PaletteMode.WindowsXPLunaBlue;
        kryptonThemeComboBox1.Text = @"Windows XP - Luna Blue";
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
        btnImport.Values.Text = "Import palette XML…";
        btnImport.Click += OnImportPaletteClick;
        kryptonPanel2.Controls.Add(btnImport);
    }

    private void SetupNavigatorPages()
    {
        kryptonPageGeneral.Controls.Add(CreateSamplePanel(@"Classic #ECE9D8 workspace (Luna/Royale) or dark chrome (Noir/Zune); RenderWindowsXPLuna glossy buttons."));
        kryptonPageSettings.Controls.Add(CreateSamplePanel(@"Theme combo lists Luna Blue/Olive/Silver, Royale, Royale Noir, and Zune."));
        kryptonPageAbout.Controls.Add(CreateSamplePanel(
            @"Built-in palettes: Luna (3), Royale, Royale Noir, Zune. Export uses WindowsXPVisualStyleCustomPaletteHelper with RendererMode.WindowsXPLuna."));
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
            Height = 48
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
        PaletteMode mode = ResolveVisualStyleMode(KryptonManager.CurrentGlobalPaletteMode);

        string defaultName = mode switch
        {
            PaletteMode.WindowsXPLunaOlive => @"WindowsXP-Luna-Olive.xml",
            PaletteMode.WindowsXPLunaSilver => @"WindowsXP-Luna-Silver.xml",
            PaletteMode.WindowsXPRoyale => @"WindowsXP-Royale.xml",
            PaletteMode.WindowsXPRoyaleNoir => @"WindowsXP-Royale-Noir.xml",
            PaletteMode.WindowsXPZune => @"WindowsXP-Zune.xml",
            _ => @"WindowsXP-Luna-Blue.xml"
        };

        using var dialog = new SaveFileDialog
        {
            Filter = @"Palette files (*.xml)|*.xml",
            FileName = defaultName,
            Title = @"Export Windows XP visual style palette"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        WindowsXPVisualStyleCustomPaletteHelper.ExportToFile(mode, dialog.FileName);
        KryptonMessageBox.Show(this, $@"Exported to {dialog.FileName}", @"Export complete");
    }

    private void OnImportPaletteClick(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = @"Palette files (*.xml)|*.xml",
            Title = @"Import Luna palette"
        };

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

    private static PaletteMode ResolveVisualStyleMode(PaletteMode mode) =>
        WindowsXPVisualStyleCustomPaletteHelper.IsWindowsXPVisualStyle(mode)
            ? mode
            : PaletteMode.WindowsXPLunaBlue;

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
