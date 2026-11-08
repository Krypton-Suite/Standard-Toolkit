#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Applies manifest banner and pulse settings to the update windows.</summary>
internal static class KryptonUpdatePresentation
{
    private const int BannerHeight = 48;
    private const int BannerImageSize = 32;
    private const int MaxImageBytes = 512 * 1024;

    /// <summary>Turns form chrome pulsing on or off from the package pulse object.</summary>
    /// <param name="form">Prompt, notes, or progress window.</param>
    /// <param name="pulse">Manifest pulse, or null when omitted.</param>
    internal static void ApplyPulse(KryptonForm form, KryptonUpdatePulse? pulse)
    {
        if (form == null)
        {
            return;
        }

        var values = form.PulsingBorderValues;
        if (pulse == null || !pulse.Enable)
        {
            values.Enable = false;
            return;
        }

        values.Enable = true;
        values.Animate = true;
        values.ShowWhen = InputPulsingBorderShowWhen.Always;
        values.Style = pulse.Style;
        values.AnimationSpeed = pulse.Speed;
        if (!pulse.Color.IsEmpty)
        {
            values.Colors.Color1 = pulse.Color;
            values.Colors.Color2 = Lighten(pulse.Color, 0.35f);
            values.Colors.HighlightColor = Lighten(pulse.Color, 0.55f);
        }
    }

    /// <summary>
    /// Shows or hides the banner strip. Returns the client height added when the strip becomes visible.
    /// </summary>
    /// <param name="host">Top-docked banner panel.</param>
    /// <param name="label">Caption label inside the host.</param>
    /// <param name="image">Optional image box. Hidden when there is no image.</param>
    /// <param name="banner">Manifest banner, or null.</param>
    /// <param name="loadedImage">Image already loaded for <see cref="KryptonUpdateBanner.ImageUrl"/>, or null.</param>
    /// <returns>Pixels added to the client height, or zero when the strip stays hidden.</returns>
    internal static int ApplyBanner(KryptonPanel host, KryptonLabel label, PictureBox image, KryptonUpdateBanner? banner, Image? loadedImage)
    {
        if (host == null || label == null || image == null)
        {
            return 0;
        }

        if (banner == null || !banner.HasContent)
        {
            host.Visible = false;
            host.Height = 0;
            image.Image = null;
            image.Visible = false;
            return 0;
        }

        var height = Scaled(BannerHeight, host);
        host.Height = height;
        host.Visible = true;
        if (!banner.BackColor.IsEmpty || !banner.BackColor2.IsEmpty)
        {
            var color1 = banner.BackColor.IsEmpty ? banner.BackColor2 : banner.BackColor;
            var color2 = banner.BackColor2.IsEmpty ? color1 : banner.BackColor2;
            host.StateCommon.Color1 = color1;
            host.StateCommon.Color2 = color2;
            host.StateCommon.ColorStyle = color2 == color1
                ? PaletteColorStyle.Solid
                : PaletteColorStyle.Linear;
        }

        var text = banner.Text ?? string.Empty;
        label.Values.Text = text;
        label.Visible = text != null && !string.IsNullOrWhiteSpace(text);
        if (!banner.ForeColor.IsEmpty)
        {
            label.StateCommon.ShortText.Color1 = banner.ForeColor;
        }

        if (loadedImage != null)
        {
            image.Image = loadedImage;
            image.Visible = true;
            image.Size = new Size(Scaled(BannerImageSize, host), Scaled(BannerImageSize, host));
        }
        else
        {
            image.Image = null;
            image.Visible = false;
        }

        return height;
    }

    /// <summary>Loads a banner image from https or a local path. Returns null when the address is missing or the load fails.</summary>
    /// <param name="imageUrl">Manifest image address.</param>
    /// <param name="cancellationToken">Cancels the download.</param>
    internal static async Task<Image?> LoadBannerImageAsync(string? imageUrl, CancellationToken cancellationToken = default)
    {
        if (imageUrl == null || string.IsNullOrWhiteSpace(imageUrl))
        {
            return null;
        }

        try
        {
            byte[] bytes;
            if (KryptonUpdateClient.TryGetLocalPath(imageUrl, out var path))
            {
                var info = new FileInfo(path);
                if (!info.Exists || info.Length <= 0 || info.Length > MaxImageBytes)
                {
                    return null;
                }

                bytes = File.ReadAllBytes(path);
            }
            else if (KryptonUpdateClient.IsHttps(imageUrl))
            {
                bytes = await KryptonUpdateClient.DownloadBytesAsync(imageUrl, MaxImageBytes, cancellationToken).ConfigureAwait(true);
            }
            else
            {
                return null;
            }

            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            using (var stream = new MemoryStream(bytes))
            {
                using (var source = Image.FromStream(stream))
                {
                    return new Bitmap(source);
                }
            }
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or ArgumentException
                                              or OutOfMemoryException or KryptonUpdateException or HttpRequestException
                                              or TaskCanceledException)
        {
            return null;
        }
    }

    private static Color Lighten(Color color, float amount)
    {
        amount = amount < 0f ? 0f : amount > 1f ? 1f : amount;
        var r = color.R + (int)((255 - color.R) * amount);
        var g = color.G + (int)((255 - color.G) * amount);
        var b = color.B + (int)((255 - color.B) * amount);
        return Color.FromArgb(color.A, Clamp(r), Clamp(g), Clamp(b));
    }

    private static int Clamp(int value) => value < 0 ? 0 : value > 255 ? 255 : value;

    private static int Scaled(int value, Control control)
    {
        var dpi = control.DeviceDpi;
        if (dpi <= 0)
        {
            dpi = 96;
        }

        return value * dpi / 96;
    }
}
