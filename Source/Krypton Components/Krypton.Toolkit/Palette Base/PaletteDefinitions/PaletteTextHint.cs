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
    /// Specifies a text rendering hint.
    /// </summary>
    public enum PaletteTextHint
    {
        /// <summary>
        /// Specifies text hint should be inherited.
        /// </summary>
        Inherit = -1,

        /// <summary>
        /// Specifies anti aliasing for text rendering.
        /// </summary>
        AntiAlias,

        /// <summary>
        /// Specifies anti aliasing with grid fit for text rendering.
        /// </summary>
        AntiAliasGridFit,

        /// <summary>
        /// Specifies clear type with grid fit for text rendering.
        /// </summary>
        ClearTypeGridFit,

        /// <summary>
        /// Specifies single bit per pixel for text rendering.
        /// </summary>
        SingleBitPerPixel,

        /// <summary>
        /// Specifies single bit for pixel with grid fit for text rendering.
        /// </summary>
        SingleBitPerPixelGridFit,

        /// <summary>
        /// Specifies system default setting for text rendering.
        /// </summary>
        SystemDefault
    }
}