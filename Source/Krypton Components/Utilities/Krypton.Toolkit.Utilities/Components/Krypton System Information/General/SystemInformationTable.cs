#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Tabular result for a System Information category.
/// </summary>
internal sealed class SystemInformationTable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SystemInformationTable"/> class.
    /// </summary>
    /// <param name="columns">Column headers.</param>
    public SystemInformationTable(params string[] columns)
    {
        Columns = columns ?? Array.Empty<string>();
        Rows = new List<string[]>();
    }

    /// <summary>Gets the column headers.</summary>
    public string[] Columns { get; }

    /// <summary>Gets the data rows.</summary>
    public List<string[]> Rows { get; }

    /// <summary>Adds a row, padding or trimming to the column count.</summary>
    public void AddRow(params string?[] values)
    {
        var row = new string[Columns.Length];
        for (var i = 0; i < Columns.Length; i++)
        {
            row[i] = i < values.Length ? values[i] ?? string.Empty : string.Empty;
        }

        Rows.Add(row);
    }

    /// <summary>Creates a two-column Item/Value table.</summary>
    public static SystemInformationTable ItemValue()
    {
        var strings = KryptonSystemInformationStrings.Current;
        return new SystemInformationTable(strings.ColumnItem, strings.ColumnValue);
    }
}
