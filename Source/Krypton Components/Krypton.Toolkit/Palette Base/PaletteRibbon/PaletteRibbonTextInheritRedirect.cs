using System.Drawing;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide inheritance of palette ribbon text properties from source redirector.
    /// </summary>
    public class PaletteRibbonTextInheritRedirect : PaletteRibbonTextInherit
    {
        #region Instance Fields
        private PaletteRedirect _redirect;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonTextInheritRedirect class.
        /// </summary>
        /// <param name="redirect">Source for inherit requests.</param>
        /// <param name="styleText">Ribbon item text style.</param>
        public PaletteRibbonTextInheritRedirect(PaletteRedirect redirect,
                                                PaletteRibbonTextStyle styleText)
        {
            Debug.Assert(redirect != null);

            _redirect = redirect;
            StyleText = styleText;
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _redirect = redirect;
        }
        #endregion

        #region StyleText
        /// <summary>
        /// Gets and sets the ribbon text style to use when inheriting.
        /// </summary>
        public PaletteRibbonTextStyle StyleText { get; set; }

        #endregion

        #region IPaletteRibbonBack
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteState state)      
        {
            return _redirect.GetRibbonTextColor(StyleText, state);
        }
        #endregion
    }
}
