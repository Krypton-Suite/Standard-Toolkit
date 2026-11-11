#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Custom type converter so that <see cref="T:KryptonToastIcon"/> values appear as neat text at design time.</summary>
/// <remarks>
/// <see cref="KryptonToastIcon"/> mirrors <see cref="MessageBoxIcon"/> aliases that share underlying values
/// (<c>SystemHand</c>/<c>SystemStop</c>/<c>SystemError</c>, etc.). Those cannot share a single bi-dictionary key,
/// so enum→string keeps one canonical display name per unique value while string→enum accepts every alias name.
/// </remarks>
internal class KryptonToastIconConverter : StringLookupConverter<KryptonToastIcon>
{
    #region Static Fields

    // One display string per unique underlying value (MessageBoxIcon aliases collapse Hand/Stop/Error, etc.).
    [Localizable(true)]
    private static readonly IReadOnlyDictionary<KryptonToastIcon, string> _pairsEnumToString =
        new Dictionary<KryptonToastIcon, string>
        {
            { KryptonToastIcon.None, DesignTimeUtilities.DEFAULT_ICON_NONE },
            { KryptonToastIcon.Hand, DesignTimeUtilities.DEFAULT_ICON_HAND },
            { KryptonToastIcon.SystemHand, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_HAND },
            { KryptonToastIcon.Question, DesignTimeUtilities.DEFAULT_ICON_QUESTION },
            { KryptonToastIcon.SystemQuestion, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_QUESTION },
            { KryptonToastIcon.Exclamation, DesignTimeUtilities.DEFAULT_ICON_EXCLAMATION },
            { KryptonToastIcon.SystemExclamation, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_EXCLAMATION },
            { KryptonToastIcon.Asterisk, DesignTimeUtilities.DEFAULT_ICON_ASTERISK },
            { KryptonToastIcon.SystemAsterisk, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ASTERISK },
            { KryptonToastIcon.Stop, DesignTimeUtilities.DEFAULT_ICON_STOP },
            { KryptonToastIcon.Error, DesignTimeUtilities.DEFAULT_ICON_ERROR },
            { KryptonToastIcon.Warning, DesignTimeUtilities.DEFAULT_ICON_WARNING },
            { KryptonToastIcon.Information, DesignTimeUtilities.DEFAULT_ICON_INFORMATION },
            { KryptonToastIcon.Shield, DesignTimeUtilities.DEFAULT_ICON_SHIELD },
            { KryptonToastIcon.WindowsLogo, DesignTimeUtilities.DEFAULT_ICON_WINDOWS_LOGO },
            { KryptonToastIcon.Application, DesignTimeUtilities.DEFAULT_ICON_APPLICATION },
            { KryptonToastIcon.SystemApplication, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_APPLICATION },
            { KryptonToastIcon.Ok, DesignTimeUtilities.DEFAULT_ICON_OK },
            { KryptonToastIcon.Custom, DesignTimeUtilities.DEFAULT_ICON_CUSTOM }
        };

    // Alias display names (Stop/Error/Warning/Information System) map onto the shared MessageBoxIcon values.
    [Localizable(true)]
    private static readonly IReadOnlyDictionary<string, KryptonToastIcon> _pairsStringToEnum =
        new Dictionary<string, KryptonToastIcon>
        {
            { DesignTimeUtilities.DEFAULT_ICON_NONE, KryptonToastIcon.None },
            { DesignTimeUtilities.DEFAULT_ICON_HAND, KryptonToastIcon.Hand },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_HAND, KryptonToastIcon.SystemHand },
            { DesignTimeUtilities.DEFAULT_ICON_QUESTION, KryptonToastIcon.Question },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_QUESTION, KryptonToastIcon.SystemQuestion },
            { DesignTimeUtilities.DEFAULT_ICON_EXCLAMATION, KryptonToastIcon.Exclamation },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_EXCLAMATION, KryptonToastIcon.SystemExclamation },
            { DesignTimeUtilities.DEFAULT_ICON_ASTERISK, KryptonToastIcon.Asterisk },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ASTERISK, KryptonToastIcon.SystemAsterisk },
            { DesignTimeUtilities.DEFAULT_ICON_STOP, KryptonToastIcon.Stop },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_STOP, KryptonToastIcon.SystemStop },
            { DesignTimeUtilities.DEFAULT_ICON_ERROR, KryptonToastIcon.Error },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ERROR, KryptonToastIcon.SystemError },
            { DesignTimeUtilities.DEFAULT_ICON_WARNING, KryptonToastIcon.Warning },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_WARNING, KryptonToastIcon.SystemWarning },
            { DesignTimeUtilities.DEFAULT_ICON_INFORMATION, KryptonToastIcon.Information },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_INFORMATION, KryptonToastIcon.SystemInformation },
            { DesignTimeUtilities.DEFAULT_ICON_SHIELD, KryptonToastIcon.Shield },
            { DesignTimeUtilities.DEFAULT_ICON_WINDOWS_LOGO, KryptonToastIcon.WindowsLogo },
            { DesignTimeUtilities.DEFAULT_ICON_APPLICATION, KryptonToastIcon.Application },
            { DesignTimeUtilities.DEFAULT_ICON_SYSTEM_APPLICATION, KryptonToastIcon.SystemApplication },
            { DesignTimeUtilities.DEFAULT_ICON_OK, KryptonToastIcon.Ok },
            { DesignTimeUtilities.DEFAULT_ICON_CUSTOM, KryptonToastIcon.Custom }
        };

    #endregion

    #region Protected

    /// <summary>Gets an array of lookup pairs.</summary>
    protected override IReadOnlyDictionary<string, KryptonToastIcon> PairsStringToEnum => _pairsStringToEnum;

    /// <summary>Gets the pairs enum to string.</summary>
    /// <value>The pairs enum to string.</value>
    protected override IReadOnlyDictionary<KryptonToastIcon, string> PairsEnumToString => _pairsEnumToString;

    #endregion
}
