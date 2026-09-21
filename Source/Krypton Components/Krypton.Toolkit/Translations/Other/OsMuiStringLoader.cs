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
/// Loads localized UI strings from Windows MUI language-pack resources (for example user32.dll / shell32.dll).
/// Modules are resolved under System32 only; resource IDs are undocumented and best-effort.
/// </summary>
internal static class OsMuiStringLoader
{
    private static readonly object Sync = new object();
    private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    // Basename allowlist — never load from arbitrary paths.
    private static readonly HashSet<string> AllowedModules = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        Libraries.User32,
        Libraries.Shell32
    };

    /// <summary>
    /// Gets an OS-defined string from the verified <see cref="WindowsMuiStringCatalog"/>, with caching and fallback.
    /// </summary>
    public static string Load(WindowsMuiStringId id, string defaultValue, ref string? cache)
    {
        if (cache != null)
        {
            return cache;
        }

        if (!WindowsMuiStringCatalog.TryGet(id, out string moduleFileName, out uint resourceId, out string catalogFallback))
        {
            cache = defaultValue;
            return defaultValue;
        }

        var fallback = string.IsNullOrEmpty(defaultValue) ? catalogFallback : defaultValue;
        cache = Load(moduleFileName, resourceId, fallback);
        return cache;
    }

    /// <summary>
    /// Gets an OS-defined string from a system module, with caching and fallback to <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="moduleFileName">Module file name such as <c>user32.dll</c> or <c>shell32.dll</c> (basename only).</param>
    /// <param name="resourceId">String resource ID in the module.</param>
    /// <param name="defaultValue">Fallback when the resource cannot be loaded.</param>
    /// <param name="cache">Reference to the cached value.</param>
    /// <returns>The OS string if available; otherwise <paramref name="defaultValue"/>.</returns>
    public static string Load(string moduleFileName, uint resourceId, string defaultValue, ref string? cache)
    {
        if (cache != null)
        {
            return cache;
        }

        cache = Load(moduleFileName, resourceId, defaultValue);
        return cache;
    }

    /// <summary>
    /// Tries to load a string from a System32 module without using a caller-side cache field.
    /// </summary>
    public static string Load(string moduleFileName, uint resourceId, string defaultValue)
    {
        var cultureName = CultureInfo.CurrentUICulture.Name;
        var cacheKey = cultureName + @"|" + moduleFileName + @"|" + resourceId.ToString(CultureInfo.InvariantCulture);

        lock (Sync)
        {
            if (Cache.TryGetValue(cacheKey, out string? cached) && cached != null)
            {
                return cached;
            }
        }

        string loaded = TryLoadFromSystem32(moduleFileName, resourceId);
        string result = loaded.Length > 0 ? loaded : defaultValue;

        lock (Sync)
        {
            Cache[cacheKey] = result;
        }

        return result;
    }

    /// <summary>
    /// Clears the process-wide MUI string cache (for example after a UI culture change).
    /// </summary>
    public static void ClearCache()
    {
        lock (Sync)
        {
            Cache.Clear();
        }
    }

    /// <summary>
    /// Returns true when <paramref name="moduleFileName"/> is an allowed System32 basename.
    /// </summary>
    public static bool IsAllowedModule(string? moduleFileName)
    {
        if (string.IsNullOrWhiteSpace(moduleFileName))
        {
            return false;
        }

        var candidate = moduleFileName!;

        // Reject paths / traversal — basename only.
        if (candidate.IndexOfAny(new[] { '/', '\\', ':' }) >= 0)
        {
            return false;
        }

        return AllowedModules.Contains(candidate);
    }

    private static string TryLoadFromSystem32(string moduleFileName, uint resourceId)
    {
        if (!IsAllowedModule(moduleFileName))
        {
            return string.Empty;
        }

        try
        {
            using SafeModuleHandle hModule = PI.LoadLibraryEx(
                moduleFileName,
                IntPtr.Zero,
                PI.LoadLibraryExFlags.LoadLibraryAsDatafile |
                PI.LoadLibraryExFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE |
                PI.LoadLibraryExFlags.LoadLibrarySearchSystem32);

            if (!hModule.IsInvalid)
            {
                string loaded = PI.LoadString(hModule, resourceId);
                if (loaded.Length > 0)
                {
                    return loaded;
                }
            }
        }
        catch
        {
            // Fall through to the toolkit default.
        }

        return string.Empty;
    }
}
