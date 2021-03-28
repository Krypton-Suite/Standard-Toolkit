using System.Xml;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for persisting extra data for a workspace cell page.
    /// </summary>
    public class PageLoadingEventArgs : XmlLoadingEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageLoadingEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="page">Reference to owning workspace cell page.</param>
        /// <param name="xmlReader">Xml reader for persisting custom data.</param>
        public PageLoadingEventArgs(KryptonWorkspace workspace,
                                    KryptonPage page,
                                    XmlReader xmlReader)
            : base(workspace, xmlReader)
        {
            Page = page;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace cell page reference.
        /// </summary>
        public KryptonPage Page { get; set; }

        #endregion
    }
}
