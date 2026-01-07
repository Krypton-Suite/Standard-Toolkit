#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Runtime.InteropServices;

using Krypton.Navigator;
using Krypton.Ribbon;
using Krypton.Toolkit;
using Krypton.Workspace;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of touchscreen support with high DPI scaling.
/// Shows per-monitor DPI awareness, combined scaling factors (DPI × Touchscreen),
/// and real-time monitoring of DPI changes when windows move between monitors.
/// </summary>
public partial class TouchscreenHighDpiDemo : KryptonForm
{
    private Timer _dpiMonitorTimer;
    private bool _updatingFromEvent;

    public TouchscreenHighDpiDemo()
    {
        InitializeComponent();
        InitializeForm();
    }

    private void InitializeForm()
    {
        // Subscribe to touchscreen support changes
        KryptonManager.GlobalTouchscreenSupportChanged += OnGlobalTouchscreenSupportChanged;

        // Setup DPI monitoring timer
        _dpiMonitorTimer = new Timer { Interval = 500 };
        _dpiMonitorTimer.Tick += DpiMonitorTimer_Tick;
        _dpiMonitorTimer.Start();

        // Initialize UI with current settings
        UpdateUIFromSettings();

        // Setup demo controls
        SetupDemoControls();

        // Setup event handlers
        chkEnableTouchscreen.CheckedChanged += ChkEnableTouchscreen_CheckedChanged;
        trackScaleFactor.ValueChanged += TrackScaleFactor_ValueChanged;
        chkEnableFontScaling.CheckedChanged += ChkEnableFontScaling_CheckedChanged;
        trackFontScaleFactor.ValueChanged += TrackFontScaleFactor_ValueChanged;
        btnResetScale.Click += BtnResetScale_Click;
        btnApplyPreset25.Click += BtnApplyPreset25_Click;
        btnApplyPreset50.Click += BtnApplyPreset50_Click;
        btnApplyPreset75.Click += BtnApplyPreset75_Click;
        btnToggle.Click += BtnToggle_Click;
        btnRefreshDpi.Click += BtnRefreshDpi_Click;

        // Handle form move/resize to detect monitor changes
        this.Move += (s, e) => UpdateDpiInfo();
        this.Resize += (s, e) => UpdateDpiInfo();
        this.DpiChanged += (s, e) => OnDpiChanged(e);

        // Update status
        UpdateStatus();
        UpdateDpiInfo();
    }

    private void SetupDemoControls()
    {
        // Button examples
        btnStandard.Text = "Standard Button";
        btnStandard.Click += (s, e) => KryptonMessageBox.Show("Standard button clicked!", "High DPI + Touchscreen Demo");

        btnPrimary.Text = "Primary Button";
        btnPrimary.ButtonStyle = ButtonStyle.Command;
        btnPrimary.Click += (s, e) => KryptonMessageBox.Show("Primary button clicked!", "High DPI + Touchscreen Demo");

        btnSuccess.Text = "Success Button";
        btnSuccess.StateCommon.Content.ShortText.Color1 = Color.Green;
        btnSuccess.Click += (s, e) => KryptonMessageBox.Show("Success button clicked!", "High DPI + Touchscreen Demo");

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
        txtInput.CueHint.CueHintText = "Enter text here...";
        txtNumeric.Value = 42;

        // ComboBox example
        cmbOptions.Items.AddRange(new[] { "Option 1", "Option 2", "Option 3", "Option 4" });
        cmbOptions.SelectedIndex = 0;

        // Progress bar
        progressBar.Value = 65;
        progressBar.StateCommon.Content.LongText.Color1 = Color.Blue;

        // Track bar
        trackBar.Minimum = 0;
        trackBar.Maximum = 100;
        trackBar.Value = 50;
        trackBar.TickFrequency = 10;

        // Navigator example - Create pages first
        var page1 = new KryptonPage { Text = "Page 1", TextTitle = "First Page" };
        var label1 = new KryptonLabel
        {
            Text = "Navigator Page 1 - High DPI + Touchscreen scaling applies to tabs and buttons",
            Dock = DockStyle.Fill
        };
        label1.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label1.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page1.Controls.Add(label1);
        navigator.Pages.Add(page1);

        var page2 = new KryptonPage { Text = "Page 2", TextTitle = "Second Page" };
        var label2 = new KryptonLabel
        {
            Text = "Navigator Page 2 - All controls scale when touchscreen support is enabled on high DPI displays",
            Dock = DockStyle.Fill
        };
        label2.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label2.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page2.Controls.Add(label2);
        navigator.Pages.Add(page2);

        navigator.SelectedPage = page1;

        // Workspace example - Create cells with pages
        var cell1 = new KryptonWorkspaceCell();
        var page1 = new KryptonPage { Text = "Workspace Cell 1", TextTitle = "Cell 1" };
        var label1w = new KryptonLabel
        {
            Text = "Workspace Cell 1 - Workspace cells and their tabs scale with touchscreen support on high DPI",
            Dock = DockStyle.Fill
        };
        label1w.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label1w.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page1.Controls.Add(label1w);
        cell1.Pages.Add(page1);
        cell1.SelectedPage = page1;

        var cell2 = new KryptonWorkspaceCell();
        var page2 = new KryptonPage { Text = "Workspace Cell 2", TextTitle = "Cell 2" };
        var label2w = new KryptonLabel
        {
            Text = "Workspace Cell 2 - Navigator, Ribbon, Workspace, and Docking all support touchscreen scaling with per-monitor DPI",
            Dock = DockStyle.Fill
        };
        label2w.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label2w.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page2.Controls.Add(label2w);
        cell2.Pages.Add(page2);
        cell2.SelectedPage = page2;

        workspace.Root.Children.Add(cell1);
        workspace.Root.Children.Add(cell2);
    }

