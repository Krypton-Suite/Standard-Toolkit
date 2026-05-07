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
public class PaletteMonthCalendarState : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteMonthCalendarState class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect)
        : this(redirect, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteMonthCalendarState class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect,
        NeedPaintHandler? needPaint) =>
        Day = new PaletteTriple(redirect.Day, needPaint);

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Day.IsDefault;

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
}