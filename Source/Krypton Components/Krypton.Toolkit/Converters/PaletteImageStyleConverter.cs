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
/// Custom type converter so that PaletteImageStyle values appear as neat text at design time.
/// </summary>
internal class PaletteImageStyleConverter : StringLookupConverter<PaletteImageStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteImageStyle, string> _pairs = new BiDictionary<PaletteImageStyle, string>(
        new Dictionary<PaletteImageStyle, string>
        {
            {PaletteImageStyle.Inherit, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_INHERIT},
            {PaletteImageStyle.Stretch, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_STRETCH},
            {PaletteImageStyle.Tile, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_TILE},
            {PaletteImageStyle.TileFlipX, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X},
            {PaletteImageStyle.TileFlipY, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_Y},
            {PaletteImageStyle.TileFlipXY, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_TILE_FLIP_X_Y},
            {PaletteImageStyle.TopLeft, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_TOP_LEFT},
            {PaletteImageStyle.TopMiddle, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_TOP_MIDDLE},
            {PaletteImageStyle.TopRight, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_TOP_RIGHT},
            {PaletteImageStyle.CenterLeft, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_CENTER_LEFT},
            {PaletteImageStyle.CenterMiddle, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_CENTER_MIDDLE},
            {PaletteImageStyle.CenterRight, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_CENTER_RIGHT},
            {PaletteImageStyle.BottomLeft, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_LEFT},
            {PaletteImageStyle.BottomMiddle, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_MIDDLE},
            {PaletteImageStyle.BottomRight, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_STYLE_BOTTOM_RIGHT}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteImageStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteImageStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}