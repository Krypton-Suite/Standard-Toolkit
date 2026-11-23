#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a design-time dropdown of common UI culture names while still allowing free-form entry.
/// </summary>
internal class KryptonTranslationsCultureNameConverter : StringConverter
{
    private static readonly string[] StandardCultures =
    {
        @"en-US",
        @"en-GB",
        @"de-DE",
        @"fr-FR",
        @"es-ES",
        @"it-IT",
        @"pt-BR",
        @"nl-NL",
        @"sv-SE",
        @"pl-PL",
        @"ru-RU",
        @"ja-JP",
        @"zh-CN",
        @"zh-TW",
        @"ko-KR",
        @"ar-SA",
        @"he-IL",
        @"tr-TR",
        @"cs-CZ",
        @"hu-HU"
    };

    /// <inheritdoc />
    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;

    /// <inheritdoc />
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => false;

    /// <inheritdoc />
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
    {
        var values = new List<string>(StandardCultures);
        var current = CultureInfo.CurrentUICulture.Name;
        if (!string.IsNullOrEmpty(current) &&
            !values.Exists(name => string.Equals(name, current, StringComparison.OrdinalIgnoreCase)))
        {
            values.Insert(0, current);
        }

        var active = KryptonManager.ActiveTranslationsCulture?.Name;
        if (!string.IsNullOrEmpty(active) &&
            !values.Exists(name => string.Equals(name, active, StringComparison.OrdinalIgnoreCase)))
        {
            values.Insert(0, active!);
        }

        return new StandardValuesCollection(values);
    }
}
