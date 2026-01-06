#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Helper class for working with Font Awesome icons. Supports both font-based rendering and image caching.
/// </summary>
public static class FontAwesomeHelper
{
    #region Static Fields

    private sealed class FontCacheEntry
    {
        public FontFamily FontFamily { get; }
        public PrivateFontCollection? PrivateFontCollection { get; }
        public IntPtr MemoryPtr { get; }
        public int MemorySize { get; }

        public FontCacheEntry(FontFamily fontFamily, PrivateFontCollection? privateFontCollection, IntPtr memoryPtr, int memorySize)
        {
            FontFamily = fontFamily;
            PrivateFontCollection = privateFontCollection;
            MemoryPtr = memoryPtr;
            MemorySize = memorySize;
        }

        public void Dispose()
        {
            PrivateFontCollection?.Dispose();
            if (MemoryPtr != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(MemoryPtr);
            }
        }
    }

    private static readonly ConcurrentDictionary<string, FontCacheEntry> _fontCache = new();
    private static readonly ConcurrentDictionary<string, Bitmap> _imageCache = new();
    private static readonly object _lockObject = new();

    // Font Awesome Unicode ranges (approximate - actual ranges vary by version)
    // Font Awesome 6 Free: Solid starts around U+F000, Regular around U+F400, Brands around U+F200
    private const int SOLID_BASE = 0xF000;
    private const int REGULAR_BASE = 0xF400;
    private const int BRANDS_BASE = 0xF200;

    // Supported Font Awesome versions (newest first for priority)
    private static readonly int[] SupportedVersions = { 7, 6, 5 };

    // Legacy font family name (Font Awesome 4 and earlier)
    private static readonly string[] LegacyFontFamilyNames = { "FontAwesome" };

    // Common Font Awesome font family names (generated dynamically)
    private static readonly string[] FontFamilyNames = GenerateFontFamilyNames();

    // Font Awesome Pro style-specific font family names (generated dynamically)
    private static readonly Dictionary<FontAwesomeStyle, string[]> ProFontFamilyNames = GenerateProFontFamilyNames();

    private static string[] GenerateFontFamilyNames()
    {
        var names = new List<string>();
        foreach (var version in SupportedVersions)
        {
            names.Add($"Font Awesome {version} Free");
            names.Add($"Font Awesome {version} Pro");
        }
        names.AddRange(LegacyFontFamilyNames);
        return names.ToArray();
    }

