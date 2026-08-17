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
/// Issue #4234: generate a custom theme from a few seed colours (hex or RGB), apply it, register it
/// in theme selectors, export XML, and open the builder dialog.
/// </summary>
public partial class CustomThemeGeneratorDemo : KryptonForm
{
    private PaletteMode _previousMode;
    private KryptonCustomPaletteBase? _previousCustom;
    private KryptonCustomPaletteBase? _currentPalette;
    private Image? _dropperGlyph;
    private bool _suppressEvents;

    public CustomThemeGeneratorDemo()
    {
        InitializeComponent();
        ConfigureScreenPickers();
    }

    private void ConfigureScreenPickers()
    {
        _dropperGlyph = KryptonScreenColorPicker.CreateDropperGlyphImage();
        ConfigureScreenPicker(kbtnPickPrimary, _dropperGlyph);
        ConfigureScreenPicker(kbtnPickSecondary, _dropperGlyph);
        ConfigureScreenPicker(kbtnPickSurface, _dropperGlyph);
    }

    private static void ConfigureScreenPicker(KryptonButton button, Image glyph)
    {
        button.Values.Image = glyph;
        button.Values.Text = string.Empty;
        button.AccessibleName = @"Pick colour from screen";
        button.ToolTipValues.EnableToolTips = true;
        button.ToolTipValues.Heading = @"Screen colour picker";
        button.ToolTipValues.Description = @"Hides this form, then magnifies pixels under the cursor. Click to sample, Esc or right-click to cancel.";
    }

    private void CustomThemeGeneratorDemo_Load(object sender, EventArgs e)
    {
        _previousMode = KryptonManager.CurrentGlobalPaletteMode;
        _previousCustom = kryptonManager1.GlobalCustomPalette;

        _suppressEvents = true;
        ktxtName.Text = @"Contoso";
        kbtnPrimary.SelectedColor = Color.FromArgb(0x00, 0x78, 0xD4);
        ktxtPrimaryHex.Text = @"#0078D4";
        ktxtPrimaryRgb.Text = @"0, 120, 212";

        kcmbDonor.Items.Clear();
        IReadOnlyList<PaletteMode> donors = KryptonCustomThemeGenerator.SupportedDonorModes;
        for (int i = 0; i < donors.Count; i++)
        {
            kcmbDonor.Items.Add(KryptonCustomThemeGenerator.GetDonorDisplayName(donors[i]));
        }

        kcmbDonor.SelectedIndex = 0;
        _suppressEvents = false;
        GenerateAndApply(register: false);
    }

    private void CustomThemeGeneratorDemo_FormClosed(object sender, FormClosedEventArgs e)
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

    private KryptonCustomThemeSeed ReadSeed()
    {
        Color primary = kbtnPrimary.SelectedColor;
        if (KryptonCustomThemeGenerator.TryParseColor(ktxtPrimaryHex.Text, out Color fromHex))
        {
            primary = fromHex;
        }

        PaletteMode donor = PaletteMode.Office2010Blue;
        IReadOnlyList<PaletteMode> donors = KryptonCustomThemeGenerator.SupportedDonorModes;
        if (kcmbDonor.SelectedIndex >= 0 && kcmbDonor.SelectedIndex < donors.Count)
        {
            donor = donors[kcmbDonor.SelectedIndex];
        }

        Color? secondary = kchkSecondary.Checked ? kbtnSecondary.SelectedColor : (Color?)null;
        Color? surface = kchkSurface.Checked ? kbtnSurface.SelectedColor : (Color?)null;

        return new KryptonCustomThemeSeed
        {
            Name = string.IsNullOrWhiteSpace(ktxtName.Text) ? @"Custom Theme" : ktxtName.Text.Trim(),
            Primary = primary,
            Secondary = secondary,
            Surface = surface,
            DonorMode = donor
        };
    }

    private void GenerateAndApply(bool register)
    {
        KryptonCustomThemeSeed seed = ReadSeed();
        _currentPalette = KryptonCustomThemeGenerator.Create(seed);
        ThemeManager.ApplyTheme(_currentPalette, kryptonManager1);

        if (register)
        {
            KryptonCustomThemeGenerator.Register(seed);
        }

        klblStatus.Values.Text = register
            ? $@"Applied and registered '{seed.Name}'. Check the theme combo."
            : $@"Applied '{seed.Name}' ({KryptonCustomThemeGenerator.GetDonorDisplayName(seed.DonorMode)}).";
    }

