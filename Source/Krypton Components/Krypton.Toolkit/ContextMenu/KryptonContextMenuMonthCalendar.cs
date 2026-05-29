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
/// Allow user to select a date using a visual monthly calendar display.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuMonthCalendar), "ToolboxBitmaps.KryptonMonthCalendar.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(DateChanged))]
[DefaultProperty(nameof(SelectionRange))]
public class KryptonContextMenuMonthCalendar : KryptonContextMenuItemBase
{
    #region Static Fields

    private const string DEFAULT_TODAY = "Today:";

    #endregion

    #region Instance Fields

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
    private Day _firstDayOfWeek;
    private Size _dimensions;
    private string _todayFormat;
    private bool _autoClose;
    private bool _enabled;
    private bool _showWeekNumbers;
    private bool _showTodayCircle;
    private bool _showToday;
    private bool _closeOnTodayClick;
    private DateTime _selectionStart;
    private DateTime _selectionEnd;
    private DateTime _minDate;
    private DateTime _maxDate;
    private DateTime _todayDate;
    private int _maxSelectionCount;
    private readonly DateTimeList _annualDates;
    private readonly DateTimeList _monthlyDates;
    private string _today;
    private int _scrollChange;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the selected date changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the selected date changes.")]
    public event DateRangeEventHandler? DateChanged;

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
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuMonthCalendar class.
    /// </summary>
    public KryptonContextMenuMonthCalendar()
    {
        // Default fields
        _autoClose = true;
        _enabled = true;
        _showToday = true;
        _showTodayCircle = true;
        _closeOnTodayClick = false;
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
        _today = DEFAULT_TODAY;
        _todayFormat = "d";

        // Create the common/override state storage
        StateCommon = new PaletteMonthCalendarRedirect();
        OverrideFocus = new PaletteMonthCalendarStateRedirect();
        OverrideBolded = new PaletteMonthCalendarStateRedirect();
        OverrideToday = new PaletteMonthCalendarStateRedirect();

        // Basic state storage
        StateDisabled = new PaletteMonthCalendarDoubleState(StateCommon);
        StateNormal = new PaletteMonthCalendarDoubleState(StateCommon);
        StateTracking = new PaletteMonthCalendarState(StateCommon);
        StatePressed = new PaletteMonthCalendarState(StateCommon);
        StateCheckedNormal = new PaletteMonthCalendarState(StateCommon);
        StateCheckedTracking = new PaletteMonthCalendarState(StateCommon);
        StateCheckedPressed = new PaletteMonthCalendarState(StateCommon);

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
    }

    /// <summary>
    /// Returns a description of the instance.
    /// </summary>
    /// <returns>String representation.</returns>
    public override string ToString() => "(Month Calendar)";

    #endregion

    #region Public
    /// <summary>
    /// Returns the number of child menu items.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ItemChildCount => 0;

    /// <summary>
    /// Returns the indexed child menu item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override KryptonContextMenuItemBase? this[int index] => null;

    /// <summary>
    /// Test for the provided shortcut and perform relevant action if a match is found.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if shortcut was handled, otherwise false.</returns>
    public override bool ProcessShortcut(Keys keyData) => false;

    /// <summary>
    /// Returns a view appropriate for this item based on the object it is inside.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="parent">Owning object reference.</param>
    /// <param name="columns">Containing columns.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    /// <returns>ViewBase that is the root of the view hierarchy being added.</returns>
    public override ViewBase GenerateView(IContextMenuProvider provider,
        object parent,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn)
    {
        SetProvider(provider);
        return new ViewDrawMenuMonthCalendar(provider, this);
    }

