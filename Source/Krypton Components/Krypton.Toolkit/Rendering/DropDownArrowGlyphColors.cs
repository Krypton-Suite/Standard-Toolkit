#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class DropDownArrowGlyphColors
{
    internal static (Color Outline, Color Fill) Resolve(RenderContext? context, IPaletteContent paletteContent,  PaletteState state)
    {
        if (TryGetOverrideColor(context, out Color overrideColor))
        {
            return (overrideColor, NormalizeFill(overrideColor, Color.Empty));
        }

        if (TryResolvePalette(context, out PaletteBase? palette))
        {
            return FromPalette(palette, state);
        }

        return FromContent(paletteContent, state);
    }

    internal static (Color Outline, Color Fill) Resolve(PaletteBase palette, PaletteState state) => FromPalette(palette, state);

    private static (Color Outline, Color Fill) FromPalette(PaletteBase palette, PaletteState state)
    {

        Color outline;
        Color fill;
        if (state == PaletteState.Disabled)
        {
            outline = palette.GetContentShortTextColor1(PaletteContentStyle.ButtonInputControl, PaletteState.Disabled);
            fill = palette.GetContentShortTextColor2(PaletteContentStyle.ButtonInputControl, PaletteState.Disabled);
            if (outline == Color.Empty || outline == Color.Empty)
            {
                outline = palette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Disabled);
            }
        }
        else
        {
            outline = palette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
            fill = palette.GetContentShortTextColor2(PaletteContentStyle.ButtonInputControl, PaletteState.Normal);
            if (outline == Color.Empty || outline == Color.Empty)
            {
                outline = palette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, state);
            }
        }

        return (outline, NormalizeFill(outline, fill));
    }



    private static (Color Outline, Color Fill) FromContent(IPaletteContent paletteContent, PaletteState state)
    {
        Color outline = paletteContent.GetContentShortTextColor1(state);

        Color fill = paletteContent.GetContentShortTextColor2(state);

        return (outline, NormalizeFill(outline, fill));
    }

    private static Color NormalizeFill(Color outline, Color fill)
    {
        if (fill == Color.Empty || fill == Color.Transparent)
        {
            return Color.FromArgb(64, outline.R, outline.G, outline.B);
        }

        return fill;
    }

    private static bool TryResolvePalette(RenderContext? context, out PaletteBase palette)
    {
        if (context?.Control is VisualControlBase visualControl)
        {
            palette = visualControl.GetResolvedPalette();

            return true;
        }

        palette = KryptonManager.CurrentGlobalPalette;

        return true;
    }

    private static bool TryGetOverrideColor(RenderContext? context, out Color color)
    {
        color = Color.Empty;

        if (context?.Control is KryptonDropButton dropButton)
        {
            Color? arrowColor = dropButton.Values.DropDownArrowColor;

            if (arrowColor.HasValue && arrowColor.Value != Color.Empty)
            {

                color = arrowColor.Value;

                return true;
            }
        }

        return false;
    }
}

