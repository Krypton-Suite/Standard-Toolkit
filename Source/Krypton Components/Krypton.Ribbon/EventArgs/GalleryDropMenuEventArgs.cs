using System.ComponentModel;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Event arguments for the drop down menu of a gallery.
    /// </summary>
    public class GalleryDropMenuEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GalleryDropMenuEventArgs class.
        /// </summary>
        /// <param name="contextMenu">Context menu.</param>
        public GalleryDropMenuEventArgs(KryptonContextMenu contextMenu)
        {
            KryptonContextMenu = contextMenu;
        }
        #endregion

        #region Public
        /// <summary>
        /// KryptonContextMenu for display.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        #endregion
    }
}
