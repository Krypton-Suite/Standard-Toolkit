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
/// Specifies the shape of the value indicator drawn on a <see cref="KryptonKnob"/>.
/// </summary>
public enum KnobIndicatorShape
{
    /// <summary>
    /// A circular inset marker.
    /// </summary>
    Circle,

    /// <summary>
    /// A square marker aligned to the radial direction.
    /// </summary>
    Square,

    /// <summary>
    /// A diamond marker aligned to the radial direction.
    /// </summary>
    Diamond,

    /// <summary>
    /// A triangular pointer aligned to the radial direction.
    /// </summary>
    Triangle,

    /// <summary>
    /// A short radial line segment.
    /// </summary>
    Line,

    /// <summary>
    /// An elongated rectangular needle aligned to the radial direction.
    /// </summary>
    Needle,

    /// <summary>
    /// A flat filled dot without a bevelled inset effect.
    /// </summary>
    Dot,

    /// <summary>
    /// A chevron pointer aligned to the radial direction.
    /// </summary>
    Chevron,

    /// <summary>
    /// A user-defined polygon or <see cref="KryptonKnob.IndicatorCustomPath"/>.
    /// </summary>
    Custom,

    /// <summary>
    /// A raised bar across the knob diameter with a contrasting stripe (industrial panel style).
    /// </summary>
    Bar,

    /// <summary>
    /// A flat dot with an outer glow (lit LED style).
    /// </summary>
    GlowDot
}

/// <summary>
/// Specifies how the <see cref="KryptonKnob"/> face is rendered.
/// </summary>
public enum KnobStyle
{
    /// <summary>
    /// Diagonal gradient ellipse with a border (default).
    /// </summary>
    Classic,

    /// <summary>
    /// Solid flat fill with a border.
    /// </summary>
    Flat,

    /// <summary>
    /// Radial gradient from the centre outward.
    /// </summary>
    Radial,

    /// <summary>
    /// A thick ring with a hollow centre showing the panel background.
    /// </summary>
    Ring,

    /// <summary>
    /// Ellipse with a bevelled 3D highlight.
    /// </summary>
    Bevel,

    /// <summary>
    /// Rounded-square face with a diagonal gradient.
    /// </summary>
    RoundedSquare,

    /// <summary>
    /// Radial industrial panel knob with enhanced depth shading (uses palette face colours).
    /// </summary>
    Industrial
}

/// <summary>
/// Specifies the mounting backplate drawn behind a <see cref="KryptonKnob"/> face.
/// </summary>
public enum KnobBackplateShape
{
    /// <summary>
    /// No backplate is drawn.
    /// </summary>
    None,

    /// <summary>
    /// A square mounting plate with sharp corners.
    /// </summary>
    Square,

    /// <summary>
    /// A square mounting plate with rounded corners.
    /// </summary>
    RoundedSquare,

    /// <summary>
    /// A circular mounting plate.
    /// </summary>
    Circle
}
