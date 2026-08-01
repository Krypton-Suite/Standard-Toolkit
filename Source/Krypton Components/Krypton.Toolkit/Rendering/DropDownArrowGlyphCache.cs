#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class DropDownArrowGlyphCache
{
    private static readonly Dictionary<(int Size, int Color, DropDownArrowGlyphDirection Direction, DropDownArrowRenderMode Mode, DropDownArrowGlyphLayer Layer, bool FitToCell), Image> _monochromeCache = new();

    private static readonly Dictionary<(int Size, int Outline, int Fill, DropDownArrowGlyphDirection Direction, DropDownArrowRenderMode Mode, DropDownArrowGlyphStyle Style, bool FitToCell), Image> _compositeCache = new();

    private static readonly object _lock = new();

    static DropDownArrowGlyphCache()
    {
        KryptonManager.GlobalPaletteChanged += (_, _) => Clear();
    }

    internal static Image GetOrCreate(int size, Color outline, Color fill, DropDownArrowGlyphDirection direction, bool fitToCell = false)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        var mode = KryptonManager.DropDownArrowRenderMode;

        var style = KryptonManager.DropDownArrowGlyphStyle;

        if (!DropDownArrowGlyphFactory.HasDistinctFill(outline, fill))
        {
            return GetMonochrome(size, outline, direction, mode, DropDownArrowGlyphLayer.Fill, fitToCell);
        }

        var key = (size, outline.ToArgb(), fill.ToArgb(), direction, mode, style, fitToCell);

        lock (_lock)
        {
            if (_compositeCache.TryGetValue(key, out var cached))
            {
                return cached;
            }

            var image = DropDownArrowGlyphFactory.Create(size, outline, fill, direction, mode, style, fitToCell);

            _compositeCache[key] = image;

            return image;
        }
    }

    private static Image GetMonochrome(int size, Color color, DropDownArrowGlyphDirection direction, DropDownArrowRenderMode mode, DropDownArrowGlyphLayer layer, bool fitToCell)
    {
        var key = (size, color.ToArgb(), direction, mode, layer, fitToCell);

        lock (_lock)
        {
            if (_monochromeCache.TryGetValue(key, out var cached))
            {
                return cached;
            }

            var image = DropDownArrowGlyphFactory.CreateMonochrome(size, color, direction, mode, layer, fitToCell);

            _monochromeCache[key] = image;

            return image;
        }
    }

    internal static void Draw(Graphics g, Rectangle cellRect, Color outline, Color fill, DropDownArrowGlyphDirection direction, Control? control = null)
    {
        int size = DropDownArrowGlyphMetrics.ResolvePixelSize(g, cellRect, control);

        if (size <= 0)
        {
            return;
        }

        Image glyph = GetOrCreate(size, outline, fill, direction);

        int x = cellRect.X + ((cellRect.Width - size) / 2);

        int y = cellRect.Y + ((cellRect.Height - size) / 2);

        g.DrawImage(glyph, x, y, size, size);

    }

    /// <summary>
    /// Draws a glyph that fills the supplied cell, without the DPI-scaled drop-down
    /// metrics. Used for cells that are already expressed in physical pixels, such as
    /// the fixed-size scrollbar arrow buttons. Unicode glyphs are scaled to their
    /// outline so the visible ink fills the cell instead of the font's em padding.
    /// </summary>
    internal static void DrawFixedCell(Graphics g, Rectangle cellRect, Color outline, Color fill, DropDownArrowGlyphDirection direction)
    {
        int size = Math.Min(cellRect.Width, cellRect.Height);

        if (size <= 0)
        {
            return;
        }

        Image glyph = GetOrCreate(size, outline, fill, direction, fitToCell: true);

        int x = cellRect.X + ((cellRect.Width - size) / 2);

        int y = cellRect.Y + ((cellRect.Height - size) / 2);

        g.DrawImage(glyph, x, y, size, size);
    }

    internal static void Clear()
    {
        lock (_lock)
        {
            foreach (Image image in _monochromeCache.Values)
            {
                image.Dispose();
            }

            foreach (Image image in _compositeCache.Values)
            {
                image.Dispose();
            }

            _monochromeCache.Clear();

            _compositeCache.Clear();
        }
    }
}
