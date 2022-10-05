﻿namespace Krypton.Ribbon
{
    /// <summary>
    /// Override the text colors for button specs that are drawn on aero glass.
    /// </summary>
    public class PaletteRedirectRibbonAeroOverride : PaletteRedirect
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRibbonAeroOverride class.
        /// </summary>
        /// <param name="ribbon">Reference to owning Ribbon instance.</param>
        /// <param name="redirect">Source for inheriting values.</param>
        public PaletteRedirectRibbonAeroOverride(KryptonRibbon ribbon,
                                                 PaletteRedirect redirect)
            : base(redirect) =>
            _ribbon = ribbon;

        #endregion

        #region ShortText
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // We only want to override buttons specs that are drawing in normal mode
            if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
            {
                // If the ribbon is showing in office 2010 style and using glass
                if (_ribbon.CaptionArea.DrawCaptionOnComposition
                    && (_ribbon.RibbonShape == PaletteRibbonShape.Office2010 || _ribbon.RibbonShape == PaletteRibbonShape.Office2013 || _ribbon.RibbonShape == PaletteRibbonShape.Office365))
                {
                    return LightBackground(base.GetContentShortTextColor1(style, state));
                }
            }

            return base.GetContentShortTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We only want to override buttons specs that are drawing in normal mode
            if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
            {
                // If the ribbon is showing in office 2010 style and using glass
                if (_ribbon.CaptionArea.DrawCaptionOnComposition
                    && (_ribbon.RibbonShape == PaletteRibbonShape.Office2010 || _ribbon.RibbonShape == PaletteRibbonShape.Office2013 || _ribbon.RibbonShape == PaletteRibbonShape.Office365))
                {
                    return LightBackground(base.GetContentShortTextColor2(style, state));
                }
            }

            return base.GetContentShortTextColor2(style, state);
        }
        #endregion

        #region LongText
        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // We only want to override buttons specs that are drawing in normal mode
            if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
            {
                // If the ribbon is showing in office 2010 style and using glass
                if (_ribbon.CaptionArea.DrawCaptionOnComposition
                    && (_ribbon.RibbonShape == PaletteRibbonShape.Office2010 || _ribbon.RibbonShape == PaletteRibbonShape.Office2013 || _ribbon.RibbonShape == PaletteRibbonShape.Office365))
                {
                    return LightBackground(base.GetContentLongTextColor1(style, state));
                }
            }

            return base.GetContentLongTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We only want to override buttons specs that are drawing in normal mode
            if ((style == PaletteContentStyle.ButtonButtonSpec) && (state == PaletteState.Normal))
            {
                // If the ribbon is showing in office 2010 style and using glass
                if (_ribbon.CaptionArea.DrawCaptionOnComposition
                    && (_ribbon.RibbonShape == PaletteRibbonShape.Office2010 || _ribbon.RibbonShape == PaletteRibbonShape.Office2013 || _ribbon.RibbonShape == PaletteRibbonShape.Office365))
                {
                    return LightBackground(base.GetContentLongTextColor2(style, state));
                }
            }

            return base.GetContentLongTextColor2(style, state);
        }
        #endregion

        #region Implementation
        private Color LightBackground(Color retColor) =>
            // With a light background we force the color to be dark in normal state so it stands out
            Color.FromArgb(Math.Min(retColor.R, (byte)60),
                Math.Min(retColor.G, (byte)60),
                Math.Min(retColor.B, (byte)60));

        #endregion
    }
}
