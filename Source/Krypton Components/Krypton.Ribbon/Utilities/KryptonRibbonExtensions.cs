#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Extension methods for KryptonRibbon to provide built-in merge/unmerge functionality.
/// </summary>
public static class KryptonRibbonExtensions
{
    /// <summary>
    /// Merges the specified ribbon into this ribbon.
    /// </summary>
    /// <param name="targetRibbon">The target ribbon that will receive the merged items.</param>
    /// <param name="sourceRibbon">The ribbon to merge into this ribbon.</param>
    /// <remarks>
    /// <para>
    /// This method merges tabs, groups, and contexts from the source ribbon into the target ribbon.
    /// If a tab with the same name exists, its groups will be merged. If a group with the same name exists, its items will be merged.
    /// </para>
    /// <para>
    /// To affect the order of merged items, set the Tag property to a numeric value indicating the desired position.
    /// </para>
    /// </remarks>
    public static void Merge(this KryptonRibbon targetRibbon, KryptonRibbon? sourceRibbon)
    {
        ArgumentNullException.ThrowIfNull(targetRibbon);

        if (sourceRibbon == null)
        {
            return;
        }

        var merger = new KryptonRibbonMerger(targetRibbon);
        merger.Merge(sourceRibbon);
    }

    /// <summary>
    /// Unmerges the specified ribbon from this ribbon.
    /// </summary>
    /// <param name="targetRibbon">The target ribbon that contains the merged items.</param>
    /// <param name="sourceRibbon">The ribbon to unmerge from this ribbon.</param>
    /// <remarks>
    /// <para>
    /// This method reverses the merge operation, moving tabs, groups, and contexts back to the source ribbon.
    /// </para>
    /// </remarks>
    public static void Unmerge(this KryptonRibbon targetRibbon, KryptonRibbon? sourceRibbon)
    {
        ArgumentNullException.ThrowIfNull(targetRibbon);

        if (sourceRibbon == null)
        {
            return;
        }

        var merger = new KryptonRibbonMerger(targetRibbon);
        merger.Unmerge(sourceRibbon);
    }

    /// <summary>
    /// Creates a ribbon merger instance for this ribbon.
    /// </summary>
    /// <param name="targetRibbon">The target ribbon that will receive merged items.</param>
    /// <returns>A new <see cref="KryptonRibbonMerger"/> instance.</returns>
    /// <remarks>
    /// <para>
    /// Use this method when you need more control over the merge process or want to reuse the same merger instance for multiple operations.
    /// </para>
    /// </remarks>
    public static KryptonRibbonMerger CreateMerger(this KryptonRibbon targetRibbon)
    {
        ArgumentNullException.ThrowIfNull(targetRibbon);

        return new KryptonRibbonMerger(targetRibbon);
    }
}