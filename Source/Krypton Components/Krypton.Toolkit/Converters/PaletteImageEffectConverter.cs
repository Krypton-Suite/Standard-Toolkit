#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteImageEffect values appear as neat text at design time.
    /// </summary>
    internal class PaletteImageEffectConverter : StringLookupConverter<PaletteImageEffect>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteImageEffect, string> _pairs = new Dictionary<PaletteImageEffect, string>
        {
            {PaletteImageEffect.Inherit, KryptonLanguageManager.ImageEffectStrings.Inherit},
            {PaletteImageEffect.Light, KryptonLanguageManager.ImageEffectStrings.Light},
            {PaletteImageEffect.LightLight, KryptonLanguageManager.ImageEffectStrings.LightLight},
            {PaletteImageEffect.Normal, KryptonLanguageManager.ImageEffectStrings.Normal},
            {PaletteImageEffect.Disabled, KryptonLanguageManager.ImageEffectStrings.Disabled},
            {PaletteImageEffect.Dark, KryptonLanguageManager.ImageEffectStrings.Dark},
            {PaletteImageEffect.DarkDark, KryptonLanguageManager.ImageEffectStrings.DarkDark},
            {PaletteImageEffect.GrayScale, KryptonLanguageManager.ImageEffectStrings.GrayScale},
            {PaletteImageEffect.GrayScaleRed, KryptonLanguageManager.ImageEffectStrings.GrayScaleRed},
            {PaletteImageEffect.GrayScaleGreen, KryptonLanguageManager.ImageEffectStrings.GrayScaleGreen},
            {PaletteImageEffect.GrayScaleBlue, KryptonLanguageManager.ImageEffectStrings.GrayScaleBlue }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteImageEffect /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
