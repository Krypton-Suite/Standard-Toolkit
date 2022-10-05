namespace Krypton.Navigator
{
    /// <summary>
    /// Details for a popup page event.
    /// </summary>
    public class PopupPageEventArgs : KryptonPageCancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PopupPageEventArgs class.
        /// </summary>
        /// <param name="page">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        /// <param name="screenRect">Screen rectangle for showing the popup.</param>
        public PopupPageEventArgs(KryptonPage page, 
                                  int index, 
                                  Rectangle screenRect)
            : base(page, index) =>
            ScreenRect = screenRect;

        #endregion

        #region ScreenRect
        /// <summary>
        /// Gets and sets the screen rectangle for showing the popup page.
        /// </summary>
        public Rectangle ScreenRect { get; set; }

        #endregion
    }
}
