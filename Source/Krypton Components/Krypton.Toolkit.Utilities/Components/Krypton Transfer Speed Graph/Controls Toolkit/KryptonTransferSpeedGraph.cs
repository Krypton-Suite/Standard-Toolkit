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
/// Colours inherit from <see cref="PaletteElement.TransferSpeedGraph"/> via <see cref="StateCommon"/>.
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
    private double _peak;
    private double _trough;
    private PaletteBase? _palette;
    private readonly PaletteRedirect _paletteRedirect;

    /// <summary>
    /// Creates an empty graph with the factory capacity.
    /// </summary>
    public KryptonTransferSpeedGraph()
        : this(PaletteElement.TransferSpeedGraph)
    {
    }

    /// <summary>
    /// Creates an empty graph that inherits colours from <paramref name="paletteElement"/>.
    /// </summary>
    /// <param name="paletteElement">Speed or progress graph palette element.</param>
    protected KryptonTransferSpeedGraph(PaletteElement paletteElement)
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
        StateCommon = new PaletteTransferSpeedGraphRedirect(_paletteRedirect, OnNeedPaintHandler, paletteElement);
        StateDisabled = new PaletteTransferSpeedGraphStates(StateCommon, OnNeedPaintHandler);
        StateNormal = new PaletteTransferSpeedGraphStates(StateCommon, OnNeedPaintHandler);
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
    /// <see cref="PaletteTransferSpeedGraphRedirect.Plot"/> is the plot background,
    /// <see cref="PaletteTransferSpeedGraphRedirect.Series"/> is the series fill,
    /// <see cref="PaletteTransferSpeedGraphRedirect.Grid"/> draws the grid,
    /// <see cref="PaletteTransferSpeedGraphRedirect.Border"/> draws the frame,
    /// and <see cref="PaletteTransferSpeedGraphRedirect.Label"/> draws the speed label.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common transfer-speed graph appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled transfer-speed graph appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal transfer-speed graph appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets the highest sample in the current window, in bytes per second.
    /// </summary>
    /// <remarks>Returns <c>0</c> when the chart has no samples.</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double Peak => _peak;

    /// <summary>
    /// Gets the lowest sample in the current window, in bytes per second.
    /// </summary>
    /// <remarks>Returns <c>0</c> when the chart has no samples.</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double Trough => _trough;

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

        bool windowFull = _count == _capacity;
        double overwritten = windowFull ? _samples[_write] : 0;
        _samples[_write] = bytesPerSecond;
        _write = (_write + 1) % _capacity;
        if (_count < _capacity)
        {
            _count++;
        }

        UpdateExtremaAfterAdd(bytesPerSecond, windowFull, overwritten);
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
        _peak = 0;
        _trough = 0;
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
        PaletteTransferSpeedGraphStates state = Enabled ? StateNormal : StateDisabled;

        Color back = ResolveColor(state.Plot.GetElementColor1(paletteState), SystemColors.Window);
        Color border = ResolveColor(state.Border.GetElementColor1(paletteState), SystemColors.ControlDark);
        Color fill = EnsureSeriesContrast(ResolveColor(state.Series.GetElementColor1(paletteState), Color.SeaGreen), back);
        Color text = ResolveColor(state.Label.GetElementColor1(paletteState), SystemColors.WindowText);

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

        if (Values.ShowGrid)
        {
            Color gridSource = ResolveColor(state.Grid.GetElementColor1(paletteState), border);
            Color grid = Color.FromArgb(60, gridSource.R, gridSource.G, gridSource.B);
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
        }

        // Keep the crest around two-thirds of the plot height so flat transfers are not a full-height slab.
        double max = Peak <= 0 ? 1 : Peak / 0.65;

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
            string rate = FormatSample(current, Values.SampleDisplay);
            string peakRate = FormatSample(Peak, Values.SampleDisplay);
            string troughRate = FormatSample(Trough, Values.SampleDisplay);
            string label = string.Format(CultureInfo.CurrentCulture, Values.SpeedLabelFormat, rate, peakRate, troughRate);
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

    private static Color EnsureSeriesContrast(Color fill, Color back)
    {
        if (fill.IsEmpty)
        {
            fill = Color.SeaGreen;
        }

        float fillBrightness = fill.GetBrightness();
        float backBrightness = back.GetBrightness();

        // Light plot: keep the series clearly darker than the chart face.
        if (backBrightness > 0.55f)
        {
            return fillBrightness > 0.5f ? ControlPaint.Dark(fill, 0.4f) : fill;
        }

        // Dark plot: lift muted accents so the fill stays visible (CheckedNormal can be near-black).
        return fillBrightness < 0.55f ? ControlPaint.Light(fill, 0.7f) : fill;
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

    private void UpdateExtremaAfterAdd(double sample, bool windowFull, double overwritten)
    {
        if (_count == 1)
        {
            _peak = sample;
            _trough = sample;
            return;
        }

        if (sample > _peak)
        {
            _peak = sample;
        }
        else if (windowFull && overwritten >= _peak)
        {
            RecomputeExtrema();
            return;
        }

        if (sample < _trough)
        {
            _trough = sample;
        }
        else if (windowFull && overwritten <= _trough)
        {
            RecomputeExtrema();
        }
    }

    private void RecomputeExtrema()
    {
        if (_count == 0)
        {
            _peak = 0;
            _trough = 0;
            return;
        }

        double peak = SampleAt(0);
        double trough = peak;
        for (int i = 1; i < _count; i++)
        {
            double sample = SampleAt(i);
            if (sample > peak)
            {
                peak = sample;
            }

            if (sample < trough)
            {
                trough = sample;
            }
        }

        _peak = peak;
        _trough = trough;
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
        RecomputeExtrema();
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
        StateCommon.SetRedirector(_paletteRedirect);
        if (_palette != null)
        {
            _palette.PalettePaintInternal += OnPalettePaint;
        }

        Invalidate();
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

    private void OnNeedPaintHandler(object? sender, NeedLayoutEventArgs e) => Invalidate();

    private static string FormatSample(double value, TransferGraphSampleDisplay display)
    {
        switch (display)
        {
            case TransferGraphSampleDisplay.Percent:
                return value.ToString("0", CultureInfo.CurrentCulture) + "%";
            case TransferGraphSampleDisplay.Bytes:
                return FormatBytes(value);
            default:
                return FormatBytes(value) + "/s";
        }
    }

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
