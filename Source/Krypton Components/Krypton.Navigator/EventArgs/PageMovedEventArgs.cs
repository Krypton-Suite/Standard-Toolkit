namespace Krypton.Navigator
{
    /// <summary>
    /// Details for an event that provides a new index position for a specified page.
    /// </summary>
    public class TabMovedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabMovedEventArgs class.
        /// </summary>
        /// <param name="page">Reference to page that has been moved.</param>
        /// <param name="index">New index of the page within the page collection.</param>
        public TabMovedEventArgs(KryptonPage page, int index)
        {
            Page = page;
            Index = index;
        }
        #endregion

        #region Dropped
        /// <summary>
        /// Gets a reference to the page that has been moved.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion

        #region Pages
        /// <summary>
        /// Gets the new index of the page within the page collection.
        /// </summary>
        public int Index { get; }

        #endregion
    }
}
