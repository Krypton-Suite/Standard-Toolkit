#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Custom type converter so that PaletteButtonOrientation values appear as neat text at design time.
/// </summary>
internal class PaletteButtonOrientationConverter : StringLookupConverter<PaletteButtonOrientation>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteButtonOrientation, string> _pairs = new BiDictionary<PaletteButtonOrientation, string>(
        new Dictionary<PaletteButtonOrientation, string>
        {
            {PaletteButtonOrientation.Inherit, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_ORIENTATION_INHERIT},
            {PaletteButtonOrientation.Auto, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_ORIENTATION_AUTO},
            {PaletteButtonOrientation.FixedTop, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_TOP},
            {PaletteButtonOrientation.FixedBottom, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_BOTTOM},
            {PaletteButtonOrientation.FixedLeft, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_LEFT},
            {PaletteButtonOrientation.FixedRight, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_ORIENTATION_FIXED_RIGHT}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteButtonOrientation /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteButtonOrientation /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}