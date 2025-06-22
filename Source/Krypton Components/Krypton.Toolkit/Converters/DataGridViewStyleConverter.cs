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
/// Custom type converter so that DataGridViewStyle values appear as neat text at design time.
/// </summary>
internal class DataGridViewStyleConverter : StringLookupConverter<DataGridViewStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<DataGridViewStyle, string> _pairs = new BiDictionary<DataGridViewStyle, string>( 
        new Dictionary<DataGridViewStyle, string>
        {
            {DataGridViewStyle.List, DesignTimeUtilities.DEFAULT_DATA_GRID_VIEW_STYLE_LIST},
            {DataGridViewStyle.Sheet, DesignTimeUtilities.DEFAULT_DATA_GRID_VIEW_STYLE_SHEET},
            {DataGridViewStyle.Custom1, DesignTimeUtilities.DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_ONE},
            {DataGridViewStyle.Custom2, DesignTimeUtilities.DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_TWO},
            {DataGridViewStyle.Custom3, DesignTimeUtilities.DEFAULT_DATA_GRID_VIEW_STYLE_CUSTOM_THREE},
            {DataGridViewStyle.Mixed, DesignTimeUtilities.DEFAULT_DATA_GRID_VIEW_STYLE_MIXED}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<DataGridViewStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, DataGridViewStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}