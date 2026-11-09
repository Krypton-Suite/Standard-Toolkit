#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Combines transfer-speed and progress charts, either side by side or as one dual-series chart.
/// </summary>
/// <remarks>
/// Part of <c>Krypton.Toolkit.Utilities</c>. Hosted by the application updater progress
/// window when <see cref="KryptonUpdateProgressDisplay.TransferSpeedGraph"/> is selected.
/// </remarks>
[ToolboxItem(true)]
[DefaultProperty(nameof(Graph))]
[Description("Shows transfer speed and optional progress as side-by-side or dual-series charts.")]
[DesignerCategory(@"code")]
public class KryptonTransferProgressPanel : UserControl
{
    private const int DefaultProgressColumnWidth = 220;
    private const int DefaultHeight = 110;

    private readonly TableLayoutPanel _layout;
    private readonly KryptonTransferSpeedGraph _graph;
    private readonly KryptonTransferProgressGraph _progressGraph;
    private readonly KryptonTransferDualGraph _dualGraph;
    private bool _showProgressGraph = true;
    private TransferProgressSampleMode _progressSampleMode = TransferProgressSampleMode.Auto;
    private TransferProgressChartLayout _chartLayout = TransferProgressChartLayout.SideBySide;

    /// <summary>Creates an empty panel with speed and progress charts.</summary>
    public KryptonTransferProgressPanel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        Size = new Size(480, DefaultHeight);
        TabStop = false;

        _graph = new KryptonTransferSpeedGraph
        {
            Dock = DockStyle.Fill,
            TabStop = false
        };

        _progressGraph = new KryptonTransferProgressGraph
        {
            Dock = DockStyle.Fill,
            TabStop = false
        };

        _dualGraph = new KryptonTransferDualGraph
        {
            Dock = DockStyle.Fill,
            TabStop = false,
            Visible = false
        };

