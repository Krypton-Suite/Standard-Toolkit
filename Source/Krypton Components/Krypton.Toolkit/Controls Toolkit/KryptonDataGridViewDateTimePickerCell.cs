#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Defines a KryptonDateTimePicker cell type for the KryptonDataGridView control
/// </summary>
public class KryptonDataGridViewDateTimePickerCell : KryptonDataGridViewTextBoxCell
{
    #region Static Fields
    private static readonly DateTimeConverter _dtc = new DateTimeConverter();
    private static readonly Type _defaultEditType = typeof(KryptonDataGridViewDateTimePickerEditingControl);
    private static readonly Type _defaultValueType = typeof(DateTime);
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
        _calendarDimensions = new Size(1, 1);
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Multiline
    {
        get => base.Multiline;
        set => base.Multiline = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool MultilineStringEditor
    {
        get => base.MultilineStringEditor;
        set => base.MultilineStringEditor = value;
    }

    /// <summary>
    /// Clones a DataGridViewDateTimePickerCell cell, copies all the custom properties.
    /// </summary>
    public override object Clone()
    {
        var dateTimeCell = base.Clone() as KryptonDataGridViewDateTimePickerCell ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("dateTimeCell"));

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
    [DefaultValue(DateTimePickerFormat.Long)]
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
    [DefaultValue(Day.Default)]
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
        DataGridView? dataGridView = DataGridView;
        if (dataGridView?.EditingControl == null)
        {
            throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
        }

        base.DetachEditingControl();
    }

