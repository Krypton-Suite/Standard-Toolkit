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
/// Paints the square panel-meter face of a <see cref="KryptonAmpereMeterGauge"/>.
/// </summary>
internal sealed class ViewDrawAmpereMeterGauge : ViewDrawPanel
{
    private readonly KryptonAmpereMeterGauge _owner;
    private Font? _labelFont;
    private Font? _captionFont;
    private Font? _zeroFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawAmpereMeterGauge"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    /// <param name="paletteBack">Palette background drawn behind the face.</param>
    public ViewDrawAmpereMeterGauge(KryptonAmpereMeterGauge owner, IPaletteBack paletteBack)
        : base(paletteBack) =>
        _owner = owner;

    /// <summary>
    /// Gets the last computed dial rectangle used for hit-testing layout.
    /// </summary>
    public Rectangle DialBounds { get; private set; }

    /// <summary>
    /// Gets the needle pivot (near the bottom of the dial).
    /// </summary>
    public PointF Pivot { get; private set; }

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return new Size(160, 160);
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
            bounds.Inflate(-3, -3);
            if (bounds.Width < 24 || bounds.Height < 24)
            {
                return;
            }

            int size = Math.Min(bounds.Width, bounds.Height);
            DialBounds = new Rectangle(
                bounds.X + ((bounds.Width - size) / 2),
                bounds.Y + ((bounds.Height - size) / 2),
                size,
                size);

            AmpereMeterGaugeValues values = _owner.AmpereMeterValues;
            Color face = _owner.ResolveFaceFill();
            Color border = _owner.ResolveFaceBorder();
            Color tick = _owner.ResolveTick();
            Color needle = _owner.ResolveNeedle();
            Color cap = _owner.ResolveCap();

            // Square panel bezel.
            using (var faceBrush = new SolidBrush(face))
            using (var borderPen = new Pen(border, 2f))
            using (var path = RoundedRect(DialBounds, Math.Max(4f, size * 0.04f)))
            {
                graphics.FillPath(faceBrush, path);
                graphics.DrawPath(borderPen, path);
            }

            // Pivot sits near the bottom center so the short upper arc reads as a panel meter.
            float pivotY = DialBounds.Bottom - (size * 0.18f);
            Pivot = new PointF(DialBounds.X + (size / 2f), pivotY);
            float radius = size * 0.72f;

            DrawRanges(graphics, Pivot, radius - 8f, values);
            DrawTicks(graphics, Pivot, radius, values, tick);

