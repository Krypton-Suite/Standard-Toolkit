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
    /// Class for events of the group image of a group row.
    /// </summary>
    public class OutlookGridGroupImageEventArgs : EventArgs
    {
        private OutlookGridRow _row;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="row">The OutlookGridRow.</param>
        public OutlookGridGroupImageEventArgs(OutlookGridRow row)
        {
            _row = row;
        }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        public OutlookGridRow Row
        {
            get => _row;
            set => _row = value;
        }
    }
}