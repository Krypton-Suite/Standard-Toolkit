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
