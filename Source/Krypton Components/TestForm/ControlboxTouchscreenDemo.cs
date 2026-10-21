#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of touchscreen support for controlbox buttons (minimize, maximize, close)
/// and KryptonContextMenu items (Issue #2925).
/// Shows how these buttons become larger when touchscreen mode is enabled, making them easier to tap.
/// </summary>
public partial class ControlboxTouchscreenDemo : KryptonForm
{
    private bool _updatingFromEvent;
    private KryptonContextMenu? _demoContextMenu;

    public ControlboxTouchscreenDemo()
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
        btnShowContextMenu.Click += BtnShowContextMenu_Click;
        btnShowSystemMenu.Click += BtnShowSystemMenu_Click;

        // Update status
        UpdateStatus();

        // Add DPI cache invalidation on form resize/move to handle monitor changes
        Resize += (s, e) => KryptonManager.InvalidateDpiCache();
        Move += (s, e) => KryptonManager.InvalidateDpiCache();
    }

    private void SetupDemoControls()
    {
        // Setup context menu button
        btnShowContextMenu.Text = "Show Context Menu";
        btnShowContextMenu.ButtonStyle = ButtonStyle.Command;
        btnShowContextMenu.ToolTipValues.Description = "Right-click this button or click to show a context menu with touchscreen-scaled items";

        // Setup system menu button
        btnShowSystemMenu.Text = "Show System Menu";
        btnShowSystemMenu.ButtonStyle = ButtonStyle.Command;
        btnShowSystemMenu.ToolTipValues.Description = "Shows the system menu (right-click on title bar) which also scales with touchscreen support";

        // Setup info label
        lblInfo.Text = "This demo shows how controlbox buttons (minimize, maximize, close) and context menu items " +
                      "automatically become larger when touchscreen support is enabled.\n\n" +
                      "• Toggle touchscreen support to see the controlbox buttons resize\n" +
                      "• Click 'Show Context Menu' to see scaled menu items\n" +
                      "• Right-click the title bar to see the system menu (also scaled)\n" +
                      "• Adjust the scale factor to see different sizes";

        // Create demo context menu
        CreateDemoContextMenu();
    }

    private void CreateDemoContextMenu()
    {
        _demoContextMenu = new KryptonContextMenu();

        // Add menu items that will scale with touchscreen support
        var items = new KryptonContextMenuItems();

        // Standard menu items
        items.Items.Add(new KryptonContextMenuItem("New", null, (s, e) => ShowMessage("New", "New item clicked")));
        items.Items.Add(new KryptonContextMenuItem("Open", null, (s, e) => ShowMessage("Open", "Open item clicked")));
        items.Items.Add(new KryptonContextMenuItem("Save", null, (s, e) => ShowMessage("Save", "Save item clicked")));
        items.Items.Add(new KryptonContextMenuItem("Save As...", null, (s, e) => ShowMessage("Save As", "Save As item clicked")));

        items.Items.Add(new KryptonContextMenuSeparator());

        // More menu items
        items.Items.Add(new KryptonContextMenuItem("Cut", null, (s, e) => ShowMessage("Cut", "Cut item clicked")));
        items.Items.Add(new KryptonContextMenuItem("Copy", null, (s, e) => ShowMessage("Copy", "Copy item clicked")));
        items.Items.Add(new KryptonContextMenuItem("Paste", null, (s, e) => ShowMessage("Paste", "Paste item clicked")));

        items.Items.Add(new KryptonContextMenuSeparator());

        // Submenu example
        var submenuItem = new KryptonContextMenuItem("More Options");
        submenuItem.Items.Add(new KryptonContextMenuItem("Option 1", (s, e) => ShowMessage("Submenu", "Option 1 clicked")));
        submenuItem.Items.Add(new KryptonContextMenuItem("Option 2", (s, e) => ShowMessage("Submenu", "Option 2 clicked")));
        submenuItem.Items.Add(new KryptonContextMenuItem("Option 3", (s, e) => ShowMessage("Submenu", "Option 3 clicked")));
        items.Items.Add(submenuItem);

        items.Items.Add(new KryptonContextMenuSeparator());

        items.Items.Add(new KryptonContextMenuItem("Exit", null, (s, e) => Close()));

        _demoContextMenu.Items.Add(items);
    }

    private void ShowMessage(string title, string message)
    {
        KryptonMessageBox.Show(message, title, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
    }

    private void ChkEnableTouchscreen_CheckedChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent)
        {
            return;
        }

        try
        {
            KryptonManager.TouchscreenSettingValues.TouchscreenModeEnabled = chkEnableTouchscreen.Checked;
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void TrackScaleFactor_ValueChanged(object? sender, EventArgs e)
    {
        if (_updatingFromEvent)
        {
            return;
        }

        try
        {
            float scaleFactor = trackScaleFactor.Value / 100f;
            KryptonManager.TouchscreenSettingValues.ControlScaleFactor = scaleFactor;
            lblScaleValue.Text = $"{scaleFactor:F2}x ({(scaleFactor * 100 - 100):F1}% larger)";
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
            KryptonManager.TouchscreenSettingValues.ControlScaleFactor = 1.25f; // Default 25% larger
            trackScaleFactor.Value = 125;
            lblScaleValue.Text = "1.25x (25.0% larger)";
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void BtnApplyPreset25_Click(object? sender, EventArgs e) => ApplyPreset(1.25f, "25% Larger (Default)");
    private void BtnApplyPreset50_Click(object? sender, EventArgs e) => ApplyPreset(1.50f, "50% Larger");
    private void BtnApplyPreset75_Click(object? sender, EventArgs e) => ApplyPreset(1.75f, "75% Larger");

    private void ApplyPreset(float scaleFactor, string description)
    {
        try
        {
            KryptonManager.TouchscreenSettingValues.TouchscreenModeEnabled = true;
            KryptonManager.TouchscreenSettingValues.ControlScaleFactor = scaleFactor;
            trackScaleFactor.Value = (int)(scaleFactor * 100);
            lblScaleValue.Text = $"{scaleFactor:F2}x ({(scaleFactor * 100 - 100):F1}% larger)";
            chkEnableTouchscreen.Checked = true;
            UpdateStatus();
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
            KryptonManager.TouchscreenSettingValues.TouchscreenModeEnabled = !KryptonManager.UseTouchscreenSupport;
            UpdateUIFromSettings();
            UpdateStatus();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error: {ex.Message}", "Touchscreen Support", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void BtnShowContextMenu_Click(object? sender, EventArgs e)
    {
        if (_demoContextMenu != null)
        {
            // Show context menu at button location
            Point screenPoint = btnShowContextMenu.PointToScreen(new Point(0, btnShowContextMenu.Height));
            _demoContextMenu.Show(btnShowContextMenu, screenPoint);
        }
    }

    private void BtnShowSystemMenu_Click(object? sender, EventArgs e)
    {
        KryptonMessageBox.Show(
            "Right-click on the form's title bar to see the system menu.\n\n" +
            "The system menu items (Restore, Move, Size, Minimize, Maximize, Close) " +
            "will also scale when touchscreen support is enabled.",
            "System Menu",
            KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Information);
    }

    private void OnGlobalTouchscreenSupportChanged(object? sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new EventHandler(OnGlobalTouchscreenSupportChanged), sender, e);
            return;
        }

        UpdateUIFromSettings();
        UpdateStatus();
    }

    private void UpdateUIFromSettings()
    {
        _updatingFromEvent = true;
        try
        {
            var settings = KryptonManager.TouchscreenSettingValues;

            chkEnableTouchscreen.Checked = settings.TouchscreenModeEnabled;

            float scaleFactor = KryptonManager.TouchscreenScaleFactor;
            trackScaleFactor.Value = (int)(scaleFactor * 100);
            trackScaleFactor.Enabled = settings.TouchscreenModeEnabled;
            lblScaleFactor.Enabled = settings.TouchscreenModeEnabled;
            lblScaleValue.Enabled = settings.TouchscreenModeEnabled;
            lblScaleValue.Text = $"{scaleFactor:F2}x ({(scaleFactor * 100 - 100):F1}% larger)";

            btnResetScale.Enabled = settings.TouchscreenModeEnabled;
            btnApplyPreset25.Enabled = settings.TouchscreenModeEnabled;
            btnApplyPreset50.Enabled = settings.TouchscreenModeEnabled;
            btnApplyPreset75.Enabled = settings.TouchscreenModeEnabled;
        }
        finally
        {
            _updatingFromEvent = false;
        }
    }

    private void UpdateStatus()
    {
        var settings = KryptonManager.TouchscreenSettingValues;
        bool isEnabled = settings.TouchscreenModeEnabled;
        float scaleFactor = KryptonManager.TouchscreenScaleFactor;

        string statusText;
        if (isEnabled)
        {
            statusText = $"Touchscreen Support: ENABLED - Controlbox buttons and context menu items are {scaleFactor:F2}x larger ({(scaleFactor * 100 - 100):F1}% larger)";
        }
        else
        {
            statusText = "Touchscreen Support: DISABLED - Controlbox buttons and context menu items are at normal size";
        }

        lblStatus.Text = statusText;
        btnToggle.Text = isEnabled ? "Disable Touchscreen Support" : "Enable Touchscreen Support";
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        KryptonManager.GlobalTouchscreenSupportChanged -= OnGlobalTouchscreenSupportChanged;
        _demoContextMenu?.Dispose();
        base.OnFormClosing(e);
    }
}
