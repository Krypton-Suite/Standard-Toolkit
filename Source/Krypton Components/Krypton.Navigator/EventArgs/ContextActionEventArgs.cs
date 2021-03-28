using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a context button action event.
    /// </summary>
    public class ContextActionEventArgs : KryptonContextMenuEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextActionEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        /// <param name="action">Close action to take.</param>
        /// <param name="contextMenu">Prepopulated context menu ready for display.</param>
        public ContextActionEventArgs(KryptonPage page, 
                                      int index, 
                                      ContextButtonAction action,
                                      KryptonContextMenu contextMenu)
            : base(page, index, contextMenu)
        {
            Action = action;
        }
        #endregion

        #region Action
        /// <summary>
        /// Gets and sets the close action to take.
        /// </summary>
        public ContextButtonAction Action { get; set; }

        #endregion
    }
}
