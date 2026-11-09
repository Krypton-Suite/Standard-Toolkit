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
/// Paints the linear bar face of a <see cref="KryptonLinearGauge"/>.
/// </summary>
internal sealed class ViewDrawLinearGauge : ViewDrawPanel
{
    private readonly KryptonLinearGauge _owner;
    private Font? _labelFont;
    private Font? _captionFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawLinearGauge"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    /// <param name="paletteBack">Palette background drawn behind the face.</param>
    public ViewDrawLinearGauge(KryptonLinearGauge owner, IPaletteBack paletteBack)
        : base(paletteBack) =>
        _owner = owner;

    /// <summary>
    /// Gets the last computed track rectangle used for hit-testing.
    /// </summary>
    public Rectangle TrackBounds { get; private set; }

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return _owner.LinearValues.Orientation == Orientation.Vertical
            ? new Size(48, 240)
            : new Size(240, 48);
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

            LinearGaugeValues values = _owner.LinearValues;
            bool vertical = values.Orientation == Orientation.Vertical;
            TrackBounds = ComputeTrack(bounds, vertical);
            Color face = _owner.ResolveFaceFill();
            Color border = _owner.ResolveFaceBorder();
            Color tick = _owner.ResolveTick();
            Color fill = _owner.ResolveFill();

            using (var faceBrush = new SolidBrush(face))
            using (var borderPen = new Pen(border, 1.5f))
            {
                graphics.FillRectangle(faceBrush, TrackBounds);
                graphics.DrawRectangle(borderPen, TrackBounds);
            }

            DrawRanges(graphics, TrackBounds, values, vertical);
            DrawFill(graphics, TrackBounds, values, vertical, fill);
            DrawTicks(graphics, TrackBounds, values, vertical, tick);
            DrawCaption(graphics, bounds, values, tick);
        }
        finally
        {
            graphics.SmoothingMode = smoothing;
            graphics.TextRenderingHint = text;
        }
    }

    private static Rectangle ComputeTrack(Rectangle bounds, bool vertical)
    {
        if (vertical)
        {
            int width = Math.Max(10, Math.Min(28, bounds.Width / 2));
            return new Rectangle(bounds.X + ((bounds.Width - width) / 2), bounds.Y + 12, width, Math.Max(8, bounds.Height - 24));
        }

        int height = Math.Max(10, Math.Min(28, bounds.Height / 2));
        return new Rectangle(bounds.X + 12, bounds.Y + ((bounds.Height - height) / 2), Math.Max(8, bounds.Width - 24), height);
    }

    private void DrawRanges(Graphics graphics, Rectangle track, LinearGaugeValues values, bool vertical)
    {
        foreach (GaugeRange range in values.Ranges)
        {
            double startN = values.Normalize(Math.Min(range.Start, range.End));
            double endN = values.Normalize(Math.Max(range.Start, range.End));
            if (endN <= startN)
            {
                continue;
            }

            Rectangle band = BandRectangle(track, startN, endN, vertical);
            using (var brush = new SolidBrush(Color.FromArgb(120, _owner.ResolveRangeColor(range))))
            {
                graphics.FillRectangle(brush, band);
            }
        }
    }

    private void DrawFill(Graphics graphics, Rectangle track, LinearGaugeValues values, bool vertical, Color fillColor)
    {
        double n = values.Normalize(_owner.DisplayedValue);
        if (n <= 0d)
        {
            return;
        }

        Rectangle fill = BandRectangle(track, 0d, n, vertical);
        using (var brush = new SolidBrush(fillColor))
        {
            graphics.FillRectangle(brush, fill);
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

    private void DrawTicks(Graphics graphics, Rectangle track, LinearGaugeValues values, bool vertical, Color tickColor)
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
                    int y = track.Bottom - (int)Math.Round(track.Height * n);
                    graphics.DrawLine(majorPen, track.Right + 2, y, track.Right + 10, y);
                }
                else
                {
                    int x = track.Left + (int)Math.Round(track.Width * n);
                    graphics.DrawLine(majorPen, x, track.Bottom + 2, x, track.Bottom + 10);
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
                            int y = track.Bottom - (int)Math.Round(track.Height * n);
                            graphics.DrawString(label, _labelFont, brush, track.Right + 12, y - (size.Height / 2f));
                        }
                        else
                        {
                            int x = track.Left + (int)Math.Round(track.Width * n);
                            graphics.DrawString(label, _labelFont, brush, x - (size.Width / 2f), track.Bottom + 10);
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
                            int y = track.Bottom - (int)Math.Round(track.Height * mn);
                            graphics.DrawLine(minorPen, track.Right + 2, y, track.Right + 6, y);
                        }
                        else
                        {
                            int x = track.Left + (int)Math.Round(track.Width * mn);
                            graphics.DrawLine(minorPen, x, track.Bottom + 2, x, track.Bottom + 6);
                        }
                    }
                }
            }
        }
    }

    private void DrawCaption(Graphics graphics, Rectangle bounds, LinearGaugeValues values, Color color)
    {
        EnsureFonts();
        if (_captionFont == null || string.IsNullOrEmpty(values.Caption))
        {
            return;
        }

        using (var brush = new SolidBrush(color))
        {
            graphics.DrawString(values.Caption, _captionFont, brush, bounds.X, bounds.Y);
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
