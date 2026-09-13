#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demonstrates Right-to-Left support for Toolkit controls (Issue #2379), including month calendar.
/// </summary>
public partial class RTLControlsTest : KryptonForm
{
    private KryptonGroupBox? _grpToolkitGallery;
    private KryptonContextMenu? _rtlContextMenu;

    public RTLControlsTest()
    {
        InitializeComponent();
        InitializeRtlDemo();

        // Screenshot / automation: TestForm.exe --demo RTLControlsTest --rtl
        foreach (string arg in Environment.GetCommandLineArgs())
        {
            if (string.Equals(arg, "--rtl", StringComparison.OrdinalIgnoreCase))
            {
                Shown += (_, _) => SetDualRtl(true);
                break;
            }
        }
    }

    /// <summary>
    /// Sets both RTL flags on this form and descendant Krypton controls.
    /// </summary>
    /// <param name="enabled">True for RightToLeft.Yes plus RightToLeftLayout; false for LTR.</param>
    public void SetDualRtl(bool enabled)
    {
        RightToLeft = enabled ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = enabled;
        ApplyDualRtl(this, enabled);

        calendarLtr.RightToLeft = RightToLeft;
        calendarLtr.RightToLeftLayout = enabled;
        calendarRtl.RightToLeft = RightToLeft;
        calendarRtl.RightToLeftLayout = enabled;
        calendarMultiMonth.RightToLeft = RightToLeft;
        calendarMultiMonth.RightToLeftLayout = enabled;
        calendarFeatures.RightToLeft = RightToLeft;
        calendarFeatures.RightToLeftLayout = enabled;

        UpdateRtlStatus();
        UpdateStatus($"Dual RTL flags {(enabled ? "on" : "off")} (RightToLeft + RightToLeftLayout).");
        PerformLayout();
        Refresh();
    }

    private void InitializeRtlDemo()
    {
        Icon = SystemIcons.Application;

        SetupRtlToggleExample();
        SetupCalendarExamples();
        SetupFeaturesExample();
        SetupOtherControlsExample();

        propertyGrid.SelectedObject = calendarLtr;

        UpdateStatus("Issue #2379: toggle RightToLeft + RightToLeftLayout. Calendars plus NUD, lists, split, group, checkbox chrome.");
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
        lblExample5.Values.Text =
            "Issue #2379 gallery: NumericUpDown, DomainUpDown, ComboBox, TextBox, ListBox, CheckedListBox, " +
            "RichTextBox, SplitContainer, GroupBox, HeaderGroup, CheckBox, RadioButton, Panel, " +
            "CommandLink, ContextMenu, PropertyGrid, TaskDialog. " +
            "Use Toggle RTL to set both flags (same contract as KryptonForm / Ribbon).";

        _grpToolkitGallery = new KryptonGroupBox
        {
            Location = new Point(12, 650),
            Size = new Size(1078, 400),
            Values = { Heading = "Toolkit control gallery (#2379)" }
        };

        var split = new KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterDistance = 360
        };

