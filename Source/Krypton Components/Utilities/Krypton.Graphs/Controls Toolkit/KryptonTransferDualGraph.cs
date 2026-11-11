#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Graphs;

/// <summary>
/// Scrolls speed and optional progress as two series on one theme-aware chart.
/// </summary>
/// <remarks>
/// Part of <c>Krypton.Toolkit.Graphs</c>. Speed colours inherit from
/// <see cref="PaletteElement.TransferSpeedGraph"/>; progress series and label colours inherit from
/// <see cref="PaletteElement.TransferProgressGraph"/>. Each series uses an independent Y scale.
/// </remarks>
[ToolboxItem(true)]
[DefaultProperty(nameof(Values))]
[Description("Displays speed and optional progress as two series on one transfer chart.")]
public class KryptonTransferDualGraph : Control
{
    private double[] _speedSamples;
    private double[] _progressSamples;
    private int _capacity;
    private int _speedCount;
    private int _progressCount;
    private int _speedWrite;
    private int _progressWrite;
    private double _speedPeak;
    private double _progressPeak;
    private PaletteBase? _palette;
    private readonly PaletteRedirect _paletteRedirect;

    /// <summary>Creates an empty dual-series graph with the factory capacity.</summary>
    public KryptonTransferDualGraph()
    {
        Values = new DualGraphValues(this);
        _capacity = Values.Capacity;
        _speedSamples = new double[_capacity];
        _progressSamples = new double[_capacity];
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
            _palette.PalettePaint += OnPalettePaint;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        _paletteRedirect = new PaletteRedirect(_palette);
        StateCommon = new PaletteTransferSpeedGraphRedirect(_paletteRedirect, OnNeedPaintHandler, PaletteElement.TransferSpeedGraph);
        StateDisabled = new PaletteTransferSpeedGraphStates(StateCommon, OnNeedPaintHandler);
        StateNormal = new PaletteTransferSpeedGraphStates(StateCommon, OnNeedPaintHandler);
        ProgressSeries = new PaletteTransferSpeedGraphColorRedirect(_paletteRedirect, TransferSpeedGraphPaletteColor.Series, OnNeedPaintHandler, PaletteElement.TransferProgressGraph);
        ProgressLabel = new PaletteTransferSpeedGraphColorRedirect(_paletteRedirect, TransferSpeedGraphPaletteColor.Label, OnNeedPaintHandler, PaletteElement.TransferProgressGraph);
    }

