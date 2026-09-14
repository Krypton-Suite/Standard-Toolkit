#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates palette-aware <see cref="KryptonLoadingCircle"/> spoke colours:
/// Empty inherits the global palette short-text colour; an explicit colour overrides the theme.
/// </summary>
public partial class LoadingCircleDemo : KryptonForm
{
    public LoadingCircleDemo()
    {
        InitializeComponent();
    }

    private void LoadingCircleDemo_Load(object? sender, EventArgs e)
    {
        loadingPalette.Active = true;
        loadingOverride.Active = true;
        loadingOverride.Color = Color.SteelBlue;
        loadingDisabled.Active = true;
        loadingDisabled.Enabled = false;

        cmbPreset.DataSource = Enum.GetValues(typeof(StylePresets));
        cmbPreset.SelectedItem = StylePresets.Custom;

        RefreshStatus();
    }

    private void chkActive_CheckedChanged(object? sender, EventArgs e)
    {
        bool active = chkActive.Checked;
        loadingPalette.Active = active;
        loadingOverride.Active = active;
        loadingDisabled.Active = active;
        RefreshStatus();
    }

    private void cmbPreset_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbPreset.SelectedItem is StylePresets preset)
        {
            loadingPalette.StylePreset = preset;
            loadingOverride.StylePreset = preset;
            loadingDisabled.StylePreset = preset;
            RefreshStatus();
        }
    }

    private void kbtnResetOverride_Click(object? sender, EventArgs e)
    {
        loadingOverride.CircleValues.Reset();
        loadingOverride.Active = chkActive.Checked;
        loadingOverride.Color = Color.Empty;
        RefreshStatus();
    }

    private void kbtnSetOverride_Click(object? sender, EventArgs e)
    {
        loadingOverride.Color = Color.OrangeRed;
        RefreshStatus();
    }

    private void kcmbTheme_SelectedIndexChanged(object? sender, EventArgs e) => RefreshStatus();

    private void RefreshStatus()
    {
        kwlblStatus.Text =
            $@"Palette circle Color={Describe(loadingPalette.Color)}; Override Color={Describe(loadingOverride.Color)}; " +
            $@"Active={chkActive.Checked}; Theme={KryptonManager.CurrentGlobalPaletteMode}.";
    }

    private static string Describe(Color color) =>
        color.IsEmpty ? @"Empty (palette)" : $"{color.Name} ({color.R},{color.G},{color.B})";
}
