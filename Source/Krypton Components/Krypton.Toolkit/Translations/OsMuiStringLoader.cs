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
/// </summary>
internal static class OsMuiStringLoader
{
    /// <summary>
    /// Gets an OS-defined string from a system module, with caching and fallback to <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="moduleFileName">Module file name such as <c>user32.dll</c> or <c>shell32.dll</c>.</param>
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

        try
        {
            using SafeModuleHandle? hModule = PI.LoadLibraryEx(
                moduleFileName,
                IntPtr.Zero,
                PI.LoadLibraryExFlags.LoadLibraryAsDatafile | PI.LoadLibraryExFlags.LoadLibrarySearchSystem32);

            if (!hModule.IsInvalid)
            {
                string loaded = PI.LoadString(hModule, resourceId);
                if (loaded.Length > 0)
                {
                    cache = loaded;
                    return cache;
                }
            }
        }
        catch
        {
            // Fall through to the toolkit default.
        }

        cache = defaultValue;
        return defaultValue;
    }
}
