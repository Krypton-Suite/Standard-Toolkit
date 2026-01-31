#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Custom type converter so that <see cref="T:KryptonMessageBoxIcon"/> values appear as neat text at design time.</summary>
internal class KryptonMessageBoxIconConverter : StringLookupConverter<KryptonMessageBoxIcon>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<KryptonMessageBoxIcon, string> _iconPairs =
        new BiDictionary<KryptonMessageBoxIcon, string>(new Dictionary<KryptonMessageBoxIcon, string>
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
            { KryptonMessageBoxIcon.SystemStop, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_STOP },
            { KryptonMessageBoxIcon.Error, DesignTimeUtilities.DEFAULT_ICON_ERROR },
            { KryptonMessageBoxIcon.SystemError, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ERROR },
            { KryptonMessageBoxIcon.Warning, DesignTimeUtilities.DEFAULT_ICON_WARNING },
            { KryptonMessageBoxIcon.SystemWarning, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_WARNING },
            { KryptonMessageBoxIcon.Information, DesignTimeUtilities.DEFAULT_ICON_INFORMATION },
            { KryptonMessageBoxIcon.SystemInformation, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_INFORMATION },
            { KryptonMessageBoxIcon.Shield, DesignTimeUtilities.DEFAULT_ICON_SHIELD },
            { KryptonMessageBoxIcon.WindowsLogo, DesignTimeUtilities.DEFAULT_ICON_WINDOWS_LOGO },
            { KryptonMessageBoxIcon.Application, DesignTimeUtilities.DEFAULT_ICON_APPLICATION },
            { KryptonMessageBoxIcon.SystemApplication, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_APPLICATION }
        });

    #endregion

    #region Protected

    /// <summary>Gets an array of lookup pairs.</summary>
    protected override IReadOnlyDictionary<string, KryptonMessageBoxIcon> PairsStringToEnum => _iconPairs.SecondToFirst;

    /// <summary>Gets the pairs enum to string.</summary>
    /// <value>The pairs enum to string.</value>
    protected override IReadOnlyDictionary<KryptonMessageBoxIcon, string> PairsEnumToString => _iconPairs.FirstToSecond;

    #endregion
}