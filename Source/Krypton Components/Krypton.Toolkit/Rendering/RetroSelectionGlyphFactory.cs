#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

internal static class RetroSelectionGlyphFactory
{
    private static readonly Color RetroText = Color.Black;
    private static readonly Color RetroDisabled = Color.FromArgb(192, 192, 192);
    private static readonly Color RetroSilverFill = Color.FromArgb(192, 192, 192);

    internal static Image[] CreateCheckBoxStrip(Size size)
    {
        return new Image[]
        {
            CreateCheckBoxImage(false, false, false, size),
            CreateCheckBoxImage(true, false, false, size),
            CreateCheckBoxImage(true, false, false, size),
            CreateCheckBoxImage(true, false, false, size),
            CreateCheckBoxImage(false, true, false, size),
            CreateCheckBoxImage(true, true, false, size),
            CreateCheckBoxImage(true, true, false, size),
            CreateCheckBoxImage(true, true, false, size),
            CreateCheckBoxImage(false, false, true, size),
            CreateCheckBoxImage(true, false, true, size),
            CreateCheckBoxImage(true, false, true, size),
            CreateCheckBoxImage(true, false, true, size)
        };
    }

    internal static Image[] CreateRadioButtonArray(Size size)
    {
        return new Image[]
        {
            CreateRadioImage(false, false, size),
            CreateRadioImage(true, false, size),
            CreateRadioImage(true, false, size),
            CreateRadioImage(true, false, size),
            CreateRadioImage(false, true, size),
            CreateRadioImage(true, true, size),
            CreateRadioImage(true, true, size),
            CreateRadioImage(true, true, size)
        };
    }

    internal static Image CreateMenuCheckedGlyph(Size size, bool enabled) =>
        DrawTextGlyph(enabled ? "[X]" : "[X]", size, enabled);

    internal static Image CreateMenuIndeterminateGlyph(Size size, bool enabled) =>
        DrawTextGlyph("[#]", size, enabled);

    internal static Image CreateMenuSubMenuArrow(Size size) =>
        DrawTextGlyph(">", size, true);

    private static Image CreateCheckBoxImage(bool enabled, bool isChecked, bool isIndeterminate, Size size)
    {
        string text = isChecked ? "[X]" : (isIndeterminate ? "[#]" : "[ ]");
        return DrawTextGlyph(text, size, enabled, fillDisabled: true);
    }

    private static Image CreateRadioImage(bool enabled, bool isChecked, Size size)
    {
        string text = isChecked ? "(*)" : "( )";
        return DrawTextGlyph(text, size, enabled, fillDisabled: true);
    }

    private static Image DrawTextGlyph(string text, Size size, bool enabled, bool fillDisabled = false)
    {
        var bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            if (!enabled && fillDisabled)
            {
                using (var back = new SolidBrush(RetroSilverFill))
                {
                    g.FillRectangle(back, 0, 0, size.Width, size.Height);
                }
            }

            var color = enabled ? RetroText : RetroDisabled;
            using (var font = new Font("Courier New", 8f, FontStyle.Bold, GraphicsUnit.Point))
            using (var brush = new SolidBrush(color))
            {
                var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(text, font, brush, new RectangleF(0, 0, size.Width, size.Height), format);
            }
        }

        return bmp;
    }
}
