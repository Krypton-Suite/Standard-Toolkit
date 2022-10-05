namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a close button action event.
    /// </summary>
    public class ShowContextMenuArgs : KryptonPageCancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ShowContextMenuArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        public ShowContextMenuArgs(KryptonPage page, int index)
            : base(page, index)
        {
            ContextMenuStrip = page.ContextMenuStrip;
            KryptonContextMenu = page.KryptonContextMenu;
        }
        #endregion

        #region ContextMenuStrip
        /// <summary>
        /// Gets and sets the context menu strip.
        /// </summary>
        public ContextMenuStrip ContextMenuStrip { get; set; }

        #endregion

        #region KryptonContextMenu
        /// <summary>
        /// Gets and sets the context menu strip.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; set; }

        #endregion
    }
}
