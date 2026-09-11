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
/// Seed colours and donor family used by <see cref="KryptonCustomThemeGenerator"/> to build a custom palette.
/// </summary>
/// <remarks>
/// Only <see cref="Primary"/> is required. When <see cref="Secondary"/> or <see cref="Surface"/> are omitted,
/// the generator derives them from the primary hue (analogous accent and a light or dark wash).
/// Supported donors are listed on <see cref="KryptonCustomThemeGenerator.SupportedDonorModes"/>.
/// </remarks>
public sealed class KryptonCustomThemeSeed
{
    /// <summary>
    /// Gets or sets the display name used in theme selectors and XML export.
    /// </summary>
    public string Name { get; set; } = @"Custom Theme";

    /// <summary>
    /// Gets or sets the required brand / accent colour.
    /// </summary>
    public Color Primary { get; set; } = Color.FromArgb(0x00, 0x78, 0xD4);

    /// <summary>
    /// Gets or sets an optional secondary accent (headers / navigation). When <c>null</c>, an analogous hue is derived.
    /// </summary>
    public Color? Secondary { get; set; }

    /// <summary>
    /// Gets or sets an optional panel / client surface colour. When <c>null</c>, a light or dark tint of primary is used.
    /// </summary>
    public Color? Surface { get; set; }

    /// <summary>
    /// Gets or sets the builtin donor whose chrome shape (gradients, rounding, glyphs) is preserved.
    /// </summary>
    public PaletteMode DonorMode { get; set; } = PaletteMode.Office2010Blue;

    /// <summary>
    /// Creates a copy of this seed so later edits do not affect a registered factory.
    /// </summary>
    /// <returns>A new seed with the same values.</returns>
    public KryptonCustomThemeSeed Clone() =>
        new KryptonCustomThemeSeed
        {
            Name = Name,
            Primary = Primary,
            Secondary = Secondary,
            Surface = Surface,
            DonorMode = DonorMode
        };
}
