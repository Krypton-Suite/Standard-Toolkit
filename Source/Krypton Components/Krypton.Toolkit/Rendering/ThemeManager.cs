#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Allows the developer to easily access the entire array of supported themes for custom controls.
/// </summary>
public class ThemeManager
{
    #region Private static fields

    private const string _msgBoxCaption = "ThemeManager";

    /// <summary>Prefix used when the theme array displays a custom palette with a name, e.g. "Custom - [My Theme Name]".</summary>
    internal const string CustomThemeNamePrefix = @"Custom - ";

    private static readonly object _registeredThemesSync = new object();
    private static readonly Dictionary<string, Func<KryptonCustomPaletteBase>> _registeredCustomThemes =
        new Dictionary<string, Func<KryptonCustomPaletteBase>>(StringComparer.Ordinal);

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the registered custom theme list changes (register / unregister).
    /// Theme selectors should rebuild their item lists when this fires.
    /// </summary>
    public static event EventHandler? RegisteredThemesChanged;

    #endregion

    #region Properties

    /// <summary>Gets the supported theme array (builtin names plus any registered custom themes).</summary>
    /// <value>The supported theme array.</value>
    public static ICollection<string> SupportedInternalThemeNames => GetThemesArray();

    /// <summary>Returns the Default Global Palette.</summary>
    public static PaletteMode DefaultGlobalPalette => ToolkitStaticConstants.GLOBAL_DEFAULT_PALETTE_MODE;

    /// <summary>
    /// Gets the display names of custom themes registered via <see cref="RegisterCustomTheme"/>.
    /// </summary>
    public static IReadOnlyCollection<string> RegisteredCustomThemeNames
    {
        get
        {
            lock (_registeredThemesSync)
            {
                return _registeredCustomThemes.Keys.ToArray();
            }
        }
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Registers a named custom theme so it appears in theme selectors (before the built-in Custom entry).
    /// Selecting it applies a fresh palette from <paramref name="factory"/> via <see cref="ApplyTheme(KryptonCustomPaletteBase, KryptonManager)"/>.
    /// </summary>
    /// <param name="displayName">Unique display name (must not collide with a builtin theme name).</param>
    /// <param name="factory">Factory that creates a named <see cref="KryptonCustomPaletteBase"/>.</param>
    /// <exception cref="ArgumentException">Name is empty or matches a builtin theme.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="factory"/> is null.</exception>
    public static void RegisterCustomTheme(string displayName, Func<KryptonCustomPaletteBase> factory)
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentException(@"A theme display name is required.", nameof(displayName));
        }

        if (factory is null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        if (PaletteModeStrings.SupportedThemesMap.ContainsKey(displayName)
            || displayName.Equals(PaletteModeStrings.DEFAULT_PALETTE_CUSTOM, StringComparison.Ordinal)
            || displayName.StartsWith(CustomThemeNamePrefix, StringComparison.Ordinal))
        {
            throw new ArgumentException(
                @"Display name collides with a builtin theme or the Custom entry.",
                nameof(displayName));
        }

        lock (_registeredThemesSync)
        {
            _registeredCustomThemes[displayName] = factory;
        }

        OnRegisteredThemesChanged();
    }

    /// <summary>
    /// Removes a previously registered custom theme from theme selectors.
    /// </summary>
    /// <param name="displayName">Name passed to <see cref="RegisterCustomTheme"/>.</param>
    /// <returns><c>true</c> if the theme was removed; otherwise <c>false</c>.</returns>
    public static bool UnregisterCustomTheme(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            return false;
        }

        bool removed;
        lock (_registeredThemesSync)
        {
            removed = _registeredCustomThemes.Remove(displayName);
        }

        if (removed)
        {
            OnRegisteredThemesChanged();
        }

