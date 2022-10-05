namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for context menu related events.
    /// </summary>
    public class ContextMenuArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        public ContextMenuArgs()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="cms">Context menu strip that can be customized.</param>
        public ContextMenuArgs(ContextMenuStrip cms)
            : this(cms, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        public ContextMenuArgs(KryptonContextMenu kcm)
            : this(null, kcm)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="cms">Context menu strip that can be customized.</param>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        public ContextMenuArgs(ContextMenuStrip cms,
                               KryptonContextMenu kcm)
        {
            ContextMenuStrip = cms;
            KryptonContextMenu = kcm;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the context menu strip instance.
        /// </summary>
        public ContextMenuStrip ContextMenuStrip { get; }

        /// <summary>
        /// Gets access to the KryptonContextMenu instance.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        #endregion
    }
}