    private void ChkEnableTouchscreen_CheckedChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            KryptonManager.TouchscreenSettingsValues.TouchscreenModeEnabled = chkEnableTouchscreen.Checked;
            UpdateStatus();
            UpdateDpiInfo();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void TrackScaleFactor_ValueChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            float scaleFactor = 1.0f + (trackScaleFactor.Value / 100f);
            KryptonManager.TouchscreenSettingsValues.ControlScaleFactor = scaleFactor;
            lblScaleValue.Text = $"{scaleFactor:F2}x ({(scaleFactor * 100 - 100):F1}% larger)";
            UpdateStatus();
            UpdateDpiInfo();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void ChkEnableFontScaling_CheckedChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            KryptonManager.TouchscreenSettingsValues.FontScalingEnabled = chkEnableFontScaling.Checked;
            trackFontScaleFactor.Enabled = KryptonManager.TouchscreenSettingsValues.TouchscreenModeEnabled && chkEnableFontScaling.Checked;
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void TrackFontScaleFactor_ValueChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            float scaleFactor = 1.0f + (trackFontScaleFactor.Value / 100f);
            KryptonManager.TouchscreenSettingsValues.FontScaleFactor = scaleFactor;
            lblFontScaleValue.Text = $"{scaleFactor:F2}x ({(scaleFactor * 100 - 100):F1}% larger)";
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void BtnResetScale_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonManager.TouchscreenSettingsValues.ControlScaleFactor = 1.25f; // Default 25% larger
            KryptonManager.TouchscreenSettingsValues.FontScaleFactor = 1.25f; // Default 25% larger
            UpdateUIFromSettings();
            UpdateStatus();
            UpdateDpiInfo();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void BtnApplyPreset25_Click(object? sender, EventArgs e)
    {
        ApplyPreset(1.25f, "25% larger (1.25x)");
    }

    private void BtnApplyPreset50_Click(object? sender, EventArgs e)
    {
        ApplyPreset(1.50f, "50% larger (1.50x)");
    }

    private void BtnApplyPreset75_Click(object? sender, EventArgs e)
    {
        ApplyPreset(1.75f, "75% larger (1.75x)");
    }

