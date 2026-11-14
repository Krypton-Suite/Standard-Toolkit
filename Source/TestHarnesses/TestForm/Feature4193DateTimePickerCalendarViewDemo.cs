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
/// Demo for Issue #4193: month- and year-only calendars on KryptonDateTimePicker and KryptonMonthCalendar.
/// </summary>
public sealed class Feature4193DateTimePickerCalendarViewDemo : KryptonForm
{
    private readonly KryptonLabel _valueLabel;

    public Feature4193DateTimePickerCalendarViewDemo()
    {
        Text = @"4193 — DateTimePicker month / year calendar";
        Size = new Size(980, 640);
        StartPosition = FormStartPosition.CenterScreen;

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 92,
            Padding = new Padding(12),
            Text =
                "Issue #4193: KryptonDateTimePicker is not a Win32 DateTimePicker, so MCM_SETCURRENTVIEW cannot force a month grid. " +
                "Use CalendarView = Months or Years with a matching CustomFormat. Click the calendar header to drill up; click a cell to select (or drill down when the view was raised from Days). " +
                "Left column is native WinForms (format only). Switch themes to confirm the drop-down stays themed."
        };

        var theme = new KryptonThemeComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        _valueLabel = new KryptonLabel
        {
            Dock = DockStyle.Bottom,
            Height = 28,
            Values = { Text = @"Value: (open a picker)" }
        };

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            Padding = new Padding(12)
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        var native = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = @"MMMM yyyy",
            Width = 220
        };

        var kryptonDays = CreatePicker(DateTimePickerFormat.Long, string.Empty, MonthCalendarView.Days);
        var kryptonMonths = CreatePicker(DateTimePickerFormat.Custom, @"MMMM yyyy", MonthCalendarView.Months);
        var kryptonYears = CreatePicker(DateTimePickerFormat.Custom, @"yyyy", MonthCalendarView.Years);

        var monthCalendar = new KryptonMonthCalendar
        {
            CalendarView = MonthCalendarView.Months,
            ShowToday = true,
            MaxSelectionCount = 1
        };
        monthCalendar.DateChanged += (_, e) => UpdateValue($"Standalone calendar: {e.Start:MMMM yyyy}");

        table.Controls.Add(Labeled(@"Native DateTimePicker (MMMM yyyy caption only)", native), 0, 0);
        table.Controls.Add(Labeled(@"KryptonDateTimePicker — CalendarView.Days", kryptonDays), 1, 0);
        table.Controls.Add(Labeled(@"KryptonDateTimePicker — CalendarView.Months + MMMM yyyy", kryptonMonths), 0, 1);
        table.Controls.Add(Labeled(@"KryptonDateTimePicker — CalendarView.Years + yyyy", kryptonYears), 1, 1);

        var calendarHost = Labeled(@"KryptonMonthCalendar — CalendarView.Months (click header to years)", monthCalendar);
        table.Controls.Add(calendarHost, 0, 2);
        table.SetColumnSpan(calendarHost, 2);
        table.SetRowSpan(calendarHost, 2);

        Controls.Add(table);
        Controls.Add(_valueLabel);
        Controls.Add(theme);
        Controls.Add(instructions);
    }

    private KryptonDateTimePicker CreatePicker(DateTimePickerFormat format, string customFormat, MonthCalendarView view)
    {
        var picker = new KryptonDateTimePicker
        {
            Format = format,
            CustomFormat = customFormat,
            CalendarView = view,
            Width = 240,
            Value = DateTime.Today
        };
        picker.ValueChanged += (_, _) => UpdateValue($"Picker ({view}): {picker.Value:yyyy-MM-dd}");
        return picker;
    }

    private static Control Labeled(string caption, Control inner)
    {
        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        var label = new KryptonLabel
        {
            Dock = DockStyle.Top,
            Values = { Text = caption }
        };
        inner.Dock = inner is KryptonMonthCalendar ? DockStyle.None : DockStyle.Top;
        if (inner is KryptonMonthCalendar)
        {
            inner.Location = new Point(0, 28);
        }
        panel.Controls.Add(inner);
        panel.Controls.Add(label);
        return panel;
    }

    private void UpdateValue(string text) => _valueLabel.Values.Text = text;
}
