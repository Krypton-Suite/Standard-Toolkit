namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a close button action event.
    /// </summary>
    public class CloseActionEventArgs : KryptonPageEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseActionEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        /// <param name="action">Close action to take.</param>
        public CloseActionEventArgs(KryptonPage page, 
                                    int index, 
                                    CloseButtonAction action)
            : base(page, index)
        {
            Action = action;
        }
        #endregion

        #region Action
        /// <summary>
        /// Gets and sets the close action to take.
        /// </summary>
        public CloseButtonAction Action { get; set; }

        #endregion
    }
}
