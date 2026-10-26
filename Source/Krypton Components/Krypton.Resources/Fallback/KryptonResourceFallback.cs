#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit.ResourceFiles;

/// <summary>
/// Drawn stand-ins used when <c>Krypton.Resources.dll</c> was not deployed beside the application.
/// Stock string resources are still compiled into this assembly, so palette schema text and Outlook grid strings remain available.
/// Images keep the original pixel size and use <see cref="Color.Magenta"/> as the colour key, matching
/// <c>SharedStaticVariables.TRANSPARENCY_KEY_COLOR</c>, so image lists still treat the background as transparent.
/// </summary>
internal static class KryptonResourceFallback
{
    /// <summary>Width and height of <see cref="PlaceholderBitmap"/>.</summary>
    internal const int PlaceholderSize = 16;

    /// <summary>True only on the embedded placeholder assembly, never on the full resource bank.</summary>
    internal static bool IsPlaceholder => true;

    internal static ResourceManager ResourceManager { get; } = new ResourceManager(typeof(KryptonResourceFallback));

    internal static CultureInfo Culture { get; set; }

    internal static Bitmap PlaceholderBitmap => BitmapOf(PlaceholderSize, PlaceholderSize);

    internal static Icon PlaceholderIcon => IconOf(PlaceholderSize, PlaceholderSize);

    internal static byte[] PlaceholderBytes { get; } = CreateBmpBytes();

    private static readonly object _gate = new object();
    private static readonly Dictionary<string, Bitmap> _bitmaps = new Dictionary<string, Bitmap>(StringComparer.Ordinal);
    private static readonly Dictionary<string, Icon> _icons = new Dictionary<string, Icon>(StringComparer.Ordinal);

    private enum FallbackGlyph
    {
        Mark,
        ArrowLeft,
        ArrowRight,
        ArrowUp,
        ArrowDown,
        ChevronDown,
        Check,
        Radio,
        Close,
        Minimize,
        Maximize,
        Folder,
        Error,
        Grip
    }

    private enum FallbackTone
    {
        Normal,
        Disabled,
        Pressed,
        Active
    }

    /// <summary>Drawn fallback at the full resource's dimensions.</summary>
    internal static Bitmap BitmapOf(int width, int height) => BitmapOf(width, height, null);

    /// <summary>
    /// Drawn fallback at the full resource's dimensions. <paramref name="name"/> selects the glyph
    /// (check, arrow, close, and so on). Strips are tiled so <c>ImageList.Images.AddStrip</c> still slices them.
    /// </summary>
    internal static Bitmap BitmapOf(int width, int height, string name)
    {
        width = width > 0 ? width : 1;
        height = height > 0 ? height : 1;
        var glyph = Classify(name);
        var tone = ClassifyTone(name);
        var cellWidth = ChooseCellWidth(width, height);
        var key = width + "x" + height + ":" + (int)glyph + ":" + (int)tone + ":" + cellWidth;
        lock (_gate)
        {
            Bitmap existing;
            if (_bitmaps.TryGetValue(key, out existing))
            {
                return existing;
            }

            var bitmap = DrawBitmap(width, height, cellWidth, glyph, tone);
            _bitmaps.Add(key, bitmap);
            return bitmap;
        }
    }

    /// <summary>Drawn fallback icon at the full resource's dimensions.</summary>
    internal static Icon IconOf(int width, int height) => IconOf(width, height, null);

    /// <summary>Drawn fallback icon. See <see cref="BitmapOf(int, int, string)"/>.</summary>
    internal static Icon IconOf(int width, int height, string name)
    {
        var bitmap = BitmapOf(width, height, name);
        var key = width + "x" + height + ":" + name;
        lock (_gate)
        {
            Icon existing;
            if (_icons.TryGetValue(key, out existing))
            {
                return existing;
            }

            var handle = bitmap.GetHicon();
            try
            {
                using (var icon = Icon.FromHandle(handle))
                {
                    existing = (Icon)icon.Clone();
                }
            }
            finally
            {
                DestroyIcon(handle);
            }

            _icons.Add(key, existing);
            return existing;
        }
    }

