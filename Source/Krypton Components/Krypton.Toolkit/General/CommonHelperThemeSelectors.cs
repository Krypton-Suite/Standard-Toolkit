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
        /// <param name="isLocalUpdate">reference to: this._isLocalUpdate.</param>
        /// <param name="isExternalUpdate">Enter: this._isExternalUpdate.</param>
        /// <param name="themeName">Name of the theme (SelectedItem text).</param>
        /// <param name="manager">Enter: this._manager.</param>
        /// <param name="kryptonCustomPalette">Enter: this._kryptonCustomPalette</param>
        /// <returns>True if the theme change was successful, false when custom was selected but no local external custom is set.</returns>
        internal static bool OnSelectedIndexChanged(ref bool isLocalUpdate, bool isExternalUpdate, string themeName, KryptonManager manager, KryptonCustomPaletteBase? kryptonCustomPalette)
        {
            bool result = true;

            if (!isExternalUpdate)
            {
                isLocalUpdate = true;

                if (ThemeManager.GetThemeManagerMode(themeName) == PaletteMode.Custom)
                {
                    if (kryptonCustomPalette is not null)
                    {
                        manager.GlobalCustomPalette = kryptonCustomPalette;
                    }
                    else
                    {
                        // Custom has been selected but there's no custom theme assigned
                        // to the ThemeSelector or in the KManager.
                        result = false;
                    }
                }
                else
                {
                    ThemeManager.ApplyTheme(themeName, manager);
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
        public static int KryptonManagerGlobalPaletteChanged(bool isLocalUpdate, ref bool isExternalUpdate, int selectedIndex, IList items)
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
    }
}