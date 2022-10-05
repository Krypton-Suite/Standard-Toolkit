namespace Krypton.Navigator
{
    /// <summary>
    /// Details of an event that is fired just before a page is reordered.
    /// </summary>
    public class PageReorderEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageReorderEventArgs class.
        /// </summary>
        /// <param name="pageMoving">Reference to page being moved.</param>
        /// <param name="pageTarget">Reference to target paged.</param>
        /// <param name="movingBefore">True if moving page is to be positioned before the target; otherwise after the target.</param>
        public PageReorderEventArgs(KryptonPage pageMoving, 
                                    KryptonPage pageTarget, 
                                    bool movingBefore)
        {
            PageMoving = pageMoving;
            PageTarget = pageTarget;
            MovingBefore = movingBefore;
        }
        #endregion

        #region PageMoving
        /// <summary>
        /// Gets the page that is being moved.
        /// </summary>
        public KryptonPage PageMoving { get; }

        #endregion

        #region PageTarget
        /// <summary>
        /// Gets the page that is the target for the move.
        /// </summary>
        public KryptonPage PageTarget { get; }

        #endregion

        #region MovingBefore
        /// <summary>
        /// Gets a value indicating if the page being moved is to be placed before the target page.
        /// </summary>
        public bool MovingBefore { get; }

        #endregion
    }
}
