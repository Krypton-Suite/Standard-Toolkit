#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonDateTimePicker's month calendar custom background (Issue #1827).
/// Shows CalendarBackColor property, theme-driven calendar body background, and how to style the drop-down calendar for dark/light themes.
/// </summary>
public partial class KryptonDateTimePickerMonthCalendarDemo : KryptonForm
{
    public KryptonDateTimePickerMonthCalendarDemo()
    {
        InitializeComponent();
        InitializeDemo();
    }

    private void InitializeDemo()
    {
        SetupDefaultThemePicker();
        SetupPresetBackgroundPickers();
        SetupColorPickerTarget();
        SetupEventHandlers();
        UpdateStatus(dtpPickColor);
    }

    private void SetupDefaultThemePicker()
    {
        dtpThemeDefault.Value = DateTime.Today;
        dtpThemeDefault.Format = DateTimePickerFormat.Long;
        dtpThemeDefault.CalendarBackColor = Color.Empty; // use theme/palette
    }

    private void SetupPresetBackgroundPickers()
    {
        dtpDarkGray.Value = DateTime.Today.AddDays(1);
        dtpDarkGray.CalendarBackColor = Color.FromArgb(45, 45, 48);

        dtpSoftBlue.Value = DateTime.Today.AddDays(2);
        dtpSoftBlue.CalendarBackColor = Color.FromArgb(220, 235, 252);

        dtpLightYellow.Value = DateTime.Today.AddDays(-1);
        dtpLightYellow.CalendarBackColor = Color.FromArgb(255, 253, 231);

        dtpDarkSlate.Value = DateTime.Today.AddDays(3);
        dtpDarkSlate.CalendarBackColor = Color.FromArgb(37, 37, 38);
    }

    private void SetupColorPickerTarget()
    {
        dtpPickColor.Value = DateTime.Today;
        dtpPickColor.CalendarBackColor = Color.Empty;
        btnPickColor.SelectedColor = Color.FromArgb(45, 45, 48);
        btnPickColor.SelectedColorChanged += BtnPickColor_SelectedColorChanged;
    }

    private void SetupEventHandlers()
    {
        dtpThemeDefault.DropDown += Dtp_DropDown;
        dtpDarkGray.DropDown += Dtp_DropDown;
        dtpSoftBlue.DropDown += Dtp_DropDown;
        dtpLightYellow.DropDown += Dtp_DropDown;
        dtpDarkSlate.DropDown += Dtp_DropDown;
        dtpPickColor.DropDown += Dtp_DropDown;

        btnUseThemeDefault.Click += BtnUseThemeDefault_Click;
        btnApplyToAll.Click += BtnApplyToAll_Click;
    }

    private void Dtp_DropDown(object? sender, DateTimePickerDropArgs e)
    {
        if (sender is KryptonDateTimePicker dtp)
        {
            UpdateStatus(dtp);
        }
    }

    private void BtnPickColor_SelectedColorChanged(object? sender, ColorEventArgs e)
    {
        dtpPickColor.CalendarBackColor = e.Color;
        UpdateStatus(dtpPickColor);
    }

    private void BtnUseThemeDefault_Click(object? sender, EventArgs e)
    {
        dtpPickColor.CalendarBackColor = Color.Empty;
        UpdateStatus(dtpPickColor);
    }

    private void BtnApplyToAll_Click(object? sender, EventArgs e)
    {
        var color = dtpPickColor.CalendarBackColor;
        dtpThemeDefault.CalendarBackColor = color;
        dtpDarkGray.CalendarBackColor = color;
        dtpSoftBlue.CalendarBackColor = color;
        dtpLightYellow.CalendarBackColor = color;
        dtpDarkSlate.CalendarBackColor = color;
        if (color != Color.Empty)
        {
            btnPickColor.SelectedColor = color;
        }
        UpdateStatus(dtpPickColor);
    }

    private void UpdateStatus(KryptonDateTimePicker? focused)
    {
        if (focused == null)
        {
            lblStatus.Values.Text = @"Open any date picker drop-down to see the month calendar background.";
            return;
        }

        var color = focused.CalendarBackColor;
        var colorText = color == Color.Empty
            ? "Theme default"
            : $"{color.Name} (R={color.R}, G={color.G}, B={color.B})";
        lblStatus.Values.Text = @$"CalendarBackColor: {colorText}  |  Open the drop-down to see the calendar.";
    }
}