        var left = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(8)
        };
        left.Controls.Add(new KryptonCheckBox { Text = "CheckBox glyph", AutoSize = true });
        left.Controls.Add(new KryptonRadioButton { Text = "RadioButton glyph", AutoSize = true, Checked = true });
        left.Controls.Add(new KryptonNumericUpDown { Value = 12, Width = 160 });
        var domain = new KryptonDomainUpDown { Width = 160 };
        domain.Items.Add("Alpha");
        domain.Items.Add("Beta");
        domain.Items.Add("Gamma");
        domain.SelectedIndex = 0;
        left.Controls.Add(domain);
        left.Controls.Add(new KryptonComboBox { Width = 200, Text = "Combo drop" });
        left.Controls.Add(new KryptonTextBox { Width = 200, Text = "TextBox" });

        var list = new KryptonListBox { Width = 200, Height = 70 };
        list.Items.Add("List one");
        list.Items.Add("List two");
        left.Controls.Add(list);

        var checkedList = new KryptonCheckedListBox { Width = 200, Height = 70 };
        checkedList.Items.Add("Checked one");
        checkedList.Items.Add("Checked two");
        left.Controls.Add(checkedList);
        var panel = new KryptonPanel { Width = 200, Height = 36 };
        panel.Controls.Add(new KryptonLabel
        {
            Location = new Point(4, 8),
            AutoSize = true,
            Values = { Text = "Panel" }
        });
        left.Controls.Add(panel);

        var commandLink = new Krypton.Toolkit.Utilities.KryptonCommandLinkButton
        {
            Width = 240,
            Height = 56
        };
        commandLink.CommandLinkTextValues.Heading = "CommandLink";
        commandLink.CommandLinkTextValues.Description = "Arrow mirrors with both flags.";
        left.Controls.Add(commandLink);

        _rtlContextMenu = new KryptonContextMenu();
        var menuItems = new KryptonContextMenuItems();
        menuItems.Items.Add(new KryptonContextMenuItem("Item one"));
        var subItem = new KryptonContextMenuItem("Submenu");
        subItem.Items.Add(new KryptonContextMenuItem("Child item"));
        menuItems.Items.Add(subItem);
        _rtlContextMenu.Items.Add(menuItems);
        var menuButton = new KryptonButton { Text = "Context menu", Width = 200 };
        menuButton.Click += (_, _) => _rtlContextMenu!.Show(menuButton);
        left.Controls.Add(menuButton);

        left.Controls.Add(new KryptonPropertyGrid
        {
            Width = 200,
            Height = 88,
            SelectedObject = menuButton
        });

        var taskDialogButton = new KryptonButton { Text = "TaskDialog", Width = 200 };
        taskDialogButton.Click += (_, _) => ShowRtlTaskDialog();
        left.Controls.Add(taskDialogButton);

        split.Panel1.Controls.Add(left);

        var header = new KryptonHeaderGroup
        {
            Dock = DockStyle.Fill
        };
        header.ValuesPrimary.Heading = "HeaderGroup";
        header.Panel.Controls.Add(new KryptonRichTextBox
        {
            Dock = DockStyle.Fill,
            Text = "Rich text sample. Toggle dual RTL flags; reading order follows RightToLeft."
        });
        split.Panel2.Controls.Add(header);

        _grpToolkitGallery.Panel.Controls.Add(split);
        Controls.Add(_grpToolkitGallery);
        _grpToolkitGallery.BringToFront();
        lblStatus.BringToFront();
    }

    private void ShowRtlTaskDialog()
    {
        using var taskDialog = new KryptonTaskDialog();
        taskDialog.Heading.Text = "RTL TaskDialog";
        taskDialog.Heading.IconType = KryptonTaskDialogIconType.ShieldInformation;
        taskDialog.Heading.Visible = true;
        taskDialog.Content.Text = "Heading icon, content, and footer follow both RTL flags from this owner form.";
        taskDialog.Content.Visible = true;
        taskDialog.FooterBar.CommonButtons.Buttons = KryptonTaskDialogCommonButtonTypes.OK;
        taskDialog.FooterBar.Visible = true;
        taskDialog.ShowDialog(this);
    }

    private static void ApplyDualRtl(Control parent, bool enabled)
    {
        foreach (Control child in parent.Controls)
        {
            child.RightToLeft = enabled ? RightToLeft.Yes : RightToLeft.No;
            switch (child)
            {
                case VisualControlBase visual:
                    visual.RightToLeftLayout = enabled;
                    break;
                case VisualPanel panel:
                    panel.RightToLeftLayout = enabled;
                    break;
            }

            ApplyDualRtl(child, enabled);
        }
    }

    private void BtnToggleRtl_Click(object? sender, EventArgs e)
    {
        bool newRtlValue = !calendarLtr.RightToLeftLayout;
        SetDualRtl(newRtlValue);
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
