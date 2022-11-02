﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
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
            try
            {
                foreach (var theme in ThemeManager.SupportedThemeArray)
                {
                    target.Items.Add(theme);
                }
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        public static void PropagateThemeSelector(KryptonRibbonGroupDomainUpDown target)
        {
            try
            {
                foreach (var theme in ThemeManager.SupportedThemeArray)
                {
                    target.Items.Add(theme);
                }
            }
            catch
            {

                throw;
            }
        }

        #region Built-in Redundancy        
        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="paletteMode">The palette mode.</param>
        /// <param name="manager">The manager.</param>
        private static void ApplyTheme(PaletteModeManager paletteMode, KryptonManager manager)
        {
            switch (paletteMode)
            {
                case PaletteModeManager.ProfessionalSystem:
                    manager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                    break;
                case PaletteModeManager.ProfessionalOffice2003:
                    manager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
                    break;
                case PaletteModeManager.Office2007Blue:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
                case PaletteModeManager.Office2007Silver:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
                    break;
                case PaletteModeManager.Office2007White:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007White;
                    break;
                case PaletteModeManager.Office2007Black:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                    break;
                case PaletteModeManager.Office2010Blue:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                    break;
                case PaletteModeManager.Office2010Silver:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
                    break;
                case PaletteModeManager.Office2010White:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010White;
                    break;
                case PaletteModeManager.Office2010Black:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
                    break;
                /*case PaletteModeManager.Office2013:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2013;
                    break;*/
                case PaletteModeManager.Office2013White:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2013White;
                    break;
                case PaletteModeManager.Microsoft365Black:
                    manager.GlobalPaletteMode = PaletteModeManager.Microsoft365Black;
                    break;
                case PaletteModeManager.Microsoft365Blue:
                    manager.GlobalPaletteMode = PaletteModeManager.Microsoft365Blue;
                    break;
                case PaletteModeManager.Microsoft365Silver:
                    manager.GlobalPaletteMode = PaletteModeManager.Microsoft365Silver;
                    break;
                case PaletteModeManager.Microsoft365White:
                    manager.GlobalPaletteMode = PaletteModeManager.Microsoft365White;
                    break;
                case PaletteModeManager.SparkleBlue:
                    manager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                    break;
                case PaletteModeManager.SparkleOrange:
                    manager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
                    break;
                case PaletteModeManager.SparklePurple:
                    manager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
                    break;
                case PaletteModeManager.Custom:
                    manager.GlobalPaletteMode = PaletteModeManager.Custom;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        /// <exception cref="ArgumentNullException"></exception>
        private static void ApplyTheme(string themeName, KryptonManager manager)
        {
            switch (themeName)
            {
                case "Custom":
                    ApplyTheme(PaletteModeManager.Custom, manager);
                    break;
                case "Professional - System":
                    ApplyTheme(PaletteModeManager.ProfessionalSystem, manager);
                    break;
                case "Professional - Office 2003":
                    ApplyTheme(PaletteModeManager.ProfessionalOffice2003, manager);
                    break;
                case "Office 2007 - Blue":
                    ApplyTheme(PaletteModeManager.Office2007Blue, manager);
                    break;
                case "Office 2007 - Silver":
                    ApplyTheme(PaletteModeManager.Office2007Silver, manager);
                    break;
                case "Office 2007 - White":
                    ApplyTheme(PaletteModeManager.Office2007White, manager);
                    break;
                case "Office 2007 - Black":
                    ApplyTheme(PaletteModeManager.Office2007Black, manager);
                    break;
                case "Office 2010 - Blue":
                    ApplyTheme(PaletteModeManager.Office2010Blue, manager);
                    break;
                case "Office 2010 - Silver":
                    ApplyTheme(PaletteModeManager.Office2010Silver, manager);
                    break;
                case "Office 2010 - White":
                    ApplyTheme(PaletteModeManager.Office2010White, manager);
                    break;
                case "Office 2010 - Black":
                    ApplyTheme(PaletteModeManager.Office2010Black, manager);
                    break;
                /*if (themeName == "Office 2013")
            {
                ApplyTheme(PaletteModeManager.Office2013, manager);
            }*/
                case "Office 2013 - White":
                    ApplyTheme(PaletteModeManager.Office2013White, manager);
                    break;
                case "Sparkle - Blue":
                    ApplyTheme(PaletteModeManager.SparkleBlue, manager);
                    break;
                case "Sparkle - Orange":
                    ApplyTheme(PaletteModeManager.SparkleOrange, manager);
                    break;
                case "Sparkle - Purple":
                    ApplyTheme(PaletteModeManager.SparklePurple, manager);
                    break;
                case "Microsoft 365 - Black":
                    ApplyTheme(PaletteModeManager.Microsoft365Black, manager);
                    break;
                case "Microsoft 365 - Blue":
                    ApplyTheme(PaletteModeManager.Microsoft365Blue, manager);
                    break;
                case "Microsoft 365 - Silver":
                    ApplyTheme(PaletteModeManager.Microsoft365Silver, manager);
                    break;
                case "Microsoft 365 - White":
                    ApplyTheme(PaletteModeManager.Microsoft365White, manager);
                    break;
                default:
                    throw new ArgumentNullException(nameof(themeName));
            }
        }

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
            try
            {
                ApplyTheme(themeName, manager);

                ApplyGlobalTheme(manager, GetCurrentPaletteMode(manager));
            }
            catch
            {
                // Swallow ?
            }
        }

        /// <summary>
        /// Returns the palette mode manager as string.
        /// </summary>
        /// <param name="paletteModeManager">The palette mode manager.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteModeManager"/> as a string.</returns>
        public static string ReturnPaletteModeManagerAsString(PaletteModeManager paletteModeManager, KryptonManager manager = null)
        {
            string result = null;

            if (manager != null)
            {
                if (manager.GlobalPaletteMode == PaletteModeManager.Custom) result = "Custom";

                if (manager.GlobalPaletteMode == PaletteModeManager.ProfessionalSystem) result = "Professional - System";

                if (manager.GlobalPaletteMode == PaletteModeManager.ProfessionalOffice2003) result = "Professional - Office 2003";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2007Blue) result = "Office 2007 - Blue";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2007Silver) result = "Office 2007 - Silver";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2007White) result = "Office 2007 - White";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2007Black) result = "Office 2007 - Black";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2010Blue) result = "Office 2010 - Blue";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2010Silver) result = "Office 2010 - Silver";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2010White) result = "Office 2010 - White";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2010Black) result = "Office 2010 - Black";

                //if (manager.GlobalPaletteMode == PaletteModeManager.Office2013) result = "Office 2013";

                if (manager.GlobalPaletteMode == PaletteModeManager.Office2013White) result = "Office 2013 - White";

                if (manager.GlobalPaletteMode == PaletteModeManager.SparkleBlue) result = "Sparkle - Blue";

                if (manager.GlobalPaletteMode == PaletteModeManager.SparkleOrange) result = "Sparkle - Orange";

                if (manager.GlobalPaletteMode == PaletteModeManager.SparklePurple) result = "Sparkle - Purple";

                if (manager.GlobalPaletteMode == PaletteModeManager.Microsoft365Blue) result = "Microsoft 365 - Blue";

                if (manager.GlobalPaletteMode == PaletteModeManager.Microsoft365Silver) result = "Microsoft 365 - Silver";

                if (manager.GlobalPaletteMode == PaletteModeManager.Microsoft365White) result = "Microsoft 365 - White";

                if (manager.GlobalPaletteMode == PaletteModeManager.Microsoft365Black) result = "Microsoft 365 - Black";
            }
            else
            {
                if (paletteModeManager == PaletteModeManager.Custom) result = "Custom";

                if (paletteModeManager == PaletteModeManager.ProfessionalSystem) result = "Professional - System";

                if (paletteModeManager == PaletteModeManager.ProfessionalOffice2003) result = "Professional - Office 2003";

                if (paletteModeManager == PaletteModeManager.Office2007Blue) result = "Office 2007 - Blue";

                if (paletteModeManager == PaletteModeManager.Office2007Silver) result = "Office 2007 - Silver";

                if (paletteModeManager == PaletteModeManager.Office2007White) result = "Office 2007 - White";

                if (paletteModeManager == PaletteModeManager.Office2007Black) result = "Office 2007 - Black";

                if (paletteModeManager == PaletteModeManager.Office2010Blue) result = "Office 2010 - Blue";

                if (paletteModeManager == PaletteModeManager.Office2010Silver) result = "Office 2010 - Silver";

                if (paletteModeManager == PaletteModeManager.Office2010White) result = "Office 2010 - White";

                if (paletteModeManager == PaletteModeManager.Office2010Black) result = "Office 2010 - Black";

                //if (paletteModeManager == PaletteModeManager.Office2013) result = "Office 2013";

                if (paletteModeManager == PaletteModeManager.Office2013White) result = "Office 2013 - White";

                if (paletteModeManager == PaletteModeManager.SparkleBlue) result = "Sparkle - Blue";

                if (paletteModeManager == PaletteModeManager.SparkleOrange) result = "Sparkle - Orange";

                if (paletteModeManager == PaletteModeManager.SparklePurple) result = "Sparkle - Purple";

                if (paletteModeManager == PaletteModeManager.Microsoft365Blue) result = "Microsoft 365 - Blue";

                if (paletteModeManager == PaletteModeManager.Microsoft365Silver) result = "Microsoft 365 - Silver";

                if (paletteModeManager == PaletteModeManager.Microsoft365White) result = "Microsoft 365 - White";

                if (paletteModeManager == PaletteModeManager.Microsoft365Black) result = "Microsoft 365 - Black";
            }

            return result;
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