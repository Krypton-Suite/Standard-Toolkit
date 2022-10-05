namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for need layout events.
    /// </summary>
    public class NeedLayoutEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NeedLayoutEventArgs class.
        /// </summary>
        /// <param name="needLayout">Does the layout need regenerating.</param>
        public NeedLayoutEventArgs(bool needLayout)
            : this(needLayout, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the NeedLayoutEventArgs class.
        /// </summary>
        /// <param name="needLayout">Does the layout need regenerating.</param>
        /// <param name="invalidRect">Specifies an invalidation rectangle.</param>
        public NeedLayoutEventArgs(bool needLayout,
                                   Rectangle invalidRect)
        {
            NeedLayout = needLayout;
            InvalidRect = invalidRect;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a value indicating if the layout needs regenerating.
        /// </summary>
        public bool NeedLayout { get; }

        /// <summary>
        /// Gets the rectangle to be invalidated.
        /// </summary>
        public Rectangle InvalidRect { get; }

        #endregion
    }
}