    private void ApplyPreset(float scaleFactor, string description)
    {
        try
        {
            KryptonManager.TouchscreenSettingsValues.TouchscreenModeEnabled = true;
            KryptonManager.TouchscreenSettingsValues.ControlScaleFactor = scaleFactor;
            KryptonManager.TouchscreenSettingsValues.FontScaleFactor = scaleFactor; // Match font scale to control scale
            UpdateUIFromSettings();
            UpdateStatus();
            UpdateDpiInfo();
            KryptonMessageBox.Show($"Applied preset: {description}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void BtnToggle_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonManager.TouchscreenSettingsValues.TouchscreenModeEnabled = !KryptonManager.UseTouchscreenSupport;
            UpdateUIFromSettings();
            UpdateStatus();
            UpdateDpiInfo();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void BtnRefreshDpi_Click(object? sender, EventArgs e)
    {
        // Invalidate DPI cache and refresh
        KryptonManager.InvalidateDpiCache();
        UpdateDpiInfo();
    }

    private void OnDpiChanged(DpiChangedEventArgs e)
    {
        // DPI changed - invalidate cache and update info
        KryptonManager.InvalidateDpiCache();
        UpdateDpiInfo();
        UpdateStatus();
    }

    private void DpiMonitorTimer_Tick(object? sender, EventArgs e)
    {
        // Periodically update DPI info to catch monitor changes
        UpdateDpiInfo();
    }

    private void OnGlobalTouchscreenSupportChanged(object? sender, EventArgs e)
    {
        // Update UI when settings change externally
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateUIFromSettings));
            Invoke(new Action(UpdateStatus));
            Invoke(new Action(UpdateDpiInfo));
        }
        else
        {
            UpdateUIFromSettings();
            UpdateStatus();
            UpdateDpiInfo();
        }
    }

    private void UpdateUIFromSettings()
    {
        _updatingFromEvent = true;
        try
        {
            var settings = KryptonManager.TouchscreenSettingsValues;

            chkEnableTouchscreen.Checked = settings.TouchscreenModeEnabled;

            // Convert control scale factor (1.0 - 3.0) to trackbar value (0-200)
            float controlScaleFactor = settings.ControlScaleFactor;
            int trackValue = (int)Math.Round((controlScaleFactor - 1.0f) * 100f);
            trackValue = Math.Max(0, Math.Min(200, trackValue)); // Clamp to valid range
            trackScaleFactor.Value = trackValue;
            lblScaleValue.Text = $"{controlScaleFactor:F2}x ({(controlScaleFactor * 100 - 100):F1}% larger)";

            // Font scaling controls
            chkEnableFontScaling.Checked = settings.FontScalingEnabled;

            // Convert font scale factor (1.0 - 3.0) to trackbar value (0-200)
            float fontScaleFactor = settings.FontScaleFactor;
            int fontTrackValue = (int)Math.Round((fontScaleFactor - 1.0f) * 100f);
            fontTrackValue = Math.Max(0, Math.Min(200, fontTrackValue)); // Clamp to valid range
            trackFontScaleFactor.Value = fontTrackValue;
            lblFontScaleValue.Text = $"{fontScaleFactor:F2}x ({(fontScaleFactor * 100 - 100):F1}% larger)";

            // Enable/disable font scaling controls based on touchscreen support
            bool touchscreenEnabled = settings.TouchscreenModeEnabled;
            chkEnableFontScaling.Enabled = touchscreenEnabled;
            bool fontScalingEnabled = touchscreenEnabled && settings.FontScalingEnabled;
            trackFontScaleFactor.Enabled = fontScalingEnabled;
            lblFontScaleFactor.Enabled = touchscreenEnabled;
            lblFontScaleValue.Enabled = touchscreenEnabled;
        }
        finally
        {
            _updatingFromEvent = false;
        }
    }

    private void UpdateStatus()
    {
        var settings = KryptonManager.TouchscreenSettingsValues;
        bool isEnabled = settings.TouchscreenModeEnabled;
        float controlScaleFactor = KryptonManager.TouchscreenScaleFactor;
        bool fontScalingEnabled = settings.FontScalingEnabled && isEnabled;
        float fontScaleFactor = KryptonManager.TouchscreenFontScaleFactor;

        string statusText;
        if (isEnabled)
        {
            statusText = $"Touchscreen Support: ENABLED - Control Scale: {controlScaleFactor:F2}x ({(controlScaleFactor * 100 - 100):F1}% larger)";
            if (fontScalingEnabled)
            {
                statusText += $" | Font Scale: {fontScaleFactor:F2}x ({(fontScaleFactor * 100 - 100):F1}% larger)";
            }
            else
            {
                statusText += " | Font Scaling: DISABLED";
            }
        }
        else
        {
            statusText = $"Touchscreen Support: DISABLED - Controls at normal size";
        }

        lblStatus.Text = statusText;
        lblStatus.StateCommon.ShortText.Color1 = isEnabled ? Color.Green : Color.Gray;

        // Update button text
        btnToggle.Text = isEnabled ? "Disable Touchscreen Support" : "Enable Touchscreen Support";
    }

