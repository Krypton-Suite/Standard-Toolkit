namespace Krypton.Navigator
{
    /// <summary>
    /// Details for an event that provides pages and cell associated with a page dragging event.
    /// </summary>
    public class PageDragEndData
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageDragEndData class.
        /// </summary>
        /// <param name="source">Source object for the drag data..</param>
        /// <param name="pages">Collection of pages.</param>
        public PageDragEndData(object source,
                               KryptonPageCollection pages)
            : this(source, null, pages)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PageDragEndData class.
        /// </summary>
        /// <param name="source">Source object for the drag data..</param>
        /// <param name="navigator">Navigator associated with pages.</param>
        /// <param name="pages">Collection of pages.</param>
        public PageDragEndData(object source,
                               KryptonNavigator navigator,
                               KryptonPageCollection pages)
        {
            Source = source;
            Navigator = navigator;
            Pages = pages;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the source of the drag data.
        /// </summary>
        public object Source { get; }

        /// <summary>
        /// Gets access to any associated KryptonNavigator instance.
        /// </summary>
        public KryptonNavigator Navigator { get; }

        /// <summary>
        /// Gets access to the collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

        #endregion
    }
}
