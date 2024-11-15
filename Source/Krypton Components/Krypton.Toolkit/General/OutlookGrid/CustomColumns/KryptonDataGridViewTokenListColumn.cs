﻿#region Licences

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
    /// Class for a rating column
    /// </summary>
    public class KryptonDataGridViewTokenListColumn : KryptonDataGridViewTextBoxColumn
    {
        #region Identity

        /// <summary>
        /// Constructor
        /// </summary>
        public KryptonDataGridViewTokenListColumn()
        {
            CellTemplate = new TokenListCell();
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ValueType = typeof(List<TokenListCell>);
        }

        #endregion
    }
}