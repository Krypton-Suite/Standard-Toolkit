#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Drawing;

/// <summary>
/// A parsed SVG that can report its intrinsic size and draw into a GDI+ bitmap.
/// </summary>
internal interface IKryptonSvgDocument : IDisposable
{
    /// <summary>
    /// Gets the document size in device-independent pixels (96 DPI).
    /// </summary>
    SizeF IntrinsicDipSize { get; }

    /// <summary>
    /// Draws the document into a new premultiplied bitmap of the requested pixel size.
    /// </summary>
    Bitmap Rasterize(int pixelWidth, int pixelHeight);
}

/// <summary>
/// Opens markup with the rasterizer selected for this process.
/// </summary>
internal static class KryptonSvgDocuments
{
    internal static IKryptonSvgDocument? TryOpen(string svg, out string? error)
    {
        if (KryptonSvgRasterizerChoice.Active == KryptonSvgRasterizer.Direct2D)
        {
            return KryptonSvgDirect2DDocument.TryOpen(svg, out error);
        }

#if NETFRAMEWORK
        return KryptonSvgSkiaDocument.TryOpen(svg, out error);
#else
        error = "Direct2D SVG is not available on this version of Windows.";
        return null;
#endif
    }
}
