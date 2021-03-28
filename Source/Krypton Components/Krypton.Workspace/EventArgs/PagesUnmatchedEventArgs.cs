using System;
using System.Collections.Generic;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event data for listing pages unmatched during the load process.
    /// </summary>
    public class PagesUnmatchedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PagesUnmatchedEventArgs class.
        /// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="unmatched">List of pages unmatched during the load process.</param>
        public PagesUnmatchedEventArgs(KryptonWorkspace workspace,
                                       List<KryptonPage> unmatched)
        {
            Workspace = workspace;
            Unmatched = unmatched;
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
        public List<KryptonPage> Unmatched { get; }

        #endregion
    }
}
