using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Override the redirection to force the borders for the caption to only show the bottom border as a maximum.
    /// </summary>
    internal class PaletteCaptionRedirect : PaletteRedirect
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteCaptionRedirect class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteCaptionRedirect(IPalette target)
            : base(target)
        {
        }
        #endregion

        #region GetBorderDrawBorders
        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            PaletteDrawBorders paletteBorder = base.GetBorderDrawBorders(style, state);

            // The ribbon caption area should only ever draw a bottom border as the maximum
            return (paletteBorder & PaletteDrawBorders.Bottom) == PaletteDrawBorders.Bottom ? PaletteDrawBorders.Bottom : PaletteDrawBorders.None;
        }
        #endregion
    }
}
