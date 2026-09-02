#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <content>
/// Directory scan and pack-from-folder helpers for <see cref="KryptonPaletteFile"/>.
/// Pack entry names use <c>/</c> as a portable folder separator (no container-version bump).
/// </content>
public static partial class KryptonPaletteFile
{
    /// <summary>
    /// Separator stored in pack theme names that represent a folder path.
    /// </summary>
    public const char PackPathSeparator = '/';

    /// <summary>
    /// Returns whether <paramref name="themeName"/> encodes a folder path (<c>/</c> or <c>\</c>).
    /// </summary>
    /// <param name="themeName">Pack theme name or relative file path.</param>
    /// <returns><see langword="true"/> when the name contains a path separator.</returns>
    public static bool IsPackThemePath(string? themeName)
    {
        if (string.IsNullOrWhiteSpace(themeName))
        {
            return false;
        }

        return themeName!.IndexOf(PackPathSeparator) >= 0
               || themeName.IndexOf(Path.DirectorySeparatorChar) >= 0
               || themeName.IndexOf(Path.AltDirectorySeparatorChar) >= 0;
    }

    /// <summary>
    /// Normalises a relative palette path to pack form: <c>/</c> separators, no leading or trailing slash.
    /// </summary>
    /// <param name="relativePath">Disk-relative or pack theme name. May be empty.</param>
    /// <returns>Normalised path, or empty when <paramref name="relativePath"/> is empty.</returns>
    public static string NormalizePackThemeName(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return string.Empty;
        }

        var normalised = relativePath!.Trim().Replace('\\', PackPathSeparator);
        while (normalised.IndexOf(@"//", StringComparison.Ordinal) >= 0)
        {
            normalised = normalised.Replace(@"//", @"/");
        }

