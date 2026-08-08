#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal static class KryptonCustomStringsPersistence
{
    private const string RootElementName = @"KryptonCustomTranslations";
    private const string ValuesElementName = @"Values";
    private const string ValueElementName = @"String";
    private const string StringSetsElementName = @"StringSets";
    private const string StringSetElementName = @"StringSet";
    private const string VersionAttribute = @"Version";
    private const string CultureAttribute = @"Culture";
    private const string GeneratedAttribute = @"Generated";
    private const string KeyAttribute = @"Key";
    private const string NameAttribute = @"Name";
    private const string ValueAttribute = @"Value";
    private const string IsNullAttribute = @"IsNull";
    private const int CurrentSupportedVersion = 1;

    public static XmlDocument ExportToXmlDocument(bool includeDefaults)
    {
        var doc = new XmlDocument();
        doc.AppendChild(doc.CreateProcessingInstruction(@"xml", @"version=""1.0"""));

        var root = doc.CreateElement(RootElementName);
        root.SetAttribute(VersionAttribute, CurrentSupportedVersion.ToString(CultureInfo.InvariantCulture));
        root.SetAttribute(CultureAttribute, Thread.CurrentThread.CurrentUICulture.Name);
        root.SetAttribute(GeneratedAttribute, DateTime.Now.ToString(CultureInfo.InvariantCulture));
        doc.AppendChild(root);

        ExportValues(doc, root);
        ExportStringSets(doc, root, includeDefaults);

        return doc;
    }

    public static void ExportToXmlFile(string filename, bool includeDefaults)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        ExportToXmlDocument(includeDefaults).Save(filename);
    }

    public static void ExportToXmlStream(Stream stream, bool includeDefaults)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        ExportToXmlDocument(includeDefaults).Save(stream);
    }

    public static void ImportFromXmlFile(string filename, bool resetFirst)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        var doc = new XmlDocument();
        doc.Load(filename);
        ImportFromXmlDocument(doc, resetFirst);
    }

    public static void ImportFromXmlStream(Stream stream, bool resetFirst)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        var doc = new XmlDocument();
        doc.Load(stream);
        ImportFromXmlDocument(doc, resetFirst);
    }

    public static void ImportFromXmlDocument(XmlDocument doc, bool resetFirst)
    {
        if (doc == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(doc));
        }

        var root = doc.SelectSingleNode(RootElementName) as XmlElement;
        if (root == null)
        {
            ThrowHelper.ThrowArgumentException($@"Root element must be called '{RootElementName}'.");
        }

        if (int.TryParse(root.GetAttribute(VersionAttribute), NumberStyles.Integer, CultureInfo.InvariantCulture, out var fileVersion)
            && fileVersion < CurrentSupportedVersion)
        {
            ThrowHelper.ThrowArgumentException($@"Custom translations format version '{fileVersion}' is incompatible. Supported version is {CurrentSupportedVersion} or above.");
        }

        if (resetFirst)
        {
            KryptonCustomStrings.ResetValues();
            KryptonCustomStrings.ResetStringSets();
        }

        ImportValues(root);
        ImportStringSets(root);
        KryptonCustomStrings.OnCustomStringsImported();
    }

    public static string ExportToJson(bool includeDefaults)
    {
        var root = ExportToXmlDocument(includeDefaults).DocumentElement;
        Debug.Assert(root != null);
        var sb = new StringBuilder(2048);
        sb.AppendLine(@"{");
        sb.AppendLine($@"  ""{VersionAttribute}"": {CurrentSupportedVersion},");
        sb.AppendLine($@"  ""{CultureAttribute}"": ""{EscapeJson(root!.GetAttribute(CultureAttribute))}"",");
        sb.AppendLine($@"  ""{GeneratedAttribute}"": ""{EscapeJson(root.GetAttribute(GeneratedAttribute))}"",");
        AppendValuesJson(sb, root);
        sb.AppendLine(@"}");
        return sb.ToString();
    }

    public static void ExportToJsonFile(string filename, bool includeDefaults)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        File.WriteAllText(filename, ExportToJson(includeDefaults), Encoding.UTF8);
    }

    public static void ExportToJsonStream(Stream stream, bool includeDefaults)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        var bytes = Encoding.UTF8.GetBytes(ExportToJson(includeDefaults));
        stream.Write(bytes, 0, bytes.Length);
    }

    public static void ImportFromJsonFile(string filename, bool resetFirst)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        var json = File.ReadAllText(filename, Encoding.UTF8);
        ImportFromJson(json, resetFirst);
    }

    public static void ImportFromJsonStream(Stream stream, bool resetFirst)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        ImportFromJson(reader.ReadToEnd(), resetFirst);
    }

    public static void ImportFromJson(string json, bool resetFirst)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            ThrowHelper.ThrowArgumentException(@"JSON content is empty.", nameof(json));
        }

        var root = ParseJsonObject(json);

        if (resetFirst)
        {
            KryptonCustomStrings.ResetValues();
            KryptonCustomStrings.ResetStringSets();
        }

        if (root.TryGetValue(ValuesElementName, out var valuesObj) && valuesObj is Dictionary<string, object?> values)
        {
            foreach (var pair in values)
            {
                KryptonCustomStrings.Set(pair.Key, pair.Value?.ToString() ?? string.Empty);
            }
        }

        if (root.TryGetValue(StringSetsElementName, out var setsObj) && setsObj is Dictionary<string, object?> sets)
        {
            foreach (var registration in sets)
            {
                if (!KryptonCustomStrings.TryGetStringSet(registration.Key, out var stringSet) || stringSet == null)
                {
                    continue;
                }

                if (registration.Value is Dictionary<string, object?> propBag)
                {
                    ImportStringPropertiesFromDictionary(stringSet, propBag);
                }
            }
        }

        KryptonCustomStrings.OnCustomStringsImported();
    }

    private static void ExportValues(XmlDocument doc, XmlElement root)
    {
        var values = doc.CreateElement(ValuesElementName);
        foreach (var entry in KryptonCustomStrings.Values.Entries)
        {
            var child = doc.CreateElement(ValueElementName);
            child.SetAttribute(KeyAttribute, entry.Key ?? string.Empty);
            child.SetAttribute(ValueAttribute, entry.Value ?? string.Empty);
            values.AppendChild(child);
        }

        root.AppendChild(values);
    }

    private static void ExportStringSets(XmlDocument doc, XmlElement root, bool includeDefaults)
    {
        var stringSets = doc.CreateElement(StringSetsElementName);
        var anyWritten = false;

        foreach (var registration in KryptonCustomStringSetRegistry.Snapshot())
        {
            var setElement = doc.CreateElement(StringSetElementName);
            setElement.SetAttribute(NameAttribute, registration.Key);
            var nestedWritten = ExportGlobalIdToElementInternal(doc, setElement, registration.Value, includeDefaults);
            if (nestedWritten || includeDefaults)
            {
                stringSets.AppendChild(setElement);
                anyWritten = true;
            }
        }

        if (anyWritten || includeDefaults)
        {
            root.AppendChild(stringSets);
        }
    }

    private static void ImportValues(XmlElement root)
    {
        var values = root.SelectSingleNode(ValuesElementName) as XmlElement;
        if (values == null)
        {
            return;
        }

        foreach (XmlNode childNode in values.ChildNodes)
        {
            if (childNode is not XmlElement child || child.Name != ValueElementName)
            {
                continue;
            }

            var key = child.GetAttribute(KeyAttribute);
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            KryptonCustomStrings.Set(key, child.GetAttribute(ValueAttribute));
        }
    }

    private static void ImportStringSets(XmlElement root)
    {
        var stringSets = root.SelectSingleNode(StringSetsElementName) as XmlElement;
        if (stringSets == null)
        {
            return;
        }

        foreach (XmlNode childNode in stringSets.ChildNodes)
        {
            if (childNode is not XmlElement child || child.Name != StringSetElementName)
            {
                continue;
            }

            var name = child.GetAttribute(NameAttribute);
            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            if (KryptonCustomStrings.TryGetStringSet(name, out var stringSet) && stringSet != null)
            {
                ImportGlobalIdFromElement(child, stringSet);
            }
        }
    }

    private static bool ExportGlobalIdToElementInternal(XmlDocument doc, XmlElement parent, object obj, bool includeDefaults)
    {
        var anyWritten = false;
        foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!prop.CanRead || prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

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

                var child = doc.CreateElement(prop.Name);
                if (value == null)
                {
                    child.SetAttribute(IsNullAttribute, @"true");
                    child.SetAttribute(ValueAttribute, string.Empty);
                }
                else
                {
                    child.SetAttribute(ValueAttribute, value);
                }

                var descriptionAttr = prop.GetCustomAttribute<DescriptionAttribute>(inherit: false);
                var description = descriptionAttr?.Description;
                if (!string.IsNullOrWhiteSpace(description))
                {
                    parent.AppendChild(doc.CreateComment(description!.Replace(@"--", @"- -")));
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
                if (ExportGlobalIdToElementInternal(doc, container, nested, includeDefaults))
                {
                    parent.AppendChild(container);
                    anyWritten = true;
                }
            }
        }

        return anyWritten;
    }

    private static void ImportGlobalIdFromElement(XmlElement parentElement, object obj)
    {
        foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!prop.CanWrite || prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

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

                if (string.Equals(child.GetAttribute(IsNullAttribute), @"true", StringComparison.OrdinalIgnoreCase))
                {
                    prop.SetValue(obj, null, null);
                }
                else
                {
                    prop.SetValue(obj, child.GetAttribute(ValueAttribute), null);
                }
            }
            else if (typeof(GlobalId).IsAssignableFrom(prop.PropertyType))
            {
                var container = parentElement.SelectSingleNode(prop.Name) as XmlElement;
                var nested = prop.GetValue(obj, null);
                if (container != null && nested != null)
                {
                    ImportGlobalIdFromElement(container, nested);
                }
            }
        }
    }

    private static void ImportStringPropertiesFromDictionary(object obj, Dictionary<string, object?> propBag)
    {
        foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!prop.CanWrite || prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

            if (prop.PropertyType == typeof(string))
            {
                var localizable = prop.GetCustomAttribute<LocalizableAttribute>(inherit: false);
                if (localizable?.IsLocalizable != true)
                {
                    continue;
                }

                if (propBag.TryGetValue(prop.Name, out var value))
                {
                    prop.SetValue(obj, value?.ToString(), null);
                }
            }
        }
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

        return string.Equals(value, defaultAttr.Value as string, StringComparison.Ordinal);
    }

    private static void AppendValuesJson(StringBuilder sb, XmlElement root)
    {
        sb.AppendLine($@"  ""{ValuesElementName}"": {{");
        if (root.SelectSingleNode(ValuesElementName) is XmlElement values)
        {
            for (var i = 0; i < values.ChildNodes.Count; i++)
            {
                if (values.ChildNodes[i] is not XmlElement { Name: ValueElementName } child)
                {
                    continue;
                }

                sb.Append($@"    ""{EscapeJson(child.GetAttribute(KeyAttribute))}"": ""{EscapeJson(child.GetAttribute(ValueAttribute))}""");
                if (HasFurtherValueNodes(values, i))
                {
                    sb.Append(',');
                }

                sb.AppendLine();
            }
        }

        sb.AppendLine(@"  },");
        sb.AppendLine($@"  ""{StringSetsElementName}"": {{");

        if (root.SelectSingleNode(StringSetsElementName) is XmlElement stringSets)
        {
            for (var i = 0; i < stringSets.ChildNodes.Count; i++)
            {
                if (stringSets.ChildNodes[i] is not XmlElement { Name: StringSetElementName } child)
                {
                    continue;
                }

                sb.AppendLine($@"    ""{EscapeJson(child.GetAttribute(NameAttribute))}"": {{");
                AppendTypedSetJson(sb, child);
                sb.Append(@"    }");
                if (HasFurtherStringSetNodes(stringSets, i))
                {
                    sb.Append(',');
                }

                sb.AppendLine();
            }
        }

        sb.AppendLine(@"  }");
    }

    private static void AppendTypedSetJson(StringBuilder sb, XmlElement setElement)
    {
        var written = new List<XmlElement>();
        foreach (XmlNode childNode in setElement.ChildNodes)
        {
            if (childNode is XmlElement child && child.HasAttribute(ValueAttribute))
            {
                written.Add(child);
            }
        }

        for (var i = 0; i < written.Count; i++)
        {
            sb.Append($@"      ""{EscapeJson(written[i].Name)}"": ""{EscapeJson(written[i].GetAttribute(ValueAttribute))}""");
            if (i < written.Count - 1)
            {
                sb.Append(',');
            }

            sb.AppendLine();
        }
    }

    private static Dictionary<string, object?> ParseJsonObject(string json)
    {
        var tokens = Tokenize(json);
        var index = 0;
        return ParseObject(tokens, ref index);
    }

    private static Dictionary<string, object?> ParseObject(List<string> tokens, ref int index)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal);
        ExpectToken(tokens, ref index, @"{");

        while (index < tokens.Count && tokens[index] != @"}")
        {
            var key = UnquoteJsonString(tokens[index++]);
            ExpectToken(tokens, ref index, @":");

            if (index < tokens.Count && tokens[index] == @"{")
            {
                result[key] = ParseObject(tokens, ref index);
            }
            else
            {
                var token = tokens[index++];
                result[key] = string.Equals(token, @"null", StringComparison.Ordinal) ? null : UnquoteJsonString(token);
            }

            if (index < tokens.Count && tokens[index] == @",")
            {
                index++;
            }
        }

        ExpectToken(tokens, ref index, @"}");
        return result;
    }

    private static void ExpectToken(List<string> tokens, ref int index, string expected)
    {
        if (index >= tokens.Count || !string.Equals(tokens[index], expected, StringComparison.Ordinal))
        {
            ThrowHelper.ThrowArgumentException($@"JSON content is invalid. Expected token '{expected}'.");
        }

        index++;
    }

    private static List<string> Tokenize(string json)
    {
        var tokens = new List<string>();
        var index = 0;

        while (index < json.Length)
        {
            var c = json[index];
            if (char.IsWhiteSpace(c))
            {
                index++;
                continue;
            }

            if (c == '{' || c == '}' || c == ':' || c == ',')
            {
                tokens.Add(c.ToString());
                index++;
                continue;
            }

            if (c == '"')
            {
                var sb = new StringBuilder();
                sb.Append('"');
                index++;
                while (index < json.Length)
                {
                    if (json[index] == '\\' && index + 1 < json.Length)
                    {
                        sb.Append(json[index]);
                        sb.Append(json[index + 1]);
                        index += 2;
                    }
                    else if (json[index] == '"')
                    {
                        sb.Append('"');
                        index++;
                        break;
                    }
                    else
                    {
                        sb.Append(json[index]);
                        index++;
                    }
                }

                tokens.Add(sb.ToString());
                continue;
            }

            var start = index;
            while (index < json.Length && !char.IsWhiteSpace(json[index]) &&
                   json[index] != ',' && json[index] != '}' && json[index] != ':')
            {
                index++;
            }

            tokens.Add(json.Substring(start, index - start));
        }

        return tokens;
    }

    private static string UnquoteJsonString(string token)
    {
        if (token.Length >= 2 && token[0] == '"' && token[token.Length - 1] == '"')
        {
            token = token.Substring(1, token.Length - 2);
        }

        return token
            .Replace(@"\""", @"""")
            .Replace(@"\\", @"\")
            .Replace(@"\r", "\r")
            .Replace(@"\n", "\n")
            .Replace(@"\t", "\t");
    }

    private static bool HasFurtherValueNodes(XmlElement parent, int currentIndex)
    {
        for (var i = currentIndex + 1; i < parent.ChildNodes.Count; i++)
        {
            if (parent.ChildNodes[i] is XmlElement { Name: ValueElementName })
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasFurtherStringSetNodes(XmlElement parent, int currentIndex)
    {
        for (var i = currentIndex + 1; i < parent.ChildNodes.Count; i++)
        {
            if (parent.ChildNodes[i] is XmlElement { Name: StringSetElementName })
            {
                return true;
            }
        }

        return false;
    }

    private static string EscapeJson(string? value) =>
        (value ?? string.Empty)
        .Replace(@"\", @"\\")
        .Replace(@"""", @"\""")
        .Replace("\r", @"\r")
        .Replace("\n", @"\n")
        .Replace("\t", @"\t");
}
