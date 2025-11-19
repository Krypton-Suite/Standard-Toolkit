#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Custom type converter so that <see cref="T:KryptonToastNotificationIcon"/> values appear as neat text at design time.</summary>
internal class KryptonToastNotificationIconConverter : StringLookupConverter<KryptonToastNotificationIcon>
{
    #region Static Fields

    [Localizable(true)] 
    private static readonly BiDictionary<KryptonToastNotificationIcon, string> _iconPairs =
        new BiDictionary<KryptonToastNotificationIcon, string>(new Dictionary<KryptonToastNotificationIcon, string>
        {
            { KryptonToastNotificationIcon.None, DesignTimeUtilities.DEFAULT_ICON_NONE },
            { KryptonToastNotificationIcon.Hand, DesignTimeUtilities.DEFAULT_ICON_HAND },
            { KryptonToastNotificationIcon.SystemHand, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_HAND },
            { KryptonToastNotificationIcon.Question, DesignTimeUtilities.DEFAULT_ICON_QUESTION },
            { KryptonToastNotificationIcon.SystemQuestion, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_QUESTION },
            { KryptonToastNotificationIcon.Exclamation, DesignTimeUtilities.DEFAULT_ICON_EXCLAMATION },
            { KryptonToastNotificationIcon.SystemExclamation, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_EXCLAMATION },
            { KryptonToastNotificationIcon.Asterisk, DesignTimeUtilities.DEFAULT_ICON_ASTERISK },
            { KryptonToastNotificationIcon.SystemAsterisk, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ASTERISK },
            { KryptonToastNotificationIcon.Stop, DesignTimeUtilities.DEFAULT_ICON_STOP },
            { KryptonToastNotificationIcon.SystemStop, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_STOP },
            { KryptonToastNotificationIcon.Error, DesignTimeUtilities.DEFAULT_ICON_ERROR },
            { KryptonToastNotificationIcon.SystemError, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ERROR },
            { KryptonToastNotificationIcon.Warning, DesignTimeUtilities.DEFAULT_ICON_WARNING },
            { KryptonToastNotificationIcon.SystemWarning, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_WARNING },
            { KryptonToastNotificationIcon.Information, DesignTimeUtilities.DEFAULT_ICON_INFORMATION },
            { KryptonToastNotificationIcon.SystemInformation, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_INFORMATION },
            { KryptonToastNotificationIcon.Shield, DesignTimeUtilities.DEFAULT_ICON_SHIELD },
            { KryptonToastNotificationIcon.WindowsLogo, DesignTimeUtilities.DEFAULT_ICON_WINDOWS_LOGO },
            { KryptonToastNotificationIcon.Application, DesignTimeUtilities.DEFAULT_ICON_APPLICATION },
            { KryptonToastNotificationIcon.SystemApplication, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_APPLICATION },
            { KryptonToastNotificationIcon.Ok, DesignTimeUtilities.DEFAULT_ICON_OK },
            { KryptonToastNotificationIcon.Custom, DesignTimeUtilities.DEFAULT_ICON_CUSTOM }
        });

    #endregion

    #region Protected

    /// <summary>Gets an array of lookup pairs.</summary>
    protected override IReadOnlyDictionary<string, KryptonToastNotificationIcon> PairsStringToEnum => _iconPairs.SecondToFirst;

    /// <summary>Gets the pairs enum to string.</summary>
    /// <value>The pairs enum to string.</value>
    protected override IReadOnlyDictionary<KryptonToastNotificationIcon, string> PairsEnumToString => _iconPairs.FirstToSecond;

    #endregion
}