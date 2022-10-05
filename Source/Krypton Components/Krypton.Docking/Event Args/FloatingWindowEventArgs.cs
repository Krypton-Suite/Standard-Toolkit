namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a FloatingWindowAdding/FloatingWindowRemoved event.
    /// </summary>
    public class FloatingWindowEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the FloatingWindowEventArgs class.
        /// </summary>
        /// <param name="floatingWindow">Reference to floating window instance.</param>
        /// <param name="element">Reference to docking floating winodw element that is managing the floating window.</param>
        public FloatingWindowEventArgs(KryptonFloatingWindow floatingWindow,
                                       KryptonDockingFloatingWindow element)
        {
            FloatingWindow = floatingWindow;
            FloatingWindowElement = element;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonFloatingWindow control.
        /// </summary>
        public KryptonFloatingWindow FloatingWindow { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingFloatingWindow that is managing the dockspace.
        /// </summary>
        public KryptonDockingFloatingWindow FloatingWindowElement { get; }

        #endregion
    }
}
