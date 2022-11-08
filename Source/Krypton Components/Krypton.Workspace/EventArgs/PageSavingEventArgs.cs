namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for persisting extra data for a workspace cell page.
    /// </summary>
    public class PageSavingEventArgs : XmlSavingEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageSavingEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="page">Reference to owning workspace cell page.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        public PageSavingEventArgs(KryptonWorkspace workspace,
                                   KryptonPage page,
                                   XmlWriter xmlWriter)
            : base(workspace, xmlWriter) =>
            Page = page;

        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace cell page reference.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
