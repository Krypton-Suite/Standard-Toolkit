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
}