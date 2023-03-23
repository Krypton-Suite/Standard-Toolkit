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
    /// Specifies a relative alignment position.
    /// </summary>
    public enum PaletteRelativeAlign
    {
        /// <summary>
        /// Specifies relative alignment should be inherited.
        /// </summary>
        Inherit = -1,

        /// <summary>
        /// Specifies a relative alignment of near.
        /// </summary>
        Near = 0,

        /// <summary>
        /// Specifies a relative alignment of center.
        /// </summary>
        Center = 1,

        /// <summary>
        /// Specifies a relative alignment of far.
        /// </summary>
        Far = 2
    }
}