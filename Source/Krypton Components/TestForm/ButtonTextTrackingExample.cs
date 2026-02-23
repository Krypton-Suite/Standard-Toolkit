#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of the ButtonTextTracking feature (Issue #1326).
/// Shows alternate text color for tracking (hover) state on KryptonButton, KryptonCheckButton,
/// KryptonColorButton, and other controls for improved readability in themes where the default
/// tracking background makes text hard to read.
/// </summary>
public partial class ButtonTextTrackingExample : KryptonForm
{
    public ButtonTextTrackingExample()
    {
        InitializeComponent();
        Load += OnLoad;
    }

    private void OnLoad(object? sender, EventArgs e)
    {
        // Default to Microsoft 365 Black Dark Mode to demonstrate the feature (has ButtonTextTracking = Black)
        kryptonManager1.GlobalPaletteMode = PaletteMode.Microsoft365BlackDarkMode;
        UpdateDescription();
    }

    private void kryptonThemeComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateDescription();
    }

    private void UpdateDescription()
    {
        var theme = kryptonThemeComboBox1.SelectedItem?.ToString() ?? "Unknown";
        klblDescription.Values.Text = $"Theme: {theme}. Hover over the buttons below to see the tracking (hover) text color. " +
            "In dark themes (e.g. Microsoft 365 Black Dark Mode), an alternate color ensures text stays readable.";
    }

    private void kcbtnTrackingColor_SelectedColorChanged(object? sender, Krypton.Toolkit.ColorEventArgs e)
    {
        ApplyTrackingColor(e.Color);
    }

    private void kbtnApplyColor_Click(object? sender, EventArgs e)
    {
        ApplyTrackingColor(kcbtnTrackingColor.SelectedColor);
    }

    private void ApplyTrackingColor(Color color)
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        if (palette is null)
            return;

        if (color.IsEmpty || color.A == 0)
        {
            palette.SetSchemeColor(SchemeBaseColors.ButtonTextTracking, GlobalStaticValues.EMPTY_COLOR);
            palette.SetSchemeExtraColor(SchemeExtraColors.ButtonTextTracking, GlobalStaticValues.EMPTY_COLOR);
            klblStatus.Values.Text = "Tracking color reset to theme default.";
        }
        else
        {
            palette.SetSchemeColor(SchemeBaseColors.ButtonTextTracking, color);
            palette.SetSchemeExtraColor(SchemeExtraColors.ButtonTextTracking, color);
            klblStatus.Values.Text = $"Tracking text color set to {color.Name}.";
        }
    }

    private void kbtnReset_Click(object? sender, EventArgs e)
    {
        kcbtnTrackingColor.SelectedColor = Color.Empty;
        ApplyTrackingColor(Color.Empty);
    }
}
