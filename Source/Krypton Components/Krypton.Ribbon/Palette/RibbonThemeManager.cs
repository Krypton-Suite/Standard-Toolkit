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
        /// <returns>The current <see cref="PaletteModeManager"/>.</returns>
        public static PaletteModeManager GetCurrentPaletteMode(KryptonManager manager) => manager.GlobalPaletteMode;

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
        /// <param name="paletteModeManager">The palette mode manager.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteModeManager"/> as a string.</returns>
        public static string ReturnPaletteModeManagerAsString(PaletteModeManager paletteModeManager, KryptonManager manager = null)
        {
            return ThemeManager.ReturnPaletteModeManagerAsString(paletteModeManager, manager);
        }

        /// <summary>
        /// Returns the palette mode as string.
        /// </summary>
        /// <param name="paletteMode">The palette mode.</param>
        /// <returns>The current <see cref="PaletteMode"/> as a string.</returns>
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode)
        {
            string result = null;

            if (paletteMode == PaletteMode.Custom) result = "Custom";

            if (paletteMode == PaletteMode.Global) result = "Global";

            if (paletteMode == PaletteMode.ProfessionalSystem) result = "Professional - System";

            if (paletteMode == PaletteMode.ProfessionalOffice2003) result = "Professional - Office 2003";

            if (paletteMode == PaletteMode.Office2007Blue) result = "Office 2007 - Blue";

            if (paletteMode == PaletteMode.Office2007Silver) result = "Office 2007 - Silver";

            if (paletteMode == PaletteMode.Office2007White) result = "Office 2007 - White";

            if (paletteMode == PaletteMode.Office2007Black) result = "Office 2007 - Black";

            if (paletteMode == PaletteMode.Office2010Blue) result = "Office 2010 - Blue";

            if (paletteMode == PaletteMode.Office2010Silver) result = "Office 2010 - Silver";

            if (paletteMode == PaletteMode.Office2010White) result = "Office 2010 - White";

            if (paletteMode == PaletteMode.Office2010Black) result = "Office 2010 - Black";

            //if (paletteMode == PaletteMode.Office2013) result = "Office 2013";

            if (paletteMode == PaletteMode.Office2013White) result = "Office 2013 - White";

            if (paletteMode == PaletteMode.SparkleBlue) result = "Sparkle - Blue";

            if (paletteMode == PaletteMode.SparkleOrange) result = "Sparkle - Orange";

            if (paletteMode == PaletteMode.SparklePurple) result = "Sparkle - Purple";

            if (paletteMode == PaletteMode.Microsoft365Blue) result = "Microsoft 365 - Blue";

            if (paletteMode == PaletteMode.Microsoft365Silver) result = "Microsoft 365 - Silver";

            if (paletteMode == PaletteMode.Microsoft365White) result = "Microsoft 365 - White";

            if (paletteMode == PaletteMode.Microsoft365Black) result = "Microsoft 365 - Black";

            return result;
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="paletteModeManager">The palette mode manager.</param>
        private static void ApplyGlobalTheme(KryptonManager manager, PaletteModeManager paletteModeManager)
        {
            try
            {
                manager.GlobalPaletteMode = paletteModeManager;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="mode">The theme mode.</param>
        private static void ApplyGlobalTheme(KryptonManager manager, PaletteMode mode)
        {
            try
            {
                manager.GlobalPaletteMode = (PaletteModeManager)mode;
            }
            catch
            {

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
        /// <returns>The <see cref="PaletteModeManager"/> equivalent.</returns>
        public static PaletteModeManager ApplyThemeManagerMode(string themeName)
        {
            PaletteModeManager modeManager = (PaletteModeManager)Enum.Parse(typeof(PaletteModeManager), themeName);

            return modeManager;
        }
        #endregion

        #endregion
    }
}