#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Horizontal text measure/draw via P/Invoke GDI (<c>DrawTextW</c> / <c>DrawTextExW</c>).
/// Faster than <see cref="TextRenderer"/> when HFONT handles are cached; does not honour
/// <see cref="Graphics"/> transforms (use GDI+ for rotated text / printing).
/// </summary>
public static class GdiNativeText
{
    #region Static Fields

    // Managed-only TextFormatFlags that must not be passed to Win32 DrawText.
    private const TextFormatFlags ManagedOnlyFlags =
        TextFormatFlags.PreserveGraphicsClipping |
        TextFormatFlags.PreserveGraphicsTranslateTransform;

    private static readonly object _fontCacheLock = new object();
    private static readonly Dictionary<FontCacheKey, IntPtr> _fontCache = new Dictionary<FontCacheKey, IntPtr>();

    #endregion

    #region Public

    /// <summary>
    /// Measure text with native GDI using the same DT flags that <see cref="Draw"/> will use.
    /// </summary>
    /// <param name="g">Graphics providing an HDC.</param>
    /// <param name="text">Text to measure.</param>
    /// <param name="font">Font to select into the DC.</param>
    /// <param name="flags">WinForms text format flags (mapped to DT_*).</param>
    /// <param name="proposedSize">Proposed layout size; width/height of <see cref="int.MaxValue"/> mean unconstrained.</param>
    /// <param name="useFontCache">When true, reuse cached HFONT handles.</param>
    /// <returns>Measured pixel size.</returns>
    public static Size Measure(Graphics g,
        string text,
        Font font,
        TextFormatFlags flags,
        Size proposedSize,
        bool useFontCache = true)
    {
        if (g == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(g));
        }

