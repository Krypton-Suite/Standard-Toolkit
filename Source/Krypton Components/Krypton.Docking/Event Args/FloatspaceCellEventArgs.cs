namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a FloatspaceCellAdding/FloatingCellRemoving events.
    /// </summary>
    public class FloatspaceCellEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the FloatspaceCellEventArgs class.
        /// </summary>
        /// <param name="floatspace">Reference to existing floatspace control instance.</param>
        /// <param name="element">Reference to docking floatspace element that is managing the floatspace control.</param>
        /// <param name="cell">Reference tofloatspace control cell instance.</param>
        public FloatspaceCellEventArgs(KryptonFloatspace floatspace,
                                       KryptonDockingFloatspace element,
                                       KryptonWorkspaceCell cell)
        {
            FloatspaceControl = floatspace;
            FloatspaceElement = element;
            CellControl = cell;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonFloatspace control.
        /// </summary>
        public KryptonFloatspace FloatspaceControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingFloatspace that is managing the floatspace.
        /// </summary>
        public KryptonDockingFloatspace FloatspaceElement { get; }

        /// <summary>
        /// Gets a reference to the KryptonWorkspaceCell control.
        /// </summary>
        public KryptonWorkspaceCell CellControl { get; }

        #endregion
    }
}
