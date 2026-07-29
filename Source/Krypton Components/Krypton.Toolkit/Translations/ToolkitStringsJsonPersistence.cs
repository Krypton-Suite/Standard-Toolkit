#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class ToolkitStringsJsonPersistence
{
    private const string VersionKey = @"Version";
    private const string CultureKey = @"Culture";
    private const string GeneratedKey = @"Generated";
    private const int CurrentSupportedVersion = 1;

    /// <summary>
    /// Exports toolkit strings to a JSON string.
    /// </summary>
    public static string Export(Object toolkitStrings, bool includeDefaults)
    {
        if (toolkitStrings == null)
        {
            throw new ArgumentNullException(nameof(toolkitStrings));
        }

        var sb = new StringBuilder(4096);
        sb.AppendLine(@"{");
        sb.AppendLine($@"  ""{VersionKey}"": {CurrentSupportedVersion},");
        sb.AppendLine($@"  ""{CultureKey}"": ""{EscapeJsonString(Thread.CurrentThread.CurrentUICulture.Name)}"",");
        sb.AppendLine($@"  ""{GeneratedKey}"": ""{EscapeJsonString(DateTime.Now.ToString(CultureInfo.InvariantCulture))}"",");

        ExportObject(sb, toolkitStrings, includeDefaults, indent: 1, trailingComma: false);

        sb.AppendLine(@"}");
        return sb.ToString();
    }

    /// <summary>
    /// Exports toolkit strings to a JSON file.
    /// </summary>
    public static void ExportToFile(Object toolkitStrings, string filename, bool includeDefaults = false)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        var json = Export(toolkitStrings, includeDefaults);
        File.WriteAllText(filename, json, Encoding.UTF8);
    }

    /// <summary>
    /// Exports toolkit strings to a stream as JSON.
    /// </summary>
    public static void ExportToStream(Object toolkitStrings, Stream stream, bool includeDefaults = false)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        var json = Export(toolkitStrings, includeDefaults);
        var bytes = Encoding.UTF8.GetBytes(json);
        stream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// Imports toolkit strings from a JSON file.
    /// </summary>
    public static void ImportFromFile(Object toolkitStrings, string filename, bool resetFirst = true, bool refreshOpenForms = true)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        var json = File.ReadAllText(filename, Encoding.UTF8);
        ImportFromJson(toolkitStrings, json, resetFirst, refreshOpenForms);
    }

    /// <summary>
    /// Imports toolkit strings from a JSON stream.
    /// </summary>
    public static void ImportFromStream(Object toolkitStrings, Stream stream, bool resetFirst = true, bool refreshOpenForms = true)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        var json = reader.ReadToEnd();
        ImportFromJson(toolkitStrings, json, resetFirst, refreshOpenForms);
    }

    /// <summary>
    /// Imports toolkit strings from a JSON string.
    /// Uses the XML persistence helper by converting the JSON to an in-memory XmlDocument,
    /// keeping the import logic (reset, refresh, culture) centralised.
    /// </summary>
    public static void ImportFromJson(Object toolkitStrings, string json, bool resetFirst = true, bool refreshOpenForms = true)
    {
        if (toolkitStrings == null)
        {
            throw new ArgumentNullException(nameof(toolkitStrings));
        }

        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException(@"JSON content is empty.", nameof(json));
        }

        // Parse minimally and build an XmlDocument matching the canonical XML format,
        // then delegate to the standard XML import path.
        var doc = JsonToXmlDocument(json);
        ToolkitStringsXmlPersistence.Import(toolkitStrings, doc, resetFirst, refreshOpenForms);
    }

    #region Export helpers

    private static void ExportObject(StringBuilder sb, Object obj, bool includeDefaults, int indent, bool trailingComma)
    {
        var objType = obj.GetType();
        var props = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        var written = new List<Action>();

        foreach (var prop in props)
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

                var capturedName = prop.Name;
                var capturedValue = value;
                written.Add(() =>
                {
                    var pad = new string(' ', (indent + 1) * 2);
                    if (capturedValue == null)
                    {
                        sb.Append($@"{pad}""{EscapeJsonString(capturedName)}"": null");
                    }
                    else
                    {
                        sb.Append($@"{pad}""{EscapeJsonString(capturedName)}"": ""{EscapeJsonString(capturedValue)}""");
                    }
                });
            }
            else if (typeof(GlobalId).IsAssignableFrom(prop.PropertyType))
            {
                var nested = prop.GetValue(obj, null);
                if (nested == null)
                {
                    continue;
                }

                var capturedName = prop.Name;
                var capturedNested = nested;
                written.Add(() =>
                {
                    var pad = new string(' ', (indent + 1) * 2);
                    sb.AppendLine($@"{pad}""{EscapeJsonString(capturedName)}"": {{");
                    ExportObject(sb, capturedNested, includeDefaults, indent + 1, trailingComma: false);
                    sb.Append($@"{pad}}}");
                });
            }
        }

        for (var i = 0; i < written.Count; i++)
        {
            written[i]();
            sb.AppendLine(i < written.Count - 1 ? @"," : string.Empty);
        }
    }

    private static bool IsDefaultStringProperty(PropertyInfo prop, string? value)
    {
        var defaultAttr = prop.GetCustomAttribute<DefaultValueAttribute>(inherit: false);
        if (defaultAttr?.Value == null)
        {
            return value == null && defaultAttr != null;
        }

        return String.Equals(value, defaultAttr.Value as string, StringComparison.Ordinal);
    }

    private static string EscapeJsonString(string s)
    {
        return s
            .Replace(@"\", @"\\")
            .Replace(@"""", @"\""")
            .Replace("\n", @"\n")
            .Replace("\r", @"\r")
            .Replace("\t", @"\t");
    }

    #endregion

    #region Import helpers (JSON → XML bridge)

    private static XmlDocument JsonToXmlDocument(string json)
    {
        var doc = new XmlDocument();
        doc.AppendChild(doc.CreateProcessingInstruction(@"xml", @"version=""1.0"""));

        var root = doc.CreateElement(@"KryptonTranslations");
        doc.AppendChild(root);

        var tokens = Tokenize(json);
        var pos = 0;
        ParseObject(tokens, ref pos, doc, root);

        // Promote Version/Culture/Generated from child elements to root attributes.
        PromoteToAttribute(root, @"Version");
        PromoteToAttribute(root, @"Culture");
        PromoteToAttribute(root, @"Generated");

        return doc;
    }

    private static void PromoteToAttribute(XmlElement root, string name)
    {
        var child = root.SelectSingleNode(name) as XmlElement;
        if (child != null)
        {
            root.SetAttribute(name, child.GetAttribute(@"Value"));
            root.RemoveChild(child);
        }
    }

    private static void ParseObject(List<string> tokens, ref int pos, XmlDocument doc, XmlElement parent)
    {
        if (pos < tokens.Count && tokens[pos] == @"{")
        {
            pos++;
        }

        while (pos < tokens.Count && tokens[pos] != @"}")
        {
            var key = UnquoteJsonString(tokens[pos]);
            pos++; // skip key
            if (pos < tokens.Count && tokens[pos] == @":")
            {
                pos++; // skip colon
            }

            if (pos < tokens.Count && tokens[pos] == @"{")
            {
                var container = doc.CreateElement(key);
                parent.AppendChild(container);
                ParseObject(tokens, ref pos, doc, container);
            }
            else if (pos < tokens.Count)
            {
                var valueToken = tokens[pos];
                pos++;
                var child = doc.CreateElement(key);
                if (valueToken == @"null")
                {
                    child.SetAttribute(@"IsNull", @"true");
                    child.SetAttribute(@"Value", string.Empty);
                }
                else
                {
                    child.SetAttribute(@"Value", UnquoteJsonString(valueToken));
                }
                parent.AppendChild(child);
            }

            if (pos < tokens.Count && tokens[pos] == @",")
            {
                pos++;
            }
        }

        if (pos < tokens.Count && tokens[pos] == @"}")
        {
            pos++;
        }
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
            .Replace(@"\n", "\n")
            .Replace(@"\r", "\r")
            .Replace(@"\t", "\t");
    }

    private static List<string> Tokenize(string json)
    {
        var tokens = new List<string>();
        var i = 0;
        while (i < json.Length)
        {
            var c = json[i];
            if (char.IsWhiteSpace(c))
            {
                i++;
                continue;
            }

            if (c == '{' || c == '}' || c == '[' || c == ']' || c == ':' || c == ',')
            {
                tokens.Add(c.ToString());
                i++;
                continue;
            }

            if (c == '"')
            {
                var sb = new StringBuilder();
                sb.Append('"');
                i++;
                while (i < json.Length)
                {
                    if (json[i] == '\\' && i + 1 < json.Length)
                    {
                        sb.Append(json[i]);
                        sb.Append(json[i + 1]);
                        i += 2;
                    }
                    else if (json[i] == '"')
                    {
                        sb.Append('"');
                        i++;
                        break;
                    }
                    else
                    {
                        sb.Append(json[i]);
                        i++;
                    }
                }

                tokens.Add(sb.ToString());
                continue;
            }

            // Bare tokens (null, true, false, numbers)
            var start = i;
            while (i < json.Length && !char.IsWhiteSpace(json[i]) &&
                   json[i] != ',' && json[i] != '}' && json[i] != ']' && json[i] != ':')
            {
                i++;
            }
            tokens.Add(json.Substring(start, i - start));
        }

        return tokens;
    }

    #endregion
}
