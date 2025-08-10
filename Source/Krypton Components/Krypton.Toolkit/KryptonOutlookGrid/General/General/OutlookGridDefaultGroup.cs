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
    /// Each arrange/grouping class must implement the IOutlookGridGroup interface
    /// the Group object will determine for each object in the grid, whether it
    /// falls in or outside its group.
    /// It uses the IComparable.CompareTo function to determine if the item is in the group.
    /// This class group the elements by default (string, int, ...)
    /// </summary>
    public class OutlookGridDefaultGroup : IOutlookGridGroup
    {
        #region Instance Fields
        /// <summary>
        /// The Value of the group
        /// </summary>
        private object? _val;
        /// <summary>
        /// Boolean if the group is collapsed or not
        /// </summary>
        private bool _collapsed;
        /// <summary>
        /// The associated DataGridView column.
        /// </summary>
        private OutlookGridColumn? _column;
        /// <summary>
        /// The number of items in this group.
        /// </summary>
        private int _itemCount;
        /// <summary>
        /// The height (in pixels).
        /// </summary>
        private int _height;

        /// <summary>
        /// The string to format the value of the group
        /// </summary>
        private string _formatStyle;
        /// <summary>
        /// The picture associated to the group
        /// </summary>
        private Image? _groupImage;
        /// <summary>
        /// The text associated for the group text (1 item)
        /// </summary>
        private string? _oneItemText;
        /// <summary>
        /// The text associated for the group text (XXX items)
        /// </summary>
        private string? _xXxItemsText;
        /// <summary>
        /// Allows the column to be hidden when it is grouped by
        /// </summary>
        private bool _allowHiddenWhenGrouped;
        /// <summary>
        /// Sort groups using count items value
        /// </summary>
        private bool _sortBySummaryCount;

        private IComparer? _itemsComparer;
        #endregion

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="OutlookGridDefaultGroup"/> class.
        /// </summary>
        public OutlookGridDefaultGroup()
        {
            _val = null;
            _column = null;
            if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderMicrosoft365)
            {
                _height = GlobalStaticValues.Office2013GroupRowHeight; // special height for office 2013
            }
            else
            {
                _height = GlobalStaticValues.DefaultGroupRowHeight; // default height
            }

            Rows = new List<OutlookGridRow>();
            Children = new OutlookGridGroupCollection();
            _formatStyle = "";
            _oneItemText = KryptonManager.Strings.OutlookGridStrings.OneItem;
            _xXxItemsText = KryptonManager.Strings.OutlookGridStrings.NumberOfItems;
            _allowHiddenWhenGrouped = true;
            _sortBySummaryCount = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentGroup">The parent group if any.</param>
        public OutlookGridDefaultGroup(IOutlookGridGroup? parentGroup) : this()
        {
            if (parentGroup != null)
            {
                Children.ParentGroup = parentGroup;
            }
        }
        #endregion

        #region IOutlookGridGroup Members

        /// <summary>
        /// Gets or sets the list of rows associated to the group.
        /// </summary>
        public List<OutlookGridRow> Rows { get; set; }

        /// <summary>
        /// Gets or sets the parent group.
        /// </summary>
        /// <value>The parent group.</value>
        public IOutlookGridGroup? ParentGroup { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public OutlookGridGroupCollection Children { get; set; }

        /// <summary>
        /// Gets or sets the displayed text
        /// </summary>
        public virtual string Text
        {
            get
            {
                string formattedValue = "";
                string res = "";
                //For formatting number we need to cast the object value to the number before applying formatting
                if (_val == null || string.IsNullOrEmpty(_val.ToString()))
                {
                    formattedValue = KryptonManager.Strings.OutlookGridStrings.Unknown;
                }
                else if (!String.IsNullOrEmpty(_formatStyle))
                {
                    if (_val is string)
                    {
                        formattedValue = string.Format(_formatStyle, Value);
                    }
                    else if (_val is DateTime)
                    {
                        formattedValue = ((DateTime)Value!).ToString(_formatStyle);
                    }
                    else if (_val is int)
                    {
                        formattedValue = ((int)Value!).ToString(_formatStyle);
                    }
                    else if (_val is float)
                    {
                        formattedValue = ((float)Value!).ToString(_formatStyle);
                    }
                    else if (_val is double)
                    {
                        formattedValue = ((double)Value!).ToString(_formatStyle);
                    }
                    else if (_val is decimal)
                    {
                        formattedValue = ((decimal)Value!).ToString(_formatStyle);
                    }
                    else if (_val is long)
                    {
                        formattedValue = ((long)Value!).ToString(_formatStyle);
                    }
                    else if (_val is TimeSpan)
                    {
                        formattedValue = ((TimeSpan)Value!).ToString(_formatStyle);
                    }
                    else
                    {
                        formattedValue = Value!.ToString()!;
                    }
                }
                else
                {
                    formattedValue = Value!.ToString()!;
                }

                res =
                    $"{_column!.DataGridViewColumn!.HeaderText}: {formattedValue} ({(_itemCount == 1 ? _oneItemText : _itemCount + XxxItemsText)})";
                //if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                //    return res.ToUpper();
                //else
                return res;
            }
            //set
            //{
            //    text = value;
            //}
        }

        /// <summary>
        /// Gets a formatted string representing the aggregated summary text for the group.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property iterates through the cells of the <see cref="SummaryRow"/> associated with this group.
        /// For each cell that contains a non-empty value, it constructs a string combining the aggregation type,
        /// the header text of the corresponding column, and the formatted aggregated value.
        /// </para>
        /// <para>
        /// The aggregated value from the <see cref="SummaryRow"/> cell is formatted using the
        /// <see cref="System.Windows.Forms.DataGridViewCellStyle.Format"/> property of the respective grid column.
        /// If a format string is applied, the value is formatted accordingly; otherwise, its string representation is used directly.
        /// </para>
        /// <para>
        /// The resulting string concatenates these formatted summaries for all relevant columns,
        /// providing a comprehensive summary of the group's aggregated data.
        /// </para>
        /// <para>
        /// Returns an empty string if <see cref="SummaryRow"/> is <c>null</c> or if no cells contain summary values.
        /// </para>
        /// </remarks>
        public virtual string SummaryText
        {
            get
            {
                // Use a StringBuilder for efficient string concatenation, especially for many summaries.
                StringBuilder sb = new();

                if (SummaryRow == null)
                {
                    return string.Empty;
                }

                var grid = (KryptonOutlookGrid)this.Column.DataGridViewColumn.DataGridView!;

                // Add a line break at the start if you always want the summary on a new line after the group text.
                // Or, you can add it conditionally later in Paint/GetPreferredHeight.
                // For now, let's build each summary item with its own line break and indent.

                for (int i = 0; i < SummaryRow.Cells.Count; i++)
                {
                    if (!grid.Columns[i].Visible) continue;
                    var value = SummaryRow.Cells[i].Value == null ? string.Empty : SummaryRow.Cells[i].Value!.ToString();
                    // Only add to summary if the value is not null/empty
                    if (!string.IsNullOrEmpty(value))
                    {
                        var col = grid.FindFromColumnIndex(i)!;
                        string cellValueFormat = grid.Columns[i].DefaultCellStyle.Format;
                        string formattedAggregatedValueForCell = value;

                        if (!string.IsNullOrEmpty(cellValueFormat))
                        {
                            try
                            {
                                formattedAggregatedValueForCell = string.Format("{0:" + cellValueFormat + "}", SummaryRow.Cells[i].Value);
                            }
                            catch (FormatException)
                            {
                                // Fallback if format is invalid, keep original string
                                formattedAggregatedValueForCell = value;
                            }
                        }

                        // Append each summary item on a new line with indentation
                        // You can adjust the number of spaces for indentation.
                        sb.AppendLine($"  {col.AggregationType} of {col.DataGridViewColumn.HeaderText}: {formattedAggregatedValueForCell}");
                    }
                }

                // The TrimEnd() removes any trailing newlines if no summaries were added, or extra lines.
                // If you want a blank line at the very end, remove TrimEnd().
                return sb.ToString().TrimEnd();
            }
        }

        /// <summary>
        /// Gets or sets the Value of the group
        /// </summary>
        public virtual object? Value { get => _val; set => _val = value; }

        /// <summary>
        /// Boolean if the group is collapsed or not
        /// </summary>
        public virtual bool Collapsed { get => _collapsed; set => _collapsed = value; }

        /// <summary>
        /// Gets or sets the associated DataGridView column.
        /// </summary>
        [DisallowNull]
        public virtual OutlookGridColumn Column
        {
            get => _column!;
            set => _column = value ?? throw new NullReferenceException(GlobalStaticValues.PropertyCannotBeNull(nameof(this.Column)));
        }

        /// <summary>
        /// Gets or set the number of items in this group.
        /// </summary>
        public virtual int ItemCount { get => _itemCount; set => _itemCount = value; }

        /// <summary>
        /// Gets or sets the height (in pixels).
        /// </summary>
        public virtual int Height { get => _height; set => _height = value; }

        /// <summary>
        /// Gets or sets the Format Info.
        /// </summary>
        public virtual string FormatStyle { get => _formatStyle; set => _formatStyle = value; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        public virtual Image? GroupImage
        {
            get => _groupImage;
            set => _groupImage = value;
        }

        /// <summary>
        /// Gets or sets the text associated to One Item
        /// </summary>
        public virtual string? OneItemText
        {
            get => _oneItemText;
            set => _oneItemText = value;
        }

        /// <summary>
        /// Gets or sets the text associated to several Items
        /// </summary>
        public virtual string? XxxItemsText
        {
            get => _xXxItemsText;
            set => _xXxItemsText = value;
        }

        /// <summary>
        /// Gets or sets the boolean that hides the column automatically when grouped.
        /// </summary>
        public virtual bool AllowHiddenWhenGrouped
        {
            get => _allowHiddenWhenGrouped;
            set => _allowHiddenWhenGrouped = value;
        }

        /// <summary>
        /// Gets or sets the boolean that sort groups using summary value
        /// </summary>
        public virtual bool SortBySummaryCount
        {
            get => _sortBySummaryCount;
            set => _sortBySummaryCount = value;
        }

        /// <summary>
        /// Gets or sets the items comparer.
        /// </summary>
        /// <value>
        /// The items comparer.
        /// </value>
        [AllowNull]
        public virtual IComparer? ItemsComparer
        {
            get => _itemsComparer;
            set => _itemsComparer = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="OutlookGridRow"/> that represents the summary row for this group or the grand total.
        /// </summary>
        /// <remarks>
        /// This property holds a reference to the special row that displays aggregated values
        /// (e.g., sums, averages, counts) for the data rows within its associated group, or for the entire grid
        /// if it's a grand total summary row. It is set internally when summary rows are created.
        /// </remarks>
        public OutlookGridRow? SummaryRow { get; set; }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Overrides the Clone() function
        /// </summary>
        /// <returns>OutlookgGridDefaultGroup</returns>
        public virtual object Clone()
        {
            var gr = new OutlookGridDefaultGroup(ParentGroup)
            {
                _column = _column,
                _val = _val,
                _collapsed = _collapsed,
                _height = _height,
                _groupImage = _groupImage,
                _formatStyle = _formatStyle,
                _xXxItemsText = XxxItemsText,
                _oneItemText = OneItemText,
                _allowHiddenWhenGrouped = _allowHiddenWhenGrouped,
                _sortBySummaryCount = _sortBySummaryCount
            };

            return gr;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// This is a comparison operation based on the type of the value.
        /// </summary>
        /// <param name="obj">the value in the related column of the item to compare to</param>
        /// <returns></returns>
        public virtual int CompareTo(object? obj)
        {
            int orderModifier = Column.SortDirection == SortOrder.Ascending ? 1 : -1;
            int compareResult = 0;

            object? o2 = (obj as OutlookGridDefaultGroup)?.Value;

            if ((_val == null || _val == DBNull.Value) && o2 != null && o2 != DBNull.Value)
            {
                compareResult = 1;
            }
            else if (_val != null && _val != DBNull.Value && (o2 == null || o2 == DBNull.Value))
            {
                compareResult = -1;
            }
            else
            {
                if (_itemsComparer != null) // Use custom comparer if provided
                {
                    compareResult = _itemsComparer.Compare(_val, o2) * orderModifier;
                }
                else if (_val is string)
                {
                    compareResult = string.Compare(_val.ToString(), o2!.ToString()) * orderModifier;
                }
                else if (_val is DateTime)
                {
                    compareResult = ((DateTime)_val).CompareTo((DateTime)o2!) * orderModifier;
                }
                else if (_val is int)
                {
                    compareResult = ((int)_val).CompareTo((int)o2!) * orderModifier;
                }
                else if (_val is bool)
                {
                    bool b1 = (bool)_val;
                    bool b2 = (bool)o2!;
                    compareResult = (b1 == b2 ? 0 : b1 ? 1 : -1) * orderModifier;
                }
                else if (_val is float)
                {
                    float n1 = (float)_val;
                    float n2 = (float)o2!;
                    compareResult = (n1 > n2 ? 1 : n1 < n2 ? -1 : 0) * orderModifier;
                }
                else if (_val is double)
                {
                    double n1 = (double)_val;
                    double n2 = (double)o2!;
                    compareResult = (n1 > n2 ? 1 : n1 < n2 ? -1 : 0) * orderModifier;
                }
                else if (_val is decimal)
                {
                    decimal n1 = (decimal)_val;
                    decimal n2 = (decimal)o2!;
                    compareResult = (n1 > n2 ? 1 : n1 < n2 ? -1 : 0) * orderModifier;
                }
                else if (_val is long)
                {
                    long n1 = (long)_val;
                    long n2 = (long)o2!;
                    compareResult = (n1 > n2 ? 1 : n1 < n2 ? -1 : 0) * orderModifier;
                }
                else if (_val is TimeSpan)
                {
                    TimeSpan t1 = (TimeSpan)_val;
                    TimeSpan t2 = (TimeSpan)o2!;
                    compareResult = (t1 > t2 ? 1 : t1 < t2 ? -1 : 0) * orderModifier;
                }
                else if (_val is TextAndImage)
                {
                    compareResult = ((TextAndImage)_val).CompareTo((TextAndImage)o2!) * orderModifier;
                }
                //TODO implement a value for Token Column ??
                else if (_val is Token)
                {
                    compareResult = ((Token)_val).CompareTo((Token)o2!) * orderModifier;
                }
                else if (_val is IComparable comparableValue) // Fallback for other IComparable types
                {
                    compareResult = comparableValue.CompareTo(o2) * orderModifier;
                }
                else
                {
                    // If all else fails, use ToString() for comparison (might not be ideal for all types)
                    compareResult = string.Compare(_val?.ToString(), o2?.ToString()) * orderModifier;
                }
            }

            // If SortBySummaryCount is true, use ItemCount for comparison
            /*if (SortBySummaryCount)
            {
                IOutlookGridGroup? otherGroup = obj as IOutlookGridGroup;
                if (otherGroup != null)
                {
                    compareResult = _itemCount.CompareTo(otherGroup.ItemCount) * orderModifier;
                }
            }*/

            return compareResult;
        }
        #endregion
    }
}