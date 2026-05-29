#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Classes;

/// <summary>
/// Holds the outcome of comparing a palette's _schemeOfficeColors array against the SchemeOfficeColors enum list.
/// </summary>
internal sealed class PaletteArrayIssues
{
    public int MissingCount { get; set; }
    public int UnlabelledCount { get; set; }
    public int OutOfOrderCount { get; set; }
    public int ExtraCount { get; set; }

    public List<int> MissingIndices { get; } = [];
    public List<int> UnlabelledIndices { get; } = [];
    public List<int> OutOfOrderIndices { get; } = [];
    public List<int> ExtraIndices { get; } = [];

    /// <summary>
    /// True when no discrepancies were found.
    /// </summary>
    public bool IsClean => MissingCount == 0
                           && UnlabelledCount == 0
                           && OutOfOrderCount == 0
                           && ExtraCount == 0;
}