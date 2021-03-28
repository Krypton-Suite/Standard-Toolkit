namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that DataGridViewStyle values appear as neat text at design time.
    /// </summary>
    internal class DataGridViewStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewStyleConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(DataGridViewStyle.List,       "List"),
            new Pair(DataGridViewStyle.Sheet,      "Sheet"),
            new Pair(DataGridViewStyle.Custom1,    "Custom1"),
            new Pair(DataGridViewStyle.Custom2,    "Custom2"),
            new Pair(DataGridViewStyle.Custom3,    "Custom3"),
            new Pair(DataGridViewStyle.Mixed,      "Mixed")

        };

        #endregion
    }
}
