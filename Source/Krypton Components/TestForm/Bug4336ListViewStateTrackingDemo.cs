#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for issue #4336: <see cref="KryptonListView"/> hover uses <c>StateTracking</c> instead of Win32 hot-track.
/// </summary>
public partial class Bug4336ListViewStateTrackingDemo : KryptonForm
{
    public Bug4336ListViewStateTrackingDemo()
    {
        InitializeComponent();
    }

    private void Bug4336ListViewStateTrackingDemo_Load(object? sender, EventArgs e)
    {
        kcmbView.Items.AddRange(new object[] { View.Details, View.List, View.SmallIcon });
        kcmbView.SelectedItem = View.Details;
        Populate(lvNative);
        Populate(klvKrypton);
        ApplyItemToolTips(kchkItemToolTips.Checked);
        ApplyView();
        UpdateStatus();
    }

    private static void Populate(ListView listView)
    {
        listView.BeginUpdate();
        try
        {
            listView.Items.Clear();
            if (listView.Columns.Count == 0)
            {
                listView.Columns.Add("Item", 140);
                listView.Columns.Add("Status", 90);
                listView.Columns.Add("Notes", 140);
            }

            listView.Items.Add(new ListViewItem(new[] { "Alpha", "Ready", "Hover this row" }));
            listView.Items.Add(new ListViewItem(new[] { "Beta", "Busy", "Selected by default" }));
            listView.Items.Add(new ListViewItem(new[] { "Gamma", "Idle", "Win32 vs Krypton" }));
            listView.Items.Add(new ListViewItem(new[] { "Delta", "Ready", "StateTracking chrome" }));
            listView.Items.Add(new ListViewItem(new[] { "Epsilon", "Idle", "Theme switch" }));
            ApplyItemToolTipText(listView.Items);
            if (listView.Items.Count > 1)
            {
                listView.Items[1].Selected = true;
            }
        }
        finally
        {
            listView.EndUpdate();
        }
    }

    private static void Populate(KryptonListView listView)
    {
        listView.BeginUpdate();
        try
        {
            listView.Items.Clear();
            if (listView.Columns.Count == 0)
            {
                listView.Columns.Add("Item", 140);
                listView.Columns.Add("Status", 90);
                listView.Columns.Add("Notes", 140);
            }

            listView.Items.Add(new ListViewItem(new[] { "Alpha", "Ready", "Hover this row" }));
            listView.Items.Add(new ListViewItem(new[] { "Beta", "Busy", "Selected by default" }));
            listView.Items.Add(new ListViewItem(new[] { "Gamma", "Idle", "Win32 vs Krypton" }));
            listView.Items.Add(new ListViewItem(new[] { "Delta", "Ready", "StateTracking chrome" }));
            listView.Items.Add(new ListViewItem(new[] { "Epsilon", "Idle", "Theme switch" }));
            ApplyItemToolTipText(listView.Items);
            if (listView.Items.Count > 1)
            {
                listView.Items[1].Selected = true;
            }
        }
        finally
        {
            listView.EndUpdate();
        }
    }

    private static void ApplyItemToolTipText(ListView.ListViewItemCollection items)
    {
        foreach (ListViewItem item in items)
        {
            string notes = item.SubItems.Count > 2 ? item.SubItems[2].Text : string.Empty;
            item.ToolTipText = string.IsNullOrEmpty(notes)
                ? $"{item.Text} item"
                : $"{item.Text}: {notes}";
        }
    }

    private void ApplyItemToolTips(bool enabled)
    {
        lvNative.ShowItemToolTips = enabled;
        klvKrypton.ShowItemToolTips = enabled;
    }

    private void ApplyView()
    {
        var view = kcmbView.SelectedItem is View selected ? selected : View.Details;
        lvNative.View = view;
        klvKrypton.View = view;
    }

    private void kcmbView_SelectedIndexChanged(object? sender, EventArgs e) => ApplyView();

    private void kchkHotTracking_CheckedChanged(object? sender, EventArgs e)
    {
        lvNative.HotTracking = kchkHotTracking.Checked;
        klvKrypton.HotTracking = kchkHotTracking.Checked;
        UpdateStatus();
    }

    private void kchkCheckBoxes_CheckedChanged(object? sender, EventArgs e)
    {
        lvNative.CheckBoxes = kchkCheckBoxes.Checked;
        klvKrypton.CheckBoxes = kchkCheckBoxes.Checked;
    }

    private void kchkItemToolTips_CheckedChanged(object? sender, EventArgs e)
    {
        ApplyItemToolTips(kchkItemToolTips.Checked);
        UpdateStatus();
    }

    private void kchkContrastTracking_CheckedChanged(object? sender, EventArgs e)
    {
        if (kchkContrastTracking.Checked)
        {
            klvKrypton.StateTracking.Node.Back.Draw = InheritBool.True;
            klvKrypton.StateTracking.Node.Back.Color1 = Color.FromArgb(255, 186, 0);
            klvKrypton.StateTracking.Node.Back.Color2 = Color.FromArgb(255, 220, 100);
            klvKrypton.StateTracking.Node.Content.ShortText.Color1 = Color.Black;
            klvKrypton.StateCheckedTracking.Node.Back.Draw = InheritBool.True;
            klvKrypton.StateCheckedTracking.Node.Back.Color1 = Color.FromArgb(255, 140, 0);
            klvKrypton.StateCheckedTracking.Node.Content.ShortText.Color1 = Color.Black;
        }
        else
        {
            klvKrypton.StateTracking.Node.Back.Draw = InheritBool.Inherit;
            klvKrypton.StateTracking.Node.Back.Color1 = Color.Empty;
            klvKrypton.StateTracking.Node.Back.Color2 = Color.Empty;
            klvKrypton.StateTracking.Node.Content.ShortText.Color1 = Color.Empty;
            klvKrypton.StateCheckedTracking.Node.Back.Draw = InheritBool.Inherit;
            klvKrypton.StateCheckedTracking.Node.Back.Color1 = Color.Empty;
            klvKrypton.StateCheckedTracking.Node.Content.ShortText.Color1 = Color.Empty;
        }

        klvKrypton.Invalidate();
        UpdateStatus();
    }

    private void klvKrypton_ItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e) =>
        UpdateStatus();

    private void UpdateStatus()
    {
        int selected = klvKrypton.SelectedItems.Count;
        string tracking = kchkContrastTracking.Checked
            ? "StateTracking override = orange"
            : "StateTracking = palette default";
        klblStatus.Values.Text =
            $"Krypton selected = {selected}. Hover a row: native uses Win32 hot-track; Krypton uses {tracking}."
            + (kchkItemToolTips.Checked ? " Item tooltips: native Win32 vs KryptonToolTip." : string.Empty);
    }
}
