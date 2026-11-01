#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Cached Luna.msstyles sheets and the 9-slice / chroma-key drawing used to paint them.
/// Margins are measured from the Blue sheets (Olive and Silver use the same layout).
/// </summary>
internal static class WindowsXPLunaArt
{
    private static readonly Color MagentaKey = Color.FromArgb(255, 0, 255);
    // Four pixels keeps the rounded corner, and the hot or default accent line, out of the stretched middle.
    private static readonly Padding ButtonMargins = new Padding(4);
    private const int ButtonMatteDistance = 32 * 32;
    private static readonly Color ButtonMatte = Color.FromArgb(255, 1, 2, 3);
    // The rounded caption corner is about six pixels. A smaller inset stretches that curve across the title bar.
    private static readonly Padding CaptionMargins = new Padding(6, 10, 6, 8);
    private static readonly Padding CaptionButtonMargins = new Padding(5);
    private static readonly Padding ComboMargins = new Padding(3);
    private static readonly Padding TrackMargins = new Padding(2);

    private static readonly Dictionary<Bitmap, WindowsXPLunaSprite> Sprites =
        new Dictionary<Bitmap, WindowsXPLunaSprite>();

    private static readonly Dictionary<long, Bitmap> Cells = new Dictionary<long, Bitmap>();

    private static readonly Dictionary<Bitmap, Bitmap> OpaqueChroma = new Dictionary<Bitmap, Bitmap>();

    private static readonly Dictionary<Bitmap, Bitmap> PunchedChroma = new Dictionary<Bitmap, Bitmap>();

    private static readonly WindowsXPSpriteTheme[] Themes =
    {
        WindowsXPSpriteTheme.LunaBlue,
        WindowsXPSpriteTheme.LunaOlive,
        WindowsXPSpriteTheme.LunaSilver,
        WindowsXPSpriteTheme.Royale,
        WindowsXPSpriteTheme.RoyaleNoir,
        WindowsXPSpriteTheme.Zune
    };

    private static readonly WindowsXPLunaPart[] Parts = (WindowsXPLunaPart[])Enum.GetValues(typeof(WindowsXPLunaPart));

    private static Bitmap?[,] _sheets;
    private static Bitmap[]? _defaultButtons;
    private static int _frameThickness = 5;

    /// <summary>Gets the Luna frame thickness in pixels (the side-frame bitmap width).</summary>
    internal static int FrameThickness
    {
        get
        {
            EnsureSheets();
            return _frameThickness;
        }
    }

    internal static bool TryGetScheme(Control? control, out WindowsXPLunaScheme scheme)
    {
        for (Control? current = control; current != null; current = current.Parent)
        {
            PaletteBase? palette = null;
            if (current is VisualControlBase visual)
            {
                palette = visual.GetResolvedPalette();
            }
            else if (current is VisualForm form)
            {
                palette = form.GetResolvedPalette();
            }

            if (TryReadScheme(palette, out scheme))
            {
                return true;
            }
        }

        if (TryReadScheme(KryptonManager.CurrentGlobalPalette, out scheme))
        {
            return true;
        }

        scheme = WindowsXPLunaScheme.Blue;
        return false;
    }

    internal static bool TryGetTheme(Control? control, out WindowsXPSpriteTheme theme)
    {
        for (Control? current = control; current != null; current = current.Parent)
        {
            PaletteBase? palette = null;
            if (current is VisualControlBase visual)
            {
                palette = visual.GetResolvedPalette();
            }
            else if (current is VisualForm form)
            {
                palette = form.GetResolvedPalette();
            }

            if (TryReadTheme(palette, out theme))
            {
                return true;
            }
        }

        if (TryReadTheme(KryptonManager.CurrentGlobalPalette, out theme))
        {
            return true;
        }

        theme = WindowsXPSpriteTheme.LunaBlue;
        return false;
    }

    private static bool TryReadTheme(PaletteBase? palette, out WindowsXPSpriteTheme theme)
    {
        PaletteBase? current = palette;
        for (int depth = 0; current != null && depth < 8; depth++)
        {
            if (current is IWindowsXPSpriteTheme sprite)
            {
                theme = sprite.Theme;
                return true;
            }

            if (current is IWindowsXPLunaScheme luna)
            {
                theme = (WindowsXPSpriteTheme)(int)luna.Scheme;
                return true;
            }

            current = current is KryptonCustomPaletteBase custom ? custom.BasePalette : null;
        }

        theme = default;
        return false;
    }

    internal static int FrameThicknessFor(WindowsXPSpriteTheme theme)
    {
        EnsureSheets();
        return Math.Max(1, Sheet(theme, WindowsXPLunaPart.FrameLeft).Width);
    }

    // A custom palette does not implement the scheme itself. Walk BasePalette so a
    // Luna renderer still finds Blue, Olive, or Silver when the wrapper inherits one.
    private static bool TryReadScheme(PaletteBase? palette, out WindowsXPLunaScheme scheme)
    {
        PaletteBase? current = palette;
        for (int depth = 0; current != null && depth < 8; depth++)
        {
            if (current is IWindowsXPLunaScheme source)
            {
                scheme = source.Scheme;
                return true;
            }

            current = current is KryptonCustomPaletteBase custom ? custom.BasePalette : null;
        }

        scheme = default;
        return false;
    }

    internal static Bitmap Sheet(WindowsXPLunaScheme scheme, WindowsXPLunaPart part) =>
        Sheet((WindowsXPSpriteTheme)(int)scheme, part);

    internal static Bitmap Sheet(WindowsXPSpriteTheme theme, WindowsXPLunaPart part)
    {
        EnsureSheets();
        return _sheets[(int)theme, (int)part]!;
    }

    /// <summary>The default-button row (orange or theme accent border), already punched to its rounded shape.</summary>
    internal static Bitmap DefaultButton(WindowsXPLunaScheme scheme) =>
        DefaultButton((WindowsXPSpriteTheme)(int)scheme);

    /// <summary>The default-button row (orange or theme accent border), already punched to its rounded shape.</summary>
    internal static Bitmap DefaultButton(WindowsXPSpriteTheme theme)
    {
        EnsureSheets();
        return _defaultButtons![(int)theme];
    }

    internal static bool TryGet(Image? image, out WindowsXPLunaSprite sprite)
    {
        if (image is Bitmap bitmap)
        {
            EnsureSheets();
            return Sprites.TryGetValue(bitmap, out sprite);
        }

        sprite = default;
        return false;
    }

