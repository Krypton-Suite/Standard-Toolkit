namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette ribbon group radio button text states.
    /// </summary>
    public class KryptonPaletteRibbonGroupRadioButtonText : KryptonPaletteRibbonGroupBaseText
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupRadioButtonText class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupRadioButtonText(PaletteRedirect redirect,
                                                        NeedPaintHandler needPaint)
            : base(redirect, PaletteRibbonTextStyle.RibbonGroupRadioButtonText, needPaint)
        {
        }
        #endregion
    }
}
