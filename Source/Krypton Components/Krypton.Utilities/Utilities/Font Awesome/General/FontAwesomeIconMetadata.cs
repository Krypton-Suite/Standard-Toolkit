#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

#if NET8_0_OR_GREATER
using System.Text.Json;
#else
using System.Web.Script.Serialization;
#endif

namespace Krypton.Utilities;

/// <summary>
/// Represents icon metadata from Font Awesome icons.json file.
/// </summary>
public class FontAwesomeIconMetadata
{
    /// <summary>
    /// Gets or sets the icon name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the icon label.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the Unicode value as a string (e.g., "f000").
    /// </summary>
    public string? Unicode { get; set; }

    /// <summary>
    /// Gets or sets the styles in which this icon is available.
    /// </summary>
    public List<string?> Styles { get; set; } = new();

    /// <summary>
    /// Gets the Unicode value as an integer.
    /// </summary>
    public int UnicodeInt
    {
        get
        {
            if (string.IsNullOrEmpty(Unicode))
            {
                return 0;
            }

            return int.TryParse(Unicode, System.Globalization.NumberStyles.HexNumber, null, out var result)
                ? result
                : 0;
        }
    }
}

/// <summary>
/// Loads and manages Font Awesome icon metadata from icons.json file.
/// </summary>
public static class FontAwesomeIconMetadataLoader
{
    private static Dictionary<string, Dictionary<FontAwesomeStyle, FontAwesomeIconMetadata>>? _iconMetadata;
    private static readonly object _metadataLock = new();

    /// <summary>
    /// Gets or sets the path to the Font Awesome icons.json file.
    /// </summary>
    public static string? IconsJsonPath { get; set; }

    /// <summary>
    /// Gets or sets the embedded resource name for the icons.json file.
    /// Example: "MyAssembly.Resources.icons.json"
    /// </summary>
    public static string? IconsJsonResourceName { get; set; }

    /// <summary>
    /// Gets or sets the assembly to load the icons.json resource from.
    /// If null, uses the executing assembly.
    /// </summary>
    public static Assembly? IconsJsonResourceAssembly { get; set; }

    /// <summary>
    /// Gets whether icon metadata has been loaded.
    /// </summary>
    public static bool IsMetadataLoaded => _iconMetadata != null;

