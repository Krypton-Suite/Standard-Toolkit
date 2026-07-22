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
    /// <param name="fitToCell">true to scale the visible glyph outline to fill the cell.</param>
    internal static Image Create(int size, Color outline, Color fill, DropDownArrowGlyphDirection direction, DropDownArrowRenderMode renderMode, DropDownArrowGlyphStyle style, bool fitToCell = false)
    {
        if (!HasDistinctFill(outline, fill))
        {
            return CreateMonochrome(size, outline, direction, renderMode, DropDownArrowGlyphLayer.Fill, fitToCell);
        }

        DropDownArrowGlyphStyleLayout.GetLayerOffsets(style, size, out Point fillOffset, out Point outlineOffset, direction);

        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);

            using Image fillGlyph = CreateMonochrome(size, fill, direction, renderMode, DropDownArrowGlyphLayer.Fill, fitToCell);

            using Image outlineGlyph = CreateMonochrome(size, outline, direction, renderMode, DropDownArrowGlyphLayer.Outline, fitToCell);

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
    /// <param name="fitToCell">true to scale the visible glyph outline to fill the cell.</param>
    internal static Image CreateMonochrome(int size, Color color, DropDownArrowGlyphDirection direction,  DropDownArrowRenderMode renderMode, DropDownArrowGlyphLayer layer, bool fitToCell = false)
        => renderMode == DropDownArrowRenderMode.Unicode
            ? fitToCell
                ? CreateUnicodeMonochromeFitted(size, color, direction)
                : CreateUnicodeMonochrome(size, color, direction)
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

    /// <summary>
    /// Creates a Unicode monochrome glyph whose visible outline is scaled to fill the
    /// cell. The standard Unicode path fits the font by measured line height, which
    /// leaves most of a small cell as em padding; this variant extracts the glyph
    /// outline as a path and scales the actual ink to the cell, so fixed-size cells
    /// (e.g. scrollbar arrow buttons) get a properly sized arrow at any DPI.
    /// </summary>
    /// <param name="size">The size of the glyph.</param>
    /// <param name="color">The color of the glyph.</param>
    /// <param name="direction">The direction of the glyph.</param>
    private static Image CreateUnicodeMonochromeFitted(int size, Color color, DropDownArrowGlyphDirection direction)
    {
        char glyph = GetUnicodeCharacter(direction, size);

        using var path = new System.Drawing.Drawing2D.GraphicsPath();

        FontFamily? symbolFamily = TryCreateSymbolFontFamily();

        try
        {
            FontFamily family = symbolFamily ?? (SystemFonts.MessageBoxFont?.FontFamily ?? FontFamily.GenericSansSerif);

            path.AddString(glyph.ToString(), family, (int)FontStyle.Regular, size, PointF.Empty, StringFormat.GenericTypographic);
        }
        finally
        {
            symbolFamily?.Dispose();
        }

        RectangleF bounds = path.GetBounds();

        if (bounds.Width < 1f || bounds.Height < 1f)
        {
            // Glyph outline unavailable; fall back to the crisp polygon triangle.
            return CreatePolygonMonochrome(size, color, direction, DropDownArrowGlyphLayer.Fill);
        }

        // Leave a 1px margin so anti-aliased edges are not clipped by the cell.
        const float Margin = 1f;
        float available = size - (2f * Margin);
        float scale = Math.Min(available / bounds.Width, available / bounds.Height);

        using (var matrix = new System.Drawing.Drawing2D.Matrix())
        {
            matrix.Translate(
                Margin + ((available - (bounds.Width * scale)) / 2f) - (bounds.X * scale),
                Margin + ((available - (bounds.Height * scale)) / 2f) - (bounds.Y * scale));
            matrix.Scale(scale, scale);
            path.Transform(matrix);
        }

        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var brush = new SolidBrush(color);

            g.FillPath(brush, path);
        }

        return bmp;
    }

    private static FontFamily? TryCreateSymbolFontFamily()
    {
        foreach (string familyName in SymbolFontFamilies)
        {
            try
            {
                return new FontFamily(familyName);
            }
            catch (ArgumentException)
            {

            }
        }

        return null;
    }

    private static Image CreatePolygonMonochrome(int size, Color color, DropDownArrowGlyphDirection direction,  DropDownArrowGlyphLayer layer)
    {
        var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        using (var g = Graphics.FromImage(bmp))
        {

            g.Clear(Color.Transparent);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            //Point[] triangle = GetTrianglePoints(size, direction);
            //Always draw the polygon pointing downward, and then rotate and/or flip the image to the correct orientation
            //The result is uniform polygons in all directions.

            int near = Math.Max(1, size / 4);

            int far = size - near - 1;

            int mid = size / 2;

            Point[] triangle = new[]
            {
                new Point(near, near),

                new Point(far, near),

                new Point(mid, far)
            };

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

        switch (direction)
        {
            case DropDownArrowGlyphDirection.Up:
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                break;

            case DropDownArrowGlyphDirection.Left:
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                break;

            case DropDownArrowGlyphDirection.Right:
                bmp.RotateFlip(RotateFlipType.Rotate270FlipY);
                break;
        }

        return bmp;
    }

    internal static bool HasDistinctFill(Color outline, Color fill) =>
        fill.A >= 16 && fill.ToArgb() != outline.ToArgb();

    private static char GetUnicodeCharacter(DropDownArrowGlyphDirection direction, int size)
    {
        bool useSmall = size <= 4;

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
