#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Paints the analog or segmented face of a <see cref="KryptonVuMeter"/>.
/// </summary>
internal sealed class ViewDrawVuMeter : ViewDrawPanel
{
    private readonly KryptonVuMeter _owner;
    private readonly List<double> _points = new List<double>();
    private Font? _labelFont;
    private Font? _captionFont;
    private Font? _sourceFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawVuMeter"/> class.
    /// </summary>
    /// <param name="owner">Owning meter.</param>
    /// <param name="paletteBack">Palette background drawn behind the face.</param>
    public ViewDrawVuMeter(KryptonVuMeter owner, IPaletteBack paletteBack)
        : base(paletteBack) =>
        _owner = owner;

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return new Size(240, 140);
    }

    /// <inheritdoc />
    public override void RenderAfter([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);
        if (context?.Graphics == null)
        {
            return;
        }

        Graphics graphics = context.Graphics;
        SmoothingMode smoothing = graphics.SmoothingMode;
        PixelOffsetMode offset = graphics.PixelOffsetMode;
        TextRenderingHint text = graphics.TextRenderingHint;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        try
        {
            Rectangle bounds = ClientRectangle;
            bounds.Inflate(-4, -4);
            if (bounds.Width < 8 || bounds.Height < 8)
            {
                return;
            }

            if (_owner.Values.Style == VuMeterStyle.Segments)
            {
                DrawSegments(graphics, bounds);
            }
            else
            {
                DrawAnalog(graphics, bounds);
            }
        }
        finally
        {
            graphics.SmoothingMode = smoothing;
            graphics.PixelOffsetMode = offset;
            graphics.TextRenderingHint = text;
        }
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _labelFont?.Dispose();
            _captionFont?.Dispose();
            _labelFont = null;
            _captionFont = null;
            _sourceFont = null;
        }

        base.Dispose(disposing);
    }

    private void DrawAnalog(Graphics graphics, Rectangle bounds)
    {
        VuMeterValues values = _owner.Values;
        PaletteVuMeterStates state = Colors();
        PaletteState paletteState = PaletteStateFor(_owner.Enabled);
        Color face = state.Face.GetElementColor1(paletteState);
        Color border = state.Face.GetElementColor2(paletteState);
        Color tick = state.Tick.GetElementColor1(paletteState);
        Color label = tick;
        Color overload = state.SegmentHigh.GetElementColor1(paletteState);
        Color needle = state.Needle.GetElementColor1(paletteState);
        Color peak = state.Peak.GetElementColor1(paletteState);

        using (GraphicsPath facePath = RoundedRectangle(bounds, 10))
        using (SolidBrush faceBrush = new SolidBrush(face))
        using (Pen borderPen = new Pen(border))
        {
            graphics.FillPath(faceBrush, facePath);
            graphics.DrawPath(borderPen, facePath);
        }

        float radius = Math.Min(bounds.Width * 0.46f, bounds.Height * 0.72f);
        if (radius < 12f)
        {
            return;
        }

        float centerX = bounds.Left + (bounds.Width / 2f);
        float centerY = bounds.Bottom - Math.Max(10f, radius * 0.18f);
        var arc = new RectangleF(centerX - radius, centerY - radius, radius * 2f, radius * 2f);
        using (Pen trackPen = new Pen(Color.FromArgb(140, tick), 3f))
        using (Pen overloadPen = new Pen(overload, 4f))
        {
            graphics.DrawArc(trackPen, arc, VuMeterScale.ArcStartDegrees, VuMeterScale.ArcSweepDegrees);
            DrawOverloadArc(graphics, overloadPen, arc, values.Minimum, values.Maximum);
        }

        VuMeterScale.CopyVisiblePoints(values.Minimum, values.Maximum, _points);
        using (Pen tickPen = new Pen(tick, 1.5f))
        using (SolidBrush labelBrush = new SolidBrush(label))
        {
            float lastLabelRight = float.MinValue;
            for (int i = 0; i < _points.Count; i++)
            {
                double mark = _points[i];
                float angle = Angle(mark, values.Minimum, values.Maximum);
                PointF outer = PointOnCircle(centerX, centerY, radius, angle);
                PointF inner = PointOnCircle(centerX, centerY, radius - 9f, angle);
                graphics.DrawLine(tickPen, inner, outer);
                if (!values.ShowScaleLabels)
                {
                    continue;
                }

                string text = VuMeterScale.FormatMark(mark);
                SizeF size = graphics.MeasureString(text, LabelFont);
                PointF at = PointOnCircle(centerX, centerY, radius + 11f, angle);
                float left = at.X - (size.Width / 2f);
                if (left <= lastLabelRight)
                {
                    continue;
                }

                graphics.DrawString(text, LabelFont, labelBrush, left, at.Y - (size.Height / 2f));
                lastLabelRight = left + size.Width + 1f;
            }
        }

        if (values.PeakHold)
        {
            DrawNeedle(graphics, centerX, centerY, radius * 0.72f, Angle(_owner.PeakLevel, values.Minimum, values.Maximum), peak, 1.4f);
        }

        DrawNeedle(graphics, centerX, centerY, radius * 0.78f, Angle(_owner.DisplayedLevel, values.Minimum, values.Maximum), needle, 2.2f);
        using (SolidBrush hubBrush = new SolidBrush(needle))
        {
            graphics.FillEllipse(hubBrush, centerX - 4f, centerY - 4f, 8f, 8f);
        }

        using (SolidBrush captionBrush = new SolidBrush(label))
        {
            SizeF caption = graphics.MeasureString("VU", CaptionFont);
            graphics.DrawString("VU", CaptionFont, captionBrush, centerX - (caption.Width / 2f), centerY - (radius * 0.42f));
        }
    }

    private void DrawSegments(Graphics graphics, Rectangle bounds)
    {
        VuMeterValues values = _owner.Values;
        PaletteVuMeterStates state = Colors();
        PaletteState paletteState = PaletteStateFor(_owner.Enabled);
        Color low = state.SegmentLow.GetElementColor1(paletteState);
        Color high = state.SegmentHigh.GetElementColor1(paletteState);
        Color off = state.SegmentOff.GetElementColor1(paletteState);
        Color peak = state.Peak.GetElementColor1(paletteState);
        Color label = state.Tick.GetElementColor1(paletteState);
        VuMeterScale.CopyVisiblePoints(values.Minimum, values.Maximum, _points);
        if (_points.Count < 2)
        {
            return;
        }

        bool vertical = values.Orientation == VisualOrientation.Top || values.Orientation == VisualOrientation.Bottom;
        bool reverse = values.Orientation == VisualOrientation.Right || values.Orientation == VisualOrientation.Top;
        Rectangle bar = bounds;
        if (values.ShowScaleLabels)
        {
            if (vertical)
            {
                bar.Width = Math.Max(8, bar.Width - 36);
            }
            else
            {
                bar.Height = Math.Max(8, bar.Height - 18);
            }
        }

        int count = _points.Count - 1;
        float gap = 3f;
        using (SolidBrush labelBrush = new SolidBrush(label))
        {
            for (int i = 0; i < count; i++)
            {
                double startValue = _points[i];
                double endValue = _points[i + 1];
                bool lit = _owner.DisplayedLevel >= startValue;
                bool peakLit = values.PeakHold && _owner.PeakLevel >= startValue && _owner.PeakLevel < endValue && _owner.PeakLevel > _owner.DisplayedLevel;
                Color fill = !lit ? off : endValue > 0d ? high : low;
                if (peakLit && !lit)
                {
                    fill = peak;
                }
                else if (peakLit)
                {
                    fill = peak;
                }

                RectangleF block = SegmentBounds(bar, count, i, gap, vertical, reverse);
                using (SolidBrush brush = new SolidBrush(fill))
                {
                    graphics.FillRectangle(brush, block);
                }

                if (!values.ShowScaleLabels || i != 0 && i != count - 1 && !IsLabelMark(startValue))
                {
                    continue;
                }

                string text = VuMeterScale.FormatMark(i == 0 ? startValue : endValue);
                if (i == count - 1)
                {
                    text = VuMeterScale.FormatMark(endValue);
                }
                else if (i == 0)
                {
                    text = VuMeterScale.FormatMark(startValue);
                }

                if (vertical)
                {
                    graphics.DrawString(text, LabelFont, labelBrush, bar.Right + 4f, block.Y);
                }
                else if (i == 0 || i == count - 1)
                {
                    graphics.DrawString(text, LabelFont, labelBrush, i == 0 ? block.X : block.Right - 16f, bar.Bottom + 1f);
                }
            }
        }
    }

    private static bool IsLabelMark(double value)
    {
        double rounded = Math.Round(value, 0, MidpointRounding.AwayFromZero);
        return Math.Abs(rounded - value) < 0.05d && (rounded <= -10d || rounded >= -3d);
    }

    private RectangleF SegmentBounds(Rectangle bar, int count, int index, float gap, bool vertical, bool reverse)
    {
        if (vertical)
        {
            float height = (bar.Height - (gap * (count - 1))) / count;
            // Bottom grows upward from the low end. Top grows downward from the low end.
            float y = _owner.Values.Orientation == VisualOrientation.Top
                ? bar.Top + (index * (height + gap))
                : bar.Bottom - ((index + 1) * height) - (index * gap);
            return new RectangleF(bar.Left, y, bar.Width, Math.Max(1f, height));
        }

        int slot = reverse ? count - 1 - index : index;
        float width = (bar.Width - (gap * (count - 1))) / count;
        float x = bar.Left + (slot * (width + gap));
        return new RectangleF(x, bar.Top, Math.Max(1f, width), bar.Height);
    }

    private static void DrawOverloadArc(Graphics graphics, Pen pen, RectangleF arc, double minimum, double maximum)
    {
        if (maximum <= 0d)
        {
            return;
        }

        float start = minimum >= 0d
            ? VuMeterScale.ArcStartDegrees
            : VuMeterScale.ArcStartDegrees + (float)(VuMeterScale.ViewPosition(0d, minimum, maximum) * VuMeterScale.ArcSweepDegrees);
        float sweep = (VuMeterScale.ArcStartDegrees + VuMeterScale.ArcSweepDegrees) - start;
        if (sweep > 0.5f)
        {
            graphics.DrawArc(pen, arc, start, sweep);
        }
    }

    private static void DrawNeedle(Graphics graphics, float centerX, float centerY, float length, float angle, Color color, float width)
    {
        PointF tip = PointOnCircle(centerX, centerY, length, angle);
        PointF tail = PointOnCircle(centerX, centerY, length * 0.12f, angle + 180f);
        using (Pen pen = new Pen(color, width))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            graphics.DrawLine(pen, tail, tip);
        }
    }

    private static float Angle(double value, double minimum, double maximum) =>
        VuMeterScale.ArcStartDegrees + (float)(VuMeterScale.ViewPosition(value, minimum, maximum) * VuMeterScale.ArcSweepDegrees);

    private static PointF PointOnCircle(float centerX, float centerY, float radius, float degrees)
    {
        double radians = degrees * Math.PI / 180d;
        return new PointF(
            centerX + (float)(Math.Cos(radians) * radius),
            centerY + (float)(Math.Sin(radians) * radius));
    }

    private static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
    {
        int diameter = radius * 2;
        var path = new GraphicsPath();
        if (bounds.Width < diameter || bounds.Height < diameter)
        {
            path.AddRectangle(bounds);
            return path;
        }

        path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }

    private PaletteVuMeterStates Colors() => _owner.Enabled ? _owner.StateNormal : _owner.StateDisabled;

    private static PaletteState PaletteStateFor(bool enabled) => enabled ? PaletteState.Normal : PaletteState.Disabled;

    private Font LabelFont => EnsureFonts().label;

    private Font CaptionFont => EnsureFonts().caption;

    private (Font label, Font caption) EnsureFonts()
    {
        Font source = _owner.Font;
        if (_labelFont != null && _captionFont != null && ReferenceEquals(_sourceFont, source))
        {
            return (_labelFont, _captionFont);
        }

        _labelFont?.Dispose();
        _captionFont?.Dispose();
        _sourceFont = source;
        float size = source.SizeInPoints;
        if (size > 8f)
        {
            size = 8f;
        }

        if (size < 6.5f)
        {
            size = 6.5f;
        }

        _labelFont = new Font(source.FontFamily, size, FontStyle.Regular, GraphicsUnit.Point);
        _captionFont = new Font(source.FontFamily, size + 1f, FontStyle.Bold, GraphicsUnit.Point);
        return (_labelFont, _captionFont);
    }
}
