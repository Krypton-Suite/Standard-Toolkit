#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteTextTrim values appear as neat text at design time.
    /// </summary>
    internal class PaletteTextTrimConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTextTrimConverter clas.
        /// </summary>
        public PaletteTextTrimConverter()
            : base(typeof(PaletteTextTrim))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(PaletteTextTrim.Inherit,              "Inherit"),
            new Pair(PaletteTextTrim.Hide,                 "Hide"),
            new Pair(PaletteTextTrim.Character,            "Character"),
            new Pair(PaletteTextTrim.Word,                 "Word"),
            new Pair(PaletteTextTrim.EllipsisCharacter,    "Ellipsis Character"),
            new Pair(PaletteTextTrim.EllipsisWord,         "Ellipsis Word"),
            new Pair(PaletteTextTrim.EllipsisPath,         "Ellipsis Path") };

        #endregion
    }
}
