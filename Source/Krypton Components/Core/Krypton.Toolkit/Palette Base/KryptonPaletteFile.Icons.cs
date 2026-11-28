#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <content>
/// Theme list and file icons: the Stable Kr tile, or a palette thumbnail with that tile as a corner overlay.
/// <c>.kthemex</c> stores <see cref="KryptonCustomPaletteBase.Thumbnail"/> as base64 PNG in the XML image cache.
/// </content>
public static partial class KryptonPaletteFile
{
    private const float ThemeIconOverlayScale = 0.45f;

    /// <summary>
    /// Returns the embedded Stable Kr tile used as the default palette-file icon and as the thumbnail overlay.
    /// Do not dispose the returned instance.
    /// </summary>
    public static Image GetThemeBadgeImage()
    {
        var image = ToolkitLogoImageResources.Krypton_Stable;
        if (image == null)
        {
            ThrowHelper.ThrowInvalidOperationException(@"Embedded Krypton Stable tile is missing.");
        }

        return image!;
    }

    /// <summary>
    /// Builds a square icon for a palette theme.
    /// When <paramref name="thumbnail"/> is set, that preview fills the canvas and the Stable Kr tile is drawn
    /// in the bottom-right corner. Otherwise the Kr tile fills the canvas.
    /// </summary>
    /// <param name="thumbnail">Optional theme preview (from <c>.kthemex</c> base64 or a <c>.ktheme</c> catalog).</param>
    /// <param name="size">Output size in pixels. Empty or non-positive values use <see cref="RecommendedThumbnailSize"/>.</param>
    /// <returns>A new bitmap the caller must dispose.</returns>
    public static Bitmap CreateThemeIcon(Image? thumbnail, Size size)
    {
        if (size.Width < 1 || size.Height < 1)
        {
            size = new Size(RecommendedThumbnailSize, RecommendedThumbnailSize);
        }

        var badge = GetThemeBadgeImage();
        var bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.Transparent);
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            var bounds = new Rectangle(0, 0, size.Width, size.Height);
            DrawFitted(graphics, thumbnail ?? badge, bounds);
            if (thumbnail != null)
            {
                var overlay = OverlayBounds(size);
                DrawFitted(graphics, badge, overlay);
            }
        }

        return bitmap;
    }

    /// <summary>
    /// Builds a square icon at <paramref name="size"/> pixels.
    /// </summary>
    /// <param name="thumbnail">Optional theme preview.</param>
    /// <param name="size">Width and height in pixels.</param>
    /// <returns>A new bitmap the caller must dispose.</returns>
    public static Bitmap CreateThemeIcon(Image? thumbnail, int size) =>
        CreateThemeIcon(thumbnail, new Size(size, size));

    /// <summary>
    /// Builds a 16×16 or 32×32 icon for a palette file. Uses the first stored thumbnail when present.
    /// </summary>
    /// <param name="path">Palette file path. Missing files use the Kr tile alone.</param>
    /// <param name="largeIcon"><see langword="true"/> for 32×32; otherwise 16×16.</param>
    /// <returns>A new icon the caller must dispose, or <see langword="null"/> if composition fails.</returns>
    public static Icon? CreateThemeFileIcon(string? path, bool largeIcon = false)
    {
        Image? thumbnail = null;
        try
        {
            thumbnail = TryFirstThumbnail(path);
            using var bitmap = CreateThemeIcon(thumbnail, largeIcon ? 32 : 16);
            return IconFromBitmap(bitmap);
        }
        catch (Exception)
        {
            return CreateShellIcon(largeIcon);
        }
        finally
        {
            thumbnail?.Dispose();
        }
    }

    private static Image? TryFirstThumbnail(string? path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path) || !IsPaletteExtension(path))
        {
            return null;
        }

        Image?[] thumbs;
        try
        {
            thumbs = GetThemeThumbnails(path!);
        }
        catch (Exception)
        {
            return null;
        }

        Image? first = null;
        for (var i = 0; i < thumbs.Length; i++)
        {
            if (first == null && thumbs[i] != null)
            {
                first = thumbs[i];
            }
            else
            {
                thumbs[i]?.Dispose();
            }
        }

        return first;
    }

    private static Icon? IconFromBitmap(Bitmap bitmap)
    {
        var handle = bitmap.GetHicon();
        try
        {
            using var created = Icon.FromHandle(handle);
            return (Icon)created.Clone();
        }
        finally
        {
            PI.DestroyIcon(handle);
        }
    }

    private static Rectangle OverlayBounds(Size canvas)
    {
        var min = Math.Min(canvas.Width, canvas.Height);
        var overlay = Math.Max(8, (int)(min * ThemeIconOverlayScale));
        var pad = Math.Max(1, overlay / 16);
        return new Rectangle(canvas.Width - overlay - pad, canvas.Height - overlay - pad, overlay, overlay);
    }

    private static void DrawFitted(Graphics graphics, Image image, Rectangle dest)
    {
        graphics.DrawImage(image, dest, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
    }
}
