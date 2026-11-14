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
/// Paints the bars, peak marks, and gain thumbs of a <see cref="KryptonEqualizer"/>.
/// </summary>
internal sealed class ViewDrawEqualizer : ViewDrawPanel
{
    private const int SegmentCount = 16;
    private readonly KryptonEqualizer _owner;
    private Font? _labelFont;
    private Font? _sourceFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawEqualizer"/> class.
    /// </summary>
    /// <param name="owner">Owning equalizer.</param>
    /// <param name="paletteBack">Palette background drawn behind the face.</param>
    public ViewDrawEqualizer(KryptonEqualizer owner, IPaletteBack paletteBack)
        : base(paletteBack) =>
        _owner = owner;

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return new Size(360, 180);
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

            DrawFace(graphics, bounds);
            DrawBands(graphics, bounds);
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
            _labelFont = null;
            _sourceFont = null;
        }

        base.Dispose(disposing);
    }

    private void DrawFace(Graphics graphics, Rectangle bounds)
    {
        PaletteEqualizerStates colors = Colors();
        PaletteState state = PaletteStateFor(_owner.Enabled);
        using (var brush = new SolidBrush(colors.Face.GetElementColor1(state)))
        {
            graphics.FillRectangle(brush, bounds);
        }

        using (var pen = new Pen(colors.Face.GetElementColor2(state)))
        {
            graphics.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
        }
    }

    private void DrawBands(Graphics graphics, Rectangle bounds)
    {
        EqualizerValues values = _owner.Values;
        int count = values.Bands.Count;
        if (count == 0)
        {
            return;
        }

        bool vertical = values.Orientation == VisualOrientation.Top || values.Orientation == VisualOrientation.Bottom;
        int labelSpace = values.ShowBandLabels ? 16 : 0;
        Rectangle plot = bounds;
        if (vertical)
        {
            plot.Height -= labelSpace;
            if (values.Orientation == VisualOrientation.Bottom)
            {
                plot.Y += labelSpace;
            }
        }
        else
        {
            plot.Width -= labelSpace;
            if (values.Orientation == VisualOrientation.Right)
            {
                plot.X += labelSpace;
            }
        }

        if (plot.Width < 4 || plot.Height < 4)
        {
            return;
        }

        float slot = vertical ? plot.Width / (float)count : plot.Height / (float)count;
        float gap = Math.Max(1f, slot * 0.12f);
        float thickness = Math.Max(2f, slot - gap);

        for (int i = 0; i < count; i++)
        {
            RectangleF barSlot = vertical
                ? new RectangleF(plot.X + (i * slot) + (gap * 0.5f), plot.Y, thickness, plot.Height)
                : new RectangleF(plot.X, plot.Y + (i * slot) + (gap * 0.5f), plot.Width, thickness);

            double displayed = _owner.GetDisplayedLevel(i);
            double peak = _owner.GetDisplayedPeak(i);
            if (values.Style == EqualizerStyle.Segments)
            {
                DrawSegmentBar(graphics, barSlot, displayed, values.Orientation);
            }
            else
            {
                DrawSolidBar(graphics, barSlot, displayed, values.Orientation);
            }

            if (values.PeakHold)
            {
                DrawPeak(graphics, barSlot, peak, values.Orientation);
            }

            if (values.Interactive)
            {
                DrawThumb(graphics, barSlot, values.Bands[i].Gain, values.MinimumGainDb, values.MaximumGainDb, values.Orientation);
            }

            if (values.ShowBandLabels)
            {
                DrawLabel(graphics, bounds, barSlot, values.Bands[i].DisplayText, values.Orientation, vertical);
            }
        }
    }

    private void DrawSolidBar(Graphics graphics, RectangleF slot, double level, VisualOrientation orientation)
    {
        level = EqualizerScale.ClampLevel(level);
        if (level <= 0.001d)
        {
            return;
        }

        RectangleF fill = LevelRectangle(slot, level, orientation);
        using (var brush = new LinearGradientBrush(slot, ColorForLevel(0.15d), ColorForLevel(1d),
                   orientation == VisualOrientation.Left || orientation == VisualOrientation.Right
                       ? LinearGradientMode.Horizontal
                       : LinearGradientMode.Vertical))
        {
            graphics.FillRectangle(brush, fill);
        }
    }

    private void DrawSegmentBar(Graphics graphics, RectangleF slot, double level, VisualOrientation orientation)
    {
        level = EqualizerScale.ClampLevel(level);
        bool vertical = orientation == VisualOrientation.Top || orientation == VisualOrientation.Bottom;
        float segmentSpan = (vertical ? slot.Height : slot.Width) / SegmentCount;
        float gap = Math.Max(1f, segmentSpan * 0.18f);
        float lit = (float)(level * SegmentCount);

        PaletteEqualizerStates colors = Colors();
        PaletteState state = PaletteStateFor(_owner.Enabled);
        Color off = colors.Off.GetElementColor1(state);

        for (int s = 0; s < SegmentCount; s++)
        {
            RectangleF seg;
            if (vertical)
            {
                float y = orientation == VisualOrientation.Top
                    ? slot.Bottom - ((s + 1) * segmentSpan) + gap
                    : slot.Top + (s * segmentSpan);
                seg = new RectangleF(slot.X, y, slot.Width, Math.Max(1f, segmentSpan - gap));
            }
            else
            {
                float x = orientation == VisualOrientation.Left
                    ? slot.Left + (s * segmentSpan)
                    : slot.Right - ((s + 1) * segmentSpan) + gap;
                seg = new RectangleF(x, slot.Y, Math.Max(1f, segmentSpan - gap), slot.Height);
            }

            bool on = s + 1 <= lit + 0.001f;
            Color color = on ? ColorForLevel((s + 0.5d) / SegmentCount) : off;
            using (var brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, seg);
            }
        }
    }

    private void DrawPeak(Graphics graphics, RectangleF slot, double peak, VisualOrientation orientation)
    {
        peak = EqualizerScale.ClampLevel(peak);
        if (peak <= 0.001d)
        {
            return;
        }

        PaletteEqualizerStates colors = Colors();
        PaletteState state = PaletteStateFor(_owner.Enabled);
        using (var brush = new SolidBrush(colors.Peak.GetElementColor1(state)))
        {
            const float thickness = 3f;
            switch (orientation)
            {
                case VisualOrientation.Bottom:
                {
                    float y = slot.Top + (float)((1d - peak) * slot.Height);
                    graphics.FillRectangle(brush, slot.X, y, slot.Width, thickness);
                    break;
                }
                case VisualOrientation.Left:
                {
                    float x = slot.Left + (float)(peak * slot.Width) - thickness;
                    graphics.FillRectangle(brush, x, slot.Y, thickness, slot.Height);
                    break;
                }
                case VisualOrientation.Right:
                {
                    float x = slot.Right - (float)(peak * slot.Width);
                    graphics.FillRectangle(brush, x, slot.Y, thickness, slot.Height);
                    break;
                }
                default:
                {
                    float y = slot.Bottom - (float)(peak * slot.Height) - thickness;
                    graphics.FillRectangle(brush, slot.X, y, slot.Width, thickness);
                    break;
                }
            }
        }
    }

    private void DrawThumb(Graphics graphics, RectangleF slot, double gain, double minimum, double maximum, VisualOrientation orientation)
    {
        float position = (float)EqualizerScale.GainPosition(gain, minimum, maximum);
        PaletteEqualizerStates colors = Colors();
        PaletteState state = PaletteStateFor(_owner.Enabled);
        Color thumb = colors.Thumb.GetElementColor1(state);
        const float size = 8f;

        PointF center;
        switch (orientation)
        {
            case VisualOrientation.Bottom:
                center = new PointF(slot.X + (slot.Width * 0.5f), slot.Top + ((1f - position) * slot.Height));
                break;
            case VisualOrientation.Left:
                center = new PointF(slot.Left + (position * slot.Width), slot.Y + (slot.Height * 0.5f));
                break;
            case VisualOrientation.Right:
                center = new PointF(slot.Right - (position * slot.Width), slot.Y + (slot.Height * 0.5f));
                break;
            default:
                center = new PointF(slot.X + (slot.Width * 0.5f), slot.Bottom - (position * slot.Height));
                break;
        }

        using (var path = new GraphicsPath())
        {
            path.AddEllipse(center.X - (size * 0.5f), center.Y - (size * 0.5f), size, size);
            using (var brush = new SolidBrush(thumb))
            {
                graphics.FillPath(brush, path);
            }

            using (var pen = new Pen(colors.Face.GetElementColor2(state)))
            {
                graphics.DrawPath(pen, path);
            }
        }
    }

    private void DrawLabel(Graphics graphics, Rectangle bounds, RectangleF barSlot, string text,
        VisualOrientation orientation, bool vertical)
    {
        EnsureLabelFont();
        if (_labelFont == null)
        {
            return;
        }

        PaletteEqualizerStates colors = Colors();
        PaletteState state = PaletteStateFor(_owner.Enabled);
        using (var brush = new SolidBrush(colors.Label.GetElementColor1(state)))
        {
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            };

            RectangleF labelBounds;
            if (vertical)
            {
                float y = orientation == VisualOrientation.Top ? bounds.Bottom - 14f : bounds.Top;
                labelBounds = new RectangleF(barSlot.X - 2f, y, barSlot.Width + 4f, 14f);
            }
            else
            {
                float x = orientation == VisualOrientation.Left ? bounds.Right - 14f : bounds.Left;
                labelBounds = new RectangleF(x, barSlot.Y - 2f, 14f, barSlot.Height + 4f);
                format.FormatFlags |= StringFormatFlags.DirectionVertical;
            }

            graphics.DrawString(text, _labelFont, brush, labelBounds, format);
        }
    }

    private static RectangleF LevelRectangle(RectangleF slot, double level, VisualOrientation orientation)
    {
        switch (orientation)
        {
            case VisualOrientation.Bottom:
            {
                float height = (float)(slot.Height * level);
                return new RectangleF(slot.X, slot.Top, slot.Width, height);
            }
            case VisualOrientation.Left:
            {
                float width = (float)(slot.Width * level);
                return new RectangleF(slot.Left, slot.Y, width, slot.Height);
            }
            case VisualOrientation.Right:
            {
                float width = (float)(slot.Width * level);
                return new RectangleF(slot.Right - width, slot.Y, width, slot.Height);
            }
            default:
            {
                float height = (float)(slot.Height * level);
                return new RectangleF(slot.X, slot.Bottom - height, slot.Width, height);
            }
        }
    }

    private Color ColorForLevel(double level)
    {
        PaletteEqualizerStates colors = Colors();
        PaletteState state = PaletteStateFor(_owner.Enabled);
        if (level < 0.55d)
        {
            return colors.Low.GetElementColor1(state);
        }

        if (level < 0.82d)
        {
            return colors.Mid.GetElementColor1(state);
        }

        return colors.High.GetElementColor1(state);
    }

    private PaletteEqualizerStates Colors() =>
        _owner.Enabled ? _owner.StateNormal : _owner.StateDisabled;

    private static PaletteState PaletteStateFor(bool enabled) =>
        enabled ? PaletteState.Normal : PaletteState.Disabled;

    private void EnsureLabelFont()
    {
        Font source = _owner.Font ?? Control.DefaultFont;
        if (_labelFont != null && ReferenceEquals(_sourceFont, source))
        {
            return;
        }

        _labelFont?.Dispose();
        _sourceFont = source;
        float size = Math.Max(6f, source.Size - 1.5f);
        _labelFont = new Font(source.FontFamily, size, FontStyle.Regular);
    }
}
