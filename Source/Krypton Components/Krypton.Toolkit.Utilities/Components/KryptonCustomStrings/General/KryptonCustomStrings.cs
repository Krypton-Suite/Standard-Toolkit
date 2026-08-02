#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides access to application-defined custom strings that can be localised independently of built-in toolkit strings.
/// </summary>
public static class KryptonCustomStrings
{
    #region Static Fields

    private static readonly KryptonCustomStringValues _values = new KryptonCustomStringValues();
    private static bool _autoDiscoveryAttempted;

    #endregion

    #region Public

    /// <summary>
    /// Gets the key/value custom string values.
    /// </summary>
    public static KryptonCustomStringValues Values => _values;

    /// <summary>
    /// Gets or sets whether custom translations are auto-discovered after the first typed string-set registration.
    /// Defaults to <c>false</c> because applications usually need to register their custom sets before import.
    /// </summary>
    public static bool AutoDiscoverCustomTranslations { get; set; }

    /// <summary>
    /// Occurs after custom strings have been successfully imported.
    /// </summary>
    public static event EventHandler? CustomStringsImported;

    /// <summary>
    /// Gets a custom string value.
    /// </summary>
    /// <param name="key">The string key.</param>
    /// <param name="defaultValue">The value to return when the key is not found.</param>
    /// <returns>The stored value, or <paramref name="defaultValue"/> when the key is not found.</returns>
    public static string Get(string key, string defaultValue = "")
        => Values.TryGetValue(key, out string value) ? value : defaultValue;

    /// <summary>
    /// Sets a custom string value.
    /// </summary>
    /// <param name="key">The string key.</param>
    /// <param name="value">The localizable value.</param>
    public static void Set(string key, string value)
        => Values.Set(key, value);

    /// <summary>
    /// Resets all key/value custom strings.
    /// </summary>
    public static void ResetValues()
        => Values.Reset();

    /// <summary>
    /// Registers or replaces a strongly-typed custom string set.
    /// </summary>
    /// <param name="name">The unique registration name.</param>
    /// <param name="stringSet">The string set instance.</param>
    public static void RegisterStringSet(string name, GlobalId stringSet)
    {
        KryptonCustomStringSetRegistry.Register(name, stringSet);
        TryAutoDiscover();
    }

    /// <summary>
    /// Gets a registered strongly-typed custom string set.
    /// </summary>
    /// <typeparam name="T">The expected string set type.</typeparam>
    /// <param name="name">The registration name.</param>
    /// <returns>The registered string set, or <c>null</c> when the name is not registered or the type does not match.</returns>
    public static T? GetStringSet<T>(string name)
        where T : GlobalId
        => KryptonCustomStringSetRegistry.Get<T>(name);

    /// <summary>
    /// Attempts to get a registered strongly-typed custom string set.
    /// </summary>
    /// <param name="name">The registration name.</param>
    /// <param name="stringSet">When this method returns, contains the registered string set if found.</param>
    /// <returns><c>true</c> if the name was registered; otherwise, <c>false</c>.</returns>
    public static bool TryGetStringSet(string name, out GlobalId? stringSet)
        => KryptonCustomStringSetRegistry.TryGet(name, out stringSet);

    /// <summary>
    /// Determines whether a strongly-typed custom string set is registered.
    /// </summary>
    /// <param name="name">The registration name.</param>
    /// <returns><c>true</c> if the name is registered; otherwise, <c>false</c>.</returns>
    public static bool ContainsStringSet(string name)
        => KryptonCustomStringSetRegistry.Contains(name);

    /// <summary>
    /// Removes a registered strongly-typed custom string set.
    /// </summary>
    /// <param name="name">The registration name.</param>
    /// <returns><c>true</c> if the string set was removed; otherwise, <c>false</c>.</returns>
    public static bool UnregisterStringSet(string name)
        => KryptonCustomStringSetRegistry.Unregister(name);

    /// <summary>
    /// Resets all registered strongly-typed custom string sets to their default values.
    /// </summary>
    public static void ResetStringSets()
        => KryptonCustomStringSetRegistry.ResetAll();

    /// <summary>
    /// Exports the current custom strings to a versioned XML document.
    /// </summary>
    /// <param name="includeDefaults">When <c>true</c>, includes default values from registered typed string sets.</param>
    public static XmlDocument ExportToXmlDocument(bool includeDefaults = false)
        => KryptonCustomStringsPersistence.ExportToXmlDocument(includeDefaults);

    /// <summary>
    /// Exports the current custom strings to a versioned XML file.
    /// </summary>
    /// <param name="filename">Destination file path.</param>
    /// <param name="includeDefaults">When <c>true</c>, includes default values from registered typed string sets.</param>
    public static void ExportToXmlFile(string filename, bool includeDefaults = false)
        => KryptonCustomStringsPersistence.ExportToXmlFile(filename, includeDefaults);

