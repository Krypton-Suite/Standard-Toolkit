namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for the change in auto hidden page showing state.
    /// </summary>
    public class AutoHiddenShowingStateEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AutoHiddenShowingStateEventArgs class.
        /// </summary>
        /// <param name="page">Page for which state has changed.</param>
        /// <param name="state">New state of the auto hidden page.</param>
        public AutoHiddenShowingStateEventArgs(KryptonPage page, DockingAutoHiddenShowState state)
        {
            Page = page;
            NewState = state;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the page that has had the state change.
        /// </summary>
        public KryptonPage Page { get; }

        /// <summary>
        /// Gets the new state of the auto hidden page.
        /// </summary>
        public DockingAutoHiddenShowState NewState { get; }

        #endregion
    }
}
