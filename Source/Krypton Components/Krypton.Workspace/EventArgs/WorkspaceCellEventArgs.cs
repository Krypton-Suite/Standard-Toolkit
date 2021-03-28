using System;

namespace Krypton.Workspace
{
    /// <summary>
    /// Workspace cell event data.
    /// </summary>
    public class WorkspaceCellEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the WorkspaceCellEventArgs class.
        /// </summary>
        /// <param name="cell">Workspace cell associated with the event.</param>
        public WorkspaceCellEventArgs(KryptonWorkspaceCell cell)
        {
            Cell = cell;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the cell reference.
        /// </summary>
        public KryptonWorkspaceCell Cell { get; }

        #endregion
    }
}
