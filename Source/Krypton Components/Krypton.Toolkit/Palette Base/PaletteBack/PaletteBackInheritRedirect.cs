#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide inheritance of palette background properties from source redirector.
    /// </summary>
    public class PaletteBackInheritRedirect : PaletteBackInherit
    {
        #region Instance Fields
        private PaletteRedirect _redirect;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBackInheritRedirect class.
        /// </summary>
        /// <param name="redirect">Source for inherit requests.</param>
        public PaletteBackInheritRedirect(PaletteRedirect redirect)
            : this(redirect, PaletteBackStyle.ButtonStandalone)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteBackInheritRedirect class.
        /// </summary>
        /// <param name="redirect">Source for inherit requests.</param>
        /// <param name="style">Style used in requests.</param>
        public PaletteBackInheritRedirect(PaletteRedirect redirect,
                                          PaletteBackStyle style)
        {
            _redirect = redirect;
            Style = style;
        }
        #endregion

        #region GetRedirector
        /// <summary>
        /// Gets the redirector instance.
        /// </summary>
        /// <returns>Return the currently used redirector.</returns>
        public PaletteRedirect GetRedirector() => _redirect;

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

        #region Style
        /// <summary>
        /// Gets and sets the style to use when inheriting.
        /// </summary>
        public PaletteBackStyle Style { get; set; }

        #endregion

        #region IPaletteBack
        /// <summary>
        /// Gets a value indicating if background should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBackDraw(PaletteState state) => _redirect.GetBackDraw(Style, state);

        /// <summary>
        /// Gets the graphics drawing hint.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) => _redirect.GetBackGraphicsHint(Style, state);

        /// <summary>
        /// Gets the first background color from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor1(PaletteState state) => _redirect.GetBackColor1(Style, state);

        /// <summary>
        /// Gets the second back color from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteState state) => _redirect.GetBackColor2(Style, state);

        /// <summary>
        /// Gets the color drawing style from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteState state) => _redirect.GetBackColorStyle(Style, state);

        /// <summary>
        /// Gets the color alignment style from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBackColorAlign(PaletteState state) => _redirect.GetBackColorAlign(Style, state);

        /// <summary>
        /// Gets the color background angle from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBackColorAngle(PaletteState state) => _redirect.GetBackColorAngle(Style, state);

        /// <summary>
        /// Gets a background image from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBackImage(PaletteState state) => _redirect.GetBackImage(Style, state);

        /// <summary>
        /// Gets the background image style from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBackImageStyle(PaletteState state) => _redirect.GetBackImageStyle(Style, state);

        /// <summary>
        /// Gets the image alignment style from the redirector.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBackImageAlign(PaletteState state) => _redirect.GetBackImageAlign(Style, state);

        #endregion
    }
}
