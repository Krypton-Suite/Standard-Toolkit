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
    /// Specifies how an image is drawn.
    /// </summary>
    [TypeConverter(typeof(PaletteImageEffectConverter))]
    public enum PaletteImageEffect
    {
        /// <summary>
        /// Specifies effect should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies image is drawn without modification.
        /// </summary>
        Normal,

        /// <summary>
        /// Specifies image is drawn to look disabled.
        /// </summary>
        Disabled,

        /// <summary>
        /// Specifies image is drawn converted to a gray-scale.
        /// </summary>
        GrayScale,

        /// <summary>
        /// Specifies image is drawn converted to a gray-scale except for red.
        /// </summary>
        GrayScaleRed,

        /// <summary>
        /// Specifies image is drawn converted to a gray-scale except for green.
        /// </summary>
        GrayScaleGreen,

        /// <summary>
        /// Specifies image is drawn converted to a gray-scale except for blue.
        /// </summary>
        GrayScaleBlue,

        /// <summary>
        /// Specifies image is drawn slightly lighter.
        /// </summary>
        Light,

        /// <summary>
        /// Specifies image is drawn much lighter.
        /// </summary>
        LightLight,

        /// <summary>
        /// Specifies image is drawn slightly darker.
        /// </summary>
        Dark,

        /// <summary>
        /// Specifies image is drawn much darker.
        /// </summary>
        DarkDark
    }
}