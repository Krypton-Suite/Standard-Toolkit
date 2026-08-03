#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Merges two Translations.xml files. Values present in the overlay
/// document take precedence; strings only in the baseline are carried through.
/// Useful when upgrading the toolkit: export a fresh template as the baseline, then overlay
/// the user's customised file to produce a merged result.
/// </summary>
public static class ToolkitStringsXmlMerge
{
    /// <summary>
    /// Merges two Translations.xml files. The <paramref name="overlayPath"/> file's values
    /// take precedence over <paramref name="baselinePath"/>.
    /// </summary>
    /// <param name="baselinePath">Path to the baseline (e.g. fresh template) file.</param>
    /// <param name="overlayPath">Path to the overlay (e.g. user customisation) file.</param>
    /// <param name="outputPath">Path to write the merged result.</param>
    public static void MergeFiles(string baselinePath, string overlayPath, string outputPath)
    {
        if (string.IsNullOrWhiteSpace(baselinePath))
        {
            throw new ArgumentNullException(nameof(baselinePath));
        }

        if (string.IsNullOrWhiteSpace(overlayPath))
        {
            throw new ArgumentNullException(nameof(overlayPath));
        }

        if (string.IsNullOrWhiteSpace(outputPath))
        {
            throw new ArgumentNullException(nameof(outputPath));
        }

        var baseline = new XmlDocument();
        baseline.Load(baselinePath);

        var overlay = new XmlDocument();
        overlay.Load(overlayPath);

        var merged = Merge(baseline, overlay);
        merged.Save(outputPath);
    }

    /// <summary>
    /// Merges two Translations.xml documents in memory. Values from <paramref name="overlay"/>
    /// overwrite matching entries in <paramref name="baseline"/>.
    /// </summary>
    /// <param name="baseline">The baseline document.</param>
    /// <param name="overlay">The overlay document whose values take precedence.</param>
    /// <returns>A new <see cref="System.Xml.XmlDocument"/> containing the merged result.</returns>
    public static XmlDocument Merge(XmlDocument baseline, XmlDocument overlay)
    {
        if (baseline == null)
        {
            throw new ArgumentNullException(nameof(baseline));
        }

        if (overlay == null)
        {
            throw new ArgumentNullException(nameof(overlay));
        }

        // Deep-clone the baseline so the caller's document is not modified.
        var result = (XmlDocument)baseline.CloneNode(deep: true);

        var resultRoot = result.SelectSingleNode(@"KryptonTranslations") as XmlElement;
        var overlayRoot = overlay.SelectSingleNode(@"KryptonTranslations") as XmlElement;

        if (resultRoot == null || overlayRoot == null)
        {
            return result;
        }

        MergeElements(result, resultRoot, overlayRoot);
        return result;
    }

    private static void MergeElements(XmlDocument doc, XmlElement target, XmlElement source)
    {
        foreach (XmlNode sourceChild in source.ChildNodes)
        {
            if (sourceChild is not XmlElement sourceEl)
            {
                continue;
            }

            var targetEl = target.SelectSingleNode(sourceEl.Name) as XmlElement;
            if (targetEl == null)
            {
                // New element — import it.
                var imported = doc.ImportNode(sourceEl, deep: true);
                target.AppendChild(imported);
            }
            else if (sourceEl.HasAttribute(@"Value"))
            {
                // Leaf value node — overwrite.
                targetEl.SetAttribute(@"Value", sourceEl.GetAttribute(@"Value"));
                if (sourceEl.HasAttribute(@"IsNull"))
                {
                    targetEl.SetAttribute(@"IsNull", sourceEl.GetAttribute(@"IsNull"));
                }
                else
                {
                    targetEl.RemoveAttribute(@"IsNull");
                }
            }
            else
            {
                // Container — recurse.
                MergeElements(doc, targetEl, sourceEl);
            }
        }
    }
}
