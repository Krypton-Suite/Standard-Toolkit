namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that HeaderGroupCollapseTarget values appear as neat text at design time.
    /// </summary>
    internal class HeaderGroupCollapsedTargetConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(HeaderGroupCollapsedTarget.CollapsedToPrimary, "Collapse to Primary Header"),
            new(HeaderGroupCollapsedTarget.CollapsedToSecondary, "Collapse to Secondary Header"),
            new(HeaderGroupCollapsedTarget.CollapsedToBoth, "Collapse to Both Headers")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderGroupCollapseTargetConverter class.
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
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
