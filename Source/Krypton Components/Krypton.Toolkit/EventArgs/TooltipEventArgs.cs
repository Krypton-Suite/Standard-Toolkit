namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for a tooltip related event.
    /// </summary>
    public class ToolTipEventArgs : EventArgs
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ToolTipEventArgs class.
        /// </summary>
        /// <param name="target">Reference to view element that requires tooltip.</param>
        /// <param name="controlMousePosition">Screen location of mouse when tooltip was required.</param>
        public ToolTipEventArgs(ViewBase target, Point controlMousePosition)
        {
            //Debug.Assert(target != null);

            // Remember parameter details
            Target = target;
            ControlMousePosition = controlMousePosition;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the view element that is related to the tooltip.
        /// </summary>
        public ViewBase Target { get; }

        /// <summary>
        /// Gets the screen point of the mouse where tooltip is required.
        /// </summary>
        public Point ControlMousePosition { get; }

        #endregion
    }
}
