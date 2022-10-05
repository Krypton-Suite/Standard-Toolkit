namespace Krypton.Docking
{
    /// <summary>
    /// Event data for saving docking page configuration.
    /// </summary>
    public class DockPageSavingEventArgs : DockGlobalSavingEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockPageSavingEventArgs class.
        /// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        /// <param name="page">Reference to page being saved.</param>
        public DockPageSavingEventArgs(KryptonDockingManager manager,
                                       XmlWriter xmlWriter,
                                       KryptonPage page)
            : base(manager, xmlWriter) =>
            Page = page;

        #endregion

        #region Public
        /// <summary>
        /// Gets the saving page reference.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
