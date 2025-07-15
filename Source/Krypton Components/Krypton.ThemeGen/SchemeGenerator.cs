#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
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
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class SchemeGenerator
{
    // Header to prepend to all generated scheme files
    private static readonly string LicenseHeader =
"#region BSD License\n" +
"/*\n" +
" *\n" +
" *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)\n" +
" *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.\n" +
" *\n" +
" */\n" +
"#endregion\n";

    // UTF8 encoding without BOM
    private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

    // Normalize line endings to CRLF
    private static string ToCrLf(string text) => text.Replace("\r\n", "\n").Replace("\r", "").Replace("\n", "\r\n");

    private const string BaseColorMarker = "private static readonly Color[] _schemeBaseColors";

    // Marker for track-bar colour arrays found in palette classes
    private const string TrackBarColorMarker = "private static readonly Color[] _trackBarColors";

    public static void Generate(string paletteFile, string outputFolder, bool embedResx, bool dryRun, bool overwrite, bool migrate, bool printMapping = false)
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
            List<string> colorsRaw;
            List<string> commentNames;
            try
            {
                colorsRaw = ExtractColorsRoslyn(palettePath, "_schemeBaseColors", out commentNames, enumNames);
                Console.WriteLine($"[DEBUG] Successfully used Roslyn extraction for {Path.GetFileName(palettePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Roslyn extraction failed for {Path.GetFileName(palettePath)}: {ex.Message}");
                Console.WriteLine("[DEBUG] Falling back to regex extraction");
                colorsRaw = ExtractColorExpressions(palettePath);
                commentNames = ExtractArrayComments(palettePath);
            }

            List<string> trackBarColorsRaw;
            try
            {
                trackBarColorsRaw = ExtractColorsRoslyn(palettePath, "_trackBarColors", out _, enumNames);
            }
            catch
            {
                trackBarColorsRaw = ExtractColorExpressions(palettePath, TrackBarColorMarker);
            }

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
            // commentNames already obtained via Roslyn or fallback above
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

            // Build lookup of comment -> color for printing
            var commentColorLookup = new Dictionary<string, string>(StringComparer.Ordinal);
            var maxCommentMap = Math.Max(colorsRaw.Count, commentNames.Count);
            for (int i = 0; i < maxCommentMap; i++)
            {
                if (i < commentNames.Count && i < colorsRaw.Count)
                {
                    var labelRaw = commentNames[i];
                    var labelNorm = NormalizeComment(labelRaw);
                    if (!string.IsNullOrEmpty(labelNorm) && !commentColorLookup.ContainsKey(labelNorm))
                    {
                        commentColorLookup[labelNorm] = colorsRaw[i];
                    }
                }
            }

            if (printMapping)
            {
                PrintMappingTable(enumNames, commentNames, colorsRaw, alignedColors);
            }

            var className = Path.GetFileNameWithoutExtension(palettePath) + "_BaseScheme";
            var code = GenerateSchemeCode(className, alignedColors, enumNames, missingFlags);

            destPath = Path.Combine(outputDir, className + ".cs");

            if (!printMapping && dryRun)
            {
                Console.WriteLine(destPath);
                continue;
            }

            bool destWritten = false;
            if (!printMapping && !dryRun)
            {
                bool existed = File.Exists(destPath);
                if (existed && !overwrite)
                {
                    Console.WriteLine(destPath + " *File exists, not replaced.");
                }
                else
                {
                    File.WriteAllText(destPath, ToCrLf(code), Utf8NoBom);
                    Console.WriteLine(destPath + (existed ? " *Overwritten." : string.Empty));
                    ok++;
                    destWritten = true;
                }
            }

            // Optional removal of processed arrays from the palette source
            if (migrate && destWritten && !dryRun)
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

        var comments = new List<string>();

        int parenDepth = 0;
        string? currentComment = null;

        for (var i = start + 1; i < lines.Length; i++)
        {
            var line = lines[i];

            if (line.Contains("]") && parenDepth == 0)
            {
                // Reached end of array definition
                break;
            }

            // Split code vs comment part on the first // occurrence
            var parts = line.Split(new[] { "//" }, 2, StringSplitOptions.None);
            var codePart = parts[0].Trim();

            bool isStructural = codePart.Length == 0 || codePart == "[" || codePart == "]";

            // Track parentheses depth exactly like ExtractColorExpressions
            parenDepth += codePart.Count(c => c == '(') - codePart.Count(c => c == ')');

            // Capture comment token (use *last* comment on the line to skip debug numeric comments like //(155, 187, 227))
            var lastIdx = line.LastIndexOf("//", StringComparison.Ordinal);
            if (lastIdx >= 0)
            {
                var token = line.Substring(lastIdx + 2).Trim();
                var match = Regex.Match(token, "^([A-Za-z0-9_]+)");
                if (match.Success)
                {
                    currentComment = NormalizeComment(match.Groups[1].Value);
                }
            }

            // If structural/blank line: assign comment (if any) to previous expression when missing
            if (isStructural)
            {
                if (currentComment != null && comments.Count > 0 && string.IsNullOrEmpty(comments[comments.Count - 1]))
                {
                    comments[comments.Count - 1] = currentComment;
                    currentComment = null;
                }
                continue;
            }

            // When parentheses balance out, we've completed a single color expression
            if (parenDepth == 0)
            {
                comments.Add(currentComment ?? string.Empty);
                currentComment = null;
            }
        }

        return comments;
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

        var original = token;

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
            // Don't normalize again - commentNames from Roslyn are already normalized
            var label = i < commentNames.Count ? commentNames[i] : string.Empty;
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
        if (start < 0) return; // nothing to migrate

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

        var finalText = ToCrLf(string.Join("\r\n", lines));
        File.WriteAllText(filePath, finalText, Utf8NoBom);
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

        File.WriteAllText(filePath, ToCrLf(text), Utf8NoBom);
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

    /// <summary>
    /// Parses the specified array variable using Roslyn and returns aligned colour expressions and comments.
    /// </summary>
    private static List<string> ExtractColorsRoslyn(string filePath, string variableName, out List<string> commentsOut, IReadOnlyList<string> enumNames)
    {
        commentsOut = new List<string>();
        var code = File.ReadAllText(filePath);
        var tree = CSharpSyntaxTree.ParseText(code, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview));
        var root = tree.GetCompilationUnitRoot();

        // Locate the field
        var field = root.DescendantNodes()
                        .OfType<FieldDeclarationSyntax>()
                        .FirstOrDefault(f => f.Declaration.Variables.Any(v => v.Identifier.Text == variableName));

        if (field == null)
            throw new InvalidOperationException("Variable not found: " + variableName);

        var varDecl = field.Declaration.Variables.First(v => v.Identifier.Text == variableName);
        var equals = varDecl.Initializer ?? throw new InvalidOperationException("No initializer for " + variableName);

        // The initializer expression may be a CollectionExpressionSyntax or an InitializerExpressionSyntax
        var expr = equals.Value;

        // Build a unified element list regardless of initializer style
        var exprNodes = new List<SyntaxNode>();

        if (expr is InitializerExpressionSyntax init)
        {
            exprNodes.AddRange(init.Expressions);
        }
        else if (expr is CollectionExpressionSyntax coll)
        {
            foreach (var cel in coll.Elements)
            {
                if (cel is ExpressionElementSyntax eel)
                {
                    exprNodes.Add(eel.Expression);
                }
            }
        }
        else
        {
            throw new InvalidOperationException("Unsupported array initializer kind for " + variableName);
        }

        var colours = new List<string>();

        for (int idx = 0; idx < exprNodes.Count; idx++)
        {
            var exprNode = exprNodes[idx];
            var rawExpr = exprNode.ToFullString().Replace("\r", "").Replace("\n", " ");
            rawExpr = Regex.Replace(rawExpr, "\\s+", " "); // collapse whitespace
            var colourExpr = rawExpr.Split(new[] {"//"}, 2, StringSplitOptions.None)[0].Trim().TrimEnd(',');
            colours.Add(colourExpr);

            // Gather trivia for potential comment labels
            string label = string.Empty;

            // Check trailing trivia first
            foreach (var triv in exprNode.GetTrailingTrivia())
            {
                if (TryExtractEnumFromTrivia(triv, enumNames, out label))
                    break;
            }

            // Only check separator trivia if we haven't found a label yet
            if (string.IsNullOrEmpty(label))
            {
                // Separator (comma) trivia
                SyntaxToken separatorToken;
                if (expr is InitializerExpressionSyntax ini && idx < ini.Expressions.Count - 1)
                {
                    var sep = ini.Expressions.GetSeparator(idx);
                    separatorToken = sep;
                }
                else if (expr is CollectionExpressionSyntax col && idx < col.Elements.Count - 1)
                {
                    var sep = col.Elements.GetSeparator(idx);
                    separatorToken = sep;
                }
                else separatorToken = default;

                if (separatorToken.IsKind(SyntaxKind.CommaToken))
                {
                    foreach (var triv in separatorToken.LeadingTrivia)
                    {
                        if (TryExtractEnumFromTrivia(triv, enumNames, out label))
                            break;
                    }

                    if (string.IsNullOrEmpty(label))
                    {
                        foreach (var triv in separatorToken.TrailingTrivia)
                        {
                            if (TryExtractEnumFromTrivia(triv, enumNames, out label))
                                break;
                        }
                    }
                }

                // Only check next token trivia if still no label
                if (string.IsNullOrEmpty(label))
                {
                    var nextToken = separatorToken != default ? separatorToken.GetNextToken() : exprNode.GetLastToken().GetNextToken();
                    foreach (var triv in nextToken.LeadingTrivia)
                    {
                        if (TryExtractEnumFromTrivia(triv, enumNames, out label))
                            break;
                    }
                }
            }

            commentsOut.Add(label);
        }

        return colours;
    }

    private static bool TryExtractEnumFromTrivia(SyntaxTrivia trivia, IReadOnlyList<string> enumNames, out string label)
    {
        label = string.Empty;

        if (!trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            return false;

        var commentText = trivia.ToString().Substring(2).Trim();

        // Split on common separators that might indicate embedded code
        var parts = commentText.Split(new[] { "Color.", "Color(", "FromArgb" }, StringSplitOptions.None);
        var mainPart = parts[0].Trim();

        foreach (Match m in Regex.Matches(mainPart, "[A-Za-z_][A-Za-z0-9_]*"))
        {
            var candidate = NormalizeComment(m.Value);
            if (enumNames.Contains(candidate))
            {
                label = candidate;
                return true;
            }
        }

        return false;
    }

    private static void PrintMappingTable(IReadOnlyList<string> enumNames,
                                          IReadOnlyList<string> commentNames,
                                          IReadOnlyList<string> commentColors,
                                          IReadOnlyList<string> alignedColors)
    {
        // Build lookup dictionary from comment name -> source color expression
        var commentLookup = new Dictionary<string, string>(StringComparer.Ordinal);
        for (int i = 0; i < commentNames.Count && i < commentColors.Count; i++)
        {
            var name = commentNames[i];
            if (!string.IsNullOrEmpty(name) && !commentLookup.ContainsKey(name))
                commentLookup[name] = commentColors[i];
        }

        // Determine column widths (include MISSING!)
        const string missingToken = "MISSING!";

        var headerEnum = "Enum Name";
        var headerComment = "Comment";
        var headerCommentColor = "Comment Color";
        var headerFinalColor = "Final Color";

        int enumWidth = Math.Max(enumNames.Max(n => n.Length), headerEnum.Length);
        int commentWidth = Math.Max(Math.Max(commentLookup.Keys.Any() ? commentLookup.Keys.Max(k => k.Length) : 0, missingToken.Length), headerComment.Length);
        int commentColorWidth = Math.Max(commentLookup.Values.Any() ? commentLookup.Values.Max(c => c.Length) : 0, headerCommentColor.Length);
        int finalColorWidth = Math.Max(alignedColors.Max(c => c.Length), headerFinalColor.Length);

        string rowFormat = $"| {{0,-{enumWidth}}} | {{1,-{commentWidth}}} | {{2,-{commentColorWidth}}} | {{3,-{finalColorWidth}}} |";

        string separator = new string('-', rowFormat.Length - 2);

        Console.WriteLine(separator);
        Console.WriteLine(rowFormat, headerEnum, headerComment, headerCommentColor, headerFinalColor);
        Console.WriteLine(separator);

        int missingCount = 0;

        for (int i = 0; i < enumNames.Count; i++)
        {
            var enumName = enumNames[i];

            bool hasComment = commentLookup.TryGetValue(enumName, out var commentColor);

            bool isTrackBar = enumName.StartsWith("TrackBar", StringComparison.Ordinal);

            string commentName;
            string prefix;

            if (!hasComment && isTrackBar)
            {
                // Suppress missing marker for TrackBar* enums
                commentName = string.Empty;
                prefix = " ";
            }
            else
            {
                commentName = hasComment ? enumName : missingToken;
                prefix = hasComment ? " " : "!";
                if (!hasComment) missingCount++;
            }

            string finalColor = i < alignedColors.Count ? alignedColors[i] : string.Empty;

            Console.Write(prefix);
            Console.WriteLine(rowFormat, enumName, commentName, hasComment ? commentColor : string.Empty, finalColor);
        }

        Console.WriteLine(separator);

        if (missingCount > 0)
        {
            Console.Error.WriteLine($"WARNING: Detected {missingCount} enumeration/comment mismatches (missing comments). Rows are prefixed with '!'.");
        }
    }
}