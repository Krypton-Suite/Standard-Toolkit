#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonMonthCalendar control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonMonthCalendarExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonMonthCalendar? _monthCalendar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonMonthCalendarExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonMonthCalendarExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _monthCalendar = (KryptonMonthCalendar?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the maximum allowable date.
    /// </summary>
    public DateTime MaxDate
    {
        get => _monthCalendar?.MaxDate ?? DateTime.MaxValue;
        set => SetPropertyValue(_monthCalendar!, nameof(MaxDate), MaxDate, value, v => _monthCalendar!.MaxDate = v);
    }

    /// <summary>
    /// Gets and sets the minimum allowable date.
    /// </summary>
    public DateTime MinDate
    {
        get => _monthCalendar?.MinDate ?? DateTime.MinValue;
        set => SetPropertyValue(_monthCalendar!, nameof(MinDate), MinDate, value, v => _monthCalendar!.MinDate = v);
    }

    /// <summary>
    /// Gets and sets the maximum number of columns and rows of months displayed.
    /// </summary>
    public Size CalendarDimensions
    {
        get => _monthCalendar?.CalendarDimensions ?? new Size(1, 1);
        set => SetPropertyValue(_monthCalendar!, nameof(CalendarDimensions), CalendarDimensions, value, v => _monthCalendar!.CalendarDimensions = v);
    }

    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _monthCalendar?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_monthCalendar!, nameof(PaletteMode), PaletteMode, value, v => _monthCalendar!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the today date format string.
    /// </summary>
    public string TodayFormat
    {
        get => _monthCalendar?.TodayFormat ?? string.Empty;
        set => SetPropertyValue(_monthCalendar!, nameof(TodayFormat), TodayFormat, value, v => _monthCalendar!.TodayFormat = v);
    }

    /// <summary>
    /// Gets and sets whether the control will show todays date.
    /// </summary>
    public bool ShowToday
    {
        get => _monthCalendar?.ShowToday ?? true;
        set => SetPropertyValue(_monthCalendar!, nameof(ShowToday), ShowToday, value, v => _monthCalendar!.ShowToday = v);
    }

    /// <summary>
    /// Gets and sets whether the control will circle the today date.
    /// </summary>
    public bool ShowTodayCircle
    {
        get => _monthCalendar?.ShowTodayCircle ?? true;
        set => SetPropertyValue(_monthCalendar!, nameof(ShowTodayCircle), ShowTodayCircle, value, v => _monthCalendar!.ShowTodayCircle = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_monthCalendar != null)
        {
            // Add the list of MonthCalendar specific actions
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(MinDate), @"Min Date", @"Values", @"Minimum allowable date"));
            actions.Add(new DesignerActionPropertyItem(nameof(MaxDate), @"Max Date", @"Values", @"Maximum allowable date"));
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(CalendarDimensions), @"Calendar Dimensions", @"Appearance", @"Calendar dimensions"));
            actions.Add(new DesignerActionPropertyItem(nameof(TodayFormat), @"Today Format", @"Appearance", @"Today date format"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowToday), @"Show Today", @"Behavior", @"Show today date"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowTodayCircle), @"Show Today Circle", @"Behavior", @"Circle today date"));
        }

        return actions;
    }
    #endregion
}
