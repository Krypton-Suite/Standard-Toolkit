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
/// Custom type converter so that HeaderStyle values appear as neat text at design time.
/// </summary>
internal class HeaderStyleConverter : StringLookupConverter<HeaderStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<HeaderStyle, string> _pairs = new BiDictionary<HeaderStyle, string>(
        new Dictionary<HeaderStyle, string>
        {
            {HeaderStyle.Primary, DesignTimeUtilities.DEFAULT_HEADER_STYLE_PRIMARY},
            {HeaderStyle.Secondary, DesignTimeUtilities.DEFAULT_HEADER_STYLE_SECONDARY},
            {HeaderStyle.DockInactive, DesignTimeUtilities.DEFAULT_HEADER_STYLE_DOCK_INACTIVE},
            {HeaderStyle.DockActive, DesignTimeUtilities.DEFAULT_HEADER_STYLE_DOCK_ACTIVE},
            {HeaderStyle.Form, DesignTimeUtilities.DEFAULT_HEADER_STYLE_FORM},
            {HeaderStyle.Calendar, DesignTimeUtilities.DEFAULT_HEADER_STYLE_CALENDAR},
            {HeaderStyle.Custom1, DesignTimeUtilities.DEFAULT_HEADER_STYLE_CUSTOM_ONE},
            {HeaderStyle.Custom2, DesignTimeUtilities.DEFAULT_HEADER_STYLE_CUSTOM_TWO},
            {HeaderStyle.Custom3, DesignTimeUtilities.DEFAULT_HEADER_STYLE_CUSTOM_THREE}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<HeaderStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, HeaderStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}