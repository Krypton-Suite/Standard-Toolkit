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
/// Implement redirected storage for common month calendar appearance.
/// </summary>
public class PaletteMonthCalendarRedirect : PaletteDoubleMetricRedirect
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteMonthCalendarRedirect class.
    /// </summary>
    public PaletteMonthCalendarRedirect()
        : this(null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteMonthCalendarRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection for bread crumb level.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteMonthCalendarRedirect(PaletteRedirect? redirect,
        NeedPaintHandler? needPaint)
        : base(redirect!, PaletteBackStyle.ControlClient, 
            PaletteBorderStyle.ControlClient)
    {
        Header = new PaletteTripleRedirect(redirect!, PaletteBackStyle.HeaderCalendar, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.HeaderCalendar, needPaint);
        DayOfWeek = new PaletteTripleRedirect(redirect!, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
        Day = new PaletteTripleRedirect(redirect!, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault && 
                                      Header.IsDefault &&
                                      DayOfWeek.IsDefault &&
                                      Day.IsDefault;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public override void SetRedirector(PaletteRedirect redirect)
    {
        base.SetRedirector(redirect);
        Header.SetRedirector(redirect);
        DayOfWeek.SetRedirector(redirect);
        Day.SetRedirector(redirect);
    }
    #endregion

    #region Styles
    internal ButtonStyle DayStyle
    {
        set => Day.SetStyles(value);
    }

    internal ButtonStyle DayOfWeekStyle
    {
        set => DayOfWeek.SetStyles(value);
    }
    #endregion

    #region Header
    /// <summary>
    /// Gets access to the month/year header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month/year header appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect Header { get; }

    private bool ShouldSerializeHeader() => !Header.IsDefault;

    #endregion

    #region Day
    /// <summary>
    /// Gets access to the day appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining day appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect Day { get; }

    private bool ShouldSerializeDay() => !Day.IsDefault;

    #endregion

    #region DayOfWeek
    /// <summary>
    /// Gets access to the day of week appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining day of week appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect DayOfWeek { get; }

    private bool ShouldSerializeDayOfWeek() => !DayOfWeek.IsDefault;

    #endregion
}