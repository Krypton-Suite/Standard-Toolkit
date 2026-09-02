#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shared scan/apply logic for <see cref="KryptonPaletteFileListBox"/>,
/// <see cref="KryptonPaletteFileComboBox"/>, and <see cref="KryptonPaletteFileTreeView"/>.
/// </summary>
internal sealed class KryptonPaletteFileThemeSelectorController
{
    internal KryptonPaletteFileThemeSelectorController()
    {
        Manager = new KryptonManager();
        Palette = new KryptonCustomPaletteBase();
    }

    internal string PaletteDirectory { get; set; } = string.Empty;

    internal bool SearchSubdirectories { get; set; }

    internal bool IncludeKpalx { get; set; } = true;

    internal bool IncludeKpal { get; set; } = true;

    // ToDo V120 LTS: Remove IncludeXml (default true). Folder selectors should list .kpalx / .kpal only.
    internal bool IncludeXml { get; set; } = true;

    internal bool AutoApply { get; set; } = true;

    internal bool LoadThumbnails { get; set; }

    internal Size ThumbnailSize { get; set; } = new Size(32, 32);

    internal bool SuppressSelection { get; set; }

    internal KryptonManager Manager { get; set; }

    internal KryptonCustomPaletteBase Palette { get; }

    internal KryptonPaletteFileThemeItem[] Scan() =>
        KryptonPaletteFileThemeItem.FromDirectory(PaletteDirectory, SearchSubdirectories,
            IncludeKpalx, IncludeKpal, IncludeXml, LoadThumbnails);

    internal int Reload(IList items, KryptonPaletteFileThemeItem? previous)
    {
        DisposeThumbnails(items);
        items.Clear();
        var found = Scan();
        for (var i = 0; i < found.Length; i++)
        {
            items.Add(found[i]);
        }

        return IndexOf(items, previous);
    }

    internal TreeNode? ReloadTree(TreeNodeCollection nodes, KryptonPaletteFileThemeItem? previous)
    {
        DisposeTreeThumbnails(nodes);
        nodes.Clear();
        var found = Scan();
        TreeNode? match = null;
        for (var i = 0; i < found.Length; i++)
        {
            var item = found[i];
            var node = InsertPath(nodes, item);
            if (Matches(item, previous))
            {
                match = node;
            }
        }

        return match;
    }

    internal int IndexOf(IList items, KryptonPaletteFileThemeItem? match)
    {
        if (match == null)
        {
            return -1;
        }

        for (var i = 0; i < items.Count; i++)
        {
            if (items[i] is KryptonPaletteFileThemeItem item && Matches(item, match))
            {
                return i;
            }
        }

        return -1;
    }

    internal static bool Matches(KryptonPaletteFileThemeItem left, KryptonPaletteFileThemeItem? right) =>
        right != null
        && string.Equals(left.FilePath, right.FilePath, StringComparison.OrdinalIgnoreCase)
        && string.Equals(left.ThemeName, right.ThemeName, StringComparison.OrdinalIgnoreCase);

    internal bool Apply(object? selected)
    {
        var item = selected as KryptonPaletteFileThemeItem
                   ?? (selected as TreeNode)?.Tag as KryptonPaletteFileThemeItem;
        if (item == null)
        {
            return false;
        }

        if (!item.TryImportInto(Palette, promptLegacyXml: true))
        {
            return false;
        }

        ThemeManager.ApplyTheme(Palette, Manager);
        return true;
    }

    internal static void DisposeThumbnails(IList items)
    {
        for (var i = 0; i < items.Count; i++)
        {
            if (items[i] is KryptonPaletteFileThemeItem item)
            {
                DisposeThumbnail(item);
            }
        }
    }

    internal static void DisposeTreeThumbnails(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Tag is KryptonPaletteFileThemeItem item)
            {
                DisposeThumbnail(item);
            }

            DisposeTreeThumbnails(node.Nodes);
        }
    }

    private static void DisposeThumbnail(KryptonPaletteFileThemeItem item)
    {
        item.Thumbnail?.Dispose();
        item.Thumbnail = null;
    }

    private static TreeNode InsertPath(TreeNodeCollection nodes, KryptonPaletteFileThemeItem item)
    {
        var segments = item.GetPathSegments();
        if (segments.Length == 0)
        {
            var leaf = new TreeNode(item.DisplayName) { Tag = item };
            nodes.Add(leaf);
            return leaf;
        }

        var current = nodes;
        TreeNode? node = null;
        for (var i = 0; i < segments.Length; i++)
        {
            var last = i == segments.Length - 1;
            var text = segments[i];
            if (last)
            {
                node = new TreeNode(text) { Tag = item };
                current.Add(node);
            }
            else
            {
                node = FindFolder(current, text);
                if (node == null)
                {
                    node = new TreeNode(text);
                    current.Add(node);
                }

                current = node.Nodes;
            }
        }

        return node!;
    }

    private static TreeNode? FindFolder(TreeNodeCollection nodes, string text)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Tag == null && string.Equals(node.Text, text, StringComparison.OrdinalIgnoreCase))
            {
                return node;
            }
        }

        return null;
    }
}
