namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that KryptonLinkBehavior values appear as neat text at design time.
    /// </summary>
    internal class KryptonLinkBehaviorConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(KryptonLinkBehavior.AlwaysUnderline, "Always Underline"),
            new(KryptonLinkBehavior.HoverUnderline, "Hover Underline"),
            new(KryptonLinkBehavior.NeverUnderline, "Never Underline")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonLinkBehaviorConverter class.
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
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
