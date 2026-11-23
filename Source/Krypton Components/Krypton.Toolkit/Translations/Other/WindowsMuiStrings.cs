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
/// Public access to the verified Windows MUI string catalog and System32 resource loading.
/// Prefer <see cref="KryptonManager.Strings"/>.CommonStrings / <c>UseWindowsLanguagePackStrings</c> for product UI.
/// Raw module/resource IDs are undocumented and may change across Windows releases.
/// </summary>
public static class WindowsMuiStrings
{
    /// <summary>
    /// Gets a verified catalog string, falling back to the catalog English default when the OS string is unavailable.
    /// </summary>
    public static string Get(WindowsMuiStringId id)
    {
        string? cache = null;
        string fallback = WindowsMuiStringCatalog.GetFallback(id);
        return OsMuiStringLoader.Load(id, fallback, ref cache);
    }

    /// <summary>
    /// Gets a verified catalog string with an explicit fallback.
    /// </summary>
    public static string Get(WindowsMuiStringId id, string fallback)
    {
        string? cache = null;
        return OsMuiStringLoader.Load(id, fallback, ref cache);
    }

    /// <summary>
    /// Tries to load a string resource from an allowed System32 module (basename only).
    /// </summary>
    /// <param name="moduleFileName">Module basename such as <c>user32.dll</c>.</param>
    /// <param name="resourceId">Undocumented string resource ID.</param>
    /// <param name="value">Receives the loaded string when this method returns <c>true</c>.</param>
    /// <returns><c>true</c> when a non-empty OS string was loaded; otherwise <c>false</c>.</returns>
    [CLSCompliant(false)]
    public static bool TryLoad(string moduleFileName, uint resourceId, out string value)
    {
        value = OsMuiStringLoader.Load(moduleFileName, resourceId, string.Empty);
        return value.Length > 0;
    }

    /// <summary>
    /// Loads a string resource from an allowed System32 module, returning <paramref name="fallback"/> on failure.
    /// </summary>
    [CLSCompliant(false)]
    public static string Load(string moduleFileName, uint resourceId, string fallback) =>
        OsMuiStringLoader.Load(moduleFileName, resourceId, fallback ?? string.Empty);

    /// <summary>
    /// Clears the process-wide MUI string cache.
    /// </summary>
    public static void ClearCache() => OsMuiStringLoader.ClearCache();
}
