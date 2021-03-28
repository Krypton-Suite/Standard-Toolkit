using System;

namespace Krypton.Workspace
{
    /// <summary>
    /// Data associated with a change in the active cell.
    /// </summary>
    public class ActiveCellChangedEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ActiveCellChangedEventArgs class.
        /// </summary>
        /// <param name="oldCell">Previous active cell value.</param>
        /// <param name="newCell">New active cell value.</param>
        public ActiveCellChangedEventArgs(KryptonWorkspaceCell oldCell,
                                          KryptonWorkspaceCell newCell)
        {
            OldCell = oldCell;
            NewCell = newCell;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the old cell reference.
        /// </summary>
        public KryptonWorkspaceCell OldCell { get; }

        /// <summary>
        /// Gets the new cell reference.
        /// </summary>
        public KryptonWorkspaceCell NewCell { get; }

        #endregion
    }
}
