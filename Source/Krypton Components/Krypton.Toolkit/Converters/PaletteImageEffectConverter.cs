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
    internal class PaletteImageEffectConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(PaletteImageEffect.Inherit, "Inherit"),
        //    new(PaletteImageEffect.Light, "Light"),
        //    new(PaletteImageEffect.LightLight, "LightLight"),
        //    new(PaletteImageEffect.Normal, "Normal"),
        //    new(PaletteImageEffect.Disabled, "Disabled"),
        //    new(PaletteImageEffect.Dark, "Dark"),
        //    new(PaletteImageEffect.DarkDark, "DarkDark"),
        //    new(PaletteImageEffect.GrayScale, "GrayScale"),
        //    new(PaletteImageEffect.GrayScaleRed, "GrayScale - Red"),
        //    new(PaletteImageEffect.GrayScaleGreen, "GrayScale - Green"),
        //    new(PaletteImageEffect.GrayScaleBlue, "GrayScale - Blue")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new Pair(PaletteImageEffect.Inherit, KryptonLanguageManager.ImageEffectStrings.Inherit),
            new Pair(PaletteImageEffect.Light, KryptonLanguageManager.ImageEffectStrings.Light),
            new Pair(PaletteImageEffect.LightLight, KryptonLanguageManager.ImageEffectStrings.LightLight),
            new Pair(PaletteImageEffect.Normal, KryptonLanguageManager.ImageEffectStrings.Normal),
            new Pair(PaletteImageEffect.Disabled, KryptonLanguageManager.ImageEffectStrings.Disabled),
            new Pair(PaletteImageEffect.Dark, KryptonLanguageManager.ImageEffectStrings.Dark),
            new Pair(PaletteImageEffect.DarkDark, KryptonLanguageManager.ImageEffectStrings.DarkDark),
            new Pair(PaletteImageEffect.GrayScale, KryptonLanguageManager.ImageEffectStrings.GrayScale),
            new Pair(PaletteImageEffect.GrayScaleRed, KryptonLanguageManager.ImageEffectStrings.GrayScaleRed),
            new Pair(PaletteImageEffect.GrayScaleGreen, KryptonLanguageManager.ImageEffectStrings.GrayScaleGreen),
            new Pair(PaletteImageEffect.GrayScaleBlue, KryptonLanguageManager.ImageEffectStrings.GrayScaleBlue)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteImageEffectConverter class.
        /// </summary>
        public PaletteImageEffectConverter()
            : base(typeof(PaletteImageEffect))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