    /// <summary>
    /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control
    /// at the beginning of an editing session. It makes sure that the properties of the KryptonDateTimePicker editing control are
    /// set according to the cell properties.
    /// </summary>
    public override void InitializeEditingControl(int rowIndex,
        object? initialFormattedValue,
        DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

        if (DataGridView!.EditingControl is KryptonDateTimePicker dateTime)
        {
            if (OwningColumn is KryptonDataGridViewDateTimePickerColumn dateTimeColumn)
            {
                dateTime.ShowCheckBox = ShowCheckBox;
                dateTime.ShowUpDown = ShowUpDown;
                dateTime.ShowAdornments = true;
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
            }

            if ((initialFormattedValue is not string initialFormattedValueStr) || string.IsNullOrEmpty(initialFormattedValueStr))
            {
                dateTime.ValueNullable = null;
            }
            else
            {
                var provider = (dataGridViewCellStyle?.FormatProvider as IFormatProvider) ?? CultureInfo.CurrentCulture;
                var culture = provider as CultureInfo ?? CultureInfo.CurrentCulture;

                string fmt = dataGridViewCellStyle?.Format ?? string.Empty;
                if (string.IsNullOrEmpty(fmt))
                {
                    fmt = _format switch
                    {
                        DateTimePickerFormat.Long => culture.DateTimeFormat.LongDatePattern,
                        DateTimePickerFormat.Short => culture.DateTimeFormat.ShortDatePattern,
                        DateTimePickerFormat.Time => culture.DateTimeFormat.LongTimePattern,
                        DateTimePickerFormat.Custom => string.IsNullOrEmpty(_customFormat) ? "G" : CommonHelper.MakeCustomDateFormat(_customFormat),
                        _ => "G"
                    };
                }

                if (DateTime.TryParseExact(initialFormattedValueStr, fmt, culture, DateTimeStyles.AllowWhiteSpaces, out DateTime dt)
                    || DateTime.TryParse(initialFormattedValueStr, culture, DateTimeStyles.AllowWhiteSpaces, out dt)
                    || DateTime.TryParse(initialFormattedValueStr, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dt))
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
    protected override object? GetFormattedValue(object? value,
        int rowIndex,
        ref DataGridViewCellStyle cellStyle,
        TypeConverter? valueTypeConverter,
        TypeConverter? formattedValueTypeConverter,
        DataGridViewDataErrorContexts context)
    {
        if ((value == null) || (value == DBNull.Value))
        {
            return string.Empty;
        }

        if (value is DateTime dt)
        {
            string? format = cellStyle?.Format;
            if (string.IsNullOrEmpty(format))
            {
                format = _format switch
                {
                    DateTimePickerFormat.Long => CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern,
                    DateTimePickerFormat.Short => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
                    DateTimePickerFormat.Time => CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern,
                    DateTimePickerFormat.Custom => string.IsNullOrEmpty(_customFormat) ? "G" : CommonHelper.MakeCustomDateFormat(_customFormat),
                    _ => "G"
                };
            }

            var provider = (cellStyle?.FormatProvider as IFormatProvider) ?? CultureInfo.CurrentCulture;
            return dt.ToString(format, provider);
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
    public override object ParseFormattedValue(object? formattedValue,
        DataGridViewCellStyle cellStyle,
        TypeConverter? formattedValueTypeConverter,
        TypeConverter? valueTypeConverter)
    {
        if (formattedValue == null)
        {
            return DBNull.Value;
        }

        string stringValue = (string)formattedValue;
        if (string.IsNullOrEmpty(stringValue))
        {
            return DBNull.Value;
        }

        var provider = (cellStyle?.FormatProvider as IFormatProvider) ?? CultureInfo.CurrentCulture;
        var culture = provider as CultureInfo ?? CultureInfo.CurrentCulture;

        string fmt = cellStyle?.Format ?? string.Empty;
        if (string.IsNullOrEmpty(fmt))
        {
            fmt = _format switch
            {
                DateTimePickerFormat.Long => culture.DateTimeFormat.LongDatePattern,
                DateTimePickerFormat.Short => culture.DateTimeFormat.ShortDatePattern,
                DateTimePickerFormat.Time => culture.DateTimeFormat.LongTimePattern,
                DateTimePickerFormat.Custom => string.IsNullOrEmpty(_customFormat) ? "G" : CommonHelper.MakeCustomDateFormat(_customFormat),
                _ => "G"
            };
        }

        if (DateTime.TryParseExact(stringValue, fmt, culture, DateTimeStyles.AllowWhiteSpaces, out DateTime dt)
            || DateTime.TryParse(stringValue, culture, DateTimeStyles.AllowWhiteSpaces, out dt)
            || DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dt))
        {
            return dt;
        }

        return _dtc.ConvertFromString(null, culture, stringValue)!;
    }

    // Intentionally use base PositionEditingControl behavior to preserve indicator strip in edit mode.
    #endregion

    #region Protected
    ///<inheritdoc/>
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value,
        object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        // Avoid drawing our own glyph/text while editing to prevent overlap with the editing control visuals.
        // Also cover the reserved indicator area so KDGV background does not show as a tall blue band.
        if (DataGridView is not null
            && DataGridView.IsCurrentCellInEditMode
            && DataGridView.CurrentCellAddress.X == ColumnIndex
            && DataGridView.CurrentCellAddress.Y == rowIndex)
        {
            // Paint normal background/borders
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null,
                string.Empty, errorText, cellStyle, advancedBorderStyle, paintParts);

            // Mask the indicator strip so the renderer background does not show full-height
            bool rtl = DataGridView.RightToLeft == RightToLeft.Yes;
            var maskRect = rtl
                ? new Rectangle(1 + cellBounds.Left + cellStyle.Padding.Left,
                                1 + cellBounds.Top + cellStyle.Padding.Top,
                                IndicatorSize + IndicatorGap + 1,
                                cellBounds.Height - cellStyle.Padding.Top - cellStyle.Padding.Bottom - 2)
                : new Rectangle(cellBounds.Right - (IndicatorSize + IndicatorGap),
                                1 + cellBounds.Top + cellStyle.Padding.Top,
                                IndicatorSize + IndicatorGap + 1,
                                cellBounds.Height - cellStyle.Padding.Top - cellStyle.Padding.Bottom - 2);
            // Only mask outside the editing control bounds; keep the drop glyph area visible
            Rectangle ecBounds = DataGridView.EditingControl?.Bounds ?? Rectangle.Empty;
            // Translate EC bounds to cell-local coordinates
            ecBounds.Offset(-cellBounds.X, -cellBounds.Y);
            Rectangle[] regions = Rectangle.Intersect(maskRect, ecBounds).IsEmpty
                ? new[] { maskRect }
                : new[]
                {
                    new Rectangle(maskRect.X, maskRect.Y, Math.Max(0, ecBounds.X - maskRect.X), maskRect.Height),
                    new Rectangle(ecBounds.Right, maskRect.Y, Math.Max(0, (maskRect.Right - ecBounds.Right)), maskRect.Height)
                };
            using (var b = new SolidBrush(cellStyle.BackColor))
            {
                foreach (var r in regions)
                {
                    if (r.Width > 0 && r.Height > 0)
                    {
                        graphics.FillRectangle(b, r);
                    }
                }
            }
            return;
        }

        if (DataGridView is not null
            && KryptonOwningColumn?.CellIndicatorImage is Image image)
        {
            int pos;
            Rectangle textArea;
            var righToLeft = DataGridView.RightToLeft == RightToLeft.Yes;

            if (righToLeft)
            {
                pos = cellBounds.Left;

                // The WinForms cell content always receives padding of one by default, custom padding is added to that.
                textArea = new Rectangle(
                    1 + cellBounds.Left + cellStyle.Padding.Left + IndicatorSize,
                    1 + cellBounds.Top + cellStyle.Padding.Top,
                    cellBounds.Width - cellStyle.Padding.Left - cellStyle.Padding.Right - IndicatorSize - 3,
                    cellBounds.Height - cellStyle.Padding.Top - cellStyle.Padding.Bottom - 2);
            }
            else
            {
                pos = cellBounds.Right - IndicatorSize;

                // The WinForms cell content always receives padding of one by default, custom padding is added to that.
                textArea = new Rectangle(
                    1 + cellBounds.Left + cellStyle.Padding.Left,
                    1 + cellBounds.Top + cellStyle.Padding.Top,
                    cellBounds.Width - cellStyle.Padding.Left - cellStyle.Padding.Right - IndicatorSize - 3,
                    cellBounds.Height - cellStyle.Padding.Top - cellStyle.Padding.Bottom - 2);
            }

            // When the Krypton column is part of a WinForms DataGridView let the default paint routine paint the cell.
            // Afterwards we paint the text and drop down image.
            if (DataGridView is DataGridView)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null,
                    string.Empty, errorText, cellStyle, advancedBorderStyle, paintParts);
            }

            // Draw the drop down button, only if no ErrorText has been set.
            // If the ErrorText is set, only the error icon is shown. Otherwise both are painted on the same spot.
            if (ErrorText.Length == 0)
            {
                var sized = KryptonOwningColumn?.GetIndicatorImageForSize(IndicatorSize) ?? image;
                int y = textArea.Top + (textArea.Height - IndicatorSize) / 2;
                graphics.DrawImage(sized, new Rectangle(pos, y, IndicatorSize, IndicatorSize));

                if (DataGridView.Rows.SharedRow(rowIndex).Index != -1
                    && formattedValue is string str
                    && str.Length > 0
                    && DateTime.TryParse(str, out DateTime dt))
                {
                    string fmt = cellStyle?.Format ?? string.Empty;
                    if (string.IsNullOrEmpty(fmt))
                    {
                        fmt = _format switch
                        {
                            DateTimePickerFormat.Long => CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern,
                            DateTimePickerFormat.Short => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
                            DateTimePickerFormat.Time => CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern,
                            DateTimePickerFormat.Custom => string.IsNullOrEmpty(_customFormat) ? "G" : CommonHelper.MakeCustomDateFormat(_customFormat),
                            _ => "G"
                        };
                    }
                    var provider = (cellStyle?.FormatProvider as IFormatProvider) ?? CultureInfo.CurrentCulture;
                    formattedValue = dt.ToString(fmt, provider);
                }
            }
            else
            {
                formattedValue = errorText;
            }

            var displayForeColor = (cellState & DataGridViewElementStates.Selected) != 0
                ? cellStyle!.SelectionForeColor
                : cellStyle!.ForeColor;

            TextRenderer.DrawText(graphics, formattedValue?.ToString() ?? string.Empty, cellStyle.Font, textArea, displayForeColor,
                KryptonDataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(righToLeft, cellStyle.Alignment, cellStyle.WrapMode));
        }
    }