        _layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = Color.Transparent,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, DefaultProgressColumnWidth));
        _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        _layout.Controls.Add(_graph, 0, 0);
        _layout.Controls.Add(_progressGraph, 1, 0);
        Controls.Add(_layout);
        Controls.Add(_dualGraph);
    }

    /// <summary>Gets the scrolling transfer-speed chart (side-by-side layout).</summary>
    [Category(@"Visuals")]
    [Description(@"The transfer-speed area chart used in SideBySide layout.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTransferSpeedGraph Graph => _graph;

    private bool ShouldSerializeGraph() => !_graph.Values.IsDefault;

    private void ResetGraph() => _graph.Values.Reset();

    /// <summary>Gets the scrolling transfer-progress chart (side-by-side layout).</summary>
    [Category(@"Visuals")]
    [Description(@"The transfer-progress area chart used in SideBySide layout.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTransferProgressGraph ProgressGraph => _progressGraph;

    private bool ShouldSerializeProgressGraph() => !_progressGraph.Values.IsDefault;

    private void ResetProgressGraph() => _progressGraph.Values.Reset();

    /// <summary>Gets the dual-series chart (dual-series layout).</summary>
    [Category(@"Visuals")]
    [Description(@"The dual-series chart used in DualSeries layout.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTransferDualGraph DualGraph => _dualGraph;

    private bool ShouldSerializeDualGraph() => !_dualGraph.Values.IsDefault;

    private void ResetDualGraph() => _dualGraph.Values.Reset();

    /// <summary>Gets or sets how speed and progress charts are arranged.</summary>
    [Category(@"Appearance")]
    [Description(@"Side-by-side charts or a single dual-series chart.")]
    [DefaultValue(TransferProgressChartLayout.SideBySide)]
    public TransferProgressChartLayout ChartLayout
    {
        get => _chartLayout;
        set
        {
            if (_chartLayout == value)
            {
                return;
            }

            _chartLayout = value;
            ApplyLayout();
        }
    }

    /// <summary>Gets or sets whether the progress chart / series is shown.</summary>
    [Category(@"Behavior")]
    [Description(@"Shows the secondary progress chart or dual-series progress series.")]
    [DefaultValue(true)]
    public bool ShowProgressGraph
    {
        get => _showProgressGraph;
        set
        {
            if (_showProgressGraph == value)
            {
                return;
            }

            _showProgressGraph = value;
            ApplyProgressVisibility();
        }
    }

    /// <summary>Gets or sets how <see cref="SetProgress"/> samples the progress chart.</summary>
    [Category(@"Behavior")]
    [Description(@"Percent, bytes, or auto (percent when total is known).")]
    [DefaultValue(TransferProgressSampleMode.Auto)]
    public TransferProgressSampleMode ProgressSampleMode
    {
        get => _progressSampleMode;
        set => _progressSampleMode = value;
    }

    /// <summary>
    /// Appends a progress sample from bytes received and optional total length.
    /// </summary>
    /// <param name="received">Bytes written so far.</param>
    /// <param name="total">Content length, or null when unknown.</param>
    public void SetProgress(long received, long? total)
    {
        if (!_showProgressGraph)
        {
            return;
        }

        var usePercent = _progressSampleMode == TransferProgressSampleMode.Percent ||
                         (_progressSampleMode == TransferProgressSampleMode.Auto && total.HasValue && total.Value > 0);

        double sample;
        TransferGraphSampleDisplay display;
        if (usePercent)
        {
            display = TransferGraphSampleDisplay.Percent;
            sample = 0d;
            if (total.HasValue && total.Value > 0)
            {
                sample = Math.Min(100d, received * 100d / total.Value);
                if (sample < 0d)
                {
                    sample = 0d;
                }
            }
        }
        else
        {
            display = TransferGraphSampleDisplay.Bytes;
            sample = received < 0 ? 0d : received;
        }

        if (_chartLayout == TransferProgressChartLayout.DualSeries)
        {
            if (_dualGraph.Values.ProgressSampleDisplay != display)
            {
                _dualGraph.Values.ProgressSampleDisplay = display;
            }

            _dualGraph.AddProgressSample(sample);
            return;
        }

        if (_progressGraph.Values.SampleDisplay != display)
        {
            _progressGraph.Values.SampleDisplay = display;
        }

        _progressGraph.AddSample(sample);
    }

    /// <summary>Appends one speed sample.</summary>
    /// <param name="bytesPerSecond">Transfer rate for this sample.</param>
    public void AddSample(double bytesPerSecond)
    {
        if (_chartLayout == TransferProgressChartLayout.DualSeries)
        {
            _dualGraph.AddSpeedSample(bytesPerSecond);
            return;
        }

        _graph.AddSample(bytesPerSecond);
    }

    /// <summary>Clears all charts.</summary>
    public void Clear()
    {
        _graph.Clear();
        _progressGraph.Clear();
        _dualGraph.Clear();
    }

    /// <summary>Stops accepting samples so charts stay frozen.</summary>
    public void Pause()
    {
        _graph.Pause();
        _progressGraph.Pause();
        _dualGraph.Pause();
    }

    /// <summary>Starts accepting samples again after <see cref="Pause"/>.</summary>
    public void Resume()
    {
        _graph.Resume();
        _progressGraph.Resume();
        _dualGraph.Resume();
    }

    private void ApplyLayout()
    {
        bool dual = _chartLayout == TransferProgressChartLayout.DualSeries;
        _layout.Visible = !dual;
        _dualGraph.Visible = dual;
        if (dual)
        {
            _dualGraph.BringToFront();
        }

        ApplyProgressVisibility();
    }

    private void ApplyProgressVisibility()
    {
        _dualGraph.ShowProgressSeries = _showProgressGraph;
        _progressGraph.Visible = _showProgressGraph;
        if (_showProgressGraph)
        {
            _layout.ColumnStyles[1].Width = DefaultProgressColumnWidth;
            _layout.ColumnStyles[1].SizeType = SizeType.Absolute;
        }
        else
        {
            _layout.ColumnStyles[1].Width = 0;
            _layout.ColumnStyles[1].SizeType = SizeType.Absolute;
        }
    }
}
