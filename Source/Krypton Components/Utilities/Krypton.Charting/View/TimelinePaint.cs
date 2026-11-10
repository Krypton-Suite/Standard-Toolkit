#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Shared palette drawing for timeline boxes, connectors, and labels.
/// </summary>
internal static class TimelinePaint
{
    internal const int IconSize = 20;
    internal const int Pad = 8;
    internal const int Gap = 6;

    internal static PaletteState ResolveState(bool enabled, bool selected, bool hot)
    {
        if (!enabled)
        {
            return PaletteState.Disabled;
        }

        if (selected)
        {
            return PaletteState.Pressed;
        }

        return hot ? PaletteState.Tracking : PaletteState.Normal;
    }

    internal static Font FontOr(PaletteContent content, PaletteState state, Font fallback)
    {
        Font? font = content.GetContentShortTextFont(state);
        return font ?? fallback;
    }

    internal static Color TextColor(PaletteContent content, PaletteState state, Color fallback)
    {
        Color color = content.GetContentShortTextColor1(state);
        return color.IsEmpty ? fallback : color;
    }

    internal static void DrawBox(RenderContext context,
        Rectangle rect,
        IPaletteBack back,
        IPaletteBorder? border,
        PaletteState state)
    {
        if (rect.Width <= 1 || rect.Height <= 1 || context.Renderer == null)
        {
            return;
        }

        using (GraphicsPath path = Rounded(rect, 4))
        {
            IDisposable? memento = null;
            try
            {
                if (back.GetBackDraw(state) == InheritBool.True)
                {
                    memento = context.Renderer.RenderStandardBack.DrawBack(context, rect, path, back, VisualOrientation.Top, state, null);
                }

                if (border != null && border.GetBorderDraw(state) == InheritBool.True)
                {
                    context.Renderer.RenderStandardBorder.DrawBorder(context, rect, border, VisualOrientation.Top, state);
                }
            }
            finally
            {
                memento?.Dispose();
            }
        }
    }

    internal static void DrawLabel(Graphics graphics, string? text, Font font, Color color, Rectangle bounds, bool center)
    {
        if (graphics == null || string.IsNullOrEmpty(text) || bounds.Width <= 1 || bounds.Height <= 1)
        {
            return;
        }

        TextFormatFlags flags = TextFormatFlags.EndEllipsis |
                                TextFormatFlags.VerticalCenter |
                                TextFormatFlags.NoPrefix |
                                TextFormatFlags.NoPadding |
                                (center ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Left);
        TextRenderer.DrawText(graphics, text, font, bounds, color, flags);
    }

