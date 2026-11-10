#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public class KryptonDateTimePickerToolStripItem : KryptonToolStripControlHostFixed
{
    #region Instance Fields

    private readonly DateTimePickerHostValues _values;

    #endregion

    #region Public

    [RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonDateTimePicker? DateTimePickerControl => Control as KryptonDateTimePicker;

    /// <summary>
    /// Gets the expandable configuration values mirroring the hosted <see cref="DateTimePickerControl"/> settings.
    /// </summary>
    [Category("Behavior")]
    [Description("Value, range, format, calendar, and appearance settings for the hosted date/time picker.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DateTimePickerHostValues DateTimeValues => _values;

    private bool ShouldSerializeDateTimeValues() => !_values.IsDefault;

    private void ResetDateTimeValues() => _values.Reset();

    #endregion

    #region Exposed Properties

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AlwaysActive
    {
        get => _values.AlwaysActive;
        set => _values.AlwaysActive = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowButtonSpecToolTips
    {
        get => _values.AllowButtonSpecToolTips;
        set => _values.AllowButtonSpecToolTips = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowButtonSpecToolTipPrioity
    {
        get => _values.AllowButtonSpecToolTipPrioity;
        set => _values.AllowButtonSpecToolTipPrioity = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AutoShift
    {
        get => _values.AutoShift;
        set => _values.AutoShift = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Checked
    {
        get => _values.Checked;
        set => _values.Checked = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowUpDown
    {
        get => _values.ShowUpDown;
        set => _values.ShowUpDown = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowCheckBox
    {
        get => _values.ShowCheckBox;
        set => _values.ShowCheckBox = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseMnemonic
    {
        get => _values.UseMnemonic;
        set => _values.UseMnemonic = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CalendarShowTodayCircle
    {
        get => _values.CalendarShowTodayCircle;
        set => _values.CalendarShowTodayCircle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CalendarShowWeekNumbers
    {
        get => _values.CalendarShowWeekNumbers;
        set => _values.CalendarShowWeekNumbers = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CalendarCloseOnTodayClick
    {
        get => _values.CalendarCloseOnTodayClick;
        set => _values.CalendarCloseOnTodayClick = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CalendarShowToday
    {
        get => _values.CalendarShowToday;
        set => _values.CalendarShowToday = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool RightToLeftLayout
    {
        get => _values.RightToLeftLayout;
        set => _values.RightToLeftLayout = value;
    }

    public bool IsActive => ((KryptonDateTimePicker)Control).IsActive;

    public bool IsMouseOver => ((KryptonDateTimePicker)Control).IsMouseOver;

    public bool IsDropped => ((KryptonDateTimePicker)Control).IsDropped;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowAdornments
    {
        set => _values.ShowAdornments = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowBorder
    {
        set => _values.ShowBorder = value;
    }

    public CheckBoxImages Images => ((KryptonDateTimePicker)Control).Images;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime? CalendarTodayDate
    {
        get => _values.CalendarTodayDate;
        set => _values.CalendarTodayDate = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime[]? CalendarAnnuallyBoldedDates
    {
        get => _values.CalendarAnnuallyBoldedDates;
        set => _values.CalendarAnnuallyBoldedDates = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime[]? CalendarMonthlyBoldedDates
    {
        get => _values.CalendarMonthlyBoldedDates;
        set => _values.CalendarMonthlyBoldedDates = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime[]? CalendarBoldedDates
    {
        get => _values.CalendarBoldedDates;
        set => _values.CalendarBoldedDates = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime Value
    {
        get => _values.Value;
        set => _values.Value = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime MaxDate
    {
        get => _values.MaxDate;
        set => _values.MaxDate = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime MinDate
    {
        get => _values.MinDate;
        set => _values.MinDate = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTimePickerFormat Format
    {
        get => _values.Format;
        set => _values.Format = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Day CalendarFirstDayOfWeek
    {
        get => _values.CalendarFirstDayOfWeek;
        set => _values.CalendarFirstDayOfWeek = value;
    }

    public KryptonDateTimePicker.DateTimePickerButtonSpecCollection ButtonSpecs =>
        ((KryptonDateTimePicker)Control).ButtonSpecs;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LeftRightAlignment DropDownAlign
    {
        get => _values.DropDownAlign;
        set => _values.DropDownAlign = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public InputControlStyle InputControlStyle
    {
        get => _values.InputControlStyle;
        set => _values.InputControlStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? ValueNullable
    {
        get => _values.ValueNullable;
        set => _values.ValueNullable = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CalendarTodayText
    {
        get => _values.CalendarTodayText;
        set => _values.CalendarTodayText = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? CustomFormat
    {
        get => _values.CustomFormat;
        set => _values.CustomFormat = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CustomNullText
    {
        get => _values.CustomNullText;
        set => _values.CustomNullText = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CalendarTodayFormat
    {
        get => _values.CalendarTodayFormat;
        set => _values.CalendarTodayFormat = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ActiveFragment
    {
        get => _values.ActiveFragment;
        set => _values.ActiveFragment = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public HeaderStyle CalendarHeaderStyle
    {
        get => _values.CalendarHeaderStyle;
        set => _values.CalendarHeaderStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonStyle CalendarDayStyle
    {
        get => _values.CalendarDayStyle;
        set => _values.CalendarDayStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonStyle CalendarDayOfWeekStyle
    {
        get => _values.CalendarDayOfWeekStyle;
        set => _values.CalendarDayOfWeekStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonStyle UpDownButtonStyle
    {
        get => _values.UpDownButtonStyle;
        set => _values.UpDownButtonStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonStyle DropButtonStyle
    {
        get => _values.DropButtonStyle;
        set => _values.DropButtonStyle = value;
    }

    public ToolTipManager ToolTipManager => ((KryptonDateTimePicker)Control).ToolTipManager;

    #endregion

    #region Exposed Events

    public event EventHandler? ValueChanged;

    protected void OnValueChanged(object sender, EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }

    #endregion

    #region Identity

    public KryptonDateTimePickerToolStripItem() : base(new KryptonDateTimePicker())
    {
        _values = new DateTimePickerHostValues(this);

        AutoSize = false;
    }

    #endregion

    #region Overrides

    public override Size GetPreferredSize(Size constrainingSize) => DateTimePickerControl!.GetPreferredSize(constrainingSize);

    protected override void OnSubscribeControlEvents(Control? control)
    {
        base.OnSubscribeControlEvents(control);

        ((control as KryptonDateTimePicker)!).ValueChanged += OnValueChanged!;
    }

    protected override void OnUnsubscribeControlEvents(Control? control)
    {
        base.OnUnsubscribeControlEvents(control);

        ((control as KryptonDateTimePicker)!).ValueChanged -= OnValueChanged!;
    }

    #endregion
}
