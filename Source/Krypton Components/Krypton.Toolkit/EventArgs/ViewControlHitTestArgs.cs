namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for context menu related events.
    /// </summary>
    public class ViewControlHitTestArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewControlHitTestArgs class.
        /// </summary>
        /// <param name="pt">Point associated with hit test message.</param>
        public ViewControlHitTestArgs(Point pt)
            : base(true)
        {
            Point = pt;
            Result = IntPtr.Zero;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the point.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// Gets and sets the result.
        /// </summary>
        public IntPtr Result { get; set; }

        #endregion
    }
}
