﻿#region BSD License
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

        private static readonly IReadOnlyDictionary<PaletteImageEffect, string> _pairs = new Dictionary<PaletteImageEffect, string>
        {
            {PaletteImageEffect.Inherit, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_INHERIT},
            {PaletteImageEffect.Light, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT},
            {PaletteImageEffect.LightLight, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_LIGHT_LIGHT},
            {PaletteImageEffect.Normal, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_NORMAL},
            {PaletteImageEffect.Disabled, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_DISABLED},
            {PaletteImageEffect.Dark, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_DARK},
            {PaletteImageEffect.DarkDark, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_DARK_DARK},
            {PaletteImageEffect.GrayScale, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE},
            {PaletteImageEffect.GrayScaleRed, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_RED},
            {PaletteImageEffect.GrayScaleGreen, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_GREEN},
            {PaletteImageEffect.GrayScaleBlue, DesignTimeUtilities.DEFAULT_PALETTE_IMAGE_EFFECT_GRAY_SCALE_BLUE}
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
