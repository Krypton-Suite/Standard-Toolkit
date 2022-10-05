namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for an event that provides a button drag offset value.
    /// </summary>
    public class ButtonDragOffsetEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonDragOffsetEventArgs class.
        /// </summary>
        /// <param name="offset">Mouse offset for button dragging.</param>
        public ButtonDragOffsetEventArgs(Point offset) => PointOffset = offset;

        #endregion

        #region Point
        /// <summary>
        /// Gets access to the left mouse dragging offer.
        /// </summary>
        public Point PointOffset { get; }

        #endregion
    }
}
