#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of the KryptonProgressBar taskbar progress integration
/// via <see cref="KryptonProgressBar.UseTaskbarProgress"/> and
/// <see cref="KryptonProgressBar.TaskbarProgressState"/> (issue #2890).
///
/// Scenarios covered:
///  1. Basic enable/disable toggle
///  2. Simulated download with a Timer (Normal state, live value updates)
///  3. Manual value slider
///  4. All ProgressBarStyles (Continuous, Blocks, Marquee)
///  5. All KryptonTaskbarProgressState overrides (Normal, Error, Paused, Indeterminate, NoProgress)
///  6. Minimum/Maximum range customisation
/// </summary>
public partial class TaskbarProgressBarDemo : KryptonForm
{
    private readonly System.Windows.Forms.Timer _simulationTimer;
    private bool _simulationRunning;

    public TaskbarProgressBarDemo()
    {
        InitializeComponent();

        _simulationTimer = new System.Windows.Forms.Timer { Interval = 80 };
        _simulationTimer.Tick += OnSimulationTick;
    }

    private void TaskbarProgressBarDemo_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;

        PopulateStyleCombo();
        PopulateStateCombo();

        kryptonTrackBar1.Minimum = 0;
        kryptonTrackBar1.Maximum = 100;
        kryptonTrackBar1.Value = 0;
        kryptonTrackBar1.SmallChange = 1;
        kryptonTrackBar1.LargeChange = 10;
        kryptonTrackBar1.TickFrequency = 10;
        kryptonTrackBar1.ValueChanged += OnTrackBarValueChanged;

        kryptonProgressBar1.Minimum = 0;
        kryptonProgressBar1.Maximum = 100;
        kryptonProgressBar1.Value = 0;
        kryptonProgressBar1.UseTaskbarProgress = kchkEnableTaskbar.Checked;

