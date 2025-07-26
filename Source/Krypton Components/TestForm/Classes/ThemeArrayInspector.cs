#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.Text.RegularExpressions;

namespace Classes;

/// <summary>
/// Provides static helper methods for analysing palette source files and detecting
/// discrepancies between the _schemeOfficeColors array and the SchemeOfficeColors enum order.
/// Ported from the ThemeArrayEnumDiff console utility but adapted to return structured data
/// instead of writing to the console.
/// </summary>
internal static class ThemeArrayInspector
{
    private const string EnumFileRelativePath = "Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Enumerations/PaletteEnumerations.cs";
    private const string EnumName = "SchemeBaseColors";
    // Some palette classes use alternate array identifiers (e.g. the Visual Studio themes).
    // Maintain a list of acceptable array variable names to search for, in priority order.
    private static readonly string[] TargetArrayNames =
    [
        "_schemeBaseColors",          // default for Office, Microsoft 365, etc.
        "_schemeVisualStudioColors"    // used by Visual Studio 2010 *Variation themes
    ];

    private static IReadOnlyList<string> _cachedEnumNames;

    // Menu special entries that may be ignored for certain outputs (kept for parity with original logic).
    private static readonly HashSet<string> MenuNames = new HashSet<string>(new[]
    {
        "MenuItemText",
        "MenuMarginGradientStart",
        "MenuMarginGradientMiddle",
        "MenuMarginGradientEnd",
        "DisabledMenuItemText",
        "MenuStripText"
    }, StringComparer.Ordinal);

