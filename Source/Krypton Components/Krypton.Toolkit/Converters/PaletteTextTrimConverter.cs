namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteTextTrim values appear as neat text at design time.
    /// </summary>
    internal class PaletteTextTrimConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTextTrimConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(PaletteTextTrim.Inherit,              "Inherit"),
            new Pair(PaletteTextTrim.Hide,                 "Hide"),
            new Pair(PaletteTextTrim.Character,            "Character"),
            new Pair(PaletteTextTrim.Word,                 "Word"),
            new Pair(PaletteTextTrim.EllipsisCharacter,    "Ellipsis Character"),
            new Pair(PaletteTextTrim.EllipsisWord,         "Ellipsis Word"),
            new Pair(PaletteTextTrim.EllipsisPath,         "Ellipsis Path") };

        #endregion
    }
}
