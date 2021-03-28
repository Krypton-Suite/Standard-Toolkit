using System;

namespace Krypton.Navigator
{
    /// <summary>
    /// Details for an event that provides pages associated with a page dragging event.
    /// </summary>
    public class PageDragEndEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageDragEndEventArgs class.
        /// </summary>
        /// <param name="dropped">True if a drop was performed; otherwise false.</param>
        /// <param name="pages">Array of event associated pages.</param>
        public PageDragEndEventArgs(bool dropped,
                                    KryptonPage[] pages)
        {
            Dropped = dropped;
            Pages = new KryptonPageCollection();

            if (pages != null)
            {
                Pages.AddRange(pages);
            }
        }
        #endregion

        #region Dropped
        /// <summary>
        /// Gets a value indicating if the drop was performed.
        /// </summary>
        public bool Dropped { get; }

        #endregion

        #region Pages
        /// <summary>
        /// Gets access to the collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

        #endregion
    }
}
