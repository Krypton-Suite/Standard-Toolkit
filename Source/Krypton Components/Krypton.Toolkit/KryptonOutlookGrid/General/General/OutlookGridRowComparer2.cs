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
        #region Instance Fields
        
        private readonly List<Tuple<int, SortOrder, IComparer>> _sortColumnIndexAndOrder;

        #endregion Instance Fields

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="OutlookGridRowComparer2"/> class.
        /// </summary>
        /// <param name="sortList">The sort list, tuple (column index, sortorder, Icomparer)</param>
        public OutlookGridRowComparer2(List<Tuple<int, SortOrder, IComparer>> sortList)
        {
            _sortColumnIndexAndOrder = sortList;
        }

        #endregion Identity

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

                        var cellX = x?.Cells[_sortColumnIndexAndOrder[i].Item1];
                        var cellY = y?.Cells[_sortColumnIndexAndOrder[i].Item1];

                        var valType = cellX?.ValueType;
                        var o1 = cellX?.Value;
                        var o2 = cellY?.Value;

                        if (_sortColumnIndexAndOrder[i].Item3 != null)
                        {
                            compareResult = _sortColumnIndexAndOrder[i].Item3.Compare(o1, o2) * orderModifier;
                        }
                        else if ((o1 == null || o1 == DBNull.Value) && o2 != null && o2 != DBNull.Value)
                        {
                            compareResult = 1;
                        }
                        else if (o1 != null && o1 != DBNull.Value && (o2 == null || o2 == DBNull.Value))
                        {
                            compareResult = -1;
                        }
                        else if (valType != null)
                        {
                            switch (Type.GetTypeCode(valType))
                            {
                                case TypeCode.String:
                                    compareResult = string.Compare(o1.ToStringNull(), o2.ToStringNull(), StringComparison.OrdinalIgnoreCase) * orderModifier;
                                    break;
                                case TypeCode.DateTime:
                                    compareResult = o1.ToDateTime().CompareTo(o2.ToDateTime()) * orderModifier;
                                    break;
                                case TypeCode.Int16:
                                case TypeCode.Int32:
                                case TypeCode.Int64:
                                case TypeCode.Byte:
                                case TypeCode.SByte:
                                case TypeCode.UInt16:
                                case TypeCode.UInt32:
                                case TypeCode.UInt64:
                                    // ToInteger/ToLong methods handle various integer types
                                    compareResult = o1.ToLong().CompareTo(o2.ToLong()) * orderModifier;
                                    break;
                                case TypeCode.Boolean:
                                    var b1 = o1.ToBoolean();
                                    var b2 = o2.ToBoolean();
                                    compareResult = (b1 == b2 ? 0 : b1 ? 1 : -1) * orderModifier;
                                    break;
                                case TypeCode.Single:
                                case TypeCode.Double:
                                case TypeCode.Decimal:
                                    // ToDouble/ToDecimal methods handle floating point types
                                    compareResult = o1.ToDecimal().CompareTo(o2.ToDecimal()) * orderModifier; // Using ToDecimal as it's the most precise for general numeric
                                    break;
                                case TypeCode.Object:
                                    if (o1 is TimeSpan ts1 && o2 is TimeSpan ts2)
                                    {
                                        compareResult = ts1.CompareTo(ts2) * orderModifier;
                                    }
                                    else if (o1 is TextAndImage ti1 && o2 is TextAndImage ti2)
                                    {
                                        compareResult = ti1.CompareTo(ti2) * orderModifier;
                                    }
                                    else if (o1 is Token tok1 && o2 is Token tok2)
                                    {
                                        compareResult = tok1.CompareTo(tok2) * orderModifier;
                                    }
                                    else if (o1 is IComparable comparable1) // Fallback for other IComparable objects
                                    {
                                        // This cast is safe if o1 is IComparable, but o2 might not be.
                                        // Consider if o2 also needs to be IComparable or if string comparison is safer here.
                                        // For strict original logic adherence, keep it.
                                        compareResult = comparable1.CompareTo(o2) * orderModifier;
                                    }
                                    else
                                    {
                                        // Original logic for types not explicitly handled in the if/else if chain
                                        compareResult = string.Compare(o1.ToStringNull(), o2.ToStringNull(), StringComparison.OrdinalIgnoreCase) * orderModifier;
                                    }
                                    break;
                                default:
                                    // This default case covers any other TypeCode not explicitly handled above
                                    // and retains the string comparison logic.
                                    compareResult = string.Compare(o1.ToStringNull(), o2.ToStringNull(), StringComparison.OrdinalIgnoreCase) * orderModifier;
                                    break;
                            }
                        }
                        else
                        {
                            switch (o1)
                            {
                                case string s1:
                                    compareResult = string.Compare(s1, o2.ToStringNull(), StringComparison.OrdinalIgnoreCase) * orderModifier;
                                    break;
                                case DateTime dt1:
                                    compareResult = dt1.CompareTo(o2.ToDateTime()) * orderModifier;
                                    break;
                                case int i1:
                                    compareResult = i1.CompareTo(o2.ToInteger()) * orderModifier;
                                    break;
                                case bool b1:
                                    var b2 = o2.ToBoolean();
                                    compareResult = (b1 == b2 ? 0 : b1 ? 1 : -1) * orderModifier;
                                    break;
                                case float f1:
                                    compareResult = f1.CompareTo((float)o2.ToDouble()) * orderModifier; // Convert o2 to double then float for consistency
                                    break;
                                case double d1:
                                    compareResult = d1.CompareTo(o2.ToDouble()) * orderModifier;
                                    break;
                                case decimal dec1:
                                    compareResult = dec1.CompareTo(o2.ToDecimal()) * orderModifier;
                                    break;
                                case long l1:
                                    compareResult = l1.CompareTo(o2.ToLong()) * orderModifier;
                                    break;
                                case TimeSpan ts1:
                                    compareResult = ts1.CompareTo(o2 as TimeSpan?) * orderModifier;
                                    break;
                                case TextAndImage ti1:
                                    compareResult = ti1.CompareTo(o2 as TextAndImage) * orderModifier;
                                    break;
                                case Token tok1:
                                    compareResult = tok1.CompareTo(o2 as Token) * orderModifier;
                                    break;
                                default:
                                    // Fallback for types not explicitly handled above, mirroring the original implicit
                                    // This will catch types like sbyte, byte, short, ushort, uint, ulong, or any other object
                                    // that didn't match an explicit case.
                                    compareResult = string.Compare(o1.ToStringNull(), o2.ToStringNull(), StringComparison.OrdinalIgnoreCase) * orderModifier;
                                    break;
                            }
                        }
                    }
                }
                return compareResult;
            }
            catch (Exception ex)
            {
                throw new Exception($"OutlookGridRowComparer: {ToString()}", ex);
            }
        }

        #endregion
    }
}