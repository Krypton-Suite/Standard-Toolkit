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
/// Combines a scrolling transfer-speed chart with an optional progress chart.
/// </summary>
/// <remarks>
/// Part of <c>Krypton.Toolkit.Utilities</c>. Hosted by the application updater progress
/// window when <see cref="KryptonUpdateProgressDisplay.TransferSpeedGraph"/> is selected.
/// </remarks>
[ToolboxItem(true)]
[DefaultProperty(nameof(Graph))]
[Description("Shows transfer speed over time beside an optional progress chart.")]
[DesignerCategory(@"code")]
public class KryptonTransferProgressPanel : UserControl
{
    private const int DefaultProgressColumnWidth = 220;
    private const int DefaultHeight = 110;

    private readonly TableLayoutPanel _layout;
    private readonly KryptonTransferSpeedGraph _graph;
    private readonly KryptonTransferProgressGraph _progressGraph;
    private bool _showProgressGraph = true;
    private TransferProgressSampleMode _progressSampleMode = TransferProgressSampleMode.Auto;

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
    }

    /// <summary>Gets the scrolling transfer-speed chart.</summary>
    [Category(@"Visuals")]
    [Description(@"The transfer-speed area chart.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTransferSpeedGraph Graph => _graph;

    private bool ShouldSerializeGraph() => !_graph.Values.IsDefault;

    private void ResetGraph() => _graph.Values.Reset();

    /// <summary>Gets the scrolling transfer-progress chart.</summary>
    [Category(@"Visuals")]
    [Description(@"The transfer-progress area chart.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTransferProgressGraph ProgressGraph => _progressGraph;

    private bool ShouldSerializeProgressGraph() => !_progressGraph.Values.IsDefault;

    private void ResetProgressGraph() => _progressGraph.Values.Reset();

    /// <summary>Gets or sets whether the progress chart column is shown.</summary>
    [Category(@"Behavior")]
    [Description(@"Shows the secondary progress chart beside the speed chart.")]
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

        if (usePercent)
        {
            if (_progressGraph.Values.SampleDisplay != TransferGraphSampleDisplay.Percent)
            {
                _progressGraph.Values.SampleDisplay = TransferGraphSampleDisplay.Percent;
            }

            double percent = 0d;
            if (total.HasValue && total.Value > 0)
            {
                percent = Math.Min(100d, received * 100d / total.Value);
                if (percent < 0d)
                {
                    percent = 0d;
                }
            }

            _progressGraph.AddSample(percent);
            return;
        }

        if (_progressGraph.Values.SampleDisplay != TransferGraphSampleDisplay.Bytes)
        {
            _progressGraph.Values.SampleDisplay = TransferGraphSampleDisplay.Bytes;
        }

        _progressGraph.AddSample(received < 0 ? 0d : received);
    }

    /// <summary>Appends one speed sample to the speed chart.</summary>
    /// <param name="bytesPerSecond">Transfer rate for this sample.</param>
    public void AddSample(double bytesPerSecond) => _graph.AddSample(bytesPerSecond);

    /// <summary>Clears both charts.</summary>
    public void Clear()
    {
        _graph.Clear();
        _progressGraph.Clear();
    }

    /// <summary>Stops accepting samples so both charts stay frozen.</summary>
    public void Pause()
    {
        _graph.Pause();
        _progressGraph.Pause();
    }

    /// <summary>Starts accepting samples again after <see cref="Pause"/>.</summary>
    public void Resume()
    {
        _graph.Resume();
        _progressGraph.Resume();
    }

    private void ApplyProgressVisibility()
    {
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
