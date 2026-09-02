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
/// Demonstrates independent menu, tool, status, and context-menu item text via
/// <see cref="SchemeBaseColors.MenuStripText"/>, <see cref="SchemeBaseColors.ToolStripText"/>,
/// <see cref="SchemeBaseColors.StatusStripText"/>, and <see cref="SchemeBaseColors.MenuItemText"/> (Issue #1100).
/// </summary>
public partial class SchemeStripTextDemo : KryptonForm
{
    private PaletteBase? _subscribedPalette;

    public SchemeStripTextDemo()
    {
        InitializeComponent();
        Load += OnLoad;
        FormClosed += OnFormClosed;
    }

    private void OnLoad(object? sender, EventArgs e)
    {
        kryptonManager1.GlobalPaletteMode = PaletteMode.Microsoft365Blue;
        kgrpNative.Panel.ContextMenuStrip = nativeContextMenuStrip1;
        WirePaletteSchemeColorChanged();
        RefreshSchemeReadout();
        UpdateDescription();
    }

    private void OnFormClosed(object? sender, FormClosedEventArgs e) => UnwirePaletteSchemeColorChanged();

    private void kryptonThemeComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        WirePaletteSchemeColorChanged();
        RefreshSchemeReadout();
        UpdateDescription();
    }

    private void UpdateDescription()
    {
        var theme = kryptonThemeComboBox1.SelectedItem?.ToString() ?? "Unknown";
        klblDescription.Values.Text =
            $"Theme: {theme}. MenuStripText / ToolStripText / StatusStripText colour the three strips. " +
            "MenuItemText colours File/Edit dropdowns, the KryptonContextMenu button, and the native right-click menu. " +
            "Empty slots keep the historic alias. Contrast demo paints each chrome family a different colour.";
    }

    private void WirePaletteSchemeColorChanged()
    {
        UnwirePaletteSchemeColorChanged();
        _subscribedPalette = KryptonManager.CurrentGlobalPalette;
        if (_subscribedPalette is not null)
        {
            _subscribedPalette.SchemeColorChanged += OnSchemeColorChanged;
        }
    }

    private void UnwirePaletteSchemeColorChanged()
    {
        if (_subscribedPalette is not null)
        {
            _subscribedPalette.SchemeColorChanged -= OnSchemeColorChanged;
            _subscribedPalette = null;
        }
    }

    private void OnSchemeColorChanged(object? sender, SchemeColorChangedEventArgs e) => RefreshSchemeReadout();

    private void RefreshSchemeReadout()
    {
        var table = KryptonManager.CurrentGlobalPalette?.ColorTable;
        klblSchemeReadout.Values.Text =
            $"Slots: MenuStripText={FormatSchemeColor(SchemeBaseColors.MenuStripText)}  |  " +
            $"ToolStripText={FormatSchemeColor(SchemeBaseColors.ToolStripText)}  |  " +
            $"StatusStripText={FormatSchemeColor(SchemeBaseColors.StatusStripText)}  |  " +
            $"MenuItemText={FormatSchemeColor(SchemeBaseColors.MenuItemText)}{Environment.NewLine}" +
            $"ColorTable: MenuStrip={FormatColor(table?.MenuStripText)}  ToolStrip={FormatColor(table?.ToolStripText)}  " +
            $"StatusStrip={FormatColor(table?.StatusStripText)}  MenuItem={FormatColor(table?.MenuItemText)}";
    }

    private static string FormatSchemeColor(SchemeBaseColors role)
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        if (palette is null)
        {
            return "(no palette)";
        }

        try
        {
            return FormatColor(palette.GetSchemeColor(role));
        }
        catch (IndexOutOfRangeException)
        {
            return "(unavailable)";
        }
    }

    private static string FormatColor(Color? color)
    {
        if (color is null)
        {
            return "(none)";
        }

        var value = color.Value;
        if (value.IsEmpty || value == SharedStaticVariables.EMPTY_COLOR)
        {
            return "(theme default)";
        }

        return $"#{value.R:X2}{value.G:X2}{value.B:X2}";
    }

    private void kcbtnMenuStrip_SelectedColorChanged(object? sender, ColorEventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.MenuStripText, e.Color);

    private void kcbtnToolStrip_SelectedColorChanged(object? sender, ColorEventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.ToolStripText, e.Color);

    private void kcbtnStatusStrip_SelectedColorChanged(object? sender, ColorEventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.StatusStripText, e.Color);

    private void kcbtnMenuItem_SelectedColorChanged(object? sender, ColorEventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.MenuItemText, e.Color);

    private void kbtnContrastDemo_Click(object? sender, EventArgs e)
    {
        ApplySchemeColor(SchemeBaseColors.MenuStripText, Color.Firebrick);
        ApplySchemeColor(SchemeBaseColors.ToolStripText, Color.MediumBlue);
        ApplySchemeColor(SchemeBaseColors.StatusStripText, Color.DarkGreen);
        ApplySchemeColor(SchemeBaseColors.MenuItemText, Color.DarkOrange);
        kcbtnMenuStrip.SelectedColor = Color.Firebrick;
        kcbtnToolStrip.SelectedColor = Color.MediumBlue;
        kcbtnStatusStrip.SelectedColor = Color.DarkGreen;
        kcbtnMenuItem.SelectedColor = Color.DarkOrange;
        klblStatus.Values.Text =
            "Contrast demo: menu strip = firebrick, tool strip = medium blue, status strip = dark green, menu items = dark orange.";
        RefreshSchemeReadout();
    }

    private void kbtnResetAll_Click(object? sender, EventArgs e)
    {
        kcbtnMenuStrip.SelectedColor = Color.Empty;
        kcbtnToolStrip.SelectedColor = Color.Empty;
        kcbtnStatusStrip.SelectedColor = Color.Empty;
        kcbtnMenuItem.SelectedColor = Color.Empty;
        ApplySchemeColor(SchemeBaseColors.MenuStripText, Color.Empty);
        ApplySchemeColor(SchemeBaseColors.ToolStripText, Color.Empty);
        ApplySchemeColor(SchemeBaseColors.StatusStripText, Color.Empty);
        ApplySchemeColor(SchemeBaseColors.MenuItemText, Color.Empty);
        klblStatus.Values.Text = "Scheme overrides cleared. Re-select the theme to restore built-in defaults.";
        RefreshSchemeReadout();
    }

    private void kbtnKryptonContext_Click(object? sender, EventArgs e) =>
        kryptonContextMenu1.Show(kbtnKryptonContext);

    private void ApplySchemeColor(SchemeBaseColors role, Color color)
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        if (palette is null)
        {
            return;
        }

        try
        {
            if (color.IsEmpty || color.A == 0)
            {
                palette.SetSchemeColor(role, SharedStaticVariables.EMPTY_COLOR);
                klblStatus.Values.Text = $"{role} reset. Re-select the theme for the built-in default.";
            }
            else
            {
                palette.SetSchemeColor(role, color);
                klblStatus.Values.Text = $"{role} set to #{color.R:X2}{color.G:X2}{color.B:X2}.";
            }
        }
        catch (IndexOutOfRangeException)
        {
            klblStatus.Values.Text = $"{role} is not available on this palette's scheme array.";
        }

        RefreshSchemeReadout();
        Invalidate(true);
        kryptonMenuStrip1.Invalidate();
        kryptonToolStrip1.Invalidate();
        kryptonStatusStrip1.Invalidate();
        nativeMenuStrip1.Invalidate();
        nativeToolStrip1.Invalidate();
        nativeStatusStrip1.Invalidate();
    }
}
