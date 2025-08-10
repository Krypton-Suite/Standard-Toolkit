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
    /// Class for a KryptonDataGridViewFormattingColumn : KryptonDataGridViewTextBoxColumn with conditional formatting abilities
    /// </summary>
    /// <seealso cref="KryptonDataGridViewTextBoxColumn" />
    public class KryptonDataGridViewFormattingColumn : KryptonDataGridViewTextBoxColumn
    {
        #region Instance Fields

        private bool _contrastTextColor;

        #endregion

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="KryptonDataGridViewFormattingColumn"/> class.
        /// </summary>
        public KryptonDataGridViewFormattingColumn()
        {
            CellTemplate = new FormattingCell();
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ValueType = typeof(FormattingCell);
            ContrastTextColor = false;
        }

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [contrast text color].</summary>
        /// <value><c>true</c> if [contrast text color]; otherwise, <c>false</c>.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ContrastTextColor
        {
            get => _contrastTextColor; 
            
            set => _contrastTextColor = value;
        }

        #endregion
    }
}