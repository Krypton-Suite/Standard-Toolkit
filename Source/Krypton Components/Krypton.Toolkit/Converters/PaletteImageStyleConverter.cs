#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteImageStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteImageStyleConverter : StringLookupConverter<PaletteImageStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteImageStyle, string> _pairs = new Dictionary<PaletteImageStyle, string>
        {
            {PaletteImageStyle.Inherit, KryptonLanguageManager.ImageStyleStrings.Inherit},
            {PaletteImageStyle.Stretch, KryptonLanguageManager.ImageStyleStrings.Stretch},
            {PaletteImageStyle.Tile, KryptonLanguageManager.ImageStyleStrings.Tile},
            {PaletteImageStyle.TileFlipX, KryptonLanguageManager.ImageStyleStrings.TileFlipX},
            {PaletteImageStyle.TileFlipY, KryptonLanguageManager.ImageStyleStrings.TileFlipY},
            {PaletteImageStyle.TileFlipXY, KryptonLanguageManager.ImageStyleStrings.TileFlipXY},
            {PaletteImageStyle.TopLeft, KryptonLanguageManager.ImageStyleStrings.TopLeft},
            {PaletteImageStyle.TopMiddle, KryptonLanguageManager.ImageStyleStrings.TopMiddle},
            {PaletteImageStyle.TopRight, KryptonLanguageManager.ImageStyleStrings.TopRight},
            {PaletteImageStyle.CenterLeft, KryptonLanguageManager.ImageStyleStrings.CenterLeft},
            {PaletteImageStyle.CenterMiddle, KryptonLanguageManager.ImageStyleStrings.CenterMiddle},
            {PaletteImageStyle.CenterRight, KryptonLanguageManager.ImageStyleStrings.CenterRight},
            {PaletteImageStyle.BottomLeft, KryptonLanguageManager.ImageStyleStrings.BottomLeft},
            {PaletteImageStyle.BottomMiddle, KryptonLanguageManager.ImageStyleStrings.BottomMiddle},
            {PaletteImageStyle.BottomRight, KryptonLanguageManager.ImageStyleStrings.BottomRight }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteImageStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
