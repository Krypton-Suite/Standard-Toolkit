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
/// Enables the user to select a date using a visual monthly calendar display.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonMonthCalendar), "ToolboxBitmaps.KryptonMonthCalendar.bmp")]
[DefaultEvent(nameof(DateChanged))]
[DefaultProperty(nameof(SelectionRange))]
[DefaultBindingProperty(nameof(SelectionRange))]
[Designer(typeof(KryptonMonthCalendarDesigner))]
[DesignerCategory(@"code")]
[Description(@"Select a date using a visual monthly calendar display.")]
public class KryptonMonthCalendar : VisualSimpleBase,
    IKryptonMonthCalendar
{
    #region Instance Fields

    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewLayoutMonths? _drawMonths;
    private readonly PaletteTripleOverride _boldedDisabled;
    private readonly PaletteTripleOverride _boldedNormal;
    private readonly PaletteTripleOverride _boldedTracking;
    private readonly PaletteTripleOverride _boldedPressed;
    private readonly PaletteTripleOverride _boldedCheckedNormal;
    private readonly PaletteTripleOverride _boldedCheckedTracking;
    private readonly PaletteTripleOverride _boldedCheckedPressed;
    private readonly PaletteTripleOverride _todayDisabled;
    private readonly PaletteTripleOverride _todayNormal;
    private readonly PaletteTripleOverride _todayTracking;
    private readonly PaletteTripleOverride _todayPressed;
    private readonly PaletteTripleOverride _todayCheckedNormal;
    private readonly PaletteTripleOverride _todayCheckedTracking;
    private readonly PaletteTripleOverride _todayCheckedPressed;
    private HeaderStyle _headerStyle;
    private ButtonStyle _dayStyle;
    private ButtonStyle _dayOfWeekStyle;
    private DateTime _selectionStart;
    private DateTime _selectionEnd;
    private DateTime _minDate;
    private DateTime _maxDate;
    private DateTime _todayDate;
    private readonly DateTimeList _annualDates;
    private readonly DateTimeList _monthlyDates;
    private Day _firstDayOfWeek;
    private Size _dimensions;
    private string _todayFormat;
    private int _maxSelectionCount;
    private int _scrollChange;
    private bool _hasFocus;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the selected date changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the selected date changes.")]
    public event DateRangeEventHandler? DateChanged;

    /// <summary>Occurs when the date is selected.</summary>
    [Category(@"Action")]
    [Description(@"Occurs when the date is selected.")]
    public event DateRangeEventHandler? DateSelected;

    /// <summary>
    /// Occurs when the selected start date changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the selected start date changes.")]
    public event EventHandler? SelectionStartChanged;

    /// <summary>
    /// Occurs when the selected end date changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the selected end date changes.")]
    public event EventHandler? SelectionEndChanged;

    /// <summary>
    /// Occurs when the control is clicked.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? Click;

    /// <summary>
    /// Occurs when the control is double clicked.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? DoubleClick;

    /// <summary>
    /// Occurs when the text value changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? TextChanged;

    /// <summary>
    /// Occurs when the foreground color value changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? ForeColorChanged;

    /// <summary>
    /// Occurs when the font value changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? FontChanged;

    /// <summary>
    /// Occurs when the background image value changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageChanged;

    /// <summary>
    /// Occurs when the background image layout value changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the background color value changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackColorChanged;

    /// <summary>
    /// Occurs when the padding value changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? PaddingChanged;

    /// <summary>
    /// Occurs when the control needs to paint.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event PaintEventHandler? Paint;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonMonthCalendar class.
    /// </summary>
    public KryptonMonthCalendar()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        // Create the palette storage
        StateCommon = new PaletteMonthCalendarRedirect(Redirector, NeedPaintDelegate);
        OverrideFocus = new PaletteMonthCalendarStateRedirect(Redirector, NeedPaintDelegate);
        OverrideBolded = new PaletteMonthCalendarStateRedirect(Redirector, NeedPaintDelegate);
        OverrideToday = new PaletteMonthCalendarStateRedirect(Redirector, NeedPaintDelegate);

        // Basic state storage
        StateDisabled = new PaletteMonthCalendarDoubleState(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteMonthCalendarDoubleState(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteMonthCalendarState(StateCommon, NeedPaintDelegate);
        StatePressed = new PaletteMonthCalendarState(StateCommon, NeedPaintDelegate);
        StateCheckedNormal = new PaletteMonthCalendarState(StateCommon, NeedPaintDelegate);
        StateCheckedTracking = new PaletteMonthCalendarState(StateCommon, NeedPaintDelegate);
        StateCheckedPressed = new PaletteMonthCalendarState(StateCommon, NeedPaintDelegate);

        // Bold overrides
        _boldedDisabled = new PaletteTripleOverride(OverrideBolded.Day, StateDisabled.Day, PaletteState.BoldedOverride);
        _boldedNormal = new PaletteTripleOverride(OverrideBolded.Day, StateNormal.Day, PaletteState.BoldedOverride);
        _boldedTracking = new PaletteTripleOverride(OverrideBolded.Day, StateTracking.Day, PaletteState.BoldedOverride);
        _boldedPressed = new PaletteTripleOverride(OverrideBolded.Day, StatePressed.Day, PaletteState.BoldedOverride);
        _boldedCheckedNormal = new PaletteTripleOverride(OverrideBolded.Day, StateCheckedNormal.Day, PaletteState.BoldedOverride);
        _boldedCheckedTracking = new PaletteTripleOverride(OverrideBolded.Day, StateCheckedTracking.Day, PaletteState.BoldedOverride);
        _boldedCheckedPressed = new PaletteTripleOverride(OverrideBolded.Day, StateCheckedPressed.Day, PaletteState.BoldedOverride);

        // Today overrides
        _todayDisabled = new PaletteTripleOverride(OverrideToday.Day, _boldedDisabled, PaletteState.TodayOverride);
        _todayNormal = new PaletteTripleOverride(OverrideToday.Day, _boldedNormal, PaletteState.TodayOverride);
        _todayTracking = new PaletteTripleOverride(OverrideToday.Day, _boldedTracking, PaletteState.TodayOverride);
        _todayPressed = new PaletteTripleOverride(OverrideToday.Day, _boldedPressed, PaletteState.TodayOverride);
        _todayCheckedNormal = new PaletteTripleOverride(OverrideToday.Day, _boldedCheckedNormal, PaletteState.TodayOverride);
        _todayCheckedTracking = new PaletteTripleOverride(OverrideToday.Day, _boldedCheckedTracking, PaletteState.TodayOverride);
        _todayCheckedPressed = new PaletteTripleOverride(OverrideToday.Day, _boldedCheckedPressed, PaletteState.TodayOverride);

        // Focus overrides added to bold overrides
        OverrideDisabled = new PaletteTripleOverride(OverrideFocus.Day, _todayDisabled, PaletteState.FocusOverride);
        OverrideNormal = new PaletteTripleOverride(OverrideFocus.Day, _todayNormal, PaletteState.FocusOverride);
        OverrideTracking = new PaletteTripleOverride(OverrideFocus.Day, _todayTracking, PaletteState.FocusOverride);
        OverridePressed = new PaletteTripleOverride(OverrideFocus.Day, _todayPressed, PaletteState.FocusOverride);
        OverrideCheckedNormal = new PaletteTripleOverride(OverrideFocus.Day, _todayCheckedNormal, PaletteState.FocusOverride);
        OverrideCheckedTracking = new PaletteTripleOverride(OverrideFocus.Day, _todayCheckedTracking, PaletteState.FocusOverride);
        OverrideCheckedPressed = new PaletteTripleOverride(OverrideFocus.Day, _todayCheckedPressed, PaletteState.FocusOverride);

        // Create view that is used by standalone control as well as this context menu element
        _drawMonths = new ViewLayoutMonths(null, null, null, this, Redirector, NeedPaintDelegate);

        // Place the months layout view inside a standard docker which provides the control border
        _drawDocker = new ViewDrawDocker(StateNormal.Back, StateNormal.Border, null)
        {
            { _drawMonths, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDocker);

        // Set default property values 
        _dimensions = new Size(1, 1);
        _firstDayOfWeek = Day.Default;
        _headerStyle = HeaderStyle.Calendar;
        _dayStyle = ButtonStyle.CalendarDay;
        _dayOfWeekStyle = ButtonStyle.CalendarDay;
        _selectionStart = DateTime.Now.Date;
        _selectionEnd = _selectionStart;
        _todayDate = _selectionStart;
        _minDate = DateTimePicker.MinimumDateTime;
        _maxDate = DateTimePicker.MaximumDateTime;
        _maxSelectionCount = 7;
        AnnuallyBoldedDatesMask = new int[12];
        _annualDates = [];
        _monthlyDates = [];
        BoldedDatesList = [];
        _scrollChange = 0;
        _todayFormat = "d";
    }
    #endregion

    #region Public
    /// <summary>
    ///  Sets date as the current selected date. The start and begin of
    ///  the selection range will both be equal to date.
    /// </summary>
    public void SetDate(DateTime date)
    {
        if (date.Ticks < _minDate.Ticks)
        {
            throw new ArgumentOutOfRangeException(nameof(date), date, string.Format(@"Value of '{1}' is not valid for '{0}'. '{0}' must be greater than or equal to {2}.", nameof(date), FormatDate(date), nameof(MinDate)));
        }

        if (date.Ticks > _maxDate.Ticks)
        {
            throw new ArgumentOutOfRangeException(nameof(date), date, string.Format(@"Value of '{1}' is not valid for '{0}'. '{0}' must be less than or equal to {2}.", nameof(date), FormatDate(date), nameof(MaxDate)));
        }

        SetSelectionRange(date, date);
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(false)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(false)]
    public override AutoSizeMode AutoSizeMode
    {
        get => base.AutoSizeMode;
        set => base.AutoSizeMode = value;
    }

    /// <summary>
    /// Gets or sets the Input Method Editor (IME) mode of the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImeMode ImeMode
    {
        get => base.ImeMode;
        set => base.ImeMode = value;
    }

    /// <summary>
    /// Gets or sets the padding internal to the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    /// <summary>
    /// Gets or sets the minimum allowable date.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Minimum allowable date.")]
    [RefreshProperties(RefreshProperties.All)]
    public DateTime MinDate
    {
        get => EffectiveMinDate(_minDate);

        set
        {
            if (value != _minDate)
            {
                if (value > EffectiveMaxDate(_maxDate))
                {
                    throw new ArgumentOutOfRangeException(nameof(MinDate), @"Date provided is greater than the maximum supported date.");
                }

                if (value < DateTimePicker.MinimumDateTime)
                {
                    throw new ArgumentOutOfRangeException(nameof(MinDate), @"Date provided is less than the minimum supported date.");
                }

                _minDate = value;

                // Update selection dates to be valid with new min date
                SetRange();
            }
        }
    }

    private void ResetMinDate() => MinDate = DateTimePicker.MinimumDateTime;

    private bool ShouldSerializeMinDate() => _minDate != DateTimePicker.MinimumDateTime;

    /// <summary>
    /// Gets or sets the today date format string.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The today format string used to format the date Displayed in the today button.")]
    [DefaultValue("d")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    [DisallowNull]
    public string TodayFormat
    {
        get => _todayFormat;

        set
        {
            if (_todayFormat != value)
            {
                _todayFormat = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the number of months to scroll when next/prev buttons are used.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Number of months to scroll when next/prev buttons are used.")]
    [DefaultValue(0)]
    public int ScrollChange
    {
        get => _scrollChange;

        set
        {
            if (value < 0)
            {
                value = 0;
            }

            _scrollChange = value;
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Gets or sets today's date.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Today's date.")]
    [DisallowNull]
    public DateTime TodayDate
    {
        get => _todayDate;

        set
        {
            _todayDate = value;
            PerformNeedPaint(true);
        }
    }

    private void ResetTodayDate() => TodayDate = DateTime.Now.Date;

    private bool ShouldSerializeTodayDate() => TodayDate != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which annual days are Displayed in bold.
    /// </summary>
    [Localizable(true)]
    [Description(@"Indicates which annual dates should be boldface.")]
    [AllowNull]
    public DateTime[]? AnnuallyBoldedDates
    {
        get => _annualDates.ToArray();

        set
        {
            value ??= Array.Empty<DateTime>();

            _annualDates.Clear();
            _annualDates.AddRange(value);

            for (var i = 0; i < 12; i++)
            {
                AnnuallyBoldedDatesMask[i] = 0;
            }

            // Set bitmap matching the days of month to be bolded
            foreach (DateTime dt in value)
            {
                AnnuallyBoldedDatesMask[dt.Month - 1] |= 1 << (dt.Day - 1);
            }

            PerformNeedPaint(true);
        }
    }

    private void ResetAnnuallyBoldedDates() => AnnuallyBoldedDates = null;

    private bool ShouldSerializeAnnuallyBoldedDates() => _annualDates.Count > 0;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
    /// </summary>
    [Localizable(true)]
    [Description(@"Indicates which monthly dates should be boldface.")]
    [AllowNull]
    public DateTime[]? MonthlyBoldedDates
    {
        get => _monthlyDates.ToArray();

        set
        {
            value ??= Array.Empty<DateTime>();

            _monthlyDates.Clear();
            _monthlyDates.AddRange(value);

            // Set bitmap matching the days of month to be bolded
            MonthlyBoldedDatesMask = 0;
            foreach (DateTime dt in value)
            {
                MonthlyBoldedDatesMask |= 1 << (dt.Day - 1);
            }

            PerformNeedPaint(true);
        }
    }

    private void ResetMonthlyBoldedDates() => MonthlyBoldedDates = null;

    private bool ShouldSerializeMonthlyBoldedDates() => _monthlyDates.Count > 0;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are Displayed in bold.
    /// </summary>
    [Localizable(true)]
    [Description(@"Indicates which dates should be boldface.")]
    [AllowNull]
    public DateTime[]? BoldedDates
    {
        get => BoldedDatesList.ToArray();

        set
        {
            value ??= Array.Empty<DateTime>();

            BoldedDatesList.Clear();
            BoldedDatesList.AddRange(value);
            PerformNeedPaint(true);
        }
    }

    private void ResetBoldedDates() => BoldedDates = null;

    private bool ShouldSerializeBoldedDates() => BoldedDatesList.Count > 0;

    /// <summary>
    /// Gets or sets the maximum allowable date.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum allowable date.")]
    [RefreshProperties(RefreshProperties.All)]
    public DateTime MaxDate
    {
        get => EffectiveMaxDate(_maxDate);

        set
        {
            if (value != _maxDate)
            {
                if (value < EffectiveMinDate(_minDate))
                {
                    throw new ArgumentOutOfRangeException(nameof(MaxDate), @"Date provided is less than the minimum supported date.");
                }

                if (value > DateTimePicker.MaximumDateTime)
                {
                    throw new ArgumentOutOfRangeException(nameof(MaxDate), @"Date provided is greater than the maximum supported date.");
                }

                _maxDate = value;

                // Update selection dates to be valid with new max date
                SetRange();
            }
        }
    }

    private void ResetMaxDate() => MaxDate = DateTime.MaxValue;

    private bool ShouldSerializeMaxDate() => (_maxDate != DateTimePicker.MaximumDateTime) && (_maxDate != DateTime.MaxValue);

    /// <summary>
    /// Gets or sets the maximum number of days that can be selected in a month calendar control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum number of days that can be selected.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(7)]
    public int MaxSelectionCount
    {
        get => _maxSelectionCount;

        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(MaxSelectionCount), @"MaxSelectionCount cannot be less than zero.");
            }

            if (value != _maxSelectionCount)
            {
                _maxSelectionCount = value;
                SetSelectionRange(_selectionStart, _selectionEnd);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the start date of the selected range of dates.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Start date of the selected range of dates.")]
    [RefreshProperties(RefreshProperties.All)]
    [Bindable(true)]
    public DateTime SelectionStart
    {
        get => _selectionStart;

        set
        {
            if (value != _selectionStart)
            {
                if (value > _maxDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(SelectionStart), @"Date provided is greater than the maximum date.");
                }

                if (value < _minDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(SelectionStart), @"Date provided is less than the minimum date.");
                }

                DateTime endDate = _selectionEnd;

                // End date cannot be before the start date
                if (endDate < value)
                {
                    endDate = value;
                }

                // Limit the selection range to the maximum selection count
                TimeSpan range = endDate - value;
                if (range.Days >= _maxSelectionCount)
                {
                    endDate = value.AddDays(_maxSelectionCount - 1);
                }

                // Update selection dates and generate event if required
                SetSelRange(value, endDate);
            }
        }
    }

    private void ResetSelectionStart() => SelectionStart = DateTime.Now.Date;

    private bool ShouldSerializeSelectionStart() => SelectionStart != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the end date of the selected range of dates.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"End date of the selected range of dates.")]
    [RefreshProperties(RefreshProperties.All)]
    [Bindable(true)]
    public DateTime SelectionEnd
    {
        get => _selectionEnd;

        set
        {
            if (value != _selectionEnd)
            {
                if (value > _maxDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(SelectionEnd), @"Date provided is greater than the maximum date.");
                }

                if (value < _minDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(SelectionEnd), @"Date provided is less than the minimum date.");
                }

                DateTime startDate = _selectionStart;

                // Start date cannot be after the end date
                if (startDate > value)
                {
                    startDate = value;
                }

                // Limit the selection range to the maximum selection count
                TimeSpan range = value - startDate;
                if (range.Days >= _maxSelectionCount)
                {
                    startDate = value.AddDays(1 - _maxSelectionCount);
                }

                // Update selection dates and generate event if required
                SetSelRange(startDate, value);
            }
        }
    }

    private void ResetSelectionEnd() => SelectionEnd = DateTime.Now.Date;

    private bool ShouldSerializeSelectionEnd() => SelectionEnd != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the selected range of dates for a month calendar control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies the selected range of dates.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RefreshProperties(RefreshProperties.All)]
    [Bindable(true)]
    public SelectionRange SelectionRange
    {
        get => new SelectionRange(SelectionStart, SelectionEnd);
        set => SetSelectionRange(value.Start, value.End);
    }

    private void ResetSelectionRange()
    {
        ResetSelectionStart();
        ResetSelectionEnd();
    }

    private bool ShouldSerializeSelectionRange() => false;

    /// <summary>
    /// Gets or sets the number of columns and rows of months Displayed. 
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Specifies the number of rows and columns of months Displayed.")]
    [DefaultValue(typeof(Size), "1,1")]
    [Localizable(true)]
    public Size CalendarDimensions
    {
        get => _dimensions;

        set
        {
            if (!_dimensions.Equals(value))
            {
                if (value.Width < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(CalendarDimensions), @"CalendarDimension Width must be greater than 0");
                }

                if (value.Height < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(CalendarDimensions), @"CalendarDimension Height must be greater than 0");
                }

                _dimensions = value;

                // Must update the size to get the new size we require, just calling the perform need 
                // paint will cause the dimensions to be reset to that matching the current size.
                Size = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// First day of the week.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"First day of the week.")]
    [DefaultValue(Day.Default)]
    [Localizable(true)]
    public Day FirstDayOfWeek
    {
        get => _firstDayOfWeek;

        set
        {
            if (_firstDayOfWeek != value)
            {
                _firstDayOfWeek = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the background style for the month calendar.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background style for the month calendar.")]
    public PaletteBackStyle ControlBackStyle
    {
        get => StateCommon.BackStyle;

        set
        {
            if (StateCommon.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeControlBackStyle() => ControlBackStyle != PaletteBackStyle.ControlClient;

    private void ResetControlBackStyle() => ControlBackStyle = PaletteBackStyle.ControlClient;

    /// <summary>
    /// Gets and sets the border style for the month calendar.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Border style for the month calendar.")]
    public PaletteBorderStyle ControlBorderStyle
    {
        get => StateCommon.BorderStyle;

        set
        {
            if (StateCommon.BorderStyle != value)
            {
                StateCommon.BorderStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeControlBorderStyle() => ControlBorderStyle != PaletteBorderStyle.ControlClient;

    private void ResetControlBorderStyle() => ControlBorderStyle = PaletteBorderStyle.ControlClient;

    /// <summary>
    /// Gets and sets the header style for the month calendar.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Header style for the month calendar.")]
    public HeaderStyle HeaderStyle
    {
        get => _headerStyle;

        set
        {
            if (_headerStyle != value)
            {
                _headerStyle = value;
                StateCommon.Header.SetStyles(_headerStyle);
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHeaderStyle() => _headerStyle != HeaderStyle.Calendar;

    private void ResetHeaderStyle() => HeaderStyle = HeaderStyle.Calendar;

    /// <summary>
    /// Gets and sets the content style for the day entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Content style for the day entries.")]
    public ButtonStyle DayStyle
    {
        get => _dayStyle;

        set
        {
            if (_dayStyle != value)
            {
                _dayStyle = value;
                StateCommon.DayStyle = value;
                OverrideBolded.DayStyle = value;
                OverrideFocus.DayStyle = value;
                OverrideToday.DayStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeDayStyle() => _dayStyle != ButtonStyle.CalendarDay;

    private void ResetDayStyle() => DayOfWeekStyle = ButtonStyle.CalendarDay;

    /// <summary>
    /// Gets and sets the content style for the day of week labels.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Content style for the day of week labels.")]
    public ButtonStyle DayOfWeekStyle
    {
        get => _dayOfWeekStyle;

        set
        {
            if (_dayOfWeekStyle != value)
            {
                _dayOfWeekStyle = value;
                StateCommon.DayOfWeekStyle = value;
                PerformNeedPaint(true);
            }
        }
    }
    private bool ShouldSerializeDayOfWeekStyle() => _dayOfWeekStyle != ButtonStyle.CalendarDay;
    private void ResetDayOfWeekStyle() => DayOfWeekStyle = ButtonStyle.CalendarDay;

    /// <summary>
    /// Gets and sets if the control will display todays date.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether this month calendar will display todays date.")]
    [DefaultValue(true)]
    public bool ShowToday
    {
        get => _drawMonths!.ShowToday;

        set
        {
            if (_drawMonths!.ShowToday != value)
            {
                _drawMonths.ShowToday = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets if the control will circle the today date.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether this month calendar will circle the today date.")]
    [DefaultValue(true)]
    public bool ShowTodayCircle
    {
        get => _drawMonths!.ShowTodayCircle;

        set
        {
            if (_drawMonths!.ShowTodayCircle != value)
            {
                _drawMonths.ShowTodayCircle = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets if week numbers to the left of each row.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether this month calendar will display week numbers to the left of each row.")]
    [DefaultValue(false)]
    public bool ShowWeekNumbers
    {
        get => _drawMonths!.ShowWeekNumbers;

        set
        {
            if (_drawMonths!.ShowWeekNumbers != value)
            {
                _drawMonths.ShowWeekNumbers = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to the day appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarStateRedirect OverrideFocus { get; }
    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets access to the day appearance when it is bolded.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar appearance when it is bolded.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarStateRedirect OverrideBolded { get; }
    private bool ShouldSerializeOverrideBolded() => !OverrideBolded.IsDefault;

    /// <summary>
    /// Gets access to the day appearance when it is todays.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar appearance when it is today.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarStateRedirect OverrideToday { get; }
    private bool ShouldSerializeOverrideToday() => !OverrideToday.IsDefault;

    /// <summary>
    /// Gets access to the common month calendar appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common month calendar appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarRedirect StateCommon { get; }
    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the month calendar disabled appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarDoubleState StateDisabled { get; }
    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the month calendar normal appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarDoubleState StateNormal { get; }
    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the tracking month calendar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateTracking { get; }
    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed month calendar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StatePressed { get; }
    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the checked normal month calendar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked normal month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateCheckedNormal { get; }
    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    /// <summary>
    /// Gets access to the checked tracking month calendar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked tracking month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateCheckedTracking { get; }
    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    /// <summary>
    /// Gets access to the checked pressed month calendar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked pressed month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateCheckedPressed { get; }
    private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MonthCalendarButtonSpecCollection ButtonSpecs => _drawMonths!.ButtonSpecs;

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => _drawMonths!.AllowButtonSpecToolTips;
        set => _drawMonths!.AllowButtonSpecToolTips = value;
    }

    /// <summary>
    /// Adds a day that is Displayed in bold on an annual basis in the month calendar.
    /// </summary>
    /// <param name="date">The date to be Displayed in bold.</param>
    public void AddAnnuallyBoldedDate(DateTime date)
    {
        if (!_annualDates.Contains(date))
        {
            _annualDates.Add(date);
            AnnuallyBoldedDatesMask[date.Month - 1] |= 1 << (date.Day - 1);
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Adds a day to be Displayed in bold in the month calendar.
    /// </summary>
    /// <param name="date">The date to be Displayed in bold.</param>
    public void AddBoldedDate(DateTime date)
    {
        if (!BoldedDatesList.Contains(date))
        {
            BoldedDatesList.Add(date);
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Adds a day that is Displayed in bold on a monthly basis in the month calendar.
    /// </summary>
    /// <param name="date">The date to be Displayed in bold.</param>
    public void AddMonthlyBoldedDate(DateTime date)
    {
        if (!_monthlyDates.Contains(date))
        {
            _monthlyDates.Add(date);
            MonthlyBoldedDatesMask |= 1 << (date.Day - 1);
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Removes all the annually bold dates.
    /// </summary>
    public void RemoveAllAnnuallyBoldedDates()
    {
        _annualDates.Clear();
        for (var i = 0; i < 12; i++)
        {
            AnnuallyBoldedDatesMask[i] = 0;
        }

        PerformNeedPaint(true);
    }

    /// <summary>
    /// Removes all the nonrecurring bold dates.
    /// </summary>
    public void RemoveAllBoldedDates()
    {
        BoldedDatesList.Clear();
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Removes all the monthly bold dates.
    /// </summary>
    public void RemoveAllMonthlyBoldedDates()
    {
        _monthlyDates.Clear();
        MonthlyBoldedDatesMask = 0;
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Gets access to the owning control
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Control CalendarControl => this;

    /// <summary>
    /// Gets if the control is in design mode.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool InDesignMode => DesignMode;

    /// <summary>
    /// Get the renderer.
    /// </summary>
    /// <returns>Render instance.</returns>
    public IRenderer GetRenderer() => Renderer;

    /// <summary>
    /// Gets access to the override for disabled day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteTripleOverride OverrideDisabled { get; }

    /// <summary>
    /// Gets access to the override for disabled day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteTripleOverride OverrideNormal { get; }

    /// <summary>
    /// Gets access to the override for tracking day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteTripleOverride OverrideTracking { get; }

    /// <summary>
    /// Gets access to the override for pressed day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteTripleOverride OverridePressed { get; }

    /// <summary>
    /// Gets access to the override for checked normal day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteTripleOverride OverrideCheckedNormal { get; }

    /// <summary>
    /// Gets access to the override for checked tracking day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteTripleOverride OverrideCheckedTracking { get; }

    /// <summary>
    /// Gets access to the override for checked pressed day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteTripleOverride OverrideCheckedPressed { get; }

    /// <summary>
    /// Dates to be bolded.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public DateTimeList BoldedDatesList { get; }

    /// <summary>
    /// Monthly days to be bolded.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MonthlyBoldedDatesMask { get; private set; }

    /// <summary>
    /// Array of annual days per month to be bolded.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public int[] AnnuallyBoldedDatesMask { get; }

    /// <summary>
    /// Set the selection range.
    /// </summary>
    /// <param name="start">New starting date.</param>
    /// <param name="end">New ending date.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetSelectionRange(DateTime start, DateTime end)
    {
        if (start.Ticks > _maxDate.Ticks)
        {
            throw new ArgumentOutOfRangeException(nameof(start), @"Start date provided is greater than the maximum date.");
        }

        if (start.Ticks < _minDate.Ticks)
        {
            throw new ArgumentOutOfRangeException(nameof(start), @"Start date provided is less than the minimum date.");
        }

        if (end.Ticks > _maxDate.Ticks)
        {
            throw new ArgumentOutOfRangeException(nameof(end), @"End date provided is greater than the maximum date.");
        }

        if (end.Ticks < _minDate.Ticks)
        {
            throw new ArgumentOutOfRangeException(nameof(end), @"End date provided is less than the minimum date.");
        }

        if (start > end)
        {
            end = start;
        }

        // If the range exceeds maxSelectionCount, compare with the previous range and adjust whichever
        // limit hasn't changed.
        TimeSpan span = end - start;
        if (span.Days >= _maxSelectionCount)
        {
            if (start.Ticks == _selectionStart.Ticks)
            {
                // Bring start date forward
                start = end.AddDays(1 - _maxSelectionCount);
            }
            else
            {
                // Bring end date back
                end = start.AddDays(_maxSelectionCount - 1);
            }
        }

        SetSelRange(start, end);
    }

    /// <summary>
    /// Gets the focus day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime? FocusDay { get; set; }

    /// <summary>
    /// Update usage of bolded overrides.
    /// </summary>
    /// <param name="bolded">New bolded state.</param>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetBoldedOverride(bool bolded)
    {
        _boldedDisabled.Apply = bolded;
        _boldedNormal.Apply = bolded;
        _boldedTracking.Apply = bolded;
        _boldedPressed.Apply = bolded;
        _boldedCheckedNormal.Apply = bolded;
        _boldedCheckedTracking.Apply = bolded;
        _boldedCheckedPressed.Apply = bolded;
    }

    /// <summary>
    /// Update usage of today overrides.
    /// </summary>
    /// <param name="today">New today state.</param>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetTodayOverride(bool today)
    {
        _todayDisabled.Apply = today;
        _todayNormal.Apply = today;
        _todayTracking.Apply = today;
        _todayPressed.Apply = today;
        _todayCheckedNormal.Apply = today;
        _todayCheckedTracking.Apply = today;
        _todayCheckedPressed.Apply = today;
    }

    /// <summary>
    /// Update usage of focus overrides.
    /// </summary>
    /// <param name="focus">Should show focus.</param>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetFocusOverride(bool focus)
    {
        OverrideDisabled.Apply = _hasFocus && focus;
        OverrideNormal.Apply = _hasFocus && focus;
        OverrideTracking.Apply = _hasFocus && focus;
        OverridePressed.Apply = _hasFocus && focus;
        OverrideCheckedNormal.Apply = _hasFocus && focus;
        OverrideCheckedTracking.Apply = _hasFocus && focus;
        OverrideCheckedPressed.Apply = _hasFocus && focus;
    }

    /// <summary>
    /// Gets a delegate for creating tool strip renderers.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public GetToolStripRenderer GetToolStripDelegate => CreateToolStripRenderer;

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool DesignerGetHitTest(Point pt)
    {
        // Ignore call as view builder is already destructed
        if (IsDisposed)
        {
            return false;
        }

        // Check if any of the button specs want the point
        return (_drawMonths != null) && _drawMonths.ButtonManager.DesignerGetHitTest(pt);
    }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public Component? DesignerComponentFromPoint(Point pt) =>
        // Ignore call as view builder is already destructed
        IsDisposed ? null : ViewManager?.ComponentFromPoint(pt);

    // Ask the current view for a decision
    /// <summary>
    /// Internal design time method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public void DesignerMouseLeave() =>
        // Simulate the mouse leaving the control so that the tracking
        // element that thinks it has the focus is informed it does not
        OnMouseLeave(EventArgs.Empty);

    #endregion

    #region Protected
    /// <summary>
    /// Processes a notification from palette storage of a button spec change.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected override void OnButtonSpecChanged(object? sender, EventArgs e)
    {
        // Recreate all the button specs with new values
        _drawMonths?.RecreateButtons();

        // Let base class perform standard processing
        base.OnButtonSpecChanged(sender, e);
    }

    /// <summary>
    /// Determines if a character is an input character that the control recognizes.
    /// </summary>
    /// <param name="charCode">The character to test.</param>
    /// <returns>true if the character should be sent directly to the control and not preprocessed; otherwise, false.</returns>
    protected override bool IsInputChar(char charCode) =>
        // We take all regular input characters
        char.IsLetterOrDigit(charCode);

    /// <summary>
    /// Determines whether the specified key is a regular input key or a special key that requires preprocessing.
    /// </summary>
    /// <param name="keyData">One of the Keys values.</param>
    /// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
    protected override bool IsInputKey(Keys keyData) => (keyData & ~Keys.Shift) switch
    {
        Keys.Left or Keys.Right or Keys.Up or Keys.Down => true,
        _ => base.IsInputKey(keyData)
    };

    /// <summary>
    /// Raises the KeyDown event.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing)
        {
            if (_drawMonths!.ProcessKeyDown(this, e))
            {
                return;
            }
        }

        // Let base class fire events
        base.OnKeyDown(e);
    }

    /// <summary>
    /// Raises when the DateChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnDateChanged(DateRangeEventArgs e) => DateChanged?.Invoke(this, e);

    protected virtual void OnDateSelected(DateRangeEventArgs e) => DateSelected?.Invoke(this, e);

    /// <summary>
    /// Raises when the SelectionStartChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnSelectionStartChanged(EventArgs e) => SelectionStartChanged?.Invoke(this, e);

    /// <summary>
    /// Raises when the SelectionEndChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnSelectionEndChanged(EventArgs e) => SelectionEndChanged?.Invoke(this, e);

    /// <summary>
    /// Raises when the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        // Ensure there is a defined focus day
        SetFocusDay();

        // Apply the focus overrides
        UpdateFocusOverride(true);

        // Change in focus requires a repaint
        PerformNeedPaint(false);

        base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises when the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
        // Apply the focus overrides
        UpdateFocusOverride(false);

        // Change in focus requires a repaint
        PerformNeedPaint(false);

        base.OnLostFocus(e);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">An PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        Paint?.Invoke(this, e!);

        base.OnPaint(e);
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        Click?.Invoke(this, e);

        base.OnClick(e);
    }

    /// <summary>
    /// Raises the DoubleClick event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnDoubleClick(EventArgs e)
    {
        DoubleClick?.Invoke(this, e);

        base.OnDoubleClick(e);
    }

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e)
    {
        TextChanged?.Invoke(this, e);

        base.OnTextChanged(e);
    }

    /// <summary>
    /// Raises the ForeColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnForeColorChanged(EventArgs e)
    {
        ForeColorChanged?.Invoke(this, e);

        base.OnForeColorChanged(e);
    }

    /// <summary>
    /// Raises the FontChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnFontChanged(EventArgs e)
    {
        FontChanged?.Invoke(this, e);

        base.OnFontChanged(e);
    }

    /// <summary>
    /// Raises the BackgroundImageChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageChanged(EventArgs e)
    {
        BackgroundImageChanged?.Invoke(this, e);

        base.OnBackgroundImageChanged(e);
    }

    /// <summary>
    /// Raises the BackgroundImageLayoutChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageLayoutChanged(EventArgs e)
    {
        BackgroundImageLayoutChanged?.Invoke(this, e);

        base.OnBackgroundImageLayoutChanged(e);
    }

    /// <summary>
    /// Raises the BackColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackColorChanged(EventArgs e)
    {
        BackColorChanged?.Invoke(this, e);

        base.OnBackColorChanged(e);
    }

    /// <summary>
    /// Raises the PaddingChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaddingChanged(EventArgs e)
    {
        PaddingChanged?.Invoke(this, e);

        base.OnPaddingChanged(e);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Update view elements
        _drawDocker.Enabled = Enabled;
        _drawMonths!.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing)
        {
            // Find the correct size for the control
            var width = Width;
            var height = Height;
            AdjustSize(ref width, ref height);

            // If the current size is not correct then change now
            if ((width != Width) || (height != Height))
            {
                Size = new Size(width, height);
            }
        }

        // Let base class layout child controls
        base.OnLayout(levent);
    }

    /// <summary>
    /// Performs the work of setting the specified bounds of this control.
    /// </summary>
    /// <param name="x">The new Left property value of the control.</param>
    /// <param name="y">The new Top property value of the control.</param>
    /// <param name="width">The new Width property value of the control.</param>
    /// <param name="height">The new Height property value of the control.</param>
    /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        AdjustSize(ref width, ref height);
        base.SetBoundsCore(x, y, width, height, specified);
    }
    #endregion

    #region Private
    /// <summary>
    ///  Return a localized string representation of the given DateTime value.
    ///  Used for throwing exceptions, etc.
    /// </summary>
    private static string FormatDate(DateTime value)
        => value.ToString("d", CultureInfo.CurrentCulture);

    private DateTime EffectiveMaxDate(DateTime maxDate)
    {
        DateTime maximumDateTime = DateTimePicker.MaximumDateTime;
        return maxDate > maximumDateTime ? maximumDateTime : maxDate;
    }

    private DateTime EffectiveMinDate(DateTime minDate)
    {
        DateTime minimumDateTime = DateTimePicker.MinimumDateTime;
        return minDate < minimumDateTime ? minimumDateTime : minDate;
    }

    private void AdjustSize(ref int width, ref int height)
    {
        using var context = new ViewLayoutContext(this, Renderer);
        // Ask back/border the size it requires
        Size backBorderSize = _drawDocker.GetNonChildSize(context);

        // Ask for the size needed to draw a single month
        Size singleMonthSize = _drawMonths!.GetSingleMonthSize(context);

        // How many full months can be fit in each dimension (with a minimum of 1 month showing)
        var gap = ViewLayoutMonths.GAP;
        var widthMonths = Math.Max(1, (width - backBorderSize.Width - gap) / (singleMonthSize.Width + gap));
        var heightMonths = Math.Max(1, (height - backBorderSize.Height - gap) / (singleMonthSize.Height + gap));

        // Calculate new sizes based on showing only full months
        width = backBorderSize.Width + (widthMonths * singleMonthSize.Width) + (gap * (widthMonths + 1));
        height = backBorderSize.Height + (heightMonths * singleMonthSize.Height) + (gap * (heightMonths + 1));

        // Ask the month layout for size of extra areas such as headers etc
        Size extraSize = _drawMonths.GetExtraSize(context);
        width += extraSize.Width;
        height += extraSize.Height;

        // Update the calendar dimensions to match the actual size
        CalendarDimensions = new Size(widthMonths, heightMonths);
    }

    private void SetRange()
    {
        var startChanged = false;
        var endChanged = false;

        DateTime minDate = EffectiveMinDate(_minDate);
        DateTime maxDate = EffectiveMaxDate(_maxDate);

        if (_selectionStart < minDate)
        {
            _selectionStart = minDate.Date;
            startChanged = true;
        }

        if (_selectionStart > maxDate)
        {
            _selectionStart = maxDate.Date;
            startChanged = true;
        }

        if (_selectionEnd < minDate)
        {
            _selectionEnd = minDate.Date;
            endChanged = true;
        }

        if (_selectionEnd > maxDate)
        {
            _selectionEnd = maxDate.Date;
            endChanged = true;
        }

        PerformNeedPaint(true);

        if (startChanged)
        {
            OnSelectionStartChanged(EventArgs.Empty);
        }

        if (endChanged)
        {
            OnSelectionEndChanged(EventArgs.Empty);
        }

        if (startChanged || endChanged)
        {
            OnDateChanged(new DateRangeEventArgs(_selectionStart, _selectionEnd));
        }

        OnDateSelected(new DateRangeEventArgs(minDate, maxDate));

        SetFocusDay();
    }

    private void SetSelRange(DateTime lower, DateTime upper)
    {
        var startChanged = false;
        var endChanged = false;

        if (lower != _selectionStart)
        {
            _selectionStart = lower;
            startChanged = true;
        }

        if (upper != _selectionEnd)
        {
            _selectionEnd = upper;
            endChanged = true;
        }

        PerformNeedPaint(true);

        if (startChanged)
        {
            OnSelectionStartChanged(EventArgs.Empty);
        }

        if (endChanged)
        {
            OnSelectionEndChanged(EventArgs.Empty);
        }

        if (startChanged || endChanged)
        {
            OnDateChanged(new DateRangeEventArgs(_selectionStart, _selectionEnd));
        }

        SetFocusDay();
    }

    private void SetFocusDay()
    {
        if (FocusDay == null)
        {
            FocusDay = SelectionStart.Date;
        }
        else
        {
            if (FocusDay.Value < SelectionStart)
            {
                FocusDay = SelectionStart.Date;
            }
            else if (FocusDay.Value > SelectionStart)
            {
                FocusDay = SelectionEnd.Date;
            }
        }
    }

    private void UpdateFocusOverride(bool focus) => _hasFocus = focus;
    #endregion
}