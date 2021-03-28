namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that KryptonLinkBehavior values appear as neat text at design time.
    /// </summary>
    internal class KryptonLinkBehaviorConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonLinkBehaviorConverter clas.
        /// </summary>
        public KryptonLinkBehaviorConverter()
            : base(typeof(KryptonLinkBehavior))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(KryptonLinkBehavior.AlwaysUnderline,  "Always Underline"),
            new Pair(KryptonLinkBehavior.HoverUnderline,   "Hover Underline"),
            new Pair(KryptonLinkBehavior.NeverUnderline,   "Never Underline") };

        #endregion
    }
}
