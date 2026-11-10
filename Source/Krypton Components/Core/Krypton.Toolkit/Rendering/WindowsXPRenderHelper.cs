#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared colour helpers for Windows XP visual-style renderers.
/// </summary>
internal static class WindowsXPRenderHelper
{
    internal static float Clamp01(float amount) => amount < 0f ? 0f : (amount > 1f ? 1f : amount);

    internal static int Clamp(int value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }

        return value > max ? max : value;
    }

    internal static Color Lighten(Color color, float amount)
    {
        amount = Clamp01(amount);
        return Color.FromArgb(color.A,
            color.R + (int)((255 - color.R) * amount),
            color.G + (int)((255 - color.G) * amount),
            color.B + (int)((255 - color.B) * amount));
    }

    internal static Color Darken(Color color, float amount)
    {
        amount = Clamp01(amount);
        return Color.FromArgb(color.A,
            (int)(color.R * (1f - amount)),
            (int)(color.G * (1f - amount)),
            (int)(color.B * (1f - amount)));
    }
}
