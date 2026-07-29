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
/// Exports and imports a combined translations artifact containing both
/// <see cref="KryptonManager.Strings"/> and <see cref="KryptonCustomStrings"/>.
/// </summary>
public static class KryptonCombinedTranslations
{
    private const string RootElementName = @"KryptonCombinedTranslations";
    private const string ToolkitElementName = @"Toolkit";
    private const string CustomElementName = @"Custom";
    private const string VersionAttribute = @"Version";
    private const int CurrentSupportedVersion = 1;

    /// <summary>
    /// Exports built-in toolkit strings plus custom application strings to one XML document.
    /// </summary>
    /// <param name="includeDefaults">When <c>true</c>, includes default values for both built-in and custom typed string sets.</param>
    public static XmlDocument ExportToXmlDocument(bool includeDefaults = false)
    {
        var doc = new XmlDocument();
        doc.AppendChild(doc.CreateProcessingInstruction(@"xml", @"version=""1.0"""));

        var root = doc.CreateElement(RootElementName);
        root.SetAttribute(VersionAttribute, CurrentSupportedVersion.ToString(CultureInfo.InvariantCulture));
        doc.AppendChild(root);

        var toolkitWrapper = doc.CreateElement(ToolkitElementName);
        var toolkitDoc = KryptonManager.Strings.ExportToXmlDocument(includeDefaults);
        if (toolkitDoc.DocumentElement != null)
        {
            toolkitWrapper.AppendChild(doc.ImportNode(toolkitDoc.DocumentElement, deep: true));
        }

        var customWrapper = doc.CreateElement(CustomElementName);
        var customDoc = KryptonCustomStrings.ExportToXmlDocument(includeDefaults);
        if (customDoc.DocumentElement != null)
        {
            customWrapper.AppendChild(doc.ImportNode(customDoc.DocumentElement, deep: true));
        }

        root.AppendChild(toolkitWrapper);
        root.AppendChild(customWrapper);
        return doc;
    }

    /// <summary>
    /// Exports the combined translations artifact to an XML file.
    /// </summary>
    public static void ExportToXmlFile(string filename, bool includeDefaults = false)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        ExportToXmlDocument(includeDefaults).Save(filename);
    }

    /// <summary>
    /// Imports the combined translations artifact from an XML file.
    /// </summary>
    public static void ImportFromXmlFile(string filename, bool resetFirst = true, bool refreshOpenForms = true)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        var doc = new XmlDocument();
        doc.Load(filename);
        ImportFromXmlDocument(doc, resetFirst, refreshOpenForms);
    }

    /// <summary>
    /// Imports the combined translations artifact from an XML document.
    /// </summary>
    public static void ImportFromXmlDocument(XmlDocument doc, bool resetFirst = true, bool refreshOpenForms = true)
    {
        if (doc == null)
        {
            throw new ArgumentNullException(nameof(doc));
        }

        var root = doc.SelectSingleNode(RootElementName) as XmlElement;
        if (root == null)
        {
            throw new ArgumentException($@"Root element must be called '{RootElementName}'.");
        }

        var toolkitRoot = root.SelectSingleNode($@"{ToolkitElementName}/KryptonTranslations") as XmlElement;
        if (toolkitRoot != null)
        {
            var toolkitDoc = new XmlDocument();
            toolkitDoc.AppendChild(toolkitDoc.ImportNode(toolkitRoot, deep: true));
            KryptonManager.Strings.ImportFromXmlDocument(toolkitDoc, resetFirst, refreshOpenForms);
        }

        var customRoot = root.SelectSingleNode($@"{CustomElementName}/KryptonCustomTranslations") as XmlElement;
        if (customRoot != null)
        {
            var customDoc = new XmlDocument();
            customDoc.AppendChild(customDoc.ImportNode(customRoot, deep: true));
            KryptonCustomStrings.ImportFromXmlDocument(customDoc, resetFirst);
        }
    }
}
