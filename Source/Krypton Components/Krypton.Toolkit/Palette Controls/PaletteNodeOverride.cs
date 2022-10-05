namespace Krypton.Toolkit
{
    /// <summary>
    /// Allow some palette values to be overridden.
    /// </summary>
    public class PaletteNodeOverride : GlobalId,
                                       IPaletteTriple
    {
        #region Intance Fields
        private readonly PaletteBackInheritNode _overrideBack;
        private readonly PaletteBorderInheritOverride _overrideBorder;
        private readonly PaletteContentInheritNode _overrideContent;
        #endregion

        #region Identity

        /// <summary>
        /// Initialize a new instance of the PaletteNodeOverride class.
        /// </summary>
        /// <param name="triple">Palette to use.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PaletteNodeOverride(IPaletteTriple triple)
        {
            Debug.Assert(triple != null);

            // Validate incoming references
            if (triple == null)
            {
                throw new ArgumentNullException(nameof(triple));
            }

            // Create the triple override instances
            _overrideBack = new PaletteBackInheritNode(triple.PaletteBack);
            _overrideBorder = new PaletteBorderInheritOverride(triple.PaletteBorder, triple.PaletteBorder);
            _overrideContent = new PaletteContentInheritNode(triple.PaletteContent);
        }            
        #endregion

        #region TreeNode
        /// <summary>
        /// Set the tree node to use for sourcing values.
        /// </summary>
        public TreeNode TreeNode
        {
            set
            {
                _overrideBack.TreeNode = value;
                _overrideContent.TreeNode = value;
            }
        }
        #endregion

        #region Palette Accessors
        /// <summary>
        /// Gets the background palette.
        /// </summary>
        public IPaletteBack PaletteBack => _overrideBack;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        public IPaletteBorder PaletteBorder => _overrideBorder;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        public IPaletteContent PaletteContent => _overrideContent;

        #endregion    
    }
}
