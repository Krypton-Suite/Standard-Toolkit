#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KComboBox and KDateTimePicker consistency fix (Issue #1651).
/// Shows that drop-down buttons now stretch to full height and text is properly centered,
/// ensuring consistent behavior across both control types.
/// </summary>
public partial class ComboBoxDateTimePickerConsistencyDemo : KryptonForm
{
    public ComboBoxDateTimePickerConsistencyDemo()
    {
        InitializeComponent();
        InitializeDemo();
    }

    private void InitializeDemo()
    {
        // Setup standard height controls
        SetupStandardHeightControls();

        // Setup tall controls
        SetupTallControls();

        // Setup different styles
        SetupStyleVariations();

        // Setup with different content
        SetupContentVariations();

        // Setup event handlers
        SetupEventHandlers();

        // Update status
        UpdateStatus();
    }

    private void SetupStandardHeightControls()
    {
        // Standard height ComboBox
        cmbStandard.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
        cmbStandard.SelectedIndex = 0;

        // Standard height DateTimePicker
        dtpStandard.Value = DateTime.Now;
        dtpStandard.Format = DateTimePickerFormat.Short;
    }

    private void SetupTallControls()
    {
        // Tall ComboBox
        cmbTall.Height = 60;
        cmbTall.Items.AddRange(new object[] { "Tall ComboBox Item 1", "Tall ComboBox Item 2", "Tall ComboBox Item 3" });
        cmbTall.SelectedIndex = 0;

        // Tall DateTimePicker
        dtpTall.Height = 60;
        dtpTall.Value = DateTime.Now.AddDays(1);
        dtpTall.Format = DateTimePickerFormat.Long;
    }

    private void SetupStyleVariations()
    {
        // InputControl style ComboBox
        cmbInputControl.InputControlStyle = InputControlStyle.Standalone;
        cmbInputControl.Items.AddRange(new object[] { "Input Control Style", "Item 2", "Item 3" });
        cmbInputControl.SelectedIndex = 0;

        // InputControl style DateTimePicker
        dtpInputControl.InputControlStyle = InputControlStyle.Standalone;
        dtpInputControl.Value = DateTime.Now.AddDays(-1);
        dtpInputControl.Format = DateTimePickerFormat.Time;

        // Standalone style ComboBox
        cmbStandalone.InputControlStyle = InputControlStyle.Standalone;
        cmbStandalone.Items.AddRange(new object[] { "Standalone Style", "Item 2", "Item 3" });
        cmbStandalone.SelectedIndex = 0;

        // Standalone style DateTimePicker
        dtpStandalone.InputControlStyle = InputControlStyle.Standalone;
        dtpStandalone.Value = DateTime.Now.AddDays(2);
        dtpStandalone.Format = DateTimePickerFormat.Custom;
        dtpStandalone.CustomFormat = "dddd, MMMM dd, yyyy";
    }

    private void SetupContentVariations()
    {
        // ComboBox with long text
        cmbLongText.Items.AddRange(new object[]
        {
            "This is a very long item text that should be displayed properly",
            "Short",
            "Medium length item",
            "Another long item with lots of text content"
        });
        cmbLongText.SelectedIndex = 0;
        cmbLongText.Text = "This is a very long item text that should be displayed properly";

        // DateTimePicker with custom format
        dtpCustomFormat.Value = DateTime.Now;
        dtpCustomFormat.Format = DateTimePickerFormat.Custom;
        dtpCustomFormat.CustomFormat = "dddd, MMMM dd, yyyy 'at' hh:mm:ss tt";

        // ComboBox with numbers
        cmbNumbers.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "10", "20", "50", "100" });
        cmbNumbers.SelectedIndex = 4;

        // DateTimePicker with date only
        dtpDateOnly.Value = DateTime.Today;
        dtpDateOnly.Format = DateTimePickerFormat.Short;
        dtpDateOnly.ShowUpDown = true;
    }

    private void SetupEventHandlers()
    {
        // Height adjustment handlers
        btnIncreaseHeight.Click += BtnIncreaseHeight_Click;
        btnDecreaseHeight.Click += BtnDecreaseHeight_Click;
        btnResetHeight.Click += BtnResetHeight_Click;

        // Style toggle handlers
        btnToggleStyle.Click += BtnToggleStyle_Click;

        // Refresh handler
        btnRefresh.Click += BtnRefresh_Click;
    }

    private void BtnIncreaseHeight_Click(object? sender, EventArgs e)
    {
        cmbStandard.Height = Math.Min(cmbStandard.Height + 10, 100);
        dtpStandard.Height = cmbStandard.Height;
        cmbTall.Height = Math.Min(cmbTall.Height + 10, 100);
        dtpTall.Height = cmbTall.Height;
        UpdateStatus();
    }

    private void BtnDecreaseHeight_Click(object? sender, EventArgs e)
    {
        cmbStandard.Height = Math.Max(cmbStandard.Height - 10, 20);
        dtpStandard.Height = cmbStandard.Height;
        cmbTall.Height = Math.Max(cmbTall.Height - 10, 20);
        dtpTall.Height = cmbTall.Height;
        UpdateStatus();
    }

    private void BtnResetHeight_Click(object? sender, EventArgs e)
    {
        cmbStandard.Height = 25;
        dtpStandard.Height = 25;
        cmbTall.Height = 60;
        dtpTall.Height = 60;
        UpdateStatus();
    }

    private bool _styleToggle;
    private void BtnToggleStyle_Click(object? sender, EventArgs e)
    {
        _styleToggle = !_styleToggle;
        var style = _styleToggle ? InputControlStyle.Standalone : InputControlStyle.PanelClient;

        cmbInputControl.InputControlStyle = style;
        dtpInputControl.InputControlStyle = style;
        cmbStandalone.InputControlStyle = style;
        dtpStandalone.InputControlStyle = style;

        UpdateStatus();
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        // Force refresh of all controls
        cmbStandard.Refresh();
        dtpStandard.Refresh();
        cmbTall.Refresh();
        dtpTall.Refresh();
        cmbInputControl.Refresh();
        dtpInputControl.Refresh();
        cmbStandalone.Refresh();
        dtpStandalone.Refresh();
        cmbLongText.Refresh();
        dtpCustomFormat.Refresh();
        cmbNumbers.Refresh();
        dtpDateOnly.Refresh();

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        lblStatus.Text = $"Standard Height: {cmbStandard.Height}px | Tall Height: {cmbTall.Height}px | " +
                        $"Style: {(_styleToggle ? "Standalone" : "InputControl")} | " +
                        $"Issue #1651: Drop-down buttons should stretch to full height";
    }
}