    /// <summary>Gets the behaviour and appearance values for this control.</summary>
    [Category(@"Visuals")]
    [Description(@"Capacity, labels, and progress-series options.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DualGraphValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    private void ResetValues() => Values.Reset();

    /// <summary>Gets common speed-chart appearance (plot, series, grid, border, label).</summary>
    [Category(@"Visuals")]
    [Description(@"Speed series and chart chrome from TransferSpeedGraph.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>Gets disabled speed appearance overrides.</summary>
    [Category(@"Visuals")]
    [Description(@"Disabled overrides for speed chart chrome and series.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>Gets normal speed appearance overrides.</summary>
    [Category(@"Visuals")]
    [Description(@"Normal overrides for speed chart chrome and series.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>Gets the progress series colour (TransferProgressGraph Series).</summary>
    [Category(@"Visuals")]
    [Description(@"Progress series fill from TransferProgressGraph.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphColorRedirect ProgressSeries { get; }

    private bool ShouldSerializeProgressSeries() => !ProgressSeries.IsDefault;

    /// <summary>Gets the progress label colour (TransferProgressGraph Label).</summary>
    [Category(@"Visuals")]
    [Description(@"Progress label colour from TransferProgressGraph.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphColorRedirect ProgressLabel { get; }

    private bool ShouldSerializeProgressLabel() => !ProgressLabel.IsDefault;

    /// <summary>Gets or sets whether the progress series is painted.</summary>
    [Category(@"Behavior")]
    [Description(@"When false, only the speed series is shown.")]
    [DefaultValue(true)]
    public bool ShowProgressSeries
    {
        get => Values.ShowProgressSeries;
        set => Values.ShowProgressSeries = value;
    }

    /// <summary>Gets or sets how progress samples are formatted.</summary>
    [Category(@"Appearance")]
    [Description(@"Formats progress samples as percent or absolute bytes.")]
    [DefaultValue(TransferGraphSampleDisplay.Percent)]
    public TransferGraphSampleDisplay ProgressSampleDisplay
    {
        get => Values.ProgressSampleDisplay;
        set => Values.ProgressSampleDisplay = value;
    }

    /// <summary>Gets the highest speed sample in the current window.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double SpeedPeak => _speedPeak;

    /// <summary>Gets the highest progress sample in the current window.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double ProgressPeak => _progressPeak;

    /// <summary>Appends one speed sample in bytes per second.</summary>
    /// <param name="bytesPerSecond">Transfer rate for this sample.</param>
    public void AddSpeedSample(double bytesPerSecond)
    {
        if (Values.Paused)
        {
            return;
        }

        if (bytesPerSecond < 0)
        {
            bytesPerSecond = 0;
        }

        AddToBuffer(_speedSamples, ref _speedCount, ref _speedWrite, ref _speedPeak, bytesPerSecond);
        Invalidate();
    }

    /// <summary>Appends one progress sample (percent or bytes per <see cref="ProgressSampleDisplay"/>).</summary>
    /// <param name="value">Progress value for this sample.</param>
    public void AddProgressSample(double value)
    {
        if (Values.Paused || !Values.ShowProgressSeries)
        {
            return;
        }

        if (value < 0)
        {
            value = 0;
        }

        AddToBuffer(_progressSamples, ref _progressCount, ref _progressWrite, ref _progressPeak, value);
        Invalidate();
    }

    /// <summary>Stops accepting samples so the chart stays frozen.</summary>
    public void Pause() => Values.Paused = true;

    /// <summary>Starts accepting samples again after <see cref="Pause"/>.</summary>
    public void Resume() => Values.Paused = false;

    /// <summary>Removes every sample from both series.</summary>
    public void Clear()
    {
        Array.Clear(_speedSamples, 0, _speedSamples.Length);
        Array.Clear(_progressSamples, 0, _progressSamples.Length);
        _speedCount = 0;
        _progressCount = 0;
        _speedWrite = 0;
        _progressWrite = 0;
        _speedPeak = 0;
        _progressPeak = 0;
        Invalidate();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
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
        Color speedFill = EnsureSeriesContrast(ResolveColor(state.Series.GetElementColor1(paletteState), Color.SeaGreen), back);
        Color speedText = ResolveColor(state.Label.GetElementColor1(paletteState), SystemColors.WindowText);
        Color progressFill = EnsureSeriesContrast(ResolveColor(ProgressSeries.GetElementColor1(paletteState), Color.DodgerBlue), back);
        Color progressText = ResolveColor(ProgressLabel.GetElementColor1(paletteState), SystemColors.WindowText);

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

        // Progress behind speed so the rate series stays the dominant stroke.
        if (Values.ShowProgressSeries)
        {
            PaintSeries(g, chart, _progressSamples, _progressCount, _progressWrite, _progressPeak, progressFill, progressText);
        }

        PaintSeries(g, chart, _speedSamples, _speedCount, _speedWrite, _speedPeak, speedFill, speedText);

        using (var borderPen = new Pen(border))
        {
            g.DrawRectangle(borderPen, chart.Left, chart.Top, chart.Width - 1, chart.Height - 1);
        }

        if (Values.ShowLabels)
        {
            float labelY = chart.Top + 4;
            if (_speedCount > 0)
            {
                double current = SampleAt(_speedSamples, _speedCount, _speedWrite, _speedCount - 1);
                string label = string.Format(
                    CultureInfo.CurrentCulture,
                    Values.SpeedLabelFormat,
                    FormatSample(current, TransferGraphSampleDisplay.Rate),
                    FormatSample(_speedPeak, TransferGraphSampleDisplay.Rate),
                    FormatSample(0, TransferGraphSampleDisplay.Rate));
                using (var textBrush = new SolidBrush(speedText))
                {
                    SizeF size = g.MeasureString(label, Font);
                    g.DrawString(label, Font, textBrush, chart.Right - size.Width - 6, labelY);
                    labelY += size.Height + 2;
                }
            }

            if (Values.ShowProgressSeries && _progressCount > 0)
            {
                double current = SampleAt(_progressSamples, _progressCount, _progressWrite, _progressCount - 1);
                string label = string.Format(
                    CultureInfo.CurrentCulture,
                    Values.ProgressLabelFormat,
                    FormatSample(current, Values.ProgressSampleDisplay),
                    FormatSample(_progressPeak, Values.ProgressSampleDisplay),
                    FormatSample(0, Values.ProgressSampleDisplay));
                using (var textBrush = new SolidBrush(progressText))
                {
                    SizeF size = g.MeasureString(label, Font);
                    g.DrawString(label, Font, textBrush, chart.Right - size.Width - 6, labelY);
                }
            }
        }
    }

    /// <summary>Applies changes from <see cref="Values"/>.</summary>
    internal void OnDualGraphValuesChanged()
    {
        if (_capacity != Values.Capacity)
        {
            ResizeBuffers(Values.Capacity);
            return;
        }

        Invalidate();
    }

    private void PaintSeries(Graphics g, Rectangle chart, double[] samples, int count, int write, double peak, Color fill, Color text)
    {
        double max = peak <= 0 ? 1 : peak / 0.65;
        float step = chart.Width / (float)(Math.Max(_capacity, 2) - 1);

        if (count == 1)
        {
            float x = chart.Left;
            float y = YFor(SampleAt(samples, count, write, 0), max, chart);
            float columnWidth = Math.Max(3f, step * 0.65f);
            using (var columnBrush = new SolidBrush(Color.FromArgb(160, fill)))
            using (var edgePen = new Pen(ContrastEdge(fill, text), 1.5f))
            {
                g.FillRectangle(columnBrush, x, y, columnWidth, chart.Bottom - y);
                g.DrawLine(edgePen, x, y, x + columnWidth, y);
            }

            return;
        }

        if (count < 2)
        {
            return;
        }

        var points = new PointF[count];
        for (int i = 0; i < count; i++)
        {
            points[i] = new PointF(chart.Left + (i * step), YFor(SampleAt(samples, count, write, i), max, chart));
        }

        using (var area = new GraphicsPath())
        using (var edge = new GraphicsPath())
        {
            area.StartFigure();
            area.AddLine(points[0].X, chart.Bottom, points[0].X, points[0].Y);
            for (int i = 1; i < count; i++)
            {
                area.AddLine(points[i - 1], points[i]);
            }

            area.AddLine(points[count - 1].X, points[count - 1].Y, points[count - 1].X, chart.Bottom);
            area.AddLine(points[count - 1].X, chart.Bottom, points[0].X, chart.Bottom);
            area.CloseFigure();
            edge.AddLines(points);

            float top = points[0].Y;
            for (int i = 1; i < count; i++)
            {
                if (points[i].Y < top)
                {
                    top = points[i].Y;
                }
            }

            float bottom = chart.Bottom;
            if (bottom - top < 1f)
            {
                top = bottom - 1f;
            }

            using (var gradient = new LinearGradientBrush(
                       new PointF(chart.Left, bottom),
                       new PointF(chart.Left, top),
                       Color.FromArgb(160, fill),
                       Color.FromArgb(50, fill)))
            using (var edgePen = new Pen(ContrastEdge(fill, text), 1.75f))
            {
                g.FillPath(gradient, area);
                g.DrawPath(edgePen, edge);
            }
        }
    }

    private void AddToBuffer(double[] samples, ref int count, ref int write, ref double peak, double sample)
    {
        bool windowFull = count == _capacity;
        double overwritten = windowFull ? samples[write] : 0;
        samples[write] = sample;
        write = (write + 1) % _capacity;
        if (count < _capacity)
        {
            count++;
        }

        if (count == 1)
        {
            peak = sample;
            return;
        }

        if (sample > peak)
        {
            peak = sample;
        }
        else if (windowFull && overwritten >= peak)
        {
            peak = RecomputePeak(samples, count, write);
        }
    }

    private static double RecomputePeak(double[] samples, int count, int write)
    {
        if (count == 0)
        {
            return 0;
        }

        double peak = SampleAt(samples, count, write, 0);
        for (int i = 1; i < count; i++)
        {
            double sample = SampleAt(samples, count, write, i);
            if (sample > peak)
            {
                peak = sample;
            }
        }

        return peak;
    }

    private void ResizeBuffers(int capacity)
    {
        _speedSamples = ResizeSeries(_speedSamples, _speedCount, _speedWrite, capacity, out _speedCount, out _speedWrite);
        _progressSamples = ResizeSeries(_progressSamples, _progressCount, _progressWrite, capacity, out _progressCount, out _progressWrite);
        _capacity = capacity;
        _speedPeak = RecomputePeak(_speedSamples, _speedCount, _speedWrite);
        _progressPeak = RecomputePeak(_progressSamples, _progressCount, _progressWrite);
        Invalidate();
    }

    private static double[] ResizeSeries(double[] samples, int count, int write, int capacity, out int newCount, out int newWrite)
    {
        var next = new double[capacity];
        int copy = Math.Min(count, capacity);
        for (int i = 0; i < copy; i++)
        {
            next[i] = SampleAt(samples, count, write, count - copy + i);
        }

        newCount = copy;
        newWrite = copy % capacity;
        return next;
    }

    private static double SampleAt(double[] samples, int count, int write, int logicalIndex)
    {
        int capacity = samples.Length;
        int start = count < capacity ? 0 : write;
        int index = (start + logicalIndex) % capacity;
        return samples[index];
    }

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

    private static Color EnsureSeriesContrast(Color fill, Color back)
    {
        if (fill.IsEmpty)
        {
            fill = Color.SeaGreen;
        }

        float fillBrightness = fill.GetBrightness();
        float backBrightness = back.GetBrightness();
        if (backBrightness > 0.55f)
        {
            return fillBrightness > 0.5f ? ControlPaint.Dark(fill, 0.4f) : fill;
        }

        return fillBrightness < 0.55f ? ControlPaint.Light(fill, 0.7f) : fill;
    }

    private static Color ContrastEdge(Color fill, Color text)
    {
        Color edge = ControlPaint.Dark(fill, 0.15f);
        return edge.GetBrightness() > 0.8f ? Color.FromArgb(220, text) : edge;
    }

    private static Color ResolveColor(Color color, Color fallback) =>
        color.IsEmpty ? fallback : color;

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

        return unit == 0
            ? ((ulong)value).ToString(CultureInfo.CurrentCulture) + " B"
            : value.ToString("0.##", CultureInfo.CurrentCulture) + " " + units[unit];
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.PalettePaint -= OnPalettePaint;
        }

        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect.Target = _palette;
        StateCommon.SetRedirector(_paletteRedirect);
        ProgressSeries.SetRedirector(_paletteRedirect);
        ProgressLabel.SetRedirector(_paletteRedirect);
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        Invalidate();
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

    private void OnNeedPaintHandler(object? sender, NeedLayoutEventArgs e) => Invalidate();
}