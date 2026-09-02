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
/// Add, remove, and rename helpers for multi-theme <c>.kpal</c> packs.
/// Rewrites use the existing KPLT kind-2 layout (no container-version bump).
/// </content>
public static partial class KryptonPaletteFile
{
    /// <summary>
    /// Returns the pack display name stored in the KPLT header of a kind-2 <c>.kpal</c>.
    /// Single-theme files, XML, and <c>.kpalx</c> return empty.
    /// </summary>
    /// <param name="path">Existing palette file.</param>
    /// <returns>Header name, or empty when the file is not a pack.</returns>
    public static string GetPackName(string path)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(path);

        RejectJsonPalettePath(path, nameof(path));
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return KryptonPaletteBinaryPersistence.GetPackDisplayName(stream);
    }

    /// <summary>
    /// Rewrites a kind-2 pack with a new header display name. Theme payloads are unchanged.
    /// </summary>
    /// <param name="packPath">Existing <c>.kpal</c> pack.</param>
    /// <param name="packName">New display name. May be empty.</param>
    /// <returns>The full pack path.</returns>
    public static string SetPackName(string packPath, string? packName)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(packPath);
        RejectJsonPalettePath(packPath, nameof(packPath));
        EnsurePackDestination(packPath);

        if (!File.Exists(packPath))
        {
            throw new FileNotFoundException(@"Palette pack file was not found.", packPath);
        }

        if (!IsPack(packPath))
        {
            ThrowHelper.ThrowArgumentException(@"File is not a multi-theme .kpal pack.", nameof(packPath));
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            LoadDestinationPalettes(packPath, palettes);
            return ExportPack(packPath, palettes, ignoreDefaults: false, packName ?? string.Empty);
        }
        finally
        {
            DisposePalettes(palettes);
        }
    }

    /// <summary>
    /// Adds a palette file to a <c>.kpal</c> pack. Missing destination files are created.
    /// A single-theme <c>.kpal</c> is promoted to a pack. Source packs add every named theme.
    /// </summary>
    /// <param name="packPath">Pack to create or update. Must be <c>.kpal</c>.</param>
    /// <param name="sourcePath">Palette file to add (<c>.kpalx</c>, <c>.xml</c>, or <c>.kpal</c>).</param>
    /// <returns>The full pack path.</returns>
    public static string AddToPack(string packPath, string sourcePath) =>
        AddToPack(packPath, sourcePath, themeName: null, replaceExisting: false);

    /// <summary>
    /// Adds a palette file to a <c>.kpal</c> pack.
    /// </summary>
    /// <param name="packPath">Pack to create or update. Must be <c>.kpal</c>.</param>
    /// <param name="sourcePath">Palette file to add.</param>
    /// <param name="themeName">
    /// Destination name for a single-theme source, or the packed name to copy from a source pack.
    /// When omitted, a single-theme file uses <c>GetPaletteName</c> or the file stem; a source pack adds every theme.
    /// </param>
    /// <param name="replaceExisting"><see langword="true"/> to overwrite a theme with the same name.</param>
    /// <returns>The full pack path.</returns>
    public static string AddToPack(string packPath, string sourcePath, string? themeName, bool replaceExisting)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(packPath);
        ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);
        return AddToPackCore(packPath, new[] { new PackAddRequest(sourcePath, themeName) }, replaceExisting);
    }

    /// <summary>
    /// Adds each palette file to a <c>.kpal</c> pack in one rewrite.
    /// </summary>
    /// <param name="packPath">Pack to create or update. Must be <c>.kpal</c>.</param>
    /// <param name="sourcePaths">Palette files to add. Cannot be empty.</param>
    /// <param name="replaceExisting"><see langword="true"/> to overwrite themes with the same name.</param>
    /// <returns>The full pack path.</returns>
    public static string AddToPack(string packPath, IEnumerable<string> sourcePaths, bool replaceExisting = false)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(packPath);
        ThrowHelper.ThrowIfNull(sourcePaths);

        var requests = new List<PackAddRequest>();
        foreach (var sourcePath in sourcePaths)
        {
            ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);
            requests.Add(new PackAddRequest(sourcePath!, themeName: null));
        }

        if (requests.Count == 0)
        {
            ThrowHelper.ThrowArgumentException(@"At least one palette file is required.", nameof(sourcePaths));
        }

        return AddToPackCore(packPath, requests, replaceExisting);
    }

    /// <summary>
    /// Adds an in-memory palette to a <c>.kpal</c> pack. The palette must have a unique
    /// <c>SetPaletteName</c> value unless <paramref name="replaceExisting"/> is <see langword="true"/>.
    /// The caller still owns <paramref name="palette"/>.
    /// </summary>
    /// <param name="packPath">Pack to create or update. Must be <c>.kpal</c>.</param>
    /// <param name="palette">Named palette to pack.</param>
    /// <param name="replaceExisting"><see langword="true"/> to overwrite a theme with the same name.</param>
    /// <returns>The full pack path.</returns>
    public static string AddToPack(string packPath, KryptonCustomPaletteBase palette, bool replaceExisting = false)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(packPath);
        ThrowHelper.ThrowIfNull(palette);
        RejectJsonPalettePath(packPath, nameof(packPath));
        EnsurePackDestination(packPath);

        if (string.IsNullOrWhiteSpace(palette!.GetPaletteName()))
        {
            ThrowHelper.ThrowArgumentException(@"The palette must have a name (SetPaletteName).", nameof(palette));
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            var packName = LoadDestinationPalettes(packPath, palettes);
            MergePalette(palettes, palette, replaceExisting, disposeIncomingOnFailure: false);
            return ExportPack(packPath, palettes, ignoreDefaults: false, packName);
        }
        finally
        {
            DisposePalettes(palettes, palette);
        }
    }

    /// <summary>
    /// Removes a named theme from a <c>.kpal</c> pack. The last remaining theme cannot be removed
    /// (a pack cannot be empty); delete the file instead.
    /// </summary>
    /// <param name="packPath">Existing <c>.kpal</c> pack or single-theme file that is a pack of one after promotion.</param>
    /// <param name="themeName">Packed name to remove (ordinal ignore-case).</param>
    /// <returns>The full pack path.</returns>
    public static string RemoveFromPack(string packPath, string themeName)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(packPath);
        ThrowHelper.ThrowIfNullOrWhiteSpace(themeName);
        RejectJsonPalettePath(packPath, nameof(packPath));
        EnsurePackDestination(packPath);

        if (!File.Exists(packPath))
        {
            throw new FileNotFoundException(@"Palette pack file was not found.", packPath);
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            var packName = LoadDestinationPalettes(packPath, palettes);
            var index = FindPaletteIndex(palettes, themeName);
            if (index < 0)
            {
                ThrowHelper.ThrowArgumentException($@"Theme '{themeName}' was not found in the pack.", nameof(themeName));
            }

            if (palettes.Count == 1)
            {
                ThrowHelper.ThrowInvalidOperationException(@"A .kpal pack cannot be empty. Delete the file to remove the last theme.");
            }

            palettes[index].Dispose();
            palettes.RemoveAt(index);
            return ExportPack(packPath, palettes, ignoreDefaults: false, packName);
        }
        finally
        {
            DisposePalettes(palettes);
        }
    }

    private readonly struct PackAddRequest
    {
        internal PackAddRequest(string sourcePath, string? themeName)
        {
            SourcePath = sourcePath;
            ThemeName = themeName;
        }

        internal string SourcePath { get; }

        internal string? ThemeName { get; }
    }

    private static string AddToPackCore(string packPath, IList<PackAddRequest> requests, bool replaceExisting)
    {
        RejectJsonPalettePath(packPath, nameof(packPath));
        EnsurePackDestination(packPath);

        var packFull = Path.GetFullPath(packPath);
        for (var i = 0; i < requests.Count; i++)
        {
            RejectJsonPalettePath(requests[i].SourcePath, nameof(requests));
            if (string.Equals(Path.GetFullPath(requests[i].SourcePath), packFull, StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException(@"Cannot add a pack to itself.", nameof(requests));
            }
        }

        var palettes = new List<KryptonCustomPaletteBase>();
        try
        {
            var packName = LoadDestinationPalettes(packPath, palettes);
            for (var i = 0; i < requests.Count; i++)
            {
                AddSourceToList(palettes, requests[i], replaceExisting);
            }

            return ExportPack(packPath, palettes, ignoreDefaults: false, packName);
        }
        finally
        {
            DisposePalettes(palettes);
        }
    }

    private static void AddSourceToList(List<KryptonCustomPaletteBase> palettes, PackAddRequest request, bool replaceExisting)
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

    private static void EnsurePackDestination(string packPath)
    {
        if (FormatFromPath(packPath) != KryptonPaletteFileFormat.PaletteBinary)
        {
            ThrowHelper.ThrowArgumentException(@"Multi-theme packs can only be written as .kpal.", nameof(packPath));
        }
    }

    private static string LoadDestinationPalettes(string packPath, List<KryptonCustomPaletteBase> palettes)
    {
        if (!File.Exists(packPath))
        {
            return Path.GetFileNameWithoutExtension(packPath) ?? string.Empty;
        }

        if (IsPack(packPath))
        {
            var names = GetThemeNames(packPath);
            for (var i = 0; i < names.Length; i++)
            {
                var palette = new KryptonCustomPaletteBase();
                try
                {
                    palette.Import(packPath, names[i], silent: true);
                    palettes.Add(palette);
                }
                catch
                {
                    palette.Dispose();
                    throw;
                }
            }

            return GetPackName(packPath);
        }

        EnsureReadablePaletteFile(packPath);

        var single = new KryptonCustomPaletteBase();
        try
        {
            using (var stream = new FileStream(packPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                single.ImportWithUpgrade(stream);
            }

            if (string.IsNullOrWhiteSpace(single.GetPaletteName()))
            {
                single.SetPaletteName(Path.GetFileNameWithoutExtension(packPath));
            }

            palettes.Add(single);
        }
        catch
        {
            single.Dispose();
            throw;
        }

        var stem = Path.GetFileNameWithoutExtension(packPath);
        return string.IsNullOrWhiteSpace(stem) ? string.Empty : stem;
    }

    private static void LoadSourcePalettes(string sourcePath, string? themeName, List<KryptonCustomPaletteBase> palettes)
    {
        if (!File.Exists(sourcePath))
        {
            throw new FileNotFoundException(@"Palette file was not found.", sourcePath);
        }

        if (IsPack(sourcePath))
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

            ThrowHelper.ThrowArgumentException(@"Each packed palette must have a name (SetPaletteName).", nameof(incoming));
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
