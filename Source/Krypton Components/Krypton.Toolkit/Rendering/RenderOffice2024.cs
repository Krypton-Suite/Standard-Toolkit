#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Microsoft 365 renderer with Office 2024 ribbon chrome: a text File tab and no cluster edge.
/// Tab underlines and hover pills are drawn by <see cref="RenderStandard"/> from
/// <see cref="PaletteRibbonColorStyle.RibbonTabSelected2024"/> and
/// <see cref="PaletteRibbonColorStyle.RibbonTabTracking2024"/>.
/// </summary>
public class RenderOffice2024 : RenderMicrosoft365
{
    /// <summary>
    /// Draw the File tab. Office 2024 uses the same underline and hover as the other tabs, with no filled block.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="palette">Palette used for sourcing settings.</param>
    /// <param name="memento">Cached values to use when drawing.</param>
    /// <returns>Updated memento, or null when the Office 2024 text tab was drawn.</returns>
    public override IDisposable? DrawRibbonFileApplicationTab(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonFileAppTab palette,
        IDisposable? memento)
    {
        if (shape != PaletteRibbonShape.Office2024)
        {
            return base.DrawRibbonFileApplicationTab(shape, context, rect, state, palette, memento);
        }

        memento?.Dispose();

        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return null;
        }

        if ((state & PaletteState.Pressed) == PaletteState.Pressed)
        {
            DrawRibbonTabUnderline2024(context, rect, palette.GetRibbonFileAppTabTopColor(state));
        }
        else if ((state & PaletteState.Tracking) == PaletteState.Tracking)
        {
            DrawRibbonTabHover2024(context, rect, palette.GetRibbonFileAppTabBottomColor(state));
        }

        return null;
    }

    /// <summary>
    /// Office 2024 groups share one flat colour, so the cluster edge is not painted.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteBack">Palette used for recovering drawing details.</param>
    /// <param name="state">State associated with rendering.</param>
    public override void DrawRibbonClusterEdge(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteBack paletteBack,
        PaletteState state)
    {
        if (shape != PaletteRibbonShape.Office2024)
        {
            base.DrawRibbonClusterEdge(shape, context, displayRect, paletteBack, state);
        }
    }
}
