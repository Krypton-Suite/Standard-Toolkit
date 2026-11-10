#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Drawing;

/// <summary>
/// Reads the root SVG viewport. Direct2D does not return the document's intrinsic size.
/// </summary>
internal static class KryptonSvgIntrinsicSize
{
    internal static bool TryGet(string markup, out float width, out float height)
    {
        width = 0f;
        height = 0f;
        var tag = FindRootStartTag(markup);
        if (tag == null)
        {
            return false;
        }

        var viewBox = GetAttribute(tag, "viewBox");
        if (viewBox != null && TryViewBox(viewBox, out width, out height))
        {
            return true;
        }

        return TryLength(GetAttribute(tag, "width"), out width)
               && TryLength(GetAttribute(tag, "height"), out height)
               && width > 0f
               && height > 0f;
    }

    private static string? FindRootStartTag(string markup)
    {
        var index = 0;
        while (index < markup.Length)
        {
            var start = markup.IndexOf('<', index);
            if (start < 0 || start + 1 >= markup.Length)
            {
                return null;
            }

            if (HasMatchAt(markup, start + 1, "!--"))
            {
                var commentEnd = markup.IndexOf("-->", start + 4, StringComparison.Ordinal);
                index = commentEnd < 0 ? markup.Length : commentEnd + 3;
                continue;
            }

            if (markup[start + 1] == '?' || markup[start + 1] == '!')
            {
                var end = markup.IndexOf('>', start + 2);
                index = end < 0 ? markup.Length : end + 1;
                continue;
            }

            if (IsSvgName(markup, start + 1))
            {
                return ReadTag(markup, start);
            }

            var next = markup.IndexOf('>', start + 1);
            index = next < 0 ? markup.Length : next + 1;
        }

        return null;
    }

    private static bool IsSvgName(string markup, int index)
    {
        if (!HasMatchAt(markup, index, "svg"))
        {
            return false;
        }

        var after = index + 3;
        return after >= markup.Length
               || char.IsWhiteSpace(markup[after])
               || markup[after] == '>'
               || markup[after] == '/';
    }

    private static string? ReadTag(string markup, int start)
    {
        var quoted = '\0';
        for (var i = start + 1; i < markup.Length; i++)
        {
            var ch = markup[i];
            if (quoted != '\0')
            {
                if (ch == quoted)
                {
                    quoted = '\0';
                }

                continue;
            }

            if (ch == '"' || ch == '\'')
            {
                quoted = ch;
                continue;
            }

            if (ch == '>')
            {
                return markup.Substring(start, i - start + 1);
            }
        }

        return null;
    }

    private static string? GetAttribute(string tag, string name)
    {
        var index = 0;
        while (index < tag.Length)
        {
            while (index < tag.Length && (char.IsWhiteSpace(tag[index]) || tag[index] == '<' || tag[index] == '/'))
            {
                index++;
            }

            if (index >= tag.Length || tag[index] == '>')
            {
                return null;
            }

            var nameStart = index;
            while (index < tag.Length && !char.IsWhiteSpace(tag[index]) && tag[index] != '=' && tag[index] != '>' && tag[index] != '/')
            {
                index++;
            }

            var attributeName = tag.Substring(nameStart, index - nameStart);
            while (index < tag.Length && char.IsWhiteSpace(tag[index]))
            {
                index++;
            }

            if (index >= tag.Length || tag[index] != '=')
            {
                continue;
            }

            index++;
            while (index < tag.Length && char.IsWhiteSpace(tag[index]))
            {
                index++;
            }

            if (index >= tag.Length)
            {
                return null;
            }

            string value;
            if (tag[index] == '"' || tag[index] == '\'')
            {
                var quote = tag[index];
                var valueStart = ++index;
                while (index < tag.Length && tag[index] != quote)
                {
                    index++;
                }

                value = tag.Substring(valueStart, index - valueStart);
                if (index < tag.Length)
                {
                    index++;
                }
            }
            else
            {
                var valueStart = index;
                while (index < tag.Length && !char.IsWhiteSpace(tag[index]) && tag[index] != '>')
                {
                    index++;
                }

                value = tag.Substring(valueStart, index - valueStart);
            }

            var localName = attributeName;
            var colon = attributeName.LastIndexOf(':');
            if (colon >= 0)
            {
                localName = attributeName.Substring(colon + 1);
            }

            if (string.Equals(localName, name, StringComparison.OrdinalIgnoreCase))
            {
                return value;
            }
        }

        return null;
    }

    private static bool TryViewBox(string viewBox, out float width, out float height)
    {
        width = 0f;
        height = 0f;
        var parts = viewBox.Split(new[] { ' ', '\t', '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 4)
        {
            return false;
        }

        return float.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out width)
               && float.TryParse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out height)
               && width > 0f
               && height > 0f
               && !float.IsInfinity(width)
               && !float.IsInfinity(height);
    }

    private static bool TryLength(string? raw, out float pixels)
    {
        pixels = 0f;
        if (raw == null || string.IsNullOrWhiteSpace(raw))
        {
            return false;
        }

        var text = raw.Trim();
        var unit = 1f;
        if (text.EndsWith("%", StringComparison.Ordinal))
        {
            return false;
        }

        if (text.Length > 2 && TryUnit(text.Substring(text.Length - 2), out var scale))
        {
            unit = scale;
            text = text.Substring(0, text.Length - 2);
        }

        if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
            || value <= 0f
            || float.IsInfinity(value)
            || float.IsNaN(value))
        {
            return false;
        }

        pixels = value * unit;
        return true;
    }

    /// <summary>
    /// Maps a two-letter SVG length unit to a scale from user units into 96-DPI pixels.
    /// </summary>
    private static bool TryUnit(string suffix, out float scale)
    {
        if (string.Equals(suffix, "px", StringComparison.OrdinalIgnoreCase))
        {
            scale = 1f;
        }
        else if (string.Equals(suffix, "in", StringComparison.OrdinalIgnoreCase))
        {
            scale = 96f;
        }
        else if (string.Equals(suffix, "cm", StringComparison.OrdinalIgnoreCase))
        {
            scale = 96f / 2.54f;
        }
        else if (string.Equals(suffix, "mm", StringComparison.OrdinalIgnoreCase))
        {
            scale = 96f / 25.4f;
        }
        else if (string.Equals(suffix, "pt", StringComparison.OrdinalIgnoreCase))
        {
            scale = 96f / 72f;
        }
        else if (string.Equals(suffix, "pc", StringComparison.OrdinalIgnoreCase))
        {
            scale = 16f;
        }
        else
        {
            scale = 0f;
            return false;
        }

        return true;
    }

    private static bool HasMatchAt(string text, int index, string value)
    {
        if (index < 0 || index + value.Length > text.Length)
        {
            return false;
        }

        return string.Compare(text, index, value, 0, value.Length, StringComparison.OrdinalIgnoreCase) == 0;
    }
}