    private static Dictionary<FontAwesomeStyle, string[]> GenerateProFontFamilyNames()
    {
        var result = new Dictionary<FontAwesomeStyle, List<string>>();

        foreach (var version in SupportedVersions)
        {
            // Solid style
            if (!result.ContainsKey(FontAwesomeStyle.Solid))
            {
                result[FontAwesomeStyle.Solid] = new List<string>();
            }
            result[FontAwesomeStyle.Solid].Add($"Font Awesome {version} Pro Solid");
            result[FontAwesomeStyle.Solid].Add($"Font Awesome {version} Free Solid");

            // Regular style
            if (!result.ContainsKey(FontAwesomeStyle.Regular))
            {
                result[FontAwesomeStyle.Regular] = new List<string>();
            }
            result[FontAwesomeStyle.Regular].Add($"Font Awesome {version} Pro Regular");
            result[FontAwesomeStyle.Regular].Add($"Font Awesome {version} Free Regular");

            // Brands style
            if (!result.ContainsKey(FontAwesomeStyle.Brands))
            {
                result[FontAwesomeStyle.Brands] = new List<string>();
            }
            result[FontAwesomeStyle.Brands].Add($"Font Awesome {version} Brands");

            // Pro-only styles (Light, Thin, Duotone)
            if (version >= 6)
            {
                if (!result.ContainsKey(FontAwesomeStyle.Light))
                {
                    result[FontAwesomeStyle.Light] = new List<string>();
                }
                result[FontAwesomeStyle.Light].Add($"Font Awesome {version} Pro Light");

                if (!result.ContainsKey(FontAwesomeStyle.Thin))
                {
                    result[FontAwesomeStyle.Thin] = new List<string>();
                }
                result[FontAwesomeStyle.Thin].Add($"Font Awesome {version} Pro Thin");

                if (!result.ContainsKey(FontAwesomeStyle.Duotone))
                {
                    result[FontAwesomeStyle.Duotone] = new List<string>();
                }
                result[FontAwesomeStyle.Duotone].Add($"Font Awesome {version} Pro Duotone");
            }
        }

        return result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets or sets the path to the Font Awesome font file. If not set, attempts to find installed fonts.
    /// </summary>
    public static string? FontFilePath { get; set; }

    /// <summary>
    /// Gets or sets the embedded resource name for the Font Awesome font file.
    /// Example: "MyAssembly.Resources.FontAwesome.otf"
    /// </summary>
    public static string? FontResourceName { get; set; }

    /// <summary>
    /// Gets or sets the assembly to load the font resource from.
    /// If null, uses the executing assembly.
    /// </summary>
    public static Assembly? FontResourceAssembly { get; set; }

    /// <summary>
    /// Gets or sets the default Font Awesome style to use.
    /// </summary>
    public static FontAwesomeStyle DefaultStyle { get; set; } = FontAwesomeStyle.Solid;

    private static int _defaultSize = 16;

    /// <summary>
    /// Gets or sets the default icon size in pixels.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than or equal to zero.</exception>
    public static int DefaultSize
    {
        get => _defaultSize;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "DefaultSize must be greater than zero.");
            }
            _defaultSize = value;
        }
    }

    /// <summary>
    /// Gets or sets the default icon color.
    /// </summary>
    public static Color DefaultColor { get; set; } = Color.Black;

    /// <summary>
    /// Clears the image and font cache.
    /// </summary>
    public static void ClearCache()
    {
        lock (_lockObject)
        {
            foreach (var bitmap in _imageCache.Values)
            {
                bitmap?.Dispose();
            }
            _imageCache.Clear();

            foreach (var fontEntry in _fontCache.Values)
            {
                fontEntry?.Dispose();
            }
            _fontCache.Clear();
        }
    }

    /// <summary>
    /// Renders a Font Awesome icon as a Bitmap image.
    /// </summary>
    /// <param name="iconName">The Font Awesome icon name (e.g., "home", "user", "cog").</param>
    /// <param name="size">The size of the icon in pixels.</param>
    /// <param name="color">The color of the icon.</param>
    /// <param name="style">The Font Awesome style to use.</param>
    /// <returns>A Bitmap containing the rendered icon, or null if the icon could not be rendered.</returns>
    public static Bitmap? RenderIcon(string iconName, int size = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        if (string.IsNullOrWhiteSpace(iconName))
        {
            return null;
        }

        size = size > 0 ? size : DefaultSize;
        if (size <= 0)
        {
            return null;
        }
        var iconColor = color ?? DefaultColor;
        var iconStyle = style ?? DefaultStyle;

        // Create cache key
        var cacheKey = $"{iconName}_{size}_{iconColor.ToArgb()}_{iconStyle}";

        // Check cache (synchronized to prevent race condition with ClearCache)
        lock (_lockObject)
        {
            if (_imageCache.TryGetValue(cacheKey, out var cachedBitmap))
            {
                return CloneBitmap(cachedBitmap);
            }
        }

        // Get Unicode character for icon
        var unicode = GetUnicodeForIcon(iconName, iconStyle);
        if (unicode == 0)
        {
            return null;
        }

        // Get font key and render icon
        // RenderIconInternal will load and validate the font within its lock to prevent
        // race condition where ClearCache() disposes the PrivateFontCollection
        // between loading the font and using it for rendering
        var fontKey = GetFontKey(iconStyle);
        var bitmap = RenderIconInternal(fontKey, unicode, size, iconColor);
        if (bitmap != null)
        {
            // Cache the bitmap (synchronized to prevent race condition with ClearCache)
            lock (_lockObject)
            {
                // Double-check pattern: another thread might have added it while we were rendering
                if (!_imageCache.TryGetValue(cacheKey, out _))
                {
                    var clonedBitmap = CloneBitmap(bitmap);
                    if (!_imageCache.TryAdd(cacheKey, clonedBitmap))
                    {
                        // Another thread added the key between TryGetValue and TryAdd
                        // Dispose the orphaned cloned bitmap to prevent resource leak
                        clonedBitmap?.Dispose();
                    }
                }
            }
        }

        return bitmap;
    }

    /// <summary>
    /// Renders a Font Awesome icon from the enum as a Bitmap image.
    /// </summary>
    /// <param name="icon">The Font Awesome icon enum value.</param>
    /// <param name="size">The size of the icon in pixels.</param>
    /// <param name="color">The color of the icon.</param>
    /// <param name="style">The Font Awesome style to use.</param>
    /// <returns>A Bitmap containing the rendered icon, or null if the icon could not be rendered.</returns>
    public static Bitmap? RenderIcon(FontAwesomeIcon icon, int size = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        return RenderIcon(icon.ToString().ToLowerInvariant(), size, color, style);
    }

    /// <summary>
    /// Checks if Font Awesome fonts are available on the system.
    /// </summary>
    /// <param name="style">The Font Awesome style to check.</param>
    /// <returns>True if the font is available, false otherwise.</returns>
    public static bool IsFontAvailable(FontAwesomeStyle style = FontAwesomeStyle.Solid)
    {
        return LoadFontAwesomeFont(style) != null;
    }

    /// <summary>
    /// Gets the Unicode character code for a Font Awesome icon.
    /// </summary>
    /// <param name="iconName">The icon name.</param>
    /// <param name="style">The Font Awesome style.</param>
    /// <returns>The Unicode character code, or 0 if not found.</returns>
    public static int GetUnicodeForIcon(string iconName, FontAwesomeStyle style = FontAwesomeStyle.Solid)
    {
        if (iconName == null)
        {
            return 0;
        }

        // First, try to load from icons.json metadata if available
        var metadataUnicode = FontAwesomeIconMetadataLoader.GetUnicodeForIcon(iconName, style);
        if (metadataUnicode > 0)
        {
            return metadataUnicode;
        }

        // Fallback to hardcoded mappings for common icons
        var unicode = GetIconUnicodeMapping(iconName, style);
        if (unicode > 0)
        {
            return unicode;
        }

        // Last resort: use hash-based approach (not accurate but works as fallback)
        // Use deterministic hash function to ensure consistent results across .NET versions
        // (StringComparer.GetHashCode is randomized in .NET Core+ for security)
        var iconHash = GetDeterministicHashCode(iconName);
        var baseOffset = style switch
        {
            FontAwesomeStyle.Solid => SOLID_BASE,
            FontAwesomeStyle.Regular => REGULAR_BASE,
            FontAwesomeStyle.Brands => BRANDS_BASE,
            FontAwesomeStyle.Light => SOLID_BASE + 0x1000,
            FontAwesomeStyle.Thin => SOLID_BASE + 0x2000,
            FontAwesomeStyle.Duotone => SOLID_BASE + 0x3000,
            _ => SOLID_BASE
        };

        return (int)(baseOffset + ((uint)iconHash % 0x1000));
    }

    #endregion

    #region Private Methods

    private static FontFamily? LoadFontAwesomeFont(FontAwesomeStyle style)
    {
        var fontKey = GetFontKey(style);

        // Synchronized cache read to prevent race condition with ClearCache
        lock (_lockObject)
        {
            if (_fontCache.TryGetValue(fontKey, out var cachedEntry))
            {
                return cachedEntry.FontFamily;
            }
        }

        FontFamily? fontFamily;

        // Try to load from embedded resource if specified
        if (!string.IsNullOrEmpty(FontResourceName))
        {
            IntPtr fontPtr = IntPtr.Zero;
            PrivateFontCollection? privateFontCollection = null;
            try
            {
                var assembly = FontResourceAssembly ?? Assembly.GetExecutingAssembly();
                using var stream = assembly.GetManifestResourceStream(FontResourceName);
                if (stream != null)
                {
                    var fontData = new byte[stream.Length];
                    var totalBytesRead = 0;
                    var bytesToRead = fontData.Length;

                    while (totalBytesRead < bytesToRead)
                    {
                        var bytesRead = stream.Read(fontData, totalBytesRead, bytesToRead - totalBytesRead);
                        if (bytesRead == 0)
                        {
                            break; // End of stream reached
                        }
                        totalBytesRead += bytesRead;
                    }

                    fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                    Marshal.Copy(fontData, 0, fontPtr, fontData.Length);

                    privateFontCollection = new PrivateFontCollection();
                    privateFontCollection.AddMemoryFont(fontPtr, fontData.Length);

                    if (privateFontCollection.Families.Length > 0)
                    {
                        fontFamily = privateFontCollection.Families[0];
                        var cacheEntry = new FontCacheEntry(fontFamily, privateFontCollection, fontPtr, fontData.Length);
                        // Synchronized cache write to prevent race condition with ClearCache
                        lock (_lockObject)
                        {
                            if (_fontCache.TryAdd(fontKey, cacheEntry))
                            {
                                privateFontCollection = null; // Ownership transferred to cache entry
                                fontPtr = IntPtr.Zero; // Ownership transferred to cache entry
                                return fontFamily;
                            }
                            else
                            {
                                // Another thread already added the font, use the cached one and clean up our resources
                                if (_fontCache.TryGetValue(fontKey, out var existingEntry))
                                {
                                    cacheEntry.Dispose();
                                    privateFontCollection = null; // Already disposed by cacheEntry
                                    fontPtr = IntPtr.Zero; // Already freed by cacheEntry
                                    return existingEntry.FontFamily;
                                }
                            }
                        }
                        cacheEntry.Dispose();
                        privateFontCollection = null; // Already disposed by cacheEntry
                        fontPtr = IntPtr.Zero; // Already freed by cacheEntry
                    }
                }
            }
            catch
            {
                // Fall through to other loading methods
            }
            finally
            {
                if (privateFontCollection != null)
                {
                    privateFontCollection.Dispose();
                }
                if (fontPtr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(fontPtr);
                }
            }
        }

        // Try to load from file path if specified
        if (!string.IsNullOrEmpty(FontFilePath) && File.Exists(FontFilePath))
        {
            PrivateFontCollection? privateFontCollection = null;
            try
            {
                privateFontCollection = new PrivateFontCollection();
                privateFontCollection.AddFontFile(FontFilePath);
                if (privateFontCollection.Families.Length > 0)
                {
                    fontFamily = privateFontCollection.Families[0];
                    var cacheEntry = new FontCacheEntry(fontFamily, privateFontCollection, IntPtr.Zero, 0);
                    // Synchronized cache write to prevent race condition with ClearCache
                    lock (_lockObject)
                    {
                        if (_fontCache.TryAdd(fontKey, cacheEntry))
                        {
                            privateFontCollection = null; // Ownership transferred to cache entry
                            return fontFamily;
                        }
                        else
                        {
                            // Another thread already added the font, use the cached one and clean up our resources
                            if (_fontCache.TryGetValue(fontKey, out var existingEntry))
                            {
                                cacheEntry.Dispose();
                                privateFontCollection = null; // Already disposed by cacheEntry
                                return existingEntry.FontFamily;
                            }
                        }
                    }
                    cacheEntry.Dispose();
                    privateFontCollection = null; // Already disposed by cacheEntry
                }
            }
            catch
            {
                // Fall through to system font lookup
            }
            finally
            {
                if (privateFontCollection != null)
                {
                    privateFontCollection.Dispose();
                }
            }
        }

        // Try to find installed Font Awesome font
        // First, try style-specific font names (for Pro fonts)
        if (ProFontFamilyNames.TryGetValue(style, out var styleSpecificNames))
        {
            using var installedFonts = new InstalledFontCollection();
            foreach (var familyName in styleSpecificNames)
            {
                foreach (var family in installedFonts.Families)
                {
                    if (family.Name.Equals(familyName, StringComparison.OrdinalIgnoreCase))
                    {
                        fontFamily = family;
                        var cacheEntry = new FontCacheEntry(family, null, IntPtr.Zero, 0);
                        // Synchronized cache write to prevent race condition with ClearCache
                        lock (_lockObject)
                        {
                            _fontCache.TryAdd(fontKey, cacheEntry);
                        }
                        return fontFamily;
                    }
                }
            }
        }

        // Fallback to generic font family names
        using (var installedFonts = new InstalledFontCollection())
        {
            foreach (var familyName in FontFamilyNames)
            {
                foreach (var family in installedFonts.Families)
                {
                    if (family.Name.Equals(familyName, StringComparison.OrdinalIgnoreCase))
                    {
                        fontFamily = family;
                        var cacheEntry = new FontCacheEntry(family, null, IntPtr.Zero, 0);
                        // Synchronized cache write to prevent race condition with ClearCache
                        lock (_lockObject)
                        {
                            _fontCache.TryAdd(fontKey, cacheEntry);
                        }
                        return fontFamily;
                    }
                }
            }
        }

        return null;
    }

    private static string GetFontKey(FontAwesomeStyle style)
    {
        return $"FontAwesome_{style}";
    }

    private static FontAwesomeStyle GetStyleFromFontKey(string fontKey)
    {
        // Font key format is "FontAwesome_{style}"
        var prefix = "FontAwesome_";
        if (fontKey.StartsWith(prefix, StringComparison.Ordinal))
        {
            var styleString = fontKey.Substring(prefix.Length);
            if (Enum.TryParse<FontAwesomeStyle>(styleString, out var style))
            {
                return style;
            }
        }
        return DefaultStyle;
    }

    private static Bitmap? RenderIconInternal(string fontKey, int unicode, int size, Color color)
    {
        Bitmap? bitmap = null;
        Font? font = null;
        try
        {
            // Load and validate the font within the lock to prevent race condition where
            // ClearCache() disposes the PrivateFontCollection between loading the font
            // and using it for rendering. This ensures the FontFamily remains valid
            // during Font creation and use.
            FontFamily? fontFamily = null;
            lock (_lockObject)
            {
                // Check cache first - if entry exists, use it directly
                if (_fontCache.TryGetValue(fontKey, out var cacheEntry))
                {
                    fontFamily = cacheEntry.FontFamily;
                }
            }

            // If font not in cache, load it outside the lock (LoadFontAwesomeFont manages its own locking)
            if (fontFamily == null)
            {
                fontFamily = LoadFontAwesomeFont(GetStyleFromFontKey(fontKey));
                if (fontFamily == null)
                {
                    return null;
                }
            }

            // Re-acquire lock to verify font is still valid and create Font object
            // This prevents race condition where ClearCache disposes PrivateFontCollection
            // between loading the font and using it for rendering
            lock (_lockObject)
            {
                // Verify the cache entry still exists and matches the FontFamily we're about to use
                // This catches the case where ClearCache disposed the font after we loaded it
                if (!_fontCache.TryGetValue(fontKey, out var cacheEntry) || cacheEntry.FontFamily != fontFamily)
                {
                    // Font was cleared or changed, reload it
                    fontFamily = LoadFontAwesomeFont(GetStyleFromFontKey(fontKey));
                    if (fontFamily == null)
                    {
                        return null;
                    }
                    // Double-check after reload
                    if (!_fontCache.TryGetValue(fontKey, out cacheEntry) || cacheEntry.FontFamily != fontFamily)
                    {
                        return null;
                    }
                }

                // Create font while holding the lock to ensure FontFamily remains valid
                // This prevents ClearCache from disposing the PrivateFontCollection
                // while we're creating the Font object
                font = new Font(fontFamily, size, FontStyle.Regular, GraphicsUnit.Pixel);
            }

            // Create a bitmap with padding for better rendering
            var padding = Math.Max(2, size / 8);
            var bitmapSize = size + (padding * 2);
            bitmap = new Bitmap(bitmapSize, bitmapSize, PixelFormat.Format32bppArgb);

            using (font)
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using var brush = new SolidBrush(color);

                // Convert Unicode to string
                var iconChar = char.ConvertFromUtf32(unicode);

                // Measure text
                var textSize = graphics.MeasureString(iconChar, font);
                var x = (bitmapSize - textSize.Width) / 2;
                var y = (bitmapSize - textSize.Height) / 2;

                // Draw icon
                graphics.DrawString(iconChar, font, brush, x, y);
            }

            return bitmap;
        }
        catch
        {
            bitmap?.Dispose();
            font?.Dispose();
            return null;
        }
    }

    private static Bitmap CloneBitmap(Bitmap source)
    {
        if (source == null)
        {
            return null!;
        }

        var clone = new Bitmap(source.Width, source.Height, source.PixelFormat);
        try
        {
            using (var graphics = Graphics.FromImage(clone))
            {
                graphics.DrawImage(source, 0, 0);
            }
            return clone;
        }
        catch
        {
            clone?.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Computes a deterministic hash code for a string that is consistent across .NET versions.
    /// This is necessary because StringComparer.GetHashCode uses randomized hash codes in .NET Core+
    /// for security (hash flooding protection), which would cause inconsistent icon rendering.
    /// </summary>
    /// <param name="value">The string to hash.</param>
    /// <returns>A deterministic hash code.</returns>
    private static int GetDeterministicHashCode(string value)
    {
        if (value == null)
        {
            return 0;
        }

        // Use case-insensitive comparison for icon names
        var normalized = value.ToUpperInvariant();
        
        // Simple deterministic hash algorithm (djb2 variant)
        // This produces the same hash for the same input across all .NET versions
        int hash = 5381;
        foreach (var c in normalized)
        {
            hash = ((hash << 5) + hash) + c; // hash * 33 + c
        }

        return hash;
    }

    private static int GetIconUnicodeMapping(string iconName, FontAwesomeStyle style)
    {
        // Font Awesome 6 Free Unicode mappings
        // Based on Font Awesome 6.5.1 Free icons
        // Solid style uses U+F000-U+F8FF range
        // Regular style uses U+F000-U+F8FF range (different glyphs)
        // Brands style uses U+F000-U+F8FF range
        // Note: Unicode values are identical across all styles (Light, Thin, Duotone use same Unicode as Solid)

        var normalizedName = iconName.ToLowerInvariant().Replace("-", "").Replace("_", "").Trim();

        // Pro styles (Light, Thin, Duotone) use the same Unicode values as Solid
        // Fall back to Solid mappings when Pro styles are requested
        if (style == FontAwesomeStyle.Light || style == FontAwesomeStyle.Thin || style == FontAwesomeStyle.Duotone)
        {
            return GetIconUnicodeMapping(iconName, FontAwesomeStyle.Solid);
        }

        // Font Awesome 6 Solid (fas) mappings
        if (style == FontAwesomeStyle.Solid)
        {
            return normalizedName switch
            {
                // Common UI icons
                "home" or "house" => 0xF015,
                "user" => 0xF007,
                "users" => 0xF0C0,
                "cog" or "settings" or "gear" => 0xF013,
                "search" => 0xF002,
                "close" or "times" or "xmark" => 0xF00D,
                "check" => 0xF00C,
                "plus" => 0xF067,
                "minus" => 0xF068,
                "edit" or "pencil" => 0xF303,
                "trash" or "trashcan" => 0xF2ED,
                "save" => 0xF0C7,
                "folder" => 0xF07B,
                "file" => 0xF15B,
                "image" or "photo" => 0xF03E,
                "download" => 0xF019,
                "upload" => 0xF093,
                "print" => 0xF02F,
                "copy" => 0xF0C5,
                "cut" or "scissors" => 0xF0C4,
                "paste" => 0xF0EA,
                "undo" => 0xF0E2,
                "redo" => 0xF01E,
                "refresh" or "sync" or "rotate" => 0xF021,
                "play" => 0xF04B,
                "pause" => 0xF04C,
                "stop" => 0xF04D,
                "forward" => 0xF04E,
                "backward" or "back" => 0xF04A,
                "next" or "stepforward" => 0xF051,
                "previous" or "stepbackward" => 0xF048,
                "first" or "fastbackward" => 0xF049,
                "last" or "fastforward" => 0xF050,
                "expand" => 0xF065,
                "collapse" or "compress" => 0xF066,
                "chevronup" => 0xF077,
                "chevrondown" => 0xF078,
                "chevronleft" => 0xF053,
                "chevronright" => 0xF054,
                "arrowup" => 0xF062,
                "arrowdown" => 0xF063,
                "arrowleft" => 0xF060,
                "arrowright" => 0xF061,
                "info" or "infocircle" => 0xF05A,
                "warning" or "exclamationtriangle" => 0xF071,
                "error" or "timescircle" or "xmarkcircle" => 0xF057,
                "question" or "questioncircle" => 0xF059,
                "star" => 0xF005,
                "heart" => 0xF004,
                "bookmark" => 0xF02E,
                "calendar" => 0xF133,
                "clock" => 0xF017,
                "envelope" => 0xF0E0,
                "phone" => 0xF095,
                "globe" => 0xF0AC,
                "link" => 0xF0C1,
                "unlink" => 0xF127,
                "lock" => 0xF023,
                "unlock" => 0xF09C,
                "eye" => 0xF06E,
                "eyeslash" => 0xF070,
                "filter" => 0xF0B0,
                "sort" => 0xF0DC,
                "sortup" or "sortasc" => 0xF0DE,
                "sortdown" or "sortdesc" => 0xF0DD,
                "list" => 0xF03A,
                "grid" or "th" => 0xF00A,
                "bars" or "menu" => 0xF0C9,
                "thlist" => 0xF00B,
                "alignleft" => 0xF036,
                "aligncenter" => 0xF037,
                "alignright" => 0xF038,
                "bold" => 0xF032,
                "italic" => 0xF033,
                "underline" => 0xF0CD,
                "code" => 0xF121,
                "terminal" => 0xF120,
                "database" => 0xF1C0,
                "server" => 0xF233,
                "cloud" => 0xF0C2,
                "wifi" => 0xF1EB,
                "bluetooth" => 0xF293,
                "batteryfull" => 0xF240,
                "batteryhalf" => 0xF242,
                "batteryempty" => 0xF244,
                "signal" => 0xF012,
                "volumeup" => 0xF028,
                "volumedown" => 0xF027,
                "volumemute" => 0xF6A9,
                "music" => 0xF001,
                "video" => 0xF03D,
                "camera" => 0xF030,
                "microphone" => 0xF130,
                "headphones" => 0xF025,
                "desktop" => 0xF390,
                "laptop" => 0xF109,
                "tablet" => 0xF3FA,
                "mobile" or "mobilephone" => 0xF3CD,
                "keyboard" => 0xF11C,
                "mouse" => 0xF8CC,
                "gamepad" => 0xF11B,
                "tv" => 0xF26C,
                "radio" => 0xF8D7,
                "lightbulb" => 0xF0EB,
                "flash" or "bolt" => 0xF0E7,
                "fire" => 0xF06D,
                "water" => 0xF773,
                "snowflake" => 0xF2DC,
                "sun" => 0xF185,
                "moon" => 0xF186,
                "cloudsun" => 0xF6C4,
                "cloudrain" => 0xF73D,
                "umbrella" => 0xF0E9,
                "shield" => 0xF3ED,
                "flag" => 0xF024,
                "trophy" => 0xF091,
                "medal" => 0xF5A2,
                "gift" => 0xF06B,
                "shoppingcart" => 0xF07A,
                "creditcard" => 0xF09D,
                "money" or "dollarsign" => 0xF155,
                "dollar" => 0xF155,
                "euro" => 0xF153,
                "pound" => 0xF154,
                "bitcoin" => 0xF15A,
                _ => 0
            };
        }

        // Font Awesome 6 Regular (far) mappings - many icons have different Unicode in Regular
        if (style == FontAwesomeStyle.Regular)
        {
            return normalizedName switch
            {
                "home" or "house" => 0xF2B0,
                "user" => 0xF2BD,
                "users" => 0xF0C0,
                "cog" or "settings" or "gear" => 0xF5B2,
                "search" => 0xF3A3,
                "close" or "times" or "xmark" => 0xF410,
                "check" => 0xF560,
                "star" => 0xF005,
                "heart" => 0xF004,
                "bookmark" => 0xF02E,
                "calendar" => 0xF133,
                "clock" => 0xF017,
                "envelope" => 0xF0E0,
                "eye" => 0xF06E,
                "eyeslash" => 0xF070,
                "folder" => 0xF07B,
                "file" => 0xF15B,
                "image" or "photo" => 0xF03E,
                "save" => 0xF0C7,
                "edit" or "pencil" => 0xF303,
                "trash" or "trashcan" => 0xF2ED,
                _ => 0
            };
        }

        // Font Awesome 6 Brands (fab) mappings
        if (style == FontAwesomeStyle.Brands)
        {
            return normalizedName switch
            {
                "paypal" => 0xF1ED,
                "amazon" => 0xF270,
                "apple" => 0xF179,
                "windows" => 0xF17A,
                "linux" => 0xF17C,
                "android" => 0xF17B,
                "chrome" => 0xF268,
                "firefox" => 0xF269,
                "edge" => 0xF282,
                "safari" => 0xF267,
                "github" => 0xF09B,
                "gitlab" => 0xF296,
                "bitbucket" => 0xF171,
                "stackoverflow" => 0xF16C,
                "twitter" => 0xF099,
                "facebook" => 0xF09A,
                "instagram" => 0xF16D,
                "linkedin" => 0xF08C,
                "youtube" => 0xF167,
                "twitch" => 0xF1E8,
                "discord" => 0xF392,
                "steam" => 0xF1B6,
                "spotify" => 0xF1BC,
                "reddit" => 0xF1A1,
                "pinterest" => 0xF0D2,
                "snapchat" => 0xF2AB,
                "tiktok" => 0xE07B,
                "whatsapp" => 0xF232,
                "telegram" => 0xF2C6,
                "skype" => 0xF17E,
                "zoom" => 0xF549,
                "microsoft" => 0xF3CA,
                "google" => 0xF1A0,
                "dropbox" => 0xF16B,
                "onedrive" => 0xF3AF,
                "googledrive" => 0xF3AA,
                _ => 0
            };
        }

        return 0;
    }

    #endregion
}
