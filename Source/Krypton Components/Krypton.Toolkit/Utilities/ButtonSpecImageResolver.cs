#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Selects the best ButtonSpec source image for the current DPI and scales to the display size (Issue #978).
/// </summary>
public static class ButtonSpecImageResolver
{
    /// <summary>
    /// Resolves and scales a ButtonSpec image for display at the given DPI factor.
    /// </summary>
    /// <param name="baseline">Baseline image (typically 16 logical pixels at 96 DPI).</param>
    /// <param name="scale2x">Optional 200% source (e.g. 32 pixel art).</param>
    /// <param name="scale3x">Optional 300% source (e.g. 48 pixel art).</param>
    /// <param name="dpiFactorX">Per-monitor horizontal DPI factor (DeviceDpi / 96).</param>
    /// <param name="dpiFactorY">Per-monitor vertical DPI factor.</param>
    /// <param name="extraScaleFactor">Additional scale (e.g. touchscreen factor).</param>
    /// <param name="logicalWidth">Logical width at 96 DPI when baseline size is not authoritative.</param>
    /// <param name="logicalHeight">Logical height at 96 DPI when baseline size is not authoritative.</param>
    /// <returns>Bitmap sized for display, or null when the target size is invalid.</returns>
    public static Image? ResolveForDpi(Image? baseline,
        Image? scale2x,
        Image? scale3x,
        float dpiFactorX,
        float dpiFactorY,
        float extraScaleFactor,
        float logicalWidth,
        float logicalHeight)
    {
        if (baseline == null)
        {
            return null;
        }

        if (logicalWidth <= 0f)
        {
            logicalWidth = baseline.Width;
        }

        if (logicalHeight <= 0f)
        {
            logicalHeight = baseline.Height;
        }

        if (dpiFactorX <= 0f)
        {
            dpiFactorX = 1f;
        }

        if (dpiFactorY <= 0f)
        {
            dpiFactorY = 1f;
        }

        if (extraScaleFactor <= 0f)
        {
            extraScaleFactor = 1f;
        }

        float targetW = logicalWidth * dpiFactorX * extraScaleFactor;
        float targetH = logicalHeight * dpiFactorY * extraScaleFactor;

        Image? source = PickBestSource(baseline, scale2x, scale3x, targetW, targetH);
        if (source == null)
        {
            return null;
        }

        if (Math.Abs(source.Width - targetW) < 0.5f && Math.Abs(source.Height - targetH) < 0.5f)
        {
            return source;
        }

        bool downscale = source.Width > targetW + 0.5f || source.Height > targetH + 0.5f;
        bool avoidPurple = !downscale;
        return CommonHelper.ScaleImageForSizedDisplay(source, targetW, targetH, avoidPurple);
    }

    /// <summary>
    /// Picks the smallest source whose dimensions are at least the target; otherwise the largest available.
    /// </summary>
    internal static Image? PickBestSource(Image? baseline, Image? scale2x, Image? scale3x, float targetW, float targetH)
    {
        Image? best = null;
        int bestArea = int.MaxValue;

        Consider(baseline, targetW, targetH, ref best, ref bestArea);
        Consider(scale2x, targetW, targetH, ref best, ref bestArea);
        Consider(scale3x, targetW, targetH, ref best, ref bestArea);

        if (best != null)
        {
            return best;
        }

        if (scale3x != null)
        {
            return scale3x;
        }

        if (scale2x != null)
        {
            return scale2x;
        }

        return baseline;
    }

    /// <summary>
    /// True when dedicated 200% artwork is registered for a built-in baseline image (Issue #978).
    /// </summary>
    /// <param name="baselineImage">Baseline palette image at 96 DPI.</param>
    public static bool HasDedicatedScale2xSource(Image? baselineImage) =>
        ButtonSpecDpiImageRegistry.HasDedicatedScale2x(baselineImage);

    /// <summary>
    /// Resolves a palette button spec image for display at the current DPI.
    /// </summary>
    public static Image? ResolveFromPalette(PaletteBase palette,
        PaletteButtonSpecStyle style,
        PaletteState state,
        float dpiFactor,
        float extraScaleFactor,
        float logicalWidth,
        float logicalHeight)
    {
        Image? baseline = palette.GetButtonSpecImage(style, state);
        Image? scale2x = palette.GetButtonSpecImageScale2(style, state);
        Image? scale3x = palette.GetButtonSpecImageScale3(style, state);
        return ResolveForDpi(baseline, scale2x, scale3x, dpiFactor, dpiFactor, extraScaleFactor, logicalWidth,
            logicalHeight);
    }

    private static void Consider(Image? candidate, float targetW, float targetH, ref Image? best, ref int bestArea)
    {
        if (candidate == null)
        {
            return;
        }

        if (candidate.Width < targetW - 0.5f || candidate.Height < targetH - 0.5f)
        {
            return;
        }

        int area = candidate.Width * candidate.Height;
        if (area < bestArea)
        {
            bestArea = area;
            best = candidate;
        }
    }
}
