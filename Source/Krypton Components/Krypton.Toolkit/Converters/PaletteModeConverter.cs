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
/// Custom type converter so that PaletteMode values appear as neat text at design time.
/// </summary>
public class PaletteModeConverter : StringLookupConverter<PaletteMode>
{
    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<string /*Display*/, PaletteMode /*Enum*/ > PairsStringToEnum => PaletteModeStrings.SupportedThemes.FirstToSecond;
    protected override IReadOnlyDictionary<PaletteMode /*Enum*/, string /*Display*/> PairsEnumToString => PaletteModeStrings.SupportedThemes.SecondToFirst;

    #endregion
}