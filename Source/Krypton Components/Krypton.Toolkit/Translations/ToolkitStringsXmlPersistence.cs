#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class ToolkitStringsXmlPersistence
{
    private const string RootElementName = @"KryptonTranslations";

    // Structural format version only — catalog drift is handled by coverage/merge, not by failing old files.
    private const int CurrentSupportedVersion = 1;

    private const string VersionAttribute = @"Version";
    private const string CultureAttribute = @"Culture";
    private const string GeneratedAttribute = @"Generated";
    private const string ToolkitVersionAttribute = @"ToolkitVersion";
    private const string ValueAttribute = @"Value";
    private const string IsNullAttribute = @"IsNull";

    // Legacy root groups that map onto the canonical CommonStrings tree.
    private static readonly Dictionary<string, string> LegacyRootAliases =
        new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { @"GeneralStrings", @"CommonStrings.General" },
            { @"ControlBoxButtonStrings", @"CommonStrings.ControlBox" },
            { @"SystemMenuStrings", @"CommonStrings.SystemMenu" },
            { @"FileSystemListViewStrings", @"CommonStrings.FileSystem" }
        };

    // Selected CustomStrings command properties owned by CommonStrings.Commands.
    private static readonly HashSet<string> LegacyCustomCommandProperties =
        new HashSet<string>(StringComparer.Ordinal)
        {
            @"Apply", @"Back", @"Exit", @"Finish", @"Next", @"Previous",
            @"Cut", @"Copy", @"Paste", @"SelectAll", @"ClearClipboard",
            @"YesToAll", @"NoToAll", @"OkToAll", @"Reset"
        };

    public static XmlDocument Export(Object toolkitStrings, bool includeDefaults)
    {
        if (toolkitStrings == null)
        {
            throw new ArgumentNullException(nameof(toolkitStrings));
        }

        var doc = new XmlDocument();

        // Create processing instruction like existing Krypton XML persistence.
        doc.AppendChild(doc.CreateProcessingInstruction(@"xml", @"version=""1.0"""));

        var root = doc.CreateElement(RootElementName);
        root.SetAttribute(VersionAttribute, CurrentSupportedVersion.ToString(CultureInfo.InvariantCulture));
        root.SetAttribute(CultureAttribute, Thread.CurrentThread.CurrentUICulture.Name);
        root.SetAttribute(GeneratedAttribute, DateTime.Now.ToString(CultureInfo.InvariantCulture));
        root.SetAttribute(ToolkitVersionAttribute, GetToolkitVersionStamp());
        doc.AppendChild(root);

        ExportGlobalIdToElement(doc, root, toolkitStrings, includeDefaults);

        return doc;
    }

    public static void Import(Object toolkitStrings, XmlDocument doc, bool resetFirst, bool refreshOpenForms, bool warnOnCultureMismatch = true)
    {
        if (toolkitStrings == null)
        {
            throw new ArgumentNullException(nameof(toolkitStrings));
        }

        if (doc == null)
        {
            throw new ArgumentNullException(nameof(doc));
        }

        if (resetFirst && toolkitStrings is KryptonGlobalToolkitStrings strings)
        {
            strings.Reset();
        }

        // Validate document content.
        if (!doc.HasChildNodes)
        {
            throw new ArgumentException(@"Xml document does not have a root element.");
        }

        var root = doc.SelectSingleNode(RootElementName) as XmlElement;
        if (root == null)
        {
            throw new ArgumentException($@"Root element must be called '{RootElementName}'.");
        }

        // Format Version is structural only. Older/newer additive files remain loadable;
        // catalog drift is reported via coverage rather than failing the import.
        var versionText = root.GetAttribute(VersionAttribute);
        if (int.TryParse(versionText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var fileVersion))
        {
            if (fileVersion < CurrentSupportedVersion)
            {
                Debug.WriteLine(
                    $@"[Krypton] Translations.xml format version '{fileVersion}' is older than supported structural version {CurrentSupportedVersion}. Import will continue best-effort.");
            }
            else if (fileVersion > CurrentSupportedVersion)
            {
                Debug.WriteLine(
                    $@"[Krypton] Translations.xml format version '{fileVersion}' is newer than this toolkit ({CurrentSupportedVersion}). Unknown structure may be ignored.");
            }
        }
        else if (!string.IsNullOrWhiteSpace(versionText))
        {
            Debug.WriteLine($@"[Krypton] Translations.xml has unrecognised Version '{versionText}'. Import will continue best-effort.");
        }

        // Warn if the file's culture doesn't match the current UI culture.
        if (warnOnCultureMismatch)
        {
            var fileCulture = root.GetAttribute(CultureAttribute);
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            if (!string.IsNullOrEmpty(fileCulture)
                && !String.Equals(fileCulture, currentCulture, StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine(
                    $@"[Krypton] Translations.xml was created for culture '{fileCulture}' but the current UI culture is '{currentCulture}'. Strings may not display correctly.");
            }
        }

        ImportGlobalIdFromElement(root, toolkitStrings);

        ReportCoverage(toolkitStrings, doc, filePath: null);

        if (refreshOpenForms)
        {
            RefreshOpenFormsBestEffort();
        }
    }

    /// <summary>
    /// Compares the live toolkit catalog against a translations document without mutating state.
    /// </summary>
    public static ToolkitStringsCoverage Analyze(Object toolkitStrings, XmlDocument doc, string? filePath = null)
    {
        if (toolkitStrings == null)
        {
            throw new ArgumentNullException(nameof(toolkitStrings));
        }

        if (doc == null)
        {
            throw new ArgumentNullException(nameof(doc));
        }

        var coverage = new ToolkitStringsCoverage
        {
            FilePath = filePath
        };

        var root = doc.SelectSingleNode(RootElementName) as XmlElement;
        if (root != null)
        {
            coverage.Culture = root.GetAttribute(CultureAttribute);
            if (string.IsNullOrWhiteSpace(coverage.Culture))
            {
                coverage.Culture = null;
            }

            coverage.ToolkitVersion = root.GetAttribute(ToolkitVersionAttribute);
            if (string.IsNullOrWhiteSpace(coverage.ToolkitVersion))
            {
                coverage.ToolkitVersion = null;
            }

            if (int.TryParse(root.GetAttribute(VersionAttribute), NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out var formatVersion))
            {
                coverage.FormatVersion = formatVersion;
            }
        }

        var toolkitKeys = new HashSet<string>(StringComparer.Ordinal);
        CollectToolkitKeys(toolkitStrings, string.Empty, toolkitKeys, skipAliases: true);

        var fileKeys = new HashSet<string>(StringComparer.Ordinal);
        if (root != null)
        {
            CollectFileKeys(root, string.Empty, fileKeys);
        }

        var normalizedFileKeys = NormalizeFileKeys(fileKeys);

        foreach (var key in toolkitKeys.OrderBy(k => k, StringComparer.Ordinal))
        {
            if (normalizedFileKeys.Contains(key))
            {
                coverage.Applied.Add(key);
            }
            else
            {
                coverage.MissingInFile.Add(key);
            }
        }

        foreach (var key in fileKeys.OrderBy(k => k, StringComparer.Ordinal))
        {
            var mapped = MapLegacyPath(key);
            if (!toolkitKeys.Contains(key) && !toolkitKeys.Contains(mapped))
            {
                coverage.ExtraInFile.Add(key);
            }
        }

        return coverage;
    }

    /// <summary>
    /// Imports an existing translations file into <paramref name="toolkitStrings"/>, then re-exports
    /// with defaults included so newly added toolkit keys appear as English placeholders while
    /// already-translated values are preserved.
    /// </summary>
    public static ToolkitStringsCoverage MergeMissingToFile(Object toolkitStrings, string filename, bool includeDefaults = true)
    {
        if (toolkitStrings == null)
        {
            throw new ArgumentNullException(nameof(toolkitStrings));
        }

        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        var doc = new XmlDocument();
        doc.Load(filename);

        Import(toolkitStrings, doc, resetFirst: true, refreshOpenForms: false, warnOnCultureMismatch: true);

        var merged = Export(toolkitStrings, includeDefaults);
        merged.Save(filename);

        // Post-merge coverage against the rewritten file (missing keys filled with toolkit defaults).
        return Analyze(toolkitStrings, merged, filename);
    }

    public static XmlDocument ExportToXmlDocument(Object toolkitStrings, bool includeDefaults) =>
        Export(toolkitStrings, includeDefaults);

    public static void ExportToStream(Object toolkitStrings, Stream stream, bool includeDefaults = false)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        var doc = Export(toolkitStrings, includeDefaults);
        doc.Save(stream);
    }

    public static void ImportFromStream(Object toolkitStrings, Stream stream, bool resetFirst = true, bool refreshOpenForms = true, bool warnOnCultureMismatch = true)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        var doc = new XmlDocument();
        doc.Load(stream);
        Import(toolkitStrings, doc, resetFirst, refreshOpenForms, warnOnCultureMismatch);
    }

    internal static void RefreshOpenFormsBestEffort()
    {
        foreach (Form? form in Application.OpenForms)
        {
            if (form == null)
            {
                continue;
            }

            // Some Krypton chrome (caption buttons) may cache layout/tooltip data tied to global settings.
            if (form is KryptonForm kForm)
            {
                kForm.RecreateMinMaxCloseButtons();
            }

            form.Invalidate(true);
            form.Refresh();
        }
    }

    private static void ExportGlobalIdToElement(
        XmlDocument doc,
        XmlElement parent,
        Object obj,
        bool includeDefaults)
    {
        // Root export always writes children (even when empty); nested exports may be elided.
        ExportGlobalIdToElementInternal(doc, parent, obj, includeDefaults, forceWriteContainer: true);
    }

    private static bool ExportGlobalIdToElementInternal(
        XmlDocument doc,
        XmlElement parent,
        Object obj,
        bool includeDefaults,
        bool forceWriteContainer)
    {
        var anyWritten = false;

        var objType = obj.GetType();
        foreach (var prop in objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!prop.CanRead)
            {
                continue;
            }

            if (prop.GetIndexParameters().Length != 0)
            {
                // Skip indexers.
                continue;
            }

            // Persist string values only when explicitly localizable.
            if (prop.PropertyType == typeof(string))
            {
                var localizable = prop.GetCustomAttribute<LocalizableAttribute>(inherit: false);
                if (localizable?.IsLocalizable != true)
                {
                    continue;
                }

                var value = (string?)prop.GetValue(obj, null);

                if (!includeDefaults && IsDefaultStringProperty(prop, value))
                {
                    continue;
                }

                // Emit a human-readable XML comment when available.
                // This makes the exported file easier to edit/maintain in external tooling.
                var descriptionAttr = prop.GetCustomAttribute<DescriptionAttribute>(inherit: false);

                var child = doc.CreateElement(prop.Name);
                if (value == null)
                {
                    child.SetAttribute(IsNullAttribute, @"true");
                    child.SetAttribute(ValueAttribute, String.Empty);
                }
                else
                {
                    child.SetAttribute(ValueAttribute, value);
                }

                var description = descriptionAttr?.Description;
                if (!string.IsNullOrWhiteSpace(description))
                {
                    // XML comments cannot contain "--".
                    var sanitized = description!.Replace(@"--", @"- -");
                    parent.AppendChild(doc.CreateComment(sanitized));
                }

                parent.AppendChild(child);
                anyWritten = true;
            }
            else if (typeof(GlobalId).IsAssignableFrom(prop.PropertyType))
            {
                var nested = prop.GetValue(obj, null);
                if (nested == null)
                {
                    continue;
                }

                var container = doc.CreateElement(prop.Name);
                var nestedWritten = ExportGlobalIdToElementInternal(doc, container, nested, includeDefaults,
                    forceWriteContainer: false);

                if (nestedWritten || forceWriteContainer)
                {
                    parent.AppendChild(container);
                }

                anyWritten |= nestedWritten;
            }
        }

        return anyWritten;
    }

    private static bool IsDefaultStringProperty(PropertyInfo prop, string? value)
    {
        var defaultAttr = prop.GetCustomAttribute<DefaultValueAttribute>(inherit: false);
        if (defaultAttr == null)
        {
            return false;
        }

        if (defaultAttr.Value == null)
        {
            return value == null;
        }

        // DefaultValue for string properties should be string-compatible, but be defensive.
        var defaultString = defaultAttr.Value as string;
        if (defaultString == null)
        {
            return false;
        }

        return String.Equals(value, defaultString, StringComparison.Ordinal);
    }

    private static void ImportGlobalIdFromElement(XmlElement parentElement, Object obj)
    {
        var objType = obj.GetType();

        foreach (var prop in objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!prop.CanWrite)
            {
                continue;
            }

            if (prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

            // String properties: locate element by name.
            if (prop.PropertyType == typeof(string))
            {
                var localizable = prop.GetCustomAttribute<LocalizableAttribute>(inherit: false);
                if (localizable?.IsLocalizable != true)
                {
                    continue;
                }

                var child = parentElement.SelectSingleNode(prop.Name) as XmlElement;
                if (child == null)
                {
                    continue;
                }

                var isNull = String.Equals(child.GetAttribute(IsNullAttribute), @"true", StringComparison.OrdinalIgnoreCase);
                if (isNull)
                {
                    prop.SetValue(obj, null, null);
                    continue;
                }

                var value = child.GetAttribute(ValueAttribute);
                prop.SetValue(obj, value, null);
                continue;
            }

            // Nested string sets (GlobalId derived): recurse.
            if (typeof(GlobalId).IsAssignableFrom(prop.PropertyType))
            {
                var container = parentElement.SelectSingleNode(prop.Name) as XmlElement;
                if (container == null)
                {
                    continue;
                }

                var nested = prop.GetValue(obj, null);
                if (nested == null)
                {
                    continue;
                }

                ImportGlobalIdFromElement(container, nested);
            }
        }
    }

    internal static string GetToolkitVersionStamp()
    {
        var assembly = typeof(KryptonManager).Assembly;
        var informational = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (!string.IsNullOrWhiteSpace(informational))
        {
            // Strip any +metadata suffix from informational versions.
            var plus = informational!.IndexOf('+');
            return plus >= 0 ? informational.Substring(0, plus) : informational;
        }

        return assembly.GetName().Version?.ToString() ?? @"0.0.0.0";
    }

    private static void ReportCoverage(Object toolkitStrings, XmlDocument doc, string? filePath)
    {
        try
        {
            var coverage = Analyze(toolkitStrings, doc, filePath);
            if (coverage.HasMissing || coverage.HasExtra)
            {
                Debug.WriteLine(
                    $@"[Krypton] Translations coverage: {coverage}. Missing sample: {string.Join(@", ", coverage.MissingInFile.Take(5))}.");
            }

            KryptonManager.OnTranslationsCoverageReported(coverage);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($@"[Krypton] Translations coverage analysis failed: {ex.Message}");
        }
    }

    private static void CollectToolkitKeys(Object obj, string prefix, ISet<string> keys, bool skipAliases)
    {
        var objType = obj.GetType();
        foreach (var prop in objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!prop.CanRead || prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

            if (skipAliases && prop.GetCustomAttribute<ToolkitStringsCanonicalAliasAttribute>(inherit: false) != null)
            {
                continue;
            }

            var path = string.IsNullOrEmpty(prefix) ? prop.Name : prefix + @"." + prop.Name;

            if (prop.PropertyType == typeof(string))
            {
                var localizable = prop.GetCustomAttribute<LocalizableAttribute>(inherit: false);
                if (localizable?.IsLocalizable == true)
                {
                    keys.Add(path);
                }
            }
            else if (typeof(GlobalId).IsAssignableFrom(prop.PropertyType))
            {
                var nested = prop.GetValue(obj, null);
                if (nested != null)
                {
                    CollectToolkitKeys(nested, path, keys, skipAliases);
                }
            }
        }
    }

    private static void CollectFileKeys(XmlElement element, string prefix, ISet<string> keys)
    {
        foreach (XmlNode child in element.ChildNodes)
        {
            if (child is not XmlElement childEl)
            {
                continue;
            }

            var path = string.IsNullOrEmpty(prefix) ? childEl.Name : prefix + @"." + childEl.Name;
            if (childEl.HasAttribute(ValueAttribute))
            {
                keys.Add(path);
            }
            else
            {
                CollectFileKeys(childEl, path, keys);
            }
        }
    }

    private static HashSet<string> NormalizeFileKeys(IEnumerable<string> fileKeys)
    {
        var normalized = new HashSet<string>(StringComparer.Ordinal);
        foreach (var key in fileKeys)
        {
            normalized.Add(key);
            normalized.Add(MapLegacyPath(key));
        }

        return normalized;
    }

    private static string MapLegacyPath(string path)
    {
        var segments = path.Split('.');
        if (segments.Length == 0)
        {
            return path;
        }

        if (LegacyRootAliases.TryGetValue(segments[0], out var mappedRoot))
        {
            segments[0] = mappedRoot;
            return string.Join(@".", segments);
        }

        if (segments.Length >= 2
            && string.Equals(segments[0], @"CustomStrings", StringComparison.Ordinal)
            && LegacyCustomCommandProperties.Contains(segments[1]))
        {
            segments[0] = @"CommonStrings.Commands";
            return string.Join(@".", segments);
        }

        return path;
    }
}