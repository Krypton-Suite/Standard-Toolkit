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
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Defines a KryptonDomainUpDown cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewDomainUpDownCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonDomainUpDown _paintingDomainUpDown;

        private const DataGridViewContentAlignment ANY_RIGHT = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
        private const DataGridViewContentAlignment ANY_CENTER = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewDomainUpDownEditingControl);
        private static readonly Type _defaultValueType = typeof(String);
        private static readonly Size _sizeLarge = new Size(10000, 10000);
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewDomainUpDownCell cell type
        /// </summary>
        public KryptonDataGridViewDomainUpDownCell()
        {
            // Create a thread specific KryptonNumericUpDown control used for the painting of the non-edited cells
            if (_paintingDomainUpDown == null)
            {
                _paintingDomainUpDown = new KryptonDomainUpDown();
                _paintingDomainUpDown.SetLayoutDisplayPadding(new Padding(0, 0, 0, -1));
                _paintingDomainUpDown.StateCommon.Border.Width = 0;
                _paintingDomainUpDown.StateCommon.Border.Draw = InheritBool.False;
            }
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "DataGridViewDomainUpDownCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
                   ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
        #endregion

        #region Public
        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType => _defaultEditType;

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType => base.ValueType ?? _defaultValueType;

        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView?.EditingControl == null)
            {
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
            }

            if (dataGridView.EditingControl is KryptonDomainUpDown domainUpDown)
            {
                if (OwningColumn is KryptonDataGridViewDomainUpDownColumn domainColumn)
                {
                    domainUpDown.Items.Clear();

                    foreach (ButtonSpec bs in domainUpDown.ButtonSpecs)
                    {
                        bs.Click -= OnButtonClick;
                    }

                    domainUpDown.ButtonSpecs.Clear();

                    if (domainUpDown.Controls[0].Controls[1] is TextBox textBox)
                    {
                        textBox.ClearUndo();
                    }
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
            object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            if (DataGridView.EditingControl is KryptonDomainUpDown domainUpDown)
            {
                domainUpDown.Items.Clear();
                domainUpDown.ButtonSpecs.Clear();

                if (OwningColumn is KryptonDataGridViewDomainUpDownColumn domainColumn)
                {
                    domainUpDown.Items.InsertRange(0, domainColumn.Items);

                    // Set this cell as the owner of the buttonspecs
                    domainUpDown.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in domainColumn.ButtonSpecs)
                    {
                        bs.Click += OnButtonClick;
                        domainUpDown.ButtonSpecs.Add(bs);
                    }
                }

                if (!(initialFormattedValue is string initialFormattedValueStr))
                {
                    domainUpDown.Text = string.Empty;
                }
                else
                {
                    domainUpDown.Text = initialFormattedValueStr;
                }
            }
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
            bool setSize,
            Rectangle cellBounds,
            Rectangle cellClip,
            DataGridViewCellStyle cellStyle,
            bool singleVerticalBorderAdded,
            bool singleHorizontalBorderAdded,
            bool isFirstDisplayedColumn,
            bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                singleVerticalBorderAdded, singleHorizontalBorderAdded,
                isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int ButtonsWidth = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
            {
                errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
            }
            else
            {
                errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;
            }

            return errorIconBounds;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
            {
                return new Size(-1, -1);
            }

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
                const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += ButtonsWidth + ButtonMargin;
            }

            return preferredSize;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewDomainUpDownColumn domainColumn = OwningColumn as KryptonDataGridViewDomainUpDownColumn;
            DataGridViewButtonSpecClickEventArgs args = new DataGridViewButtonSpecClickEventArgs(domainColumn, this, (ButtonSpecAny)sender);
            domainColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewDomainUpDownEditingControl EditingDomainUpDown => DataGridView.EditingControl as KryptonDataGridViewDomainUpDownEditingControl;

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
            DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            int preferredHeight = _paintingDomainUpDown.GetPreferredSize(_sizeLarge).Height + 2;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        private void OnCommonChange()
        {
            if ((DataGridView != null) && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                {
                    DataGridView.InvalidateColumn(ColumnIndex);
                }
                else
                {
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
                }
            }
        }

        private bool OwnsEditingDomainUpDown(int rowIndex)
        {
            if ((rowIndex == -1) || (DataGridView == null))
            {
                return false;
            }

            return (DataGridView.EditingControl is KryptonDataGridViewDomainUpDownEditingControl control)
                   && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
        }

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        #endregion

        #region Internal
        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & ANY_RIGHT) != 0)
            {
                return HorizontalAlignment.Right;
            }
            else if ((align & ANY_CENTER) != 0)
            {
                return HorizontalAlignment.Center;
            }
            else
            {
                return HorizontalAlignment.Left;
            }
        }
        #endregion
    }
}