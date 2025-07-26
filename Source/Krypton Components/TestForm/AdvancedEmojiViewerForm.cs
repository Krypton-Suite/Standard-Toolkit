#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class AdvancedEmojiViewerForm : KryptonForm
{
    private List<KryptonEmojiInfo> _emojiList;

    public AdvancedEmojiViewerForm()
    {
        InitializeComponent();
    }

    private async void AdvancedEmojiViewerForm_Load(object sender, EventArgs e)
    {
        try
        {
            _emojiList = await KryptonEmojiParser.GetEmojisAsync();

            BindToGrid(_emojiList);
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }
    }

    private void ktxtSearch_TextChanged(object sender, EventArgs e)
    {
        if (_emojiList == null)
        {
            return;
        }

        var filtered = _emojiList
            .Where(e => e.Name.IndexOf(ktxtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        BindToGrid(filtered);
    }

    private void BindToGrid(List<KryptonEmojiInfo> list)
    {
        // Add helper property for display
        var bound = list.Select(e => new
        {
            e.Glyph,
            e.Name,
            CodepointsText = string.Join(" ", e.CodePoints)
        }).ToList();

        kdvEmojis.DataSource = bound;
    }

    private void kcCopyEmoji_Execute(object sender, EventArgs e)
    {
        if (kdvEmojis.SelectedRows.Count > 0)
        {
            var selectedRow = kdvEmojis.SelectedRows[0];

            var glyph = selectedRow.Cells["Glyph"].Value.ToString();

            var name = selectedRow.Cells["Name"].Value.ToString();

            var codepoints = selectedRow.Cells["CodePointsText"].Value.ToString();

            string copiedText = $"{glyph} - {name} ({codepoints})";

            Clipboard.SetText(copiedText);
        }
        else
        {
            KryptonMessageBox.Show("Please select an emoji to copy.", "No Emoji Selected", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
        }
    }

    private void kcmdCopyEmojiOnly_Execute(object sender, EventArgs e)
    {
        if (kdvEmojis.SelectedRows.Count > 0)
        {
            var selectedRow = kdvEmojis.SelectedRows[0];

            var glyph = selectedRow.Cells["Glyph"].Value.ToString();

            Clipboard.SetText(glyph);
        }
        else
        {
            KryptonMessageBox.Show("Please select an emoji to copy.", "No Emoji Selected", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
        }
    }
}