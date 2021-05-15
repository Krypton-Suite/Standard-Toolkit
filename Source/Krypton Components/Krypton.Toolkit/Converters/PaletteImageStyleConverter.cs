// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
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
        { new(PaletteImageStyle.Inherit,        "Inherit"),
            new(PaletteImageStyle.Stretch,        "Stretch"),
            new(PaletteImageStyle.Tile,           "Tile"),
            new(PaletteImageStyle.TileFlipX,      "TileFlip - X"),
            new(PaletteImageStyle.TileFlipY,      "TileFlip - Y"),
            new(PaletteImageStyle.TileFlipXY,     "TileFlip - XY"),
            new(PaletteImageStyle.TopLeft,        "Top - Left"),
            new(PaletteImageStyle.TopMiddle,      "Top - Middle"),
            new(PaletteImageStyle.TopRight,       "Top - Right"),
            new(PaletteImageStyle.CenterLeft,     "Center - Left"),
            new(PaletteImageStyle.CenterMiddle,   "Center - Middle"),
            new(PaletteImageStyle.CenterRight,    "Center - Right"),
            new(PaletteImageStyle.BottomLeft,     "Bottom - Left"),
            new(PaletteImageStyle.BottomMiddle,   "Bottom - Middle"),
            new(PaletteImageStyle.BottomRight,    "Bottom - Right") };

        #endregion
    }
}