    /// <summary>
    /// Customized implementation of the GetErrorIconBounds function in order to draw the potential
    /// error icon next to the up/down buttons and not on top of them.
    /// </summary>
    protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
    {
        Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);

        errorIconBounds.X = DataGridView!.RightToLeft == RightToLeft.Yes
            ? errorIconBounds.Left + IndicatorSize
            : errorIconBounds.Left - IndicatorSize;

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

        if (rowIndex < 0)
        {
            Size hdr = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            hdr.Width += IndicatorSize + IndicatorGap;
            return hdr;
        }

        string format = cellStyle?.Format ?? string.Empty;
        if (string.IsNullOrEmpty(format))
        {
            format = _format switch
            {
                DateTimePickerFormat.Long => CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern,
                DateTimePickerFormat.Short => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
                DateTimePickerFormat.Time => CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern,
                DateTimePickerFormat.Custom => string.IsNullOrEmpty(_customFormat) ? "G" : CommonHelper.MakeCustomDateFormat(_customFormat),
                _ => "G"
            };
        }

        var provider = (cellStyle?.FormatProvider as IFormatProvider) ?? CultureInfo.CurrentCulture;

        object? raw = GetValue(rowIndex);
        string text;
        if (raw is DateTime dt)
        {
            text = dt.ToString(format, provider);
        }
        else if (raw is string s && DateTime.TryParse(s, out DateTime parsed))
        {
            text = parsed.ToString(format, provider);
        }
        else
        {
            text = Convert.ToString(raw, CultureInfo.CurrentCulture) ?? string.Empty;
        }

