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
    internal class PaletteImageStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(PaletteImageStyle.Inherit,        "Inherit"),
        //    new(PaletteImageStyle.Stretch,        "Stretch"),
        //    new(PaletteImageStyle.Tile,           "Tile"),
        //    new(PaletteImageStyle.TileFlipX,      "TileFlip - X"),
        //    new(PaletteImageStyle.TileFlipY,      "TileFlip - Y"),
        //    new(PaletteImageStyle.TileFlipXY,     "TileFlip - XY"),
        //    new(PaletteImageStyle.TopLeft,        "Top - Left"),
        //    new(PaletteImageStyle.TopMiddle,      "Top - Middle"),
        //    new(PaletteImageStyle.TopRight,       "Top - Right"),
        //    new(PaletteImageStyle.CenterLeft,     "Center - Left"),
        //    new(PaletteImageStyle.CenterMiddle,   "Center - Middle"),
        //    new(PaletteImageStyle.CenterRight,    "Center - Right"),
        //    new(PaletteImageStyle.BottomLeft,     "Bottom - Left"),
        //    new(PaletteImageStyle.BottomMiddle,   "Bottom - Middle"),
        //    new(PaletteImageStyle.BottomRight,    "Bottom - Right")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new Pair(PaletteImageStyle.Inherit, KryptonLanguageManager.ImageStyleStrings.Inherit),
            new Pair(PaletteImageStyle.Stretch, KryptonLanguageManager.ImageStyleStrings.Stretch),
            new Pair(PaletteImageStyle.Tile, KryptonLanguageManager.ImageStyleStrings.Tile),
            new Pair(PaletteImageStyle.TileFlipX, KryptonLanguageManager.ImageStyleStrings.TileFlipX),
            new Pair(PaletteImageStyle.TileFlipY, KryptonLanguageManager.ImageStyleStrings.TileFlipY),
            new Pair(PaletteImageStyle.TileFlipXY, KryptonLanguageManager.ImageStyleStrings.TileFlipXY),
            new Pair(PaletteImageStyle.TopLeft, KryptonLanguageManager.ImageStyleStrings.TopLeft),
            new Pair(PaletteImageStyle.TopMiddle, KryptonLanguageManager.ImageStyleStrings.TopMiddle),
            new Pair(PaletteImageStyle.TopRight, KryptonLanguageManager.ImageStyleStrings.TopRight),
            new Pair(PaletteImageStyle.CenterLeft, KryptonLanguageManager.ImageStyleStrings.CenterLeft),
            new Pair(PaletteImageStyle.CenterMiddle, KryptonLanguageManager.ImageStyleStrings.CenterMiddle),
            new Pair(PaletteImageStyle.CenterRight, KryptonLanguageManager.ImageStyleStrings.CenterRight),
            new Pair(PaletteImageStyle.BottomLeft, KryptonLanguageManager.ImageStyleStrings.BottomLeft),
            new Pair(PaletteImageStyle.BottomMiddle, KryptonLanguageManager.ImageStyleStrings.BottomMiddle),
            new Pair(PaletteImageStyle.BottomRight, KryptonLanguageManager.ImageStyleStrings.BottomRight)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteImageStyleConverter class.
        /// </summary>
        public PaletteImageStyleConverter()
            : base(typeof(PaletteImageStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
