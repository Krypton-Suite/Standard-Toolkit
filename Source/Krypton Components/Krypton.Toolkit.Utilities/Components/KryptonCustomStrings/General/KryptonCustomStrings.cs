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

    #endregion

    #region Public

    /// <summary>
    /// Gets the key/value custom string values.
    /// </summary>
    public static KryptonCustomStringValues Values => _values;

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
        => KryptonCustomStringSetRegistry.Register(name, stringSet);

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
    /// Imports custom strings from a JSON file.
    /// </summary>
    /// <param name="filename">Source file path.</param>
    /// <param name="resetFirst">When <c>true</c>, resets key/value strings and registered typed sets before import.</param>
    public static void ImportFromJsonFile(string filename, bool resetFirst = true)
        => KryptonCustomStringsPersistence.ImportFromJsonFile(filename, resetFirst);

    #endregion
}
