#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Draws images using the same palette image effect and color-remap rules as <see cref="RenderBase"/>.
/// </summary>
public static class PaletteImageDrawing
{
    #region Static Fields

    private static readonly object _threadLock = new object();

    private static readonly ColorMatrix _matrixGrayScale = new ColorMatrix([
        [0.3f, 0.3f, 0.3f, 0, 0], [0.59f, 0.59f, 0.59f, 0, 0],
        [0.11f, 0.11f, 0.11f, 0, 0], [0, 0, 0, 1, 0], [0, 0, 0, 0, 1]
    ]);

    private static readonly ColorMatrix _matrixGrayScaleRed = new ColorMatrix([
        [1, 0, 0, 0, 0], [0, 0.59f, 0.59f, 0, 0], [0, 0.11f, 0.11f, 0, 0],
        [0, 0, 0, 1, 0], [0, 0, 0, 0, 1]
    ]);

    private static readonly ColorMatrix _matrixGrayScaleGreen = new ColorMatrix([
        [0.3f, 0, 0.3f, 0, 0], [0, 1, 0, 0, 0], [0.11f, 0, 0.11f, 0, 0],
        [0, 0, 0, 1, 0], [0, 0, 0, 0, 1]
    ]);

    private static readonly ColorMatrix _matrixGrayScaleBlue = new ColorMatrix([
        [0.3f, 0.3f, 0, 0, 0], [0.59f, 0.59f, 0, 0, 0], [0, 0, 1, 0, 0],
        [0, 0, 0, 1, 0], [0, 0, 0, 0, 1]
    ]);

    private static readonly ColorMatrix _matrixLight = new ColorMatrix([
        [1, 0, 0, 0, 0], [0, 1, 0, 0, 0], [0, 0, 1, 0, 0],
        [0, 0, 0, 1, 0], [0.1f, 0.1f, 0.1f, 0, 1]
    ]);

    private static readonly ColorMatrix _matrixLightLight = new ColorMatrix([
        [1, 0, 0, 0, 0], [0, 1, 0, 0, 0], [0, 0, 1, 0, 0],
        [0, 0, 0, 1, 0], [0.2f, 0.2f, 0.2f, 0, 1]
    ]);

    private static readonly ColorMatrix _matrixDark = new ColorMatrix([
        [1, 0, 0, 0, 0], [0, 1, 0, 0, 0], [0, 0, 1, 0, 0],
        [0, 0, 0, 1, 0], [-0.1f, -0.1f, -0.1f, 0, 1]
    ]);

    private static readonly ColorMatrix _matrixDarkDark = new ColorMatrix([
        [1, 0, 0, 0, 0], [0, 1, 0, 0, 0], [0, 0, 1, 0, 0],
        [0, 0, 0, 1, 0], [-0.25f, -0.25f, -0.25f, 0, 1]
    ]);

    #endregion

    #region Identity

