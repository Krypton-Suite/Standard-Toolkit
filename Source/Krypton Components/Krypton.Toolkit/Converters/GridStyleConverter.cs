namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that GridStyle values appear as neat text at design time.
    /// </summary>
    internal class GridStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GridStyleConverter clas.
        /// </summary>
        public GridStyleConverter()
            : base(typeof(GridStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(GridStyle.List,       "List"),
            new Pair(GridStyle.Sheet,      "Sheet"),
            new Pair(GridStyle.Custom1,    "Custom1"),
            new Pair(GridStyle.Custom2,    "Custom2"),
            new Pair(GridStyle.Custom3,    "Custom3")
        };

        #endregion
    }
}
