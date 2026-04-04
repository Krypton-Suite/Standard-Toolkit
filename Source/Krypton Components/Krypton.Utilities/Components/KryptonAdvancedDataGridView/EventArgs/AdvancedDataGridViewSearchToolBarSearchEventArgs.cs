#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

public class AdvancedDataGridViewSearchToolBarSearchEventArgs : EventArgs
{
    /// <summary>Gets the value to search.</summary>
    /// <value>The value to search.</value>
    public string ValueToSearch { get; private set; }

    /// <summary>
    /// Gets the column to search.
    /// </summary>
    /// <value>
    /// The column to search.
    /// </value>
    public DataGridViewColumn? ColumnToSearch { get; private set; }

    /// <summary>
    /// Gets a value indicating whether [case sensitive].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [case sensitive]; otherwise, <c>false</c>.
    /// </value>
    public bool CaseSensitive { get; private set; }

    /// <summary>
    /// Gets a value indicating whether [whole word].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [whole word]; otherwise, <c>false</c>.
    /// </value>
    public bool WholeWord { get; private set; }

    /// <summary>
    /// Gets a value indicating whether [from begin].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [from begin]; otherwise, <c>false</c>.
    /// </value>
    public bool FromBegin { get; private set; }

    /// <summary>Initializes a new instance of the <see cref="AdvancedDataGridViewSearchToolBarSearchEventArgs" /> class.</summary>
    /// <param name="value">The value.</param>
    /// <param name="column">The column.</param>
    /// <param name="case">if set to <c>true</c> [case].</param>
    /// <param name="whole">if set to <c>true</c> [whole].</param>
    /// <param name="fromBegin">if set to <c>true</c> [from begin].</param>
    public AdvancedDataGridViewSearchToolBarSearchEventArgs(string value, DataGridViewColumn? column, bool @case, bool whole, bool fromBegin)
    {
        ValueToSearch = value;
        ColumnToSearch = column;
        CaseSensitive = @case;
        WholeWord = whole;
        FromBegin = fromBegin;
    }
}