    private void kbtnApply_Click(object sender, EventArgs e) => GenerateAndApply(register: false);

    private void kbtnRegister_Click(object sender, EventArgs e) => GenerateAndApply(register: true);

    private void kbtnUseRgb_Click(object sender, EventArgs e)
    {
        if (!KryptonCustomThemeGenerator.TryParseColor(ktxtPrimaryRgb.Text, out Color rgb))
        {
            klblStatus.Values.Text = @"RGB must be 'r, g, b' with values 0–255.";
            return;
        }

        _suppressEvents = true;
        kbtnPrimary.SelectedColor = rgb;
        ktxtPrimaryHex.Text = KryptonCustomThemeGenerator.FormatColor(rgb);
        _suppressEvents = false;
        GenerateAndApply(register: false);
    }

    private void kbtnExport_Click(object sender, EventArgs e)
    {
        KryptonCustomThemeSeed seed = ReadSeed();
        _currentPalette ??= KryptonCustomThemeGenerator.Create(seed);

        using var dialog = new SaveFileDialog
        {
            Filter = @"Krypton Palette (*.xml)|*.xml",
            FileName = seed.Name + @".xml",
            Title = @"Export generated theme"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        KryptonCustomThemeGenerator.Export(_currentPalette, dialog.FileName);
        klblStatus.Values.Text = $@"Exported to {dialog.FileName}";
    }

    private void kbtnBuilder_Click(object sender, EventArgs e)
    {
        KryptonCustomThemeBuilder.Show(this, ReadSeed());
        klblStatus.Values.Text = @"Builder closed. Theme combo lists any themes you registered.";
    }

    private void kbtnReset_Click(object sender, EventArgs e)
    {
        kryptonManager1.GlobalPaletteMode = PaletteMode.Office2010Blue;
        klblStatus.Values.Text = @"Reset to builtin Office 2010 Blue.";
    }

    private void OnSeedChanged(object? sender, EventArgs e)
    {
        if (_suppressEvents)
        {
            return;
        }

        kbtnSecondary.Enabled = kchkSecondary.Checked;
        kbtnSurface.Enabled = kchkSurface.Checked;

        if (ReferenceEquals(sender, kbtnPrimary))
        {
            ktxtPrimaryHex.Text = KryptonCustomThemeGenerator.FormatColor(kbtnPrimary.SelectedColor);
            ktxtPrimaryRgb.Text = string.Format(CultureInfo.InvariantCulture, @"{0}, {1}, {2}",
                kbtnPrimary.SelectedColor.R, kbtnPrimary.SelectedColor.G, kbtnPrimary.SelectedColor.B);
        }
    }

    private void OnColorChanged(object? sender, ColorEventArgs e) => OnSeedChanged(sender, e);

    private void kbtnPickPrimary_Click(object? sender, EventArgs e) =>
        PickFromScreen(kbtnPrimary, ktxtPrimaryHex, null);

    private void kbtnPickSecondary_Click(object? sender, EventArgs e) =>
        PickFromScreen(kbtnSecondary, null, kchkSecondary);

    private void kbtnPickSurface_Click(object? sender, EventArgs e) =>
        PickFromScreen(kbtnSurface, null, kchkSurface);

    private void PickFromScreen(KryptonColorButton target, KryptonTextBox? hexBox, KryptonCheckBox? enableCheck)
    {
        if (!KryptonScreenColorPicker.TryPick(this, out Color color))
        {
            return;
        }

        _suppressEvents = true;
        if (enableCheck != null)
        {
            enableCheck.Checked = true;
            target.Enabled = true;
        }

        target.SelectedColor = color;
        if (hexBox != null)
        {
            hexBox.Text = KryptonCustomThemeGenerator.FormatColor(color);
            ktxtPrimaryRgb.Text = string.Format(CultureInfo.InvariantCulture, @"{0}, {1}, {2}",
                color.R, color.G, color.B);
        }

        _suppressEvents = false;
        GenerateAndApply(register: false);
    }
}
