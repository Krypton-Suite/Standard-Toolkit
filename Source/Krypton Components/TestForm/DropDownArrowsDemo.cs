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
    private KryptonComboBox? _cmbRenderMode;
    private KryptonComboBox? _cmbGlyphStyle;

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
        SetupGlyphOptionCombos();

        cmbItems.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
        cmbItems.SelectedIndex = 0;

        dtpValue.Value = DateTime.Now;
        dtpValue.Format = DateTimePickerFormat.Short;

        numValue.Value = 42;
        numValue.Minimum = 0;
        numValue.Maximum = 100;
    }

    private void SetupGlyphOptionCombos()
    {
        var lblRenderMode = new KryptonLabel
        {
            Location = new Point(400, 58),
            Size = new Size(42, 20),
            Values = { Text = "Mode:" }
        };

        _cmbRenderMode = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(448, 56),
            Size = new Size(100, 22)
        };
        foreach (DropDownArrowRenderMode mode in Enum.GetValues(typeof(DropDownArrowRenderMode)))
        {
            _cmbRenderMode.Items.Add(mode);
        }

        _cmbRenderMode.SelectedItem = KryptonManager.DropDownArrowRenderMode;

        var lblGlyphStyle = new KryptonLabel
        {
            Location = new Point(558, 58),
            Size = new Size(42, 20),
            Values = { Text = "Style:" }
        };

        _cmbGlyphStyle = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(606, 56),
            Size = new Size(90, 22)
        };
        foreach (DropDownArrowGlyphStyle style in Enum.GetValues(typeof(DropDownArrowGlyphStyle)))
        {
            _cmbGlyphStyle.Items.Add(style);
        }

        _cmbGlyphStyle.SelectedItem = KryptonManager.DropDownArrowGlyphStyle;

        pnlHeader.Controls.Add(lblRenderMode);
        pnlHeader.Controls.Add(_cmbRenderMode);
        pnlHeader.Controls.Add(lblGlyphStyle);
        pnlHeader.Controls.Add(_cmbGlyphStyle);
    }

    private void RefreshArrowControls()
    {
        kryptonThemeComboBox1.Refresh();
        _cmbRenderMode?.Refresh();
        _cmbGlyphStyle?.Refresh();
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
        KryptonManager.GlobalDropDownArrowRenderModeChanged += OnDropDownArrowSettingsChanged;
        KryptonManager.GlobalDropDownArrowGlyphStyleChanged += OnDropDownArrowSettingsChanged;
        btnRefresh.Click += (s, e) => RefreshArrowControls();

        if (_cmbRenderMode is not null)
        {
            _cmbRenderMode.SelectedIndexChanged += OnRenderModeComboChanged;
        }

        if (_cmbGlyphStyle is not null)
        {
            _cmbGlyphStyle.SelectedIndexChanged += OnGlyphStyleComboChanged;
        }

        kryptonColorButton1.SelectedColorChanged += KryptonColorButton1_SelectedColorChanged;

        kryptonContextMenuItem1.Click += (s, e) => KryptonMessageBox.Show("Menu item 1 clicked", "Drop-Down Demo");
        kryptonContextMenuItem2.Click += (s, e) => KryptonMessageBox.Show("Menu item 2 clicked", "Drop-Down Demo");
        kryptonContextMenuItem3.Click += (s, e) => KryptonMessageBox.Show("Menu item 3 clicked", "Drop-Down Demo");
    }

    private void OnDropDownArrowSettingsChanged(object? sender, EventArgs e) => RefreshArrowControls();

    private void OnRenderModeComboChanged(object? sender, EventArgs e)
    {
        if (_cmbRenderMode?.SelectedItem is DropDownArrowRenderMode mode)
        {
            KryptonManager.DropDownArrowRenderMode = mode;
        }
    }

    private void OnGlyphStyleComboChanged(object? sender, EventArgs e)
    {
        if (_cmbGlyphStyle?.SelectedItem is DropDownArrowGlyphStyle style)
        {
            KryptonManager.DropDownArrowGlyphStyle = style;
        }
    }

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
                $"Arrow: {arrowSizeScaled}px | Mode: {KryptonManager.DropDownArrowRenderMode} | Style: {KryptonManager.DropDownArrowGlyphStyle}";
        }
        catch
        {
            lblDpiInfo.Values.Text = "DPI info unavailable";
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        KryptonManager.GlobalDropDownArrowRenderModeChanged -= OnDropDownArrowSettingsChanged;
        KryptonManager.GlobalDropDownArrowGlyphStyleChanged -= OnDropDownArrowSettingsChanged;
        _dpiMonitorTimer?.Stop();
        _dpiMonitorTimer?.Dispose();
        base.OnFormClosing(e);
    }
}
