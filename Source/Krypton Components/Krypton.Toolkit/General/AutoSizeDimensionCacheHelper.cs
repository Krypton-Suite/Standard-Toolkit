#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared helpers for caching/restoring dimensions when AutoSize is toggled.
/// </summary>
internal static class AutoSizeDimensionCacheHelper
{
    /// <summary>
    /// Caches a bounds dimension when it is explicitly specified.
    /// </summary>
    /// <param name="specified">Bounds flags provided to SetBoundsCore.</param>
    /// <param name="dimension">Dimension to test for (Width/Height).</param>
    /// <param name="value">Incoming dimension value.</param>
    /// <param name="cachedValue">Stored cached value.</param>
    public static void CacheIfSpecified(BoundsSpecified specified, BoundsSpecified dimension, int value, ref int cachedValue)
    {
        if ((specified & dimension) == dimension)
        {
            cachedValue = value;
        }
    }

    /// <summary>
    /// Stores the current manual dimension before enabling AutoSize.
    /// </summary>
    /// <param name="autoSizeTargetValue">Target AutoSize value being applied.</param>
    /// <param name="currentValue">Current dimension value on the control.</param>
    /// <param name="cachedValue">Stored cached value.</param>
    public static void CacheCurrentBeforeEnable(bool autoSizeTargetValue, int currentValue, ref int cachedValue)
    {
        if (autoSizeTargetValue)
        {
            cachedValue = currentValue;
        }
    }

    /// <summary>
    /// Returns true when a cached dimension is available for restoring.
    /// </summary>
    /// <param name="cachedValue">Cached dimension value.</param>
    /// <param name="restoredValue">Value to restore.</param>
    /// <returns>True if a positive cached value exists.</returns>
    public static bool TryGetCachedValue(int cachedValue, out int restoredValue)
    {
        restoredValue = cachedValue;
        return cachedValue > 0;
    }
}
