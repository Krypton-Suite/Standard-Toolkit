#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Opt-in translation of keys that Merge Missing adds to an existing culture file.
/// Both <see cref="AutoTranslateMissingStrings"/> and <see cref="Translator"/> must be set.
/// The toolkit does not call a translation service on its own.
/// </summary>
public static class KryptonStringTranslation
{
    /// <summary>
    /// When <see langword="true"/>, Merge Missing asks <see cref="Translator"/> to fill keys that were absent from the file.
    /// Defaults to <see langword="false"/>. Existing translations are never sent to the translator.
    /// </summary>
    public static bool AutoTranslateMissingStrings { get; set; }

    /// <summary>
    /// Application-supplied translator. Ignored while <see cref="AutoTranslateMissingStrings"/> is <see langword="false"/>.
    /// </summary>
    public static IKryptonStringTranslator? Translator { get; set; }

    /// <summary>
    /// Built-in catalogue language. Toolkit defaults are English.
    /// </summary>
    public static readonly CultureInfo SourceCulture = CultureInfo.GetCultureInfo(@"en");

    /// <summary>
    /// Translates one placeholder when auto-translate is enabled. Returns <see langword="null"/> when the placeholder should stay as-is.
    /// </summary>
    internal static string? TranslatePlaceholder(string text, string? targetCultureName, string keyPath)
    {
        if (!AutoTranslateMissingStrings || Translator == null || string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(targetCultureName))
        {
            return null;
        }

        CultureInfo targetCulture;
        try
        {
            targetCulture = CultureInfo.GetCultureInfo(targetCultureName);
        }
        catch (CultureNotFoundException)
        {
            Debug.WriteLine($@"[Krypton] Auto-translate skipped. Culture '{targetCultureName}' was not recognised.");
            return null;
        }

        if (string.Equals(targetCulture.TwoLetterISOLanguageName, SourceCulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        try
        {
            var translated = Translator.Translate(text, SourceCulture, targetCulture, keyPath);
            return string.IsNullOrEmpty(translated) ? null : translated;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($@"[Krypton] Auto-translate failed for '{keyPath}': {ex.Message}");
            return null;
        }
    }
}
