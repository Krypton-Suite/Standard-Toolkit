﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewCheckBoxCell cells.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDataGridViewCheckBoxColumn), "ToolboxBitmaps.KryptonCheckBox.bmp")]
    public class KryptonDataGridViewCheckBoxColumn : KryptonDataGridViewIconColumn
    {
        #region Implementation
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxColumn class.
        /// </summary>
        public KryptonDataGridViewCheckBoxColumn()
            : this(false)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxColumn class.
        /// </summary>
        /// <param name="threeState">true to display check boxes with three states; false to display check boxes with two states.</param>
        public KryptonDataGridViewCheckBoxColumn(bool threeState)
            : base(new KryptonDataGridViewCheckBoxCell(threeState))
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            if (threeState)
            {
                style.NullValue = CheckState.Indeterminate;
            }
            else
            {
                style.NullValue = false;
            }

            DefaultCellStyle = style;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewCheckBoxColumn { Name=");
            // ReSharper disable RedundantBaseQualifier
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            // ReSharper restore RedundantBaseQualifier
            builder.Append(" }");
            return builder.ToString();
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the template used to model cell appearance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;

            set
            {
                if ((value != null) && !(value is KryptonDataGridViewCheckBoxCell))
                {
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewCheckBoxCell");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the underlying value corresponding to a cell value of false, which appears as an unchecked box.
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        public object FalseValue
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");
                }

                return CheckBoxCellTemplate.FalseValue;
            }
            set
            {
                if (FalseValue != value)
                {
                    CheckBoxCellTemplate.FalseValue = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                            {
                                cell.FalseValue = value;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the underlying value corresponding to an indeterminate or a null reference (Nothing in Visual Basic) cell value, which appears as a disabled checkbox.
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        public object IndeterminateValue
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");
                }

                return CheckBoxCellTemplate.IndeterminateValue;
            }
            set
            {
                if (IndeterminateValue != value)
                {
                    CheckBoxCellTemplate.IndeterminateValue = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                            {
                                cell.IndeterminateValue = value;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }        
        
        /// <summary>
        /// Gets or sets the underlying value corresponding to a cell value of true, which appears as a checked box.
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        public object TrueValue
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");
                }

                return CheckBoxCellTemplate.TrueValue;
            }
            set
            {
                if (TrueValue != value)
                {
                    CheckBoxCellTemplate.TrueValue = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                            {
                                cell.TrueValue = value;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hosted check box cells will allow three check states rather than two.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ThreeState
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");
                }

                return CheckBoxCellTemplate.ThreeState;
            }
            set
            {
                if (ThreeState != value)
                {
                    CheckBoxCellTemplate.ThreeState = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                            {
                                cell.ThreeState = value;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }

                    if (value 
                        && DefaultCellStyle.NullValue is bool b 
                        && !b
                        )
                    {
                        DefaultCellStyle.NullValue = CheckState.Indeterminate;
                    }
                    else if (!value 
                             && (DefaultCellStyle.NullValue is CheckState state) 
                             && (state == CheckState.Indeterminate)
                             )
                    {
                        DefaultCellStyle.NullValue = false;
                    }
                }
            }
        }
        #endregion

        #region Private
        private KryptonDataGridViewCheckBoxCell CheckBoxCellTemplate => (KryptonDataGridViewCheckBoxCell)CellTemplate;

        private bool ShouldSerializeDefaultCellStyle()
        {
            KryptonDataGridViewCheckBoxCell cellTemplate = CheckBoxCellTemplate;
            if (cellTemplate != null)
            {
                object indeterminate;
                if (cellTemplate.ThreeState)
                {
                    indeterminate = CheckState.Indeterminate;
                }
                else
                {
                    indeterminate = false;
                }

                // ReSharper disable RedundantBaseQualifier
                if (!base.HasDefaultCellStyle)
                {
                    return false;
                }
                // ReSharper restore RedundantBaseQualifier

                DataGridViewCellStyle defaultCellStyle = DefaultCellStyle;
                if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && (((defaultCellStyle.Font == null) && defaultCellStyle.NullValue.Equals(indeterminate)) && (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)) && ((defaultCellStyle.WrapMode == DataGridViewTriState.NotSet) && (defaultCellStyle.Tag == null))))
                {
                    return !defaultCellStyle.Padding.Equals(Padding.Empty);
                }
            }
            return true;
        }

        #endregion
    }
}