        bool rtl = DataGridView.RightToLeft == RightToLeft.Yes;
        TextFormatFlags flags = KryptonDataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(rtl, cellStyle!.Alignment, cellStyle!.WrapMode);

        Size textSize;
        if ((constraintSize.Width > 0) && (cellStyle.WrapMode != DataGridViewTriState.False))
        {
            int availTextW = Math.Max(1, constraintSize.Width - IndicatorSize - IndicatorGap - cellStyle.Padding.Horizontal);
            textSize = TextRenderer.MeasureText(graphics, text, cellStyle.Font, new Size(availTextW, int.MaxValue), flags);
        }
        else
        {
            textSize = TextRenderer.MeasureText(graphics, text, cellStyle.Font, Size.Empty, flags);
        }

        int width = textSize.Width + cellStyle.Padding.Horizontal + IndicatorSize + IndicatorGap + 1;
        int height = Math.Max(textSize.Height + cellStyle.Padding.Vertical + 1, base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize).Height);
        return new Size(width, height);
    }
    #endregion

    #region Private

    private KryptonDataGridViewDateTimePickerEditingControl EditingDateTimePicker =>
        DataGridView!.EditingControl as KryptonDataGridViewDateTimePickerEditingControl ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(DataGridView.EditingControl)));

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
        rowIndex != -1 && DataGridView is { EditingControl: KryptonDataGridViewDateTimePickerEditingControl control }
                       && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

    private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
        DataGridViewCellStyle cellStyle)
    {
        var preferredHeight = DataGridView!.EditingControl!.GetPreferredSize(new Size(editingControlBounds.Width, 10000)).Height;
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

    /// <summary>
    /// Type casted version of OwningColumn
    /// </summary>
    internal KryptonDataGridViewDateTimePickerColumn? KryptonOwningColumn => OwningColumn as KryptonDataGridViewDateTimePickerColumn;
    #endregion
}