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
/// Provides pulsing border state for input control view rendering.
/// </summary>
internal interface IInputPulsingBorderProvider
{
    /// <summary>
    /// Gets the pulsing border values.
    /// </summary>
    InputPulsingBorderValues Values { get; }

    /// <summary>
    /// Gets the current animation phase.
    /// </summary>
    float AnimationPhase { get; }

    /// <summary>
    /// Gets a value indicating whether the pulsing border should currently be drawn.
    /// </summary>
    bool ShouldDrawGlowingBorder();

    /// <summary>
    /// Gets the palette triple state used to resolve border rounding.
    /// </summary>
    IPaletteTriple GetGlowingBorderTripleState();

    /// <summary>
    /// Gets the palette state used to resolve border rounding.
    /// </summary>
    PaletteState GetGlowingBorderState();
}
