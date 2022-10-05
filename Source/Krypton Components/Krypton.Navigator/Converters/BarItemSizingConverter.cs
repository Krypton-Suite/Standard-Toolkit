namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that BarItemSizing values appear as neat text at design time.
    /// </summary>
    public class BarItemSizingConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the BarItemSizingConverter class.
        /// </summary>
        public BarItemSizingConverter()
            : base(typeof(BarItemSizing))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(BarItemSizing.Individual,           "Individual Sizing"),
            new(BarItemSizing.SameHeight,           "All Same Height"),
            new(BarItemSizing.SameWidth,            "All Same Width"),
            new(BarItemSizing.SameWidthAndHeight,   "All Same Width & Height") };

        #endregion
    }
}
