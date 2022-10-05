namespace Krypton.Navigator
{
    /// <summary>
    /// Details for page related events that can be cancelled.
    /// </summary>
    public class KryptonPageCancelEventArgs : KryptonPageEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCancelPageEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        public KryptonPageCancelEventArgs(KryptonPage page, int index)
            : base(page, index)
        {
        }
        #endregion

        #region Cancel
        /// <summary>
        /// Gets the page associated with the event.
        /// </summary>
        public bool Cancel { get; set; }

        #endregion
    }
}
