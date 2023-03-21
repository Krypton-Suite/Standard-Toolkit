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
    /// Specifies a graphics rendering hint.
    /// </summary>
    public enum PaletteGraphicsHint
    {
        /// <summary>
        /// Specifies graphics hint should be inherited.
        /// </summary>
        Inherit = -1,

        /// <summary>
        /// Specifies no smoothing for graphics rendering.
        /// </summary>
        None,

        /// <summary>
        /// Specifies anti aliasing for graphics rendering.
        /// </summary>
        AntiAlias,

        /// <summary>Specifies no anti-aliasing.</summary>
        HighSpeed,

        /// <summary>Specifies anti-aliased rendering.</summary>
        HighQuality

    }
}