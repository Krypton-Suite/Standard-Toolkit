#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// JSON twin of <see cref="RibbonTranslationsXmlPersistence"/>. Converts through the XML document so import/export stay in sync.
/// </summary>
internal static class RibbonTranslationsJsonPersistence
{
    public static string Export(KryptonRibbon ribbon, RibbonTranslationOptions? options)
    {
        var doc = RibbonTranslationsXmlPersistence.Export(ribbon, options);
        return XmlToJson(doc);
    }

    public static void ExportToFile(KryptonRibbon ribbon, string filename, RibbonTranslationOptions? options)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        File.WriteAllText(filename, Export(ribbon, options), Encoding.UTF8);
    }

    public static void ExportToStream(KryptonRibbon ribbon, Stream stream, RibbonTranslationOptions? options)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        var bytes = Encoding.UTF8.GetBytes(Export(ribbon, options));
        stream.Write(bytes, 0, bytes.Length);
    }

    public static void ImportFromJson(KryptonRibbon ribbon, string json, RibbonTranslationOptions? options)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            ThrowHelper.ThrowArgumentException(@"JSON content is empty.", nameof(json));
        }

        var doc = JsonToXmlDocument(json);
        RibbonTranslationsXmlPersistence.Import(ribbon, doc, options);
    }

    public static void ImportFromFile(KryptonRibbon ribbon, string filename, RibbonTranslationOptions? options)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        ImportFromJson(ribbon, File.ReadAllText(filename, Encoding.UTF8), options);
    }

    public static void ImportFromStream(KryptonRibbon ribbon, Stream stream, RibbonTranslationOptions? options)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        ImportFromJson(ribbon, reader.ReadToEnd(), options);
    }

    public static RibbonTranslationsCoverage Analyze(KryptonRibbon ribbon, string json, RibbonTranslationOptions? options, string? filePath = null)
    {
        var doc = JsonToXmlDocument(json);
        return RibbonTranslationsXmlPersistence.Analyze(ribbon, doc, options, filePath);
    }

    public static RibbonTranslationsCoverage MergeMissingToFile(KryptonRibbon ribbon, string filename, RibbonTranslationOptions? options)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        options ??= new RibbonTranslationOptions();
        var json = File.ReadAllText(filename, Encoding.UTF8);
        var importOptions = new RibbonTranslationOptions
        {
            IncludeDefaults = options.IncludeDefaults,
            IncludeChrome = options.IncludeChrome,
            IncludeToolTips = options.IncludeToolTips,
            IncludeKeyTips = options.IncludeKeyTips,
            IncludeContentStrings = options.IncludeContentStrings,
            ResetFirst = true,
            ChangeService = options.ChangeService
        };
        ImportFromJson(ribbon, json, importOptions);

        var exportOptions = new RibbonTranslationOptions
        {
            IncludeDefaults = true,
            IncludeChrome = options.IncludeChrome,
            IncludeToolTips = options.IncludeToolTips,
            IncludeKeyTips = options.IncludeKeyTips,
            IncludeContentStrings = options.IncludeContentStrings
        };
        ExportToFile(ribbon, filename, exportOptions);
        var merged = File.ReadAllText(filename, Encoding.UTF8);
        return Analyze(ribbon, merged, exportOptions, filename);
    }

    internal static XmlDocument JsonToXmlDocument(string json)
    {
        var doc = new XmlDocument();
        doc.AppendChild(doc.CreateProcessingInstruction(@"xml", @"version=""1.0"""));
        var root = doc.CreateElement(RibbonTranslationsXmlPersistence.RootElementName);
        doc.AppendChild(root);

        var tokens = Tokenize(json);
        var pos = 0;
        ParseObject(tokens, ref pos, doc, root);
        PromoteToAttribute(root, @"Version");
        PromoteToAttribute(root, @"Culture");
        PromoteToAttribute(root, @"Generated");
        PromoteToAttribute(root, @"ToolkitVersion");
        PromoteToAttribute(root, @"RibbonName");
        PromoteIdentityAttributes(root);
        return doc;
    }

    private static string XmlToJson(XmlDocument doc)
    {
        var sb = new StringBuilder(4096);
        WriteElementAsObject(sb, doc.DocumentElement!, indent: 0, trailingComma: false, isRoot: true);
        sb.AppendLine();
        return sb.ToString();
    }

    private static void WriteElementAsObject(StringBuilder sb, XmlElement element, int indent, bool trailingComma, bool isRoot)
    {
        var pad = new string(' ', indent * 2);
        sb.Append(pad);
        sb.AppendLine(@"{");

        var members = new List<Action<bool>>();

        if (isRoot)
        {
            foreach (XmlAttribute attr in element.Attributes)
            {
                var name = attr.Name;
                var value = attr.Value;
                members.Add(comma => WriteJsonProperty(sb, indent + 1, name, value, isNumber: name == @"Version", comma));
            }
        }
        else
        {
            foreach (XmlAttribute attr in element.Attributes)
            {
                if (attr.Name == @"Value" || attr.Name == @"IsNull")
                {
                    continue;
                }

                var name = attr.Name;
                var value = attr.Value;
                members.Add(comma => WriteJsonProperty(sb, indent + 1, name, value, isNumber: false, comma));
            }
        }

        var groups = new Dictionary<string, List<XmlElement>>(StringComparer.Ordinal);
        foreach (XmlNode node in element.ChildNodes)
        {
            if (node is XmlElement child)
            {
                if (!groups.TryGetValue(child.Name, out var list))
                {
                    list = new List<XmlElement>();
                    groups[child.Name] = list;
                }

                list.Add(child);
            }
        }

        foreach (var pair in groups)
        {
            var name = pair.Key;
            var list = pair.Value;
            members.Add(comma => WriteChildGroup(sb, indent + 1, name, list, comma));
        }

        for (var i = 0; i < members.Count; i++)
        {
            members[i](i < members.Count - 1);
        }

        sb.Append(pad);
        sb.Append('}');
        if (trailingComma)
        {
            sb.Append(',');
        }
    }

    private static void WriteChildGroup(StringBuilder sb, int indent, string name, List<XmlElement> items, bool comma)
    {
        var pad = new string(' ', indent * 2);
        sb.Append(pad);
        sb.Append('"');
        sb.Append(EscapeJsonString(name));
        sb.Append(@""": ");

        if (items.Count == 1 && IsValueLeaf(items[0]))
        {
            WriteLeafValue(sb, items[0]);
            sb.AppendLine(comma ? @"," : string.Empty);
            return;
        }

        if (items.Count == 1)
        {
            sb.AppendLine();
            WriteElementAsObject(sb, items[0], indent, trailingComma: comma, isRoot: false);
            sb.AppendLine();
            return;
        }

        sb.AppendLine(@"[");
        for (var i = 0; i < items.Count; i++)
        {
            if (IsValueLeaf(items[i]) && items[i].Attributes.Count <= 2)
            {
                var itemPad = new string(' ', (indent + 1) * 2);
                sb.Append(itemPad);
                WriteLeafValue(sb, items[i]);
                sb.AppendLine(i < items.Count - 1 ? @"," : string.Empty);
            }
            else
            {
                WriteElementAsObject(sb, items[i], indent + 1, trailingComma: i < items.Count - 1, isRoot: false);
                sb.AppendLine();
            }
        }

        sb.Append(pad);
        sb.Append(']');
        sb.AppendLine(comma ? @"," : string.Empty);
    }

    private static bool IsValueLeaf(XmlElement element) =>
        !element.HasChildNodes && element.HasAttribute(@"Value");

    private static void WriteLeafValue(StringBuilder sb, XmlElement element)
    {
        if (string.Equals(element.GetAttribute(@"IsNull"), @"true", StringComparison.OrdinalIgnoreCase))
        {
            sb.Append(@"null");
            return;
        }

        sb.Append('"');
        sb.Append(EscapeJsonString(element.GetAttribute(@"Value")));
        sb.Append('"');
    }

    private static void WriteJsonProperty(StringBuilder sb, int indent, string name, string value, bool isNumber, bool comma)
    {
        var pad = new string(' ', indent * 2);
        sb.Append(pad);
        sb.Append('"');
        sb.Append(EscapeJsonString(name));
        sb.Append(@""": ");
        if (isNumber && int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
        {
            sb.Append(value);
        }
        else
        {
            sb.Append('"');
            sb.Append(EscapeJsonString(value));
            sb.Append('"');
        }

        sb.AppendLine(comma ? @"," : string.Empty);
    }

    private static string EscapeJsonString(string s) =>
        s.Replace(@"\", @"\\")
            .Replace(@"""", @"\""")
            .Replace("\n", @"\n")
            .Replace("\r", @"\r")
            .Replace("\t", @"\t");

    private static void PromoteIdentityAttributes(XmlElement element)
    {
        PromoteToAttribute(element, @"TranslationId");
        PromoteToAttribute(element, @"Name");
        PromoteToAttribute(element, @"Type");
        PromoteToAttribute(element, @"Index");
        PromoteToAttribute(element, @"UniqueName");
        PromoteToAttribute(element, @"ContextName");

        var children = new List<XmlElement>();
        foreach (XmlNode child in element.ChildNodes)
        {
            if (child is XmlElement childElement)
            {
                children.Add(childElement);
            }
        }

        foreach (var child in children)
        {
            PromoteIdentityAttributes(child);
        }
    }

    private static void PromoteToAttribute(XmlElement root, string name)
    {
        var child = root.SelectSingleNode(name) as XmlElement;
        if (child == null)
        {
            return;
        }

        root.SetAttribute(name, child.HasAttribute(@"Value") ? child.GetAttribute(@"Value") : child.InnerText);
        root.RemoveChild(child);
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
            pos++;
            if (pos < tokens.Count && tokens[pos] == @":")
            {
                pos++;
            }

            if (pos < tokens.Count && tokens[pos] == @"{")
            {
                var container = doc.CreateElement(SanitizeName(key));
                parent.AppendChild(container);
                ParseObject(tokens, ref pos, doc, container);
            }
            else if (pos < tokens.Count && tokens[pos] == @"[")
            {
                ParseArray(tokens, ref pos, doc, parent, SanitizeName(key));
            }
            else if (pos < tokens.Count)
            {
                var valueToken = tokens[pos];
                pos++;
                var child = doc.CreateElement(SanitizeName(key));
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

    private static void ParseArray(List<string> tokens, ref int pos, XmlDocument doc, XmlElement parent, string itemName)
    {
        if (pos < tokens.Count && tokens[pos] == @"[")
        {
            pos++;
        }

        while (pos < tokens.Count && tokens[pos] != @"]")
        {
            if (tokens[pos] == @"{")
            {
                var child = doc.CreateElement(itemName);
                parent.AppendChild(child);
                ParseObject(tokens, ref pos, doc, child);
            }
            else
            {
                var valueToken = tokens[pos];
                pos++;
                var child = doc.CreateElement(itemName);
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

        if (pos < tokens.Count && tokens[pos] == @"]")
        {
            pos++;
        }
    }

    private static string SanitizeName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return @"Item";
        }

        var first = name[0];
        if (first != '_' && !char.IsLetter(first))
        {
            return @"Item";
        }

        return name;
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
}
