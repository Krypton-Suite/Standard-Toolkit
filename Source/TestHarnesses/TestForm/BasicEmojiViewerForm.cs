#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class BasicEmojiViewerForm : KryptonForm
{
    private List<KryptonEmojiInfo> _emojiList;

    public BasicEmojiViewerForm()
    {
        InitializeComponent();
    }

    private async void EmojiViewerForm_Load(object sender, EventArgs e)
    {
        try
        {
            _emojiList = await KryptonEmojiParser.GetEmojisAsync();

            klbEmojis.DataSource = _emojiList;

            klbEmojis.DisplayMember = nameof(KryptonEmojiInfo.ToString);
        }
        catch (Exception exception)
        {
            KryptonExceptionHandler.CaptureException(exception);
        }
    }

    private void ktxtSearch_TextChanged(object sender, EventArgs e)
    {
        if (_emojiList == null)
        {
            return;
        }

        var filteredEmojis = _emojiList
            .Where(emoji => emoji.Name.IndexOf(ktxtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        klbEmojis.DataSource = filteredEmojis;
    }

    private void klbEmojis_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (klbEmojis.SelectedItem is KryptonEmojiInfo emoji)
        {
            klblEmojis.Text = $"{emoji.Glyph} - {emoji.Name}";
        }
    }
}