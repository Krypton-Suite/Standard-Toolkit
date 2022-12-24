#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Ribbon
{
    /// <summary>
    /// Allows the developer to easily access the entire array of supported themes for ribbon controls.
    /// </summary>
    public class RibbonThemeManager
    {
        #region Methods        
        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        public static void PropagateThemeSelector(KryptonRibbonGroupComboBox target)
        {
            foreach (var theme in ThemeManager.SupportedInternalThemeNames)
            {
                target.Items.Add(theme);
            }
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        public static void PropagateThemeSelector(KryptonRibbonGroupDomainUpDown target)
        {
            foreach (var theme in ThemeManager.SupportedInternalThemeNames)
            {
                target.Items.Add(theme);
            }
        }

        #region Built-in Redundancy        
        /// <summary>
        /// Gets the current palette mode.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteMode"/>.</returns>
        public static PaletteMode GetCurrentPaletteMode(KryptonManager manager) => manager.GlobalPaletteMode;

        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        public static void SetTheme(string themeName, KryptonManager manager)
        {
            ThemeManager.SetTheme(themeName, manager);
        }

        /// <summary>
        /// Returns the palette mode manager as string.
        /// </summary>
        /// <param name="paletteMode">The palette mode manager.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteMode"/> as a string.</returns>
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode, KryptonManager manager = null)
        {
            return ThemeManager.ReturnPaletteModeAsString(paletteMode, manager);
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="paletteMode">The palette mode manager.</param>
        private static void ApplyGlobalTheme(KryptonManager manager, PaletteMode paletteMode)
        {
            try
            {
                manager.GlobalPaletteMode = paletteMode;
            }
            catch
            {
                // TODO: Used DebugUtilities
                throw;
            }
        }

        /// <summary>
        /// Applies the theme mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteMode"/> equivalent.</returns>
        public static PaletteMode ApplyThemeMode(string themeName)
        {
            PaletteMode mode = (PaletteMode)Enum.Parse(typeof(PaletteMode), themeName);

            return mode;
        }

        /// <summary>
        /// Applies the theme manager mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteMode"/> equivalent.</returns>
        public static PaletteMode ApplyThemeManagerMode(string themeName)
        {
            PaletteMode modeManager = (PaletteMode)Enum.Parse(typeof(PaletteMode), themeName);

            return modeManager;
        }
        #endregion

        #endregion
    }
}