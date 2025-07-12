#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
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

    public static void Generate(string paletteFile, string outputFolder, bool embedResx, bool dryRun)
    {
        if (string.IsNullOrWhiteSpace(paletteFile)) throw new ArgumentException("paletteFile");
        var palettePath = Path.GetFullPath(paletteFile);
        if (!File.Exists(palettePath)) throw new FileNotFoundException(palettePath);
        var root = LocateRepoRoot(Path.GetDirectoryName(palettePath)!);
        var enumFile = Path.Combine(root, "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Enumerations", "PaletteEnumerations.cs");
        if (!File.Exists(enumFile)) throw new FileNotFoundException(enumFile);
        var enumNames = ParseEnumNames(enumFile);
        if (enumNames.Count == 0) throw new InvalidOperationException("Could not parse enum names");
        var colors = ExtractColorExpressions(palettePath);
        if (colors.Count == 0) throw new InvalidOperationException("No color expressions found in palette");
        if (colors.Count < enumNames.Count)
        {
            var pad = Enumerable.Repeat("GlobalStaticValues.EMPTY_COLOR", enumNames.Count - colors.Count);
            colors.AddRange(pad);
        }
        var className = Path.GetFileNameWithoutExtension(palettePath) + "Scheme";
        var code = GenerateSchemeCode(className, colors, enumNames);
        var outputDir = string.IsNullOrWhiteSpace(outputFolder) ? Path.GetDirectoryName(palettePath)! : Path.GetFullPath(outputFolder);
        Directory.CreateDirectory(outputDir);
        var destPath = Path.Combine(outputDir, className + ".cs");
        if (dryRun) return;
        if (File.Exists(destPath)) return;
        File.WriteAllText(destPath, code, Encoding.UTF8);
    }

    private static string LocateRepoRoot(string start)
    {
        var dir = new DirectoryInfo(start);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "Directory.Build.props")))
        {
            dir = dir.Parent;
        }
        return dir?.FullName ?? throw new InvalidOperationException("Unable to locate repo root");
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

    private static string GenerateSchemeCode(string className, IReadOnlyList<string> colors, IReadOnlyList<string> enumNames)
    {
        var ns = "Krypton.Toolkit";
        var sb = new StringBuilder();
        sb.AppendLine("namespace " + ns + ";");
        sb.AppendLine();
        sb.AppendLine("public sealed class " + className + " : AbstractBaseColorScheme");
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
            sb.AppendLine(line);
        }
        sb.AppendLine("}");
        return sb.ToString();
    }
}