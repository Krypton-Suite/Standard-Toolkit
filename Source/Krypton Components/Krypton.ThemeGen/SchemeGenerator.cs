#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.ThemeGen;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class SchemeGenerator
{
    // Header to prepend to all generated scheme files
    private static readonly string LicenseHeader =
"#region BSD License\n" +
"/*\n" +
" *\n" +
" *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)\n" +
" *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.\n" +
" *\n" +
" */\n" +
"#endregion\n";

    private const string BaseColorMarker = "private static readonly Color[] _schemeBaseColors";

    // Marker for track-bar colour arrays found in palette classes
    private const string TrackBarColorMarker = "private static readonly Color[] _trackBarColors";

    public static void Generate(string paletteFile, string outputFolder, bool embedResx, bool dryRun, bool overwrite, bool remove)
    {
        if (string.IsNullOrWhiteSpace(paletteFile)) throw new ArgumentException("paletteFile");

        static bool ShouldSkipPaletteFile(string path)
        {
            var fileName = Path.GetFileName(path);
            // Skip original base palette classes like "PaletteOffice2010Base.cs"
            if (fileName.EndsWith("Base.cs", StringComparison.OrdinalIgnoreCase) && fileName.StartsWith("Palette", StringComparison.OrdinalIgnoreCase)) return true;

            // Skip any previously generated scheme classes
            if (fileName.EndsWith("_BaseScheme.cs", StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }

        var files = EnumeratePaletteFiles(paletteFile).Where(f => !ShouldSkipPaletteFile(f)).ToArray();
        if (files.Length == 0) throw new FileNotFoundException($"No palette file found matching '{paletteFile}' (after excluding base classes)");

        int ok = 0, fail = 0;
        foreach (var palettePath in files)
        {
            var root = LocateRepoRoot(Path.GetDirectoryName(palettePath)!);
            var enumFile = Path.Combine(root, "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Enumerations", "PaletteEnumerations.cs");
            if (!File.Exists(enumFile))
            {
                Console.Error.WriteLine($"{palettePath}: missing enumeration file");
                fail++;
                continue;
            }
            var enumNames = ParseEnumNames(enumFile);
            if (enumNames.Count == 0) {
                Console.Error.WriteLine($"{palettePath}: enum parse failed");
                fail++;
                continue;
            }
            var colorsRaw = ExtractColorExpressions(palettePath);
            var trackBarColorsRaw = ExtractColorExpressions(palettePath, TrackBarColorMarker);

            // If no arrays found at all, report and continue
            if (colorsRaw.Count == 0 && trackBarColorsRaw.Count == 0)
            {
                Console.Error.WriteLine($"{palettePath}: no scheme arrays found");
                fail++;
                continue;
            }

            List<string> alignedColors = new();
            List<bool> missingFlags = new();
            string? destPath = null;

            var outputDir = string.IsNullOrWhiteSpace(outputFolder)
                ? Path.Combine(Path.GetDirectoryName(palettePath)!, "Schemes")
                : Path.GetFullPath(outputFolder);
            Directory.CreateDirectory(outputDir);

            // Align base colours first (may be empty)
            var commentNames = ExtractArrayComments(palettePath);
            var maxLen = Math.Max(colorsRaw.Count, commentNames.Count);
            while (colorsRaw.Count < maxLen) colorsRaw.Add("GlobalStaticValues.EMPTY_COLOR");
            while (commentNames.Count < maxLen) commentNames.Add(string.Empty);

            AlignColors(enumNames, colorsRaw, commentNames, out alignedColors, out missingFlags);

            // Overlay track-bar colours onto the aligned list
            if (trackBarColorsRaw.Count > 0)
            {
                string[] trackBarEnumNames =
                {
                    "TrackBarTickMarks",
                    "TrackBarTopTrack",
                    "TrackBarBottomTrack",
                    "TrackBarFillTrack",
                    "TrackBarOutsidePosition",
                    "TrackBarBorderPosition"
                };

                for (int i = 0; i < trackBarEnumNames.Length; i++)
                {
                    var idx = enumNames.IndexOf(trackBarEnumNames[i]);
                    if (idx >= 0)
                    {
                        if (i < trackBarColorsRaw.Count)
                        {
                            alignedColors[idx] = trackBarColorsRaw[i];
                            missingFlags[idx] = false;
                        }
                    }
                }
            }

            var className = Path.GetFileNameWithoutExtension(palettePath) + "_BaseScheme";
            var code = GenerateSchemeCode(className, alignedColors, enumNames, missingFlags);

            destPath = Path.Combine(outputDir, className + ".cs");

            if (dryRun)
            {
                Console.WriteLine(destPath);
                continue;
            }

            bool existed = File.Exists(destPath);
            bool destWritten = false;
            if (existed && !overwrite)
            {
                Console.WriteLine(destPath + " *File exists, not replaced.");
            }
            else
            {
                File.WriteAllText(destPath, code, Encoding.UTF8);
                Console.WriteLine(destPath + (existed ? " *Overwritten." : string.Empty));
                ok++;
                destWritten = true;
            }

            // Optional removal of processed arrays from the palette source
            if (remove && destWritten && !dryRun)
            {
                if (colorsRaw.Count > 0)
                {
                    RemoveArrayFromFile(palettePath, BaseColorMarker);
                }
                if (trackBarColorsRaw.Count > 0)
                {
                    RemoveArrayFromFile(palettePath, TrackBarColorMarker);
                }

                // Update constructor arguments to use generated scheme + extension method
                UpdateConstructorArguments(palettePath, className);
            }

            // done processing this palette
            continue;
        }

        if (!dryRun)
        {
            if (fail > 0 && ok == 0)
            {
                throw new InvalidOperationException("All palette generations failed");
            }

            if (fail > 0 && ok > 0)
            {
                Console.Error.WriteLine($"{fail} palette files failed");
            }
        }
    }

    private static string LocateRepoRoot(string start)
    {
        var dir = new DirectoryInfo(start);
        DirectoryInfo? lastMatch = null;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "Directory.Build.props")))
            {
                lastMatch = dir;
            }
            dir = dir.Parent;
        }
        return lastMatch?.FullName ?? throw new InvalidOperationException("Unable to locate repo root");
    }

    private static List<string> ParseEnumNames(string enumFile, string enumName = "SchemeBaseColors")
    {
        var result = new List<string>();
        var inside = false;
        foreach (var line in File.ReadLines(enumFile))
        {
            if (!inside)
            {
                if (line.Contains($"enum {enumName}")) inside = true;
                continue;
            }
            if (line.Contains("}")) break;
            var trimmed = line.Trim();
            if (trimmed.StartsWith("//")) continue;
            var m = Regex.Match(trimmed, "^([A-Za-z0-9_]+)\\s*[,=]?");
            if (m.Success) result.Add(m.Groups[1].Value);
        }
        return result;
    }

    private static List<string> ExtractColorExpressions(string palettePath, string marker = BaseColorMarker)
    {
        var lines = File.ReadAllLines(palettePath);
        var start = Array.FindIndex(lines, l => l.Contains(marker));
        if (start < 0) return new List<string>();
        var colors = new List<string>();
        var current = new StringBuilder();
        var paren = 0;
        for (var i = start + 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.Contains("]") && current.Length == 0) break;
            var codePart = line.Split(new[] { "//" }, 2, StringSplitOptions.None)[0].Trim();
            if (codePart.Length == 0 || codePart == "[" || codePart == "]") continue;
            if (current.Length > 0) current.Append(' ');
            current.Append(codePart);
            paren += codePart.Count(c => c == '(') - codePart.Count(c => c == ')');
            if (paren == 0)
            {
                var expr = current.ToString().Trim().TrimEnd(',');
                if (expr.Length > 0) colors.Add(expr);
                current.Clear();
            }
        }
        return colors;
    }

    private static List<string> ExtractArrayComments(string palettePath, string marker = BaseColorMarker)
    {
        var lines = File.ReadAllLines(palettePath);
        var start = Array.FindIndex(lines, l => l.Contains(marker));
        if (start < 0) return new List<string>();

        var names = new List<string>();
        for (var i = start + 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.Contains("]")) break;

            var idx = line.LastIndexOf("//", StringComparison.Ordinal);
            if (idx >= 0)
            {
                var token = line.Substring(idx + 2).Trim();
                var match = Regex.Match(token, "^([A-Za-z0-9_]+)");
                if (match.Success)
                {
                    names.Add(NormalizeComment(match.Groups[1].Value));
                    continue;
                }
            }
            names.Add(string.Empty);
        }
        return names;
    }

    private static readonly HashSet<string> MenuNames = new HashSet<string>(StringComparer.Ordinal)
    {
        "MenuItemText",
        "MenuMarginGradientStart",
        "MenuMarginGradientMiddle",
        "MenuMarginGradientEnd",
        "DisabledMenuItemText",
        "MenuStripText"
    };

    private static string NormalizeComment(string token)
    {
        if (string.IsNullOrEmpty(token)) return token;

        token = token.Replace("Inctive", "Inactive");
        if (token == "ButtonNormalBorder1") return "ButtonNormalBorder";
        if (token == "ButtonNormalBorder2") return "ButtonNormalDefaultBorder";
        if (token == "ContextMenuHeading") return "ContextMenuHeadingBack";
        if (token == "AppButtonMenuDocs") return "AppButtonMenuDocsBack";
        return token;
    }

    private static void AlignColors(IReadOnlyList<string> enumNames, IReadOnlyList<string> colors, IReadOnlyList<string> commentNames,
                                    out List<string> alignedColors, out List<bool> missingFlags)
    {
        var lookup = new Dictionary<string, string>(StringComparer.Ordinal);
        var max = Math.Max(colors.Count, commentNames.Count);
        for (var i = 0; i < max; i++)
        {
            var label = i < commentNames.Count ? NormalizeComment(commentNames[i]) : string.Empty;
            var colour = i < colors.Count ? colors[i] : "GlobalStaticValues.EMPTY_COLOR";
            if (!string.IsNullOrEmpty(label) && !lookup.ContainsKey(label))
            {
                lookup[label] = colour;
            }
        }

        alignedColors = new List<string>(enumNames.Count);
        missingFlags = new List<bool>(enumNames.Count);
        foreach (var name in enumNames)
        {
            if (lookup.TryGetValue(name, out var col))
            {
                alignedColors.Add(col);
                missingFlags.Add(false);
            }
            else
            {
                alignedColors.Add("GlobalStaticValues.EMPTY_COLOR");
                missingFlags.Add(true);
            }
        }
    }

    private static string GenerateSchemeCode(string className, IReadOnlyList<string> colors, IReadOnlyList<string> enumNames, IReadOnlyList<bool> missingFlags, string baseClassName = "KryptonColorSchemeBase", int braceCol = 59)
    {
        var ns = "Krypton.Toolkit";
        var sb = new StringBuilder();
        sb.AppendLine(LicenseHeader);
        sb.AppendLine("namespace " + ns + ";");
        sb.AppendLine();
        sb.AppendLine("public sealed class " + className + " : " + baseClassName);
        sb.AppendLine("{");
        var indent = new string(' ', 4);
        for (var i = 0; i < enumNames.Count; i++)
        {
            var name = enumNames[i];
            var expr = i < colors.Count ? colors[i] : "GlobalStaticValues.EMPTY_COLOR";
            var line = indent + "public override Color " + name + " ";
            var pad = braceCol - line.Length;
            if (pad < 1) pad = 1;
            line += new string(' ', pad) + "{ get; set; } = " + expr + ";";
            if (i < missingFlags.Count && missingFlags[i] && !MenuNames.Contains(name))
            {
                line += " // missing value";
            }
            sb.AppendLine(line);
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

    /// <summary>
    /// Removes the field declaration that begins with the given marker from a source file.
    /// </summary>
    private static void RemoveArrayFromFile(string filePath, string marker)
    {
        var lines = File.ReadAllLines(filePath).ToList();
        int start = lines.FindIndex(l => l.Contains(marker));
        if (start < 0) return; // nothing to remove

        int bracketDepth = 0;
        int end = -1;

        for (int i = start; i < lines.Count; i++)
        {
            string line = lines[i];
            bracketDepth += line.Count(c => c == '[');
            bracketDepth -= line.Count(c => c == ']');

            // Once brackets balance out and a semicolon is encountered, we've reached the end
            if (bracketDepth == 0 && line.Contains(";"))
            {
                end = i;
                break;
            }
        }

        if (end < 0) return; // unmatched – abort

        lines.RemoveRange(start, end - start + 1);

        // Remove any consecutive blank lines introduced by deletion
        for (int i = Math.Max(0, start - 1); i < lines.Count - 1; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]) && string.IsNullOrWhiteSpace(lines[i + 1]))
            {
                lines.RemoveAt(i);
                i--; // stay at same index
            }
        }

        File.WriteAllLines(filePath, lines);
    }

    /// <summary>
    /// Replaces constructor arguments referencing the removed arrays with calls to the generated scheme class.
    /// </summary>
    private static void UpdateConstructorArguments(string filePath, string schemeClassName)
    {
        var text = File.ReadAllText(filePath);

        var schemeCtor = $"new {schemeClassName}()";

        // 1) Default replacements (arrays removed in most contexts)
        text = Regex.Replace(text, "\\b_schemeBaseColors\\b", schemeCtor + ".ToArray()");
        text = Regex.Replace(text, "\\b_trackBarColors\\b", schemeCtor + ".ToTrackBarArray()");

        // 2) Fix the base-constructor argument list which should receive the scheme instance, not its array
        var baseIdx = text.IndexOf(": base(");
        if (baseIdx >= 0)
        {
            int openIdx = text.IndexOf('(', baseIdx);
            if (openIdx > -1)
            {
                int depth = 1;
                int i = openIdx + 1;
                for (; i < text.Length && depth > 0; i++)
                {
                    if (text[i] == '(') depth++;
                    else if (text[i] == ')') depth--;
                }
                if (depth == 0)
                {
                    var callSpan = text.Substring(openIdx + 1, i - openIdx - 2); // inside parentheses
                    var fixedSpan = callSpan.Replace(schemeCtor + ".ToArray()", schemeCtor);
                    // trackbar should stay array
                    text = text.Remove(openIdx + 1, callSpan.Length).Insert(openIdx + 1, fixedSpan);
                }
            }
        }

        File.WriteAllText(filePath, text);
    }

    private static IEnumerable<string> EnumeratePaletteFiles(string spec)
    {
        bool hasWildcards = spec.IndexOfAny(new[] { '*', '?' }) >= 0;

        if (!hasWildcards && File.Exists(spec)) return new[] { Path.GetFullPath(spec) };

        var repoRoot = LocateRepoRoot(Directory.GetCurrentDirectory());
        var tkPaletteRoot = Path.Combine(repoRoot, "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin");

        if (hasWildcards)
        {
            return Directory.GetFiles(tkPaletteRoot, spec, SearchOption.AllDirectories);
        }

        if (Path.GetFileName(spec).Equals(spec, StringComparison.OrdinalIgnoreCase))
        {
            return Directory.GetFiles(tkPaletteRoot, spec, SearchOption.AllDirectories);
        }

        return Array.Empty<string>();
    }
}