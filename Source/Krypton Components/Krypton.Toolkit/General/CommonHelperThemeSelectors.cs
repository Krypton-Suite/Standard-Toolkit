#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion
namespace Krypton.Toolkit
{
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
        /// Returns a list with theme names.
        /// </summary>
        /// <returns>String array of theme names.</returns>
        internal static string[] GetThemesArray()
        {
            return PaletteModeStrings.SupportedThemesMap
                .Select(x => x.Key)
                .ToArray();
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

                // Get palette from theme name.
                PaletteMode mode = ThemeManager.GetThemeManagerMode(themeName) ?? PaletteMode.Global;

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
                else
                {
                    ThemeManager.ApplyTheme(themeName, manager);
                    defaultPalette = mode;
                }

                isLocalUpdate = false;
            }

            return result;
        }

        /// <summary>
        /// Return the index in the list of the requested PaletteMode parameter.
        /// </summary>
        /// <param name="items">The control's list of themes (usually Items).</param>
        /// <param name="mode">The PaletteMode for which to locate the index in items.</param>
        /// <returns>
        /// The index of the requested palette.<br/>
        /// If the PaletteMode was not found in the list, -1 will be returned.<br/>
        /// </returns>
        internal static int GetPaletteIndex(IList items, PaletteMode mode)
        {
            var selectedText = PaletteModeStrings.SupportedThemes.SecondToFirst[mode];
            var newIdx = items.IndexOf(selectedText);

            return (newIdx >= 0 && newIdx < PaletteModeStrings.SupportedThemesMap.Count)
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
        /// Returns if a global theme is set at KryptonManager.GlobalPaletteMode.<br/>
        /// </summary>
        /// <param name="defaultPalette">Enter: this._defaultPalette.</param>
        /// <param name="manager">Enter: this._manager.</param>
        /// <returns>
        /// True when: DefaultPalette == Global and  manager.GlobalPaletteMode != Custom and Global<br/>
        /// Otherwise false.
        /// </returns>
        internal static bool InitFromManagerPalette(PaletteMode defaultPalette, KryptonManager manager)
        {
            return defaultPalette == PaletteMode.Global
                && manager.GlobalPaletteMode != PaletteMode.Custom
                && manager.GlobalPaletteMode != PaletteMode.Global;
        }

        /// <summary>
        /// The Set handler for the DefaultPalette property.
        /// </summary>
        /// <param name="defaultPalette">enter: ref this._defaultPalette.</param>
        /// <param name="value">Incoming value from the property set.</param>
        /// <param name="items">The control's list of themes (usually Items).</param>
        /// <param name="selectedIndex">The currently selected index of the control.</param>
        /// <returns></returns>
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
                    result = CommonHelperThemeSelectors.GetPaletteIndex(items, defaultPalette);
                }
            }

            return result;
        }
    }
    #endregion

    #region IKryptonThemeSelectorBase

    /// <summary>
    /// Interface IKryptonThemeSelectorBase<br/>
    /// Common public entities for the Theme Selector controls.
    /// </summary>
    internal interface IKryptonThemeSelectorBase
    {
        /// <summary>
        /// Gets or sets the default palette mode.</summary>
        /// <value>The default palette mode.</value>
        PaletteMode DefaultPalette { get; set; }

        /// <summary>
        /// Gets or sets the user defined custom palette.</summary>
        /// <value>The user defined palette mode.</value>
        KryptonCustomPaletteBase? KryptonCustomPalette { get; set; }

        bool ReportSelectedThemeIndex { get; set; }
    }

    #endregion
}