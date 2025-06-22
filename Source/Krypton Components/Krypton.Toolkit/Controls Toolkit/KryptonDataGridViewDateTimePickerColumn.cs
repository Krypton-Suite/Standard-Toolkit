#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Hosts a collection of KryptonDataGridViewDateTimePickerCell cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewDateTimePickerColumn), "ToolboxBitmaps.KryptonDateTimePicker.bmp")]
public class KryptonDataGridViewDateTimePickerColumn : KryptonDataGridViewIconColumn
{
    #region Instance Fields

    private readonly DateTimeList _annualDates;
    private readonly DateTimeList _monthlyDates;
    private readonly DateTimeList _dates;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewDateTimePickerColumn class.
    /// </summary>
    public KryptonDataGridViewDateTimePickerColumn()
        : base(new KryptonDataGridViewDateTimePickerCell())
    {
        _annualDates = [];
        _monthlyDates = [];
        _dates = [];
        _kryptonDataGridViewCellIndicatorImage = new();
    }

    /// <summary>
    /// Returns a standard compact string representation of the column.
    /// </summary>
    public override string ToString()
    {
        var builder = new StringBuilder(0x40);
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
        var cloned = base.Clone() as KryptonDataGridViewDateTimePickerColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("cloned"));


        cloned.CalendarAnnuallyBoldedDates = CalendarAnnuallyBoldedDates;
        cloned.CalendarMonthlyBoldedDates = CalendarMonthlyBoldedDates;
        cloned.CalendarBoldedDates = CalendarBoldedDates;

