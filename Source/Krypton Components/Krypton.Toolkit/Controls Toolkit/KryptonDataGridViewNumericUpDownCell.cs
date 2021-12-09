#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Defines a KryptonNumericUpDown cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonNumericUpDown _paintingNumericUpDown;

        private const DataGridViewContentAlignment ANY_RIGHT =
            DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight |
            DataGridViewContentAlignment.BottomRight;
        private const DataGridViewContentAlignment ANY_CENTER =
            DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter |
            DataGridViewContentAlignment.BottomCenter;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewNumericUpDownEditingControl);
        private static readonly Type _defaultValueType = typeof(decimal);
        private static readonly Size _sizeLarge = new(10000, 10000);
        #endregion

        #region Instance Fields
        private int _decimalPlaces;
        private decimal _increment;
        private decimal _minimum;
        private decimal _maximum;
        private bool _thousandsSeparator;
        private bool _hexadecimal;
        private bool _allowDecimals;
        private bool _trailingZeroes;
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the DataGridViewNumericUpDownCell cell type
        /// </summary>
        public KryptonDataGridViewNumericUpDownCell()
        {
            // Create a thread specific KryptonNumericUpDown control used for the painting of the non-edited cells
            if (_paintingNumericUpDown == null)
            {
                _paintingNumericUpDown = new KryptonNumericUpDown();
                _paintingNumericUpDown.SetLayoutDisplayPadding(new Padding(0, 0, 0, -1));
                _paintingNumericUpDown.Maximum = decimal.MaxValue / 10;
                _paintingNumericUpDown.Minimum = decimal.MinValue / 10;
                _paintingNumericUpDown.StateCommon.Border.Width = 0;
                _paintingNumericUpDown.StateCommon.Border.Draw = InheritBool.False;
            }

            // Set the default values of the properties:
            _decimalPlaces = 0;
            _increment = decimal.One;
            _minimum = decimal.Zero;
            _maximum = (decimal)100.0;
            _thousandsSeparator = false;
            _hexadecimal = false;
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString() =>
            "DataGridViewNumericUpDownCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
            ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";

        #endregion

        #region Public
        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType => _defaultEditType;

        /// <summary>
        /// The AllowDecimals property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        [DefaultValue(true)]
        public bool AllowDecimals
        {
            get => _allowDecimals;
            set
            {
                if (_allowDecimals != value)
                {
                    SetAllowDecimals(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The TrailingZeroes property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        [DefaultValue(false)]
        public bool TrailingZeroes
        {
            get => _trailingZeroes;
            set
            {
                if (_trailingZeroes != value)
                {
                    SetTrailingZeroes(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The DecimalPlaces property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get => _decimalPlaces;

            set
            {
                if ((value < 0) || (value > 99))
                {
                    throw new ArgumentOutOfRangeException("The DecimalPlaces property cannot be smaller than 0 or larger than 99.");
                }

                if (_decimalPlaces != value)
                {
                    SetDecimalPlaces(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Indicates wheather the numeric up-down should display its value in hexadecimal.
        /// </summary>
        public bool Hexadecimal
        {
            get => _hexadecimal;

            set
            {
                if (_hexadecimal != value)
                {
                    SetHexadecimal(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Increment property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        public decimal Increment
        {
            get => _increment;

            set
            {
                if (value < (decimal)0.0)
                {
                    throw new ArgumentOutOfRangeException("The Increment property cannot be smaller than 0.");
                }

                SetIncrement(RowIndex, value);
            }
        }

        /// <summary>
        /// The Maximum property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        public decimal Maximum
        {
            get => _maximum;

            set
            {
                if (_maximum != value)
                {
                    SetMaximum(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Minimum property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        public decimal Minimum
        {
            get => _minimum;

            set
            {
                if (_minimum != value)
                {
                    SetMinimum(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ThousandsSeparator property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        [DefaultValue(false)]
        public bool ThousandsSeparator
        {
            get => _thousandsSeparator;

            set
            {
                if (_thousandsSeparator != value)
                {
                    SetThousandsSeparator(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType => base.ValueType ?? _defaultValueType;

        /// <summary>
        /// Clones a DataGridViewNumericUpDownCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            KryptonDataGridViewNumericUpDownCell dataGridViewCell = base.Clone() as KryptonDataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.DecimalPlaces = DecimalPlaces;
                dataGridViewCell.Increment = Increment;
                dataGridViewCell.Maximum = Maximum;
                dataGridViewCell.Minimum = Minimum;
                dataGridViewCell.ThousandsSeparator = ThousandsSeparator;
                dataGridViewCell.Hexadecimal = Hexadecimal;
                dataGridViewCell.AllowDecimals = AllowDecimals;
                dataGridViewCell.TrailingZeroes = TrailingZeroes;
            }
            return dataGridViewCell;
        }

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

            if (dataGridView.EditingControl is KryptonNumericUpDown numericUpDown)
            {
                if (OwningColumn is KryptonDataGridViewNumericUpDownColumn)
                {
                    foreach (ButtonSpec bs in numericUpDown.ButtonSpecs)
                    {
                        bs.Click -= OnButtonClick;
                    }

                    numericUpDown.ButtonSpecs.Clear();

                    if (numericUpDown.Controls[0].Controls[1] is TextBox textBox)
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

            if (DataGridView.EditingControl is KryptonNumericUpDown numericUpDown)
            {
                numericUpDown.DecimalPlaces = DecimalPlaces;
                numericUpDown.Increment = Increment;
                numericUpDown.Maximum = Maximum;
                numericUpDown.Minimum = Minimum;
                numericUpDown.ThousandsSeparator = ThousandsSeparator;
                numericUpDown.Hexadecimal = Hexadecimal;

                if (OwningColumn is KryptonDataGridViewNumericUpDownColumn numericColumn)
                {
                    // Set this cell as the owner of the buttonspecs
                    numericUpDown.ButtonSpecs.Clear();
                    numericUpDown.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in numericColumn.ButtonSpecs)
                    {
                        bs.Click += OnButtonClick;
                        numericUpDown.ButtonSpecs.Add(bs);
                    }
                }

                if (initialFormattedValue is not string initialFormattedValueStr)
                {
                    numericUpDown.Text = string.Empty;
                }
                else
                {
                    numericUpDown.Text = initialFormattedValueStr;
                }
            }
        }

        /// <summary>
        /// Custom implementation of the KeyEntersEditMode function. This function is called by the DataGridView control
        /// to decide whether a keystroke must start an editing session or not. In this case, a new session is started when
        /// a digit or negative sign key is hit.
        /// </summary>
        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            Keys negativeSignKey = Keys.None;
            var negativeSignStr = numberFormatInfo.NegativeSign;
            if (!string.IsNullOrEmpty(negativeSignStr) && (negativeSignStr.Length == 1))
            {
                negativeSignKey = (Keys)PI.VkKeyScan(negativeSignStr[0]);
            }

            return (char.IsDigit((char)e.KeyCode) ||
                    e.KeyCode is >= Keys.NumPad0 and <= Keys.NumPad9 ||
                    (negativeSignKey == e.KeyCode) ||
                    (Keys.Subtract == e.KeyCode)) &&
                   !e.Shift && !e.Alt && !e.Control;
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
            errorIconBounds.X = DataGridView.RightToLeft == RightToLeft.Yes
                ? errorIconBounds.Left + ButtonsWidth
                : errorIconBounds.Left - ButtonsWidth;

            return errorIconBounds;
        }

        /// <summary>
        /// Customized implementation of the GetFormattedValue function in order to include the decimal and thousand separator
        /// characters in the formatted representation of the cell value.
        /// </summary>
        protected override object GetFormattedValue(object value,
            int rowIndex,
            ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            // By default, the base implementation converts the Decimal 1234.5 into the string "1234.5"
            var formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
            var formattedNumber = formattedValue as string;
            if (!string.IsNullOrEmpty(formattedNumber)
                && (value != null)
                && (value != DBNull.Value)
                )
            {
                var unformattedDecimal = Convert.ToDecimal(value);
                var formattedDecimal = Convert.ToDecimal(formattedNumber);
                if (unformattedDecimal == formattedDecimal)
                {
                    if (!Hexadecimal && !TrailingZeroes)
                    {
                        return formattedDecimal.ToString("0.##############################");
                    }
                    // The base implementation of GetFormattedValue (which triggers the CellFormatting event) did nothing else than 
                    // the typical 1234.5 to "1234.5" conversion. But depending on the values of ThousandsSeparator and DecimalPlaces,
                    // this may not be the actual string displayed. The real formatted value may be "1,234.500"
                    return formattedDecimal.ToString((ThousandsSeparator ? "N" : "F") + DecimalPlaces.ToString());
                }
            }
            return formattedValue;
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
                const int BUTTONS_WIDTH = 16; // Account for the width of the up/down buttons.
                const int BUTTON_MARGIN = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += BUTTONS_WIDTH + BUTTON_MARGIN;
            }

            return preferredSize;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewNumericUpDownColumn numericColumn = OwningColumn as KryptonDataGridViewNumericUpDownColumn;
            DataGridViewButtonSpecClickEventArgs args = new(numericColumn, this, (ButtonSpecAny)sender);
            numericColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewNumericUpDownEditingControl EditingNumericUpDown => DataGridView.EditingControl as KryptonDataGridViewNumericUpDownEditingControl;

        private decimal Constrain(decimal value)
        {
            if (value < _minimum)
            {
                value = _minimum;
            }

            if (value > _maximum)
            {
                value = _maximum;
            }

            return value;
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
            DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            var preferredHeight = _paintingNumericUpDown.GetPreferredSize(_sizeLarge).Height + 2;
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
            if (DataGridView is { IsDisposed: false, Disposing: false })
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

        private bool OwnsEditingNumericUpDown(int rowIndex) =>
            (rowIndex == -1) || (DataGridView == null)
                ? false
                : (DataGridView.EditingControl is KryptonDataGridViewNumericUpDownEditingControl control)
                  && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart) => (paintParts & paintPart) != 0;

        #endregion

        #region Internal
        internal void SetAllowDecimals(int rowIndex, bool value)
        {
            _allowDecimals = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.AllowDecimals = value;
            }
        }

        internal void SetTrailingZeroes(int rowIndex, bool value)
        {
            _trailingZeroes = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.TrailingZeroes = value;
            }
        }

        internal void SetDecimalPlaces(int rowIndex, int value)
        {
            _decimalPlaces = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.DecimalPlaces = value;
            }
        }

        internal void SetHexadecimal(int rowIndex, bool value)
        {
            _hexadecimal = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.Hexadecimal = value;
            }
        }

        internal void SetIncrement(int rowIndex, decimal value)
        {
            _increment = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.Increment = value;
            }
        }

        internal void SetMaximum(int rowIndex, decimal value)
        {
            _maximum = value;
            if (_minimum > _maximum)
            {
                _minimum = _maximum;
            }

            var cellValue = GetValue(rowIndex);
            if (cellValue != null)
            {
                var currentValue = Convert.ToDecimal(cellValue);
                var constrainedValue = Constrain(currentValue);
                if (constrainedValue != currentValue)
                {
                    SetValue(rowIndex, constrainedValue);
                }
            }

            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.Maximum = value;
            }
        }

        internal void SetMinimum(int rowIndex, decimal value)
        {
            _minimum = value;
            if (_minimum > _maximum)
            {
                _maximum = value;
            }

            var cellValue = GetValue(rowIndex);
            if (cellValue != null)
            {
                var currentValue = Convert.ToDecimal(cellValue);
                var constrainedValue = Constrain(currentValue);
                if (constrainedValue != currentValue)
                {
                    SetValue(rowIndex, constrainedValue);
                }
            }

            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.Minimum = value;
            }
        }

        internal void SetThousandsSeparator(int rowIndex, bool value)
        {
            _thousandsSeparator = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.ThousandsSeparator = value;
            }
        }

        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            return align switch
            {
                ANY_RIGHT => HorizontalAlignment.Right,
                ANY_CENTER => HorizontalAlignment.Center,
                _ => HorizontalAlignment.Left
            };
        }
        #endregion
    }
}