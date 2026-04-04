#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal class ColumnHeaderCellEventArgs : EventArgs
{
    public MenuStrip? FilterMenu { get; private set; }

    public DataGridViewColumn Column { get; private set; }

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnHeaderCellEventArgs"/> class.
    /// </summary>
    /// <param name="filterMenu">The filter menu.</param>
    /// <param name="column">The column.</param>
    public ColumnHeaderCellEventArgs(MenuStrip? filterMenu, DataGridViewColumn column)
    {
        FilterMenu = filterMenu;

        Column = column;
    }

    #endregion
}