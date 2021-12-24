#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Allows the developer to easily access the entire array of supported themes for custom controls.
    /// </summary>
    public class ThemeManager
    {
        #region Theme Array        
        /// <summary>
        /// The supported themes
        /// </summary>
        private static readonly string[] _supportedThemes =
        {
            "Professional - System",

            "Professional - Office 2003",

            "Office 2007 - Black",

            "Office 2007 - Black (Dark Mode)",

            //"Office 2007 - Black (Light Mode)",

            "Office 2007 - Blue",

            "Office 2007 - Blue (Dark Mode)",

            "Office 2007 - Blue (Light Mode)",

            "Office 2007 - Silver",

            "Office 2007 - Silver (Dark Mode)",

            "Office 2007 - Silver (Light Mode)",

            "Office 2010 - Black",

            "Office 2010 - Black (Dark Mode)",

            //"Office 2010 - Black (Light Mode)",

            "Office 2010 - Blue",

            "Office 2010 - Blue (Dark Mode)",

            "Office 2010 - Blue (Light Mode)",

            "Office 2010 - Silver",

            "Office 2010 - Silver (Dark Mode)",

            "Office 2010 - Silver (Light Mode)",

            "Office 2010 - White",

            "Office 2013 - White",

            "Office 365 - Black",

            "Office 365 - Black (Dark Mode)",

            //"Office 365 - Black (Light Mode)",

            "Office 365 - Blue",

            "Office 365 - Blue (Dark Mode)",

            "Office 365 - Blue (Light Mode)",

            "Office 365 - Silver",

            "Office 365 - Silver (Dark Mode)",

            "Office 365 - Silver (Light Mode)",

            "Office 365 - White",

            "Sparkle - Blue",

            "Sparkle - Blue (Dark Mode)",

            "Sparkle - Blue (Light Mode)",

            "Sparkle - Orange",

            "Sparkle - Orange (Dark Mode)",

            "Sparkle - Orange (Light Mode)",

            "Sparkle - Purple",

            "Sparkle - Purple (Dark Mode)",

            "Sparkle - Purple (Light Mode)",

            "Custom"
        };
        #endregion

        #region Instance Fields

        #endregion

        #region Properties        
        /// <summary>
        /// Gets the supported theme array.
        /// </summary>
        /// <value>
        /// The supported theme array.
        /// </value>
        public static string[] SupportedThemeArray => _supportedThemes;

        #endregion

        #region Methods
        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="manager">The manager.</param>
        private static void ApplyTheme(PaletteModeManager mode, KryptonManager manager) => manager.GlobalPaletteMode = mode;

        /// <summary>
        /// Gets the palette mode.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteModeManager"/> mode.</returns>
        public static PaletteModeManager GetPaletteMode(KryptonManager manager) => manager.GlobalPaletteMode;

        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        public static void ApplyTheme(string themeName, KryptonManager manager)
        {
            switch (themeName)
            {
                case @"Custom":
                    ApplyTheme(PaletteModeManager.Custom, manager);
                    break;
                case @"Professional - System":
                    ApplyTheme(PaletteModeManager.ProfessionalSystem, manager);
                    break;
                case @"Professional - Office 2003":
                    ApplyTheme(PaletteModeManager.ProfessionalOffice2003, manager);
                    break;
                case @"Office 2007 - Blue":
                    ApplyTheme(PaletteModeManager.Office2007Blue, manager);
                    break;
                case @"Office 2007 - Blue (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office2007BlueDarkMode, manager);
                    break;
                case @"Office 2007 - Blue (Light Mode)":
                    ApplyTheme(PaletteModeManager.Office2007BlueLightMode, manager);
                    break;
                case @"Office 2007 - Silver":
                    ApplyTheme(PaletteModeManager.Office2007Silver, manager);
                    break;
                case @"Office 2007 - Silver (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office2007SilverDarkMode, manager);
                    break;
                case @"Office 2007 - Silver (Light Mode)":
                    ApplyTheme(PaletteModeManager.Office2007SilverLightMode, manager);
                    break;
                case @"Office 2007 - White":
                    ApplyTheme(PaletteModeManager.Office2007White, manager);
                    break;
                case @"Office 2007 - Black":
                    ApplyTheme(PaletteModeManager.Office2007Black, manager);
                    break;
                case @"Office 2007 - Black (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office2007BlackDarkMode, manager);
                    break;
                case @"Office 2010 - Blue":
                    ApplyTheme(PaletteModeManager.Office2010Blue, manager);
                    break;
                case @"Office 2010 - Blue (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office2010BlueDarkMode, manager);
                    break;
                case @"Office 2010 - Blue (Light Mode)":
                    ApplyTheme(PaletteModeManager.Office2010BlueLightMode, manager);
                    break;
                case @"Office 2010 - Silver":
                    ApplyTheme(PaletteModeManager.Office2010Silver, manager);
                    break;
                case @"Office 2010 - Silver (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office2010SilverDarkMode, manager);
                    break;
                case @"Office 2010 - Silver (Light Mode)":
                    ApplyTheme(PaletteModeManager.Office2010SilverLightMode, manager);
                    break;
                case @"Office 2010 - White":
                    ApplyTheme(PaletteModeManager.Office2010White, manager);
                    break;
                case @"Office 2010 - Black":
                    ApplyTheme(PaletteModeManager.Office2010Black, manager);
                    break;
                case @"Office 2010 - Black (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office2010BlackDarkMode, manager);
                    break;
                /*case @"Office 2013":
                    ApplyTheme(PaletteModeManager.Office2013, manager);
                    break;*/
                case @"Office 2013 - White":
                    ApplyTheme(PaletteModeManager.Office2013White, manager);
                    break;
                case @"Sparkle - Blue":
                    ApplyTheme(PaletteModeManager.SparkleBlue, manager);
                    break;
                case @"Sparkle - Blue (Dark Mode)":
                    ApplyTheme(PaletteModeManager.SparkleBlueDarkMode, manager);
                    break;
                case @"Sparkle - Blue (Light Mode)":
                    ApplyTheme(PaletteModeManager.SparkleBlueLightMode, manager);
                    break;
                case @"Sparkle - Orange":
                    ApplyTheme(PaletteModeManager.SparkleOrange, manager);
                    break;
                case @"Sparkle - Orange (Dark Mode)":
                    ApplyTheme(PaletteModeManager.SparkleOrangeDarkMode, manager);
                    break;
                case @"Sparkle - Orange (Light Mode)":
                    ApplyTheme(PaletteModeManager.SparkleOrangeLightMode, manager);
                    break;
                case @"Sparkle - Purple":
                    ApplyTheme(PaletteModeManager.SparklePurple, manager);
                    break;
                case @"Sparkle - Purple (Dark Mode)":
                    ApplyTheme(PaletteModeManager.SparklePurpleDarkMode, manager);
                    break;
                case @"Sparkle - Purple (Light Mode)":
                    ApplyTheme(PaletteModeManager.SparklePurpleLightMode, manager);
                    break;
                case @"Office 365 - Blue":
                    ApplyTheme(PaletteModeManager.Office365Blue, manager);
                    break;
                case @"Office 365 - Blue (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office365BlueDarkMode, manager);
                    break;
                case @"Office 365 - Blue (Light Mode)":
                    ApplyTheme(PaletteModeManager.Office365BlueLightMode, manager);
                    break;
                case @"Office 365 - Silver":
                    ApplyTheme(PaletteModeManager.Office365Silver, manager);
                    break;
                case @"Office 365 - Silver (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office365SilverDarkMode, manager);
                    break;
                case @"Office 365 - Silver (Light Mode)":
                    ApplyTheme(PaletteModeManager.Office365SilverLightMode, manager);
                    break;
                case @"Office 365 - White":
                    ApplyTheme(PaletteModeManager.Office365White, manager);
                    break;
                case @"Office 365 - Black":
                    ApplyTheme(PaletteModeManager.Office365Black, manager);
                    break;
                case @"Office 365 - Black (Dark Mode)":
                    ApplyTheme(PaletteModeManager.Office365BlackDarkMode, manager);
                    break;
                default:
                    throw new ArgumentNullException(nameof(themeName));
            }

        }

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

                ApplyGlobalTheme(manager, GetPaletteMode(manager));
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Returns the palette mode manager as string.
        /// </summary>
        /// <param name="paletteModeManager">The palette mode manager.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The chosen theme as a string.</returns>
        public static string ReturnPaletteModeManagerAsString(PaletteModeManager paletteModeManager, KryptonManager manager = null)
        {
            var paletteMode = manager?.GlobalPaletteMode ?? paletteModeManager;

            return paletteMode switch
            {
                PaletteModeManager.Custom => "Custom",
                PaletteModeManager.ProfessionalSystem => "Professional - System",
                PaletteModeManager.ProfessionalOffice2003 => "Professional - Office 2003",
                PaletteModeManager.Office2007Blue => "Office 2007 - Blue",
                PaletteModeManager.Office2007BlueDarkMode => "Office 2007 - Blue (Dark Mode)",
                PaletteModeManager.Office2007BlueLightMode => "Office 2007 - Blue (Light Mode)",
                PaletteModeManager.Office2007Silver => "Office 2007 - Silver",
                PaletteModeManager.Office2007SilverDarkMode => "Office 2007 - Silver (Dark Mode)",
                PaletteModeManager.Office2007SilverLightMode => "Office 2007 - Silver (Light Mode)",
                PaletteModeManager.Office2007White => "Office 2007 - White",
                PaletteModeManager.Office2007Black => "Office 2007 - Black",
                PaletteModeManager.Office2007BlackDarkMode => "Office 2007 - Black (Dark Mode)",
                PaletteModeManager.Office2010Blue => "Office 2010 - Blue",
                PaletteModeManager.Office2010BlueDarkMode => "Office 2010 - Blue (Dark Mode)",
                PaletteModeManager.Office2010BlueLightMode => "Office 2010 - Blue (Light Mode)",
                PaletteModeManager.Office2010Silver => "Office 2010 - Silver",
                PaletteModeManager.Office2010SilverDarkMode => "Office 2010 - Silver (Dark Mode)",
                PaletteModeManager.Office2010SilverLightMode => "Office 2010 - Silver (Light Mode)",
                PaletteModeManager.Office2010White => "Office 2010 - White",
                PaletteModeManager.Office2010Black => "Office 2010 - Black",
                PaletteModeManager.Office2010BlackDarkMode => "Office 2010 - Black (Dark Mode)",
                //PaletteModeManager.Office2013 => "Office 2013",
                PaletteModeManager.Office2013White => "Office 2013 - White",
                PaletteModeManager.SparkleBlue => "Sparkle - Blue",
                PaletteModeManager.SparkleOrange => "Sparkle - Orange",
                PaletteModeManager.SparklePurple => "Sparkle - Purple",
                PaletteModeManager.Office365Blue => "Office 365 - Blue",
                PaletteModeManager.Office365BlueDarkMode => "Office 365 - Blue (Dark Mode)",
                PaletteModeManager.Office365BlueLightMode => "Office 365 - Blue (Light Mode)",
                PaletteModeManager.Office365Silver => "Office 365 - Silver",
                PaletteModeManager.Office365SilverDarkMode => "Office 365 - Silver (Dark Mode)",
                PaletteModeManager.Office365SilverLightMode => "Office 365 - Silver (Light Mode)",
                PaletteModeManager.Office365White => "Office 365 - White",
                PaletteModeManager.Office365Black => "Office 365 - Black",
                PaletteModeManager.Office365BlackDarkMode => "Office 365 - Black (Dark Mode)",
                _ => null
            };
        }

        /// <summary>
        /// Loads the custom theme.
        /// </summary>
        /// <param name="palette">The palette.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="themeFile">A custom theme file.</param>
        /// <param name="silent">if set to <c>true</c> [silent].</param>
        public static void LoadCustomTheme(KryptonPalette palette, KryptonManager manager, string themeFile = "", bool silent = false)
        {
            try
            {
                //throw new ApplicationException(@"Currently not implemented correctly");

                // Declare new instances
                palette = new KryptonPalette();

                manager = new KryptonManager();

                // Prompt user for palette definition

                // TODO: Add silent option
                if (silent)
                {
                    if (themeFile is not ("" and ""))
                    {
                        palette.Import(themeFile, silent);
                    }
                }
                else
                {
                    palette.Import();
                }

                // Set manager
                manager.GlobalPalette = palette;

                ApplyTheme(PaletteModeManager.Custom, manager);
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Returns the palette mode as string.
        /// </summary>
        /// <param name="paletteMode">The palette mode.</param>
        /// <returns></returns>
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode)
        {
            PaletteModeConverter modeConverter = new();

            return modeConverter.ConvertToString(paletteMode);
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="paletteModeManager">The palette mode manager.</param>
        public static void ApplyGlobalTheme(KryptonManager manager, PaletteModeManager paletteModeManager)
        {
            try
            {
                manager.GlobalPaletteMode = paletteModeManager;
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="mode">The theme mode.</param>
        public static void ApplyGlobalTheme(KryptonManager manager, PaletteMode mode)
        {
            try
            {
                manager.GlobalPaletteMode = (PaletteModeManager)mode;
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(KryptonComboBox target, params string[] excludePartials)
        {
            AddToCollection(target.Items, excludePartials);
        }

        private static void AddToCollection(IList target, string[] excludes)
        {
            try
            {
                foreach (var theme in SupportedThemeArray)
                {
                    if (!excludes.Any(t => theme.IndexOf(t, StringComparison.InvariantCultureIgnoreCase) > -1))
                    {
                        target.Add(theme);
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }


        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(KryptonListBox target, params string[] excludePartials)
        {
            AddToCollection(target.Items, excludePartials);
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(KryptonDomainUpDown target, params string[] excludePartials)
        {
            AddToCollection(target.Items, excludePartials);
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(ComboBox target, params string[] excludePartials)
        {
            AddToCollection(target.Items, excludePartials);
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(DomainUpDown target, params string[] excludePartials)
        {
            AddToCollection(target.Items, excludePartials);
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(ToolStripComboBox target, params string[] excludePartials)
        {
            AddToCollection(target.Items, excludePartials);
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

        /// <summary>Returns the theme array.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string[] ReturnThemeArray() => _supportedThemes;

        /// <summary>
        /// 
        /// </summary>
        public static List<string> PropagateSupportedThemeList()
        {
            try
            {
                return ReturnThemeArray().ToList();
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);
            }

            return new List<string>();
        }

        #endregion
    }
}