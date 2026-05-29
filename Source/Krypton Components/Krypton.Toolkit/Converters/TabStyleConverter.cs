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
/// Custom type converter so that TabStyle values appear as neat text at design time.
/// </summary>
internal class TabStyleConverter : StringLookupConverter<TabStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<TabStyle, string> _pairs = new BiDictionary<TabStyle, string>(
        new Dictionary<TabStyle, string>
        {
            {TabStyle.HighProfile, DesignTimeUtilities.DEFAULT_TAB_STYLE_HIGH_PROFILE},
            {TabStyle.StandardProfile, DesignTimeUtilities.DEFAULT_TAB_STYLE_STANDARD_PROFILE},
            {TabStyle.LowProfile, DesignTimeUtilities.DEFAULT_TAB_STYLE_LOW_PROFILE},
            {TabStyle.OneNote, DesignTimeUtilities.DEFAULT_TAB_STYLE_ONE_NOTE},
            {TabStyle.Dock, DesignTimeUtilities.DEFAULT_TAB_STYLE_DOCK},
            {TabStyle.DockAutoHidden, DesignTimeUtilities.DEFAULT_TAB_STYLE_DOCK_AUTO_HIDDEN},
            {TabStyle.Custom1, DesignTimeUtilities.DEFAULT_TAB_STYLE_CUSTOM1},
            {TabStyle.Custom2, DesignTimeUtilities.DEFAULT_TAB_STYLE_CUSTOM2},
            {TabStyle.Custom3, DesignTimeUtilities.DEFAULT_TAB_STYLE_CUSTOM3}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<TabStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, TabStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}