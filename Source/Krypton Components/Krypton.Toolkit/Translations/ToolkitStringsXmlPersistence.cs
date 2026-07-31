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

    // Format version for the exported XML file.
    private const int CurrentSupportedVersion = 1;

    private const string VersionAttribute = @"Version";
    private const string CultureAttribute = @"Culture";
    private const string GeneratedAttribute = @"Generated";
    private const string ValueAttribute = @"Value";
    private const string IsNullAttribute = @"IsNull";

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

        // Attempt to validate version compatibility.
        if (int.TryParse(root.GetAttribute(VersionAttribute), NumberStyles.Integer,
                CultureInfo.InvariantCulture, out var fileVersion)
            && fileVersion < CurrentSupportedVersion)
        {
            // We can still attempt best-effort import for unknown properties, but old formats may not match.
            throw new ArgumentException(
                $@"Translations.xml format version '{fileVersion}' is incompatible. Supported version is {CurrentSupportedVersion} or above.");
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

        // Canonical CommonStrings wins when both legacy alias paths and CommonStrings are present.
        if (toolkitStrings is KryptonGlobalToolkitStrings globalStrings)
        {
            var commonElement = root.SelectSingleNode(nameof(KryptonGlobalToolkitStrings.CommonStrings)) as XmlElement;
            if (commonElement != null)
            {
                ImportGlobalIdFromElement(commonElement, globalStrings.CommonStrings);
            }
        }

        if (refreshOpenForms)
        {
            RefreshOpenFormsBestEffort();
        }
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

            // Compatibility aliases are still imported, but canonical CommonStrings is exported once.
            if (prop.GetCustomAttribute<ToolkitStringsCanonicalAliasAttribute>(inherit: false) != null)
            {
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
        var deferCommonStrings = obj is KryptonGlobalToolkitStrings;

        foreach (var prop in objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

            // First pass: skip CommonStrings so legacy alias paths apply first.
            if (deferCommonStrings && prop.Name == nameof(KryptonGlobalToolkitStrings.CommonStrings))
            {
                continue;
            }

            // String properties: locate element by name.
            if (prop.PropertyType == typeof(string))
            {
                if (!prop.CanWrite)
                {
                    continue;
                }

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

            // Nested string sets (GlobalId derived): recurse into existing instance (get-only Content properties).
            if (typeof(GlobalId).IsAssignableFrom(prop.PropertyType))
            {
                if (!prop.CanRead)
                {
                    continue;
                }

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
}

