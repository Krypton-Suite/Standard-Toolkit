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
/// Paints the circular needle face of a <see cref="KryptonCircularGauge"/>.
/// </summary>
internal sealed class ViewDrawCircularGauge : ViewDrawPanel
{
    private readonly KryptonCircularGauge _owner;
    private Font? _labelFont;
    private Font? _captionFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawCircularGauge"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    /// <param name="paletteBack">Palette background drawn behind the face.</param>
    public ViewDrawCircularGauge(KryptonCircularGauge owner, IPaletteBack paletteBack)
        : base(paletteBack) =>
        _owner = owner;

    /// <summary>
    /// Gets the last computed dial rectangle used for hit-testing.
    /// </summary>
    public Rectangle DialBounds { get; private set; }

    /// <summary>
    /// Gets the last computed dial centre.
    /// </summary>
    public PointF DialCenter { get; private set; }

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return new Size(180, 180);
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
            if (bounds.Width < 16 || bounds.Height < 16)
            {
                return;
            }

            int size = Math.Min(bounds.Width, bounds.Height);
            DialBounds = new Rectangle(
                bounds.X + ((bounds.Width - size) / 2),
                bounds.Y + ((bounds.Height - size) / 2),
                size,
                size);
            DialCenter = new PointF(DialBounds.X + (size / 2f), DialBounds.Y + (size / 2f));

            CircularGaugeValues values = _owner.CircularValues;
            Color face = _owner.ResolveFaceFill();
            Color border = _owner.ResolveFaceBorder();
            Color tick = _owner.ResolveTick();
            Color needle = _owner.ResolveNeedle();
            Color cap = _owner.ResolveCap();
            Color peak = _owner.ResolvePeak();

            using (var faceBrush = new SolidBrush(face))
            using (var borderPen = new Pen(border, 2f))
            {
                graphics.FillEllipse(faceBrush, DialBounds);
                graphics.DrawEllipse(borderPen, DialBounds);
            }