        return cloned;
    }
    #endregion

    #region Public
    /// <summary>
    /// Represents the implicit cell that gets cloned when adding rows to the grid.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate
    {
        get => base.CellTemplate;

        set
        {
            if ((value != null) && (value is not KryptonDataGridViewDateTimePickerCell ))
            {
                throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewDateTimePickerCell or derive from it.");
            }

            base.CellTemplate = value;
        }
    }

    /// <summary>
    /// Replicates the ShowCheckBox property of the KryptonDataGridViewDateTimePickerCell cell type.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines whether a check box is Displayed in the control. When the box is unchecked, no value is selected.")]
    [DefaultValue(false)]
    public bool ShowCheckBox
    {
        get =>
            DateTimePickerCellTemplate?.ShowCheckBox ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Appearance")]
    [Description(@"Indicates whether a spin box rather than a drop-down calendar is Displayed for modifying the control value.")]
    [DefaultValue(false)]
    public bool ShowUpDown
    {
        get =>
            DateTimePickerCellTemplate?.ShowUpDown ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Appearance")]
    [Description(@"Determines whether dates and times are Displayed using standard or custom formatting.")]
    [DefaultValue(DateTimePickerFormat.Long)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public DateTimePickerFormat Format
    {
        get =>
            DateTimePickerCellTemplate?.Format ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Behavior")]
    [Description(@"Determines if keyboard input will automatically shift to the next input field.")]
    [DefaultValue(false)]
    public bool AutoShift
    {
        get =>
            DateTimePickerCellTemplate?.AutoShift ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Behavior")]
    [Description(@"Determines if the check box is checked and if the ValueNullable is DBNull or a DateTime value.")]
    [DefaultValue(true)]
    public bool Checked
    {
        get =>
            DateTimePickerCellTemplate?.Checked ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Behavior")]
    [Description(@"The custom format string used to format the date and/or time Displayed in the control.")]
    [DefaultValue("")]
    public string CustomFormat
    {
        get =>
            DateTimePickerCellTemplate == null
                ? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                : DateTimePickerCellTemplate.CustomFormat;
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Behavior")]
    [Description(@"The custom text to draw when the control is not checked. Provide an empty string for default action of showing the defined date.")]
    [DefaultValue(" ")]
    public string CustomNullText
    {
        get =>
            DateTimePickerCellTemplate == null
                ? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                : DateTimePickerCellTemplate.CustomNullText;
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Behavior")]
    [Description(@"Maximum allowable date.")]
    public DateTime MaxDate
    {
        get =>
            DateTimePickerCellTemplate?.MaxDate ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"Behavior")]
    [Description(@"Minimum allowable date.")]
    public DateTime MinDate
    {
        get =>
            DateTimePickerCellTemplate?.MinDate ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    /// Gets or sets the number of columns and rows of months Displayed. 
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Specifies the number of rows and columns of months Displayed.")]
    [DefaultValue(typeof(Size), "1,1")]
    public Size CalendarDimensions
    {
        get =>
            DateTimePickerCellTemplate?.CalendarDimensions ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"MonthCalendar")]
    [Description(@"Text used as label for todays date.")]
    [DefaultValue("Today:")]
    public string CalendarTodayText
    {
        get =>
            DateTimePickerCellTemplate == null
                ? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                : DateTimePickerCellTemplate.CalendarTodayText;
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"MonthCalendar")]
    [Description(@"First day of the week.")]
    [DefaultValue(Day.Default)]
    public Day CalendarFirstDayOfWeek
    {
        get =>
            DateTimePickerCellTemplate?.CalendarFirstDayOfWeek ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"MonthCalendar")]
    [Description(@"Indicates whether this month calendar will display todays date.")]
    [DefaultValue(true)]
    public bool CalendarShowToday
    {
        get =>
            DateTimePickerCellTemplate?.CalendarShowToday ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    /// Gets and sets if clicking the Today button closes the drop-down menu.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates if clicking the Today button closes the drop-down menu.")]
    [DefaultValue(false)]
    public bool CalendarCloseOnTodayClick
    {
        get =>
            DateTimePickerCellTemplate?.CalendarCloseOnTodayClick ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"MonthCalendar")]
    [Description(@"Indicates whether this month calendar will circle the today date.")]
    [DefaultValue(true)]
    public bool CalendarShowTodayCircle
    {
        get =>
            DateTimePickerCellTemplate?.CalendarShowTodayCircle ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"MonthCalendar")]
    [Description(@"Indicates whether this month calendar will display week numbers to the left of each row.")]
    [DefaultValue(false)]
    public bool CalendarShowWeekNumbers
    {
        get =>
            DateTimePickerCellTemplate?.CalendarShowWeekNumbers ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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
    [Category(@"MonthCalendar")]
    [Description(@"Today's date.")]
    public DateTime CalendarTodayDate
    {
        get =>
            DateTimePickerCellTemplate?.CalendarTodayDate ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
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
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
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

    private bool ShouldSerializeCalendarTodayDate() => CalendarTodayDate != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which annual days are Displayed in bold.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates which annual dates should be boldface.")]
    [Localizable(true)]
    [AllowNull]
    public DateTime[]? CalendarAnnuallyBoldedDates
    {
        get => _annualDates.ToArray();

        set
        {
            value ??= Array.Empty<DateTime>();

            _annualDates.Clear();
            _annualDates.AddRange(value);
        }
    }

    /// <summary>
    /// Should the CalendarAnnuallyBoldedDates property be serialized.
    /// </summary>
    /// <returns>True if property needs to be serialized.</returns>
    public bool ShouldSerializeCalendarAnnuallyBoldedDates() => _annualDates.Count > 0;

    private void ResetCalendarAnnuallyBoldedDates() => CalendarAnnuallyBoldedDates = null;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates which monthly dates should be boldface.")]
    [Localizable(true)]
    [AllowNull]
    public DateTime[]? CalendarMonthlyBoldedDates
    {
        get => _monthlyDates.ToArray();

        set
        {
            value ??= Array.Empty<DateTime>();

            _monthlyDates.Clear();
            _monthlyDates.AddRange(value);
        }
    }

    /// <summary>
    /// Should the CalendarMonthlyBoldedDates property be serialized.
    /// </summary>
    /// <returns>True if property needs to be serialized.</returns>
    public bool ShouldSerializeCalendarMonthlyBoldedDates() => _monthlyDates.Count > 0;

    private void ResetCalendarMonthlyBoldedDates() => CalendarMonthlyBoldedDates = null;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are Displayed in bold.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates which dates should be boldface.")]
    [Localizable(true)]
    [AllowNull]
    public DateTime[]? CalendarBoldedDates
    {
        get => _dates.ToArray();

        set
        {
            value ??= Array.Empty<DateTime>();

            _dates.Clear();
            _dates.AddRange(value);
        }
    }

    /// <summary>
    /// Should the CalendarBoldedDates property be serialized.
    /// </summary>
    /// <returns>True if property needs to be serialized.</returns>
    public bool ShouldSerializeCalendarBoldedDates() => _dates.Count > 0;

    private void ResetCalendarBoldedDates() => CalendarBoldedDates = null;

    #endregion

    #region Private
    /// <summary>
    /// Small utility function that returns the template cell as a KryptonDataGridViewDateTimePickerCell
    /// </summary>
    private KryptonDataGridViewDateTimePickerCell? DateTimePickerCellTemplate => CellTemplate as KryptonDataGridViewDateTimePickerCell;
    // Cell indicator image instance
    private KryptonDataGridViewCellIndicatorImage _kryptonDataGridViewCellIndicatorImage;
    #endregion

    #region Internal
    /// <summary>
    /// Provides the cell indicator images to the cells from from this column instance.<br/>
    /// For internal use only.
    /// </summary>
    internal Image? CellIndicatorImage => _kryptonDataGridViewCellIndicatorImage.Image;
    #endregion Internal

    #region Protected
    /// <inheritdoc/>
    protected override void OnDataGridViewChanged()
    {
        _kryptonDataGridViewCellIndicatorImage.DataGridView = DataGridView as KryptonDataGridView;
        base.OnDataGridViewChanged();
    }
    #endregion Protected
}