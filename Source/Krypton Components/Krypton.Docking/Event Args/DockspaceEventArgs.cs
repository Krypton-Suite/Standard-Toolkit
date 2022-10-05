namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a DockspaceAdding/DockspaceRemoved events.
    /// </summary>
    public class DockspaceEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockspaceEventArgs class.
        /// </summary>
        /// <param name="dockspace">Reference to new dockspace control instance.</param>
        /// <param name="element">Reference to docking dockspace element that is managing the dockspace control.</param>
        public DockspaceEventArgs(KryptonDockspace dockspace,
                                  KryptonDockingDockspace element)
        {
            DockspaceControl = dockspace;
            DockspaceElement = element;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonDockspace control.
        /// </summary>
        public KryptonDockspace DockspaceControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace control.
        /// </summary>
        public KryptonDockingDockspace DockspaceElement { get; }

        #endregion
    }
}