    /// <summary>
    /// Exports the current custom strings to a stream containing the XML format.
    /// </summary>
    /// <param name="stream">Destination stream.</param>
    /// <param name="includeDefaults">When <c>true</c>, includes default values from registered typed string sets.</param>
    public static void ExportToXmlStream(Stream stream, bool includeDefaults = false)
        => KryptonCustomStringsPersistence.ExportToXmlStream(stream, includeDefaults);

    /// <summary>
    /// Imports custom strings from a versioned XML document.
    /// </summary>
    /// <param name="doc">The XML document to import.</param>
    /// <param name="resetFirst">When <c>true</c>, resets key/value strings and registered typed sets before import.</param>
    public static void ImportFromXmlDocument(XmlDocument doc, bool resetFirst = true)
        => KryptonCustomStringsPersistence.ImportFromXmlDocument(doc, resetFirst);

    /// <summary>
    /// Imports custom strings from a versioned XML file.
    /// </summary>
    /// <param name="filename">Source file path.</param>
    /// <param name="resetFirst">When <c>true</c>, resets key/value strings and registered typed sets before import.</param>
    public static void ImportFromXmlFile(string filename, bool resetFirst = true)
        => KryptonCustomStringsPersistence.ImportFromXmlFile(filename, resetFirst);

    /// <summary>
    /// Imports custom strings from an XML stream.
    /// </summary>
    /// <param name="stream">Source stream.</param>
    /// <param name="resetFirst">When <c>true</c>, resets key/value strings and registered typed sets before import.</param>
    public static void ImportFromXmlStream(Stream stream, bool resetFirst = true)
        => KryptonCustomStringsPersistence.ImportFromXmlStream(stream, resetFirst);

    /// <summary>
    /// Exports the current custom strings to JSON.
    /// </summary>
    /// <param name="includeDefaults">When <c>true</c>, includes default values from registered typed string sets.</param>
    public static string ExportToJson(bool includeDefaults = false)
        => KryptonCustomStringsPersistence.ExportToJson(includeDefaults);

    /// <summary>
    /// Exports the current custom strings to a JSON file.
    /// </summary>
    /// <param name="filename">Destination file path.</param>
    /// <param name="includeDefaults">When <c>true</c>, includes default values from registered typed string sets.</param>
    public static void ExportToJsonFile(string filename, bool includeDefaults = false)
        => KryptonCustomStringsPersistence.ExportToJsonFile(filename, includeDefaults);

    /// <summary>
    /// Exports the current custom strings to a JSON stream.
    /// </summary>
    /// <param name="stream">Destination stream.</param>
    /// <param name="includeDefaults">When <c>true</c>, includes default values from registered typed string sets.</param>
    public static void ExportToJsonStream(Stream stream, bool includeDefaults = false)
        => KryptonCustomStringsPersistence.ExportToJsonStream(stream, includeDefaults);

    /// <summary>
    /// Imports custom strings from a JSON file.
    /// </summary>
    /// <param name="filename">Source file path.</param>
    /// <param name="resetFirst">When <c>true</c>, resets key/value strings and registered typed sets before import.</param>
    public static void ImportFromJsonFile(string filename, bool resetFirst = true)
        => KryptonCustomStringsPersistence.ImportFromJsonFile(filename, resetFirst);

    /// <summary>
    /// Imports custom strings from a JSON stream.
    /// </summary>
    /// <param name="stream">Source stream.</param>
    /// <param name="resetFirst">When <c>true</c>, resets key/value strings and registered typed sets before import.</param>
    public static void ImportFromJsonStream(Stream stream, bool resetFirst = true)
        => KryptonCustomStringsPersistence.ImportFromJsonStream(stream, resetFirst);

    /// <summary>
    /// Attempts to auto-discover a custom translations file in the supplied directory.
    /// If no directory is supplied, uses the application's base directory.
    /// Uses the same culture-aware probe order as toolkit translations: exact → neutral → default,
    /// preferring XML over JSON, with graceful fallback when a candidate is missing or invalid.
    /// </summary>
    /// <param name="directory">Optional directory to probe.</param>
    /// <returns><c>true</c> if a file was found and imported successfully; otherwise, <c>false</c>.</returns>
    public static bool TryAutoDiscover(string? directory = null)
    {
        if (!AutoDiscoverCustomTranslations || _autoDiscoveryAttempted)
        {
            return false;
        }

        _autoDiscoveryAttempted = true;

        var baseDirectory = string.IsNullOrWhiteSpace(directory)
            ? AppDomain.CurrentDomain.BaseDirectory
            : directory;

        if (string.IsNullOrWhiteSpace(baseDirectory))
        {
            baseDirectory = Directory.GetCurrentDirectory();
        }

        return TryLoadCultureSpecificFile(baseDirectory!, @"CustomTranslations");
    }

