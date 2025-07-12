#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
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
    private const string ArrayMarker = "private static readonly Color[] _schemeBaseColors";

    public static void Generate(string paletteFile, string outputFolder, bool embedResx, bool dryRun, bool overwrite)
    {
        if (string.IsNullOrWhiteSpace(paletteFile)) throw new ArgumentException("paletteFile");

        static bool IsBasePalette(string path) => Path.GetFileName(path).EndsWith("Base.cs", StringComparison.OrdinalIgnoreCase) && Path.GetFileName(path).StartsWith("Palette", StringComparison.OrdinalIgnoreCase);

        var files = EnumeratePaletteFiles(paletteFile).Where(f => !IsBasePalette(f)).ToArray();
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
            if (colorsRaw.Count == 0) {
                Console.Error.WriteLine($"{palettePath}: no colors found");
                fail++;
                continue;
            }
            var commentNames = ExtractArrayComments(palettePath);
            var maxLen = Math.Max(colorsRaw.Count, commentNames.Count);
            while (colorsRaw.Count < maxLen) colorsRaw.Add("GlobalStaticValues.EMPTY_COLOR");
            while (commentNames.Count < maxLen) commentNames.Add(string.Empty);

            List<string> alignedColors;
            List<bool> missingFlags;
            AlignColors(enumNames, colorsRaw, commentNames, out alignedColors, out missingFlags);

            var className = Path.GetFileNameWithoutExtension(palettePath) + "Scheme";
            var code = GenerateSchemeCode(className, alignedColors, enumNames, missingFlags);

            var outputDir = string.IsNullOrWhiteSpace(outputFolder) ? Path.GetDirectoryName(palettePath)! : Path.GetFullPath(outputFolder);
            Directory.CreateDirectory(outputDir);

            var destPath = Path.Combine(outputDir, className + ".cs");

            if (dryRun)
            {
                Console.WriteLine(destPath);
                continue;
            }

            bool existed = File.Exists(destPath);
            if (existed && !overwrite)
            {
                Console.WriteLine(destPath + " *File exists, not replaced.");
                continue;
            }

            File.WriteAllText(destPath, code, Encoding.UTF8);
            Console.WriteLine(destPath + (existed ? " *Overwritten." : string.Empty));
            ok++;
        }

        if (fail > 0 && ok == 0) throw new InvalidOperationException("All palette generations failed");
        if (fail > 0 && ok > 0) Console.Error.WriteLine($"{fail} palette files failed");
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

    private static List<string> ParseEnumNames(string enumFile)
    {
        var result = new List<string>();
        var inside = false;
        foreach (var line in File.ReadLines(enumFile))
        {
            if (!inside)
            {
                if (line.Contains("enum SchemeBaseColors")) inside = true;
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

    private static List<string> ExtractColorExpressions(string palettePath)
    {
        var lines = File.ReadAllLines(palettePath);
        var start = Array.FindIndex(lines, l => l.Contains(ArrayMarker));
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

    private static List<string> ExtractArrayComments(string palettePath)
    {
        var lines = File.ReadAllLines(palettePath);
        var start = Array.FindIndex(lines, l => l.Contains(ArrayMarker));
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

    private static string GenerateSchemeCode(string className, IReadOnlyList<string> colors, IReadOnlyList<string> enumNames, IReadOnlyList<bool> missingFlags)
    {
        var ns = "Krypton.Toolkit";
        var sb = new StringBuilder();
        sb.AppendLine("namespace " + ns + ";");
        sb.AppendLine();
        sb.AppendLine("public class " + className + " : AbstractBaseColorScheme");
        sb.AppendLine("{");
        const int braceCol = 60;
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