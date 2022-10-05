namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteTextTrim values appear as neat text at design time.
    /// </summary>
    internal class PaletteTextTrimConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(PaletteTextTrim.Inherit, "Inherit"),
            new(PaletteTextTrim.Hide, "Hide"),
            new(PaletteTextTrim.Character, "Character"),
            new(PaletteTextTrim.Word, "Word"),
            new(PaletteTextTrim.EllipsisCharacter, "Ellipsis Character"),
            new(PaletteTextTrim.EllipsisWord, "Ellipsis Word"),
            new(PaletteTextTrim.EllipsisPath, "Ellipsis Path")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTextTrimConverter class.
        /// </summary>
        public PaletteTextTrimConverter()
            : base(typeof(PaletteTextTrim))
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
