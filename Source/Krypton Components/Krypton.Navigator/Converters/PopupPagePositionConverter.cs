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

namespace Krypton.Navigator;

/// <summary>
/// Custom type converter so that PopupPagePosition values appear as neat text at design time.
/// </summary>
public class PopupPagePositionConverter : StringLookupConverter<PopupPagePosition>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PopupPagePosition, string> _pairs = new BiDictionary<PopupPagePosition, string>(
        new Dictionary<PopupPagePosition, string>
        {
            {PopupPagePosition.ModeAppropriate, @"Mode Appropriate"},
            {PopupPagePosition.AboveFar, @"Above Element - Far Aligned"},
            {PopupPagePosition.AboveMatch, @"Above Element - Element Width"},
            {PopupPagePosition.AboveNear, @"Above Element - Near Aligned"},
            {PopupPagePosition.BelowFar, @"Below Element - Far Aligned"},
            {PopupPagePosition.BelowMatch, @"Below Element - Element Width"},
            {PopupPagePosition.BelowNear, @"Below Element - Near Aligned"},
            {PopupPagePosition.FarBottom, @"Far Side of Element - Bottom Aligned"},
            {PopupPagePosition.FarMatch, @"Far Side of Element - Element Height"},
            {PopupPagePosition.FarTop, @"Far Side of Element - Top Aligned"},
            {PopupPagePosition.NearBottom, @"Near Side of Element - Bottom Aligned"},
            {PopupPagePosition.NearMatch, @"Near Side of Element - Element Height"},
            {PopupPagePosition.NearTop, @"Near Side of Element - Top Aligned"}
        });

    #endregion
    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<string /*Display*/, PopupPagePosition /*Enum*/ > PairsStringToEnum  => _pairs.SecondToFirst;
    protected override IReadOnlyDictionary<PopupPagePosition /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    #endregion
}