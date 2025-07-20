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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
    private static readonly Encoding Utf8Bom = new UTF8Encoding(true);

    // Normalize line endings to CRLF
    private static string ToCrLf(string text) => text.Replace("\r\n", "\n").Replace("\r", "").Replace("\n", "\r\n");

    private const string BaseColorMarker = "private static readonly Color[] _schemeBaseColors";
    // Some early Office 2007 palettes used a different variable name for the base colour array
    private const string OfficeColorMarker = "private static readonly Color[] _schemeOfficeColours";
    // Marker for Sparkle ribbon colour arrays
    private const string RibbonColorMarker = "private static readonly Color[] _ribbonColors";

    // Marker for track-bar color arrays found in palette classes
    private const string TrackBarColorMarker = "private static readonly Color[] _trackBarColors";

    // Family-base palette classes that must retain array backing for one more release
    private static readonly HashSet<string> FamilyBaseNames = new HashSet<string>(StringComparer.Ordinal)
    {
        "PaletteMicrosoft365Base",
        "PaletteOffice2007Base",
        "PaletteOffice2010Base",
        "PaletteOffice2013Base",
        "PaletteSparkleBase",
        "PaletteSparkleBlueDarkModeBase"
    };

    private static readonly HashSet<string> ExplicitlyExcludedFiles = new HashSet<string>(StringComparer.Ordinal)
    {
        "PaletteBase.cs",
        "PaletteEnumerations.cs",
        "KryptonColorSchemeBase.cs"
    };

    private static bool IsFamilyBase(string palettePath)
    {
        var name = Path.GetFileNameWithoutExtension(palettePath);
        if (FamilyBaseNames.Contains(name)) return true;

        // Treat any PaletteSparkle*Base (including DarkMode) as family base for now
        return Regex.IsMatch(name, @"^PaletteSparkle.*Base$");
    }

    // Single-file cache: only keep the currently processed file in memory
    private static string? _cachedPath;
    private static string  _cachedContent = string.Empty;
    private static bool    _cacheDirty;

    /// <summary>
    /// Reads a text file into memory only once. Subsequent calls for the same path
    /// return the cached content. When switching to a different file, pending changes
    /// for the previous file are flushed automatically.
    /// </summary>
    private static string GetFileText(string path)
    {
        if (string.Equals(_cachedPath, path, StringComparison.OrdinalIgnoreCase))
        {
            return _cachedContent;
        }

        FlushCache();

        _cachedContent = File.ReadAllText(path);
        _cachedPath    = path;
        _cacheDirty    = false;
        return _cachedContent;
    }

    /// Returns an array of lines for the given file while still reading from disk only once.
    private static string[] GetFileLines(string path)
        => GetFileText(path).Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

    /// <summary>
    /// Stores updated content for the given path in memory and marks it dirty. The file will be
    /// physically written to disk when a different file is accessed or at the end of processing.
    /// If SetFile is invoked for a path other than the currently cached file, the existing cache is
    /// flushed first and the new content is written immediately (no caching for such one-off writes).
    /// </summary>
    private static void SetFile(string path, string content)
    {
        var normalized = ToCrLf(content);

        if (string.Equals(_cachedPath, path, StringComparison.OrdinalIgnoreCase) || _cachedPath is null)
        {
            _cachedPath    = path;
            _cachedContent = normalized;
            _cacheDirty    = true;
        }
        else
        {
            // Different file: flush previous cache once, then write directly without caching
            FlushCache();
            File.WriteAllText(path, normalized, Utf8Bom);
        }
    }

    /// <summary>
    /// Writes the cached file (if any and dirty) to disk and clears the dirty flag.
    /// </summary>
    private static void FlushCache()
    {
        if (_cacheDirty && _cachedPath != null)
        {
            File.WriteAllText(_cachedPath, _cachedContent, Utf8Bom);
            _cacheDirty = false;
        }
    }

    private static bool _emptySchemeCreated;

    public static void Generate(string paletteFile, string outputFolder,
                            bool embedResx, bool dryRun,
                            bool overwrite, bool migrate,
                            bool printMapping = false,
                            bool oneCtor = false)
    {
        if (string.IsNullOrWhiteSpace(paletteFile)) throw new ArgumentException("paletteFile");

        var enumerated = EnumeratePaletteFiles(paletteFile);

        bool treatAsExplicit = !paletteFile.Contains("*") && !paletteFile.Contains("?") && !Directory.Exists(paletteFile);
        string specFileName = Path.GetFileName(paletteFile);

        var files = enumerated.Where(f =>
        {
            var fileName = Path.GetFileName(f);

            // Always exclude previously generated *_BaseScheme.cs files
            if (fileName.EndsWith("_BaseScheme.cs", StringComparison.OrdinalIgnoreCase)) return false;

            bool isPaletteBase = fileName.EndsWith("Base.cs", StringComparison.OrdinalIgnoreCase) && fileName.StartsWith("Palette", StringComparison.OrdinalIgnoreCase);

            if (treatAsExplicit)
            {
                // If user provided a specific filename (no wildcards), allow that single file even if it's a base palette.
                if (string.Equals(fileName, specFileName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            // Default filtering
            // Allow Palette*Base.cs when migration is requested
            return !isPaletteBase || migrate;
        }).ToArray();

        if (files.Length == 0) throw new FileNotFoundException($"No palette file found matching '{paletteFile}' (after excluding base classes)");

        // Create shared EmptySchemeBase once
        try
        {
            var repoRoot = LocateRepoRoot(Path.GetDirectoryName(files[0])!);
            EnsureEmptySchemeBase(repoRoot, overwrite, dryRun);
        }
        catch { }

        int ok = 0, fail = 0;
        foreach (var palettePath in files)
        {
            if (ExplicitlyExcludedFiles.Contains(Path.GetFileName(palettePath)))
                continue;
            bool isFamilyBase = IsFamilyBase(palettePath);
            bool modified = false; // track if this palette resulted in changes
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
            List<string> colorsRaw = new();
            List<string> commentNames = new();
            string detectedBaseArrayMarker = BaseColorMarker;

            // Try modern, legacy and Sparkle array variable names
            string[] possibleVars = { "_schemeBaseColors", "_schemeOfficeColours", "_ribbonColors" };
            foreach (var varName in possibleVars)
            {
                try
                {
                    colorsRaw = ExtractColorsRoslyn(palettePath, varName, out commentNames, enumNames);
                    // Found and parsed successfully
                    detectedBaseArrayMarker = varName switch
                    {
                        "_schemeOfficeColours" => OfficeColorMarker,
                        "_ribbonColors"        => RibbonColorMarker,
                        _                      => BaseColorMarker
                    };
                    break;
                }
                catch (InvalidOperationException ioe) when (ioe.Message.Contains("Variable not found") || ioe.Message.Contains("No initializer"))
                {
                    // Try the next variant
                    continue;
                }
                catch (Exception)
                {
                    // We'll handle Roslyn failure after the loop
                    throw;
                }
            }

            if (colorsRaw.Count == 0)
            {
                // Either variable not found or Roslyn parse failed – fall back to simple text extraction
                try
                {
                    string markerToUse = detectedBaseArrayMarker;
                    // Determine marker based on file contents if not set
                    var paletteTextTmp = GetFileText(palettePath);
                    if (!paletteTextTmp.Contains(BaseColorMarker) && paletteTextTmp.Contains(OfficeColorMarker))
                    {
                        markerToUse = OfficeColorMarker;
                    }
                    else if (!paletteTextTmp.Contains(BaseColorMarker) && paletteTextTmp.Contains(RibbonColorMarker))
                    {
                        markerToUse = RibbonColorMarker;
                    }

                    colorsRaw = ExtractColorExpressions(palettePath, markerToUse);
                    commentNames = new List<string>();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"{Path.GetFileName(palettePath)}: color array extraction failed ({ex.Message})");
                }
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

            bool hasMainColors = colorsRaw.Count > 0;
            bool hasTrackBar   = trackBarColorsRaw.Count > 0;
            bool mainArrayHasContent  = colorsRaw.Any(c => !c.Contains("GlobalStaticValues.EMPTY_COLOR", StringComparison.Ordinal));
            bool generateScheme       = mainArrayHasContent && hasTrackBar;
            bool hasAnyArrays         = hasMainColors || hasTrackBar;

            // Treat 'LightGray' themed palettes specially – we still want to generate a dummy
            // base scheme class even when they have no embedded colour arrays. This allows
            // their constructors to migrate to the KryptonColorSchemeBase overload just like
            // every other palette.
            bool isLightGray = Path.GetFileName(palettePath).Contains("LightGray", StringComparison.OrdinalIgnoreCase);
            if (isLightGray)
            {
                // Do NOT generate a per-palette scheme; use shared EmptySchemeBase instead
                hasAnyArrays = false;
            }

            if (!hasAnyArrays)
            {
                string info;
                if (isFamilyBase)
                {
                    info = $"Info: {Path.GetFileName(palettePath)} – processing as base class.";
                }
                else
                {
                    info = $"Info: {Path.GetFileName(palettePath)} – no color arrays found (already migrated).";
                    if (migrate)
                    {
                        info += " Running index conversion only.";
                    }
                }

                Console.WriteLine(info);
            }

            List<string> alignedColors = new();
            List<bool> missingFlags = new();
            string? destPath = null;

            var outputDir = string.IsNullOrWhiteSpace(outputFolder)
                ? Path.Combine(Path.GetDirectoryName(palettePath)!, "Schemes")
                : Path.GetFullPath(outputFolder);
            Directory.CreateDirectory(outputDir);

            // Align base colors first (may be empty)
            // commentNames already obtained via Roslyn or fallback above
            var maxLen = Math.Max(colorsRaw.Count, commentNames.Count);
            while (colorsRaw.Count < maxLen) colorsRaw.Add("GlobalStaticValues.EMPTY_COLOR");
            while (commentNames.Count < maxLen) commentNames.Add(string.Empty);

            AlignColors(enumNames, colorsRaw, commentNames, out alignedColors, out missingFlags);

            // Overlay track-bar colors onto the aligned list
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

            string? className = null;
            if (isLightGray)
            {
                className = "EmptySchemeBase";
            }
            else if (generateScheme)
            {
                className = Path.GetFileNameWithoutExtension(palettePath) + "_BaseScheme";
                var code = GenerateSchemeCode(className, alignedColors, enumNames, missingFlags);

                destPath = Path.Combine(outputDir, className + ".cs");

                if (!printMapping && dryRun)
                {
                    Console.WriteLine(destPath);
                }
                else if (!printMapping && !dryRun)
                {
                    bool existed = File.Exists(destPath);
                    if (existed && !overwrite)
                    {
                        Console.WriteLine(destPath + " *File exists, not replaced.");
                    }
                    else
                    {
                        SetFile(destPath, code);
                        Console.WriteLine(destPath + (existed ? " *Overwritten." : string.Empty));
                        ok++;
                        modified = true;
                    }
                }
            }

            // Optional migration steps (array removal, constructor fix, index conversions).
            // We only touch the original palette file when:
            //   • we generated a scheme alongside it (destWritten), or
            //   • no separate output folder was specified (in-place generation).
            // Determine if we process files in-place (no --output) or write converted copies elsewhere
            bool inPlace = string.IsNullOrWhiteSpace(outputFolder);
            string? targetPath = palettePath;
            bool modificationsAllowed = true;

            if (!inPlace && !dryRun)
            {
                var copyPath = Path.Combine(Path.GetFullPath(outputFolder), Path.GetFileName(palettePath));

                if (!Path.Equals(copyPath, palettePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(copyPath)!);

                    bool copyExists = File.Exists(copyPath);

                    if (!copyExists || overwrite)
                    {
                        // Create or overwrite the copy as requested
                        File.Copy(palettePath, copyPath, overwrite: true);
                    }
                    else
                    {
                        // Copy exists and overwrite not requested – skip later modifications
                        modificationsAllowed = false;
                    }

                    targetPath = copyPath;
                }
            }

            if (migrate && !dryRun && modificationsAllowed)
            {
                if (isFamilyBase)
                {
                    // For family-base palettes: keep arrays, only add scheme ctor & obsolete attributes

                    try
                    {
                        var srcText = GetFileText(targetPath);
                        var replacedText = srcText;

                        // Ensure BaseColors field exists (harmless if already present)
                        replacedText = EnsureBaseColorsField(replacedText, oneCtor);

                        // Ensure KryptonColorSchemeBase constructor overload exists
                        replacedText = EnsureSchemeConstructor(replacedText);

                        // Mark any Color[] constructor as obsolete
                        replacedText = EnsureObsoleteOnColorArrayConstructors(replacedText);

                        if (!string.Equals(srcText, replacedText, StringComparison.Ordinal))
                        {
                            SetFile(targetPath, replacedText);

                            if (!inPlace)
                            {
                                Console.WriteLine($"Updated family-base palette {Path.GetFileName(targetPath)}");
                            }
                            modified = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"{targetPath}: family-base migration failed - {ex.Message}");
                    }
                }
                else
                {
                    // ===== regular (non-base) palette migration logic =====
                    if (className != null && generateScheme)
                    {
                        // Remove main colour array
                        RemoveArrayFromFile(targetPath, detectedBaseArrayMarker);

                        // Remove track-bar array at the same time
                        RemoveArrayFromFile(targetPath, TrackBarColorMarker);

                        // Update constructor arguments to use generated scheme
                        UpdateConstructorArguments(targetPath, className, oneCtor);
                    }

                    try
                    {
                        var srcText = GetFileText(targetPath);
                        var replacedText = ConvertRibbonColorArrayEntriesToProperties(srcText);
                        replacedText = ConvertTrackBarColorArrayEntriesToProperties(replacedText);
                        replacedText = ConvertColorTableArrayEntriesToBaseColors(replacedText);
                        replacedText = EnsureBaseColorsField(replacedText, oneCtor);
                        if (!oneCtor)
                            replacedText = EnsureSchemeConstructor(replacedText);

                        // Mark any Color[] constructor as obsolete
                        replacedText = EnsureObsoleteOnColorArrayConstructors(replacedText);

                        // newline fix
                        replacedText = Regex.Replace(
                            replacedText,
                            @"(?<indent>^[ \t]*)\}(?:[ \t]*#(?<dir>region|endregion))",
                            m =>
                            {
                                var indent = m.Groups["indent"].Value;
                                var dir    = m.Groups["dir"].Value;
                                return $"{indent}}}\r\n{indent}#{dir}";
                            },
                            RegexOptions.Multiline);

                        if (!string.Equals(srcText, replacedText, StringComparison.Ordinal))
                        {
                            SetFile(targetPath, replacedText);

                            if (!inPlace)
                            {
                                Console.WriteLine($"Converted copy written to {targetPath}");
                            }
                            modified = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"{targetPath}: array reference conversion failed - {ex.Message}");
                    }
                }
            }

            // Dry-run validation asserts
            if (dryRun)
            {
                try
                {
                    string validationText;

                    if (migrate)
                    {
                        // Use already transformed text if we migrated in-memory earlier
                        if (isFamilyBase)
                        {
                            var src = GetFileText(palettePath);
                            validationText = EnsureObsoleteOnColorArrayConstructors(EnsureSchemeConstructor(EnsureBaseColorsField(src, oneCtor)));
                        }
                        else
                        {
                            var src = GetFileText(palettePath);
                            var tmp = ConvertRibbonColorArrayEntriesToProperties(src);
                            tmp = ConvertTrackBarColorArrayEntriesToProperties(tmp);
                            tmp = ConvertColorTableArrayEntriesToBaseColors(tmp);
                            tmp = EnsureBaseColorsField(tmp, oneCtor);
                            if (!oneCtor)
                                tmp = EnsureSchemeConstructor(tmp);
                            validationText = tmp;
                        }
                    }
                    else
                    {
                        // No migration requested – validate current state only
                        validationText = GetFileText(palettePath);
                    }

                    if (isFamilyBase)
                    {
                        ValidateFamilyBaseDryRun(Path.GetFileName(palettePath), validationText);
                    }
                    else
                    {
                        ValidateThemeDryRun(Path.GetFileName(palettePath), validationText);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[CHECK ERROR] {Path.GetFileName(palettePath)}: {ex.Message}");
                }
            }

            if (modified && !dryRun)
            {
                Console.WriteLine($"{Path.GetFileName(palettePath)} processed.");
            }
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

        // Ensure any pending cached file is flushed to disk
        FlushCache();
    }

    private static void EnsureEmptySchemeBase(string repoRoot, bool overwrite, bool dryRun)
    {
        if (_emptySchemeCreated) return;
        _emptySchemeCreated = true;

        var destDir = Path.Combine(repoRoot, "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Base", "Schemes");
        var destPath = Path.Combine(destDir, "EmptySchemeBase.cs");

        if (dryRun)
        {
            Console.WriteLine(destPath);
            return; // Skip generation in dry-run mode
        }

        var enumFile = Path.Combine(repoRoot, "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Enumerations", "PaletteEnumerations.cs");
        if (!File.Exists(enumFile)) return;

        var enumNames = ParseEnumNames(enumFile);
        if (enumNames.Count == 0) return;

        var colors = Enumerable.Repeat("GlobalStaticValues.EMPTY_COLOR", enumNames.Count).ToList();
        var missing = Enumerable.Repeat(true, enumNames.Count).ToList();
        var code = GenerateSchemeCode("EmptySchemeBase", colors, enumNames, missing);

        Directory.CreateDirectory(destDir);

        if (File.Exists(destPath) && !overwrite) return;

        SetFile(destPath, code);
    }

    // ------------------------ NEW: Professional System extraction ---------------------------
    /// <summary>
    /// Generates a PaletteProfessionalSystem_BaseScheme.cs file by instantiating the existing
    /// PaletteProfessionalSystem class and reading its private _ribbonColors field via reflection.
    /// Only SchemeBaseColors properties are produced – standalone colors can be added later.
    /// </summary>
    /// <param name="outputFolder">Folder to place generated file (current dir if empty)</param>
    /// <param name="dryRun">If true, no file is written – path is printed instead</param>
    /// <param name="overwrite">Allow overwriting existing scheme file</param>
    public static void GenerateProfessional(string outputFolder, bool dryRun, bool overwrite)
    {
        // 1. Locate repo root so we can get enumeration names (same helper as other path)
        var root = LocateRepoRoot(Directory.GetCurrentDirectory());
        var enumFile = Path.Combine(root, "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Enumerations", "PaletteEnumerations.cs");
        if (!File.Exists(enumFile)) throw new FileNotFoundException(enumFile);

        var enumNames = ParseEnumNames(enumFile);
        if (enumNames.Count == 0) throw new InvalidOperationException("Failed to read SchemeBaseColors enum.");

        // 2. Instantiate palette – this runs DefineRibbonColors()
        var palette = new Krypton.Toolkit.PaletteProfessionalSystem();
        var field   = typeof(Krypton.Toolkit.PaletteProfessionalSystem)
                        .GetField("_ribbonColors", BindingFlags.Instance | BindingFlags.NonPublic);
        if (field == null) throw new InvalidOperationException("_ribbonColors field not found – toolkit updated?");

        var colorsArr = (Color[])field.GetValue(palette)!;

        // 3. Convert colors to expressions – pad with EMPTY_COLOR for missing
        var exprs = new List<string>(enumNames.Count);
        var missing = new List<bool>(enumNames.Count);
        for (int i = 0; i < enumNames.Count; i++)
        {
            if (i < colorsArr.Length)
            {
                exprs.Add(ToColorExpr(colorsArr[i]));
                missing.Add(false);
            }
            else
            {
                exprs.Add("GlobalStaticValues.EMPTY_COLOR");
                missing.Add(true);
            }
        }

        // 4. Generate code
        var className = "PaletteProfessionalSystem_BaseScheme";
        var code = GenerateSchemeCode(className, exprs, enumNames, missing);

        // 5. Determine output path
        var outputDir = string.IsNullOrWhiteSpace(outputFolder) ? Directory.GetCurrentDirectory() : Path.GetFullPath(outputFolder);
        Directory.CreateDirectory(outputDir);
        var destPath = Path.Combine(outputDir, className + ".cs");

        if (dryRun)
        {
            Console.WriteLine(destPath);
            return;
        }

        if (File.Exists(destPath) && !overwrite)
        {
            Console.WriteLine(destPath + " *File exists, not replaced. Use --overwrite to force.");
            return;
        }

        SetFile(destPath, code);
        Console.WriteLine("Scheme written to " + destPath);
    }

    /// <summary>
    /// Generates a PaletteProfessionalOffice2003_BaseScheme.cs using reflection.
    /// </summary>
    public static void GenerateProfessionalOffice2003(string outputFolder, bool dryRun, bool overwrite)
    {
        var root = LocateRepoRoot(Directory.GetCurrentDirectory());
        var enumFile = Path.Combine(root, "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Enumerations", "PaletteEnumerations.cs");
        var enumNames = ParseEnumNames(enumFile);

        // Build a full Office 2003 ribbon palette from the two-entry header pair and generate a scheme class.
        void Emit(Color[] headerPair, string classSuffix)
        {
            // 1. Create a fresh palette instance
            var palette = new Krypton.Toolkit.PaletteProfessionalOffice2003();

            // 2. Construct a KryptonProfessionalKCT with the header colours (constructor is internal)
            var kctType = typeof(Krypton.Toolkit.PaletteOffice2003Base).Assembly
                .GetType("Krypton.Toolkit.KryptonProfessionalKCT", throwOnError: true)!;

            var kctCtor = kctType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: new[] { typeof(Color[]), typeof(bool), typeof(Krypton.Toolkit.PaletteBase) },
                modifiers: null) ?? throw new InvalidOperationException("KryptonProfessionalKCT ctor not found.");

            var kct = kctCtor.Invoke(new object[] { headerPair, false, palette });

            // 3. Inject the colour table into the palette's private field
            typeof(Krypton.Toolkit.PaletteOffice2003Base)
                .GetField("_table", BindingFlags.Instance | BindingFlags.NonPublic)!
                .SetValue(palette, kct);

            // 4. Re-run DefineRibbonColors so the palette rebuilds its _ribbonColors array
            typeof(Krypton.Toolkit.PaletteOffice2003Base)
                .GetMethod("DefineRibbonColors", BindingFlags.Instance | BindingFlags.NonPublic)!
                .Invoke(palette, null);

            // 5. Generate the scheme file from the populated palette instance
            var className = $"PaletteProfessionalOffice2003_{classSuffix}Scheme";
            GenerateFromPalette(palette, className, outputFolder, dryRun, overwrite);
        }

        var pType = typeof(Krypton.Toolkit.PaletteProfessionalOffice2003);
        Color[]? cB = (Color[]?)pType.GetField("_colorsB", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null);
        Color[]? cG = (Color[]?)pType.GetField("_colorsG", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null);
        Color[]? cS = (Color[]?)pType.GetField("_colorsS", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null);

        if (cB == null || cG == null || cS == null)
            throw new InvalidOperationException("Office2003 palette static arrays not found.");

        Emit(cB, "Blue");
        Emit(cG, "HomeStead");
        Emit(cS, "Metallic");
    }

    /// <summary>
    /// Converts a Color to a C# expression (Color.FromArgb(...))
    /// </summary>
    private static string ToColorExpr(Color c)
    {
        if (c.A != 255)
            return $"Color.FromArgb({c.A}, {c.R}, {c.G}, {c.B})";
        return $"Color.FromArgb({c.R}, {c.G}, {c.B})";
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
        var lines = GetFileLines(palettePath);
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
            if (className != "EmptySchemeBase")
            {
                if (i < missingFlags.Count && missingFlags[i] && !MenuNames.Contains(name))
                {
                    line += " // missing value";
                }
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
        var lines = GetFileLines(filePath).ToList();
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

        var finalText = string.Join("\r\n", lines);
        SetFile(filePath, finalText);
    }

    /// <summary>
    /// Replaces constructor arguments referencing the removed arrays with calls to the generated scheme class.
    /// </summary>
    private static void UpdateConstructorArguments(string filePath, string schemeClassName, bool singleCtor)
    {
        var source = GetFileText(filePath);

        // For LightGray palettes we leave the original constructor untouched.
        if (schemeClassName.IndexOf("LightGray", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return; // skip modifications
        }

        var tree = CSharpSyntaxTree.ParseText(source, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview));
        var root = tree.GetCompilationUnitRoot();

        var rewriter = singleCtor
            ? (CSharpSyntaxRewriter)new BaseCtorSingleRewriter(schemeClassName)
            : new BaseCtorArgRewriter(schemeClassName);
        var newRoot = (CompilationUnitSyntax)rewriter.Visit(root);

        bool hasChanges;
        if (rewriter is BaseCtorArgRewriter r1)
        {
            hasChanges = r1.HasChanges;
        }
        else if (rewriter is BaseCtorSingleRewriter r2)
        {
            hasChanges = r2.HasChanges;
        }
        else
        {
            hasChanges = false;
        }

        if (!hasChanges)
        {
            return; // nothing modified
        }

        var newText = newRoot.ToFullString();
        SetFile(filePath, newText);
    }

    /// <summary>
    /// Roslyn-based visitor that rewrites only the <c>: base(...)</c> constructor initializer.
    ///
    /// What it changes:
    ///   • <c>_schemeBaseColors</c>  → <c>new {schemeClass}()</c><br/>
    ///   • <c>_trackBarColors</c>   → <c>new {schemeClass}().ToTrackBarArray()</c><br/>
    ///   • Legacy fragment <c>new {schemeClass}().ToArray()</c> → <c>new {schemeClass}()</c>
    ///
    /// Everything else—including comments, strings and any other code—remains untouched.
    /// On subsequent runs, if there is nothing left to fix, <see cref="HasChanges"/> stays <c>false</c>
    /// and the source file is left exactly as it is.
    /// </summary>
    private sealed class BaseCtorArgRewriter : CSharpSyntaxRewriter
    {
        private readonly string _schemeClassName;
        private readonly bool _isLightGray;
        public bool HasChanges { get; private set; }

        private ExpressionSyntax SchemeCtorExpr => SyntaxFactory.ParseExpression($"new {_schemeClassName}().ToArray()");
        private ExpressionSyntax TrackBarExpr  => SyntaxFactory.ParseExpression($"new {_schemeClassName}().ToTrackBarArray()");
        // New: scheme instance expression (without converting to array)
        private ExpressionSyntax SchemeInstanceExpr => SyntaxFactory.ParseExpression($"new {_schemeClassName}()");

        public BaseCtorArgRewriter(string schemeClassName)
        {
            _schemeClassName = schemeClassName;
            _isLightGray = schemeClassName.Contains("LightGray", StringComparison.OrdinalIgnoreCase);
        }

        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            // Only process constructors that delegate to base(...)
            var init = node.Initializer;
            if (init == null || init.Kind() != SyntaxKind.BaseConstructorInitializer)
            {
                var visited = base.VisitConstructorDeclaration(node);
                return visited ?? node;
            }

            var args = init.ArgumentList.Arguments;
            bool updated = false;

            var newArgsList = new List<ArgumentSyntax>();

            // When converting LightGray palettes we also need to adjust the parameter list
            ParameterListSyntax newParamList = node.ParameterList;
            if (_isLightGray)
            {
                var paramBuilder = new List<ParameterSyntax>();
                bool schemeInserted = false;

                ParameterSyntax MakeSchemeParam()
                {
                    var type = SyntaxFactory.IdentifierName("KryptonColorSchemeBase").WithTrailingTrivia(SyntaxFactory.Space);
                    return SyntaxFactory.Parameter(SyntaxFactory.Identifier("scheme")).WithType(type);
                }

                foreach (var p in node.ParameterList.Parameters)
                {
                    var pName = p.Identifier.Text;
                    // Drop the two Color[] parameters
                    if (pName is "schemeColors" or "schemeColors" or "trackBarColors" or "trackBarColors")
                    {
                        updated = true;
                        continue;
                    }

                    paramBuilder.Add(p);

                    // insert 'scheme' parameter immediately after themeName (if present)
                    if (!schemeInserted && pName == "themeName")
                    {
                        paramBuilder.Add(MakeSchemeParam());
                        schemeInserted = true;
                    }
                }

                if (!schemeInserted)
                {
                    // prepend if themeName not found
                    paramBuilder.Insert(0, MakeSchemeParam());
                }

                newParamList = node.ParameterList.WithParameters(ToSeparated(paramBuilder));
            }

            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                var expr = arg.Expression;

                ExpressionSyntax? replacement = null;

                if (expr is IdentifierNameSyntax id)
                {
                    var name = id.Identifier.Text;
                    if (name is "_schemeBaseColors" or "_schemeOfficeColours" or "_ribbonColors" or "schemeColors")
                    {
                        // Use the provided scheme parameter for LightGray palettes; otherwise, pass a new scheme instance
                        replacement = _isLightGray ? SyntaxFactory.IdentifierName("scheme") : SchemeInstanceExpr;
                    }
                    else if (name is "_trackBarColors" or "trackBarColors")
                    {
                        // Track-bar colors are now supplied by the scheme instance; remove argument entirely
                        updated = true;
                        continue;
                    }
                }
                else if (expr is InvocationExpressionSyntax invExpr)
                {
                    var invText = invExpr.ToString().Replace(" ", string.Empty);
                    var patternToArray = $"new{_schemeClassName}().ToArray()";
                    var patternTrack = $"new{_schemeClassName}().ToTrackBarArray()";

                    if (string.Equals(invText, patternToArray, StringComparison.Ordinal))
                    {
                        replacement = SchemeInstanceExpr;
                    }
                    else if (string.Equals(invText, patternTrack, StringComparison.Ordinal))
                    {
                        updated = true;
                        continue; // skip track-bar argument
                    }
                }
                // If the expression already matches the intended SchemeCtorExpr, no replacement needed.

                if (replacement != null)
                {
                    updated = true;
                    // Preserve original leading/trailing trivia so indentation and line breaks stay intact
                    var newExpr = replacement.WithTriviaFrom(expr);
                    newArgsList.Add(arg.WithExpression(newExpr));
                }
                else
                {
                    newArgsList.Add(arg);
                }
            }

            // Helper to build separated lists with commas
            static SeparatedSyntaxList<T> ToSeparated<T>(IEnumerable<T> nodes) where T : SyntaxNode
            {
                var nodeList = nodes.ToList();
                if (nodeList.Count == 0) return default;
                if (nodeList.Count == 1) return SyntaxFactory.SeparatedList(nodeList);

                var separators = new List<SyntaxToken>();
                for (int i = 0; i < nodeList.Count - 1; i++)
                {
                    separators.Add(SyntaxFactory.Token(SyntaxKind.CommaToken).WithTrailingTrivia(SyntaxFactory.Space));
                }
                return SyntaxFactory.SeparatedList(nodeList, separators);
            }

            // Re-format: put each argument on its own indented line for readability
            var ctorIndentTrivia = node.GetLeadingTrivia()
                                        .LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

            // Base indentation of the constructor plus four more spaces
            var indentText = (ctorIndentTrivia == default ? string.Empty : ctorIndentTrivia.ToString()) + new string(' ', 4);

            var separated = new List<SyntaxNodeOrToken>();
            for (int i = 0; i < newArgsList.Count; i++)
            {
                var arg = newArgsList[i]
                    .WithLeadingTrivia(SyntaxFactory.ElasticCarriageReturnLineFeed, SyntaxFactory.Whitespace(indentText))
                    .WithTrailingTrivia(); // remove trailing space to avoid 'arg ,' pattern

                separated.Add(arg);

                if (i < newArgsList.Count - 1)
                {
                    // comma token followed by nothing (newline is part of next arg leading trivia)
                    separated.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                }
            }

            var newArgs = SyntaxFactory.SeparatedList<ArgumentSyntax>(separated);

            var originalArgsText = init.ArgumentList.Arguments.ToFullString();
            var newArgsText = newArgs.ToFullString();

            if (!updated && string.Equals(originalArgsText, newArgsText, StringComparison.Ordinal))
            {
                var visited = base.VisitConstructorDeclaration(node);
                return visited ?? node;
            }

            var newInit = init.WithArgumentList(init.ArgumentList.WithArguments(newArgs));
            var newNode = node.WithInitializer(newInit);
            if (_isLightGray)
            {
                newNode = newNode.WithParameterList(newParamList);
            }

            // Do NOT call NormalizeWhitespace here – it strips our custom indentation.

            HasChanges = true;

            var visitedNew = base.VisitConstructorDeclaration(newNode);
            return visitedNew ?? newNode;
        }
    }

    /// <summary>
    /// Rewrites the *existing* constructor of a palette-base class so it
    ///   • takes a single KryptonColorSchemeBase "scheme" parameter
    ///   • drops the two Color[] parameters
    ///   • rewrites body:  _ribbonColors = scheme.ToArray();
    ///   • adds "BaseColors = scheme;" as first statement
    /// A readonly field 'trackBarColors' is deleted by pre-existing removal
    /// code once it becomes unused.
    /// </summary>
    private sealed class BaseCtorSingleRewriter : CSharpSyntaxRewriter
    {
        private readonly string _schemeClassName;
        private readonly bool _isLightGray;
        public bool HasChanges { get; private set; }

        private ExpressionSyntax SchemeCtorExpr => SyntaxFactory.ParseExpression($"new {_schemeClassName}().ToArray()");
        private ExpressionSyntax TrackBarExpr  => SyntaxFactory.ParseExpression($"new {_schemeClassName}().ToTrackBarArray()");

        public BaseCtorSingleRewriter(string schemeClassName)
        {
            _schemeClassName = schemeClassName;
            _isLightGray = schemeClassName.Contains("LightGray", StringComparison.OrdinalIgnoreCase);
        }

        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            // Only process constructors that delegate to base(...)
            var init = node.Initializer;
            if (init == null || init.Kind() != SyntaxKind.BaseConstructorInitializer)
            {
                var visited = base.VisitConstructorDeclaration(node);
                return visited ?? node;
            }

            var args = init.ArgumentList.Arguments;
            bool updated = false;

            var newArgsList = new List<ArgumentSyntax>();

            // When converting LightGray palettes we also need to adjust the parameter list
            ParameterListSyntax newParamList = node.ParameterList;
            if (_isLightGray)
            {
                var paramBuilder = new List<ParameterSyntax>();
                bool schemeInserted = false;

                ParameterSyntax MakeSchemeParam()
                {
                    var type = SyntaxFactory.IdentifierName("KryptonColorSchemeBase").WithTrailingTrivia(SyntaxFactory.Space);
                    return SyntaxFactory.Parameter(SyntaxFactory.Identifier("scheme")).WithType(type);
                }

                foreach (var p in node.ParameterList.Parameters)
                {
                    var pName = p.Identifier.Text;
                    // Drop the two Color[] parameters
                    if (pName is "schemeColors" or "trackBarColors")
                    {
                        updated = true;
                        continue;
                    }

                    paramBuilder.Add(p);

                    // insert 'scheme' parameter immediately after themeName (if present)
                    if (!schemeInserted && pName == "themeName")
                    {
                        paramBuilder.Add(MakeSchemeParam());
                        schemeInserted = true;
                    }
                }

                if (!schemeInserted)
                {
                    // prepend if themeName not found
                    paramBuilder.Insert(0, MakeSchemeParam());
                }

                newParamList = node.ParameterList.WithParameters(ToSeparated(paramBuilder));
            }

            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                var expr = arg.Expression;

                ExpressionSyntax? replacement = null;

                if (expr is IdentifierNameSyntax id)
                {
                    var name = id.Identifier.Text;
                    if (name is "_schemeBaseColors" or "_schemeOfficeColours" or "_ribbonColors" or "schemeColors")
                    {
                        replacement = _isLightGray ? SyntaxFactory.IdentifierName("scheme") : SchemeCtorExpr;
                    }
                    else if (name is "_trackBarColors" or "trackBarColors" or "trackBarColors")
                    {
                        replacement = TrackBarExpr; // always pass track-bar array
                    }
                }
                // If the expression already matches the intended SchemeCtorExpr, no replacement needed.

                if (replacement != null)
                {
                    updated = true;
                    // Preserve original leading/trailing trivia so indentation and line breaks stay intact
                    var newExpr = replacement.WithTriviaFrom(expr);
                    newArgsList.Add(arg.WithExpression(newExpr));
                }
                else
                {
                    newArgsList.Add(arg);
                }
            }

            // Helper to build separated lists with commas
            static SeparatedSyntaxList<T> ToSeparated<T>(IEnumerable<T> nodes) where T : SyntaxNode
            {
                var nodeList = nodes.ToList();
                if (nodeList.Count == 0) return default;
                if (nodeList.Count == 1) return SyntaxFactory.SeparatedList(nodeList);

                var separators = new List<SyntaxToken>();
                for (int i = 0; i < nodeList.Count - 1; i++)
                {
                    separators.Add(SyntaxFactory.Token(SyntaxKind.CommaToken).WithTrailingTrivia(SyntaxFactory.Space));
                }
                return SyntaxFactory.SeparatedList(nodeList, separators);
            }

            // Re-format: put each argument on its own indented line for readability
            var ctorIndentTrivia = node.GetLeadingTrivia()
                                        .LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

            // Base indentation of the constructor plus four more spaces
            var indentText = (ctorIndentTrivia == default ? string.Empty : ctorIndentTrivia.ToString()) + new string(' ', 4);

            var separated = new List<SyntaxNodeOrToken>();
            for (int i = 0; i < newArgsList.Count; i++)
            {
                var arg = newArgsList[i]
                    .WithLeadingTrivia(SyntaxFactory.ElasticCarriageReturnLineFeed, SyntaxFactory.Whitespace(indentText))
                    .WithTrailingTrivia(); // remove trailing space to avoid 'arg ,' pattern

                separated.Add(arg);

                if (i < newArgsList.Count - 1)
                {
                    // comma token followed by nothing (newline is part of next arg leading trivia)
                    separated.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                }
            }

            var newArgs = SyntaxFactory.SeparatedList<ArgumentSyntax>(separated);

            var originalArgsText = init.ArgumentList.Arguments.ToFullString();
            var newArgsText = newArgs.ToFullString();

            if (!updated && string.Equals(originalArgsText, newArgsText, StringComparison.Ordinal))
            {
                var visited = base.VisitConstructorDeclaration(node);
                return visited ?? node;
            }

            var newInit = init.WithArgumentList(init.ArgumentList.WithArguments(newArgs));
            var newNode = node.WithInitializer(newInit);
            if (_isLightGray)
            {
                newNode = newNode.WithParameterList(newParamList);
            }

            // Do NOT call NormalizeWhitespace here – it strips our custom indentation.

            HasChanges = true;

            var visitedNew = base.VisitConstructorDeclaration(newNode);
            return visitedNew ?? newNode;
        }
    }

    private static IEnumerable<string> EnumeratePaletteFiles(string spec)
    {
        // If a directory path was supplied, gather all .cs files beneath it
        if (Directory.Exists(spec))
        {
            return Directory.GetFiles(Path.GetFullPath(spec), "*.cs", SearchOption.AllDirectories);
        }

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

        // Try resolving directory relative to repo root when not absolute / not found
        if (!Path.IsPathRooted(spec))
        {
            var altDir = Path.Combine(LocateRepoRoot(Directory.GetCurrentDirectory()), spec);
            if (Directory.Exists(altDir))
            {
                return Directory.GetFiles(altDir, "*.cs", SearchOption.AllDirectories);
            }
        }

        return Array.Empty<string>();
    }

    /// <summary>
    /// Parses the specified array variable using Roslyn and returns aligned color expressions and comments.
    /// </summary>
    private static List<string> ExtractColorsRoslyn(string filePath, string variableName, out List<string> commentsOut, IReadOnlyList<string> enumNames)
    {
        commentsOut = new List<string>();
        var code = GetFileText(filePath);
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
            if (string.IsNullOrWhiteSpace(colourExpr))
            {
                // Comment-only entry: mark as empty color so generated code remains valid
                colourExpr = "GlobalStaticValues.EMPTY_COLOR";
            }
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
                var separatorToken = default(SyntaxToken);
                try
                {
                    if (expr is InitializerExpressionSyntax ini)
                    {
                        var seps = ini.Expressions.GetSeparators().ToList();
                        if (idx < seps.Count)
                        {
                            separatorToken = seps[idx];
                        }
                    }
                    else if (expr is CollectionExpressionSyntax col)
                    {
                        var seps = col.Elements.GetSeparators().ToList();
                        if (idx < seps.Count)
                        {
                            separatorToken = seps[idx];
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Malformed initializer; ignore and continue without a separator token.
                    separatorToken = default;
                }
                // 'separatorToken' remains 'default' if no separator exists for this element

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

    /// <summary>
    /// Replaces all _ribbonColors[(int)SchemeBaseColors.SomeValue] and _schemeBaseColors[(int)SchemeBaseColors.SomeValue] entries with BaseColors.SomeValue properties<br/>
    /// Where "SomeValue" here is just an example property.
    /// </summary>
    /// <param name="file">Text to work on.</param>
    /// <returns>The updated result of the replace operation.</returns>
    private static string ConvertRibbonColorArrayEntriesToProperties(string text)
    {
        // Handles both _ribbonColors and _schemeBaseColors array accesses with optional (int) cast
        const string pattern = @"(?:_ribbonColou?rs|_schemeBaseColou?rs)\s*\[\s*(?:\(\s*int\s*\)\s*)?SchemeBaseColors\s*\.\s*(\w+)\s*\]";

        return Regex.Replace(text, pattern, m => $"BaseColors!.{m.Groups[1].Value}", RegexOptions.Singleline);
    }

    // Mapping for track-bar colors
    private static readonly Dictionary<string, string> TrackBarIndexMap = new Dictionary<string, string>(6)
    {
        { "0", "TrackBarTickMarks" },
        { "1", "TrackBarTopTrack" },
        { "2", "TrackBarBottomTrack" },
        { "3", "TrackBarFillTrack" },
        { "4", "TrackBarOutsidePosition" },
        { "5", "TrackBarBorderPosition" }
    };

    /// <summary>
    /// Replaces all _trackBarColors[n] entries with BaseColors.GridListPressed1 properties<br/>
    /// Where "GridListPressed1" here is just an example property.
    /// </summary>
    /// <param name="file">Text to work on.</param>
    /// <returns>The updated result of the replace operation.</returns>
    private static string ConvertTrackBarColorArrayEntriesToProperties(string text)
    {
        const string pattern = @"_trackBarColors\s*\[\s*(\d+)\s*\]";

        return Regex.Replace(text, pattern, m =>
        {
            var idx = m.Groups[1].Value;
            if (TrackBarIndexMap.TryGetValue(idx, out var prop))
            {
                return $"BaseColors!.{prop}";
            }
            throw new ArgumentOutOfRangeException($"Array index incorrect: {idx}");
        }, RegexOptions.Singleline);
    }

    /// <summary>
    /// Replaces occurrences of _schemeBaseColors inside KryptonColorTable constructor calls
    /// with BaseColors!.ToArray() so code continues to compile after the array field is removed.
    /// </summary>
    private static string ConvertColorTableArrayEntriesToBaseColors(string text)
    {
        const string pattern = @"new\s+KryptonColorTable[^\(]*\(\s*(?:_schemeBaseColors|_schemeOfficeColours|_ribbonColors)\s*,";

        return Regex.Replace(text, pattern, m => m.Value.Replace("_schemeBaseColors", "BaseColors!.ToArray()").Replace("_schemeOfficeColours", "BaseColors!.ToArray()").Replace("_ribbonColors", "BaseColors!.ToArray()"), RegexOptions.Singleline);
    }

    /// <summary>
    /// Ensures that a <c>protected readonly KryptonColorSchemeBase? BaseColors;</c> field is declared in the
    /// provided source text. If the declaration is missing, it is inserted immediately after a
    /// <c>#region Variables</c> directive when present, or as the first member inside the class body otherwise.
    /// </summary>
    /// <param name="text">C# source code to inspect/modify.</param>
    /// <returns>Updated source code that includes the field declaration.</returns>
    private static string EnsureBaseColorsField(string text, bool singleCtor)
    {
        // Detect an existing declaration irrespective of nullable annotation
        if (Regex.IsMatch(text, @"\bKryptonColorSchemeBase\s*\??\s*BaseColors\s*;"))
        {
            return text; // Already present – nothing to do
        }

        // Apply only to classes that explicitly derive from PaletteBase
        if (!Regex.IsMatch(text, @"class\s+\w+\s*:\s*[^\{\r\n]*\bPaletteBase\b", RegexOptions.Singleline))
        {
            return text; // Not derived from PaletteBase – do not insert
        }

        var lines = text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None).ToList();

        // Attempt to insert inside a '#region Variables' or '#region Instance Fields' block if it exists
        int regionIdx = lines.FindIndex(l => Regex.IsMatch(l, @"#region\s+(Variables|Instance\s+Fields)", RegexOptions.IgnoreCase));
        int insertIdx = -1;
        string indent = "    "; // default 4-space indent

        // Treat any abstract class derived from PaletteBase as an umbrella "base" palette class
        bool isBaseClass = Regex.IsMatch(text, @"abstract\s+class\s+\w+\s*:\s*[^\{\r\n]*\bPaletteBase\b", RegexOptions.Singleline);

        if (regionIdx >= 0)
        {
            // Use indentation of the region line for consistency
            indent = new string(lines[regionIdx].TakeWhile(char.IsWhiteSpace).ToArray());
            insertIdx = regionIdx + 1;
            // Skip any blank lines following the region
            while (insertIdx < lines.Count && string.IsNullOrWhiteSpace(lines[insertIdx]))
            {
                insertIdx++;
            }
        }
        else
        {
            // Fallback: locate first '{' after the class declaration and insert on next line
            int classIdx = lines.FindIndex(l => l.Contains(" class "));
            if (classIdx >= 0)
            {
                // Find the opening brace
                int braceLine = classIdx;
                while (braceLine < lines.Count && !lines[braceLine].Contains("{"))
                {
                    braceLine++;
                }
                if (braceLine < lines.Count)
                {
                    insertIdx = braceLine + 1;
                    indent = new string(lines[braceLine].TakeWhile(char.IsWhiteSpace).ToArray()) + "    ";
                }
            }
        }

        if (insertIdx < 0)
        {
            // As a last resort, append at the end before the closing brace
            insertIdx = lines.Count - 1;
        }

        var fieldLine = (isBaseClass && singleCtor)
            ? "protected readonly KryptonColorSchemeBase BaseColors;"
            : "protected readonly KryptonColorSchemeBase? BaseColors;";

        lines.Insert(insertIdx, indent + fieldLine);

        return string.Join("\r\n", lines);
    }

    /// <summary>
    /// Ensures that a constructor overload accepting a <see cref="KryptonColorSchemeBase"/> instance exists.
    /// If missing, a new constructor is inserted just before the <c>#endregion Identity</c> marker of the class.
    /// </summary>
    /// <param name="text">The source code to inspect/modify.</param>
    /// <returns>The updated source code.</returns>
    private static string EnsureSchemeConstructor(string text)
    {
        // 1. Detect palette class
        var mClass = Regex.Match(text, @"class\s+(\w+)\s*:\s*[^\r\n{]*\bPaletteBase\b");
        if (!mClass.Success)
        {
            return text; // Not relevant
        }

        string className = mClass.Groups[1].Value;

        // Special early handling for PaletteSparkleBase
        bool isSparkleBaseClass = className.StartsWith("PaletteSparkle", StringComparison.Ordinal) && className.EndsWith("Base", StringComparison.Ordinal);
        if (isSparkleBaseClass)
        {
            // Bail if appropriate ctor already exists
            if (Regex.IsMatch(text, $@"{Regex.Escape(className)}\s*\([^)]*KryptonColorSchemeBase[^)]*Color\[\][^)]*sparkleColors", RegexOptions.Singleline))
                return text;

            // Build Sparkle-specific overload
            var ind = "    ";
            var sbSpark = new StringBuilder();
            string visibilitySpark = Regex.IsMatch(text, $@"abstract\s+class\s+{Regex.Escape(className)}\b") ? "protected" : "public";
            sbSpark.AppendLine(ind + "/// <summary>");
            sbSpark.AppendLine(ind + "/// Overload that accepts a KryptonColorSchemeBase instance and forwards colours to the main constructor.");
            sbSpark.AppendLine(ind + "/// </summary>");
            sbSpark.AppendLine(ind + $"{visibilitySpark} {className}(");
            sbSpark.AppendLine(ind + "    [DisallowNull] KryptonColorSchemeBase scheme,");
            sbSpark.AppendLine(ind + "    [DisallowNull] Color[] sparkleColors,");
            sbSpark.AppendLine(ind + "    [DisallowNull] Color[] appButtonNormal,");
            sbSpark.AppendLine(ind + "    [DisallowNull] Color[] appButtonTrack,");
            sbSpark.AppendLine(ind + "    [DisallowNull] Color[] appButtonPressed,");
            sbSpark.AppendLine(ind + "    [DisallowNull] Color[] ribbonGroupCollapsedBorderContextTracking,");
            sbSpark.AppendLine(ind + "    [DisallowNull] ImageList checkBoxList,");
            sbSpark.AppendLine(ind + "    [DisallowNull] Image?[] radioButtonArray)");
            sbSpark.AppendLine(ind + "    : this(scheme.ToArray(), sparkleColors, appButtonNormal, appButtonTrack, appButtonPressed, ribbonGroupCollapsedBorderContextTracking, checkBoxList, radioButtonArray)");
            sbSpark.AppendLine(ind + "{");
            sbSpark.AppendLine(ind + "    BaseColors = scheme;");
            sbSpark.AppendLine(ind + "}");

            // Insert Sparkle ctor after existing primary one or at end of class
            int sCtorStart = text.IndexOf($"public {className}(", StringComparison.Ordinal);
            if (sCtorStart < 0) sCtorStart = text.IndexOf($"protected {className}(", StringComparison.Ordinal);

            int sInsertPos;
            if (sCtorStart >= 0)
            {
                int sBraceOpen = text.IndexOf('{', sCtorStart);
                int sDepth = 1; int sPos = sBraceOpen + 1;
                while (sPos < text.Length && sDepth > 0) { char c = text[sPos]; if (c=='{') sDepth++; else if (c=='}') sDepth--; sPos++; }
                sInsertPos = sPos;
            }
            else
            {
                sInsertPos = text.LastIndexOf('}');
                if (sInsertPos < 0) return text;
            }

            string newTextS = text.Substring(0, sInsertPos).TrimEnd() + "\r\n\r\n" + sbSpark.ToString() + "\r\n" + text.Substring(sInsertPos);
            return Regex.Replace(newTextS, "(\r\n){3,}", "\r\n\r\n");
        }

        // 2. Bail if constructor already present
        if (Regex.IsMatch(text, $@"{Regex.Escape(className)}\s*\([^)]*KryptonColorSchemeBase", RegexOptions.Singleline))
        {
            return text;
        }

        // 3. Determine if existing main ctor has themeName first parameter (attribute tolerant)
        bool hasThemeName = Regex.IsMatch(text, $@"{Regex.Escape(className)}\s*\(\s*(?:\[[^\]]*\]\s*)*string\s+themeName\b", RegexOptions.Singleline);

        // 4. Build new ctor source
        var indent = "    ";
        var sb = new StringBuilder();
        // Determine if target class is marked abstract (i.e. a palette *base* class)
        bool isAbstract = Regex.IsMatch(text, $@"abstract\s+class\s+{Regex.Escape(className)}\b");
        string visibility = isAbstract ? "protected" : "public";
        sb.AppendLine(indent + "/// <summary>");
        sb.AppendLine(indent + $"{visibility} {className}(");
        sb.AppendLine(indent + "/// Overload that accepts a KryptonColorSchemeBase instance and forwards colours to the main constructor.");
        sb.AppendLine(indent + "/// </summary>");
        sb.AppendLine(indent + "// TODO this should be merged into main constructor once all palettes");
        sb.AppendLine(indent + "// have their own KryptonColorSchemeBase-derived class");

        if (hasThemeName)
        {
            sb.AppendLine(indent + $"{visibility} {className}(");
            sb.AppendLine(indent + "    string themeName,");
            sb.AppendLine(indent + "    [DisallowNull] KryptonColorSchemeBase scheme,");
            sb.AppendLine(indent + "    [DisallowNull] ImageList checkBoxList,");
            sb.AppendLine(indent + "    [DisallowNull] ImageList galleryButtonList,");
            sb.AppendLine(indent + "    [DisallowNull] Image?[] radioButtonArray)");
            sb.AppendLine(indent + "    : this(themeName,");
            sb.AppendLine(indent + "           scheme.ToArray(),");
            sb.AppendLine(indent + "           checkBoxList,");
            sb.AppendLine(indent + "           galleryButtonList,");
            sb.AppendLine(indent + "           radioButtonArray,");
            sb.AppendLine(indent + "           scheme.ToTrackBarArray())");
        }
        else
        {
            sb.AppendLine(indent + $"{visibility} {className}(");
            sb.AppendLine(indent + "    [DisallowNull] KryptonColorSchemeBase scheme,");
            sb.AppendLine(indent + "    [DisallowNull] ImageList checkBoxList,");
            sb.AppendLine(indent + "    [DisallowNull] ImageList galleryButtonList,");
            sb.AppendLine(indent + "    [DisallowNull] Image?[] radioButtonArray)");
            sb.AppendLine(indent + "    : this(scheme.ToArray(),");
            sb.AppendLine(indent + "           checkBoxList,");
            sb.AppendLine(indent + "           galleryButtonList,");
            sb.AppendLine(indent + "           radioButtonArray,");
            sb.AppendLine(indent + "           scheme.ToTrackBarArray())");
        }

        sb.AppendLine(indent + "{");
        sb.AppendLine(indent + "    BaseColors = scheme;");
        sb.AppendLine(indent + "}");
        sb.AppendLine();

        // 5. Insert immediately after the primary constructor
        int ctorStart = text.IndexOf($"protected {className}(", StringComparison.Ordinal);
        if (ctorStart < 0)
        {
            // Try public ctor
            ctorStart = text.IndexOf($"public {className}(", StringComparison.Ordinal);
        }
        if (ctorStart < 0)
        {
            // No constructor found – fallback to end of class
            int lastBrace = text.LastIndexOf('}');
            if (lastBrace < 0) return text;
            var newTextLast = text.Substring(0, lastBrace).TrimEnd() + "\r\n\r\n" + sb.ToString() + "\r\n" + text.Substring(lastBrace);
            // collapse duplicate blank lines
            return Regex.Replace(newTextLast, "(\r\n){3,}", "\r\n\r\n");
        }

        // find the opening brace of that constructor
        int braceOpen = text.IndexOf('{', ctorStart);
        if (braceOpen < 0) return text;

        int depth = 1; // after first '{'
        int pos = braceOpen + 1;
        while (pos < text.Length && depth > 0)
        {
            char c = text[pos];
            if (c == '{') depth++; else if (c == '}') depth--;
            pos++;
        }
        int insertPos = pos; // position after closing brace

        string beforeCtor = text.Substring(0, insertPos);
        string afterCtor  = text.Substring(insertPos);
        var newTextInsert = beforeCtor.TrimEnd() + "\r\n\r\n" + sb.ToString() + "\r\n" + afterCtor;
        return Regex.Replace(newTextInsert, "(\r\n){3,}", "\r\n\r\n");
    }

    /// <summary>
    /// Ensures that a constructor overload accepting a <see cref="KryptonColorSchemeBase"/> instance exists.
    /// If missing, a new constructor is inserted just before the <c>#endregion Identity</c> marker of the class.
    /// </summary>
    /// <param name="text">The source code to inspect/modify.</param>
    /// <returns>The updated source code.</returns>
    private static string EnsureObsoleteOnColorArrayConstructors(string text)
    {
        // Apply only to classes that derive from PaletteBase (including abstract ones) to avoid
        // marking unrelated color-table helper classes.
        if (!Regex.IsMatch(text, @"class\s+\w+\s*:\s*[^\{\r\n]*\bPaletteBase\b", RegexOptions.Singleline))
            return text;

        // Pure textual processing – skip Roslyn to avoid trivia duplication issues.
        return InsertObsoleteAttributeLine(text);
    }

    /// <summary>
    /// Ensures that each Color[] constructor has the obsolete attribute placed on the line
    /// immediately preceding the constructor signature. This is a final textual fallback that
    /// avoids complex trivia handling issues.
    /// </summary>
    private static string InsertObsoleteAttributeLine(string text)
    {
        var lines = text.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();

        // Regex that roughly matches a constructor line containing a Color[] parameter.
        var ctorRegex = new Regex(@"^([ \t]*)(public|protected|internal)[ \t]+\w+[ \t]*\([^\)]*Color\[\]", RegexOptions.Compiled);

        // Attribute text constant
        const string AttrTemplate = "[System.Obsolete(\"Color[] constructor is deprecated and will be removed in V110. Use KryptonColorSchemeBase overload.\", false)]";

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var m = ctorRegex.Match(line);
            if (!m.Success)
                continue;

            bool alreadyHasAttr = false;
            int insertPos = i; // default to just before constructor

            // Find where XML docs end and attributes begin (if any)
            int j = i - 1;
            int firstAttributeLine = -1;
            int lastXmlDocLine = -1;

            // Walk backwards to analyze what's before the constructor
            while (j >= 0)
            {
                var prev = lines[j].TrimStart();

                if (prev.Length == 0)
                {
                    j--;
                    continue; // skip blank lines
                }

                if (prev.StartsWith("["))
                {
                    firstAttributeLine = j;
                    if (prev.Contains("Obsolete", StringComparison.OrdinalIgnoreCase))
                        alreadyHasAttr = true;
                }
                else if (prev.StartsWith("///"))
                {
                    if (lastXmlDocLine == -1)
                    {
                        // Find the last XML doc line by checking forward
                        lastXmlDocLine = j;
                        while (lastXmlDocLine + 1 < i && lines[lastXmlDocLine + 1].TrimStart().StartsWith("///"))
                        {
                            lastXmlDocLine++;
                        }
                    }
                    // Skip to before XML docs
                    while (j >= 0 && lines[j].TrimStart().StartsWith("///"))
                    {
                        j--;
                    }
                    break;
                }
                else
                {
                    break; // hit something else
                }

                j--;
            }

            if (alreadyHasAttr)
                continue; // constructor already marked

            // Determine insertion position
            if (firstAttributeLine >= 0)
            {
                // Insert before first attribute
                insertPos = firstAttributeLine;
            }
            else if (lastXmlDocLine >= 0)
            {
                // Insert after last XML doc line
                insertPos = lastXmlDocLine + 1;
            }
            // else use default (just before constructor)

            var indent = m.Groups[1].Value;
            lines.Insert(insertPos, indent + AttrTemplate);
            i++; // skip over inserted line so we don't process same ctor again
        }

        return string.Join("\r\n", lines);
    }

    // Generic helper used by both professional palettes
    private static void GenerateFromPalette(object paletteInstance, string className, string outputFolder, bool dryRun, bool overwrite)
    {
        var enumFile = Path.Combine(LocateRepoRoot(Directory.GetCurrentDirectory()),
            "Source", "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Enumerations", "PaletteEnumerations.cs");
        if (!File.Exists(enumFile)) throw new FileNotFoundException(enumFile);

        var enumNames = ParseEnumNames(enumFile);
        if (enumNames.Count == 0) throw new InvalidOperationException("Failed to read SchemeBaseColors enum.");

        FieldInfo? field = null;
        var type = paletteInstance.GetType();
        while (type != null && field == null)
        {
            field = type.GetField("_ribbonColors", BindingFlags.Instance | BindingFlags.NonPublic);
            type = type.BaseType;
        }
        if (field == null)
            throw new InvalidOperationException("_ribbonColors field not found – toolkit updated?");

        var arr = (Color[])field.GetValue(paletteInstance)!;

        var exprs = new List<string>(enumNames.Count);
        var missing = new List<bool>(enumNames.Count);
        for (int i = 0; i < enumNames.Count; i++)
        {
            if (i < arr.Length)
            {
                exprs.Add(ToColorExpr(arr[i]));
                missing.Add(false);
            }
            else
            {
                exprs.Add("GlobalStaticValues.EMPTY_COLOR");
                missing.Add(true);
            }
        }

        var code = GenerateSchemeCode(className, exprs, enumNames, missing);

        var outputDir = string.IsNullOrWhiteSpace(outputFolder) ? Directory.GetCurrentDirectory() : Path.GetFullPath(outputFolder);
        Directory.CreateDirectory(outputDir);
        var destPath = Path.Combine(outputDir, className + ".cs");

        if (dryRun)
        {
            Console.WriteLine(destPath);
            return;
        }

        if (File.Exists(destPath) && !overwrite)
        {
            Console.WriteLine(destPath + " *File exists, not replaced. Use --overwrite to force.");
            return;
        }

        SetFile(destPath, code);
        Console.WriteLine("Scheme written to " + destPath);
    }

    private static void ValidateFamilyBaseDryRun(string fileName, string text)
    {
        if (!IsPaletteClass(text)) return; // skip helper files

        bool ok = true;
        void Fail(string msg) { Console.Error.WriteLine($"[CHECK FAIL] {fileName}: {msg}"); ok = false; }

        // At least one obsolete attribute on Color[] ctor
        bool hasColorArrayCtor = Regex.IsMatch(text, @"\(\s*(?:\[[^\]]*\]\s*)*Color\[\]\s+\w+");
        if (hasColorArrayCtor && !Regex.IsMatch(text, @"\[\s*System\.Obsolete"))
            Fail("Missing [Obsolete] attribute on Color[] constructor");

        if (!Regex.IsMatch(text, @"private\s+readonly\s+Color\[\]\s+_ribbonColors"))
            Fail("_ribbonColors array field removed");

        // Disallow BaseColors.property reads except the "?.ToArray()/ToTrackBarArray()" helper accesses
        var matches = Regex.Matches(text, @"BaseColors\.([A-Za-z0-9_]+)");
        foreach (Match m in matches)
        {
            var prop = m.Groups[1].Value;
            if (prop != "ToArray()" && prop != "ToTrackBarArray()")
            {
                Fail("Found direct BaseColors property access: " + prop);
                break;
            }
        }

        // Ensure scheme overload exists
        if (!Regex.IsMatch(text, @"KryptonColorSchemeBase\s+scheme"))
            Fail("Scheme overload constructor missing");

        if (ok)
            Console.WriteLine($"[CHECK OK] {fileName} family-base validation passed.");
    }

    private static void ValidateThemeDryRun(string fileName, string text)
    {
        if (!IsPaletteClass(text)) return; // skip helper files

        bool ok = true;
        void Fail(string msg) { Console.Error.WriteLine($"[CHECK FAIL] {fileName}: {msg}"); ok = false; }

        bool hasArrayIndexers = Regex.IsMatch(text, @"_ribbonColors\[");
        if (hasArrayIndexers)
            Fail("Residual _ribbonColors indexers detected");

        bool baseColorsUsage = Regex.IsMatch(text, @"BaseColors\.[A-Z]");

        if (!baseColorsUsage && !hasArrayIndexers)
        {
            Console.WriteLine($"[UNSUPPORTED] {fileName} – palette not yet covered by automatic migration.");
            return;
        }

        if (!baseColorsUsage)
            Fail("No BaseColors property usages detected – conversion might have failed");

        bool hasColorArrayCtor = Regex.IsMatch(text, @"\(\s*(?:\[[^\]]*\]\s*)*Color\[\]\s+\w+");
        bool hasObsolete = Regex.IsMatch(text, @"\[\s*System\.Obsolete");
        if (hasColorArrayCtor && !hasObsolete)
            Fail("Obsolete attribute missing on Color[] constructor");

        if (ok)
            Console.WriteLine($"[CHECK OK] {fileName} theme validation passed.");
    }

    private static bool IsPaletteClass(string text)
        => Regex.IsMatch(text, @"class\s+\w+\s*:\s*[^\{\r\n]*\bPaletteBase\b")
           && !Regex.IsMatch(text, @"abstract\s+class");
}
