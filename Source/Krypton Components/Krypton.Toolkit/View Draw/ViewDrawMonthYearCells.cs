#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Draws a 4×3 grid of months or years for month-calendar drill views.
/// </summary>
public class ViewDrawMonthYearCells : ViewLeaf,
    IContentValues
{
    #region Static Fields
    private const int COLUMNS = 3;
    private const int ROWS = 4;
    private const int CELLS = 12;
    #endregion

    #region Instance Fields
    private readonly IKryptonMonthCalendar _calendar;
    private readonly ViewLayoutMonths _months;
    private readonly IDisposable?[] _cellMementos;
    private readonly Rectangle[] _cellRects;
    private DateTime _displayMonth;
    private string _drawText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMonthYearCells class.
    /// </summary>
    /// <param name="calendar">Reference to calendar provider.</param>
    /// <param name="months">Reference to months instance.</param>
    public ViewDrawMonthYearCells(IKryptonMonthCalendar calendar, ViewLayoutMonths months)
    {
        _calendar = calendar;
        _months = months;
        _cellMementos = new IDisposable[CELLS];
        _cellRects = new Rectangle[CELLS];
        _drawText = string.Empty;
        _displayMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }

    /// <inheritdoc />
    public override string ToString() => $"ViewDrawMonthYearCells:{Id}";

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        for (var i = 0; i < _cellMementos.Length; i++)
        {
            _cellMementos[i]?.Dispose();
            _cellMementos[i] = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Sets the date this view uses as the displayed year or decade.
    /// </summary>
    public DateTime Month
    {
        set => _displayMonth = new DateTime(value.Year, value.Month, 1);
    }

    /// <summary>
    /// Gets the cell date underneath the provided point.
    /// </summary>
    /// <param name="pt">Point to lookup.</param>
    /// <returns>DateTime for matching cell; otherwise null.</returns>
    public DateTime? CellFromPoint(Point pt)
    {
        for (var i = 0; i < CELLS; i++)
        {
            if ((_cellMementos[i] != null) && _cellRects[i].Contains(pt))
            {
                DateTime cellDate = GetCellDate(i);
                if (IsCellEnabled(cellDate))
                {
                    return cellDate;
                }
            }
        }

        return null;
    }
    #endregion

    #region Layout
    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return new Size(_months.SizeDays.Width * 7, _months.SizeDays.Height * 6);
    }

    /// <inheritdoc />
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context.Renderer));
        }

        ClientRectangle = context.DisplayRectangle;

        var cellWidth = Math.Max(1, ClientSize.Width / COLUMNS);
        var cellHeight = Math.Max(1, ClientSize.Height / ROWS);
        bool isRtl = context.IsRightToLeftLayout;

        for (var row = 0; row < ROWS; row++)
        {
            for (var col = 0; col < COLUMNS; col++)
            {
                var visualCol = isRtl ? (COLUMNS - 1 - col) : col;
                var index = (row * COLUMNS) + visualCol;
                var cellRect = new Rectangle(ClientLocation.X + (col * cellWidth),
                    ClientLocation.Y + (row * cellHeight), cellWidth, cellHeight);
                _cellRects[index] = cellRect;

                DateTime cellDate = GetCellDate(index);
                _drawText = GetCellText(cellDate);

                _cellMementos[index]?.Dispose();
                _cellMementos[index] = null;

                if (!IsCellEnabled(cellDate))
                {
                    continue;
                }

                ResolvePalette(cellDate, out PaletteState paletteState, out IPaletteTriple paletteTriple);
                _cellMementos[index] = context.Renderer.RenderStandardContent.LayoutContent(context, cellRect,
                    paletteTriple.PaletteContent!, this, VisualOrientation.Top, paletteState);
            }
        }

        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <inheritdoc />
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context.Renderer));
        }

        for (var i = 0; i < CELLS; i++)
        {
            if (_cellMementos[i] == null)
            {
                continue;
            }

            DateTime cellDate = GetCellDate(i);
            if (!IsCellEnabled(cellDate))
            {
                continue;
            }

            ResolvePalette(cellDate, out PaletteState paletteState, out IPaletteTriple paletteTriple);
            Rectangle cellRect = _cellRects[i];

            if (paletteTriple.PaletteBack.GetBackDraw(paletteState) == InheritBool.True)
            {
                using GraphicsPath path = context.Renderer.RenderStandardBorder.GetBackPath(context, cellRect,
                    paletteTriple.PaletteBorder!, VisualOrientation.Top, paletteState)!;
                using var gh = new GraphicsHint(context.Graphics, paletteTriple.PaletteBorder!.GetBorderGraphicsHint(paletteState));
                context.Renderer.RenderStandardBack.DrawBack(context, cellRect, path, paletteTriple.PaletteBack,
                    VisualOrientation.Top, paletteState, null);
            }

            if (paletteTriple.PaletteBorder!.GetBorderDraw(paletteState) == InheritBool.True)
            {
                context.Renderer.RenderStandardBorder.DrawBorder(context, cellRect, paletteTriple.PaletteBorder,
                    VisualOrientation.Top, paletteState);
            }

            if (paletteTriple.PaletteContent!.GetContentDraw(paletteState) == InheritBool.True)
            {
                _drawText = GetCellText(cellDate);
                context.Renderer.RenderStandardContent.DrawContent(context, cellRect, paletteTriple.PaletteContent,
                    _cellMementos[i]!, VisualOrientation.Top, paletteState, true);
            }
        }
    }
    #endregion

    #region IContentValues
    /// <inheritdoc />
    public Image? GetImage(PaletteState state) => null;

    /// <inheritdoc />
    public Color GetImageTransparentColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public string GetShortText() => _drawText;

    /// <inheritdoc />
    public string GetLongText() => string.Empty;

    /// <inheritdoc />
    public Image? GetOverlayImage(PaletteState state) => null;

    /// <inheritdoc />
    public Color GetOverlayImageTransparentColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    /// <inheritdoc />
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    /// <inheritdoc />
    public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

    /// <inheritdoc />
    public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);
    #endregion

    #region Implementation
    private DateTime GetCellDate(int index)
    {
        if (_months.DisplayView == MonthCalendarView.Years)
        {
            var decadeStart = Math.Max(1, (_displayMonth.Year / 10) * 10);
            var year = Math.Min(DateTime.MaxValue.Year, decadeStart + index);
            return new DateTime(year, 1, 1);
        }

        return new DateTime(_displayMonth.Year, index + 1, 1);
    }

    private string GetCellText(DateTime cellDate) =>
        _months.DisplayView == MonthCalendarView.Years
            ? cellDate.Year.ToString(CultureInfo.CurrentCulture)
            : CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[cellDate.Month - 1];

    private bool IsCellEnabled(DateTime cellDate)
    {
        DateTime minDate = _calendar.MinDate.Date;
        DateTime maxDate = _calendar.MaxDate.Date;
        DateTime rangeStart = cellDate;
        DateTime rangeEnd = _months.DisplayView == MonthCalendarView.Years
            ? new DateTime(cellDate.Year, 12, 31)
            : new DateTime(cellDate.Year, cellDate.Month, DateTime.DaysInMonth(cellDate.Year, cellDate.Month));

        return (rangeEnd >= minDate) && (rangeStart <= maxDate);
    }

    private bool IsCellSelected(DateTime cellDate)
    {
        DateTime selectStart = _calendar.SelectionStart.Date;
        return _months.DisplayView == MonthCalendarView.Years
            ? selectStart.Year == cellDate.Year
            : (selectStart.Year == cellDate.Year) && (selectStart.Month == cellDate.Month);
    }

    private bool IsCellToday(DateTime cellDate)
    {
        DateTime today = _calendar.TodayDate.Date;
        return _months.DisplayView == MonthCalendarView.Years
            ? today.Year == cellDate.Year
            : (today.Year == cellDate.Year) && (today.Month == cellDate.Month);
    }

    private void ResolvePalette(DateTime cellDate, out PaletteState paletteState, out IPaletteTriple paletteTriple)
    {
        _calendar.SetFocusOverride(false);
        _calendar.SetBoldedOverride(false);
        _calendar.SetTodayOverride(_months.ShowTodayCircle && IsCellToday(cellDate));

        var tracking = _months.TrackingDay.HasValue && CellMatches(_months.TrackingDay.Value, cellDate);
        var focused = _months.FocusDay.HasValue && CellMatches(_months.FocusDay.Value, cellDate);

        if (IsCellSelected(cellDate))
        {
            _calendar.SetFocusOverride(focused);
            if (tracking)
            {
                paletteState = PaletteState.CheckedTracking;
                paletteTriple = _calendar.OverrideCheckedTracking;
            }
            else
            {
                paletteState = PaletteState.CheckedNormal;
                paletteTriple = _calendar.OverrideCheckedNormal;
            }
        }
        else if (tracking)
        {
            paletteState = PaletteState.Tracking;
            paletteTriple = _calendar.OverrideTracking;
        }
        else
        {
            paletteState = PaletteState.Normal;
            paletteTriple = _calendar.OverrideNormal;
        }
    }

    private bool CellMatches(DateTime value, DateTime cellDate) =>
        _months.DisplayView == MonthCalendarView.Years
            ? value.Year == cellDate.Year
            : (value.Year == cellDate.Year) && (value.Month == cellDate.Month);
    #endregion
}
