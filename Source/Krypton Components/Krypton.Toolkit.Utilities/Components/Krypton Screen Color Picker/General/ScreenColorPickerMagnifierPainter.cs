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
/// Shared nearest-neighbour magnifier drawing for Classic and Krypton flyouts.
/// </summary>
internal static class ScreenColorPickerMagnifierPainter
{
    internal static void Draw(Graphics graphics, Bitmap screenshot, Point samplePoint, Rectangle imageRect, int sourceOdd, int zoom)
    {
        int odd = KryptonScreenColorPicker.ClampMagnifierSize(sourceOdd);
        int half = odd / 2;
        var source = new Rectangle(samplePoint.X - half, samplePoint.Y - half, odd, odd);
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.PixelOffsetMode = PixelOffsetMode.Half;
        graphics.DrawImage(screenshot, imageRect, source, GraphicsUnit.Pixel);

        if (zoom >= 8)
        {
            using (var grid = new Pen(Color.FromArgb(60, 255, 255, 255)))
            {
                for (int i = 1; i < odd; i++)
                {
                    int x = imageRect.X + (i * zoom);
                    int y = imageRect.Y + (i * zoom);
                    graphics.DrawLine(grid, x, imageRect.Top, x, imageRect.Bottom);
                    graphics.DrawLine(grid, imageRect.Left, y, imageRect.Right, y);
                }
            }
        }

        var center = new Rectangle(imageRect.X + (half * zoom), imageRect.Y + (half * zoom), zoom, zoom);
        using (var centerPen = new Pen(Color.White, 2f))
        {
            graphics.DrawRectangle(centerPen, center);
        }

        using (var blackPen = new Pen(Color.Black))
        {
            graphics.DrawRectangle(blackPen, Rectangle.Inflate(center, 1, 1));
        }
    }

    internal static string FormatRgbHex(Color color) =>
        string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
}