    internal static void Draw(Graphics graphics, WindowsXPLunaSprite sprite, Rectangle destination, PaletteState state)
    {
        if (graphics == null || destination.Width <= 0 || destination.Height <= 0 || sprite.Bitmap == null)
        {
            return;
        }

        int row = StateRow(state, sprite.States);
        int rowHeight = Math.Max(1, sprite.Bitmap.Height / sprite.States);
        var source = new Rectangle(0, row * rowHeight, sprite.Bitmap.Width, rowHeight);
        // Caption corners are chroma. Fill them with the neighbouring caption colour so a
        // rectangular form does not show a magenta fringe or a transparent hole.
        DrawNineSlice(graphics, sprite.Bitmap, source, destination, sprite.Margins, sprite.Chroma, punchTransparent: false);
    }

    internal static void DrawFrame(Graphics graphics, Rectangle bounds, WindowsXPLunaSprite frameToken, PaletteState state)
    {
        if (graphics == null || bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        Bitmap left = Sheet(frameToken.Scheme, WindowsXPLunaPart.FrameLeft);
        Bitmap right = Sheet(frameToken.Scheme, WindowsXPLunaPart.FrameRight);
        Bitmap bottom = Sheet(frameToken.Scheme, WindowsXPLunaPart.FrameBottom);
        Bitmap caption = Sheet(frameToken.Scheme, WindowsXPLunaPart.Caption);
        int leftThickness = Math.Max(1, left.Width);
        int rightThickness = Math.Max(1, right.Width);
        int row = StateRow(state, 2);
        int sideHeight = Math.Max(1, left.Height / 2);
        int bottomHeight = Math.Max(1, bottom.Height / 2);
        // The caption sheet owns the title bar, including its rounded corners.
        // The side sprites are the client-edge frame and must not be painted through that bar.
        int captionHeight = Math.Max(1, caption.Height / 2);
        int sideTop = bounds.Top + Math.Min(captionHeight, Math.Max(0, bounds.Height - 1));
        int sideLength = Math.Max(0, bounds.Bottom - sideTop);

        if (sideLength > 0)
        {
            var sideSource = new Rectangle(0, row * sideHeight, left.Width, sideHeight);
            DrawNineSlice(graphics, left, sideSource,
                new Rectangle(bounds.Left, sideTop, leftThickness, sideLength), Padding.Empty, Color.Empty, punchTransparent: false);

            var rightSource = new Rectangle(0, row * Math.Max(1, right.Height / 2), right.Width, Math.Max(1, right.Height / 2));
            DrawNineSlice(graphics, right, rightSource,
                new Rectangle(bounds.Right - rightThickness, sideTop, rightThickness, sideLength), Padding.Empty, Color.Empty, punchTransparent: false);
        }

        var bottomSource = new Rectangle(0, row * bottomHeight, bottom.Width, bottomHeight);
        DrawNineSlice(graphics, bottom, bottomSource,
            new Rectangle(bounds.Left, bounds.Bottom - bottomHeight, bounds.Width, bottomHeight),
            new Padding(2, 0, 2, 0), Color.Empty, punchTransparent: false);
    }

    internal static void DrawComboGlyph(Graphics graphics, Rectangle bounds, WindowsXPLunaScheme scheme, PaletteState state) =>
        DrawComboGlyph(graphics, bounds, (WindowsXPSpriteTheme)(int)scheme, state);

    internal static void DrawComboGlyph(Graphics graphics, Rectangle bounds, WindowsXPSpriteTheme theme, PaletteState state)
    {
        Bitmap sheet = Sheet(theme, WindowsXPLunaPart.ComboGlyph);
        int states = 4;
        int row = StateRow(state, states);
        int rowHeight = Math.Max(1, sheet.Height / states);
        float dpi = graphics.DpiY / 96f;
        int width = Math.Max(1, (int)Math.Round(sheet.Width * dpi));
        int height = Math.Max(1, (int)Math.Round(rowHeight * dpi));
        var destination = new Rectangle(
            bounds.Left + Math.Max(0, (bounds.Width - width) / 2),
            bounds.Top + Math.Max(0, (bounds.Height - height) / 2),
            Math.Min(width, bounds.Width),
            Math.Min(height, bounds.Height));
        var source = new Rectangle(0, row * rowHeight, sheet.Width, rowHeight);
        DrawNineSlice(graphics, sheet, source, destination, Padding.Empty, MagentaKey, punchTransparent: true);
    }

    internal static void DrawProgressTrack(Graphics graphics, Rectangle bounds, WindowsXPLunaScheme scheme, bool vertical) =>
        DrawProgressTrack(graphics, bounds, (WindowsXPSpriteTheme)(int)scheme, vertical);

    internal static void DrawProgressTrack(Graphics graphics, Rectangle bounds, WindowsXPSpriteTheme theme, bool vertical)
    {
        WindowsXPLunaPart part = vertical ? WindowsXPLunaPart.ProgressTrackVertical : WindowsXPLunaPart.ProgressTrack;
        if (!TryGet(Sheet(theme, part), out WindowsXPLunaSprite sprite))
        {
            return;
        }

        Draw(graphics, sprite, bounds, PaletteState.Normal);
    }

    internal static void DrawProgressChunks(Graphics graphics, Rectangle valueBounds, WindowsXPLunaScheme scheme, bool vertical) =>
        DrawProgressChunks(graphics, valueBounds, (WindowsXPSpriteTheme)(int)scheme, vertical);

    internal static void DrawProgressChunks(Graphics graphics, Rectangle valueBounds, WindowsXPSpriteTheme theme, bool vertical)
    {
        if (valueBounds.Width <= 0 || valueBounds.Height <= 0)
        {
            return;
        }

        WindowsXPLunaPart part = vertical ? WindowsXPLunaPart.ProgressChunkVertical : WindowsXPLunaPart.ProgressChunk;
        Bitmap chunk = Sheet(theme, part);
        int tileAlong = vertical ? chunk.Height : chunk.Width;
        int gap = 1;
        var oldClip = graphics.Clip;
        graphics.SetClip(valueBounds, CombineMode.Intersect);
        try
        {
            if (vertical)
            {
                int y = valueBounds.Bottom;
                while (y > valueBounds.Top)
                {
                    int height = Math.Min(tileAlong, y - valueBounds.Top);
                    y -= height;
                    graphics.DrawImage(chunk,
                        new Rectangle(valueBounds.Left, y, valueBounds.Width, height),
                        new Rectangle(0, chunk.Height - height, chunk.Width, height),
                        GraphicsUnit.Pixel);
                    y -= gap;
                }
            }
            else
            {
                int x = valueBounds.Left;
                while (x < valueBounds.Right)
                {
                    int width = Math.Min(tileAlong, valueBounds.Right - x);
                    graphics.DrawImage(chunk,
                        new Rectangle(x, valueBounds.Top, width, valueBounds.Height),
                        new Rectangle(0, 0, width, chunk.Height),
                        GraphicsUnit.Pixel);
                    x += width + gap;
                }
            }
        }
        finally
        {
            graphics.Clip = oldClip;
        }
    }

    internal static Bitmap CheckBox(WindowsXPLunaScheme scheme, bool enabled, CheckState checkState, bool tracking, bool pressed) =>
        CheckBox((WindowsXPSpriteTheme)(int)scheme, enabled, checkState, tracking, pressed);

    internal static Bitmap CheckBox(WindowsXPSpriteTheme theme, bool enabled, CheckState checkState, bool tracking, bool pressed)
    {
        int group = checkState == CheckState.Checked ? 4 : checkState == CheckState.Indeterminate ? 8 : 0;
        int pose = !enabled ? 3 : pressed ? 2 : tracking ? 1 : 0;
        return Cell(theme, WindowsXPLunaPart.Checkbox, group + pose, 12, false);
    }

    internal static Bitmap Radio(WindowsXPLunaScheme scheme, bool enabled, bool checkState, bool tracking, bool pressed) =>
        Radio((WindowsXPSpriteTheme)(int)scheme, enabled, checkState, tracking, pressed);

    internal static Bitmap Radio(WindowsXPSpriteTheme theme, bool enabled, bool checkState, bool tracking, bool pressed)
    {
        int group = checkState ? 4 : 0;
        int pose = !enabled ? 3 : pressed ? 2 : tracking ? 1 : 0;
        return Cell(theme, WindowsXPLunaPart.Radio, group + pose, 8, true);
    }

    internal static Bitmap Glyph(WindowsXPLunaScheme scheme, WindowsXPLunaPart part, PaletteState state) =>
        Glyph((WindowsXPSpriteTheme)(int)scheme, part, state);

    internal static Bitmap Glyph(WindowsXPSpriteTheme theme, WindowsXPLunaPart part, PaletteState state)
    {
        int row = StateRow(state, 8);
        long key = CellKey(theme, part, row, glyph: true);
        if (Cells.TryGetValue(key, out Bitmap? cached))
        {
            return cached;
        }

        Bitmap sheet = Sheet(theme, part);
        int rowHeight = Math.Max(1, sheet.Height / 8);
        Bitmap cell = CreateGlyphCell(sheet, row, rowHeight);
        Cells[key] = cell;
        return cell;
    }

    private static Bitmap Cell(WindowsXPSpriteTheme theme, WindowsXPLunaPart part, int row, int states, bool clearWhite)
    {
        long key = CellKey(theme, part, row, glyph: false);
        if (Cells.TryGetValue(key, out Bitmap? cached))
        {
            return cached;
        }

        Bitmap sheet = Sheet(theme, part);
        int rowHeight = Math.Max(1, sheet.Height / states);
        var cell = new Bitmap(sheet.Width, rowHeight, PixelFormat.Format32bppArgb);
        for (int y = 0; y < rowHeight; y++)
        {
            for (int x = 0; x < sheet.Width; x++)
            {
                Color color = sheet.GetPixel(x, (row * rowHeight) + y);
                if (clearWhite && color.R == 255 && color.G == 255 && color.B == 255)
                {
                    color = Color.Transparent;
                }

                cell.SetPixel(x, y, color);
            }
        }

        Cells[key] = cell;
        return cell;
    }

    private static Bitmap CreateGlyphCell(Bitmap sheet, int row, int rowHeight)
    {
        bool anyMagenta = false;
        bool anyInk = false;
        for (int y = 0; y < rowHeight; y++)
        {
            for (int x = 0; x < sheet.Width; x++)
            {
                Color color = sheet.GetPixel(x, (row * rowHeight) + y);
                if (IsMagenta(color))
                {
                    anyMagenta = true;
                }
                else if (!IsWhite(color))
                {
                    anyInk = true;
                }
            }
        }

        int template = row;
        if (!anyMagenta && !anyInk)
        {
            int states = Math.Max(1, sheet.Height / rowHeight);
            for (int candidate = 0; candidate < states; candidate++)
            {
                if (RowHasInk(sheet, candidate, rowHeight))
                {
                    template = candidate;
                    break;
                }
            }
        }

        var cell = new Bitmap(sheet.Width, rowHeight, PixelFormat.Format32bppArgb);
        bool stampWhite = !anyMagenta && !anyInk;
        for (int y = 0; y < rowHeight; y++)
        {
            for (int x = 0; x < sheet.Width; x++)
            {
                Color color = sheet.GetPixel(x, (template * rowHeight) + y);
                if (stampWhite)
                {
                    cell.SetPixel(x, y, IsWhite(color) || IsMagenta(color) ? Color.Transparent : Color.White);
                }
                else if (IsMagenta(color) || (!anyMagenta && IsWhite(color)))
                {
                    cell.SetPixel(x, y, Color.Transparent);
                }
                else
                {
                    cell.SetPixel(x, y, color);
                }
            }
        }

        return cell;
    }

    private static bool RowHasInk(Bitmap sheet, int row, int rowHeight)
    {
        for (int y = 0; y < rowHeight; y++)
        {
            for (int x = 0; x < sheet.Width; x++)
            {
                Color color = sheet.GetPixel(x, (row * rowHeight) + y);
                if (!IsWhite(color) && !IsMagenta(color))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static int StateRow(PaletteState state, int states)
    {
        if (states <= 1)
        {
            return 0;
        }

        if (states == 2)
        {
            return state == PaletteState.Disabled ? 1 : 0;
        }

        int row;
        if (state == PaletteState.Disabled)
        {
            row = 3;
        }
        else if (state == PaletteState.Pressed || state == PaletteState.CheckedPressed)
        {
            row = 2;
        }
        else if (state == PaletteState.Tracking || state == PaletteState.CheckedTracking)
        {
            row = 1;
        }
        else if (states >= 5 && (state == PaletteState.CheckedNormal || state == PaletteState.NormalDefaultOverride))
        {
            // Row 4 is the default-button face (orange on Luna Blue, the theme accent on the others).
            row = 4;
        }
        else
        {
            row = 0;
        }

        return row >= states ? 0 : row;
    }

    private static void DrawNineSlice(Graphics graphics, Bitmap bitmap, Rectangle source, Rectangle destination, Padding margins, Color chroma, bool punchTransparent)
    {
        if (destination.Width <= 0 || destination.Height <= 0 || source.Width <= 0 || source.Height <= 0)
        {
            return;
        }

        if (!chroma.IsEmpty)
        {
            bitmap = PrepareChroma(bitmap, punchTransparent);
        }

        int left = Math.Max(0, margins.Left);
        int top = Math.Max(0, margins.Top);
        int right = Math.Max(0, margins.Right);
        int bottom = Math.Max(0, margins.Bottom);
        if (left + right >= source.Width)
        {
            left = Math.Max(0, (source.Width - 1) / 2);
            right = Math.Max(0, source.Width - 1 - left);
        }

        if (top + bottom >= source.Height)
        {
            top = Math.Max(0, (source.Height - 1) / 2);
            bottom = Math.Max(0, source.Height - 1 - top);
        }

        int destLeft = left;
        int destTop = top;
        int destRight = right;
        int destBottom = bottom;
        if (destLeft + destRight > destination.Width)
        {
            destLeft = destination.Width / 2;
            destRight = destination.Width - destLeft;
        }

        if (destTop + destBottom > destination.Height)
        {
            destTop = destination.Height / 2;
            destBottom = destination.Height - destTop;
        }

        int sourceMidW = source.Width - left - right;
        int sourceMidH = source.Height - top - bottom;
        int destMidW = destination.Width - destLeft - destRight;
        int destMidH = destination.Height - destTop - destBottom;
        var oldInterpolation = graphics.InterpolationMode;
        var oldOffset = graphics.PixelOffsetMode;
        // Nearest neighbour keeps a chroma key from bleeding into the neighbouring pixels.
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.PixelOffsetMode = PixelOffsetMode.Half;
        using (var attributes = new ImageAttributes())
        {
            DrawPart(graphics, bitmap, attributes, source.X, source.Y, left, top, destination.X, destination.Y, destLeft, destTop);
            DrawPart(graphics, bitmap, attributes, source.Right - right, source.Y, right, top, destination.Right - destRight, destination.Y, destRight, destTop);
            DrawPart(graphics, bitmap, attributes, source.X, source.Bottom - bottom, left, bottom, destination.X, destination.Bottom - destBottom, destLeft, destBottom);
            DrawPart(graphics, bitmap, attributes, source.Right - right, source.Bottom - bottom, right, bottom, destination.Right - destRight, destination.Bottom - destBottom, destRight, destBottom);
            DrawPart(graphics, bitmap, attributes, source.X + left, source.Y, sourceMidW, top, destination.X + destLeft, destination.Y, destMidW, destTop);
            DrawPart(graphics, bitmap, attributes, source.X + left, source.Bottom - bottom, sourceMidW, bottom, destination.X + destLeft, destination.Bottom - destBottom, destMidW, destBottom);
            DrawPart(graphics, bitmap, attributes, source.X, source.Y + top, left, sourceMidH, destination.X, destination.Y + destTop, destLeft, destMidH);
            DrawPart(graphics, bitmap, attributes, source.Right - right, source.Y + top, right, sourceMidH, destination.Right - destRight, destination.Y + destTop, destRight, destMidH);
            DrawPart(graphics, bitmap, attributes, source.X + left, source.Y + top, sourceMidW, sourceMidH, destination.X + destLeft, destination.Y + destTop, destMidW, destMidH);
        }

        graphics.InterpolationMode = oldInterpolation;
        graphics.PixelOffsetMode = oldOffset;
    }

    private static Bitmap CloneSheet(Bitmap source) => new Bitmap(source);

    private static Bitmap PrepareChroma(Bitmap source, bool punchTransparent)
    {
        Dictionary<Bitmap, Bitmap> cache = punchTransparent ? PunchedChroma : OpaqueChroma;
        if (cache.TryGetValue(source, out Bitmap? cached) && cached != null)
        {
            return cached;
        }

        int width = source.Width;
        int height = source.Height;
        var colors = new Color[width, height];
        var opaque = new bool[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color color = source.GetPixel(x, y);
                if (IsMagenta(color))
                {
                    colors[x, y] = Color.Transparent;
                }
                else
                {
                    colors[x, y] = color;
                    opaque[x, y] = true;
                }
            }
        }

        var prepared = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (opaque[x, y])
                {
                    prepared.SetPixel(x, y, colors[x, y]);
                    continue;
                }

                Color neighbor = NearestOpaque(colors, opaque, x, y);
                prepared.SetPixel(x, y, punchTransparent ? Color.FromArgb(0, neighbor) : neighbor);
            }
        }

        cache[source] = prepared;
        return prepared;
    }

    private static Color NearestOpaque(Color[,] colors, bool[,] opaque, int x, int y)
    {
        int width = colors.GetLength(0);
        int height = colors.GetLength(1);
        for (int radius = 1; radius <= 8; radius++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                int sy = y + dy;
                if (sy < 0 || sy >= height)
                {
                    continue;
                }

                for (int dx = -radius; dx <= radius; dx++)
                {
                    if (Math.Max(Math.Abs(dx), Math.Abs(dy)) != radius)
                    {
                        continue;
                    }

                    int sx = x + dx;
                    if (sx < 0 || sx >= width || !opaque[sx, sy])
                    {
                        continue;
                    }

                    return colors[sx, sy];
                }
            }
        }

        return Color.Black;
    }

