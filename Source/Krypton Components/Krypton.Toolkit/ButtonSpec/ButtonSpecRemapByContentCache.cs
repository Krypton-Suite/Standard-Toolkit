namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirect requests for image/text colors to remap.
    /// </summary>
    public class ButtonSpecRemapByContentCache : ButtonSpecRemapByContentBase
    {
        #region Instance Fields
        private IPaletteContent _paletteContent;
        private PaletteState _paletteState;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecRemapByContentCache class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="buttonSpec">Reference to button specification.</param>
        public ButtonSpecRemapByContentCache(IPalette target,
                                             ButtonSpec buttonSpec)
            : base(target, buttonSpec)
        {
        }
        #endregion

        #region SetPaletteContent
        /// <summary>
        /// Set the palette content to use for remapping.
        /// </summary>
        /// <param name="paletteContent">Palette for requesting foreground colors.</param>
        public void SetPaletteContent(IPaletteContent paletteContent)
        {
            _paletteContent = paletteContent;
        }
        #endregion

        #region SetPaletteState
        /// <summary>
        /// Set the palette state of the remapping element.
        /// </summary>
        /// <param name="paletteState">Palette state.</param>
        public void SetPaletteState(PaletteState paletteState)
        {
            _paletteState = paletteState;
        }
        #endregion

        #region PaletteContent
        /// <summary>
        /// Gets the palette content to use for remapping.
        /// </summary>
        public override IPaletteContent PaletteContent => _paletteContent;

        #endregion

        #region PaletteState
        /// <summary>
        /// Gets the state of the remapping area
        /// </summary>
        public override PaletteState PaletteState => _paletteState;

        #endregion
    }
}