    private void UpdateDpiInfo()
    {
        if (!IsHandleCreated) return;

        IntPtr hWnd = Handle;

        // Get primary monitor DPI (legacy method)
        float dpiXPrimary = KryptonManager.GetDpiFactorX();
        float dpiYPrimary = KryptonManager.GetDpiFactorY();
        float dpiAvgPrimary = KryptonManager.GetDpiFactor();

        // Get per-monitor DPI (window-aware method)
        float dpiXPerMonitor = KryptonManager.GetDpiFactorX(hWnd);
        float dpiYPerMonitor = KryptonManager.GetDpiFactorY(hWnd);
        float dpiAvgPerMonitor = KryptonManager.GetDpiFactor(hWnd);

        // Get combined scaling factors (DPI × Touchscreen)
        float combinedXPrimary = KryptonManager.GetCombinedScaleFactorX();
        float combinedYPrimary = KryptonManager.GetCombinedScaleFactorY();
        float combinedAvgPrimary = KryptonManager.GetCombinedScaleFactor();

        float combinedXPerMonitor = KryptonManager.GetCombinedScaleFactorX(hWnd);
        float combinedYPerMonitor = KryptonManager.GetCombinedScaleFactorY(hWnd);
        float combinedAvgPerMonitor = KryptonManager.GetCombinedScaleFactor(hWnd);

        // Calculate actual DPI value from the per-monitor factor (DPI = factor * 96)
        int actualDpi = (int)Math.Round(dpiAvgPerMonitor * 96f);

        // Update DPI info labels
        lblDpiPrimary.Text = $"Primary Monitor DPI: {dpiAvgPrimary:F2}x ({dpiXPrimary:F2}x, {dpiYPrimary:F2}y)";
        lblDpiPerMonitor.Text = $"Per-Monitor DPI: {dpiAvgPerMonitor:F2}x ({dpiXPerMonitor:F2}x, {dpiYPerMonitor:F2}y)";

        // Display the calculated DPI value
        lblDpiActual.Text = $"Calculated DPI: {actualDpi} ({dpiAvgPerMonitor:F2}x)";
        lblDpiActual.StateCommon.ShortText.Color1 = Color.Blue;

        // Update combined scaling info
        lblCombinedPrimary.Text = $"Combined (Primary): {combinedAvgPrimary:F2}x ({combinedXPrimary:F2}x, {combinedYPrimary:F2}y)";
        lblCombinedPerMonitor.Text = $"Combined (Per-Monitor): {combinedAvgPerMonitor:F2}x ({combinedXPerMonitor:F2}x, {combinedYPerMonitor:F2}y)";

        // Highlight if there's a difference (multi-monitor scenario)
        if (Math.Abs(dpiAvgPrimary - dpiAvgPerMonitor) > 0.01f)
        {
            lblDpiPerMonitor.StateCommon.ShortText.Color1 = Color.Orange;
            lblCombinedPerMonitor.StateCommon.ShortText.Color1 = Color.Orange;
            lblDpiWarning.Text = "⚠ Multi-monitor detected: Per-monitor DPI differs from primary monitor";
            lblDpiWarning.StateCommon.ShortText.Color1 = Color.Orange;
        }
        else
        {
            lblDpiPerMonitor.StateCommon.ShortText.Color1 = Color.Black;
            lblCombinedPerMonitor.StateCommon.ShortText.Color1 = Color.Black;
            lblDpiWarning.Text = "✓ Single monitor or matching DPI";
            lblDpiWarning.StateCommon.ShortText.Color1 = Color.Green;
        }

        // Show scaling example
        int baseSize = 100;
        int scaledByDpi = KryptonManager.ScaleValueByDpi(baseSize);
        int scaledByBoth = KryptonManager.ScaleValueByDpiAndTouchscreen(baseSize);
        int scaledByBothPerMonitor = (int)Math.Round(baseSize * combinedAvgPerMonitor);

        lblScalingExample.Text = $"Scaling Example (base={baseSize}px): DPI only={scaledByDpi}px, DPI+Touchscreen={scaledByBoth}px, Per-Monitor={scaledByBothPerMonitor}px";
    }
}
