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
/// Allocate space for the week number corner in the month calendar.
/// </summary>
public class ViewLayoutWeekCorner : ViewLeaf
{
    #region Instance Fields
    private readonly IKryptonMonthCalendar _calendar;
    private readonly ViewLayoutMonths _months;
    private readonly PaletteBorder _palette;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutWeekCorner class.
    /// </summary>
    /// <param name="calendar">Reference to calendar provider.</param>
    /// <param name="months">Reference to months instance.</param>
    /// <param name="palette">Reference to border palette.</param>
    public ViewLayoutWeekCorner(IKryptonMonthCalendar calendar, 
        ViewLayoutMonths months,
        PaletteBorder palette)
    {
        _calendar = calendar;
        _months = months;
        _palette = palette;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutWeekCorner:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Start with size needed to draw a week number
        var retSize = new Size(_months.SizeDay.Width, _months.SizeDays.Height);

        // Add the width of the vertical border
        retSize.Width += _palette.GetBorderWidth(State);

        return retSize;
    }
        
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;
    
        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context) => Debug.Assert(context != null);

    #endregion
}