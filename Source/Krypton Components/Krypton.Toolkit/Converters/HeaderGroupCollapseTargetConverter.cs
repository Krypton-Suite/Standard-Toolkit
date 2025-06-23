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
/// Custom type converter so that HeaderGroupCollapseTarget values appear as neat text at design time.
/// </summary>
internal class HeaderGroupCollapsedTargetConverter : StringLookupConverter<HeaderGroupCollapsedTarget>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<HeaderGroupCollapsedTarget, string> _pairs = new BiDictionary<HeaderGroupCollapsedTarget, string>(
        new Dictionary<HeaderGroupCollapsedTarget, string>
        {
            { HeaderGroupCollapsedTarget.CollapsedToPrimary, DesignTimeUtilities.DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_PRIMARY},
            {HeaderGroupCollapsedTarget.CollapsedToSecondary, DesignTimeUtilities.DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_SECONDARY},
            {HeaderGroupCollapsedTarget.CollapsedToBoth, DesignTimeUtilities.DEFAULT_HEADER_GROUP_COLLAPSED_TARGET_COLLAPSED_TO_BOTH}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<HeaderGroupCollapsedTarget /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, HeaderGroupCollapsedTarget /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}