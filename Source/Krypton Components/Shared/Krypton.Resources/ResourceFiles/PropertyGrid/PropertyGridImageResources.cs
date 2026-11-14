#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.ResourceFiles.PropertyGrid;

/// <summary>
/// Builtin property grid toolbar images for palette themes.
/// </summary>
/// <remarks>
/// All members currently return <see langword="null"/> so property grids fall back to
/// <c>KryptonManager.Images.PropertyGridImages</c> then stock WinForms icons.
/// When theme art is ready, add 16×16 PNGs under <c>Resources/</c>, convert this type to a
/// <c>.resx</c> / Designer pair (same pattern as <c>TreeItemImageResources</c>), and keep the
/// property names below.
/// </remarks>
internal static class PropertyGridImageResources
{
    // TODO: Implement PropertyGrid toolbar artwork (16×16 PNGs) for Alphabetical, Categorized,
    // PropertyPages, and Events; convert this class to a .resx / Designer pair like TreeItemImageResources.

    /// <summary>Alphabetical (A-Z) sort toolbar image, or null for stock WinForms.</summary>
    // TODO: PropertyGrid toolbar artwork — Alphabetical (16×16).
    internal static Image? Alphabetical => null;

    /// <summary>Categorized sort toolbar image, or null for stock WinForms.</summary>
    // TODO: PropertyGrid toolbar artwork — Categorized (16×16).
    internal static Image? Categorized => null;

    /// <summary>Property pages toolbar image, or null for stock WinForms.</summary>
    // TODO: PropertyGrid toolbar artwork — PropertyPages (16×16).
    internal static Image? PropertyPages => null;

    /// <summary>Events (lightning bolt) toolbar image, or null for stock WinForms.</summary>
    // TODO: PropertyGrid toolbar artwork — Events (16×16).
    internal static Image? Events => null;
}
