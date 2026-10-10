#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Recolours a light Office or Sparkle checkbox/radio glyph for a dark palette surface.
/// Near-white fills become a dark face. Borders and marks are lifted and keep their hue,
/// so a blue tick stays blue instead of turning gray.
/// </summary>
internal static class DarkSelectionGlyph
{
    /// <summary>
    /// Returns a new bitmap. The source image is not modified.
    /// </summary>
    internal static Bitmap Recolor(Image source)
    {
        var bitmap = new Bitmap(source.Width, source.Height);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.DrawImage(source, 0, 0, source.Width, source.Height);
        }

        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                Color color = bitmap.GetPixel(x, y);
                if (color.A == 0)
                {
                    continue;
                }

                int luminance = (color.R + color.G + color.B) / 3;
                int target = luminance >= 205
                    ? 30 + ((255 - luminance) / 2)
                    : 255 - (luminance / 3);
                int max = Math.Max(color.R, Math.Max(color.G, color.B));
                int min = Math.Min(color.R, Math.Min(color.G, color.B));
                int red;
                int green;
                int blue;
                if (max - min < 24 || luminance == 0)
                {
                    red = target;
                    green = target;
                    blue = target;
                }
                else
                {
                    float scale = target / (float)luminance;
                    red = ClampChannel((int)Math.Round(color.R * scale));
                    green = ClampChannel((int)Math.Round(color.G * scale));
                    blue = ClampChannel((int)Math.Round(color.B * scale));
                }

                bitmap.SetPixel(x, y, Color.FromArgb(color.A, red, green, blue));
            }
        }

        return bitmap;
    }

    private static int ClampChannel(int channel) => channel < 0 ? 0 : (channel > 255 ? 255 : channel);
}
