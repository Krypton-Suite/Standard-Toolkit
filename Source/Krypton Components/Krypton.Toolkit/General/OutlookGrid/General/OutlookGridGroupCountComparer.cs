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
    internal class OutlookGridGroupCountComparer : IComparer<IOutlookGridGroup>
    {
        public OutlookGridGroupCountComparer()
        {
        }

        #region IComparer Members

        public int Compare([DisallowNull] IOutlookGridGroup x, [DisallowNull] IOutlookGridGroup y)
        {
            int compareResult;
            try
            {
                int orderModifier = x.Column.SortDirection == SortOrder.Ascending ? 1 : -1;

                int c1 = x.ItemCount;
                int c2 = y.ItemCount;
                compareResult = c1.CompareTo(c2) * orderModifier;

                if (compareResult == 0)
                {
                    compareResult = x.CompareTo(y);
                }
                return compareResult;
            }
            catch (Exception ex)
            {
                throw new($"OutlookGridGroupCountComparer: {this}", ex);
            }
        }
        #endregion
    }
}