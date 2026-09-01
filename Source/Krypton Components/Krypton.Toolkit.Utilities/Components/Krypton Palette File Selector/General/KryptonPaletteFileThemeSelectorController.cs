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
/// Shared scan/apply logic for <see cref="KryptonPaletteFileListBox"/> and
/// <see cref="KryptonPaletteFileComboBox"/>.
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

    internal bool IncludeXml { get; set; } = true;

    internal bool AutoApply { get; set; } = true;

    internal bool SuppressSelection { get; set; }

    internal KryptonManager Manager { get; set; }

    internal KryptonCustomPaletteBase Palette { get; }

    internal int Reload(IList items, KryptonPaletteFileThemeItem? previous)
    {
        items.Clear();
        var found = KryptonPaletteFileThemeItem.FromDirectory(PaletteDirectory, SearchSubdirectories,
            IncludeKpalx, IncludeKpal, IncludeXml);
        for (var i = 0; i < found.Length; i++)
        {
            items.Add(found[i]);
        }

        return IndexOf(items, previous);
    }

    internal int IndexOf(IList items, KryptonPaletteFileThemeItem? match)
    {
        if (match == null)
        {
            return -1;
        }

        for (var i = 0; i < items.Count; i++)
        {
            if (items[i] is KryptonPaletteFileThemeItem item
                && string.Equals(item.FilePath, match.FilePath, StringComparison.OrdinalIgnoreCase)
                && string.Equals(item.ThemeName, match.ThemeName, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }

    internal bool Apply(object? selected)
    {
        if (selected is not KryptonPaletteFileThemeItem item)
        {
            return false;
        }

        item.ImportInto(Palette);
        ThemeManager.ApplyTheme(Palette, Manager);
        return true;
    }
}
