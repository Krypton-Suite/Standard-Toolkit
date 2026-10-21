#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class TimerTest : KryptonForm
{
    private KryptonTimer? _timer;
    private DateTime _startTime;
    private int _tickCount;

    public TimerTest()
    {
        InitializeComponent();
        InitializeTimer();
        UpdateUIState();
    }

    private void InitializeTimer()
    {
        _timer = new KryptonTimer(this.components)
        {
            Interval = 1000,
            Enabled = false
        };

        _timer.Tick += OnTimerTick;

        // Set default values
        knudInterval.Value = 1000;
        ktxtInterval.Text = "1000";

        // Populate palette mode combo
        InitializeComboBoxes();
    }

    private void InitializeComboBoxes()
    {
        kcmbPaletteMode.Items.Clear();
        kcmbPaletteMode.Items.Add("Global");
        kcmbPaletteMode.Items.Add("Professional System");
        kcmbPaletteMode.Items.Add("Office 2003");
        kcmbPaletteMode.Items.Add("Office 2007");
        kcmbPaletteMode.Items.Add("Office 2010");
        kcmbPaletteMode.Items.Add("Office 2013");
        kcmbPaletteMode.Items.Add("Sparkle Blue");
        kcmbPaletteMode.Items.Add("Sparkle Orange");
        kcmbPaletteMode.Items.Add("Sparkle Purple");
        kcmbPaletteMode.SelectedIndex = 0; // "Global"
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _tickCount++;
        var elapsed = DateTime.Now - _startTime;

        klblTickCount.Text = $"Tick Count: {_tickCount}";
        klblElapsedTime.Text = $"Elapsed Time: {elapsed:hh\\:mm\\:ss\\.fff}";
        klblLastTick.Text = $"Last Tick: {DateTime.Now:HH:mm:ss.fff}";

        // Add to event list
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Tick #{_tickCount}");

        // Update progress bar if enabled
        if (kchkUpdateProgressBar.Checked)
        {
            var intervalMs = (double)knudInterval.Value;
            var progress = (int)((elapsed.TotalMilliseconds % (intervalMs * 10)) / (intervalMs * 10) * 100);
            kprogressBar.Value = Math.Min(progress, 100);
        }
    }

    private void AddEventToList(string message)
    {
        if (klstEvents.InvokeRequired)
        {
            klstEvents.Invoke(new Action(() => AddEventToList(message)));
            return;
        }

        klstEvents.Items.Add(message);

        // Auto-scroll to bottom
        if (klstEvents.Items.Count > 0)
        {
            klstEvents.SelectedIndex = klstEvents.Items.Count - 1;
        }

        // Limit to 1000 items
        if (klstEvents.Items.Count > 1000)
        {
            klstEvents.Items.RemoveAt(0);
        }
    }

    private void UpdateUIState()
    {
        var isRunning = _timer?.Enabled ?? false;

        kbtnStart.Enabled = !isRunning;
        kbtnStop.Enabled = isRunning;
        kbtnReset.Enabled = isRunning || _tickCount > 0;
        kbtnClearEvents.Enabled = klstEvents.Items.Count > 0;

        knudInterval.Enabled = !isRunning;
        ktxtInterval.Enabled = !isRunning;

        klblStatus.Text = isRunning ? "Running" : "Stopped";
        klblStatus.StateCommon.ShortText.Color1 = isRunning ? Color.Green : Color.Gray;

        if (_timer != null)
        {
            klblIntervalStatus.Text = $"Interval: {_timer.Interval} ms";
            klblEnabled.Text = $"Enabled: {_timer.Enabled}";
        }
    }

    private void kbtnStart_Click(object sender, EventArgs e)
    {
        if (_timer == null)
        {
            return;
        }

        try
        {
            var interval = (int)knudInterval.Value;
            if (interval < 1)
            {
                KryptonMessageBox.Show(this,
                    "Interval must be at least 1 millisecond.",
                    "Invalid Interval",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Warning);
                return;
            }

            _timer.Interval = interval;
            _timer.Enabled = true;
            _startTime = DateTime.Now;

            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Timer started (Interval: {interval} ms)");
            UpdateUIState();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this,
                $"Error starting timer:\n{ex.Message}",
                "Error",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private void kbtnStop_Click(object sender, EventArgs e)
    {
        if (_timer != null)
        {
            _timer.Enabled = false;
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Timer stopped");
            UpdateUIState();
        }
    }

    private void kbtnReset_Click(object sender, EventArgs e)
    {
        _tickCount = 0;
        _startTime = DateTime.Now;

        klblTickCount.Text = "Tick Count: 0";
        klblElapsedTime.Text = "Elapsed Time: 00:00:00.000";
        klblLastTick.Text = "Last Tick: --";
        kprogressBar.Value = 0;

        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Timer reset");
        UpdateUIState();
    }

    private void kbtnClearEvents_Click(object sender, EventArgs e)
    {
        klstEvents.Items.Clear();
        UpdateUIState();
    }

    private void knudInterval_ValueChanged(object sender, EventArgs e)
    {
        if (_timer != null && !_timer.Enabled)
        {
            _timer.Interval = (int)knudInterval.Value;
            ktxtInterval.Text = knudInterval.Value.ToString();
            UpdateUIState();
        }
    }

    private void ktxtInterval_TextChanged(object sender, EventArgs e)
    {
        if (int.TryParse(ktxtInterval.Text, out var interval) && interval >= 1)
        {
            if (_timer != null && !_timer.Enabled)
            {
                _timer.Interval = interval;
                knudInterval.Value = interval;
                UpdateUIState();
            }
        }
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_timer == null)
        {
            return;
        }

        var selected = kcmbPaletteMode.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        _timer.PaletteMode = selected switch
        {
            "Global" => PaletteMode.Global,
            "Professional System" => PaletteMode.ProfessionalSystem,
            "Office 2003" => PaletteMode.ProfessionalOffice2003,
            "Office 2007" => PaletteMode.Office2007Blue,
            "Office 2010" => PaletteMode.Office2010Blue,
            "Office 2013" => PaletteMode.Office2013White,
            "Sparkle Blue" => PaletteMode.SparkleBlue,
            "Sparkle Orange" => PaletteMode.SparkleOrange,
            "Sparkle Purple" => PaletteMode.SparklePurple,
            _ => PaletteMode.Global
        };
    }

    private void kchkUpdateProgressBar_CheckedChanged(object sender, EventArgs e)
    {
        kprogressBar.Visible = kchkUpdateProgressBar.Checked;
        if (!kchkUpdateProgressBar.Checked)
        {
            kprogressBar.Value = 0;
        }
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _timer?.Dispose();
        base.OnFormClosing(e);
    }
}

