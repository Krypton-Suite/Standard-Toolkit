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
    /// Specifies how to show hotkey prefix characters.
    /// </summary>
    public enum PaletteTextHotkeyPrefix
    {
        /// <summary>
        /// Specifies text prefix should be inherited.
        /// </summary>
        Inherit = -1,

        /// <summary>Turns off processing of prefix characters.</summary>
        None,

        /// <summary>Turns on processing of prefix characters.</summary>
        Show,

        /// <summary>Ignores the ampersand prefix character in text.</summary>
        Hide
    }
}