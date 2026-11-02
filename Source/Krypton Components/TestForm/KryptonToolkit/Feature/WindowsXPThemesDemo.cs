#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Windows XP visual-style Extra themes (Luna Blue/Olive/Silver, Royale, Royale Noir, Zune, Embedded).
/// </summary>
public partial class WindowsXPThemesDemo : KryptonForm
{
    private static readonly (string Name, PaletteMode Mode)[] Themes =
    {
        ("Luna Blue", PaletteMode.WindowsXPLunaBlue),
        ("Luna Olive", PaletteMode.WindowsXPLunaOlive),
        ("Luna Silver", PaletteMode.WindowsXPLunaSilver),
        ("Royale", PaletteMode.WindowsXPRoyale),
        ("Royale Noir", PaletteMode.WindowsXPRoyaleNoir),
        ("Zune", PaletteMode.WindowsXPZune),
        ("Embedded", PaletteMode.WindowsXPEmbedded)
    };

    private PaletteMode _previousMode;
    private bool _suppressApply;

    public WindowsXPThemesDemo()
    {
        InitializeComponent();
    }

    private void WindowsXPThemesDemo_Load(object sender, EventArgs e)
    {
        _previousMode = KryptonManager.CurrentGlobalPaletteMode;
        _suppressApply = true;
        cmbTheme.Items.Clear();
        foreach (var theme in Themes)
        {
            cmbTheme.Items.Add(theme.Name);
        }

        cmbTheme.SelectedIndex = 0;
        _suppressApply = false;
        ApplySelectedTheme();
        RefreshStatus();
    }

    private void WindowsXPThemesDemo_FormClosed(object sender, FormClosedEventArgs e) =>
        ThemeManager.ApplyTheme(_previousMode, kryptonManager1);

    private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_suppressApply)
        {
            return;
        }

        ApplySelectedTheme();
        RefreshStatus();
    }

    private void ApplySelectedTheme()
    {
        if (cmbTheme.SelectedIndex < 0 || cmbTheme.SelectedIndex >= Themes.Length)
        {
            return;
        }

        ThemeManager.ApplyTheme(Themes[cmbTheme.SelectedIndex].Mode, kryptonManager1);
    }

    private void RefreshStatus()
    {
        var mode = KryptonManager.CurrentGlobalPaletteMode;
        var family = KryptonThemeCatalog.TryGetDescriptor(mode, out var descriptor) && descriptor != null
            ? descriptor.Family
            : @"(unknown)";
        klblStatus.Values.Text =
            $"Current: {mode} | Family: {family} | Chrome: {KryptonThemeChrome.GetChromeKind(mode)}";
    }
}