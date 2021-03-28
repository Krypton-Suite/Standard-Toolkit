namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette ribbon group label text states.
    /// </summary>
    public class KryptonPaletteRibbonGroupLabelText : KryptonPaletteRibbonGroupBaseText
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupLabelText class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupLabelText(PaletteRedirect redirect,
                                                  NeedPaintHandler needPaint)
            : base(redirect, PaletteRibbonTextStyle.RibbonGroupLabelText, needPaint)
        {
        }
        #endregion
    }
}
