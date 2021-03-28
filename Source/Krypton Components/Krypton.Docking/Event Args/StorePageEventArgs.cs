using System;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need to provide a store page reference.
    /// </summary>
    public class StorePageEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the StorePageEventArgs class.
        /// </summary>
        /// <param name="storePage">Reference to store page that is associated with the event.</param>
        public StorePageEventArgs(KryptonStorePage storePage)
        {
            StorePage = storePage;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to store page that is associated with the event.
        /// </summary>
        public KryptonStorePage StorePage { get; }

        #endregion
    }
}
