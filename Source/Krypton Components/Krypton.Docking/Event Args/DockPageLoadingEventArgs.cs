namespace Krypton.Docking
{
    /// <summary>
    /// Event data for loading docking page configuration.
    /// </summary>
    public class DockPageLoadingEventArgs : DockGlobalLoadingEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockPageLoadingEventArgs class.
        /// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlReading">Xml reader for persisting custom data.</param>
        /// <param name="page">Reference to page being loaded.</param>
        public DockPageLoadingEventArgs(KryptonDockingManager manager,
                                        XmlReader xmlReading,
                                        KryptonPage page)
            : base(manager, xmlReading) =>
            Page = page;

        #endregion

        #region Public
        /// <summary>
        /// Gets the loading page reference.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
