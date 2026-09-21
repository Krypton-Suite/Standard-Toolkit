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
/// Draws an optional pulsing border for input controls.
/// </summary>
internal static class InputPulsingBorderRenderer
{
    private const int GlowHeight = 10;
    private const float LineHeight = 2.5f;

    /// <summary>
    /// Draw a pulsing border inside the specified bounds.
    /// </summary>
    public static void Draw(RenderContext context,
        Rectangle bounds,
        IPaletteBorder? paletteBorder,
        PaletteState state,
        float animationPhase,
        bool animate,
        InputPulsingBorderStyle style,
        Color edgeColor1,
        Color edgeColor2,
        Color highlightColor)
    {
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        if (context.Renderer == null)
        {
            return;
        }

        using GraphicsPath path = GetGlowBorderPath(context, bounds, paletteBorder, state);
        if (path.PointCount == 0)
        {
            return;
        }

        GraphicsState? clipState = null;
        if (style == InputPulsingBorderStyle.Bottom)
        {
            float rounding = paletteBorder?.GetBorderRounding(state) ?? 0f;
            int clipHeight = Math.Max(GlowHeight + 6, (int)Math.Ceiling(rounding) + GlowHeight);
            clipHeight = Math.Min(clipHeight, bounds.Height);
            clipState = context.Graphics.Save();
            var clip = new Rectangle(bounds.X, bounds.Bottom - clipHeight, bounds.Width, clipHeight);
            context.Graphics.SetClip(clip, CombineMode.Intersect);
        }

        try
        {
            using var antiAlias = new AntiAlias(context.Graphics);

            if (style == InputPulsingBorderStyle.Bottom)
            {
                DrawBottomHalo(context.Graphics, path, animate, highlightColor);
            }

            DrawPathGlow(context.Graphics, bounds, path, animationPhase, animate, edgeColor1, edgeColor2, highlightColor);
        }
        finally
        {
            if (clipState != null)
            {
                context.Graphics.Restore(clipState);
            }
        }
    }

    private static GraphicsPath GetGlowBorderPath(RenderContext context,
        Rectangle bounds,
        IPaletteBorder? paletteBorder,
        PaletteState state)
    {
        if (paletteBorder == null)
        {
            return CreateRoundedRectPath(bounds, 0f);
        }

        var forcedPalette = new PaletteBorderInheritForced(paletteBorder);
        forcedPalette.ForceBorderEdges(PaletteDrawBorders.All);

        return context.Renderer!.RenderStandardBorder.GetBorderPath(context,
            bounds,
            forcedPalette,
            VisualOrientation.Top,
            state);
    }

    private static GraphicsPath CreateRoundedRectPath(Rectangle bounds, float rounding)
    {
        var path = new GraphicsPath();
        if (rounding <= 0.1f)
        {
            path.AddRectangle(bounds);
            return path;
        }

        float radius = Math.Min(rounding, Math.Min(bounds.Width, bounds.Height) / 2f);
        float diameter = radius * 2f;
        var rect = new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180f, 90f);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270f, 90f);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0f, 90f);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90f, 90f);
        path.CloseFigure();
        return path;
    }

    private static void DrawBottomHalo(Graphics g,
        GraphicsPath path,
        bool animate,
        Color highlightColor)
    {
        RectangleF pathBounds = path.GetBounds();
        if (pathBounds.Width <= 0 || pathBounds.Height <= 0)
        {
            return;
        }

        float ellipseWidth = pathBounds.Width * 0.55f;
        float ellipseHeight = GlowHeight * 1.6f;
        var ellipseRect = new RectangleF(
            pathBounds.Left + ((pathBounds.Width - ellipseWidth) / 2f),
            pathBounds.Bottom - GlowHeight - (ellipseHeight / 2f),
            ellipseWidth,
            ellipseHeight);

        if (ellipseRect.Width <= 0 || ellipseRect.Height <= 0)
        {
            return;
        }

        using var ellipsePath = new GraphicsPath();
        ellipsePath.AddEllipse(ellipseRect);
        using var glowBrush = new PathGradientBrush(ellipsePath)
        {
            CenterColor = Color.FromArgb(animate ? 72 : 56, highlightColor),
            CenterPoint = new PointF(ellipseRect.Left + (ellipseRect.Width / 2f), ellipseRect.Bottom - 1f)
        };
        glowBrush.SurroundColors = [Color.Transparent];
        g.FillPath(glowBrush, ellipsePath);
    }

    private static void DrawPathGlow(Graphics g,
        Rectangle bounds,
        GraphicsPath path,
        float animationPhase,
        bool animate,
        Color edgeColor1,
        Color edgeColor2,
        Color highlightColor)
    {
        using (var haloPen = new Pen(Color.FromArgb(animate ? 40 : 30, highlightColor), LineHeight + 6f)
               {
                   LineJoin = LineJoin.Round,
                   StartCap = LineCap.Round,
                   EndCap = LineCap.Round
               })
        {
            g.DrawPath(haloPen, path);
        }

        using var spectralBrush = CreateSpectralBrush(bounds, 0f, edgeColor1, edgeColor2, highlightColor);
        if (animate)
        {
            var center = new PointF(bounds.Left + (bounds.Width / 2f), bounds.Top + (bounds.Height / 2f));
            using var matrix = new Matrix();
            matrix.RotateAt(animationPhase * 360f, center);
            spectralBrush.Transform = matrix;
        }

        using var borderPen = new Pen(spectralBrush, LineHeight)
        {
            LineJoin = LineJoin.Round,
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };
        g.DrawPath(borderPen, path);
    }

    private static LinearGradientBrush CreateSpectralBrush(RectangleF brushRect,
        float phaseOffset,
        Color edgeColor1,
        Color edgeColor2,
        Color highlightColor)
    {
        var lineBrush = new LinearGradientBrush(
            new PointF(brushRect.Left - phaseOffset, brushRect.Top),
            new PointF(brushRect.Right - phaseOffset, brushRect.Top),
            edgeColor1,
            edgeColor2);

        var blend = new ColorBlend(5)
        {
            Colors =
            [
                Color.FromArgb(0, edgeColor1),
                Color.FromArgb(220, edgeColor1),
                Color.FromArgb(255, highlightColor),
                Color.FromArgb(220, edgeColor2),
                Color.FromArgb(0, edgeColor2)
            ],
            Positions = [0f, 0.32f, 0.5f, 0.68f, 1f]
        };
        lineBrush.InterpolationColors = blend;
        return lineBrush;
    }
}
