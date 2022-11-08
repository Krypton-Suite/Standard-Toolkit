namespace Krypton.Workspace
{
    /// <summary>
    /// Data associated with a change in the active page.
    /// </summary>
    public class ActivePageChangedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ActivePageChangedEventArgs class.
        /// </summary>
        /// <param name="oldPage">Previous active page value.</param>
        /// <param name="newPage">New active page value.</param>
        public ActivePageChangedEventArgs(KryptonPage oldPage,
                                          KryptonPage newPage)
        {
            OldPage = oldPage;
            NewPage = newPage;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the old page reference.
        /// </summary>
        public KryptonPage OldPage { get; }

        /// <summary>
        /// Gets the new page reference.
        /// </summary>
        public KryptonPage NewPage { get; }

        #endregion
    }
}
