#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Describes how a translations XML/JSON document covers the current toolkit string catalog.
/// Unknown file entries are listed in <see cref="ExtraInFile"/>; toolkit keys absent from the file
/// appear in <see cref="MissingInFile"/> and keep built-in defaults after a tolerant import.
/// </summary>
public sealed class ToolkitStringsCoverage
{
    /// <summary>Initializes a new instance of the <see cref="ToolkitStringsCoverage"/> class.</summary>
    public ToolkitStringsCoverage()
    {
        MissingInFile = new List<string>();
        ExtraInFile = new List<string>();
        Applied = new List<string>();
    }

    /// <summary>Gets toolkit string paths present in the live catalog but absent from the file.</summary>
    public IList<string> MissingInFile { get; }

    /// <summary>Gets file string paths that do not map to any current toolkit property.</summary>
    public IList<string> ExtraInFile { get; }

    /// <summary>Gets toolkit string paths that were present in the file (after legacy-alias normalization).</summary>
    public IList<string> Applied { get; }

    /// <summary>Gets or sets the culture declared by the translations file, when available.</summary>
    public string? Culture { get; set; }

    /// <summary>Gets or sets the source file path when analysis was performed against a file.</summary>
    public string? FilePath { get; set; }

    /// <summary>Gets or sets the toolkit version stamp from the file, when available.</summary>
    public string? ToolkitVersion { get; set; }

    /// <summary>Gets or sets the structural format version from the file, when available.</summary>
    public int? FormatVersion { get; set; }

    /// <summary>Gets a value indicating whether the file is missing any current catalog keys.</summary>
    public bool HasMissing => MissingInFile.Count > 0;

    /// <summary>Gets a value indicating whether the file contains unknown/orphan keys.</summary>
    public bool HasExtra => ExtraInFile.Count > 0;

    /// <inheritdoc />
    public override string ToString() =>
        $@"Applied={Applied.Count}, Missing={MissingInFile.Count}, Extra={ExtraInFile.Count}";

    /// <summary>
    /// Groups dotted catalog paths by their first segment (for example <c>CommonStrings</c>).
    /// </summary>
    public static string SectionOf(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return @"(root)";
        }

        var dot = path.IndexOf('.');
        return dot < 0 ? path : path.Substring(0, dot);
    }

    /// <summary>
    /// Formats paths grouped by <see cref="SectionOf"/> for designer and TestForm summaries.
    /// </summary>
    public static string FormatGrouped(IList<string> paths)
    {
        if (paths == null || paths.Count == 0)
        {
            return @"none";
        }

        var groups = new Dictionary<string, List<string>>(StringComparer.Ordinal);
        foreach (var path in paths)
        {
            var section = SectionOf(path);
            if (!groups.TryGetValue(section, out var list))
            {
                list = new List<string>();
                groups[section] = list;
            }

            list.Add(path);
        }

        var sb = new StringBuilder();
        foreach (var pair in groups.OrderBy(p => p.Key, StringComparer.Ordinal))
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            sb.Append(pair.Key);
            sb.Append(@" (");
            sb.Append(pair.Value.Count.ToString(CultureInfo.InvariantCulture));
            sb.Append(@"): ");
            var sample = pair.Value.Take(6);
            sb.Append(string.Join(@", ", sample));
            if (pair.Value.Count > 6)
            {
                sb.Append(@"…");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Writes a CSV or JSON coverage report. <c>.json</c> writes JSON; any other extension writes CSV.
    /// </summary>
    public void ExportReport(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        var json = Path.GetExtension(filename).Equals(@".json", StringComparison.OrdinalIgnoreCase);
        File.WriteAllText(filename, json ? BuildJsonReport() : BuildCsvReport(), Encoding.UTF8);
    }

    private string BuildCsvReport()
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"Kind,Section,Path");
        AppendCsvRows(sb, @"Missing", MissingInFile);
        AppendCsvRows(sb, @"Extra", ExtraInFile);
        AppendCsvRows(sb, @"Applied", Applied);
        return sb.ToString();
    }

    private static void AppendCsvRows(StringBuilder sb, string kind, IList<string> paths)
    {
        foreach (var path in paths)
        {
            sb.Append(Csv(kind));
            sb.Append(',');
            sb.Append(Csv(SectionOf(path)));
            sb.Append(',');
            sb.AppendLine(Csv(path));
        }
    }

    private static string Csv(string value)
    {
        if (value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) < 0)
        {
            return value;
        }

        return $@"""{value.Replace(@"""", @"""""")}""";
    }

    private string BuildJsonReport()
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"{");
        sb.AppendLine($@"  ""culture"": ""{EscapeJson(Culture)}"",");
        sb.AppendLine($@"  ""toolkitVersion"": ""{EscapeJson(ToolkitVersion)}"",");
        sb.AppendLine($@"  ""formatVersion"": {(FormatVersion.HasValue ? FormatVersion.Value.ToString(CultureInfo.InvariantCulture) : @"null")},");
        sb.AppendLine($@"  ""filePath"": ""{EscapeJson(FilePath)}"",");
        AppendJsonArray(sb, @"missing", MissingInFile, trailingComma: true);
        AppendJsonArray(sb, @"extra", ExtraInFile, trailingComma: true);
        AppendJsonArray(sb, @"applied", Applied, trailingComma: false);
        sb.AppendLine(@"}");
        return sb.ToString();
    }

    private static void AppendJsonArray(StringBuilder sb, string name, IList<string> paths, bool trailingComma)
    {
        sb.Append($@"  ""{name}"": [");
        if (paths.Count == 0)
        {
            sb.AppendLine(trailingComma ? @"]," : @"]");
            return;
        }

        sb.AppendLine();
        for (var i = 0; i < paths.Count; i++)
        {
            sb.Append($@"    ""{EscapeJson(paths[i])}""");
            sb.AppendLine(i < paths.Count - 1 ? @"," : string.Empty);
        }

        sb.AppendLine(trailingComma ? @"  ]," : @"  ]");
    }

    private static string EscapeJson(string? value) =>
        (value ?? string.Empty).Replace(@"\", @"\\").Replace(@"""", @"\""").Replace("\r", @"\r").Replace("\n", @"\n");
}
