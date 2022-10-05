namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need to provide a colletion of pages.
    /// </summary>
    public class PagesEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PagesEventArgs class.
        /// </summary>
        /// <param name="pages">Collection of pages.</param>
        public PagesEventArgs(KryptonPageCollection pages) => Pages = pages;

        #endregion

        #region Public
        /// <summary>
        /// Gets access to a collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

        #endregion
    }
}
