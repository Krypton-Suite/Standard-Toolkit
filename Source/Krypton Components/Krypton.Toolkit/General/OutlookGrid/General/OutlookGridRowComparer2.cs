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
    internal class OutlookGridRowComparer2 : IComparer<OutlookGridRow>
    {
        private readonly List<Tuple<int, SortOrder, IComparer>> _sortColumnIndexAndOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutlookGridRowComparer2"/> class.
        /// </summary>
        /// <param name="sortList">The sort list, tuple (column index, sortorder, Icomparer)</param>
        public OutlookGridRowComparer2(List<Tuple<int, SortOrder, IComparer>> sortList)
        {
            _sortColumnIndexAndOrder = sortList;
        }

        #region IComparer Members

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">OutlookGridRowComparer:  + this.ToString()</exception>
        public int Compare(OutlookGridRow? x, OutlookGridRow? y)
        {
            int compareResult = 0, orderModifier;

            try
            {
                for (var i = 0; i < _sortColumnIndexAndOrder.Count; i++)
                {
                    if (compareResult == 0)
                    {
                        orderModifier = _sortColumnIndexAndOrder[i].Item2 == SortOrder.Ascending ? 1 : -1;

                        var o1 = x!.Cells[_sortColumnIndexAndOrder[i].Item1].Value;
                        var o2 = y!.Cells[_sortColumnIndexAndOrder[i].Item1].Value;
                        if (_sortColumnIndexAndOrder[i].Item3 != null)
                        {
                            compareResult = _sortColumnIndexAndOrder[i].Item3.Compare(o1, o2) * orderModifier;
                        }
                        else
                        {
                            if ((o1 == null || o1 == DBNull.Value) && o2 != null && o2 != DBNull.Value)
                            {
                                compareResult = 1;
                            }
                            else if (o1 != null && o1 != DBNull.Value && (o2 == null || o2 == DBNull.Value))
                            {
                                compareResult = -1;
                            }
                            else
                            {
                                if (o1 is string)
                                {
                                    compareResult = string.CompareOrdinal(o1.ToString(), o2!.ToString()) * orderModifier;
                                }
                                else if (o1 is DateTime)
                                {
                                    compareResult = ((DateTime)o1).CompareTo((DateTime)o2!) * orderModifier;
                                }
                                else if (o1 is int)
                                {
                                    compareResult = ((int)o1).CompareTo((int)o2!) * orderModifier;
                                }
                                else if (o1 is bool)
                                {
                                    var b1 = (bool)o1;
                                    var b2 = (bool)o2!;
                                    compareResult = (b1 == b2 ? 0 : b1 ? 1 : -1) * orderModifier;
                                }
                                else if (o1 is float)
                                {
                                    var n1 = (float)o1;
                                    var n2 = (float)o2!;
                                    compareResult = (n1 > n2 ? 1 : n1 < n2 ? -1 : 0) * orderModifier;
                                }
                                else if (o1 is double)
                                {
                                    var n1 = (double)o1;
                                    var n2 = (double)o2!;
                                    compareResult = (n1 > n2 ? 1 : n1 < n2 ? -1 : 0) * orderModifier;
                                }
                                else if (o1 is decimal)
                                {
                                    var d1 = (decimal)o1;
                                    var d2 = (decimal)o2!;
                                    compareResult = (d1 > d2 ? 1 : d1 < d2 ? -1 : 0) * orderModifier;
                                }
                                else if (o1 is long)
                                {
                                    var n1 = (long)o1;
                                    var n2 = (long)o2!;
                                    compareResult = (n1 > n2 ? 1 : n1 < n2 ? -1 : 0) * orderModifier;
                                }
                                else if (o1 is TimeSpan)
                                {
                                    var t1 = (TimeSpan)o1;
                                    var t2 = (TimeSpan)o2!;
                                    compareResult = (t1 > t2 ? 1 : t1 < t2 ? -1 : 0) * orderModifier;
                                }
                                else if (o1 is TextAndImage)
                                {
                                    compareResult = ((TextAndImage)o1).CompareTo(o2 as TextAndImage) * orderModifier;
                                }
                                else if (o1 is Token)
                                {
                                    compareResult = ((Token)o1).CompareTo(o2 as Token) * orderModifier;
                                }
                            }
                        }
                    }
                }
                return compareResult;
            }
            catch (Exception ex)
            {
                throw new($"OutlookGridRowComparer: {ToString()}", ex);
            }
        }
        #endregion
    }
}