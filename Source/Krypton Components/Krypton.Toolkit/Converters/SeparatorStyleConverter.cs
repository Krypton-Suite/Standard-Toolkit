namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that SeparatorStyle values appear as neat text at design time.
    /// </summary>
    internal class SeparatorStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(SeparatorStyle.LowProfile, "Low Profile"),
            new(SeparatorStyle.HighProfile, "High Profile"),
            new(SeparatorStyle.HighInternalProfile, "High Internal Profile"),
            new(SeparatorStyle.Custom1, "Custom1"),
            new(SeparatorStyle.Custom2, "Custom2"),
            new(SeparatorStyle.Custom3, "Custom3")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SeparatorStyleConverter class.
        /// </summary>
        public SeparatorStyleConverter()
            : base(typeof(SeparatorStyle))
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
