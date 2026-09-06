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
/// Reads and writes the versioned <c>KPLT</c> palette container and the native persist payload.
/// </summary>
/// <remarks>
/// Container layout (little-endian):
/// magic (4 ASCII bytes "KPLT"), container version (uint16, currently 1),
/// payload kind (uint16: 0 = Deflate XML, 1 = native persist, 2 = named theme collection),
/// palette schema version (int32), name length (uint16) + UTF-8 name, then payload bytes.
/// Kind 2 payload: uint16 count, then count entries of (uint16 name length + UTF-8 name,
/// uint16 inner kind, int32 payload length, payload bytes). Optional trailing catalog:
/// ASCII "KPTH", uint16 catalog version, int32 byte length, then that many bytes
/// (uint16 count, then per-image name + width + height + PNG). Older readers ignore the tail.
/// Do not bump the container version for the catalog.
/// </remarks>
internal static class KryptonPaletteBinaryPersistence
{
    internal const ushort CurrentContainerVersion = 1;
    internal const ushort KindCompressedXml = 0;
    internal const ushort KindNative = 1;
    internal const ushort KindCollection = 2;
    internal const ushort ThumbnailCatalogVersion = 1;

    private static readonly byte[] ThumbnailCatalogMagicBytes = Encoding.ASCII.GetBytes(@"KPTH");

    private const byte RecordEnd = 0;
    private const byte RecordNavigate = 1;
    private const byte RecordValue = 2;

    private static readonly byte[] MagicBytes = Encoding.ASCII.GetBytes(KryptonPaletteFile.ContainerMagic);

    internal enum SniffedKind
    {
        Unknown,
        Xml,
        Container
    }

    internal static SniffedKind Sniff(Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);

        if (!stream.CanSeek)
        {
            ThrowHelper.ThrowArgumentException(@"Palette import requires a seekable stream.", nameof(stream));
        }