    /// <summary>
    /// Draws an image with palette image effect and optional color remapping.
    /// </summary>
    /// <param name="graphics">Target graphics.</param>
    /// <param name="image">Image to draw.</param>
    /// <param name="destRect">Destination rectangle.</param>
    /// <param name="orientation">Visual orientation.</param>
    /// <param name="effect">Drawing effect.</param>
    /// <param name="remapTransparent">Color that should become transparent.</param>
    /// <param name="remapColor">Image color to remap.</param>
    /// <param name="remapNew">Replacement color for <paramref name="remapColor"/>.</param>
    public static void Draw(
        Graphics graphics,
        Image image,
        Rectangle destRect,
        VisualOrientation orientation,
        PaletteImageEffect effect,
        Color remapTransparent,
        Color remapColor,
        Color remapNew)
    {
        if (graphics == null)
        {
            throw new ArgumentNullException(nameof(graphics));
        }

        if (image == null)
        {
            throw new ArgumentNullException(nameof(image));
        }

        lock (_threadLock)
        {
            using var attribs = new ImageAttributes();

            switch (effect)
            {
                case PaletteImageEffect.Disabled:
                    attribs.SetColorMatrix(CommonHelper.MatrixDisabled);
                    break;
                case PaletteImageEffect.GrayScale:
                    attribs.SetColorMatrix(_matrixGrayScale, ColorMatrixFlag.SkipGrays);
                    break;
                case PaletteImageEffect.GrayScaleRed:
                    attribs.SetColorMatrix(_matrixGrayScaleRed, ColorMatrixFlag.SkipGrays);
                    break;
                case PaletteImageEffect.GrayScaleGreen:
                    attribs.SetColorMatrix(_matrixGrayScaleGreen, ColorMatrixFlag.SkipGrays);
                    break;
                case PaletteImageEffect.GrayScaleBlue:
                    attribs.SetColorMatrix(_matrixGrayScaleBlue, ColorMatrixFlag.SkipGrays);
                    break;
                case PaletteImageEffect.Light:
                    attribs.SetColorMatrix(_matrixLight);
                    break;
                case PaletteImageEffect.LightLight:
                    attribs.SetColorMatrix(_matrixLightLight);
                    break;
                case PaletteImageEffect.Dark:
                    attribs.SetColorMatrix(_matrixDark);
                    break;
                case PaletteImageEffect.DarkDark:
                    attribs.SetColorMatrix(_matrixDarkDark);
                    break;
                case PaletteImageEffect.Inherit:
                case PaletteImageEffect.Normal:
                    break;
            }

            if ((remapTransparent != GlobalStaticVariables.EMPTY_COLOR) ||
                ((remapColor != GlobalStaticVariables.EMPTY_COLOR) && (remapNew != GlobalStaticVariables.EMPTY_COLOR)))
            {
                var colorMaps = new List<ColorMap>();

                if (remapTransparent != GlobalStaticVariables.EMPTY_COLOR)
                {
                    colorMaps.Add(new ColorMap
                    {
                        OldColor = remapTransparent,
                        NewColor = Color.Transparent
                    });
                }

                if ((remapColor != GlobalStaticVariables.EMPTY_COLOR) && (remapNew != GlobalStaticVariables.EMPTY_COLOR))
                {
                    colorMaps.Add(new ColorMap
                    {
                        OldColor = remapColor,
                        NewColor = remapNew
                    });
                }

                attribs.SetRemapTable(colorMaps.ToArray(), ColorAdjustType.Bitmap);
            }

            var translateX = 0;
            var translateY = 0;
            var rotation = 0f;
            Rectangle drawRect = destRect;

            switch (orientation)
            {
                case VisualOrientation.Bottom:
                    translateX = (drawRect.X * 2) + drawRect.Width;
                    translateY = (drawRect.Y * 2) + drawRect.Height;
                    rotation = 180f;
                    break;
                case VisualOrientation.Left:
                    drawRect = drawRect with { Width = drawRect.Height, Height = drawRect.Width };
                    translateX = drawRect.X - drawRect.Y;
                    translateY = drawRect.X + drawRect.Y + drawRect.Width;
                    rotation = -90f;
                    break;
                case VisualOrientation.Right:
                    drawRect = drawRect with { Width = drawRect.Height, Height = drawRect.Width };
                    translateX = drawRect.X + drawRect.Y + drawRect.Height;
                    translateY = -(drawRect.X - drawRect.Y);
                    rotation = 90f;
                    break;
            }

            if ((translateX != 0) || (translateY != 0))
            {
                graphics.TranslateTransform(translateX, translateY);
            }

            if (rotation != 0f)
            {
                graphics.RotateTransform(rotation);
            }

            try
            {
                graphics.DrawImage(image, drawRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attribs);
            }
            catch (ArgumentException)
            {
            }
            finally
            {
                if (rotation != 0f)
                {
                    graphics.RotateTransform(-rotation);
                }

                if ((translateX != 0) || (translateY != 0))
                {
                    graphics.TranslateTransform(-translateX, -translateY);
                }
            }
        }
    }

    #endregion
}
