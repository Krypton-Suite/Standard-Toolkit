namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that GridStyle values appear as neat text at design time.
    /// </summary>
    internal class GridStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(GridStyle.List, "List"),
            new(GridStyle.Sheet, "Sheet"),
            new(GridStyle.Custom1, "Custom1"),
            new(GridStyle.Custom2, "Custom2"),
            new(GridStyle.Custom3, "Custom3")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GridStyleConverter class.
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
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
