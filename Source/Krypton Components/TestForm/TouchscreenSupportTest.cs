#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Workspace;

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
        chkEnableFontScaling.CheckedChanged += ChkEnableFontScaling_CheckedChanged;
        trackFontScaleFactor.ValueChanged += TrackFontScaleFactor_ValueChanged;
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

        // Label examples
        lblInfo.Text = "This label demonstrates text scaling";
        lblInfo.StateCommon.ShortText.Color1 = Color.DarkBlue;

        // Link label
        linkLabel.Text = "Click this link";
        linkLabel.LinkClicked += (s, e) => KryptonMessageBox.Show("Link clicked!", "Touchscreen Test");

        // Setup Navigator
        SetupNavigator();

        // Setup Workspace
        SetupWorkspace();
    }

    private void SetupNavigator()
    {
        // Create pages for navigator
        var page1 = new KryptonPage { Text = "Page 1", TextTitle = "First Page" };
        var label1 = new KryptonLabel { Text = "Navigator Page 1 - Touchscreen scaling applies to tabs and buttons", Dock = DockStyle.Fill };
        label1.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label1.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page1.Controls.Add(label1);

        var page2 = new KryptonPage { Text = "Page 2", TextTitle = "Second Page" };
        var label2 = new KryptonLabel { Text = "Navigator Page 2 - All controls scale when touchscreen support is enabled", Dock = DockStyle.Fill };
        label2.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label2.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page2.Controls.Add(label2);

        var page3 = new KryptonPage { Text = "Page 3", TextTitle = "Third Page" };
        var label3 = new KryptonLabel { Text = "Navigator Page 3 - Try adjusting the scale factor to see the effect", Dock = DockStyle.Fill };
        label3.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label3.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page3.Controls.Add(label3);

        navigator.Pages.AddRange(new[] { page1, page2, page3 });
        navigator.SelectedPage = page1;
    }

    private void SetupWorkspace()
    {
        // Create workspace cells with pages
        // Note: KryptonWorkspaceCell inherits from KryptonNavigator, so it automatically
        // gets touchscreen scaling support
        var cell1 = new KryptonWorkspaceCell();
        var page1 = new KryptonPage { Text = "Workspace Cell 1", TextTitle = "Cell 1" };
        var label1 = new KryptonLabel { Text = "Workspace Cell 1 - Workspace cells and their tabs scale with touchscreen support", Dock = DockStyle.Fill };
        label1.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label1.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page1.Controls.Add(label1);
        cell1.Pages.Add(page1);
        cell1.SelectedPage = page1;

        var cell2 = new KryptonWorkspaceCell();
        var page2 = new KryptonPage { Text = "Workspace Cell 2", TextTitle = "Cell 2" };
        var label2 = new KryptonLabel { Text = "Workspace Cell 2 - Navigator, Ribbon, Workspace, and Docking all support touchscreen scaling", Dock = DockStyle.Fill };
        label2.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label2.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        page2.Controls.Add(label2);
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
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private void TrackScaleFactor_ValueChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            // Convert trackbar value (0-200) to scale factor (1.0 - 3.0)
            float scaleFactor = 1.0f + (trackScaleFactor.Value / 100f);
            KryptonManager.TouchscreenSettingsValues.ControlScaleFactor = scaleFactor;
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
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
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private void TrackFontScaleFactor_ValueChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent) return;

        try
        {
            // Convert trackbar value (0-200) to scale factor (1.0 - 3.0)
            float scaleFactor = 1.0f + (trackFontScaleFactor.Value / 100f);
            KryptonManager.TouchscreenSettingsValues.FontScaleFactor = scaleFactor;
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
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
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
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
            KryptonManager.TouchscreenSettingsValues.TouchscreenModeEnabled = true;
            KryptonManager.TouchscreenSettingsValues.ControlScaleFactor = scaleFactor;
            KryptonManager.TouchscreenSettingsValues.FontScaleFactor = scaleFactor; // Match font scale to control scale
            UpdateUIFromSettings();
            UpdateStatus();
            KryptonMessageBox.Show($"Applied preset: {description}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private void BtnToggle_Click(object? sender, EventArgs e)
    {
        try
        {
            KryptonManager.TouchscreenSettingsValues.TouchscreenModeEnabled = !KryptonManager.UseTouchscreenSupport;
            UpdateUIFromSettings();
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
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
            statusText =
                $"Touchscreen Support: ENABLED - Control Scale: {controlScaleFactor:F2}x ({(controlScaleFactor * 100 - 100):F1}% larger)";
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
            statusText = "Touchscreen Support: DISABLED - Controls at normal size";
        }

        lblStatus.Text = statusText;
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