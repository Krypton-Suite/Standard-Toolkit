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
    /// Access to the double of back and border palettes.
    /// </summary>
    public interface IPaletteDouble
    {
        /// <summary>
        /// Gets the background palette.
        /// </summary>
        IPaletteBack PaletteBack { get; }

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        IPaletteBorder? PaletteBorder { get; }
    }
}