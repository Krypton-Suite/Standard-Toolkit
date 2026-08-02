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
/// Merges two <c>KryptonCustomTranslations</c> XML documents.
/// Values present in the overlay document take precedence.
/// </summary>
public static class KryptonCustomStringsXmlMerge
{
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

        Merge(baseline, overlay).Save(outputPath);
    }

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

        var result = (XmlDocument)baseline.CloneNode(deep: true);
        var resultRoot = result.SelectSingleNode(@"KryptonCustomTranslations") as XmlElement;
        var overlayRoot = overlay.SelectSingleNode(@"KryptonCustomTranslations") as XmlElement;
        if (resultRoot == null || overlayRoot == null)
        {
            return result;
        }

        MergeValues(resultRoot, overlayRoot);
        MergeStringSets(result, resultRoot, overlayRoot);
        return result;
    }

    private static void MergeValues(XmlElement targetRoot, XmlElement sourceRoot)
    {
        var targetValues = targetRoot.SelectSingleNode(@"Values") as XmlElement;
        var sourceValues = sourceRoot.SelectSingleNode(@"Values") as XmlElement;
        if (targetValues == null || sourceValues == null)
        {
            return;
        }

        foreach (XmlNode sourceNode in sourceValues.ChildNodes)
        {
            if (sourceNode is not XmlElement sourceEl || sourceEl.Name != @"String")
            {
                continue;
            }

            var key = sourceEl.GetAttribute(@"Key");
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            XmlElement? targetEl = null;
            foreach (XmlNode targetNode in targetValues.ChildNodes)
            {
                if (targetNode is XmlElement candidate &&
                    candidate.Name == @"String" &&
                    string.Equals(candidate.GetAttribute(@"Key"), key, StringComparison.Ordinal))
                {
                    targetEl = candidate;
                    break;
                }
            }

            if (targetEl == null)
            {
                targetValues.AppendChild(targetRoot.OwnerDocument!.ImportNode(sourceEl, deep: true));
            }
            else
            {
                targetEl.SetAttribute(@"Value", sourceEl.GetAttribute(@"Value"));
            }
        }
    }

    private static void MergeStringSets(XmlDocument doc, XmlElement targetRoot, XmlElement sourceRoot)
    {
        var targetSets = targetRoot.SelectSingleNode(@"StringSets") as XmlElement;
        var sourceSets = sourceRoot.SelectSingleNode(@"StringSets") as XmlElement;
        if (targetSets == null || sourceSets == null)
        {
            return;
        }

        foreach (XmlNode sourceNode in sourceSets.ChildNodes)
        {
            if (sourceNode is not XmlElement sourceEl || sourceEl.Name != @"StringSet")
            {
                continue;
            }

            var name = sourceEl.GetAttribute(@"Name");
            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            XmlElement? targetEl = null;
            foreach (XmlNode targetNode in targetSets.ChildNodes)
            {
                if (targetNode is XmlElement candidate &&
                    candidate.Name == @"StringSet" &&
                    string.Equals(candidate.GetAttribute(@"Name"), name, StringComparison.Ordinal))
                {
                    targetEl = candidate;
                    break;
                }
            }

            if (targetEl == null)
            {
                targetSets.AppendChild(doc.ImportNode(sourceEl, deep: true));
                continue;
            }

            foreach (XmlNode propNode in sourceEl.ChildNodes)
            {
                if (propNode is not XmlElement propEl || !propEl.HasAttribute(@"Value"))
                {
                    continue;
                }

                var targetProp = targetEl.SelectSingleNode(propEl.Name) as XmlElement;
                if (targetProp == null)
                {
                    targetEl.AppendChild(doc.ImportNode(propEl, deep: true));
                }
                else
                {
                    targetProp.SetAttribute(@"Value", propEl.GetAttribute(@"Value"));
                    if (propEl.HasAttribute(@"IsNull"))
                    {
                        targetProp.SetAttribute(@"IsNull", propEl.GetAttribute(@"IsNull"));
                    }
                    else
                    {
                        targetProp.RemoveAttribute(@"IsNull");
                    }
                }
            }
        }
    }
}
