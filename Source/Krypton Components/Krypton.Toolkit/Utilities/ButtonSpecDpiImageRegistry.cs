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
/// Supplies optional high-DPI source images for built-in button spec baseline images (Issue #978).
/// </summary>
internal static class ButtonSpecDpiImageRegistry
{
    private static readonly Dictionary<Image, Image> _lazyScale2x = new(ReferenceImageEqualityComparer.Instance);
    private static readonly Dictionary<Image, Image> _lazyScale3x = new(ReferenceImageEqualityComparer.Instance);
    private static readonly Dictionary<Image, Image> _dedicatedScale2x = new(ReferenceImageEqualityComparer.Instance);
    private static readonly Dictionary<Image, Image> _dedicatedScale3x = new(ReferenceImageEqualityComparer.Instance);

    /// <summary>
    /// Gets the 200% source image for a button spec baseline.
    /// </summary>
    /// <param name="baseline">Baseline palette image at 96 DPI.</param>
    /// <param name="style">Unused; retained for call-site compatibility.</param>
    public static Image? GetScale2x(Image? baseline, PaletteButtonSpecStyle? style = null)
    {
        if (baseline != null && _dedicatedScale2x.TryGetValue(baseline, out Image? dedicated2x))
        {
            return dedicated2x;
        }

        return GetLazyScaled(baseline, 2f, _lazyScale2x);
    }

    /// <summary>
    /// Gets the 300% source image for a button spec baseline.
    /// </summary>
    /// <param name="baseline">Baseline palette image at 96 DPI.</param>
    /// <param name="style">Unused; retained for call-site compatibility.</param>
    public static Image? GetScale3x(Image? baseline, PaletteButtonSpecStyle? style = null)
    {
        if (baseline != null && _dedicatedScale3x.TryGetValue(baseline, out Image? dedicated3x))
        {
            return dedicated3x;
        }

        return GetLazyScaled(baseline, 3f, _lazyScale3x);
    }

    /// <summary>
    /// Registers dedicated 200% artwork for a baseline image from ResourceFiles.
    /// </summary>
    public static void RegisterScale2x(Image baseline, Image image) =>
        _dedicatedScale2x[baseline] = image;

    /// <summary>
    /// Registers dedicated 300% artwork for a baseline image from ResourceFiles.
    /// </summary>
    public static void RegisterScale3x(Image baseline, Image image) =>
        _dedicatedScale3x[baseline] = image;

    /// <summary>
    /// Clears lazily created placeholder images (e.g. after DPI or palette changes).
    /// </summary>
    public static void InvalidateCache()
    {
        DisposeCache(_lazyScale2x);
        DisposeCache(_lazyScale3x);
    }

    private static Image? GetLazyScaled(Image? baseline, float multiplier, Dictionary<Image, Image> cache)
    {
        if (baseline == null)
        {
            return null;
        }

        if (cache.TryGetValue(baseline, out Image? cached))
        {
            return cached;
        }

        float w = baseline.Width * multiplier;
        float h = baseline.Height * multiplier;
        Image? scaled = CommonHelper.ScaleImageForSizedDisplay(baseline, w, h, avoidPurple: false);
        if (scaled != null)
        {
            cache[baseline] = scaled;
        }

        return scaled;
    }

    private static void DisposeCache(Dictionary<Image, Image> cache)
    {
        foreach (Image image in cache.Values)
        {
            image.Dispose();
        }

        cache.Clear();
    }

    private sealed class ReferenceImageEqualityComparer : IEqualityComparer<Image>
    {
        public static ReferenceImageEqualityComparer Instance { get; } = new();

        public bool Equals(Image? x, Image? y) => ReferenceEquals(x, y);

        public int GetHashCode(Image obj) => RuntimeHelpers.GetHashCode(obj);
    }
}

