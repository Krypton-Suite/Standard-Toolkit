using System;
using System.Xml;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for persisting extra data for a workspace.
    /// </summary>
    public class XmlSavingEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the XmlSavingEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        public XmlSavingEventArgs(KryptonWorkspace workspace,
                                  XmlWriter xmlWriter)
        {
            Workspace = workspace;
            XmlWriter = xmlWriter;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the workspace reference.
        /// </summary>
        public KryptonWorkspace Workspace { get; }

        /// <summary>
        /// Gets the xml writer.
        /// </summary>
        public XmlWriter XmlWriter { get; }

        #endregion
    }
}
