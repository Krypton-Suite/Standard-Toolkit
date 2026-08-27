#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit;

#region Static
/// <summary>
/// Class CommonHelperThemeSelectors hold the common code for all Theme Selector controls:<br/>
/// - KryptonThemeComboBox<br/>
/// - KryptonThemeListBox<br/>
/// - KryptonRibbonGroupThemeComboBox<br/>
/// - KryptonThemeBrowser
/// </summary>
internal static class CommonHelperThemeSelectors
{
    /// <summary>
    /// Returns a list with theme names (builtin + registered custom themes).
    /// When the current global palette is an unregistered named custom, the "Custom" entry is shown as
    /// "Custom - [Theme Name]" (see issue #1031).
    /// </summary>
    /// <returns>String array of theme names.</returns>
    internal static string[] GetThemesArray() => ThemeManager.GetThemesArray();

    /// <summary>
    /// Returns theme names, optionally limited to core palettes.
    /// </summary>
    /// <param name="includeExtra">When <see langword="false"/>, extra catalogued palettes are omitted.</param>
    /// <returns>String array of theme names.</returns>
    internal static string[] GetThemesArray(bool includeExtra) => ThemeManager.GetThemesArray(includeExtra);

    /// <summary>
    /// Rebuilds a selector list and restores selection by theme name, then by <paramref name="fallbackMode"/>.
    /// </summary>
    /// <param name="items">Selector items collection.</param>
    /// <param name="includeExtra">Whether extra palettes are listed.</param>
    /// <param name="previousName">Previously selected display name.</param>
    /// <param name="fallbackMode">Mode used when the previous name is no longer listed.</param>
    /// <returns>Index to select, or <c>-1</c>.</returns>
    internal static int ReloadThemeItems(IList items, bool includeExtra, string? previousName, PaletteMode fallbackMode)
    {
        items.Clear();
        foreach (var name in GetThemesArray(includeExtra))
        {
            items.Add(name);
        }

        if (!string.IsNullOrEmpty(previousName))
        {
            int byName = items.IndexOf(previousName);
            if (byName >= 0)
            {
                return byName;
            }
        }

        return GetPaletteIndex(items, fallbackMode);
    }

    /// <summary>
    /// Rebuilds selector items after <see cref="KryptonManager.GlobalPaletteChanged"/>.
    /// When the active mode is <see cref="PaletteMode.Custom"/>, the previous builtin
    /// display name is not preserved so the list selects Custom instead of re-applying
    /// the last builtin theme (which would wipe custom TMS colours such as ImageMargin).
    /// </summary>
    internal static int ReloadThemeItemsForGlobalChange(IList items, bool includeExtra, string? selectedName, PaletteMode mode)
    {
        string? previous = mode == PaletteMode.Custom ? null : selectedName;
        return ReloadThemeItems(items, includeExtra, previous, mode);
    }

    /// <summary>
    /// Fills a theme-browser list using <see cref="KryptonThemeBrowserData.ShowExtraThemes"/>.
    /// </summary>
    internal static void FillThemeBrowserItems(IList items, KryptonThemeBrowserData themeBrowserData)
    {
        items.Clear();
        var includeExtra = themeBrowserData.ShowExtraThemes ?? true;
        foreach (var name in GetThemesArray(includeExtra))
        {
            items.Add(name);
        }
    }

