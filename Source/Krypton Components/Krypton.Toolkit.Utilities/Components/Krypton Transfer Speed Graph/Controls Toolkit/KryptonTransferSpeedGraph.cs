#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Scrolls a theme-aware area chart of transfer speed samples.
/// </summary>
/// <remarks>
/// Part of <c>Krypton.Toolkit.Utilities</c>. The file-operation progress dialog in
/// <c>Krypton.Dialogs</c> hosts an instance behind More details.
/// </remarks>
[ToolboxItem(true)]
[DefaultProperty(nameof(Values))]
[Description("Displays a scrolling transfer-speed area chart.")]
public class KryptonTransferSpeedGraph : Control
{
    private double[] _samples;
    private int _capacity;
    private int _count;
    private int _write;
    private PaletteBase? _palette;
    private readonly PaletteRedirect _paletteRedirect;
    private readonly PaletteBackInheritRedirect _seriesBack;

    /// <summary>
    /// Creates an empty graph with the factory capacity.
    /// </summary>
    public KryptonTransferSpeedGraph()
    {
        Values = new SpeedGraphValues(this);
        _capacity = Values.Capacity;
        _samples = new double[_capacity];
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor, true);
        Size = new Size(320, 96);
        TabStop = false;

        _palette = KryptonManager.CurrentGlobalPalette;
        if (_palette != null)
        {
            _palette.PalettePaintInternal += OnPalettePaint;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        _paletteRedirect = new PaletteRedirect(_palette);
        StateCommon = new PaletteTripleRedirect(
            _paletteRedirect,
            PaletteBackStyle.PanelClient,
            PaletteBorderStyle.ControlClient,
            PaletteContentStyle.LabelNormalControl,
            OnNeedPaintHandler);
        StateDisabled = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        StateNormal = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        _seriesBack = new PaletteBackInheritRedirect(_paletteRedirect)
        {
            Style = PaletteBackStyle.ButtonStandalone
        };
    }

    /// <summary>
    /// Gets the behaviour and appearance values for this control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Capacity, speed label, and related transfer-speed graph values.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SpeedGraphValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    private void ResetValues() => Values.Reset();

    /// <summary>
    /// Gets access to the common appearance that other states can override.
    /// </summary>
    /// <remarks>
    /// <see cref="IPaletteBack.GetBackColor1"/> is the plot background,
    /// <see cref="IPaletteBack.GetBackColor2"/> is the series fill when set,
    /// border colours draw the frame and grid, and content short-text colour draws the speed label.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common transfer-speed graph appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled transfer-speed graph appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal transfer-speed graph appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Appends one sample in bytes per second and scrolls the chart.
    /// </summary>
    /// <param name="bytesPerSecond">Transfer rate for this sample.</param>
    /// <remarks>Ignored while <see cref="SpeedGraphValues.Paused"/> is <c>true</c>.</remarks>
    public void AddSample(double bytesPerSecond)
    {
        if (Values.Paused)
        {
            return;
        }

        if (bytesPerSecond < 0)
        {
            bytesPerSecond = 0;
        }

        _samples[_write] = bytesPerSecond;
        _write = (_write + 1) % _capacity;
        if (_count < _capacity)
        {
            _count++;
        }

        Invalidate();
    }

    /// <summary>
    /// Stops accepting samples so the chart stays frozen.
    /// </summary>
    public void Pause() => Values.Paused = true;

    /// <summary>
    /// Starts accepting samples again after <see cref="Pause"/>.
    /// </summary>
    public void Resume() => Values.Paused = false;

    /// <summary>
    /// Removes every sample.
    /// </summary>
    public void Clear()
    {
        Array.Clear(_samples, 0, _samples.Length);
        _count = 0;
        _write = 0;
        Invalidate();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_palette != null)
            {
                _palette.PalettePaintInternal -= OnPalettePaint;
                _palette = null;
            }

            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        PaletteState paletteState = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        IPaletteTriple state = Enabled ? StateNormal : StateDisabled;

