#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating touchscreen support functionality.
/// Shows how controls scale when touchscreen support is enabled and allows
/// real-time adjustment of scale factors.
/// </summary>
public partial class TouchscreenSupportTest : KryptonForm
{
    private bool _updatingFromEvent;

    public TouchscreenSupportTest()
    {
        InitializeComponent();
        InitializeForm();
    }

    private void InitializeForm()
    {
        // Subscribe to touchscreen support changes
        KryptonManager.GlobalTouchscreenSupportChanged += OnGlobalTouchscreenSupportChanged;

        // Initialize UI with current settings
        UpdateUIFromSettings();

        // Setup demo controls
        SetupDemoControls();

        // Setup event handlers
        chkEnableTouchscreen.CheckedChanged += ChkEnableTouchscreen_CheckedChanged;
        trackScaleFactor.ValueChanged += TrackScaleFactor_ValueChanged;
        btnResetScale.Click += BtnResetScale_Click;
        btnApplyPreset25.Click += BtnApplyPreset25_Click;
        btnApplyPreset50.Click += BtnApplyPreset50_Click;
        btnApplyPreset75.Click += BtnApplyPreset75_Click;
        btnToggle.Click += BtnToggle_Click;

        // Update status
        UpdateStatus();
    }

    private void SetupDemoControls()
    {
        // Button examples
        btnStandard.Text = "Standard Button";
        btnStandard.Click += (s, e) => KryptonMessageBox.Show("Standard button clicked!", "Touchscreen Test");

        btnPrimary.Text = "Primary Button";
        btnPrimary.ButtonStyle = ButtonStyle.Command;
        btnPrimary.Click += (s, e) => KryptonMessageBox.Show("Primary button clicked!", "Touchscreen Test");

        btnSuccess.Text = "Success Button";
        btnSuccess.StateCommon.Content.ShortText.Color1 = Color.Green;
        btnSuccess.Click += (s, e) => KryptonMessageBox.Show("Success button clicked!", "Touchscreen Test");

        // Checkbox examples
        chkOption1.Text = "Option 1";
        chkOption2.Text = "Option 2";
        chkOption3.Text = "Option 3";

        // Radio button examples
        radioOption1.Text = "Radio Option A";
        radioOption2.Text = "Radio Option B";
        radioOption3.Text = "Radio Option C";
        radioOption1.Checked = true;

        // Text input examples
        txtInput.Text = "Sample text input";
        txtInput.Watermark.Text = "Enter text here...";
        txtNumeric.Value = 42;

        // ComboBox example
        cmbOptions.Items.AddRange(new[] { "Option 1", "Option 2", "Option 3", "Option 4" });
        cmbOptions.SelectedIndex = 0;

        // Progress bar
        progressBar.Value = 65;
        progressBar.StateCommon.LongText.Color1 = Color.Blue;

        // Track bar
        trackBar.Value = 50;
        trackBar.Minimum = 0;
        trackBar.Maximum = 100;
        trackBar.TickFrequency = 10;

        // Label examples
        lblInfo.Text = "This label demonstrates text scaling";
        lblInfo.StateCommon.ShortText.Color1 = Color.DarkBlue;

        // Link label
        linkLabel.Text = "Click this link";
        linkLabel.LinkClicked += (s, e) => KryptonMessageBox.Show("Link clicked!", "Touchscreen Test");
    }

    private void ChkEnableTouchscreen_CheckedChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            KryptonManager.GlobalTouchscreenSupport = chkEnableTouchscreen.Checked;
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void TrackScaleFactor_ValueChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            // Convert trackbar value (0-200) to scale factor (1.0 - 3.0)
            float scaleFactor = 1.0f + (trackScaleFactor.Value / 100f);
            KryptonManager.GlobalTouchscreenScaleFactor = scaleFactor;
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnResetScale_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonManager.GlobalTouchscreenScaleFactor = 1.25f; // Default 25% larger
            UpdateUIFromSettings();
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnApplyPreset25_Click(object? sender, EventArgs e)
    {
        ApplyPreset(1.25f, "25% larger");
    }

    private void BtnApplyPreset50_Click(object? sender, EventArgs e)
    {
        ApplyPreset(1.5f, "50% larger");
    }

    private void BtnApplyPreset75_Click(object? sender, EventArgs e)
    {
        ApplyPreset(1.75f, "75% larger");
    }

    private void ApplyPreset(float scaleFactor, string description)
    {
        try
        {
            KryptonManager.GlobalTouchscreenSupport = true;
            KryptonManager.GlobalTouchscreenScaleFactor = scaleFactor;
            UpdateUIFromSettings();
            UpdateStatus();
            KryptonMessageBox.Show($"Applied preset: {description}", "Touchscreen Support", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnToggle_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonManager.GlobalTouchscreenSupport = !KryptonManager.UseTouchscreenSupport;
            UpdateUIFromSettings();
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnGlobalTouchscreenSupportChanged(object? sender, EventArgs e)
    {
        // Update UI when settings change externally
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateUIFromSettings));
            Invoke(new Action(UpdateStatus));
        }
        else
        {
            UpdateUIFromSettings();
            UpdateStatus();
        }
    }

    private void UpdateUIFromSettings()
    {
        _updatingFromEvent = true;
        try
        {
            chkEnableTouchscreen.Checked = KryptonManager.UseTouchscreenSupport;

            // Convert scale factor (1.0 - 3.0) to trackbar value (0-200)
            float scaleFactor = KryptonManager.TouchscreenScaleFactorValue;
            int trackValue = (int)Math.Round((scaleFactor - 1.0f) * 100f);
            trackValue = Math.Max(0, Math.Min(200, trackValue)); // Clamp to valid range
            trackScaleFactor.Value = trackValue;

            lblScaleValue.Text = $"{scaleFactor:F2}x ({(scaleFactor * 100 - 100):F1}% larger)";
        }
        finally
        {
            _updatingFromEvent = false;
        }
    }

    private void UpdateStatus()
    {
        bool isEnabled = KryptonManager.UseTouchscreenSupport;
        float scaleFactor = KryptonManager.TouchscreenScaleFactor;

        lblStatus.Text = isEnabled
            ? $"Touchscreen Support: ENABLED - Scale Factor: {scaleFactor:F2}x ({(scaleFactor * 100 - 100):F1}% larger)"
            : "Touchscreen Support: DISABLED - Controls at normal size";

        lblStatus.StateCommon.ShortText.Color1 = isEnabled ? Color.Green : Color.Gray;

        // Update button text
        btnToggle.Text = isEnabled ? "Disable Touchscreen Support" : "Enable Touchscreen Support";
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // Unsubscribe from events
        KryptonManager.GlobalTouchscreenSupportChanged -= OnGlobalTouchscreenSupportChanged;
        base.OnFormClosed(e);
    }
}

