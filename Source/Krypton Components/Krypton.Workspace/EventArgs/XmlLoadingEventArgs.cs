namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for persisting extra data for a workspace.
    /// </summary>
    public class XmlLoadingEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the XmlLoadingEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="xmlReading">Xml reader for persisting custom data.</param>
        public XmlLoadingEventArgs(KryptonWorkspace workspace,
                                   XmlReader xmlReading)
        {
            Workspace = workspace;
            XmlReader = xmlReading;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace reference.
        /// </summary>
        public KryptonWorkspace Workspace { get; }

        /// <summary>
        /// Gets the xml reader.
        /// </summary>
        public XmlReader XmlReader { get; }

        #endregion
    }
}
