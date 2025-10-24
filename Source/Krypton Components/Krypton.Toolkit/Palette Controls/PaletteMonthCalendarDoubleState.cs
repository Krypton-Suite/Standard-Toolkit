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
/// Implement storage for month calendar appearance states.
/// </summary>
public class PaletteMonthCalendarDoubleState : PaletteDouble
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteMonthCalendarDoubleState class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    public PaletteMonthCalendarDoubleState(PaletteMonthCalendarRedirect redirect)
        : this(redirect, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteMonthCalendarDoubleState class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteMonthCalendarDoubleState(PaletteMonthCalendarRedirect redirect,
        NeedPaintHandler? needPaint) 
        : base(redirect, needPaint!)
    {
        Header = new PaletteTriple(redirect.Header, needPaint);
        Day = new PaletteTriple(redirect.Day, needPaint);
        DayOfWeek = new PaletteTriple(redirect.DayOfWeek, needPaint);
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
                                      Day.IsDefault &&
                                      DayOfWeek.IsDefault;

    #endregion

    #region Header
    /// <summary>
    /// Gets access to the month/year header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining month/year header appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple Header { get; }

    private bool ShouldSerializeHeader() => !Header.IsDefault;

    #endregion

    #region Day
    /// <summary>
    /// Gets access to the day appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining day appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple Day { get; }

    private bool ShouldSerializeDay() => !Day.IsDefault;

    #endregion

    #region DayOfWeek
    /// <summary>
    /// Gets access to the day of week appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining day of week appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple DayOfWeek { get; }

    private bool ShouldSerializeDayOfWeek() => !DayOfWeek.IsDefault;

    #endregion
}