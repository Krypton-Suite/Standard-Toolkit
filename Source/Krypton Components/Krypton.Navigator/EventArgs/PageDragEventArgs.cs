namespace Krypton.Navigator
{
    /// <summary>
    /// Details for an event that provides pages associated with a page dragging event.
    /// </summary>
    public class PageDragEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPageDragEventArgs class.
        /// </summary>
        /// <param name="screenPoint">Screen point of the mouse.</param>
        /// <param name="pages">Array of event associated pages.</param>
        public PageDragEventArgs(Point screenPoint,
                                 KryptonPage[] pages)
        {
            ScreenPoint = screenPoint;
            Pages = new KryptonPageCollection();

            if (pages != null)
            {
                Pages.AddRange(pages);
            }
        }

        /// <summary>
        /// Initialize a new instance of the KryptonPageDragEventArgs class.
        /// </summary>
        /// <param name="screenPoint">Screen point of the mouse.</param>
        /// <param name="pages">Collection of event associated pages.</param>
        public PageDragEventArgs(Point screenPoint,
                                 KryptonPageCollection pages)
        {
            ScreenPoint = screenPoint;
            Pages = pages;
        }
        #endregion

        #region ScreenPoint
        /// <summary>
        /// Gets access to the associated screen point.
        /// </summary>
        public Point ScreenPoint { get; }

        #endregion

        #region Pages
        /// <summary>
        /// Gets access to the collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

        #endregion
    }
}