        if (text == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(text));
        }

        if (font == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(font));
        }

        if (text.Length == 0)
        {
            return Size.Empty;
        }

        var dt = ToNativeDrawTextFlags(flags) | PI.DT_.CALCRECT;
        var rect = new PI.RECT
        {
            left = 0,
            top = 0,
            right = proposedSize.Width == int.MaxValue || proposedSize.Width <= 0 ? 1 : proposedSize.Width,
            bottom = proposedSize.Height == int.MaxValue || proposedSize.Height <= 0 ? 1 : proposedSize.Height
        };

        RunWithFont(g, font, useFontCache, hdc =>
        {
            if ((flags & TextFormatFlags.NoPadding) != 0)
            {
                var dtp = PI.DRAWTEXTPARAMS.Create();
                dtp.iLeftMargin = 0;
                dtp.iRightMargin = 0;
                PI.DrawTextExW(hdc, text, text.Length, ref rect, dt, ref dtp);
            }
            else
            {
                PI.DrawTextW(hdc, text, text.Length, ref rect, dt);
            }
        });

        return new Size(Math.Max(0, rect.right - rect.left), Math.Max(0, rect.bottom - rect.top));
    }

    /// <summary>
    /// Draw text with native GDI into <paramref name="bounds"/>.
    /// </summary>
    /// <param name="g">Graphics providing an HDC.</param>
    /// <param name="text">Text to draw.</param>
    /// <param name="font">Font to select into the DC.</param>
    /// <param name="bounds">Destination rectangle.</param>
    /// <param name="foreColor">Solid text colour.</param>
    /// <param name="flags">WinForms text format flags (mapped to DT_*).</param>
    /// <param name="useFontCache">When true, reuse cached HFONT handles.</param>
    public static void Draw(Graphics g,
        string text,
        Font font,
        Rectangle bounds,
        Color foreColor,
        TextFormatFlags flags,
        bool useFontCache = true)
    {
        if (g == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(g));
        }

        if (text == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(text));
        }

        if (font == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(font));
        }

        if (text.Length == 0 || bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        var dt = ToNativeDrawTextFlags(flags);
        var rect = new PI.RECT
        {
            left = bounds.Left,
            top = bounds.Top,
            right = bounds.Right,
            bottom = bounds.Bottom
        };

        RunWithFont(g, font, useFontCache, hdc =>
        {
            PI.SetBkMode(hdc, PI.BKMODE_TRANSPARENT);
            PI.SetTextColor(hdc, ColorTranslator.ToWin32(foreColor));

            if ((flags & TextFormatFlags.NoPadding) != 0)
            {
                var dtp = PI.DRAWTEXTPARAMS.Create();
                dtp.iLeftMargin = 0;
                dtp.iRightMargin = 0;
                PI.DrawTextExW(hdc, text, text.Length, ref rect, dt, ref dtp);
            }
            else
            {
                PI.DrawTextW(hdc, text, text.Length, ref rect, dt);
            }
        });
    }

    /// <summary>
    /// Fast single-line baseline draw via <c>ExtTextOutW</c> (no wrap / ellipsis / hotkey).
    /// Intended for benchmarks; not used by <see cref="AccurateText"/>.
    /// </summary>
    public static void ExtTextOut(Graphics g,
        string text,
        Font font,
        Point location,
        Color foreColor,
        bool useFontCache = true)
    {
        if (g == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(g));
        }

        if (text == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(text));
        }

        if (font == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(font));
        }

        if (text.Length == 0)
        {
            return;
        }

        var empty = new PI.RECT();
        RunWithFont(g, font, useFontCache, hdc =>
        {
            PI.SetBkMode(hdc, PI.BKMODE_TRANSPARENT);
            PI.SetTextColor(hdc, ColorTranslator.ToWin32(foreColor));
            PI.ExtTextOutW(hdc, location.X, location.Y, 0, ref empty, text, (uint)text.Length, IntPtr.Zero);
        });
    }

    /// <summary>
    /// Release all cached HFONT handles. Call after a global font/theme change if fonts are replaced.
    /// </summary>
    public static void ClearFontCache()
    {
        lock (_fontCacheLock)
        {
            foreach (var handle in _fontCache.Values)
            {
                if (handle != IntPtr.Zero)
                {
                    PI.DeleteObject(handle);
                }
            }

            _fontCache.Clear();
        }
    }

    /// <summary>
    /// Convert <see cref="TextFormatFlags"/> to Win32 DT_* flag bits,
    /// stripping managed-only bits that <see cref="TextRenderer"/> also strips.
    /// </summary>
    public static int ToDrawTextFlags(TextFormatFlags flags)
    {
        var raw = (int)(flags & ~ManagedOnlyFlags);
        return raw;
    }

    #endregion

    #region Implementation

    private static PI.DT_ ToNativeDrawTextFlags(TextFormatFlags flags) => (PI.DT_)ToDrawTextFlags(flags);

    private static void RunWithFont(Graphics g, Font font, bool useFontCache, Action<IntPtr> action)
    {
        var hdc = g.GetHdc();
        IntPtr hFont = IntPtr.Zero;
        IntPtr oldFont = IntPtr.Zero;
        var ownsFont = false;

        try
        {
            hFont = AcquireFont(font, useFontCache, out ownsFont);
            oldFont = PI.SelectObject(hdc, hFont);
            action(hdc);
        }
        finally
        {
            if (oldFont != IntPtr.Zero)
            {
                PI.SelectObject(hdc, oldFont);
            }

            if (ownsFont && hFont != IntPtr.Zero)
            {
                PI.DeleteObject(hFont);
            }

            g.ReleaseHdc(hdc);
        }
    }

    private static IntPtr AcquireFont(Font font, bool useFontCache, out bool ownsFont)
    {
        if (!useFontCache)
        {
            ownsFont = true;
            return font.ToHfont();
        }

        var key = FontCacheKey.FromFont(font);
        lock (_fontCacheLock)
        {
            if (_fontCache.TryGetValue(key, out var cached) && cached != IntPtr.Zero)
            {
                ownsFont = false;
                return cached;
            }

            var created = font.ToHfont();
            _fontCache[key] = created;
            ownsFont = false;
            return created;
        }
    }

    private readonly struct FontCacheKey : IEquatable<FontCacheKey>
    {
        private readonly string _name;
        private readonly float _size;
        private readonly FontStyle _style;
        private readonly GraphicsUnit _unit;
        private readonly byte _gdiCharSet;

        private FontCacheKey(string name, float size, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            _name = name;
            _size = size;
            _style = style;
            _unit = unit;
            _gdiCharSet = gdiCharSet;
        }

        public static FontCacheKey FromFont(Font font) =>
            new FontCacheKey(font.Name, font.Size, font.Style, font.Unit, font.GdiCharSet);

        public bool Equals(FontCacheKey other) =>
            _size.Equals(other._size)
            && _style == other._style
            && _unit == other._unit
            && _gdiCharSet == other._gdiCharSet
            && string.Equals(_name, other._name, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object? obj) => obj is FontCacheKey other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = _name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(_name) : 0;
                hash = (hash * 397) ^ _size.GetHashCode();
                hash = (hash * 397) ^ (int)_style;
                hash = (hash * 397) ^ (int)_unit;
                hash = (hash * 397) ^ _gdiCharSet;
                return hash;
            }
        }
    }

    #endregion
}