    /// <summary>
    /// Tries to load a culture-specific custom translations file.
    /// Exact-culture, neutral-culture, and default filenames are probed in order, preferring XML over JSON.
    /// Missing or invalid candidates are skipped gracefully until a load succeeds or no candidates remain.
    /// </summary>
    /// <param name="directory">Directory containing the translations files.</param>
    /// <param name="baseName">Base file name without culture suffix or extension.</param>
    /// <param name="culture">Culture to resolve. When null, uses <see cref="CultureInfo.CurrentUICulture"/>.</param>
    /// <returns><c>true</c> if a file was found and imported successfully; otherwise, <c>false</c>.</returns>
    public static bool TryLoadCultureSpecificFile(
        string directory,
        string baseName = @"CustomTranslations",
        CultureInfo? culture = null)
    {
        if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(baseName))
        {
            return false;
        }

        foreach (var candidate in BuildCultureSpecificCandidates(directory, baseName, culture ?? CultureInfo.CurrentUICulture))
        {
            if (!File.Exists(candidate))
            {
                continue;
            }

            try
            {
                if (string.Equals(Path.GetExtension(candidate), @".json", StringComparison.OrdinalIgnoreCase))
                {
                    ImportFromJsonFile(candidate, resetFirst: true);
                }
                else
                {
                    ImportFromXmlFile(candidate, resetFirst: true);
                }

                Debug.WriteLine($@"[KryptonCustomStrings] Loaded culture-specific translations from '{candidate}'.");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"[KryptonCustomStrings] Culture-specific load failed for '{candidate}': {ex.Message}");
            }
        }

        return false;
    }

    /// <summary>
    /// Switches the current UI culture and reloads the best matching custom translations file for that culture.
    /// When no matching file is found, custom string values and registered typed sets are reset to defaults.
    /// </summary>
    /// <param name="culture">The culture to switch to.</param>
    /// <param name="directory">Directory containing the translation files. When null/empty, uses the application base directory.</param>
    /// <param name="baseName">Base file name without culture suffix or extension.</param>
    /// <returns>
    /// <c>true</c> when a culture-specific or fallback translations file was loaded;
    /// <c>false</c> when no file was found and defaults were restored. The UI culture is updated in both cases.
    /// </returns>
    public static bool TrySwitchCulture(
        CultureInfo culture,
        string? directory = null,
        string baseName = @"CustomTranslations")
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        var baseDirectory = string.IsNullOrWhiteSpace(directory)
            ? AppDomain.CurrentDomain.BaseDirectory
            : directory;

        if (string.IsNullOrWhiteSpace(baseDirectory))
        {
            baseDirectory = Directory.GetCurrentDirectory();
        }

        if (TryLoadCultureSpecificFile(baseDirectory!, baseName, culture))
        {
            return true;
        }

        ResetValues();
        ResetStringSets();
        OnCustomStringsImported();
        Debug.WriteLine(
            $@"[KryptonCustomStrings] Switched UI culture to '{culture.Name}' with no matching translations file; restored defaults.");
        return false;
    }

    /// <summary>
    /// Switches the current UI culture using a culture name and reloads matching custom translations.
    /// </summary>
    public static bool TrySwitchCulture(
        string cultureName,
        string? directory = null,
        string baseName = @"CustomTranslations")
    {
        if (string.IsNullOrWhiteSpace(cultureName))
        {
            return false;
        }

        try
        {
            return TrySwitchCulture(new CultureInfo(cultureName), directory, baseName);
        }
        catch (CultureNotFoundException ex)
        {
            Debug.WriteLine($@"[KryptonCustomStrings] TrySwitchCulture failed for '{cultureName}': {ex.Message}");
            return false;
        }
    }

    private static IEnumerable<string> BuildCultureSpecificCandidates(
        string directory,
        string baseName,
        CultureInfo culture)
    {
        var cultureName = culture?.Name ?? string.Empty;
        var neutralName = string.Empty;

        if (!string.IsNullOrEmpty(cultureName))
        {
            if (culture != null && culture.Parent != null && !string.IsNullOrEmpty(culture.Parent.Name))
            {
                neutralName = culture.Parent.Name;
            }
            else if (cultureName.Length >= 2)
            {
                neutralName = cultureName.Substring(0, 2);
            }
        }

        var names = new List<string>();

        void AddUnique(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) &&
                !names.Exists(existing => string.Equals(existing, name, StringComparison.OrdinalIgnoreCase)))
            {
                names.Add(name);
            }
        }

        AddUnique(cultureName);
        AddUnique(neutralName);
        names.Add(string.Empty);

        foreach (var name in names)
        {
            var fileStem = string.IsNullOrEmpty(name) ? baseName : $@"{baseName}.{name}";
            yield return Path.Combine(directory, $@"{fileStem}.xml");
        }

        foreach (var name in names)
        {
            var fileStem = string.IsNullOrEmpty(name) ? baseName : $@"{baseName}.{name}";
            yield return Path.Combine(directory, $@"{fileStem}.json");
        }
    }

    internal static void OnCustomStringsImported() =>
        CustomStringsImported?.Invoke(null, EventArgs.Empty);

    #endregion
}