        Color back = ResolveColor(state.PaletteBack.GetBackColor1(paletteState), SystemColors.Window);
        Color border = ResolveColor(state.PaletteBorder!.GetBorderColor1(paletteState), SystemColors.ControlDark);
        Color grid = Color.FromArgb(60, border.R, border.G, border.B);
        Color fill = EnsureSeriesContrast(ResolveFill(state.PaletteBack, paletteState), back);
        Color text = ResolveColor(state.PaletteContent!.GetContentShortTextColor1(paletteState), SystemColors.WindowText);

        using (var backBrush = new SolidBrush(back))
        {
            g.FillRectangle(backBrush, ClientRectangle);
        }

        Rectangle chart = ClientRectangle;
        chart.Inflate(-1, -1);
        if (chart.Width < 4 || chart.Height < 4)
        {
            return;
        }

        int midY = chart.Top + (chart.Height / 2);
        using (var gridPen = new Pen(grid))
        {
            g.DrawLine(gridPen, chart.Left, midY, chart.Right, midY);
            int stepX = Math.Max(12, chart.Width / 8);
            for (int x = chart.Left + stepX; x < chart.Right; x += stepX)
            {
                g.DrawLine(gridPen, x, chart.Top, x, chart.Bottom);
            }

            int stepY = Math.Max(12, chart.Height / 4);
            for (int y = chart.Top + stepY; y < chart.Bottom; y += stepY)
            {
                if (y == midY)
                {
                    continue;
                }

                g.DrawLine(gridPen, chart.Left, y, chart.Right, y);
            }
        }

        double peak = 0;
        for (int i = 0; i < _count; i++)
        {
            double sample = SampleAt(i);
            if (sample > peak)
            {
                peak = sample;
            }
        }

        // Keep the crest around two-thirds of the plot height so flat transfers are not a full-height slab.
        double max = peak <= 0 ? 1 : peak / 0.65;

        // Fixed window step: grow from the left until full, then scroll (oldest left, newest right).
        float step = chart.Width / (float)(Math.Max(_capacity, 2) - 1);
        float firstX = chart.Left;

        if (_count == 1)
        {
            float x = firstX;
            float y = YFor(SampleAt(0), max, chart);
            float columnWidth = Math.Max(3f, step * 0.65f);
            using (var columnBrush = new SolidBrush(Color.FromArgb(200, fill)))
            using (var edgePen = new Pen(ContrastEdge(fill, text), 1.75f))
            {
                g.FillRectangle(columnBrush, x, y, columnWidth, chart.Bottom - y);
                g.DrawLine(edgePen, x, y, x + columnWidth, y);
            }
        }
        else if (_count >= 2)
        {
            var points = new PointF[_count];
            for (int i = 0; i < _count; i++)
            {
                points[i] = new PointF(firstX + (i * step), YFor(SampleAt(i), max, chart));
            }

            using (var area = new GraphicsPath())
            using (var edge = new GraphicsPath())
            {
                area.StartFigure();
                area.AddLine(points[0].X, chart.Bottom, points[0].X, points[0].Y);
                for (int i = 1; i < _count; i++)
                {
                    area.AddLine(points[i - 1], points[i]);
                }

                area.AddLine(points[_count - 1].X, points[_count - 1].Y, points[_count - 1].X, chart.Bottom);
                area.AddLine(points[_count - 1].X, chart.Bottom, points[0].X, chart.Bottom);
                area.CloseFigure();

                edge.AddLines(points);

                float top = points[0].Y;
                float bottom = chart.Bottom;
                for (int i = 1; i < _count; i++)
                {
                    if (points[i].Y < top)
                    {
                        top = points[i].Y;
                    }
                }

                if (bottom - top < 1f)
                {
                    top = bottom - 1f;
                }

                Color edgeColor = ContrastEdge(fill, text);
                using (var gradient = new LinearGradientBrush(
                           new PointF(chart.Left, bottom),
                           new PointF(chart.Left, top),
                           Color.FromArgb(220, fill),
                           Color.FromArgb(90, fill)))
                using (var edgePen = new Pen(edgeColor, 2f))
                {
                    g.FillPath(gradient, area);
                    g.DrawPath(edgePen, edge);
                }
            }
        }

        using (var borderPen = new Pen(border))
        {
            g.DrawRectangle(borderPen, chart.Left, chart.Top, chart.Width - 1, chart.Height - 1);
        }

