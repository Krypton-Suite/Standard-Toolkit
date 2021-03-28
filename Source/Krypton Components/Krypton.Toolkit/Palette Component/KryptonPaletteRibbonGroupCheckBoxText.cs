namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette ribbon group button text states.
    /// </summary>
    public class KryptonPaletteRibbonGroupCheckBoxText : KryptonPaletteRibbonGroupBaseText
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupCheckBoxText class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupCheckBoxText(PaletteRedirect redirect,
                                                     NeedPaintHandler needPaint)
            : base(redirect, PaletteRibbonTextStyle.RibbonGroupCheckBoxText, needPaint)
        {
        }
        #endregion
    }
}
