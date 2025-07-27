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
    public static void PaintBuffered(Graphics targetGraphics, Rectangle targetRectangle, Action<Graphics> paintAction)
    {
        if (targetRectangle.Width <= 0 || targetRectangle.Height <= 0)
        {
            return;
        }

        using var bitmap = new Bitmap(targetRectangle.Width, targetRectangle.Height);
        using var gMem = Graphics.FromImage(bitmap);

        paintAction(gMem);
        targetGraphics.DrawImageUnscaled(bitmap, targetRectangle.Location);
    }

    /// <summary>
    /// Performs buffered painting when only an HDC is available.
    /// </summary>
    /// <param name="hdc">Handle to device context.</param>
    /// <param name="targetRectangle">Bounds to render into.</param>
    /// <param name="paintAction">Delegate that does the actual drawing into the provided Graphics.</param>
    public static void PaintBuffered(IntPtr hdc, Rectangle targetRectangle, Action<Graphics> paintAction)
    {
        using var g = Graphics.FromHdc(hdc);
        PaintBuffered(g, targetRectangle, paintAction);
    }
}
