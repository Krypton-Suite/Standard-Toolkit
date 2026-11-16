#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Advanced System32-only Windows resource string loader.
/// Prefer <see cref="KryptonManager.Strings"/>.CommonStrings and
/// <see cref="WindowsMuiStrings"/> for supported product UI text.
/// Raw resource IDs are undocumented and version-dependent.
/// </summary>
/// <remarks>
/// Requires the <c>Krypton.Standard.Toolkit</c> NuGet package (<c>Krypton.Toolkit.Utilities</c> assembly).
/// </remarks>
public static class WindowsSystemStringLoader
{
    /// <summary>
    /// Tries to load a string from an allowed System32 module basename (for example <c>user32.dll</c>).
    /// Paths and directory traversal are rejected.
    /// </summary>
    /// <param name="moduleFileName">Module basename under <c>%SystemRoot%\System32</c>.</param>
    /// <param name="resourceId">Undocumented string resource ID.</param>
    /// <param name="value">Receives the loaded string when successful.</param>
    /// <returns><c>true</c> when a non-empty string was loaded; otherwise <c>false</c>.</returns>
    public static bool TryLoad(string moduleFileName, uint resourceId, out string value) =>
        WindowsMuiStrings.TryLoad(moduleFileName, resourceId, out value);

    /// <summary>
    /// Loads a System32 module string resource, returning <paramref name="fallback"/> when unavailable.
    /// </summary>
    public static string Load(string moduleFileName, uint resourceId, string fallback) =>
        WindowsMuiStrings.Load(moduleFileName, resourceId, fallback);

    /// <summary>
    /// Loads a verified catalog entry via <see cref="WindowsMuiStrings.Get(WindowsMuiStringId)"/>.
    /// </summary>
    public static string GetCatalogString(WindowsMuiStringId id) =>
        WindowsMuiStrings.Get(id);

    /// <summary>
    /// Loads a verified catalog entry with an explicit fallback.
    /// </summary>
    public static string GetCatalogString(WindowsMuiStringId id, string fallback) =>
        WindowsMuiStrings.Get(id, fallback);

    /// <summary>
    /// Clears the shared MUI string cache.
    /// </summary>
    public static void ClearCache() => WindowsMuiStrings.ClearCache();
}
