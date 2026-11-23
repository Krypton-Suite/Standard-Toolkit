#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Vector and image painting for <see cref="KryptonRating"/> glyphs, plus value snapping.
/// </summary>
internal static class RatingGlyphPainter
{
    internal static readonly Color DefaultFill = Color.FromArgb(255, 196, 37);
    internal static readonly Color DefaultEmpty = Color.FromArgb(180, 176, 176, 176);
    internal static readonly Color DefaultHover = Color.FromArgb(255, 220, 80);
    internal static readonly Color DefaultDisabled = Color.FromArgb(160, 160, 160);
    internal static readonly Color DefaultOutline = Color.FromArgb(180, 140, 20);

    internal static decimal GetStep(KryptonRatingPrecision precision) =>
        precision switch
        {
            KryptonRatingPrecision.Half => 0.5m,
            KryptonRatingPrecision.Exact => 0.1m,
            _ => 1m
        };

    internal static decimal Snap(decimal value, KryptonRatingPrecision precision, int maximum)
    {
        if (value < 0m)
        {
            value = 0m;
        }

        if (value > maximum)
        {
            value = maximum;
        }

        return precision switch
        {
            KryptonRatingPrecision.Full => Math.Round(value, MidpointRounding.AwayFromZero),
            KryptonRatingPrecision.Half => Math.Round(value * 2m, MidpointRounding.AwayFromZero) / 2m,
            _ => Math.Round(value, 2, MidpointRounding.AwayFromZero)
        };
    }

    internal static float GlyphFill(decimal displayValue, int glyphIndex, KryptonRatingPrecision precision)
    {
        // glyphIndex is 1-based.
        decimal remainder = displayValue - (glyphIndex - 1);
        if (remainder <= 0m)
        {
            return 0f;
        }

        if (remainder >= 1m)
        {
            return 1f;
        }

        return precision switch
        {
            KryptonRatingPrecision.Full => 0f,
            KryptonRatingPrecision.Half => remainder >= 0.5m ? 0.5f : 0f,
            _ => (float)remainder
        };
    }

    internal static void ResolveColors(KryptonRating owner,
        PaletteBase? palette,
        bool enabled,
        bool hovering,
        out Color fill,
        out Color empty,
        out Color outline)
    {
        Color paletteText = palette?.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Disabled)
                            ?? DefaultEmpty;
        Color fallbackEmpty = Color.FromArgb(160, paletteText);

        if (!enabled)
        {
            fill = owner.StateDisabled.GetResolvedFill(DefaultDisabled);
            empty = owner.StateDisabled.GetResolvedEmpty(Color.FromArgb(120, fill));
            outline = ControlPaint.Dark(fill);
        }
        else if (hovering)
        {
            fill = owner.StateTracking.GetResolvedFill(DefaultHover);
            empty = owner.StateTracking.GetResolvedEmpty(fallbackEmpty);
            outline = ControlPaint.Dark(fill);
        }
        else
        {
            fill = owner.StateNormal.GetResolvedFill(DefaultFill);
            empty = owner.StateNormal.GetResolvedEmpty(fallbackEmpty);
            outline = ControlPaint.Dark(fill);
        }

