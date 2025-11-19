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
/// Custom type converter so that KryptonLinkBehavior values appear as neat text at design time.
/// </summary>
internal class KryptonLinkBehaviorConverter : StringLookupConverter<KryptonLinkBehavior>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<KryptonLinkBehavior, string> _pairs = new BiDictionary<KryptonLinkBehavior, string>(
        new Dictionary<KryptonLinkBehavior, string>
        {
            {KryptonLinkBehavior.AlwaysUnderline, DesignTimeUtilities.DEFAULT_LINK_BEHAVIOR_ALWAYS_UNDERLINE},
            {KryptonLinkBehavior.HoverUnderline, DesignTimeUtilities.DEFAULT_LINK_BEHAVIOR_HOVER_UNDERLINE},
            {KryptonLinkBehavior.NeverUnderline, DesignTimeUtilities.DEFAULT_LINK_BEHAVIOR_NEVER_UNDERLINE}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<KryptonLinkBehavior /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, KryptonLinkBehavior /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}