    /// <summary>
    /// Performs a theme change when the control's SelectedIndex is changed.
    /// </summary>
    /// <param name="isLocalUpdate">Enter: ref this._isLocalUpdate.</param>
    /// <param name="isExternalUpdate">Enter: this._isExternalUpdate.</param>
    /// <param name="defaultPalette">Enter: ref this._defaultPalette.</param>
    /// <param name="themeName">Name of the theme (SelectedItem text).</param>
    /// <param name="manager">Enter: this._manager.</param>
    /// <param name="kryptonCustomPalette">Enter: this._kryptonCustomPalette</param>
    /// <returns>True if the theme change was successful, false when custom was selected but no local external custom palette is set.</returns>
    internal static bool OnSelectedIndexChanged(ref bool isLocalUpdate, bool isExternalUpdate, ref PaletteMode defaultPalette,
        string themeName, KryptonManager manager, KryptonCustomPaletteBase? kryptonCustomPalette)
    {
        bool result = true;

        if (!isExternalUpdate)
        {
            isLocalUpdate = true;

            if (ThemeManager.TryApplyRegisteredTheme(themeName, manager))
            {
                defaultPalette = PaletteMode.Custom;
            }
            else
            {
                // Get palette from theme name. If themeName is not valid default to Global
                PaletteMode mode = string.IsNullOrEmpty(themeName)
                    ? PaletteMode.Global
                    : ThemeManager.GetThemeManagerMode(themeName);

                if (mode == PaletteMode.Custom)
                {
                    if (kryptonCustomPalette is not null)
                    {
                        manager.GlobalCustomPalette = kryptonCustomPalette;
                        defaultPalette = mode;
                    }
                    else
                    {
                        // Custom has been selected but there's no custom theme assigned
                        // to the ThemeSelector or in the KManager.
                        // Leave defaultPalette as it is.
                        result = false;
                    }
                }
                else if (mode == PaletteMode.Global)
                {
                    // If mode is set to Global, a theme change is not necessary.
                    result = false;
                }
                else
                {
                    ThemeManager.ApplyTheme(themeName, manager);
                    defaultPalette = mode;
                }
            }

            isLocalUpdate = false;
        }

        return result;
    }

    /// <summary>
    /// Return the index in the list of the requested PaletteMode parameter.
    /// For Custom mode, prefers a registered theme name matching the active custom palette, then
    /// "Custom" / "Custom - [Theme Name]".
    /// </summary>
    /// <param name="items">The control's list of themes (usually Items).</param>
    /// <param name="mode">The PaletteMode for which to locate the index in items.</param>
    /// <returns>
    /// The index of the requested palette.<br/>
    /// If the PaletteMode was not found in the list, -1 will be returned.<br/>
    /// </returns>
    internal static int GetPaletteIndex(IList items, PaletteMode mode)
    {
        //intitial value must be an invalid SelectedIndex.
        int newIdx = -1;

        // When a control has the DefaultPalette property set to Global newIdx is -1
        // A lookup is not possible since Global does not exist in the themes dictionary.
        if (mode != PaletteMode.Global)
        {
            if (mode == PaletteMode.Custom)
            {
                string? paletteName = null;
                if (KryptonManager.CurrentGlobalPalette is KryptonCustomPaletteBase custom)
                {
                    paletteName = custom.GetPaletteName();
                }

                if (!string.IsNullOrWhiteSpace(paletteName))
                {
                    newIdx = items.IndexOf(paletteName);
                }

                if (newIdx < 0)
                {
                    // Theme array may show "Custom" or "Custom - [Theme Name]"
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i] is string s
                            && (s == PaletteModeStrings.DEFAULT_PALETTE_CUSTOM
                                || s.StartsWith(ThemeManager.CustomThemeNamePrefix, StringComparison.Ordinal)))
                        {
                            newIdx = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                var selectedText = PaletteModeStrings.SupportedThemes.SecondToFirst[mode];
                newIdx = items.IndexOf(selectedText);
            }
        }

        return (newIdx >= 0 && newIdx < items.Count)
            ? newIdx
            : -1;
    }

    /// <summary>
    /// Is executed when a KryptonManager.GlobalPaletteChanged event is fired.<br/>
    /// It will synchronize the list control's selected theme with that from Krypton Manager.
    /// </summary>
    /// <param name="isLocalUpdate">Enter: this._isLocalUpdate.</param>
    /// <param name="isExternalUpdate">Enter: ref this._isExternalUpdate.</param>
    /// <param name="selectedIndex">The currently selected index of the control.</param>
    /// <param name="items">The control's list of themes (usually Items).</param>
    /// <returns>The selected index.</returns>
    internal static int KryptonManagerGlobalPaletteChanged(bool isLocalUpdate, ref bool isExternalUpdate, int selectedIndex, IList items)
    {
        int result = selectedIndex;

        // Only run on external change
        if (!isLocalUpdate)
        {
            // Avoid triggering a circular palette change
            isExternalUpdate = true;

            // When Global is selected as CurrentGlobalPalette, the theme stays as it is currently.
            // So, there's no need to change the index.
            if (KryptonManager.CurrentGlobalPaletteMode != PaletteMode.Global)
            {
                result = CommonHelperThemeSelectors.GetPaletteIndex(items, KryptonManager.CurrentGlobalPaletteMode);
            }

            // Back to norml
            isExternalUpdate = false;
        }

        return result;
    }

