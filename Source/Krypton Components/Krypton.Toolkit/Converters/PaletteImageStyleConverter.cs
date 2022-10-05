namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteImageStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteImageStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(PaletteImageStyle.Inherit,        "Inherit"),
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
            new(PaletteImageStyle.BottomRight,    "Bottom - Right") 
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