    /// <summary>
    /// Loads icon metadata from icons.json file.
    /// </summary>
    /// <returns>True if metadata was loaded successfully, false otherwise.</returns>
    public static bool LoadMetadata()
    {
        lock (_metadataLock)
        {
            if (_iconMetadata != null)
            {
                return true; // Already loaded
            }

            string? jsonContent = null;

            // Try to load from embedded resource if specified
            if (!string.IsNullOrEmpty(IconsJsonResourceName))
            {
                try
                {
                    var assembly = IconsJsonResourceAssembly ?? Assembly.GetExecutingAssembly();
                    using var stream = assembly.GetManifestResourceStream(IconsJsonResourceName);
                    if (stream != null)
                    {
                        using var reader = new StreamReader(stream);
                        jsonContent = reader.ReadToEnd();
                    }
                }
                catch
                {
                    // Fall through to file path loading
                }
            }

            // Try to load from file path if specified
            if (string.IsNullOrEmpty(jsonContent) && !string.IsNullOrEmpty(IconsJsonPath) && File.Exists(IconsJsonPath))
            {
                try
                {
                    jsonContent = File.ReadAllText(IconsJsonPath);
                }
                catch
                {
                    return false;
                }
            }

            if (string.IsNullOrEmpty(jsonContent))
            {
                return false;
            }

            try
            {
                // Parse JSON - Font Awesome icons.json is a dictionary where keys are icon names
#if NET8_0_OR_GREATER
                using var jsonDoc = JsonDocument.Parse(jsonContent);
                _iconMetadata = new Dictionary<string, Dictionary<FontAwesomeStyle, FontAwesomeIconMetadata>>(System.StringComparer.OrdinalIgnoreCase);

                foreach (var iconProperty in jsonDoc.RootElement.EnumerateObject())
                {
                    var iconName = iconProperty.Name;
                    var iconElement = iconProperty.Value;

                    // Get Unicode value
                    if (!iconElement.TryGetProperty("unicode", out var unicodeElement))
                    {
                        continue;
                    }

                    var unicode = unicodeElement.GetString();
                    if (string.IsNullOrEmpty(unicode))
                    {
                        continue;
                    }

                    // Get styles array
                    var styles = new List<string>();
                    if (iconElement.TryGetProperty("styles", out var stylesElement) && stylesElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var styleElement in stylesElement.EnumerateArray())
                        {
                            var style = styleElement.GetString();
                            if (!string.IsNullOrEmpty(style))
                            {
                                styles.Add(style);
                            }
                        }
                    }

                    // Get label
                    var label = iconElement.TryGetProperty("label", out var labelElement) 
                        ? labelElement.GetString() 
                        : null;

                    var metadata = new FontAwesomeIconMetadata
                    {
                        Name = iconName,
                        Label = label,
                        Unicode = unicode,
                        Styles = styles!
                    };

                    // Normalize icon name for dictionary key (remove hyphens/underscores to match enum naming)
                    var normalizedIconName = NormalizeIconName(iconName);

                    // Map Font Awesome style strings to our enum
                    foreach (var style in styles)
                    {
                        var faStyle = MapStyleStringToEnum(style);
                        if (faStyle.HasValue)
                        {
                            if (!_iconMetadata.ContainsKey(normalizedIconName))
                            {
                                _iconMetadata[normalizedIconName] = new Dictionary<FontAwesomeStyle, FontAwesomeIconMetadata>();
                            }

                            _iconMetadata[normalizedIconName][faStyle.Value] = metadata;
                        }
                    }
                }
#else
                var serializer = new JavaScriptSerializer();
                var iconsDict = serializer.Deserialize<Dictionary<string, object>>(jsonContent);
                if (iconsDict == null)
                {
                    return false;
                }

                _iconMetadata = new Dictionary<string, Dictionary<FontAwesomeStyle, FontAwesomeIconMetadata>>(System.StringComparer.OrdinalIgnoreCase);

                foreach (var iconEntry in iconsDict)
                {
                    var iconName = iconEntry.Key;
                    var iconData = iconEntry.Value as Dictionary<string, object>;
                    if (iconData == null)
                    {
                        continue;
                    }

                    // Get Unicode value
                    if (!iconData.TryGetValue("unicode", out var unicodeObj) || unicodeObj == null)
                    {
                        continue;
                    }

                    var unicode = unicodeObj.ToString();
                    if (string.IsNullOrEmpty(unicode))
                    {
                        continue;
                    }

                    // Get styles array
                    var styles = new List<string?>();
                    if (iconData.TryGetValue("styles", out var stylesObj) && stylesObj != null)
                    {
                        IEnumerable? stylesEnumerable = null;
                        if (stylesObj is object[] objectArray)
                        {
                            stylesEnumerable = objectArray;
                        }
                        else if (stylesObj is ArrayList arrayList)
                        {
                            stylesEnumerable = arrayList.Cast<object>();
                        }
                        else if (stylesObj is IEnumerable enumerable)
                        {
                            stylesEnumerable = enumerable;
                        }

                        if (stylesEnumerable != null)
                        {
                            foreach (var styleObj in stylesEnumerable)
                            {
                                var style = styleObj?.ToString();
                                if (!string.IsNullOrEmpty(style))
                                {
                                    styles.Add(style);
                                }
                            }
                        }
                    }

                    // Get label
                    var label = iconData.TryGetValue("label", out var labelObj)
                        ? labelObj?.ToString()
                        : null;

                    var metadata = new FontAwesomeIconMetadata
                    {
                        Name = iconName,
                        Label = label,
                        Unicode = unicode,
                        Styles = styles
                    };

                    // Normalize icon name for dictionary key (remove hyphens/underscores to match enum naming)
                    var normalizedIconName = NormalizeIconName(iconName);

                    // Map Font Awesome style strings to our enum
                    foreach (var style in styles)
                    {
                        var faStyle = MapStyleStringToEnum(style);
                        if (faStyle.HasValue)
                        {
                            if (!_iconMetadata.ContainsKey(normalizedIconName))
                            {
                                _iconMetadata[normalizedIconName] = new Dictionary<FontAwesomeStyle, FontAwesomeIconMetadata>();
                            }

                            _iconMetadata[normalizedIconName][faStyle.Value] = metadata;
                        }
                    }
                }
#endif

                return true;
            }
            catch
            {
                _iconMetadata = null;
                return false;
            }
        }
    }

    /// <summary>
    /// Gets the Unicode value for an icon name and style.
    /// </summary>
    /// <param name="iconName">The icon name.</param>
    /// <param name="style">The Font Awesome style.</param>
    /// <returns>The Unicode value, or 0 if not found.</returns>
    public static int GetUnicodeForIcon(string iconName, FontAwesomeStyle style)
    {
        if (iconName == null)
        {
            return 0;
        }

        if (_iconMetadata == null)
        {
            LoadMetadata();
        }

        var metadataDict = _iconMetadata;
        if (metadataDict == null)
        {
            return 0;
        }

        var normalizedIconName = NormalizeIconName(iconName);
        if (metadataDict.TryGetValue(normalizedIconName, out var styleDict) &&
            styleDict.TryGetValue(style, out var metadata))
        {
            return metadata.UnicodeInt;
        }

        return 0;
    }

    /// <summary>
    /// Gets all available icon names.
    /// </summary>
    /// <returns>A list of icon names.</returns>
    public static List<string> GetAvailableIconNames()
    {
        if (_iconMetadata == null)
        {
            LoadMetadata();
        }

        var metadata = _iconMetadata;
        return metadata?.Keys.ToList() ?? new List<string>();
    }

    /// <summary>
    /// Gets available styles for a specific icon.
    /// </summary>
    /// <param name="iconName">The icon name.</param>
    /// <returns>A list of available styles for the icon.</returns>
    public static List<FontAwesomeStyle> GetAvailableStyles(string iconName)
    {
        if (iconName == null)
        {
            return new List<FontAwesomeStyle>();
        }

        if (_iconMetadata == null)
        {
            LoadMetadata();
        }

        var metadata = _iconMetadata;
        if (metadata == null)
        {
            return new List<FontAwesomeStyle>();
        }

        var normalizedIconName = NormalizeIconName(iconName);
        if (metadata.TryGetValue(normalizedIconName, out var styleDict))
        {
            return styleDict.Keys.ToList();
        }

        return new List<FontAwesomeStyle>();
    }

    /// <summary>
    /// Clears the loaded metadata cache.
    /// </summary>
    public static void ClearMetadata()
    {
        lock (_metadataLock)
        {
            _iconMetadata = null;
        }
    }

    private static FontAwesomeStyle? MapStyleStringToEnum(string? style)
    {
        return style?.ToLowerInvariant() switch
        {
            "solid" => FontAwesomeStyle.Solid,
            "regular" => FontAwesomeStyle.Regular,
            "brands" => FontAwesomeStyle.Brands,
            "light" => FontAwesomeStyle.Light,
            "thin" => FontAwesomeStyle.Thin,
            "duotone" => FontAwesomeStyle.Duotone,
            _ => null
        };
    }

    /// <summary>
    /// Normalizes an icon name by removing hyphens and underscores, and converting to lowercase.
    /// This matches the normalization used in FontAwesomeHelper.GetIconUnicodeMapping to ensure
    /// consistent lookups between enum values (CamelCase) and icons.json keys (kebab-case).
    /// </summary>
    /// <param name="iconName">The icon name to normalize.</param>
    /// <returns>The normalized icon name.</returns>
    private static string NormalizeIconName(string iconName)
    {
        if (string.IsNullOrEmpty(iconName))
        {
            return string.Empty;
        }

        return iconName.ToLowerInvariant().Replace("-", "").Replace("_", "").Trim();
    }
}
