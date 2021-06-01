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
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewButtonCell cells.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDataGridViewButtonColumn), "ToolboxBitmaps.KryptonButton.bmp")]
    public class KryptonDataGridViewButtonColumn : KryptonDataGridViewIconColumn
    {
        #region Static Fields
        private MethodInfo _miColumnCommonChange;
        private PropertyInfo _piUseColumnTextForButtonValueInternal;
        #endregion

        #region Instance Fields
        private string _text;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewButtonColumn class.
        /// </summary>
        public KryptonDataGridViewButtonColumn()
            : base(new KryptonDataGridViewButtonCell())
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            DefaultCellStyle = style;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewButtonColumn { Name=");
            // ReSharper disable RedundantBaseQualifier
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            // ReSharper restore RedundantBaseQualifier
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// This member overrides DataGridViewButtonColumn.Clone.
        /// </summary>
        /// <returns>New object instance.</returns>
        public override object Clone()
        {
            // Create a new instance
            KryptonDataGridViewButtonColumn clone = base.Clone() as KryptonDataGridViewButtonColumn;
            clone.Text = Text;
            return clone;
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
                if ((value != null) && !(value is KryptonDataGridViewButtonCell))
                {
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewButtonCell");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the column's default cell style.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        public override DataGridViewCellStyle DefaultCellStyle
        {
            get => base.DefaultCellStyle;
            set => base.DefaultCellStyle = value;
        }
        
        /// <summary>
        /// Gets or sets the default text displayed on the button cell.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        public string Text
        {
            get => _text;
            set
            {
                if (!string.Equals(value, _text, StringComparison.Ordinal))
                {
                    _text = value;
                    if (DataGridView != null)
                    {
                        if (UseColumnTextForButtonValue)
                        {
                            ColumnCommonChange(Index);
                        }
                        else
                        {
                            DataGridViewRowCollection rows = DataGridView.Rows;
                            int count = rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                if ((rows.SharedRow(i).Cells[Index] is KryptonDataGridViewButtonCell cell) && cell.UseColumnTextForButtonValue)
                                {
                                    ColumnCommonChange(Index);
                                    return;
                                }
                            }
                            DataGridView.InvalidateColumn(Index);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Text property value is displayed as the button text for cells in this column.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool UseColumnTextForButtonValue
        {
            get
            {
                if (CellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewButtonColumn cell template required");
                }

                return ((KryptonDataGridViewButtonCell)CellTemplate).UseColumnTextForButtonValue;
            }

            set
            {
                if (UseColumnTextForButtonValue != value)
                {
                    SetUseColumnTextForButtonValueInternal(CellTemplate, value);
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewButtonCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewButtonCell;
                            if (cell != null)
                            {
                                SetUseColumnTextForButtonValueInternal(cell, value);
                            }
                        }
                        ColumnCommonChange(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Text property value is displayed as the button text for cells in this column.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(ButtonStyle), "Standalone")]
        public ButtonStyle ButtonStyle
        {
            get
            {
                if (CellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewButtonColumn cell template required");
                }

                return ((KryptonDataGridViewButtonCell)CellTemplate).ButtonStyle;
            }

            set
            {
                if (ButtonStyle != value)
                {
                    ((KryptonDataGridViewButtonCell)CellTemplate).ButtonStyleInternal = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (rows.SharedRow(i).Cells[Index] is KryptonDataGridViewButtonCell cell)
                            {
                                cell.ButtonStyleInternal = value;
                            }
                        }
                        ColumnCommonChange(Index);
                    }
                }
            }
        }
        #endregion

        #region Private
        private bool ShouldSerializeDefaultCellStyle()
        {
            if (!HasDefaultCellStyle)
            {
                return false;
            }

            DataGridViewCellStyle defaultCellStyle = DefaultCellStyle;
            if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && 
                  (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && 
                 (((defaultCellStyle.Font == null) && defaultCellStyle.IsNullValueDefault) && 
                  (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && 
                ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)) && 
                 ((defaultCellStyle.WrapMode == DataGridViewTriState.NotSet) && (defaultCellStyle.Tag == null))))
            {
                return !defaultCellStyle.Padding.Equals(Padding.Empty);
            }
            
            return true;
        }

        private void ColumnCommonChange(int columnIndex)
        {
            // Only need to cache reflection info the first time around
            if (_miColumnCommonChange == null)
            {
                // Cache access to the internal method 'OnColumnCommonChange'
                _miColumnCommonChange = typeof(DataGridView).GetMethod("OnColumnCommonChange", BindingFlags.Instance |
                                                                                               BindingFlags.NonPublic |
                                                                                               BindingFlags.GetField);

            }

            _miColumnCommonChange.Invoke(DataGridView, new object[] { columnIndex });
        }

        private void SetUseColumnTextForButtonValueInternal(object instance, bool value)
        {
            // Only need to cache reflection info the first time around
            if (_piUseColumnTextForButtonValueInternal == null)
            {
                // Cache access to the internal property sette 'UseColumnTextForButtonValueInternal'
                _piUseColumnTextForButtonValueInternal = typeof(DataGridViewButtonCell).GetProperty("UseColumnTextForButtonValueInternal", BindingFlags.Instance |
                                                                                                                                           BindingFlags.NonPublic |
                                                                                                                                           BindingFlags.SetProperty);

            }

            _piUseColumnTextForButtonValueInternal.SetValue(instance, value, null);
        }
        #endregion
    }
}