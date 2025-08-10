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
    /// Hosts a collection of KryptonDataGridViewPercentageColumn cells.
    /// </summary>
    /// <seealso cref="DataGridViewColumn" />
    public class KryptonDataGridViewPercentageColumn : DataGridViewColumn// KryptonDataGridViewTextBoxColumn
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewPercentageColumn class.
        /// </summary>
        public KryptonDataGridViewPercentageColumn()
            : base(new DataGridViewPercentageCell()) => DefaultCellStyle.Format = "P";

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new(0x40);
            builder.Append("KryptonDataGridViewPercentageColumn { Name=");
            builder.Append(Name);
            builder.Append(", Index=");
            builder.Append(Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        #endregion

        #region Public Overrides

        /// <summary>
        /// Overrides CellTemplate
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell? CellTemplate
        {
            get => base.CellTemplate;

            set
            {
                // Ensure that the cell used for the template is a DataGridViewPercentageCell.
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewPercentageCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewPercentageCell");
                }
                base.CellTemplate = value;

            }
        }

        #endregion

    }
}