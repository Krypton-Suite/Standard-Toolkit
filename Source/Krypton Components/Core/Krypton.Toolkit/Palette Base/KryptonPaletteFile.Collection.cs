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
/// Add, remove, and rename helpers for multi-theme <c>.ktheme</c> collections.
/// Rewrites use the existing KPLT kind-2 layout (no container-version bump).
/// </content>
public static partial class KryptonPaletteFile
{
    /// <summary>
    /// Returns the collection display name stored in the KPLT header of a kind-2 <c>.ktheme</c>.
    /// Single-theme files, XML, and <c>.kthemex</c> return empty.
    /// </summary>
    /// <param name="path">Existing palette file.</param>
    /// <returns>Header name, or empty when the file is not a collection.</returns>
    public static string GetCollectionName(string path)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(path);

        RejectJsonPalettePath(path, nameof(path));
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return KryptonPaletteBinaryPersistence.GetCollectionDisplayName(stream);
    }

    /// <summary>
    /// Rewrites a kind-2 collection with a new header display name. Theme payloads are unchanged.
    /// </summary>
    /// <param name="collectionPath">Existing <c>.ktheme</c> collection.</param>
    /// <param name="collectionName">New display name. May be empty.</param>
    /// <returns>The full collection path.</returns>
    public static string SetCollectionName(string collectionPath, string? collectionName)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(collectionPath);
        RejectJsonPalettePath(collectionPath, nameof(collectionPath));
        EnsureCollectionDestination(collectionPath);

        if (!File.Exists(collectionPath))
        {
            throw new FileNotFoundException(@"Palette collection file was not found.", collectionPath);
        }

        if (!IsCollection(collectionPath))
        {
            ThrowHelper.ThrowArgumentException(@"File is not a multi-theme .ktheme collection.", nameof(collectionPath));
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            LoadDestinationPalettes(collectionPath, palettes);
            return ExportCollection(collectionPath, palettes, ignoreDefaults: false, collectionName ?? string.Empty);
        }
        finally
        {
            DisposePalettes(palettes);
        }
    }

    /// <summary>
    /// Adds a palette file to a <c>.ktheme</c> collection. Missing destination files are created.
    /// A single-theme <c>.ktheme</c> is promoted to a collection. Source collections add every named theme.
    /// </summary>
    /// <param name="collectionPath">Collection to create or update. Must be <c>.ktheme</c>.</param>
    /// <param name="sourcePath">Palette file to add (<c>.kthemex</c>, <c>.xml</c>, or <c>.ktheme</c>).</param>
    /// <returns>The full collection path.</returns>
    public static string AddToCollection(string collectionPath, string sourcePath) =>
        AddToCollection(collectionPath, sourcePath, themeName: null, replaceExisting: false);

    /// <summary>
    /// Adds a palette file to a <c>.ktheme</c> collection.
    /// </summary>
    /// <param name="collectionPath">Collection to create or update. Must be <c>.ktheme</c>.</param>
    /// <param name="sourcePath">Palette file to add.</param>
    /// <param name="themeName">
    /// Destination name for a single-theme source, or the collection theme name to copy from a source collection.
    /// When omitted, a single-theme file uses <c>GetPaletteName</c> or the file stem; a source collection adds every theme.
    /// </param>
    /// <param name="replaceExisting"><see langword="true"/> to overwrite a theme with the same name.</param>
    /// <returns>The full collection path.</returns>
    public static string AddToCollection(string collectionPath, string sourcePath, string? themeName, bool replaceExisting)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(collectionPath);
        ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);
        return AddToCollectionCore(collectionPath, new[] { new CollectionAddRequest(sourcePath, themeName) }, replaceExisting);
    }

    /// <summary>
    /// Adds each palette file to a <c>.ktheme</c> collection in one rewrite.
    /// </summary>
    /// <param name="collectionPath">Collection to create or update. Must be <c>.ktheme</c>.</param>
    /// <param name="sourcePaths">Palette files to add. Cannot be empty.</param>
    /// <param name="replaceExisting"><see langword="true"/> to overwrite themes with the same name.</param>
    /// <returns>The full collection path.</returns>
    public static string AddToCollection(string collectionPath, IEnumerable<string> sourcePaths, bool replaceExisting = false)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(collectionPath);
        ThrowHelper.ThrowIfNull(sourcePaths);

        var requests = new List<CollectionAddRequest>();
        foreach (var sourcePath in sourcePaths)
        {
            ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);
            requests.Add(new CollectionAddRequest(sourcePath!, themeName: null));
        }

        if (requests.Count == 0)
        {
            ThrowHelper.ThrowArgumentException(@"At least one palette file is required.", nameof(sourcePaths));
        }

        return AddToCollectionCore(collectionPath, requests, replaceExisting);
    }

    /// <summary>
    /// Adds an in-memory palette to a <c>.ktheme</c> collection. The palette must have a unique
    /// <c>SetPaletteName</c> value unless <paramref name="replaceExisting"/> is <see langword="true"/>.
    /// The caller still owns <paramref name="palette"/>.
    /// </summary>
    /// <param name="collectionPath">Collection to create or update. Must be <c>.ktheme</c>.</param>
    /// <param name="palette">Named palette to collection.</param>
    /// <param name="replaceExisting"><see langword="true"/> to overwrite a theme with the same name.</param>
    /// <returns>The full collection path.</returns>
    public static string AddToCollection(string collectionPath, KryptonCustomPaletteBase palette, bool replaceExisting = false)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(collectionPath);
        ThrowHelper.ThrowIfNull(palette);
        RejectJsonPalettePath(collectionPath, nameof(collectionPath));
        EnsureCollectionDestination(collectionPath);

        if (string.IsNullOrWhiteSpace(palette!.GetPaletteName()))
        {
            ThrowHelper.ThrowArgumentException(@"The palette must have a name (SetPaletteName).", nameof(palette));
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            var collectionName = LoadDestinationPalettes(collectionPath, palettes);
            MergePalette(palettes, palette, replaceExisting, disposeIncomingOnFailure: false);
            return ExportCollection(collectionPath, palettes, ignoreDefaults: false, collectionName);
        }
        finally
        {
            DisposePalettes(palettes, palette);
        }
    }

    /// <summary>
    /// Removes a named theme from a <c>.ktheme</c> collection. The last remaining theme cannot be removed
    /// (a collection cannot be empty); delete the file instead.
    /// </summary>
    /// <param name="collectionPath">Existing <c>.ktheme</c> collection or single-theme file that is a collection of one after promotion.</param>
    /// <param name="themeName">Collection theme name to remove (ordinal ignore-case).</param>
    /// <returns>The full collection path.</returns>
    public static string RemoveFromCollection(string collectionPath, string themeName)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(collectionPath);
        ThrowHelper.ThrowIfNullOrWhiteSpace(themeName);
        RejectJsonPalettePath(collectionPath, nameof(collectionPath));
        EnsureCollectionDestination(collectionPath);

        if (!File.Exists(collectionPath))
        {
            throw new FileNotFoundException(@"Palette collection file was not found.", collectionPath);
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            var collectionName = LoadDestinationPalettes(collectionPath, palettes);
            var index = FindPaletteIndex(palettes, themeName);
            if (index < 0)
            {
                ThrowHelper.ThrowArgumentException($@"Theme '{themeName}' was not found in the collection.", nameof(themeName));
            }

            if (palettes.Count == 1)
            {
                ThrowHelper.ThrowInvalidOperationException(@"A .ktheme collection cannot be empty. Delete the file to remove the last theme.");
            }

            palettes[index].Dispose();
            palettes.RemoveAt(index);
            return ExportCollection(collectionPath, palettes, ignoreDefaults: false, collectionName);
        }
        finally
        {
            DisposePalettes(palettes);
        }
    }

    private readonly struct CollectionAddRequest
    {
        internal CollectionAddRequest(string sourcePath, string? themeName)
        {
            SourcePath = sourcePath;
            ThemeName = themeName;
        }

        internal string SourcePath { get; }

        internal string? ThemeName { get; }
    }

    private static string AddToCollectionCore(string collectionPath, IList<CollectionAddRequest> requests, bool replaceExisting)
    {
        RejectJsonPalettePath(collectionPath, nameof(collectionPath));
        EnsureCollectionDestination(collectionPath);

        var collectionFull = Path.GetFullPath(collectionPath);
        for (var i = 0; i < requests.Count; i++)
        {
            RejectJsonPalettePath(requests[i].SourcePath, nameof(requests));
            if (string.Equals(Path.GetFullPath(requests[i].SourcePath), collectionFull, StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException(@"Cannot add a collection to itself.", nameof(requests));
            }
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            var collectionName = LoadDestinationPalettes(collectionPath, palettes);
            for (var i = 0; i < requests.Count; i++)
            {
                AddSourceToList(palettes, requests[i], replaceExisting);
            }

            return ExportCollection(collectionPath, palettes, ignoreDefaults: false, collectionName);
        }
        finally
        {
            DisposePalettes(palettes);
        }
    }

    private static void AddSourceToList(List<KryptonCustomPaletteBase> palettes, CollectionAddRequest request, bool replaceExisting)
    {
        var loaded = new List<KryptonCustomPaletteBase>();
        try
        {
            LoadSourcePalettes(request.SourcePath, request.ThemeName, loaded);
            while (loaded.Count > 0)
            {
                var incoming = loaded[0];
                loaded.RemoveAt(0);
                try
                {
                    MergePalette(palettes, incoming, replaceExisting, disposeIncomingOnFailure: false);
                }
                catch
                {
                    incoming.Dispose();
                    throw;
                }
            }
        }
        catch
        {
            DisposePalettes(loaded);
            throw;
        }
    }

    private static void EnsureCollectionDestination(string collectionPath)
    {
        if (FormatFromPath(collectionPath) != KryptonPaletteFileFormat.PaletteBinary)
        {
            ThrowHelper.ThrowArgumentException(@"Multi-theme collections can only be written as .ktheme.", nameof(collectionPath));
        }
    }

    private static string LoadDestinationPalettes(string collectionPath, List<KryptonCustomPaletteBase> palettes)
    {
        if (!File.Exists(collectionPath))
        {
            return Path.GetFileNameWithoutExtension(collectionPath) ?? string.Empty;
        }

        if (IsCollection(collectionPath))
        {
            var names = GetThemeNames(collectionPath);
            for (var i = 0; i < names.Length; i++)
            {
                var palette = new KryptonCustomPaletteBase();
                try
                {
                    palette.Import(collectionPath, names[i], silent: true);
                    palettes.Add(palette);
                }
                catch
                {
                    palette.Dispose();
                    throw;
                }
            }

            return GetCollectionName(collectionPath);
        }

        EnsureReadablePaletteFile(collectionPath);

        var single = new KryptonCustomPaletteBase();
        try
        {
            using (var stream = new FileStream(collectionPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                single.ImportWithUpgrade(stream);
            }

            if (string.IsNullOrWhiteSpace(single.GetPaletteName()))
            {
                single.SetPaletteName(Path.GetFileNameWithoutExtension(collectionPath));
            }

            palettes.Add(single);
        }
        catch
        {
            single.Dispose();
            throw;
        }

        var stem = Path.GetFileNameWithoutExtension(collectionPath);
        return string.IsNullOrWhiteSpace(stem) ? string.Empty : stem;
    }

    private static void LoadSourcePalettes(string sourcePath, string? themeName, List<KryptonCustomPaletteBase> palettes)
    {
        if (!File.Exists(sourcePath))
        {
            throw new FileNotFoundException(@"Palette file was not found.", sourcePath);
        }

        if (IsCollection(sourcePath))
        {
            var names = GetThemeNames(sourcePath);
            if (!string.IsNullOrWhiteSpace(themeName))
            {
                var match = FindThemeName(names, themeName!);
                if (match == null)
                {
                    ThrowHelper.ThrowArgumentException($@"Theme '{themeName}' was not found in '{sourcePath}'.", nameof(themeName));
                }

                names = new[] { match };
            }

            for (var i = 0; i < names.Length; i++)
            {
                var palette = new KryptonCustomPaletteBase();
                try
                {
                    palette.Import(sourcePath, names[i], silent: true);
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

        EnsureReadablePaletteFile(sourcePath);

        var single = new KryptonCustomPaletteBase();
        try
        {
            using (var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                single.ImportWithUpgrade(stream);
            }

            var name = themeName;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = single.GetPaletteName();
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = Path.GetFileNameWithoutExtension(sourcePath);
            }

            single.SetPaletteName(name!.Trim());
            palettes.Add(single);
        }
        catch
        {
            single.Dispose();
            throw;
        }
    }

    private static void EnsureReadablePaletteFile(string path)
    {
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            if (!KryptonPaletteBinaryPersistence.TryGetSchemaVersion(stream, out _))
            {
                ThrowHelper.ThrowArgumentException($@"'{path}' is not a Krypton palette file.", nameof(path));
            }
        }
    }

    private static void MergePalette(List<KryptonCustomPaletteBase> palettes,
        KryptonCustomPaletteBase incoming,
        bool replaceExisting,
        bool disposeIncomingOnFailure)
    {
        var name = incoming.GetPaletteName();
        if (string.IsNullOrWhiteSpace(name))
        {
            if (disposeIncomingOnFailure)
            {
                incoming.Dispose();
            }

            ThrowHelper.ThrowArgumentException(@"Each named palette must have a name (SetPaletteName).", nameof(incoming));
        }

        var index = FindPaletteIndex(palettes, name!);
        if (index < 0)
        {
            palettes.Add(incoming);
            return;
        }

        if (!replaceExisting)
        {
            if (disposeIncomingOnFailure)
            {
                incoming.Dispose();
            }

            ThrowHelper.ThrowArgumentException($@"Duplicate palette name '{name}'.", nameof(incoming));
        }

        palettes[index].Dispose();
        palettes[index] = incoming;
    }

    private static int FindPaletteIndex(List<KryptonCustomPaletteBase> palettes, string themeName)
    {
        for (var i = 0; i < palettes.Count; i++)
        {
            if (string.Equals(palettes[i].GetPaletteName(), themeName, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }

    private static string? FindThemeName(string[] names, string themeName)
    {
        for (var i = 0; i < names.Length; i++)
        {
            if (string.Equals(names[i], themeName, StringComparison.OrdinalIgnoreCase))
            {
                return names[i];
            }
        }

        return null;
    }

    private static void DisposePalettes(List<KryptonCustomPaletteBase> palettes, KryptonCustomPaletteBase? keep = null)
    {
        for (var i = 0; i < palettes.Count; i++)
        {
            if (!ReferenceEquals(palettes[i], keep))
            {
                palettes[i].Dispose();
            }
        }

        palettes.Clear();
    }
}
