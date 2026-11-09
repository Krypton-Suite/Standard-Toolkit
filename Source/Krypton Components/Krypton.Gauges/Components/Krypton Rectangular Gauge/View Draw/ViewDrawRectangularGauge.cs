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
/// Paints the rectangular indicator face of a <see cref="KryptonRectangularGauge"/>.
/// </summary>
internal sealed class ViewDrawRectangularGauge : ViewDrawPanel
{
    private readonly KryptonRectangularGauge _owner;
    private Font? _valueFont;
    private Font? _captionFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawRectangularGauge"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    /// <param name="paletteBack">Palette background drawn behind the face.</param>
    public ViewDrawRectangularGauge(KryptonRectangularGauge owner, IPaletteBack paletteBack)
        : base(paletteBack) =>
        _owner = owner;

    /// <summary>
    /// Gets the last computed indicator rectangle used for hit-testing.
    /// </summary>
    public Rectangle IndicatorBounds { get; private set; }

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return _owner.RectangularValues.Orientation == Orientation.Vertical
            ? new Size(56, 160)
            : new Size(160, 56);
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
        TextRenderingHint textHint = graphics.TextRenderingHint;
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

            RectangularGaugeValues values = _owner.RectangularValues;
            IndicatorBounds = bounds;
            Color face = _owner.ResolveFaceFill();
            Color border = _owner.ResolveFaceBorder();
            Color fill = ResolveValueFill(values);
            Color off = _owner.ResolveSegmentOff();
            Color tick = _owner.ResolveTick();

            using (var faceBrush = new SolidBrush(face))
            using (var borderPen = new Pen(border, 1.5f))
            {
                graphics.FillRectangle(faceBrush, bounds);
                graphics.DrawRectangle(borderPen, bounds);
            }

            if (values.Style == RectangularGaugeStyle.Segmented)
            {
                DrawSegments(graphics, bounds, values, fill, off);
            }
            else
            {
                DrawSolid(graphics, bounds, values, fill);
            }

            DrawCaptionAndValue(graphics, bounds, values, tick);
        }
        finally
        {
            graphics.SmoothingMode = smoothing;
            graphics.TextRenderingHint = textHint;
        }
    }

    private Color ResolveValueFill(RectangularGaugeValues values)
    {
        double value = _owner.DisplayedValue;
        foreach (GaugeRange range in values.Ranges)
        {
            double low = Math.Min(range.Start, range.End);
            double high = Math.Max(range.Start, range.End);
            if (value >= low && value <= high)
            {
                return _owner.ResolveRangeColor(range);
            }
        }

        return _owner.ResolveFill();
    }

    private void DrawSolid(Graphics graphics, Rectangle bounds, RectangularGaugeValues values, Color fillColor)
    {
        double n = values.Normalize(_owner.DisplayedValue);
        if (n <= 0d)
        {
            return;
        }

        Rectangle fill = values.Orientation == Orientation.Vertical
            ? new Rectangle(bounds.X + 2, bounds.Bottom - (int)Math.Round((bounds.Height - 4) * n) - 2, bounds.Width - 4, (int)Math.Round((bounds.Height - 4) * n))
            : new Rectangle(bounds.X + 2, bounds.Y + 2, (int)Math.Round((bounds.Width - 4) * n), bounds.Height - 4);

        using (var brush = new SolidBrush(fillColor))
        {
            graphics.FillRectangle(brush, fill);
        }
    }

    private void DrawSegments(Graphics graphics, Rectangle bounds, RectangularGaugeValues values, Color onColor, Color offColor)
    {
        int count = values.SegmentCount;
        double n = values.Normalize(_owner.DisplayedValue);
        int lit = (int)Math.Round(count * n);
        bool vertical = values.Orientation == Orientation.Vertical;
        int gap = 2;
        if (vertical)
        {
            int segmentHeight = Math.Max(2, ((bounds.Height - 4) - ((count - 1) * gap)) / count);
            for (int i = 0; i < count; i++)
            {
                int indexFromBottom = i;
                int y = bounds.Bottom - 2 - ((indexFromBottom + 1) * segmentHeight) - (indexFromBottom * gap);
                var rect = new Rectangle(bounds.X + 2, y, bounds.Width - 4, segmentHeight);
                using (var brush = new SolidBrush(indexFromBottom < lit ? onColor : offColor))
                {
                    graphics.FillRectangle(brush, rect);
                }
            }
        }
        else
        {
            int segmentWidth = Math.Max(2, ((bounds.Width - 4) - ((count - 1) * gap)) / count);
            for (int i = 0; i < count; i++)
            {
                int x = bounds.X + 2 + (i * (segmentWidth + gap));
                var rect = new Rectangle(x, bounds.Y + 2, segmentWidth, bounds.Height - 4);
                using (var brush = new SolidBrush(i < lit ? onColor : offColor))
                {
                    graphics.FillRectangle(brush, rect);
                }
            }
        }
    }

    private void DrawCaptionAndValue(Graphics graphics, Rectangle bounds, RectangularGaugeValues values, Color color)
    {
        EnsureFonts();
        using (var brush = new SolidBrush(color))
        {
            if (!string.IsNullOrEmpty(values.Caption) && _captionFont != null)
            {
                graphics.DrawString(values.Caption, _captionFont, brush, bounds.X + 4, bounds.Y + 2);
            }

            if (values.ShowValueText && _valueFont != null)
            {
                string text = _owner.DisplayedValue.ToString("0.##", CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(values.Units))
                {
                    text += " " + values.Units;
                }

                SizeF size = graphics.MeasureString(text, _valueFont);
                graphics.DrawString(text, _valueFont, brush,
                    bounds.X + ((bounds.Width - size.Width) / 2f),
                    bounds.Y + ((bounds.Height - size.Height) / 2f));
            }
        }
    }

    private void EnsureFonts()
    {
        Font baseFont = _owner.Font ?? Control.DefaultFont;
        if (_valueFont == null || !_valueFont.FontFamily.Equals(baseFont.FontFamily))
        {
            _valueFont?.Dispose();
            _valueFont = new Font(baseFont.FontFamily, Math.Max(8f, baseFont.Size), FontStyle.Bold);
        }

        if (_captionFont == null || !_captionFont.FontFamily.Equals(baseFont.FontFamily))
        {
            _captionFont?.Dispose();
            _captionFont = new Font(baseFont.FontFamily, Math.Max(6f, baseFont.Size * 0.8f), baseFont.Style);
        }
    }
}
