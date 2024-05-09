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

        public int Compare(IOutlookGridGroup? x,  IOutlookGridGroup? y)
        {
            /*
             * OutlookGridGroupCountComparer implements the IComparer<IOutlookGridGroup> interface.             
             * So added the [DisallowNull] attribs on request which solves the forgiving operators but that causes the next warning:
             * 
             * Warning CS8767: Nullability of reference types in type of parameter 'x' of 
             * 'int OutlookGridGroupCountComparer.Compare(IOutlookGridGroup x, IOutlookGridGroup y) 
             * doesn't match implicitly implemented member 
             * int IComparer<IOutlookGridGroup>.Compare(IOutlookGridGroup? x, IOutlookGridGroup? y) 
             * (possibly because of nullability attributes).
             * 
             * The interface dictates params to be nullable and does not allow the [DisallowNull] attribute. 
             * So there's not really a way around the use of the null forgiving operator.
             */
            try
            {
                int orderModifier = x!.Column.SortDirection == SortOrder.Ascending 
                    ? 1 
                    : -1;

                int c1 = x.ItemCount;
                int c2 = y!.ItemCount;
                int compareResult = c1.CompareTo(c2) * orderModifier;

                return compareResult == 0
                    ? x.CompareTo(y)
                    : compareResult;
            }
            catch (Exception ex)
            {
                throw new($"OutlookGridGroupCountComparer: {this}", ex);
            }
        }
        #endregion
    }
}