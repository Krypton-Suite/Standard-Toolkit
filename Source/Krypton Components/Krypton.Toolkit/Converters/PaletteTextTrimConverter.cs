#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Custom type converter so that PaletteTextTrim values appear as neat text at design time.
/// </summary>
internal class PaletteTextTrimConverter : StringLookupConverter<PaletteTextTrim>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteTextTrim, string> _pairs = new BiDictionary<PaletteTextTrim, string>(
        new Dictionary<PaletteTextTrim, string>
        {
            {PaletteTextTrim.Inherit, DesignTimeUtilities.DEFAULT_PALETTE_TEXT_TRIM_INHERIT},
            {PaletteTextTrim.Hide, DesignTimeUtilities.DEFAULT_PALETTE_TEXT_TRIM_HIDE},
            {PaletteTextTrim.Character, DesignTimeUtilities.DEFAULT_PALETTE_TEXT_TRIM_CHARACTER},
            {PaletteTextTrim.Word, DesignTimeUtilities.DEFAULT_PALETTE_TEXT_TRIM_WORD},
            {PaletteTextTrim.EllipsisCharacter, DesignTimeUtilities.DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_CHARACTER},
            {PaletteTextTrim.EllipsisWord, DesignTimeUtilities.DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_WORD},
            {PaletteTextTrim.EllipsisPath, DesignTimeUtilities.DEFAULT_PALETTE_TEXT_TRIM_ELLIPSIS_PATH}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteTextTrim /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteTextTrim /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}