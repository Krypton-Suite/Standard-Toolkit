#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #4230: extra palettes in Krypton.Themes, auto-discovery, and selector enable/disable.
/// </summary>
public sealed class ThemeCatalogDemo : KryptonForm
{
    private readonly KryptonManager _manager = new KryptonManager();
    private readonly KryptonThemeComboBox _themeCombo;
    private readonly KryptonLabel _lblStatus;
    private readonly KryptonCheckBox _chkVisualStudio;
    private readonly KryptonCheckBox _chkMaterial;
    private readonly KryptonCheckBox _chkSparkle;
    private readonly KryptonCheckBox _chkShowExtra;
    private readonly KryptonThemeListBox _themeList;
    private PaletteMode _previousMode;

    public ThemeCatalogDemo()
    {
        Text = @"4230 — Theme catalog";
        Size = new Size(780, 640);
        StartPosition = FormStartPosition.CenterScreen;
        _previousMode = KryptonManager.CurrentGlobalPaletteMode;

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 120,
            Padding = new Padding(12),
            Text =
                "Issue #4230: core palettes live in Krypton.Toolkit (Professional, Sparkle Blue/Orange/Purple, plus Office 2007/2010/Microsoft 365 Blue, Silver, and Black). " +
                "Everything else is in Krypton.Themes.dll and is auto-discovered when that assembly is beside the app (TestForm references it). " +
                "Use the family check boxes to hide themes from selectors (Sparkle extra-only keeps Blue/Orange/Purple). Uncheck Show extra themes to list core palettes only. Programmatic GlobalPaletteMode still applies a hidden extra theme. If Krypton.Themes.dll is missing, extra modes paint as Microsoft 365 Blue instead of throwing."
        };

