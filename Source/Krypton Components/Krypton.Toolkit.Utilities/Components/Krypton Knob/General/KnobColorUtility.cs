#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shared colour helpers for disabled knob rendering.
/// </summary>
internal static class KnobColorUtility
{
    /// <summary>
    /// Converts a colour to a washed-out grey tone suitable for disabled knob elements.
    /// </summary>
    /// <param name="color">Source colour.</param>
    /// <returns>Desaturated light-grey equivalent.</returns>
    public static Color GetDisabledColor(Color color)
    {
        if (color == GlobalStaticVariables.EMPTY_COLOR)
        {
            return color;
        }

        // Remove hue entirely and lighten toward a flat disabled grey so the result is
        // visibly inactive regardless of what the palette returns for the source element.
        var luminance = color.R * 0.299 + color.G * 0.587 + color.B * 0.114;
        var grey = (byte)Math.Min(255, luminance * 0.4 + 160);

        return Color.FromArgb(color.A, grey, grey, grey);
    }

    /// <summary>
    /// Returns backplate settings with colours muted for a disabled control.
    /// </summary>
    /// <param name="settings">Active backplate settings.</param>
    /// <returns>Muted copy of the settings.</returns>
    public static KnobBackplateSettings GetDisabledBackplateSettings(KnobBackplateSettings settings)
    {
        settings.Color1 = GetDisabledColor(settings.Color1);
        settings.Color2 = GetDisabledColor(settings.Color2);
        settings.BorderColor = GetDisabledColor(settings.BorderColor);
        return settings;
    }

    /// <summary>
    /// Draws a translucent wash over the knob face to reinforce the disabled appearance.
    /// </summary>
    /// <param name="g">Target graphics.</param>
    /// <param name="knobRect">Knob bounding rectangle.</param>
    public static void DrawDisabledWash(Graphics g, Rectangle knobRect)
    {
        if (knobRect.Width <= 0 || knobRect.Height <= 0)
        {
            return;
        }

        using var washBrush = new SolidBrush(Color.FromArgb(72, 215, 215, 215));
        g.FillEllipse(washBrush, knobRect);
    }
}
