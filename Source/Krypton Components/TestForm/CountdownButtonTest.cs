#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of the KryptonCountdownButton control.
/// </summary>
public partial class CountdownButtonTest : KryptonForm
{
    public CountdownButtonTest()
    {
        InitializeComponent();
        SetupDemo();
    }

    private void SetupDemo()
    {
        // Setup event handlers
        btnStartCountdown1.Click += (s, e) => btnCountdown1.StartCountdown();
        btnStartCountdown2.Click += (s, e) => btnCountdown2.StartCountdown();
        btnStartCountdown3.Click += (s, e) => btnCountdown3.StartCountdown();
        btnStartCountdown4.Click += (s, e) => btnCountdown4.StartCountdown();
        btnStartCountdown5.Click += (s, e) => btnCountdown5.StartCountdown();
        btnStartAll.Click += BtnStartAll_Click;

        btnReset1.Click += (s, e) => btnCountdown1.ResetCountdown();
        btnReset2.Click += (s, e) => btnCountdown2.ResetCountdown();
        btnReset3.Click += (s, e) => btnCountdown3.ResetCountdown();
        btnReset4.Click += (s, e) => btnCountdown4.ResetCountdown();
        btnReset5.Click += (s, e) => btnCountdown5.ResetCountdown();
        btnResetAll.Click += BtnResetAll_Click;

        // Setup countdown finished events
        btnCountdown1.CountdownFinished += (s, e) => AddLog("Countdown 1 finished!");
        btnCountdown2.CountdownFinished += (s, e) => AddLog("Countdown 2 finished!");
        btnCountdown3.CountdownFinished += (s, e) => AddLog("Countdown 3 finished!");
        btnCountdown4.CountdownFinished += (s, e) => AddLog("Countdown 4 finished!");
        btnCountdown5.CountdownFinished += (s, e) => AddLog("Countdown 5 finished!");

        // Setup countdown error events
        btnCountdown1.CountdownError += (s, e) => AddLog($"Countdown 1 error: {e}");
        btnCountdown2.CountdownError += (s, e) => AddLog($"Countdown 2 error: {e}");
        btnCountdown3.CountdownError += (s, e) => AddLog($"Countdown 3 error: {e}");
        btnCountdown4.CountdownError += (s, e) => AddLog($"Countdown 4 error: {e}");
        btnCountdown5.CountdownError += (s, e) => AddLog($"Countdown 5 error: {e}");

        // Setup click handlers to show that button clicks work after countdown
        btnCountdown1.Click += (s, e) => AddLog("Countdown 1 clicked!");
        btnCountdown2.Click += (s, e) => AddLog("Countdown 2 clicked!");
        btnCountdown3.Click += (s, e) => AddLog("Countdown 3 clicked!");
        btnCountdown4.Click += (s, e) => AddLog("Countdown 4 clicked!");
        btnCountdown5.Click += (s, e) => AddLog("Countdown 5 clicked!");

        // Setup property change handlers
        numDuration1.ValueChanged += (s, e) => btnCountdown1.CountdownButtonValues.CountdownDuration = (int)numDuration1.Value;
        numDuration2.ValueChanged += (s, e) => btnCountdown2.CountdownButtonValues.CountdownDuration = (int)numDuration2.Value;
        numDuration3.ValueChanged += (s, e) => btnCountdown3.CountdownButtonValues.CountdownDuration = (int)numDuration3.Value;
        numInterval1.ValueChanged += (s, e) => btnCountdown3.CountdownButtonValues.CountdownInterval = (int)numInterval1.Value;

        // Setup format change handlers
        txtFormat1.TextChanged += (s, e) =>
        {
            try
            {
                btnCountdown3.CountdownButtonValues.CountdownTextFormat = txtFormat1.Text;
                lblFormatError1.Values.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblFormatError1.Values.Text = $"Error: {ex.Message}";
            }
        };

        // Setup suffix change handler
        txtSuffix1.TextChanged += (s, e) => btnCountdown3.CountdownButtonValues.CountdownSecondSuffix = txtSuffix1.Text ?? "s";

        // Setup EnableButtonAtZero checkbox
        chkEnableAtZero.CheckedChanged += (s, e) => btnCountdown5.CountdownButtonValues.EnableButtonAtZero = chkEnableAtZero.Checked;

        AddLog("Countdown Button Demo initialized. Use the controls above to test various features.");
    }

    private void BtnStartAll_Click(object? sender, EventArgs e)
    {
        btnCountdown1.StartCountdown();
        btnCountdown2.StartCountdown();
        btnCountdown3.StartCountdown();
        btnCountdown4.StartCountdown();
        btnCountdown5.StartCountdown();
        AddLog("All countdowns started!");
    }

    private void BtnResetAll_Click(object? sender, EventArgs e)
    {
        btnCountdown1.ResetCountdown();
        btnCountdown2.ResetCountdown();
        btnCountdown3.ResetCountdown();
        btnCountdown4.ResetCountdown();
        btnCountdown5.ResetCountdown();
        AddLog("All countdowns reset!");
    }

    private void AddLog(string message)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => AddLog(message)));
            return;
        }

        var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        txtLog.AppendText($"[{timestamp}] {message}\r\n");
        txtLog.SelectionStart = txtLog.Text.Length;
        txtLog.ScrollToCaret();
    }

    private void BtnClearLog_Click(object? sender, EventArgs e)
    {
        txtLog.Text = string.Empty;
        AddLog("Log cleared.");
    }
}
