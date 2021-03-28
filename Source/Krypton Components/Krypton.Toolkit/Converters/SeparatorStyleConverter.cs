namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that SeparatorStyle values appear as neat text at design time.
    /// </summary>
    internal class SeparatorStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SeparatorStyleConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(SeparatorStyle.LowProfile,            "Low Profile"),
            new Pair(SeparatorStyle.HighProfile,           "High Profile"),  
            new Pair(SeparatorStyle.HighInternalProfile,   "High Internal Profile"),  
            new Pair(SeparatorStyle.Custom1,               "Custom1"),
            new Pair(SeparatorStyle.Custom2,               "Custom2"),
            new Pair(SeparatorStyle.Custom3,               "Custom3")

        };

        #endregion
    }
}
