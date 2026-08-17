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
/// Demo for issue #1551: Materialize Blue, Materialize Light Blue, and Silver Dark Alternate
/// builtin palettes across Office 2007 / 2010 / 2013, Microsoft 365, and Material.
/// </summary>
public partial class Issue1551ThemeDemo : KryptonForm
{
    private static readonly PaletteMode[] ThemeOrder =
    {
        PaletteMode.Office2007MaterializeBlue,
        PaletteMode.Office2007MaterializeBlueDark,
        PaletteMode.Office2007MaterializeLightBlue,
        PaletteMode.Office2007MaterializeLightBlueDark,
        PaletteMode.Office2007SilverDarkModeAlternate,
        PaletteMode.Office2010MaterializeBlue,
        PaletteMode.Office2010MaterializeBlueDark,
        PaletteMode.Office2010MaterializeLightBlue,
        PaletteMode.Office2010MaterializeLightBlueDark,
        PaletteMode.Office2010SilverDarkModeAlternate,
        PaletteMode.Office2013MaterializeBlue,
        PaletteMode.Office2013MaterializeBlueDark,
        PaletteMode.Office2013MaterializeLightBlue,
        PaletteMode.Office2013MaterializeLightBlueDark,
        PaletteMode.Office2013SilverDarkModeAlternate,
        PaletteMode.Microsoft365MaterializeBlue,
        PaletteMode.Microsoft365MaterializeBlueDark,
        PaletteMode.Microsoft365MaterializeLightBlue,
        PaletteMode.Microsoft365MaterializeLightBlueDark,
        PaletteMode.Microsoft365SilverDarkModeAlternate,
        PaletteMode.MaterialMaterializeBlue,
        PaletteMode.MaterialMaterializeBlueDark,
        PaletteMode.MaterialMaterializeBlueRipple,
        PaletteMode.MaterialMaterializeBlueDarkRipple,
        PaletteMode.MaterialMaterializeLightBlue,
        PaletteMode.MaterialMaterializeLightBlueDark,
        PaletteMode.MaterialMaterializeLightBlueRipple,
        PaletteMode.MaterialMaterializeLightBlueDarkRipple,
        PaletteMode.MaterialSilverDarkModeAlternate,
        PaletteMode.MaterialSilverDarkModeAlternateRipple
    };

    private PaletteMode _previousMode;
    private KryptonCustomPaletteBase? _previousCustom;
    private bool _suppressFamilyChange;

    public Issue1551ThemeDemo()
    {
        InitializeComponent();
    }

    private void Issue1551ThemeDemo_Load(object sender, EventArgs e)
    {
        _previousMode = KryptonManager.CurrentGlobalPaletteMode;
        _previousCustom = kryptonManager1.GlobalCustomPalette;

        _suppressFamilyChange = true;
        kcmbFamily.Items.Clear();
        foreach (PaletteMode mode in ThemeOrder)
        {
            kcmbFamily.Items.Add(ThemeManager.ReturnPaletteModeAsString(mode));
        }

        kcmbFamily.SelectedIndex = 15;
        _suppressFamilyChange = false;
        ApplySelectedTheme();
    }

    private void Issue1551ThemeDemo_FormClosed(object sender, FormClosedEventArgs e)
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
        if (!_suppressFamilyChange)
        {
            ApplySelectedTheme();
        }
    }

    private void kbtnApply_Click(object sender, EventArgs e) => ApplySelectedTheme();

    private void kbtnResetTheme_Click(object sender, EventArgs e)
    {
        kryptonManager1.GlobalPaletteMode = PaletteMode.Microsoft365Blue;
        klblStatus.Values.Text = @"Reset to Microsoft 365 Blue.";
        statusLabel.Text = @"Theme: Microsoft365Blue";
    }

    private void kbtnExport_Click(object sender, EventArgs e)
    {
        PaletteMode mode = GetSelectedMode();
        var custom = new KryptonCustomPaletteBase { BasePaletteMode = mode };
        custom.PopulateFromBase(silent: true);
        custom.SetPaletteName(ThemeManager.ReturnPaletteModeAsString(mode));
        using var dialog = new SaveFileDialog
        {
            Filter = @"Palette files (*.xml)|*.xml",
            FileName = mode + @".xml",
            Title = @"Export #1551 palette"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        custom.Export(dialog.FileName, ignoreDefaults: true, silent: true);
        klblStatus.Values.Text = $@"Exported to {dialog.FileName}";
    }

    private void ApplySelectedTheme()
    {
        PaletteMode mode = GetSelectedMode();
        ThemeManager.ApplyTheme(mode, kryptonManager1);
        string name = ThemeManager.ReturnPaletteModeAsString(mode);
        klblStatus.Values.Text = $@"Applied builtin ""{name}"" ({mode}).";
        statusLabel.Text = @"Theme: " + name;
    }

    private PaletteMode GetSelectedMode()
    {
        int index = kcmbFamily.SelectedIndex;
        if (index < 0 || index >= ThemeOrder.Length)
        {
            return PaletteMode.Microsoft365MaterializeBlue;
        }

        return ThemeOrder[index];
    }
}