    /// <summary>
    /// Returns the intially selected index.<br/>
    /// Should only be used in the constructor to set a palette from the manager or the control's DefaultPalette value set at design time.
    /// </summary>
    /// <param name="defaultPalette">Enter: this._defaultPalette.</param>
    /// <param name="manager">Enter: this._manager.</param>
    /// <param name="items">The control's list of themes (usually Items).</param>
    /// <returns>Returns the location in the list of items for defaultPalette.</returns>
    internal static int GetInitialSelectedIndex(PaletteMode defaultPalette, KryptonManager manager, IList items)
    {
        PaletteMode pm = defaultPalette == PaletteMode.Global
                         && manager.GlobalPaletteMode != PaletteMode.Custom
                         && manager.GlobalPaletteMode != PaletteMode.Global
            ? manager.GlobalPaletteMode
            : defaultPalette;

        return CommonHelperThemeSelectors.GetPaletteIndex(items, pm);
    }

    /// <summary>
    /// The Set handler for the DefaultPalette property.
    /// </summary>
    /// <param name="defaultPalette">enter: ref this._defaultPalette.</param>
    /// <param name="value">Incoming value from the property set.</param>
    /// <param name="items">The control's list of themes (usually Items).</param>
    /// <param name="selectedIndex">The currently selected index of the control.</param>
    /// <returns>Returns the location in the list of items for defaultPalette.</returns>
    internal static int DefaultPaletteSetter(ref PaletteMode defaultPalette, PaletteMode value, IList items, int selectedIndex)
    {
        // If value == defaultPalette or value == PaletteMode.Global
        // the index remains the same and will not trigger an IndexChanged event.
        int result = selectedIndex;

        // Value needs to be different
        if (defaultPalette != value)
        {
            defaultPalette = value;

            // Any PaletteMode can be set as a theme, EXCEPT Global.
            if (value != PaletteMode.Global)
            {
                // Setting the index triggers OnSelectedIndexChanged()
                result = GetPaletteIndex(items, defaultPalette);
            }
        }

        return result;
    }

    /// <summary>
    /// Resolves the initial theme list index for <see cref="KryptonThemeBrowser"/>.
    /// Prefers <see cref="KryptonThemeBrowserData.DefaultPalette"/> over
    /// <see cref="KryptonThemeBrowserData.StartIndex"/>, then the global default theme.
    /// </summary>
    /// <param name="themeBrowserData">Caller-supplied theme browser options.</param>
    /// <param name="items">Populated theme list items.</param>
    /// <param name="manager">Manager used when <see cref="PaletteMode.Global"/> is requested.</param>
    /// <returns>A valid index into <paramref name="items"/>, or <c>-1</c> when the list is empty.</returns>
    internal static int GetThemeBrowserStartIndex(KryptonThemeBrowserData themeBrowserData, IList items, KryptonManager manager)
    {
        if (items.Count == 0)
        {
            return -1;
        }

        if (themeBrowserData.DefaultPalette.HasValue)
        {
            int paletteIndex = GetInitialSelectedIndex(themeBrowserData.DefaultPalette.Value, manager, items);
            if (paletteIndex >= 0)
            {
                return paletteIndex;
            }
        }

        if (themeBrowserData.StartIndex.HasValue
            && themeBrowserData.StartIndex.Value >= 0
            && themeBrowserData.StartIndex.Value < items.Count)
        {
            return themeBrowserData.StartIndex.Value;
        }

        int fallbackIndex = GetPaletteIndex(items, ToolkitStaticConstants.GLOBAL_DEFAULT_PALETTE_MODE);
        return fallbackIndex >= 0 ? fallbackIndex : 0;
    }
}

#endregion

#region IKryptonThemeSelectorBase

/// <summary>
/// Interface IKryptonThemeSelectorBase<br/>
/// Common entities for the Theme Selector controls.
/// </summary>
internal interface IKryptonThemeSelectorBase
{
    /// <summary>
    /// Gets or sets the default palette mode.
    /// </summary>
    PaletteMode DefaultPalette { get; set; }

    /// <summary>
    /// Gets or sets whether extra (non-core) catalogued palettes appear in the list.
    /// </summary>
    bool ShowExtraThemes { get; set; }
}

#endregion