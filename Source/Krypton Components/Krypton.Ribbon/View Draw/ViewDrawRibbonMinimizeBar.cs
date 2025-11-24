#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws a separator at the bottom of the tabs when ribbon minimized.
/// </summary>
internal class ViewDrawRibbonMinimizeBar : ViewLayoutRibbonSeparator
{
    #region Static Fields
    private const int SEP_WIDTH = 2; // DPI conversion will happen in base class
    #endregion

    #region Instance Fields
    private readonly IPaletteRibbonGeneral _palette;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonMinimizeBar class.
    /// </summary>
    /// <param name="palette">Source for palette values.</param>
    public ViewDrawRibbonMinimizeBar([DisallowNull] IPaletteRibbonGeneral palette)
        : base(SEP_WIDTH, true)
    {
        Debug.Assert(palette != null);
        _palette = palette!;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawRibbonMinimizeBar:{Id}";

    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        using var darkPen = new Pen(_palette.GetRibbonMinimizeBarDark(PaletteState.Normal));
        using var lightPen = new Pen(_palette.GetRibbonMinimizeBarLight(PaletteState.Normal));
        context.Graphics.DrawLine(darkPen, ClientRectangle.Left, ClientRectangle.Bottom - 2, ClientRectangle.Right - 1, ClientRectangle.Bottom - 2);
        context.Graphics.DrawLine(lightPen, ClientRectangle.Left, ClientRectangle.Bottom - 1, ClientRectangle.Right - 1, ClientRectangle.Bottom - 1);
    }
    #endregion
}