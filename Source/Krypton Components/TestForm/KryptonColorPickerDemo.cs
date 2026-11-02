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
/// Designer-sited <see cref="KryptonColorPicker"/> compared with native <see cref="ColorDialog"/>.
/// </summary>
public partial class KryptonColorPickerDemo : KryptonForm
{
    public KryptonColorPickerDemo()
    {
        InitializeComponent();
        kcmbFlyout.Items.Add(KryptonScreenColorPicker.GetFlyoutStyleDisplayName(KryptonScreenColorPickerFlyoutStyle.Krypton));
        kcmbFlyout.Items.Add(KryptonScreenColorPicker.GetFlyoutStyleDisplayName(KryptonScreenColorPickerFlyoutStyle.Classic));
        kcmbFlyout.SelectedIndex = 0;
        knudMagnifier.Value = kryptonColorPicker1.MagnifierSize;
        knudZoom.Value = kryptonColorPicker1.Zoom;
        kryptonColorPicker1.BindColorFormatList(kclbFormats);
        UpdateSwatch(pnlKryptonSwatch, klblKryptonResult, kryptonColorPicker1.Color, @"No colour sampled yet.");
        UpdateSwatch(pnlNativeSwatch, klblNativeResult, colorDialog1.Color, @"No colour chosen yet.");
    }

    private void ApplySettingsToComponent(KryptonColorPicker picker)
    {
        picker.FlyoutStyle = kcmbFlyout.SelectedIndex == 1
            ? KryptonScreenColorPickerFlyoutStyle.Classic
            : KryptonScreenColorPickerFlyoutStyle.Krypton;
        picker.MagnifierSize = decimal.ToInt32(knudMagnifier.Value);
        picker.Zoom = decimal.ToInt32(knudZoom.Value);
    }

    private void kbtnPickKrypton_Click(object sender, EventArgs e)
    {
        bool useSited = kchkUseSitedComponent.Checked;
        KryptonColorPicker picker = useSited ? kryptonColorPicker1 : new KryptonColorPicker();
        try
        {
            ApplySettingsToComponent(picker);
            picker.VisibleColorFormats = kryptonColorPicker1.VisibleColorFormats;
            DialogResult result = picker.ShowDialog(this);
            knudMagnifier.Value = picker.MagnifierSize;
            knudZoom.Value = picker.Zoom;
            if (result == DialogResult.OK)
            {
                UpdateSwatch(pnlKryptonSwatch, klblKryptonResult, picker.Color,
                    @"KryptonColorPicker: sampled from the screen.");
            }
            else
            {
                klblKryptonResult.Values.Text = @"KryptonColorPicker: cancelled.";
            }
        }
        finally
        {
            if (!useSited)
            {
                picker.Dispose();
            }
        }
    }

    private void kbtnPickNative_Click(object sender, EventArgs e)
    {
        DialogResult result = colorDialog1.ShowDialog(this);
        string summary = result == DialogResult.OK
            ? @"Native ColorDialog: chose from the palette dialog."
            : @"Native ColorDialog: cancelled.";
        UpdateSwatch(pnlNativeSwatch, klblNativeResult, colorDialog1.Color, summary);
    }

    private static void UpdateSwatch(Panel swatch, KryptonLabel label, Color color, string summary)
    {
        if (color.IsEmpty)
        {
            swatch.BackColor = SystemColors.ControlDark;
            label.Values.Text = summary;
            return;
        }

        swatch.BackColor = Color.FromArgb(255, color.R, color.G, color.B);
        label.Values.Text = string.Format(CultureInfo.InvariantCulture,
            @"{0}{1}{2}  ·  RGB({3}, {4}, {5})",
            summary, Environment.NewLine, KryptonScreenColorPicker.FormatColor(color, KryptonScreenColorPickerColorFormat.Hex),
            color.R, color.G, color.B);
    }
}
