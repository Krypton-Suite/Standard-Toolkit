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
/// Draw element for a context menu month calendar.
/// </summary>
public class ViewDrawMenuMonthCalendar : ViewComposite,
    IKryptonMonthCalendar
{
    #region Instance Fields
    private readonly KryptonContextMenuMonthCalendar _monthCalendar;
    private readonly IContextMenuProvider _provider;
    private readonly ViewLayoutMonths _layoutMonths;
    private readonly bool _itemEnabled;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuMonthCalendar class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="monthCalendar">Reference to owning month calendar entry.</param>
    public ViewDrawMenuMonthCalendar(IContextMenuProvider provider,
        KryptonContextMenuMonthCalendar monthCalendar)
    {
        _provider = provider;
        _monthCalendar = monthCalendar;
        FirstDayOfWeek = _monthCalendar.FirstDayOfWeek;
        MinDate = _monthCalendar.MinDate;
        MaxDate = _monthCalendar.MaxDate;
        TodayDate = _monthCalendar.TodayDate;
        MaxSelectionCount = _monthCalendar.MaxSelectionCount;
        ScrollChange = _monthCalendar.ScrollChange;
        TodayText = _monthCalendar.TodayText;
        TodayFormat = _monthCalendar.TodayFormat;
        CalendarDimensions = _monthCalendar.CalendarDimensions;

        // Decide on the enabled state of the display
        _itemEnabled = provider.ProviderEnabled && _monthCalendar.Enabled;

        // Give the item object the redirector to use when inheriting values
        _monthCalendar.SetPaletteRedirect(provider.ProviderRedirector);

        // Create view that is used by standalone control as well as this context menu element
        _layoutMonths = new ViewLayoutMonths(provider, monthCalendar, provider.ProviderViewManager, this, provider.ProviderRedirector, provider.ProviderNeedPaintDelegate)
        {
            CloseOnTodayClick = _monthCalendar.CloseOnTodayClick,
            ShowWeekNumbers = _monthCalendar.ShowWeekNumbers,
            ShowTodayCircle = _monthCalendar.ShowTodayCircle,
            ShowToday = _monthCalendar.ShowToday,
            Enabled = _itemEnabled
        };

        Add(_layoutMonths);
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing) =>
        // Prevent memory leak
        base.Dispose(disposing);

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuMonthCalendar:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        base.Layout(context);
    }
    #endregion

    #region IKryptonMonthCalendar
    /// <summary>
    /// Gets access to the owning control
    /// </summary>
    public Control CalendarControl => _provider.ProviderViewManager.Control;

    /// <summary>
    /// Gets if the control is in design mode.
    /// </summary>
    public bool InDesignMode => false;

    /// <summary>
    /// Get the renderer.
    /// </summary>
    /// <returns>Render instance.</returns>
    public IRenderer GetRenderer()
    {
        var contextMenu = (VisualContextMenu)_provider.ProviderViewManager.Control;
        return contextMenu.Renderer;
    }

    /// <summary>
    /// Gets a delegate for creating tool strip renderers.
    /// </summary>
    public GetToolStripRenderer GetToolStripDelegate
    {
        get 
        {
            var contextMenu = (VisualContextMenu)_provider.ProviderViewManager.Control;
            return contextMenu.CreateToolStripRenderer!;
        }
    }

    /// <summary>
    /// Gets the number of columns and rows of months displayed.
    /// </summary>
    public Size CalendarDimensions { get; }

    /// <summary>
    /// First day of the week.
    /// </summary>
    public Day FirstDayOfWeek { get; }

    /// <summary>
    /// First date allowed to be drawn/selected.
    /// </summary>
    public DateTime MinDate { get; }

    /// <summary>
    /// Last date allowed to be drawn/selected.
    /// </summary>
    public DateTime MaxDate { get; }

    /// <summary>
    /// Today's date.
    /// </summary>
    public DateTime TodayDate { get; }

    /// <summary>
    /// Today's date format.
    /// </summary>
    public string TodayFormat { get; }

    /// <summary>
    /// Gets the focus day.
    /// </summary>
    public DateTime? FocusDay
    {
        get => _monthCalendar.FocusDay;
        set => _monthCalendar.FocusDay = value;
    }

    /// <summary>
    /// Number of days allowed to be selected at a time.
    /// </summary>
    public int MaxSelectionCount { get; }

    /// <summary>
    /// Gets the text used as a today label.
    /// </summary>
    public string TodayText { get; }

    /// <summary>
    /// Gets the number of months to move for next/prev buttons.
    /// </summary>
    public int ScrollChange { get; }

    /// <summary>
    /// Start of selected range.
    /// </summary>
    public DateTime SelectionStart => _monthCalendar.SelectionStart;

    /// <summary>
    /// End of selected range.
    /// </summary>
    public DateTime SelectionEnd => _monthCalendar.SelectionEnd;

    /// <summary>
    /// Gets access to the month calendar common appearance entries.
    /// </summary>
    public PaletteMonthCalendarRedirect StateCommon => _monthCalendar.StateCommon;

    /// <summary>
    /// Gets access to the month calendar normal appearance entries.
    /// </summary>
    public PaletteMonthCalendarDoubleState StateNormal => _monthCalendar.StateNormal;

    /// <summary>
    /// Gets access to the month calendar disabled appearance entries.
    /// </summary>
    public PaletteMonthCalendarDoubleState StateDisabled => _monthCalendar.StateDisabled;

    /// <summary>
    /// Gets access to the month calendar tracking appearance entries.
    /// </summary>
    public PaletteMonthCalendarState StateTracking => _monthCalendar.StateTracking;

    /// <summary>
    /// Gets access to the month calendar pressed appearance entries.
    /// </summary>
    public PaletteMonthCalendarState StatePressed => _monthCalendar.StatePressed;

    /// <summary>
    /// Gets access to the month calendar checked normal appearance entries.
    /// </summary>
    public PaletteMonthCalendarState StateCheckedNormal => _monthCalendar.StateCheckedNormal;

    /// <summary>
    /// Gets access to the month calendar checked tracking appearance entries.
    /// </summary>
    public PaletteMonthCalendarState StateCheckedTracking => _monthCalendar.StateCheckedTracking;

    /// <summary>
    /// Gets access to the month calendar checked pressed appearance entries.
    /// </summary>
    public PaletteMonthCalendarState StateCheckedPressed => _monthCalendar.StateCheckedPressed;

    /// <summary>
    /// Gets access to the override for disabled day.
    /// </summary>
    public PaletteTripleOverride OverrideDisabled => _monthCalendar.OverrideDisabled;

    /// <summary>
    /// Gets access to the override for normal day.
    /// </summary>
    public PaletteTripleOverride OverrideNormal => _monthCalendar.OverrideNormal;

    /// <summary>
    /// Gets access to the override for tracking day.
    /// </summary>
    public PaletteTripleOverride OverrideTracking => _monthCalendar.OverrideTracking;

    /// <summary>
    /// Gets access to the override for pressed day.
    /// </summary>
    public PaletteTripleOverride OverridePressed => _monthCalendar.OverridePressed;

    /// <summary>
    /// Gets access to the override for checked normal day.
    /// </summary>
    public PaletteTripleOverride OverrideCheckedNormal => _monthCalendar.OverrideCheckedNormal;

    /// <summary>
    /// Gets access to the override for checked tracking day.
    /// </summary>
    public PaletteTripleOverride OverrideCheckedTracking => _monthCalendar.OverrideCheckedTracking;

    /// <summary>
    /// Gets access to the override for checked pressed day.
    /// </summary>
    public PaletteTripleOverride OverrideCheckedPressed => _monthCalendar.OverrideCheckedPressed;

    /// <summary>
    /// Dates to be bolded.
    /// </summary>
    public DateTimeList BoldedDatesList => _monthCalendar.BoldedDatesList;

    /// <summary>
    /// Monthly days to be bolded.
    /// </summary>
    public int MonthlyBoldedDatesMask => _monthCalendar.MonthlyBoldedDatesMask;

    /// <summary>
    /// Array of annual days per month to be bolded.
    /// </summary>
    public int[] AnnuallyBoldedDatesMask => _monthCalendar.AnnuallyBoldedDatesMask;

    /// <summary>
    /// Set the selection range.
    /// </summary>
    /// <param name="start">New starting date.</param>
    /// <param name="end">New ending date.</param>
    public void SetSelectionRange(DateTime start, DateTime end) => _monthCalendar.SetSelectionRange(start, end);

    /// <summary>
    /// Update usage of bolded overrides.
    /// </summary>
    /// <param name="bolded">New bolded state.</param>
    public void SetBoldedOverride(bool bolded) => _monthCalendar.SetBoldedOverride(bolded);

    /// <summary>
    /// Update usage of today overrides.
    /// </summary>
    /// <param name="today">New today state.</param>
    public void SetTodayOverride(bool today) => _monthCalendar.SetTodayOverride(today);

    /// <summary>
    /// Update usage of focus overrides.
    /// </summary>
    /// <param name="focus">Should show focus.</param>
    public void SetFocusOverride(bool focus) => _monthCalendar.SetFocusOverride(focus);

    #endregion
}