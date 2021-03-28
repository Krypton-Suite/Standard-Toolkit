namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that HeaderGroupCollapseTarget values appear as neat text at design time.
    /// </summary>
    internal class HeaderGroupCollapsedTargetConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderGroupCollapseTargetConverter clas.
        /// </summary>
        public HeaderGroupCollapsedTargetConverter()
            : base(typeof(HeaderGroupCollapsedTarget))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(HeaderGroupCollapsedTarget.CollapsedToPrimary,   "Collapse to Primary Header"),
            new Pair(HeaderGroupCollapsedTarget.CollapsedToSecondary, "Collapse to Secondary Header"),
            new Pair(HeaderGroupCollapsedTarget.CollapsedToBoth,      "Collapse to Both Headers") };

        #endregion
    }
}