        UpdateValueLabel(0);
        UpdateStatusLabel();
        UpdateButtonStates();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _simulationTimer.Stop();
        _simulationTimer.Dispose();
        base.OnFormClosed(e);
    }

    // ── Scenario 1: Enable/Disable toggle ────────────────────────────────────

    private void kchkEnableTaskbar_CheckedChanged(object? sender, EventArgs e)
    {
        kryptonProgressBar1.UseTaskbarProgress = kchkEnableTaskbar.Checked;
        UpdateStatusLabel();
    }

    // ── Scenario 2: Simulated download ───────────────────────────────────────

    private void kbtnStartSimulation_Click(object? sender, EventArgs e)
    {
        if (_simulationRunning)
        {
            return;
        }

        kryptonProgressBar1.Value = 0;
        kryptonTrackBar1.Value = 0;
        kryptonProgressBar1.Style = ProgressBarStyle.Continuous;
        kryptonProgressBar1.TaskbarProgressState = KryptonTaskbarProgressState.Normal;
        kcmbStyle.SelectedIndex = 0;
        kcmbState.SelectedIndex = 2;

        _simulationRunning = true;
        _simulationTimer.Start();
        UpdateButtonStates();
        UpdateStatusLabel();
    }

    private void kbtnStopSimulation_Click(object? sender, EventArgs e)
    {
        StopSimulation();
    }

    private void OnSimulationTick(object? sender, EventArgs e)
    {
        int next = kryptonProgressBar1.Value + 1;

        if (next >= kryptonProgressBar1.Maximum)
        {
            next = kryptonProgressBar1.Maximum;
            StopSimulation();
        }

        kryptonProgressBar1.Value = next;
        kryptonTrackBar1.Value = next;
        UpdateValueLabel(next);
    }

    private void StopSimulation()
    {
        _simulationTimer.Stop();
        _simulationRunning = false;
        UpdateButtonStates();
        UpdateStatusLabel();
    }

    // ── Scenario 3: Manual slider ─────────────────────────────────────────────

    private void OnTrackBarValueChanged(object? sender, EventArgs e)
    {
        if (_simulationRunning)
        {
            return;
        }

        kryptonProgressBar1.Value = kryptonTrackBar1.Value;
        UpdateValueLabel(kryptonTrackBar1.Value);
    }

    // ── Scenario 4: ProgressBarStyle ─────────────────────────────────────────

    private void PopulateStyleCombo()
    {
        kcmbStyle.Items.Clear();
        kcmbStyle.Items.Add(ProgressBarStyle.Continuous);
        kcmbStyle.Items.Add(ProgressBarStyle.Blocks);
        kcmbStyle.Items.Add(ProgressBarStyle.Marquee);
        kcmbStyle.SelectedIndex = 0;
    }

    private void kcmbStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbStyle.SelectedItem is ProgressBarStyle style)
        {
            kryptonProgressBar1.Style = style;
        }
    }

    // ── Scenario 5: KryptonTaskbarProgressState overrides ────────────────────

    private void PopulateStateCombo()
    {
        kcmbState.Items.Clear();
        kcmbState.Items.Add(KryptonTaskbarProgressState.NoProgress);
        kcmbState.Items.Add(KryptonTaskbarProgressState.Indeterminate);
        kcmbState.Items.Add(KryptonTaskbarProgressState.Normal);
        kcmbState.Items.Add(KryptonTaskbarProgressState.Error);
        kcmbState.Items.Add(KryptonTaskbarProgressState.Paused);
        kcmbState.SelectedIndex = 2;
    }

    private void kcmbState_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbState.SelectedItem is KryptonTaskbarProgressState state)
        {
            kryptonProgressBar1.TaskbarProgressState = state;
            UpdateStatusLabel();
        }
    }

    private void kbtnSetError_Click(object? sender, EventArgs e)
    {
        kryptonProgressBar1.TaskbarProgressState = KryptonTaskbarProgressState.Error;
        kcmbState.SelectedItem = KryptonTaskbarProgressState.Error;
        UpdateStatusLabel();
    }

    private void kbtnSetPaused_Click(object? sender, EventArgs e)
    {
        kryptonProgressBar1.TaskbarProgressState = KryptonTaskbarProgressState.Paused;
        kcmbState.SelectedItem = KryptonTaskbarProgressState.Paused;
        UpdateStatusLabel();
    }

    private void kbtnSetNormal_Click(object? sender, EventArgs e)
    {
        kryptonProgressBar1.TaskbarProgressState = KryptonTaskbarProgressState.Normal;
        kcmbState.SelectedItem = KryptonTaskbarProgressState.Normal;
        UpdateStatusLabel();
    }

    private void kbtnClearProgress_Click(object? sender, EventArgs e)
    {
        kryptonProgressBar1.UseTaskbarProgress = false;
        kchkEnableTaskbar.Checked = false;
        UpdateStatusLabel();
    }

    // ── Scenario 6: Min/Max range ─────────────────────────────────────────────

    private void knudMinimum_ValueChanged(object? sender, EventArgs e)
    {
        int min = (int)knudMinimum.Value;
        if (min >= kryptonProgressBar1.Maximum)
        {
            return;
        }

        kryptonProgressBar1.Minimum = min;
        kryptonTrackBar1.Minimum = min;

        if (kryptonProgressBar1.Value < min)
        {
            kryptonProgressBar1.Value = min;
            kryptonTrackBar1.Value = min;
        }

        UpdateValueLabel(kryptonProgressBar1.Value);
    }

    private void knudMaximum_ValueChanged(object? sender, EventArgs e)
    {
        int max = (int)knudMaximum.Value;
        if (max <= kryptonProgressBar1.Minimum)
        {
            return;
        }

        kryptonProgressBar1.Maximum = max;
        kryptonTrackBar1.Maximum = max;

        if (kryptonProgressBar1.Value > max)
        {
            kryptonProgressBar1.Value = max;
            kryptonTrackBar1.Value = max;
        }

        UpdateValueLabel(kryptonProgressBar1.Value);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private void UpdateValueLabel(int value)
    {
        klblValue.Text = $"Value: {value} / {kryptonProgressBar1.Maximum}";
    }

    private void UpdateStatusLabel()
    {
        string taskbarPart = kryptonProgressBar1.UseTaskbarProgress
            ? $"Taskbar: ON  |  State: {kryptonProgressBar1.TaskbarProgressState}"
            : "Taskbar: OFF";
        string simPart = _simulationRunning ? "  |  Simulation: Running" : string.Empty;
        klblStatus.Text = $"{taskbarPart}{simPart}";
    }

    private void UpdateButtonStates()
    {
        kbtnStartSimulation.Enabled = !_simulationRunning;
        kbtnStopSimulation.Enabled = _simulationRunning;
        kryptonTrackBar1.Enabled = !_simulationRunning;
    }
}
