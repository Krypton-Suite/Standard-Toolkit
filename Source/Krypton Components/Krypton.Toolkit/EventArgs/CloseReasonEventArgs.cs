using System.ComponentModel;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for close reason event handlers.
    /// </summary>
    public class CloseReasonEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseReasonEventArgs class.
        /// </summary>
        /// <param name="closeReason">Reason for the close action occuring.</param>
        public CloseReasonEventArgs(ToolStripDropDownCloseReason closeReason)
        {
            CloseReason = closeReason;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the reason for the context menu closing.
        /// </summary>
        public ToolStripDropDownCloseReason CloseReason { get; }

        #endregion
    }
}
