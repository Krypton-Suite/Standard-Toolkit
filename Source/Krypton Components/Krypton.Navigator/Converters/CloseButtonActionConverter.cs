namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that CloseButtonAction values appear as neat text at design time.
    /// </summary>
    public class CloseButtonActionConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseButtonActionConverter class.
        /// </summary>
        public CloseButtonActionConverter()
            : base(typeof(CloseButtonAction))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(CloseButtonAction.None,                   "None (Do nothing)"),
            new(CloseButtonAction.RemovePage,             "RemovePage"),
            new(CloseButtonAction.RemovePageAndDispose,   "RemovePage & Dispose"),
            new(CloseButtonAction.HidePage,               "Hide Page") };

        #endregion
    }
}
