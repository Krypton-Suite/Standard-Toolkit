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
/// Translates a built-in English placeholder into the culture of a translations file.
/// Return <see langword="null"/> or empty to leave the placeholder unchanged.
/// </summary>
public interface IKryptonStringTranslator
{
    /// <summary>
    /// Translates <paramref name="text"/> from <paramref name="sourceCulture"/> into <paramref name="targetCulture"/>.
    /// </summary>
    /// <param name="text">The current placeholder, normally the English toolkit default.</param>
    /// <param name="sourceCulture">Culture of <paramref name="text"/>.</param>
    /// <param name="targetCulture">Culture declared by the translations file.</param>
    /// <param name="keyPath">Catalog path being filled, such as <c>CommonStrings.General.OK</c>.</param>
    /// <returns>The translated text, or <see langword="null"/> to keep <paramref name="text"/>.</returns>
    string? Translate(string text, CultureInfo sourceCulture, CultureInfo targetCulture, string keyPath);
}
