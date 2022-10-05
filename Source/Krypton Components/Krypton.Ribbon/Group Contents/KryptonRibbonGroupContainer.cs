namespace Krypton.Ribbon
{
    /// <summary>
    /// Represents the base class for all ribbon group containers.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    [DesignTimeVisible(false)]
    public abstract class KryptonRibbonGroupContainer : KryptonRibbonGroupItem,
                                                        IRibbonGroupContainer
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupContainer class.
        /// </summary>
        protected KryptonRibbonGroupContainer()
        {
        }
        #endregion

        #region Public
        
        /// <summary>
        /// Gets access to the parent group instance.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual KryptonRibbonGroup RibbonGroup { get; set; }

        /// <summary>
        /// Gets an array of all the contained components.
        /// </summary>
        /// <returns>Array of child components.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual Component[] GetChildComponents() => Array.Empty<Component>();

        #endregion
    }
}
