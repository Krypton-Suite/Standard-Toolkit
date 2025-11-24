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

internal class DesignTimeDraw
{
    #region Static Fields

    private const int DESIGN_FLAP_WIDTH = 12;
    private const int DESIGN_SEP_WIDTH = 6;

    #endregion

    #region FlapWidth
    /// <summary>
    /// Gets the width of the design time flap.
    /// </summary>
    public static int FlapWidth => DESIGN_FLAP_WIDTH;

    #endregion

    #region SepWidth
    /// <summary>
    /// Gets the width of the design time separation.
    /// </summary>
    public static int SepWidth => DESIGN_SEP_WIDTH;

    #endregion

    #region DrawArea
    /// <summary>
    /// Draw a design area with a flap on the left hand edge.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="context">Rendering context.</param>
    /// <param name="clientRect">Client rectangle of the source view.</param>
    /// <param name="state">State of element.</param>
    public static void DrawArea(KryptonRibbon ribbon,
        RenderContext context,
        Rectangle clientRect,
        PaletteState state)
    {
        Color c = state == PaletteState.Normal
            ? ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorDark(PaletteState.Normal)
            : ribbon.StateCommon.RibbonGroupButton.Back.GetBackColor1(PaletteState.Tracking);

        // Draw entire area in color
        using var darkBrush = new SolidBrush(c);
        context.Graphics.FillRectangle(darkBrush, clientRect);
    }
    #endregion
 
    #region DrawFlapArea
    /// <summary>
    /// Draw a design area with a flap on the left hand edge.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="context">Rendering context.</param>
    /// <param name="clientRect">Client rectangle of the source view.</param>
    /// <param name="state">State of element.</param>
    public static void DrawFlapArea(KryptonRibbon ribbon,
        RenderContext context,
        Rectangle clientRect,
        PaletteState state)
    {
        Color c = state == PaletteState.Normal
            ? ControlPaint.Dark(ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorDark(PaletteState.Normal))
            : ribbon.StateCommon.RibbonGroupButton.Back.GetBackColor1(PaletteState.Tracking);

        // Draw border around entire area
        Rectangle drawRect = clientRect;
        drawRect.Width -= DESIGN_SEP_WIDTH;
        drawRect.Height--;
        drawRect.X++;
        using (var darkPen = new Pen(c))
        {
            context.Graphics.DrawRectangle(darkPen, drawRect);
        }

        // Draw the flap in the dark color
        drawRect.Width = DESIGN_FLAP_WIDTH - 2;
        using (var darkBrush = new SolidBrush(c))
        {
            context.Graphics.FillRectangle(darkBrush, drawRect);
        }
    }
    #endregion
}