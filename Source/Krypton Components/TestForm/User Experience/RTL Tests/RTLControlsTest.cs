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
/// Comprehensive test form demonstrating Right-to-Left (RTL) support for KryptonMonthCalendar.
/// </summary>
public partial class RTLControlsTest : KryptonForm
{
    public RTLControlsTest()
    {
        InitializeComponent();
        InitializeRtlDemo();
    }

    private void InitializeRtlDemo()
    {
        // Set form icon
        Icon = SystemIcons.Application;

        // Setup examples
        SetupRtlToggleExample();
        SetupCalendarExamples();
        SetupFeaturesExample();
        SetupOtherControlsExample();

        // Setup property grid
        propertyGrid.SelectedObject = calendarLtr;

        // Update status
        UpdateStatus("RTL support demo initialized. All VisualSimpleBase controls (KryptonLabel, KryptonCheckBox, KryptonRadioButton, KryptonTrackBar, KryptonHeader, KryptonColorButton, KryptonCommandLinkButton, KryptonDropButton, KryptonBreadCrumb, KryptonMonthCalendar) now inherit RTL support. Use controls below to test RTL layout.");
    }

    private void SetupRtlToggleExample()
    {
        // Example 1: Toggle RTL on LTR calendar
        lblExample1.Text = "Example 1: Toggle RTL layout";
        btnToggleRtl.Text = "Toggle RTL";
        btnToggleRtl.Click += BtnToggleRtl_Click;

        UpdateRtlStatus();
    }

    private void SetupCalendarExamples()
    {
        // Example 2: Setup RTL calendar
        lblExample2.Text = "Example 2: LTR calendar (toggleable)";
        calendarRtl.RightToLeft = RightToLeft.Yes;
        calendarRtl.RightToLeftLayout = true;
        calendarRtl.SelectionStart = DateTime.Now;
        calendarRtl.SelectionEnd = DateTime.Now;

        // Update label for RTL calendar
        lblExample3.Text = "Example 3: Pre-configured RTL calendar";

        // Example 4: Multi-month calendar  
        calendarMultiMonth.CalendarDimensions = new Size(2, 2);
        calendarMultiMonth.RightToLeft = RightToLeft.Yes;
        calendarMultiMonth.RightToLeftLayout = true;

        // Wire up selection events
        calendarLtr.DateSelected += Calendar_DateSelected;
        calendarRtl.DateSelected += Calendar_DateSelected;
        calendarMultiMonth.DateSelected += Calendar_DateSelected;
    }

    private void SetupFeaturesExample()
    {
        // Example 4: Calendar with features
        lblExample4.Text = "Example 4: Calendar with week numbers and features";
        calendarFeatures.ShowWeekNumbers = true;
        calendarFeatures.RightToLeft = RightToLeft.Yes;
        calendarFeatures.RightToLeftLayout = true;
        calendarFeatures.ShowToday = true;
        calendarFeatures.ShowTodayCircle = true;

        // Add some bolded dates
        calendarFeatures.AddBoldedDate(DateTime.Now.AddDays(5));
        calendarFeatures.AddBoldedDate(DateTime.Now.AddDays(10));
        calendarFeatures.AddMonthlyBoldedDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15));
    }

    private void SetupOtherControlsExample()
    {
        // Example 5: Show all VisualSimpleBase controls that now have RTL support
        // (This demonstrates the global nature of the implementation)
        lblExample5.Values.Text = "All VisualSimpleBase controls now inherit RTL support: " +
            "KryptonBreadCrumb, KryptonLabel, KryptonColorButton, KryptonCommandLinkButton, " +
            "KryptonMonthCalendar, KryptonRadioButton, KryptonCheckBox, KryptonDropButton, " +
            "KryptonHeader, KryptonTrackBar. RTL support is enabled via the RightToLeft and RightToLeftLayout properties inherited from VisualSimpleBase.";
    }

    private void BtnToggleRtl_Click(object? sender, EventArgs e)
    {
        bool newRtlValue = !calendarLtr.RightToLeftLayout;

        calendarLtr.RightToLeft = newRtlValue ? RightToLeft.Yes : RightToLeft.No;
        calendarLtr.RightToLeftLayout = newRtlValue;

        UpdateRtlStatus();
        UpdateStatus($"RTL layout toggled to: {newRtlValue}");
    }

    private void UpdateRtlStatus()
    {
        bool isRtl = calendarLtr.RightToLeft == RightToLeft.Yes && calendarLtr.RightToLeftLayout;
        lblRtlStatus.Text = $"RTL Layout: {(isRtl ? "Enabled" : "Disabled")}";
    }

    private void Calendar_DateSelected(object? sender, DateRangeEventArgs e)
    {
        if (sender is KryptonMonthCalendar calendar)
        {
            string rtlInfo = calendar.RightToLeft == RightToLeft.Yes && calendar.RightToLeftLayout
                ? " (RTL)"
                : " (LTR)";
            UpdateStatus($"Date selected: {e.Start:yyyy-MM-dd} to {e.End:yyyy-MM-dd}{rtlInfo}");
        }
    }

    private void UpdateStatus(string message)
    {
        lblStatus.Text = $"Status: {message}";
        lblStatus.Refresh();
    }

    private void BtnApplyToAll_Click(object? sender, EventArgs e)
    {
        bool newRtlValue = calendarLtr.RightToLeft == RightToLeft.Yes && calendarLtr.RightToLeftLayout;

        calendarRtl.RightToLeft = newRtlValue ? RightToLeft.Yes : RightToLeft.No;
        calendarRtl.RightToLeftLayout = newRtlValue;

        calendarMultiMonth.RightToLeft = newRtlValue ? RightToLeft.Yes : RightToLeft.No;
        calendarMultiMonth.RightToLeftLayout = newRtlValue;

        calendarFeatures.RightToLeft = newRtlValue ? RightToLeft.Yes : RightToLeft.No;
        calendarFeatures.RightToLeftLayout = newRtlValue;

        UpdateStatus($"RTL layout applied to all calendars: {newRtlValue}");
    }

    private void PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
    {
        if (propertyGrid.SelectedObject == calendarLtr)
        {
            calendarLtr.Refresh();
            UpdateRtlStatus();

            // If RightToLeft or RightToLeftLayout changed, update status display
            if (e.ChangedItem?.Label == "RightToLeft" || e.ChangedItem?.Label == "RightToLeftLayout")
            {
                UpdateRtlStatus();
            }

            UpdateStatus($"Property changed: {e.ChangedItem?.Label} = {e.ChangedItem?.Value}");
        }
    }
}
