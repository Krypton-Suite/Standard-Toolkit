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
    /// Separator stored in collection theme names that represent a folder path.
    /// </summary>
    public const char CollectionPathSeparator = '/';

    /// <summary>
    /// Returns whether <paramref name="themeName"/> encodes a folder path (<c>/</c> or <c>\</c>).
    /// </summary>
    /// <param name="themeName">Pack theme name or relative file path.</param>
    /// <returns><see langword="true"/> when the name contains a path separator.</returns>
    public static bool IsCollectionThemePath(string? themeName)
    {
        if (string.IsNullOrWhiteSpace(themeName))
        {
            return false;
        }

        return themeName!.IndexOf(CollectionPathSeparator) >= 0
               || themeName.IndexOf(Path.DirectorySeparatorChar) >= 0
               || themeName.IndexOf(Path.AltDirectorySeparatorChar) >= 0;
    }

    /// <summary>
    /// Normalises a relative palette path to collection form: <c>/</c> separators, no leading or trailing slash.
    /// </summary>
    /// <param name="relativePath">Disk-relative or collection theme name. May be empty.</param>
    /// <returns>Normalised path, or empty when <paramref name="relativePath"/> is empty.</returns>
    public static string NormalizeCollectionThemeName(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return string.Empty;
        }

        var normalised = relativePath!.Trim().Replace('\\', CollectionPathSeparator);
        while (normalised.IndexOf(@"//", StringComparison.Ordinal) >= 0)
        {
            normalised = normalised.Replace(@"//", @"/");
        }

        return normalised.Trim(CollectionPathSeparator);
    }

    /// <summary>
    /// Splits a collection theme name or relative path into folder and leaf segments.
    /// </summary>
    /// <param name="themeName">Pack theme name.</param>
    /// <returns>Segments in order. Empty when <paramref name="themeName"/> is empty.</returns>
    public static string[] SplitCollectionThemePath(string? themeName)
    {
        var normalised = NormalizeCollectionThemeName(themeName);
        return string.IsNullOrEmpty(normalised)
            ? Array.Empty<string>()
            : normalised.Split(new[] { CollectionPathSeparator }, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Joins two pack path fragments with <see cref="CollectionPathSeparator"/>.
    /// </summary>
    /// <param name="left">Parent path. May be empty.</param>
    /// <param name="right">Child path. May be empty.</param>
    /// <returns>Combined path. <paramref name="right"/> is returned unchanged when it already starts with <paramref name="left"/>.</returns>
    public static string CombineCollectionThemePath(string? left, string? right)
    {
        var parent = NormalizeCollectionThemeName(left);
        var child = NormalizeCollectionThemeName(right);
        if (string.IsNullOrEmpty(parent))
        {
            return child;
        }

        if (string.IsNullOrEmpty(child))
        {
            return parent;
        }

        if (string.Equals(child, parent, StringComparison.OrdinalIgnoreCase)
            || child.StartsWith(parent + CollectionPathSeparator, StringComparison.OrdinalIgnoreCase))
        {
            return child;
        }

        return parent + CollectionPathSeparator + child;
    }

    /// <summary>
    /// Converts a collection theme name to a display path using the platform directory separator.
    /// </summary>
    /// <param name="themeName">Pack theme name or relative path.</param>
    /// <returns>Display string, or empty when <paramref name="themeName"/> is empty.</returns>
    public static string ToDisplayPath(string? themeName) =>
        NormalizeCollectionThemeName(themeName).Replace(CollectionPathSeparator, Path.DirectorySeparatorChar);

    /// <summary>
    /// Returns the collection theme name for a file under <paramref name="rootDirectory"/>
    /// (relative path, <c>/</c> separators, palette extension removed).
    /// </summary>
    /// <param name="filePath">Palette file path.</param>
    /// <param name="rootDirectory">Folder that is treated as the path root.</param>
    /// <returns>Relative pack name, or the file stem when the file is not under the root.</returns>
    public static string GetRelativeCollectionThemeName(string filePath, string rootDirectory)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(filePath);

        var relative = GetRelativePath(rootDirectory, filePath);
        return StripPaletteExtension(NormalizeCollectionThemeName(relative));
    }

    /// <summary>
    /// Lists palette files in a folder. Missing folders yield an empty array.
    /// </summary>
    /// <param name="directory">Folder to scan.</param>
    /// <param name="searchSubdirectories">When <see langword="true"/>, include nested folders.</param>
    /// <param name="includeKthemex">Include <c>*.kthemex</c>.</param>
    /// <param name="includeKtheme">Include <c>*.ktheme</c>.</param>
    /// <param name="includeXml">Include legacy <c>*.xml</c>.</param>
    /// <returns>Full paths, sorted ordinal ignore-case.</returns>
    // ToDo V120 LTS: Remove includeXml (default listing of *.xml). Callers should UpgradeXmlToKthemex first.
    public static string[] GetPaletteFiles(string directory,
        bool searchSubdirectories = false,
        bool includeKthemex = true,
        bool includeKtheme = true,
        bool includeXml = true)
    {
        if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
        {
            return Array.Empty<string>();
        }

        var option = searchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        var files = new List<string>();
        AddPaletteFiles(files, directory, @"*." + Extension, includeKthemex, option);
        AddPaletteFiles(files, directory, @"*." + BinaryExtension, includeKtheme, option);
        // ToDo V120 LTS: Stop scanning *.xml once XmlExtension is removed.
        AddPaletteFiles(files, directory, @"*." + XmlExtension, includeXml, option);
        files.Sort(StringComparer.OrdinalIgnoreCase);
        return files.ToArray();
    }

    /// <summary>
    /// Rewrites every Krypton palette <c>.xml</c> under <paramref name="directory"/> as <c>.kthemex</c>
    /// beside the source (nested folders included). Source files are left in place.
    /// </summary>
    /// <param name="directory">Folder to scan.</param>
    /// <returns>Converted paths, skipped non-palette XML, and per-file errors.</returns>
    /// <exception cref="ArgumentException">The folder does not exist.</exception>
    // ToDo V120 LTS: Remove with XmlExtension. Callers should already have .kthemex files.
    public static KryptonPaletteDirectoryUpgradeResult UpgradeXmlToKthemexFromDirectory(string directory) =>
        UpgradeXmlToKthemexFromDirectory(directory, searchSubdirectories: true);

    /// <summary>
    /// Rewrites every Krypton palette <c>.xml</c> under <paramref name="directory"/> as <c>.kthemex</c>
    /// beside the source. Source files are left in place. Non-palette <c>.xml</c> is skipped.
    /// A failure on one file does not stop the rest.
    /// </summary>
    /// <param name="directory">Folder to scan.</param>
    /// <param name="searchSubdirectories">When <see langword="true"/>, include nested folders.</param>
    /// <returns>Converted paths, skipped non-palette XML, and per-file errors.</returns>
    /// <exception cref="ArgumentException">The folder does not exist.</exception>
    // ToDo V120 LTS: Remove with XmlExtension. Callers should already have .kthemex files.
    public static KryptonPaletteDirectoryUpgradeResult UpgradeXmlToKthemexFromDirectory(string directory,
        bool searchSubdirectories)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(directory);

        if (!Directory.Exists(directory))
        {
            ThrowHelper.ThrowArgumentException(@"Palette folder does not exist.", nameof(directory));
        }

        var xmlFiles = GetPaletteFiles(directory, searchSubdirectories, includeKthemex: false, includeKtheme: false,
            includeXml: true);
        var converted = new List<string>();
        var sources = new List<string>();
        var skipped = new List<string>();
        var errors = new List<KryptonPaletteDirectoryUpgradeError>();

        for (var i = 0; i < xmlFiles.Length; i++)
        {
            var sourcePath = xmlFiles[i];
            try
            {
                if (!IsKryptonPaletteXmlFile(sourcePath))
                {
                    skipped.Add(sourcePath);
                    continue;
                }

                converted.Add(UpgradeXmlToKthemex(sourcePath));
                sources.Add(sourcePath);
            }
            catch (Exception ex)
            {
                errors.Add(new KryptonPaletteDirectoryUpgradeError(sourcePath, ex.Message));
            }
        }

        return new KryptonPaletteDirectoryUpgradeResult(converted.ToArray(), sources.ToArray(), skipped.ToArray(),
            errors.ToArray());
    }

    private static bool IsKryptonPaletteXmlFile(string path)
    {
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            return KryptonPaletteBinaryPersistence.TryGetSchemaVersion(stream, out _);
        }
    }

    /// <summary>
    /// Stores every palette file under <paramref name="sourceDirectory"/> into one <c>.ktheme</c>.
    /// Theme names are the relative paths with <c>/</c> separators. Nested <c>.ktheme</c> packs are flattened.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite. Must be <c>.ktheme</c>.</param>
    /// <param name="sourceDirectory">Folder of <c>.kthemex</c> / <c>.ktheme</c> / legacy <c>.xml</c> files.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportCollectionFromDirectory(string destinationPath, string sourceDirectory) =>
        ExportCollectionFromDirectory(destinationPath, sourceDirectory, searchSubdirectories: true, ignoreDefaults: false, collectionName: null);

    /// <summary>
    /// Stores palette files under <paramref name="sourceDirectory"/> into one <c>.ktheme</c>.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite. Must be <c>.ktheme</c>.</param>
    /// <param name="sourceDirectory">Folder of palette files.</param>
    /// <param name="searchSubdirectories">When <see langword="true"/>, include nested folders.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match each file's base palette.</param>
    /// <param name="collectionName">Optional collection display name (header). Defaults to the folder name.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportCollectionFromDirectory(string destinationPath,
        string sourceDirectory,
        bool searchSubdirectories,
        bool ignoreDefaults,
        string? collectionName)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(destinationPath);
        ThrowHelper.ThrowIfNullOrWhiteSpace(sourceDirectory);

        if (!Directory.Exists(sourceDirectory))
        {
            ThrowHelper.ThrowArgumentException(@"Palette folder does not exist.", nameof(sourceDirectory));
        }

        RejectJsonPalettePath(destinationPath, nameof(destinationPath));
        if (FormatFromPath(destinationPath) != KryptonPaletteFileFormat.PaletteBinary)
        {
            ThrowHelper.ThrowArgumentException(@"Multi-theme collections can only be written as .ktheme.", nameof(destinationPath));
        }

        var destFull = Path.GetFullPath(destinationPath);
        var files = GetPaletteFiles(sourceDirectory, searchSubdirectories);
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

                AddDirectoryCollectionEntries(palettes, usedNames, file, sourceDirectory);
            }

            if (palettes.Count == 0)
            {
                ThrowHelper.ThrowArgumentException(@"The folder does not contain any palette files.", nameof(sourceDirectory));
            }

            var headerName = string.IsNullOrWhiteSpace(collectionName)
                ? Path.GetFileName(Path.GetFullPath(sourceDirectory).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
                : collectionName;

            return ExportCollection(destinationPath, palettes, ignoreDefaults, headerName);
        }
        finally
        {
            for (var i = 0; i < palettes.Count; i++)
            {
                palettes[i].Dispose();
            }
        }
    }

    private static void AddDirectoryCollectionEntries(List<KryptonCustomPaletteBase> palettes,
        HashSet<string> usedNames,
        string file,
        string rootDirectory)
    {
        var relativeName = GetRelativeCollectionThemeName(file, rootDirectory);
        var relativeDir = ParentCollectionPath(relativeName);
        string[] names;
        bool isCollection;
        try
        {
            names = GetThemeNames(file);
            isCollection = IsCollection(file);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($@"Could not read palette file '{file}'. {ex.Message}", ex);
        }

        if (isCollection || names.Length > 1)
        {
            for (var n = 0; n < names.Length; n++)
            {
                var innerName = names[n];
                var collectionKey = IsCollectionThemePath(innerName)
                    ? CombineCollectionThemePath(relativeDir, innerName)
                    : CombineCollectionThemePath(relativeName,
                        string.IsNullOrWhiteSpace(innerName) ? Path.GetFileNameWithoutExtension(file) : innerName);
                collectionKey = UniqueCollectionName(collectionKey, usedNames);

                var palette = new KryptonCustomPaletteBase();
                try
                {
                    palette.Import(file, innerName, silent: true);
                    palette.SetPaletteName(collectionKey);
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

        var singleKey = UniqueCollectionName(relativeName, usedNames);
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

    private static string ParentCollectionPath(string collectionThemeName)
    {
        var slash = collectionThemeName.LastIndexOf(CollectionPathSeparator);
        return slash <= 0 ? string.Empty : collectionThemeName.Substring(0, slash);
    }

    private static string UniqueCollectionName(string candidate, HashSet<string> used)
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
            // ToDo V120 LTS: Stop stripping .xml from collection theme names.
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
            if (files.Find(existing => string.Equals(existing, path, StringComparison.OrdinalIgnoreCase)) == null)
            {
                files.Add(path);
            }
        }
    }
}
