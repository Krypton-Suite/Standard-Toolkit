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
/// Builds a small themed window mock-up for custom-theme previews (issue #3870).
/// Palette Designer and theme-browser export store the result on
/// <see cref="KryptonCustomPaletteBase.Thumbnail"/> as base64 PNG in <c>.kthemex</c>.
/// </summary>
public static class KryptonThemePreview
{
    /// <summary>
    /// Paints a square themed-window preview from <paramref name="palette"/> colours.
    /// </summary>
    /// <param name="palette">Palette to sample.</param>
    /// <param name="size">Output size. Empty or non-positive values use
    /// <see cref="KryptonPaletteFile.RecommendedThumbnailSize"/>.</param>
    /// <returns>A new bitmap the caller must dispose.</returns>
    public static Bitmap Create(PaletteBase palette, Size size)
    {
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (size.Width < 1 || size.Height < 1)
        {
            size = new Size(KryptonPaletteFile.RecommendedThumbnailSize, KryptonPaletteFile.RecommendedThumbnailSize);
        }

        var bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.Transparent);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            PaintWindow(graphics, palette, new Rectangle(0, 0, size.Width, size.Height));
        }

        return bitmap;
    }

    /// <summary>
    /// Paints a square preview at <paramref name="size"/> pixels.
    /// </summary>
    /// <param name="palette">Palette to sample.</param>
    /// <param name="size">Width and height in pixels.</param>
    /// <returns>A new bitmap the caller must dispose.</returns>
    public static Bitmap Create(PaletteBase palette, int size) =>
        Create(palette, new Size(size, size));

    /// <summary>
    /// Replaces <see cref="KryptonCustomPaletteBase.Thumbnail"/> with a generated window mock-up.
    /// Disposes the previous thumbnail when it is a different instance.
    /// </summary>
    /// <param name="palette">Custom palette that will persist the image.</param>
    public static void AssignGeneratedThumbnail(KryptonCustomPaletteBase palette)
    {
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        var created = Create(palette, KryptonPaletteFile.RecommendedThumbnailSize);
        var previous = palette.Thumbnail;
        palette.Thumbnail = created;
        if (!ReferenceEquals(previous, created))
        {
            previous?.Dispose();
        }
    }

    /// <summary>
    /// Resolves a preview image for a theme selector row.
    /// Stored <see cref="KryptonCustomPaletteBase.Thumbnail"/> wins; otherwise a generated mock-up
    /// is returned for builtin or custom palettes. Returns <see langword="null"/> when the caller
    /// should use the Stable Kr tile alone (no palette and no stored image).
    /// </summary>
    /// <param name="themeName">Selector display name.</param>
    /// <param name="localCustom">Optional custom palette assigned to the selector.</param>
    /// <param name="generateWhenMissing">When <see langword="true"/>, paint a mock-up if nothing is stored.</param>
    /// <returns>A new image the caller must dispose, or <see langword="null"/>.</returns>
    public static Image? Resolve(string themeName, KryptonCustomPaletteBase? localCustom, bool generateWhenMissing)
    {
        if (ThemeManager.TryCreateRegisteredTheme(themeName, out var registered) && registered != null)
        {
            try
            {
                // Custom palettes without a stored Thumbnail use the Kr tile, not a generated mock-up.
                return FromCustom(registered);
            }
            finally
            {
                if (!ReferenceEquals(registered, KryptonManager.CurrentGlobalPalette)
                    && registered.Container == null)
                {
                    registered.Dispose();
                }
            }
        }

        if (IsCustomSelectorName(themeName))
        {
            var custom = localCustom ?? KryptonManager.CurrentGlobalPalette as KryptonCustomPaletteBase;
            if (custom != null)
            {
                return FromCustom(custom);
            }
        }

        if (!generateWhenMissing)
        {
            return null;
        }

        var mode = ThemeManager.GetThemeManagerMode(themeName);
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            return null;
        }

        try
        {
            return Create(KryptonManager.GetPaletteForMode(mode), KryptonPaletteFile.RecommendedThumbnailSize);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static Image? FromCustom(KryptonCustomPaletteBase custom)
    {
        var thumbnail = custom.Thumbnail;
        if (thumbnail == null)
        {
            return null;
        }

        try
        {
            return new Bitmap(thumbnail);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static bool IsCustomSelectorName(string themeName) =>
        !string.IsNullOrEmpty(themeName)
        && (themeName == PaletteModeStrings.DEFAULT_PALETTE_CUSTOM
            || themeName.StartsWith(ThemeManager.CustomThemeNamePrefix, StringComparison.Ordinal));

    private static void PaintWindow(Graphics graphics, PaletteBase palette, Rectangle bounds)
    {
        var table = palette.ColorTable;
        var pad = Math.Max(2, bounds.Width / 32);
        var window = Rectangle.Inflate(bounds, -pad, -pad);
        var captionHeight = Math.Max(8, window.Height * 22 / 100);
        var menuHeight = Math.Max(5, window.Height * 12 / 100);
        var statusHeight = Math.Max(6, window.Height * 14 / 100);
        var caption = new Rectangle(window.X, window.Y, window.Width, captionHeight);
        var menu = new Rectangle(window.X, caption.Bottom, window.Width, menuHeight);
        var status = new Rectangle(window.X, window.Bottom - statusHeight, window.Width, statusHeight);
        var client = new Rectangle(window.X, menu.Bottom, window.Width, Math.Max(1, status.Top - menu.Bottom));

        var formBack = Coalesce(palette.GetBackColor1(PaletteBackStyle.HeaderForm, PaletteState.Normal),
            table.MenuStripGradientBegin, SystemColors.ActiveCaption);
        var formBack2 = Coalesce(palette.GetBackColor2(PaletteBackStyle.HeaderForm, PaletteState.Normal),
            table.MenuStripGradientEnd, formBack);
        var menuBack = Coalesce(table.MenuStripGradientBegin, formBack);
        var clientBack = Coalesce(palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal),
            table.ToolStripContentPanelGradientBegin, SystemColors.Window);
        var statusBack = Coalesce(table.StatusStripGradientBegin, table.StatusStripGradientEnd, menuBack);
        var buttonBack = Coalesce(palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal),
            table.ButtonSelectedHighlight, SystemColors.Control);
        var border = Coalesce(palette.GetBorderColor1(PaletteBorderStyle.FormMain, PaletteState.Normal),
            table.ToolStripBorder, SystemColors.ActiveBorder);
        var captionText = Coalesce(palette.GetContentShortTextColor1(PaletteContentStyle.HeaderForm, PaletteState.Normal),
            table.MenuStripText, SystemColors.ActiveCaptionText);

        using (var path = Rounded(window, Math.Max(2, window.Width / 16)))
        using (var borderPen = new Pen(border))
        {
            graphics.SetClip(path);
            FillGradient(graphics, caption, formBack, formBack2);
            using (var menuBrush = new SolidBrush(menuBack))
            {
                graphics.FillRectangle(menuBrush, menu);
            }

            using (var clientBrush = new SolidBrush(clientBack))
            {
                graphics.FillRectangle(clientBrush, client);
            }

            using (var statusBrush = new SolidBrush(statusBack))
            {
                graphics.FillRectangle(statusBrush, status);
            }

            PaintCaptionGlyphs(graphics, caption, captionText);
            PaintClientButton(graphics, client, buttonBack, border);
            graphics.ResetClip();
            graphics.DrawPath(borderPen, path);
        }
    }

    private static void PaintCaptionGlyphs(Graphics graphics, Rectangle caption, Color color)
    {
        var glyph = Math.Max(2, caption.Height / 5);
        var top = caption.Y + (caption.Height - glyph) / 2;
        var right = caption.Right - glyph - Math.Max(2, caption.Width / 16);
        using (var brush = new SolidBrush(color))
        {
            for (var i = 0; i < 3; i++)
            {
                graphics.FillEllipse(brush, right - i * (glyph + 2), top, glyph, glyph);
            }
        }
    }

    private static void PaintClientButton(Graphics graphics, Rectangle client, Color fill, Color border)
    {
        var width = Math.Max(8, client.Width * 36 / 100);
        var height = Math.Max(6, client.Height * 28 / 100);
        var button = new Rectangle(client.X + (client.Width - width) / 2, client.Y + (client.Height - height) / 2,
            width, height);
        using (var path = Rounded(button, Math.Max(1, height / 4)))
        using (var brush = new SolidBrush(fill))
        using (var pen = new Pen(border))
        {
            graphics.FillPath(brush, path);
            graphics.DrawPath(pen, path);
        }
    }

    private static void FillGradient(Graphics graphics, Rectangle bounds, Color start, Color end)
    {
        if (bounds.Width < 1 || bounds.Height < 1)
        {
            return;
        }

        using (var brush = new LinearGradientBrush(bounds, start, end, LinearGradientMode.Vertical))
        {
            graphics.FillRectangle(brush, bounds);
        }
    }

    private static GraphicsPath Rounded(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        var d = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
        if (d < 2)
        {
            path.AddRectangle(bounds);
            return path;
        }

        var arc = new Rectangle(bounds.X, bounds.Y, d, d);
        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - d;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - d;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.X;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }

    private static Color Coalesce(params Color[] colors)
    {
        for (var i = 0; i < colors.Length; i++)
        {
            if (!colors[i].IsEmpty && colors[i].A > 0)
            {
                return colors[i];
            }
        }

        return SystemColors.Control;
    }
}
