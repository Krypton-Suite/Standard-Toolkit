#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a ribbon group date time picker.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupDateTimePicker), "ToolboxBitmaps.KryptonRibbonGroupDateTimePicker.bmp")]
[Designer(typeof(KryptonRibbonGroupDateTimePickerDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Value))]
[DefaultBindingProperty(nameof(Value))]
public class KryptonRibbonGroupDateTimePicker : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private string _keyTip;
    private GroupItemSize _itemSizeCurrent;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the control receives focus.
    /// </summary>
    [Browsable(false)]
    public event EventHandler? GotFocus;

    /// <summary>
    /// Occurs when the control loses focus.
    /// </summary>
    [Browsable(false)]
    public event EventHandler? LostFocus;

    /// <summary>
    /// Occurs when the Value property has changed value.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Event raised when the value of the Value property is changed.")]
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Occurs when the ValueNullable property has changed value.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Event raised when the value of the ValueNullable property is changed.")]
    public event EventHandler? ValueNullableChanged;

    /// <summary>
    /// Occurs when the drop-down is shown.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Event raised when the drop-down is shown.")]
    public event EventHandler<DateTimePickerDropArgs>? DropDown;

    /// <summary>
    /// Occurs when the drop-down has been closed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Event raised when the drop-down has been closed.")]
    public event EventHandler<DateTimePickerCloseArgs>? CloseUp;

    /// <summary>
    /// Occurs when the Checked property has changed value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Event raised when the value of the Checked property is changed.")]
    public event EventHandler? CheckedChanged;

    /// <summary>
    /// Occurs when the Format property has changed value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Event raised when the value of the Format property is changed.")]
    public event EventHandler? FormatChanged;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus. 
    /// </summary>
    [Description(@"Occurs when a key is pressed while the control has focus.")]
    [Category(@"Key")]
    public event KeyPressEventHandler? KeyPress;

    /// <summary>
    /// Occurs when a key is released while the control has focus. 
    /// </summary>
    [Description(@"Occurs when a key is released while the control has focus.")]
    [Category(@"Key")]
    public event KeyEventHandler? KeyUp;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus.
    /// </summary>
    [Description(@"Occurs when a key is pressed while the control has focus.")]
    [Category(@"Key")]
    public event KeyEventHandler? KeyDown;

    /// <summary>
    /// Occurs before the KeyDown event when a key is pressed while focus is on this control.
    /// </summary>
    [Description(@"Occurs before the KeyDown event when a key is pressed while focus is on this control.")]
    [Category(@"Key")]
    public event PreviewKeyDownEventHandler? PreviewKeyDown;

    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;

    internal event EventHandler? MouseEnterControl;
    internal event EventHandler? MouseLeaveControl;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupDateTimePicker class.
    /// </summary>
    public KryptonRibbonGroupDateTimePicker()
    {
        // Default fields
        _visible = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        ShortcutKeys = Keys.None;
        _keyTip = "X";

        // Create the actual date time picker control and set initial settings
        DateTimePicker = new KryptonDateTimePicker
        {
            InputControlStyle = InputControlStyle.Ribbon,
            AlwaysActive = false,
            MinimumSize = new Size(180, 0),
            MaximumSize = new Size(180, 0),
            TabStop = false
        };

        // Hook into events to expose via this container
        DateTimePicker.ValueChanged += OnDateTimePickerValueChanged;
        DateTimePicker.ValueNullableChanged += OnDateTimePickerValueNullableChanged;
        DateTimePicker.DropDown += OnDateTimePickerDropDown;
        DateTimePicker.CloseUp += OnDateTimePickerCloseUp;
        DateTimePicker.CheckedChanged += OnDateTimePickerCheckedChanged;
        DateTimePicker.FormatChanged += OnDateTimePickerFormatChanged;
        DateTimePicker.GotFocus += OnDateTimePickerGotFocus;
        DateTimePicker.LostFocus += OnDateTimePickerLostFocus;
        DateTimePicker.KeyDown += OnDateTimePickerKeyDown;
        DateTimePicker.KeyUp += OnDateTimePickerKeyUp;
        DateTimePicker.KeyPress += OnDateTimePickerKeyPress;
        DateTimePicker.PreviewKeyDown += OnDateTimePickerKeyDown;

        // Ensure we can track mouse events on the date time picker
        MonitorControl(DateTimePicker);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (DateTimePicker != null!)
            {
                UnmonitorControl(DateTimePicker);
                DateTimePicker.Dispose();
                DateTimePicker = null!;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the owning ribbon control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override KryptonRibbon? Ribbon
    {
        set
        {
            base.Ribbon = value;

            if (value is not null)
            {
                // Use the same palette in the date time picker as the ribbon, plus we need
                // to know when the ribbon palette changes, so we can reflect that change
                DateTimePicker.PaletteMode = Ribbon!.PaletteMode;
                DateTimePicker.LocalCustomPalette = Ribbon.LocalCustomPalette;
                Ribbon.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to set focus to the date time picker.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Access to the actual embedded KryptonDateTimePicker instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonDateTimePicker instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonDateTimePicker DateTimePicker { get; private set; }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group date time picker.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group date time picker key tip.")]
    [DefaultValue("X")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"X";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the date time picker.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the date time picker is visible or hidden.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool Visible
    {
        get => _visible;

        set
        {
            if (value != _visible)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Make the ribbon group date time picker visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group date time picker hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group date time picker.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group date time picker is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => DateTimePicker.Enabled;
        set => DateTimePicker.Enabled = value;
    }

    /// <summary>
    /// Gets or sets the minimum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the minimum size of the control.")]
    [DefaultValue(typeof(Size), "180, 0")]
    public Size MinimumSize
    {
        get => DateTimePicker.MinimumSize;
        set => DateTimePicker.MinimumSize = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum size of the control.")]
    [DefaultValue(typeof(Size), "180, 0")]
    public Size MaximumSize
    {
        get => DateTimePicker.MaximumSize;
        set => DateTimePicker.MaximumSize = value;
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => DateTimePicker.ContextMenuStrip;
        set => DateTimePicker.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the date time picker is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the date time picker is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => DateTimePicker.KryptonContextMenu;
        set => DateTimePicker.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => DateTimePicker.ToolTipValues;

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => DateTimePicker.AllowButtonSpecToolTips;
        set => DateTimePicker.AllowButtonSpecToolTips = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority
    {
        get => DateTimePicker.AllowButtonSpecToolTipPriority;
        set => DateTimePicker.AllowButtonSpecToolTipPriority = value;
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonDateTimePicker.DateTimePickerButtonSpecCollection ButtonSpecs => DateTimePicker.ButtonSpecs;

    /// <summary>
    /// Gets or sets the number of columns and rows of months Displayed. 
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Specifies the number of rows and columns of months Displayed.")]
    [DefaultValue(typeof(Size), "1,1")]
    [Localizable(true)]
    public Size CalendarDimensions
    {
        get => DateTimePicker.CalendarDimensions;
        set => DateTimePicker.CalendarDimensions = value;
    }

    /// <summary>
    /// Gets or sets the label text for todays text. 
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Text used as label for todays date.")]
    [DefaultValue("Today:")]
    [Localizable(true)]
    public string CalendarTodayText
    {
        get => DateTimePicker.CalendarTodayText;
        set => DateTimePicker.CalendarTodayText = value;
    }

    private void ResetCalendarTodayText() => DateTimePicker.ResetCalendarTodayText();

    /// <summary>
    /// First day of the week.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"First day of the week.")]
    [DefaultValue(typeof(Day), "Default")]
    [Localizable(true)]
    public Day CalendarFirstDayOfWeek
    {
        get => DateTimePicker.CalendarFirstDayOfWeek;
        set => DateTimePicker.CalendarFirstDayOfWeek = value;
    }

    /// <summary>
    /// Gets and sets if clicking the Today button closes the drop-down menu.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates if clicking the Today button closes the drop-down menu.")]
    [DefaultValue(false)]
    public bool CalendarCloseOnTodayClick
    {
        get => DateTimePicker.CalendarCloseOnTodayClick;
        set => DateTimePicker.CalendarCloseOnTodayClick = value;
    }

    /// <summary>
    /// Gets and sets if the control will display todays date.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates whether this month calendar will display todays date.")]
    [DefaultValue(true)]
    public bool CalendarShowToday
    {
        get => DateTimePicker.CalendarShowToday;
        set => DateTimePicker.CalendarShowToday = value;
    }

    /// <summary>
    /// Gets and sets if the control will circle the today date.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates whether this month calendar will circle the today date.")]
    [DefaultValue(true)]
    public bool CalendarShowTodayCircle
    {
        get => DateTimePicker.CalendarShowTodayCircle;
        set => DateTimePicker.CalendarShowTodayCircle = value;
    }

    /// <summary>
    /// Gets and sets if week numbers to the left of each row.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates whether this month calendar will display week numbers to the left of each row.")]
    [DefaultValue(false)]
    public bool CalendarShowWeekNumbers
    {
        get => DateTimePicker.CalendarShowWeekNumbers;
        set => DateTimePicker.CalendarShowWeekNumbers = value;
    }

    /// <summary>
    /// Gets or sets today's date.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Today's date.")]
    [AllowNull]
    public DateTime CalendarTodayDate
    {
        get => DateTimePicker.CalendarTodayDate;
        set => DateTimePicker.CalendarTodayDate = value;
    }

    private void ResetCalendarTodayDate() => CalendarTodayDate = DateTime.Now.Date;

    private bool ShouldSerializeCalendarTodayDate() => CalendarTodayDate != DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which annual days are Displayed in bold.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates which annual dates should be boldface.")]
    [Localizable(true)]
    public DateTime[]? CalendarAnnuallyBoldedDates
    {
        get => DateTimePicker.CalendarAnnuallyBoldedDates;
        set => DateTimePicker.CalendarAnnuallyBoldedDates = value;
    }

    private void ResetCalendarAnnuallyBoldedDates() => CalendarAnnuallyBoldedDates = null;

    private bool ShouldSerializeCalendarAnnuallyBoldedDates() => DateTimePicker.ShouldSerializeCalendarAnnuallyBoldedDates();

    /// <summary>
    /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates which monthly dates should be boldface.")]
    [Localizable(true)]
    public DateTime[]? CalendarMonthlyBoldedDates
    {
        get => DateTimePicker.CalendarMonthlyBoldedDates;
        set => DateTimePicker.CalendarMonthlyBoldedDates = value;
    }

    private void ResetCalendarMonthlyBoldedDates() => CalendarMonthlyBoldedDates = null;

    private bool ShouldSerializeCalendarMonthlyBoldedDates() => DateTimePicker.ShouldSerializeCalendarMonthlyBoldedDates();

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are Displayed in bold.
    /// </summary>
    [Category(@"MonthCalendar")]
    [Description(@"Indicates which dates should be boldface.")]
    [Localizable(true)]
    public DateTime[]? CalendarBoldedDates
    {
        get => DateTimePicker.CalendarBoldedDates;
        set => DateTimePicker.CalendarBoldedDates = value;
    }

    private void ResetCalendarBoldedDates() => CalendarBoldedDates = null;

    private bool ShouldSerializeCalendarBoldedDates() => DateTimePicker.ShouldSerializeCalendarBoldedDates();

    /// <summary>
    /// Gets or sets the alignment of the drop-down calendar on the DateTimePicker control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Alignment of the drop-down calendar on the KryptonDateTimePicker control.")]
    [DefaultValue(typeof(LeftRightAlignment), "Left")]
    [Localizable(true)]
    public LeftRightAlignment DropDownAlign
    {
        get => DateTimePicker.DropDownAlign;
        set => DateTimePicker.DropDownAlign = value;
    }

    /// <summary>
    /// Gets or sets the date/time value assigned to the control that can be null.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Property for the date/time that can be null.")]
    [TypeConverter(typeof(DateTimeNullableConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [Bindable(true)]
    public object? ValueNullable
    {
        get => DateTimePicker.ValueNullable;
        set => DateTimePicker.ValueNullable = value;
    }
    private void ResetValueNullable() => DateTimePicker.ResetValueNullable();
    private bool ShouldSerializeValueNullable() => DateTimePicker.ShouldSerializeValueNullable();

    /// <summary>
    ///  Sets date as the current selected date.
    /// </summary>
    public void SetDate(DateTime date) => DateTimePicker.SetDate( date);

    /// <summary>
    /// Gets or sets the date/time value assigned to the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Property for the date/time.")]
    [RefreshProperties(RefreshProperties.All)]
    [Bindable(true)]
    public DateTime Value
    {
        get => DateTimePicker.Value;
        set => DateTimePicker.Value = value;
    }
    private void ResetValue() => DateTimePicker.ResetValue();
    private bool ShouldSerializeValue() => DateTimePicker.ShouldSerializeValue();

    /// <summary>
    /// Gets or sets the format of the date and time Displayed in the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines whether dates and times are Displayed using standard or custom formatting.")]
    [DefaultValue(typeof(DateTimePickerFormat), "Long")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public DateTimePickerFormat Format
    {
        get => DateTimePicker.Format;
        set => DateTimePicker.Format = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a spin button control (also known as an up-down control) is used to adjust the date/time value.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether a spin box rather than a drop-down calendar is Displayed for modifying the control value.")]
    [DefaultValue(false)]
    public bool ShowUpDown
    {
        get => DateTimePicker.ShowUpDown;
        set => DateTimePicker.ShowUpDown = value;
    }

    /// <summary>
    /// Specifies whether to show the check box in the exception message box.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines whether a check box is Displayed in the control. When the box is unchecked, no value is selected.")]
    [DefaultValue(false)]
    public bool ShowCheckBox
    {
        get => DateTimePicker.ShowCheckBox;
        set => DateTimePicker.ShowCheckBox = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Defines if mnemonic characters generate click events for button specs.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => DateTimePicker.UseMnemonic;
        set => DateTimePicker.UseMnemonic = value;
    }

    /// <summary>
    /// Gets or sets the maximum date and time that can be selected in the control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum allowable date.")]
    public DateTime MaxDate
    {
        get => DateTimePicker.MaxDate;
        set => DateTimePicker.MaxDate = value;
    }

    private void ResetMaxDate() => MaxDate = DateTime.MaxValue;

    private bool ShouldSerializeMaxDate() => DateTimePicker.ShouldSerializeMaxDate();

    /// <summary>
    /// Gets or sets the minimum date and time that can be selected in the control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Minimum allowable date.")]
    public DateTime MinDate
    {
        get => DateTimePicker.MinDate;
        set => DateTimePicker.MinDate = value;
    }

    private void ResetMinDate() => MinDate = DateTime.MinValue;

    private bool ShouldSerializeMinDate() => DateTimePicker.ShouldSerializeMinDate();

    /// <summary>
    /// Gets or sets a value indicating if the check box is checked and if the ValueNullable is DBNull or a DateTime value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if the check box is checked and if the ValueNullable is DBNull or a DateTime value.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    [Bindable(true)]
    public bool Checked
    {
        get => DateTimePicker.Checked;
        set => DateTimePicker.Checked = value;
    }

    /// <summary>
    /// Gets or sets the custom date/time format string.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The custom format string used to format the date and/or time Displayed in the control.")]
    [DefaultValue("")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    public string? CustomFormat
    {
        get => DateTimePicker.CustomFormat;
        set => DateTimePicker.CustomFormat = value;
    }

    /// <summary>
    /// Gets or sets the custom text to show when control is not checked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The custom text to draw when the control is not checked. Provide an empty string for default action of showing the defined date.")]
    [DefaultValue("")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    public string CustomNullText
    {
        get => DateTimePicker.CustomNullText;
        set => DateTimePicker.CustomNullText = value;
    }

    /// <summary>
    /// Gets or sets the today date format string.
    /// </summary>
    [Category(@"Visuals - MonthCalendar")]
    [Description(@"The today format string used to format the date Displayed in the today button.")]
    [DefaultValue("d")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    public string CalendarTodayFormat
    {
        get => DateTimePicker.CalendarTodayFormat;
        set => DateTimePicker.CalendarTodayFormat = value;
    }

    /// <summary>
    /// Gets and sets the header style for the month calendar.
    /// </summary>
    [Category(@"Visuals - MonthCalendar")]
    [Description(@"Header style for the month calendar.")]
    public HeaderStyle CalendarHeaderStyle
    {
        get => DateTimePicker.CalendarHeaderStyle;
        set => DateTimePicker.CalendarHeaderStyle = value;
    }

    private void ResetCalendarHeaderStyle() => CalendarHeaderStyle = HeaderStyle.Calendar;

    private bool ShouldSerializeCalendarHeaderStyle() => CalendarHeaderStyle != HeaderStyle.Calendar;

    /// <summary>
    /// Gets and sets the content style for the day entries.
    /// </summary>
    [Category(@"Visuals - MonthCalendar")]
    [Description(@"Content style for the day entries.")]
    public ButtonStyle CalendarDayStyle
    {
        get => DateTimePicker.CalendarDayStyle;
        set => DateTimePicker.CalendarDayStyle = value;
    }

    private void ResetCalendarDayStyle() => CalendarDayStyle = ButtonStyle.CalendarDay;

    private bool ShouldSerializeCalendarDayStyle() => CalendarDayStyle != ButtonStyle.CalendarDay;

    /// <summary>
    /// Gets and sets the content style for the day of week labels.
    /// </summary>
    [Category(@"Visuals - MonthCalendar")]
    [Description(@"Content style for the day of week labels.")]
    public ButtonStyle CalendarDayOfWeekStyle
    {
        get => DateTimePicker.CalendarDayOfWeekStyle;
        set => DateTimePicker.CalendarDayOfWeekStyle = value;
    }

    private void ResetCalendarDayOfWeekStyle() => CalendarDayOfWeekStyle = ButtonStyle.CalendarDay;

    private bool ShouldSerializeCalendarDayOfWeekStyle() => CalendarDayOfWeekStyle != ButtonStyle.CalendarDay;

    /// <summary>
    /// Gets and sets the maximum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMaximum
    {
        get => GroupItemSize.Large;
        set { }
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMinimum
    {
        get => GroupItemSize.Small;
        set { }
    }

    /// <summary>
    /// Gets and sets the current item size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeCurrent
    {
        get => _itemSizeCurrent;

        set
        {
            if (_itemSizeCurrent != value)
            {
                _itemSizeCurrent = value;
                OnPropertyChanged(nameof(ItemSizeCurrent));
            }
        }
    }

    /// <summary>
    /// Creates an appropriate view element for this item.
    /// </summary>
    /// <param name="ribbon">Reference to the owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    /// <returns>ViewBase derived instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ViewBase CreateView(KryptonRibbon ribbon, NeedPaintHandler needPaint) => new ViewDrawRibbonGroupDateTimePicker(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject DateTimePickerDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase DateTimePickerView { get; set; }

    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnGotFocus(EventArgs e) => GotFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLostFocus(EventArgs e) => LostFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the FormatChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFormatChanged(EventArgs e) => FormatChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the CheckedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the CloseUp event.
    /// </summary>
    /// <param name="e">An DateTimePickerCloseArgs containing the event data.</param>
    protected virtual void OnCloseUp(DateTimePickerCloseArgs e) => CloseUp?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDown event.
    /// </summary>
    /// <param name="e">An DateTimePickerDropArgs containing the event data.</param>
    protected virtual void OnDropDown(DateTimePickerDropArgs e) => DropDown?.Invoke(this, e);

    /// <summary>
    /// Raises the ValueNullableChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueNullableChanged(EventArgs e) => ValueNullableChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyDown event.
    /// </summary>
    /// <param name="e">An KeyEventArgs containing the event data.</param>
    protected virtual void OnKeyDown(KeyEventArgs e) => KeyDown?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyUp event.
    /// </summary>
    /// <param name="e">An KeyEventArgs containing the event data.</param>
    protected virtual void OnKeyUp(KeyEventArgs e) => KeyUp?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyPress event.
    /// </summary>
    /// <param name="e">An KeyPressEventArgs containing the event data.</param>
    protected virtual void OnKeyPress(KeyPressEventArgs e) => KeyPress?.Invoke(this, e);

    /// <summary>
    /// Raises the PreviewKeyDown event.
    /// </summary>
    /// <param name="e">An PreviewKeyDownEventArgs containing the event data.</param>
    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e) => PreviewKeyDown?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Control LastParentControl { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonDateTimePicker? LastDateTimePicker { get; set; }

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    internal NeedPaintHandler? ViewPaintDelegate { get; set; }

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Only interested in key processing if this control definition 
        // is enabled and itself and all parents are also visible
        if (Enabled && ChainVisible)
        {
            // Do we have a shortcut definition for ourself?
            if (ShortcutKeys != Keys.None)
            {
                // Does it match the incoming key combination?
                if (ShortcutKeys == keyData)
                {
                    // Can the date time picker take the focus
                    if (LastDateTimePicker is { CanFocus: true })
                    {
                        LastDateTimePicker.Focus();
                    }

                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Implementation
    private void MonitorControl(KryptonDateTimePicker c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonDateTimePicker c)
    {
        c.MouseEnter -= OnControlEnter;
        c.MouseLeave -= OnControlLeave;
    }

    private void OnControlEnter(object? sender, EventArgs e) => MouseEnterControl?.Invoke(this, e);

    private void OnControlLeave(object? sender, EventArgs e) => MouseLeaveControl?.Invoke(this, e);

    private void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e) =>
        // Pass request onto the view provided paint delegate
        ViewPaintDelegate?.Invoke(this, e);

    private void OnDateTimePickerGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnDateTimePickerLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnDateTimePickerFormatChanged(object? sender, EventArgs e) => OnFormatChanged(e);

    private void OnDateTimePickerCheckedChanged(object? sender, EventArgs e) => OnCheckedChanged(e);

    private void OnDateTimePickerCloseUp(object? sender, DateTimePickerCloseArgs e) => OnCloseUp(e);

    private void OnDateTimePickerDropDown(object? sender, DateTimePickerDropArgs e) => OnDropDown(e);

    private void OnDateTimePickerValueNullableChanged(object? sender, EventArgs e) => OnValueNullableChanged(e);

    private void OnDateTimePickerValueChanged(object? sender, EventArgs e) => OnValueChanged(e);

    private void OnDateTimePickerKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnDateTimePickerKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnDateTimePickerKeyDown(object?  sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnDateTimePickerKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        DateTimePicker.PaletteMode = Ribbon!.PaletteMode;
        DateTimePicker.LocalCustomPalette = Ribbon.LocalCustomPalette;
    }

    #endregion
}