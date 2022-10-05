namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that DataGridViewStyle values appear as neat text at design time.
    /// </summary>
    internal class DataGridViewStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(DataGridViewStyle.List, "List"),
            new(DataGridViewStyle.Sheet, "Sheet"),
            new(DataGridViewStyle.Custom1, "Custom1"),
            new(DataGridViewStyle.Custom2, "Custom2"),
            new(DataGridViewStyle.Custom3, "Custom3"),
            new(DataGridViewStyle.Mixed, "Mixed")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewStyleConverter class.
        /// </summary>
        public DataGridViewStyleConverter()
            : base(typeof(DataGridViewStyle))
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
