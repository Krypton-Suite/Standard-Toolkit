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
/// Extends the ViewComposite by creating/destroying month instances in a grid.
/// </summary>
public class ViewLayoutMonths : ViewComposite,
    IContentValues
{
    #region Static Fields
    internal const int GAP = 2;
    #endregion

    #region Instance Fields

    private readonly ViewDrawDocker _drawHeader;
    private readonly PaletteBorderInheritForced _borderForced;
    private VisualPopupToolTip? _visualPopupToolTip;
    private readonly ViewDrawToday _drawToday;
    private readonly ButtonSpecRemapByContentView _remapPalette;
    private readonly ViewDrawEmptyContent _emptyContent;
    private readonly PaletteTripleRedirect _palette;
    private readonly ToolTipManager _toolTipManager;
    private CultureInfo? _lastCultureInfo;
    private DateTime _displayMonth;
    private string _dayOfWeekMeasure;
    private string _dayMeasure;
    private string _shortText;
    private DateTime _oldSelectionStart;
    private DateTime _oldSelectionEnd;
    private DateTime? _oldFocusDay;
    private DateTime? _trackingDay;
    private DateTime? _anchorDay;
    private readonly NeedPaintHandler _needPaintDelegate;
    private readonly PaletteRedirect _redirector;
    private bool _showWeekNumbers;
    private bool _showTodayCircle;
    private bool _showToday;
    private bool _firstTimeSync;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutMonths class.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="monthCalendar">Reference to owning month calendar entry.</param>
    /// <param name="viewManager">Owning view manager instance.</param>
    /// <param name="calendar">Reference to calendar provider.</param>
    /// <param name="redirector">Redirector for getting values.</param>
    /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
    public ViewLayoutMonths(IContextMenuProvider? provider,
        KryptonContextMenuMonthCalendar? monthCalendar,
        ViewContextMenuManager? viewManager,
        IKryptonMonthCalendar calendar,
        PaletteRedirect redirector,
        NeedPaintHandler needPaintDelegate)
    {
        Provider = provider!;
        Calendar = calendar;
        _oldSelectionStart = Calendar.SelectionStart;
        _oldSelectionEnd = Calendar.SelectionEnd;
        _displayMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        _redirector = redirector;
        _needPaintDelegate = needPaintDelegate;
        _showToday = true;
        _showTodayCircle = true;
        CloseOnTodayClick = false;
        _firstTimeSync = true;
        AllowButtonSpecToolTips = false;

        // Use a controller that can work against all the displayed months
        var controller =
            new MonthCalendarController(monthCalendar!, viewManager!, this, _needPaintDelegate);
        MouseController = controller;
        SourceController = controller;
        KeyController = controller;

        _borderForced = new PaletteBorderInheritForced(Calendar.StateNormal.Header.Border);
        _borderForced.ForceBorderEdges(PaletteDrawBorders.None);
        _drawHeader = new ViewDrawDocker(Calendar.StateNormal.Header.Back, _borderForced, null);
        _emptyContent = new ViewDrawEmptyContent(Calendar.StateDisabled.Header.Content, Calendar.StateNormal.Header.Content);
        _drawHeader.Add(_emptyContent, ViewDockStyle.Fill);
        Add(_drawHeader);

        // Using a button spec manager to add the buttons to the header
        ButtonSpecs = new MonthCalendarButtonSpecCollection(this);
        ButtonManager = new ButtonSpecManagerDraw(Calendar.CalendarControl, redirector, ButtonSpecs, null,
            [_drawHeader],
            [Calendar.StateCommon],
            [PaletteMetricInt.HeaderButtonEdgeInsetCalendar],
            [PaletteMetricPadding.None],
            Calendar.GetToolStripDelegate, _needPaintDelegate);

        // Create the manager for handling tooltips
        _toolTipManager = new ToolTipManager(new ToolTipValues(null, GetDpiFactor)); // use default, as each button "could" have different values ??!!??
        _toolTipManager.ShowToolTip += OnShowToolTip;
        _toolTipManager.CancelToolTip += OnCancelToolTip;
        ButtonManager.ToolTipManager = _toolTipManager;

        // Create the bottom header used for showing 'today' and defined button specs
        _remapPalette = (ButtonSpecRemapByContentView)ButtonManager.CreateButtonSpecRemap(redirector, new ButtonSpecAny());
        _remapPalette.Foreground = _emptyContent;

        // Use a redirector to get button values directly from palette
        _palette = new PaletteTripleRedirect(_remapPalette,
            PaletteBackStyle.ButtonButtonSpec,
            PaletteBorderStyle.ButtonButtonSpec,
            PaletteContentStyle.ButtonButtonSpec,
            _needPaintDelegate);

        _drawToday = new ViewDrawToday(Calendar, _palette, _palette, _palette, _palette, _needPaintDelegate);
        _drawToday.Click += OnTodayClick;
        _drawHeader.Add(_drawToday, ViewDockStyle.Left);
    }

    private float GetDpiFactor() =>
        (_visualPopupToolTip != null)
            ? _visualPopupToolTip.DeviceDpi / 96F
            : 1F;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutMonths:{Id}";

    #endregion

    #region Public
    /// <summary>
    /// Gets and sets a value indicating if tooltips should be displayed for button specs.
    /// </summary>
    public bool AllowButtonSpecToolTips { get; set; }

    /// <summary>
    /// Gets access to the button manager.
    /// </summary>
    public ButtonSpecManagerDraw ButtonManager { get; }

    /// <summary>
    /// Gets access to the collection of button spec definitions.
    /// </summary>
    public MonthCalendarButtonSpecCollection ButtonSpecs { get; }

    /// <summary>
    /// Recreate the set of button spec instances.
    /// </summary>
    public void RecreateButtons() => ButtonManager.RecreateButtons();

    /// <summary>
    /// Gets access to the month calendar.
    /// </summary>
    public IKryptonMonthCalendar Calendar { get; }

    /// <summary>
    /// Gets access to the optional context menu provider.
    /// </summary>
    public IContextMenuProvider Provider { get; }

    /// <summary>
    /// Gets and sets the day that is currently being tracked.
    /// </summary>
    public DateTime? TrackingDay
    {
        get => _trackingDay;

        set
        {
            if (value != _trackingDay)
            {
                _needPaintDelegate(this, new NeedLayoutEventArgs(false));
                _trackingDay = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the day that is currently showing focus.
    /// </summary>
    public DateTime? FocusDay
    {
        get => Calendar.FocusDay;
        set => Calendar.FocusDay = value;
    }

    /// <summary>
    /// Gets and sets the day that is the anchor for shift changes.
    /// </summary>
    public DateTime? AnchorDay
    {
        get => _anchorDay;

        set
        {
            if (value != _anchorDay)
            {
                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                _anchorDay = value;
            }
        }
    }

    /// <summary>
    /// Gets and set the display of a circle for todays date.
    /// </summary>
    public bool ShowTodayCircle
    {
        get => _showTodayCircle;

        set
        {
            if (value != _showTodayCircle)
            {
                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                _showTodayCircle = value;
            }
        }
    }

    /// <summary>
    /// Gets and set the display of todays date.
    /// </summary>
    public bool ShowToday
    {
        get => _showToday;

        set
        {
            if (value != _showToday)
            {
                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                _showToday = value;
            }
        }
    }

    /// <summary>
    /// Gets and set if the menu is closed when the today button is pressed.
    /// </summary>
    public bool CloseOnTodayClick { get; set; }

    /// <summary>
    /// Gets and sets the showing of week numbers.
    /// </summary>
    public bool ShowWeekNumbers
    {
        get => _showWeekNumbers;

        set
        {
            if (value != _showWeekNumbers)
            {
                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                _showWeekNumbers = value;
            }
        }
    }

    /// <summary>
    /// Gets the number of display months.
    /// </summary>
    public int Months => Calendar.CalendarDimensions.Width *
                         Calendar.CalendarDimensions.Height;

    /// <summary>
    /// Process a key down by finding the correct month and calling the associated key controller.
    /// </summary>
    /// <param name="c">Owning control.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    /// <returns>True if the key was processed; otherwise false.</returns>
    public bool ProcessKeyDown(Control c, KeyEventArgs e)
    {
        // We must have a focused day
        if (FocusDay != null)
        {
            KeyController?.KeyDown(c, e);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the button for the day that is nearest (date wise) to the point provided.
    /// </summary>
    /// <param name="pt">Point to lookup.</param>
    /// <returns>DateTime for nearest matching day.</returns>
    public DateTime DayNearPoint(Point pt)
    {
        // Search for an exact matching month view
        foreach (ViewBase view in this)
        {
            if ((view is ViewDrawMonth month) && month.ClientRectangle.Contains(pt))
            {
                return month.ViewDrawMonthDays.DayNearPoint(pt);
            }
        }

        var cols = Calendar.CalendarDimensions.Width;
        var rows = Calendar.CalendarDimensions.Height;
        var ptCol = cols - 1;
        var ptRow = rows - 1;

        // Find the column to be used in lookup
        for (var col = 0; col < cols; col++)
        {
            if (pt.X < this[col + 1]!.ClientRectangle.Right)
            {
                ptCol = col;
                break;
            }
        }

        // Find the row to be used in lookup
        for (var row = 0; row < rows; row++)
        {
            if (pt.Y < this[(row * cols) + 1]!.ClientRectangle.Bottom)
            {
                ptRow = row;
                break;
            }
        }

        var target = this[ptCol + (ptRow * cols) + 1] as ViewDrawMonth;
        return target!.ViewDrawMonthDays.DayNearPoint(pt);
    }

    /// <summary>
    /// Gets the button for the day that is under the provided point.
    /// </summary>
    /// <param name="pt">Point to lookup.</param>
    /// <param name="exact">Exact requires that the day must be with the month range.</param>
    /// <returns>DateTime for matching day; otherwise null.</returns>
    public DateTime? DayFromPoint(Point pt, bool exact)
    {
        // Get the bottom most view element matching the point
        ViewBase? view = ViewFromPoint(pt);

        // Climb view hierarchy looking for the days view 
        while (view != null)
        {
            if ((view is ViewDrawMonthDays month) && month.ClientRectangle.Contains(pt))
            {
                return month.DayFromPoint(pt, exact);
            }

            view = view.Parent;
        }

        return null;
    }

    /// <summary>
    /// Move to the next month.
    /// </summary>
    public void NextMonth()
    {
        // Get the number of months to move
        var move = Calendar.ScrollChange;
        if (move == 0)
        {
            move = 1;
        }

        // Calculate the next set of months shown
        DateTime nextMonth = _displayMonth.AddMonths(move);
        DateTime lastDate = nextMonth.AddMonths(Calendar.CalendarDimensions.Width *
                                                Calendar.CalendarDimensions.Height);

        DateTime ld = lastDate.AddDays(-1);
        DateTime ldofm = LastDayOfMonth(Calendar.MaxDate);

        // We do not move the month if doing so moves it past the maximum date
        if (lastDate.AddDays(-1) <= LastDayOfMonth(Calendar.MaxDate))
        {
            // Use the newly calculated month
            _displayMonth = nextMonth;

            // If the end of the selection is no longer visible
            if (Calendar.SelectionEnd < _displayMonth)
            {
                // Find new selection dates
                DateTime newSelStart = Calendar.SelectionStart.AddMonths(move);
                DateTime newSelEnd = Calendar.SelectionEnd.AddMonths(move);

                // Impose the min/max dates
                if (newSelStart > Calendar.MaxDate)
                {
                    newSelStart = Calendar.MaxDate;
                }

                if (newSelEnd > Calendar.MaxDate)
                {
                    newSelEnd = Calendar.MaxDate;
                }

                // Shift selection onwards
                Calendar.SetSelectionRange(newSelStart, newSelEnd);
            }

            _needPaintDelegate(this, new NeedLayoutEventArgs(true));
        }
    }

    /// <summary>
    /// Move to the previous month.
    /// </summary>
    public void PrevMonth()
    {
        // Get the number of months to move
        var move = Calendar.ScrollChange;
        if (move == 0)
        {
            move = 1;
        }

        // Calculate the next set of months shown
        DateTime prevMonth = _displayMonth.AddMonths(-move);

        // We do not move the month if doing so moves it past the maximum date
        if (prevMonth >= FirstDayOfMonth(Calendar.MinDate))
        {
            // Use the newly calculated month
            _displayMonth = prevMonth;

            DateTime lastDate = _displayMonth.AddMonths(Calendar.CalendarDimensions.Width *
                                                        Calendar.CalendarDimensions.Height);

            // If the start of the selection is no longer visible
            if (Calendar.SelectionStart >= lastDate)
            {
                // Find new selection dates
                DateTime newSelStart = Calendar.SelectionStart.AddMonths(-move);
                DateTime newSelEnd = Calendar.SelectionEnd.AddMonths(-move);

                // Impose the min/max dates
                if (newSelStart < Calendar.MinDate)
                {
                    newSelStart = Calendar.MinDate;
                }

                if (newSelEnd < Calendar.MinDate)
                {
                    newSelEnd = Calendar.MinDate;
                }

                // Shift selection backwards
                Calendar.SetSelectionRange(newSelStart, newSelEnd);
            }

            _needPaintDelegate(this, new NeedLayoutEventArgs(true));
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Gets the size required to draw a single month.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public Size GetSingleMonthSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        SyncData(context!);
        SyncMonths();

        return this[1]!.GetPreferredSize(context!);
    }

    /// <summary>
    /// Gets the size required to draw extra elements such as headers.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public Size GetExtraSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        if (_drawHeader.Visible)
        {
            Size retSize = _drawHeader.GetPreferredSize(context!);
            retSize.Width = 0;
            retSize.Height += GAP * 2;
            return retSize;
        }
        else
        {
            return Size.Empty;
        }
    }

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        SyncData(context!);
        SyncMonths();

        var preferredSize = Size.Empty;

        // Is there a today header to be measured?
        if (_drawHeader.Visible)
        {
            // Measure size of the header
            Size headerSize = _drawHeader.GetPreferredSize(context!);

            // Only use the height as the width is based on the months only
            preferredSize.Height = headerSize.Height + (GAP * 2);
        }

        // Are there any months to be measured?
        if (Count > 1)
        {
            // Only need to measure the first child as all children must be the same size
            Size monthSize = this[1]!.GetPreferredSize(context!);

            // Find total width based on requested dimensions and add a single pixel space around and between months
            preferredSize.Width += (monthSize.Width * Calendar.CalendarDimensions.Width) + (GAP * Calendar.CalendarDimensions.Width) + GAP;
            preferredSize.Height += (monthSize.Height * Calendar.CalendarDimensions.Height) + (GAP * Calendar.CalendarDimensions.Height) + GAP;
        }

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        SyncData(context!);
        SyncMonths();

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Is there a today header to layout?
        if (_drawHeader.Visible)
        {
            // Measure the required size of the header
            Size headerSize = _drawHeader.GetPreferredSize(context);

            // Position the header a the bottom of the area
            context.DisplayRectangle = new Rectangle(ClientLocation.X + GAP, ClientRectangle.Bottom - GAP - headerSize.Height,
                ClientSize.Width - (GAP * 2), headerSize.Height);

            _drawHeader.Layout(context);
        }

        // Are there any month views to layout?
        if (Count > 1)
        {
            // Only need to measure the first child as all children must be the same size
            Size monthSize = this[1]!.GetPreferredSize(context);

            // Position each child within the required grid
            Size dimensions = Calendar.CalendarDimensions;
            for (int y = 0, index = 1; y < dimensions.Height; y++)
            {
                for (var x = 0; x < dimensions.Width; x++)
                {
                    context.DisplayRectangle = new Rectangle(ClientLocation.X + (x * monthSize.Width) + (GAP * (x + 1)),
                        ClientLocation.Y + (y * monthSize.Height) + (GAP * (y + 1)),
                        monthSize.Width, monthSize.Height);

                    this[index++]?.Layout(context);
                }
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => _shortText;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => string.Empty;

    #endregion

    #region Internal
    internal Size SizeDays { get; private set; }

    internal Size SizeDay { get; private set; }

    internal DayOfWeek DisplayDayOfWeek { get; private set; }

    internal string[]? DayNames { get; private set; }

    #endregion

    #region Private
    private DateTime JustDay(DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day);

    private void OnTodayClick(object? sender, EventArgs e)
    {
        // Remove any time information as selecting a date is based only on the day
        DateTime today = Calendar.TodayDate;

        // Can only set a date that is within the valid min/max range
        if ((today >= Calendar.MinDate) && (today <= Calendar.MaxDate))
        {
            Calendar.SetSelectionRange(today, today);
            _needPaintDelegate(this, new NeedLayoutEventArgs(true));
        }

        // Is the menu capable of being closed?
        if (CloseOnTodayClick && Provider is { ProviderCanCloseMenu: true })
        {
            // Ask the original context menu definition, if we can close
            var cea = new CancelEventArgs();
            Provider.OnClosing(cea);

            if (!cea.Cancel)
            {
                // Close the menu from display and pass in the item clicked as the reason
                Provider.OnClose(new CloseReasonEventArgs(ToolStripDropDownCloseReason.ItemClicked));
            }
        }
    }

    private void SyncData(ViewLayoutContext context)
    {
        // A change in culture means we need to recache information
        if ((_lastCultureInfo == null) || (!Equals(_lastCultureInfo, CultureInfo.CurrentCulture)))
        {
            _lastCultureInfo = CultureInfo.CurrentCulture;
            _needPaintDelegate(this, new NeedLayoutEventArgs(true));
            DayNames = null;
        }

        if (DayNames == null)
        {
            // Grab the names for each day of the week
            DayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
            _dayOfWeekMeasure = new string('W', Math.Max(3, DayNames[0].Length));
            _dayMeasure = "WW";
        }

        if (Calendar.FirstDayOfWeek == Day.Default)
        {
            DisplayDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }
        else
        {
            DisplayDayOfWeek = (DayOfWeek)((((int)Calendar.FirstDayOfWeek) + 1) % 6);
        }

        // Find the grid cell sizes needed for day names and day entries
        SizeDays = MaxGridCellDayOfWeek(context);
        SizeDay = MaxGridCellDay(context);
    }

    private void SyncMonths()
    {
        // We need the today header if we show the today button or a button spec
        this[0]!.Visible = _showToday || (ButtonSpecs.Count > 0);
        this[0]!.Enabled = Enabled;
        _drawToday.Visible = _showToday;

        // How many month children instances do we need?
        var months = Months;

        // Do we need to create more month view?
        if (Count < (months + 1))
        {
            for (var i = Count - 1; i < months; i++)
            {
                Add(new ViewDrawMonth(Calendar, this, _redirector, _needPaintDelegate));
            }
        }
        else if (Count > (months + 1))
        {
            // Remove excess month view instances
            for (var i = Count - 1; i > months; i--)
            {
                this[i]?.Dispose();
                RemoveAt(i);
            }
        }

        // Is there a change in the selection range?
        if ((_oldSelectionStart != Calendar.SelectionStart) ||
            (_oldSelectionEnd != Calendar.SelectionEnd) ||
            (_oldFocusDay != FocusDay) ||
            _firstTimeSync)
        {
            _firstTimeSync = false;
            _oldSelectionStart = Calendar.SelectionStart;
            _oldSelectionEnd = Calendar.SelectionEnd;
            _oldFocusDay = FocusDay;

            // If we have a day with the focus
            if (FocusDay != null)
            {
                // If focus day is before the first month
                if (FocusDay.Value < _displayMonth)
                {
                    _displayMonth = new DateTime(FocusDay.Value.Year, FocusDay.Value.Month, 1);
                }
                else
                {
                    // If focus day is after the last month
                    DateTime endDate = _displayMonth.AddMonths(months);
                    if (FocusDay.Value >= endDate)
                    {
                        _displayMonth = new DateTime(FocusDay.Value.Year, FocusDay.Value.Month, 1).AddMonths(-(months - 1));
                    }
                }
            }
            else
            {
                // Bring the selection into the display range
                DateTime endMonth = _displayMonth.AddMonths(months - 1);
                DateTime oldSelEndDate = _oldSelectionEnd.Date;
                var oldSelEndMonth = new DateTime(oldSelEndDate.Year, oldSelEndDate.Month, 1);
                if (oldSelEndMonth >= endMonth)
                {
                    _displayMonth = oldSelEndMonth.AddMonths(-(months - 1));
                }

                if (_oldSelectionStart < _displayMonth)
                {
                    _displayMonth = new DateTime(Calendar.SelectionStart.Year, Calendar.SelectionStart.Month, 1);
                }
            }
        }

        DateTime currentMonth = _displayMonth;

        // Inform each view which month it should be drawing
        for (var i = 1; i < Count; i++)
        {
            var viewMonth = this[i] as ViewDrawMonth;
            viewMonth!.Enabled = Enabled;
            viewMonth.Month = currentMonth;
            viewMonth.FirstMonth = i == 1;
            viewMonth.LastMonth = i == (Count - 1);
            viewMonth.UpdateButtons(i == 1, (i - 1) == (Calendar.CalendarDimensions.Width - 1));

            // Move forward to next month
            currentMonth = currentMonth.AddMonths(1);
        }
    }

    private void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!IsDisposed)
        {
            // Do not show tooltips when the form we are in does not have focus
            Form? topForm = Calendar.CalendarControl.FindForm();
            if (topForm is { ContainsFocus: false })
            {
                return;
            }

            // Never show tooltips are design time
            if (!Calendar.InDesignMode)
            {
                IContentValues? sourceContent = null;
                var toolTipStyle = LabelStyle.ToolTip;

                var shadow = true;

                // Find the button spec associated with the tooltip request
                ButtonSpec? buttonSpec = ButtonManager.ButtonSpecFromView(e.Target);

                // If the tooltip is for a button spec
                if (buttonSpec != null)
                {
                    // Are we allowed to show page related tooltips
                    if (AllowButtonSpecToolTips)
                    {
                        // Create a helper object to provide tooltip values
                        var buttonSpecMapping = new ButtonSpecToContent(_redirector, buttonSpec);

                        // Is there actually anything to show for the tooltip
                        if (buttonSpecMapping.HasContent)
                        {
                            sourceContent = buttonSpecMapping;
                            toolTipStyle = buttonSpec.ToolTipStyle;
                            shadow = buttonSpec.ToolTipShadow;
                        }
                    }
                }

                if (sourceContent != null)
                {
                    // Remove any currently showing tooltip
                    _visualPopupToolTip?.Dispose();

                    // Create the actual tooltip popup object
                    _visualPopupToolTip = new VisualPopupToolTip(_redirector,
                        sourceContent,
                        Calendar.GetRenderer(),
                        PaletteBackStyle.ControlToolTip,
                        PaletteBorderStyle.ControlToolTip,
                        CommonHelper.ContentStyleFromLabelStyle(toolTipStyle),
                        shadow);

                    _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;

                    _visualPopupToolTip.ShowRelativeTo(e.Target, e.ControlMousePosition);
                }
            }
        }
    }

    private void OnCancelToolTip(object? sender, EventArgs e) =>
        // Remove any currently showing tooltip
        _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page any more
        _visualPopupToolTip = null;
    }

    private Size MaxGridCellDay([DisallowNull] ViewLayoutContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        _shortText = _dayMeasure;

        // Find sizes required for the different 
        Size normalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateNormal.Day.Content, this, VisualOrientation.Top, PaletteState.Normal);
        Size disabledSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateDisabled.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled);
        Size trackingSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateTracking.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled);
        Size pressedSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StatePressed.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled);
        Size checkedNormalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateCheckedNormal.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled);
        Size checkedTrackingSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateCheckedTracking.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled);
        Size checkedPressedSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateCheckedPressed.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled);

        // Find largest size required
        normalSize.Width = Math.Max(normalSize.Width, Math.Max(disabledSize.Width, Math.Max(trackingSize.Width, Math.Max(pressedSize.Width, Math.Max(checkedNormalSize.Width, Math.Max(checkedTrackingSize.Width, checkedPressedSize.Width))))));
        normalSize.Height = Math.Max(normalSize.Height, Math.Max(disabledSize.Height, Math.Max(trackingSize.Height, Math.Max(pressedSize.Height, Math.Max(checkedNormalSize.Height, Math.Max(checkedTrackingSize.Height, checkedPressedSize.Height))))));

        return normalSize;
    }

    private Size MaxGridCellDayOfWeek([DisallowNull] ViewLayoutContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        _shortText = "A";

        // Find sizes required for the different 
        Size shortNormalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateNormal.DayOfWeek.Content, this, VisualOrientation.Top, PaletteState.Normal);
        Size shortDisabledSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateDisabled.DayOfWeek.Content, this, VisualOrientation.Top, PaletteState.Disabled);

        _shortText = $"A{_dayOfWeekMeasure}";

        // Find sizes required for the different 
        Size fullNormalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateNormal.DayOfWeek.Content, this, VisualOrientation.Top, PaletteState.Normal);
        Size fullDisabledSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, Calendar.StateDisabled.DayOfWeek.Content, this, VisualOrientation.Top, PaletteState.Disabled);

        // Find largest size required (subtract a fudge factor of 3 pixels as Graphics.MeasureString is always too big)
        fullNormalSize.Width = Math.Max(fullNormalSize.Width - shortNormalSize.Width - 3, fullDisabledSize.Width - shortDisabledSize.Width - 3);
        fullNormalSize.Height = Math.Max(fullNormalSize.Height, fullDisabledSize.Height);

        return fullNormalSize;
    }

    private DateTime FirstDayOfMonth(DateTime dt)
    {
        dt = dt.AddDays(-(dt.Day - 1));
        return JustDay(dt);
    }

    private DateTime LastDayOfMonth(DateTime dt)
    {
        dt = dt.AddMonths(1);
        dt = dt.AddDays(-dt.Day);
        return JustDay(dt);
    }
    #endregion
}