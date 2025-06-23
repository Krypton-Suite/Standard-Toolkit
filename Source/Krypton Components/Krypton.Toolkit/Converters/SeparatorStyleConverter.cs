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
/// Custom type converter so that SeparatorStyle values appear as neat text at design time.
/// </summary>
internal class SeparatorStyleConverter : StringLookupConverter<SeparatorStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<SeparatorStyle, string> _pairs = new BiDictionary<SeparatorStyle, string>(
        new Dictionary<SeparatorStyle, string>
        {
            {SeparatorStyle.LowProfile, DesignTimeUtilities.DEFAULT_SEPARATOR_STYLE_LOW_PROFILE},
            {SeparatorStyle.HighProfile, DesignTimeUtilities.DEFAULT_SEPARATOR_STYLE_HIGH_PROFILE},
            {SeparatorStyle.HighInternalProfile, DesignTimeUtilities.DEFAULT_SEPARATOR_STYLE_HIGH_INTERNAL_PROFILE},
            {SeparatorStyle.Custom1, DesignTimeUtilities.DEFAULT_SEPARATOR_STYLE_CUSTOM1},
            {SeparatorStyle.Custom2, DesignTimeUtilities.DEFAULT_SEPARATOR_STYLE_CUSTOM2},
            {SeparatorStyle.Custom3, DesignTimeUtilities.DEFAULT_SEPARATOR_STYLE_CUSTOM3}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<SeparatorStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, SeparatorStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}