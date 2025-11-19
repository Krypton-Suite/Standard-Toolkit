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

/// <summary>
/// Provides functionality to download, parse, and cache a list of Unicode emojis from the official Unicode emoji-test.txt file.
/// </summary>
public class KryptonEmojiParser
{
    #region Implementation

    /// <summary>
    /// Lazy-loaded task that asynchronously loads the emoji list on first access using the <see cref="KryptonEmojiListType.Latest"/> source.
    /// </summary>
    public static readonly Lazy<Task<List<KryptonEmojiInfo>>> _emojiListLazy =
        new Lazy<Task<List<KryptonEmojiInfo>>>(() => LoadEmojisInternalAsync(KryptonEmojiListType.Latest));

    /// <summary>
    /// Asynchronously retrieves the emoji list. Uses a cached result on subsequent calls.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation, with a result of a <see cref="List{KryptonEmojiInfo}"/> containing the emoji data.
    /// </returns>
    public static Task<List<KryptonEmojiInfo>> GetEmojisAsync() => _emojiListLazy.Value;

    /// <summary>
    /// Asynchronously downloads and parses the official Unicode emoji-test.txt file into a list of emoji objects.
    /// </summary>
    /// <param name="emojiListType">
    /// Specifies whether to load the latest Unicode emoji test file or a public/fixed version.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation, with a result of a <see cref="List{KryptonEmojiInfo}"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when an invalid <see cref="KryptonEmojiListType"/> is provided.
    /// </exception>
    private static async Task<List<KryptonEmojiInfo>> LoadEmojisInternalAsync(KryptonEmojiListType emojiListType)
    {
        // Initialize an empty list to hold the parsed emoji information
        var emojiList = new List<KryptonEmojiInfo>();

        using var client = new HttpClient();

        // Determine the URL to fetch based on the specified emoji list type
        var emojiListUrl = emojiListType switch
        {
            KryptonEmojiListType.Latest => GlobalStaticValues.DEFAULT_LATEST_EMOJI_LIST_URL,
            KryptonEmojiListType.Public => GlobalStaticValues.DEFAULT_PUBLIC_EMOJI_LIST_URL,
            _ => throw new ArgumentException("Invalid emoji list type specified.")
        };

        // Fetch the emoji list content from the specified URL
        var content = await client.GetStringAsync(emojiListUrl);

        // Split the content into lines, removing any empty entries
        var lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // Iterate through each line in the content
        foreach (var rawLine in lines)
        {
#if NET8_0_OR_GREATER
            // Use Span<T> for better performance with string manipulation
            var line = rawLine.AsSpan().Trim();

            // Check if the line is empty or a comment
            if (line.Length == 0 || line[0] == '#')
            {
                continue;
            }

            int semiIndex = line.IndexOf(';');
            if (semiIndex == -1)
            {
                continue;
            }

            // Extract the code points and the rest of the line
            var codeSpan = line.Slice(0, semiIndex).Trim();

            // Check if the rest of the line starts with "fully-qualified"
            var rest = line.Slice(semiIndex + 1).TrimStart();

            // Ensure the rest of the line starts with "fully-qualified"
            if (!rest.StartsWith("fully-qualified", StringComparison.Ordinal))
            {
                continue;
            }

            int hashIndex = rest.IndexOf('#');
            if (hashIndex == -1)
            {
                continue;
            }

            // Extract the part after the hash symbol
            var afterHash = rest.Slice(hashIndex + 1).TrimStart();

            // Find the first space to separate the glyph and name
            int spaceIndex = afterHash.IndexOf(' ');
            if (spaceIndex == -1)
            {
                continue;
            }

            // Extract the glyph and name from the afterHash part
            var glyph = afterHash.Slice(0, spaceIndex).ToString();

            // Extract the name after the space
            var name = afterHash.Slice(spaceIndex + 1).ToString();

            // Split the code points into an array
            var codepoints = codeSpan.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
#else
            // Fallback for .NET versions before 8.0, using string manipulation
            var line = rawLine.Trim();

            // Check if the line is empty or a comment
            if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
            {
                continue;
            }

            int semiIndex = line.IndexOf(';');
            if (semiIndex == -1)
            {
                continue;
            }

            // Extract the code points and the rest of the line
            var codeString = line.Substring(0, semiIndex).Trim();

            // Check if the rest of the line starts with "fully-qualified"
            var rest = line.Substring(semiIndex + 1).TrimStart();

            // Ensure the rest of the line starts with "fully-qualified"
            if (!rest.StartsWith("fully-qualified", StringComparison.Ordinal))
            {
                continue;
            }

            // Find the index of the hash symbol to separate the glyph and name
            int hashIndex = rest.IndexOf('#');
            if (hashIndex == -1)
            {
                continue;
            }

            // Extract the part after the hash symbol
            var afterHash = rest.Substring(hashIndex + 1).TrimStart();

            // Find the first space to separate the glyph and name
            int spaceIndex = afterHash.IndexOf(' ');
            if (spaceIndex == -1)
            {
                continue;
            }

            // Extract the glyph and name from the afterHash part
            var glyph = afterHash.Substring(0, spaceIndex);

            // Extract the name after the space
            var name = afterHash.Substring(spaceIndex + 1);

            // Split the code points into an array
            var codepoints = codeString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
#endif
            // Create a new KryptonEmojiInfo object and add it to the list
            emojiList.Add(new KryptonEmojiInfo
            {
                Glyph = glyph,
                Name = name,
                CodePoints = codepoints
            });
        }

        // Return the populated list of emojis
        return emojiList;
    }

    #endregion
}