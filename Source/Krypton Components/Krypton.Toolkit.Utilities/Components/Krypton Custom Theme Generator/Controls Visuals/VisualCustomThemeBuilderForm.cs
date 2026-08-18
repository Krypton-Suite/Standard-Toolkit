#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Dialog that collects seed colours and generates, applies, registers, or exports a custom theme.
/// </summary>
internal partial class VisualCustomThemeBuilderForm : KryptonForm
{
    private readonly KryptonCustomThemeSeed _initialSeed;
    private KryptonCustomPaletteBase? _currentPalette;
    private Image? _dropperGlyph;
    private bool _suppressPreview;

    internal VisualCustomThemeBuilderForm(KryptonCustomThemeSeed seed)
    {
        _initialSeed = seed;
        InitializeComponent();
        ConfigureScreenPickers();
        LoadSeed(seed);
        UpdatePreview();
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
        button.ToolTipValues.Description = @"Hides this dialog, then magnifies pixels under the cursor. Click to sample, Esc or right-click to cancel.";
    }

    private void LoadSeed(KryptonCustomThemeSeed seed)
    {
        _suppressPreview = true;
        ktxtName.Text = seed.Name;
        kbtnPrimary.SelectedColor = seed.Primary;
        ktxtPrimaryHex.Text = KryptonCustomThemeGenerator.FormatColor(seed.Primary);

        kchkSecondary.Checked = seed.Secondary.HasValue;
        Color secondary = seed.Secondary ?? CustomThemeColorMath.Analogous(seed.Primary, 30f);
        kbtnSecondary.SelectedColor = secondary;
        ktxtSecondaryHex.Text = KryptonCustomThemeGenerator.FormatColor(secondary);
        kbtnSecondary.Enabled = kchkSecondary.Checked;
        ktxtSecondaryHex.Enabled = kchkSecondary.Checked;

        kchkSurface.Checked = seed.Surface.HasValue;
        Color surface = seed.Surface ?? Color.White;
        kbtnSurface.SelectedColor = surface;
        ktxtSurfaceHex.Text = KryptonCustomThemeGenerator.FormatColor(surface);
        kbtnSurface.Enabled = kchkSurface.Checked;
        ktxtSurfaceHex.Enabled = kchkSurface.Checked;

        kcmbDonor.Items.Clear();
        int selected = 0;
        IReadOnlyList<PaletteMode> donors = KryptonCustomThemeGenerator.SupportedDonorModes;
        for (int i = 0; i < donors.Count; i++)
        {
            var item = new DonorItem(donors[i]);
            kcmbDonor.Items.Add(item);
            if (donors[i] == seed.DonorMode)
            {
                selected = i;
            }
        }

        kcmbDonor.SelectedIndex = selected;

        kcmbFlyout.Items.Clear();
        kcmbFlyout.Items.Add(KryptonScreenColorPicker.GetFlyoutStyleDisplayName(KryptonScreenColorPickerFlyoutStyle.Krypton));
        kcmbFlyout.Items.Add(KryptonScreenColorPicker.GetFlyoutStyleDisplayName(KryptonScreenColorPickerFlyoutStyle.Classic));
        kcmbFlyout.SelectedIndex = KryptonScreenColorPicker.DefaultFlyoutStyle == KryptonScreenColorPickerFlyoutStyle.Classic ? 1 : 0;
        knudMagnifierSize.Value = KryptonScreenColorPicker.DefaultMagnifierSize;
        _suppressPreview = false;
    }

    private KryptonCustomThemeSeed ReadSeed()
    {
        Color primary = kbtnPrimary.SelectedColor;
        if (KryptonCustomThemeGenerator.TryParseColor(ktxtPrimaryHex.Text, out Color parsedPrimary))
        {
            primary = parsedPrimary;
        }

        Color? secondary = null;
        if (kchkSecondary.Checked)
        {
            secondary = kbtnSecondary.SelectedColor;
            if (KryptonCustomThemeGenerator.TryParseColor(ktxtSecondaryHex.Text, out Color parsedSecondary))
            {
                secondary = parsedSecondary;
            }
        }

        Color? surface = null;
        if (kchkSurface.Checked)
        {
            surface = kbtnSurface.SelectedColor;
            if (KryptonCustomThemeGenerator.TryParseColor(ktxtSurfaceHex.Text, out Color parsedSurface))
            {
                surface = parsedSurface;
            }
        }

        PaletteMode donor = PaletteMode.Office2010Blue;
        if (kcmbDonor.SelectedItem is DonorItem item)
        {
            donor = item.Mode;
        }

        string name = string.IsNullOrWhiteSpace(ktxtName.Text) ? @"Custom Theme" : ktxtName.Text.Trim();

        return new KryptonCustomThemeSeed
        {
            Name = name,
            Primary = primary,
            Secondary = secondary,
            Surface = surface,
            DonorMode = donor
        };
    }