        if (Values.ShowSpeedLabel)
        {
            double current = _count == 0 ? 0 : SampleAt(_count - 1);
            string rate = FormatBytes(current) + "/s";
            string label = string.Format(CultureInfo.CurrentCulture, Values.SpeedLabelFormat, rate);
            using (var textBrush = new SolidBrush(text))
            {
                SizeF size = g.MeasureString(label, Font);
                float x = chart.Right - size.Width - 6;
                float y = chart.Top + 4;
                g.DrawString(label, Font, textBrush, x, y);
            }
        }
    }

    /// <summary>
    /// Applies changes from <see cref="Values"/>.
    /// </summary>
    internal void OnSpeedGraphValuesChanged()
    {
        if (_capacity != Values.Capacity)
        {
            ResizeBuffer(Values.Capacity);
            return;
        }

        Invalidate();
    }

    private Color ResolveFill(IPaletteBack back, PaletteState paletteState)
    {
        Color color2 = back.GetBackColor2(paletteState);
        if (!color2.IsEmpty && color2.A >= 32)
        {
            return color2;
        }

        Color checkedFill = _seriesBack.GetBackColor1(PaletteState.CheckedNormal);
        if (!checkedFill.IsEmpty && checkedFill.A >= 32)
        {
            return checkedFill;
        }

        Color tracking = _seriesBack.GetBackColor1(PaletteState.Tracking);
        if (!tracking.IsEmpty && tracking.A >= 32)
        {
            return tracking;
        }

        Color normal = _seriesBack.GetBackColor1(PaletteState.Normal);
        return ResolveColor(normal, Color.SeaGreen);
    }

    private static Color EnsureSeriesContrast(Color fill, Color back)
    {
        if (fill.IsEmpty)
        {
            return Color.SeaGreen;
        }

        float fillBrightness = fill.GetBrightness();
        float backBrightness = back.GetBrightness();
        if (Math.Abs(fillBrightness - backBrightness) >= 0.22f)
        {
            return fill;
        }

        // Light chart backs need a darker series; dark backs need a lighter one.
        return backBrightness > 0.55f
            ? ControlPaint.Dark(fill, 0.35f)
            : ControlPaint.Light(fill, 0.35f);
    }

    private static Color ContrastEdge(Color fill, Color text)
    {
        Color edge = ControlPaint.Dark(fill, 0.15f);
        if (edge.GetBrightness() > 0.8f)
        {
            return Color.FromArgb(220, text);
        }

        return edge;
    }

    private static Color ResolveColor(Color color, Color fallback) =>
        color.IsEmpty ? fallback : color;

    private static float YFor(double sample, double max, Rectangle chart)
    {
        double ratio = max <= 0 ? 0 : sample / max;
        if (ratio < 0)
        {
            ratio = 0;
        }

        if (ratio > 1)
        {
            ratio = 1;
        }

        return (float)(chart.Bottom - (ratio * chart.Height));
    }

    private double SampleAt(int logicalIndex)
    {
        int start = _count < _capacity ? 0 : _write;
        int index = (start + logicalIndex) % _capacity;
        return _samples[index];
    }

    private void ResizeBuffer(int capacity)
    {
        var next = new double[capacity];
        int copy = Math.Min(_count, capacity);
        for (int i = 0; i < copy; i++)
        {
            next[i] = SampleAt(_count - copy + i);
        }

        _samples = next;
        _capacity = capacity;
        _count = copy;
        _write = copy % capacity;
        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.PalettePaintInternal -= OnPalettePaint;
        }

        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect.Target = _palette;
        if (_palette != null)
        {
            _palette.PalettePaintInternal += OnPalettePaint;
        }

        Invalidate();
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

    private void OnNeedPaintHandler(object? sender, NeedLayoutEventArgs e) => Invalidate();

    private static string FormatBytes(double bytes)
    {
        string[] units = { "B", "KB", "MB", "GB", "TB" };
        double value = Math.Max(0, bytes);
        int unit = 0;
        while (value >= 1024 && unit < units.Length - 1)
        {
            value /= 1024;
            unit++;
        }

        if (unit == 0)
        {
            return ((ulong)value).ToString(CultureInfo.CurrentCulture) + " B";
        }

        return value.ToString("0.##", CultureInfo.CurrentCulture) + " " + units[unit];
    }
}
