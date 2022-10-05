namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a direction button (next/previous) action event.
    /// </summary>
    public class DirectionActionEventArgs : KryptonPageEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DirectionActionEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        /// <param name="action">Previous/Next action to take.</param>
        public DirectionActionEventArgs(KryptonPage page, 
                                        int index,
                                        DirectionButtonAction action)
            : base(page, index) =>
            Action = action;

        #endregion

        #region Action
        /// <summary>
        /// Gets and sets the next/previous action to take.
        /// </summary>
        public DirectionButtonAction Action { get; set; }

        #endregion
    }
}
