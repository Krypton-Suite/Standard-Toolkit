#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shared GDI+ helpers for industrial knob and backplate rendering.
/// </summary>
internal static class KnobIndustrialDrawing
{
    #region Geometry
    public static void ComputeGeometry(Rectangle clientRect,
        KnobBackplateShape backplateShape,
        out Rectangle backplateRect,
        out Rectangle knobRect,
        out Point knobCenter)
    {
        var size = Math.Min(clientRect.Width, clientRect.Height);
        if (backplateShape == KnobBackplateShape.None)
        {
            var offset = (int)Math.Round(size * 0.1);
            var knobSize = (int)Math.Round(size * 0.8);
            backplateRect = Rectangle.Empty;
            knobRect = new Rectangle(
                clientRect.X + offset,
                clientRect.Y + offset,
                knobSize,
                knobSize);
        }
        else
        {
            var margin = Math.Max(2, (int)Math.Round(size * 0.04));
            backplateRect = new Rectangle(
                clientRect.X + margin,
                clientRect.Y + margin,
                size - margin * 2,
                size - margin * 2);

            var knobSize = (int)Math.Round(backplateRect.Width * 0.68);
            var knobOffset = (backplateRect.Width - knobSize) / 2;
            knobRect = new Rectangle(
                backplateRect.X + knobOffset,
                backplateRect.Y + knobOffset,
                knobSize,
                knobSize);
        }

        knobCenter = new Point(
            knobRect.X + knobRect.Width / 2,
            knobRect.Y + knobRect.Height / 2);
    }
    #endregion

    #region Backplate
    public static void DrawBackplate(Graphics g, KnobBackplateSettings settings, Rectangle backplateRect, Rectangle knobRect)
    {
        if (settings.Shape == KnobBackplateShape.None)
        {
            return;
        }

        var color1 = settings.Color1 == GlobalStaticVariables.EMPTY_COLOR ? Color.FromArgb(210, 210, 215) : settings.Color1;
        var color2 = settings.Color2 == GlobalStaticVariables.EMPTY_COLOR ? Color.FromArgb(150, 150, 158) : settings.Color2;
        var border = settings.BorderColor == GlobalStaticVariables.EMPTY_COLOR ? GetDarkColor(color1, 70) : settings.BorderColor;

        using var plateBrush = new LinearGradientBrush(backplateRect, color1, color2, LinearGradientMode.Vertical);

        switch (settings.Shape)
        {
            case KnobBackplateShape.Square:
                g.FillRectangle(plateBrush, backplateRect);
                using (var borderPen = new Pen(border))
                {
                    g.DrawRectangle(borderPen, backplateRect);
                }
                break;

            case KnobBackplateShape.RoundedSquare:
                var radius = Math.Max(6, (int)Math.Round(backplateRect.Width * 0.12));
                using (var path = CreateRoundedRectPath(backplateRect, radius))
                {
                    g.FillPath(plateBrush, path);
                    using var borderPen = new Pen(border);
                    g.DrawPath(borderPen, path);
                }
                break;

            case KnobBackplateShape.Circle:
                g.FillEllipse(plateBrush, backplateRect);
                using (var borderPen = new Pen(border))
                {
                    g.DrawEllipse(borderPen, backplateRect);
                }
                break;
        }

        if (settings.ShowInsetWell)
        {
            var wellInset = Math.Max(3, (int)Math.Round(knobRect.Width * 0.08));
            var wellRect = new Rectangle(
                knobRect.X - wellInset,
                knobRect.Y - wellInset,
                knobRect.Width + wellInset * 2,
                knobRect.Height + wellInset * 2);

            using var wellPen = new Pen(GetDarkColor(color2, 60), Math.Max(1f, wellInset * 0.45f));
            if (settings.Shape == KnobBackplateShape.Circle)
            {
                g.DrawEllipse(wellPen, wellRect);
            }
            else if (settings.Shape == KnobBackplateShape.RoundedSquare)
            {
                var wellRadius = Math.Max(8, (int)Math.Round(wellRect.Width * 0.12));
                using var wellPath = CreateRoundedRectPath(wellRect, wellRadius);
                g.DrawPath(wellPen, wellPath);
            }
            else
            {
                g.DrawRectangle(wellPen, wellRect);
            }
        }
    }

    public static void DrawKnobDropShadow(Graphics g, Rectangle knobRect)
    {
        var shadowOffset = Math.Max(2, (int)Math.Round(knobRect.Width * 0.04));
        var shadowRect = new Rectangle(
            knobRect.X + shadowOffset,
            knobRect.Y + shadowOffset,
            knobRect.Width,
            knobRect.Height);

        using var shadowBrush = new SolidBrush(Color.FromArgb(70, 0, 0, 0));
        g.FillEllipse(shadowBrush, shadowRect);
    }
    #endregion

