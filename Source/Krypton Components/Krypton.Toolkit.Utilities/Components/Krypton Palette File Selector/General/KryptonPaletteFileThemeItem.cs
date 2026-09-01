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
/// One selectable theme from a palette file. A <c>.kpal</c> pack yields one item per named theme.
/// </summary>
public sealed class KryptonPaletteFileThemeItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPaletteFileThemeItem"/> class.
    /// </summary>
    /// <param name="filePath">Palette file path.</param>
    /// <param name="themeName">Pack theme name, or the single-file palette name. May be empty.</param>
    /// <param name="isPack"><see langword="true"/> when the file is a multi-theme KPLT pack.</param>
    /// <param name="displayName">Text shown in a list or combo.</param>
    public KryptonPaletteFileThemeItem(string filePath, string themeName, bool isPack, string displayName)
    {
        FilePath = filePath ?? string.Empty;
        ThemeName = themeName ?? string.Empty;
        IsPack = isPack;
        DisplayName = string.IsNullOrWhiteSpace(displayName)
            ? Path.GetFileNameWithoutExtension(FilePath)
            : displayName;
    }

    /// <summary>Gets the palette file path.</summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets the theme name to pass to <see cref="KryptonCustomPaletteBase.Import(string, string, bool)"/>.
    /// Empty for an unnamed single-theme file.
    /// </summary>
    public string ThemeName { get; }

    /// <summary>Gets whether the file is a KPLT pack (payload kind 2).</summary>
    public bool IsPack { get; }

    /// <summary>Gets the display text used by list and combo controls.</summary>
    public string DisplayName { get; }

    /// <inheritdoc />
    public override string ToString() => DisplayName;

    /// <summary>
    /// Scans a directory for palette files and expands <c>.kpal</c> packs into named items.
    /// Unreadable files are skipped.
    /// </summary>
    /// <param name="directory">Folder to scan. Empty or missing folders yield an empty array.</param>
    /// <param name="searchSubdirectories">When <see langword="true"/>, include nested folders.</param>
    /// <param name="includeKpalx">Include <c>*.kpalx</c>.</param>
    /// <param name="includeKpal">Include <c>*.kpal</c>.</param>
    /// <param name="includeXml">Include legacy <c>*.xml</c>.</param>
    /// <returns>Items sorted by display name.</returns>
    public static KryptonPaletteFileThemeItem[] FromDirectory(string directory,
        bool searchSubdirectories = false,
        bool includeKpalx = true,
        bool includeKpal = true,
        bool includeXml = true)
    {
        if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
        {
            return Array.Empty<KryptonPaletteFileThemeItem>();
        }

        var option = searchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        var files = new List<string>();
        AddFiles(files, directory, @"*." + KryptonPaletteFile.Extension, includeKpalx, option);
        AddFiles(files, directory, @"*." + KryptonPaletteFile.BinaryExtension, includeKpal, option);
        AddFiles(files, directory, @"*." + KryptonPaletteFile.XmlExtension, includeXml, option);

        files.Sort(StringComparer.OrdinalIgnoreCase);

        var items = new List<KryptonPaletteFileThemeItem>();
        var usedDisplayNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (var i = 0; i < files.Count; i++)
        {
            var path = files[i];
            string[] names;
            bool isPack;
            try
            {
                names = KryptonPaletteFile.GetThemeNames(path);
                isPack = KryptonPaletteFile.IsPack(path);
            }
            catch (Exception)
            {
                continue;
            }

            var fileName = Path.GetFileName(path);
            if (isPack || names.Length > 1)
            {
                for (var n = 0; n < names.Length; n++)
                {
                    var themeName = names[n];
                    if (string.IsNullOrWhiteSpace(themeName))
                    {
                        themeName = Path.GetFileNameWithoutExtension(path);
                    }

                    var display = UniqueDisplay($@"{themeName} ({fileName})", usedDisplayNames);
                    items.Add(new KryptonPaletteFileThemeItem(path, names[n], isPack: true, display));
                }
            }
            else
            {
                var themeName = names.Length == 1 ? names[0] : string.Empty;
                var baseDisplay = !string.IsNullOrWhiteSpace(themeName)
                    ? themeName
                    : Path.GetFileNameWithoutExtension(path);
                var display = UniqueDisplay(baseDisplay, usedDisplayNames);
                items.Add(new KryptonPaletteFileThemeItem(path, themeName, isPack: false, display));
            }
        }

        items.Sort((left, right) => string.Compare(left.DisplayName, right.DisplayName, StringComparison.OrdinalIgnoreCase));
        return items.ToArray();
    }

    /// <summary>
    /// Imports this theme into <paramref name="palette"/>.
    /// </summary>
    /// <param name="palette">Destination custom palette.</param>
    public void ImportInto(KryptonCustomPaletteBase palette)
    {
        ThrowHelper.ThrowIfNull(palette);

        palette.Import(FilePath, ThemeName, silent: true);
    }

    /// <summary>
    /// Imports this theme and assigns it as the manager's global custom palette.
    /// </summary>
    /// <param name="manager">Krypton manager that receives the palette.</param>
    /// <param name="palette">Palette instance to import into. A new instance is created when omitted.</param>
    public void Apply(KryptonManager manager, KryptonCustomPaletteBase? palette = null)
    {
        ThrowHelper.ThrowIfNull(manager);
        var target = palette ?? new KryptonCustomPaletteBase();
        ImportInto(target);
        ThemeManager.ApplyTheme(target, manager);
    }

    private static void AddFiles(List<string> files, string directory, string pattern, bool include, SearchOption option)
    {
        if (!include)
        {
            return;
        }

        var found = Directory.GetFiles(directory, pattern, option);
        for (var i = 0; i < found.Length; i++)
        {
            var path = found[i];
            var exists = false;
            for (var f = 0; f < files.Count; f++)
            {
                if (string.Equals(files[f], path, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                files.Add(path);
            }
        }
    }

    private static string UniqueDisplay(string candidate, HashSet<string> used)
    {
        var display = candidate;
        var suffix = 2;
        while (!used.Add(display))
        {
            display = $@"{candidate} ({suffix})";
            suffix++;
        }

        return display;
    }
}
