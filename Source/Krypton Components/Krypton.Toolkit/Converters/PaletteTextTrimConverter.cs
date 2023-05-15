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
    /// Custom type converter so that PaletteTextTrim values appear as neat text at design time.
    /// </summary>
    internal class PaletteTextTrimConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(PaletteTextTrim.Inherit, "Inherit"),
        //    new(PaletteTextTrim.Hide, "Hide"),
        //    new(PaletteTextTrim.Character, "Character"),
        //    new(PaletteTextTrim.Word, "Word"),
        //    new(PaletteTextTrim.EllipsisCharacter, "Ellipsis Character"),
        //    new(PaletteTextTrim.EllipsisWord, "Ellipsis Word"),
        //    new(PaletteTextTrim.EllipsisPath, "Ellipsis Path")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new(PaletteTextTrim.Inherit, KryptonLanguageManager.TextTrimStrings.Inherit),
            new(PaletteTextTrim.Hide, KryptonLanguageManager.TextTrimStrings.Hide),
            new(PaletteTextTrim.Character, KryptonLanguageManager.TextTrimStrings.Character),
            new(PaletteTextTrim.Word, KryptonLanguageManager.TextTrimStrings.Word),
            new(PaletteTextTrim.EllipsisCharacter, KryptonLanguageManager.TextTrimStrings.EllipsisCharacter),
            new(PaletteTextTrim.EllipsisWord, KryptonLanguageManager.TextTrimStrings.EllipsisWord),
            new(PaletteTextTrim.EllipsisPath, KryptonLanguageManager.TextTrimStrings.EllipsisPath)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTextTrimConverter class.
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
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