    private static int ChooseCellWidth(int width, int height)
    {
        // Single images stay one cell. Horizontal strips (checks, galleries, grid glyphs) are sliced
        // into cells whose height is the bitmap height, which is what ImageList.AddStrip requires.
        if (width < height * 2)
        {
            return width;
        }

        if (height > 0 && width % height == 0)
        {
            var cells = width / height;
            if (cells >= 2 && cells <= 24)
            {
                return height;
            }
        }

        for (var candidate = Math.Min(32, width / 2); candidate >= 8; candidate--)
        {
            if (width % candidate == 0)
            {
                return candidate;
            }
        }

        return width;
    }

    private static FallbackGlyph Classify(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return FallbackGlyph.Mark;
        }

        if (Has(name, "CheckBox") || Has(name, "Check"))
        {
            return FallbackGlyph.Check;
        }

        if (Has(name, "Radio"))
        {
            return FallbackGlyph.Radio;
        }

        if (Has(name, "Close"))
        {
            return FallbackGlyph.Close;
        }

        if (Has(name, "Minimise") || Has(name, "Minimize"))
        {
            return FallbackGlyph.Minimize;
        }

        if (Has(name, "Maximise") || Has(name, "Maximize") || Has(name, "Restore"))
        {
            return FallbackGlyph.Maximize;
        }

        if (Has(name, "DropDown"))
        {
            return FallbackGlyph.ChevronDown;
        }

        if (Has(name, "Arrow") || Has(name, "Chevron"))
        {
            if (Has(name, "Left"))
            {
                return FallbackGlyph.ArrowLeft;
            }

            if (Has(name, "Up"))
            {
                return FallbackGlyph.ArrowUp;
            }

            if (Has(name, "Down"))
            {
                return FallbackGlyph.ArrowDown;
            }

            return FallbackGlyph.ArrowRight;
        }

        if (Has(name, "Folder"))
        {
            return FallbackGlyph.Folder;
        }

        if (Has(name, "Error") || Has(name, "Warning"))
        {
            return FallbackGlyph.Error;
        }

        if (Has(name, "Grip"))
        {
            return FallbackGlyph.Grip;
        }

