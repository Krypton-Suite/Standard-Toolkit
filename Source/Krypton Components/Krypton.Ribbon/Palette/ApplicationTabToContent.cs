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


namespace Krypton.Ribbon
{
    internal class ApplicationTabToContent : RibbonToContent
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ApplicationTabToContent class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control..</param>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        public ApplicationTabToContent(KryptonRibbon ribbon,
                                       PaletteRibbonGeneral ribbonGeneral)
            : base(ribbonGeneral) =>
            _ribbon = ribbon;

        #endregion
        
        #region IPaletteContent
        /// <summary>
        /// Gets the first color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state) => _ribbon.RibbonAppButton.AppButtonTextColor;

        /// <summary>
        /// Gets the second color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state) => _ribbon.RibbonAppButton.AppButtonTextColor;

        /// <summary>
        /// Gets the first color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state) => _ribbon.RibbonAppButton.AppButtonTextColor;

        /// <summary>
        /// Gets the second color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state) => _ribbon.RibbonAppButton.AppButtonTextColor;

        #endregion
    }
}