        return normalised.Trim(PackPathSeparator);
    }

    /// <summary>
    /// Splits a pack theme name or relative path into folder and leaf segments.
    /// </summary>
    /// <param name="themeName">Pack theme name.</param>
    /// <returns>Segments in order. Empty when <paramref name="themeName"/> is empty.</returns>
    public static string[] SplitPackThemePath(string? themeName)
    {
        var normalised = NormalizePackThemeName(themeName);
        return string.IsNullOrEmpty(normalised)
            ? Array.Empty<string>()
            : normalised.Split(new[] { PackPathSeparator }, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Joins two pack path fragments with <see cref="PackPathSeparator"/>.
    /// </summary>
    /// <param name="left">Parent path. May be empty.</param>
    /// <param name="right">Child path. May be empty.</param>
    /// <returns>Combined path. <paramref name="right"/> is returned unchanged when it already starts with <paramref name="left"/>.</returns>
    public static string CombinePackThemePath(string? left, string? right)
    {
        var parent = NormalizePackThemeName(left);
        var child = NormalizePackThemeName(right);
        if (string.IsNullOrEmpty(parent))
        {
            return child;
        }

        if (string.IsNullOrEmpty(child))
        {
            return parent;
        }

        if (string.Equals(child, parent, StringComparison.OrdinalIgnoreCase)
            || child.StartsWith(parent + PackPathSeparator, StringComparison.OrdinalIgnoreCase))
        {
            return child;
        }

        return parent + PackPathSeparator + child;
    }

    /// <summary>
    /// Converts a pack theme name to a display path using the platform directory separator.
    /// </summary>
    /// <param name="themeName">Pack theme name or relative path.</param>
    /// <returns>Display string, or empty when <paramref name="themeName"/> is empty.</returns>
    public static string ToDisplayPath(string? themeName) =>
        NormalizePackThemeName(themeName).Replace(PackPathSeparator, Path.DirectorySeparatorChar);

    /// <summary>
    /// Returns the pack theme name for a file under <paramref name="rootDirectory"/>
    /// (relative path, <c>/</c> separators, palette extension removed).
    /// </summary>
    /// <param name="filePath">Palette file path.</param>
    /// <param name="rootDirectory">Folder that is treated as the path root.</param>
    /// <returns>Relative pack name, or the file stem when the file is not under the root.</returns>
    public static string GetRelativePackThemeName(string filePath, string rootDirectory)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filePath));
        }

        var relative = GetRelativePath(rootDirectory, filePath!);
        return StripPaletteExtension(NormalizePackThemeName(relative));
    }

    /// <summary>
    /// Lists palette files in a folder. Missing folders yield an empty array.
    /// </summary>
    /// <param name="directory">Folder to scan.</param>
    /// <param name="searchSubdirectories">When <see langword="true"/>, include nested folders.</param>
    /// <param name="includeKpalx">Include <c>*.kpalx</c>.</param>
    /// <param name="includeKpal">Include <c>*.kpal</c>.</param>
    /// <param name="includeXml">Include legacy <c>*.xml</c>.</param>
    /// <returns>Full paths, sorted ordinal ignore-case.</returns>
    public static string[] GetPaletteFiles(string directory,
        bool searchSubdirectories = false,
        bool includeKpalx = true,
        bool includeKpal = true,
        bool includeXml = true)
    {
        if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
        {
            return Array.Empty<string>();
        }

        var option = searchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        var files = new List<string>();
        AddPaletteFiles(files, directory, @"*." + Extension, includeKpalx, option);
        AddPaletteFiles(files, directory, @"*." + BinaryExtension, includeKpal, option);
        AddPaletteFiles(files, directory, @"*." + XmlExtension, includeXml, option);
        files.Sort(StringComparer.OrdinalIgnoreCase);
        return files.ToArray();
    }

    /// <summary>
    /// Packs every palette file under <paramref name="sourceDirectory"/> into one <c>.kpal</c>.
    /// Theme names are the relative paths with <c>/</c> separators. Nested <c>.kpal</c> packs are flattened.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite. Must be <c>.kpal</c>.</param>
    /// <param name="sourceDirectory">Folder of <c>.kpalx</c> / <c>.kpal</c> / <c>.xml</c> files.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportPackFromDirectory(string destinationPath, string sourceDirectory) =>
        ExportPackFromDirectory(destinationPath, sourceDirectory, searchSubdirectories: true, ignoreDefaults: false, packName: null);

    /// <summary>
    /// Packs palette files under <paramref name="sourceDirectory"/> into one <c>.kpal</c>.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite. Must be <c>.kpal</c>.</param>
    /// <param name="sourceDirectory">Folder of palette files.</param>
    /// <param name="searchSubdirectories">When <see langword="true"/>, include nested folders.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match each file's base palette.</param>
    /// <param name="packName">Optional pack display name (header). Defaults to the folder name.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportPackFromDirectory(string destinationPath,
        string sourceDirectory,
        bool searchSubdirectories,
        bool ignoreDefaults,
        string? packName)
    {
        if (string.IsNullOrWhiteSpace(destinationPath))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(destinationPath));
        }

        if (string.IsNullOrWhiteSpace(sourceDirectory))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(sourceDirectory));
        }

        if (!Directory.Exists(sourceDirectory))
        {
            ThrowHelper.ThrowArgumentException(@"Palette folder does not exist.", nameof(sourceDirectory));
        }

        RejectJsonPalettePath(destinationPath!, nameof(destinationPath));
        if (FormatFromPath(destinationPath) != KryptonPaletteFileFormat.PaletteBinary)
        {
            ThrowHelper.ThrowArgumentException(@"Multi-theme packs can only be written as .kpal.", nameof(destinationPath));
        }

        var destFull = Path.GetFullPath(destinationPath);
        var files = GetPaletteFiles(sourceDirectory!, searchSubdirectories);
        var palettes = new List<KryptonCustomPaletteBase>();
        var usedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (string.Equals(Path.GetFullPath(file), destFull, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                AddDirectoryPackEntries(palettes, usedNames, file, sourceDirectory!);
            }

            if (palettes.Count == 0)
            {
                ThrowHelper.ThrowArgumentException(@"The folder does not contain any palette files.", nameof(sourceDirectory));
            }

            var headerName = string.IsNullOrWhiteSpace(packName)
                ? Path.GetFileName(Path.GetFullPath(sourceDirectory!).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
                : packName;

            return ExportPack(destinationPath!, palettes, ignoreDefaults, headerName);
        }
        finally
        {
            for (var i = 0; i < palettes.Count; i++)
            {
                palettes[i].Dispose();
            }
        }
    }

    private static void AddDirectoryPackEntries(List<KryptonCustomPaletteBase> palettes,
        HashSet<string> usedNames,
        string file,
        string rootDirectory)
    {
        var relativeName = GetRelativePackThemeName(file, rootDirectory);
        var relativeDir = ParentPackPath(relativeName);
        string[] names;
        bool isPack;
        try
        {
            names = GetThemeNames(file);
            isPack = IsPack(file);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($@"Could not read palette file '{file}'. {ex.Message}", ex);
        }

        if (isPack || names.Length > 1)
        {
            for (var n = 0; n < names.Length; n++)
            {
                var innerName = names[n];
                var packKey = IsPackThemePath(innerName)
                    ? CombinePackThemePath(relativeDir, innerName)
                    : CombinePackThemePath(relativeName,
                        string.IsNullOrWhiteSpace(innerName) ? Path.GetFileNameWithoutExtension(file) : innerName);
                packKey = UniquePackName(packKey, usedNames);

                var palette = new KryptonCustomPaletteBase();
                try
                {
                    palette.Import(file, innerName, silent: true);
                    palette.SetPaletteName(packKey);
                    palettes.Add(palette);
                }
                catch
                {
                    palette.Dispose();
                    throw;
                }
            }

            return;
        }

        var singleKey = UniquePackName(relativeName, usedNames);
        var single = new KryptonCustomPaletteBase();
        try
        {
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                single.ImportWithUpgrade(stream);
            }

            single.SetPaletteName(singleKey);
            palettes.Add(single);
        }
        catch (Exception ex)
        {
            single.Dispose();
            throw new InvalidOperationException($@"Could not import palette file '{file}'. {ex.Message}", ex);
        }
    }

    private static string ParentPackPath(string packThemeName)
    {
        var slash = packThemeName.LastIndexOf(PackPathSeparator);
        return slash <= 0 ? string.Empty : packThemeName.Substring(0, slash);
    }

    private static string UniquePackName(string candidate, HashSet<string> used)
    {
        var name = string.IsNullOrWhiteSpace(candidate) ? @"Palette" : candidate;
        var suffix = 2;
        var unique = name;
        while (!used.Add(unique))
        {
            unique = name + @" (" + suffix + @")";
            suffix++;
        }

        return unique;
    }

    private static string StripPaletteExtension(string relativePath)
    {
        var extension = Path.GetExtension(relativePath);
        if (string.IsNullOrEmpty(extension))
        {
            return relativePath;
        }

        if (string.Equals(extension, @"." + Extension, StringComparison.OrdinalIgnoreCase)
            || string.Equals(extension, @"." + BinaryExtension, StringComparison.OrdinalIgnoreCase)
            || string.Equals(extension, @"." + XmlExtension, StringComparison.OrdinalIgnoreCase))
        {
            return relativePath.Substring(0, relativePath.Length - extension.Length);
        }

        return relativePath;
    }

    private static string GetRelativePath(string rootDirectory, string filePath)
    {
        if (string.IsNullOrWhiteSpace(rootDirectory))
        {
            return Path.GetFileName(filePath);
        }

        var rootFull = Path.GetFullPath(rootDirectory)
            .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            + Path.DirectorySeparatorChar;
        var fileFull = Path.GetFullPath(filePath);
        if (fileFull.StartsWith(rootFull, StringComparison.OrdinalIgnoreCase))
        {
            return fileFull.Substring(rootFull.Length);
        }

        return Path.GetFileName(filePath);
    }

    private static void AddPaletteFiles(List<string> files, string directory, string pattern, bool include, SearchOption option)
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
}
