#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Exercises <see cref="KryptonTimeline"/> layouts and <see cref="KryptonDateTimeline"/> bars, milestones, and pan/zoom.
/// </summary>
public partial class TimelineDemo : KryptonForm
{
    public TimelineDemo()
    {
        InitializeComponent();
    }

    private void TimelineDemo_Load(object? sender, EventArgs e)
    {
        cmbOrientation.Items.Add(Orientation.Horizontal);
        cmbOrientation.Items.Add(Orientation.Vertical);
        cmbOrientation.SelectedItem = Orientation.Horizontal;

        cmbLayout.Items.Add(TimelineLayout.Alternate);
        cmbLayout.Items.Add(TimelineLayout.Compact);
        cmbLayout.Items.Add(TimelineLayout.SnapIcon);
        cmbLayout.SelectedItem = TimelineLayout.Alternate;

        cmbDateStyle.Items.Add(DateTimelineStyle.Chart);
        cmbDateStyle.Items.Add(DateTimelineStyle.Vis);
        cmbDateStyle.SelectedItem = DateTimelineStyle.Chart;

        SeedSteps();
        SeedDates();
        RefreshStatus("Drag a step to reorder it. Drag a date item to move it. Custom colours edits ItemStyles on both timelines.");
    }

    private void SeedSteps()
    {
        timeline.Items.Clear();
        timeline.Items.Add(new KryptonTimelineItem
        {
            StartText = "Research",
            EndText = "Discover",
            StartUseBox = true,
            Style = TimelineItemStyle.Completed
        });
        timeline.Items.Add(new KryptonTimelineItem
        {
            StartText = "Design",
            EndText = "Prototype",
            EndUseBox = true,
            Style = TimelineItemStyle.Hatched
        });
        timeline.Items.Add(new KryptonTimelineItem
        {
            StartText = "Build",
            EndText = "Implement",
            StartUseBox = true,
            EndUseBox = true,
            Style = TimelineItemStyle.Delayed
        });
        timeline.Items.Add(new KryptonTimelineItem
        {
            StartText = "Ship",
            EndText = "Release",
            Style = TimelineItemStyle.Cancelled
        });
        timeline.SelectedIndex = 0;
    }

    private void SeedDates()
    {
        dateTimeline.Items.Clear();
        dateTimeline.Items.Add(new KryptonDateTimelineItem
        {
            Caption = "Design",
            Details = "Explore the layout",
            Start = new DateTime(2026, 10, 1),
            Finish = new DateTime(2026, 10, 18),
            Row = 0,
            Style = TimelineItemStyle.Completed
        });
        dateTimeline.Items.Add(new KryptonDateTimelineItem
        {
            Caption = "Build",
            Details = "Implement both controls",
            Start = new DateTime(2026, 10, 12),
            Finish = new DateTime(2026, 11, 20),
            Row = 0,
            Style = TimelineItemStyle.Hatched
        });
        dateTimeline.Items.Add(new KryptonDateTimelineItem
        {
            Caption = "Review",
            Details = "Palette and designer checks",
            Start = new DateTime(2026, 10, 25),
            Finish = new DateTime(2026, 11, 30),
            Row = 1,
            Style = TimelineItemStyle.Delayed
        });
        dateTimeline.Items.Add(new KryptonDateTimelineItem
        {
            Caption = "Alpha",
            Details = "Milestone",
            Start = new DateTime(2026, 10, 15),
            Finish = new DateTime(2026, 10, 15),
            Row = 1
        });
        dateTimeline.Items.Add(new KryptonDateTimelineItem
        {
            Caption = "Docs",
            Details = "Guide and examples",
            Start = new DateTime(2026, 10, 20),
            Finish = new DateTime(2026, 11, 25),
            Row = 2,
            Style = TimelineItemStyle.Cancelled
        });
        dateTimeline.Items.Add(new KryptonDateTimelineItem
        {
            Caption = "Ship",
            Details = "Milestone",
            Start = new DateTime(2026, 12, 1),
            Row = 2
        });
        dateTimeline.SelectedIndex = 0;
    }

    private void cmbOrientation_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbOrientation.SelectedItem is Orientation orientation)
        {
            timeline.Orientation = orientation;
            RefreshStatus($"Step orientation is {orientation}.");
        }
    }

    private void cmbLayout_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbLayout.SelectedItem is TimelineLayout layout)
        {
            timeline.Layout = layout;
            RefreshStatus($"Step layout is {layout}.");
        }
    }

    private void cmbDateStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbDateStyle.SelectedItem is DateTimelineStyle style)
        {
            dateTimeline.Style = style;
            RefreshStatus(style == DateTimelineStyle.Vis
                ? "Vis style: boxes stack above the axis. A single date has a stem."
                : "Chart style: bars and diamonds on rows.");
        }
    }

    private void kcmbTheme_SelectedIndexChanged(object? sender, EventArgs e) =>
        RefreshStatus($"Theme is {KryptonManager.CurrentGlobalPaletteMode}.");

    private void timeline_ItemClick(object? sender, KryptonTimelineItemEventArgs e) =>
        RefreshStatus($"Step {e.Index + 1}: {e.Item.StartText} / {e.Item.EndText} ({e.Item.Style}).");

    private void dateTimeline_ItemClick(object? sender, KryptonDateTimelineItemEventArgs e)
    {
        string kind = e.Item.IsMilestone ? "Milestone" : "Task";
        RefreshStatus($"{kind} {e.Item.Caption} ({e.Item.Style}) on row {e.Item.Row}. AutoRange={dateTimeline.AutoRange}.");
    }

    private void kchkCustomColours_CheckedChanged(object? sender, EventArgs e) => ApplyStyleColours(kchkCustomColours.Checked);

    private void ApplyStyleColours(bool custom)
    {
        Apply(timeline.ItemStyles, custom);
        Apply(dateTimeline.ItemStyles, custom);
        RefreshStatus(custom
            ? "ItemStyles replaces the built-in stripe, hatch, and mark. Empty a colour to restore that part."
            : "ItemStyles colours are empty, so each style uses its built-in colours.");
    }

    private static void Apply(TimelineItemStyleValues styles, bool custom)
    {
        if (!custom)
        {
            styles.Reset();
            return;
        }

        styles.Hatched.Hatch = Color.FromArgb(210, 30, 90, 200);
        styles.Hatched.Mark = Color.FromArgb(30, 90, 200);
        styles.Completed.Stripe = Color.FromArgb(123, 31, 162);
        styles.Completed.Mark = Color.White;
        styles.Delayed.Stripe = Color.FromArgb(0, 140, 160);
        styles.Delayed.Hatch = Color.FromArgb(170, 0, 140, 160);
        styles.Delayed.Mark = Color.FromArgb(0, 100, 120);
        styles.Cancelled.Stripe = Color.FromArgb(160, 40, 40);
        styles.Cancelled.Hatch = Color.FromArgb(150, 160, 40, 40);
        styles.Cancelled.Mark = Color.FromArgb(120, 20, 20);
    }

    private void RefreshStatus(string message) => kwlblStatus.Text = message;
}
