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
/// Adapts a palette double as a palette triple for border rendering helpers.
/// </summary>
internal sealed class PaletteDoubleTripleAdapter : IPaletteTriple
{
    #region Instance Fields

    private readonly Func<IPaletteDouble> _getPalette;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the PaletteDoubleTripleAdapter class.
    /// </summary>
    /// <param name="getPalette">Delegate that returns the current palette double state.</param>
    public PaletteDoubleTripleAdapter(Func<IPaletteDouble> getPalette) => _getPalette = getPalette;

    #endregion

    #region IPaletteTriple

    /// <inheritdoc />
    public IPaletteBack PaletteBack => _getPalette().PaletteBack;

    /// <inheritdoc />
    public IPaletteBorder? PaletteBorder => _getPalette().PaletteBorder;

    /// <inheritdoc />
    public IPaletteContent? PaletteContent => null;

    #endregion
}