            float angle = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, values.Normalize(_owner.DisplayedValue));
            DrawNeedle(graphics, Pivot, radius - 18f, angle, needle);

            float capRadius = Math.Max(5f, size * 0.055f);
            using (var capBrush = new SolidBrush(cap))
            using (var capBorder = new Pen(border, 1f))
            {
                graphics.FillEllipse(capBrush, Pivot.X - capRadius, Pivot.Y - capRadius, capRadius * 2f, capRadius * 2f);
                graphics.DrawEllipse(capBorder, Pivot.X - capRadius, Pivot.Y - capRadius, capRadius * 2f, capRadius * 2f);
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

    private void DrawRanges(Graphics graphics, PointF center, float radius, AmpereMeterGaugeValues values)
    {
        if (values.Ranges.Count == 0 || radius < 8f)
        {
            return;
        }

        using (var path = new GraphicsPath())
        using (var pen = new Pen(Color.Black, Math.Max(5f, radius * 0.1f))
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

    private void DrawTicks(Graphics graphics, PointF center, float radius, AmpereMeterGaugeValues values, Color tickColor)
    {
        bool bipolar = values.Minimum < 0d;
        double zeroNormalized = bipolar ? values.Normalize(0d) : -1d;

        using (var tickPen = new Pen(tickColor, 1.5f))
        using (var zeroPen = new Pen(tickColor, 2.5f))
        using (var minorPen = new Pen(tickColor, 1f))
        {
            EnsureFonts();
            int majors = values.MajorTickCount;
            for (int i = 0; i < majors; i++)
            {
                double n = i / (double)(majors - 1);
                float angle = GaugeGeometry.ValueToAngle(values.StartAngle, values.SweepAngle, n);
                bool isZero = bipolar && values.ShowCenterZeroMark && Math.Abs(n - zeroNormalized) < 0.02d;
                float tickLen = isZero ? 14f : 10f;
                Pen majorPen = isZero ? zeroPen : tickPen;
                PointF outer = GaugeGeometry.PointOnCircle(center, radius, angle);
                PointF inner = GaugeGeometry.PointOnCircle(center, radius - tickLen, angle);
                graphics.DrawLine(majorPen, inner, outer);

                if (values.ShowScaleLabels && _labelFont != null)
                {
                    double value = values.Minimum + ((values.Maximum - values.Minimum) * n);
                    string label = value.ToString("0.##", CultureInfo.CurrentCulture);
                    Font font = isZero && _zeroFont != null ? _zeroFont : _labelFont;
                    PointF textPoint = GaugeGeometry.PointOnCircle(center, radius - 24f, angle);
                    SizeF size = graphics.MeasureString(label, font);
                    using (var brush = new SolidBrush(tickColor))
                    {
                        graphics.DrawString(label, font, brush, textPoint.X - (size.Width / 2f), textPoint.Y - (size.Height / 2f));
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
        PointF back = GaugeGeometry.PointOnCircle(center, length * 0.12f, angle + 180f);
        using (var pen = new Pen(color, 2.25f)
        {
            EndCap = LineCap.Round,
            StartCap = LineCap.Round
        })
        {
            graphics.DrawLine(pen, back, tip);
        }
    }

    private void DrawCaption(Graphics graphics, Rectangle dial, AmpereMeterGaugeValues values, Color color)
    {
        EnsureFonts();
        if (_captionFont == null)
        {
            return;
        }

        float baseY = dial.Bottom - (dial.Height * 0.12f);
        using (var brush = new SolidBrush(color))
        {
            if (!string.IsNullOrEmpty(values.Caption))
            {
                SizeF size = graphics.MeasureString(values.Caption, _captionFont);
                graphics.DrawString(values.Caption, _captionFont, brush,
                    Pivot.X - (size.Width / 2f),
                    baseY - size.Height);
            }

            if (!string.IsNullOrEmpty(values.Units) && _labelFont != null)
            {
                SizeF size = graphics.MeasureString(values.Units, _labelFont);
                float y = string.IsNullOrEmpty(values.Caption) ? baseY - size.Height : baseY;
                graphics.DrawString(values.Units, _labelFont, brush,
                    Pivot.X - (size.Width / 2f),
                    y);
            }
        }
    }

    private static GraphicsPath RoundedRect(Rectangle bounds, float radius)
    {
        float diameter = radius * 2f;
        var path = new GraphicsPath();
        var arc = new RectangleF(bounds.X, bounds.Y, diameter, diameter);
        path.AddArc(arc, 180f, 90f);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270f, 90f);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0f, 90f);
        arc.X = bounds.X;
        path.AddArc(arc, 90f, 90f);
        path.CloseFigure();
        return path;
    }

    private void EnsureFonts()
    {
        Font baseFont = _owner.Font ?? Control.DefaultFont;
        if (_labelFont == null || !_labelFont.FontFamily.Equals(baseFont.FontFamily) || Math.Abs(_labelFont.Size - (baseFont.Size * 0.7f)) > 0.1f)
        {
            _labelFont?.Dispose();
            _labelFont = new Font(baseFont.FontFamily, Math.Max(6f, baseFont.Size * 0.7f), baseFont.Style);
        }

        if (_zeroFont == null || !_zeroFont.FontFamily.Equals(baseFont.FontFamily) || Math.Abs(_zeroFont.Size - (baseFont.Size * 0.75f)) > 0.1f)
        {
            _zeroFont?.Dispose();
            _zeroFont = new Font(baseFont.FontFamily, Math.Max(6.5f, baseFont.Size * 0.75f), FontStyle.Bold);
        }

        if (_captionFont == null || !_captionFont.FontFamily.Equals(baseFont.FontFamily) || Math.Abs(_captionFont.Size - baseFont.Size) > 0.1f)
        {
            _captionFont?.Dispose();
            _captionFont = new Font(baseFont.FontFamily, Math.Max(7f, baseFont.Size), FontStyle.Bold);
        }
    }
}