    /// <summary>
    /// Performs the discrepancy analysis for a palette type's source file.
    /// </summary>
    /// <param name="paletteType">Type of the runtime palette (e.g., PaletteOffice2010Blue).</param>
    /// <param name="sourceRoot">Root folder that contains the Krypton Toolkit source tree.</param>
    /// <returns>PaletteArrayIssues; returns null if file could not be located or parsed.</returns>
    public static PaletteArrayIssues? GetIssues(Type paletteType, string sourceRoot)
    {
        if (paletteType == null
            || string.IsNullOrWhiteSpace(sourceRoot)
            || !Directory.Exists(sourceRoot))
        {
            return null; // invalid path – silently ignore
        }

        EnsureEnumNamesCached(sourceRoot);

        string fileName = paletteType.Name + ".cs";

        // Search recursively for the palette file – constrained under Palette Builtin
        string paletteDir = Path.Combine(sourceRoot, "Source");
        if (!Directory.Exists(paletteDir))
        {
            // Fallback: search entire tree under provided root
            paletteDir = sourceRoot;
        }

        string paletteBuiltinSegment = Path.Combine("Palette Builtin", string.Empty);
        var matches = Directory.EnumerateFiles(paletteDir, fileName, SearchOption.AllDirectories)
            .Where(f => f.IndexOf(paletteBuiltinSegment, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        string? palettePath = matches.FirstOrDefault();
        if (palettePath == null)
        {
            return null; // file not found
        }

        List<string>? arrayNames = null;

        // Attempt extraction using each known array variable name until one yields results.
        foreach (var arrayVar in TargetArrayNames)
        {
            try
            {
                arrayNames = ExtractArrayComments(palettePath, arrayVar);
            }
            catch (IOException)
            {
                return null; // I/O error while reading – abort
            }

            if (arrayNames != null && arrayNames.Count > 0)
            {
                break; // success
            }
        }

        // If still no entries, we cannot analyse this palette.
        if (arrayNames == null || arrayNames.Count == 0)
        {
            return null;
        }

        var issues = Compare(_cachedEnumNames, arrayNames);
        return issues;
    }

    private static void EnsureEnumNamesCached(string sourceRoot)
    {
        if (_cachedEnumNames != null) return;

        string enumPath = Path.Combine(sourceRoot, EnumFileRelativePath.Replace('/', Path.DirectorySeparatorChar));
        if (!File.Exists(enumPath))
        {
            _cachedEnumNames = Array.Empty<string>();
            return;
        }

        _cachedEnumNames = ExtractEnumNames(enumPath, EnumName);
    }

    private static PaletteArrayIssues Compare(IReadOnlyList<string> enumNames, List<string> arrayNames)
    {
        var result = new PaletteArrayIssues();

        if (enumNames == null || enumNames.Count == 0)
        {
            return result; // nothing to compare against
        }

        // Build lookup for array names (first occurrence wins)
        var arrayLookup = new Dictionary<string, int>(StringComparer.Ordinal);
        for (int i = 0; i < arrayNames.Count; i++)
        {
            var name = arrayNames[i];
            if (string.IsNullOrEmpty(name)) continue;
            if (!arrayLookup.ContainsKey(name))
            {
                arrayLookup[name] = i;
            }
        }

        int? currentDelta = null;

        for (int i = 0; i < enumNames.Count; i++)
        {
            var enumName = enumNames[i];
            bool isMenu = MenuNames.Contains(enumName);

            if (!arrayLookup.TryGetValue(enumName, out var arrIndex))
            {
                bool unlabeledHere = i < arrayNames.Count && string.IsNullOrEmpty(arrayNames[i]);
                if (!isMenu)
                {
                    if (unlabeledHere)
                    {
                        result.UnlabelledCount++;
                        result.UnlabelledIndices.Add(i);
                    }
                    else
                    {
                        result.MissingCount++;
                        result.MissingIndices.Add(i);
                    }
                }
            }
            else if (arrIndex != i)
            {
                int diff = arrIndex - i;
                if (currentDelta != diff && !isMenu)
                {
                    result.OutOfOrderCount++;
                    result.OutOfOrderIndices.Add(arrIndex);
                    currentDelta = diff;
                }
            }
        }

        // Extras – names in array not in enum
        var extras = arrayLookup.Keys.Except(enumNames, StringComparer.Ordinal).ToList();
        foreach (var extra in extras)
        {
            if (MenuNames.Contains(extra))
            {
                continue;
            }
            result.ExtraCount++;
            result.ExtraIndices.Add(arrayLookup[extra]);
        }

        return result;
    }

    #region Parsing helpers (ported)

    private static List<string> ExtractEnumNames(string filePath, string enumName)
    {
        var lines = File.ReadAllLines(filePath);
        var names = new List<string>();
        bool inEnum = false;
        foreach (var line in lines)
        {
            if (!inEnum)
            {
                if (line.Contains($"enum {enumName}"))
                    inEnum = true;
            }
            else
            {
                if (line.Contains("}")) break;
                var match = Regex.Match(line, @"^\s*([A-Za-z0-9_]+)\s*(=\s*\d+)?\s*,?");
                if (match.Success)
                {
                    names.Add(match.Groups[1].Value.Trim());
                }
            }
        }
        return names;
    }

    private static List<string> ExtractArrayComments(string filePath, string arrayName)
    {
        var lines = File.ReadAllLines(filePath);
        var names = new List<string>();
        bool inArray = false;
        foreach (var line in lines)
        {
            if (!inArray)
            {
                if (line.Contains($"{arrayName} ="))
                    inArray = true;
            }
            else
            {
                // Skip lines that are completely commented out (e.g., //Color.FromArgb(...))
                var trimmed = line.TrimStart();
                if (trimmed.StartsWith("//", StringComparison.Ordinal))
                {
                    continue;
                }

                if (line.Contains("];") || line.Contains("};")) break;

                int idx = line.LastIndexOf("//", StringComparison.Ordinal);
                if (idx >= 0)
                {
                    var commentPart = line.Substring(idx + 2);
                    var match = Regex.Match(commentPart, @"^\s*([A-Za-z0-9_]+)");
                    if (match.Success)
                    {
                        names.Add(Normalize(match.Groups[1].Value.Trim()));
                        continue;
                    }
                }

                if (line.Contains(")") || line.Contains("EMPTY_COLOR"))
                {
                    names.Add(string.Empty);
                }
            }
        }
        return names;
    }

    private static string Normalize(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return token;

        var aliases = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            {"ButtonNormalBorder1", "ButtonNormalBorder"},
            {"ButtonNormalBorder2", "ButtonNormalDefaultBorder"},
            {"ContextMenuHeading", "ContextMenuHeadingBack"},
            {"AppButtonMenuDocs", "AppButtonMenuDocsBack"}
        };

        if (aliases.TryGetValue(token, out var mapped))
        {
            token = mapped;
        }

        token = token.Replace("Inctive", "Inactive");

        if (token.Equals("Color", StringComparison.OrdinalIgnoreCase) ||
            token.Equals("FromArgb", StringComparison.OrdinalIgnoreCase))
        {
            return string.Empty;
        }

        return token;
    }

    #endregion
}