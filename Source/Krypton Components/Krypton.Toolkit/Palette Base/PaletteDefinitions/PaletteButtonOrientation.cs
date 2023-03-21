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
    /// Specifies the orientation of a button specification.
    /// </summary>
    [TypeConverter(typeof(PaletteButtonOrientationConverter))]
    public enum PaletteButtonOrientation
    {
        /// <summary>
        /// Specifies orientation should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies orientation should automatically match the concept of use.
        /// </summary>
        Auto,

        /// <summary>
        /// Specifies the button is orientated in a vertical top down manner.
        /// </summary>
        FixedTop,

        /// <summary>
        /// Specifies the button is orientated in a vertical bottom upwards manner.
        /// </summary>
        FixedBottom,

        /// <summary>
        /// Specifies the button is orientated in a horizontal left to right manner.
        /// </summary>
        FixedLeft,

        /// <summary>
        /// Specifies the button is orientated in a horizontal right to left manner.
        /// </summary>
        FixedRight
    }
}