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
    /// Custom type converter so that PaletteImageEffect values appear as neat text at design time.
    /// </summary>
    internal class PaletteImageEffectConverter : StringLookupConverter
    {
        #region Static Fields

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
        protected override Pair[] Pairs { get; } =
        { new(PaletteImageEffect.Inherit,           "Inherit"),
            new(PaletteImageEffect.Light,             "Light"),
            new(PaletteImageEffect.LightLight,        "LightLight"),
            new(PaletteImageEffect.Normal,            "Normal"),
            new(PaletteImageEffect.Disabled,          "Disabled"),
            new(PaletteImageEffect.Dark,              "Dark"),
            new(PaletteImageEffect.DarkDark,          "DarkDark"),
            new(PaletteImageEffect.GrayScale,         "GrayScale"),
            new(PaletteImageEffect.GrayScaleRed,      "GrayScale - Red"),
            new(PaletteImageEffect.GrayScaleGreen,    "GrayScale - Green"),
            new(PaletteImageEffect.GrayScaleBlue,     "GrayScale - Blue") };

        #endregion
    }
}
