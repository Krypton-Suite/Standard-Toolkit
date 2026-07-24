#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Expandable configuration for the <see cref="KryptonDateTimePickerToolStripItem"/> tool strip host. Mirrors the
/// settable value/behavior settings of the hosted <see cref="KryptonDateTimePicker"/>
/// (<see cref="KryptonDateTimePickerToolStripItem.DateTimePickerControl"/>). Pure infrastructure members
/// (<c>ButtonSpecs</c>, <c>ToolTipManager</c>, <c>Images</c>, and the read-only state flags) remain directly on the host.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class DateTimePickerHostValues : Storage
{
    #region Instance Fields

    private readonly KryptonDateTimePickerToolStripItem _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="DateTimePickerHostValues"/> class.
    /// </summary>
    /// <param name="owner">Owning date/time picker host.</param>
    public DateTimePickerHostValues(KryptonDateTimePickerToolStripItem owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault
    {
        get
        {
            KryptonDateTimePicker? control = _owner.DateTimePickerControl;

            if (control is null)
            {
                return true;
            }

            return control.AlwaysActive
                   && !control.AllowButtonSpecToolTips
                   && !control.AllowButtonSpecToolTipPriority
                   && !control.AutoShift
                   && control.Checked
                   && !control.ShowUpDown
                   && !control.ShowCheckBox
                   && control.UseMnemonic
                   && control.CalendarShowTodayCircle
                   && !control.CalendarShowWeekNumbers
                   && !control.CalendarCloseOnTodayClick
                   && control.CalendarShowToday
                   && !control.RightToLeftLayout
                   && control.CalendarAnnuallyBoldedDates is null
                   && control.CalendarMonthlyBoldedDates is null
                   && control.CalendarBoldedDates is null
                   && control.MaxDate == DateTime.MaxValue
                   && control.MinDate == DateTime.MinValue
                   && control.Format == DateTimePickerFormat.Long
                   && control.CalendarFirstDayOfWeek == Day.Default
                   && control.DropDownAlign == LeftRightAlignment.Left
                   && control.InputControlStyle == InputControlStyle.Standalone
                   && control.ValueNullable is null
                   && control.CalendarTodayText == @"Today:"
                   && string.IsNullOrEmpty(control.CustomFormat)
                   && string.IsNullOrEmpty(control.CustomNullText)
                   && control.CalendarTodayFormat == @"d"
                   && string.IsNullOrEmpty(control.ActiveFragment)
                   && control.CalendarHeaderStyle == HeaderStyle.Calendar
                   && control.CalendarDayStyle == ButtonStyle.CalendarDay
                   && control.CalendarDayOfWeekStyle == ButtonStyle.CalendarDay
                   && control.UpDownButtonStyle == ButtonStyle.InputControl
                   && control.DropButtonStyle == ButtonStyle.InputControl;
        }
    }

    #endregion

    #region Public

    /// <summary>Gets or sets whether the control is always shown in the active (focused) state.</summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    public bool AlwaysActive
    {
        get => _owner.DateTimePickerControl!.AlwaysActive;
        set => _owner.DateTimePickerControl!.AlwaysActive = value;
    }

    /// <summary>Gets or sets whether button spec tool tips are shown.</summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => _owner.DateTimePickerControl!.AllowButtonSpecToolTips;
        set => _owner.DateTimePickerControl!.AllowButtonSpecToolTips = value;
    }

    /// <summary>Gets or sets whether button spec tool tips take priority.</summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPrioity
    {
        get => _owner.DateTimePickerControl!.AllowButtonSpecToolTipPriority;
        set => _owner.DateTimePickerControl!.AllowButtonSpecToolTipPriority = value;
    }

    /// <summary>Gets or sets whether focus automatically shifts between date fragments.</summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    public bool AutoShift
    {
        get => _owner.DateTimePickerControl!.AutoShift;
        set => _owner.DateTimePickerControl!.AutoShift = value;
    }

    /// <summary>Gets or sets whether the control's checkbox is checked.</summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    public bool Checked
    {
        get => _owner.DateTimePickerControl!.Checked;
        set => _owner.DateTimePickerControl!.Checked = value;
    }

    /// <summary>Gets or sets whether an up-down control is shown instead of a drop-down calendar.</summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool ShowUpDown
    {
        get => _owner.DateTimePickerControl!.ShowUpDown;
        set => _owner.DateTimePickerControl!.ShowUpDown = value;
    }

    /// <summary>Gets or sets whether a check box is shown to the left of the date/time value.</summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool ShowCheckBox
    {
        get => _owner.DateTimePickerControl!.ShowCheckBox;
        set => _owner.DateTimePickerControl!.ShowCheckBox = value;
    }

    /// <summary>Gets or sets whether mnemonic characters are used.</summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _owner.DateTimePickerControl!.UseMnemonic;
        set => _owner.DateTimePickerControl!.UseMnemonic = value;
    }

    /// <summary>Gets or sets whether a circle is drawn around today's date in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(true)]
    public bool CalendarShowTodayCircle
    {
        get => _owner.DateTimePickerControl!.CalendarShowTodayCircle;
        set => _owner.DateTimePickerControl!.CalendarShowTodayCircle = value;
    }

    /// <summary>Gets or sets whether week numbers are shown in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(false)]
    public bool CalendarShowWeekNumbers
    {
        get => _owner.DateTimePickerControl!.CalendarShowWeekNumbers;
        set => _owner.DateTimePickerControl!.CalendarShowWeekNumbers = value;
    }

    /// <summary>Gets or sets whether the calendar closes when the "Today" link is clicked.</summary>
    [Category(@"Calendar")]
    [DefaultValue(false)]
    public bool CalendarCloseOnTodayClick
    {
        get => _owner.DateTimePickerControl!.CalendarCloseOnTodayClick;
        set => _owner.DateTimePickerControl!.CalendarCloseOnTodayClick = value;
    }

    /// <summary>Gets or sets whether the "Today" link is shown in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(true)]
    public bool CalendarShowToday
    {
        get => _owner.DateTimePickerControl!.CalendarShowToday;
        set => _owner.DateTimePickerControl!.CalendarShowToday = value;
    }

    /// <summary>Gets or sets whether the calendar layout is right-to-left.</summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool RightToLeftLayout
    {
        get => _owner.DateTimePickerControl!.RightToLeftLayout;
        set => _owner.DateTimePickerControl!.RightToLeftLayout = value;
    }

    /// <summary>Sets whether the control's adornments (buttons/checkbox) are shown.</summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    public bool ShowAdornments
    {
        set => _owner.DateTimePickerControl!.ShowAdornments = value;
    }

    /// <summary>Sets whether the control's border is shown.</summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    public bool ShowBorder
    {
        set => _owner.DateTimePickerControl!.ShowBorder = value;
    }

    /// <summary>Gets or sets the date used as "today" in the calendar.</summary>
    [Category(@"Calendar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DateTime? CalendarTodayDate
    {
        get => _owner.DateTimePickerControl!.CalendarTodayDate;
        set => _owner.DateTimePickerControl!.CalendarTodayDate = (DateTime)value!;
    }

    private bool ShouldSerializeCalendarTodayDate() => CalendarTodayDate != DateTime.Now.Date;

    /// <summary>Gets or sets the dates that are bolded every year in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(null)]
    public DateTime[]? CalendarAnnuallyBoldedDates
    {
        get => _owner.DateTimePickerControl!.CalendarAnnuallyBoldedDates;
        set => _owner.DateTimePickerControl!.CalendarAnnuallyBoldedDates = value;
    }

    /// <summary>Gets or sets the dates that are bolded every month in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(null)]
    public DateTime[]? CalendarMonthlyBoldedDates
    {
        get => _owner.DateTimePickerControl!.CalendarMonthlyBoldedDates;
        set => _owner.DateTimePickerControl!.CalendarMonthlyBoldedDates = value;
    }

    /// <summary>Gets or sets the specific dates that are bolded in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(null)]
    public DateTime[]? CalendarBoldedDates
    {
        get => _owner.DateTimePickerControl!.CalendarBoldedDates;
        set => _owner.DateTimePickerControl!.CalendarBoldedDates = value;
    }

    /// <summary>Gets or sets the selected date/time value.</summary>
    [Category(@"Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DateTime Value
    {
        get => _owner.DateTimePickerControl!.Value;
        set => _owner.DateTimePickerControl!.Value = value;
    }

    private bool ShouldSerializeValue() => false;

    /// <summary>Gets or sets the maximum selectable date.</summary>
    [Category(@"Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DateTime MaxDate
    {
        get => _owner.DateTimePickerControl!.MaxDate;
        set => _owner.DateTimePickerControl!.MaxDate = value;
    }

    private bool ShouldSerializeMaxDate() => MaxDate != DateTime.MaxValue;

    /// <summary>Gets or sets the minimum selectable date.</summary>
    [Category(@"Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DateTime MinDate
    {
        get => _owner.DateTimePickerControl!.MinDate;
        set => _owner.DateTimePickerControl!.MinDate = value;
    }

    private bool ShouldSerializeMinDate() => MinDate != DateTime.MinValue;

    /// <summary>Gets or sets the format used to display the date/time value.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DateTimePickerFormat.Long)]
    public DateTimePickerFormat Format
    {
        get => _owner.DateTimePickerControl!.Format;
        set => _owner.DateTimePickerControl!.Format = value;
    }

    /// <summary>Gets or sets the first day of the week shown in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(Day.Default)]
    public Day CalendarFirstDayOfWeek
    {
        get => _owner.DateTimePickerControl!.CalendarFirstDayOfWeek;
        set => _owner.DateTimePickerControl!.CalendarFirstDayOfWeek = value;
    }

    /// <summary>Gets or sets the alignment of the drop-down calendar.</summary>
    [Category(@"Appearance")]
    [DefaultValue(LeftRightAlignment.Left)]
    public LeftRightAlignment DropDownAlign
    {
        get => _owner.DateTimePickerControl!.DropDownAlign;
        set => _owner.DateTimePickerControl!.DropDownAlign = value;
    }

    /// <summary>Gets or sets the input control style.</summary>
    [Category(@"Appearance")]
    [DefaultValue(InputControlStyle.Standalone)]
    public InputControlStyle InputControlStyle
    {
        get => _owner.DateTimePickerControl!.InputControlStyle;
        set => _owner.DateTimePickerControl!.InputControlStyle = value;
    }

    /// <summary>Gets or sets the nullable value of the control.</summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    public object? ValueNullable
    {
        get => _owner.DateTimePickerControl!.ValueNullable;
        set => _owner.DateTimePickerControl!.ValueNullable = value;
    }

    /// <summary>Gets or sets the text shown for the "Today" link in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(@"Today:")]
    public string CalendarTodayText
    {
        get => _owner.DateTimePickerControl!.CalendarTodayText;
        set => _owner.DateTimePickerControl!.CalendarTodayText = value;
    }

    /// <summary>Gets or sets the custom format string used when <see cref="Format"/> is <see cref="DateTimePickerFormat.Custom"/>.</summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    public string? CustomFormat
    {
        get => _owner.DateTimePickerControl!.CustomFormat;
        set => _owner.DateTimePickerControl!.CustomFormat = value;
    }

    /// <summary>Gets or sets the text shown when the value is null.</summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    public string CustomNullText
    {
        get => _owner.DateTimePickerControl!.CustomNullText;
        set => _owner.DateTimePickerControl!.CustomNullText = value;
    }

    /// <summary>Gets or sets the format string used for today's date in the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(@"d")]
    public string CalendarTodayFormat
    {
        get => _owner.DateTimePickerControl!.CalendarTodayFormat;
        set => _owner.DateTimePickerControl!.CalendarTodayFormat = value;
    }

    /// <summary>Gets or sets the currently active date fragment.</summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    public string ActiveFragment
    {
        get => _owner.DateTimePickerControl!.ActiveFragment;
        set => _owner.DateTimePickerControl!.ActiveFragment = value;
    }

    /// <summary>Gets or sets the header style of the calendar.</summary>
    [Category(@"Calendar")]
    [DefaultValue(HeaderStyle.Calendar)]
    public HeaderStyle CalendarHeaderStyle
    {
        get => _owner.DateTimePickerControl!.CalendarHeaderStyle;
        set => _owner.DateTimePickerControl!.CalendarHeaderStyle = value;
    }

    /// <summary>Gets or sets the button style used for calendar day buttons.</summary>
    [Category(@"Calendar")]
    [DefaultValue(ButtonStyle.CalendarDay)]
    public ButtonStyle CalendarDayStyle
    {
        get => _owner.DateTimePickerControl!.CalendarDayStyle;
        set => _owner.DateTimePickerControl!.CalendarDayStyle = value;
    }

    /// <summary>Gets or sets the button style used for calendar day-of-week headers.</summary>
    [Category(@"Calendar")]
    [DefaultValue(ButtonStyle.CalendarDay)]
    public ButtonStyle CalendarDayOfWeekStyle
    {
        get => _owner.DateTimePickerControl!.CalendarDayOfWeekStyle;
        set => _owner.DateTimePickerControl!.CalendarDayOfWeekStyle = value;
    }

    /// <summary>Gets or sets the button style used for the up-down buttons.</summary>
    [Category(@"Appearance")]
    [DefaultValue(ButtonStyle.InputControl)]
    public ButtonStyle UpDownButtonStyle
    {
        get => _owner.DateTimePickerControl!.UpDownButtonStyle;
        set => _owner.DateTimePickerControl!.UpDownButtonStyle = value;
    }

    /// <summary>Gets or sets the button style used for the drop-down button.</summary>
    [Category(@"Appearance")]
    [DefaultValue(ButtonStyle.InputControl)]
    public ButtonStyle DropButtonStyle
    {
        get => _owner.DateTimePickerControl!.DropButtonStyle;
        set => _owner.DateTimePickerControl!.DropButtonStyle = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        if (_owner.DateTimePickerControl is not { } control)
        {
            return;
        }

        control.AlwaysActive = true;
        control.AllowButtonSpecToolTips = false;
        control.AllowButtonSpecToolTipPriority = false;
        control.AutoShift = false;
        control.Checked = true;
        control.ShowUpDown = false;
        control.ShowCheckBox = false;
        control.UseMnemonic = true;
        control.CalendarShowTodayCircle = true;
        control.CalendarShowWeekNumbers = false;
        control.CalendarCloseOnTodayClick = false;
        control.CalendarShowToday = true;
        control.RightToLeftLayout = false;
        control.CalendarAnnuallyBoldedDates = null;
        control.CalendarMonthlyBoldedDates = null;
        control.CalendarBoldedDates = null;
        control.MaxDate = DateTime.MaxValue;
        control.MinDate = DateTime.MinValue;
        control.Format = DateTimePickerFormat.Long;
        control.CalendarFirstDayOfWeek = Day.Default;
        control.DropDownAlign = LeftRightAlignment.Left;
        control.InputControlStyle = InputControlStyle.Standalone;
        control.ValueNullable = null;
        control.CalendarTodayText = @"Today:";
        control.CustomFormat = string.Empty;
        control.CustomNullText = string.Empty;
        control.CalendarTodayFormat = @"d";
        control.ActiveFragment = string.Empty;
        control.CalendarHeaderStyle = HeaderStyle.Calendar;
        control.CalendarDayStyle = ButtonStyle.CalendarDay;
        control.CalendarDayOfWeekStyle = ButtonStyle.CalendarDay;
        control.UpDownButtonStyle = ButtonStyle.InputControl;
        control.DropButtonStyle = ButtonStyle.InputControl;
    }

    #endregion
}