    internal static Size Measure(string? text, Font font)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new Size(0, font.Height);
        }

        Size size = TextRenderer.MeasureText(text, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding);
        return new Size(Math.Max(size.Width, 1), Math.Max(size.Height, font.Height));
    }

    internal static void DrawIcon(Graphics graphics, Rectangle bounds, Image? image, Color fill, Color border)
    {
        if (bounds.Width <= 1 || bounds.Height <= 1)
        {
            return;
        }

        SmoothingMode previous = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        if (image != null)
        {
            graphics.DrawImage(image, bounds);
        }
        else
        {
            using (var brush = new SolidBrush(fill))
            using (var pen = new Pen(border, 2f))
            {
                graphics.FillEllipse(brush, bounds);
                graphics.DrawEllipse(pen, bounds);
            }
        }

        graphics.SmoothingMode = previous;
    }

    /// <summary>
    /// Shape used to clip a <see cref="TimelineItemStyle"/> overlay.
    /// </summary>
    internal enum TimelineItemShape
    {
        /// <summary>Rounded rectangle.</summary>
        Box,

        /// <summary>Milestone diamond.</summary>
        Diamond,

        /// <summary>Step icon.</summary>
        Ellipse
    }

    internal static void DrawItemStyleFill(Graphics graphics, Rectangle bounds, TimelineItemStyle style, TimelineItemShape shape, Color ink, TimelineItemStyleValues styles)
    {
        if (graphics == null || styles == null || style == TimelineItemStyle.Normal || bounds.Width <= 2 || bounds.Height <= 2)
        {
            return;
        }

        Resolve(style, styles, ink, out Color stripe, out Color hatch, out Color mark);
        using (GraphicsPath path = ShapePath(bounds, shape))
        {
            GraphicsState saved = graphics.Save();
            graphics.SetClip(path, CombineMode.Intersect);
            graphics.SmoothingMode = SmoothingMode.None;
            if (!stripe.IsEmpty && shape == TimelineItemShape.Box)
            {
                var band = new Rectangle(bounds.Left, bounds.Top, Math.Min(5, bounds.Width), bounds.Height);
                using (var brush = new SolidBrush(stripe))
                {
                    graphics.FillRectangle(brush, band);
                }
            }

            if (!hatch.IsEmpty)
            {
                DrawHatch(graphics, bounds, hatch, style != TimelineItemStyle.Delayed && style != TimelineItemStyle.Cancelled, style == TimelineItemStyle.Cancelled);
            }

            graphics.Restore(saved);
            bool outline = style == TimelineItemStyle.Delayed || (style == TimelineItemStyle.Hatched && !styles.Hatched.Mark.IsEmpty);
            if (outline && !mark.IsEmpty)
            {
                using (var pen = new Pen(mark, 1.5f))
                {
                    pen.DashStyle = style == TimelineItemStyle.Delayed ? DashStyle.Dash : DashStyle.Solid;
                    graphics.DrawPath(pen, path);
                }
            }
        }
    }

    internal static void DrawItemStyleMark(Graphics graphics, Rectangle bounds, TimelineItemStyle style, TimelineItemShape shape, Color ink, TimelineItemStyleValues styles)
    {
        if (graphics == null || styles == null || bounds.Width <= 4 || bounds.Height <= 4)
        {
            return;
        }

        if (style != TimelineItemStyle.Completed && style != TimelineItemStyle.Cancelled)
        {
            return;
        }

        Resolve(style, styles, ink, out _, out _, out Color mark);
        if (mark.IsEmpty)
        {
            return;
        }

        SmoothingMode previous = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(mark, 2f))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            if (style == TimelineItemStyle.Cancelled)
            {
                int y = bounds.Top + (bounds.Height / 2);
                graphics.DrawLine(pen, bounds.Left + 3, y, bounds.Right - 3, y);
            }
            else
            {
                int arm = Math.Max(3, Math.Min(bounds.Width, bounds.Height) / 3);
                int cx = shape == TimelineItemShape.Box
                    ? bounds.Right - 4 - arm
                    : bounds.Left + (bounds.Width / 2);
                int cy = bounds.Top + (bounds.Height / 2);
                graphics.DrawLines(pen, new[]
                {
                    new Point(cx - arm, cy),
                    new Point(cx - (arm / 3), cy + (arm / 2)),
                    new Point(cx + arm, cy - (arm / 2))
                });
            }
        }

        graphics.SmoothingMode = previous;
    }

    internal static void DrawDiamond(Graphics graphics, Rectangle bounds, Color fill, Color border)
    {
        if (bounds.Width <= 1 || bounds.Height <= 1)
        {
            return;
        }

        Point midTop = new Point(bounds.Left + (bounds.Width / 2), bounds.Top);
        Point midRight = new Point(bounds.Right, bounds.Top + (bounds.Height / 2));
        Point midBottom = new Point(bounds.Left + (bounds.Width / 2), bounds.Bottom);
        Point midLeft = new Point(bounds.Left, bounds.Top + (bounds.Height / 2));
        Point[] points = { midTop, midRight, midBottom, midLeft };
        SmoothingMode previous = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(fill))
        using (var pen = new Pen(border, 1.5f))
        {
            graphics.FillPolygon(brush, points);
            graphics.DrawPolygon(pen, points);
        }

        graphics.SmoothingMode = previous;
    }

    private static void Resolve(TimelineItemStyle style, TimelineItemStyleValues styles, Color ink, out Color stripe, out Color hatch, out Color mark)
    {
        TimelineItemStyleColors chosen = styles.For(style);
        stripe = chosen.Stripe.IsEmpty ? BuiltinStripe(style) : chosen.Stripe;
        hatch = chosen.Hatch.IsEmpty ? BuiltinHatch(style, stripe, ink) : chosen.Hatch;
        mark = chosen.Mark.IsEmpty ? BuiltinMark(style, stripe, ink) : chosen.Mark;
    }

    private static Color BuiltinStripe(TimelineItemStyle style)
    {
        switch (style)
        {
            case TimelineItemStyle.Completed:
                return Color.FromArgb(46, 139, 87);
            case TimelineItemStyle.Delayed:
                return Color.FromArgb(184, 115, 51);
            case TimelineItemStyle.Cancelled:
                return Color.FromArgb(128, 128, 128);
            default:
                return Color.Empty;
        }
    }

    private static Color BuiltinHatch(TimelineItemStyle style, Color stripe, Color ink)
    {
        switch (style)
        {
            case TimelineItemStyle.Hatched:
                return Color.FromArgb(160, ink);
            case TimelineItemStyle.Delayed:
            case TimelineItemStyle.Cancelled:
                return Color.FromArgb(180, stripe.IsEmpty ? ink : stripe);
            default:
                return Color.Empty;
        }
    }

    private static Color BuiltinMark(TimelineItemStyle style, Color stripe, Color ink)
    {
        switch (style)
        {
            case TimelineItemStyle.Completed:
                return ControlPaint.Dark(stripe.IsEmpty ? Color.FromArgb(46, 139, 87) : stripe);
            case TimelineItemStyle.Delayed:
                return stripe;
            case TimelineItemStyle.Cancelled:
                return ink;
            default:
                return Color.Empty;
        }
    }

    private static void DrawHatch(Graphics graphics, Rectangle bounds, Color color, bool forward, bool horizontal)
    {
        using (var pen = new Pen(color, 1f))
        {
            if (horizontal)
            {
                for (int y = bounds.Top + 3; y < bounds.Bottom; y += 4)
                {
                    graphics.DrawLine(pen, bounds.Left, y, bounds.Right, y);
                }

                return;
            }

            int step = 6;
            if (forward)
            {
                for (int i = -bounds.Height; i < bounds.Width; i += step)
                {
                    graphics.DrawLine(pen, bounds.Left + i, bounds.Bottom, bounds.Left + i + bounds.Height, bounds.Top);
                }
            }
            else
            {
                for (int i = 0; i < bounds.Width + bounds.Height; i += step)
                {
                    graphics.DrawLine(pen, bounds.Left + i, bounds.Top, bounds.Left + i - bounds.Height, bounds.Bottom);
                }
            }
        }
    }

    private static GraphicsPath ShapePath(Rectangle bounds, TimelineItemShape shape)
    {
        switch (shape)
        {
            case TimelineItemShape.Ellipse:
                var ellipse = new GraphicsPath();
                ellipse.AddEllipse(bounds);
                return ellipse;
            case TimelineItemShape.Diamond:
                var diamond = new GraphicsPath();
                diamond.AddPolygon(new[]
                {
                    new Point(bounds.Left + (bounds.Width / 2), bounds.Top),
                    new Point(bounds.Right, bounds.Top + (bounds.Height / 2)),
                    new Point(bounds.Left + (bounds.Width / 2), bounds.Bottom),
                    new Point(bounds.Left, bounds.Top + (bounds.Height / 2))
                });
                return diamond;
            default:
                return Rounded(bounds, 4);
        }
    }

    private static GraphicsPath Rounded(Rectangle rect, int radius)
    {
        var path = new GraphicsPath();
        int limit = Math.Min(rect.Width, rect.Height) / 2;
        int r = Math.Max(1, Math.Min(radius, limit));
        int diameter = r * 2;
        if (rect.Width <= diameter || rect.Height <= diameter)
        {
            path.AddRectangle(rect);
            return path;
        }

        path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }
}
