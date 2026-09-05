#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonTagInputControl"/>: wrap chips, Enter/comma commit, suggestions, category colours, and theme switching.
/// </summary>
public partial class KryptonTagInputDemo : KryptonForm
{
    public KryptonTagInputDemo()
    {
        InitializeComponent();
    }

    private void KryptonTagInputDemo_Load(object? sender, EventArgs e)
    {
        ktiTags.SetSuggestions(new[]
        {
            "Bug", "Feature", "Documentation", "Performance", "Security",
            "Urgent", "Blocked", "Needs Review", "Good First Issue"
        });
        ktiTags.SetCategoryColor("Bug", Color.IndianRed);
        ktiTags.SetCategoryColor("Feature", Color.SteelBlue);
        ktiTags.SetCategoryColor("Security", Color.DarkOrange);
        ktiTags.SetCategoryColor("Urgent", Color.MediumVioletRed);
        ktiTags.SetCategoryColor("Documentation", Color.SeaGreen);

        ktiTags.AddTag("Bug");
        ktiTags.AddTag("Needs Review");

        ktiTags.Values.CueHintText = @"Add a tag";
        RefreshTagList();
        Log(@"Demo ready. Type a tag and press Enter or comma. Backspace removes the last chip when the input is empty.");
    }

    private void ktiTags_TagAdded(object? sender, KryptonTagEventArgs e)
    {
        Log($"Added: {e.Tag}");
        RefreshTagList();
    }

    private void ktiTags_TagRemoved(object? sender, KryptonTagEventArgs e)
    {
        Log($"Removed: {e.Tag}");
        RefreshTagList();
    }

    private void ktiTags_TagAdding(object? sender, KryptonTagCancelEventArgs e)
    {
        if (string.Equals(e.Tag, @"reject", StringComparison.OrdinalIgnoreCase))
        {
            e.Cancel = true;
            Log(@"Cancelled: the demo rejects the tag 'reject'.");
        }
    }

    private void chkAllowDuplicates_CheckedChanged(object sender, EventArgs e) =>
        ktiTags.Values.AllowDuplicates = chkAllowDuplicates.Checked;

    private void chkReadOnly_CheckedChanged(object sender, EventArgs e) =>
        ktiTags.ReadOnly = chkReadOnly.Checked;

    private void chkCommitOnComma_CheckedChanged(object sender, EventArgs e) =>
        ktiTags.Values.CommitOnComma = chkCommitOnComma.Checked;

    private void chkShowRemove_CheckedChanged(object sender, EventArgs e) =>
        ktiTags.Values.ShowRemoveButton = chkShowRemove.Checked;

    private void chkAllowCustom_CheckedChanged(object sender, EventArgs e) =>
        ktiTags.Values.AllowCustomTags = chkAllowCustom.Checked;

    private void nudMaxTags_ValueChanged(object sender, EventArgs e) =>
        ktiTags.Values.MaxTags = (int)nudMaxTags.Value;

    private void kbtnAddUrgent_Click(object sender, EventArgs e)
    {
        if (!ktiTags.AddTag(@"Urgent"))
        {
            Log(@"Could not add Urgent (duplicate, max tags, or cancelled).");
        }
    }

    private void kbtnClear_Click(object sender, EventArgs e) => ktiTags.ClearTags();

    private void RefreshTagList()
    {
        klbTags.Items.Clear();
        foreach (var tag in ktiTags.Tags)
        {
            klbTags.Items.Add(tag);
        }

        kwlblStatus.Text = ktiTags.Tags.Count == 0
            ? @"No tags."
            : $"{ktiTags.Tags.Count} tag(s): {string.Join(@", ", ktiTags.Tags)}";
    }

    private void Log(string message)
    {
        krtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        krtbLog.SelectionStart = krtbLog.TextLength;
        krtbLog.ScrollToCaret();
    }
}
