namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for cancellable events that need to provide a unique name and context menu.
    /// </summary>
    public class CancelDropDownEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CancelDropDownEventArgs class.
        /// </summary>
        /// <param name="contextMenu">Reference to associated context menu.</param>
        /// <param name="page">Reference to the associated page.</param>
        public CancelDropDownEventArgs(KryptonContextMenu contextMenu, KryptonPage page)
            : base(false)
        {
            KryptonContextMenu = contextMenu;
            Page = page;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the context menu.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        /// <summary>
        /// Gets a reference to the page.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
