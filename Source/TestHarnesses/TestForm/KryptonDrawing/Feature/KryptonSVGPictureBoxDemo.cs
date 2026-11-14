#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Issue #4657: <see cref="KryptonSVGPictureBox"/> markup, file load, size modes, resize, tooltip, and invalid SVG.
/// </summary>
public partial class KryptonSVGPictureBoxDemo : KryptonForm
{
    private const string SampleSvg =
        "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 120 60\">" +
        "<rect width=\"120\" height=\"60\" fill=\"#1b3a4b\"/>" +
        "<circle cx=\"36\" cy=\"30\" r=\"16\" fill=\"#e6c07b\"/>" +
        "<polygon points=\"70,12 108,30 70,48\" fill=\"#ffffff\"/>" +
        "</svg>";

    private const string InvalidSvg = "<svg xmlns=\"http://www.w3.org/2000/svg\"><rect";

    public KryptonSVGPictureBoxDemo()
    {
        InitializeComponent();
        foreach (PictureBoxSizeMode mode in Enum.GetValues(typeof(PictureBoxSizeMode)))
        {
            kcmbSizeMode.Items.Add(mode);
        }

        ksvg.ToolTipValues.EnableToolTips = true;
        ksvg.ToolTipValues.Heading = "SVG picture";
        ksvg.ToolTipValues.Description = "Krypton tooltip. Resize the form to re-rasterize Zoom and Stretch at the current DPI.";
        kcmbSizeMode.SelectedItem = PictureBoxSizeMode.Zoom;
        ApplyMarkup(SampleSvg);
    }

    private void OnSizeModeChanged(object? sender, EventArgs e)
    {
        if (kcmbSizeMode.SelectedItem is PictureBoxSizeMode mode)
        {
            ksvg.SizeMode = mode;
            ksvg.Dock = mode == PictureBoxSizeMode.AutoSize ? DockStyle.None : DockStyle.Fill;
            if (mode == PictureBoxSizeMode.AutoSize)
            {
                ksvg.Location = new Point(8, 8);
            }
        }

        RefreshStatus();
    }

    private void OnSampleClick(object? sender, EventArgs e) => ApplyMarkup(SampleSvg);

    private void OnApplyClick(object? sender, EventArgs e) => ApplyMarkup(ktxtSvg.Text);

    private void OnInvalidClick(object? sender, EventArgs e) => ApplyMarkup(InvalidSvg);

    private void OnLoadClick(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "SVG files (*.svg)|*.svg|All files (*.*)|*.*",
            Title = "Load SVG"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            ksvg.LoadFromFile(dialog.FileName);
            ktxtSvg.Text = ksvg.Svg;
        }
        catch (Exception ex)
        {
            klblStatus.Values.Text = ex.Message;
            return;
        }

        RefreshStatus();
    }

    private void OnPictureResize(object? sender, EventArgs e) => RefreshStatus();

    private void ApplyMarkup(string markup)
    {
        ktxtSvg.Text = markup;
        ksvg.Svg = markup;
        RefreshStatus();
    }

    private void RefreshStatus()
    {
        var error = string.IsNullOrEmpty(ksvg.SvgError) ? "none" : ksvg.SvgError;
        klblStatus.Values.Text = $"SizeMode {ksvg.SizeMode}, client {ksvg.ClientSize.Width}x{ksvg.ClientSize.Height}, DPI {ksvg.DeviceDpi}, error {error}";
    }
}
