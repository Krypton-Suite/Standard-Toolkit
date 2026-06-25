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
/// Draws an optional glowing border for input controls.
/// </summary>
internal static class InputGlowBorderRenderer
{
    private const int GlowHeight = 10;
    private const float LineHeight = 2.5f;

    /// <summary>
    /// Draw a glowing border inside the specified bounds.
    /// </summary>
    public static void Draw(Graphics g,
        Rectangle bounds,
        float cornerRounding,
        float animationPhase,
        bool animate,
        InputGlowingBorderStyle style,
        Color edgeColor1,
        Color edgeColor2,
        Color highlightColor)
    {
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        if (style == InputGlowingBorderStyle.All)
        {
            DrawAll(g, bounds, cornerRounding, animationPhase, animate, edgeColor1, edgeColor2, highlightColor);
        }
        else
        {
            DrawBottom(g, bounds, cornerRounding, animationPhase, animate, edgeColor1, edgeColor2, highlightColor);
        }
    }

    private static void DrawBottom(Graphics g,
        Rectangle bounds,
        float cornerRounding,
        float animationPhase,
        bool animate,
        Color edgeColor1,
        Color edgeColor2,
        Color highlightColor)
    {
        using var antiAlias = new AntiAlias(g);

        float phaseOffset = animate ? animationPhase * bounds.Width : 0f;
        int inset = Math.Max(1, (int)Math.Ceiling(cornerRounding / 2f));
        int left = bounds.Left + inset;
        int right = bounds.Right - inset;
        int width = right - left;

        if (width <= 0)
        {
            return;
        }

        // Soft upward glow from the bottom edge.
        float ellipseWidth = width * 0.55f;
        float ellipseHeight = GlowHeight * 1.6f;
        var ellipseRect = new RectangleF(
            left + ((width - ellipseWidth) / 2f),
            bounds.Bottom - GlowHeight - (ellipseHeight / 2f),
            ellipseWidth,
            ellipseHeight);

        if (ellipseRect.Width > 0 && ellipseRect.Height > 0)
        {
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

        // Bright spectral line along the bottom edge.
        var lineRect = new RectangleF(left, bounds.Bottom - LineHeight, width, LineHeight);
        using var lineBrush = CreateSpectralBrush(lineRect, phaseOffset, edgeColor1, edgeColor2, highlightColor);
        g.FillRectangle(lineBrush, lineRect);
    }

    private static void DrawAll(Graphics g,
        Rectangle bounds,
        float cornerRounding,
        float animationPhase,
        bool animate,
        Color edgeColor1,
        Color edgeColor2,
        Color highlightColor)
    {
        using var antiAlias = new AntiAlias(g);

        int rounding = GetPathRounding(bounds, cornerRounding);
        var pathRect = bounds;
        pathRect.Inflate(-(int)Math.Ceiling(LineHeight / 2f), -(int)Math.Ceiling(LineHeight / 2f));

        if (pathRect.Width <= 0 || pathRect.Height <= 0)
        {
            return;
        }

        using GraphicsPath path = CommonHelper.RoundedRectanglePath(pathRect, rounding);

        // Soft outer halo following the full border path.
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

    private static int GetPathRounding(Rectangle bounds, float cornerRounding)
    {
        int rounding = (int)Math.Max(0f, cornerRounding);
        int maxRounding = Math.Min(bounds.Width, bounds.Height) / 2;
        return Math.Min(rounding, maxRounding);
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
