#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 -2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Mac OS X Aqua-inspired renderer: Office 2010 base with gel buttons and pinstripe title chrome.
/// </summary>
public class RenderMacOSXAqua : RenderOffice2010
{
    private const float GelCornerRadius = 5f;

    /// <inheritdoc />
    public override IDisposable? DrawBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        VisualOrientation orientation,
        PaletteState state,
        IDisposable? memento)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return base.DrawBack(context, rect, path, palette, orientation, state, memento);
        }

        if (AquaRenderHelper.IsAquaGelBack(palette, state))
        {
            Color top = palette.GetBackColor1(state);
            Color bottom = palette.GetBackColor2(state);
            if (Math.Abs(top.GetHue() - bottom.GetHue()) < 1f && top.GetBrightness() > 0.85f)
            {
                AquaRenderHelper.DrawPinstripeHeader(context.Graphics, rect, bottom, top);
            }
            else
            {
                bool glossyHighlight = state is PaletteState.Tracking or PaletteState.Pressed
                    or PaletteState.CheckedTracking or PaletteState.CheckedPressed;
                AquaRenderHelper.DrawGelRectangle(context.Graphics, rect, top, bottom, GelCornerRadius, glossyHighlight);
            }

            return memento;
        }

        return base.DrawBack(context, rect, path, palette, orientation, state, memento);
    }

    /// <inheritdoc />
    protected override IDisposable? DrawRibbonTabSelected2010(RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonBack palette,
        VisualOrientation orientation,
        IDisposable? memento,
        bool standard)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width > 0 && rect.Height > 0)
        {
            AquaRenderHelper.DrawGelRectangle(
                context.Graphics,
                rect,
                palette.GetRibbonBackColor1(state),
                palette.GetRibbonBackColor2(state),
                4f);
        }

        return memento;
    }

    /// <inheritdoc />
    protected override IDisposable? DrawRibbonTabTracking2010(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonBack palette,
        VisualOrientation orientation,
        IDisposable? memento,
        bool standard)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width > 0 && rect.Height > 0)
        {
            var top = palette.GetRibbonBackColor1(state);
            var bottom = CommonHelper.MergeColors(top, 0.7f, Color.White, 0.3f);
            AquaRenderHelper.DrawGelRectangle(context.Graphics, rect, top, bottom, 4f);
        }

        return memento;
    }
}