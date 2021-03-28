using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Details providing a KryptonContextMenu instance.
    /// </summary>
    public class KryptonContextMenuEventArgs : KryptonPageEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        /// <param name="contextMenu">Prepopulated context menu ready for display.</param>
        public KryptonContextMenuEventArgs(KryptonPage page, 
                                           int index,
                                           KryptonContextMenu contextMenu)
            : base(page, index)
        {
            KryptonContextMenu = contextMenu;
        }
        #endregion

        #region KryptonContextMenu
        /// <summary>
        /// Gets access to the KryptonContextMenu that is to be shown.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        #endregion
    }
}
