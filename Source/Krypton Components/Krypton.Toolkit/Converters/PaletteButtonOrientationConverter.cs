namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteButtonOrientation values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonOrientationConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(PaletteButtonOrientation.Inherit, "Inherit"),
            new(PaletteButtonOrientation.Auto, "Auto"),
            new(PaletteButtonOrientation.FixedTop, "Fixed Top"),
            new(PaletteButtonOrientation.FixedBottom, "Fixed Bottom"),
            new(PaletteButtonOrientation.FixedLeft, "Fixed Left"),
            new(PaletteButtonOrientation.FixedRight, "Fixed Right")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteButtonOrientation class.
        /// </summary>
        public PaletteButtonOrientationConverter()
            : base(typeof(PaletteButtonOrientation))
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
