#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// File-dialog filters, extensions, and path helpers for custom palette persistence.
/// </summary>
public static partial class KryptonPaletteFile
{
    /// <summary>
    /// Preferred extension for the XML palette document (without a leading dot).
    /// </summary>
    public const string Extension = @"kpalx";

    /// <summary>
    /// Legacy XML palette extension (without a leading dot).
    /// </summary>
    public const string XmlExtension = @"xml";

    /// <summary>
    /// Optional native binary container extension (without a leading dot).
    /// </summary>
    public const string BinaryExtension = @"kpal";

    /// <summary>
    /// Open/save dialog filter with <c>.kpalx</c> first, then optional native <c>.kpal</c>, then legacy XML.
    /// </summary>
    public const string DialogFilter =
        @"Krypton palette files (*.kpalx)|*.kpalx|Binary palette files (*.kpal)|*.kpal|XML palette files (*.xml)|*.xml|All files (*.*)|*.*";

    /// <summary>
    /// Four-byte ASCII magic written at the start of every optional KPLT <c>.kpal</c> container.
    /// </summary>
    public const string ContainerMagic = @"KPLT";

    /// <summary>
    /// Chooses a persist format from a file path. <c>.kpalx</c> and <c>.xml</c> map to
    /// <see cref="KryptonPaletteFileFormat.Xml"/>; <c>.kpal</c> maps to the optional native
    /// persist stream.
    /// </summary>
    /// <param name="path">Destination or source file path. Cannot be empty.</param>
    /// <returns>The format implied by the extension.</returns>
    public static KryptonPaletteFileFormat FormatFromPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        return string.Equals(Path.GetExtension(path), @"." + BinaryExtension, StringComparison.OrdinalIgnoreCase)
            ? KryptonPaletteFileFormat.PaletteBinary
            : KryptonPaletteFileFormat.Xml;
    }

    /// <summary>
    /// Loads a palette file, applies the XML schema upgrade when needed, and writes it to
    /// <paramref name="destinationPath"/>. The destination extension selects XML vs native <c>.kpal</c>.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c>, <c>.kpalx</c>, or KPLT <c>.kpal</c> file.</param>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <returns>The full destination path.</returns>
    /// <remarks>
    /// JSON is not a Krypton palette format. Native <c>.kpal</c> sources must already be the current
    /// schema; older palettes stay XML plus <see cref="KryptonCustomPaletteBase.ImportWithUpgrade(Stream)"/>.
    /// </remarks>
    public static string Convert(string sourcePath, string destinationPath) =>
        Convert(sourcePath, destinationPath, FormatFromPath(destinationPath), ignoreDefaults: false);

    /// <summary>
    /// Loads a palette file, applies the XML schema upgrade when needed, and writes it using
    /// <paramref name="format"/>.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c>, <c>.kpalx</c>, or KPLT <c>.kpal</c> file.</param>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="format">XML, compressed-XML container, or native binary container.</param>
    /// <returns>The full destination path.</returns>
    public static string Convert(string sourcePath, string destinationPath, KryptonPaletteFileFormat format) =>
        Convert(sourcePath, destinationPath, format, ignoreDefaults: false);

    /// <summary>
    /// Loads a palette file, applies the XML schema upgrade when needed, and writes it using
    /// <paramref name="format"/>.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c>, <c>.kpalx</c>, or KPLT <c>.kpal</c> file.</param>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="format">XML, compressed-XML container, or native binary container.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match the current base palette.</param>
    /// <returns>The full destination path.</returns>
    /// <exception cref="ArgumentException">The source or destination is JSON, which is not a palette format.</exception>
    public static string Convert(string sourcePath, string destinationPath, KryptonPaletteFileFormat format, bool ignoreDefaults)
    {
        if (string.IsNullOrWhiteSpace(sourcePath))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(sourcePath));
        }

        if (string.IsNullOrWhiteSpace(destinationPath))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(destinationPath));
        }

        RejectJsonPalettePath(sourcePath!, nameof(sourcePath));
        RejectJsonPalettePath(destinationPath!, nameof(destinationPath));

        using (var palette = new KryptonCustomPaletteBase())
        {
            using (var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                palette.ImportWithUpgrade(stream);
            }

            palette.Export(destinationPath, ignoreDefaults, silent: true, format);
        }

        return Path.GetFullPath(destinationPath);
    }

    /// <summary>
    /// Returns the theme names stored in a palette file. A single-theme file yields one name
    /// (possibly empty). A <c>.kpal</c> pack yields every packed name.
    /// </summary>
    /// <param name="path">Existing <c>.xml</c>, <c>.kpalx</c>, or KPLT <c>.kpal</c> file.</param>
    /// <returns>Theme names in file order.</returns>
    public static string[] GetThemeNames(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        RejectJsonPalettePath(path!, nameof(path));
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return KryptonPaletteBinaryPersistence.GetThemeNames(stream);
    }

    /// <summary>
    /// Recommended width and height in pixels for <see cref="KryptonCustomPaletteBase.Thumbnail"/>.
    /// </summary>
    public const int RecommendedThumbnailSize = 64;

    /// <summary>
    /// Four-byte ASCII magic for the optional thumbnail catalog after a kind-2 pack.
    /// Older readers ignore bytes past the pack entries; do not bump the KPLT container version.
    /// </summary>
    public const string ThumbnailCatalogMagic = @"KPTH";

    /// <summary>
    /// Returns preview images stored for each theme, in the same order as <see cref="GetThemeNames"/>.
    /// Missing previews are <see langword="null"/>. The caller disposes non-null images.
    /// </summary>
    /// <param name="path">Existing palette file.</param>
    /// <returns>One slot per theme name.</returns>
    public static Image?[] GetThemeThumbnails(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        RejectJsonPalettePath(path!, nameof(path));
        if (IsPack(path))
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return KryptonPaletteBinaryPersistence.GetPackThemeThumbnails(stream);
        }

        using (var palette = new KryptonCustomPaletteBase())
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                palette.ImportWithUpgrade(stream);
            }

            var images = new Image?[1];
            if (palette.Thumbnail != null)
            {
                images[0] = new Bitmap(palette.Thumbnail);
            }

            return images;
        }
    }

    /// <summary>
    /// Returns whether <paramref name="path"/> is a multi-theme KPLT pack (payload kind 2).
    /// Single-theme <c>.kpal</c>, <c>.kpalx</c>, and XML files return <see langword="false"/>.
    /// </summary>
    /// <param name="path">Existing palette file.</param>
    /// <returns><see langword="true"/> when the file is a named theme pack.</returns>
    public static bool IsPack(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        RejectJsonPalettePath(path!, nameof(path));
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return KryptonPaletteBinaryPersistence.IsPack(stream);
    }

    /// <summary>
    /// Writes two or more named palettes into one native <c>.kpal</c> pack. Each palette must
    /// have a unique <c>SetPaletteName</c> value. Destination extension must be <c>.kpal</c>.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="palettes">Palettes to pack. Cannot be empty.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportPack(string destinationPath, IEnumerable<KryptonCustomPaletteBase> palettes) =>
        ExportPack(destinationPath, palettes, ignoreDefaults: false, packName: null);

    /// <summary>
    /// Writes named palettes into one native <c>.kpal</c> pack.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="palettes">Palettes to pack. Cannot be empty.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match the current base palette.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportPack(string destinationPath, IEnumerable<KryptonCustomPaletteBase> palettes, bool ignoreDefaults) =>
        ExportPack(destinationPath, palettes, ignoreDefaults, packName: null);

    /// <summary>
    /// Writes named palettes into one native <c>.kpal</c> pack.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite. Must be <c>.kpal</c>.</param>
    /// <param name="palettes">Palettes to pack. Cannot be empty.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match the current base palette.</param>
    /// <param name="packName">Optional display name for the pack (header name, not a theme name).</param>
    /// <returns>The full destination path.</returns>
    public static string ExportPack(string destinationPath, IEnumerable<KryptonCustomPaletteBase> palettes, bool ignoreDefaults, string? packName)
    {
        if (string.IsNullOrWhiteSpace(destinationPath))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(destinationPath));
        }

        ThrowHelper.ThrowIfNull(palettes);
        RejectJsonPalettePath(destinationPath!, nameof(destinationPath));

        if (FormatFromPath(destinationPath) != KryptonPaletteFileFormat.PaletteBinary)
        {
            ThrowHelper.ThrowArgumentException(@"Multi-theme packs can only be written as .kpal.", nameof(destinationPath));
        }

        var list = palettes as IList<KryptonCustomPaletteBase> ?? new List<KryptonCustomPaletteBase>(palettes!);
        using (var stream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            KryptonPaletteBinaryPersistence.ExportPack(stream, list, ignoreDefaults, packName ?? string.Empty);
        }

        return Path.GetFullPath(destinationPath);
    }

    /// <summary>
    /// Returns whether <paramref name="pathOrExtension"/> is a <c>.kpal</c> or <c>.kpalx</c> palette file.
    /// </summary>
    /// <param name="pathOrExtension">A file path, or an extension with or without a leading dot.</param>
    /// <returns><see langword="true"/> when the extension is a Krypton palette file.</returns>
    public static bool IsPaletteExtension(string? pathOrExtension)
    {
        if (string.IsNullOrWhiteSpace(pathOrExtension))
        {
            return false;
        }

        var extension = pathOrExtension!.IndexOf('.') >= 0
            ? Path.GetExtension(pathOrExtension)
            : @"." + pathOrExtension;

        return string.Equals(extension, @"." + Extension, StringComparison.OrdinalIgnoreCase)
            || string.Equals(extension, @"." + BinaryExtension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Registers per-user Explorer icons for <c>.kpal</c> and <c>.kpalx</c> using the Stable Kr tile.
    /// </summary>
    /// <remarks>
    /// Writes <c>HKCU\Software\Classes</c> only. Safe to call more than once. Failures are ignored so
    /// restricted designer hosts still work.
    /// </remarks>
    public static void EnsureShellAssociations()
    {
        if (Interlocked.CompareExchange(ref _shellAssociationsState, 1, 0) != 0)
        {
            return;
        }

        try
        {
            var iconPath = ExtractShellIconFile();
            RegisterExtension(Extension, @"Krypton.Toolkit.PaletteXml", @"Krypton Palette XML", iconPath);
            RegisterExtension(BinaryExtension, @"Krypton.Toolkit.PaletteBinary", @"Krypton Palette", iconPath);
            SHChangeNotify(ShcneAssocChanged, ShcnfIdList, IntPtr.Zero, IntPtr.Zero);
        }
        catch (Exception)
        {
            Interlocked.Exchange(ref _shellAssociationsState, -1);
        }
    }

    /// <summary>
    /// Creates an icon for palette files from the embedded Stable Kr tile.
    /// </summary>
    /// <param name="largeIcon"><see langword="true"/> for 32×32; otherwise 16×16.</param>
    /// <returns>A new <see cref="Icon"/> the caller must dispose, or <see langword="null"/> if the resource is missing.</returns>
    public static Icon? CreateShellIcon(bool largeIcon = false)
    {
        var stream = typeof(KryptonPaletteFile).Assembly.GetManifestResourceStream(ShellIconResourceName);
        if (stream == null)
        {
            return null;
        }

        using (stream)
        using (var source = new Icon(stream))
        {
            var size = largeIcon ? 32 : 16;
            return new Icon(source, new Size(size, size));
        }
    }

    private const string ShellIconResourceName = @"Krypton.Toolkit.Resources.KryptonPalette.ico";
    private const uint ShcneAssocChanged = 0x08000000;
    private const uint ShcnfIdList = 0x0000;
    private static int _shellAssociationsState;

    [DllImport(@"shell32.dll")]
    private static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

    private static string ExtractShellIconFile()
    {
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Krypton-Suite");
        Directory.CreateDirectory(folder);
        var iconPath = Path.Combine(folder, @"KryptonPalette.ico");

        using var stream = typeof(KryptonPaletteFile).Assembly.GetManifestResourceStream(ShellIconResourceName);
        if (stream == null)
        {
            ThrowHelper.ThrowInvalidOperationException(@"Embedded palette file icon is missing.");
        }

        var bytes = new byte[stream!.Length];
        var read = 0;
        while (read < bytes.Length)
        {
            var n = stream.Read(bytes, read, bytes.Length - read);
            if (n == 0)
            {
                break;
            }

            read += n;
        }

        if (!File.Exists(iconPath) || new FileInfo(iconPath).Length != bytes.Length)
        {
            File.WriteAllBytes(iconPath, bytes);
        }

        return iconPath;
    }

    private static void RejectJsonPalettePath(string path, string paramName)
    {
        if (string.Equals(Path.GetExtension(path), @".json", StringComparison.OrdinalIgnoreCase))
        {
            ThrowHelper.ThrowArgumentException(
                @"JSON is not a Krypton palette format. Convert XML (.xml / .kpalx) or a KPLT .kpal file.",
                paramName);
        }
    }

    private static void RegisterExtension(string extensionWithoutDot, string progId, string friendlyName, string iconPath)
    {
        using (var extKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\." + extensionWithoutDot))
        {
            extKey?.SetValue(null, progId);
        }

        using (var progKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + progId))
        {
            progKey?.SetValue(null, friendlyName);
            using var iconKey = progKey?.CreateSubKey(@"DefaultIcon");
            iconKey?.SetValue(null, iconPath);
        }
    }
}
