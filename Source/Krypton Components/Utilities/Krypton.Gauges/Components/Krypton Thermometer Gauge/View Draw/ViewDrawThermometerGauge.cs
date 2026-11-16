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
/// Paints the bulb-and-tube face of a <see cref="KryptonThermometerGauge"/>.
/// </summary>
internal sealed class ViewDrawThermometerGauge : ViewDrawPanel
{
    private readonly KryptonThermometerGauge _owner;
    private Font? _labelFont;
    private Font? _captionFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawThermometerGauge"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    /// <param name="paletteBack">Palette background drawn behind the face.</param>
    public ViewDrawThermometerGauge(KryptonThermometerGauge owner, IPaletteBack paletteBack)
        : base(paletteBack) =>
        _owner = owner;

    /// <summary>
    /// Gets the last computed tube rectangle used for hit-testing.
    /// </summary>
    public Rectangle TubeBounds { get; private set; }

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return _owner.ThermometerValues.Orientation == Orientation.Vertical
            ? new Size(64, 220)
            : new Size(220, 64);
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
        TextRenderingHint text = graphics.TextRenderingHint;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        try
        {
            Rectangle bounds = ClientRectangle;
            bounds.Inflate(-4, -4);
            if (bounds.Width < 8 || bounds.Height < 8)
            {
                return;
            }

            ThermometerGaugeValues values = _owner.ThermometerValues;
            bool vertical = values.Orientation == Orientation.Vertical;
            LayoutGeometry(bounds, values, vertical, out Rectangle tube, out RectangleF bulb);
            TubeBounds = tube;

            Color face = _owner.ResolveFaceFill();
            Color border = _owner.ResolveFaceBorder();
            Color tick = _owner.ResolveTick();
            Color fill = _owner.ResolveFill();

            using (var faceBrush = new SolidBrush(face))
            using (var borderPen = new Pen(border, 1.5f))
            {
                FillRoundedTube(graphics, tube, vertical, faceBrush);
                if (values.ShowBulb && bulb.Width > 2f)
                {
                    graphics.FillEllipse(faceBrush, bulb);
                }

                DrawRoundedTubeOutline(graphics, tube, vertical, borderPen);
                if (values.ShowBulb && bulb.Width > 2f)
                {
                    graphics.DrawEllipse(borderPen, bulb);
                }
            }

            DrawRanges(graphics, tube, values, vertical);
            DrawLiquid(graphics, tube, bulb, values, vertical, fill);
            DrawTicks(graphics, tube, values, vertical, tick);
            DrawCaption(graphics, bounds, values, tick);
        }
        finally
        {
            graphics.SmoothingMode = smoothing;
            graphics.TextRenderingHint = text;
        }
    }

    private static void LayoutGeometry(Rectangle bounds, ThermometerGaugeValues values, bool vertical,
        out Rectangle tube, out RectangleF bulb)
    {
        if (vertical)
        {
            int tubeWidth = Math.Max(8, Math.Min(18, bounds.Width / 4));
            int bulbDiameter = values.ShowBulb
                ? Math.Max(tubeWidth + 4, (int)Math.Round(tubeWidth * 2.2f * values.BulbScale))
                : 0;
            int labelPad = values.ShowScaleLabels ? 28 : 8;
            int tubeLeft = bounds.X + Math.Max(4, (bounds.Width - tubeWidth - labelPad) / 2);
            int tubeTop = bounds.Y + 8;
            int tubeBottom = bounds.Bottom - (values.ShowBulb ? bulbDiameter - (tubeWidth / 2) : 8);
            tube = new Rectangle(tubeLeft, tubeTop, tubeWidth, Math.Max(16, tubeBottom - tubeTop));
            float bulbX = tube.X + ((tube.Width - bulbDiameter) / 2f);
            float bulbY = tube.Bottom - (tubeWidth / 2f) - 2f;
            bulb = values.ShowBulb
                ? new RectangleF(bulbX, bulbY, bulbDiameter, bulbDiameter)
                : RectangleF.Empty;
            return;
        }

        int tubeHeight = Math.Max(8, Math.Min(18, bounds.Height / 4));
        int hBulb = values.ShowBulb
            ? Math.Max(tubeHeight + 4, (int)Math.Round(tubeHeight * 2.2f * values.BulbScale))
            : 0;
        int vPad = values.ShowScaleLabels ? 20 : 8;
        int tubeTopH = bounds.Y + Math.Max(4, (bounds.Height - tubeHeight - vPad) / 2);
        int tubeLeftH = bounds.X + (values.ShowBulb ? hBulb - (tubeHeight / 2) : 8);
        tube = new Rectangle(tubeLeftH, tubeTopH, Math.Max(16, bounds.Right - 8 - tubeLeftH), tubeHeight);
        float bulbYH = tube.Y + ((tube.Height - hBulb) / 2f);
        float bulbXH = tube.Left - (hBulb / 2f) + (tubeHeight / 2f) - 2f;
        bulb = values.ShowBulb
            ? new RectangleF(bulbXH, bulbYH, hBulb, hBulb)
            : RectangleF.Empty;
    }

    private static void FillRoundedTube(Graphics graphics, Rectangle tube, bool vertical, Brush brush)
    {
        using (var path = CreateTubePath(tube, vertical))
        {
            graphics.FillPath(brush, path);
        }
    }

    private static void DrawRoundedTubeOutline(Graphics graphics, Rectangle tube, bool vertical, Pen pen)
    {
        using (var path = CreateTubePath(tube, vertical))
        {
            graphics.DrawPath(pen, path);
        }
    }

    private static GraphicsPath CreateTubePath(Rectangle tube, bool vertical)
    {
        var path = new GraphicsPath();
        if (vertical)
        {
            int radius = tube.Width / 2;
            path.AddArc(tube.X, tube.Y, tube.Width, tube.Width, 180, 180);
            path.AddLine(tube.Right, tube.Y + radius, tube.Right, tube.Bottom - radius);
            path.AddArc(tube.X, tube.Bottom - tube.Width, tube.Width, tube.Width, 0, 180);
            path.CloseFigure();
        }
        else
        {
            int radius = tube.Height / 2;
            path.AddArc(tube.X, tube.Y, tube.Height, tube.Height, 90, 180);
            path.AddLine(tube.X + radius, tube.Y, tube.Right - radius, tube.Y);
            path.AddArc(tube.Right - tube.Height, tube.Y, tube.Height, tube.Height, 270, 180);
            path.CloseFigure();
        }

        return path;
    }

    private void DrawRanges(Graphics graphics, Rectangle tube, ThermometerGaugeValues values, bool vertical)
    {
        foreach (GaugeRange range in values.Ranges)
        {
            double startN = values.Normalize(Math.Min(range.Start, range.End));
            double endN = values.Normalize(Math.Max(range.Start, range.End));
            if (endN <= startN)
            {
                continue;
            }

            Rectangle band = BandRectangle(tube, startN, endN, vertical);
            using (var brush = new SolidBrush(Color.FromArgb(110, _owner.ResolveRangeColor(range))))
            {
                graphics.FillRectangle(brush, band);
            }
        }
    }

    private void DrawLiquid(Graphics graphics, Rectangle tube, RectangleF bulb, ThermometerGaugeValues values,
        bool vertical, Color fillColor)
    {
        double n = values.Normalize(_owner.DisplayedValue);
        using (var brush = new SolidBrush(fillColor))
        {
            if (values.ShowBulb && bulb.Width > 2f)
            {
                graphics.FillEllipse(brush, bulb);
            }

            if (n <= 0d)
            {
                return;
            }

            Rectangle fill = BandRectangle(tube, 0d, n, vertical);
            if (vertical)
            {
                // Keep liquid continuous into the bulb by extending slightly past the tube bottom.
                fill = Rectangle.FromLTRB(fill.Left, fill.Top, fill.Right, Math.Max(fill.Bottom, tube.Bottom));
            }
            else
            {
                fill = Rectangle.FromLTRB(Math.Min(fill.Left, tube.Left), fill.Top, fill.Right, fill.Bottom);
            }

            using (var path = CreateTubePath(tube, vertical))
            {
                var state = graphics.Save();
                graphics.SetClip(path);
                graphics.FillRectangle(brush, fill);
                graphics.Restore(state);
            }
        }
    }

    private static Rectangle BandRectangle(Rectangle track, double startN, double endN, bool vertical)
    {
        if (vertical)
        {
            int bottom = track.Bottom - (int)Math.Round(track.Height * startN);
            int top = track.Bottom - (int)Math.Round(track.Height * endN);
            return Rectangle.FromLTRB(track.Left, top, track.Right, bottom);
        }

        int left = track.Left + (int)Math.Round(track.Width * startN);
        int right = track.Left + (int)Math.Round(track.Width * endN);
        return Rectangle.FromLTRB(left, track.Top, right, track.Bottom);
    }

    private void DrawTicks(Graphics graphics, Rectangle tube, ThermometerGaugeValues values, bool vertical, Color tickColor)
    {
        using (var majorPen = new Pen(tickColor, 1.5f))
        using (var minorPen = new Pen(tickColor, 1f))
        {
            EnsureFonts();
            int majors = values.MajorTickCount;
            for (int i = 0; i < majors; i++)
            {
                double n = i / (double)(majors - 1);
                if (vertical)
                {
                    int y = tube.Bottom - (int)Math.Round(tube.Height * n);
                    graphics.DrawLine(majorPen, tube.Right + 2, y, tube.Right + 10, y);
                }
                else
                {
                    int x = tube.Left + (int)Math.Round(tube.Width * n);
                    graphics.DrawLine(majorPen, x, tube.Bottom + 2, x, tube.Bottom + 10);
                }

                if (values.ShowScaleLabels && _labelFont != null)
                {
                    double value = values.Minimum + ((values.Maximum - values.Minimum) * n);
                    string label = value.ToString("0.##", CultureInfo.CurrentCulture);
                    using (var brush = new SolidBrush(tickColor))
                    {
                        SizeF size = graphics.MeasureString(label, _labelFont);
                        if (vertical)
                        {
                            int y = tube.Bottom - (int)Math.Round(tube.Height * n);
                            graphics.DrawString(label, _labelFont, brush, tube.Right + 12, y - (size.Height / 2f));
                        }
                        else
                        {
                            int x = tube.Left + (int)Math.Round(tube.Width * n);
                            graphics.DrawString(label, _labelFont, brush, x - (size.Width / 2f), tube.Bottom + 10);
                        }
                    }
                }

                if (i < majors - 1 && values.MinorTickCount > 0)
                {
                    for (int m = 1; m <= values.MinorTickCount; m++)
                    {
                        double mn = (i + (m / (double)(values.MinorTickCount + 1))) / (majors - 1);
                        if (vertical)
                        {
                            int y = tube.Bottom - (int)Math.Round(tube.Height * mn);
                            graphics.DrawLine(minorPen, tube.Right + 2, y, tube.Right + 6, y);
                        }
                        else
                        {
                            int x = tube.Left + (int)Math.Round(tube.Width * mn);
                            graphics.DrawLine(minorPen, x, tube.Bottom + 2, x, tube.Bottom + 6);
                        }
                    }
                }
            }
        }
    }

    private void DrawCaption(Graphics graphics, Rectangle bounds, ThermometerGaugeValues values, Color color)
    {
        EnsureFonts();
        using (var brush = new SolidBrush(color))
        {
            if (!string.IsNullOrEmpty(values.Caption) && _captionFont != null)
            {
                graphics.DrawString(values.Caption, _captionFont, brush, bounds.X, bounds.Y);
            }

            if (!string.IsNullOrEmpty(values.Units) && _labelFont != null)
            {
                SizeF size = graphics.MeasureString(values.Units, _labelFont);
                graphics.DrawString(values.Units, _labelFont, brush,
                    bounds.Right - size.Width - 2,
                    bounds.Bottom - size.Height - 2);
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

        if (_captionFont == null || !_captionFont.FontFamily.Equals(baseFont.FontFamily))
        {
            _captionFont?.Dispose();
            _captionFont = new Font(baseFont.FontFamily, Math.Max(7f, baseFont.Size), FontStyle.Bold);
        }
    }
}
