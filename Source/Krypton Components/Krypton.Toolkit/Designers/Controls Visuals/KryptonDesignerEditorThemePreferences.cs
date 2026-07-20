#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Persists the user's preferred palette for designer-editor dialogs.
/// </summary>
/// <remarks>
/// Stored under <c>%LocalAppData%\Krypton-Suite\Toolkit\DesignerEditorTheme.prefs</c>.
/// Preference applies only to editor chrome; it does not change the edited component or
/// <see cref="KryptonManager.GlobalPaletteMode"/>. <see cref="PaletteMode.Custom"/> is not
/// persisted because a custom palette instance cannot be restored across sessions.
/// </remarks>
internal static class KryptonDesignerEditorThemePreferences
{
    private const string SettingsFileName = @"DesignerEditorTheme.prefs";
    private static readonly object SyncRoot = new object();
    private static bool _loaded;
    private static bool _hasPreference;
    private static PaletteMode _preferredMode;

    /// <summary>
    /// Tries to read the saved designer-editor theme preference.
    /// </summary>
    /// <param name="paletteMode">Saved palette mode when present and valid.</param>
    /// <returns><c>true</c> when a usable preference exists.</returns>
    public static bool TryGetPreferredPaletteMode(out PaletteMode paletteMode)
    {
        EnsureLoaded();

        lock (SyncRoot)
        {
            if (_hasPreference && IsPersistable(_preferredMode))
            {
                paletteMode = _preferredMode;
                return true;
            }
        }

        paletteMode = PaletteMode.Global;
        return false;
    }

    /// <summary>
    /// Saves the preferred designer-editor theme.
    /// </summary>
    /// <param name="paletteMode">Mode selected in the editor footer theme combo.</param>
    public static void SavePreferredPaletteMode(PaletteMode paletteMode)
    {
        if (!IsPersistable(paletteMode))
        {
            return;
        }

        lock (SyncRoot)
        {
            _preferredMode = paletteMode;
            _hasPreference = true;
            _loaded = true;

            try
            {
                var path = GetSettingsPath();
                var directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(path, paletteMode.ToString());
            }
            catch
            {
                // Preference persistence must never break designer editors.
            }
        }
    }

    /// <summary>
    /// Clears any saved designer-editor theme preference.
    /// </summary>
    public static void ClearPreferredPaletteMode()
    {
        lock (SyncRoot)
        {
            _hasPreference = false;
            _loaded = true;

            try
            {
                var path = GetSettingsPath();
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
                // Preference persistence must never break designer editors.
            }
        }
    }

    private static bool IsPersistable(PaletteMode paletteMode) =>
        paletteMode != PaletteMode.Custom;

    private static void EnsureLoaded()
    {
        lock (SyncRoot)
        {
            if (_loaded)
            {
                return;
            }

            _loaded = true;
            _hasPreference = false;

            try
            {
                var path = GetSettingsPath();
                if (!File.Exists(path))
                {
                    return;
                }

                var text = File.ReadAllText(path).Trim();
                if (text.Length == 0)
                {
                    return;
                }

                if (Enum.TryParse(text, true, out PaletteMode mode) && IsPersistable(mode))
                {
                    _preferredMode = mode;
                    _hasPreference = true;
                }
            }
            catch
            {
                _hasPreference = false;
            }
        }
    }

    private static string GetSettingsPath()
    {
        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(localAppData, @"Krypton-Suite", @"Toolkit", SettingsFileName);
    }
}
