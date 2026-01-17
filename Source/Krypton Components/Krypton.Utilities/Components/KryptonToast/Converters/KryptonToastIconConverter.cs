#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>Custom type converter so that <see cref="T:KryptonToastIcon"/> values appear as neat text at design time.</summary>
internal class KryptonToastIconConverter : StringLookupConverter<KryptonToastIcon>
{
    #region Static Fields

    [Localizable(true)] 
    private static readonly BiDictionary<KryptonToastIcon, string> _iconPairs =
        new BiDictionary<KryptonToastIcon, string>(new Dictionary<KryptonToastIcon, string>
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
            { KryptonToastIcon.SystemStop, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_STOP },
            { KryptonToastIcon.Error, DesignTimeUtilities.DEFAULT_ICON_ERROR },
            { KryptonToastIcon.SystemError, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_ERROR },
            { KryptonToastIcon.Warning, DesignTimeUtilities.DEFAULT_ICON_WARNING },
            { KryptonToastIcon.SystemWarning, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_WARNING },
            { KryptonToastIcon.Information, DesignTimeUtilities.DEFAULT_ICON_INFORMATION },
            { KryptonToastIcon.SystemInformation, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_INFORMATION },
            { KryptonToastIcon.Shield, DesignTimeUtilities.DEFAULT_ICON_SHIELD },
            { KryptonToastIcon.WindowsLogo, DesignTimeUtilities.DEFAULT_ICON_WINDOWS_LOGO },
            { KryptonToastIcon.Application, DesignTimeUtilities.DEFAULT_ICON_APPLICATION },
            { KryptonToastIcon.SystemApplication, DesignTimeUtilities.DEFAULT_ICON_SYSTEM_APPLICATION },
            { KryptonToastIcon.Ok, DesignTimeUtilities.DEFAULT_ICON_OK },
            { KryptonToastIcon.Custom, DesignTimeUtilities.DEFAULT_ICON_CUSTOM }
        });

    #endregion

    #region Protected

    /// <summary>Gets an array of lookup pairs.</summary>
    protected override IReadOnlyDictionary<string, KryptonToastIcon> PairsStringToEnum => _iconPairs.SecondToFirst;

    /// <summary>Gets the pairs enum to string.</summary>
    /// <value>The pairs enum to string.</value>
    protected override IReadOnlyDictionary<KryptonToastIcon, string> PairsEnumToString => _iconPairs.FirstToSecond;

    #endregion
}