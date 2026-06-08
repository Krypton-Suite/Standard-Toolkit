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
/// Specifies the direction of a drop-down arrow glyph.
/// </summary>
internal enum DropDownArrowGlyphDirection
{

    /// <summary>
    /// Indicates the arrow points down.
    /// </summary>
    Down,
    /// <summary>
    /// Indicates the arrow points up.
    /// </summary>
    Up,
    /// <summary>
    /// Indicates the arrow points left.
    /// </summary>
    Left,
    /// <summary>
    /// Indicates the arrow points right.
    /// </summary>
    Right
}

/// <summary>
/// Specifies the layer of a drop-down arrow glyph.
/// </summary>
internal enum DropDownArrowGlyphLayer
{
    /// <summary>
    /// Indicates the fill layer of the glyph.
    /// </summary>
    Fill,
    /// <summary>
    /// Indicates the outline layer of the glyph.
    /// </summary>
    Outline
}

/// <summary>
/// Provides factory methods for creating drop-down arrow glyphs.
/// </summary>
internal static class DropDownArrowGlyphFactory
{
    /// <summary>
    /// Specifies the font families to use for the glyphs.
    /// </summary>
    private static readonly string[] SymbolFontFamilies = ["Segoe UI Symbol", "Segoe UI"];

    /// <summary>
    /// Creates a drop-down arrow glyph.
    /// </summary>
    /// <param name="size">The size of the glyph.</param>
    /// <param name="outline">The outline color of the glyph.</param>
    /// <param name="fill">The fill color of the glyph.</param>
    /// <param name="direction">The direction of the glyph.</param>
    /// <param name="renderMode">The render mode of the glyph.</param>
    /// <param name="style">The glyph drawing style.</param>
    internal static Image Create(int size, Color outline, Color fill, DropDownArrowGlyphDirection direction, DropDownArrowRenderMode renderMode, DropDownArrowGlyphStyle style)
    {
        if (!HasDistinctFill(outline, fill))
        {
            return CreateMonochrome(size, outline, direction, renderMode, DropDownArrowGlyphLayer.Fill);
        }

        DropDownArrowGlyphStyleLayout.GetLayerOffsets(style, size, out Point fillOffset, out Point outlineOffset);

        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);

            using Image fillGlyph = CreateMonochrome(size, fill, direction, renderMode, DropDownArrowGlyphLayer.Fill);

            using Image outlineGlyph = CreateMonochrome(size, outline, direction, renderMode, DropDownArrowGlyphLayer.Outline);

            g.DrawImage(fillGlyph, fillOffset);

            g.DrawImage(outlineGlyph, outlineOffset);

        }

        return bmp;
    }

    /// <summary>
    /// Creates a monochrome drop-down arrow glyph.
    /// </summary>
    /// <param name="size">The size of the glyph.</param>
    /// <param name="color">The color of the glyph.</param>
    /// <param name="direction">The direction of the glyph.</param>
    /// <param name="renderMode">The render mode of the glyph.</param>
    /// <param name="layer">The layer of the glyph.</param>
    internal static Image CreateMonochrome(int size, Color color, DropDownArrowGlyphDirection direction,  DropDownArrowRenderMode renderMode, DropDownArrowGlyphLayer layer)
        => renderMode == DropDownArrowRenderMode.Unicode
            ? CreateUnicodeMonochrome(size, color, direction)
            : CreatePolygonMonochrome(size, color, direction, layer);

    /// <summary>
    /// Creates a Unicode monochrome drop-down arrow glyph.
    /// </summary>
    /// <param name="size">The size of the glyph.</param>
    /// <param name="color">The color of the glyph.</param>
    /// <param name="direction">The direction of the glyph.</param>
    private static Image CreateUnicodeMonochrome(int size, Color color, DropDownArrowGlyphDirection direction)
    {
        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        using (var g = Graphics.FromImage(bmp))
        {

            g.Clear(Color.Transparent);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;


            char glyph = GetUnicodeCharacter(direction, size);

            using var format = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip)
            {

                Alignment = StringAlignment.Center,

                LineAlignment = StringAlignment.Center
            };

            using var font = CreateSymbolFont(g, size, glyph);

            using var brush = new SolidBrush(color);

            g.DrawString(glyph.ToString(), font, brush, new RectangleF(0, 0, size, size), format);

        }

        return bmp;
    }

    private static Image CreatePolygonMonochrome(int size, Color color, DropDownArrowGlyphDirection direction,  DropDownArrowGlyphLayer layer)
    {
        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        using (var g = Graphics.FromImage(bmp))
        {

            g.Clear(Color.Transparent);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            Point[] triangle = GetTrianglePoints(size, direction);

            if (layer == DropDownArrowGlyphLayer.Fill)
            {
                using var brush = new SolidBrush(color);

                g.FillPolygon(brush, triangle);
            }
            else
            {
                using var pen = new Pen(color, 1f);

                g.DrawPolygon(pen, triangle);
            }
        }

        return bmp;
    }

    internal static bool HasDistinctFill(Color outline, Color fill) =>
        fill.A >= 16 && fill.ToArgb() != outline.ToArgb();

    private static char GetUnicodeCharacter(DropDownArrowGlyphDirection direction, int size)
    {
        bool useSmall = size <= 12;

        return direction switch
        {
            DropDownArrowGlyphDirection.Up => useSmall ? '\u25B4' : '\u25B2',
            DropDownArrowGlyphDirection.Left => useSmall ? '\u25C2' : '\u25C0',
            DropDownArrowGlyphDirection.Right => useSmall ? '\u25B8' : '\u25B6',
            _ => useSmall ? '\u25BE' : '\u25BC'
        };
    }

    private static Font CreateSymbolFont(Graphics g, int boxSize, char glyph)
    {
        string text = glyph.ToString();

        for (float emSize = boxSize * 0.95f; emSize >= boxSize * 0.45f; emSize -= 0.5f)
        {
            foreach (string familyName in SymbolFontFamilies)
            {
                try
                {
                    using var probe = new Font(familyName, emSize, FontStyle.Regular, GraphicsUnit.Pixel);

                    SizeF measured = g.MeasureString(text, probe);

                    if (measured.Width <= boxSize && measured.Height <= boxSize)
                    {
                        return new Font(familyName, emSize, FontStyle.Regular, GraphicsUnit.Pixel);
                    }
                }
                catch (ArgumentException)
                {

                }
            }
        }

        FontFamily fontFamily = SystemFonts.MessageBoxFont?.FontFamily ?? FontFamily.GenericSansSerif;

        return new Font(fontFamily, boxSize * 0.7f, FontStyle.Regular, GraphicsUnit.Pixel);
    }

    private static Point[] GetTrianglePoints(int size, DropDownArrowGlyphDirection direction)
    {

        int pad = Math.Max(1, size / 4);

        int near = pad;

        int far = size - pad - 1;

        int mid = size / 2;

        return direction switch
        {
            DropDownArrowGlyphDirection.Up => new[]
            {
                new Point(mid, near),

                new Point(near, far),

                new Point(far, far)
            },
            DropDownArrowGlyphDirection.Left => new[]
            {
                new Point(near, mid),

                new Point(far, near),

                new Point(far, far)
            },
            DropDownArrowGlyphDirection.Right => new[]
            {
                new Point(far, mid),

                new Point(near, near),

                new Point(near, far)
            },
            _ => new[]
            {
                new Point(near, near),

                new Point(far, near),

                new Point(mid, far)
            }
        };
    }
}
