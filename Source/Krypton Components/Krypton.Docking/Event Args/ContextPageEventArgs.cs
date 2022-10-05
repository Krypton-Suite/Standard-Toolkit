namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need a page and context menu.
    /// </summary>
    public class ContextPageEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextPageEventArgs class.
        /// </summary>
        /// <param name="page">Page associated with the context menu.</param>
        /// <param name="contextMenu">Context menu that can be customized.</param>
        /// <param name="cancel">Initial value for the cancel property.</param>
        public ContextPageEventArgs(KryptonPage page, 
                                    KryptonContextMenu contextMenu,
                                    bool cancel)
            : base(cancel)
        {
            Page = page;
            KryptonContextMenu = contextMenu;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to page associated with the context menu.
        /// </summary>
        public KryptonPage Page { get; }

        /// <summary>
        /// Gets access to context menu that can be customized.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        #endregion
    }
}
