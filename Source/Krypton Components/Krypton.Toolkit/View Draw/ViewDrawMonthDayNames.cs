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
/// Draws the month day names.
/// </summary>
public class ViewDrawMonthDayNames : ViewLeaf,
    IContentValues
{
    #region Instance Fields
    private readonly IKryptonMonthCalendar _calendar;
    private readonly ViewLayoutMonths _months;
    private readonly IDisposable?[] _dayMementos;
    private string _drawText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMonthDayNames class.
    /// </summary>
    /// <param name="calendar">Reference to calendar provider.</param>
    /// <param name="months">Reference to months instance.</param>
    public ViewDrawMonthDayNames(IKryptonMonthCalendar calendar, ViewLayoutMonths months)
    {
        _calendar = calendar;
        _months = months;

        // 7 day mementos
        _dayMementos = new IDisposable[7];
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMonthDayNames:{Id}";

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        // Dispose of the mementos to prevent memory leak
        for(var i=0; i<_dayMementos.Length; i++)
        {
            if (_dayMementos[i] != null)
            {
                _dayMementos[i]?.Dispose();
                _dayMementos[i] = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        var preferredSize = Size.Empty;

        // Width is 7 days times the width of a day name
        preferredSize.Width = _months.SizeDays.Width * 7;

        // Height is the height of the day name
        preferredSize.Height = _months.SizeDays.Height;

        return preferredSize;
    }
        
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }
        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Content palette depends on enabled state of the control
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;

        // Layout the 7 day names
        var layoutRect = new Rectangle(ClientLocation, _months.SizeDays);
        for (int i = 0, day=(int)_months.DisplayDayOfWeek; i < 7; i++, day++)
        {
            // Define text to be drawn
            _drawText = _months.DayNames![day % 7];

            _dayMementos[i]?.Dispose();

            _dayMementos[i] = context.Renderer.RenderStandardContent.LayoutContent(context, layoutRect, _calendar.StateNormal.DayOfWeek.Content, this, 
                VisualOrientation.Top, state);

            // Move across to next day
            layoutRect.X += _months.SizeDays.Width;
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Content palette depends on enabled state of the control
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;

        // Draw the 7 day names
        var drawRect = new Rectangle(ClientLocation, _months.SizeDays);
        for(int i=0, day=(int)_months.DisplayDayOfWeek; i<7; i++, day++)
        {
            // Draw using memento cached from the layout call
            if (_dayMementos[day % 7] != null)
            {
                context?.Renderer.RenderStandardContent.DrawContent(context, drawRect,
                    _calendar.StateNormal.DayOfWeek.Content, _dayMementos[day % 7]!,
                    VisualOrientation.Top, state, true);
            }

            // Move across to next day
            drawRect.X += _months.SizeDays.Width;
        }
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
    public string GetShortText() => _drawText;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}