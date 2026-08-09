#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Enum KryptonRadialMenuDisplayStyle

/// <summary>
/// Display style for radial menu item content.
/// </summary>
public enum KryptonRadialMenuDisplayStyle
{
    /// <summary>Show text only.</summary>
    Text,

    /// <summary>Show image only.</summary>
    Image,

    /// <summary>Show image above text.</summary>
    ImageAboveText,

    /// <summary>Show text above image.</summary>
    TextAboveImage
}

#endregion

#region Enum KryptonRadialMenuAnimationStyle

/// <summary>
/// Open / navigation animation for <see cref="KryptonRadialMenu"/>.
/// </summary>
public enum KryptonRadialMenuAnimationStyle
{
    /// <summary>No animation.</summary>
    None,

    /// <summary>Scale up from slightly smaller (default legacy behaviour).</summary>
    FadeScale,

    /// <summary>Reveal the menu with a clockwise pie sweep.</summary>
    Sweep,

    /// <summary>Rotate while scaling in.</summary>
    Spiral,

    /// <summary>Scale in with a short overshoot (pop).</summary>
    Pop
}

#endregion

#region Enum RadialHitKind

/// <summary>
/// Identifies which hit-test region the pointer is over.
/// </summary>
internal enum RadialHitKind
{
    None,
    Center,
    Sector,
    /// <summary>Outer-ring band of a sector (submenu / editor affordance).</summary>
    OuterRing,
    Editor
}

#endregion

/// <summary>
/// Describes a hit-test result against the radial menu.
/// </summary>
internal readonly struct RadialHitResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RadialHitResult"/> struct.
    /// </summary>
    /// <param name="kind">Hit region kind.</param>
    /// <param name="sectorIndex">Sector index when <paramref name="kind"/> is <see cref="RadialHitKind.Sector"/>; otherwise -1.</param>
    /// <param name="editorIndex">Editor element index when <paramref name="kind"/> is <see cref="RadialHitKind.Editor"/>; otherwise -1.</param>
    public RadialHitResult(RadialHitKind kind, int sectorIndex, int editorIndex)
    {
        Kind = kind;
        SectorIndex = sectorIndex;
        EditorIndex = editorIndex;
    }

    /// <summary>Gets the hit region kind.</summary>
    public RadialHitKind Kind { get; }

    /// <summary>Gets the sector index, or -1.</summary>
    public int SectorIndex { get; }

    /// <summary>Gets the editor index, or -1.</summary>
    public int EditorIndex { get; }

    /// <summary>Gets an empty miss result.</summary>
    public static RadialHitResult None => new RadialHitResult(RadialHitKind.None, -1, -1);
}
