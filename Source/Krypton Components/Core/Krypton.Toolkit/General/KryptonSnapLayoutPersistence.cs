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
/// Versioned XML export and import for <see cref="SnapLayoutValues.CustomLayouts"/>.
/// </summary>
internal static class KryptonSnapLayoutPersistence
{
    private const string RootName = @"KryptonSnapCustomLayouts";
    private const string VersionAttribute = @"version";
    private const string CurrentVersion = @"1";
    private const string IncludeBuiltinElement = @"IncludeBuiltinLayouts";
    private const string CardElement = @"Card";
    private const string ZoneElement = @"Zone";
    private const string AttrX = @"X";
    private const string AttrY = @"Y";
    private const string AttrWidth = @"Width";
    private const string AttrHeight = @"Height";

    /// <summary>
    /// Builds a versioned XML document for the custom cards and builtin-include flag.
    /// </summary>
    /// <param name="cards">Live custom card collection.</param>
    /// <param name="includeBuiltinLayouts">Current <see cref="SnapLayoutValues.IncludeBuiltinLayouts"/> value.</param>
    /// <returns>UTF-8 XML text.</returns>
    internal static string Export(KryptonSnapLayoutCardCollection cards, bool includeBuiltinLayouts)
    {
        if (cards == null)
        {
            throw new ArgumentNullException(nameof(cards));
        }

        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = @"  ",
            Encoding = new UTF8Encoding(false),
            OmitXmlDeclaration = false
        };

        // MemoryStream + UTF-8 so the XML declaration matches the file encoding (StringWriter is UTF-16).
        using (var stream = new MemoryStream())
        {
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(RootName);
                writer.WriteAttributeString(VersionAttribute, CurrentVersion);
                writer.WriteElementString(IncludeBuiltinElement, includeBuiltinLayouts ? @"true" : @"false");

                for (int i = 0; i < cards.Count; i++)
                {
                    KryptonSnapLayoutCard card = cards[i];
                    if (card == null || card.Zones.Count == 0)
                    {
                        continue;
                    }

                    writer.WriteStartElement(CardElement);
                    for (int z = 0; z < card.Zones.Count; z++)
                    {
                        KryptonSnapLayoutZone zone = card.Zones[z];
                        if (zone == null || zone.Width <= 0f || zone.Height <= 0f)
                        {
                            continue;
                        }

                        writer.WriteStartElement(ZoneElement);
                        writer.WriteAttributeString(AttrX, FormatFloat(zone.X));
                        writer.WriteAttributeString(AttrY, FormatFloat(zone.Y));
                        writer.WriteAttributeString(AttrWidth, FormatFloat(zone.Width));
                        writer.WriteAttributeString(AttrHeight, FormatFloat(zone.Height));
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    /// <summary>
    /// Parses <paramref name="xml"/> into cards and the builtin-include flag.
    /// </summary>
    /// <param name="xml">Document previously produced by <see cref="Export"/>. Null or whitespace means reset.</param>
    /// <param name="cards">Parsed cards when the return value is not a reset.</param>
    /// <param name="includeBuiltinLayouts">Parsed flag, or <see langword="true"/> on reset.</param>
    /// <returns><see langword="true"/> when the document should replace the live collection; <see langword="false"/> when the caller should reset to defaults.</returns>
    internal static bool TryImport(string? xml, out List<KryptonSnapLayoutCard> cards, out bool includeBuiltinLayouts)
    {
        cards = new List<KryptonSnapLayoutCard>();
        includeBuiltinLayouts = true;
        if (string.IsNullOrWhiteSpace(xml))
        {
            return false;
        }

        var document = new XmlDocument();
        try
        {
            document.LoadXml(xml!);
        }
        catch (XmlException ex)
        {
            throw new ArgumentException(@"The snap-layout document is not valid XML.", nameof(xml), ex);
        }

        XmlElement? root = document.DocumentElement;
        if (root == null || !string.Equals(root.Name, RootName, StringComparison.Ordinal))
        {
            throw new ArgumentException(@"The snap-layout document root must be KryptonSnapCustomLayouts.", nameof(xml));
        }

        string? version = root.GetAttribute(VersionAttribute);
        if (string.IsNullOrEmpty(version))
        {
            throw new ArgumentException(@"The snap-layout document is missing a version attribute.", nameof(xml));
        }

        if (!string.Equals(version, CurrentVersion, StringComparison.Ordinal))
        {
            throw new InvalidOperationException(@"Unsupported snap-layout document version '" + version + @"'.");
        }

        XmlNode? includeNode = root[IncludeBuiltinElement];
        if (includeNode != null && !string.IsNullOrWhiteSpace(includeNode.InnerText))
        {
            includeBuiltinLayouts = !string.Equals(includeNode.InnerText.Trim(), @"false", StringComparison.OrdinalIgnoreCase);
        }

        foreach (XmlNode child in root.ChildNodes)
        {
            if (child.NodeType != XmlNodeType.Element
                || !string.Equals(child.Name, CardElement, StringComparison.Ordinal))
            {
                continue;
            }

            var zones = new List<KryptonSnapLayoutZone>();
            foreach (XmlNode zoneNode in child.ChildNodes)
            {
                if (zoneNode.NodeType != XmlNodeType.Element
                    || !string.Equals(zoneNode.Name, ZoneElement, StringComparison.Ordinal))
                {
                    continue;
                }

                if (!(zoneNode is XmlElement zoneElement))
                {
                    continue;
                }

                if (!TryReadZone(zoneElement, out KryptonSnapLayoutZone? zone) || zone == null)
                {
                    continue;
                }

                zones.Add(zone);
            }

            if (zones.Count == 0)
            {
                continue;
            }

            cards.Add(new KryptonSnapLayoutCard(zones.ToArray(), true));
        }

        return true;
    }

    private static bool TryReadZone(XmlElement element, out KryptonSnapLayoutZone? zone)
    {
        zone = null;
        if (!TryParseFloat(element.GetAttribute(AttrX), out float x)
            || !TryParseFloat(element.GetAttribute(AttrY), out float y)
            || !TryParseFloat(element.GetAttribute(AttrWidth), out float width)
            || !TryParseFloat(element.GetAttribute(AttrHeight), out float height))
        {
            return false;
        }

        try
        {
            zone = new KryptonSnapLayoutZone(x, y, width, height);
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    private static bool TryParseFloat(string? text, out float value)
    {
        value = 0f;
        return !string.IsNullOrWhiteSpace(text)
               && float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
    }

    private static string FormatFloat(float value) =>
        value.ToString(@"0.########", CultureInfo.InvariantCulture);
}