        return FallbackGlyph.Mark;
    }

    private static FallbackTone ClassifyTone(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return FallbackTone.Normal;
        }

        if (Has(name, "Disabled"))
        {
            return FallbackTone.Disabled;
        }

        if (Has(name, "Pressed"))
        {
            return FallbackTone.Pressed;
        }

        if (Has(name, "Active") || Has(name, "Tracking") || Has(name, "Hot"))
        {
            return FallbackTone.Active;
        }

        return FallbackTone.Normal;
    }

    private static bool Has(string name, string token) => name.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0;

    private static Color ToneColor(FallbackTone tone)
    {
        switch (tone)
        {
            case FallbackTone.Disabled:
                return Color.FromArgb(255, 168, 168, 168);
            case FallbackTone.Pressed:
                return Color.FromArgb(255, 20, 20, 20);
            case FallbackTone.Active:
                return Color.FromArgb(255, 0, 90, 160);
            default:
                return Color.FromArgb(255, 55, 55, 55);
        }
    }

    private static Bitmap DrawBitmap(int width, int height, int cellWidth, FallbackGlyph glyph, FallbackTone tone)
    {
        var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.Magenta);
            graphics.SmoothingMode = cellWidth >= 24 ? SmoothingMode.AntiAlias : SmoothingMode.None;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var color = ToneColor(tone);
            var cells = Math.Max(1, width / cellWidth);
            for (var index = 0; index < cells; index++)
            {
                var cell = new Rectangle(index * cellWidth, 0, cellWidth, height);
                DrawGlyph(graphics, cell, glyph, color, index);
            }
        }

        return bitmap;
    }

    private static void DrawGlyph(Graphics graphics, Rectangle cell, FallbackGlyph glyph, Color color, int index)
    {
        var pad = Math.Max(1, Math.Min(cell.Width, cell.Height) / 6);
        var box = Rectangle.Inflate(cell, -pad, -pad);
        if (box.Width < 2 || box.Height < 2)
        {
            return;
        }

        switch (glyph)
        {
            case FallbackGlyph.ArrowLeft:
                FillArrow(graphics, box, color, FallbackGlyph.ArrowLeft);
                break;
            case FallbackGlyph.ArrowRight:
                FillArrow(graphics, box, color, FallbackGlyph.ArrowRight);
                break;
            case FallbackGlyph.ArrowUp:
                FillArrow(graphics, box, color, FallbackGlyph.ArrowUp);
                break;
            case FallbackGlyph.ArrowDown:
            case FallbackGlyph.ChevronDown:
                FillArrow(graphics, box, color, FallbackGlyph.ArrowDown);
                break;
            case FallbackGlyph.Check:
                DrawCheck(graphics, box, color, index);
                break;
            case FallbackGlyph.Radio:
                DrawRadio(graphics, box, color, index);
                break;
            case FallbackGlyph.Close:
                DrawClose(graphics, box, color);
                break;
            case FallbackGlyph.Minimize:
                DrawMinimize(graphics, box, color);
                break;
            case FallbackGlyph.Maximize:
                DrawMaximize(graphics, box, color);
                break;
            case FallbackGlyph.Folder:
                DrawFolder(graphics, box, color);
                break;
            case FallbackGlyph.Error:
                DrawError(graphics, box, color);
                break;
            case FallbackGlyph.Grip:
                DrawGrip(graphics, box, color);
                break;
            default:
                DrawMark(graphics, box, color);
                break;
        }
    }

    private static void FillArrow(Graphics graphics, Rectangle box, Color color, FallbackGlyph direction)
    {
        Point[] points;
        var midX = box.Left + (box.Width / 2);
        var midY = box.Top + (box.Height / 2);
        var shaft = box.Width / 3;
        switch (direction)
        {
            case FallbackGlyph.ArrowLeft:
                points = new[]
                {
                    new Point(box.Right, box.Top + (box.Height / 3)),
                    new Point(box.Left + shaft, box.Top + (box.Height / 3)),
                    new Point(box.Left + shaft, box.Top),
                    new Point(box.Left, midY),
                    new Point(box.Left + shaft, box.Bottom),
                    new Point(box.Left + shaft, box.Top + ((box.Height * 2) / 3)),
                    new Point(box.Right, box.Top + ((box.Height * 2) / 3))
                };
                break;
            case FallbackGlyph.ArrowUp:
                points = new[]
                {
                    new Point(box.Left + (box.Width / 3), box.Bottom),
                    new Point(box.Left + (box.Width / 3), box.Top + shaft),
                    new Point(box.Left, box.Top + shaft),
                    new Point(midX, box.Top),
                    new Point(box.Right, box.Top + shaft),
                    new Point(box.Left + ((box.Width * 2) / 3), box.Top + shaft),
                    new Point(box.Left + ((box.Width * 2) / 3), box.Bottom)
                };
                break;
            case FallbackGlyph.ArrowDown:
                points = new[]
                {
                    new Point(box.Left + (box.Width / 3), box.Top),
                    new Point(box.Left + (box.Width / 3), box.Bottom - shaft),
                    new Point(box.Left, box.Bottom - shaft),
                    new Point(midX, box.Bottom),
                    new Point(box.Right, box.Bottom - shaft),
                    new Point(box.Left + ((box.Width * 2) / 3), box.Bottom - shaft),
                    new Point(box.Left + ((box.Width * 2) / 3), box.Top)
                };
                break;
            default:
                points = new[]
                {
                    new Point(box.Left, box.Top + (box.Height / 3)),
                    new Point(box.Right - shaft, box.Top + (box.Height / 3)),
                    new Point(box.Right - shaft, box.Top),
                    new Point(box.Right, midY),
                    new Point(box.Right - shaft, box.Bottom),
                    new Point(box.Right - shaft, box.Top + ((box.Height * 2) / 3)),
                    new Point(box.Left, box.Top + ((box.Height * 2) / 3))
                };
                break;
        }

        using (var brush = new SolidBrush(color))
        {
            graphics.FillPolygon(brush, points);
        }
    }

    private static void DrawCheck(Graphics graphics, Rectangle box, Color color, int index)
    {
        var state = index % 3;
        using (var pen = new Pen(color, Math.Max(1f, box.Width / 8f)))
        {
            graphics.DrawRectangle(pen, box);
            if (state == 1)
            {
                graphics.DrawLines(pen, new[]
                {
                    new Point(box.Left + (box.Width / 5), box.Top + (box.Height / 2)),
                    new Point(box.Left + ((box.Width * 2) / 5), box.Bottom - (box.Height / 5)),
                    new Point(box.Right - (box.Width / 6), box.Top + (box.Height / 4))
                });
            }
            else if (state == 2)
            {
                var y = box.Top + (box.Height / 2);
                graphics.DrawLine(pen, box.Left + (box.Width / 4), y, box.Right - (box.Width / 4), y);
            }
        }
    }

    private static void DrawRadio(Graphics graphics, Rectangle box, Color color, int index)
    {
        using (var pen = new Pen(color, Math.Max(1f, box.Width / 8f)))
        {
            graphics.DrawEllipse(pen, box);
        }

        if (index % 2 == 1)
        {
            var inner = Rectangle.Inflate(box, -Math.Max(2, box.Width / 4), -Math.Max(2, box.Height / 4));
            if (inner.Width > 0 && inner.Height > 0)
            {
                using (var brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, inner);
                }
            }
        }
    }

    private static void DrawClose(Graphics graphics, Rectangle box, Color color)
    {
        using (var pen = new Pen(color, Math.Max(1f, Math.Min(box.Width, box.Height) / 6f)))
        {
            graphics.DrawLine(pen, box.Left, box.Top, box.Right, box.Bottom);
            graphics.DrawLine(pen, box.Right, box.Top, box.Left, box.Bottom);
        }
    }

    private static void DrawMinimize(Graphics graphics, Rectangle box, Color color)
    {
        using (var pen = new Pen(color, Math.Max(1f, box.Height / 6f)))
        {
            var y = box.Bottom - Math.Max(1, box.Height / 4);
            graphics.DrawLine(pen, box.Left, y, box.Right, y);
        }
    }

    private static void DrawMaximize(Graphics graphics, Rectangle box, Color color)
    {
        using (var pen = new Pen(color, Math.Max(1f, Math.Min(box.Width, box.Height) / 8f)))
        {
            graphics.DrawRectangle(pen, box);
        }
    }

    private static void DrawFolder(Graphics graphics, Rectangle box, Color color)
    {
        var tab = new Rectangle(box.Left, box.Top, Math.Max(2, box.Width / 2), Math.Max(2, box.Height / 4));
        var body = new Rectangle(box.Left, box.Top + tab.Height, box.Width, box.Height - tab.Height);
        using (var brush = new SolidBrush(color))
        {
            graphics.FillRectangle(brush, tab);
            graphics.FillRectangle(brush, body);
        }
    }

    private static void DrawError(Graphics graphics, Rectangle box, Color color)
    {
        Point[] triangle =
        {
            new Point(box.Left + (box.Width / 2), box.Top),
            new Point(box.Right, box.Bottom),
            new Point(box.Left, box.Bottom)
        };
        using (var brush = new SolidBrush(color))
        {
            graphics.FillPolygon(brush, triangle);
        }
    }

    private static void DrawGrip(Graphics graphics, Rectangle box, Color color)
    {
        var dot = Math.Max(1, Math.Min(box.Width, box.Height) / 5);
        using (var brush = new SolidBrush(color))
        {
            for (var row = 0; row < 2; row++)
            {
                for (var column = 0; column < 3; column++)
                {
                    var x = box.Left + (column * (box.Width / 3));
                    var y = box.Top + (row * (box.Height / 2));
                    graphics.FillRectangle(brush, x, y, dot, dot);
                }
            }
        }
    }

    private static void DrawMark(Graphics graphics, Rectangle box, Color color)
    {
        var size = Math.Max(2, Math.Min(box.Width, box.Height) / 3);
        var mark = new Rectangle(
            box.Left + ((box.Width - size) / 2),
            box.Top + ((box.Height - size) / 2),
            size,
            size);
        using (var brush = new SolidBrush(color))
        {
            graphics.FillEllipse(brush, mark);
        }
    }

    private static byte[] CreateBmpBytes()
    {
        using (var bitmap = new Bitmap(1, 1, PixelFormat.Format24bppRgb))
        using (var stream = new MemoryStream())
        {
            bitmap.Save(stream, ImageFormat.Bmp);
            return stream.ToArray();
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool DestroyIcon(IntPtr handle);
}