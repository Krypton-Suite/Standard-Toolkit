#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Radial menu item that edits a date in an editor ring when activated.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(SelectedDate))]
[DefaultEvent(nameof(SelectedDateChanged))]
public class KryptonRadialMenuCalendarItem : KryptonRadialMenuItemBase
{
    #region Instance Fields

    private string _text;
    private DateTime _selectedDate;
    private DateTime _viewMonth;
    private int _scrollOffset;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when <see cref="SelectedDate"/> changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the SelectedDate property changes.")]
    public event EventHandler? SelectedDateChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuCalendarItem"/> class.
    /// </summary>
    public KryptonRadialMenuCalendarItem()
    {
        _text = @"Date";
        _selectedDate = DateTime.Today;
        _viewMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        _scrollOffset = 0;
    }

    /// <inheritdoc />
    public override string? ToString() => string.IsNullOrEmpty(Text) ? "(Radial Calendar)" : Text;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the sector label.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text displayed on the calendar sector.")]
    [DefaultValue(@"Date")]
    [Localizable(true)]
    public string? Text
    {
        get => _text;
        set
        {
            value ??= string.Empty;
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected date.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Currently selected date.")]
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            var day = value.Date;
            if (_selectedDate != day)
            {
                _selectedDate = day;
                _viewMonth = new DateTime(day.Year, day.Month, 1);
                OnPropertyChanged(nameof(SelectedDate));
                SelectedDateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeSelectedDate() => _selectedDate.Date != DateTime.Today;
    private void ResetSelectedDate() => SelectedDate = DateTime.Today;

    /// <summary>
    /// Gets or sets the month currently shown in the editor ring.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime ViewMonth
    {
        get => _viewMonth;
        set => _viewMonth = new DateTime(value.Year, value.Month, 1);
    }

    /// <summary>
    /// Gets or sets the day-page scroll offset while editing.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ScrollOffset
    {
        get => _scrollOffset;
        set => _scrollOffset = Math.Max(0, value);
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool HasChildren => true;

    /// <summary>
    /// Gets the days of <see cref="ViewMonth"/> for the editor ring.
    /// </summary>
    /// <returns>Day dates in month order.</returns>
    public DateTime[] GetMonthDays()
    {
        var daysInMonth = DateTime.DaysInMonth(_viewMonth.Year, _viewMonth.Month);
        var days = new DateTime[daysInMonth];
        for (var d = 1; d <= daysInMonth; d++)
        {
            days[d - 1] = new DateTime(_viewMonth.Year, _viewMonth.Month, d);
        }

        return days;
    }

    /// <summary>
    /// Moves <see cref="ViewMonth"/> by the given number of months and resets scroll.
    /// </summary>
    /// <param name="deltaMonths">Months to add.</param>
    public void ShiftMonth(int deltaMonths)
    {
        _viewMonth = _viewMonth.AddMonths(deltaMonths);
        _scrollOffset = 0;
        OnPropertyChanged(nameof(ViewMonth));
    }

    #endregion
}