    #region Knob face
    public static void DrawKnobFace(Graphics g,
        KnobStyle knobStyle,
        Rectangle knobRect,
        Point knobCenter,
        Color faceColor1,
        Color faceColor2,
        Color borderColor,
        Color? ringInnerColor)
    {
        if (faceColor2 == GlobalStaticVariables.EMPTY_COLOR)
        {
            faceColor2 = faceColor1;
        }

        switch (knobStyle)
        {
            case KnobStyle.Flat:
                using (var faceBrush = new SolidBrush(faceColor1))
                {
                    g.FillEllipse(faceBrush, knobRect);
                }
                break;

            case KnobStyle.Radial:
                DrawRadialFace(g, knobRect, knobCenter, faceColor1, faceColor2);
                break;

            case KnobStyle.Ring:
                using (var faceBrush = new LinearGradientBrush(knobRect, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(faceBrush, knobRect);
                }
                var innerSize = (int)Math.Round(knobRect.Width * 0.5);
                var offset = (knobRect.Width - innerSize) / 2;
                var innerRect = new Rectangle(knobRect.X + offset, knobRect.Y + offset, innerSize, innerSize);
                var innerColor = ringInnerColor ?? faceColor1;
                using (var innerBrush = new SolidBrush(innerColor))
                {
                    g.FillEllipse(innerBrush, innerRect);
                }
                using (var innerBorderPen = new Pen(GetDarkColor(borderColor, 30)))
                {
                    g.DrawEllipse(innerBorderPen, innerRect);
                }
                break;

            case KnobStyle.Bevel:
                DrawBevelFace(g, knobRect, faceColor1, faceColor2, borderColor);
                break;

            case KnobStyle.RoundedSquare:
                DrawRoundedSquareFace(g, knobRect, faceColor1, faceColor2, borderColor);
                break;

            case KnobStyle.Industrial:
                DrawIndustrialFace(g, knobRect, knobCenter, faceColor1, faceColor2, borderColor);
                break;

            default:
                using (var faceBrush = new LinearGradientBrush(knobRect, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(faceBrush, knobRect);
                }
                break;
        }

        using var borderPen = new Pen(borderColor);
        if (knobStyle == KnobStyle.RoundedSquare)
        {
            var radius = Math.Max(4, (int)Math.Round(Math.Min(knobRect.Width, knobRect.Height) * 0.15));
            using var path = CreateRoundedRectPath(knobRect, radius);
            g.DrawPath(borderPen, path);
        }
        else
        {
            g.DrawEllipse(borderPen, knobRect);
        }
    }

    private static void DrawRadialFace(Graphics g, Rectangle knobRect, Point knobCenter, Color faceColor1, Color faceColor2)
    {
        using var path = new GraphicsPath();
        path.AddEllipse(knobRect);
        using var brush = new PathGradientBrush(path)
        {
            CenterColor = GetLightColor(faceColor1, 30),
            SurroundColors = new[] { faceColor2 },
            CenterPoint = new PointF(knobCenter.X, knobCenter.Y)
        };
        g.FillPath(brush, path);
    }

    private static void DrawBevelFace(Graphics g, Rectangle knobRect, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using var faceBrush = new LinearGradientBrush(knobRect, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal);
        g.FillEllipse(faceBrush, knobRect);

        using var lightPen = new Pen(GetLightColor(borderColor, 55));
        using var darkPen = new Pen(GetDarkColor(borderColor, 55));
        for (var i = 0; i <= 2; i++)
        {
            var arcRect = new Rectangle(knobRect.X + i, knobRect.Y + i, knobRect.Width - i * 2, knobRect.Height - i * 2);
            g.DrawArc(lightPen, arcRect, -45, 180);
            g.DrawArc(darkPen, arcRect, 135, 180);
        }
    }

    private static void DrawRoundedSquareFace(Graphics g, Rectangle knobRect, Color faceColor1, Color faceColor2, Color borderColor)
    {
        var radius = Math.Max(4, (int)Math.Round(Math.Min(knobRect.Width, knobRect.Height) * 0.15));
        using var path = CreateRoundedRectPath(knobRect, radius);
        var bounds = Rectangle.Round(path.GetBounds());
        if (bounds.Width < 1)
        {
            bounds.Width = 1;
        }

        if (bounds.Height < 1)
        {
            bounds.Height = 1;
        }

        using var faceBrush = new LinearGradientBrush(bounds, faceColor1, faceColor2, LinearGradientMode.ForwardDiagonal);
        g.FillPath(faceBrush, path);
    }

    private static void DrawIndustrialFace(Graphics g, Rectangle knobRect, Point knobCenter, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using var path = new GraphicsPath();
        path.AddEllipse(knobRect);
        using var brush = new PathGradientBrush(path)
        {
            CenterColor = GetLightColor(faceColor1, 45),
            SurroundColors = new[] { faceColor2 == GlobalStaticVariables.EMPTY_COLOR ? GetDarkColor(faceColor1, 55) : faceColor2 },
            CenterPoint = new PointF(knobCenter.X, knobCenter.Y)
        };
        g.FillPath(brush, path);

        using var lightPen = new Pen(GetLightColor(borderColor, 40));
        using var darkPen = new Pen(GetDarkColor(borderColor, 50));
        for (var i = 0; i <= 2; i++)
        {
            var arcRect = new Rectangle(knobRect.X + i, knobRect.Y + i, knobRect.Width - i * 2, knobRect.Height - i * 2);
            g.DrawArc(lightPen, arcRect, -50, 160);
            g.DrawArc(darkPen, arcRect, 130, 160);
        }
    }
    #endregion

    #region Indicators
    public static void DrawGlowDot(Graphics g, RectangleF rect, Color fillColor)
    {
        var glowColor = Color.FromArgb(90, fillColor);
        var outer = new RectangleF(rect.X - rect.Width * 0.35f, rect.Y - rect.Height * 0.35f, rect.Width * 1.7f, rect.Height * 1.7f);
        using (var glowBrush = new SolidBrush(glowColor))
        {
            g.FillEllipse(glowBrush, outer);
        }

        using var fillBrush = new SolidBrush(fillColor);
        g.FillEllipse(fillBrush, rect);
        using var highlightBrush = new SolidBrush(Color.FromArgb(160, Color.White));
        var highlight = new RectangleF(rect.X + rect.Width * 0.22f, rect.Y + rect.Height * 0.18f, rect.Width * 0.35f, rect.Height * 0.35f);
        g.FillEllipse(highlightBrush, highlight);
    }

    public static void DrawIndustrialBar(Graphics g,
        Point knobCenter,
        Rectangle knobRect,
        float angleDegrees,
        Color stripeColor,
        Color barColor)
    {
        var state = g.Save();
        try
        {
            g.TranslateTransform(knobCenter.X, knobCenter.Y);
            g.RotateTransform(angleDegrees);

            var barLength = knobRect.Width * 0.42f;
            var barThickness = Math.Max(4f, knobRect.Width * 0.1f);
            var barRect = new RectangleF(-barLength, -barThickness / 2f, barLength * 2f, barThickness);

            var baseColor = barColor == GlobalStaticVariables.EMPTY_COLOR ? Color.FromArgb(40, 40, 40) : GetDarkColor(barColor, 30);
            using (var baseBrush = new LinearGradientBrush(barRect, GetLightColor(baseColor, 25), GetDarkColor(baseColor, 25), LinearGradientMode.Vertical))
            {
                g.FillRectangle(baseBrush, barRect);
            }

            var stripeHeight = Math.Max(2f, barThickness * 0.28f);
            var stripeRect = new RectangleF(-barLength * 0.75f, -stripeHeight / 2f, barLength * 1.5f, stripeHeight);
            using (var stripeBrush = new SolidBrush(stripeColor))
            {
                g.FillRectangle(stripeBrush, stripeRect);
            }

            using var borderPen = new Pen(GetDarkColor(baseColor, 40));
            g.DrawRectangle(borderPen, barRect.X, barRect.Y, barRect.Width, barRect.Height);
        }
        finally
        {
            g.Restore(state);
        }
    }
    #endregion

    #region Plate labels
    public static void DrawPlateLabels(Graphics g,
        IEnumerable<KnobPlateLabel> labels,
        Point plateCenter,
        float plateRadius,
        Font defaultFont)
    {
        foreach (var label in labels)
        {
            if (!label.Visible || string.IsNullOrEmpty(label.Text))
            {
                continue;
            }

            var font = label.Font ?? defaultFont;
            var angleRadians = label.Angle * Math.PI / 180.0;
            var radius = plateRadius * label.RadiusFactor;
            var x = plateCenter.X + radius * Math.Cos(angleRadians);
            var y = plateCenter.Y + radius * Math.Sin(angleRadians);

            var color = label.Color == GlobalStaticVariables.EMPTY_COLOR ? Color.Black : label.Color;
            using var brush = new SolidBrush(color);
            var size = g.MeasureString(label.Text, font);
            g.DrawString(label.Text, font, brush, (float)(x - size.Width / 2), (float)(y - size.Height / 2));
        }
    }
    #endregion

    #region Helpers
    public static GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
    {
        var path = new GraphicsPath();
        var diameter = radius * 2;
        if (diameter > rect.Width)
        {
            diameter = rect.Width;
        }

        if (diameter > rect.Height)
        {
            diameter = rect.Height;
        }

        var arc = new Rectangle(rect.Location, new Size(diameter, diameter));
        path.AddArc(arc, 180, 90);
        arc.X = rect.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = rect.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = rect.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }

    public static Color GetDarkColor(Color c, byte d)
    {
        var r = c.R > d ? (byte)(c.R - d) : (byte)0;
        var g = c.G > d ? (byte)(c.G - d) : (byte)0;
        var b = c.B > d ? (byte)(c.B - d) : (byte)0;
        return Color.FromArgb(r, g, b);
    }

    public static Color GetLightColor(Color c, byte d)
    {
        var r = c.R + d <= 255 ? (byte)(c.R + d) : (byte)255;
        var g = c.G + d <= 255 ? (byte)(c.G + d) : (byte)255;
        var b = c.B + d <= 255 ? (byte)(c.B + d) : (byte)255;
        return Color.FromArgb(r, g, b);
    }
    #endregion
}
