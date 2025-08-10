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
    /// List of the current columns of the OutlookGrid
    /// </summary>
    public class OutlookGridColumnCollection : List<OutlookGridColumn>
    {
        #region Instance Fields

        private int _maxGroupIndex;
        private int _maxSortIndex;

        #endregion

        #region Identity

        /// <summary>
        /// Constructor
        /// </summary>
        public OutlookGridColumnCollection()
            : base()
        {
            _maxGroupIndex = -1;
            _maxSortIndex = -1;
        }

        #endregion

        #region Public

        /// <summary>
        /// Gets the OutlookGridColumn in the list by its name
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>OutlookGridColumn</returns>
        public OutlookGridColumn? this[string columnName] => Find( c => c.DataGridViewColumn.Name.Equals(columnName) );

        /// <summary>
        /// Gets or Sets the maximum GroupIndex in the collection
        /// </summary>
        public int MaxGroupIndex { get => _maxGroupIndex; set => _maxGroupIndex = value; }

        /// <summary>
        /// Gets or sets the maximum SortIndex in the collection
        /// </summary>
        public int MaxSortIndex { get => _maxSortIndex; set => _maxSortIndex = value; }

        #endregion

        #region Implementation

        /// <summary>
        /// Add an OutlookGridColumn to the collection.
        /// </summary>
        /// <param name="item">The OutlookGridColumn to add.</param>
        public new void Add(OutlookGridColumn item)
        {
            base.Add(item);

            if (item.GroupIndex > -1)
            {
                _maxGroupIndex++;
            }

            if (item.SortIndex > -1)
            {
                _maxSortIndex++;
            }
        }

        /// <summary>
        /// Gets the number of columns grouped
        /// </summary>
        /// <returns>the number of columns grouped.</returns>
        public int CountGrouped() => this.Count(c => c.IsGrouped);

        /// <summary>
        /// Gets the list of grouped columns
        /// </summary>
        /// <returns>The list of grouped columns.</returns>
        public List<OutlookGridColumn>? FindGroupedColumns() => this.Where(c => c.IsGrouped).OrderBy(c => c.GroupIndex).ToList();

        /// <summary>
        /// Gets a list of columns which are sorted and not grouped.
        /// </summary>
        /// <returns>List of Column indexes and SortDirection ordered by SortIndex.</returns>
        public List<Tuple<int, SortOrder, IComparer>> GetIndexAndSortGroupedColumns()
        {
            List<Tuple<int, SortOrder, IComparer>> res = new();
            var tmp = this.OrderBy(x => x.GroupIndex);
            foreach (OutlookGridColumn col in tmp)
            {
                if (col.IsGrouped && col.GroupIndex > -1)
                {
                    res.Add(Tuple.Create<int, SortOrder, IComparer>(col.DataGridViewColumn.Index, col.SortDirection, col.RowsComparer!));
                }
            }
            return res;
        }

        /// <summary>
        /// Gets the column from its real index (from the underlying DataGridViewColumn)
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The OutlookGridColumn.</returns>
        public OutlookGridColumn? FindFromColumnIndex(int index) => this.FirstOrDefault(c => c.DataGridViewColumn.Index == index);

        /// <summary>
        /// Gets the column from its name
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <returns>The associated OutlookGridColumn.</returns>
        public OutlookGridColumn? FindFromColumnName(string? name) => this.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets a list of columns which are sorted and not grouped.
        /// </summary>
        /// <returns>List of Column indexes and SortDirection ordered by SortIndex.</returns>
        public List<Tuple<int, SortOrder, IComparer>> GetIndexAndSortSortedOnlyColumns()
        {
            var res = new List<Tuple<int, SortOrder, IComparer>>();
            var tmp = this.OrderBy(x => x.SortIndex);
            foreach (OutlookGridColumn col in tmp)
            {
                if (!col.IsGrouped && col.SortIndex > -1)
                {
                    res.Add(Tuple.Create(col.DataGridViewColumn.Index, col.SortDirection, (IComparer)col.RowsComparer!));
                }
            }
            return res;
        }

        /// <summary>
        /// Removes a groupIndex and update the GroupIndex for all columns
        /// </summary>
        /// <param name="col">The OutlookGridColumn that will be removed.</param>
        internal void RemoveGroupIndex(OutlookGridColumn col)
        {
            int removed = col.GroupIndex;

            // TODO: Turn this into a foreach loop
            for (int i = 0; i < Count; i++)
            {
                if (this[i].GroupIndex > removed)
                {
                    this[i].GroupIndex--;
                }
            }
            _maxGroupIndex--;
            col.GroupIndex = -1;
        }

        /// <summary>
        ///  Removes a SortIndex and update the SortIndex for all columns
        /// </summary>
        /// <param name="col">The OutlookGridColumn that will be removed.</param>
        internal void RemoveSortIndex(OutlookGridColumn col)
        {
            int removed = col.SortIndex;

            // TODO: Turn this into a foreach loop
            for (int i = 0; i < Count; i++)
            {
                if (this[i].SortIndex > removed)
                {
                    this[i].SortIndex--;
                }
            }
            _maxSortIndex--;
            col.SortIndex = -1;
        }

        internal void ChangeGroupIndex(OutlookGridColumn outlookGridColumn)
        {
            int currentGroupIndex = -1;
            int newGroupIndex = outlookGridColumn.GroupIndex;

            // TODO: Turn this into a foreach loop
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == outlookGridColumn.Name)
                {
                    currentGroupIndex = this[i].GroupIndex;
                }
            }

            if (currentGroupIndex == -1)
            {
                throw new("OutlookGrid : Unable to interpret the change of GroupIndex!");
            }

#if (DEBUG)
            Console.WriteLine("currentGroupIndex=" + currentGroupIndex.ToString());
            Console.WriteLine("newGroupIndex=" + newGroupIndex.ToString());
            Console.WriteLine("Before");
            DebugOutput();
#endif

            for (int i = 0; i < Count; i++)
            {
                if (this[i].IsGrouped)
                {
                    if (this[i].GroupIndex == currentGroupIndex)
                    {
                        this[i].GroupIndex = newGroupIndex;
                    }
                    else if (this[i].GroupIndex >= newGroupIndex && this[i].GroupIndex < currentGroupIndex)
                    {
                        this[i].GroupIndex++;
                    }
                    else if (this[i].GroupIndex <= newGroupIndex && this[i].GroupIndex > currentGroupIndex)
                    {
                        this[i].GroupIndex--;
                    }
                }
            }
#if (DEBUG)
            Console.WriteLine("After");
            DebugOutput();
#endif
        }

        /// <summary>
        /// Outputs Debug information to the console.
        /// </summary>
        public void DebugOutput()
        {
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine($"{this[i].Name} , GroupIndex={this[i].GroupIndex}, SortIndex={this[i].SortIndex}");
            }
            Console.WriteLine($"MaxGroupIndex={_maxGroupIndex}, MaxSortIndex={_maxSortIndex}");
        }

        #endregion
    }
}