        _lblStatus = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 48,
            Padding = new Padding(12, 4, 12, 4)
        };

        var toolbar = new KryptonPanel { Dock = DockStyle.Top, Height = 168, Padding = new Padding(12, 8, 12, 8) };
        var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = true };

        flow.Controls.Add(new KryptonLabel { Text = @"Theme:", AutoSize = true, Padding = new Padding(0, 6, 8, 0) });
        _themeCombo = new KryptonThemeComboBox { Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
        _themeList = new KryptonThemeListBox { Dock = DockStyle.Fill };
        flow.Controls.Add(_themeCombo);

        _chkVisualStudio = new KryptonCheckBox { Text = @"Visual Studio family selectable", Checked = true, AutoSize = true, Padding = new Padding(16, 6, 0, 0) };
        _chkMaterial = new KryptonCheckBox { Text = @"Material family selectable", Checked = true, AutoSize = true, Padding = new Padding(16, 6, 0, 0) };
        _chkSparkle = new KryptonCheckBox { Text = @"Sparkle extra selectable", Checked = true, AutoSize = true, Padding = new Padding(16, 6, 0, 0) };
        _chkShowExtra = new KryptonCheckBox { Text = @"Show extra themes", Checked = true, AutoSize = true, Padding = new Padding(16, 6, 0, 0) };
        _chkVisualStudio.CheckedChanged += (_, _) => KryptonThemeAvailability.SetFamilyEnabled(KryptonThemeFamilies.VisualStudio, _chkVisualStudio.Checked);
        _chkMaterial.CheckedChanged += (_, _) => KryptonThemeAvailability.SetFamilyEnabled(KryptonThemeFamilies.Material, _chkMaterial.Checked);
        _chkSparkle.CheckedChanged += (_, _) => KryptonThemeAvailability.SetFamilyEnabled(KryptonThemeFamilies.Sparkle, _chkSparkle.Checked, extraOnly: true);
        _chkShowExtra.CheckedChanged += (_, _) =>
        {
            _themeCombo.ShowExtraThemes = _chkShowExtra.Checked;
            _themeList.ShowExtraThemes = _chkShowExtra.Checked;
            UpdateStatus();
        };
        flow.Controls.Add(_chkVisualStudio);
        flow.Controls.Add(_chkMaterial);
        flow.Controls.Add(_chkSparkle);
        flow.Controls.Add(_chkShowExtra);

        var btnCore = new KryptonButton { Text = @"Apply core Microsoft 365 Blue", AutoSize = true };
        btnCore.Click += (_, _) => { _manager.GlobalPaletteMode = PaletteMode.Microsoft365Blue; UpdateStatus(); };
        var btnExtra = new KryptonButton { Text = @"Apply extra VS 2022 Dark", AutoSize = true };
        btnExtra.Click += (_, _) =>
        {
            _manager.GlobalPaletteMode = PaletteMode.VisualStudio2022Dark;
            UpdateStatus();
        };
        flow.Controls.Add(btnCore);
        flow.Controls.Add(btnExtra);

        var btnBrowser = new KryptonButton { Text = @"Theme browser", AutoSize = true };
        btnBrowser.Click += (_, _) =>
        {
            KryptonThemeBrowser.Show(new KryptonThemeBrowserData
            {
                WindowTitle = @"4230 theme browser",
                StartPosition = FormStartPosition.CenterParent,
                ShowExtraThemes = _chkShowExtra.Checked,
                DefaultPalette = KryptonManager.CurrentGlobalPaletteMode
            });
            UpdateStatus();
        };
        var btnExport = new KryptonButton { Text = @"Save availability", AutoSize = true };
        btnExport.Click += (_, _) =>
        {
            var path = GetAvailabilityPath();
            System.IO.File.WriteAllText(path, KryptonThemeAvailability.Export());
            UpdateStatus();
            KryptonMessageBox.Show(this, path, @"Availability saved", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        };
        var btnImport = new KryptonButton { Text = @"Load availability", AutoSize = true };
        btnImport.Click += (_, _) =>
        {
            var path = GetAvailabilityPath();
            if (System.IO.File.Exists(path))
            {
                KryptonThemeAvailability.Import(System.IO.File.ReadAllText(path));
            }

            UpdateStatus();
        };
        flow.Controls.Add(btnBrowser);
        flow.Controls.Add(btnExport);
        flow.Controls.Add(btnImport);

        toolbar.Controls.Add(flow);

        var sample = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(16) };
        var split = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55f));
        split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));
        var inner = new FlowLayoutPanel { Dock = DockStyle.Fill };
        inner.Controls.Add(new KryptonButton { Text = @"Sample button", AutoSize = true });
        inner.Controls.Add(new KryptonCheckBox { Text = @"Sample check", Checked = true, AutoSize = true });
        inner.Controls.Add(new KryptonTextBox { Text = @"Sample text", Width = 200 });
        split.Controls.Add(inner, 0, 0);
        split.Controls.Add(_themeList, 1, 0);
        sample.Controls.Add(split);

        Controls.Add(sample);
        Controls.Add(toolbar);
        Controls.Add(_lblStatus);
        Controls.Add(instructions);

        Load += (_, _) => UpdateStatus();
        FormClosed += (_, _) =>
        {
            KryptonThemeAvailability.Reset();
            _manager.GlobalPaletteMode = _previousMode;
        };
    }

    private static string GetAvailabilityPath() =>
        System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"krypton-theme-availability-4230.txt");

    private void UpdateStatus()
    {
        var extra = KryptonThemeCatalog.IsImplementationAvailable(PaletteMode.VisualStudio2022Dark);
        var sample = false;
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (string.Equals(assembly.GetName().Name, @"ThemeProviderSample", StringComparison.OrdinalIgnoreCase))
            {
                sample = true;
                break;
            }
        }

        _lblStatus.Text =
            @"Current: " + KryptonManager.CurrentGlobalPaletteMode +
            @"  |  Extra: " + extra +
            @"  |  Families: " + KryptonThemeCatalog.GetFamilies().Length +
            @"  |  Descriptors: " + KryptonThemeCatalog.GetDescriptors().Length +
            @"  |  Sample DLL: " + sample +
            @"  |  Combo: " + _themeCombo.Items.Count +
            @"  |  List: " + _themeList.Items.Count;
    }
}
