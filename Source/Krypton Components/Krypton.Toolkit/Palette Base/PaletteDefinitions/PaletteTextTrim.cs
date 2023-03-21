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
    /// Specifies how to trim text.
    /// </summary>
    [TypeConverter(typeof(PaletteTextTrimConverter))]
    public enum PaletteTextTrim
    {
        /// <summary>
        /// Specifies text trim should be inherited.
        /// </summary>
        Inherit = -1,

        /// <summary>
        /// Specifies text is not drawn if it needs trimming.
        /// </summary>
        Hide,

        /// <summary>
        /// Specifies text is trimmed by removing end characters.
        /// </summary>
        Character,

        /// <summary>
        /// Specifies text is trimmed by removing end words.
        /// </summary>
        Word,

        /// <summary>
        /// Specifies text is trimmed by using ellipses and removing end characters.
        /// </summary>
        EllipsisCharacter,

        /// <summary>
        /// Specifies text is trimmed by using ellipses and removing end words.
        /// </summary>
        EllipsisWord,

        /// <summary>
        /// Specifies text is trimmed by using ellipses and removing from middle.
        /// </summary>
        EllipsisPath
    }
}