    private void UpdatePreview()
    {
        if (_suppressPreview || !IsHandleCreated)
        {
            return;
        }

        try
        {
            KryptonCustomThemeSeed seed = ReadSeed();
            _currentPalette = KryptonCustomThemeGenerator.Create(seed);
            ApplyPreviewPalette(_currentPalette);
            klblStatus.Text = string.Format(CultureInfo.CurrentCulture, @"Previewing '{0}'.", seed.Name);
        }
        catch (Exception ex)
        {
            klblStatus.Text = ex.Message;
        }
    }

    private void OnColorChanged(object? sender, ColorEventArgs e) => OnSeedChanged(sender, e);

    private void OnSeedChanged(object? sender, EventArgs e)
    {
        if (_suppressPreview)
        {
            return;
        }

        if (ReferenceEquals(sender, kbtnPrimary))
        {
            ktxtPrimaryHex.Text = KryptonCustomThemeGenerator.FormatColor(kbtnPrimary.SelectedColor);
        }
        else if (ReferenceEquals(sender, kbtnSecondary))
        {
            ktxtSecondaryHex.Text = KryptonCustomThemeGenerator.FormatColor(kbtnSecondary.SelectedColor);
        }
        else if (ReferenceEquals(sender, kbtnSurface))
        {
            ktxtSurfaceHex.Text = KryptonCustomThemeGenerator.FormatColor(kbtnSurface.SelectedColor);
        }

        kbtnSecondary.Enabled = kchkSecondary.Checked;
        ktxtSecondaryHex.Enabled = kchkSecondary.Checked;
        kbtnSurface.Enabled = kchkSurface.Checked;
        ktxtSurfaceHex.Enabled = kchkSurface.Checked;
        UpdatePreview();
    }

    private void OnHexLeave(object? sender, EventArgs e)
    {
        if (_suppressPreview)
        {
            return;
        }

        if (ReferenceEquals(sender, ktxtPrimaryHex) && KryptonCustomThemeGenerator.TryParseColor(ktxtPrimaryHex.Text, out Color primary))
        {
            _suppressPreview = true;
            kbtnPrimary.SelectedColor = primary;
            ktxtPrimaryHex.Text = KryptonCustomThemeGenerator.FormatColor(primary);
            _suppressPreview = false;
        }
        else if (ReferenceEquals(sender, ktxtSecondaryHex) && KryptonCustomThemeGenerator.TryParseColor(ktxtSecondaryHex.Text, out Color secondary))
        {
            _suppressPreview = true;
            kbtnSecondary.SelectedColor = secondary;
            ktxtSecondaryHex.Text = KryptonCustomThemeGenerator.FormatColor(secondary);
            _suppressPreview = false;
        }
        else if (ReferenceEquals(sender, ktxtSurfaceHex) && KryptonCustomThemeGenerator.TryParseColor(ktxtSurfaceHex.Text, out Color surface))
        {
            _suppressPreview = true;
            kbtnSurface.SelectedColor = surface;
            ktxtSurfaceHex.Text = KryptonCustomThemeGenerator.FormatColor(surface);
            _suppressPreview = false;
        }

        UpdatePreview();
    }

    private KryptonScreenColorPickerFlyoutStyle ReadFlyoutStyle() =>
        kcmbFlyout.SelectedIndex == 1
            ? KryptonScreenColorPickerFlyoutStyle.Classic
            : KryptonScreenColorPickerFlyoutStyle.Krypton;

    private int ReadMagnifierSize() =>
        KryptonScreenColorPicker.ClampMagnifierSize(decimal.ToInt32(knudMagnifierSize.Value));

    private void knudMagnifierSize_ValueChanged(object? sender, EventArgs e)
    {
        int next = ReadMagnifierSize();
        if (decimal.ToInt32(knudMagnifierSize.Value) != next)
        {
            knudMagnifierSize.Value = next;
        }
    }

