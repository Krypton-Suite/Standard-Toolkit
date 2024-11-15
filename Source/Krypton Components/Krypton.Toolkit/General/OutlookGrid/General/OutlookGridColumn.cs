#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Column for the OutlookGrid
    /// </summary>
    public class OutlookGridColumn : IEquatable<OutlookGridColumn>
    {
        #region Identity
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        /// <param name="comparer">The comparer if needed.</param>
        public OutlookGridColumn(DataGridViewColumn col, IOutlookGridGroup group, SortOrder sortDirection, int groupIndex, int sortIndex, IComparer? comparer)
        {
            DataGridViewColumn = col;
            Name = col?.Name;
            GroupingType = group;
            SortDirection = sortDirection;
            GroupIndex = groupIndex;
            SortIndex = sortIndex;
            RowsComparer = comparer;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnName">The name.</param>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        /// <param name="comparer">The comparer if needed</param>
        public OutlookGridColumn(string? columnName, DataGridViewColumn? col, IOutlookGridGroup? group, SortOrder sortDirection, int groupIndex, int sortIndex, IComparer? comparer)
        {
            DataGridViewColumn = col!;
            Name = columnName;
            GroupingType = group;
            SortDirection = sortDirection;
            GroupIndex = groupIndex;
            SortIndex = sortIndex;
            RowsComparer = comparer;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnName">The name.</param>
        /// <param name="col">The DataGridViewColumn.</param>
        /// <param name="group">The group type for the column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="groupIndex">The column's position in grouping and at which level.</param>
        /// <param name="sortIndex">the column's position among sorted columns.</param>
        public OutlookGridColumn(string columnName, DataGridViewColumn col, IOutlookGridGroup group, SortOrder sortDirection, int groupIndex, int sortIndex)
        {
            DataGridViewColumn = col;
            Name = columnName;
            GroupingType = group;
            SortDirection = sortDirection;
            GroupIndex = groupIndex;
            SortIndex = sortIndex;
        }

        /// <summary>Initializes a new instance of the <see cref="OutlookGridColumn" /> class.</summary>
        /// <param name="dataGridViewColumn">The data grid view column.</param>
        /// <param name="group">The group.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="groupIndex">Index of the group.</param>
        /// <param name="sortIndex">Index of the sort.</param>
        public OutlookGridColumn(DataGridViewColumn dataGridViewColumn, IOutlookGridGroup? group, SortOrder sortOrder, int groupIndex, int sortIndex)
        {
            DataGridViewColumn = dataGridViewColumn;
            GroupingType = group;
            SortDirection = sortOrder;
            GroupIndex = groupIndex;
            SortIndex = sortIndex;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the column name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets if the column is grouped
        /// </summary>
        public bool IsGrouped => GroupIndex > -1;

        /// <summary>
        /// Gets or sets the sort direction
        /// </summary>
        public SortOrder SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the associated DataGridViewColumn
        /// </summary>
        public DataGridViewColumn DataGridViewColumn { get; set; }

        /// <summary>
        /// Gets or sets the group
        /// </summary>
        public IOutlookGridGroup? GroupingType { get; set; }

        /// <summary>
        /// Gets or sets the column's position in grouping and at which level
        /// </summary>
        public int GroupIndex { get; set; }

        /// <summary>
        /// Gets or sets the column's position among sorted columns
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// Gets or sets the custom row comparer, if needed.
        /// </summary>
        public IComparer? RowsComparer { get; set; }

        #endregion

        #region Implements

        /// <summary>Defines Equals method (interface IEquatable)</summary>
        /// <param name="other">The OutlookGridColumn to compare with</param>
        /// <returns></returns>
        public bool Equals(OutlookGridColumn? other) 
            // Use of [DisallowNull] not possible due to interface restrictions (earlier requested change reverted)
            => DataGridViewColumn.Name.Equals(other?.DataGridViewColumn.Name);

        #endregion
    }
}