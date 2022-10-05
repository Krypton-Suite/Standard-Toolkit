namespace Krypton.Toolkit
{
    /// <summary>
    /// Details of the context menu showing related to a bread crumb.
    /// </summary>
    public class BreadCrumbMenuArgs : ContextPositionMenuArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextMenuArgs class.
        /// </summary>
        /// <param name="crumb">Reference to related crumb.</param>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        /// <param name="positionH">Relative horizontal position of the KryptonContextMenu.</param>
        /// <param name="positionV">Relative vertical position of the KryptonContextMenu.</param>
        public BreadCrumbMenuArgs(KryptonBreadCrumbItem crumb,
                                  KryptonContextMenu kcm,
                                  KryptonContextMenuPositionH positionH,
                                  KryptonContextMenuPositionV positionV)
            : base(null, kcm, positionH, positionV) =>
            Crumb = crumb;

        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the crumb related to the event.
        /// </summary>
        public KryptonBreadCrumbItem Crumb { get; set; }

        #endregion
    }
}