    private void kbtnApply_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonCustomThemeSeed seed = ReadSeed();
            _currentPalette = KryptonCustomThemeGenerator.Create(seed);
            ThemeManager.ApplyTheme(_currentPalette, new KryptonManager());
            klblStatus.Text = string.Format(CultureInfo.CurrentCulture, @"Applied '{0}' as the global custom theme.", seed.Name);
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            klblStatus.Text = ex.Message;
        }
    }

    private void kbtnRegister_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonCustomThemeSeed seed = ReadSeed();
            KryptonCustomThemeGenerator.Register(seed);
            klblStatus.Text = string.Format(CultureInfo.CurrentCulture, @"Registered '{0}' in theme selectors.", seed.Name);
        }
        catch (Exception ex)
        {
            klblStatus.Text = ex.Message;
        }
    }

    private void kbtnExport_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonCustomThemeSeed seed = ReadSeed();
            _currentPalette ??= KryptonCustomThemeGenerator.Create(seed);

            using var dialog = new SaveFileDialog
            {
                Title = @"Export custom theme",
                Filter = @"Krypton Palette (*.xml)|*.xml|All files (*.*)|*.*",
                FileName = seed.Name + @".xml",
                OverwritePrompt = true
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                KryptonCustomThemeGenerator.Export(_currentPalette, dialog.FileName);
                klblStatus.Text = string.Format(CultureInfo.CurrentCulture, @"Exported '{0}'.", dialog.FileName);
            }
        }
        catch (Exception ex)
        {
            klblStatus.Text = ex.Message;
        }
    }

    private void kbtnPickPrimary_Click(object? sender, EventArgs e) =>
        PickFromScreen(kbtnPrimary, ktxtPrimaryHex, null);

    private void kbtnPickSecondary_Click(object? sender, EventArgs e) =>
        PickFromScreen(kbtnSecondary, ktxtSecondaryHex, kchkSecondary);

    private void kbtnPickSurface_Click(object? sender, EventArgs e) =>
        PickFromScreen(kbtnSurface, ktxtSurfaceHex, kchkSurface);

    private void PickFromScreen(KryptonColorButton target, KryptonTextBox hexBox, KryptonCheckBox? enableCheck)
    {
        if (!KryptonScreenColorPicker.TryPick(this, ReadFlyoutStyle(), ReadMagnifierSize(), out Color color))
        {
            return;
        }

        knudMagnifierSize.Value = KryptonScreenColorPicker.DefaultMagnifierSize;

        _suppressPreview = true;
        if (enableCheck != null)
        {
            enableCheck.Checked = true;
            target.Enabled = true;
            hexBox.Enabled = true;
        }

        target.SelectedColor = color;
        hexBox.Text = KryptonCustomThemeGenerator.FormatColor(color);
        _suppressPreview = false;
        UpdatePreview();
    }

    private void kbtnReset_Click(object? sender, EventArgs e)
    {
        LoadSeed(_initialSeed);
        _currentPalette = null;
        ApplyPreviewPalette(null);
        UpdatePreview();
        klblStatus.Text = @"Reset to the original seed.";
    }

    private void kbtnRandom_Click(object? sender, EventArgs e)
    {
        LoadSeed(KryptonCustomThemeGenerator.CreateRandomSeed());
        UpdatePreview();
        klblStatus.Text = @"Generated a random theme seed.";
    }

    private void kbtnClose_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void VisualCustomThemeBuilderForm_Load(object? sender, EventArgs e) => UpdatePreview();

    private void ApplyPreviewPalette(KryptonCustomPaletteBase? palette)
    {
        LocalCustomPalette = palette;
        ApplyPreviewPalette(this, palette);
    }

    private static void ApplyPreviewPalette(Control parent, KryptonCustomPaletteBase? palette)
    {
        foreach (Control child in parent.Controls)
        {
            if (child is VisualControlBase visual)
            {
                visual.LocalCustomPalette = palette;
            }
            else if (child is VisualPanel panel)
            {
                panel.Palette = palette;
            }
            else if (child is VisualContainerControlBase container)
            {
                container.Palette = palette;
            }

            ApplyPreviewPalette(child, palette);
        }
    }

    private sealed class DonorItem
    {
        internal DonorItem(PaletteMode mode)
        {
            Mode = mode;
            Label = KryptonCustomThemeGenerator.GetDonorDisplayName(mode);
        }

        internal PaletteMode Mode { get; }

        internal string Label { get; }

        public override string ToString() => Label;
    }
}
