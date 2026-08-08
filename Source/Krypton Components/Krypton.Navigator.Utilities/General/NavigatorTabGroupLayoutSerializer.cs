#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// XML persistence helpers for browser-style tab group catalogs and navigator page order/membership.
/// </summary>
public static class NavigatorTabGroupLayoutSerializer
{
    private const string RootElement = @"KNFI";
    private const string GroupsElement = @"NTG";
    private const string GroupElement = @"G";
    private const string PagesElement = @"Pages";
    private const string PageElement = @"P";
    private const string SelectedElement = @"Selected";

    /// <summary>
    /// Writes the tab-group catalog into the current XML writer position.
    /// </summary>
    public static void WriteGroups(XmlWriter xmlWriter, NavigatorTabGroupCollection groups)
    {
        if (xmlWriter == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(xmlWriter));
        }

        if (groups == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(groups));
        }

        xmlWriter.WriteStartElement(GroupsElement);
        foreach (NavigatorTabGroup group in groups)
        {
            xmlWriter.WriteStartElement(GroupElement);
            XmlHelper.TextToXmlAttribute(xmlWriter, @"Id", group.Id);
            XmlHelper.TextToXmlAttribute(xmlWriter, @"Title", group.Title);
            XmlHelper.TextToXmlAttribute(xmlWriter, @"C", CommonHelper.ColorToString(group.Color) ?? string.Empty);
            XmlHelper.TextToXmlAttribute(xmlWriter, @"Collapsed", CommonHelper.BoolToString(group.Collapsed), @"False");
            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndElement();
    }

    /// <summary>
    /// Reads an <c>NTG</c> group catalog element and replaces the target collection contents.
    /// </summary>
    /// <remarks>
    /// Expects the reader to be positioned on the <c>NTG</c> start element.
    /// </remarks>
    public static void ReadGroups(XmlReader xmlReader, NavigatorTabGroupCollection groups)
    {
        if (xmlReader == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(xmlReader));
        }

        if (groups == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(groups));
        }

        if (xmlReader.Name != GroupsElement)
        {
            ThrowHelper.ThrowArgumentException(@"Expected 'NTG' element was not found.", nameof(xmlReader));
        }

        groups.Clear();
        var empty = xmlReader.IsEmptyElement;
        if (!empty)
        {
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == GroupsElement)
                {
                    break;
                }

                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == GroupElement)
                {
                    var group = new NavigatorTabGroup(
                        XmlHelper.XmlAttributeToText(xmlReader, @"Id"),
                        XmlHelper.XmlAttributeToText(xmlReader, @"Title"),
                        CommonHelper.StringToColor(XmlHelper.XmlAttributeToText(xmlReader, @"C", @"DodgerBlue")))
                    {
                        Collapsed = CommonHelper.StringToBool(XmlHelper.XmlAttributeToText(xmlReader, @"Collapsed", @"False"))
                    };
                    groups.Add(group);
                }
            }
        }
    }

    /// <summary>
    /// Saves navigator page order, TabGroupId membership, selected page, and the group catalog.
    /// </summary>
    public static void SaveNavigatorLayout(XmlWriter xmlWriter, KryptonNavigator navigator, NavigatorTabGroupCollection groups)
    {
        if (xmlWriter == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(xmlWriter));
        }

        if (navigator == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(navigator));
        }

        if (groups == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(groups));
        }

        xmlWriter.WriteStartElement(RootElement);
        xmlWriter.WriteAttributeString(@"V", @"1");

        WriteGroups(xmlWriter, groups);

        xmlWriter.WriteStartElement(PagesElement);
        foreach (KryptonPage page in navigator.Pages)
        {
            xmlWriter.WriteStartElement(PageElement);
            XmlHelper.TextToXmlAttribute(xmlWriter, @"UN", page.UniqueName);
            XmlHelper.TextToXmlAttribute(xmlWriter, @"TG", page.TabGroupId, string.Empty);
            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(SelectedElement);
        XmlHelper.TextToXmlAttribute(xmlWriter, @"UN", navigator.SelectedPage?.UniqueName ?? string.Empty);
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();
    }

    /// <summary>
    /// Loads group catalog, page order, TabGroupId membership, and selected page onto an existing navigator.
    /// </summary>
    /// <remarks>
    /// Pages are matched by <see cref="KryptonPage.UniqueName"/>. Unknown unique names are skipped.
    /// Pages present in the navigator but missing from the layout keep their content but are moved after restored pages.
    /// </remarks>
    public static void LoadNavigatorLayout(XmlReader xmlReader, KryptonNavigator navigator, NavigatorTabGroupCollection groups)
    {
        if (xmlReader == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(xmlReader));
        }

        if (navigator == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(navigator));
        }

        if (groups == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(groups));
        }

        if (xmlReader.Name != RootElement)
        {
            ThrowHelper.ThrowArgumentException(@"Expected 'KNFI' element was not found.", nameof(xmlReader));
        }

        if (!xmlReader.Read())
        {
            ThrowHelper.ThrowArgumentException(@"An element was expected but could not be read in.");
        }

        if (xmlReader.Name != GroupsElement)
        {
            ThrowHelper.ThrowArgumentException(@"Expected 'NTG' element was not found.");
        }

        ReadGroups(xmlReader, groups);

        if (!xmlReader.Read())
        {
            ThrowHelper.ThrowArgumentException(@"An element was expected but could not be read in.");
        }

        if (xmlReader.Name != PagesElement)
        {
            ThrowHelper.ThrowArgumentException(@"Expected 'Pages' element was not found.");
        }

        var ordered = new List<(string UniqueName, string TabGroupId)>();
        if (!xmlReader.IsEmptyElement)
        {
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == PagesElement)
                {
                    break;
                }

                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == PageElement)
                {
                    ordered.Add((
                        XmlHelper.XmlAttributeToText(xmlReader, @"UN"),
                        XmlHelper.XmlAttributeToText(xmlReader, @"TG", string.Empty)));
                }
            }
        }

        string selectedUniqueName = string.Empty;
        if (xmlReader.Read() && xmlReader.Name == SelectedElement)
        {
            selectedUniqueName = XmlHelper.XmlAttributeToText(xmlReader, @"UN");
        }

        ApplyPageOrderAndGroups(navigator, ordered, selectedUniqueName);
    }

    private static void ApplyPageOrderAndGroups(
        KryptonNavigator navigator,
        List<(string UniqueName, string TabGroupId)> ordered,
        string selectedUniqueName)
    {
        var byUniqueName = new Dictionary<string, KryptonPage>(StringComparer.Ordinal);
        foreach (KryptonPage page in navigator.Pages)
        {
            if (!string.IsNullOrEmpty(page.UniqueName) && !byUniqueName.ContainsKey(page.UniqueName))
            {
                byUniqueName.Add(page.UniqueName, page);
            }
        }

        var restored = new List<KryptonPage>();
        foreach ((string uniqueName, string tabGroupId) in ordered)
        {
            if (string.IsNullOrEmpty(uniqueName) || !byUniqueName.TryGetValue(uniqueName, out KryptonPage? page))
            {
                continue;
            }

            page.TabGroupId = tabGroupId ?? string.Empty;
            restored.Add(page);
            byUniqueName.Remove(uniqueName);
        }

        // Preserve unmatched pages after restored ones.
        foreach (KryptonPage leftover in navigator.Pages)
        {
            if (!restored.Contains(leftover))
            {
                restored.Add(leftover);
            }
        }

        navigator.Pages.Clear();
        foreach (KryptonPage page in restored)
        {
            navigator.Pages.Add(page);
        }

        if (!string.IsNullOrEmpty(selectedUniqueName))
        {
            foreach (KryptonPage page in navigator.Pages)
            {
                if (string.Equals(page.UniqueName, selectedUniqueName, StringComparison.Ordinal))
                {
                    if (navigator.AllowTabSelect)
                    {
                        navigator.SelectedPage = page;
                    }

                    break;
                }
            }
        }
    }
}
