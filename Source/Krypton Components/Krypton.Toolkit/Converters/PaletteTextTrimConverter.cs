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
    internal class PaletteTextTrimConverter : StringLookupConverter<PaletteTextTrim>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteTextTrim, string> _pairs = new Dictionary<PaletteTextTrim, string>
        {
            {PaletteTextTrim.Inherit, KryptonLanguageManager.TextTrimStrings.Inherit},
            {PaletteTextTrim.Hide, KryptonLanguageManager.TextTrimStrings.Hide},
            {PaletteTextTrim.Character, KryptonLanguageManager.TextTrimStrings.Character},
            {PaletteTextTrim.Word, KryptonLanguageManager.TextTrimStrings.Word},
            {PaletteTextTrim.EllipsisCharacter, KryptonLanguageManager.TextTrimStrings.EllipsisCharacter},
            {PaletteTextTrim.EllipsisWord, KryptonLanguageManager.TextTrimStrings.EllipsisWord},
            {PaletteTextTrim.EllipsisPath, KryptonLanguageManager.TextTrimStrings.EllipsisPath }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteTextTrim /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
