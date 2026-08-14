#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Builds a small AutoSize host of <see cref="KryptonLabel"/> / <see cref="KryptonLinkLabel"/> from a limited HTML fragment
/// (plain text, <c>&lt;br&gt;</c>, and <c>&lt;a href="..."&gt;</c>) for use with <see cref="KryptonToolTip.SetToolTip(Control, Control, bool)"/>.
/// </summary>
public static class KryptonHtmlToolTipContent
{
    /// <summary>
    /// Creates hosted tooltip content from a simple HTML fragment.
    /// </summary>
    /// <param name="html">Fragment supporting text, <c>&lt;br&gt;</c>, and <c>&lt;a href="url"&gt;text&lt;/a&gt;</c>.</param>
    /// <returns>An AutoSize <see cref="FlowLayoutPanel"/> suitable as tooltip hosted content.</returns>
    public static Control Create(string html)
    {
        var host = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            MaximumSize = new Size(320, 0),
            BackColor = Color.Transparent,
            Margin = Padding.Empty,
            Padding = new Padding(2)
        };

        if (string.IsNullOrEmpty(html))
        {
            return host;
        }

        string remaining = html
            .Replace(@"<br/>", @"<br>")
            .Replace(@"<br />", @"<br>")
            .Replace(@"<BR>", @"<br>")
            .Replace(@"<BR/>", @"<br>")
            .Replace(@"<BR />", @"<br>");

        while (remaining.Length > 0)
        {
            int br = IndexOfIgnoreCase(remaining, @"<br>");
            int aOpen = IndexOfIgnoreCase(remaining, @"<a ");
            int aOpen2 = IndexOfIgnoreCase(remaining, @"<a>");
            if (aOpen < 0 || (aOpen2 >= 0 && aOpen2 < aOpen))
            {
                aOpen = aOpen2;
            }

            int next = MinPositive(br, aOpen);
            if (next < 0)
            {
                AddText(host, Decode(remaining));
                break;
            }

            if (next > 0)
            {
                AddText(host, Decode(remaining.Substring(0, next)));
                remaining = remaining.Substring(next);
                continue;
            }

            if (br == 0)
            {
                host.SetFlowBreak(host.Controls.Count == 0 ? AddText(host, " ") : host.Controls[host.Controls.Count - 1], true);
                remaining = remaining.Substring(4);
                continue;
            }

            int hrefStart = IndexOfIgnoreCase(remaining, @"href=");
            int tagEnd = remaining.IndexOf('>');
            int close = IndexOfIgnoreCase(remaining, @"</a>");
            if (hrefStart < 0 || tagEnd < 0 || close < 0 || hrefStart > tagEnd)
            {
                AddText(host, Decode(remaining));
                break;
            }

            string hrefAttr = remaining.Substring(hrefStart + 5, tagEnd - (hrefStart + 5)).Trim();
            hrefAttr = hrefAttr.Trim('"', '\'', ' ');
            string linkText = Decode(remaining.Substring(tagEnd + 1, close - tagEnd - 1));
            var link = new KryptonLinkLabel { AutoSize = true };
            link.Values.Text = string.IsNullOrEmpty(linkText) ? hrefAttr : linkText;
            string url = hrefAttr;
            link.LinkClicked += (_, _) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
                }
                catch
                {
                    // Ignore missing associations.
                }
            };
            host.Controls.Add(link);
            remaining = remaining.Substring(close + 4);
        }

        return host;
    }

    private static Control AddText(FlowLayoutPanel host, string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return host;
        }

        var label = new KryptonLabel { AutoSize = true };
        label.Values.Text = text;
        host.Controls.Add(label);
        return label;
    }

    private static string Decode(string value) =>
        value.Replace(@"&amp;", @"&")
            .Replace(@"&lt;", @"<")
            .Replace(@"&gt;", @">")
            .Replace(@"&quot;", "\"");

    private static int IndexOfIgnoreCase(string haystack, string needle) =>
        haystack.IndexOf(needle, StringComparison.OrdinalIgnoreCase);

    private static int MinPositive(int a, int b)
    {
        if (a < 0)
        {
            return b;
        }

        if (b < 0)
        {
            return a;
        }

        return Math.Min(a, b);
    }
}
