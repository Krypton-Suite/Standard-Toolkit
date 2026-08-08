#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo: builtin Lime Green palette variants (Office 2007 / 2010 / Microsoft 365 light and dark bases)
/// via <see cref="LimeGreenButtonThemeHelper"/>, <see cref="PaletteMode"/> and <see cref="ThemeManager"/>.
/// </summary>
public partial class LimeGreenButtonThemeDemo : KryptonForm
{
    private static readonly LimeGreenThemeFamily[] FamilyOrder =
    {
        LimeGreenThemeFamily.Office2007,
        LimeGreenThemeFamily.Office2007Dark,
        LimeGreenThemeFamily.Office2010,
        LimeGreenThemeFamily.Office2010Dark,
        LimeGreenThemeFamily.Microsoft365,
        LimeGreenThemeFamily.Microsoft365Dark
    };

    private KryptonCustomPaletteBase? _exportPalette;
    private PaletteMode _previousMode;
    private KryptonCustomPaletteBase? _previousCustom;
    private bool _suppressFamilyChange;

    public LimeGreenButtonThemeDemo()
    {
        InitializeComponent();
    }

    private void LimeGreenButtonThemeDemo_Load(object sender, EventArgs e)
    {
        _previousMode = KryptonManager.CurrentGlobalPaletteMode;
        _previousCustom = kryptonManager1.GlobalCustomPalette;

        _suppressFamilyChange = true;
        kcmbFamily.Items.Clear();
        foreach (LimeGreenThemeFamily family in FamilyOrder)
        {
            kcmbFamily.Items.Add(GetFamilyComboLabel(family));
        }

        kcmbFamily.SelectedIndex = 4; // Microsoft 365 (light)
        _suppressFamilyChange = false;

        ApplyLimeTheme();
    }

    private void LimeGreenButtonThemeDemo_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (_previousMode == PaletteMode.Custom && _previousCustom != null)
        {
            ThemeManager.ApplyTheme(_previousCustom, kryptonManager1);
        }
        else
        {
            kryptonManager1.GlobalPaletteMode = _previousMode;
        }
    }

    private void kcmbFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_suppressFamilyChange)
        {
            return;
        }

        ApplyLimeTheme();
    }

    private void kbtnApplyLime_Click(object sender, EventArgs e) => ApplyLimeTheme();

    private void kbtnResetTheme_Click(object sender, EventArgs e)
    {
        var family = GetSelectedFamily();
        var baseMode = LimeGreenButtonThemeHelper.GetBasePaletteMode(family);
        kryptonManager1.GlobalPaletteMode = baseMode;
        klblStatus.Values.Text = $@"Reset to builtin {GetFamilyLabel(family)} (no lime overrides).";
        statusLabel.Text = $@"Theme: {baseMode}";
    }

    private void kbtnExport_Click(object sender, EventArgs e)
    {
        var family = GetSelectedFamily();
        _exportPalette = LimeGreenButtonThemeHelper.CreateExportPalette(family);
        using var dialog = new SaveFileDialog
        {
            Filter = @"Palette files (*.xml)|*.xml",
            FileName = LimeGreenButtonThemeHelper.GetExportFileName(family),
            Title = @"Export lime-green palette"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _exportPalette.Export(dialog.FileName, ignoreDefaults: true, silent: true);
        klblStatus.Values.Text = $@"Exported to {dialog.FileName}";
        statusLabel.Text = @"Exported palette XML";
    }

    private void ApplyLimeTheme()
    {
        var family = GetSelectedFamily();
        var mode = LimeGreenButtonThemeHelper.GetPaletteMode(family);
        ThemeManager.ApplyTheme(mode, kryptonManager1);
        var name = LimeGreenButtonThemeHelper.GetPaletteName(family);
        klblStatus.Values.Text =
            $@"Applied builtin ""{name}"" ({mode}). " +
            @"Compare light / dark across 2007 · 2010 · 365.";
        statusLabel.Text = @"Theme: " + name;
    }

    private LimeGreenThemeFamily GetSelectedFamily()
    {
        int index = kcmbFamily.SelectedIndex;
        if (index < 0 || index >= FamilyOrder.Length)
        {
            return LimeGreenThemeFamily.Microsoft365;
        }

        return FamilyOrder[index];
    }

    private static string GetFamilyComboLabel(LimeGreenThemeFamily family) => family switch
    {
        LimeGreenThemeFamily.Office2007 => @"Office 2007",
        LimeGreenThemeFamily.Office2007Dark => @"Office 2007 Dark",
        LimeGreenThemeFamily.Office2010 => @"Office 2010",
        LimeGreenThemeFamily.Office2010Dark => @"Office 2010 Dark",
        LimeGreenThemeFamily.Microsoft365 => @"Microsoft 365",
        LimeGreenThemeFamily.Microsoft365Dark => @"Microsoft 365 Dark",
        _ => family.ToString()
    };

    private static string GetFamilyLabel(LimeGreenThemeFamily family) => family switch
    {
        LimeGreenThemeFamily.Office2007 => @"Office 2007 Blue",
        LimeGreenThemeFamily.Office2007Dark => @"Office 2007 Blue Dark Mode",
        LimeGreenThemeFamily.Office2010 => @"Office 2010 Blue",
        LimeGreenThemeFamily.Office2010Dark => @"Office 2010 Blue Dark Mode",
        LimeGreenThemeFamily.Microsoft365 => @"Microsoft 365 White",
        LimeGreenThemeFamily.Microsoft365Dark => @"Microsoft 365 Black Dark Mode",
        _ => family.ToString()
    };
}
