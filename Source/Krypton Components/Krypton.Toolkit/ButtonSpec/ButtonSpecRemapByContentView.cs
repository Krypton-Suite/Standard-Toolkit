namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirect requests for image/text colors to remap.
    /// </summary>
    public class ButtonSpecRemapByContentView : ButtonSpecRemapByContentBase
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecRemapByContentView class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="buttonSpec">Reference to button specification.</param>
        public ButtonSpecRemapByContentView(IPalette target,
                                            ButtonSpec buttonSpec)
            : base(target, buttonSpec)
        {
        }
        #endregion

        #region Foreground
        /// <summary>
        /// Gets and sets the foreground to use for color map redirection.
        /// </summary>
        public ViewDrawContent Foreground { get; set; }

        #endregion

        #region PaletteContent
        /// <summary>
        /// Gets the palette content to use for remapping.
        /// </summary>
        public override IPaletteContent PaletteContent => Foreground?.GetPalette();

        #endregion

        #region PaletteState
        /// <summary>
        /// Gets the state of the remapping area
        /// </summary>
        public override PaletteState PaletteState => Foreground.State;

        #endregion
    }
}
