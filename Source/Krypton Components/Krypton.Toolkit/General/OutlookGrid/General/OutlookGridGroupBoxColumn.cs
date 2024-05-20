#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
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
    /// Column for the OutlookGrid GroupBox
    /// </summary>
    public class OutlookGridGroupBoxColumn : IEquatable<OutlookGridGroupBoxColumn>
    {
        #region Identity
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="columnText">The display text of the column.</param>
        /// <param name="sort">The column sort order.</param>
        /// <param name="groupingType">The name of the used OutlookGridGroup mode.</param>
        public OutlookGridGroupBoxColumn(string? columnName, string? columnText, SortOrder sort, string groupingType)
        {
            Text = columnText;
            ColumnName = columnName;
            SortDirection = sort;
            GroupingType = groupingType;
        }
        #endregion

        #region Public

        /// <summary>
        /// Gets or sets the associated Rectangle that represents the column
        /// </summary>
        public Rectangle Rect { get; set; }
        /// <summary>
        /// Gets or sets the HeaderText of the column.
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// Gets or sets the boolean that indicates if the column is in a pressed state.
        /// </summary>
        public bool Pressed { get; set; }
        /// <summary>
        /// Gets or sets the Sort direction of the column.
        /// </summary>
        public SortOrder SortDirection { get; set; }
        /// <summary>
        /// Gets or sets the associated column name
        /// </summary>
        public string? ColumnName { get; set; }
        /// <summary>
        /// Gets or sets the boolean that indicates if the column is currently being dragged.
        /// </summary>
        public bool IsMoving { get; set; }
        /// <summary>
        /// Gets or sets the boolean that indicates if the column is currently being hovered by the mouse.
        /// </summary>
        public bool IsHovered { get; set; }
        /// <summary>
        /// Gets or sets a string that corresponds to the name of the OutlookGridGroup
        /// </summary>
        public string GroupingType { get; set; }
        /// <summary>
        /// Gets or sets the date interval if the grouping type is OutlookDateTimeGroup
        /// </summary>
        public string? GroupInterval { get; set; }
        /// <summary>
        /// Gets or sets the boolean that indicates if the column should be grouped by using the count value
        /// </summary>
        public bool SortBySummaryCount { get; set; }

        #endregion

        #region Implementation
        /// <summary>
        /// Defines Equals method on the columnName
        /// </summary>
        /// <param name="other">The OutlookGridGroupBoxColumn to compare with.</param>
        /// <returns>True or False.</returns>
        public bool Equals(OutlookGridGroupBoxColumn? other) => ColumnName!.Equals(other!.ColumnName);

        #endregion
    }
}