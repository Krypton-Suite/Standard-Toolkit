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
    private static readonly Dictionary<(int Size, int Outline, int Fill, DropDownArrowGlyphDirection Direction, DropDownArrowRenderMode Mode), Image> _cache = new();
    private static readonly object _lock = new();

    static DropDownArrowGlyphCache()
    {
        KryptonManager.GlobalPaletteChanged += (_, _) => Clear();
    }

    internal static Image GetOrCreate(int size, Color outline, Color fill, DropDownArrowGlyphDirection direction)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        var mode = KryptonManager.DropDownArrowRenderMode;
        var key = (size, outline.ToArgb(), fill.ToArgb(), direction, mode);

        lock (_lock)
        {
            if (_cache.TryGetValue(key, out var cached))
            {
                return cached;
            }

            var image = DropDownArrowGlyphFactory.Create(size, outline, fill, direction, mode);
            _cache[key] = image;
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

    internal static void Clear()
    {
        lock (_lock)
        {
            foreach (Image image in _cache.Values)
            {
                image.Dispose();
            }

            _cache.Clear();
        }
    }
}
