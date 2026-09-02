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
/// Folder packs use <see cref="TreePath"/> with <c>/</c> separators.
/// </summary>
public sealed class KryptonPaletteFileThemeItem : IContentValues
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPaletteFileThemeItem"/> class.
    /// </summary>
    /// <param name="filePath">Palette file path.</param>
    /// <param name="themeName">Pack theme name, or the single-file palette name. May be empty.</param>
    /// <param name="isPack"><see langword="true"/> when the file is a multi-theme KPLT pack.</param>
    /// <param name="displayName">Text shown in a list or combo.</param>
    public KryptonPaletteFileThemeItem(string filePath, string themeName, bool isPack, string displayName)
        : this(filePath, themeName, isPack, displayName, treePath: displayName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPaletteFileThemeItem"/> class.
    /// </summary>
    /// <param name="filePath">Palette file path.</param>
    /// <param name="themeName">Pack theme name, or the single-file palette name. May be empty.</param>
    /// <param name="isPack"><see langword="true"/> when the file is a multi-theme KPLT pack.</param>
    /// <param name="displayName">Text shown in a list or combo.</param>
    /// <param name="treePath">Folder path used by the tree and by nested list labels (<c>/</c> separators).</param>
    public KryptonPaletteFileThemeItem(string filePath, string themeName, bool isPack, string displayName, string treePath)
    {
        FilePath = filePath ?? string.Empty;
        ThemeName = themeName ?? string.Empty;
        IsPack = isPack;
        TreePath = KryptonPaletteFile.NormalizePackThemeName(treePath);
        DisplayName = string.IsNullOrWhiteSpace(displayName)
            ? (string.IsNullOrEmpty(TreePath)
                ? Path.GetFileNameWithoutExtension(FilePath)
                : KryptonPaletteFile.ToDisplayPath(TreePath))
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

    /// <summary>
    /// Gets the folder path for tree nodes and nested list labels, using <c>/</c> separators.
    /// </summary>
    public string TreePath { get; }

    /// <summary>Gets the display text used by list and combo controls.</summary>
    public string DisplayName { get; }

    /// <summary>
    /// Gets or sets the optional preview image. Pack catalogs and
    /// <see cref="KryptonCustomPaletteBase.Thumbnail"/> populate this when loading thumbnails.
    /// </summary>
    public Image? Thumbnail { get; set; }

    /// <summary>Gets folder and leaf segments for <see cref="TreePath"/>.</summary>
    public string[] GetPathSegments() => KryptonPaletteFile.SplitPackThemePath(TreePath);

    /// <inheritdoc />
    public override string ToString() => DisplayName;

    /// <summary>
    /// Scans a directory for palette files and expands <c>.kpal</c> packs into named items.
    /// Unreadable files are skipped. Nested files and path-named pack themes keep their folder path.
    /// </summary>
    /// <param name="directory">Folder to scan. Empty or missing folders yield an empty array.</param>
    /// <param name="searchSubdirectories">When <see langword="true"/>, include nested folders.</param>
    /// <param name="includeKpalx">Include <c>*.kpalx</c>.</param>
    /// <param name="includeKpal">Include <c>*.kpal</c>.</param>
    /// <param name="includeXml">Include legacy <c>*.xml</c>.</param>
    /// <param name="loadThumbnails">When <see langword="true"/>, attach <see cref="Thumbnail"/> from the pack catalog or persisted image.</param>
    /// <returns>Items sorted by display name.</returns>
    public static KryptonPaletteFileThemeItem[] FromDirectory(string directory,
        bool searchSubdirectories = false,
        bool includeKpalx = true,
        bool includeKpal = true,
        bool includeXml = true,
        bool loadThumbnails = false)
    {
        var files = KryptonPaletteFile.GetPaletteFiles(directory, searchSubdirectories, includeKpalx, includeKpal, includeXml);
        var items = new List<KryptonPaletteFileThemeItem>();
        var usedDisplayNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (var i = 0; i < files.Length; i++)
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

            var relativeName = KryptonPaletteFile.GetRelativePackThemeName(path, directory);
            var relativeDir = ParentPath(relativeName);

            if (isPack || names.Length > 1)
            {
                for (var n = 0; n < names.Length; n++)
                {
                    var innerName = names[n];
                    var treePath = KryptonPaletteFile.IsPackThemePath(innerName)
                        ? KryptonPaletteFile.CombinePackThemePath(relativeDir, innerName)
                        : KryptonPaletteFile.CombinePackThemePath(relativeName,
                            string.IsNullOrWhiteSpace(innerName)
                                ? Path.GetFileNameWithoutExtension(path)
                                : innerName);
                    var display = UniqueDisplay(KryptonPaletteFile.ToDisplayPath(treePath), usedDisplayNames);
                    items.Add(new KryptonPaletteFileThemeItem(path, innerName, isPack: true, display, treePath));
                }
            }
            else
            {
                var themeName = names.Length == 1 ? names[0] : string.Empty;
                var treePath = relativeName;
                var displaySource = searchSubdirectories && KryptonPaletteFile.IsPackThemePath(relativeName)
                    ? KryptonPaletteFile.ToDisplayPath(treePath)
                    : (!string.IsNullOrWhiteSpace(themeName)
                        ? themeName
                        : Path.GetFileNameWithoutExtension(path));
                var display = UniqueDisplay(displaySource, usedDisplayNames);
                items.Add(new KryptonPaletteFileThemeItem(path, themeName, isPack: false, display, treePath));
            }
        }

        items.Sort((left, right) => string.Compare(left.DisplayName, right.DisplayName, StringComparison.OrdinalIgnoreCase));
        if (loadThumbnails)
        {
            AttachThumbnails(items);
        }

        return items.ToArray();
    }

    /// <inheritdoc />
    public Image? GetImage(PaletteState state) => Thumbnail;

    /// <inheritdoc />
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <inheritdoc />
    public string GetShortText() => DisplayName;

    /// <inheritdoc />
    public string GetLongText() => string.Empty;

    /// <inheritdoc />
    public Image? GetOverlayImage(PaletteState state) => null;

    /// <inheritdoc />
    public Color GetOverlayImageTransparentColor(PaletteState state) => Color.Empty;

    /// <inheritdoc />
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    /// <inheritdoc />
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    /// <inheritdoc />
    public float GetOverlayImageScaleFactor(PaletteState state) => 1f;

    /// <inheritdoc />
    public Size GetOverlayImageFixedSize(PaletteState state) => Size.Empty;

    private static void AttachThumbnails(List<KryptonPaletteFileThemeItem> items)
    {
        var byFile = new Dictionary<string, Dictionary<string, Image>>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < items.Count; i++)
        {
            var path = items[i].FilePath;
            if (byFile.ContainsKey(path))
            {
                continue;
            }

            try
            {
                var names = KryptonPaletteFile.GetThemeNames(path);
                var thumbs = KryptonPaletteFile.GetThemeThumbnails(path);
                var map = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
                var length = Math.Min(names.Length, thumbs.Length);
                for (var n = 0; n < length; n++)
                {
                    if (thumbs[n] == null)
                    {
                        continue;
                    }

                    var key = names[n] ?? string.Empty;
                    if (!map.ContainsKey(key))
                    {
                        map[key] = thumbs[n]!;
                    }
                    else
                    {
                        thumbs[n]!.Dispose();
                    }
                }

                byFile[path] = map;
            }
            catch (Exception)
            {
                byFile[path] = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
            }
        }

        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];
            if (!byFile.TryGetValue(item.FilePath, out var map))
            {
                continue;
            }

            var key = item.ThemeName ?? string.Empty;
            if (map.TryGetValue(key, out var image))
            {
                item.Thumbnail = image;
                map.Remove(key);
            }
            else if (map.Count == 1 && string.IsNullOrWhiteSpace(item.ThemeName))
            {
                foreach (var pair in map)
                {
                    item.Thumbnail = pair.Value;
                    map.Remove(pair.Key);
                    break;
                }
            }
        }

        foreach (var map in byFile.Values)
        {
            foreach (var pair in map)
            {
                pair.Value.Dispose();
            }
        }
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

    private static string ParentPath(string packThemeName)
    {
        var slash = packThemeName.LastIndexOf(KryptonPaletteFile.PackPathSeparator);
        return slash <= 0 ? string.Empty : packThemeName.Substring(0, slash);
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