        var position = stream.Position;
        try
        {
            var header = new byte[4];
            var read = ReadFully(stream, header, 0, header.Length);
            if (read >= 4 && header[0] == MagicBytes[0] && header[1] == MagicBytes[1]
                && header[2] == MagicBytes[2] && header[3] == MagicBytes[3])
            {
                return SniffedKind.Container;
            }

            return LooksLikeXml(header, read) ? SniffedKind.Xml : SniffedKind.Unknown;
        }
        finally
        {
            stream.Position = position;
        }
    }

    internal static void Import(KryptonCustomPaletteBase palette, Stream stream) =>
        Import(palette, stream, themeName: null);

    internal static void Import(KryptonCustomPaletteBase palette, Stream stream, string? themeName)
    {
        ThrowHelper.ThrowIfNull(palette);
        ThrowHelper.ThrowIfNull(stream);

        var owned = false;
        if (!stream.CanSeek)
        {
            stream = CopyRemaining(stream);
            owned = true;
        }

        try
        {
            var sniffed = Sniff(stream);
            switch (sniffed)
            {
                case SniffedKind.Xml:
                    ImportXml(palette, stream, themeName);
                    break;
                case SniffedKind.Container:
                    ImportContainer(palette, stream, themeName);
                    break;
                default:
                    ThrowHelper.ThrowArgumentException(@"Unrecognised palette file. Expected XML or a KPLT container.", nameof(stream));
                    break;
            }
        }
        finally
        {
            if (owned)
            {
                stream.Dispose();
            }
        }
    }

    internal static string[] GetThemeNames(Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);

        var owned = false;
        if (!stream.CanSeek)
        {
            stream = CopyRemaining(stream);
            owned = true;
        }

        var position = stream.Position;
        try
        {
            var sniffed = Sniff(stream);
            switch (sniffed)
            {
                case SniffedKind.Xml:
                {
                    return new[] { ReadXmlPaletteName(stream) };
                }
                case SniffedKind.Container:
                {
                    var header = ReadContainerHeader(stream);
                    if (header.Kind == KindCollection)
                    {
                        var entries = ReadCollectionEntries(stream);
                        var names = new string[entries.Count];
                        for (var i = 0; i < entries.Count; i++)
                        {
                            names[i] = entries[i].Name;
                        }

                        return names;
                    }

                    return new[] { header.Name };
                }
                default:
                    ThrowHelper.ThrowArgumentException(@"Unrecognised palette file. Expected XML or a KPLT container.", nameof(stream));
                    return Array.Empty<string>();
            }
        }
        finally
        {
            if (owned)
            {
                stream.Dispose();
            }
            else
            {
                stream.Position = position;
            }
        }
    }

    internal static Image?[] GetCollectionThemeThumbnails(Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);

        var owned = false;
        if (!stream.CanSeek)
        {
            stream = CopyRemaining(stream);
            owned = true;
        }

        var position = stream.Position;
        try
        {
            if (Sniff(stream) != SniffedKind.Container)
            {
                return Array.Empty<Image?>();
            }

            var header = ReadContainerHeader(stream);
            if (header.Kind != KindCollection)
            {
                return Array.Empty<Image?>();
            }

            var entries = ReadCollectionEntries(stream);
            var names = new string[entries.Count];
            for (var i = 0; i < entries.Count; i++)
            {
                names[i] = entries[i].Name;
            }

            return AlignThumbnails(names, TryReadThumbnailCatalog(stream));
        }
        finally
        {
            if (owned)
            {
                stream.Dispose();
            }
            else
            {
                stream.Position = position;
            }
        }
    }

    private static void WriteThumbnailCatalog(BinaryWriter writer, IList<KryptonCustomPaletteBase> palettes)
    {
        using var payload = new MemoryStream();
        using (var payloadWriter = new BinaryWriter(payload, Encoding.UTF8, leaveOpen: true))
        {
            var countPosition = payload.Position;
            payloadWriter.Write((ushort)0);
            ushort count = 0;
            for (var i = 0; i < palettes.Count; i++)
            {
                var palette = palettes[i];
                if (palette?.Thumbnail == null)
                {
                    continue;
                }

                Bitmap? owned = null;
                var bitmap = palette.Thumbnail as Bitmap;
                if (bitmap == null)
                {
                    owned = new Bitmap(palette.Thumbnail);
                    bitmap = owned;
                }

                try
                {
                    var name = palette.GetPaletteName()?.Trim() ?? string.Empty;
                    var nameBytes = Encoding.UTF8.GetBytes(name);
                    if (nameBytes.Length > ushort.MaxValue)
                    {
                        continue;
                    }

                    var png = EncodePng(bitmap);
                    if (png.Length == 0 || png.Length > MaxThumbnailPngBytes)
                    {
                        continue;
                    }

                    payloadWriter.Write((ushort)nameBytes.Length);
                    payloadWriter.Write(nameBytes);
                    payloadWriter.Write((ushort)Math.Min(bitmap.Width, ushort.MaxValue));
                    payloadWriter.Write((ushort)Math.Min(bitmap.Height, ushort.MaxValue));
                    payloadWriter.Write(png.Length);
                    payloadWriter.Write(png);
                    count++;
                }
                finally
                {
                    owned?.Dispose();
                }
            }

            if (count == 0)
            {
                return;
            }

            payloadWriter.Flush();
            payload.Position = countPosition;
            payloadWriter.Write(count);
            payloadWriter.Flush();
        }

        var bytes = payload.ToArray();
        writer.Write(ThumbnailCatalogMagicBytes);
        writer.Write(ThumbnailCatalogVersion);
        writer.Write(bytes.Length);
        writer.Write(bytes);
    }

    private static Dictionary<string, Image>? TryReadThumbnailCatalog(Stream stream)
    {
        if (stream.Length - stream.Position < 10)
        {
            return null;
        }

        var magic = new byte[4];
        var read = stream.Read(magic, 0, 4);
        if (read != 4 || magic[0] != ThumbnailCatalogMagicBytes[0]
            || magic[1] != ThumbnailCatalogMagicBytes[1]
            || magic[2] != ThumbnailCatalogMagicBytes[2]
            || magic[3] != ThumbnailCatalogMagicBytes[3])
        {
            if (read > 0 && stream.CanSeek)
            {
                stream.Position -= read;
            }

            return null;
        }

        using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
        var version = reader.ReadUInt16();
        var byteLength = reader.ReadInt32();
        if (byteLength < 0 || byteLength > stream.Length - stream.Position)
        {
            return null;
        }

        var payload = byteLength == 0 ? Array.Empty<byte>() : reader.ReadBytes(byteLength);
        if (payload.Length != byteLength)
        {
            return null;
        }

        if (version != ThumbnailCatalogVersion)
        {
            return null;
        }

        return ParseThumbnailCatalogPayload(payload);
    }

    private static Dictionary<string, Image>? ParseThumbnailCatalogPayload(byte[] payload)
    {
        using var memory = new MemoryStream(payload, writable: false);
        using var reader = new BinaryReader(memory, Encoding.UTF8, leaveOpen: true);
        var count = reader.ReadUInt16();
        var map = new Dictionary<string, Image>(count, StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < count; i++)
        {
            var nameLength = reader.ReadUInt16();
            var nameBytes = nameLength == 0 ? Array.Empty<byte>() : reader.ReadBytes(nameLength);
            if (nameBytes.Length != nameLength)
            {
                DisposeImages(map);
                return null;
            }

            var name = nameLength == 0 ? string.Empty : Encoding.UTF8.GetString(nameBytes);
            reader.ReadUInt16();
            reader.ReadUInt16();
            var pngLength = reader.ReadInt32();
            if (pngLength < 0 || pngLength > MaxThumbnailPngBytes)
            {
                DisposeImages(map);
                return null;
            }

            var png = pngLength == 0 ? Array.Empty<byte>() : reader.ReadBytes(pngLength);
            if (png.Length != pngLength)
            {
                DisposeImages(map);
                return null;
            }

            if (pngLength == 0 || string.IsNullOrEmpty(name) || map.ContainsKey(name))
            {
                continue;
            }

            map[name] = LoadPng(png);
        }

        return map;
    }

    private static Image?[] AlignThumbnails(string[] names, Dictionary<string, Image>? catalog)
    {
        var result = new Image?[names.Length];
        if (catalog == null)
        {
            return result;
        }

        var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < names.Length; i++)
        {
            if (catalog.TryGetValue(names[i], out var image))
            {
                result[i] = image;
                used.Add(names[i]);
            }
        }

        foreach (var pair in catalog)
        {
            if (!used.Contains(pair.Key))
            {
                pair.Value.Dispose();
            }
        }

        return result;
    }

    private static void DisposeImages(Dictionary<string, Image> map)
    {
        foreach (var pair in map)
        {
            pair.Value.Dispose();
        }

        map.Clear();
    }

    private const int MaxThumbnailPngBytes = 1024 * 1024;

    internal static bool IsCollection(Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);

        var owned = false;
        if (!stream.CanSeek)
        {
            stream = CopyRemaining(stream);
            owned = true;
        }

        var position = stream.Position;
        try
        {
            if (Sniff(stream) != SniffedKind.Container)
            {
                return false;
            }

            var header = ReadContainerHeader(stream);
            return header.Kind == KindCollection;
        }
        finally
        {
            if (owned)
            {
                stream.Dispose();
            }
            else
            {
                stream.Position = position;
            }
        }
    }

    internal static string GetCollectionDisplayName(Stream stream)
    {
        ThrowHelper.ThrowIfNull(stream);

        var owned = false;
        if (!stream.CanSeek)
        {
            stream = CopyRemaining(stream);
            owned = true;
        }

        var position = stream.Position;
        try
        {
            if (Sniff(stream) != SniffedKind.Container)
            {
                return string.Empty;
            }

            var header = ReadContainerHeader(stream);
            return header.Kind == KindCollection ? header.Name : string.Empty;
        }
        finally
        {
            if (owned)
            {
                stream.Dispose();
            }
            else
            {
                stream.Position = position;
            }
        }
    }

    internal static void ExportCollection(Stream stream, IList<KryptonCustomPaletteBase> palettes, bool ignoreDefaults, string collectionName)
    {
        ThrowHelper.ThrowIfNull(stream);
        ThrowHelper.ThrowIfNull(palettes);

        if (palettes.Count == 0)
        {
            ThrowHelper.ThrowArgumentException(@"A .ktheme collection requires at least one palette.", nameof(palettes));
        }

        if (palettes.Count > ushort.MaxValue)
        {
            ThrowHelper.ThrowArgumentException(@"A .ktheme collection cannot contain more than 65535 palettes.", nameof(palettes));
        }

        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var entries = new List<CollectionEntry>(palettes.Count);
        for (var i = 0; i < palettes.Count; i++)
        {
            var palette = palettes[i];
            ThrowHelper.ThrowIfNull(palette);
            var name = palette!.GetPaletteName();
            if (string.IsNullOrWhiteSpace(name))
            {
                ThrowHelper.ThrowArgumentException(@"Each packed palette must have a name (SetPaletteName).", nameof(palettes));
            }

            name = name!.Trim();
            if (Encoding.UTF8.GetByteCount(name) > ushort.MaxValue)
            {
                ThrowHelper.ThrowArgumentException(@"Palette name is too long to store in a KPLT container.", nameof(palettes));
            }

            if (!names.Add(name))
            {
                ThrowHelper.ThrowArgumentException($@"Duplicate palette name '{name}'.", nameof(palettes));
            }

            using (var payloadStream = new MemoryStream())
            {
                ExportNative(palette, payloadStream, ignoreDefaults);
                entries.Add(new CollectionEntry(name, KindNative, payloadStream.ToArray()));
            }
        }

        var collectionNameBytes = Encoding.UTF8.GetBytes(collectionName ?? string.Empty);
        if (collectionNameBytes.Length > ushort.MaxValue)
        {
            ThrowHelper.ThrowArgumentException(@"Collection name is too long to store in a KPLT container.", nameof(collectionName));
        }

        using var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
        writer.Write(MagicBytes);
        writer.Write(CurrentContainerVersion);
        writer.Write(KindCollection);
        writer.Write(SharedStaticConstants.CURRENT_SUPPORTED_PALETTE_VERSION);
        writer.Write((ushort)collectionNameBytes.Length);
        writer.Write(collectionNameBytes);
        writer.Write((ushort)entries.Count);
        for (var i = 0; i < entries.Count; i++)
        {
            var entry = entries[i];
            var nameBytes = Encoding.UTF8.GetBytes(entry.Name);
            writer.Write((ushort)nameBytes.Length);
            writer.Write(nameBytes);
            writer.Write(entry.Kind);
            writer.Write(entry.Payload.Length);
            writer.Write(entry.Payload);
        }

        WriteThumbnailCatalog(writer, palettes);
        writer.Flush();
    }

    internal static void Export(KryptonCustomPaletteBase palette, Stream stream, KryptonPaletteFileFormat format, bool ignoreDefaults)
    {
        ThrowHelper.ThrowIfNull(palette);
        ThrowHelper.ThrowIfNull(stream);

        switch (format)
        {
            case KryptonPaletteFileFormat.Xml:
                ExportXml(palette, stream, ignoreDefaults);
                break;
            case KryptonPaletteFileFormat.PaletteCompressedXml:
                ExportContainer(palette, stream, KindCompressedXml, ignoreDefaults);
                break;
            case KryptonPaletteFileFormat.PaletteBinary:
                ExportContainer(palette, stream, KindNative, ignoreDefaults);
                break;
            default:
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(format), format, @"Unknown palette file format.");
                break;
        }
    }

    /// <summary>
    /// Copies XML that PaletteUpgradeTool / XSLT can consume.
    /// Returns <see langword="true"/> only when <paramref name="xmlStream"/> is a usable XML copy;
    /// <see langword="false"/> for native payloads, unknown data, or when no XML could be produced.
    /// </summary>
    internal static bool TryCopyXmlForUpgrade(Stream stream, [NotNullWhen(true)] out MemoryStream? xmlStream)
    {
        xmlStream = null;
        ThrowHelper.ThrowIfNull(stream);

        var owned = false;
        if (!stream.CanSeek)
        {
            stream = CopyRemaining(stream);
            owned = true;
        }

        try
        {
            MemoryStream? xml = null;
            var sniffed = Sniff(stream);
            switch (sniffed)
            {
                case SniffedKind.Xml:
                {
                    xml = CopyRemaining(stream);
                    break;
                }
                case SniffedKind.Container:
                {
                    var header = ReadContainerHeader(stream);
                    if (header.Kind != KindCompressedXml)
                    {
                        return false;
                    }

                    xml = InflateToMemory(stream);
                    break;
                }
                default:
                    return false;
            }

            if (xml == null)
            {
                return false;
            }

            xmlStream = xml;
            return true;
        }
        finally
        {
            if (owned)
            {
                stream.Dispose();
            }
        }
    }

    /// <summary>
    /// Reads the palette schema version from XML or a KPLT header without consuming the stream.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> when a version could be read; <see langword="false"/> for
    /// unrecognised data or when the stream is not seekable.
    /// </returns>
    internal static bool TryGetSchemaVersion(Stream stream, out int schemaVersion)
    {
        schemaVersion = 0;
        ThrowHelper.ThrowIfNull(stream);

        if (!stream.CanSeek)
        {
            return false;
        }

        var position = stream.Position;
        try
        {
            switch (Sniff(stream))
            {
                case SniffedKind.Xml:
                    schemaVersion = ReadXmlSchemaVersionWithoutRewind(stream);
                    return schemaVersion > 0;
                case SniffedKind.Container:
                    schemaVersion = ReadContainerHeader(stream).SchemaVersion;
                    return true;
                default:
                    return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            stream.Position = position;
        }
    }

    private static void ImportXml(KryptonCustomPaletteBase palette, Stream stream) =>
        ImportXml(palette, stream, themeName: null);

    private static void ImportXml(KryptonCustomPaletteBase palette, Stream stream, string? themeName)
    {
        EnsureSingleThemeName(themeName, ReadXmlPaletteNameWithoutRewind(stream), allowUnnamed: true, nameof(stream));
        var doc = new XmlDocument();
        doc.Load(stream);
        palette.ImportFromXmlDocument(doc);
    }

    private static string ReadXmlPaletteName(Stream stream)
    {
        var position = stream.Position;
        try
        {
            return ReadXmlPaletteNameWithoutRewind(stream);
        }
        finally
        {
            stream.Position = position;
        }
    }

    private static string ReadXmlPaletteNameWithoutRewind(Stream stream)
    {
        var position = stream.Position;
        try
        {
            var doc = new XmlDocument();
            doc.Load(stream);
            var root = doc.SelectSingleNode(@"KryptonPalette") as XmlElement;
            return root?.GetAttribute(@"Name") ?? string.Empty;
        }
        finally
        {
            stream.Position = position;
        }
    }

    private static int ReadXmlSchemaVersionWithoutRewind(Stream stream)
    {
        var position = stream.Position;
        try
        {
            var doc = new XmlDocument();
            doc.Load(stream);
            var root = doc.SelectSingleNode(@"KryptonPalette") as XmlElement;
            if (root == null || !root.HasAttribute(@"Version"))
            {
                return 0;
            }

            return int.TryParse(root.GetAttribute(@"Version"), NumberStyles.Integer, CultureInfo.InvariantCulture,
                out var version)
                ? version
                : 0;
        }
        finally
        {
            stream.Position = position;
        }
    }

    private static void ExportXml(KryptonCustomPaletteBase palette, Stream stream, bool ignoreDefaults)
    {
        var doc = palette.ExportToXmlDocument(ignoreDefaults);
        doc.Save(stream);
    }

    private static void ImportContainer(KryptonCustomPaletteBase palette, Stream stream)
        => ImportContainer(palette, stream, themeName: null);

    private static void ImportContainer(KryptonCustomPaletteBase palette, Stream stream, string? themeName)
    {
        var header = ReadContainerHeader(stream);
        if (header.ContainerVersion < 1)
        {
            ThrowHelper.ThrowArgumentException($@"Palette container version '{header.ContainerVersion}' is not supported.", nameof(stream));
        }

        if (header.ContainerVersion > CurrentContainerVersion)
        {
            ThrowHelper.ThrowArgumentException(
                $@"Palette container version '{header.ContainerVersion}' is newer than this toolkit ({CurrentContainerVersion}).",
                nameof(stream));
        }

        switch (header.Kind)
        {
            case KindCompressedXml:
                EnsureSingleThemeName(themeName, header.Name, allowUnnamed: true, nameof(stream));
                if (!string.IsNullOrWhiteSpace(header.Name))
                {
                    palette.SetPaletteName(header.Name);
                }

                using (var xmlStream = InflateToMemory(stream))
                {
                    xmlStream.Position = 0;
                    ImportXml(palette, xmlStream);
                }
                break;
            case KindNative:
                EnsureSingleThemeName(themeName, header.Name, allowUnnamed: true, nameof(stream));
                if (!string.IsNullOrWhiteSpace(header.Name))
                {
                    palette.SetPaletteName(header.Name);
                }

                ImportNative(palette, stream, header.SchemaVersion);
                break;
            case KindCollection:
                ImportCollection(palette, stream, themeName, header.SchemaVersion);
                break;
            default:
                ThrowHelper.ThrowArgumentException($@"Unknown palette payload kind '{header.Kind}'.", nameof(stream));
                break;
        }
    }

    private static void ImportCollection(KryptonCustomPaletteBase palette, Stream stream, string? themeName, int schemaVersion)
    {
        var entries = ReadCollectionEntries(stream);
        if (entries.Count == 0)
        {
            ThrowHelper.ThrowArgumentException(@"The .ktheme collection does not contain any themes.", nameof(stream));
        }

        CollectionEntry selected;
        if (string.IsNullOrWhiteSpace(themeName))
        {
            if (entries.Count > 1)
            {
                ThrowHelper.ThrowArgumentException(
                    $@"This .ktheme file contains multiple themes ({FormatThemeNames(entries)}). Pass a theme name to Import.",
                    nameof(themeName));
            }

            selected = entries[0];
        }
        else
        {
            selected = default;
            var found = false;
            for (var i = 0; i < entries.Count; i++)
            {
                if (string.Equals(entries[i].Name, themeName, StringComparison.OrdinalIgnoreCase))
                {
                    selected = entries[i];
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                ThrowHelper.ThrowArgumentException(
                    $@"Theme '{themeName}' was not found in this .ktheme collection. Available: {FormatThemeNames(entries)}.",
                    nameof(themeName));
            }
        }

        ImportCollectionPayload(palette, selected, schemaVersion);
    }

    private static void ImportCollectionPayload(KryptonCustomPaletteBase palette, CollectionEntry entry, int schemaVersion)
    {
        if (!string.IsNullOrWhiteSpace(entry.Name))
        {
            palette.SetPaletteName(entry.Name);
        }

        using var payload = new MemoryStream(entry.Payload, writable: false);
        switch (entry.Kind)
        {
            case KindCompressedXml:
                using (var xmlStream = InflateToMemory(payload))
                {
                    xmlStream.Position = 0;
                    ImportXml(palette, xmlStream);
                }
                break;
            case KindNative:
                ImportNative(palette, payload, schemaVersion);
                break;
            default:
                ThrowHelper.ThrowArgumentException($@"Unknown palette payload kind '{entry.Kind}'.", nameof(entry));
                break;
        }
    }

    private static List<CollectionEntry> ReadCollectionEntries(Stream stream)
    {
        using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
        var count = reader.ReadUInt16();
        var entries = new List<CollectionEntry>(count);
        for (var i = 0; i < count; i++)
        {
            var nameLength = reader.ReadUInt16();
            var nameBytes = nameLength == 0 ? Array.Empty<byte>() : reader.ReadBytes(nameLength);
            if (nameBytes.Length != nameLength)
            {
                ThrowHelper.ThrowArgumentException(@"Palette collection entry name is truncated.", nameof(stream));
            }

            var name = nameLength == 0 ? string.Empty : Encoding.UTF8.GetString(nameBytes);
            var kind = reader.ReadUInt16();
            var payloadLength = reader.ReadInt32();
            if (payloadLength < 0)
            {
                ThrowHelper.ThrowArgumentException(@"Palette collection entry payload length is invalid.", nameof(stream));
            }

            var payload = payloadLength == 0 ? Array.Empty<byte>() : reader.ReadBytes(payloadLength);
            if (payload.Length != payloadLength)
            {
                ThrowHelper.ThrowArgumentException(@"Palette collection entry payload is truncated.", nameof(stream));
            }

            entries.Add(new CollectionEntry(name, kind, payload));
        }

        return entries;
    }

    private static void EnsureSingleThemeName(string? requestedName, string storedName, bool allowUnnamed, string paramName)
    {
        if (string.IsNullOrWhiteSpace(requestedName) || string.IsNullOrWhiteSpace(storedName))
        {
            if (!allowUnnamed && string.IsNullOrWhiteSpace(storedName) && !string.IsNullOrWhiteSpace(requestedName))
            {
                ThrowHelper.ThrowArgumentException($@"Theme '{requestedName}' was not found.", paramName);
            }

            return;
        }

        if (!string.Equals(requestedName, storedName, StringComparison.OrdinalIgnoreCase))
        {
            ThrowHelper.ThrowArgumentException(
                $@"Theme '{requestedName}' was not found. This file contains '{storedName}'.",
                paramName);
        }
    }

    private static string FormatThemeNames(List<CollectionEntry> entries)
    {
        var names = new string[entries.Count];
        for (var i = 0; i < entries.Count; i++)
        {
            names[i] = entries[i].Name;
        }

        return string.Join(@", ", names);
    }

    private readonly struct CollectionEntry
    {
        internal string Name { get; }
        internal ushort Kind { get; }
        internal byte[] Payload { get; }

        internal CollectionEntry(string name, ushort kind, byte[] payload)
        {
            Name = name;
            Kind = kind;
            Payload = payload;
        }
    }

    private static void ExportContainer(KryptonCustomPaletteBase palette, Stream stream, ushort kind, bool ignoreDefaults)
    {
        byte[] payload;
        switch (kind)
        {
            case KindCompressedXml:
                using (var xmlMs = new MemoryStream())
                {
                    ExportXml(palette, xmlMs, ignoreDefaults);
                    payload = Deflate(xmlMs.ToArray());
                }
                break;
            case KindNative:
                using (var nativeMs = new MemoryStream())
                {
                    ExportNative(palette, nativeMs, ignoreDefaults);
                    payload = nativeMs.ToArray();
                }
                break;
            default:
                ThrowHelper.ThrowArgumentException($@"Unknown palette payload kind '{kind}'.", nameof(kind));
                return;
        }

        var name = palette.GetPaletteName() ?? string.Empty;
        var nameBytes = Encoding.UTF8.GetBytes(name);
        if (nameBytes.Length > ushort.MaxValue)
        {
            ThrowHelper.ThrowArgumentException(@"Palette name is too long to store in a KPLT container.", nameof(palette));
        }

        using var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
        writer.Write(MagicBytes);
        writer.Write(CurrentContainerVersion);
        writer.Write(kind);
        writer.Write(SharedStaticConstants.CURRENT_SUPPORTED_PALETTE_VERSION);
        writer.Write((ushort)nameBytes.Length);
        writer.Write(nameBytes);
        writer.Write(payload);
        writer.Flush();
    }

    private readonly struct ContainerHeader
    {
        internal ushort ContainerVersion { get; }
        internal ushort Kind { get; }
        internal int SchemaVersion { get; }
        internal string Name { get; }

        internal ContainerHeader(ushort containerVersion, ushort kind, int schemaVersion, string name)
        {
            ContainerVersion = containerVersion;
            Kind = kind;
            SchemaVersion = schemaVersion;
            Name = name;
        }
    }

    private static ContainerHeader ReadContainerHeader(Stream stream)
    {
        using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
        var magic = reader.ReadBytes(4);
        if (magic.Length != 4
            || magic[0] != MagicBytes[0]
            || magic[1] != MagicBytes[1]
            || magic[2] != MagicBytes[2]
            || magic[3] != MagicBytes[3])
        {
            ThrowHelper.ThrowArgumentException(@"Palette container magic is missing or invalid.", nameof(stream));
        }

        var containerVersion = reader.ReadUInt16();
        var kind = reader.ReadUInt16();
        var schemaVersion = reader.ReadInt32();
        var nameLength = reader.ReadUInt16();
        var nameBytes = nameLength == 0 ? Array.Empty<byte>() : reader.ReadBytes(nameLength);
        if (nameBytes.Length != nameLength)
        {
            ThrowHelper.ThrowArgumentException(@"Palette container name is truncated.", nameof(stream));
        }

        var name = nameLength == 0 ? string.Empty : Encoding.UTF8.GetString(nameBytes);
        return new ContainerHeader(containerVersion, kind, schemaVersion, name);
    }

    private static void ImportNative(KryptonCustomPaletteBase palette, Stream stream, int schemaVersion)
    {
        if (schemaVersion < SharedStaticConstants.CURRENT_SUPPORTED_PALETTE_VERSION)
        {
            ThrowHelper.ThrowArgumentException(
                $@"Version '{schemaVersion}' number is incompatible, only version {SharedStaticConstants.CURRENT_SUPPORTED_PALETTE_VERSION} or above can be imported.\nUse the PaletteUpgradeTool from the Application tab of the KryptonExplorer to upgrade.",
                nameof(stream));
        }

        using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
        var imageCount = reader.ReadInt32();
        if (imageCount < 0)
        {
            ThrowHelper.ThrowArgumentException(@"Palette image table count is invalid.", nameof(stream));
        }

        var imageCache = new Dictionary<string, Bitmap>(imageCount, StringComparer.Ordinal);
        for (var i = 0; i < imageCount; i++)
        {
            var name = reader.ReadString();
            var length = reader.ReadInt32();
            if (length < 0)
            {
                ThrowHelper.ThrowArgumentException(@"Palette image blob length is invalid.", nameof(stream));
            }

            var png = reader.ReadBytes(length);
            if (png.Length != length)
            {
                ThrowHelper.ThrowArgumentException(@"Palette image blob is truncated.", nameof(stream));
            }

            if (string.IsNullOrEmpty(name))
            {
                continue;
            }

            imageCache[name] = LoadPng(png);
        }

        ImportObject(reader, imageCache, palette);
    }

    private static void ExportNative(KryptonCustomPaletteBase palette, Stream stream, bool ignoreDefaults)
    {
        var imageCache = new Dictionary<Bitmap, string>();
        using var propertiesMs = new MemoryStream();
        using (var propertiesWriter = new BinaryWriter(propertiesMs, Encoding.UTF8, leaveOpen: true))
        {
            ExportObject(propertiesWriter, imageCache, palette, ignoreDefaults);
            propertiesWriter.Write(RecordEnd);
            propertiesWriter.Flush();
        }

        using var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
        writer.Write(imageCache.Count);
        foreach (var entry in imageCache)
        {
            var png = EncodePng(entry.Key);
            writer.Write(entry.Value);
            writer.Write(png.Length);
            writer.Write(png);
        }

        writer.Write(propertiesMs.ToArray());
        writer.Flush();
    }

    private static void ImportObject(BinaryReader reader, Dictionary<string, Bitmap> imageCache, object? obj)
    {
        if (obj == null)
        {
            SkipObject(reader);
            return;
        }

        var persistMap = BuildPersistMap(obj.GetType());
        while (true)
        {
            var kind = reader.ReadByte();
            switch (kind)
            {
                case RecordEnd:
                    return;
                case RecordNavigate:
                {
                    var name = reader.ReadString();
                    if (persistMap.TryGetValue(name, out var persist)
                        && persist.Attribute.Navigate
                        && persist.Property.CanRead)
                    {
                        ImportObject(reader, imageCache, persist.Property.GetValue(obj, null));
                    }
                    else
                    {
                        SkipObject(reader);
                    }

                    break;
                }
                case RecordValue:
                {
                    var name = reader.ReadString();
                    var valueType = reader.ReadString();
                    var valueValue = reader.ReadString();
                    if (!persistMap.TryGetValue(name, out var persist) || persist.Attribute.Navigate)
                    {
                        break;
                    }

                    ApplyLeafValue(persist.Property, obj, imageCache, valueType, valueValue);
                    break;
                }
                default:
                    ThrowHelper.ThrowArgumentException($@"Unknown palette persist record '{kind}'.", nameof(reader));
                    return;
            }
        }
    }

    private static void SkipObject(BinaryReader reader)
    {
        while (true)
        {
            var kind = reader.ReadByte();
            switch (kind)
            {
                case RecordEnd:
                    return;
                case RecordNavigate:
                    reader.ReadString();
                    SkipObject(reader);
                    break;
                case RecordValue:
                    reader.ReadString();
                    reader.ReadString();
                    reader.ReadString();
                    break;
                default:
                    ThrowHelper.ThrowArgumentException($@"Unknown palette persist record '{kind}'.", nameof(reader));
                    return;
            }
        }
    }

    private static void ApplyLeafValue(
        PropertyInfo prop,
        object obj,
        Dictionary<string, Bitmap> imageCache,
        string valueType,
        string valueValue)
    {
        if (prop.PropertyType == typeof(Image))
        {
            if (valueValue.Length == 0)
            {
                prop.SetValue(obj, null, null);
            }
            else
            {
                prop.SetValue(obj, imageCache.TryGetValue(valueValue, out var imageValue) ? imageValue : null, null);
            }

            return;
        }

        object? setValue = null;
        var resolvedType = KryptonCustomPaletteBase.StringToType(valueType);
        if (resolvedType != typeof(Font) || valueValue != "(none)")
        {
            var converter = TypeDescriptor.GetConverter(resolvedType);
            setValue = converter.ConvertFromInvariantString(valueValue);
        }

        prop.SetValue(obj, setValue, null);
    }

    private static void ExportObject(BinaryWriter writer, Dictionary<Bitmap, string> imageCache, object? obj, bool ignoreDefaults)
    {
        if (obj == null)
        {
            return;
        }

        var t = obj.GetType();
        foreach (var prop in t.GetProperties())
        {
            if (prop.Name == nameof(KryptonCustomPaletteBase.PaletteName)
                || prop.Name == @"CustomisedKryptonPaletteFilePath")
            {
                continue;
            }

            foreach (var attrib in prop.GetCustomAttributes(false))
            {
                if (attrib is not KryptonPersistAttribute persist)
                {
                    continue;
                }

                if (persist.Navigate)
                {
                    if (!prop.CanRead)
                    {
                        continue;
                    }

                    var childObj = prop.GetValue(obj, null);
                    if (ignoreDefaults && childObj != null)
                    {
                        var propertyIsDefault = TypeDescriptor.GetProperties(childObj)[nameof(KryptonCustomPaletteBase.IsDefault)];
                        if (propertyIsDefault != null
                            && propertyIsDefault.PropertyType == typeof(bool)
                            && (bool)propertyIsDefault.GetValue(childObj)!)
                        {
                            childObj = null;
                        }
                    }

                    if (childObj == null)
                    {
                        continue;
                    }

                    writer.Write(RecordNavigate);
                    writer.Write(prop.Name);
                    ExportObject(writer, imageCache, childObj, ignoreDefaults);
                    writer.Write(RecordEnd);
                }
                else
                {
                    if (ShouldIgnoreLeaf(t, prop, obj, ignoreDefaults, out var childObj))
                    {
                        continue;
                    }

                    writer.Write(RecordValue);
                    writer.Write(prop.Name);
                    writer.Write(KryptonCustomPaletteBase.TypeToString(prop.PropertyType));
                    writer.Write(EncodeLeafValue(prop, childObj, imageCache));
                }
            }
        }
    }

    private static bool ShouldIgnoreLeaf(Type t, PropertyInfo prop, object obj, bool ignoreDefaults, out object? childObj)
    {
        childObj = prop.GetValue(obj, null);
        if (!ignoreDefaults)
        {
            return false;
        }

        var shouldSerializeMethod = t.GetMethod($"ShouldSerialize{prop.Name}",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            binder: null,
            types: Type.EmptyTypes,
            modifiers: null);

        if (shouldSerializeMethod != null
            && shouldSerializeMethod.ReturnType == typeof(bool)
            && !(bool)shouldSerializeMethod.Invoke(obj, null)!)
        {
            return true;
        }

        var defaultAttribs = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false);
        if (defaultAttribs.Length < 1)
        {
            return false;
        }

        var defaultAttrib = (DefaultValueAttribute)defaultAttribs[0];
        return defaultAttrib.Value == null ? childObj == null : defaultAttrib.Value.Equals(childObj);
    }

    private static string EncodeLeafValue(PropertyInfo prop, object? childObj, Dictionary<Bitmap, string> imageCache)
    {
        if (prop.PropertyType == typeof(Image))
        {
            if (childObj == null)
            {
                return string.Empty;
            }

            if (childObj is not Bitmap image)
            {
                image = new Bitmap((Image)childObj);
            }

            if (imageCache.TryGetValue(image, out var existing))
            {
                return existing;
            }

            var imageName = $@"ImageCache{imageCache.Count + 1}";
            imageCache.Add(image, imageName);
            return imageName;
        }

        if (prop.PropertyType == typeof(Font))
        {
            if (childObj == null)
            {
                return @"(none)";
            }

            var cultureInfo = new CultureInfo("en-US");
            var converter = TypeDescriptor.GetConverter(prop.PropertyType);
            var converted = converter.ConvertTo(context: null, culture: cultureInfo, value: childObj, destinationType: typeof(string));
            return converted?.ToString() ?? string.Empty;
        }

        var defaultCulture = new CultureInfo("en-US");
        var defaultConverter = TypeDescriptor.GetConverter(prop.PropertyType);
        var defaultConverted = defaultConverter.ConvertTo(context: null, culture: defaultCulture, value: childObj!, destinationType: typeof(string));
        return defaultConverted?.ToString() ?? string.Empty;
    }

    private readonly struct PersistEntry
    {
        internal PropertyInfo Property { get; }
        internal KryptonPersistAttribute Attribute { get; }

        internal PersistEntry(PropertyInfo property, KryptonPersistAttribute attribute)
        {
            Property = property;
            Attribute = attribute;
        }
    }

    private static Dictionary<string, PersistEntry> BuildPersistMap(Type type)
    {
        var map = new Dictionary<string, PersistEntry>(StringComparer.Ordinal);
        foreach (var prop in type.GetProperties())
        {
            if (prop.Name == nameof(KryptonCustomPaletteBase.PaletteName)
                || prop.Name == @"CustomisedKryptonPaletteFilePath")
            {
                continue;
            }

            foreach (var attrib in prop.GetCustomAttributes(false))
            {
                if (attrib is KryptonPersistAttribute persist)
                {
                    map[prop.Name] = new PersistEntry(prop, persist);
                    break;
                }
            }
        }

        return map;
    }

    private static MemoryStream CopyRemaining(Stream stream)
    {
        var copy = new MemoryStream();
        stream.CopyTo(copy);
        copy.Position = 0;
        return copy;
    }

    private static byte[] Deflate(byte[] raw)
    {
        using var output = new MemoryStream();
        using (var deflate = new DeflateStream(output, CompressionMode.Compress, leaveOpen: true))
        {
            deflate.Write(raw, 0, raw.Length);
        }

        return output.ToArray();
    }

    private static MemoryStream InflateToMemory(Stream compressed)
    {
        var output = new MemoryStream();
        using (var deflate = new DeflateStream(compressed, CompressionMode.Decompress, leaveOpen: true))
        {
            deflate.CopyTo(output);
        }

        output.Position = 0;
        return output;
    }

    private static byte[] EncodePng(Bitmap image)
    {
        using var memory = new MemoryStream();
        image.Save(memory, ImageFormat.Png);
        return memory.ToArray();
    }

    private static Bitmap LoadPng(byte[] png)
    {
        using var memory = new MemoryStream(png);
        using var loaded = new Bitmap(memory);
        return new Bitmap(loaded);
    }

    private static bool LooksLikeXml(byte[] header, int length)
    {
        if (length <= 0)
        {
            return false;
        }

        var index = 0;
        if (length >= 3 && header[0] == 0xEF && header[1] == 0xBB && header[2] == 0xBF)
        {
            index = 3;
        }

        while (index < length)
        {
            var b = header[index];
            if (b == (byte)' ' || b == (byte)'\t' || b == (byte)'\r' || b == (byte)'\n')
            {
                index++;
                continue;
            }

            return b == (byte)'<';
        }

        return false;
    }

    private static int ReadFully(Stream stream, byte[] buffer, int offset, int count)
    {
        var total = 0;
        while (total < count)
        {
            var read = stream.Read(buffer, offset + total, count - total);
            if (read <= 0)
            {
                break;
            }

            total += read;
        }

        return total;
    }
}
