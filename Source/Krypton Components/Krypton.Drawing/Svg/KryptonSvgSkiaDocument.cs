#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if NETFRAMEWORK
using SkiaSharp;
using Svg.Skia;
#endif

namespace Krypton.Drawing;

#if NETFRAMEWORK
/// <summary>
/// Svg.Skia document used when Direct2D SVG is not available.
/// </summary>
internal sealed class KryptonSvgSkiaDocument : IKryptonSvgDocument
{
    private readonly SKSvg _svg;
    private readonly SKPicture _picture;

    private KryptonSvgSkiaDocument(SKSvg svg, SKPicture picture, SizeF intrinsicDipSize)
    {
        _svg = svg;
        _picture = picture;
        IntrinsicDipSize = intrinsicDipSize;
    }

    /// <inheritdoc />
    public SizeF IntrinsicDipSize { get; }

    internal static KryptonSvgSkiaDocument? TryOpen(string svg, out string? error)
    {
        SKSvg? document = null;
        try
        {
            document = new SKSvg();
            document.Settings.EnableJavaScript = false;
            document.Settings.EnableExternalJavaScript = false;
            var picture = document.FromSvg(svg);
            if (picture == null || !TryGetCull(picture, out var width, out var height))
            {
                document.Dispose();
                error = picture == null
                    ? "The SVG document could not be parsed."
                    : "The SVG document has no drawable bounds.";
                return null;
            }

            error = null;
            var opened = new KryptonSvgSkiaDocument(document, picture, new SizeF(width, height));
            document = null;
            return opened;
        }
        catch (Exception ex)
        {
            document?.Dispose();
            error = ex.Message;
            return null;
        }
    }

    /// <inheritdoc />
    public Bitmap Rasterize(int pixelWidth, int pixelHeight)
    {
        var cull = _picture.CullRect;
        using (var skBitmap = new SKBitmap(pixelWidth, pixelHeight, SKColorType.Bgra8888, SKAlphaType.Unpremul))
        using (var canvas = new SKCanvas(skBitmap))
        {
            canvas.Clear(SKColors.Transparent);
            // Translate in SVG space first, then scale into the bitmap.
            canvas.Scale(pixelWidth / cull.Width, pixelHeight / cull.Height);
            canvas.Translate(-cull.Left, -cull.Top);
            canvas.DrawPicture(_picture);
            canvas.Flush();
            return CopyToGdiBitmap(skBitmap);
        }
    }

    /// <inheritdoc />
    public void Dispose() => _svg.Dispose();

    private static bool TryGetCull(SKPicture picture, out float width, out float height)
    {
        var cull = picture.CullRect;
        width = cull.Width;
        height = cull.Height;
        return width > 0f
               && height > 0f
               && !float.IsNaN(width)
               && !float.IsNaN(height)
               && !float.IsInfinity(width)
               && !float.IsInfinity(height);
    }

    private static Bitmap CopyToGdiBitmap(SKBitmap source)
    {
        var bitmap = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
        var bounds = new Rectangle(0, 0, source.Width, source.Height);
        var bits = bitmap.LockBits(bounds, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        try
        {
            var copyBytes = Math.Min(source.RowBytes, bits.Stride);
            var row = new byte[copyBytes];
            var srcBase = source.GetPixels();
            for (var y = 0; y < source.Height; y++)
            {
                Marshal.Copy(IntPtr.Add(srcBase, y * source.RowBytes), row, 0, copyBytes);
                Marshal.Copy(row, 0, IntPtr.Add(bits.Scan0, y * bits.Stride), copyBytes);
            }
        }
        finally
        {
            bitmap.UnlockBits(bits);
        }

        return bitmap;
    }
}
#endif
