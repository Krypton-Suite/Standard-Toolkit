#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Renders a QR code module matrix to a bitmap.
/// </summary>
internal static class QRCodeBitmapRenderer
{
    #region Implementation

    /// <summary>
    /// Renders the QR code matrix to a bitmap.
    /// </summary>
    /// <param name="matrix">The module matrix (true = dark).</param>
    /// <param name="moduleSize">Pixels per module.</param>
    /// <param name="darkColor">Color for dark modules.</param>
    /// <param name="lightColor">Color for light modules.</param>
    /// <param name="showBorder">Whether to add a quiet zone (4 modules).</param>
    /// <returns>A bitmap of the QR code.</returns>
    public static Bitmap Render(bool[,] matrix, int moduleSize, Color darkColor, Color lightColor, bool showBorder)
        => Render(matrix, moduleSize, darkColor, lightColor, showBorder, null, 0.22f, 1, default);

    /// <summary>
    /// Renders the QR code matrix to a bitmap, optionally drawing a centered image over the symbol.
    /// </summary>
    public static Bitmap Render(
        bool[,] matrix,
        int moduleSize,
        Color darkColor,
        Color lightColor,
        bool showBorder,
        Image? centerImage,
        float centerImageRelativeSize,
        int centerImagePaddingModules,
        QRCodeCenterImagePalette centerImagePalette)
    {
        int border = showBorder ? moduleSize * 4 : 0;
        int size = matrix.GetLength(0);
        int pixelSize = size * moduleSize + border * 2;

        var bmp = new Bitmap(pixelSize, pixelSize);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(lightColor);
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (matrix[row, col])
                    {
                        using var brush = new SolidBrush(darkColor);
                        g.FillRectangle(brush, border + col * moduleSize, border + row * moduleSize, moduleSize, moduleSize);
                    }
                }
            }

            DrawCenterImageIfAny(g, size, moduleSize, border, border, lightColor, centerImage, centerImageRelativeSize, centerImagePaddingModules, centerImagePalette);
        }

        return bmp;
    }

    /// <summary>
    /// Draws an optional centered image on top of an already-rendered module grid.
    /// </summary>
    internal static void DrawCenterImageIfAny(
        Graphics g,
        int matrixModuleCount,
        int moduleSize,
        int modulesOriginX,
        int modulesOriginY,
        Color lightColor,
        Image? centerImage,
        float relativeSize,
        int paddingModules,
        QRCodeCenterImagePalette centerImagePalette)
    {
        if (centerImage is null)
        {
            return;
        }

        int qrPx = matrixModuleCount * moduleSize;
        if (qrPx < 1)
        {
            return;
        }

        float r = relativeSize;
        if (r < 0.05f)
        {
            r = 0.05f;
        }
        else if (r > 0.45f)
        {
            r = 0.45f;
        }

        int outer = Math.Max(moduleSize * 2, (int)Math.Round(qrPx * (double)r));
        if (outer > qrPx)
        {
            outer = qrPx;
        }

        int ox = modulesOriginX + (qrPx - outer) / 2;
        int oy = modulesOriginY + (qrPx - outer) / 2;

        using (var lightBrush = new SolidBrush(lightColor))
        {
            g.FillRectangle(lightBrush, ox, oy, outer, outer);
        }

        int padPx = Math.Max(0, paddingModules) * moduleSize;
        int innerSide = outer - 2 * padPx;
        if (innerSide < 1)
        {
            innerSide = 1;
            padPx = (outer - innerSide) / 2;
        }

        float ix = ox + padPx;
        float iy = oy + padPx;
        float ratio = Math.Min(innerSide / (float)centerImage.Width, innerSide / (float)centerImage.Height);
        int w = Math.Max(1, (int)Math.Round(centerImage.Width * ratio));
        int h = Math.Max(1, (int)Math.Round(centerImage.Height * ratio));
        int lx = (int)Math.Round(ix + (innerSide - w) / 2f);
        int ly = (int)Math.Round(iy + (innerSide - h) / 2f);

        InterpolationMode savedInterpolation = g.InterpolationMode;
        PixelOffsetMode savedPixelOffset = g.PixelOffsetMode;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.Half;
        try
        {
            var destRect = new Rectangle(lx, ly, w, h);
            if (centerImagePalette.UsePaletteColors)
            {
                PaletteImageDrawing.Draw(
                    g,
                    centerImage,
                    destRect,
                    VisualOrientation.Top,
                    centerImagePalette.Effect,
                    centerImagePalette.TransparentColor,
                    centerImagePalette.ColorMap,
                    centerImagePalette.ColorTo);
            }
            else
            {
                g.DrawImage(centerImage, destRect);
            }
        }
        finally
        {
            g.InterpolationMode = savedInterpolation;
            g.PixelOffsetMode = savedPixelOffset;
        }
    }

#endregion
}