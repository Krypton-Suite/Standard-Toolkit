#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Outcome of <see cref="KryptonPaletteFile.UpgradeXmlToKthemexFromDirectory(string, bool)"/>.
/// Each converted <c>.xml</c> is rewritten as <c>.kthemex</c> beside the source; the source is left in place.
/// </summary>
public sealed class KryptonPaletteDirectoryUpgradeResult
{
    /// <summary>
    /// Empty result used when the folder dialog is cancelled or no files were scanned.
    /// </summary>
    public static KryptonPaletteDirectoryUpgradeResult Empty { get; } =
        new KryptonPaletteDirectoryUpgradeResult(
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<KryptonPaletteDirectoryUpgradeError>());

    internal KryptonPaletteDirectoryUpgradeResult(string[] convertedPaths,
        string[] sourcePaths,
        string[] skippedPaths,
        KryptonPaletteDirectoryUpgradeError[] errors)
    {
        ConvertedPaths = convertedPaths;
        SourcePaths = sourcePaths;
        SkippedPaths = skippedPaths;
        Errors = errors;
    }

    /// <summary>
    /// Full paths of written <c>.kthemex</c> files, in the same order as <see cref="SourcePaths"/>.
    /// </summary>
    public string[] ConvertedPaths { get; }

    /// <summary>
    /// Source <c>.xml</c> palette paths that were converted.
    /// </summary>
    public string[] SourcePaths { get; }

    /// <summary>
    /// <c>.xml</c> files that are not Krypton palette documents (left unchanged).
    /// </summary>
    public string[] SkippedPaths { get; }

    /// <summary>
    /// Palette <c>.xml</c> files that could not be converted.
    /// </summary>
    public KryptonPaletteDirectoryUpgradeError[] Errors { get; }

    /// <summary>Number of palettes rewritten as <c>.kthemex</c>.</summary>
    public int ConvertedCount => ConvertedPaths.Length;

    /// <summary>Number of non-palette <c>.xml</c> files skipped.</summary>
    public int SkippedCount => SkippedPaths.Length;

    /// <summary>Number of conversion failures.</summary>
    public int ErrorCount => Errors.Length;

    /// <summary>
    /// Builds a short summary for a completion dialog or log line.
    /// </summary>
    /// <returns>Converted / skipped / failed counts, plus up to eight failure details.</returns>
    public string ToSummaryString()
    {
        var builder = new StringBuilder();
        builder.Append("Converted ");
        builder.Append(ConvertedCount);
        builder.AppendLine(" palette file(s) to .kthemex. Original .xml files were left in place.");

        if (SkippedCount > 0)
        {
            builder.Append("Skipped ");
            builder.Append(SkippedCount);
            builder.AppendLine(" .xml file(s) that are not Krypton palettes.");
        }

        if (ErrorCount > 0)
        {
            builder.Append("Failed: ");
            builder.Append(ErrorCount);
            builder.AppendLine(".");
            var shown = Math.Min(Errors.Length, 8);
            for (var i = 0; i < shown; i++)
            {
                builder.Append(Path.GetFileName(Errors[i].SourcePath));
                builder.Append(": ");
                builder.AppendLine(Errors[i].Message);
            }

            if (Errors.Length > shown)
            {
                builder.Append("… and ");
                builder.Append(Errors.Length - shown);
                builder.AppendLine(" more.");
            }
        }

        return builder.ToString();
    }
}

/// <summary>
/// One failed file from <see cref="KryptonPaletteFile.UpgradeXmlToKthemexFromDirectory(string, bool)"/>.
/// </summary>
public sealed class KryptonPaletteDirectoryUpgradeError
{
    internal KryptonPaletteDirectoryUpgradeError(string sourcePath, string message)
    {
        SourcePath = sourcePath;
        Message = message;
    }

    /// <summary>The <c>.xml</c> path that failed.</summary>
    public string SourcePath { get; }

    /// <summary>Exception or validation message.</summary>
    public string Message { get; }
}
