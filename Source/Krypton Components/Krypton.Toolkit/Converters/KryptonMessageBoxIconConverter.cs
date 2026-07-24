#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Custom type converter so that <see cref="T:KryptonMessageBoxIcon"/> values appear as neat text at design time.</summary>
/// <remarks>
/// <see cref="KryptonMessageBoxIcon"/> mirrors <see cref="MessageBoxIcon"/> aliases that share underlying values
/// (<c>SystemHand</c>/<c>SystemStop</c>/<c>SystemError</c>, etc.). Those cannot share a single bi-dictionary key,
/// so enum→string keeps one canonical display name per unique value while string→enum accepts every alias name.
/// </remarks>
internal class KryptonMessageBoxIconConverter : StringLookupConverter<KryptonMessageBoxIcon>
{
    #region Static Fields

    // One display string per unique underlying value (MessageBoxIcon aliases collapse Hand/Stop/Error, etc.).
    [Localizable(true)]
    private static readonly IReadOnlyDictionary<KryptonMessageBoxIcon, string> _pairsEnumToString =
        new Dictionary<KryptonMessageBoxIcon, string>
        {
            { KryptonMessageBoxIcon.None, DesignTimeUtilities.DEFAULT_ICON_NONE },
            { KryptonMessageBoxIcon.Hand, DesignTimeUtilities.DEFAULT_ICON_HAND },
            { KryptonMessageBoxIcon.SystemHand, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_HAND },
            { KryptonMessageBoxIcon.Question, DesignTimeUtilities.DEFAULT_ICON_QUESTION },
            { KryptonMessageBoxIcon.SystemQuestion, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_QUESTION },
            { KryptonMessageBoxIcon.Exclamation, DesignTimeUtilities.DEFAULT_ICON_EXCLAMATION },
            { KryptonMessageBoxIcon.SystemExclamation, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_EXCLAMATION },
            { KryptonMessageBoxIcon.Asterisk, DesignTimeUtilities.DEFAULT_ICON_ASTERISK },
            { KryptonMessageBoxIcon.SystemAsterisk, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ASTERISK },
            { KryptonMessageBoxIcon.Stop, DesignTimeUtilities.DEFAULT_ICON_STOP },
            { KryptonMessageBoxIcon.Error, DesignTimeUtilities.DEFAULT_ICON_ERROR },
            { KryptonMessageBoxIcon.Warning, DesignTimeUtilities.DEFAULT_ICON_WARNING },
            { KryptonMessageBoxIcon.Information, DesignTimeUtilities.DEFAULT_ICON_INFORMATION },
            { KryptonMessageBoxIcon.Shield, DesignTimeUtilities.DEFAULT_ICON_SHIELD },
            { KryptonMessageBoxIcon.WindowsLogo, DesignTimeUtilities.DEFAULT_ICON_WINDOWS_LOGO },
            { KryptonMessageBoxIcon.Application, DesignTimeUtilities.DEFAULT_ICON_APPLICATION },
            { KryptonMessageBoxIcon.SystemApplication, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_APPLICATION }
        };

    // Alias display names (Stop/Error/Warning/Information System) map onto the shared MessageBoxIcon values.
    [Localizable(true)]
    private static readonly IReadOnlyDictionary<string, KryptonMessageBoxIcon> _pairsStringToEnum =
        new Dictionary<string, KryptonMessageBoxIcon>
        {
            { DesignTimeUtilities.DEFAULT_ICON_NONE, KryptonMessageBoxIcon.None },
            { DesignTimeUtilities.DEFAULT_ICON_HAND, KryptonMessageBoxIcon.Hand },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_HAND, KryptonMessageBoxIcon.SystemHand },
            { DesignTimeUtilities.DEFAULT_ICON_QUESTION, KryptonMessageBoxIcon.Question },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_QUESTION, KryptonMessageBoxIcon.SystemQuestion },
            { DesignTimeUtilities.DEFAULT_ICON_EXCLAMATION, KryptonMessageBoxIcon.Exclamation },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_EXCLAMATION, KryptonMessageBoxIcon.SystemExclamation },
            { DesignTimeUtilities.DEFAULT_ICON_ASTERISK, KryptonMessageBoxIcon.Asterisk },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ASTERISK, KryptonMessageBoxIcon.SystemAsterisk },
            { DesignTimeUtilities.DEFAULT_ICON_STOP, KryptonMessageBoxIcon.Stop },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_STOP, KryptonMessageBoxIcon.SystemStop },
            { DesignTimeUtilities.DEFAULT_ICON_ERROR, KryptonMessageBoxIcon.Error },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ERROR, KryptonMessageBoxIcon.SystemError },
            { DesignTimeUtilities.DEFAULT_ICON_WARNING, KryptonMessageBoxIcon.Warning },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_WARNING, KryptonMessageBoxIcon.SystemWarning },
            { DesignTimeUtilities.DEFAULT_ICON_INFORMATION, KryptonMessageBoxIcon.Information },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_INFORMATION, KryptonMessageBoxIcon.SystemInformation },
            { DesignTimeUtilities.DEFAULT_ICON_SHIELD, KryptonMessageBoxIcon.Shield },
            { DesignTimeUtilities.DEFAULT_ICON_WINDOWS_LOGO, KryptonMessageBoxIcon.WindowsLogo },
            { DesignTimeUtilities.DEFAULT_ICON_APPLICATION, KryptonMessageBoxIcon.Application },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_APPLICATION, KryptonMessageBoxIcon.SystemApplication }
        };

    #endregion

    #region Protected

    /// <summary>Gets an array of lookup pairs.</summary>
    protected override IReadOnlyDictionary<string, KryptonMessageBoxIcon> PairsStringToEnum => _pairsStringToEnum;

    /// <summary>Gets the pairs enum to string.</summary>
    /// <value>The pairs enum to string.</value>
    protected override IReadOnlyDictionary<KryptonMessageBoxIcon, string> PairsEnumToString => _pairsEnumToString;

    #endregion
}
