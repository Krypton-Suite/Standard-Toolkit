#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper class for performing buffered painting operations.
/// </summary>
public static class RenderBufferedPaintHelper
{
    /// <summary>
    /// Performs buffered painting by rendering into a GDI+ bitmap and then drawing it to the target graphics.
    /// </summary>
    /// <param name="targetGraphics">Graphics of the target surface (e.g. from HDC or PaintEvent).</param>
    /// <param name="targetRectangle">Bounds to render into.</param>
    /// <param name="paintAction">Delegate that does the actual drawing into the provided Graphics.</param>
    public static void PaintBuffered(Graphics targetGraphics, Rectangle targetRectangle, Action<Graphics> paintAction, bool preserveTextRenderingHint)
    {
        if (targetGraphics is null)
        {
            throw new ArgumentNullException(nameof(targetGraphics));
        }

        if (paintAction is null)
        {
            throw new ArgumentNullException(nameof(paintAction));
        }

        if (targetRectangle.Width <= 0 || targetRectangle.Height <= 0)
        {
            return;
        }

        // IMPORTANT:
        // Use an opaque bitmap so that Clear-Type rendered pixels are copied verbatim (no extra alpha).
        using var bitmap = new Bitmap(targetRectangle.Width,
                                      targetRectangle.Height,
                                      System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        using var gMem = Graphics.FromImage(bitmap);

        // Copy important graphics properties for consistent rendering quality
        gMem.TextRenderingHint = targetGraphics.TextRenderingHint;
        gMem.SmoothingMode = targetGraphics.SmoothingMode;
        gMem.PixelOffsetMode = targetGraphics.PixelOffsetMode;
        gMem.CompositingQuality = targetGraphics.CompositingQuality;

        // Apply high-quality defaults
        if (!preserveTextRenderingHint)
        {
            // Use grayscale anti-aliasing to avoid colour fringing when blitting
            gMem.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
        gMem.SmoothingMode = SmoothingMode.HighQuality;
        gMem.PixelOffsetMode = PixelOffsetMode.HighQuality;
        gMem.CompositingQuality = CompositingQuality.HighQuality;
        gMem.InterpolationMode = InterpolationMode.HighQualityBicubic;

        paintAction(gMem);

        // Draw with high-quality interpolation
        var originalInterpolation = targetGraphics.InterpolationMode;
        var originalSmoothing = targetGraphics.SmoothingMode;
        var originalPixelOffset = targetGraphics.PixelOffsetMode;
        var originalCompositingQuality = targetGraphics.CompositingQuality;
        var originalCompositingMode = targetGraphics.CompositingMode;
        try
        {
            targetGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            targetGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            targetGraphics.CompositingQuality = CompositingQuality.HighQuality;
            targetGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Use an overwrite copy so Clear-Type pixels inside the buffered bitmap are not alpha-blended a second time.
            targetGraphics.CompositingMode = CompositingMode.SourceCopy;

            targetGraphics.DrawImage(bitmap, targetRectangle.Location);
        }
        finally
        {
            targetGraphics.InterpolationMode = originalInterpolation;
            targetGraphics.SmoothingMode = originalSmoothing;
            targetGraphics.PixelOffsetMode = originalPixelOffset;
            targetGraphics.CompositingQuality = originalCompositingQuality;
            targetGraphics.CompositingMode = originalCompositingMode;
        }
    }

    /// <summary>
    /// Performs buffered painting when only an HDC is available.
    /// </summary>
    /// <param name="hdc">Handle to device context.</param>
    /// <param name="targetRectangle">Bounds to render into.</param>
    /// <param name="paintAction">Delegate that does the actual drawing into the provided Graphics.</param>
    public static void PaintBuffered(IntPtr hdc, Rectangle targetRectangle, Action<Graphics> paintAction, bool preserveTextRenderingHint)
    {
        using var g = Graphics.FromHdc(hdc);
        // Ensure high-quality settings for the target graphics as well
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        PaintBuffered(g, targetRectangle, paintAction, preserveTextRenderingHint);
    }

    // Backwards-compatible overloads (preserves previous behaviour)
    public static void PaintBuffered(Graphics targetGraphics, Rectangle targetRectangle, Action<Graphics> paintAction)
        => PaintBuffered(targetGraphics, targetRectangle, paintAction, preserveTextRenderingHint: false);

    public static void PaintBuffered(IntPtr hdc, Rectangle targetRectangle, Action<Graphics> paintAction)
        => PaintBuffered(hdc, targetRectangle, paintAction, preserveTextRenderingHint: false);


    public static void PaintWithOptionalBuffering(Graphics graphics, Rectangle bounds, bool skipBufferedPainting, Action<Graphics, Rectangle> paintAction)
    {
        if (paintAction == null)
        {
            throw new ArgumentNullException(nameof(paintAction));
        }

        // If buffering should be skipped (e.g. item contains colour emoji), draw directly.
        if (skipBufferedPainting)
        {
            paintAction(graphics, bounds);
            return;
        }

        // Otherwise draw via an off-screen buffer to avoid flicker.
        IntPtr hdc = graphics.GetHdc();
        try
        {
            Rectangle targetBounds = bounds; // Local copy for lambda capture
            PaintBuffered(hdc, targetBounds, g =>
            {
                // Use a (0,0)-based rectangle inside the buffered context
                var localBounds = new Rectangle(Point.Empty, targetBounds.Size);
                paintAction(g, localBounds);
            });
        }
        finally
        {
            graphics.ReleaseHdc(hdc);
        }
    }
}
