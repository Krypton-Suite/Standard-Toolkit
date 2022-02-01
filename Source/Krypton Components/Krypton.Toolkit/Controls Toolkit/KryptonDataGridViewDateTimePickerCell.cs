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
    /// Defines a KryptonDateTimePicker cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewDateTimePickerCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonDateTimePicker _paintingDateTime;
        private static readonly DateTimeConverter _dtc = new();
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewDateTimePickerEditingControl);
        private static readonly Type _defaultValueType = typeof(DateTime);
        private static readonly Size _sizeLarge = new(10000, 10000);
        #endregion

        #region Instance Fields
        private bool _showCheckBox;
        private bool _showUpDown;
        private bool _autoShift;
        private bool _checked;
        private string _customFormat;
        private string _customNullText;
        private DateTime _maxDate;
        private DateTime _minDate;
        private DateTimePickerFormat _format;
        private Size _calendarDimensions;
        private string _calendarTodayText;
        private Day _calendarFirstDayOfWeek;
        private bool _calendarShowToday;
        private bool _calendarCloseOnTodayClick;
        private bool _calendarShowTodayCircle;
        private bool _calendarShowWeekNumbers;
        private DateTime _calendarTodayDate;
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewDateTimePickerCell cell type
        /// </summary>
        public KryptonDataGridViewDateTimePickerCell()
        {
            // Create a thread specific KryptonDateTimePicker control used for the painting of the non-edited cells
            if (_paintingDateTime == null)
            {
                _paintingDateTime = new KryptonDateTimePicker
                {
                    ShowBorder = false
                };
                _paintingDateTime.StateCommon.Border.Width = 0;
                _paintingDateTime.StateCommon.Border.Draw = InheritBool.False;
            }

            // Set the default values of the properties:
            _showCheckBox = false;
            _showUpDown = false;
            _autoShift = false;
            _checked = false;
            _customFormat = string.Empty;
            _customNullText = " ";
            _maxDate = DateTime.MaxValue;
            _minDate = DateTime.MinValue;
            _format = DateTimePickerFormat.Long;
            _calendarDimensions = new Size(1,1);
            _calendarTodayText = "Today:";
            _calendarFirstDayOfWeek = Day.Default;
            _calendarShowToday = true;
            _calendarCloseOnTodayClick = false;
            _calendarShowTodayCircle = true;
            _calendarShowWeekNumbers = false;
            _calendarTodayDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString() => $"KryptonDataGridViewDateTimePickerCell {{ ColumnIndex={ColumnIndex.ToString(CultureInfo.CurrentCulture)}, RowIndex={RowIndex.ToString(CultureInfo.CurrentCulture)} }}";

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
        /// Clones a DataGridViewDateTimePickerCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            KryptonDataGridViewDateTimePickerCell dateTimeCell = base.Clone() as KryptonDataGridViewDateTimePickerCell;
            if (dateTimeCell != null)
            {
                dateTimeCell.AutoShift = AutoShift;
                dateTimeCell.Checked = Checked;
                dateTimeCell.ShowCheckBox = ShowCheckBox;
                dateTimeCell.ShowUpDown = ShowUpDown;
                dateTimeCell.CustomFormat = CustomFormat;
                dateTimeCell.CustomNullText = CustomNullText;
                dateTimeCell.MaxDate = MaxDate;
                dateTimeCell.MinDate = MinDate;
                dateTimeCell.Format = Format;
                dateTimeCell.CalendarDimensions = CalendarDimensions;
                dateTimeCell.CalendarTodayText = CalendarTodayText;
                dateTimeCell.CalendarFirstDayOfWeek = CalendarFirstDayOfWeek;
                dateTimeCell.CalendarShowToday = CalendarShowToday;
                dateTimeCell.CalendarCloseOnTodayClick = CalendarCloseOnTodayClick;
                dateTimeCell.CalendarShowTodayCircle = CalendarShowTodayCircle;
                dateTimeCell.CalendarShowWeekNumbers = CalendarShowWeekNumbers;
                dateTimeCell.CalendarTodayDate = CalendarTodayDate;
            }
            return dateTimeCell;
        }

        /// <summary>
        /// The ShowCheckBox property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool ShowCheckBox
        {
            get => _showCheckBox;

            set
            {
                if (_showCheckBox != value)
                {
                    SetShowCheckBox(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ShowUpDown property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool ShowUpDown
        {
            get => _showUpDown;

            set
            {
                if (_showUpDown != value)
                {
                    SetShowUpDown(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The AutoShift property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool AutoShift
        {
            get => _autoShift;

            set
            {
                if (_autoShift != value)
                {
                    SetAutoShift(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Checked property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool Checked
        {
            get => _checked;

            set
            {
                if (_checked != value)
                {
                    SetChecked(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CustomFormat property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue("")]
        public string CustomFormat
        {
            get => _customFormat;

            set
            {
                if (_customFormat != value)
                {
                    SetCustomFormat(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CustomNullText property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(" ")]
        public string CustomNullText
        {
            get => _customNullText;

            set
            {
                if (_customNullText != value)
                {
                    SetCustomNullText(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The MaxDate property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        public DateTime MaxDate
        {
            get => _maxDate;

            set
            {
                if (_maxDate != value)
                {
                    SetMaxDate(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Should the MaxDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMaxDate() => (MaxDate != DateTimePicker.MaximumDateTime) && (MaxDate != DateTime.MaxValue);

        private void ResetMaxDate() => MaxDate = DateTime.MaxValue;

        /// <summary>
        /// The MaxDate property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        public DateTime MinDate
        {
            get => _minDate;

            set
            {
                if (_minDate != value)
                {
                    SetMinDate(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Should the MinDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMinDate() => (MinDate != DateTimePicker.MinimumDateTime) && (MinDate != DateTime.MinValue);

        private void ResetMinDate() => MinDate = DateTime.MinValue;

        /// <summary>
        /// The Format property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(typeof(DateTimePickerFormat), "Long")]
        public DateTimePickerFormat Format
        {
            get => _format;

            set
            {
                if (_format != value)
                {
                    SetFormat(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarDimensions property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(typeof(Size), "1,1")]
        public Size CalendarDimensions
        {
            get => _calendarDimensions;

            set
            {
                if (_calendarDimensions != value)
                {
                    SetCalendarDimensions(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarTodayText property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue("Today:")]
        public string CalendarTodayText
        {
            get => _calendarTodayText;

            set
            {
                if (_calendarTodayText != value)
                {
                    SetCalendarTodayText(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarFirstDayOfWeek property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(typeof(Day), "Default")]
        public Day CalendarFirstDayOfWeek
        {
            get => _calendarFirstDayOfWeek;

            set
            {
                if (_calendarFirstDayOfWeek != value)
                {
                    SetCalendarFirstDayOfWeek(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarShowToday property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarShowToday
        {
            get => _calendarShowToday;

            set
            {
                if (_calendarShowToday != value)
                {
                    SetCalendarShowToday(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarCloseOnTodayClick property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarCloseOnTodayClick
        {
            get => _calendarCloseOnTodayClick;

            set
            {
                if (_calendarCloseOnTodayClick != value)
                {
                    SetCalendarCloseOnTodayClick(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        
        /// <summary>
        /// The CalendarShowTodayCircle property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarShowTodayCircle
        {
            get => _calendarShowTodayCircle;

            set
            {
                if (_calendarShowTodayCircle != value)
                {
                    SetCalendarShowTodayCircle(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarShowWeekNumbers property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarShowWeekNumbers
        {
            get => _calendarShowWeekNumbers;

            set
            {
                if (_calendarShowWeekNumbers != value)
                {
                    SetCalendarShowWeekNumbers(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarTodayDate property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public DateTime CalendarTodayDate
        {
            get => _calendarTodayDate;

            set
            {
                if (_calendarTodayDate != value)
                {
                    SetCalendarTodayDate(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        private void ResetCalendarTodayDate() => CalendarTodayDate = DateTime.Now.Date;

        private bool ShouldSerializeCalendarTodayDate() => CalendarTodayDate != DateTime.Now.Date;

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

            if (dataGridView.EditingControl is KryptonDateTimePicker dateTimePicker)
            {
                if (OwningColumn is KryptonDataGridViewDateTimePickerColumn dateTimeColumn)
                {
                    foreach (ButtonSpec bs in dateTimePicker.ButtonSpecs)
                    {
                        bs.Click -= OnButtonClick;
                    }

                    dateTimePicker.ButtonSpecs.Clear();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonDateTimePicker editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
            object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            if (DataGridView.EditingControl is KryptonDateTimePicker dateTime)
            {
                if (OwningColumn is KryptonDataGridViewDateTimePickerColumn dateTimeColumn)
                {
                    dateTime.ShowCheckBox = ShowCheckBox;
                    dateTime.ShowUpDown = ShowUpDown;
                    dateTime.AutoShift = AutoShift;
                    dateTime.Checked = Checked;
                    dateTime.CustomFormat = CustomFormat;
                    dateTime.CustomNullText = CustomNullText;
                    dateTime.MaxDate = MaxDate;
                    dateTime.MinDate = MinDate;
                    dateTime.Format = Format;
                    dateTime.CalendarDimensions = CalendarDimensions;
                    dateTime.CalendarTodayText = CalendarTodayText;
                    dateTime.CalendarFirstDayOfWeek = CalendarFirstDayOfWeek;
                    dateTime.CalendarShowToday = CalendarShowToday;
                    dateTime.CalendarCloseOnTodayClick = CalendarCloseOnTodayClick;
                    dateTime.CalendarShowTodayCircle = CalendarShowTodayCircle;
                    dateTime.CalendarShowWeekNumbers = CalendarShowWeekNumbers;
                    dateTime.CalendarTodayDate = CalendarTodayDate;
                    dateTime.CalendarAnnuallyBoldedDates = dateTimeColumn.CalendarAnnuallyBoldedDates;
                    dateTime.CalendarMonthlyBoldedDates = dateTimeColumn.CalendarMonthlyBoldedDates;
                    dateTime.CalendarBoldedDates = dateTimeColumn.CalendarBoldedDates;

                    // Set this cell as the owner of the buttonspecs
                    dateTime.ButtonSpecs.Clear();
                    dateTime.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in dateTimeColumn.ButtonSpecs)
                    {
                        bs.Click += OnButtonClick;
                        dateTime.ButtonSpecs.Add(bs);
                    }
                }

                if ((initialFormattedValue is not string initialFormattedValueStr) || string.IsNullOrEmpty(initialFormattedValueStr))
                {
                    dateTime.ValueNullable = null;
                }
                else
                {
                    DateTime dt = (DateTime)_dtc.ConvertFromInvariantString(initialFormattedValueStr);
                    if (dt != null)
                    {
                        dateTime.Value = dt;
                    }
                    else
                    {
                        dateTime.Text = initialFormattedValueStr;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the value of the cell as formatted for display. 
        /// </summary>
        /// <param name="value">The value to be formatted.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <param name="cellStyle">The DataGridViewCellStyle in effect for the cell.</param>
        /// <param name="valueTypeConverter">A TypeConverter associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed</param>
        /// <param name="formattedValueTypeConverter">A TypeConverter associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
        /// <param name="context">A bitwise combination of DataGridViewDataErrorContexts values describing the context in which the formatted value is needed.</param>
        /// <returns></returns>
        protected override object GetFormattedValue(object value, int rowIndex, 
            ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter, 
            TypeConverter formattedValueTypeConverter, 
            DataGridViewDataErrorContexts context)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return string.Empty;
            }
            else
            {
                DateTime dt = (DateTime)value;
                if (dt != null)
                {
                    return _dtc.ConvertToInvariantString(dt);
                }
            }

            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        /// <summary>
        /// Converts a value formatted for display to an actual cell value.
        /// </summary>
        /// <param name="formattedValue">The display value of the cell.</param>
        /// <param name="cellStyle">The DataGridViewCellStyle in effect for the cell.</param>
        /// <param name="formattedValueTypeConverter">A TypeConverter for the display value type, or null to use the default converter.</param>
        /// <param name="valueTypeConverter">A TypeConverter for the cell value type, or null to use the default converter.</param>
        /// <returns></returns>
        public override object ParseFormattedValue(object formattedValue, 
            DataGridViewCellStyle cellStyle, 
            TypeConverter formattedValueTypeConverter, 
            TypeConverter valueTypeConverter)
        {
            if (formattedValue == null)
            {
                return DBNull.Value;
            }
            else
            {
                var stringValue = (string)formattedValue;
                return string.IsNullOrEmpty(stringValue) ? DBNull.Value : _dtc.ConvertFromInvariantString(stringValue);
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

        /// <summary>
        /// Custom paints the cell. The base implementation of the DataGridViewTextBoxCell type is called first,
        /// dropping the icon error and content foreground parts. Those two parts are painted by this custom implementation.
        /// In this sample, the non-edited KryptonDateTimePicker control is painted by using a call to Control.DrawToBitmap. This is
        /// an easy solution for painting controls but it's not necessarily the most performant. An alternative would be to paint
        /// the KryptonDateTimePicker control piece by piece (text and up/down buttons).
        /// </summary>
        protected override void Paint(Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (DataGridView == null)
            {
                return;
            }

            _paintingDateTime.RightToLeft = DataGridView.RightToLeft;
            _paintingDateTime.Format = Format;
            _paintingDateTime.CustomFormat = CustomFormat;
            _paintingDateTime.CustomNullText = CustomNullText;
            _paintingDateTime.MaxDate = MaxDate;
            _paintingDateTime.MinDate = MinDate;

            var drawText = CustomNullText;
            if ((value == null) || (value == DBNull.Value))
            {
                _paintingDateTime.ValueNullable = value;
                _paintingDateTime.PerformLayout();
            }
            else
            {
                _paintingDateTime.Value = (DateTime)value;
                _paintingDateTime.PerformLayout();
                drawText = _paintingDateTime.Text;
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                cellState, value, drawText, errorText,
                cellStyle, advancedBorderStyle, paintParts);
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewDateTimePickerColumn dateTimeColumn = OwningColumn as KryptonDataGridViewDateTimePickerColumn;
            DataGridViewButtonSpecClickEventArgs args = new(dateTimeColumn, this, (ButtonSpecAny)sender);
            dateTimeColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewDateTimePickerEditingControl EditingDateTimePicker =>
            DataGridView.EditingControl as KryptonDataGridViewDateTimePickerEditingControl;

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
            DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            var preferredHeight = _paintingDateTime.GetPreferredSize(_sizeLarge).Height + 2;
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

        private bool OwnsEditingDateTimePicker(int rowIndex) =>
            rowIndex != -1 && DataGridView != null
&& (DataGridView.EditingControl is KryptonDataGridViewDateTimePickerEditingControl control)
                  && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart) => (paintParts & paintPart) != 0;
        #endregion

        #region Internal
        internal void SetShowCheckBox(int rowIndex, bool value)
        {
            _showCheckBox = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.ShowCheckBox = value;
            }
        }

        internal void SetShowUpDown(int rowIndex, bool value)
        {
            _showUpDown = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.ShowUpDown = value;
            }
        }

        internal void SetAutoShift(int rowIndex, bool value)
        {
            _autoShift = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.AutoShift = value;
            }
        }

        internal void SetChecked(int rowIndex, bool value)
        {
            _checked = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.Checked = value;
            }
        }

        internal void SetCustomFormat(int rowIndex, string value)
        {
            _customFormat = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CustomFormat = value;
            }
        }

        internal void SetCustomNullText(int rowIndex, string value)
        {
            _customNullText = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CustomNullText = value;
            }
        }

        internal void SetMaxDate(int rowIndex, DateTime value)
        {
            _maxDate = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.MaxDate = value;
            }
        }

        internal void SetMinDate(int rowIndex, DateTime value)
        {
            _minDate = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.MinDate = value;
            }
        }

        internal void SetFormat(int rowIndex, DateTimePickerFormat value)
        {
            _format = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.Format = value;
            }
        }

        internal void SetCalendarCloseOnTodayClick(int rowIndex, bool value)
        {
            _calendarCloseOnTodayClick = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarCloseOnTodayClick = value;
            }
        }

        internal void SetCalendarDimensions(int rowIndex, Size value)
        {
            _calendarDimensions = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarDimensions = value;
            }
        }

        internal void SetCalendarFirstDayOfWeek(int rowIndex, Day value)
        {
            _calendarFirstDayOfWeek = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarFirstDayOfWeek = value;
            }
        }

        internal void SetCalendarShowToday(int rowIndex, bool value)
        {
            _calendarShowToday = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarShowToday = value;
            }
        }

        internal void SetCalendarShowTodayCircle(int rowIndex, bool value)
        {
            _calendarShowTodayCircle = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarShowTodayCircle = value;
            }
        }

        internal void SetCalendarShowWeekNumbers(int rowIndex, bool value)
        {
            _calendarShowWeekNumbers = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarShowWeekNumbers = value;
            }
        }

        internal void SetCalendarTodayText(int rowIndex, string value)
        {
            _calendarTodayText = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarTodayText = value;
            }
        }

        internal void SetCalendarTodayDate(int rowIndex, DateTime value)
        {
            _calendarTodayDate = value;
            if (OwnsEditingDateTimePicker(rowIndex))
            {
                EditingDateTimePicker.CalendarTodayDate = value;
            }
        }
        #endregion
    }
}