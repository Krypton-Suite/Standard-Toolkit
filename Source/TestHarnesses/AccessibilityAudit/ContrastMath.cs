#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Drawing;

namespace AccessibilityAudit;

/// <summary>
/// WCAG 2 relative luminance and contrast ratio.
/// </summary>
internal static class ContrastMath
{
    internal const double NormalTextThreshold = 4.5;

    internal const double LargeTextThreshold = 3.0;

    internal static void AssertSelfCheck()
    {
        double blackOnWhite = ContrastRatio(Color.Black, Color.White);
        double whiteOnBlack = ContrastRatio(Color.White, Color.Black);
        double identical = ContrastRatio(Color.FromArgb(255, 12, 34, 56), Color.FromArgb(255, 12, 34, 56));

        if (Math.Abs(blackOnWhite - 21.0) > 0.05 || Math.Abs(whiteOnBlack - 21.0) > 0.05)
        {
            throw new InvalidOperationException(
                $"Contrast self-check failed. Black on white was {blackOnWhite:0.###}:1 and white on black was {whiteOnBlack:0.###}:1. Expected 21:1.");
        }

        if (Math.Abs(identical - 1.0) > 0.001)
        {
            throw new InvalidOperationException(
                $"Contrast self-check failed. Identical colors scored {identical:0.###}:1. Expected 1:1.");
        }
    }

    internal static bool IsOpaque(Color color) => !color.IsEmpty && color.A == 255;

    internal static bool IsLargeText(Font font)
    {
        float points = font.SizeInPoints;
        return points >= 18f || (font.Bold && points >= 14f);
    }

    internal static double ContrastRatio(Color first, Color second)
    {
        double lighter = Math.Max(RelativeLuminance(first), RelativeLuminance(second));
        double darker = Math.Min(RelativeLuminance(first), RelativeLuminance(second));
        return (lighter + 0.05) / (darker + 0.05);
    }

    internal static string ToHex(Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";

    private static double RelativeLuminance(Color color)
    {
        double r = Linearize(color.R / 255.0);
        double g = Linearize(color.G / 255.0);
        double b = Linearize(color.B / 255.0);
        return (0.2126 * r) + (0.7152 * g) + (0.0722 * b);
    }

    private static double Linearize(double channel) =>
        channel <= 0.03928
            ? channel / 12.92
            : Math.Pow((channel + 0.055) / 1.055, 2.4);
}