        return removed;
    }

    /// <summary>
    /// Returns whether <paramref name="themeName"/> is a registered custom theme.
    /// </summary>
    /// <param name="themeName">Theme display name.</param>
    /// <returns><c>true</c> when registered.</returns>
    public static bool IsRegisteredCustomTheme(string themeName)
    {
        if (string.IsNullOrEmpty(themeName))
        {
            return false;
        }

        lock (_registeredThemesSync)
        {
            return _registeredCustomThemes.ContainsKey(themeName);
        }
    }

    /// <summary>
    /// Builds the theme name list for selectors: builtin names, then registered custom themes, with Custom last.
    /// When the active global palette is an unregistered named custom, the Custom entry is shown as
    /// <c>Custom - [name]</c> (issue #1031).
    /// </summary>
    /// <returns>Theme display names.</returns>
    public static string[] GetThemesArray() => GetThemesArray(includeExtra: true);

    /// <summary>
    /// Builds the theme name list for selectors.
    /// </summary>
    /// <param name="includeExtra">When <see langword="false"/>, extra (non-core) catalogued palettes are omitted.</param>
    /// <returns>Theme display names.</returns>
    public static string[] GetThemesArray(bool includeExtra)
    {
        var builtins = new List<string>();
        foreach (var pair in PaletteModeStrings.SupportedThemes.FirstToSecond)
        {
            if (pair.Value == PaletteMode.Custom)
            {
                if (KryptonThemeAvailability.AllowCustomThemes)
                {
                    builtins.Add(pair.Key);
                }

                continue;
            }

            if (!includeExtra && !KryptonThemeCatalog.IsCoreMode(pair.Value))
            {
                continue;
            }

            if (KryptonThemeAvailability.IsSelectable(pair.Value))
            {
                builtins.Add(pair.Key);
            }
        }

        int customIndex = builtins.IndexOf(PaletteModeStrings.DEFAULT_PALETTE_CUSTOM);
        if (customIndex < 0)
        {
            customIndex = builtins.Count;
        }

        string[] registered;
        if (!KryptonThemeAvailability.AllowCustomThemes)
        {
            registered = Array.Empty<string>();
        }
        else
        {
            lock (_registeredThemesSync)
            {
                registered = _registeredCustomThemes.Keys.OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToArray();
            }
        }

        for (int i = 0; i < registered.Length; i++)
        {
            builtins.Insert(customIndex + i, registered[i]);
        }

        if (KryptonManager.CurrentGlobalPalette is KryptonCustomPaletteBase custom
            && !string.IsNullOrWhiteSpace(custom.GetPaletteName()))
        {
            string name = custom.GetPaletteName()!;
            // Registered themes already have their own row; only rewrite the bare Custom entry for ad-hoc customs.
            if (!IsRegisteredCustomTheme(name))
            {
                for (int i = 0; i < builtins.Count; i++)
                {
                    if (builtins[i] == PaletteModeStrings.DEFAULT_PALETTE_CUSTOM)
                    {
                        builtins[i] = CustomThemeNamePrefix + name;
                        break;
                    }
                }
            }
        }

        return builtins.ToArray();
    }

    /// <summary>
    /// Applies a registered custom theme by display name.
    /// </summary>
    /// <param name="themeName">Registered display name.</param>
    /// <param name="manager">Target manager.</param>
    /// <returns><c>true</c> if applied; <c>false</c> if not registered.</returns>
    public static bool TryApplyRegisteredTheme(string themeName, KryptonManager manager)
    {
        if (manager is null || string.IsNullOrEmpty(themeName))
        {
            return false;
        }

        Func<KryptonCustomPaletteBase>? factory;
        lock (_registeredThemesSync)
        {
            if (!_registeredCustomThemes.TryGetValue(themeName, out factory))
            {
                return false;
            }
        }

        KryptonCustomPaletteBase palette = factory();
        if (string.IsNullOrWhiteSpace(palette.GetPaletteName()))
        {
            palette.SetPaletteName(themeName);
        }

        ApplyTheme(palette, manager);
        return true;
    }

    private static void OnRegisteredThemesChanged() => RegisteredThemesChanged?.Invoke(null, EventArgs.Empty);

    /// <summary>
    /// Raises <see cref="RegisteredThemesChanged"/> so selectors rebuild after catalog or availability changes.
    /// </summary>
    internal static void NotifyThemeListChanged() => OnRegisteredThemesChanged();

    /// <summary>Returns the palette mode from the Krypton Manager instance.</summary>
    /// <param name="manager">The manager instance.</param>
    /// <returns>The current <see cref="PaletteMode"/>.</returns>
    public static PaletteMode GetPaletteMode(KryptonManager manager) => manager.GlobalPaletteMode;

    /// <summary>
    /// Applies the theme using PaletteMode enumeration.
    /// </summary>
    /// <param name="mode">The palette mode.</param>
    /// <param name="manager">The manager.</param>
    public static void ApplyTheme(PaletteMode mode, KryptonManager manager) => ApplyGlobalTheme(manager, mode);

    /// <summary>
    /// Applies the theme using the theme's name (builtin or registered custom).
    /// </summary>
    /// <param name="themeName">Valid name of the theme.</param>
    /// <param name="manager">The manager.</param>
    public static void ApplyTheme(string themeName, KryptonManager manager)
    {
        if (TryApplyRegisteredTheme(themeName, manager))
        {
            return;
        }

        ApplyGlobalTheme(manager, GetThemeManagerMode(themeName));
    }

    /// <summary>
    /// Applies the provided custom palette object.
    /// </summary>
    /// <param name="palette">Reference to a KryptonCustomPaletteBase object</param>
    /// <param name="manager">The manager.</param>
    public static void ApplyTheme(KryptonCustomPaletteBase palette, KryptonManager manager)
    {
        manager.GlobalCustomPalette = palette;
        manager.GlobalPaletteMode = PaletteMode.Custom;
    }

    /// <summary>
    /// Loads a custom theme from the given file.
    /// </summary>
    /// <param name="themeFile">Valid path including filename to the theme file. The file must exist an be compatible, otherwise the import will fail.</param>
    /// <param name="silent">True if the operation should suppress messages from the palette import process, otherwise false.</param>
    /// <param name="manager">The manager.</param>
    // ToDo V120 LTS: Document .kpalx as the expected custom theme file. Import still sniffs XML content.
    public static void ApplyTheme(string themeFile, bool silent, KryptonManager manager)
    {
        if (File.Exists(themeFile))
        {
            try
            {
                KryptonCustomPaletteBase palette = new();
                var imported = palette.Import(themeFile, silent);
                if (string.IsNullOrEmpty(imported))
                {
                    return;
                }

                ApplyTheme(palette, manager);
            }
            catch (Exception exc)
            {
                KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
            }
        }
        else
        {
            KryptonMessageBox.Show(
                $"The parameter 'themeFile' points to a file that does not exist.\n" +
                $"Filename: {themeFile}\n\n" +
                $"ApplyTheme aborted.",
                _msgBoxCaption,
                buttons: KryptonMessageBoxButtons.OK,
                icon: KryptonMessageBoxIcon.Exclamation);
        }
    }

    /// <summary>
    /// Loads one named theme from a <c>.kpal</c> pack (or a matching single-theme file).
    /// </summary>
    /// <param name="themeFile">Valid path including filename to the theme file.</param>
    /// <param name="themeName">Theme name in the pack. Comparison is case-insensitive.</param>
    /// <param name="silent">True if the operation should suppress messages from the palette import process.</param>
    /// <param name="manager">The manager.</param>
    public static void ApplyTheme(string themeFile, string themeName, bool silent, KryptonManager manager)
    {
        if (File.Exists(themeFile))
        {
            try
            {
                KryptonCustomPaletteBase palette = new();
                var imported = palette.Import(themeFile, themeName, silent);
                if (string.IsNullOrEmpty(imported))
                {
                    return;
                }

                ApplyTheme(palette, manager);
            }
            catch (Exception exc)
            {
                KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
            }
        }
        else
        {
            KryptonMessageBox.Show(
                $"The parameter 'themeFile' points to a file that does not exist.\n" +
                $"Filename: {themeFile}\n\n" +
                $"ApplyTheme aborted.",
                _msgBoxCaption,
                buttons: KryptonMessageBoxButtons.OK,
                icon: KryptonMessageBoxIcon.Exclamation);
        }
    }

    /// <summary>
    /// Applies the global theme.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="paletteMode">The palette mode manager.</param>
    public static void ApplyGlobalTheme(KryptonManager manager, PaletteMode paletteMode)
    {
        try
        {
            // Set the global palette mode
            manager.GlobalPaletteMode = paletteMode;
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>
    /// Returns the respective theme name for the given KryptonManager instance.
    /// When the mode is Custom and the custom palette has a bundled name, that name is returned so it displays correctly (e.g. in KryptonManager).
    /// </summary>
    /// <param name="manager">A valid reference to a KryptonManager instance.</param>
    /// <returns>The theme name.</returns>
    public static string ReturnPaletteModeAsString(KryptonManager manager)
    {
        // When in Custom mode, attempt to return the custom palette's name if it exists, otherwise return the palette mode as string.
        if (manager is { GlobalPaletteMode: PaletteMode.Custom, GlobalCustomPalette: { } customPalette })
        {
            // Attempt to get the custom palette's name. If it exists and is not just whitespace, return it. Otherwise, return the palette mode as string.
            var name = customPalette.GetPaletteName();

            // ReSharper disable once SuspiciousTypeConversion.Global - The check is necessary to ensure that the method exists before calling it, as GetPaletteName is not guaranteed to be implemented in all custom palettes.
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Return the custom palette's name if it exists and is not just whitespace.
                return name;
            }
        }

        // Return the palette mode as string if not in Custom mode or if the custom palette does not have a valid name.
        return ReturnPaletteModeAsString(manager.GlobalPaletteMode);
    }

    /// <summary>
    /// Returns the palette mode as string.
    /// </summary>
    /// <param name="paletteMode">The palette mode.</param>
    /// <returns>The theme name</returns>
    public static string ReturnPaletteModeAsString(PaletteMode paletteMode) => new PaletteModeConverter().ConvertToString(paletteMode)!;

    /// <summary>
    /// Returns the themes PaletteMode from the theme's name.
    /// Accepts builtin names, registered custom theme names, static "Custom", or "Custom - [Theme Name]".
    /// </summary>
    /// <param name="themeName">Name of the theme.</param>
    /// <returns>The respective PaletteMode if the theme name is valid. Otherwise PaletteMode.Global.</returns>
    public static PaletteMode GetThemeManagerMode(string themeName)
    {
        if (string.IsNullOrEmpty(themeName))
        {
            return PaletteMode.Global;
        }

        if (IsRegisteredCustomTheme(themeName)
            || themeName == PaletteModeStrings.DEFAULT_PALETTE_CUSTOM
            || themeName.StartsWith(CustomThemeNamePrefix, StringComparison.Ordinal))
        {
            return PaletteMode.Custom;
        }

        return PaletteModeStrings.SupportedThemesMap.TryGetValue(themeName, out PaletteMode paletteMode)
            ? paletteMode
            : PaletteMode.Global;
    }

    #endregion
}
