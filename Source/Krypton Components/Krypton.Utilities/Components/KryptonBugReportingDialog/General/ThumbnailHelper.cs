#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System.Runtime.InteropServices;

namespace Krypton.Utilities;

/// <summary>
/// Helper class for generating thumbnails from images and videos.
/// </summary>
internal static class ThumbnailHelper
{
    private const int THUMBNAIL_SIZE = 128;

    /// <summary>
    /// Generates a thumbnail image from a file path.
    /// </summary>
    /// <param name="filePath">The path to the image or video file.</param>
    /// <param name="thumbnailSize">The size of the thumbnail (default 128x128).</param>
    /// <returns>A thumbnail bitmap, or null if generation fails.</returns>
    public static Bitmap? GenerateThumbnail(string filePath, int thumbnailSize = THUMBNAIL_SIZE)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            return null;
        }

        try
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            
            if (IsImageFile(extension))
            {
                return GenerateImageThumbnail(filePath, thumbnailSize);
            }
            else if (IsVideoFile(extension))
            {
                return GenerateVideoThumbnail(filePath, thumbnailSize);
            }
            else
            {
                return GenerateFileIconThumbnail(filePath, thumbnailSize);
            }
        }
        catch
        {
            return null;
        }
    }

    private static bool IsImageFile(string extension)
    {
        return extension is ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".tiff" or ".tif" or ".ico" or ".webp";
    }

    private static bool IsVideoFile(string extension)
    {
        return extension is ".mp4" or ".avi" or ".mov" or ".wmv" or ".flv" or ".mkv" or ".webm" or ".m4v" or ".3gp";
    }

    private static Bitmap? GenerateImageThumbnail(string filePath, int size)
    {
        try
        {
            using var originalImage = Image.FromFile(filePath);
            return CreateThumbnail(originalImage, size);
        }
        catch
        {
            return null;
        }
    }

    private static Bitmap? GenerateVideoThumbnail(string filePath, int size)
    {
        try
        {
            var thumbnail = ExtractVideoThumbnail(filePath);
            if (thumbnail != null)
            {
                var result = CreateThumbnail(thumbnail, size);
                thumbnail.Dispose();
                return result;
            }
        }
        catch
        {
        }

        return GenerateFileIconThumbnail(filePath, size);
    }

    private static Bitmap? GenerateFileIconThumbnail(string filePath, int size)
    {
        try
        {
            var shfi = new Krypton.Toolkit.PI.SHFILEINFO();
            var flags = (uint)(Krypton.Toolkit.PI.SHGFI_.ICON | Krypton.Toolkit.PI.SHGFI_.LARGEICON);
            
            if (!File.Exists(filePath) && !Directory.Exists(filePath))
            {
                flags |= (uint)Krypton.Toolkit.PI.SHGFI_.USEFILEATTRIBUTES;
            }
            
            var result = Krypton.Toolkit.PI.SHGetFileInfo(filePath, 0, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
            
            if (result != IntPtr.Zero && shfi.hIcon != IntPtr.Zero)
            {
                var icon = Icon.FromHandle(shfi.hIcon);
                var bitmap = icon.ToBitmap();
                Krypton.Toolkit.PI.DestroyIcon(shfi.hIcon);
                var resultThumbnail = CreateThumbnail(bitmap, size);
                bitmap.Dispose();
                return resultThumbnail;
            }
        }
        catch
        {
        }

        return null;
    }

    private static Bitmap CreateThumbnail(Image sourceImage, int size)
    {
        var thumbnail = new Bitmap(size, size);
        using (var g = Graphics.FromImage(thumbnail))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            var sourceAspect = (float)sourceImage.Width / sourceImage.Height;
            var thumbAspect = 1.0f;

            int thumbWidth, thumbHeight;
            if (sourceAspect > thumbAspect)
            {
                thumbWidth = size;
                thumbHeight = (int)(size / sourceAspect);
            }
            else
            {
                thumbWidth = (int)(size * sourceAspect);
                thumbHeight = size;
            }

            var x = (size - thumbWidth) / 2;
            var y = (size - thumbHeight) / 2;

            g.Clear(Color.White);
            g.DrawImage(sourceImage, x, y, thumbWidth, thumbHeight);
        }

        return thumbnail;
    }

    private static Bitmap? ExtractVideoThumbnail(string filePath)
    {
        try
        {
            var shfi = new Krypton.Toolkit.PI.SHFILEINFO();
            var flags = (uint)(Krypton.Toolkit.PI.SHGFI_.ICON | Krypton.Toolkit.PI.SHGFI_.LARGEICON);
            
            var result = Krypton.Toolkit.PI.SHGetFileInfo(filePath, 0, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
            
            if (result != IntPtr.Zero && shfi.hIcon != IntPtr.Zero)
            {
                var icon = Icon.FromHandle(shfi.hIcon);
                var bitmap = icon.ToBitmap();
                Krypton.Toolkit.PI.DestroyIcon(shfi.hIcon);
                return bitmap;
            }
        }
        catch
        {
        }

        return null;
    }
}

