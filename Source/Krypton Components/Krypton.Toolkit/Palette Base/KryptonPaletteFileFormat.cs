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
    public const string Extension = @"kthemex";

    /// <summary>
    /// Legacy XML palette extension (without a leading dot).
    /// </summary>
    // ToDo V120 LTS: Remove legacy .xml palette file support (constant, dialog filter, and FormatFromPath).
    // Consumers should call UpgradeXmlToKthemex or Convert to rewrite .xml as .kthemex.
    public const string XmlExtension = @"xml";

    /// <summary>
    /// Optional native binary container extension (without a leading dot).
    /// </summary>
    public const string BinaryExtension = @"ktheme";

    /// <summary>
    /// Open/save dialog filter with <c>.kthemex</c> first, then optional native <c>.ktheme</c>,
    /// then legacy <c>.xml</c>.
    /// </summary>
    // ToDo V120 LTS: Drop the *.xml filter entry once XmlExtension is removed.
    public const string DialogFilter =
        @"Krypton theme files (*.kthemex)|*.kthemex|Krypton theme containers (*.ktheme)|*.ktheme|XML palette files (*.xml)|*.xml|All files (*.*)|*.*";

    /// <summary>
    /// Four-byte ASCII magic written at the start of every optional KPLT <c>.ktheme</c> container.
    /// </summary>
    public const string ContainerMagic = @"KPLT";

    /// <summary>
    /// Chooses a persist format from a file path. <c>.kthemex</c> and <c>.xml</c> map to
    /// <see cref="KryptonPaletteFileFormat.Xml"/>; <c>.ktheme</c> maps to the optional native
    /// persist stream.
    /// </summary>
    /// <param name="path">Destination or source file path. Cannot be empty.</param>
    /// <returns>The format implied by the extension.</returns>
    // ToDo V120 LTS: Stop mapping .xml to Xml; only .kthemex (and non-.ktheme fallbacks) should select XML persist.
    public static KryptonPaletteFileFormat FormatFromPath(string path)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(path);

        return string.Equals(Path.GetExtension(path), @"." + BinaryExtension, StringComparison.OrdinalIgnoreCase)
            ? KryptonPaletteFileFormat.PaletteBinary
            : KryptonPaletteFileFormat.Xml;
    }

    /// <summary>
    /// Loads a palette file, applies the XML schema upgrade when needed, and writes it to
    /// <paramref name="destinationPath"/>. The destination extension selects XML vs native <c>.ktheme</c>.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c>, <c>.kthemex</c>, or KPLT <c>.ktheme</c> file.</param>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <returns>The full destination path.</returns>
    /// <remarks>
    /// JSON is not a Krypton palette format. Native <c>.ktheme</c> sources must already be the current
    /// schema; older palettes stay XML plus <see cref="KryptonCustomPaletteBase.ImportWithUpgrade(Stream)"/>,
    /// which raises the XML schema version before export.
    /// For a dedicated <c>.xml</c> → <c>.kthemex</c> rewrite, prefer <see cref="UpgradeXmlToKthemex(string)"/>.
    /// </remarks>
    public static string Convert(string sourcePath, string destinationPath) =>
        Convert(sourcePath, destinationPath, FormatFromPath(destinationPath), ignoreDefaults: false);

    /// <summary>
    /// Loads a palette file, applies the XML schema upgrade when needed, and writes it using
    /// <paramref name="format"/>.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c>, <c>.kthemex</c>, or KPLT <c>.ktheme</c> file.</param>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="format">XML, compressed-XML container, or native binary container.</param>
    /// <returns>The full destination path.</returns>
    public static string Convert(string sourcePath, string destinationPath, KryptonPaletteFileFormat format) =>
        Convert(sourcePath, destinationPath, format, ignoreDefaults: false);

    /// <summary>
    /// Loads a palette file, applies the XML schema upgrade when needed, and writes it using
    /// <paramref name="format"/>.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c>, <c>.kthemex</c>, or KPLT <c>.ktheme</c> file.</param>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="format">XML, compressed-XML container, or native binary container.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match the current base palette.</param>
    /// <returns>The full destination path.</returns>
    /// <exception cref="ArgumentException">The source or destination is JSON, which is not a palette format.</exception>
    public static string Convert(string sourcePath, string destinationPath, KryptonPaletteFileFormat format, bool ignoreDefaults)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);
        ThrowHelper.ThrowIfNullOrWhiteSpace(destinationPath);

        RejectJsonPalettePath(sourcePath, nameof(sourcePath));
        RejectJsonPalettePath(destinationPath, nameof(destinationPath));

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
    /// Rewrites a legacy <c>.xml</c> palette as <c>.kthemex</c> in the same folder
    /// (<c>theme.xml</c> → <c>theme.kthemex</c>). The source file is left in place.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c> palette file.</param>
    /// <returns>The full path of the written <c>.kthemex</c> file.</returns>
    /// <remarks>
    /// Applies <see cref="KryptonCustomPaletteBase.ImportWithUpgrade(Stream)"/> so older schema
    /// versions are raised to the current persist version before the <c>.kthemex</c> is written.
    /// Prefer this over saving a new <c>.xml</c> file. The document inside <c>.kthemex</c> remains XML.
    /// </remarks>
    /// <exception cref="ArgumentException">The source is not a <c>.xml</c> file.</exception>
    public static string UpgradeXmlToKthemex(string sourcePath)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);
        return UpgradeXmlToKthemex(sourcePath, Path.ChangeExtension(sourcePath, @"." + Extension));
    }

    /// <summary>
    /// Rewrites a legacy <c>.xml</c> palette as <c>.kthemex</c>. The source file is left in place.
    /// </summary>
    /// <param name="sourcePath">Existing <c>.xml</c> palette file.</param>
    /// <param name="destinationPath">File to create or overwrite. Must be <c>.kthemex</c>.</param>
    /// <returns>The full path of the written <c>.kthemex</c> file.</returns>
    /// <exception cref="ArgumentException">The source is not <c>.xml</c>, or the destination is not <c>.kthemex</c>.</exception>
    public static string UpgradeXmlToKthemex(string sourcePath, string destinationPath)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);
        ThrowHelper.ThrowIfNullOrWhiteSpace(destinationPath);

        if (!IsLegacyXmlExtension(sourcePath))
        {
            ThrowHelper.ThrowArgumentException(
                @"Source must be a legacy .xml palette file. Preferred palettes use .kthemex; use Convert for other formats.",
                nameof(sourcePath));
        }

        if (!string.Equals(Path.GetExtension(destinationPath), @"." + Extension, StringComparison.OrdinalIgnoreCase))
        {
            ThrowHelper.ThrowArgumentException(@"Destination must be a .kthemex file.", nameof(destinationPath));
        }

        return Convert(sourcePath, destinationPath, KryptonPaletteFileFormat.Xml, ignoreDefaults: false);
    }

    /// <summary>
    /// When <paramref name="sourcePath"/> is a legacy <c>.xml</c> palette and prompting is enabled,
    /// warns that the format may be removed and offers to upgrade to <c>.kthemex</c> before load.
    /// </summary>
    /// <param name="sourcePath">Palette file the caller is about to import or apply.</param>
    /// <param name="silent">When <see langword="true"/>, skip the dialog and return <paramref name="sourcePath"/>.</param>
    /// <returns>
    /// The path to import: the original file, a newly written <c>.kthemex</c>, or
    /// <see langword="null"/> if the user cancelled.
    /// Non-<c>.xml</c> paths are returned unchanged.
    /// </returns>
    /// <remarks>
    /// Strings come from <see cref="KryptonMiscellaneousThemeStrings.LegacyXmlUpgradeTitle"/> and
    /// <see cref="KryptonMiscellaneousThemeStrings.LegacyXmlUpgradeMessage"/>. Yes / No / Cancel
    /// captions come from <see cref="GeneralToolkitStrings"/>.
    /// </remarks>
    // ToDo V120 LTS: Stop offering "load .xml anyway"; upgrade or cancel only.
    public static string? PromptLegacyXmlUpgrade(string sourcePath, bool silent)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(sourcePath);

        if (silent || !IsLegacyXmlExtension(sourcePath) || !SystemInformation.UserInteractive)
        {
            return sourcePath;
        }

        var themeStrings = KryptonManager.Strings.MiscellaneousThemeStrings;
        var general = KryptonManager.Strings.GeneralStrings;
        var fileName = Path.GetFileName(sourcePath);
        string message;
        try
        {
            message = string.Format(themeStrings.LegacyXmlUpgradeMessage,
                fileName,
                StripMenuAccelerator(general.Yes),
                StripMenuAccelerator(general.No),
                StripMenuAccelerator(general.Cancel));
        }
        catch (FormatException)
        {
            message = string.Format(
                @"'{0}' uses the legacy .xml palette format. Prefer .kthemex. {1}: upgrade and apply. {2}: apply .xml without upgrading. {3}: do not apply.",
                fileName,
                StripMenuAccelerator(general.Yes),
                StripMenuAccelerator(general.No),
                StripMenuAccelerator(general.Cancel));
        }

        var result = KryptonMessageBox.Show(message,
            themeStrings.LegacyXmlUpgradeTitle,
            KryptonMessageBoxButtons.YesNoCancel,
            KryptonMessageBoxIcon.Warning,
            KryptonMessageBoxDefaultButton.Button1);

        switch (result)
        {
            case DialogResult.Yes:
                return UpgradeXmlToKthemex(sourcePath);
            case DialogResult.No:
                return sourcePath;
            default:
                return null;
        }
    }

    private static string StripMenuAccelerator(string value) =>
        string.IsNullOrEmpty(value) ? value : value.Replace(@"&", string.Empty);

    /// <summary>
    /// Returns whether <paramref name="pathOrExtension"/> uses the legacy <c>.xml</c> palette extension.
    /// </summary>
    /// <param name="pathOrExtension">A file path, or an extension with or without a leading dot.</param>
    /// <returns><see langword="true"/> when the extension is <c>.xml</c>.</returns>
    // ToDo V120 LTS: Remove with XmlExtension. IsPaletteExtension already excludes .xml.
    public static bool IsLegacyXmlExtension(string? pathOrExtension)
    {
        if (string.IsNullOrWhiteSpace(pathOrExtension))
        {
            return false;
        }

        var extension = pathOrExtension!.IndexOf('.') >= 0
            ? Path.GetExtension(pathOrExtension)
            : @"." + pathOrExtension;

        return string.Equals(extension, @"." + XmlExtension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns the theme names stored in a palette file. A single-theme file yields one name
    /// (possibly empty). A <c>.ktheme</c> collection yields every named theme.
    /// </summary>
    /// <param name="path">Existing <c>.xml</c>, <c>.kthemex</c>, or KPLT <c>.ktheme</c> file.</param>
    /// <returns>Theme names in file order.</returns>
    // ToDo V120 LTS: Drop .xml from this remark; GetThemeNames still reads .kthemex XML content.
    public static string[] GetThemeNames(string path)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(path);

        RejectJsonPalettePath(path, nameof(path));
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return KryptonPaletteBinaryPersistence.GetThemeNames(stream);
    }

    /// <summary>
    /// Recommended width and height in pixels for <see cref="KryptonCustomPaletteBase.Thumbnail"/>.
    /// </summary>
    public const int RecommendedThumbnailSize = 64;

    /// <summary>
    /// Four-byte ASCII magic for the optional thumbnail catalog after a kind-2 collection.
    /// Older readers ignore bytes past the collection entries; do not bump the KPLT container version.
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
        ThrowHelper.ThrowIfNullOrWhiteSpace(path);

        RejectJsonPalettePath(path, nameof(path));
        if (IsCollection(path))
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return KryptonPaletteBinaryPersistence.GetCollectionThemeThumbnails(stream);
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
    /// Returns whether <paramref name="path"/> is a multi-theme KPLT collection (payload kind 2).
    /// Single-theme <c>.ktheme</c>, <c>.kthemex</c>, and XML files return <see langword="false"/>.
    /// </summary>
    /// <param name="path">Existing palette file.</param>
    /// <returns><see langword="true"/> when the file is a named theme collection.</returns>
    public static bool IsCollection(string path)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(path);

        RejectJsonPalettePath(path, nameof(path));
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return KryptonPaletteBinaryPersistence.IsCollection(stream);
    }

    /// <summary>
    /// Writes two or more named palettes into one native <c>.ktheme</c> pack. Each palette must
    /// have a unique <c>SetPaletteName</c> value. Destination extension must be <c>.ktheme</c>.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="palettes">Palettes to collection. Cannot be empty.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportCollection(string destinationPath, IEnumerable<KryptonCustomPaletteBase> palettes) =>
        ExportCollection(destinationPath, palettes, ignoreDefaults: false, collectionName: null);

    /// <summary>
    /// Writes named palettes into one native <c>.ktheme</c> pack.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite.</param>
    /// <param name="palettes">Palettes to collection. Cannot be empty.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match the current base palette.</param>
    /// <returns>The full destination path.</returns>
    public static string ExportCollection(string destinationPath, IEnumerable<KryptonCustomPaletteBase> palettes, bool ignoreDefaults) =>
        ExportCollection(destinationPath, palettes, ignoreDefaults, collectionName: null);

    /// <summary>
    /// Writes named palettes into one native <c>.ktheme</c> pack.
    /// </summary>
    /// <param name="destinationPath">File to create or overwrite. Must be <c>.ktheme</c>.</param>
    /// <param name="palettes">Palettes to collection. Cannot be empty.</param>
    /// <param name="ignoreDefaults"><see langword="true"/> to omit values that match the current base palette.</param>
    /// <param name="collectionName">Optional display name for the collection (header name, not a theme name).</param>
    /// <returns>The full destination path.</returns>
    public static string ExportCollection(string destinationPath, IEnumerable<KryptonCustomPaletteBase> palettes, bool ignoreDefaults, string? collectionName)
    {
        ThrowHelper.ThrowIfNullOrWhiteSpace(destinationPath);
        ThrowHelper.ThrowIfNull(palettes);
        RejectJsonPalettePath(destinationPath, nameof(destinationPath));

        if (FormatFromPath(destinationPath) != KryptonPaletteFileFormat.PaletteBinary)
        {
            ThrowHelper.ThrowArgumentException(@"Multi-theme collections can only be written as .ktheme.", nameof(destinationPath));
        }

        var list = palettes as IList<KryptonCustomPaletteBase> ?? new List<KryptonCustomPaletteBase>(palettes);
        using (var stream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            KryptonPaletteBinaryPersistence.ExportCollection(stream, list, ignoreDefaults, collectionName ?? string.Empty);
        }

        return Path.GetFullPath(destinationPath);
    }

    /// <summary>
    /// Returns whether <paramref name="pathOrExtension"/> is a <c>.ktheme</c> or <c>.kthemex</c> palette file.
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
    /// Registers per-user Explorer icons for <c>.ktheme</c> and <c>.kthemex</c> using the Stable Kr tile.
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
            RegisterExtension(Extension, @"Krypton.Toolkit.PaletteXml", @"Krypton Theme XML", iconPath);
            RegisterExtension(BinaryExtension, @"Krypton.Toolkit.PaletteBinary", @"Krypton Theme Container", iconPath);
            UnregisterUnreleasedExtension(@"kpal", @"Krypton.Toolkit.PaletteBinary");
            UnregisterUnreleasedExtension(@"kpalx", @"Krypton.Toolkit.PaletteXmlLegacy");
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
                @"JSON is not a Krypton palette format. Convert XML (.xml / .kthemex) or a KPLT .ktheme file.",
                paramName);
        }
    }

    private static void UnregisterUnreleasedExtension(string extensionWithoutDot, string progId)
    {
        var extensionKey = @"Software\Classes\." + extensionWithoutDot;
        using (var extKey = Registry.CurrentUser.OpenSubKey(extensionKey))
        {
            if (extKey == null)
            {
                return;
            }

            var current = extKey.GetValue(null) as string;
            if (!string.Equals(current, progId, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }

        Registry.CurrentUser.DeleteSubKeyTree(extensionKey, throwOnMissingSubKey: false);
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