        if (outline.A == 0)
        {
            outline = DefaultOutline;
        }
    }

    internal static void DrawGlyph(Graphics g,
        Rectangle bounds,
        KryptonRatingGlyph glyph,
        RatingValues values,
        float fillFraction,
        bool reverseFill,
        Color fill,
        Color empty,
        Color outline,
        bool enabled)
    {
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        SmoothingMode oldSmooth = g.SmoothingMode;
        PixelOffsetMode oldOffset = g.PixelOffsetMode;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        try
        {
            if (glyph == KryptonRatingGlyph.Image)
            {
                DrawImageGlyph(g, bounds, values, fillFraction, reverseFill, enabled);
                return;
            }

            using GraphicsPath path = CreatePath(glyph, bounds);
            using (var emptyBrush = new SolidBrush(empty))
            {
                g.FillPath(emptyBrush, path);
            }

            if (fillFraction > 0f)
            {
                Rectangle clip = GetFillClip(bounds, fillFraction, reverseFill);
                Region oldClip = g.Clip;
                g.SetClip(clip, CombineMode.Intersect);
                using (var fillBrush = new SolidBrush(fill))
                {
                    g.FillPath(fillBrush, path);
                }

                g.Clip = oldClip;
            }

            using var pen = new Pen(outline, Math.Max(1f, bounds.Width / 16f));
            pen.LineJoin = LineJoin.Round;
            g.DrawPath(pen, path);
        }
        finally
        {
            g.SmoothingMode = oldSmooth;
            g.PixelOffsetMode = oldOffset;
        }
    }

    private static void DrawImageGlyph(Graphics g,
        Rectangle bounds,
        RatingValues values,
        float fillFraction,
        bool reverseFill,
        bool enabled)
    {
        Image filled = values.ImageFilled
                       ?? ResourceFiles.Stars.StarImageResources.star_yellow as Image
                       ?? CreateFallbackBitmap(bounds.Size, enabled ? DefaultFill : DefaultDisabled);

        Image? emptyImage = values.ImageEmpty;
        Image? halfImage = values.ImageHalf
                           ?? ResourceFiles.Stars.StarImageResources.star_yellow_half_16 as Image;

        if (!enabled)
        {
            Image? disabled = ResourceFiles.Stars.StarImageResources.star_yellow_disabled as Image;
            if (disabled != null)
            {
                filled = disabled;
            }
        }

        if (emptyImage != null)
        {
            DrawScaledImage(g, emptyImage, bounds, 1f);
        }
        else
        {
            DrawScaledImage(g, filled, bounds, 0.28f);
        }

        if (fillFraction <= 0f)
        {
            return;
        }

        if (Math.Abs(fillFraction - 0.5f) < 0.01f && halfImage != null && !reverseFill)
        {
            DrawScaledImage(g, halfImage, bounds, 1f);
            return;
        }

        Rectangle clip = GetFillClip(bounds, fillFraction, reverseFill);
        Region oldClip = g.Clip;
        g.SetClip(clip, CombineMode.Intersect);
        DrawScaledImage(g, filled, bounds, 1f);
        g.Clip = oldClip;
    }

    private static void DrawScaledImage(Graphics g, Image image, Rectangle bounds, float opacity)
    {
        if (opacity <= 0f)
        {
            return;
        }

        if (opacity >= 0.999f)
        {
            g.DrawImage(image, bounds);
            return;
        }

        var matrix = new ColorMatrix
        {
            Matrix33 = opacity
        };

        using var attributes = new ImageAttributes();
        attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        g.DrawImage(image, bounds, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
    }

    private static Rectangle GetFillClip(Rectangle bounds, float fraction, bool reverseFill)
    {
        fraction = Math.Max(0f, Math.Min(1f, fraction));
        int width = Math.Max(1, (int)Math.Round(bounds.Width * fraction));
        return reverseFill
            ? new Rectangle(bounds.Right - width, bounds.Y, width, bounds.Height)
            : new Rectangle(bounds.X, bounds.Y, width, bounds.Height);
    }

    private static GraphicsPath CreatePath(KryptonRatingGlyph glyph, Rectangle bounds)
    {
        var path = new GraphicsPath();
        switch (glyph)
        {
            case KryptonRatingGlyph.Heart:
                AddHeart(path, bounds);
                break;
            case KryptonRatingGlyph.Circle:
                path.AddEllipse(Rectangle.Inflate(bounds, -1, -1));
                break;
            default:
                AddStar(path, bounds);
                break;
        }

        return path;
    }

    private static void AddStar(GraphicsPath path, Rectangle bounds)
    {
        float cx = bounds.X + bounds.Width / 2f;
        float cy = bounds.Y + bounds.Height / 2f;
        float outer = Math.Min(bounds.Width, bounds.Height) / 2f - 1f;
        float inner = outer * 0.42f;
        var points = new PointF[10];
        for (int i = 0; i < 10; i++)
        {
            double angle = (-90d + i * 36d) * Math.PI / 180d;
            float radius = (i % 2) == 0 ? outer : inner;
            points[i] = new PointF(cx + (float)(Math.Cos(angle) * radius), cy + (float)(Math.Sin(angle) * radius));
        }

        path.AddPolygon(points);
        path.CloseFigure();
    }

    private static void AddHeart(GraphicsPath path, Rectangle bounds)
    {
        float x = bounds.X;
        float y = bounds.Y;
        float w = bounds.Width;
        float h = bounds.Height;
        path.AddBezier(x + w / 2f, y + h * 0.32f, x + w * 0.15f, y, x, y + h * 0.28f, x + w / 2f, y + h);
        path.AddBezier(x + w / 2f, y + h, x + w, y + h * 0.28f, x + w * 0.85f, y, x + w / 2f, y + h * 0.32f);
        path.CloseFigure();
    }

    private static Image CreateFallbackBitmap(Size size, Color color)
    {
        int width = Math.Max(8, size.Width);
        int height = Math.Max(8, size.Height);
        var bitmap = new Bitmap(width, height);
        using Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using var brush = new SolidBrush(color);
        g.FillEllipse(brush, 1, 1, width - 3, height - 3);
        return bitmap;
    }
}