    private static void DrawPart(Graphics graphics, Bitmap bitmap, ImageAttributes attributes,
        int sourceX, int sourceY, int sourceWidth, int sourceHeight,
        int destX, int destY, int destWidth, int destHeight)
    {
        if (sourceWidth <= 0 || sourceHeight <= 0 || destWidth <= 0 || destHeight <= 0)
        {
            return;
        }

        // The control buffer keeps the RGB of a transparent pixel, so the matte is a sentinel
        // that is never drawn. The parent colour stays in the rounded corner.
        if (RectHasMatte(bitmap, sourceX, sourceY, sourceWidth, sourceHeight))
        {
            DrawOpaqueRuns(graphics, bitmap, sourceX, sourceY, sourceWidth, sourceHeight, destX, destY, destWidth, destHeight);
            return;
        }

        graphics.DrawImage(bitmap,
            new Rectangle(destX, destY, destWidth, destHeight),
            sourceX, sourceY, sourceWidth, sourceHeight,
            GraphicsUnit.Pixel, attributes);
    }

    private static bool IsButtonMatte(Color color) => color.R == 1 && color.G == 2 && color.B == 3;

    private static bool RectHasMatte(Bitmap bitmap, int x, int y, int width, int height)
    {
        for (int sy = 0; sy < height; sy++)
        {
            for (int sx = 0; sx < width; sx++)
            {
                if (IsButtonMatte(bitmap.GetPixel(x + sx, y + sy)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static void DrawOpaqueRuns(Graphics graphics, Bitmap bitmap,
        int sourceX, int sourceY, int sourceWidth, int sourceHeight,
        int destX, int destY, int destWidth, int destHeight)
    {
        using (var brush = new SolidBrush(Color.Black))
        {
            for (int sy = 0; sy < sourceHeight; sy++)
            {
                int rowTop = MapTexel(sy, sourceHeight, destHeight, destY);
                int rowBottom = MapTexel(sy + 1, sourceHeight, destHeight, destY);
                if (rowBottom <= rowTop)
                {
                    continue;
                }

                int runStart = -1;
                Color runColor = Color.Empty;
                for (int sx = 0; sx <= sourceWidth; sx++)
                {
                    Color color = sx < sourceWidth
                        ? bitmap.GetPixel(sourceX + sx, sourceY + sy)
                        : Color.Transparent;
                    bool opaque = !IsButtonMatte(color);
                    bool sameRun = opaque && runStart >= 0 && color.ToArgb() == runColor.ToArgb();
                    if (sameRun)
                    {
                        continue;
                    }

                    if (runStart >= 0)
                    {
                        int runLeft = MapTexel(runStart, sourceWidth, destWidth, destX);
                        int runRight = MapTexel(sx, sourceWidth, destWidth, destX);
                        if (runRight > runLeft)
                        {
                            brush.Color = Color.FromArgb(255, runColor);
                            graphics.FillRectangle(brush, runLeft, rowTop, runRight - runLeft, rowBottom - rowTop);
                        }
                    }

                    runStart = opaque ? sx : -1;
                    runColor = color;
                }
            }
        }
    }

    private static int MapTexel(int index, int source, int dest, int origin) =>
        index >= source ? origin + dest : origin + (int)((index * (long)dest) / source);

    // The sheet's outer pixels are the cell background, not the button. They form a square
    // around a corner that is only a pixel or two. Clearing the edge-connected matte leaves the rounded face.
    private static Bitmap PunchButtonMatte(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        int states = height % 5 == 0 ? 5 : 1;
        int rowHeight = height / states;
        var prepared = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        for (int state = 0; state < states; state++)
        {
            int top = state * rowHeight;
            var corners = new[]
            {
                source.GetPixel(0, top),
                source.GetPixel(width - 1, top),
                source.GetPixel(0, top + rowHeight - 1),
                source.GetPixel(width - 1, top + rowHeight - 1)
            };
            var matte = new bool[width, rowHeight];
            var queue = new Queue<Point>();
            for (int x = 0; x < width; x++)
            {
                queue.Enqueue(new Point(x, 0));
                queue.Enqueue(new Point(x, rowHeight - 1));
            }

            for (int y = 0; y < rowHeight; y++)
            {
                queue.Enqueue(new Point(0, y));
                queue.Enqueue(new Point(width - 1, y));
            }

            while (queue.Count > 0)
            {
                Point point = queue.Dequeue();
                if (point.X < 0 || point.Y < 0 || point.X >= width || point.Y >= rowHeight || matte[point.X, point.Y])
                {
                    continue;
                }

                Color color = source.GetPixel(point.X, top + point.Y);
                if (!NearButtonCorner(color, corners))
                {
                    continue;
                }

                matte[point.X, point.Y] = true;
                queue.Enqueue(new Point(point.X - 1, point.Y));
                queue.Enqueue(new Point(point.X + 1, point.Y));
                queue.Enqueue(new Point(point.X, point.Y - 1));
                queue.Enqueue(new Point(point.X, point.Y + 1));
            }

            for (int y = 0; y < rowHeight; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = source.GetPixel(x, top + y);
                    prepared.SetPixel(x, top + y, matte[x, y] ? ButtonMatte : Color.FromArgb(255, color));
                }
            }
        }

        return prepared;
    }

    private static bool NearButtonCorner(Color color, Color[] corners)
    {
        for (int i = 0; i < corners.Length; i++)
        {
            int dr = color.R - corners[i].R;
            int dg = color.G - corners[i].G;
            int db = color.B - corners[i].B;
            if ((dr * dr) + (dg * dg) + (db * db) <= ButtonMatteDistance)
            {
                return true;
            }
        }

        return false;
    }

    private static Bitmap RegisterDefaultButton(WindowsXPSpriteTheme theme, WindowsXPLunaSprite sprite)
    {
        Bitmap punched = sprite.Bitmap;
        int states = Math.Max(1, sprite.States);
        int row = states >= 5 ? 4 : 0;
        int rowHeight = Math.Max(1, punched.Height / states);
        var cell = new Bitmap(punched.Width, rowHeight, PixelFormat.Format32bppArgb);
        for (int y = 0; y < rowHeight; y++)
        {
            for (int x = 0; x < punched.Width; x++)
            {
                cell.SetPixel(x, y, punched.GetPixel(x, (row * rowHeight) + y));
            }
        }

        Sprites[cell] = new WindowsXPLunaSprite(cell, theme, WindowsXPLunaPart.Button, 1, ButtonMargins, Color.Empty);
        return cell;
    }

    private static void EnsureSheets()
    {
        if (_sheets != null)
        {
            return;
        }

        _sheets = new Bitmap[Themes.Length, Parts.Length];
        _defaultButtons = new Bitmap[Themes.Length];
        for (int themeIndex = 0; themeIndex < Themes.Length; themeIndex++)
        {
            WindowsXPSpriteTheme theme = Themes[themeIndex];
            for (int partIndex = 0; partIndex < Parts.Length; partIndex++)
            {
                WindowsXPLunaPart part = Parts[partIndex];
                Bitmap bitmap = Load(theme, part);
                _sheets[themeIndex, partIndex] = bitmap;
                WindowsXPLunaSprite sprite = Define(part, theme, bitmap);
                Sprites[bitmap] = sprite;
                if (part == WindowsXPLunaPart.Button)
                {
                    _defaultButtons[themeIndex] = RegisterDefaultButton(theme, sprite);
                }
            }
        }

        Bitmap frame = _sheets[(int)WindowsXPSpriteTheme.LunaBlue, (int)WindowsXPLunaPart.FrameLeft]!;
        _frameThickness = Math.Max(1, frame.Width);
    }

    // Royale, Royale Noir, and Zune sheets divide into the same row counts as Luna
    // (button 20x115, caption 66x58, caption button 21x168, combo 15x64, frame sides ~x62).
    private static WindowsXPLunaSprite Define(WindowsXPLunaPart part, WindowsXPSpriteTheme theme, Bitmap bitmap)
    {
        switch (part)
        {
            case WindowsXPLunaPart.Button:
                // The cell padding is an opaque square. Punching it lets the rounded face show the parent.
                return new WindowsXPLunaSprite(PunchButtonMatte(bitmap), theme, part, Rows(bitmap, 5), ButtonMargins, Color.Empty);
            case WindowsXPLunaPart.ComboButton:
                return new WindowsXPLunaSprite(bitmap, theme, part, Rows(bitmap, 4), ComboMargins, Color.Empty);
            case WindowsXPLunaPart.Caption:
                return new WindowsXPLunaSprite(bitmap, theme, part, Rows(bitmap, 2), CaptionMargins, MagentaKey);
            case WindowsXPLunaPart.FrameLeft:
            case WindowsXPLunaPart.FrameRight:
                return new WindowsXPLunaSprite(bitmap, theme, part, Rows(bitmap, 2), Padding.Empty, Color.Empty);
            case WindowsXPLunaPart.FrameBottom:
                return new WindowsXPLunaSprite(bitmap, theme, part, Rows(bitmap, 2), new Padding(2, 0, 2, 0), Color.Empty);
            case WindowsXPLunaPart.CaptionButton:
            case WindowsXPLunaPart.CloseButton:
                return new WindowsXPLunaSprite(bitmap, theme, part, Rows(bitmap, 8), CaptionButtonMargins, Color.Empty);
            case WindowsXPLunaPart.ProgressTrack:
            case WindowsXPLunaPart.ProgressTrackVertical:
                return new WindowsXPLunaSprite(bitmap, theme, part, 1, TrackMargins, Color.Empty);
            default:
                return new WindowsXPLunaSprite(bitmap, theme, part, 1, Padding.Empty, Color.Empty);
        }
    }

    private static int Rows(Bitmap bitmap, int expected) =>
        bitmap.Height % expected == 0 ? expected : 1;

    private static Bitmap Load(WindowsXPSpriteTheme theme, WindowsXPLunaPart part)
    {
        switch (theme)
        {
            case WindowsXPSpriteTheme.LunaOlive: return LoadOlive(part);
            case WindowsXPSpriteTheme.LunaSilver: return LoadSilver(part);
            case WindowsXPSpriteTheme.Royale: return LoadRoyale(part);
            case WindowsXPSpriteTheme.RoyaleNoir: return LoadRoyaleNoir(part);
            case WindowsXPSpriteTheme.Zune: return LoadZune(part);
            default: return LoadBlue(part);
        }
    }

    private static Bitmap LoadBlue(WindowsXPLunaPart part)
    {
        switch (part)
        {
            case WindowsXPLunaPart.Button: return WindowsXPLunaImageResources.Button!;
            case WindowsXPLunaPart.ComboButton: return WindowsXPLunaImageResources.COMBOBUTTONBmp!;
            case WindowsXPLunaPart.ComboGlyph: return WindowsXPLunaImageResources.COMBOBUTTONGLYPHBmp!;
            case WindowsXPLunaPart.Caption: return WindowsXPLunaImageResources.FRAMECAPTIONBmp!;
            case WindowsXPLunaPart.FrameLeft: return WindowsXPLunaImageResources.FRAMELEFTBmp!;
            case WindowsXPLunaPart.FrameRight: return WindowsXPLunaImageResources.FRAMERIGHTBmp!;
            // FrameBottom.bmp is the classic gray window edge. Luna Blue's bottom is the same blue strip as the side frame.
            case WindowsXPLunaPart.FrameBottom: return WindowsXPRoyaleNoirImageResources.ROYALEFRAMEBOTTOMBmp!;
            case WindowsXPLunaPart.CaptionButton: return WindowsXPLunaImageResources.CAPTIONBUTTONBmp!;
            case WindowsXPLunaPart.CloseButton: return WindowsXPLunaImageResources.CLOSEBUTTONBmp!;
            // The Blue horizontal chunk file is orange. Luna Blue progress is the green block.
            case WindowsXPLunaPart.ProgressChunk: return CloneSheet(WindowsXPLunaImageResources.METALLICPROGRESSCHUNKBmp!);
            case WindowsXPLunaPart.ProgressChunkVertical: return WindowsXPLunaImageResources.PROGRESSCHUNKVERTBmp!;
            case WindowsXPLunaPart.ProgressTrack: return WindowsXPLunaImageResources.ProgressTrack!;
            case WindowsXPLunaPart.ProgressTrackVertical: return WindowsXPLunaImageResources.PROGRESSTRACKVERTBmp!;
            case WindowsXPLunaPart.Checkbox: return WindowsXPLunaImageResources.Checkbox13Bmp!;
            case WindowsXPLunaPart.Radio: return WindowsXPLunaImageResources.Radiobutton13Bmp!;
            case WindowsXPLunaPart.MinimizeGlyph: return WindowsXPLunaImageResources.MINIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.MaximizeGlyph: return WindowsXPLunaImageResources.MAXIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.RestoreGlyph: return WindowsXPLunaImageResources.RESTOREGLYPHBmp!;
            case WindowsXPLunaPart.CloseGlyph: return WindowsXPLunaImageResources.CLOSEGLYPHBmp!;
            case WindowsXPLunaPart.HelpGlyph: return WindowsXPLunaImageResources.HELPGLYPHBmp!;
            default: return WindowsXPLunaImageResources.Button!;
        }
    }

    private static Bitmap LoadOlive(WindowsXPLunaPart part)
    {
        switch (part)
        {
            case WindowsXPLunaPart.Button: return WindowsXPLunaImageResources.HOMESTEADBUTTONBmp!;
            case WindowsXPLunaPart.ComboButton: return WindowsXPLunaImageResources.HOMESTEADCOMBOBUTTONBmp!;
            case WindowsXPLunaPart.ComboGlyph: return WindowsXPLunaImageResources.HOMESTEADCOMBOBUTTONGLYPHBmp!;
            case WindowsXPLunaPart.Caption: return WindowsXPLunaImageResources.HOMESTEADFRAMECAPTIONBmp!;
            case WindowsXPLunaPart.FrameLeft: return WindowsXPLunaImageResources.HOMESTEADFRAMELEFTBmp!;
            case WindowsXPLunaPart.FrameRight: return WindowsXPLunaImageResources.HOMESTEADFRAMERIGHTBmp!;
            case WindowsXPLunaPart.FrameBottom: return WindowsXPLunaImageResources.HOMESTEADFRAMEBOTTOMBmp!;
            case WindowsXPLunaPart.CaptionButton: return WindowsXPLunaImageResources.HOMESTEADCAPTIONBUTTONBmp!;
            case WindowsXPLunaPart.CloseButton: return WindowsXPLunaImageResources.HOMESTEADCLOSEBUTTONBmp!;
            case WindowsXPLunaPart.ProgressChunk: return WindowsXPLunaImageResources.HOMESTEADPROGRESSCHUNKBmp!;
            case WindowsXPLunaPart.ProgressChunkVertical: return WindowsXPLunaImageResources.HOMESTEADPROGRESSCHUNKVERTBmp!;
            case WindowsXPLunaPart.ProgressTrack: return WindowsXPLunaImageResources.HOMESTEADPROGRESSTRACKBmp!;
            case WindowsXPLunaPart.ProgressTrackVertical: return WindowsXPLunaImageResources.HOMESTEADPROGRESSTRACKVERTBmp!;
            case WindowsXPLunaPart.Checkbox: return WindowsXPLunaImageResources.HOMESTEADCheckbox13Bmp!;
            case WindowsXPLunaPart.Radio: return WindowsXPLunaImageResources.HOMESTEADRadiobutton13Bmp!;
            case WindowsXPLunaPart.MinimizeGlyph: return WindowsXPLunaImageResources.HOMESTEADMINIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.MaximizeGlyph: return WindowsXPLunaImageResources.HOMESTEADMAXIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.RestoreGlyph: return WindowsXPLunaImageResources.HOMESTEADRESTOREGLYPHBmp!;
            case WindowsXPLunaPart.CloseGlyph: return WindowsXPLunaImageResources.HOMESTEADCLOSEGLYPHBmp!;
            case WindowsXPLunaPart.HelpGlyph: return WindowsXPLunaImageResources.HOMESTEADHELPGLYPHBmp!;
            default: return WindowsXPLunaImageResources.HOMESTEADBUTTONBmp!;
        }
    }

    private static Bitmap LoadSilver(WindowsXPLunaPart part)
    {
        switch (part)
        {
            case WindowsXPLunaPart.Button: return WindowsXPLunaImageResources.METALLICBUTTONBmp!;
            case WindowsXPLunaPart.ComboButton: return WindowsXPLunaImageResources.METALLICCOMBOBUTTONBmp!;
            case WindowsXPLunaPart.ComboGlyph: return WindowsXPLunaImageResources.METALLICCOMBOBUTTONGLYPHBmp!;
            case WindowsXPLunaPart.Caption: return WindowsXPLunaImageResources.METALLICFRAMECAPTIONBmp!;
            case WindowsXPLunaPart.FrameLeft: return WindowsXPLunaImageResources.METALLICFRAMELEFTBmp!;
            case WindowsXPLunaPart.FrameRight: return WindowsXPLunaImageResources.METALLICFRAMERIGHTBmp!;
            case WindowsXPLunaPart.FrameBottom: return WindowsXPLunaImageResources.METALLICFRAMEBOTTOMBmp!;
            case WindowsXPLunaPart.CaptionButton: return WindowsXPLunaImageResources.METALLICCAPTIONBUTTONBmp!;
            case WindowsXPLunaPart.CloseButton: return WindowsXPLunaImageResources.METALLICCLOSEBUTTONBmp!;
            case WindowsXPLunaPart.ProgressChunk: return WindowsXPLunaImageResources.METALLICPROGRESSCHUNKBmp!;
            case WindowsXPLunaPart.ProgressChunkVertical: return WindowsXPLunaImageResources.METALLICPROGRESSCHUNKVERTBmp!;
            case WindowsXPLunaPart.ProgressTrack: return WindowsXPLunaImageResources.METALLICPROGRESSTRACKBmp!;
            case WindowsXPLunaPart.ProgressTrackVertical: return WindowsXPLunaImageResources.METALLICPROGRESSTRACKVERTBmp!;
            case WindowsXPLunaPart.Checkbox: return WindowsXPLunaImageResources.METALLICCheckbox13Bmp!;
            case WindowsXPLunaPart.Radio: return WindowsXPLunaImageResources.METALLICRadiobutton13Bmp!;
            case WindowsXPLunaPart.MinimizeGlyph: return WindowsXPLunaImageResources.METALLICMINIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.MaximizeGlyph: return WindowsXPLunaImageResources.METALLICMAXIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.RestoreGlyph: return WindowsXPLunaImageResources.METALLICRESTOREGLYPHBmp!;
            case WindowsXPLunaPart.CloseGlyph: return WindowsXPLunaImageResources.METALLICCLOSEGLYPHBmp!;
            case WindowsXPLunaPart.HelpGlyph: return WindowsXPLunaImageResources.METALLICHELPGLYPHBmp!;
            default: return WindowsXPLunaImageResources.METALLICBUTTONBmp!;
        }
    }

    private static Bitmap LoadRoyale(WindowsXPLunaPart part)
    {
        switch (part)
        {
            case WindowsXPLunaPart.Button: return WindowsXPRoyaleNoirImageResources.ROYALEBUTTONBmp!;
            case WindowsXPLunaPart.ComboButton: return WindowsXPRoyaleNoirImageResources.ROYALECOMBOBUTTONBmp!;
            case WindowsXPLunaPart.ComboGlyph: return WindowsXPRoyaleNoirImageResources.ROYALECOMBOBUTTONGLYPHBmp!;
            case WindowsXPLunaPart.Caption: return WindowsXPRoyaleNoirImageResources.ROYALEFRAMECAPTIONBmp!;
            case WindowsXPLunaPart.FrameLeft: return WindowsXPRoyaleNoirImageResources.ROYALEFRAMELEFTBmp!;
            case WindowsXPLunaPart.FrameRight: return WindowsXPRoyaleNoirImageResources.ROYALEFRAMERIGHTBmp!;
            case WindowsXPLunaPart.FrameBottom: return WindowsXPRoyaleNoirImageResources.ROYALEFRAMEBOTTOMBmp!;
            case WindowsXPLunaPart.CaptionButton: return WindowsXPRoyaleNoirImageResources.ROYALECAPTIONBUTTONBmp!;
            case WindowsXPLunaPart.CloseButton: return WindowsXPRoyaleNoirImageResources.ROYALECLOSEBUTTONBmp!;
            case WindowsXPLunaPart.ProgressChunk: return WindowsXPRoyaleNoirImageResources.ROYALEPROGRESSCHUNKBmp!;
            case WindowsXPLunaPart.ProgressChunkVertical: return WindowsXPRoyaleNoirImageResources.ROYALEPROGRESSCHUNKVERTBmp!;
            case WindowsXPLunaPart.ProgressTrack: return WindowsXPRoyaleNoirImageResources.ROYALEPROGRESSTRACKBmp!;
            case WindowsXPLunaPart.ProgressTrackVertical: return WindowsXPRoyaleNoirImageResources.ROYALEPROGRESSTRACKVERTBmp!;
            case WindowsXPLunaPart.Checkbox: return WindowsXPRoyaleNoirImageResources.ROYALECheckbox13Bmp!;
            case WindowsXPLunaPart.Radio: return WindowsXPRoyaleNoirImageResources.ROYALERadiobutton13Bmp!;
            case WindowsXPLunaPart.MinimizeGlyph: return WindowsXPRoyaleNoirImageResources.ROYALEMINIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.MaximizeGlyph: return WindowsXPRoyaleNoirImageResources.ROYALEMAXIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.RestoreGlyph: return WindowsXPRoyaleNoirImageResources.ROYALERESTOREGLYPHBmp!;
            case WindowsXPLunaPart.CloseGlyph: return WindowsXPRoyaleNoirImageResources.ROYALECLOSEGLYPHBmp!;
            case WindowsXPLunaPart.HelpGlyph: return WindowsXPRoyaleNoirImageResources.ROYALEHELPGLYPHBmp!;
            default: return WindowsXPRoyaleNoirImageResources.ROYALEBUTTONBmp!;
        }
    }

    private static Bitmap LoadRoyaleNoir(WindowsXPLunaPart part)
    {
        switch (part)
        {
            case WindowsXPLunaPart.Button: return WindowsXPRoyaleNoirImageResources.DARKROYALEBUTTONBmp!;
            case WindowsXPLunaPart.ComboButton: return WindowsXPRoyaleNoirImageResources.DARKROYALECOMBOBUTTONBmp!;
            case WindowsXPLunaPart.ComboGlyph: return WindowsXPRoyaleNoirImageResources.DARKROYALECOMBOBUTTONGLYPHBmp!;
            case WindowsXPLunaPart.Caption: return WindowsXPRoyaleNoirImageResources.DARKROYALEFRAMECAPTIONBmp!;
            case WindowsXPLunaPart.FrameLeft: return WindowsXPRoyaleNoirImageResources.DARKROYALEFRAMELEFTBmp!;
            case WindowsXPLunaPart.FrameRight: return WindowsXPRoyaleNoirImageResources.DARKROYALEFRAMERIGHTBmp!;
            case WindowsXPLunaPart.FrameBottom: return WindowsXPRoyaleNoirImageResources.DARKROYALEFRAMEBOTTOMBmp!;
            case WindowsXPLunaPart.CaptionButton: return WindowsXPRoyaleNoirImageResources.DARKROYALECAPTIONBUTTONBmp!;
            case WindowsXPLunaPart.CloseButton: return WindowsXPRoyaleNoirImageResources.DARKROYALECLOSEBUTTONBmp!;
            case WindowsXPLunaPart.ProgressChunk: return WindowsXPRoyaleNoirImageResources.DARKROYALEPROGRESSCHUNKBmp!;
            case WindowsXPLunaPart.ProgressChunkVertical: return WindowsXPRoyaleNoirImageResources.DARKROYALEPROGRESSCHUNKVERTBmp!;
            case WindowsXPLunaPart.ProgressTrack: return WindowsXPRoyaleNoirImageResources.DARKROYALEPROGRESSTRACKBmp!;
            case WindowsXPLunaPart.ProgressTrackVertical: return WindowsXPRoyaleNoirImageResources.DARKROYALEPROGRESSTRACKVERTBmp!;
            case WindowsXPLunaPart.Checkbox: return WindowsXPRoyaleNoirImageResources.DARKROYALECheckbox13Bmp!;
            case WindowsXPLunaPart.Radio: return WindowsXPRoyaleNoirImageResources.DARKROYALERadiobutton13Bmp!;
            case WindowsXPLunaPart.MinimizeGlyph: return WindowsXPRoyaleNoirImageResources.DARKROYALEMINIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.MaximizeGlyph: return WindowsXPRoyaleNoirImageResources.DARKROYALEMAXIMIZEGLYPHBmp!;
            case WindowsXPLunaPart.RestoreGlyph: return WindowsXPRoyaleNoirImageResources.DARKROYALERESTOREGLYPHBmp!;
            case WindowsXPLunaPart.CloseGlyph: return WindowsXPRoyaleNoirImageResources.DARKROYALECLOSEGLYPHBmp!;
            case WindowsXPLunaPart.HelpGlyph: return WindowsXPRoyaleNoirImageResources.DARKROYALEHELPGLYPHBmp!;
            default: return WindowsXPRoyaleNoirImageResources.DARKROYALEBUTTONBmp!;
        }
    }

    private static Bitmap LoadZune(WindowsXPLunaPart part)
    {
        switch (part)
        {
            case WindowsXPLunaPart.Button: return WindowsXPZuneImageResources.Button!;
            case WindowsXPLunaPart.ComboButton: return WindowsXPZuneImageResources.Combobutton!;
            case WindowsXPLunaPart.ComboGlyph: return WindowsXPZuneImageResources.Combobuttonglyph!;
            case WindowsXPLunaPart.Caption: return WindowsXPZuneImageResources.Framecaption!;
            case WindowsXPLunaPart.FrameLeft: return WindowsXPZuneImageResources.Frameleft!;
            case WindowsXPLunaPart.FrameRight: return WindowsXPZuneImageResources.Frameright!;
            case WindowsXPLunaPart.FrameBottom: return WindowsXPZuneImageResources.Framebottom!;
            case WindowsXPLunaPart.CaptionButton: return WindowsXPZuneImageResources.Captionbutton!;
            case WindowsXPLunaPart.CloseButton: return WindowsXPZuneImageResources.Closebutton!;
            case WindowsXPLunaPart.ProgressChunk: return WindowsXPZuneImageResources.Progresschunk!;
            case WindowsXPLunaPart.ProgressChunkVertical: return WindowsXPZuneImageResources.Progresschunkvert!;
            case WindowsXPLunaPart.ProgressTrack: return WindowsXPZuneImageResources.Progresstrack!;
            case WindowsXPLunaPart.ProgressTrackVertical: return WindowsXPZuneImageResources.Progresstrackvert!;
            case WindowsXPLunaPart.Checkbox: return WindowsXPZuneImageResources.CHECKBOX13!;
            case WindowsXPLunaPart.Radio: return WindowsXPZuneImageResources.RADIOBUTTON13!;
            case WindowsXPLunaPart.MinimizeGlyph: return WindowsXPZuneImageResources.Minimizeglyph!;
            case WindowsXPLunaPart.MaximizeGlyph: return WindowsXPZuneImageResources.Maximizeglyph!;
            case WindowsXPLunaPart.RestoreGlyph: return WindowsXPZuneImageResources.Restoreglyph!;
            case WindowsXPLunaPart.CloseGlyph: return WindowsXPZuneImageResources.Closeglyph!;
            case WindowsXPLunaPart.HelpGlyph: return WindowsXPZuneImageResources.Helpglyph!;
            default: return WindowsXPZuneImageResources.Button!;
        }
    }

    private static long CellKey(WindowsXPSpriteTheme theme, WindowsXPLunaPart part, int row, bool glyph) =>
        ((long)theme << 24) | ((long)part << 12) | (uint)row | (glyph ? 1L << 40 : 0L);

    private static bool IsMagenta(Color color) => color.R == 255 && color.G == 0 && color.B == 255;

    private static bool IsWhite(Color color) => color.R == 255 && color.G == 255 && color.B == 255;
}

/// <summary>Layout of one cached Luna sprite sheet.</summary>
internal readonly struct WindowsXPLunaSprite
{
    internal WindowsXPLunaSprite(Bitmap bitmap, WindowsXPSpriteTheme theme, WindowsXPLunaPart part, int states, Padding margins, Color chroma)
    {
        Bitmap = bitmap;
        Scheme = theme;
        Part = part;
        States = Math.Max(1, states);
        Margins = margins;
        Chroma = chroma;
    }

    internal Bitmap Bitmap { get; }

    internal WindowsXPSpriteTheme Scheme { get; }

    internal WindowsXPLunaPart Part { get; }

    internal int States { get; }

    internal Padding Margins { get; }

    internal Color Chroma { get; }
}
