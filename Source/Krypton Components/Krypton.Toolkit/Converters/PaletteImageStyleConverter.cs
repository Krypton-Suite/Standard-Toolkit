// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteImageStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteImageStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteImageStyleConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(PaletteImageStyle.Inherit,        "Inherit"),
            new Pair(PaletteImageStyle.Stretch,        "Stretch"),
            new Pair(PaletteImageStyle.Tile,           "Tile"),
            new Pair(PaletteImageStyle.TileFlipX,      "TileFlip - X"),
            new Pair(PaletteImageStyle.TileFlipY,      "TileFlip - Y"),
            new Pair(PaletteImageStyle.TileFlipXY,     "TileFlip - XY"),
            new Pair(PaletteImageStyle.TopLeft,        "Top - Left"),
            new Pair(PaletteImageStyle.TopMiddle,      "Top - Middle"),
            new Pair(PaletteImageStyle.TopRight,       "Top - Right"),
            new Pair(PaletteImageStyle.CenterLeft,     "Center - Left"),
            new Pair(PaletteImageStyle.CenterMiddle,   "Center - Middle"),
            new Pair(PaletteImageStyle.CenterRight,    "Center - Right"),
            new Pair(PaletteImageStyle.BottomLeft,     "Bottom - Left"),
            new Pair(PaletteImageStyle.BottomMiddle,   "Bottom - Middle"),
            new Pair(PaletteImageStyle.BottomRight,    "Bottom - Right") };

        #endregion
    }
}