    /// <summary>
    /// Gets and sets if selecting a day automatically closes the context menu.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates if selecting a day closes the context menu.")]
    [DefaultValue(true)]
    public bool AutoClose
    {
        get => _autoClose;

        set
        {
            if (_autoClose != value)
            {
                _autoClose = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AutoClose)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if clicking the Today button closes the drop-down menu.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates if clicking the Today button closes the drop-down menu.")]
    [DefaultValue(false)]
    public bool CloseOnTodayClick
    {
        get => _closeOnTodayClick;

        set
        {
            if (_closeOnTodayClick != value)
            {
                _closeOnTodayClick = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CloseOnTodayClick)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if the radio button is enabled.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the month calendar is enabled.")]
    [DefaultValue(true)]
    [Bindable(true)]
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the number of months to scroll when next/prev buttons are used.
    /// </summary>
    [KryptonPersist]
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
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ScrollChange)));
        }
    }

    /// <summary>
    /// Gets or sets today's date.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Today's date.")]
    [AllowNull]
    public DateTime TodayDate
    {
        get => _todayDate;

        set
        {
            _todayDate = value == null ? DateTime.Now.Date : value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(TodayDate)));
        }
    }

    private void ResetTodayDate() => TodayDate = DateTime.Now.Date;

    private bool ShouldSerializeTodayDate() => TodayDate != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which annual days are Displayed in bold.
    /// </summary>
    [KryptonPersist]
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

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(AnnuallyBoldedDates)));
        }
    }

    private void ResetAnnuallyBoldedDates() => AnnuallyBoldedDates = null;

    private bool ShouldSerializeAnnuallyBoldedDates() => _annualDates.Count > 0;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
    /// </summary>
    [KryptonPersist]
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

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(MonthlyBoldedDates)));
        }
    }

    private void ResetMonthlyBoldedDates() => MonthlyBoldedDates = null;

    private bool ShouldSerializeMonthlyBoldedDates() => _monthlyDates.Count > 0;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are Displayed in bold.
    /// </summary>
    [KryptonPersist]
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
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(BoldedDates)));
        }
    }

    private void ResetBoldedDates() => BoldedDates = null;

    private bool ShouldSerializeBoldedDates() => BoldedDatesList.Count > 0;

    /// <summary>
    /// Gets or sets the minimum allowable date.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Minimum allowable date.")]
    [RefreshProperties(RefreshProperties.All)]
    public DateTime MinDate
    {
        get => _minDate;

        set
        {
            if (value != _minDate)
            {
                if (value > DateTimePicker.MaximumDateTime)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is greater than the maximum culture supported date.");
                }

                if (value < DateTimePicker.MinimumDateTime)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is less than the minimum culture supported date.");
                }
            }

            _minDate = value;
            SetRange();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(MinDate)));
        }
    }

    private void ResetMinDate() => MinDate = DateTimePicker.MinimumDateTime;

    private bool ShouldSerializeMinDate() => _minDate != DateTimePicker.MinimumDateTime;

    /// <summary>
    /// Gets or sets the maximum allowable date.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Maximum allowable date.")]
    [RefreshProperties(RefreshProperties.All)]
    public DateTime MaxDate
    {
        get => _maxDate;

        set
        {
            if (value != _maxDate)
            {
                if (value > DateTimePicker.MaximumDateTime)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is greater than the maximum culture supported date.");
                }

                if (value < DateTimePicker.MinimumDateTime)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is less than the minimum culture supported date.");
                }
            }

            _maxDate = value;
            SetRange();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(MaxDate)));
        }
    }

    private void ResetMaxDate() => MaxDate = DateTimePicker.MaximumDateTime;

    private bool ShouldSerializeMaxDate() => _maxDate != DateTimePicker.MaximumDateTime;

    /// <summary>
    /// Gets or sets the maximum number of days that can be selected in a month calendar control.
    /// </summary>
    [KryptonPersist]
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
                throw new ArgumentOutOfRangeException(nameof(value), @"MaxSelectionCount cannot be less than zero.");
            }

            if (value != _maxSelectionCount)
            {
                _maxSelectionCount = value;
                SetSelectionRange(_selectionStart, _selectionEnd);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MaxSelectionCount)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the start date of the selected range of dates.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Start date of the selected range of dates.")]
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    public DateTime SelectionStart
    {
        get => _selectionStart;

        set
        {
            if (value != _selectionStart)
            {
                if (value > _maxDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is greater than the maximum date.");
                }

                if (value < _minDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is less than the minimum date.");
                }

                // End date cannot be before the start date
                if (_selectionEnd < value)
                {
                    _selectionEnd = value;
                }

                // Limit the selection range to the maximum selection count
                TimeSpan range = _selectionEnd - value;
                if (range.Days >= _maxSelectionCount)
                {
                    _selectionEnd = value.AddDays(_maxSelectionCount - 1);
                }

                // Update selection dates and generate event if required
                SetSelRange(value, _selectionEnd);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectionStart)));
            }
        }
    }

    private void ResetSelectionStart() => SelectionStart = DateTime.Now.Date;

    private bool ShouldSerializeSelectionStart() => SelectionStart != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the end date of the selected range of dates.
    /// </summary>
    [KryptonPersist]
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
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is greater than the maximum date.");
                }

                if (value < _minDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Date provided is less than the minimum date.");
                }

                // Start date cannot be after the end date
                if (_selectionStart > value)
                {
                    _selectionStart = value;
                }

                // Limit the selection range to the maximum selection count
                TimeSpan range = value - _selectionStart;
                if (range.Days >= _maxSelectionCount)
                {
                    _selectionStart = value.AddDays(1 - _maxSelectionCount);
                }

                // Update selection dates and generate event if required
                SetSelRange(_selectionStart, value);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectionEnd)));
            }
        }
    }

    private void ResetSelectionEnd() => SelectionEnd = DateTime.Now.Date;

    private bool ShouldSerializeSelectionEnd() => SelectionStart != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the selected range of dates for a month calendar control.
    /// </summary>
    [KryptonPersist]
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
    /// Gets or sets the today date format string.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The today format string used to format the date Displayed in the today button.")]
    [DefaultValue(@"d")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    [AllowNull]
    public string TodayFormat
    {
        get => _todayFormat;

        set
        {
            if ((_todayFormat != value) && (value != null))
            {
                _todayFormat = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TodayFormat)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the label text for todays text. 
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Text used as label for todays date.")]
    [DefaultValue(@"Today:")]
    [Localizable(true)]
    [AllowNull]
    public string TodayText
    {
        get => _today;

        set
        {
            value ??= DEFAULT_TODAY;

            _today = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(TodayText)));
        }
    }

    private void ResetTodayText() => TodayText = DEFAULT_TODAY;

    /// <summary>
    /// Gets or sets the number of columns and rows of months Displayed. 
    /// </summary>
    [KryptonPersist]
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
                    throw new ArgumentOutOfRangeException(nameof(value), @"CalendarDimension Width must be greater than 0");
                }

                if (value.Height < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"CalendarDimension Height must be greater than 0");
                }

                _dimensions = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CalendarDimensions)));
            }
        }
    }

    /// <summary>
    /// First day of the week.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"First day of the week.")]
    [Localizable(true)]
    public Day FirstDayOfWeek
    {
        get => _firstDayOfWeek;

        set
        {
            if (_firstDayOfWeek != value)
            {
                _firstDayOfWeek = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FirstDayOfWeek)));
            }
        }
    }

    private void ResetFirstDayOfWeek() => FirstDayOfWeek = Day.Default;

    private bool ShouldSerializeFirstDayOfWeek() => FirstDayOfWeek != Day.Default;

    /// <summary>
    /// Gets and sets if the control will display todays date.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether this month calendar will display todays date.")]
    [DefaultValue(true)]
    public bool ShowToday
    {
        get => _showToday;

        set
        {
            if (_showToday != value)
            {
                _showToday = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShowToday)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if the control will circle the today date.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether this month calendar will circle the today date.")]
    [DefaultValue(true)]
    public bool ShowTodayCircle
    {
        get => _showTodayCircle;

        set
        {
            if (_showTodayCircle != value)
            {
                _showTodayCircle = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShowTodayCircle)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if week numbers to the left of each row.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether this month calendar will display week numbers to the left of each row.")]
    [DefaultValue(false)]
    public bool ShowWeekNumbers
    {
        get => _showWeekNumbers;

        set
        {
            if (_showWeekNumbers != value)
            {
                _showWeekNumbers = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShowWeekNumbers)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the header style for the month calendar.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Header style for the month calendar.")]
    [DefaultValue(HeaderStyle.Calendar)]
    public HeaderStyle HeaderStyle
    {
        get => _headerStyle;

        set
        {
            if (_headerStyle != value)
            {
                _headerStyle = value;
                StateCommon.Header.SetStyles(_headerStyle);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(HeaderStyle)));
            }
        }
    }

    private bool ShouldSerializeHeaderStyle() => _headerStyle != HeaderStyle.Calendar;

    /// <summary>
    /// Gets and sets the content style for the day entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Content style for the day entries.")]
    [DefaultValue(ButtonStyle.CalendarDay)]
    public ButtonStyle DayStyle
    {
        get => _dayStyle;

        set
        {
            if (_dayStyle != value)
            {
                _dayStyle = value;
                StateCommon.DayStyle = value;
                OverrideFocus.DayStyle = value;
                OverrideBolded.DayStyle = value;
                OverrideToday.DayStyle = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DayStyle)));
            }
        }
    }

    private bool ShouldSerializeDayStyle() => _dayStyle != ButtonStyle.CalendarDay;

    private void ResetDayStyle() => DayStyle = ButtonStyle.CalendarDay;

    /// <summary>
    /// Gets and sets the content style for the day of week labels.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Content style for the day of week labels.")]
    [DefaultValue(ButtonStyle.CalendarDay)]
    public ButtonStyle DayOfWeekStyle
    {
        get => _dayOfWeekStyle;

        set
        {
            if (_dayOfWeekStyle != value)
            {
                _dayOfWeekStyle = value;
                StateCommon.DayOfWeekStyle = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DayOfWeekStyle)));
            }
        }
    }

    private bool ShouldSerializeDayOfWeekStyle() => _dayOfWeekStyle != ButtonStyle.CalendarDay;

    private void ResetDayOfWeekStyle() => DayOfWeekStyle = ButtonStyle.CalendarDay;

    /// <summary>
    /// Gets access to the day appearance when it has focus.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarStateRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets access to the day appearance when it is bolded.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar appearance when it is bolded.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarStateRedirect OverrideBolded { get; }

    private bool ShouldSerializeOverrideBolded() => !OverrideBolded.IsDefault;

    /// <summary>
    /// Gets access to the day appearance when it is todays.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar appearance when it is today.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarStateRedirect OverrideToday { get; }

    private bool ShouldSerializeOverrideToday() => !OverrideToday.IsDefault;

    /// <summary>
    /// Gets access to the common month calendar appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common month calendar appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the month calendar disabled appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarDoubleState StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the month calendar normal appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month calendar normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarDoubleState StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the tracking month calendar appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed month calendar appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the checked normal month calendar appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked normal month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    /// <summary>
    /// Gets access to the checked tracking month calendar appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked tracking month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    /// <summary>
    /// Gets access to the checked pressed month calendar appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked pressed month calendar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteMonthCalendarState StateCheckedPressed { get; }

    private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

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
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(AnnuallyBoldedDates)));
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
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(BoldedDates)));
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
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(MonthlyBoldedDates)));
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

        OnPropertyChanged(new PropertyChangedEventArgs(nameof(AnnuallyBoldedDates)));
    }

    /// <summary>
    /// Removes all the nonrecurring bold dates.
    /// </summary>
    public void RemoveAllBoldedDates()
    {
        BoldedDatesList.Clear();
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(BoldedDates)));
    }

    /// <summary>
    /// Removes all the monthly bold dates.
    /// </summary>
    public void RemoveAllMonthlyBoldedDates()
    {
        _monthlyDates.Clear();
        MonthlyBoldedDatesMask = 0;
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(MonthlyBoldedDates)));
    }

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

        TimeSpan span = end - start;
        if (span.Days >= _maxSelectionCount)
        {
            if (start.Ticks == _selectionStart.Ticks)
            {
                start = end.AddDays(1 - _maxSelectionCount);
            }
            else
            {
                end = start.AddDays(_maxSelectionCount - 1);
            }
        }

        SetSelRange(start, end);
    }

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
    /// Sets the use of the focus override.
    /// </summary>
    /// <param name="focus">New focus state.</param>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetFocusOverride(bool focus)
    {
        OverrideDisabled.Apply = HasFocus && focus;
        OverrideNormal.Apply = HasFocus && focus;
        OverrideTracking.Apply = HasFocus && focus;
        OverridePressed.Apply = HasFocus && focus;
        OverrideCheckedNormal.Apply = HasFocus && focus;
        OverrideCheckedTracking.Apply = HasFocus && focus;
        OverrideCheckedPressed.Apply = HasFocus && focus;
    }

    /// <summary>
    /// Gets and sets if the item has the focus.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasFocus { get; set; }

    /// <summary>
    /// Gets the focus day.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime? FocusDay { get; set; }

    #endregion

    #region Protected
    /// <summary>
    /// Raises when the DateChanged event.
    /// </summary>
    /// <param name="e">An DateRangeEventArgs that contains the event data.</param>
    protected virtual void OnDateChanged(DateRangeEventArgs e) => DateChanged?.Invoke(this, e);

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

    #endregion

    #region Internal
    internal void SetPaletteRedirect(PaletteRedirect redirector)
    {
        StateCommon.SetRedirector(redirector);
        OverrideFocus.SetRedirector(redirector);
        OverrideBolded.SetRedirector(redirector);
        OverrideToday.SetRedirector(redirector);
    }
    #endregion

    #region Implementation
    private void SetRange()
    {
        var startChanged = false;
        var endChanged = false;

        if (_selectionStart < _minDate)
        {
            _selectionStart = _minDate.Date;
            startChanged = true;
        }

        if (_selectionStart > _maxDate)
        {
            _selectionStart = _maxDate.Date;
            startChanged = true;
        }

        if (_selectionEnd < _minDate)
        {
            _selectionEnd = _minDate.Date;
            endChanged = true;
        }

        if (_selectionEnd > _maxDate)
        {
            _selectionEnd = _maxDate.Date;
            endChanged = true;
        }

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
    #endregion
}