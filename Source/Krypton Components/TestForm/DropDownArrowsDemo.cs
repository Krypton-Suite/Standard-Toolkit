#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of drop-down arrows: smaller size, DPI awareness (Issue #2129), and crisp bitmap glyphs (Issue #3663).
/// Shows KryptonButton, KryptonDropButton, KryptonComboBox, KryptonDateTimePicker, KryptonColorButton,
/// and KryptonNumericUpDown with their drop-down arrows. Verifies arrows scale correctly at different DPI.
/// </summary>
public partial class DropDownArrowsDemo : KryptonForm
{
    private Timer _dpiMonitorTimer;

    public DropDownArrowsDemo()
    {
        InitializeComponent();
        InitializeDemo();
    }

    private void InitializeDemo()
    {
        SetupControls();
        SetupEventHandlers();
        SetupDpiMonitoring();
        UpdateDpiInfo();
    }

    private void SetupControls()
    {
        cmbItems.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
        cmbItems.SelectedIndex = 0;

        dtpValue.Value = DateTime.Now;
        dtpValue.Format = DateTimePickerFormat.Short;

        numValue.Value = 42;
        numValue.Minimum = 0;
        numValue.Maximum = 100;
    }

    private void RefreshArrowControls()
    {
        cmbItems.Refresh();
        dtpValue.Refresh();
        numValue.Refresh();
        btnDropDown.Refresh();
        btnSplit.Refresh();
        btnStandalone.Refresh();
        kryptonColorButton1.Refresh();
        UpdateDpiInfo();
    }

    private void SetupEventHandlers()
    {
        kryptonThemeComboBox1.SelectedIndexChanged += (s, e) => UpdateDpiInfo();
        KryptonManager.GlobalDropDownArrowRenderModeChanged += OnDropDownArrowRenderModeChanged;
        btnRefresh.Click += (s, e) => RefreshArrowControls();

        kryptonColorButton1.SelectedColorChanged += KryptonColorButton1_SelectedColorChanged;

        kryptonContextMenuItem1.Click += (s, e) => KryptonMessageBox.Show("Menu item 1 clicked", "Drop-Down Demo");
        kryptonContextMenuItem2.Click += (s, e) => KryptonMessageBox.Show("Menu item 2 clicked", "Drop-Down Demo");
        kryptonContextMenuItem3.Click += (s, e) => KryptonMessageBox.Show("Menu item 3 clicked", "Drop-Down Demo");
    }

    private void OnDropDownArrowRenderModeChanged(object? sender, EventArgs e) => RefreshArrowControls();

    private void KryptonColorButton1_SelectedColorChanged(object? sender, ColorEventArgs e)
    {
        btnDropDown.Values.DropDownArrowColor = e.Color;
        btnSplit.Values.DropDownArrowColor = e.Color;
    }

    private void SetupDpiMonitoring()
    {
        _dpiMonitorTimer = new Timer { Interval = 500 };
        _dpiMonitorTimer.Tick += (s, e) => UpdateDpiInfo();
        _dpiMonitorTimer.Start();

        Move += (s, e) => UpdateDpiInfo();
        Resize += (s, e) => UpdateDpiInfo();
    }

    private void UpdateDpiInfo()
    {
        try
        {
            IntPtr hWnd = IsHandleCreated ? Handle : IntPtr.Zero;
            float dpiFactorX = KryptonManager.GetDpiFactorX(hWnd);
            float dpiFactorY = KryptonManager.GetDpiFactorY(hWnd);
            float dpiX = dpiFactorX * 96f;
            float dpiY = dpiFactorY * 96f;
            float scalePercent = dpiFactorY * 100f;
            int arrowSize96 = DropDownArrowGlyphDefaults.DefaultBaseSizeAt96Dpi;
            int arrowSizeScaled = (int)(arrowSize96 * dpiFactorY);

            lblDpiInfo.Values.Text =
                $"DPI: {dpiX:F0}×{dpiY:F0} | Scale: {scalePercent:F0}% | " +
                $"Arrow: {arrowSizeScaled}px | Mode: {KryptonManager.DropDownArrowRenderMode}";
        }
        catch
        {
            lblDpiInfo.Values.Text = "DPI info unavailable";
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        KryptonManager.GlobalDropDownArrowRenderModeChanged -= OnDropDownArrowRenderModeChanged;
        _dpiMonitorTimer?.Stop();
        _dpiMonitorTimer?.Dispose();
        base.OnFormClosing(e);
    }
}
