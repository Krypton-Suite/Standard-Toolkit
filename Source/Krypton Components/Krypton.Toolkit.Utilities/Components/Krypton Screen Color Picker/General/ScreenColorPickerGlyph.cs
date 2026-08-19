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
/// Tiny eyedropper glyph used on screen-picker buttons.
/// </summary>
internal static class ScreenColorPickerGlyph
{
    internal static Image Create()
    {
        var bitmap = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.Transparent);
            using (var pen = new Pen(Color.FromArgb(40, 40, 40), 1.6f))
            using (var fill = new SolidBrush(Color.FromArgb(70, 130, 180)))
            {
                graphics.DrawLine(pen, 3, 13, 9, 7);
                graphics.FillEllipse(fill, 8, 1, 7, 7);
                graphics.DrawEllipse(pen, 8, 1, 7, 7);
                graphics.DrawLine(pen, 4, 12, 2, 14);
            }
        }

        return bitmap;
    }
}
