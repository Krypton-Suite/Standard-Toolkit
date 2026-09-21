#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
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

    #region Public
    /// <inheritdoc />
    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;

    /// <inheritdoc />
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => true;

    /// <inheritdoc />
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
    {
        var values = new List<PaletteMode> { PaletteMode.Global };
        foreach (var pair in PaletteModeStrings.SupportedThemes.FirstToSecond)
        {
            if (pair.Value == PaletteMode.Custom)
            {
                if (KryptonThemeAvailability.AllowCustomThemes)
                {
                    values.Add(PaletteMode.Custom);
                }

                continue;
            }

            if (KryptonThemeAvailability.IsSelectable(pair.Value))
            {
                values.Add(pair.Value);
            }
        }

        return new StandardValuesCollection(values);
    }

    #endregion
}