// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

namespace ComponentFactory.Krypton.Toolkit
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
        /// Initialize a new instance of the PaletteImageEffectConverter clas.
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
        { new Pair(PaletteImageEffect.Inherit,           "Inherit"),
            new Pair(PaletteImageEffect.Light,             "Light"),
            new Pair(PaletteImageEffect.LightLight,        "LightLight"),
            new Pair(PaletteImageEffect.Normal,            "Normal"),
            new Pair(PaletteImageEffect.Disabled,          "Disabled"),
            new Pair(PaletteImageEffect.Dark,              "Dark"),
            new Pair(PaletteImageEffect.DarkDark,          "DarkDark"),
            new Pair(PaletteImageEffect.GrayScale,         "GrayScale"),
            new Pair(PaletteImageEffect.GrayScaleRed,      "GrayScale - Red"),
            new Pair(PaletteImageEffect.GrayScaleGreen,    "GrayScale - Green"),
            new Pair(PaletteImageEffect.GrayScaleBlue,     "GrayScale - Blue") };

        #endregion
    }
}
