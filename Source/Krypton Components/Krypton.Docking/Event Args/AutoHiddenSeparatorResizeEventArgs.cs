namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for a AutoHiddenSeparatorResize event.
    /// </summary>
    public class AutoHiddenSeparatorResizeEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AutoHiddenSeparatorResizeEventArgs class.
        /// </summary>
        /// <param name="separator">Reference to separator control instance.</param>
        /// <param name="dockspace">Reference to dockspace control instance.</param>
        /// <param name="page">Reference to page contained in the dockspace.</param>
        /// <param name="resizeRect">Initial resizing rectangle.</param>
        public AutoHiddenSeparatorResizeEventArgs(KryptonSeparator separator,
                                                  KryptonDockspace dockspace,
                                                  KryptonPage page,
                                                  Rectangle resizeRect)
        {
            SeparatorControl = separator;
            DockspaceControl = dockspace;
            Page = page;
            ResizeRect = resizeRect;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonSeparator control.
        /// </summary>
        public KryptonSeparator SeparatorControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockspace control.
        /// </summary>
        public KryptonDockspace DockspaceControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonPage instance.
        /// </summary>
        public KryptonPage Page { get; }

        /// <summary>
        /// Gets and sets the rectangle that limits resizing of the dockspace using the separator.
        /// </summary>
        public Rectangle ResizeRect { get; set; }

        #endregion
    }
}
