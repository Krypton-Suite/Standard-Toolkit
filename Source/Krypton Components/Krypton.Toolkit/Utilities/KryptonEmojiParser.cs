#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using HttpClient = System.Net.Http.HttpClient;

namespace Krypton.Toolkit;

public class KryptonEmojiParser
{
    #region Implementation

    public static readonly Lazy<Task<List<KryptonEmojiInfo>>> _emojiListLazy =
        new Lazy<Task<List<KryptonEmojiInfo>>>(() => LoadEmojisInternalAsync(KryptonEmojiListType.Latest));

    public static Task<List<KryptonEmojiInfo>> GetEmojisAsync() => _emojiListLazy.Value;

    private static async Task<List<KryptonEmojiInfo>> LoadEmojisInternalAsync(KryptonEmojiListType emojiListType)
    {
        var emojiList = new List<KryptonEmojiInfo>();

        using var client = new HttpClient();

        var emojiListUrl = emojiListType switch
        {
            KryptonEmojiListType.Latest => GlobalStaticValues.DEFAULT_LATEST_EMOJI_LIST_URL,
            KryptonEmojiListType.Public => GlobalStaticValues.DEFAULT_PUBLIC_EMOJI_LIST_URL,
            _ => throw new ArgumentException("Invalid emoji list type specified.")
        };

        var content = await client.GetStringAsync(emojiListUrl);
        var lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawLine in lines)
        {
#if NET8_0_OR_GREATER
                var line = rawLine.AsSpan().Trim();

                if (line.Length == 0 || line[0] == '#')
                    continue;

                int semiIndex = line.IndexOf(';');
                if (semiIndex == -1)
                    continue;

                var codeSpan = line.Slice(0, semiIndex).Trim();
                var rest = line.Slice(semiIndex + 1).TrimStart();

                if (!rest.StartsWith("fully-qualified", StringComparison.Ordinal))
                    continue;

                int hashIndex = rest.IndexOf('#');
                if (hashIndex == -1)
                    continue;

                var afterHash = rest.Slice(hashIndex + 1).TrimStart();
                int spaceIndex = afterHash.IndexOf(' ');
                if (spaceIndex == -1)
                    continue;

                var glyph = afterHash.Slice(0, spaceIndex).ToString();
                var name = afterHash.Slice(spaceIndex + 1).ToString();
                var codepoints = codeSpan.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
#else
            var line = rawLine.Trim();

            if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
            {
                continue;
            }

            int semiIndex = line.IndexOf(';');
            if (semiIndex == -1)
            {
                continue;
            }

            var codeString = line.Substring(0, semiIndex).Trim();
            var rest = line.Substring(semiIndex + 1).TrimStart();

            if (!rest.StartsWith("fully-qualified", StringComparison.Ordinal))
            {
                continue;
            }

            int hashIndex = rest.IndexOf('#');
            if (hashIndex == -1)
            {
                continue;
            }

            var afterHash = rest.Substring(hashIndex + 1).TrimStart();
            int spaceIndex = afterHash.IndexOf(' ');
            if (spaceIndex == -1)
            {
                continue;
            }

            var glyph = afterHash.Substring(0, spaceIndex);
            var name = afterHash.Substring(spaceIndex + 1);
            var codepoints = codeString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
#endif

            emojiList.Add(new KryptonEmojiInfo
            {
                Glyph = glyph,
                Name = name,
                Codepoints = codepoints
            });
        }

        return emojiList;
    }

    #endregion
}