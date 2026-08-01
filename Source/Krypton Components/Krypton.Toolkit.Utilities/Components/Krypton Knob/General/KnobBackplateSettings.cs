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
/// Describes industrial backplate rendering settings.
/// </summary>
public struct KnobBackplateSettings
{
    /// <summary>
    /// Gets or sets the backplate shape.
    /// </summary>
    public KnobBackplateShape Shape;

    /// <summary>
    /// Gets or sets the primary backplate colour.
    /// </summary>
    public Color Color1;

    /// <summary>
    /// Gets or sets the secondary backplate colour.
    /// </summary>
    public Color Color2;

    /// <summary>
    /// Gets or sets the backplate border colour.
    /// </summary>
    public Color BorderColor;

    /// <summary>
    /// Gets or sets whether an inset well is drawn.
    /// </summary>
    public bool ShowInsetWell;

    /// <summary>
    /// Gets or sets whether a drop shadow is drawn under the knob.
    /// </summary>
    public bool ShowDropShadow;
}
