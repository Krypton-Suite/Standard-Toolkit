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
/// payload kind (uint16: 0 = Deflate XML, 1 = native persist), palette schema version (int32),
/// name length (uint16) + UTF-8 name, then payload bytes.
/// </remarks>
internal static class KryptonPaletteBinaryPersistence
{
    internal const ushort CurrentContainerVersion = 1;
    internal const ushort KindCompressedXml = 0;
    internal const ushort KindNative = 1;

    private const byte RecordEnd = 0;
    private const byte RecordNavigate = 1;
    private const byte RecordValue = 2;

    private static readonly byte[] MagicBytes = Encoding.ASCII.GetBytes(KryptonPaletteFile.ContainerMagic);

    internal enum SniffedKind
    {
        Xml,
        Container,
        Unknown
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

    internal static void Import(KryptonCustomPaletteBase palette, Stream stream)
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
                    ImportXml(palette, stream);
                    break;
                case SniffedKind.Container:
                    ImportContainer(palette, stream);
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
    /// Copies XML that PaletteUpgradeTool / XSLT can consume. Returns <see langword="false"/> for native payloads.
    /// </summary>
    internal static bool TryCopyXmlForUpgrade(Stream stream, out MemoryStream? xmlStream)
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
            var sniffed = Sniff(stream);
            switch (sniffed)
            {
                case SniffedKind.Xml:
                    xmlStream = CopyRemaining(stream);
                    return true;
                case SniffedKind.Container:
                    var header = ReadContainerHeader(stream);
                    if (header.Kind != KindCompressedXml)
                    {
                        return false;
                    }

                    xmlStream = InflateToMemory(stream);
                    return true;
                default:
                    return false;
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

    private static void ImportXml(KryptonCustomPaletteBase palette, Stream stream)
    {
        var doc = new XmlDocument();
        doc.Load(stream);
        palette.ImportFromXmlDocument(doc);
    }

    private static void ExportXml(KryptonCustomPaletteBase palette, Stream stream, bool ignoreDefaults)
    {
        var doc = palette.ExportToXmlDocument(ignoreDefaults);
        doc.Save(stream);
    }

    private static void ImportContainer(KryptonCustomPaletteBase palette, Stream stream)
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

        if (!string.IsNullOrWhiteSpace(header.Name))
        {
            palette.SetPaletteName(header.Name);
        }

        switch (header.Kind)
        {
            case KindCompressedXml:
                using (var xmlStream = InflateToMemory(stream))
                {
                    xmlStream.Position = 0;
                    ImportXml(palette, xmlStream);
                }
                break;
            case KindNative:
                ImportNative(palette, stream, header.SchemaVersion);
                break;
            default:
                ThrowHelper.ThrowArgumentException($@"Unknown palette payload kind '{header.Kind}'.", nameof(stream));
                break;
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
