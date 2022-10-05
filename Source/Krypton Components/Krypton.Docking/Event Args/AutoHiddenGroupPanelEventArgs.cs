namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a AutoHiddenGroupPanelAdding/AutoHiddenGroupPanelRemoved events.
    /// </summary>
    public class AutoHiddenGroupPanelEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AutoHiddenGroupPanelEventArgs class.
        /// </summary>
        /// <param name="autoHiddenPanel">Reference to auto hidden panel control instance.</param>
        /// <param name="element">Reference to docking auto hidden edge element that is managing the panel.</param>
        public AutoHiddenGroupPanelEventArgs(KryptonAutoHiddenPanel autoHiddenPanel,
                                             KryptonDockingEdgeAutoHidden element)
        {
            AutoHiddenPanelControl = autoHiddenPanel;
            EdgeAutoHiddenElement = element;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonAutoHiddenPanel control.
        /// </summary>
        public KryptonAutoHiddenPanel AutoHiddenPanelControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingEdgeAutoHidden that is managing the edge.
        /// </summary>
        public KryptonDockingEdgeAutoHidden EdgeAutoHiddenElement { get; }

        #endregion
    }
}
