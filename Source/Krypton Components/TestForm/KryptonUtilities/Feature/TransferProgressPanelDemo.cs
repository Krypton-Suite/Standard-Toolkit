#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Exercises the Toolbox <see cref="KryptonTransferProgressPanel"/> composite (speed + optional progress graphs).
/// </summary>
public partial class TransferProgressPanelDemo : KryptonForm
{
    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
    private long _received;
    private long _total = 10_000_000;
    private readonly Random _random = new Random();

    public TransferProgressPanelDemo()
    {
        InitializeComponent();
        _timer.Interval = 280;
        _timer.Tick += OnTick;
    }

    private void TransferProgressPanelDemo_Load(object? sender, EventArgs e)
    {
        kcmbMode.Items.Clear();
        foreach (TransferProgressSampleMode mode in Enum.GetValues(typeof(TransferProgressSampleMode)))
        {
            kcmbMode.Items.Add(mode);
        }

        kcmbMode.SelectedItem = TransferProgressSampleMode.Auto;
        chkShowProgress.Checked = transferProgressPanel.ShowProgressGraph;
        transferProgressPanel.ProgressGraph.Values.SpeedLabelFormat = "Progress: {0}";
        transferProgressPanel.Clear();
        UpdateStatus();
    }

    private void btnStart_Click(object? sender, EventArgs e)
    {
        _received = 0;
        transferProgressPanel.Clear();
        transferProgressPanel.Resume();
        _timer.Start();
        btnStart.Enabled = false;
        btnPause.Enabled = true;
        btnResume.Enabled = false;
        UpdateStatus();
    }

    private void btnPause_Click(object? sender, EventArgs e)
    {
        transferProgressPanel.Pause();
        _timer.Stop();
        btnPause.Enabled = false;
        btnResume.Enabled = true;
        UpdateStatus();
    }

    private void btnResume_Click(object? sender, EventArgs e)
    {
        transferProgressPanel.Resume();
        _timer.Start();
        btnPause.Enabled = true;
        btnResume.Enabled = false;
        UpdateStatus();
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        _timer.Stop();
        _received = 0;
        transferProgressPanel.Clear();
        btnStart.Enabled = true;
        btnPause.Enabled = false;
        btnResume.Enabled = false;
        UpdateStatus();
    }

    private void chkUnknownTotal_CheckedChanged(object? sender, EventArgs e) => UpdateStatus();

    private void chkShowProgress_CheckedChanged(object? sender, EventArgs e)
    {
        transferProgressPanel.ShowProgressGraph = chkShowProgress.Checked;
        UpdateStatus();
    }

    private void kcmbMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbMode.SelectedItem is TransferProgressSampleMode mode)
        {
            transferProgressPanel.ProgressSampleMode = mode;
            UpdateStatus();
        }
    }

    private void OnTick(object? sender, EventArgs e)
    {
        var delta = 180_000 + _random.Next(0, 80_000);
        _received += delta;
        if (_received > _total)
        {
            _received = _total;
        }

        long? total = chkUnknownTotal.Checked ? (long?)null : _total;
        transferProgressPanel.SetProgress(_received, total);
        transferProgressPanel.AddSample(delta / 0.28d);
        UpdateStatus();

        if (_received >= _total && !chkUnknownTotal.Checked)
        {
            _timer.Stop();
            transferProgressPanel.Pause();
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnResume.Enabled = false;
        }
    }

    private void UpdateStatus()
    {
        var totalText = chkUnknownTotal.Checked ? "unknown" : _total.ToString("N0");
        kwlblStatus.Text =
            $"Received {_received:N0} of {totalText} bytes. Mode={transferProgressPanel.ProgressSampleMode}; " +
            $"ShowProgress={transferProgressPanel.ShowProgressGraph}; Theme={KryptonManager.CurrentGlobalPaletteMode}.";
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _timer.Stop();
        _timer.Dispose();
        base.OnFormClosed(e);
    }
}
