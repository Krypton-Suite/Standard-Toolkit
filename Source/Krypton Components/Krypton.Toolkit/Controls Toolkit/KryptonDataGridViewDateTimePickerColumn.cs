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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewDateTimePickerCell cells.
    /// </summary>
    [Designer(typeof(KryptonDateTimePickerColumnDesigner))]
    [ToolboxBitmap(typeof(KryptonDataGridViewDateTimePickerColumn), "ToolboxBitmaps.KryptonDateTimePicker.bmp")]
    public class KryptonDataGridViewDateTimePickerColumn : KryptonDataGridViewIconColumn
    {
        #region Instance Fields

        private readonly DateTimeList _annualDates;
        private readonly DateTimeList _monthlyDates;
        private readonly DateTimeList _dates;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewDateTimePickerColumn class.
        /// </summary>
        public KryptonDataGridViewDateTimePickerColumn()
            : base(new KryptonDataGridViewDateTimePickerCell())
        {
            ButtonSpecs = new DataGridViewColumnSpecCollection(this);
            _annualDates = new DateTimeList();
            _monthlyDates = new DateTimeList();
            _dates = new DateTimeList();
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewDateTimePickerColumn { Name=");
            // ReSharper disable RedundantBaseQualifier
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            // ReSharper restore RedundantBaseQualifier
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewDateTimePickerColumn cloned = base.Clone() as KryptonDataGridViewDateTimePickerColumn;
            
            cloned.CalendarAnnuallyBoldedDates = CalendarAnnuallyBoldedDates;
            cloned.CalendarMonthlyBoldedDates = CalendarMonthlyBoldedDates;
            cloned.CalendarBoldedDates = CalendarBoldedDates;

            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
            {
                cloned.ButtonSpecs.Add(bs.Clone());
            }

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;

            set
            {
                if ((value != null) && (!(value is KryptonDataGridViewDateTimePickerCell cell)))
                {
                    throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewDateTimePickerCell or derive from it.");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs { get; }

        /// <summary>
        /// Replicates the ShowCheckBox property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether a check box is displayed in the control. When the box is unchecked, no value is selected.")]
        [DefaultValue(false)]
        public bool ShowCheckBox
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.ShowCheckBox;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.ShowCheckBox = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetShowCheckBox(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Replicates the ShowUpDown property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates whether a spin box rather than a drop-down calendar is displayed for modifying the control value.")]
        [DefaultValue(false)]
        public bool ShowUpDown
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.ShowUpDown;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.ShowUpDown = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetShowUpDown(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Replicates the Format property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether dates and times are displayed using standard or custom formatting.")]
        [DefaultValue(typeof(DateTimePickerFormat), "Long")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public DateTimePickerFormat Format
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.Format;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.Format = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetFormat(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Replicates the AutoShift property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if keyboard input will automatically shift to the next input field.")]
        [DefaultValue(false)]
        public bool AutoShift
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.AutoShift;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.AutoShift = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetAutoShift(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is checked and if the ValueNullable is DBNull or a DateTime value.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if the check box is checked and if the ValueNullable is DBNull or a DateTime value.")]
        [DefaultValue(true)]
        public bool Checked
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.Checked;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.Checked = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetChecked(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom date/time format string.
        /// </summary>
        [Category("Behavior")]
        [Description("The custom format string used to format the date and/or time displayed in the control.")]
        [DefaultValue("")]
        public string CustomFormat
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CustomFormat;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CustomFormat = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCustomFormat(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom text to show when control is not checked.
        /// </summary>
        [Category("Behavior")]
        [Description("The custom text to draw when the control is not checked. Provide an empty string for default action of showing the defined date.")]
        [DefaultValue(" ")]
        public string CustomNullText
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CustomNullText;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CustomNullText = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCustomNullText(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Maximum allowable date.")]
        public DateTime MaxDate
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.MaxDate;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.MaxDate = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetMaxDate(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Should the MaxDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMaxDate() =>
            (MaxDate != DateTimePicker.MaximumDateTime) && (MaxDate != DateTime.MaxValue);

        private void ResetMaxDate() => MaxDate = DateTime.MaxValue;

        /// <summary>
        /// Gets or sets the minimum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Minimum allowable date.")]
        public DateTime MinDate
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.MinDate;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.MinDate = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetMinDate(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Should the MinDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMinDate() =>
            (MinDate != DateTimePicker.MinimumDateTime) && (MinDate != DateTime.MinValue);

        private void ResetMinDate() => MinDate = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the number of columns and rows of months displayed. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Specifies the number of rows and columns of months displayed.")]
        [DefaultValue(typeof(Size), "1,1")]
        public Size CalendarDimensions
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarDimensions;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarDimensions = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarDimensions(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the label text for todays text. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Text used as label for todays date.")]
        [DefaultValue("Today:")]
        public string CalendarTodayText
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarTodayText;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarTodayText = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarTodayText(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Reset the value of the CalendarTodayText property.
        /// </summary>
        public void ResetCalendarTodayText() => CalendarTodayText = "Today:";

        /// <summary>
        /// First day of the week.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("First day of the week.")]
        [DefaultValue(typeof(Day), "Default")]
        public Day CalendarFirstDayOfWeek
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarFirstDayOfWeek;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarFirstDayOfWeek = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarFirstDayOfWeek(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if the control will display todays date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display todays date.")]
        [DefaultValue(true)]
        public bool CalendarShowToday
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarShowToday;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarShowToday = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarShowToday(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if clicking the Today button closes the drop down menu.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates if clicking the Today button closes the drop down menu.")]
        [DefaultValue(false)]
        public bool CalendarCloseOnTodayClick
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarCloseOnTodayClick;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarCloseOnTodayClick = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarCloseOnTodayClick(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if the control will circle the today date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will circle the today date.")]
        [DefaultValue(true)]
        public bool CalendarShowTodayCircle
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarShowTodayCircle;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarShowTodayCircle = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarShowTodayCircle(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if week numbers to the left of each row.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display week numbers to the left of each row.")]
        [DefaultValue(false)]
        public bool CalendarShowWeekNumbers
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarShowWeekNumbers;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarShowWeekNumbers = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarShowWeekNumbers(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets today's date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Today's date.")]
        public DateTime CalendarTodayDate
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return DateTimePickerCellTemplate.CalendarTodayDate;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarTodayDate = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewDateTimePickerCell dataGridViewCell)
                        {
                            dataGridViewCell.SetCalendarTodayDate(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        private void ResetCalendarTodayDate() => CalendarTodayDate = DateTime.Now.Date;

        private bool ShouldSerializeCalendarTodayDate() => (CalendarTodayDate != DateTime.Now.Date);

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which annual days are displayed in bold.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which annual dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarAnnuallyBoldedDates
        {
            get => _annualDates.ToArray();

            set
            {
                if (value == null)
                {
                    value = new DateTime[0];
                }

                _annualDates.Clear();
                _annualDates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarAnnuallyBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarAnnuallyBoldedDates() => (_annualDates.Count > 0);

        private void ResetCalendarAnnuallyBoldedDates() => CalendarAnnuallyBoldedDates = null;

        /// <summary>
        /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which monthly dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarMonthlyBoldedDates
        {
            get => _monthlyDates.ToArray();

            set
            {
                if (value == null)
                {
                    value = new DateTime[0];
                }

                _monthlyDates.Clear();
                _monthlyDates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarMonthlyBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarMonthlyBoldedDates() => (_monthlyDates.Count > 0);

        private void ResetCalendarMonthlyBoldedDates() => CalendarMonthlyBoldedDates = null;

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are displayed in bold.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarBoldedDates
        {
            get => _dates.ToArray();

            set
            {
                if (value == null)
                {
                    value = new DateTime[0];
                }

                _dates.Clear();
                _dates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarBoldedDates() => (_dates.Count > 0);

        private void ResetCalendarBoldedDates() => CalendarBoldedDates = null;

        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewDateTimePickerCell
        /// </summary>
        private KryptonDataGridViewDateTimePickerCell DateTimePickerCellTemplate => (KryptonDataGridViewDateTimePickerCell)CellTemplate;

        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args) => ButtonSpecClick?.Invoke(this, args);

        #endregion
    }
}