namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteImageEffect values appear as neat text at design time.
    /// </summary>
    internal class PaletteImageEffectConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(PaletteImageEffect.Inherit, "Inherit"),
            new(PaletteImageEffect.Light, "Light"),
            new(PaletteImageEffect.LightLight, "LightLight"),
            new(PaletteImageEffect.Normal, "Normal"),
            new(PaletteImageEffect.Disabled, "Disabled"),
            new(PaletteImageEffect.Dark, "Dark"),
            new(PaletteImageEffect.DarkDark, "DarkDark"),
            new(PaletteImageEffect.GrayScale, "GrayScale"),
            new(PaletteImageEffect.GrayScaleRed, "GrayScale - Red"),
            new(PaletteImageEffect.GrayScaleGreen, "GrayScale - Green"),
            new(PaletteImageEffect.GrayScaleBlue, "GrayScale - Blue")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteImageEffectConverter class.
        /// </summary>
        public PaletteImageEffectConverter()
            : base(typeof(PaletteImageEffect))
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
