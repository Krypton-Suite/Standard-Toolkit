using System;
using System.Xml;

namespace Krypton.Docking
{
    /// <summary>
    /// Event data for saving global docking configuration.
    /// </summary>
    public class DockGlobalSavingEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockGlobalSavingEventArgs class.
        /// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        public DockGlobalSavingEventArgs(KryptonDockingManager manager,
                                         XmlWriter xmlWriter)
        {
            DockingManager = manager;
            XmlWriter = xmlWriter;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the docking manager reference.
        /// </summary>
        public KryptonDockingManager DockingManager { get; }

        /// <summary>
        /// Gets the xml writer.
        /// </summary>
        public XmlWriter XmlWriter { get; }

        #endregion
    }
}
