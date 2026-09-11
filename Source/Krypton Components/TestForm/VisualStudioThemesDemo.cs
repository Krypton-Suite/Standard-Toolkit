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
/// Demo for Visual Studio built-in themes (#1083): years 2010–2026
/// (Dark/Light/Blue for 2012–2022; Fluent Dark/Light for 2026; VS2010 Office renderer variations).
/// </summary>
public partial class VisualStudioThemesDemo : KryptonForm
{
    private static readonly (string Year, PaletteMode Dark, PaletteMode Light, PaletteMode? Blue)[] YearThemes =
    {
        ("2012", PaletteMode.VisualStudio2012Dark, PaletteMode.VisualStudio2012Light, PaletteMode.VisualStudio2012Blue),
        ("2013", PaletteMode.VisualStudio2013Dark, PaletteMode.VisualStudio2013Light, PaletteMode.VisualStudio2013Blue),
        ("2015", PaletteMode.VisualStudio2015Dark, PaletteMode.VisualStudio2015Light, PaletteMode.VisualStudio2015Blue),
        ("2017", PaletteMode.VisualStudio2017Dark, PaletteMode.VisualStudio2017Light, PaletteMode.VisualStudio2017Blue),
        ("2019", PaletteMode.VisualStudio2019Dark, PaletteMode.VisualStudio2019Light, PaletteMode.VisualStudio2019Blue),
        ("2022", PaletteMode.VisualStudio2022Dark, PaletteMode.VisualStudio2022Light, PaletteMode.VisualStudio2022Blue),
        ("2026", PaletteMode.VisualStudio2026Dark, PaletteMode.VisualStudio2026Light, null)
    };

    private static readonly (string Name, PaletteMode Mode)[] Vs2010Variants =
    {
        ("2010 (2007 Variation)", PaletteMode.VisualStudio2010Render2007),
        ("2010 (2010 Variation)", PaletteMode.VisualStudio2010Render2010),
        ("2010 (2013 Variation)", PaletteMode.VisualStudio2010Render2013),
        ("2010 (Microsoft 365 Variation)", PaletteMode.VisualStudio2010Render365)
    };

    private PaletteMode _previousMode;
    private bool _suppressApply;

    public VisualStudioThemesDemo()
    {
        InitializeComponent();
    }

    private void VisualStudioThemesDemo_Load(object sender, EventArgs e)
    {
        _previousMode = KryptonManager.CurrentGlobalPaletteMode;

        _suppressApply = true;
        cmbYear.Items.Clear();
        foreach (var entry in YearThemes)
        {
            cmbYear.Items.Add(entry.Year);
        }

        cmbYear.Items.Add("2010 variants");
        cmbYear.SelectedIndex = YearThemes.Length - 1; // 2026
        PopulateVariantItems(selectedIndex: 1); // Light
        _suppressApply = false;

        ApplySelectedTheme();
        RefreshStatusLabel();
    }

    private void VisualStudioThemesDemo_FormClosed(object sender, FormClosedEventArgs e) =>
        ThemeManager.ApplyTheme(_previousMode, kryptonManager1);

    private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_suppressApply)
        {
            return;
        }

        _suppressApply = true;
        PopulateVariantItems(selectedIndex: 1);
        _suppressApply = false;
        ApplySelectedTheme();
        RefreshStatusLabel();
    }

    private void cmbVariant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_suppressApply)
        {
            return;
        }

        ApplySelectedTheme();
        RefreshStatusLabel();
    }

    private void PopulateVariantItems(int selectedIndex)
    {
        cmbVariant.Items.Clear();
        if (IsVs2010YearSelected())
        {
            foreach (var variant in Vs2010Variants)
            {
                cmbVariant.Items.Add(variant.Name);
            }

            cmbVariant.SelectedIndex = 0;
            return;
        }

        cmbVariant.Items.Add("Dark");
        cmbVariant.Items.Add("Light");
        if (TryGetYearEntry(out var entry) && entry.Blue.HasValue)
        {
            cmbVariant.Items.Add("Blue");
        }

        cmbVariant.SelectedIndex = Math.Min(selectedIndex, cmbVariant.Items.Count - 1);
    }

    private bool IsVs2010YearSelected() =>
        string.Equals(cmbYear.Text, "2010 variants", StringComparison.Ordinal);

    private bool TryGetYearEntry(out (string Year, PaletteMode Dark, PaletteMode Light, PaletteMode? Blue) entry)
    {
        foreach (var candidate in YearThemes)
        {
            if (candidate.Year == cmbYear.Text)
            {
                entry = candidate;
                return true;
            }
        }

        entry = default;
        return false;
    }

    private void ApplySelectedTheme() =>
        ThemeManager.ApplyTheme(ResolveSelectedMode(), kryptonManager1);

    private PaletteMode ResolveSelectedMode()
    {
        if (IsVs2010YearSelected())
        {
            var index = Math.Max(0, cmbVariant.SelectedIndex);
            if (index >= Vs2010Variants.Length)
            {
                index = 0;
            }

            return Vs2010Variants[index].Mode;
        }

        if (!TryGetYearEntry(out var entry))
        {
            return PaletteMode.VisualStudio2026Light;
        }

        return cmbVariant.SelectedIndex switch
        {
            0 => entry.Dark,
            1 => entry.Light,
            _ => entry.Blue ?? entry.Light
        };
    }

    private void RefreshStatusLabel()
    {
        var mode = KryptonManager.CurrentGlobalPaletteMode;
        lblStatus.Values.Text =
            $"Applied: {mode}  |  ThemeName: {KryptonManager.CurrentGlobalPalette?.ThemeName}";
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        ThemeManager.ApplyTheme(_previousMode, kryptonManager1);
        RefreshStatusLabel();
    }
}