            float radius = (size / 2f) - 8f;
            DrawRanges(graphics, DialCenter, radius - 6f, values);
            DrawTicks(graphics, DialCenter, radius, values, tick);
            if (values.ShowNeedle)
            {
                float angle = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, values.Normalize(_owner.DisplayedValue));
                DrawNeedle(graphics, DialCenter, radius - 14f, angle, needle);
            }

            if (values.ShowPeak)
            {
                float peakAngle = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, values.Normalize(_owner.PeakValue));
                PointF tip = GaugeGeometry.PointOnCircle(DialCenter, radius - 8f, peakAngle);
                using (var peakBrush = new SolidBrush(peak))
                {
                    graphics.FillEllipse(peakBrush, tip.X - 3f, tip.Y - 3f, 6f, 6f);
                }
            }

            float capRadius = Math.Max(4f, size * 0.05f);
            using (var capBrush = new SolidBrush(cap))
            {
                graphics.FillEllipse(capBrush, DialCenter.X - capRadius, DialCenter.Y - capRadius, capRadius * 2f, capRadius * 2f);
            }

            DrawCaption(graphics, DialBounds, values, tick);
        }
        finally
        {
            graphics.SmoothingMode = smoothing;
            graphics.PixelOffsetMode = offset;
            graphics.TextRenderingHint = text;
        }
    }

    private void DrawRanges(Graphics graphics, PointF center, float radius, CircularGaugeValues values)
    {
        if (values.Ranges.Count == 0 || radius < 8f)
        {
            return;
        }

        using (var path = new GraphicsPath())
        using (var pen = new Pen(Color.Black, Math.Max(6f, radius * 0.12f))
        {
            EndCap = LineCap.Flat,
            StartCap = LineCap.Flat
        })
        {
            RectangleF arc = new RectangleF(center.X - radius, center.Y - radius, radius * 2f, radius * 2f);
            foreach (GaugeRange range in values.Ranges)
            {
                double startN = values.Normalize(Math.Min(range.Start, range.End));
                double endN = values.Normalize(Math.Max(range.Start, range.End));
                if (endN <= startN)
                {
                    continue;
                }

                float start = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, startN);
                float sweep = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, endN) - start;
                pen.Color = _owner.ResolveRangeColor(range);
                path.Reset();
                path.AddArc(arc, start, sweep);
                graphics.DrawPath(pen, path);
            }
        }
    }

    private void DrawTicks(Graphics graphics, PointF center, float radius, CircularGaugeValues values, Color tickColor)
    {
        using (var tickPen = new Pen(tickColor, 1.5f))
        using (var minorPen = new Pen(tickColor, 1f))
        {
            EnsureFonts();
            int majors = values.MajorTickCount;
            for (int i = 0; i < majors; i++)
            {
                double n = i / (double)(majors - 1);
                float angle = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, n);
                PointF outer = GaugeGeometry.PointOnCircle(center, radius, angle);
                PointF inner = GaugeGeometry.PointOnCircle(center, radius - 10f, angle);
                graphics.DrawLine(tickPen, inner, outer);

                if (values.ShowScaleLabels && _labelFont != null)
                {
                    double value = values.Minimum + ((values.Maximum - values.Minimum) * n);
                    string label = value.ToString("0.##", CultureInfo.CurrentCulture);
                    PointF textPoint = GaugeGeometry.PointOnCircle(center, radius - 22f, angle);
                    SizeF size = graphics.MeasureString(label, _labelFont);
                    using (var brush = new SolidBrush(tickColor))
                    {
                        graphics.DrawString(label, _labelFont, brush, textPoint.X - (size.Width / 2f), textPoint.Y - (size.Height / 2f));
                    }
                }

                if (i < majors - 1 && values.MinorTickCount > 0)
                {
                    for (int m = 1; m <= values.MinorTickCount; m++)
                    {
                        double mn = (i + (m / (double)(values.MinorTickCount + 1))) / (majors - 1);
                        float minorAngle = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, mn);
                        PointF mOuter = GaugeGeometry.PointOnCircle(center, radius, minorAngle);
                        PointF mInner = GaugeGeometry.PointOnCircle(center, radius - 6f, minorAngle);
                        graphics.DrawLine(minorPen, mInner, mOuter);
                    }
                }
            }
        }
    }

    private static void DrawNeedle(Graphics graphics, PointF center, float length, float angle, Color color)
    {
        PointF tip = GaugeGeometry.PointOnCircle(center, length, angle);
        PointF back = GaugeGeometry.PointOnCircle(center, length * 0.15f, angle + 180f);
        using (var pen = new Pen(color, 2.5f)
        {
            EndCap = LineCap.Round,
            StartCap = LineCap.Round
        })
        {
            graphics.DrawLine(pen, back, tip);
        }
    }

    private void DrawCaption(Graphics graphics, Rectangle dial, CircularGaugeValues values, Color color)
    {
        EnsureFonts();
        if (_captionFont == null)
        {
            return;
        }

        using (var brush = new SolidBrush(color))
        {
            if (!string.IsNullOrEmpty(values.Caption))
            {
                SizeF size = graphics.MeasureString(values.Caption, _captionFont);
                graphics.DrawString(values.Caption, _captionFont, brush,
                    DialCenter.X - (size.Width / 2f),
                    DialCenter.Y + (dial.Height * 0.18f));
            }

            if (!string.IsNullOrEmpty(values.Units) && _labelFont != null)
            {
                SizeF size = graphics.MeasureString(values.Units, _labelFont);
                graphics.DrawString(values.Units, _labelFont, brush,
                    DialCenter.X - (size.Width / 2f),
                    DialCenter.Y + (dial.Height * 0.28f));
            }
        }
    }

    private void EnsureFonts()
    {
        Font baseFont = _owner.Font ?? Control.DefaultFont;
        if (_labelFont == null || !_labelFont.FontFamily.Equals(baseFont.FontFamily) || Math.Abs(_labelFont.Size - (baseFont.Size * 0.75f)) > 0.1f)
        {
            _labelFont?.Dispose();
            _labelFont = new Font(baseFont.FontFamily, Math.Max(6f, baseFont.Size * 0.75f), baseFont.Style);
        }

        if (_captionFont == null || !_captionFont.FontFamily.Equals(baseFont.FontFamily) || Math.Abs(_captionFont.Size - baseFont.Size) > 0.1f)
        {
            _captionFont?.Dispose();
            _captionFont = new Font(baseFont.FontFamily, Math.Max(7f, baseFont.Size), FontStyle.Bold);
        }
    }
}
