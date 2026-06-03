#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal enum DropDownArrowGlyphDirection
{
    Down,
    Up,
    Left,
    Right
}

internal static class DropDownArrowGlyphFactory
{
    private static readonly string[] SymbolFontFamilies = ["Segoe UI Symbol", "Segoe UI"];

    internal static Image Create(int size, Color outline, Color fill, DropDownArrowGlyphDirection direction, DropDownArrowRenderMode renderMode)
        => renderMode == DropDownArrowRenderMode.Unicode
            ? CreateUnicode(size, outline, fill, direction)
            : CreatePolygon(size, outline, fill, direction);

    private static Image CreateUnicode(int size, Color outline, Color fill, DropDownArrowGlyphDirection direction)
    {
        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            Point[] triangle = GetTrianglePoints(size, direction);
            if (HasDistinctFill(outline, fill))
            {
                using (var fillBrush = new SolidBrush(fill))
                {
                    g.FillPolygon(fillBrush, triangle);
                }

                using (var outlinePen = new Pen(outline, 1f))
                {
                    g.DrawPolygon(outlinePen, triangle);
                }
            }

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            char glyph = GetUnicodeCharacter(direction, size);
            using var format = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            using var font = CreateSymbolFont(g, size, glyph);
            using var brush = new SolidBrush(outline);
            g.DrawString(glyph.ToString(), font, brush, new RectangleF(0, 0, size, size), format);
        }

        return bmp;
    }

    private static bool HasDistinctFill(Color outline, Color fill) =>
        fill.A >= 16 && fill.ToArgb() != outline.ToArgb();

    private static Image CreatePolygon(int size, Color outline, Color fill, DropDownArrowGlyphDirection direction)
    {
        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            Point[] triangle = GetTrianglePoints(size, direction);

            using (var brush = new SolidBrush(fill))
            {
                g.FillPolygon(brush, triangle);
            }

            using (var pen = new Pen(outline, 1f))
            {
                g.DrawPolygon(pen, triangle);
            }
        }

        return bmp;
    }

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

        return new Font(SystemFonts.MessageBoxFont.FontFamily, boxSize * 0.7f, FontStyle.Regular, GraphicsUnit.Pixel);
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
