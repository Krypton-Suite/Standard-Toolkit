#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Aviles (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Specifies how <see cref="KryptonRating"/> snaps and fills glyph values.
/// </summary>
public enum KryptonRatingPrecision
{
    /// <summary>
    /// Snap to whole glyphs. Clicking a glyph selects that integer rating.
    /// </summary>
    Full = 0,

    /// <summary>
    /// Snap to half glyphs. The left (or start) half of a glyph is n.5.
    /// </summary>
    Half = 1,

    /// <summary>
    /// Use the click position within a glyph. Values are stored to two decimal places.
    /// </summary>
    Exact = 2
}

/// <summary>
/// Glyph drawn by <see cref="KryptonRating"/>.
/// </summary>
public enum KryptonRatingGlyph
{
    /// <summary>
    /// Vector five-point star (default). Recolours with <see cref="PaletteRatingStates"/>.
    /// </summary>
    Star = 0,

    /// <summary>
    /// Vector heart.
    /// </summary>
    Heart = 1,

    /// <summary>
    /// Vector circle.
    /// </summary>
    Circle = 2,

    /// <summary>
    /// Custom or stock images from <see cref="RatingValues"/>.
    /// </summary>
    Image = 3
}
